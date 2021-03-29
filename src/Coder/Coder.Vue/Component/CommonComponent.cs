using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.VueComponents
{

    public class CommonComponent
    {
        #region JS

        public static string JsCode(IEntityConfig model)
        {
            var com = new CommonComponent
            {
                model = model
            };
            return com.ScriptCode();
        }
        #endregion

        IEntityConfig model;

        public string ScriptCode()
        {
            return $@"EntityComponents.components['{model.Project.PageFolder}-{model.PageFolder}'].common = {{
    default: {{
        idField : '{model.PrimaryColumn.JsonName}',
        apiPrefix : '/{model.Project.ApiName}/{model.ApiName}/v1'
    }},{Options().LinkToString(',')}
}};";
        }


        #region 构建节点

        readonly List<string> types = new List<string>();

        readonly List<string> filters = new List<string>();

        readonly List<string> datas = new List<string>();


        readonly List<string> overrides = new List<string>();


        List<string> Options()
        {
            ArgumentMethod();
            DataCheckMethod();
            SetData();
            Rules();
            Combos();
            EnumScript();
            Filter();

            List<string> options = new List<string>();

            void Join(List<string> codes, string fmt, bool canEmpty = true)
            {
                if (canEmpty && codes.Count == 0)
                    return;
                options.Add(string.Format(fmt, string.Join(',', codes)));
            }

            Join(datas, @"
    data:{{{0}
    }}");
            Join(types, @"
    types:{{{0}
    }}");
            Join(rules, @"
    rules:{{{0}
    }}");
            Join(filters, @"
    filters:{{{0}
    }}");
            Join(overrides, @"
    overrides:{{{0}
    }}");
            return options;
        }
        #endregion

        #region 下拉列表支持

        /// <summary>
        /// 下拉列表支持
        /// </summary>
        /// <returns></returns>
        void Combos()
        {
            var array = model.ClientProperty
                .Where(p => p.UserSee && !p.NoStorage && p.DataBaseField.IsLinkKey)
                .Select(p => GlobalConfig.GetEntity(p.DataBaseField.LinkTable))
                .Distinct().ToArray();
            if (array.Length == 0)
                return;
            foreach (var entity in array.Where(p => p.CaptionColumn != null))
            {
                var comboName = entity.Name.ToLWord().ToPluralism();
                datas.Add($@"
        {comboName}:[]");
                filters.Add($@"
        //{entity.Caption}:主键到标题
        {entity.Name.ToLWord()}Formater(val,data_self) {{
            var obj = data_self.combos.{comboName}.find((n) => n.id == val);
            return obj ? obj.text : '-';
        }}");
            }
        }
        #endregion

        #region 枚举

        void EnumScript()
        {
            var enums = new List<EnumConfig>();
            enums.AddRange(model.Properties.Where(p => p.EnumConfig != null).Select(p => p.EnumConfig));
            if (model is ModelConfig mc)
            {
                foreach (var ch in mc.Releations)
                {
                    enums.AddRange(ch.ForeignEntity.Properties.Where(p => p.EnumConfig != null).Select(p => p.EnumConfig));
                }
            }
            if (enums.Count == 0)
                return;

            foreach (var enumConfig in enums.Distinct())
            {
                var code = new StringBuilder();
                code.Append($@"
        //{enumConfig.Caption}
        {enumConfig.Name.ToLWord()} : [");
                bool first = true;
                foreach (var item in enumConfig.Items)
                {
                    if (first)
                        first = false;
                    else
                        code.Append(",");
                    code.Append($@"{{
            key: '{item.Value}', value: '{item.Name}', label: '{item.Caption}'
        }}");
                }
                code.Append(@"]");
                types.Add(code.ToString());
            }
        }

        void Filter()
        {
            var enums = new List<EnumConfig>();
            enums.AddRange(model.ClientProperty.Where(p => p.EnumConfig != null).Select(p => p.EnumConfig));
            if (model is ModelConfig mc)
            {
                foreach (var ch in mc.Releations)
                {
                    enums.AddRange(ch.ForeignEntity.Properties.Where(p => p.EnumConfig != null).Select(p => p.EnumConfig));
                }
            }
            foreach (var enu in enums.Distinct())
            {
                filters.Add(Filter(enu));
            }
        }
        string Filter(EnumConfig config)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"
        //{config.Caption}枚举转文本
        {config.Name.ToLWord()}Formater(val) {{
            if(!val)return '-';
            switch (val) {{");
            string def = "错误";
            foreach (var item in config.Items)
            {
                if (item.Value == "0")
                    def = item.Caption;
                code.Append($@"
                case '{item.Name}': return '{item.Caption}';");
            }
            code.Append($@"
                default: return '{def}';
            }}
        }}");
            return code.ToString();
        }
        #endregion

        #region 规则

        readonly List<string> rules = new List<string>();
        /// <summary>
        ///     生成Form录入字段界面
        /// </summary>
        /// <param name="columns"></param>
        private void Rules()
        {
            if (model.IsUiReadOnly)
                return;
            var columns = model.ClientProperty.Where(p => !p.IsUserReadOnly);
            foreach (var property in columns)
            {
                var field = property;
                var sub = new StringBuilder();
                var dot = "";
                if (field.UiRequired)
                {
                    sub.Append($@"{dot}{{ required: true, message: '请输入{property.Caption}', trigger: 'blur' }}");
                    dot = @",";
                }
                switch (field.CsType)
                {
                    case "string":
                        StringCheck(field, sub, ref dot);
                        break;
                    case "int":
                    case "long":
                    case "uint":
                    case "ulong":
                    case "short":
                    case "ushort":
                    case "decimal":
                    case "float":
                    case "double":
                        NumberCheck(field, sub, ref dot);
                        break;
                    case "DateTime":
                        DateTimeCheck(field, sub, ref dot);
                        break;
                }
                if (sub.Length == 0)
                    continue;
                rules.Add($@"
            '{property.JsonName}' : [{sub}]");
            }
        }
        private static void DateTimeCheck(IPropertyConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max.IsPresent() && field.Min.IsPresent())
                code.Append($@"{dot}{{ min: {field.Min}, max: {field.Max}, message: '时间从 {field.Min} 到 {field.Max} 之间', trigger: 'blur' }}");
            else if (field.Max.IsPresent())
                code.Append($@"{dot}{{ max: {field.Max}, message: '时间不大于 {field.Max}', trigger: 'blur' }}");
            else if (field.Min.IsPresent())
                code.Append($@"{dot}{{ min: {field.Min}, message: '时间不小于 {field.Min}', trigger: 'blur' }}");
            else return;
            dot = @",";
        }

        private static void NumberCheck(IPropertyConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max.IsPresent() && field.Min.IsPresent())
                code.Append($@"{dot}{{ min: {field.Min}, max: {field.Max}, message: '数值从 {field.Min} 到 {field.Max} 之间', trigger: 'blur' }}");
            else if (field.Max.IsPresent())
                code.Append($@"{dot}{{ max: {field.Max}, message: '数值不大于 {field.Max}', trigger: 'blur' }}");
            else if (field.Min.IsPresent())
                code.Append($@"{dot}{{ min: {field.Min}, message: '数值不小于 {field.Min}', trigger: 'blur' }}");
            else return;
            dot = @",";
        }

        private static void StringCheck(IPropertyConfig property, StringBuilder code, ref string dot)
        {
            if (property.Max.IsPresent() && property.Min.IsPresent())
                code.Append($@"{dot}{{ min: {property.Min}, max: {property.Max}, message: '长度在 {property.Min} 到 {property.Max} 个字符', trigger: 'blur' }}");
            else if (property.Max.IsPresent())
                code.Append($@"{dot}{{ max: {property.Max}, message: '长度不大于 {property.Max} 个字符', trigger: 'blur' }}");
            else if (property.Min.IsPresent())
                code.Append($@"{dot}{{ min: {property.Min}, message: '长度不小于 {property.Min} 个字符', trigger: 'blur' }}");
            else return;
            dot = @",";
        }

        #endregion

        #region 数据定义
        void SetData()
        {
            var order = model.Find(p => p.Name == model.OrderField);
            if (order != null)
                datas.Append($@"
            list:{{
                sort : '{order.JsonName}',
                order : '{(model.OrderDesc ? "desc" : "asc")}'
            }}");
        }
        #endregion

        #region 参数

        void ArgumentMethod()
        {
            if (model.FormQuery)
            {
                var code = new StringBuilder();

                code.Append(@"
        emptyQuery () {
            return {");
                var first = true;
                foreach (var property in model.ClientProperty.Where(p => p.CanUserQuery).ToArray())
                {
                    if (first) first = false;
                    else code.Append(',');

                    code.Append($@"
                {property.JsonName} : ''");
                }
                code.Append(@"
            };
        }");
                overrides.Add(code.ToString());
            }
            {
                var code = new StringBuilder();
                code.Append(@"
        copyQuery(arg, filter) {");
                /*
                if (!Model.FormQuery)
                    code.Append(@"
                arg.field = filter.field;
                arg.keyWords = filter.keyWords;");
                if (Model.Interfaces != null)
                {
                    if (Model.Interfaces.Contains("IStateData"))
                        code.Append(@"
                arg.dataState = filter.dataState;");
                    if (Model.Interfaces.Contains("IAuditData"))
                        code.Append(@"
                arg.audit = filter.audit;");
                }*/
                if (model.TreeUi)
                {
                    if (model.ParentColumn == null)
                        code.Append(@"
                arg.pid = this.tree.pid;
                arg.type = this.tree.type;");
                    else
                        code.Append($@",
                arg.{model.ParentColumn.JsonName}: this.tree.pid,
                arg.type: this.tree.type");
                }

                if (model.FormQuery)
                {
                    foreach (var property in model.ClientProperty.Where(p => p.CanUserQuery).ToArray())
                    {
                        code.Append($@"
            if(filter.{property.JsonName})
                arg.{property.JsonName} = filter.{property.JsonName};");
                    }
                }
                code.Append(@"
            return arg;
        }");
                overrides.Add(code.ToString());
            }
        }
        #endregion
        #region 数据

        void DataCheckMethod()
        {
            overrides.Add($@"
        createData() {{
            return {{{DefaultValue()}
            }};;
        }},
        copyData(row) {{
            let data = {{}};{CopyValue()}
            return data;
        }},
        refillData(row) {{{CheckValue()}
        }}");
        }

        /// <summary>
        ///     缺省空对象
        /// </summary>
        private string DefaultValue()
        {
            bool isInner = model.Interfaces.Contains("IInnerTree");
            var code = new StringBuilder();
            bool first = true;
            foreach (var property in model.ClientProperty.Where(p => !p.IsSystemField && !p.NoneJson))
            {
                if (first) first = false;
                else code.Append(',');
                var field = property;
                if (isInner && field.Name == "ParentId")
                {
                    if (!model.TreeUi)
                        code.Append($@"
                {property.JsonName} : !this.currentRow ? 0 : this.currentRow.{model.PrimaryColumn.JsonName}");
                    else
                        code.Append($@"
                {property.JsonName} : !this.tree || this.tree.pid ? 0 : this.tree.pid");
                }
                else if (field.CsType == "string" || field.CsType == nameof(DateTime) || field.Nullable)
                {
                    code.Append($@"
                {property.JsonName} : ''");
                }
                else if (field.IsEnum)
                {
                    code.Append($@"
                {property.JsonName} : '{field.EnumConfig?.Items.FirstOrDefault()?.Name}'");
                }
                else if (field.CsType == "bool")
                {
                    code.Append($@"
                {property.JsonName} : false");
                }
                else
                {
                    code.Append($@"
                {property.JsonName} : 0");
                }
            }
            return code.ToString();
        }

        /// <summary>
        ///     检查字段存在
        /// </summary>
        private string CheckValue()
        {
            var code = new StringBuilder();
            foreach (var property in model.ClientProperty.Where(p => !p.IsSystemField && !p.NoneJson))
            {
                var field = property;
                code.Append($@"
            if (typeof row.{property.JsonName} === 'undefined') row.{property.JsonName} = ");
                if (field.Nullable)
                {
                    code.Append("null;");
                }
                else if (field.CsType == "string" || field.CsType == nameof(DateTime))
                {
                    code.Append("'';");
                }
                else if (field.CsType == "bool")
                {
                    code.Append("false;");
                }
                else if (field.IsEnum)
                {
                    code.Append($"'{field.EnumConfig?.Items.FirstOrDefault()?.Name}';");
                }
                else
                {
                    code.Append("0;");
                }
            }
            return code.ToString();
        }

        /// <summary>
        ///     复制字段
        /// </summary>
        private string CopyValue()
        {
            var code = new StringBuilder();
            foreach (var property in model.ClientProperty.Where(p => !p.IsSystemField && !p.NoneJson))
            {
                var field = property;
                code.Append($@"
            if (typeof row.{property.JsonName} === 'undefined') data.{property.JsonName} = ");
                if (field.Nullable)
                {
                    code.Append("null;");
                }
                else if (field.CsType == "string" || field.CsType == nameof(DateTime))
                {
                    code.Append("'';");
                }
                else if (field.CsType == "bool")
                {
                    code.Append("false;");
                }
                else if (field.IsEnum)
                {
                    code.Append($"'{field.EnumConfig?.Items.FirstOrDefault()?.Name}';");
                }
                else
                {
                    code.Append("0;");
                }
                code.Append($@" else data.{property.JsonName} = row.{property.JsonName};");
            }
            return code.ToString();
        }
        #endregion
    }
}