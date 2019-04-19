
/**
 * 当前页面
 */
var page = {
    grid: null,
    name: "PageItemData",
    apiPrefix: "app/page/v1/",
    initialize: function () {
        this.initToolBar();
        this.initTree();
        this.initializeGrid();
    },
    curFolderId: 0,
    /*
        读取查询条件
    */
    getQueryParams: function () {
        return {
            keyWord: $("#qKeyWord").textbox("getValue")
        };
    },
    /**
     * 初始化树
     */
    initTree: function () {
        var me = this;
        $("#tree").tree({
            iconCls: "icon-details",
            lines: true,
            onLoadSuccess: function (node, data) {
                data.state = "open";
            },
            onSelect: function (node) {
                if (node.attributes === "Folder" || node.attributes === "Root") {
                    $("#cParent").textbox("setValue", node.tag);
                }
                me.curFolderId = node.id;
                me.grid.changedUrl(me.grid.listUrl = page.apiPrefix + "edit/list?fid=" + me.curFolderId);
            }
        });
        ajaxLoadValue("载入", page.apiPrefix + "edit/tree", null, function (data) {
            if (data)
                $("#tree").tree("loadData", data);
        });

    },
    initializeGrid: function () {
        var grid = new GridPanel();
        grid.tag = this;
        grid.cmdPath = page.apiPrefix;
        grid.idField = "id";
        grid.getQueryParams = this.getQueryParams;
        grid.edit = this.edit;
        grid.addNew = this.add;
        grid.remove = grid.doRemove;
        grid.columns = this.columns;
        grid.initialize();
        this.grid = grid;
    },
    add: function (page) {
        var editor = page.grid.createEditor();
        editor.isAddNew = true;
        editor.title = "页面节点";
        editor.uiUrl = "form.htm";
        editor.saveUrl = page.apiPrefix + "edit/addnew?id=";
        editor.dataUrl = page.apiPrefix + "edit/details?fid=" + page.curFolderId + "&id=";
        editor.data = {};
        editor.afterSave = function (succeed, data) {
            if (succeed)
                $("#grid").datagrid("reload");
        };
        editor.onUiLoaded = function (editor) {
            page.itemTypeChagned(editor);
        };
        editor.show();
    },
    edit: function (id) {
        var editor = page.grid.createEditor("update", id, {
            afterSave: function (succeed, data) {
                if (succeed)
                    $("#grid").datagrid("reload");
            },
            onUiLoaded: function (editor) {
                page.itemTypeChagned(editor);
            },
            title: "页面节点",
            uiUrl: "form.htm",
            saveUrl: page.apiPrefix + "edit/update?id=",
            dataUrl: page.apiPrefix + "edit/details?id="
        });
        editor.show();
    },
    itemTypeChagned: function (editor) {
        $("#itemType").combobox({
            onSelect: function (rec) {
                if (rec.value === 2)
                    $('#pageAttr').show();
                else
                    $('#pageAttr').hide();
            }
        });

    },
    /*
        初始化工具栏
    */
    initToolBar: function () {

        createButton("#btnFlushTree", "icon-flush", function () {
            ajaxLoadValue("载入", page.apiPrefix + "edit/tree", null, function (data) {
                if (data)
                    $("#tree").tree("loadData", data);
            });
        });
        createRoleButton("按钮检查", "NormalButtons", "#btnButtons", "icon-code", function () {
            call_remote("按钮检查", page.apiPrefix + "edit/normal_buttons",
                { id: page.curFolderId, type: "edit" }, function (r) {
                    if (r.succeess)
                        $("#grid").datagrid("reload");
                });
        });
        createRoleButton("绑定类型", "BindType", "#btnBindType", "icon-code", function () {
            call_remote("绑定类型", page.apiPrefix + "v1/sys/bind_type", { id: page.curFolderId | 0 }, function (r) {
                if (r.succeess)
                    page.grid.reload();
            });
        });
        createRoleButton("刷新缓存", "FlushCache", "#btnFlushCache", "icon-flush", function () {
            page.doFlushCache();
        });
        createRoleButton("设置分类", "setparent", "#btnSetParent", "icon-cmd", function () {
            page.doSetParent();
        });
        //createRoleButton("页面静态化", "setparent", "#btnToHtml", "icon-cmd", function () {
        //    page.doToHtml();
        //});
    },
    /*
        删除一条项目模板节点的界面操作
    */
    delItem: function () {
        if (page.curFolderId === 0) {
            messager.alert("请选择一个节点");
            return;
        }
        call_remote("删除", page.apiPrefix + "edit/delete", { selects: page.curFolderId }, function (r) {
            if (r.succeess)
                $("#tree").tree("reload");
        });
    },
    //doToHtml: function () {
    //    var pid = page.curFolderId;
    //    if (!pid) {
    //        pid = 0;
    //    }
    //    call_remote("页面静态化", page.apiPrefix + "page/static", { id: pid, host: location.host });
    //},
    doSetParent: function () {
        var cla = $("#cParent").textbox("getValue");
        if (!cla) {
            $.messager.alert("设置新分类", "分类名称不能为空");
        } else {
            call_remote("设置新分类", page.apiPrefix + "edit/set_parent", { parent: cla, selects: page.curFolderId });
        }
    },
    doFlushCache: function () {
        call_remote("刷新系统缓存", "v1/sys/flush_cache", null, null, null, globalOptions.api.userApiHost);
    },
    columns: [[
        { halign: "center", align: "center", field: "IsSelected", checkbox: true },
        { halign: "center", align: "center", field: "id", title: "标识", sortable: true },
        { halign: "center", align: "center", sortable: true, field: "itemType", title: "类型", formatter: itemTypeFormat },
        { halign: "center", align: "left", field: "caption", title: "标题", sortable: true },
        { halign: "center", align: "center", field: "icon", title: "图标", sortable: true }, 
        { halign: "center", align: "center", field: "index", title: "序号", sortable: true },
        { halign: "center", align: "left", field: "hide", title: "隐藏", formatter: okFormat },
        { halign: "center", align: "left", field: "audit", title: "审批", formatter: okFormat },
        { halign: "center", align: "left", field: "level_audit", title: "逐级审批", formatter: okFormat },
        { halign: "center", align: "left", field: "edit", title: "编辑", formatter: okFormat },
        { halign: "center", align: "left", field: "data_state", title: "数据管理", formatter: okFormat },
        { halign: "center", align: "left", field: "url", title: "链接地址" },
        { halign: "center", align: "left", field: "name", title: "系统名称", sortable: true },
        { halign: "center", align: "left", field: "type", title: "系统对象" },
        { halign: "center", align: "left", field: "tags", title: "扩展" }
    ]]

};

/**
 * 附件枚举类型
 */
var itemType = [
    { value: 0, text: "-" },
    { value: 1, text: "分组" },
    { value: 2, text: "页面" },
    { value: 3, text: "按钮" },
    { value: 4, text: "命令" }
];
/**
 * 附件枚举类型之表格格式化方法
 * @param {object} value 附件类型
 * @returns {string} 文本
 */
function itemTypeFormat(value) {
    return arrayFormat(value, itemType);
}

/**
 * 依赖功能扩展
 */
mainPageOptions.extend({
    doPageInitialize: function (callback) {
        globalOptions.api.customHost = globalOptions.api.appApiHost;
        mainPageOptions.loadPageInfo(page.name, function () {
            page.initialize();
            callback();
        });
    }
});
