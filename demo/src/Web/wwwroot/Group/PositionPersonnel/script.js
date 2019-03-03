/*design by:agebull designer date:2017/5/26 22:15:18*/
/*
    人员职位设置的前端操作类对象,实现基本的增删改查
*/
var page = {
    /**
     * 表格对象
     */
    grid: null,
    /**
     * 标题
     */
    title: "人员职位设置",
    name: "PositionPersonnelData",
    /**
     * API前缀
     */
    apiPrefix: "sys/person/v1/",
    /**
     * 命令执行地址前缀
     */
    cmdPath: "",
    /**
     * 列表的URL
     */
    listUrl: "edit/list",
    /**
     * 列表的URL的扩展参数
     */
    urlArg: "",
    /**
     * 是否自动载入数据
     */
    autoLoad: true,
    /**
     * 默认支持命令按钮的名称后缀
     */
    cmdElementEx: "",
    /**
     * 表节点名称
     */
    gridId: "#grid",
    /**
     * 工具栏节点名称
     */
    toolbarId: "#pageToolbarEx",
    /**
     * 表单地址
     */
    formUrl: "form.htm",
    /**
     * 表单对象
     */
    formOption: null,
    /*
        人员职位设置的页面初始化
    */
    initialize: function () {
        var me = this;
        me.initForm();
        this.initTree();
        this.initGrid();
        this.initHistoryQuery();
    },
    /**
     * 当前组织ID
     */
    curOid: 0,
    /**
     * 当前职位ID
     */
    curPid: 0,
    /*
        项目洽谈的录入对象初始化
    */
    initForm: function () {
        var me = this;
        this.formOption = {
            /**
             * 主键字段
             */
            idField: "Id",
            /**
             * 标题
             */
            title: "人员职位设置",
            /**
             * 命令执行地址前缀
             */
            apiPrefix: "sys/person/v1/",
            /**
             * 表单地址
             */
            formUrl: "form.htm",
            /**
             * 表单参数
             */
            formArg: "_-_=1",
            afterSaved: function (succeed) {
                if (succeed)
                    me.reload();
            },
            onUiLoaded: function (editor) {
                editor.formArg = me.formArg;
                //角色标识远程缓存模式的列表
                comboRemote("#RoleId", "sys/role/v1/edit/roles", null, null, null, globalOptions.api.appApiHost);
                //角色标识远程缓存模式的列表
                comboRemote("#OrganizePositionId", "sys/person/v1/edit/tree", null, true,'title');
            }
        };
    },
    /*
        改变列表参数
    */
    setUrlArgs: function (args) {
        var me = this;
        if (args) {
            me.urlArg = args;
            me.formArg = args;
            me.grid.changedUrl(me.apiPrefix + me.listUrl + '?' + args);
        } else {
            me.formArg = me.urlArg = '_-_=1';
            me.grid.changedUrl(me.apiPrefix + me.listUrl);
        }
        console.log(me.formArg);
    },

    /**
     * 初始化组织树
     */
    initTree: function () {
        var me = this;
        $("#tree").tree({
            iconCls: "icon-details",
            lines: true,
            //onDblClick
            onSelect: function (node) {
                console.log(node.attributes);
                if (node.attributes === "post") {
                    me.curOid = parseInt(node.tag);
                    me.curPid = parseInt(node.id);
                    me.setUrlArgs("pid=" + me.curPid);
                } else if (node.attributes === "org") {
                    me.curPid = -1;
                    me.curOid = parseInt(node.tag);
                    me.setUrlArgs("oid=" + me.curOid +"&pid=-1");
                } else {
                    me.curPid = -1;
                    me.curOid = -1;
                    me.setUrlArgs("aid=" + parseInt(node.tag));
                }
            }
        });
        ajaxLoadValue("载入", page.apiPrefix + "edit/tree", null, function (data) {
            if (!data)
                return;
            $("#tree").tree("loadData", data);
        });
    },
    /*
        重新载入职位组织关联的列表数据
    */
    reload: function () {
        $("#grid").datagrid("reload");
    },
    /*
        初始化列表表格
    */
    initGrid: function () {
        var me = this;
        var grid = new GridPanel();
        grid.ex = this;
        grid.auditData = true;
        grid.historyData = true;
        grid.stateData = true;
        grid.idField = "Id";
        grid.cmdPath = page.apiPrefix;
        grid.pageSize = 20;
        grid.isListDetails = true;
        grid.elementId = "#grid";
        grid.toolbar = "#pageToolbarEx";
        grid.elementEx = "";
        grid.edit = function (id) {
            var dialog = new EditDialog();
            dialog.showEdit(id, me.formOption);
        };
        grid.addNew = function () {
            var dialog = new EditDialog();
            dialog.showAddNew(me.formOption);
        };
        grid.getQueryParams = this.getQueryParams;
        grid.remove = grid.doRemove;
        grid.columns = this.columns;
        grid.listUrl = page.apiPrefix + "edit/list";
        grid.initialize();
        this.grid = grid;
    },
    /*
        历史查询条件还原
    */
    initHistoryQuery: function () {
        if (!preQueryArgs.audit)
            preQueryArgs.audit = 0x100;
        //$("#qAudit").combobox("setValue", preQueryArgs.audit);
        $("#qKeyWord").textbox("setValue", preQueryArgs.keyWord);
    },
    /*
        读取查询条件
    */
    getQueryParams: function () {
        return {
            //audit: $("#qAudit").combobox("getValue"),
            keyWord: $("#qKeyWord").textbox("getValue")
        };
    },
    /*
        列表表格的列信息
    */
    columns:
        [
            [
                { styler: vlStyle, halign: "center", align: "center", field: "IsSelected", checkbox: true }
                , { styler: vlStyle, halign: "center", align: "center", sortable: true, field: "dataState", title: "状态", formatter: auditIconFormat }
                , { styler: vlStyle, width: 1, halign: "center", align: "left", sortable: true, field: "RealName", title: "姓名" }
                , { styler: vlStyle, width: 1, halign: "center", align: "left", sortable: true, field: "Sex", title: "性别", formatter: sexFormat }
                , { styler: vlStyle, width: 1, halign: "center", align: "left", sortable: true, field: "Mobile", title: "手机" }
                , { styler: vlStyle, width: 1, halign: "center", align: "left", sortable: true, field: "Birthday", title: "生日", formatter: dateFormat }
                , { styler: vlStyle, width: 3, halign: "center", align: "left", sortable: true, field: "Organization", title: "机构" }
                , { styler: vlStyle, width: 1, halign: "center", align: "left", sortable: true, field: "Position", title: "职位" }
                , { styler: vlStyle, width: 1, halign: "center", align: "left", sortable: true, field: "Appellation", title: "称谓" }
                , { width: 1, styler: vlStyle, halign: "center", align: "center", sortable: true, field: "Role", title: "系统角色" }
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
        //$('#tree').tree('resize', window.o99);
        $('#grid').datagrid('resize', window.o99);
    }
});