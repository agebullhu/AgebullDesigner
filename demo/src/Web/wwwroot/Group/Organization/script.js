/*
    组织机构的前端操作类对象,实现基本的增删改查
*/
var page = {
    grid: null,
    /*当前录入是否校验正确*/
    inputSucceed: true,
    name:"OrganizationData",
    /**
     * API前缀
     */
    apiPrefix: "sys/org/v1/",
    /*
        组织机构的页面初始化
    */
    initialize: function () {
        this.initToolBar();
        this.initTree();
        this.initGrid();
    },
    /*
        重新载入组织机构的列表数据
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
    curOid: 0,
    /**
     * 初始化组织树
     */
    initTree: function () {
        $("#tree").tree({
            iconCls: "icon-details",
            lines: true,
            //onDblClick
            onSelect: function (node) {
                page.curOid = node.id;
                page.grid.changedUrl(grid.listUrl = page.apiPrefix + "edit/list?oid=" + page.curOid);
            }
        });
        ajaxLoadValue("载入", page.apiPrefix + "edit/tree", null, function (data) {
            if (data)
                $("#tree").tree("loadData", data);
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
            options.idField = "Id";
            options.treeField = "FullName";
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
        grid.idField = "Id";
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
     * 生成组织机构的编辑器
     * @param {object} me 当前页面对象
     * @returns {object} 编辑器
     */
    createEditor: function (me) {
        var editor = new CardEditor();
        editor.ex = me;
        editor.title = "组织机构";
        editor.uiUrl = "form.htm";
        editor.afterSave = function (succeed, data) {
            if (succeed)
                me.reload();
        };

        editor.dataUrl = page.apiPrefix + "edit/details?oid=" + page.curOid + "&pid=" + me.parentId + "&id=";

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
        editor.title = "批量导入组织机构";
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
        editor.saveUrl = page.apiPrefix + "edit/import?oid=" + page.curOid +"&_=";
        editor.dataId = 0;
        editor.show();
    },
    /*
        新增一条组织机构的界面操作
    */
    addNew: function () {
        if (!page.curOid) {
            $.messager.alert("新增", "请选择一个行政区划");
            return;
        }
        var me = this.tag;
        var parent = $("#grid").treegrid("getSelected");
        if (!parent) {
            $.messager.alert("新增", "请选择一行数据作为上级");
            return;
        }
        me.parentId = parent.Id;
        var editor = me.createEditor(me);
        editor.isAddNew = true;
        editor.saveUrl = page.apiPrefix + "edit/addnew?oid=" + page.curOid + "&pid=" + parent.Id + "&id=";
        editor.dataId = 0;
        editor.show();
    },
    /*
        修改或查看组织机构的界面操作
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
    /*
        列表表格的列信息
    */
    columns:
        [
            [
                { styler: vlStyle, halign: "center", align: "center", field: "IsSelected", checkbox: true }
                //,{ styler: vlStyle, halign: 'center', align: 'center', sortable: true, field: 'dataState', title: ''}
                //, { styler: vlStyle, halign: 'center', align: 'center', sortable: true, field: 'Id', title: 'ID' }
                , { styler: vlStyle,  halign: "center", align: "left", sortable: true, field: "FullName", title: "全称" }
                , { styler: vlStyle,  halign: "center", align: "left", sortable: true, field: "ShortName", title: "简称" }
                , { styler: vlStyle,  halign: "center", align: "left", sortable: true, field: "Type", title: "类型", formatter: organizationTypeFormat }
                , { styler: vlStyle,  halign: "center", align: "left", sortable: true, field: "Code", title: "代码" }
                , { styler: vlStyle,  halign: 'center', align: 'center', sortable: true, field: 'OrgLevel', title: '级别' }
                //, { styler: vlStyle,  halign: "center", align: "left", sortable: true, field: "LevelIndex", title: "序号" }
                //, { styler: vlStyle,  halign: "center", align: "left", sortable: true, field: "TreeName", title: "完整名称" }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'super_orgcode', title: '上级机构代码' }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'manag_orgcode', title: '注册管理机构代码' }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'manag_orgname', title: '注册管理机构名称' }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'city_code', title: '所在市级编码' }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'district_code', title: '所在区县编码' }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'org_address', title: '机构详细地址' }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'law_personname', title: '机构负责人' }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'law_persontel', title: '机构负责人电话' }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'contact_name', title: '机构联系人' }
                , { styler: vlStyle, halign: 'center', align: 'left', sortable: true, field: 'contact_tel', title: '机构联系人电话' }
            ]
        ]
};

/**
 * 依赖功能扩展
 */
mainPageOptions.extend({
    doPageInitialize: function (callback) {
        globalOptions.api.customHost = globalOptions.api.authApiHost;
        globalOptions.api.customHost = globalOptions.api.yiMaTongHost;
        mainPageOptions.loadPageInfo(page.name, function () {
            page.initialize();
            callback();
        });
    },
    onCheckSize: function (wid, hei) {
        $('#layout').layout('resize', window.o99);
        //$('#tree').tree('resize', opt);
        $('#grid').datagrid('resize', window.o99);
    }
});