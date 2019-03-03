/**
 *  基本的编辑的界面操作基类
 * @param {object} options 自定义配置
*/
function EditDialog(options) {

    this.options = options || {
        /**
         * 主键字段
         */
        idField: "Id",
        /**
         * 标题
         */
        title: "",
        /**
         * 命令执行地址前缀
         */
        apiPrefix: "",
        /**
         * 表单地址
         */
        formUrl: "form.htm",
        /**
         * 表单参数
         */
        formArg: "_-_=1"
    };
}

/**
 * 生成编辑器
 * @param {string} action 当前页面对象
 * @param {number} id 数据标识
 * @param {object} options 自定义配置
 * @returns {CardEditor} 编辑器
 */
EditDialog.prototype.createEditor = function (action, id, options) {
    var me = this;
    if (!options)
        options = {};
    $.extend(options, me.option);
    var editor = new CardEditor(options);
    //是否为新增
    editor.isAddNew = action === "addnew";
    editor.ex = me;
    editor.dataId = id;
    editor.title = editor.options.title;
    editor.showValidate = true;
    editor.validatePath = editor.options.apiPrefix + "audit/validate";
    editor.autoNew = editor.options.autoNew;

    if (editor.options.uiUrl) {
        editor.uiUrl = editor.options.uiUrl;
    } else if (editor.options.formArg) {
        editor.uiUrl = editor.options.formUrl + "?" + editor.options.formArg + "&id=" + id;
    } else {
        editor.uiUrl = editor.options.formUrl + "?id=" + id;
    }
    if (editor.options.dataUrl) {
        editor.dataUrl = editor.options.dataUrl;
    } else if (editor.options.formArg) {
        editor.dataUrl = editor.options.apiPrefix + "edit/details?" + editor.options.formArg + "&id=";
    } else {
        editor.dataUrl = editor.options.apiPrefix + "edit/details?id=";
    }
    if (editor.options.saveUrl) {
        editor.saveUrl = editor.options.saveUrl;
    } else if (editor.options.formArg) {
        editor.saveUrl = editor.options.apiPrefix + "edit/" + action + "?" + editor.options.formArg + "&id=";
    } else {
        editor.saveUrl = editor.options.apiPrefix + "edit/" + action + "?id=";
    }
    return editor;
};
/*
    新增界面操作
*/
EditDialog.prototype.showAddNew = function (options) {
    var me = this;
    var editor = me.createEditor("addnew", 0, options);
    editor.dataId = 0;
    editor.show();
};
/*
    修改或查看界面
*/
EditDialog.prototype.showEdit = function (id, options) {
    var me = this;
    var editor = me.createEditor("update", id, options);
    editor.dataId = id;
    editor.show();
};

/**
 *  基本的编辑的界面操作基类
 * @param {object} options 配置
*/
function CardEditor(options) {
    this.dlgParId = null;
    this.dlgDomId = null;
    this.dlgBodyDomId = null;
    this.btnSaveDomId = null;
    this.dlgMsgDomId = null;
    /*远程地址*/
    this.saveUrl = null;
    this.dataUrl = null;
    this.uiUrl = null;
    //标题
    this.title = null;
    //是否为新增
    this.isAddNew = false;
    //数据的标识符
    this.dataId = null;
    /*操作数据*/
    this.data = null;
    //数据是否只读
    this.isPanel = false;
    //数据是否只读
    this.readOnly = false;
    //持续新增
    this.autoNew = false;

    this.closeText = "关闭";
    this.saveText = "保存";
    this.dataLoaded = false;

    this.form = null;

    this.showValidate = false;
    this.validatePath = "Action.aspx";
    /**
     * 当前录入是否校验正确
     */
    this.inputSucceed = true;

    var me = this;

    me.options = options || {
        /**
         * 主键字段
         */
        idField: "Id",
        /**
         * 标题
         */
        title: "",
        /**
         * 命令执行地址前缀
         */
        apiPrefix: "",
        /**
         * 表单地址
         */
        formUrl: "form.htm",
        /**
         * 表单参数
         */
        formArg: "_-_=1"
    };

    me.setFormValidate = function () {
        if (me.options.setFormValidate)
            me.options.setFormValidate();
    };
    me.onUiLoaded = function (data, ed) {
        if (me.options.onUiLoaded)
            me.options.onUiLoaded(data, ed);
    };
    me.onDataLoaded = function (data, ed) {
        if (me.options.onDataLoaded)
            me.options.onDataLoaded(data, ed);
    };
    me.afterDataLoaded = function (data, ed) {
        if (me.options.afterDataLoaded)
            me.options.afterDataLoaded(data, ed);
        me.inputSucceed = true;
    };
    me.beforeSave = function () {
        if (me.options.beforeSave)
            return me.options.beforeSave();
        return true;
    };
    me.afterSave = function (succeed, data) {
        if (me.options.afterSave)
            me.options.afterSave(succeed, data);
    };
    me.onDestroy = function () {
        if (me.options.onDestroy)
            me.options.onDestroy();
    };
}

