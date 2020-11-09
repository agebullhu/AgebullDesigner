using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.VUE
{

    public partial class VueScriptCoder<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        public TModel Model { get; set; }
        public ProjectConfig Project { get; set; }

        public string ScriptCode()
        {
            Init();

            return $@"
{mothodsScript}
{filterScript}
{dataScript}
{readyScript}";

        }


        #region 初始化
        string mothodsScript, readyScript, dataScript, filterScript;

        readonly List<string> methods = new List<string>();
        readonly List<string> readys = new List<string>();
        readonly List<string> forms = new List<string>();

        public void Init()
        {
            ArgumentMethod(methods);
            DataCheckMethod(methods);
            SetData();
            LinkFunctions();
            EnumScript();
            Filter();
            TreeMethod(methods);
            DetailsPage();
            readyScript = $@"
vue_option.ready(v =>{{
    {string.Join(@",
    ", readys)}
    v.loadList();
}});";
            mothodsScript = methods.Count == 0
                ? null
                : $@"
extend_methods({{
    {string.Join(@",
    ", methods)}
}});";
            if(forms.Count > 0)
            {
                datas.Add($@"form : {{
    {string.Join(@",
        ", forms)}
    }}");
            }
            dataScript = datas.Count == 0
                ? null
                : $@"
extend_data({{
    {string.Join(@",
    ", datas)}
}});";
            filterScript = filters.Count == 0
                ? null
                : $@"
extend_filter({{
    {string.Join(@",
    ", filters)}
}});";
        }
        string Join(string fmt, string space, char letter, List<string> codes)
        {
            if (codes.Count == 0)
                return null;
            return string.Format(fmt, string.Join($@"{letter}
{space}", codes));
        }
        #endregion

        #region 下拉列表支持

        /// <summary>
        /// 下拉列表支持
        /// </summary>
        /// <returns></returns>
        void LinkFunctions()
        {
            if (Model.IsUiReadOnly)
                return;
            var array = Model.ClientProperty
                .Where(p => p.CanUserInput && p.IsLinkCaption)
                .Select(p => GlobalConfig.GetEntity(p.LinkTable))
                .Distinct().ToArray();
            if (array.Length == 0)
                return;
            StringBuilder code = new StringBuilder();
            code.AppendLine("combos:{");
            bool first = true;
            foreach (var entity in array.Where(p => p.Properties.Any(a => a.IsCaption)))
            {
                var comboName = entity.Name.ToLWord().ToPluralism();
                if (first) first = false;
                else code.Append(',');
                code.Append($@"
        {comboName}: []");
                readys.Add($"ajax_load('载入{entity.Caption}','{entity.Parent.Abbreviation}/{entity.Abbreviation}/v1/edit/combo',null, d => v.data.combos.{comboName} = d);");

            }
        }
        #endregion

        #region 枚举

        readonly List<string> filters = new List<string>();

        void EnumScript()
        {
            var enums = new List<EnumConfig>();
            enums.AddRange(Model.LastProperties.Where(p => p.EnumConfig != null).Select(p => p.EnumConfig));
            if (Model is ModelConfig model)
            {
                foreach (var ch in model.Releations)
                {
                    enums.AddRange(ch.ForeignEntity.LastProperties.Where(p => p.EnumConfig != null).Select(p => p.EnumConfig));
                }
            }

            if (enums.Count == 0)
                return;
            var code = new StringBuilder();
            code.AppendLine(@"
    /**
    *  枚举列表
    */
    types : {");
            bool enumFirst = true;
            foreach (var enumConfig in enums.Distinct())
            {
                if (enumFirst)
                    enumFirst = false;
                else
                    code.Append(",");

                code.Append($@"
        /**
        *   {enumConfig.Caption}
        */
        {enumConfig.Name.ToLWord()} : [");
                bool first = true;
                foreach (var item in enumConfig.Items)
                {
                    if (first)
                        first = false;
                    else
                        code.Append(",");
                    code.Append($@"
            {{
                key: '{item.Value}',
                value: '{item.Name}',
                label: '{item.Caption}'
            }}");
                }
                code.Append(@"
        ]");
            }
            code.Append(@"
    }");
            datas.Add(code.ToString());
        }

        void Filter()
        {
            var enums = new List<EnumConfig>();
            enums.AddRange(Model.LastProperties.Where(p => p.EnumConfig != null).Select(p => p.EnumConfig));
            if (Model is ModelConfig model)
            {
                foreach (var ch in model.Releations)
                {
                    enums.AddRange(ch.ForeignEntity.LastProperties.Where(p => p.EnumConfig != null).Select(p => p.EnumConfig));
                }
            }

            if (enums.Count == 0)
                return;
            foreach (var enu in enums.Distinct())
            {
                filters.Add(Filter(enu));
            }
        }
        string Filter(EnumConfig config)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"
    /**
    *   {config.Caption}枚举转文本
    */
    {config.Name.ToLWord()}Formater(val) {{
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
            default:
                return '{def}';
        }}
    }}");
            return code.ToString();
        }
        #endregion

        #region 规则

        /// <summary>
        ///     生成Form录入字段界面
        /// </summary>
        /// <param name="columns"></param>
        private void Rules()
        {
            if (Model.IsUiReadOnly)
                return;
            var columns = Model.ClientProperty.Where(p => p.CanUserInput);
            bool first = true;
            var code = new StringBuilder();
            foreach (var property in columns)
            {
                var field = property;
                var sub = new StringBuilder();
                var dot = "";
                if (field.IsRequired && field.CanUserInput)
                {
                    sub.Append($@"{dot}{{ required: true, message: '请输入{property.Caption}', trigger: 'blur' }}");
                    dot = @",
                ";
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
                if (first)
                    first = false;
                else
                    code.Append(',');
                code.Append($@"
            '{property.JsonName}' : [{sub}]");
            }
            if (!first)
                forms.Add($@"rules : {{{code}
        }}");
        }
        private static void DateTimeCheck(IFieldConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max != null && field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, max: {field.Max}, message: '时间从 {field.Min} 到 {field.Max} 之间', trigger: 'blur' }}");
            else if (field.Max != null)
                code.Append($@"{dot}{{ max: {field.Max}, message: '时间不大于 {field.Max}', trigger: 'blur' }}");
            else if (field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, message: '时间不小于 {field.Min}', trigger: 'blur' }}");
            else return;
            dot = @",
    ";
        }

        private static void NumberCheck(IFieldConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max != null && field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, max: {field.Max}, message: '数值从 {field.Min} 到 {field.Max} 之间', trigger: 'blur' }}");
            else if (field.Max != null)
                code.Append($@"{dot}{{ max: {field.Max}, message: '数值不大于 {field.Max}', trigger: 'blur' }}");
            else if (field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, message: '数值不小于 {field.Min}', trigger: 'blur' }}");
            else return;
            dot = @",
    ";
        }

        private static void StringCheck(IFieldConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max != null && field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, max: {field.Max}, message: '长度在 {field.Min} 到 {field.Max} 个字符', trigger: 'blur' }}");
            else if (field.Max != null)
                code.Append($@"{dot}{{ max: {field.Max}, message: '长度不大于 {field.Max} 个字符', trigger: 'blur' }}");
            else if (field.Min != null)
                code.Append($@"{dot}{{ min: {field.Min}, message: '长度不小于 {field.Min} 个字符', trigger: 'blur' }}");
            else return;
            dot = @",
    ";
        }

        #endregion

        #region 数据定义
        readonly List<string> datas = new List<string>();

        void SetData()
        {
            datas.Add($"idField : '{Model.PrimaryColumn.JsonName}'");
            datas.Add($"apiPrefix : '/{Project.ApiName}/{Model.ApiName}/v1'");
            Rules();
        }

        #endregion

        #region 扩展方法

        #region 参数

        void ArgumentMethod(List<string> methods)
        {
            string ext = "";
            var order = Model.Properties.FirstOrDefault(p => p.Name == Model.OrderField);
            if (order != null)
                ext += $@",
            _sort_: '{order.JsonName}',
            _order_: '{(Model.OrderDesc ? "desc" : "asc")}'";
            if (Model.Interfaces != null)
            {
                if (Model.Interfaces.Contains("IStateData"))
                    ext += $@",
            _state_: this.list.dataState";
                if (Model.Interfaces.Contains("IAuditData"))
                    ext += $@",
            _audit_: this.list.audit";
            }
            if (Model.TreeUi)
                ext = @",
            pid: this.tree.pid,
            type: this.tree.type";

            methods.Add($@"getQueryArgs() {{
        return {{
            _field_: this.list.field,
            _value_: this.list.keyWords,
            _page_: this.list.page,
            _size_: this.list.pageSize{ext}
        }}; 
    }}");
            if (Model.TreeUi)
                methods.Add($@"setArgument(arg) {{
        arg.pid = this.tree.pid;
        arg.type = this.tree.type;
    }}");
        }
        #endregion
        #region 数据

        void DataCheckMethod(List<string> methods)
        {
            methods.Add($@"getDef() {{
        return {{
            selected: false{DefaultValue()}
        }};
    }},
    checkListData(row) {{{CheckValue()}
    }}");
        }

        /// <summary>
        ///     缺省空对象
        /// </summary>
        private string DefaultValue()
        {
            bool isInner = Model.Interfaces.Contains("IInnerTree");
            var code = new StringBuilder();

            foreach (var property in Model.LastProperties.Where(p => !p.IsSystemField && !p.NoneJson))
            {
                var field = property;
                if (isInner && field.Name == "ParentId")
                {
                    if (!Model.TreeUi)
                        code.Append($@",
            {property.JsonName} : !this.currentRow ? 0 : this.currentRow.{Model.PrimaryColumn.JsonName}");
                    else
                        code.Append($@",
            {property.JsonName} : !this.tree.pid ? 0 : this.tree.pid");
                }
                else if (field.CsType == "string" || field.CsType == nameof(DateTime) || field.Nullable)
                {
                    code.Append($@",
            {property.JsonName} : ''");
                }
                else if (field.IsEnum)
                {
                    code.Append($@",
            {property.JsonName} : '{field.EnumConfig.Items.FirstOrDefault()?.Name}'");
                }
                else if (field.CsType == "bool")
                {
                    code.Append($@",
            {property.JsonName} : false");
                }
                else
                {
                    code.Append($@",
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
            foreach (var property in Model.LastProperties.Where(p => !p.IsSystemField && !p.NoneJson))
            {
                var field = property;
                code.Append($@"
        if (typeof row.{property.JsonName} === 'undefined')
            row.{property.JsonName} = ");
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
                    code.Append($"'{field.EnumConfig.Items.FirstOrDefault()?.Name}';");
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
            foreach (var property in Model.LastProperties.Where(p => !p.IsSystemField && !p.NoneJson))
            {
                var field = property;
                code.Append($@"
        if (typeof row.{property.JsonName} === 'undefined')
            data.{property.JsonName} = ");
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
                    code.Append($"'{field.EnumConfig.Items.FirstOrDefault()?.Name}';");
                }
                else
                {
                    code.Append("0;");
                }
                code.Append($@"
        else
            data.{property.JsonName} = row.{property.JsonName};");
            }
            return code.ToString();
        }
        #endregion

        #region 树

        void TreeMethod(List<string> methods)
        {
            if (!Model.TreeUi)
                return;
            methods.Add(@"onTreeNodeChanged(data, node) {
        this.tree.pid = data.id;
        this.tree.type = data.type;
        this.tree.node = node;
        this.tree.current = data;
        this.loadList();
    }");
            datas.Add(@"tree: {
        pid: 0,
        type: 'root',
        node: null,
        current: null,
        nodes: [],
        props: {
            label: 'label',
            children: 'children',
            isLeaf: 'isLeaf'
        }
    }");
            readys.Add("ajax_load('导航数据', `${v.apiPrefix}/edit/tree`, { pid: 0, type: 'root' }, d => v.tree.nodes = d);");
        }

        #endregion
        void DetailsPage()
        {
            if (!Model.DetailsPage)
                return;
            var load =new StringBuilder();
            if(Model is ModelConfig model)
            {
                foreach (var ch in model.Releations)
                {
                    switch (ch.JoinType)
                    {
                        case EntityJoinType.none:
                            break;
                        case EntityJoinType.Inner:
                            continue;
                        case EntityJoinType.Left:
                            continue;
                    }
                    var name = ch.Name.ToLWord().ToPluralism();
                    forms.Add($"{name}:[]");
                    load.Append($@"
        ajax_load('{ch.Caption}','/{Project.ApiName}/{ch.ForeignEntity.ApiName}/v1/edit/list',{{{ch.ForeignField.JsonName}:row.id}},data => that.form.{name} = data.rows);");
                }
            }
            methods.Add($@"
    showList(){{
        this.form.visible=false;
    }},
    handleClick(row){{
        var that=this;
        this.form.edit=true;
        this.form.visible=true;
        this.form.data=row;{load}
    }}");

        }
        #endregion
    }
}