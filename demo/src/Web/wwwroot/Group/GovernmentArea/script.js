/*design by:agebull designer date:2019/3/2 23:49:06*/
/*
*   行政区域的前端操作类对象,实现基本的增删改查的界面操作
*/
var page = {
    /**
    表格对象
     */
    grid: null,
    /**
     * 标题
     */
    title: '行政区域',
    /**
     * 名称
     */
    name: 'GovernmentAreaData',
    /**
     * API前缀
     */
    apiPrefix: 'sys/area/v1/',
    /*
        行政区划的页面初始化
    */
    initialize: function () {
        this.initToolBar();
        this.initGrid();
    },
    /*
        重新载入行政区划的列表数据
    */
    reload: function () {
        $("#grid").treegrid("reload");
    },
    /*
        初始化工具栏
    */
    initToolBar: function () {
        createButton("#btnFlush", "icon-flush", function () {
            page.reload();
        });
        createButton("#btnImport", "icon-import", function () {
            page.importRegion();
        });
    },
    /*
        历史查询条件还原
    */
    initHistoryQuery: function () {
        /*if(!preQueryArgs.audit)
            preQueryArgs.audit = 0x100;;
        $('#qAudit').textbox('setValue', preQueryArgs.audit);
        $('#qKeyWord').textbox('setValue', preQueryArgs.keyWord);*/
    },
    /*
        读取查询条件
    */
    getQueryParams: function () {
        return {
            //keyWord: $('#qKeyWord').textbox('getValue')
        };
    },
    /*
        初始化列表表格
    */
    initGrid: function () {
        var me = this;
        var grid = new GridPanel();
        grid.tag = this;
        grid.isTree = true;
        grid.cmdPath = page.apiPrefix;
        //var first = true;
        grid.initGridOptionEx = function (options) {
            options.idField = "id";
            options.treeField = "fullName";
            options.pageSize = 20;
            options.columns = me.columns;
            options.plain = true;
            options.striped = true;
            options.pagination = false;
            options.rownumbers = true;
            options.fitColumns = true;
            options.selectOnCheck = false;
            options.checkOnSelect = false;
            options.singleSelect = true;
            //options.loadFilter = function (data) {
            //    if (first) {
            //        //first = false;
            //        setTimeout(function() {
            //            me.grid.execGridMethod('expandAll');
            //        },1);
            //    }
            //    return data;
            //};
        };
        grid.tag = this;
        grid.idField = "id";
        grid.isListDetails = true;
        grid.elementId = "#grid";
        grid.toolbar = "#pageToolbarEx";
        grid.elementEx = "";
        grid.edit = this.edit;
        grid.addNew = this.addNew;
        grid.getQueryParams = this.getQueryParams;
        grid.remove = grid.doRemove;
        grid.listUrl = page.apiPrefix + "edit/list";
        grid.initialize();
        //setTimeout(onCheckSize,1);
        this.grid = grid;
    },
    /*
        录入界面载入时执行控件初始化
    */
    onFormUiLoaded: function (editor, callback) {
        //var me = editor.ex;
        //TO DO:控件初始化代码
        if (callback)
            callback();
    },
    /*
        录入界面数据载入后给Form赋值前,对数据进行预处理
    */
    onFormDataLoaded: function (data, editor) {
        //var me = editor.ex;
        //TO DO:数据预处理
    },
    /*
        录入界面数据载入后且已给Form赋值,对进行界面逻辑处理
    */
    afterFormDataLoaded: function (data, editor) {
        var me = editor.ex;
        me.inputSucceed = true;
        //TO DO:界面逻辑处理
    },
    /*
        录入界面数据校验
    */
    doFormValidate: function () {
        //TO DO:数据校验
        return this.NoError;
    },
    /**
     * 生成行政区划的编辑器
     * @param {object} me 当前页面对象
     * @returns {object} 编辑器
     */
    createEditor: function (me) {
        var editor = new CardEditor();
        editor.ex = me;
        editor.title = "行政区划";
        editor.uiUrl = "form.htm";
        editor.afterSave = function (succeed, data) {
            if (succeed)
                me.reload();
        };

        editor.dataUrl = page.apiPrefix + "edit/details?pid=" + me.parentId + "&id=";

        editor.onUiLoaded = function (ed, callback) {
            me.onFormUiLoaded(ed, callback);
        };
        editor.onDataLoaded = function (data, ed) {
            me.onFormDataLoaded(data, ed);
        };
        editor.afterDataLoaded = function (data, ed) {
            me.afterFormDataLoaded(data, ed);
        };
        return editor;
    },
    parentId: 0,
    /*
        导入
    */
    importRegion: function () {
        var me = this.tag;

        var editor = new CardEditor();
        editor.ex = me;
        editor.title = "批量导入行政区划";
        editor.uiUrl = "import.htm";
        editor.afterSave = function (succeed, data) {
            if (succeed)
                me.reload();
        };

        editor.dataUrl = '';

        editor.onUiLoaded = function (ed, callback) {
            me.onFormUiLoaded(ed, callback);
        };
        editor.onDataLoaded = function (data, ed) {
            me.onFormDataLoaded(data, ed);
        };
        editor.afterDataLoaded = function (data, ed) {
            me.afterFormDataLoaded(data, ed);
        };
        editor.isAddNew = true;
        editor.saveUrl = page.apiPrefix + "edit/import?_=";
        editor.dataId = 0;
        editor.show();
    },
    /*
        新增一条行政区划的界面操作
    */
    addNew: function () {
        var me = this.tag;
        var parent = $("#grid").treegrid("getSelected");
        if (!parent) {
            $.messager.alert("新增", "请选择一行数据作为上级");
            return;
        }
        me.parentId = parent.Id;
        var editor = me.createEditor(me);
        editor.isAddNew = true;
        editor.saveUrl = page.apiPrefix + "edit/addnew?pid=" + parent.Id + "&id=";
        editor.dataId = 0;
        editor.show();
    },
    /*
        修改或查看行政区划的界面操作
    */
    edit: function (id, row) {
        if (id <= 0)
            return;
        var me = this.tag;
        me.parentId = row.ParentOid;
        var editor = me.createEditor(me);
        editor.saveUrl = page.apiPrefix + "edit/update?id=";
        editor.dataId = id;
        editor.show();
    },
    /**
    * 列表表格的列信息
    */
    columns:
        [
            [
                { styler: vlStyle, halign: 'center', align: 'center', field: 'IsSelected', checkbox: true }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'fullName', title: '全称', width: 3 }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'shortName', title: '简称', width: 3 }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'treeName', title: '树形名称', width: 3 }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'orgLevel', title: '级别', width: 3 }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'levelIndex', title: '层级的序号', width: 1 }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'code', title: '编码', width: 3 }
            ]
        ]
};

/**
 * 依赖功能扩展
 */
mainPageOptions.extend({
    doPageInitialize: function (callback) {
        globalOptions.api.customHost = globalOptions.api.authHost;
        mainPageOptions.loadPageInfo(page.name, function () {
            page.initialize();
            callback();
        });
    },
    onCheckSize: function (wid, hei) {

        $('#grid').datagrid('resize', window.o99);
    }
});