CardEditor.prototype.checkReadOnly = function () {
    return false;
};
/*功能实现*/
CardEditor.prototype.show = function () {
    var me = this;

    var panel = document.getElementById("xx_dialog_region_xx");
    if (panel == null) {
        $(body).append("<div id='xx_dialog_region_xx' style='visibility:collapse;display:none'></div>");
    }

    var timestamp = new Date().getTime();

    this.dlgParId = "xx_panel_" + timestamp;

    $("#xx_dialog_region_xx").append("<div id=" + this.dlgParId + ' style="overflow: auto;"></div>');

    var pnid = "xx_dlg_" + timestamp;
    $("#xx_dialog_region_xx").append(
        "<div id=" + pnid + ' style="overflow: auto;"><div>' +
        '<fieldset id="' + pnid + '_msgp" style="display: none;"><legend>数据校验</legend>' +
        '<div id="' + pnid + '_msg" class="inputField"></div></fieldset>' +
        '<div id="' + pnid + '_body" style="overflow: visible;">' +
        "</div></div></div>");
    this.dlgDomId = "#" + pnid;
    this.btnSaveDomId = pnid + "_save";
    this.dlgBodyDomId = "#" + pnid + "_body";
    this.dlgMsgDomId = "#" + pnid + "_msg";
    if (this.dataId == 0)
        me.isAddNew = true;
    $(this.dlgBodyDomId).panel({
        width: "auto",
        height: "auto",
        border: false,
        cache: false,
        loadingMessage: "正在载入……",
        href: this.uiUrl,
        onLoad: function () {
            me.onShow();
        }
    });
};
/*功能辅助*/
CardEditor.prototype.createEditButton = function () {
    if (this.readOnly)
        return [];
    var me = this;
    me.closebtn = {
        text: this.closeText,
        plain: "true",
        iconCls: "icon-cancel",
        handler: function () {
            $(me.dlgDomId).dialog("close");
        }
    };
    var buttons = [];
    if (mainPageOptions.canDo(me.isAddNew ? "#btnAdd" : "#btnSave", me.isAddNew ? "addnew" : "update", me.isAddNew ? "写入" : "更新")) {
        me.saveButton = {
            id: this.btnSaveDomId,
            text: me.saveText,
            plain: "true",
            iconCls: "icon-save",
            handler: function () {
                setTimeout(function () {
                    me.save();
                }, 100);
            }
        };
        buttons.push(me.saveButton);
    }
    if (me.showValidate) {
        me.validateButton = {
            id: this.btnSaveDomId + "_v",
            text: "校验数据",
            plain: "true",
            iconCls: "icon-validate",
            handler: function () {
                me.doValidate(me.dataId);
            }
        };
        buttons.push(me.validateButton);
    }
    return buttons;
};
CardEditor.prototype.onShow = function () {
    var me = this;
    var dlg_body = $(me.dlgBodyDomId);
    me.form = dlg_body.children("form");
    var form_body = me.form.children("div");

    me.isPanel = form_body.attr("isPanel") === "true";

    var left = 0, top = 0;
    if (!this.isPanel) {
        //计算使对话框居中
        var doc_body = $(document);
        var doc_wid = doc_body.width(),
            doc_hei = doc_body.height(),
            dlg_wid = dlg_body.width(),
            dlg_hei = dlg_body.height();
        left = (doc_wid - dlg_wid) / 2 - 12;
        top = (doc_hei - dlg_hei) / 2 - 40;

        //检查宽度是否比整个页面还宽,如果是,则强制最大化
        if (left <= 0 || top <= 0) {
            left = 0;
            top = 0;
            this.isPanel = true;
        }
    }
    if (this.isPanel) {
        dlg_body.css("width", null);
        dlg_body.css("height", null);
        form_body.css("width", "100%");
        form_body.css("height", null);
    } else {
        dlg_body.css("width", form_body.css("width"));
        dlg_body.css("height", form_body.css("height"));
    }
    me.form.form("clear");
    me.create(top, left);
};
CardEditor.prototype.create = function (top, left) {
    showBusy();
    window.dialog_input = this;
    var dlg = $(this.dlgDomId);


    var buttons = this.createEditButton();
    var me = this;
    dlg.dialog({
        top: top,
        left: left,
        modal: false,
        closed: true,
        closable: true,
        buttons: buttons,
        title: this.title || "Edit",
        fit: this.isPanel,
        resizable: !this.isPanel,
        onBeforeClose: function () {
            me.destroy();
        }
    });
    dlg.dialog("open");
    dlg.dialog("center");
    initUeditor(this.dlgBodyDomId);
    if (this.onUiLoaded)
        this.onUiLoaded(this);

    this.reLoadData();
};
CardEditor.prototype.reLoadData = function () {
    showBusy();
    this.dataLoaded = false;
    var me = this;

    if (this.isAddNew || this.dataId == null)
        this.dataId = 0;
    if (this.dataUrl) {
        var url = this.dataUrl + this.dataId;
        if (this.formArg)
            url += '&' + this.formArg;
        ajaxOperator("编辑",
            url,
            null,
            function (result) {
                me.onLoaded(result);
            });
    } else if (this.dataId === 0 || this.data != null) {
        if (!this.data)
            this.data = { Id: 0 };
        this.showData(this.data);
    }
};
CardEditor.prototype.onLoaded = function (result) {
    var me = this;
    if (result.status && result.status.code === 999) {
        showMessage(me.dataId ? "编辑" : "新增", result.status.msg);
        $(me.dlgDomId).dialog("close");
        console.error(result.status.msg);
        hideBusy();
        return;
    }
    if (result.success) {
        if (result.data)
            me.showData(result.data);
    } else {
        showMessage("数据载入", result.status.msg);
        console.error(result.status.msg);
    }
};
CardEditor.prototype.showData = function (d) {
    var me = this;
    me.data = d;
    if (!me.readOnly) {
        me.readOnly = !d || d.AuditState && d.AuditState >= 2 || d.IsFreeze || d.dataState && d.dataState !== 0;
        if (!me.readOnly && me.checkReadOnly) {
            me.readOnly = me.checkReadOnly(me.isAddNew);
        }
    }
    me.syncReadOnly();
    syncFormInput(me.readOnly, me.dlgBodyDomId);
    if (me.onDataLoaded)
        me.onDataLoaded(me.data, me);

    me.form.form("load", me.data);
    setUeditorData(d, me.dlgBodyDomId, me.readOnly);

    me.dataLoaded = true;

    if (me.afterDataLoaded)
        me.afterDataLoaded(me.data, me);

    if (!me.readOnly) {
        $(me.dlgDomId).on("keypress",
            function (event) {
                if ((event.which == 96) && event.ctrlKey)
                    me.save();
            });
    } else {
        setComboReadValue(me.dlgBodyDomId, me.data);
    }
    hideBusy();
    return true;
};
CardEditor.prototype.syncReadOnly = function () {
    var readonly = this.readOnly;
    $(this.dlgDomId).dialog("setTitle", this.title + (this.isAddNew ? "（新增）" : readonly ? "（查看）" : "（编辑）"));

    if (readonly) {
        $("#" + this.btnSaveDomId).hide();
    } else {
        $("#" + this.btnSaveDomId).show();
    }

    if (this.isAddNew || readonly)
        $("#" + this.btnSaveDomId + "_v").hide();
    else
        $("#" + this.btnSaveDomId + "_v").show();
};
CardEditor.prototype.save = function () {
    if (!this.form.form("enableValidation").form("validate")) {
        return;
    }
    if (!this.beforeSave())
        return;
    var me = this;
    ajax_post("保存", this.saveUrl + this.dataId, getFormJson("#" + this.form.attr('id')), function (r) {
        me.onSaved(r);
    });
    //this.form.form('submit', {
    //    url: globalOptions.geturl(me.saveUrl + me.dataId),
    //    onSubmit: function(par) {
    //        return false;
    //    },
    //    success: function (r) {
    //        me.onSaved(r);
    //    }
    //});
};
CardEditor.prototype.onSaved = function (r) {
    var result = ajaxComplete("保存", r);
    if (result && this.afterSave) {
        this.afterSave(result.success, result.data);
    }
    if (result && result.success) {
        if (this.isAddNew && this.autoNew) {
            this.reset();
        }
        else {
            $(this.dlgDomId).dialog("close");
        }
    }
};
CardEditor.prototype.reset = function () {
    this.form.form("reset");
    resetUeditorData(this.dlgDomId);
    this.reLoadData();
};
CardEditor.prototype.doValidate = function (id) {
    var me = this;
    if (me.setFormValidate) {
        me.setFormValidate();
    }
    if (!this.form.form("enableValidation").form("validate")) {
        return;
    }
    doSilentOperator2("数据校验", this.validatePath + "validate", { selects: id }, function (result) {
        if (result.succeed) {
            return;
        }
        try {
            $(me.dlgMsgDomId).html(result.message2);
            syncFormValidate("#content", result.value, me.form);
        } catch (e) {
            console.error("%s.%s() ： %s", typeof (this), "doValidate", e);
        }
    });
};
CardEditor.prototype.destroy = function () {
    if (!this.readOnly)
        destroyUeditor(this.dlgBodyDomId);
    destroyFormInput(this.dlgBodyDomId);
    if (this.onDestroy)
        this.onDestroy();
    $(this.dlgDomId + ".tooltip").remove();
    $(this.dlgDomId + ".validatebox-invalid").removeClass("validatebox-invalid");
    $(this.dlgDomId).remove();
    $(this.dlgDomId).dialog("destroy");
    $("#" + this.dlgParId).remove();
};

