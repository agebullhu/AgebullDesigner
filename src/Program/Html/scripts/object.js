function base64Array(base64Str) {

    var bString = atob(base64Str);

    var len = bString.length;

    var arr = new Uint8Array(len);

    while (len--) {

        arr[len] = bString.charCodeAt(len);

    }

    return arr;

}

var vueObject;
var vue_option = {
    el: '#work_space',
    data: {
        /**
         * API前缀
         */
        apiPrefix: '',
        ws_active: false,
        idField: "id",
        title: '',
        stateData: false,
        historyData: false,
        auditData: false,
        currentRow: null,
        list: {
            keyWords: null,
            field: '_any_',
            dataState: -1,
            audit: -1,
            sort: null,
            order: null,
            rows: [],
            page: 1,
            pageSize: 15,
            pageSizes: [15, 20, 30, 50, 100],
            pageCount: 0,
            total: 0,
            multipleSelection: []
        },
        form: {
            title: '',
            readonly: false,
            visible: false,
            edit: false,
            data: {},
            rules: {}
        },
        combos: {},
        types: {

        },
        res: {
            full: true
        },
        showFriend: false
    },
    filters: {
        boolFormater(b) {
            return b ? "是" : "否";
        },
        formatTime(time) {
            return !time || time === '0001-01-01T00:00:00' ? '-' : NewDate(time).format("yyyy-MM-dd hh:mm:ss");
        },
        formatTime2(time) {
            return !time || time === '0001-01-01T00:00:00' ? '-' : NewDate(time).format("hh:mm:ss");
        },
        shortTime(time) {
            return !time || time === '0001-01-01T00:00:00' ? '-' : NewDate(time).format("MM-dd hh:mm:ss");
        },
        formatDate(date) {
            return !date || date === '0001-01-01T00:00:00' ? '-' : NewDate(date).format("yyyy-MM-dd");
        },
        emptyNumber(number) {
            if (number) {
                return number;
            } else {
                return "-";
            }
        },
        formatUnixDate(unix) {
            if (unix === 0)
                return "";
            return new Date(unix * 1000).format("yyyy-MM-dd hh:mm:ss");
        },
        formatNumber(number) {
            if (number) {
                return number.toFixed(4);
            } else {
                return "0.0000";
            }
        },
        formatNumber2(number) {
            if (number) {
                return toThousandsInt(number.toFixed(2));
            } else {
                return "0.00";
            }
        },
        thousandsNumber(number) {
            if (number) {
                return toThousandsInt(number);
            } else {
                return "0";
            }
        },
        formatMoney(number) {
            if (number) {
                return "￥" + number.toFixed(2);
            } else {
                return "￥0.00";
            }
        },
        formatNumber1(number) {
            if (number) {
                return number.toFixed(1);
            } else {
                return "0.0";
            }
        },
        formatNumber0(number) {
            if (number) {
                return number.toFixed(0);
            } else {
                return "0";
            }
        },
        formatHex(number) {
            if (number) {
                return number.toString(16).toUpperCase();
            } else {
                return "-";
            }
        },
        dataStateIcon(dataState) {
            switch (dataState) {
                case "None":
                    return "el-icon-edit";
                case "Enable":
                    return "el-icon-lock";
                case "Disable":
                    return "el-icon-remove";
                case "Orther":
                    return "el-icon-user";
                case "Lock":
                    return "icon_a_end";
                case "Discard":
                    return "el-icon-delete";
                case "Delete":
                    return "el-icon-close";
            }
            return "el-icon-question";
        },
        typeIcon(val) {
            switch (val) {
                case 'app':
                case 'App': return 'el-icon-mobile-phone';
                case 'Root': return 'el-icon-menu';
                case 'Folder': return 'el-icon-folder-opened';
                case 'File': return 'el-icon-document';
                case 'Page': return 'el-icon-document';
                case 'Button': return 'el-icon-mouse';
                case 'Action': return 'el-icon-mouse';
                case 'Position': return 'el-icon-user';
                case 'Person': return 'el-icon-s-custom';
                case 'Area': return 'el-icon-office-building';
                case 'org':
                case 'Organization': return 'el-icon-s-shop';
                case 'Department': return 'el-icon-user';
                case 'tmp': return 'el-icon-document-copy';
                default:
                    return 'el-icon-view';
            }
        },
        longTextFormater(val) {
            if (!val)
                return '　';
            if (val.length > 160)
                return val.substr(0, 156) + ' ......';
            return val;
        }
    },
    methods: {
        doQuery() {
            vue_option.data.list.rows = [];
            vue_option.data.list.page = 0;
            vue_option.data.list.pageCount = 0;
            vue_option.data.list.total = 0;
            this.clearQueryFilter();
            this.loadList();
        },
        clearQueryFilter() {

        },
        exportExcel() {
            var arg = this.getQueryArgs();
            ajax_load("导出" + this.title + "到Excel",
                `${vue_option.data.apiPrefix}/export/xlsx`,
                arg,
                function (data) {
                    var bytes = base64Array(data.item3);
                    var blob = new Blob([bytes], { type: data.item2, endings: "native" });
                    var link = document.getElementById('_export_excel_');
                    if (!link) {
                        let a = document.createElement('a');
                        a.id = '_export_excel_';
                        a.style.display = 'none';
                        document.body.appendChild(a);
                        link = document.getElementById('_export_excel_');
                    }
                    link.href = URL.createObjectURL(blob);
                    link.download = data.item1;
                    link.innerHTML = data.item1;
                    link.click();
                });
        },
        refresh() {
            this.loadList();
        },
        loadList(callback) {
            var that = this;
            var arg = this.getQueryArgs();
            ajax_page_post("载入" + this.title + '列表', `${vue_option.data.apiPrefix}/edit/list`,
                arg,
                function (result) {
                    if (result.success) {
                        that.onListLoaded(result.data);
                        if (typeof callback === "function")
                            callback(result.data);
                        else {
                            vue_option.data.list.rows = result.data.rows;
                            vue_option.data.list.page = result.data.page;

                            vue_option.data.list.pageCount = result.data.pageCount;
                            vue_option.data.list.total = result.data.total;
                        }
                    }
                });
        },
        getQueryArgs() {
            return {
                _field_: vue_option.data.list.field,
                _value_: vue_option.data.list.keyWords,
                _state_: vue_option.data.list.dataState,
                _audit_: vue_option.data.list.audit,
                _page_: vue_option.data.list.page,
                _size_: vue_option.data.list.pageSize,
                _sort_: vue_option.data.list.sort,
                _order_: vue_option.data.list.order
            };
        },
        onListLoaded(data) {
            if (data.rows) {
                for (var idx = 0; idx < data.rows.length; idx++) {
                    var row = data.rows[idx];
                    if (!row.selected)
                        row.selected = false;
                    this.toFullData(row);
                }
            }
            else {
                data.rows = [];
            }
        },
        toFullData(row) {
            if (this.checkListData)
                this.checkListData(row);
        },
        dblclick(row, column, event) {
            this.currentRow = row;
            this.doEdit();
        },
        sizeChange(size) {
            vue_option.data.list.pageSize = size;
            this.loadList();
        },
        pageChange(page) {
            vue_option.data.list.page = page;
            this.loadList();
        },
        onSort(arg) {
            vue_option.data.list.sort = arg.prop;
            vue_option.data.list.order = arg.order === "ascending" ? "asc" : "desc";
            this.loadList();
        },
        currentRowChange(row) {
            this.currentRow = row;
        },
        selectionRowChange(val) {
            this.list.multipleSelection = val;
        },
        doAddNew() {
            var data = this.form;
            data.data = this.createNew();
            if (!data.data)
                return;
            data.readonly = false;
            data.edit = false;
            if (this.prepareEdit())
                data.visible = true;
        },
        createNew() {
            if (this.getDef)
                return this.getDef();
            return {};
        },
        doEdit() {
            if (!this.currentRow) {
                common.showError('编辑' + this.title, '请单击一行');
                return;
            }
            var data = this.form;
            data.data = this.copyData(this.currentRow);
            data.edit = data.data[this.idField] > 0;
            data.readonly = data.edit && this.checkReadOnly(data.data);
            this.prepareEdit();
            data.title = "【" + this.title + "】" + (data.readonly ? "查看(执行解锁操作后可编辑)" : "编辑");
            if (this.prepareEdit())
                data.visible = true;
            this.onEdit();
        },
        prepareEdit() {
            return true;
        },
        onEdit() {
        },
        copyData(row) {
            let data = {};
            extend(data, row);
            return data;
        },
        checkReadOnly(row) {
            return !row || (row.dataState && row.dataState != 'None');
        },
        save() {
            var that = this;
            var data = that.form;
            var title = this.form.edit ? "更新" + this.title : "新增" + this.title;
            var action = this.form.edit ? "edit_update" : "edit_addnew";
            this.$refs['dataForm'].validate((valid) => {
                if (!valid) {
                    common.showError('编辑' + this.title, '数据校验出错误，请修正后再保存');
                    return false;
                }
                var isEdit = data.edit;
                ajax_action_post(title, action, data.edit ? `${vue_option.data.apiPrefix}/edit/update?id=${data.data[that.idField]}` : `${vue_option.data.apiPrefix}/edit/addnew`, data.data,
                    function (result) {
                        if (result.success) {
                            common.showMessage(title, '操作成功');
                            data.visible = false;
                            result.data.edit = isEdit;
                            that.onSaved(result.data);
                            that.loadList();
                        }
                    },
                    function (result) {
                        common.showError(title, result && result.message ? result.message : title + '失败');
                    });
                return true;
            });
        },
        onSaved(data) {

        },
        doDelete() {
            var me = this;
            this.mulitSelectAction("删除" + this.title, "edit_delete", `${vue_option.data.apiPrefix}/edit/delete`, function (row) {
                if (!me.stateData)
                    return true;
                return row.dataState === 'None'
                    || row.dataState === 'Discard'
                    || row.dataState === 'Delete';
            }, ids => {
                me.onDeleted(ids);
            });
        },
        onDeleted(ids) {

        },
        handleDataCommand(command) {
            switch (command) {
                case "Disable": this.doDisable(); break;
                case "Discard": this.doDiscard(); break;
                case "Reset": this.doReset(); break;
                case "Lock": this.doLock(); break;
                case "Unlock": this.doUnlock(); break;
                case "Back": this.doBack(); break;
                case "Pass": this.doPass(); break;
                case "Deny": this.doDeny(); break;
                case "ReDo": this.doReDo(); break;
            }
        },
        doEnable() {
            this.mulitSelectAction("启用" + this.title, "state_enable", `${vue_option.data.apiPrefix}/state/enable`, function (row) {
                return !row.IsFreeze
                    && row.dataState !== 'Enable'
                    && row.dataState !== 'Discard'
                    && row.dataState !== 'Delete';
            });
        },
        doDisable() {
            this.mulitSelectAction("禁用" + this.title, "state_disable", `${vue_option.data.apiPrefix}/state/disable`, function (row) {
                return !row.IsFreeze
                    && row.dataState !== 'Disable'
                    && row.dataState !== 'Discard'
                    && row.dataState !== 'Delete';
            });
        },
        doDiscard() {
            this.mulitSelectAction("废弃", "state_discard", `${vue_option.data.apiPrefix}/state/discard`, function (row) {
                return !row.IsFreeze
                    && row.dataState === 'None';
            });
        },
        doReset() {
            this.mulitSelectAction("重置" + this.title, "state_reset", `${vue_option.data.apiPrefix}/state/reset`);
        },
        doLock() {
            this.mulitSelectAction("锁定" + this.title, "state_lock", `${vue_option.data.apiPrefix}/state/lock`, function (row) {
                return !row.IsFreeze;
            });
        },
        doUnlock() {
            this.mulitSelectAction("解锁" + this.title, "state_unlock", `${vue_option.data.apiPrefix}/state/unlock`, function (row) {
                return row.IsFreeze;
            });
        },
        doBack() {
            this.mulitSelectAction("退回" + this.title, "audit_back", `${vue_option.data.apiPrefix}/audit/back`, function (row) {
                return !row.IsFreeze
                    && (row.auditState === 'Submit' || row.auditState === 'Pass');
            });
        },
        doPass() {
            this.mulitSelectAction("通过" + this.title, "audit_pass", `${vue_option.data.apiPrefix}/audit/pass`, function (row) {
                return !row.IsFreeze
                    && (row.auditState === 'Submit' || row.auditState === 'Pass');
            });
        },
        doDeny() {
            this.mulitSelectAction("否决" + this.title, "audit_deny", `${vue_option.data.apiPrefix}/audit/deny`, function (row) {
                return !row.IsFreeze
                    && (row.auditState === 'Submit' || row.auditState === 'Pass');
            });
        },
        doReDo() {
            this.mulitSelectAction("重新审核" + this.title, "audit_redo", `${vue_option.data.apiPrefix}/audit/redo`, function (row) {
                return row.auditState === 'Pass'
                    || row.auditState === 'Deny'
                    || row.auditState === 'End';
            });
        },
        getSelectedRows(filter, field) {
            var rows = this.list.multipleSelection;
            if (!rows || rows.length === 0)
                return null;
            if (!field)
                field = this.idField;
            var ids = '';
            let first = true;
            for (var idx = 0; idx < rows.length; idx++) {
                if (!filter || filter(rows[idx])) {
                    if (first)
                        first = false;
                    else
                        ids += ',';
                    ids += rows[idx][field];
                }
                else { this.$refs.dataTable.toggleRowSelection(rows[idx], false); }
            }
            return ids;
        },
        setArgument(arg) {

        },
        mulitSelectAction(title, action, api, filter, callback, arg, field) {
            var that = this;
            var ids = this.getSelectedRows(filter, field);
            if (!ids) {
                that.$notify({
                    message: '请选择一或多行合格的数据,不适合当前操作的选择已被自动清除！',
                    duration: 1500,
                    type: 'error'
                });
                return;
            }
            vueObject.$confirm(`你确定要${title}所选择的数据吗?`, title, {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                if (!arg)
                    arg = { selects: ids };
                else
                    arg.selects = ids;
                that.setArgument(arg);
                ajax_action_post(title, action, api, arg, function (result) {
                    if (result.success) {
                        that.$notify({
                            message: result.message ? result.message : '操作成功',
                            type: 'success'
                        });
                        if (callback)
                            callback(arg.selects);
                        that.loadList();
                    }
                    else {
                        that.$notify({
                            message: result.message ? '操作失败:' + result.message : '操作失败',
                            type: 'error'
                        });
                    }
                }, function (result) {
                    that.$notify({
                        message: result && result.message ? '操作失败:' + result.message : '操作失败',
                        type: 'error'
                    });
                });
            });
        }
    },
    optionEx(){},
    ready(init) {
        try {
            this.optionEx(this);
            vueObject = new Vue(this);
            if (this.websocket)
                this.websocket.open();
            if (init)
                init(vueObject);
            if (!this.data.res.full)
                ajax_load("载入权限", `/appManage/v1/res/page`, null, (data) => vueObject.res = result.data);
        } catch (e) {
            console.error(e);
        }
    }
};

function extend_vue_option(option) {
    if (option.data)
        extend(vue_option.data, option.data);
    if (option.form)
        extend(vue_option.data.form, option.form);
    if (option.rules)
        extend(vue_option.data.form.rules, option.rules);
    if (option.types)
        extend(vue_option.data.types, option.types);
    if (option.filters) {
        extend(vue_option.filters, option.filters);
        extend(vue_option.methods, option.filters);
    }
    if (option.overrides)
        extend(vue_option.methods, option.overrides);
    if (option.commands)
        extend(vue_option.methods, option.commands);
    if (option.methods)
        extend(vue_option.methods, option.methods);
    if (option.optionEx)
        vue_option.optionEx = option.optionEx;
    if (option.websocket)
        vue_option.websocket = option.websocket;

    if (option.onReady) {
        vue_option.ready(option.onReady);
    }
}

function extend_data(data) {
    extend(vue_option.data, data);
}
function extend_filter(filters) {
    extend(vue_option.filters, filters);
    extend(vue_option.methods, filters);
}
function extend_methods(methods) {
    extend(vue_option.methods, methods);
}
