using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.VUE
{

    public partial class VueScriptCoder
    {
        public IEntityConfig Model { get; set; }
        public ProjectConfig Project { get; set; }

        public string ScriptCode()
        {
            Init();

            List<string> options = new List<string>();

            void Join(List<string> codes, string fmt, bool canEmpty = true)
            {
                if (canEmpty && codes.Count == 0)
                    return;
                options.Add(string.Format(fmt, string.Join(',', codes)));
            }

            Join(readys, @"
    onReady(v){{{0}
        v.loadList();
    }}", false);
            Join(datas, @"
    data:{{{0}
    }}");
            Join(commands, @"
    commands:{{{0}
    }}");
            Join(forms, @"
    form:{{{0}
    }}");
            Join(rules, @"
    rules:{{{0}
    }}");
            Join(types, @"
    types:{{{0}
    }}");
            Join(filters, @"
    filters:{{{0}
    }}");
            Join(overrides, @"
    overrides:{{{0}
    }}");
            return $@"extend_vue_option({{{options.LinkToString(',')}
}});";
        }


        #region 构建节点

        readonly List<string> types = new List<string>();

        readonly List<string> filters = new List<string>();

        readonly List<string> datas = new List<string>();

        readonly List<string> commands = new List<string>();
        readonly List<string> overrides = new List<string>();
        readonly List<string> readys = new List<string>();
        readonly List<string> forms = new List<string>();

        void Init()
        {
            ArgumentMethod();
            DataCheckMethod();
            SetData();
            Rules();
            LinkFunctions();
            EnumScript();
            Filter();
            TreeMethod();
            DetailsPage();
            Command();
        }
        #endregion

        #region 下拉列表支持

        /// <summary>
        /// 下拉列表支持
        /// </summary>
        /// <returns></returns>
        void LinkFunctions()
        {
            var array = Model.LastProperties
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
                readys.Add($@"
        ajax_load('载入{entity.Caption}','/{entity.Project.ApiName}/{entity.ApiName}/v1/edit/combo',null, d => v.combos.{comboName} = d);");
                filters.Add($@"
        //{entity.Caption}:主键到标题
        {entity.Name.ToLWord()}Formater(val) {{
            var obj = vue_option.data.combos.{comboName}.find((n) => n.id == val);
            return obj ? obj.text : '-';
        }}");
            }
        }
        #endregion

        #region 枚举

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
            enums.AddRange(Model.LastProperties.Where(p => p.EnumConfig != null).Select(p => p.EnumConfig));
            if (Model is ModelConfig model)
            {
                foreach (var ch in model.Releations)
                {
                    enums.AddRange(ch.ForeignEntity.LastProperties.Where(p => p.EnumConfig != null).Select(p => p.EnumConfig));
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
            if (Model.IsUiReadOnly)
                return;
            var columns = Model.ClientProperty.Where(p => !p.IsUserReadOnly);
            foreach (var property in columns)
            {
                var field = property;
                var sub = new StringBuilder();
                var dot = "";
                if (field.IsRequired && !field.IsUserReadOnly)
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
            datas.Add($@"
        idField : '{Model.PrimaryColumn.JsonName}'");
            datas.Add($@"
        apiPrefix : '/{Project.ApiName}/{Model.ApiName}/v1'");
        }
        #endregion

        #region 扩展方法

        #region 参数

        void ArgumentMethod()
        {
            if (Model.FormQuery)
            {
                var code = new StringBuilder();
                code.Append(@"
        query : {");
                bool first = true;
                foreach (var property in Model.Where(p => p.CanUserQuery).ToArray())
                {
                    if (first) first = false;
                    else code.Append(',');

                    code.Append($@"
            {property.JsonName} : ''");
                }
                code.Append(@"
        }");
                datas.Add(code.ToString());
                code.Clear();
                code.Append(@"
        clearQuery () {
            this.query={");
                first = true;
                foreach (var property in Model.Where(p => p.CanUserQuery).ToArray())
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
        getQueryArgs() {
            let args = {
                _page_: this.list.page,
                _size_: this.list.pageSize");

                var order = Model.Find(p => p.Name == Model.OrderField);
                if (order != null)
                    code.Append($@",
                _sort_: '{order.JsonName}',
                _order_: '{(Model.OrderDesc ? "desc" : "asc")}'");

                if (Model.Interfaces != null)
                {
                    if (Model.Interfaces.Contains("IStateData"))
                        code.Append(@",
                _state_: this.list.dataState");
                    if (Model.Interfaces.Contains("IAuditData"))
                        code.Append(@",
                _audit_: this.list.audit");
                }
                if (Model.TreeUi)
                {
                    if (Model.ParentColumn == null)
                        code.Append(@",
                pid: this.tree.pid,
                type: this.tree.type");
                    else
                        code.Append($@",
                {Model.ParentColumn.JsonName}: this.tree.pid,
                type: this.tree.type");
                }

                if (!Model.FormQuery)
                    code.Append(@",
                _field_: this.list.field,
                _value_: this.list.keyWords");

                code.Append(@"
            };");
                if (Model.FormQuery)
                {
                    foreach (var property in Model.Where(p => p.CanUserQuery).ToArray())
                    {
                        code.Append($@"
            if(this.query.{property.JsonName})
                args.{property.JsonName} = this.query;");
                    }
                }
                code.Append(@"
            return args;
        }");
                overrides.Add(code.ToString());
            }

            if (!Model.TreeUi)
                return;
            if (Model.ParentColumn == null)
                overrides.Add($@"
        setArgument(arg) {{
            arg.pid = this.tree.pid;
            arg.type = this.tree.type;
        }}");
            else
                overrides.Add($@"
        setArgument(arg) {{
            arg.{Model.ParentColumn.JsonName} = this.tree.pid;
            arg.type = this.tree.type;
        }}");
        }
        #endregion
        #region 数据

        void DataCheckMethod()
        {
            overrides.Add($@"
        createNew() {{
            let newData= {{{DefaultValue()}
            }};
            return newData;
        }},
        copyData(row) {{
            let data = {{}};{CopyValue()}
            return data;
        }},
        toFullData(row) {{{CheckValue()}
        }}");
        }

        /// <summary>
        ///     缺省空对象
        /// </summary>
        private string DefaultValue()
        {
            bool isInner = Model.Interfaces.Contains("IInnerTree");
            var code = new StringBuilder();
            bool first = true;
            foreach (var property in Model.LastProperties.Where(p => !p.IsSystemField && !p.NoneJson))
            {
                if (first) first = false;
                else code.Append(',');
                var field = property;
                if (isInner && field.Name == "ParentId")
                {
                    if (!Model.TreeUi)
                        code.Append($@"
                {property.JsonName} : !this.currentRow ? 0 : this.currentRow.{Model.PrimaryColumn.JsonName}");
                    else
                        code.Append($@"
                {property.JsonName} : !this.tree.pid ? 0 : this.tree.pid");
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
            foreach (var property in Model.LastProperties.Where(p => !p.IsSystemField && !p.NoneJson))
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
                    code.Append($"'{field.EnumConfig.Items.FirstOrDefault()?.Name}';");
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

        #region 树

        void TreeMethod()
        {
            if (!Model.TreeUi)
                return;
            commands.Add(@"
        onTreeNodeChanged(data, node) {
            this.tree.pid = data.id;
            this.tree.type = data.type;
            this.tree.node = node;
            this.tree.current = data;
            this.loadList();
        }");
            datas.Add(@"
        tree: {
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
            readys.Add(@"
        ajax_load('导航数据', `${v.apiPrefix}/edit/tree`, { pid: 0, type: 'root' }, d => v.tree.nodes = d);");
        }

        #endregion
        #region 命令

        void Command()
        {
            foreach (var cmd in Model.Commands)
            {
                if (cmd.IsSingleObject)
                    commands.Add($@"
        {cmd.JsMethod}(id) {{
            confirmCall('{cmd.Caption}', `${{this.apiPrefix}}/{cmd.Api}`, {{id: id}}, res => true, '确认要执行【{cmd.Description}】操作吗？')
        }}");
                else if (cmd.IsMulitOperator)
                    commands.Add($@"
        {cmd.JsMethod}(id) {{
            this.mulitSelectAction('{cmd.Caption}', '{cmd.Api}',`${{this.apiPrefix}}/{cmd.Api}`, row => true);
        }}");
                else
                    commands.Add($@"
        {cmd.JsMethod}() {{
            confirmCall('{cmd.Caption}', `${{this.apiPrefix}}/{cmd.Api}`, {{}}, res => true, '确认要执行【{cmd.Description}】操作吗？')
        }}");
            }
        }

        #endregion

        #region 详细页面

        void DetailsPage()
        {
            if (!Model.DetailsPage)
                return;
            var load = new StringBuilder();
            if (Model is ModelConfig model)
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
                    forms.Add($@"
            {name}:[]");
                    load.Append($@"
            ajax_load('{ch.Caption}','/{Project.ApiName}/{ch.ForeignEntity.ApiName}/v1/edit/list',{{{ch.ForeignField.JsonName}:row.id,_size_ : 999}},data => that.form.{name} = data.rows);");
                }
            }
            commands.Add($@"
        showList(){{
            this.form.visible=false;
        }},
        handleClick(row){{
            this.currentRow = row;
            this.doEdit();
        }},
        onEdit() {{
            var that=this;
            var row = this.currentRow;{load}
        }}");

        }
        #endregion
        #endregion
    }
}