var noCheckSucceed = true;

//function noValidate(value) {
//    $(".tooltip").remove();
//    $(".validatebox-invalid").removeClass("validatebox-invalid");
//    return noCheckSucceed ? null : "编号不唯一";
//}

function initUeditor(par) {
    var ueditors = $(par).find(".myueditor");
    ueditors.each(function () {
        try {
            var name = $(this).attr("id");
            if (name)
                UE.getEditor(name);
        } catch (e) {
            alert(e);
        }
    });
}

function clearImage(field, img) {
    $("#" + field).val("");
    $("#" + img).attr("src", "/images/expert.png");
}
function selectImage(field, img) {
    showImageUpload(function (url) {
        $("#" + field).val(url);
        $("#" + img).attr("src", url);
    });
}


function setUeditorData(data, par, readonly) {
    var ueditors = $(par).find(".myueditor");
    ueditors.each(function () {
        var name = $(this).attr("id");
        if (!name) {
            console.error("%s.setUeditorData() ： %s", typeof (this), "Editor element must a id ");
            return;
        }
        var val = data[name];
        if (!val)
            val = "";
        if (readonly) {
            $("#" + name).css("border", "1px solid silver");
            $("#" + name).css("overflow-x", "auto");
            $("#" + name).css("overflow-y", "visible");
            $("#" + name).css("display", "inline-block");
            $("#" + name).html(val);
            return;
        }
        var ue = UE.getEditor(name);
        try {
            if (ue.isReady === 1)
                ue.setContent(val);
            else
                ue.addListener("ready", function () {
                    ue.setContent(val);
                });
        } catch (e) {
            console.error("setUeditorData:%s %s", name, e);
            console.trace();
        }
    });
}
function resetUeditorData(par) {
    var ueditors = $(par).find(".myueditor");
    ueditors.each(function () {
        var name = $(this).attr("id");
        if (!name) {
            console.error("%s.resetUeditorData() ： %s", typeof (this), "Editor element must a id ");
            return;
        }
        try {
            console.log("%s.resetUeditorData() ：Ueditor %s", typeof (this), name);
            UE.getEditor(name).setContent("");
        } catch (e) {
            console.error("%s.resetUeditorData() ： %s", typeof (this), e);
        }
    });
}
function destroyUeditor(par, readonly) {
    if (readonly)
        return;
    var ueditors = $(par).find(".myueditor");
    ueditors.each(function () {
        try {
            var name = $(this).attr("id");
            if (name)
                UE.getEditor(name).destroy();
        } catch (e) {
            console.error("%s.%s() ： %s", typeof (this), "syncFormValidate", e);
        }
    });
}
function syncFormInput(readonly, par) {
    var inputs = $(par).find(".inputRegion");
    inputs.each(function () {
        var reg = $(this);
        var name;
        if (readonly) {
            reg.find(".easyui-textbox").each(function () {
                name = $(this).attr("textboxname");
                $("#" + name).textbox("readonly", readonly);
            });
            reg.find(".easyui-datebox").each(function () {
                name = $(this).attr("textboxname");
                $("#" + name).datebox({ editable: !readonly, hasDownArrow: !readonly, readonly: readonly });
            });
            reg.find(".easyui-combobox").each(function () {
                name = $(this).attr("textboxname");
                $("#" + name).combobox({ editable: !readonly, hasDownArrow: !readonly, readonly: readonly });
            });
            reg.find(".easyui-combotree").each(function () {
                name = $(this).attr("textboxname");
                $("#" + name).combotree({ editable: !readonly, hasDownArrow: !readonly, readonly: readonly });
            });
            reg.find("textarea").each(function () {
                if (readonly) {
                    $(this).attr("readonly", "readonly");
                    $(this).attr("disabled", "disabled");
                } else {
                    $(this).removeAttr("readonly");
                    $(this).removeAttr("disabled");
                }
            });
        }
    });
}

