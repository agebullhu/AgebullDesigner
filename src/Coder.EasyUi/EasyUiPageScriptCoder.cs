using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class EasyUiPageScriptCoder : EasyUiCoderBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Aspnet_Script_js";

        #region Form.js

        public string FormJsCode()
        {
            return $@"/*
    {Entity.Caption}的前端操作类对象,实现基本的编辑的界面操作
*/
var {Entity.Name}Form = {{
    /**
     * 主键字段
     */
    idField : '{Entity.PrimaryColumn.Name}',
    /**
     * 标题
     */
    title:'{Entity.Caption}',
    /**
     * 命令执行地址前缀
     */
    cmdPath: '',
    /**
     * 表单地址
     */
    formUrl: 'Form.htm',
    /**
     * 表单参数
     */
    formArg: '_-_=1',
    /*
        新增一条项目洽谈的界面操作
    */
    addNew: function () {{
        var dialog = new EditDialog(this);
        dialog.showAddNew();
    }},
    /*
        修改或查看项目洽谈的界面操作
    */
    edit: function (id) {{
        var dialog = new EditDialog(this);
        dialog.showEdit(id);
    }},
    /*
        录入界面载入时执行控件初始化
    */
    onFormUiLoaded: function () {{
{InputInitCode()}
/*
{ValidateConfig()}*/
    }}
}};";
        }

        #endregion

        #region Page.js

        public string PageJsCode()
        {
            return $@"/*
    {Entity.Caption}的前端操作类对象,实现基本的增删改查的界面操作
*/
var {Entity.Name}Page = {{
    /**
     * 表格对象
     */
    grid: null,
    /**
     * 标题
     */
    title:'{Entity.Caption}',
    /**
     * 命令执行地址前缀
     */
    cmdPath: '',
    /**
     * 列表的URL
     */
    listUrl: 'Action.aspx?action=list',
    /**
     * 列表的URL的扩展参数
     */
    urlArg: '',
    /**
     * 是否自动载入数据
     */
    autoLoad: true,
    /**
     * 是否最小系统
     */
    isSmall: false,
    /**
     * 默认支持命令按钮的名称后缀
     */
    cmdElementEx: '',
    /**
     * 表节点名称
     */
    gridId: '#grid',
    /**
     * 工具栏节点名称
     */
    toolbarId: '#pageToolbarEx',
    /**
     * 表单地址
     */
    formUrl: 'Form.htm',
    /**
     * 表单对象
     */
    formOption: null,
    /*
        {Entity.Caption}的页面初始化
    */
    initialize: function () {{
        var me = this;
        if(!me.isSmall)
            me.initHistoryQuery();
        me.initForm();
        me.initToolBar();
        me.initGrid();
    }},
    /*
        项目洽谈的录入对象初始化
    */
    initForm: function () {{
        var me = this;
        me.formOption = Object.create({Entity.Name}Form);
        me.formOption.afterFormSaved = function (succeed) {{
            if (succeed)
                me.reload();
        }}
    }},
    /*
        改变列表参数
    */
    setUrlArgs: function (args) {{
        var me = this;
        me.urlArg = args && args != '' ? args : '_-_=1';
        me.formArg = args && args != '' ? args : '_-_=1';
        me.grid.changedUrl(me.cmdPath + me.listUrl + '&' + args);
    }},
    /*
        初始化工具栏
    */
    initToolBar: function() {{
        var me = this;{CommandJsCode()}
    }},
{CommandJsCode2()}
    /*
        历史查询条件还原
    */
    initHistoryQuery: function() {{
        $('#qKeyWord').textbox('setValue', preQueryArgs.keyWord);
        {InitQueryParams()}
    }},
    /*
        读取查询条件
    */
    getQueryParams: function () {{
        return {{
            keyWord:$('#qKeyWord').textbox('getValue')
            {QueryParams()}
        }};
    }},
    /*
        重新载入{Entity.Caption}的列表数据
    */
    reload:function() {{
        $(this.gridId).datagrid('reload');
    }},
    /*
        初始化列表表格
    */
    initGrid: function() {{
        var me = this;
        var grid = new GridPanel();
        me.grid=grid;
        grid.ex = me;{Gridjs()}
        grid.idField = '{Entity.PrimaryColumn.Name}';
        grid.cmdPath = me.cmdPath;
        grid.pageSize = 20;
        grid.elementId = me.gridId;
        grid.toolbar = me.toolbarId;
        grid.elementEx = me.cmdElementEx;
        grid.columns = me.columns;
        grid.edit = function(id){{
            me.formOption.formUrl = me.formUrl;
            me.formOption.formArg = me.formArg;
            me.formOption.title = me.title;
            me.formOption.edit(id);
        }};
        grid.addNew = function(){{
            me.formOption.formUrl = me.formUrl;
            me.formOption.formArg = me.formArg;
            me.formOption.title = me.title;
            me.formOption.addNew();
        }};
        if(!me.isSmall){{
            grid.getQueryParams = me.getQueryParams;{GridDetailsScript()}
        }}
        if(me.autoLoad){{
            me.setUrlArgs(me.urlArg);
        }}
        grid.initialize();
    }},
    /*
        列表表格的列信息
    */
    columns:{GridFields()}
}};";
        }

        #endregion

        #region script.js

        public override string Code()
        {
            return $@"/*
    {Entity.Caption}的前端操作类对象,实现基本的增删改查的界面操作
*/
var {Entity.Name}Page = {{
    /**
     * 表格对象
     */
    grid: null,
    /**
     * 标题
     */
    title:'{Entity.Caption}',
    /**
     * 命令执行地址前缀
     */
    cmdPath: '',
    /**
     * 列表的URL
     */
    listUrl: 'Action.aspx?action=list',
    /**
     * 列表的URL的扩展参数
     */
    urlArg: '',
    /**
     * 表单地址
     */
    formUrl: 'Form.htm',
    /**
     * 表单参数
     */
    formArg: '_-_=1',
    /**
     * 是否自动载入数据
     */
    autoLoad: true,
    /**
     * 是否最小系统
     */
    isSmall: true,
    /**
     * 默认支持命令按钮的名称后缀
     */
    cmdElementEx: '',
    /**
     * 表节点名称
     */
    gridId: '#grid',
    /**
     * 工具栏节点名称
     */
    toolbarId: '#pageToolbarEx',
    /**
     * 当前录入是否校验正确
     */
    inputSucceed: true,
    /*
        {Entity.Caption}的页面初始化
    */
    initialize: function() {{
        var me = this;
        if(!me.isSmall)
            me.initHistoryQuery();
        me.initGrid();
        me.initToolBar();
    }},
    /*
        初始化工具栏
    */
    initToolBar: function() {{
        var me = this;{CommandJsCode()}
    }},
    /*
        初始化列表表格
    */
    initGrid: function() {{
        var me = this;
        var grid = new GridPanel();
        me.grid=grid;
        grid.ex = me;{Gridjs()}
        grid.idField = '{Entity.PrimaryColumn.Name}';
        grid.cmdPath = me.cmdPath;
        grid.pageSize = 20;
        grid.elementId = this.gridId;
        grid.toolbar = this.toolbarId;
        grid.elementEx = me.cmdElementEx;
        grid.columns = me.columns;
        grid.edit = me.edit;
        grid.addNew = me.addNew;
        if(!me.isSmall){{
            grid.getQueryParams = me.getQueryParams;{GridDetailsScript()}
        }}
        if(me.autoLoad){{
            me.setUrlArgs(me.urlArg);
        }}
        grid.initialize();
    }},
    /*
        改变列表参数
    */
    setUrlArgs: function (args) {{
        var me = this;
        me.urlArg = args && args != '' ? args : '_-_=1';
        me.formArg = args && args != '' ? args : '_-_=1';
        me.grid.changedUrl(me.cmdPath + me.listUrl + '&' + args);
    }},
    /*
        历史查询条件还原
    */
    initHistoryQuery: function() {{
        $('#qKeyWord').textbox('setValue', preQueryArgs.keyWord);
        {InitQueryParams()}
    }},
    /*
        读取查询条件
    */
    getQueryParams: function () {{
        return {{
            keyWord:$('#qKeyWord').textbox('getValue')
            {QueryParams()}
        }};
    }},
    /*
        重新载入{Entity.Caption}的列表数据
    */
    reload:function() {{
        $(this.gridId).datagrid('reload');
    }},
    /*
        录入界面载入时执行控件初始化
    */
    onFormUiLoaded: function (editor,callback) {{
        var me = editor.ex;
        me.setFormValidate();
        //TO DO:控件初始化代码{InputInitCode()}
        
        if (callback)
            callback();
    }},
    /*
        录入界面数据载入后给Form赋值前,对数据进行预处理
    */
    onFormDataLoaded: function (data, editor) {{
        //var me = editor.ex;
        //TO DO:数据预处理
    }},
    /*
        录入界面数据载入后且已给Form赋值,对进行界面逻辑处理
    */
    afterFormDataLoaded: function (data, editor) {{
        var me = editor.ex;
        me.inputSucceed = true;
        //TO DO:界面逻辑处理
    }},
    /*
        录入界面数据校验
    */
    doFormValidate: function() {{
        var me = this;
        //TO DO:数据校验
        return me.NoError;
    }},
    /*
        设置校验规则
    */
    setFormValidate: function() {{
{ValidateConfig()}
    }},

    /**
     * 生成{Entity.Caption}的编辑器
     * @param {{{Entity.Name}Page}} me 当前页面对象
     * @returns {{EditorDialog}} 编辑器
     */
    createEditor: function (me) {{
        var editor = new EditorDialog();
        editor.ex = me;
        editor.title = me.title;
        if(me.grid.auditData) {{
            editor.showValidate = true;
            editor.validatePath = me.cmdPath + 'Action.aspx';
            editor.setFormValidate= me.setFormValidate;
        }}
        editor.onUiLoaded = function (ed, callback) {{
            me.onFormUiLoaded(ed, callback);
        }};
        editor.onDataLoaded = function (data, ed) {{
            me.onFormDataLoaded(data, ed);
        }};
        editor.afterDataLoaded = function (data, ed) {{
            me.afterFormDataLoaded(data, ed);
        }};
        editor.afterSave = function(succeed, data) {{
            me.reload();
        }};
        return editor;
    }},
    /*
        新增一条{Entity.Caption}的界面操作
    */
    addNew: function () {{
        var me = this.ex;
        var editor = me.createEditor(me);
        editor.uiUrl = me.formUrl + '?' + me.formArg + '&id=0';
        if(me.urlArg)
            editor.dataUrl = me.cmdPath + 'Action.aspx?action=details&' + me.urlArg + '&id=';
        else
            editor.dataUrl = me.cmdPath + 'Action.aspx?action=details&id=';
        editor.saveUrl = me.cmdPath + 'Action.aspx?action=add&id=';
        editor.dataId = 0;
        editor.show();
    }},
    /*
        修改或查看{Entity.Caption}的界面操作
    */
    edit: function (id) {{
        var me = this.ex;
        var editor = me.createEditor(me);
        editor.uiUrl = me.formUrl + '?' + me.formArg + '&id=' + id;
        editor.dataUrl = me.cmdPath + 'Action.aspx?action=details&id=';
        editor.saveUrl = me.cmdPath + 'Action.aspx?action=edit&id=';
        editor.dataId = id;
        editor.show();
    }},{CommandJsCode2()}
    /*
        列表表格的列信息
    */
    columns:{GridFields()}
}};";
        }

        private string CommandJsCode()
        {
            var code = new StringBuilder();
            foreach (var cmd in Entity.Commands.Where(p => !p.Discard))
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

        private string CommandJsCode2()
        {
            var code = new StringBuilder();
            foreach (var cmd in Entity.Commands.Where(p => !p.Discard))
            {
                if (!string.IsNullOrWhiteSpace(cmd.Url))
                {
                    code.Append($@"/*
        {cmd.Caption}:{cmd.Description}
    */
    do{cmd.Name} : function () {{
        var me = this;
        var id = me.grid.selectData.{Entity.PrimaryField};
        location.href='{cmd.Url}?pid=' + id;
    }},");
                    continue;
                }
                code.Append($@"
    /*
        {cmd.Caption}:{cmd.Description}
    */
    do{cmd.Name} : function () {{
        var me = this;");
                if (cmd.IsSingleObject)
                {
                    if (cmd.IsLocalAction)
                        code.Append($@"
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
        }});*/");
                    else
                        code.Append($@"
        me.grid.doSingleRemote('{cmd.Caption}', 'Action.aspx', '{cmd.Name.ToLWord()}');");
                }
                else
                {
                    if (cmd.IsLocalAction)
                        code.Append($@"
        /*me.grid.doMulit('{cmd.Caption}',null,function (ids){{
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
        }});*/");
                    else
                        code.Append($@"
        me.grid.doRemote('{cmd.Caption}', 'Action.aspx', '{cmd.Name.ToLWord()}');");
                }
                code.Append(@"               
    },");
            }

            return code.ToString();
        }

        private string Gridjs()
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

        private string QueryParams()
        {
            if (Entity.Interfaces == null)
                return null;
            if (Entity.Interfaces.Contains("IAuditData"))
                return ",audit: $('#qAudit').combobox('getValue')";
            if (Entity.Interfaces.Contains("IStateData"))
                return ",dataState: $('#qAudit').combobox('getValue')";
            return null;
        }

        private string InitQueryParams()
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


        private string GridDetailsScript()
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
            $('#row-d-' + row.{Entity.PrimaryColumn.Name}).panel({{
                border: false,
                cache: false,
                href: 'details.aspx?id=' + row.{Entity.PrimaryColumn.Name},
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
        options.sortName = '{Entity.PrimaryColumn.Name}';
        options.sortOrder = 'asc';");

            return code.ToString();
        }

        /// <summary>
        ///     生成初始化脚本
        /// </summary>
        private string InputInitCode()
        {
            var code = new StringBuilder();
            //    foreach (var combo in Entity.ClientProperty.Where(p => !string.IsNullOrWhiteSpace(p.ComboBoxUrl)))
            //    {
            //        code.Append($@"
            ////{combo.Caption}远程缓存模式的列表
            //comboRemote('#{combo.Name}', '{combo.ComboBoxUrl}');");
            //    }

            //    foreach (var combo in Entity.ClientProperty.Where(p => p.IsLinkKey && !p.NoneDetails ))
            //    {
            //        var entity = Find(p => p.SaveTable == combo.LinkTable);
            //        if (entity == null)
            //            continue;
            //        code.Append($@"
            ////{combo.Caption}远程缓存模式的列表
            //comboRemote('#{combo.Name}', '/Api/Index.aspx?action={entity.Name.ToLower()}');");
            //    }
            foreach (var money in Entity.ClientProperty.Where(p => p.IsMoney))
                code.Append($@"
        //{money.Caption}动态大写金额显示
        $('#{money.Name}').textbox({{
            onChange: function (am) {{
                $('#cm_{money.Name}').text(ChinessMoney(am));
            }}
        }});");
            return code.ToString();
        }


        /// <summary>
        ///     生成表格字段页面
        /// </summary>
        private string GridFields()
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
            //, {{ styler: vlStyle, halign: 'center', align: 'center', sortable: true, field: '{
                    Entity.PrimaryColumn.Name
                }', title: 'ID'}}");

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
        private static void GridField(StringBuilder code, PropertyConfig field)
        {
            var align = string.IsNullOrWhiteSpace(field.GridAlign) ? "left" : field.GridAlign;
            var extend = string.Empty;
            if (!string.IsNullOrWhiteSpace(field.DataFormater))
            {
                extend = $@", formatter: {field.DataFormater}";
            }
            else if (field.CsType == "bool")
            {
                extend = ", formatter: yesnoFormat";
            }
            else if (field.CsType == "DateTime")
            {
                extend = ", formatter: dateFormat";
            }
            else if (!string.IsNullOrWhiteSpace(field.CustomType))
            {
                extend = $", formatter: {field.CustomType.ToLWord()}Format";
            }
            else if (field.IsMoney) //unixDateFormat
            {
                extend = @", formatter: moneyFormat";
                align = "right";
            }
            else if (!string.IsNullOrWhiteSpace(field.Prefix) || !string.IsNullOrWhiteSpace(field.Suffix))
            {
                extend = $@", formatter: function(value, row) {{
                    return '{field.Prefix}' + value + '{field.Suffix}';
                }}";
            }
            PropertyEasyUiModel.CheckField(field);
            code.Append($@"
            , {{ styler: vlStyle,width:{field.GridWidth}, halign: 'center', align: '{align}', sortable: true, field: '{
                    field.Name
                }', title: '{field.Caption}'{extend}}}");
        }

        #endregion

        #region 数据规则代码

        public string ValidateConfig()
        {
            var code = new StringBuilder();
            var fields = Entity.PublishProperty.Where(p => !p.NoneDetails).ToArray();


            foreach (var field in fields)
            {
                if (field.InputType == null)
                    field.InputType = "easyui-textbox";
                bool required;
                var validType = ValidType(field, out required);
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
                            ? $"'selectNoZero[\"#{field.Name}\"]'"
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
    }
}