
/*
动态加载UI
*/
var currentPage;

var mainPageOptions = {
    /**
     * 扩展
     * @param {object} options 扩展
     */
    extend: function (options) {
        this.isNewVersion = true;
        for (var property in options) {
            if (options.hasOwnProperty(property)) {
                this[property] = options[property];
            }
        }
    },
    /**
     * 版本兼容标记
     */
    isNewVersion: false,
    /*页面基本对象*/
    /*
        是否初始化完成
    */
    isInitialized: false,
    /**
     * 是否显示所有按钮
     */
    allButton: false,
    /**
     * 当前页面标识
     */
    currentPageId: 0,
    /**
     * 当前页面用户可访问的按钮
     */
    userButtons: [],
    /**
     * 当前页面当前用户的上一次查询条件
     */
    preQueryArgs: null,
    /**
     * 主数据主键
     */
    IdField: null,

    /**
     * 初始化完成
     */
    onInitialized: null,
    /**
     * 初始化
     */
    onReady: function () {
        //版本兼容
        var me = this;
        if (window.doPageInitialize)
            me.doPageInitialize = window.doPageInitialize;
        if (!me.onCheckSize)
            me.onCheckSize = window.onCheckSize;

        this.initWorkSpace();
        //回车
        //if (document.addEventListener) {
        //    document.addEventListener("keydown", fireFoxHandler, true);
        //} else {
        //    document.attachEvent("onkeydown", fireFoxHandler);
        //}
        if (me.doPageInitialize) {
            me.doPageInitialize(me.onPageInitialized);
        }
        else {
            me.onPageInitialized();
        }
    },
    initWorkSpace: function () {
        var me = this;
        me.wWidth = $(window).width();
        me.wHeight = $(window).height();
        $("#rBody").width(me.wWidth);
        $("#rBody").height(me.wHeight);
    },
    onPageInitialized: function () {
        $("#rBody").css("visibility", "visible");
        $("#__loading__").css("opacity", 0.5);
        var me = mainPageOptions;
        if (me.onCheckSize)
            me.onCheckSize(me.wWidth, me.wHeight);
        hideBusy();
        me.checkQueryArgs();
        if (!me.userButtons)
            me.userButtons = [];
        me.isInitialized = true;
        if (me.onInitialized)
            me.onInitialized();
        $(window).resize(function () {
            me.checkSize();
        });
    },
    checkQueryArgs: function () {
        var me = this;
        if (me.preQueryArgs) {
            if (!me.preQueryArgs.page)
                me.preQueryArgs.page = 0;
            if (!me.preQueryArgs.size)
                me.preQueryArgs.size = 20;
            if (!me.preQueryArgs.sort)
                me.preQueryArgs.sort = me.IdField || "Id";
            if (!me.preQueryArgs.order)
                me.preQueryArgs.order = "desc";
            if (!me.preQueryArgs.audit)
                me.preQueryArgs.audit = 0x100;
        } else {
            me.preQueryArgs = { page: 0, size: 20, sort: me.IdField || "Id", order: "desc", audit: 0x100 }
        }
        window.preQueryArgs = me.preQueryArgs;
    },

    /**
     * 页面设置查询条件历史的调用方法
     * @param {object} vl 查询历史数据
     */
    setPreQueryArgs: function (vl) {
        var me = this;
        me.preQueryArgs = vl;
        me.checkQueryArgs();
    },
    /**
     * 当前窗口长宽
     */
    wWidth: 0,
    /**
     * 当前窗口长宽
     */
    wHeight: 0,
    /**
     * 窗口大小变化时调整对应视图大小的处理
     */
    checkSize: function () {
        var me = this;
        me.wWidth = $(window).width();
        me.wHeight = $(window).height();
        $("#rBody").width(me.wWidth);
        $("#rBody").height(me.wHeight);
        if (me.onCheckSize)
            me.onCheckSize(me.wWidth, me.wHeight);
    },
    /**
     * 
     * @param {object} id 当前校验的数据ID
     * @param {Function} callback 校验成功后的回调
     */
    doValidate: function (id, callback) {
        doSilentOperator2("数据校验", this.validatePath + "validate", { selects: id }, function (result) {
            var msg = "";
            if (result.value && result.value.length > 0)
                msg = result.value[0].msg;
            if (!msg)
                msg = "数据合格,可以提交审核";
            try {
                callback(msg);
            } catch (e) {
                console.error("%s.%s() ： %s", typeof (this), "doValidate", e);
            }
        });
    },

    /**
     * 检查用户的按钮权限
     * @param {string} title 按钮标题
     * @param {string} element 按钮ID的JQuery表示方式(如'#btnAdd')
     * @param {string} action 行为
     * @param {string} memo 说明
     * @returns {boolean} 如果有权访问,返回true
     */
    checkRole: function (title, element, action, memo) {
        return this.canDo(element, action, title, memo);
    },

    /**
     * 检查用户的按钮权限
     * @param {string} element 按钮ID的JQuery表示方式(如'#btnAdd')
     * @param {string} action 行为
     * @param {string} title 按钮标题
     * @param {string} memo 说明
     * @returns {boolean} 如果有权访问,返回true
     */
    canDo: function (element, action, title, memo) {
        if (action === 'list')
            return true;//列表权限总是有的

        this.saveRole(title, element, action, memo);
        if (this.allButton)
            return true;
        if (!this.userButtons)
            return false;
        if (this.userButtons.indexOf(element) >= 0)
            return true;
        if (element === '#btnAuditSubmit')
            return this.userButtons.indexOf('#btnSubmit') >= 0;
        return false;
    },

    /**
     * 检查用户的按钮权限
     * @param {string} title 按钮标题
     * @param {string} element 按钮ID的JQuery表示方式(如'#btnAdd')
     * @param {string} action 行为
     * @param {string} memo 说明
     */
    saveRole: function (title, element, action, memo) {
        /*console.log(title + "*" + element + "*" + action + "*" + this.currentPageId);
        doSilentOperator('回写', "app/page/v1/help/button", {
            action: 'button',
            title: title,
            pid: this.currentPageId,
            element: element,
            name: action,
            memo: memo
        });*/
    },
    loadPageInfo: function (pageName, callback) {
        ajaxLoadValue('载入', 'app/page/v1/global/info', { value: pageName }, function (data) {
            mainPageOptions.currentPageId = data.pageId;
            mainPageOptions.userButtons = data.buttons;
            mainPageOptions.allButton = data.allButton;
            mainPageOptions.setPreQueryArgs(data.preQueryArgs);
            callback();
        }, globalOptions.api.appApiHost);
    },
    /**
     * 窗口大小变化的页面处理方法(注入)
     * @param {Function} callback 回调
     */
    doPageInitialize: null,
    /**
     * 当前页面的初始化方法(注入)
     * @param {number} wid 宽
     * @param {number} hei 高
     */
    onCheckSize: function (wid, hei) {
        $('#layout').layout('resize', window.o99);
        $('#grid').datagrid('resize', window.o99);
    }
};



function goPage(url) {
    location.href = url;
}


/**
 * 
 * @param {int} id 当前校验的数据ID
 * @param {Function} callback 校验成功后的回调
 */
function doValidate(id, callback) {
    mainPageOptions.doValidate(id, callback);
}

/**
 * 页面设置查询条件历史的调用方法
 * @param {object} vl 查询历史数据
 */
function setPreQueryArgs(vl) {
    //版本兼容
    mainPageOptions.allButton = window.allButton || true;
    mainPageOptions.currentPageId = window.currentPageId;
    mainPageOptions.userButtons = window.userButtons || [];
    mainPageOptions.setPreQueryArgs(vl);
}

function setSubPageNme(name1, name2) {
    var tp = $(window.parent.document.getElementById("txtPage"));
    var title = tp.attr("title");
    if (title.indexOf(name1) < 0)
        title += " > " + name1;
    if (name2)
        title += " > " + name2;
    tp.html(title);
}


/**
 * 页面初始化操作
 */
$(document).ready(function () {
    mainPageOptions.onReady();
});