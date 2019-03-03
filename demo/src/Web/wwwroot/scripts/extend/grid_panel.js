
/**
 * 表格对象深度封装
 */
function GridPanel() {
    this.stateData = false;
    this.historyData = false;
    this.auditData = false;
    /**
     * 绑定的对象
     */
    this.tag = null;
    /**
     * 列表数据地址
     */
    this.listUrl = null;
    /**
     * 命令执行地址
     */
    this.cmdPath = "";
    /**
     * 表节点名称
     */
    this.elementId = "#grid";
    /**
     * 工具栏节点名称
     */
    this.toolbar = "#pageToolbarEx";
    /**
     * 默认每页数据行数
     */
    this.pageSize = 20;
    /**
     * 默认支持命令按钮的名称后缀
     */
    this.elementEx = "";
    /**
     * 主键字段名称
     */
    this.idField = "Id";
    /**
     * Check字段名称
     */
    this.checkField = "IsSelected";
    /**
     * 是否跳过Check处理
     */
    this.skipCheck = false;
    /**
     * 是否在数据载入后还原选择
     */
    this.checkByLoaded = false;
    /**
     * 行选择Check时的扩展方法
     * 参数为当前行
     */
    this.onCheck = null;
    /**
     * 行取消Check时的扩展方法
     * 参数为当前行
     */
    this.onUncheck = null;
    /**
     * 编辑能力掩码
     * 1 增 2 改 4 删 8查
     */
    this.editMask = 0xF;
    /**
     * 新增的执行扩展方法
     */
    this.addNew = null;
    /**
     * 编辑的执行扩展方法
     */
    this.edit = null;
    /**
     * 删除的执行方法(跳过内部方法)
     */
    this.remove = null;
    /**
     * 详细命令的扩展方法
     */
    this.details = null;
    /**
     * 查询方法(跳过内部方法)
     */
    this.query = null;
    /**
     * 默认查询参数
     */
    this.queryParams = null;
    /**
     * 取查询参数的扩展方法
     */
    this.getQueryParams = null;
    /**
     * 列对象
     */
    this.columns = null;
    /**
     * 当前选择中对象
     */
    this.selectData = null;
    /**
     * 是否显示校验UI
     */
    this.validate = false;
    /**
     * 是否显示为表格树
     */
    this.isTree = false;
    /**
     * 展开行详细(未实现)的扩展方法
     */
    this.onExpandRow = null;
    /**
     * 是否有列表详细UI
     */
    this.isListDetails = false;
    /**
     * 数据载入后呈现前的扩展方法
     */
    this.afterDataLoaded = null;
    /**
     * 行被选择的扩展方法;参数为当前行数据
     */
    this.onRowSelected = null;
    /**
     * 行被双击的扩展方法;参数为当前行数据
     */
    this.onRowDoubleClick = null;
    /**
    * 表格设置参数(初始化后扩展无效)
    */
    this.gridOption = null;

    /**
     * 依赖于Check选择的按钮集合
     * @type Array
     */
    this.bySelectButtons = [];

    /**
     * 依赖于当前选择的按钮集合
     */
    this.bySingleButtons = [];


    /**
     * 是否处在选择处理方法中
     * @private 
     */
    this.isSelecting = false;


    /**
     * 初始化表格设置的扩展方法(初始化前)
     * @abstract 
     */
    this.initGridOptionEx = null;


    /**
     * 表格数据
     */
    this.rows = [];
}
/**
 * 构建表格UI
 * @param {object} options 表格设置参数
 */
GridPanel.prototype.createGrid = function (options) {
    var me = this;
    options.onBeforeLoad = function (arg) {
        if (!me.listUrl) {
            me.rows = [];
            me.selectData = null;
            me.execGridMethod("loadData", me.rows);
            me.execGridMethod("clearChecked");
            return false;
        }
        doPost("载入", me.listUrl, arg, function (result) {
            foreachDatas(me.rows, row => {
                row.selected = false;
                row.IsSelected = false;
                row[me.checkField] = false;
            });
            me.rows = result.data || [];
            me.selectData = null;
            me.execGridMethod("loadData", me.rows);
            me.execGridMethod("clearChecked");
        });
        return false;
    };

    var g = $(me.elementId);
    if (me.isTree)
        g.treegrid(options);
    else {
        g.datagrid(options);
        g.datagrid('getPager').pagination({
            displayMsg: '当前显示从 [{from}] 到 [{to}] 共[{total}]条记录',
            onSelectPage: function (pPageIndex, pPageSize) {
                //改变opts.pageNumber和opts.pageSize的参数值，用于下次查询传给数据层查询指定页码的数据   
                var gridOpts = $(me.elementId).datagrid('options');
                gridOpts.pageNumber = pPageIndex;
                gridOpts.pageSize = pPageSize;
                me.reload();
            }
        });
    }
};
/**
 * 执行表格的对应方法
 * @param {function} func 方法
 * @param {object} arg 参数
 * @returns {object} 执行方法的返回
 */
