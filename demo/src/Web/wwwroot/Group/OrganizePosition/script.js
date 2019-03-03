

/**
 * 职位组织关联的前端操作类对象,实现基本的增删改查
*/
var page = {
    grid: null,
    name: "OrganizePositionData",
    /**
     * API前缀
     */
    apiPrefix: "sys/post/v1/",
    /*当前录入是否校验正确*/
    inputSucceed: true,
    /*
        职位组织关联的页面初始化
    */
    initialize: function () {
        this.initTree();
        this.initToolBar();
        this.initHistoryQuery();
        this.initGrid();
    },
    /*
        重新载入职位组织关联的列表数据
    */
    reload: function () {
        $("#grid").datagrid("reload");
    },
    /*
        初始化工具栏
    */
    initToolBar: function () {
        createRoleButton("启用所有修改", "createSubjection", "#btnCreateSubjection", "icon-man", function () {
            page.createSubjection(page);
        });
        createRoleButton("添加管理员与审核员", "createAll", "#btnCreateAll", "icon-man", function () {
            page.createAll();
        });
    },
    createAll: function () {
        $.messager.confirm("添加管理员与审核员", "确定添加管理员与审核员吗?", function (s) {
            if (s) {
                ajaxOperator("添加管理员与审核员", page.apiPrefix + "edit/createAll", null, function (res) {
                    if (res.succeed)
                        $.messager.alert("添加管理员与审核员", "操作成功");
                    else
                        $.messager.alert("添加管理员与审核员", res.message);
                });
            }
        });
    },
    /**
     * 生成机构与职位隶属关系
     * @returns {void} 
     */
    createSubjection: function () {
        $.messager.confirm("启用所有修改", "确定启用所有修改吗?", function (s) {
            if (s) {
                ajaxOperator("启用所有修改", page.apiPrefix + "edit/createSubjection", null, function (res) {
                    if (res.succeed)
                        $.messager.alert("启用所有修改", "生成成功");
                    else
                        $.messager.alert("启用所有修改", res.message);
                });
            }
        });
    },
    /*
        历史查询条件还原
    */
    initHistoryQuery: function () {
        //if (!preQueryArgs.audit)
        //    preQueryArgs.audit = 0x100;;
        //$('#qAudit').textbox('setValue', preQueryArgs.audit);
        //$('#qKeyWord').textbox('setValue', preQueryArgs.keyWord);
    },
    /*
        读取查询条件
    */
    getQueryParams: function () {
        return {
            //keyWord: $('#qKeyWord').textbox('getValue')
        };
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
                if (node.attributes === 'org') {
                    page.curOid = node.id;
                    page.grid.changedUrl(grid.listUrl = page.apiPrefix + "edit/list?oid=" + page.curOid);
                } else {
                    page.curOid = 0;
                    //page.grid.changedUrl(grid.listUrl = page.apiPrefix + "edit/list?aid=" + page.curOid);
                }
            }
        });
        ajaxLoadValue("载入", page.apiPrefix + "edit/tree", null, function (data) {
            if (data)
                $("#tree").tree("loadData", data);
        });
    },
    /*
        初始化列表表格
    */
    initGrid: function () {
        var grid = new GridPanel();
        grid.tag = this;
        grid.checkByLoaded = true;
        grid.isListDetails = true;
        grid.idField = "Id";
        grid.cmdPath = page.apiPrefix;
        grid.elementId = "#grid";
        grid.toolbar = "#pageToolbarEx";
        grid.elementEx = "";
        grid.edit = this.edit;
        grid.addNew = this.addNew;
        grid.getQueryParams = this.getQueryParams;
        grid.remove = grid.doRemove;
        grid.columns = this.columns;
        grid.initGridOptionEx = function (options) {
            options.pagination = false;
        };
        if (!mainPageOptions.allButton && mainPageOptions.userButtons && mainPageOptions.userButtons.indexOf("#btnSaveSelect") < 0) {
            grid.afterDataLoaded = function(data) {
                var data2 = { succeed: true, total: data.total, rows: [] };
                for (var i = 0; i < data.rows.length; i++) {
                    if (data.rows[i].IsSelected)
                        data2.rows.push(data.rows[i]);
                }
                return data2;
            };
        }
        grid.initialize();
        this.grid = grid;
    },
    /*
        录入界面载入时执行控件初始化
    */
    onFormUiLoaded: function (editor, callback) {
        //TO DO:控件初始化代码
        if (callback)
            callback();
        //角色标识远程缓存模式的列表
        comboRemote("#RoleId", "sys/role/v1/edit/roles", null, null, null, globalOptions.api.appApiHost);
    },
    /*
        录入界面数据载入后给Form赋值前,对数据进行预处理
    */
    onFormDataLoaded: function (data, editor) {
        //TO DO:数据预处理
    },
    /*
        录入界面数据载入后且已给Form赋值,对进行界面逻辑处理
    */
    afterFormDataLoaded: function (data, editor) {
        page.inputSucceed = true;
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
     * 生成职位组织关联的编辑器
     * @returns {object} 编辑器
     */
    createEditor: function () {
        var editor = page.grid.createEditor();
        editor.ex = page;
        editor.title = "职位";
        editor.uiUrl = "form.htm";
        editor.onUiLoaded = function (ed, callback) {
            page.onFormUiLoaded(ed, callback);
        };
        editor.onDataLoaded = function (data, ed) {
            page.onFormDataLoaded(data, ed);
        };
        editor.afterDataLoaded = function (data, ed) {
            page.afterFormDataLoaded(data, ed);
        };
        editor.afterSave = function (succeed, data) {
            if (succeed)
                page.reload();
        };
        return editor;
    },
    /*
        新增一条组织机构的界面操作
    */
    addNew: function () {
        if (page.curOid <= 0) {
            $.messager.alert("新增", "请选择一个机构");
            return;
        }
        var editor = page.createEditor(page);
        editor.isAddNew = true;
        editor.dataUrl = page.apiPrefix + "edit/details?oid=" + page.curOid + "&id=0";
        editor.saveUrl = page.apiPrefix + "edit/addnew?id=";
        editor.dataId = 0;
        editor.show();
    },
    /*
        修改或查看职位组织关联的界面操作
    */
    edit: function (id) {
        var page = this.tag;
        var editor = page.createEditor(page);
        editor.dataUrl = page.apiPrefix + "edit/details?id=";
        editor.saveUrl = page.apiPrefix + "edit/update?id=";
        editor.dataId = id;
        editor.show();
    },
    /**
    *  列表表格的列信息
    */
    columns:
        [
            [
                { styler: vlStyle, halign: "center", align: "left", field: "IsSelected", checkbox: true }
                , { width: 3, styler: vlStyle, halign: "center", align: "left", sortable: true, field: "Department", title: "部门" }
                , { width: 1, styler: vlStyle, halign: "center", align: "left", sortable: true, field: "Position", title: "职位" }
                , { width: 1, styler: vlStyle, halign: "center", align: "left", sortable: true, field: "Role", title: "系统角色" }
                , { width: 3, styler: vlStyle, halign: "center", align: "left", sortable: true, field: "Memo", title: "备注" }
            ]
        ]
};



/**
 * 依赖功能扩展
 */
mainPageOptions.extend({
    doPageInitialize: function (callback) {
        globalOptions.api.customHost = globalOptions.api.userApiHost;
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