function syncFormValidate(par, result, form) {
    if (!result.value ||
        !result.value.results ||
        result.value.results.length === 0)
        return;
    var items = result.value.results[0].items;
    for (var i = 0; i < items.length; i++) {
        try {
            var item = items[i];
            var name = item.field;
            var attr = $("#" + item.field).attr("class");
            if (attr.indexOf("easyui-datebox") >= 0) {
                $("#" + name).datebox({ required: true, validType: 'setMessageValidate["' + item.message + '"]' });
            }
            else if (attr.indexOf("easyui-combotree") >= 0) {

                $("#" + name).combotree({ validType: 'setMessageValidate["' + item.message + '"]' });
            }
            else if (attr.indexOf("easyui-combobox") >= 0) {

                $("#" + name).combobox({ validType: 'setMessageValidate["' + item.message + '"]' });
            }
            else if (attr.indexOf("easyui-textbox") >= 0) {
                $("#" + name).textbox({ validType: 'setMessageValidate["' + item.message + '"]' });
            }
        } catch (e) {
            console.error("%s.%s() ： %s", typeof (this), "syncFormValidate", e);
        }
    }
    form.form("enableValidation").form("validate");
}

function setComboReadValue(par, data) {
    var combos = $(par).find("[readfield]");
    combos.each(function () {
        var combo = $(this);
        var att = combo.attr("readfield");
        if (att == "" || !att)
            return;
        var name = combo.attr("id");
        var txt = "";
        var fls = att.split(" ");
        for (var i = 0; i < fls.length; i++) {
            var f = fls[i];
            if (f == "")
                continue;
            var vl = data[f];
            if (!vl)
                continue;
            txt += vl;
        }
        $("#" + name).combobox("setText", txt);
    });
}
function destroyFormInput(par) {
    var inputs = $(par).find(".inputRegion");
    inputs.each(function () {
        var reg = $(this);
        var chd = reg.find(".easyui-textbox");
        var name;
        if (chd.length > 0) {
            chd = $(chd);
            name = chd.attr("textboxname");
            $("#" + name).textbox("destroy");
        }
        chd = reg.find(".easyui-datebox");
        if (chd.length > 0) {
            chd = $(chd);
            name = chd.attr("textboxname");
            $("#" + name).datebox("destroy");
        }
        chd = reg.find(".easyui-combobox");
        if (chd.length > 0) {
            chd = $(chd);
            name = chd.attr("textboxname");
            $("#" + name).combobox("destroy");
        }
    });
}