GridPanel.prototype.execGridMethod = function (func, arg) {
    var me = this;
    if (me.isTree)
        return $(me.elementId).treegrid(func, arg);
    else
        return $(me.elementId).datagrid(func, arg);
};
GridPanel.prototype.reload = function () {
    var me = this;
    me.selectData = null;
    me.execGridMethod("reload");
};
/**
 * 选择或反选行
 * @param {object} row 行数据
 * @param {function} check 方法
 * @returns {void} 
 */
GridPanel.prototype.selectRow = function (row, check) {
    if (!row)
        return;
    var me = this;
    if (me.isTree) {
        me.execGridMethod(check ? "checkNode" : "uncheckNode", row[me.idField]);
    }
    else {
        var index = me.execGridMethod("getRowIndex", row);
        if (index >= 0) {
            me.execGridMethod(check ? "checkRow" : "uncheckRow", index);
        }
    }
};
/**
 * 选择或反选行(树形表格无效)
 * @param {int} index 行号
 * @param {function} check 方法
 * @returns {void} 
 */
GridPanel.prototype.selectLine = function (index, check) {
    var me = this;
    if (!me.isTree && index >= 0) {
        me.execGridMethod(check ? "checkRow" : "uncheckRow", index);
    }
};
/**
 * 扩展表格设置
 * @param {object} options 表格设置参数
 * @returns {void} 
 */
GridPanel.prototype.appendGridOption = function (options) {
    var me = this;
    me.createGrid(options);
};
/**
 * 取得当前选择的行
 * @returns {object} 当前选择的行
 */
GridPanel.prototype.getSelectRow = function () {
    var me = this;
    if (me.selectData) {
        me.checkedCurRow();
        return me.selectData;
    }
    me.selectData = me.execGridMethod("getSelected");
    if (me.selectData) {
        me.checkedCurRow();
        return me.selectData;
    }
    var sels = me.execGridMethod("getChecked");
    if (sels && sels.length >= 1) {
        me.selectData = sels[0];
        if (sels.length > 1) {
            showWarning("系统选了你选定的多行中的一行。请尽量不要这样操作，因为你不确定能打开哪行数据！最正确的姿势是点选一行,使之高亮显示后点你要操作的按钮!");
        }
    }
    return me.selectData; //me.execGridMethod("getSelected");
};
/**
 * 设置当前行的CHECK
 * @returns {void} 
 */
GridPanel.prototype.checkedCurRow = function () {
    var me = this;
    if (!me.selectData)
        return;
    me.selectData[me.checkField] = true;
    if (me.isTree)
        me.execGridMethod("checkNode", me.selectData[me.idField]);
    else
        me.execGridMethod("checkRow", me.execGridMethod("getRowIndex", me.selectData));
};
/**
 * 设置当前行的CHECK
 * @returns {void} 
 */
GridPanel.prototype.uncheckedCurRow = function () {
    var me = this;
    if (!me.selectData)
        return;
    me.selectData[me.checkField] = false;
    if (me.isTree)
        me.execGridMethod("uncheckedCurRow", me.selectData[me.idField]);
    else
        me.execGridMethod("uncheckedCurRow", me.execGridMethod("getRowIndex", me.selectData));
};
GridPanel.prototype.createEditor = function (action, id, options) {
    var me = this;
    var ed = new EditDialog();
    if (!options)
        options = {};
    options.apiPrefix = me.cmdPath;
    var editor = ed.createEditor(action, id, options);
    editor.checkReadOnly = function (isAddNew) {
        if (isAddNew) {
            return !mainPageOptions.checkRole(null, "#btnAdd" + me.elementEx);
        }
        else {
            return !mainPageOptions.checkRole(null, "#btnEdit" + me.elementEx);
        }
    };
    return editor;
};
/**
 * 取得表格的设置参数(初始化后无效)
 * @returns {object} 表格的设置参数
 */
GridPanel.prototype.getGridOption = function () {
    var me = this;
    if (me.gridOption)
        return me.gridOption;
    if (!mainPageOptions.preQueryArgs.size) {
        return me.gridOption = {
            plain: true,
            pageSize: 20,
            sortName: me.idField,
            sortOrder: "desc",
            pageNumber: 0,
            striped: true,
            pagination: true,
            rownumbers: true,
            fitColumns: true,
            selectOnCheck: false,
            checkOnSelect: false,
            singleSelect: true,
            border: false,
            loadMsg: "正在载入数据,请稍候……",
            //emptyMsg: "没有数据……",
            pageList: [10, 15, 20, 25, 30, 40, 50, 75, 100, 200, 500]
        };
    }
    return me.gridOption = {
        plain: true,
        pageSize: mainPageOptions.preQueryArgs.size,
        sortName: mainPageOptions.preQueryArgs.sort,
        sortOrder: mainPageOptions.preQueryArgs.order,
        pageNumber: mainPageOptions.preQueryArgs.page,
        striped: true,
        border: false,
        pagination: true,
        rownumbers: true,
        fitColumns: true,
        selectOnCheck: false,
        checkOnSelect: false,
        singleSelect: true,
        loadMsg: "正在载入数据,请稍候……",
        //emptyMsg: "没有数据……",
        pageList: [10, 15, 20, 25, 30, 40, 50, 75, 100, 200, 500]
    };
};
/**
 * 取得初始化高度(初始化后无效)
 * @returns {void} 
 */
