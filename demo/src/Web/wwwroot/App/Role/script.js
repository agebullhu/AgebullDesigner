
var page = {
    name: "RoleData",
    apiPrefix: "sys/role/v1/",
    initialize: function () {
        this.initPageGrid();
        this.initRolesGrid();
    },
    initPageGrid: function () {
        createButton("#btnExpandAll", "icon-add", function () {
            $("#tree").treegrid("expandAll");
        });
        createRoleButton("权限保存", "savepowers", "#btnSaveRolePower", "icon-save", page.savePower);
        $("#tree").treegrid({
            height: "100%",
            idField: "id",
            treeField: "text",
            lines: true,
            plain: true,
            striped: true,
            pagination: false,
            rownumbers: true,
            fitColumns: true,
            selectOnCheck: false,
            checkOnSelect: false,
            singleSelect: true,
            toolbar: "#pageToolbarEx2",
            loadMsg: "正在载入数据,请稍候……",
            columns: this.treeFields
        });
    },
    reLoadData: function () {
        $("#grid").datagrid("reload");
        $("#tree").treegrid("load", []);
    },
    newRole: function () {
        var editor = page.grid.createEditor();
        editor.isAddNew = true;
        editor.title = "系统角色";
        editor.uiUrl = "form.htm";
        editor.saveUrl = page.apiPrefix + "edit/addnew?id=";
        editor.data = {};
        editor.afterSave = page.reLoadData;
        editor.show();
    },
    editRole: function (id) {
        var editor = page.grid.createEditor();
        editor.title = "系统角色";
        editor.uiUrl = "form.htm";
        editor.saveUrl = page.apiPrefix + "edit/update?id=";
        editor.dataUrl = page.apiPrefix + "edit/details?id=";
        editor.dataId = id;
        editor.afterSave = page.reLoadData;
        editor.show();
    },
    initRolesGrid: function () {
        var that = this;
        var grid = new GridPanel();
        grid.listUrl = page.apiPrefix + "edit/list";
        grid.idField = "Id";
        grid.edit = that.editRole;
        grid.addNew = that.newRole;
        grid.remove = that.doRemove;
        grid.columns = this.gridFields;
        grid.onRowSelected = function (row) {
            that.loadPower(row);
        };
        grid.initGridOptionEx = function (options) {
            options.pagination = false;
            options.pageSize = 999;
        };
        grid.initialize();
        this.grid = grid;
    },
    rid: 0,
    loadPower: function (row) {
        if (row) {
            this.rid = row.Id;
        } else {
            this.rid = 0;
        }
        this.reLoadPower();
    },
    reLoadPower: function () {
        if (this.rid > 0) {
            ajaxLoadValue("载入", page.apiPrefix + "edit/powers?rid=" + this.rid, null, function (data) {
                if (data)
                    $("#tree").treegrid("loadData", data);
            });
        } else {
            $("#tree").treegrid("loadData", []);
        }
    },
    getSelected: function (rows) {
        if (!rows || rows.length == 0)
            return "";
        var that = page;
        var sel = "";
        for (var i = 0; i < rows.length; i++) {
            if (rows[i].selected) {
                sel += rows[i].id + "," + (!rows[i].data_scope ? 0 : rows[i].data_scope) + ";";
            }
            sel += that.getSelected(rows[i].children);
        }
        return sel;
    },
    onDataScope: function (id) {
        var that = page;
        var datas = $("#tree").treegrid("getData");
        var row = that.findData(id, datas);
        if (row == null)
            return;
        row.data_scope = $("#dataScope" + id).val();
    },
    inChecking: false,
    onChecked: function (id) {
        var that = page;
        if (that.inChecking)
            return;
        var ctr = document.getElementById("icChk" + id);
        if (!ctr)
            return;
        var datas = $("#tree").treegrid("getData");
        var row = that.findData(id, datas);
        if (row == null)
            return;
        that.inChecking = true;
        try {
            that.setChecked(row, ctr.checked);
        } catch (e) {
            console.error("%s.%s() ： %s", "RolePage", "onChecked", e);
            $.messager.alert("层级选中", e);
        }
        that.inChecking = false;
    },
    findData: function (id, rows) {
        if (!rows)
            return null;
        if (rows.length > 0) {
            for (var i = 0; i < rows.length; i++) {
                var chd = rows[i];
                if (chd.id == id)
                    return chd;
                var re = this.findData(id, chd.children);
                if (re)
                    return re;
            }
        }
        return null;
    },
    setChecked: function (row, checked) {
        row.selected = checked;
        var ctr = document.getElementById("icChk" + row.id);
        if (!ctr) {
            console.log("icChk" + row.id+ "找不到");
            return;
        }
        var that = page;
        var chi = that.inChecking;
        that.inChecking = true;
        ctr.checked  = checked;
        if (row.children) {
            for (var i = 0; i < row.children.length; i++) {
                this.setChecked(row.children[i], checked);
            }
        }
        that.inChecking = chi;
    },
    savePower: function () {
        var that = page;
        var sl = that.getSelected($("#tree").treegrid("getRoots"));
        $.messager.confirm("保存", "确定保存你选择的数据吗?", function (s) {
            if (s) {
                ajaxOperator("保存", page.apiPrefix + "edit/savepowers", { selects: sl, id: that.rid }, function () {
                    that.reLoadPower();
                });
            }
        });
    },

    treeFields: [[
        {
            width: 1,
            align: "center",
            field: "selected",
            title: "选择", //checkbox: true
            formatter: function (val, row) {
                var html = "<input type='checkbox' id='icChk" + row.id + "' onclick=\"page.onChecked('" + row.id + "');\"";
                if (row.selected)
                    html += "checked=\"checked\"";
                //else
                //    html += "checked=\"false\"";
                html += "/>";
                return html;
            }
        },
        { width: 2, align: 'center', field: 'id', title: '标识' },
        { width: 6, align: "left", field: "text", title: "标题" },
        { width: 5, align: "left", field: "title", title: "名称" },
        { width: 3, align: "left", field: "tag", title: "动作" }/*,
        {
            width: 3,
            align: "center",
            field: "attributes",
            title: "行级权限",
            formatter: function (val, row) {
                row.data_scope = val;
                if (row.extend !== "row")
                    return "";
                var html = "<select style='width:100%' id='dataScope" + row.id + "' onchange=\"page.onDataScope('" + row.id + "');\">";
                for (var idx = 0; idx < dataScopeType.length; idx++) {
                    html += "<option value='" + dataScopeType[idx].value + "' ";
                    if (dataScopeType[idx].value == val)
                        html += "selected='selected'";
                    html += ">" + dataScopeType[idx].text + "</option>";
                }
                html += "</select>";
                return html;
            }
        }//,{ width: 2, align: "left", field: "memo", title: "备注" }*/
    ]],
    gridFields: [[
        //{ align: "center", field: "Id", title: "角色标识" },
        { width: 5, align: "left", field: "Role", title: "角色名称" },
        { width: 5, align: "left", field: "Caption", title: "角色显示的文本" },
        { width: 5, align: "left", field: "Memo", title: "备注" }
    ]]
};
function allEdit(yes) {
    var ctr;
    foreachTree("#tree", function (row) {
        switch (row.tag) {
            case "details":
            case "addnew":
            case "delete":
            case "update":
            case "save":
            case "validate":
            case "submit":
            case "pullback":
            case "export":
            case "list":
                page.setChecked(row, yes);
                return false;
            case "page":
                ctr = document.getElementById("icChk" + row.id);
                return (ctr && ctr.checked);
        }
        return true;
    });
}
function allData(yes) {
    foreachTree("#tree", function (row) {
        switch (row.tag) {
            case "disable":
            case "enable":
            case "discard":
            case "reset":
            case "lock":
            case "delete":
            case "physical_delete":
                page.setChecked(row, yes);
                return false;
            case "page":
                var ctr = document.getElementById("icChk" + row.id);
                return (ctr && ctr.checked);
        }
        return true;
    });
}
function allAudit(yes) {
    foreachTree("#tree", function (row) {
        switch (row.tag) {
            case "pass":
            case "deny":
            case "back":
            case "reaudit":
            case "submit":
                page.setChecked(row, yes);
                return false;
            case "page":
                var ctr = document.getElementById("icChk" + row.id);
                return (ctr && ctr.checked);
        }
        return true;
    });
}
function allCustom(yes) {
    foreachTree("#tree", function (row) {
        switch (row.tag) {
            case "page":
            case "folder":
            case "prj":
            case "inv":
            case "pro":
            case "sys":
            case "web":
                return true;
            case "export":
            case "addnew":
            case "update":
            case "save":
            case "details":
            case "list":
            case "validate":
            case "submit":
            case "pullback":
            case "delete":
            case "physical_delete":
            case "pass":
            case "deny":
            case "back":
            case "reaudit":
            case "disable":
            case "enable":
            case "discard":
            case "reset":
            case "lock":
                return false;
        }
        if (row.iconCls === "icon-cmd" || row.iconCls === "icon-cus") {
            page.setChecked(row, yes);
        }
        return true;
    });

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
    },
    onCheckSize: function (wid, hei) {
        $("#body2").layout("resize", window.o99);
        $("#grid").datagrid("resize", { width: $("#lyC").width(), height: $("#lyC").height() });
        $("#tree").treegrid("resize", window.o99);
    }
});