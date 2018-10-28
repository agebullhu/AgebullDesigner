using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    public abstract class EasyUiPageScriptCoderBase : EasyUiCoderBase
    {

        protected const string listCheckSize = @"
        $('#grid').datagrid('resize', window.o99);";
        protected const string treeCheckSize = @"
        $('#layout').layout('resize', window.o99);
        $('#grid').datagrid('resize', window.o99);";

        protected string TreeInit => @"
            var tree = Object.create(TreeExtend);
            tree.onTreeSelected = function (id, type, node) {
                page.setUrlArgs('pid=' + node.tag + '&type=' + type);
            };
            tree.initialize(page.apiPrefix + 'edit/tree');
            page.tree = tree;
            page.autoLoad = false;";

        #region 数据规则代码

        public string ValidateConfig()
        {
            if (Entity.IsUiReadOnly)
                return "";
            var code = new StringBuilder();
            var fields = Entity.PublishProperty.Where(p => !p.NoneDetails).ToArray();


            foreach (var field in fields)
            {
                if (field.InputType == null)
                    field.InputType = "easyui-textbox";
                var validType = ValidType(field, out bool required);
                if (validType.Count == 0 && !required)
                    continue;
                var type = field.InputType.Split('-').Last();
                code.Append($"        $('#{field.Name}').{type}({{");
                if (required)
                    code.Append("required:true");
                if (validType.Count > 0)
                {
                    if (required)
                        code.Append(',');
                    code.Append($"validType:[{validType.LinkToString(',')}]");
                }
                code.AppendLine("});");
            }
            return code.ToString();
        }

        internal static List<string> ValidType(PropertyConfig field, out bool required)
        {
            required = false;
            var validType = new List<string>();
            if (!field.CanUserInput || field.InputType==null)
                return validType;
            if (!field.InputType.Contains("easyui"))
                return validType;
            if (field.IsRequired)
                switch (field.CsType)
                {
                    case "string":
                    case "DateTime":
                    case "Guid":
                        required = true;
                        break;
                    default:
                        validType.Add(field.InputType.Contains("combo")
                            ? $@"'selectNoZero[\'#{field.Name}\']'"
                            : @"'noZero[0]'");
                        break;
                }
            if (field.IsLinkKey)
                return validType;
            switch (field.CsType)
            {
                case "string":
                    StringCheck(validType, field);
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
                    NumberCheck(validType, field);
                    break;
                case "DateTime":
                    DateTimeCheck(validType, field);
                    break;
            }
            return validType;
        }

        private static void DateTimeCheck(List<string> validType, PropertyConfig field)
        {
            if (field.Max == null && field.Min == null)
                return;
            validType.Add(
                $"'dateLimit[{(field.Min == null ? "null" : $"NewDate(\"{field.Min}\")")},{(field.Max == null ? "null" : $"NewDate(\"{field.Max}\")")}]'");
        }

        private static void NumberCheck(List<string> validType, PropertyConfig field)
        {
            if (field.Max == null && field.Min == null)
                return;
            switch (field.CsType)
            {
                case "int":
                case "long":
                case "uint":
                case "ulong":
                case "short":
                case "ushort":
                    validType.Add($"'numLimit[{field.Min ?? "null"},{field.Max ?? "null"}]'");
                    break;
                case "decimal":
                case "float":
                case "double":
                    validType.Add($"'floatLimit[{field.Min ?? "null"},{field.Max ?? "null"}]'");
                    break;
            }
        }

        private static void StringCheck(List<string> validType, PropertyConfig field)
        {
            if (field.Datalen <= 0 && field.Min == null || field.IsMemo || field.IsBlob)
                return;
            validType.Add($"'strLimit[{field.Min ?? "0"},{field.Datalen}]'");
        }

        #endregion

        protected string Gridjs()
        {
            if (Entity.Interfaces == null)
                return @"
        grid.auditData = false;
        grid.historyData = false;
        grid.stateData = false;";
            if (Entity.Interfaces.Contains("IAuditData"))
                return @"
        grid.auditData = true;
        grid.historyData = true;
        grid.stateData = true;";
            if (Entity.Interfaces.Contains("IHistoryData"))
                return @"
        grid.auditData = false;
        grid.historyData = true;
        grid.stateData = true;";
            if (Entity.Interfaces.Contains("IStateData"))
                return @"
        grid.auditData = false;
        grid.historyData = false;
        grid.stateData = true;";
            return @"
        grid.auditData = false;
        grid.historyData = false;
        grid.stateData = false;";
        }

        /// <summary>
        ///     生成初始化脚本
        /// </summary>
        protected string InputInitCode()
        {
            var code = new StringBuilder();
            foreach (var combo in Entity.ClientProperty.Where(p => !p.IsLinkKey && !string.IsNullOrWhiteSpace(p.ComboBoxUrl)))
            {
                code.Append($@"
        //{combo.Caption}下拉列表
        comboRemote('#{combo.JsonName}', '{combo.ComboBoxUrl}');");
        }

            foreach (var combo in Entity.ClientProperty.Where(p => p.IsLinkKey && !p.NoneDetails))
            {
                var entity = GlobalConfig.GetEntity(combo.LinkTable);
                if (entity == null)
                    continue;
                code.Append($@"
        //{combo.Caption}下拉列表
        comboRemote('#{combo.JsonName}', '{entity.Parent.Abbreviation ?? entity.Parent.Name}/{entity.Abbreviation ?? entity.Name}/v1/edit/combo');");
            }
            foreach (var money in Entity.ClientProperty.Where(p => p.IsMoney))
                code.Append($@"
        //{money.Caption}动态大写金额显示
        $('#{money.JsonName}').textbox({{
            onChange: function (am) {{
                $('#cm_{money.JsonName}').text(ChinessMoney(am));
            }}
        }});");
            return code.ToString();
        }


        /// <summary>
        ///     生成表格字段页面
        /// </summary>
        protected string GridFields()
        {
            var code = new StringBuilder();
            //        code.Append(@"
            //[
            //    [
            //         { halign: 'center', align: 'center', title: '-', colspan: 2 }");
            //        var columns = this.Entity.ClientProperty.Where(p => !p.IsPrimaryKey && !p.IsMemo && !p.IsSystemField).ToArray();
            //        var groups = columns.Where(p => !string.IsNullOrWhiteSpace(p.Group)).ToArray();
            //        foreach (var group in groups.GroupBy(p => p.Group))
            //        {
            //            code.Append($@"
            //        ,{{ halign: 'center', align: 'center', title: '{group.Key}', colspan: {group.Count()} }}");
            //        }
            //        var noGroup = columns.Where(p => string.IsNullOrWhiteSpace(p.Group)).ToArray();
            //        if (noGroup.Length > 0)
            //        {
            //            code.Append($@"
            //        ,{{ halign: 'center', align: 'center', title: '其它', colspan: {noGroup.Length} }}");
            //        }
            //        var memos = this.Entity.ClientProperty.Where(p => p.IsMemo).ToArray();
            //        if (memos.Length > 0)
            //        {
            //            code.Append($@"
            //        ,{{ halign: 'center', align: 'center', title: '备注', colspan: {memos.Length} }}");
            //        }
            //        code.Append(
            //            $@"
            //   ],");
            code.Append($@"
    [
       [
            {{ styler: vlStyle, halign: 'center', align: 'center', field: 'IsSelected', checkbox: true}}
            //, {{ styler: vlStyle, halign: 'center', align: 'center', sortable: true, field: '{Entity.PrimaryColumn.JsonName}', title: 'ID'}}");

            if (Entity.Interfaces != null)
                if (Entity.Interfaces.Contains("IAuditData"))
                    code.Append(@"
            , { styler: vlStyle, halign: 'center', align: 'center', sortable: true, field: 'AuditState', title: '状态', formatter: auditIconFormat }");
                else if (Entity.Interfaces.Contains("IStateData"))
                    code.Append(@"
            , { styler: vlStyle, halign: 'center', align: 'center', sortable: true, field: 'DataState', title: '状态', formatter: dataStateIconFormat }");
            foreach (var field in Entity.UserProperty.Where(p => !p.IsPrimaryKey && !p.IsLinkKey && !p.NoneGrid))
                GridField(code, field);
            //foreach (var group in groups.GroupBy(p => p.Group))
            //{
            //    GridFields(code, group);
            //}
            //GridFields(code, noGroup);
            //GridFields(code, memos);
            code.Append(@"
        ]
    ]");
            return code.ToString();
        }


        /// <summary>
        ///     生成列表字段
        /// </summary>
        /// <param name="code"></param>
        /// <param name="field"></param>
        protected static void GridField(StringBuilder code, PropertyConfig field)
        {
            var align = string.IsNullOrWhiteSpace(field.GridAlign) ? "left" : field.GridAlign;
            var extend = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(field.DataFormater))
            {
                extend .Append($@", formatter: {field.DataFormater}");
            }
            else if (field.CsType == "bool")
            {
                extend.Append(", formatter: yesnoFormat");
            }
            else if (field.CsType == "DateTime")
            {
                extend.Append(", formatter: dateFormat");
            }
            else if (!string.IsNullOrWhiteSpace(field.CustomType))
            {
                extend.Append($", formatter: {field.CustomType.ToLWord()}Format");
            }
            else if (field.IsMoney) //unixDateFormat
            {
                extend.Append(@", formatter: moneyFormat");
                align = "right";
            }
            else if (!string.IsNullOrWhiteSpace(field.Prefix) || !string.IsNullOrWhiteSpace(field.Suffix))
            {
                extend.Append($@", formatter: function(value, row) {{
                    return '{field.Prefix}' + value + '{field.Suffix}';
                }}");
            }

            if (field.GridWidth > 0)
            {
                extend.Append($@" , width:{field.GridWidth}");
            }
            code.Append($@"
            , {{ styler: vlStyle,halign: 'center', align: '{align}', sortable: true, field: '{field.JsonName}', title: '{field.Caption}'{extend}}}");
        }

        protected string CommandJsCode()
        {
            var code = new StringBuilder();
            foreach (var cmd in Entity.Commands.Where(p => !p.IsDiscard))
            {
                if (!cmd.IsSingleObject)
                    code.Append($@"
        me.grid.bySelectButtons.push('#{cmd.Button}');");
                code.Append($@"
        createRoleButton('{cmd.Caption}','{cmd.Name}', '#{cmd.Button}', '{
                        cmd.Icon
                    }', function(){{me.do{cmd.Name}();}});");
            }

            return code.ToString();
        }

        protected string CommandJsCode2()
        {
            var code = new StringBuilder();
            foreach (var cmd in Entity.Commands.Where(p => !p.IsDiscard))
            {
                if (!string.IsNullOrWhiteSpace(cmd.Url))
                {
                    code.Append($@"
    /**
    * {cmd.Caption}:{cmd.Description}
    */
    do{cmd.Name} : function () {{
        var me = this;
        var id = me.grid.selectData.{Entity.PrimaryColumn.JsonName};
        location.href='{cmd.Url}?pid=' + id;
    }},");
                    continue;
                }
                code.Append($@"
    /**
    *  {cmd.Caption}:{cmd.Description}
    */
    do{cmd.Name} : function () {{
        var me = this;");
                if (cmd.IsSingleObject)
                {
                    code.Append(cmd.IsLocalAction
                        ? $@"
        /*me.grid.doSingle('{cmd.Caption}',null,function (id,row,tag){{
            //TO DO:{cmd.Caption}的操作
        }});*/
        /*$.messager.confirm('{cmd.Caption}', '确定执行<B>{cmd.Caption}</B>操作吗?', function (s) {{
            if (s) {{
                $.messager.alert('{cmd.Caption}', 'succeed');
                ajaxOperator('{cmd.Caption}', 'Action.aspx', {{action:'{cmd.Name.ToLWord()}'}}', function (res) {{
                    if (res.succeed)
                        me.grid.execGridMethod('reload');
                    else
                        $.messager.alert('{cmd.Caption}', res.message);
                }});
            }}
        }});*/"
                        : $@"
        me.grid.doSingleRemote('{cmd.Caption}', 'Action.aspx', '{cmd.Name.ToLWord()}');");
                }
                else
                {
                    code.Append(cmd.IsLocalAction
                        ? $@"
        /*me.grid.doMulit('{cmd.Caption}',null,function (ids){{
            //TO DO:{cmd.Caption}的操作
        }});*/
        /*$.messager.confirm('{cmd.Caption}', '确定执行<B>{cmd.Caption}</B>操作吗?', function (s) {{
            if (s) {{
                $.messager.alert('{cmd.Caption}', 'succeed');
                ajaxOperator('{cmd.Caption}', me.apiPrefix + '{cmd.Name.ToLWord()}',{{}}, function (res) {{
                    if (res.success)
                        me.grid.reload();
                    else if(res.status)
                        $.messager.alert('{cmd.Caption}', res.status.msg);
                }});
            }}
        }});*/"
                        : $@"
        me.grid.doRemote('{cmd.Caption}', me.apiPrefix + '{cmd.Name.ToLWord()}');");
                }
                code.Append(@"               
    },");
            }

            return code.ToString();
        }


        protected string QueryParams()
        {
            if (Entity.Interfaces == null)
                return null;
            if (Entity.Interfaces.Contains("IAuditData"))
                return ",audit: $('#qAudit').combobox('getValue')";
            if (Entity.Interfaces.Contains("IStateData"))
                return ",dataState: $('#qAudit').combobox('getValue')";
            return null;
        }

        protected string InitQueryParams()
        {
            if (Entity.Interfaces == null)
                return null;
            if (Entity.Interfaces.Contains("IAuditData"))
                return @"
        if (!preQueryArgs.audit || preQueryArgs.audit > 0x100)
            preQueryArgs.audit = 0x100;
        $('#qAudit').combobox('setValue', preQueryArgs.audit); ";
            if (Entity.Interfaces.Contains("IStateData"))
                return @"
        if (!preQueryArgs.dataState|| preQueryArgs.dataState > 0x100)
            preQueryArgs.dataState = 0x100;
        $('#qAudit').combobox('setValue', preQueryArgs.dataState); ";
            return null;
        }


        protected string GridDetailsScript()
        {
            var code = new StringBuilder();
            if (Entity.Interfaces != null)
            {
                if (!Entity.Interfaces.Contains("IStateData"))
                    code.Append(@"
        grid.stateData = false;");
                if (!Entity.Interfaces.Contains("IHistoryData"))
                    code.Append(@"
        grid.historyData = false;");
                if (!Entity.Interfaces.Contains("IAuditData"))
                    code.Append(@"
        grid.auditData = false;");
            }
            if (Entity.ListDetails)
                code.Append($@"
        grid.isListDetails = true;
        grid.onExpandRow = function (index, row) {{
            $('#row-d-' + row.{Entity.PrimaryColumn.JsonName}).panel({{
                border: false,
                cache: false,
                href: 'details.aspx?id=' + row.{Entity.PrimaryColumn.JsonName},
                onLoad: function () {{
                    $(me.gridId).datagrid('fixDetailRowHeight', index);
                    $(me.gridId).datagrid('selectRow', index);
                }}
            }});
            $(me.gridId).datagrid('fixDetailRowHeight', index);
            $(me.gridId).datagrid('selectRow', index);
        }};");
            if (Entity.NoSort)
                code.Append($@"
        var options = grid.getGridOption();
        options.sortName = '{Entity.PrimaryColumn.JsonName}';
        options.sortOrder = 'asc';");

            return code.ToString();
        }

    }
}