GridPanel.prototype.initHeight = function () {
    var me = this;
    return me.isListDetails ? "100%" : $("#rBody").height();
};
GridPanel.prototype.insertRow = function (data) {
    if (!data)
        return;
    var me = this;
    me.execGridMethod("appendRow", data);
};
/**
 * 更新一行的显示
 * @param {object} data 行数据
 * @param {int} idx 行序号
 * @returns {void} 
 */
GridPanel.prototype.updateRow = function (data, idx) {
    if (!data)
        return;
    var me = this;
    me.execGridMethod("updateRow", {
        index: idx,
        row: data
    });
};
/**
 * 编辑当前行
 * @returns {void} 
 */
GridPanel.prototype.editRow = function () {
    var me = this;
    if (!me.edit)
        return;
    var value = me.getSelectRow();
    if (!value) {
        showMessage("编辑", "请选择一行数据(多选也不行哟");
        return;
    }
    me.edit(value[me.idField], value, me.tag);
};
/**
 * 执行新增操作
 * @private
 * @returns {void} 
 */
GridPanel.prototype.doAddNew = function () {
    var me = this;
    me.addNew(me.tag);
};
/**
 * 执行详细操作
 * @private
 * @param {object} me 当前表格扩展对象
 * @returns {void} 
 */
GridPanel.prototype.doDetails = function (me) {
    var value = me.getSelectRow();
    if (!value) {
        showMessage("详细", "请选择一行数据");
    } else {
        me.details(value[me.idField], value, me.tag);
    }
};
/**
 * check变化后同步依赖于Check选择的按钮状态
 */
GridPanel.prototype.checkChangedForButtons = function () {
    var me = this;
    if (!me.isInitialized)
        return;
    var hase = 0;
    var rows = me.execGridMethod("getChecked");
    if (!rows || rows.length === 0) {
        if (!me.selectData)
            me.selectData = me.execGridMethod("getSelected");
        if (me.selectData)
            hase = 1;
    } else {
        hase = 2;
    }
    var i;
    if (hase === 0) {
        for (i = 0; i < me.bySelectButtons.length; i++) {
            disableButton(me.bySelectButtons[i]);
        }
        for (i = 0; i < me.bySingleButtons.length; i++) {
            disableButton(me.bySingleButtons[i]);
        }
    } else if (hase === 1) {
        for (i = 0; i < me.bySelectButtons.length; i++) {
            enableButton(me.bySelectButtons[i]);
        }
        for (i = 0; i < me.bySingleButtons.length; i++) {
            enableButton(me.bySingleButtons[i]);
        }
    } else {
        for (i = 0; i < me.bySelectButtons.length; i++) {
            enableButton(me.bySelectButtons[i]);
        }
        for (i = 0; i < me.bySingleButtons.length; i++) {
            disableButton(me.bySingleButtons[i]);
        }
    }
};
/**
 * 行选择变化后的处理
 * @private 
 * @param {object} data 当前选择的数据
 * @param {boolean} dbClick 是否为双击
 * @returns {void} 
 */
GridPanel.prototype.onRowSelectChanged = function (data, dbClick) {
    var me = this;
    if (me.isSelecting || !me.isInitialized)
        return;
    me.isSelecting = true;
    var old = me.selectData;
    if (data) {
        me.selectData = data;
    }
    try {
        me.checkChangedForButtons();
        if (!me.selectData) {
            if (!data) {
                me.isSelecting = false;
                return;
            }
        }
        if (dbClick) {
            if (me.onRowDoubleClick)
                me.onRowDoubleClick(data);
            else me.editRow();
        } else {
            if (old != me.selectData && me.onRowSelected)
                me.onRowSelected(me.selectData);
        }
    } catch (e) {
        console.error("%s.%s() ： %s", typeof (this), "onRowSelectChanged", e);
        $.messager.alert("发生错误", e);
    }
    me.isSelecting = false;
};
/**
 * 初始化数据校验UI
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.initValidate = function () {
    var me = this;
    if (document.getElementById("btnValidate" + me.elementEx)) {
        me.validate = true;
    }
    if (me.validate) {
        for (var i = 0; i < me.columns[0].length; i++)
            me.columns[0][i].styler = vlStyle;
        me.columns[0].unshift({
            styler: vlStyle,
            align: "center",
            title: "校验结果",
            field: "msg",
            rowspan: me.columns.length,
            hidden: true,
            formatter: function (val) {
                if (!val)
                    return "";
                return val;
            }
        });
    }
};
/**
 * 初始化表格设置
 * @returns {object} 配置 
 */