/*
    easyui校验扩展
*/

$.extend($.fn.validatebox.defaults.rules, {
    selectNoZero: {
        validator: function (value, param) {
            //if (value === '-')
            //    return false;
            //value = $(param[0]).combobox("getValue");
            //alert(value);
            return value !== "-" && value !== "";
        },
        message: "请选择一个有效内容"
    },
    noZero: {
        validator: function (value, param) {
            return value > 0;
        },
        message: "不能为0"
    },
    strLimit: {
        validator: function (value, param) {
            if ((/^[ 　\t\r\n]+$/.test(value))) {
                param[2] = "内容不应该为空白";
                return false;
            }
            if (param[0] && param[1]) {
                if (value.length < param[0] || value.length > param[1]) {
                    param[2] = "长度应该在" + param[0] + "与" + param[1] + "之间";
                    return false;
                }
            }
            else if (param[0]) {
                if (value.length < param[0]) {
                    param[2] = "长度应该大于" + param[0];
                    return false;
                }
            }
            else if (param[1]) {
                if (value.length > param[1]) {
                    param[2] = "长度应该小于" + param[1];
                    return false;
                }
            }
            return true;
        },
        message: "{2}"
    },
    dateLimit: {
        validator: function (value, param) {
            var date = NewDate(value);
            if (date.getYear() <= 1) {
                param[2] = "日期不正确(应如:2017-7-1)";
                return false;
            }
            if (param[0] && param[1]) {
                if (date < param[0] || date > param[1]) {
                    param[2] = "日期应该在" + param[0] + "与" + param[1] + "之间";
                    return false;
                }
            }
            else if (param[0]) {
                if (date < param[0]) {
                    param[2] = "日期应该在" + param[0] + "之后";
                    return false;
                }
            }
            else if (param[1]) {
                if (date > param[1]) {
                    param[2] = "日期应该在" + param[1] + "之前";
                    return false;
                }
            }
            return true;
        },
        message: "{2}"
    },
    numLimit: {
        validator: function (value, param) {
            if (!(/^[+|-]?([0-9]+)|[0-9]+$/.test(value))) {
                param[2] = "数字不正确";
                return false;
            }
            var num = parseInt(value);
            if (num === NaN) {
                param[2] = "数字不正确";
                return false;
            }
            if (param[0] && param[1]) {
                if (num < param[0] || num > param[1]) {
                    param[2] = "数字应该在" + param[0] + "与" + param[1] + "之间";
                    return false;
                }
            }
            else if (param[0]) {
                if (num < param[0]) {
                    param[2] = "数字应该大于" + param[0];
                    return false;
                }
            }
            else if (param[1]) {
                if (num > param[1]) {
                    param[2] = "数字应该小于" + param[1];
                    return false;
                }
            }
            return true;
        },
        message: "数字应该在{0}与{1}之间"
    },
    floatLimit: {
        validator: function (value, param) {
            if (!(/^[+|-]?([0-9]+\.[0-9]+)|[0-9]+$/.test(value))) {
                param[2] = "数字不正确";
                return false;
            }
            var num = parseFloat(value);
            if (num === NaN) {
                param[2] = "数字不正确";
                return false;
            }
            if (param[0] && param[1]) {
                if (num < param[0] || num > param[1]) {
                    param[2] = "数字应该在" + param[0] + "与" + param[1] + "之间";
                    return false;
                }
            }
            else if (param[0]) {
                if (num < param[0]) {
                    param[2] = "数字应该大于" + param[0];
                    return false;
                }
            }
            else if (param[1]) {
                if (num > param[1]) {
                    param[2] = "数字应该小于" + param[1];
                    return false;
                }
            }
            return true;
        },
        message: "数字应该在{0}与{1}之间"
    },
    customValidate: {
        validator: function (value, param) {
            var msg = param[0](value); //调用函数
            if (msg != null)
                param[1] = msg;
            return msg == null || msg == "";
        },
        message: "{1}" //显示校验错误信息
    },
    //强制错误
    setMessageValidate: {
        validator: function (value, param) {
            param[1] = param[0]; //消息
            return false;
        },
        message: "{1}" //显示校验错误信息
    }
});


/*
保存支持
*/
function doSave(title, formid, url, onsubimt, onSucceed) {
    if (onsubimt)
        onsubimt();
    if (!$(formid).form("validate"))
        return;
    ajax_post(title, url, getFormJson(formid), function (result) {
        result = ajaxComplete(title, result);
        if (result && onSucceed)
            onSucceed(result);
    });
}
function submitData(formid, url, onSucceed) {
    if (!$(formid).form("validate"))
        return;
    ajax_post("提交", url, getFormJson(formid), function (result) {
        result = ajaxComplete("提交", result);
        if (result && onSucceed)
            onSucceed(result);
    });
}

/*
删除支持
*/
function deleteData(id, url, onSucceed) {
    if (!id)
        return;
    $.messager.confirm("删除", "是否确定删除当前选择的数据?", function (yn) {
        if (yn) {
            doDelete(id, url, onSucceed);
        }
    });
}

function doDelete(id, url, onSucceed) {
    ajaxOperator("删除", url, { ID: id }, function (result) {
        if (onSucceed)
            onSucceed(result);
        else
            reload();
    });
}
