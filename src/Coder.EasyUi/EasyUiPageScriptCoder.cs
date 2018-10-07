using System.ComponentModel.Composition;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class EasyUiPageScriptCoder : EasyUiPageScriptCoderBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileName => "page.js";

        /// <summary>
        /// 名称
        /// </summary>
        protected override string ExFileName => "form.js";

        #region Form.js

        protected override string ExtendCode()
        {
            return $@"/*
    {Entity.Caption}的前端操作类对象,实现基本的编辑的界面操作
*/
var {Entity.Name}Form = {{
    /**
     * 主键字段
     */
    idField : '{Entity.PrimaryColumn.JsonName}',
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

        protected override string BaseCode()
        {
            return $@"
/**
*  {Entity.Caption}的前端操作类对象,实现基本的增删改查的界面操作
*/
var page = {{
    /**
     * 表格对象
     */
    grid: null,
    /**
     * 标题
     */
    title:'{Entity.Caption}',
    /**
     * 名称
     */
    name: '{Entity.EntityName}',
    /**
     * API前缀
     */
    apiPrefix: '{Entity.Parent.Abbreviation}/{Entity.Abbreviation}/v1/',
    /**
     * 命令执行地址前缀
     */
    cmdPath: '',
    /**
     * 列表的URL
     */
    listUrl: '{Entity.Parent.Abbreviation}/{Entity.Abbreviation}/v1/edit/list',
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
        grid.idField = me.idField;
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


    }
}