GridPanel.prototype.initGridOptions = function () {
    var me = this;
    var options = me.getGridOption();
    options.toolbar = me.toolbar;
    options.height = me.initHeight();
    options.idField = me.idField;
    options.columns = me.columns;
    options.onSelect = function (a, b) {
        if (me.isTree)
            me.onRowSelectChanged(a, false);
        else
            me.onRowSelectChanged(b, false);
    };
    /*if (!me.onExpandRow) {
        options.onDblClickRow = function (a, b) {
            if (me.isTree)
                me.onRowSelectChanged(a, true);
            else
                me.onRowSelectChanged(b, true);
        };
    }*/
    options.loadFilter = function (data) {
        return me.loadFilter(data);
    };
    options.onBeforeLoad = function () {
        me.execGridMethod("uncheckAll");
        me.execGridMethod("unselectAll");
        me.onRowSelectChanged(null, false);
    };
    options.onDblClickRow = function (idx, row) {
        me.onRowSelectChanged(row, true);
    };
    options.onLoadSuccess = function () {
        me.onRowSelectChanged(null, false);
        if (me.checkByLoaded) {
            setTimeout(function () {
                me.initCheck();
            }, 10);
        }
    };
    if (!me.skipCheck)
        me.checkHandler(options);
    if (me.onExpandRow) {
        options.view = detailview;
        options.onExpandRow = me.onExpandRow;
        options.detailFormatter = function (index, row) {
            return "<div id='row-d-" + row[me.idField] + "' style='border: silver solid 1px; padding:0px 0'></div>";
        };
    }
    return options;
};
/**
 * 初始化数据的check
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.loadFilter = function (data) {
    var me = this;
    if (!data) {
        console.log("loadFilter=>:nullValue");
        data = { succeed: false, rows: [], total: 0 };
    } else if (data instanceof Array) {
        data = { succeed: true, rows: data, total: data.length };
    }
    else if (data.succeed || (data.succeed === undefined && data.rows)) {
        if (me.validate) {
            me.execGridMethod("hideColumn", "msg");
        }
    } else {
        console.log("loadFilter=>:%s", data.message);
        showMessage("数据载入", data.message);
        data = { rows: [], total: 0 };
    }
    if (me.afterDataLoaded)
        me.afterDataLoaded(data);
    return data;
};
/**
 * 初始化数据的check
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.initCheck = function () {
    var me = this;
    
    if (me.isTree) {
        foreachTree(me.elementId, function (row) {
            if (row[me.checkField]) {
                me.execGridMethod("checkNode", row[me.idField]);
            }
        });
    } else {
        foreachGridRows(me.elementId, function (row) {
            if (row[me.checkField]) {
                me.execGridMethod("checkRow", me.execGridMethod("getRowIndex", row));
            }
        });
    }
};
/**
 * 初始化Check的事件处理方法
 * @param {string} options 配置
 * @returns {void}  
 */
GridPanel.prototype.checkHandler = function (options) {
    var me = this;
    options.onCheck = function (idx, data) {
        var row = me.isTree ? idx : data;
        row[me.checkField] = true;
        me.checkChangedForButtons();
        if (me.onCheck)
            me.onCheck(row);
    };
    options.onUncheck = function (idx, data) {
        var row = me.isTree ? idx : data;
        row[me.checkField] = false;
        me.checkChangedForButtons();
        if (me.onUncheck)
            me.onUncheck(row);

    };
    options.onCheckAll = function (rows) {
        me.checkChangedForButtons();
        if (rows.rows)
            rows = rows.rows;
        var i;
        for (i = 0; i < rows.length; i++) {
            rows[i][me.checkField] = true;
        }
        if (me.onCheck)
            for (i = 0; i < rows.length; i++) {
                me.onCheck(rows[i]);
            }
    };
    options.onUncheckAll = function (rows) {
        me.checkChangedForButtons();
        if (rows.rows)
            rows = rows.rows;
        var i;
        for (i = 0; i < rows.length; i++) {
            rows[i][me.checkField] = false;
        }
        if (me.onUncheck)
            for (i = 0; i < rows.length; i++) {
                me.onUncheck(rows[i]);
            }
    };
};
/**
 * 执行初始化方法
 * @public 
 * @param {boolean} keepToolbar 跳过工具栏生成
 * @returns {void} 
 */
GridPanel.prototype.initialize = function (keepToolbar) {
    var me = this;
    me.initValidate();
    var options = me.initGridOptions();
    if (me.initGridOptionEx)
        me.initGridOptionEx(options);
    me.createGrid(options);
    if (!keepToolbar)
        me.initToolbar(me);
    if (me.listUrl) {
        setTimeout(function () {
            me.changedUrl(me.listUrl);
        }, 100);
    }
    me.isInitialized = true;
};
/**
 * 处理列表URL
 * @public
 * @param {string} url 新的URL
 * @returns {void} 
 */
GridPanel.prototype.changedUrl = function (url) {
    var me = this;
    me.listUrl = url;
    me.doQuery();
};
/**
 * 执行查询操作
 * @public 
 * @returns {void} 
 */
GridPanel.prototype.doQuery = function () {
    var me = this;
    var args = me.getQueryParams ? me.getQueryParams() : {};
    if (!me.isInitialized) {
        var option = me.getGridOption();
        //option.url = me.listUrl;
        option.queryParams = args;
    } else {
        me.execGridMethod({
            url: me.listUrl,
            queryParams: args
        });
    }
};
/**
 * 执行删除操作
 * @private
 * @param {string} me 自有对象
 * @returns {void} 
 */
