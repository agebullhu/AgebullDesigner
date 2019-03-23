/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/22 10:27:49*/
/*
*   机构职位设置的前端操作类对象,实现基本的增删改查的界面操作
*/
var page = {
    /**
    表格对象
     */
    grid: null,
    /**
     * 标题
     */
    title:'机构职位设置',
    /**
     * 名称
     */
    name: 'OrganizePositionData',
    /**
     * API前缀
     */
    apiPrefix: 'user/OrganizePosition/v1/',
    /**
     * 列表的URL
     */
    listUrl: 'edit/list',
    /**
     * 列表的URL的扩展参数
     */
    urlArg: '',
    /**
     * 表单地址
     */
    formUrl: 'form.htm',
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
    * 当前录入是否校验正确
    */
    inputSucceed: true,
    /**
    * 机构职位设置的页面初始化
    */
    initialize: function() {
        var me = this;
        if(!me.isSmall)
            me.initHistoryQuery();
        me.initGrid();
        me.initToolBar();
    },
    /**
    * 初始化工具栏
    */
    initToolBar: function() {
        var me = this;
    },
    /**
    * 初始化列表表格
    */
    initGrid: function() {
        var me = this;
        var grid = new GridPanel();
        me.grid=grid;
        grid.tag = me;
        grid.idField = 'Id';
        grid.cmdPath = page.apiPrefix;
        grid.auditData = true;
        grid.historyData = true;
        grid.stateData = true;
        grid.pageSize = 20;
        grid.elementId = this.gridId;
        grid.toolbar = this.toolbarId;
        grid.elementEx = me.cmdElementEx;
        grid.columns = me.columns;
        grid.edit = me.edit;
        grid.addNew = me.addNew;
        if(!me.isSmall){
            grid.getQueryParams = me.getQueryParams;
        }
        if(me.autoLoad){
            me.setUrlArgs(me.urlArg);
        }
        grid.initialize();
    },
    /**
    * 改变列表参数
     * @param {string} args 参数
    */
    setUrlArgs: function (args) {
        var me = this;
        if (args && args != '') {
            me.urlArg = args;
            me.formArg = args;
            me.grid.changedUrl(me.apiPrefix + me.listUrl + '?' + args);
        } else {
            me.formArg = me.urlArg = '_-_=1';
            me.grid.changedUrl(me.apiPrefix + me.listUrl);
        }
    },
    /**
    * 历史查询条件还原
    */
    initHistoryQuery: function() {
        $('#qKeyWord').textbox('setValue', preQueryArgs.keyWord);
        
        if (!preQueryArgs.audit || preQueryArgs.audit > 0x100)
            preQueryArgs.audit = 0x100;
        $('#qAudit').combobox('setValue', preQueryArgs.audit); 
    },
    /**
    * 读取查询条件
    * @returns {object} 查询条件
    */
    getQueryParams: function () {
        return {
            keyWord:$('#qKeyWord').textbox('getValue')
            ,audit: $('#qAudit').combobox('getValue')
        };
    },
    /**
    * 重新载入机构职位设置的列表数据
    */
    reload:function() {
        $(this.gridId).datagrid('reload');
    },
    /**
    * 录入界面载入时执行控件初始化
    * @param {object} editor 编辑器
    * @param {Function} callback 回调
    */
    onFormUiLoaded: function (editor,callback) {
        var me = editor.ex;
        me.setFormValidate();
        //部门标识下拉列表
        comboRemote('#DepartmentId', 'user/org/v1/edit/combo');
        //角色外键下拉列表
        comboRemote('#RoleId', 'app/Role/v1/edit/combo');
        if (callback)
            callback();
    },
    /**
    * 录入界面数据载入后给Form赋值前,对数据进行预处理
    * @param {object} data 数据
    * @param {object} editor 编辑器
    */
    onFormDataLoaded: function (data, editor) {
    },
    /**
    * 录入界面数据载入后且已给Form赋值,对进行界面逻辑处理
    * @param {object} data 数据
    * @param {object} editor 编辑器
    */
    afterFormDataLoaded: function (data, editor) {
        var me = editor.ex;
        me.inputSucceed = true;
    },
    /**
    * 设置校验规则
    */
    setFormValidate: function() {
        $('#Position').textbox({required:true,validType:['strLimit[0,200]']});

    },

    /**
     * 生成机构职位设置的编辑器
     * @param {OrganizePositionPage} me 当前页面对象
     * @returns {EditorDialog} 编辑器
     */
    createEditor: function (me) {
        var editor = me.grid.createEditor();
        editor.ex = me;
        editor.title = me.title;
        if(me.grid.auditData) {
            editor.showValidate = true;
            editor.validatePath = me.apiPrefix + 'audit/validate';
            editor.setFormValidate= me.setFormValidate;
        }
        editor.onUiLoaded = function (ed, callback) {
            me.onFormUiLoaded(ed, callback);
        };
        editor.onDataLoaded = function (data, ed) {
            me.onFormDataLoaded(data, ed);
        };
        editor.afterDataLoaded = function (data, ed) {
            me.afterFormDataLoaded(data, ed);
        };
        editor.afterSave = function(succeed, data) {
            me.reload();
        };
        return editor;
    },
    /**
    * 新增一条机构职位设置的界面操作
    */
    addNew: function () {
        var me = this.tag;
        var editor = me.createEditor(me);
        editor.uiUrl = me.formUrl + '?' + me.formArg + '&id=0';
        if(me.urlArg)
            editor.dataUrl = me.apiPrefix + 'edit/details?' + me.urlArg + '&id=';
        else
            editor.dataUrl = me.apiPrefix + 'edit/details?id=';
        editor.saveUrl = me.apiPrefix + 'edit/addnew?id=';
        editor.dataId = 0;
        editor.show();
    },
    /**
    * 修改或查看机构职位设置的界面操作
    * @param {int} id 数据主键
    */
    edit: function (id) {
        var me = this.tag;
        var editor = me.createEditor(me);
        
        editor.uiUrl = me.formUrl + '?' + me.formArg + '&id=' + id;
        editor.dataUrl = me.apiPrefix + 'edit/details?id=';
        editor.saveUrl = me.apiPrefix + 'edit/update?id=';
        editor.dataId = id;
        editor.show();
    },
    /**
    * 列表表格的列信息
    */
    columns:
    [
       [
            { styler: vlStyle, halign: 'center', align: 'center', field: 'IsSelected', checkbox: true}
            //, { styler: vlStyle, halign: 'center', align: 'center', sortable: true, field: 'Id', title: 'ID'}
            , { styler: vlStyle, halign: 'center', align: 'center', sortable: true, field: 'AuditState', title: '状态', formatter: auditIconFormat }
            , { styler: vlStyle,halign: 'center', align: 'left', sortable: true, field: 'Position', title: '职位' , width:1}
            , { styler: vlStyle,halign: 'center', align: 'left', sortable: true, field: 'Department', title: '部门名称' , width:1}
            , { styler: vlStyle,halign: 'center', align: 'left', sortable: true, field: 'OrgLevel', title: '部门级别' , width:3}
            , { styler: vlStyle,halign: 'center', align: 'left', sortable: true, field: 'OrgId', title: '边界机构标识' , width:1}
            , { styler: vlStyle,halign: 'center', align: 'left', sortable: true, field: 'Role', title: '角色' , width:3}
            , { styler: vlStyle,halign: 'center', align: 'left', sortable: true, field: 'Memo', title: '备注' , width:3}
        ]
    ]
};

/**
 * 依赖功能扩展
 */
mainPageOptions.extend({
    doPageInitialize: function (callback) {
        globalOptions.api.customHost = globalOptions.api.userHost;
        mainPageOptions.loadPageInfo(page.name, function () {
            page.initialize();
            callback();
        });
    },
    onCheckSize: function (wid, hei) {

        $('#grid').datagrid('resize', window.o99);
    }
});