GridPanel.prototype.doRemove = function (me) {
    me.doRemote("删除", me.cmdPath + "edit/delete", "确认删除吗?", null, function (row) {
        if (!me.stateData || row.dataState === 255)
            return true;
        if (row.dataState !== 0)
            return false;
        if (me.auditData) {
            return row.AuditState === 0;
        } return true;
    });
};
/**
 * 执行还原操作
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.doReset = function () {
    var me = this;
    me.doRemote("还原", me.cmdPath + "state/reset", null, null, function (row) {
        return row.dataState !== 0;
    });
};
/**
 * 执行废弃操作
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.doDiscard = function () {
    var me = this;
    me.doRemote("废弃", me.cmdPath + "state/discard", null, null, function (row) {
        if (row.dataState !== 0)
            return false;
        if (me.auditData) {
            return row.AuditState <= 1;
        } return true;
    });
};
/**
 * 执行锁定操作
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.doLock = function () {
    var me = this;
    me.doRemote("锁定", me.cmdPath + "state/lock", null, null, function (row) {
        if (me.auditData) {
            return row.AuditState === 3;
        } else return row.dataState < 0x10;
    });
};
/**
 * 执行启用操作
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.doEnable = function () {
    var me = this;
    me.doRemote("启用", me.cmdPath + "state/enable", null, null, function (row) {
        if (me.auditData) {
            return row.AuditState === 4 && row.dataState !== 1 && row.dataState < 0x10;//&& !row.IsFreeze;
        } else
            return row.dataState !== 1 && row.dataState < 0x10;// && !row.IsFreeze;
    });
};
/**
 * 执行禁用操作
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.doDisable = function () {
    var me = this;
    me.doRemote("禁用", me.cmdPath + "state/disable", null, null, function (row) {
        if (me.auditData) {
            return row.AuditState === 4 && row.dataState === 1;//&& !row.IsFreeze;
        } else return row.dataState === 1;// && !row.IsFreeze;
    });
};
/**
 * 执行数据校验操作
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.doValidate = function () {
    var me = this;
    me.doRemote("数据校验", me.cmdPath + "audit/validate", null, function (result) {
        me.setValidateMessage(result, "数据校验");
    }, function (row) {
        return row.AuditState <= 2;
    });
};
/**
 * 执行提交审核操作
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.doSubmit = function () {
    var arg = {};
    var me = this;
    if (!me.getMulitArgs(function (row) {
        return row.AuditState <= 2;
    }, arg))
        return;
    doSubmitJob(arg.selects, me.cmdPath, function () {
        me.reload();
    });

};
/**
 * 拉回
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.doPullback = function () {
    var me = this;
    me.doRemote("拉回编辑", me.cmdPath + "audit/pullback", null, null, function (row) {
        return row.AuditState === 2;
    });
};
/**
 * 执行反审核操作
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.doReAudit = function () {
    var me = this;
    me.doRemote("重新审核", me.cmdPath + "audit/redo", null, null, function (row) {
        return (row.AuditState >= 3 || row.AuditState <= 4);// && !row.IsFreeze;
    });
};
/**
 * 执行审核通过操作
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.doPass = function () {
    var me = this;
    me.doRemote("审核通过", me.cmdPath + "audit/pass", null, function (result) {
        me.setValidateMessage(result, "审核通过");
    }, function (row) {
        return row.AuditState <= 2/* && row.CanAudit*/;
    });
};
/**
 * 执行审核否决操作
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.doDeny = function () {
    var me = this;
    me.doRemote("审核否决", me.cmdPath + "audit/deny", null, null, function (row) {
        return row.AuditState === 2;
    });
};
/**
 * 执行退回重做操作
 * @private 
 * @returns {void} 
 */
GridPanel.prototype.doBack = function () {
    var me = this;
    me.doRemote("退回重做", me.cmdPath + "audit/back", null, null, function (row) {
        return row.AuditState === 2;
    });
};
/**
 * 单选操作参数
 * @returns {void} 
 */
GridPanel.prototype.getSingleArg = function () {
    var me = this;
    var row = me.execGridMethod("getSelected");
    if (row == null) {
        var rows = me.execGridMethod("getChecked");
        if (rows && rows.length === 1) {
            row = rows[0];
        }
    }
    if (row) {
        if (me.isTree)
            me.execGridMethod("select", row[me.idField]);
        else
            me.execGridMethod("selectRecord", row[me.idField]);
    }
    return row;
};
/**
 * 执行单选操作
 * @public 
 * @param {string} title 操作标题
 * @param {function} check 操作前置条件检查（参数为当前选择对象）
 * @param {function} action 操作动作（参数1 为当前选择对象的ID，参数2为当前选择对象，参数三为表格对象的附属对象（一般为当前页面））
 * @returns {void} 
 */
GridPanel.prototype.doSingle = function (title, check, action) {
    var me = this;
    me.selectData = me.getSingleArg();
    if (me.selectData == null) {
        showWarning("请选择一行数据");
        return;
    }
    if (check && !check(me.selectData)) {
        showWarning("请选择<B>符合操作条件数据</B>后执行操作");
        return;
    }
    me.execGridMethod("uncheckAll");
    action(me.selectData[me.idField], me.selectData, me.tag);
};
/**
 * 执行单选远程操作
 * @public 
 * @param {string} title 操作标题
 * @param {string} url 远程URL
 * @param {string} action 远程URL的action参数
 * @param {string} confirmMessage 确认操作的消息
 * @param {function} callBack 执行完成后的回调方法
 * @param {function} check 操作前置条件检查（参数为当前选择对象）
 * @returns {void} 
 */
GridPanel.prototype.doSingleRemote = function (title, url, action, confirmMessage, callBack, check) {
    var me = this;
    me.selectData = me.getSingleArg();
    if (me.selectData == null) {
        showWarning("请选择一行数据");
        return;
    }
    if (check && !check(me.selectData)) {
        showWarning("请选择<B>符合操作条件数据</B>后执行操作");
        return;
    }
    me.execGridMethod("uncheckAll");

    call_remote(title, url, action, { id: me.selectData[me.idField] }, function (res) {
        if (callBack) {
            callBack(res);
        }
        me.reload();
    }, confirmMessage);
};
/**
 * 多选操作参数
 * @public 
 * @param {function} check 操作前置条件检查（参数为当前选择对象）
 * @param {object} arg 其它参数
 * @returns {boolean} 是否有参数
 */
GridPanel.prototype.getMulitArgs = function (check, arg) {
    var me = this;
    var checkSelect;
    if (check) {
        checkSelect = function(r) {
            if (!check(r)) {
                me.selectRow(r, false);
                return false;
            }
            return true;
        };
    } else {
        checkSelect = function() {
            return true;
        };
    }
    arg.selects = me.isTree
        ? getTreeSelects(me.elementId, me.idField, me.checkField, checkSelect)
        : getGridSelects(me.elementId, me.idField, me.checkField, checkSelect);
    if (!arg.selects) {
        var row = me.execGridMethod("getSelected");
        if (!row) {
            showWarning("请选择(勾选一或多行或单击一行使之高亮显示)数据后执行操作");
            return false;
        }
        if (!checkSelect(row)) {
            showWarning("请选择(勾选一或多行或单击一行使之高亮显示)<B>符合操作条件数据</B>后执行操作");
            return false;
        }
        me.checkedCurRow();
        arg.selects = row[me.idField];
    }
    return true;
};
/**
 * 执行多选操作
 * @public 
 * @param {string} title 操作标题
 * @param {function} check 操作前置条件检查（参数为当前选择对象）
 * @param {function} action 操作动作（参数1 为当前选择对象的ID，参数2为当前选择对象，参数三为表格对象的附属对象（一般为当前页面））
 * @param {string} confirmMessage 操作动作（参数1 为当前选择对象的ID，参数2为当前选择对象，参数三为表格对象的附属对象（一般为当前页面））
 * @returns {void} 
 */
GridPanel.prototype.doMulit = function (title, check, action, confirmMessage) {
    var arg = {};
    var me = this;
    if (!me.getMulitArgs(check, arg))
        return;
    if (!confirmMessage)
        confirmMessage = "确定对你选择的数据执行<B>[" + title + "]</B>操作吗?";
    $.messager.confirm(title, confirmMessage, function (s) {
        if (s) {
            action(arg.selects, me.tag);
        }
    });
};
/**
 * 执行多选远程操作
 * @public 
 * @param {string} title 操作标题
 * @param {string} api 远程URL
 * @param {string} confirmMessage 确认操作的消息
 * @param {function} callBack 执行完成后的回调方法
 * @param {function} check 操作前置条件检查（参数为当前选择对象）
 * @param {object} arg 其它参数
 * @returns {void} 
 */
GridPanel.prototype.doRemote = function (title, api, confirmMessage, callBack, check, arg) {
    if (!arg)
        arg = {};
    var me = this;
    if (!me.getMulitArgs(check, arg))
        return;
    call_remote(title, api, arg, function (res) {
        if (callBack) {
            callBack(res);
        }
        for (var idx = 0; idx < arg.length; idx++)
            arg.selects[idx].IsSelected = false;
        me.reload();
    }, confirmMessage);
};
/**
 * 执行多选远程操作扩展1
 * @public 
 * @param {string} title 操作标题
 * @param {string} url 远程URL
 * @param {object} arg 其它参数
 * @param {function} check 操作前置条件检查（参数为当前选择对象）
 * @param {string} confirmMessage 确认操作的消息
 * @param {function} callBack 执行完成后的回调方法
 * @returns {void} 
 */
GridPanel.prototype.doRemoteEx = function (title, url, arg, check, confirmMessage, callBack) {
    if (!arg)
        arg = {};
    var me = this;
    if (!me.getMulitArgs(check, arg))
        return;
    call_remote(title, url, action, arg, function (res) {
        if (callBack) {
            callBack(res);
        }
        me.reload();
    }, confirmMessage);
};
GridPanel.prototype.setValidateMessage = function (result, title) {
    //var me = this;
    //try {
    //    var datas = me.execGridMethod("getData");
    //    if (!me.isTree)
    //        datas = datas.rows;
    //    resetMessage(datas);
    //    var msgs = result.value;
    //    if (msgs == null || msgs.length == 0) {
    //        me.reload();
    //    } else {
    //        for (var i = 0; i < msgs.length; i++) {
    //            var row = findGridData(msgs[i].id, datas, me.idField);
    //            if (row)
    //                row.msg = msgs[i].msg;
    //        }
    //        me.execGridMethod("loadData", datas);
    //        me.execGridMethod("showColumn", "msg");
    //    }
    //} catch (e) {
    //    $.messager.alert(title, result.message2);
    //}
    if (result.message2 != null)
        $.messager.alert(title, result.message2);
};

/*
 * 初始化表格的按钮
 */
GridPanel.prototype.createToolbarButton = function (eid, title, action, icon, condition, isSelectEvent, eventFunc, noPowerFunc, reg) {
    var me = this;
    try {
        if (!reg)
            reg = eid;
        if (!document.getElementById(reg + me.elementEx))
            return;
        reg = "#" + reg + me.elementEx;
        if (!condition) {
            $(reg).hide();
            return;
        }
        eid = "#" + eid + me.elementEx;
        if (isSelectEvent)
            this.bySelectButtons.push(eid);
        if (!createButtonByAction(title, action, reg, eid, icon, eventFunc)) {
            if (noPowerFunc)
                noPowerFunc();
            $(eid).hide();
        }
    } catch (e) {
        console.error("%s.%s() ： %s", typeof (this), "createToolbarButton", e);
        $.messager.alert("发生错误:GridPanel.prototype.createToolbarButton:" + title, e);
    }
};
/**
 * 初始化表格的标准工具栏
 */
GridPanel.prototype.initToolbar = function() {
    var me = this;
    if ($("#regQuery").length > 0) {
        if ($('#btnExport').length == 0)
            $("#regQuery").append("<a id='btnExport' href='Export.aspx'>导出</a>");
        me.createToolbarButton("btnExport", "导出", "export", "icon-excel", true);
    }


    me.createToolbarButton("btnQuery",
        "查询",
        "list",
        "icon-search",
        true,
        false,
        function() {
            me.doQuery();
        },
        null,
        "regQuery");

    me.createToolbarButton("btnAdd",
        "新增",
        "addnew",
        "icon-add",
        me.addNew,
        false,
        function() {
            me.processIndex = 0;
            me.execGridMethod("uncheckAll");
            me.doAddNew();
        },
        function() {
            me.addNew = null;
        });

    me.createToolbarButton("btnEdit",
        "查看",
        "details",
        "icon-edit",
        me.edit,
        true,
        function() {
            me.editRow();
        },
        function() {
            me.edit = null;
        });
    me.createToolbarButton("btnDelete",
        "删除",
        "delete",
        "icon-cancel",
        (me.editMask & 0x8) === 0x8,
        true,
        function() {
            me.doRemove(me);
        });

    me.createToolbarButton("btnEnable",
        "启用",
        "disable",
        "icon-enable",
        me.stateData,
        true,
        function() {
            me.doEnable();
        });
    me.createToolbarButton("btnDisable",
        "禁用",
        "enable",
        "icon-disable",
        me.stateData,
        true,
        function() {
            me.doDisable();
        });
    me.createToolbarButton("btnLock",
        "锁定",
        "lock",
        "icon-lock",
        me.stateData,
        true,
        function() {
            me.doLock();
        });
    me.createToolbarButton("btnDiscard",
        "废弃",
        "discard",
        "icon-discard",
        me.stateData,
        true,
        function() {
            me.doDiscard();
        });
    me.createToolbarButton("btnReset",
        "重置",
        "reset",
        "icon-reset",
        me.stateData,
        true,
        function() {
            me.doReset();
        });

    me.createToolbarButton("btnValidate",
        "数据校验",
        "validate",
        "icon-validate",
        me.auditData,
        true,
        function() {
            me.doValidate();
        });
    me.createToolbarButton("btnAuditSubmit",
        "提交审核",
        "submit",
        "icon_a_submit",
        me.auditData,
        true,
        function() {
            me.doSubmit();
        });
    me.createToolbarButton("btnPullback",
        "拉回",
        "pullback",
        "icon_a_pullback",
        me.auditData,
        true,
        function() {
            me.doPullback();
        });
    me.createToolbarButton("btnAuditBack",
        "退回",
        "back",
        "icon_a_back",
        me.auditData,
        true,
        function() {
            me.doBack();
        });
    me.createToolbarButton("btnAuditPass",
        "通过",
        "pass",
        "icon_a_pass",
        me.auditData,
        true,
        function() {
            me.doPass();
        });
    me.createToolbarButton("btnAuditDeny",
        "否决",
        "deny",
        "icon_a_deny",
        me.auditData,
        true,
        function() {
            me.doDeny();
        });
    me.createToolbarButton("btnReAudit",
        "重做",
        "reaudit",
        "icon_a_again",
        me.auditData,
        true,
        function() {
            me.doReAudit();
        });
};


/*
取得当前表格选择的所有ID
*/
function getGridSelects(grid, idProperty, selProperty, check) {
    if (!idProperty)
        idProperty = "Id";
    if (!selProperty)
        selProperty = "IsSelected";
    var rows = $(grid).datagrid("getChecked");
    if (check)
        return getAndCheckSelects(rows, idProperty, selProperty, check);
    return getSelects(rows, idProperty, selProperty);
}

/**
 * 取得树表格的选择数据
 * @param {object} grid  grid
 * @param {object} idProperty idProperty
 * @param {object} selProperty selProperty
 * @param {object} check selProperty
 * @returns {object} d
 */
function getTreeSelects(grid, idProperty, selProperty, check) {
    if (!idProperty)
        idProperty = "Id";
    if (!selProperty)
        selProperty = "IsSelected";
    var rows = $(grid).treegrid("getChecked");
    if (rows.rows)
        rows = rows.rows;
    return getAndCheckSelects(rows, idProperty, selProperty, check);
}
/*
取得当前表格数据选择的所有ID
*/
function getAndCheckSelects(rows, idProperty, selProperty, check) {
    if (!rows)
        return "";
    var sel = "";
    var arr2 = new Array();
    for (var idx = 0; idx < rows.length; idx++)
        arr2.push(rows[idx]);
    for (var i = 0; i < arr2.length; i++) {
        var chd = arr2[i];
        if (chd.children && chd.children.length > 0)
            sel += getAndCheckSelects(chd.children, idProperty, selProperty, check);
        if (chd[selProperty] && (!check || check(chd)))
            sel += chd[idProperty] + ",";
    }
    return sel;
}

/*
取得当前表格数据选择的所有ID
*/
function getSelects(rows, idProperty, selProperty) {
    if (!rows)
        return "";
    var sel = "";
    var arr2 = new Array();
    for (var idx = 0; idx < rows.length; idx++)
        arr2.push(rows[idx]);
    for (var i = 0; i < arr2.length; i++) {
        var chd = arr2[i];
        if (chd[selProperty])
            sel += chd[idProperty] + ",";
        if (chd.children && chd.children.length > 0)
            sel += getSelects(chd.children, idProperty, selProperty);
    }
    return sel;
}

/*
取得当前表格数据选择的所有ID
*/
function haseSelected(rows, idProperty, selProperty) {
    if (!rows)
        return false;
    var arr2 = new Array();
    for (var idx = 0; idx < rows.length; idx++) {
        arr2.push(rows[idx]);
    }
    for (var i = 0; i < arr2.length; i++) {
        var chd = arr2[i];
        if (chd[selProperty])
            return true;
        if (haseSelected(chd.children, idProperty, selProperty))
            return true;
    }
    return false;
}

/*
找出ID为指定值的对象
*/
function findGridData(id, rows, idProperty) {
    if (!rows)
        return null;
    if (!idProperty)
        idProperty = "Id";
    if (rows.length <= 0)
        return null;
    for (var i = 0; i < rows.length; i++) {
        var chd = rows[i];
        if (chd[idProperty] == id)
            return chd;
        if (chd.children && chd.children.length > 0) {
            var re = findGridData(id, chd.children, idProperty);
            if (re)
                return re;
        }
    }
    return null;
}


/**
 * 取得树表格的选择路径
 * @param {object} node 树节点
 * @returns {string} 树表格的选择路径
 */
function GetTreePath(node) {
    if (!node)
        return "";
    var pn = $("#tree").tree("getParent", node.target);
    if (!pn)
        return node.text;
    return GetTreePath(pn) + " > " + node.text;
}
/**
 * 遍历当前树表格的数据
 * @param {string} grid 表格的Element ID
 * @param {function} func 处理方法
 * @returns {string} 是否有执行过
 */
function foreachTree(grid, func) {
    var rows = $(grid).treegrid("getRoots");
    return foreachTreeDatas(rows, func);
}

/**
 * 遍历当前树数据
 * @param {array} rows 树数据
 * @param {function} func 处理方法
 * @returns {string}  是否有执行过
 */
function foreachTreeDatas(rows, func) {
    if (!rows)
        return false;
    for (var i = 0; i < rows.length; i++) {
        var chd = rows[i];
        if (func(chd) && chd.children && chd.children.length > 0)
            foreachTreeDatas(chd.children, func);
    }
    return true;
}

/**
 * 遍历当前树表格的数据
 * @param {string} grid 表格的Element ID
 * @param {function} func 处理方法
 * @returns {string} 是否有执行过
 */
function foreachGridRows(grid, func) {
    var rows = $(grid).datagrid("getRows");
    return foreachRows(rows, func);
}

/**
 * 遍历当前数据
 * @param {array} rows 树数据
 * @param {function} func 处理方法
 * @returns {string} 是否有执行过
 */
function foreachRows(rows, func) {
    if (!rows)
        return false;
    for (var i = 0; i < rows.length; i++) {
        func(rows[i]);
    }
    return true;
}


/*
遍历数据
*/
function foreachDatas(rows, func) {
    if (!rows || rows.length === 0)
        return;
    for (var idx = 0; idx < rows.length; idx++) {
        var chd = rows[idx];
        try {
            func(chd);
        } catch (e) {
            console.error(e);
        }
        if (!chd.children || chd.length === 0)
            continue;
        foreachDatas(chd.children, func);
    }
}