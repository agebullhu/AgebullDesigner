
/**   
* 扩展两个方法   
*/
$.extend($.fn.datagrid.methods, {
    /** 
    * 开打提示功能   
    * @param {object} jq   对象
    * @param {object} params 提示消息框的样式   
    * @return {object}   扩展
    */
    doCellTip: function (jq, params) {
        var showTip = function (data, td, e) {
            if ($(td).text() === "")
                return;
            data.tooltip.text($(td).text()).css({
                top: (e.pageY + 10) + "px",
                left: (e.pageX + 20) + "px",
                'z-index': $.fn.window.defaults.zIndex,
                display: "block"
            });
        };
        return jq.each(function () {
            var me = this;
            var grid = $(me);
            var options = $(me).data("datagrid");
            if (!options.tooltip) {
                var panel = grid.datagrid("getPanel").panel("panel");
                var defaultCls = {
                    'border': "1px solid #333",
                    'padding': "1px",
                    'color': "#333",
                    'background': "#f7f5d1",
                    'position': "absolute",
                    'max-width': "200px",
                    'border-radius': "4px",
                    '-moz-border-radius': "4px",
                    '-webkit-border-radius': "4px",
                    'display': "none"
                };
                var tooltip = $("<div id='celltip'></div>").appendTo("body");
                tooltip.css($.extend({}, defaultCls, params.cls));
                options.tooltip = tooltip;
                panel.find(".datagrid-body").each(function () {
                    var delegateEle = $(me).find("> div.datagrid-body-inner").length
                        ? $(me).find("> div.datagrid-body-inner")[0]
                        : me;
                    $(delegateEle).undelegate("td", "mouseover").undelegate(
                        "td", "mouseout").undelegate("td", "mousemove")
                        .delegate("td", {
                            'mouseover': function (e) {
                                if (params.delay) {
                                    if (options.tipDelayTime)
                                        clearTimeout(options.tipDelayTime);
                                    var me2 = this;
                                    options.tipDelayTime = setTimeout(
                                        function () {
                                            showTip(options, me2, e);
                                        }, params.delay);
                                } else {
                                    showTip(options, me, e);
                                }

                            },
                            'mouseout': function () {
                                if (options.tipDelayTime)
                                    clearTimeout(options.tipDelayTime);
                                options.tooltip.css({
                                    'display': "none"
                                });
                            },
                            'mousemove': function (e) {
                                var me = this;
                                if (options.tipDelayTime) {
                                    clearTimeout(options.tipDelayTime);
                                    options.tipDelayTime = setTimeout(
                                        function () {
                                            showTip(options, me, e);
                                        }, params.delay);
                                } else {
                                    showTip(options, me, e);
                                }
                            }
                        });
                });
            }

        });
    },
    /** 
    * 关闭消息提示功能
    * @param {object} jq   对象
    * @return {object}   扩展
    */
    cancelCellTip: function (jq) {
        return jq.each(function () {
            var me = this;
            var data = $(me).data("datagrid");
            if (data.tooltip) {
                data.tooltip.remove();
                data.tooltip = null;
                var panel = $(me).datagrid("getPanel").panel("panel");
                panel.find(".datagrid-body")
                    .undelegate("td", "mouseover")
                    .undelegate("td", "mouseout")
                    .undelegate("td", "mousemove");
            }
            if (data.tipDelayTime) {
                clearTimeout(data.tipDelayTime);
                data.tipDelayTime = null;
            }
        });
    }
});

function resetMessage(datas) {
    if (datas == undefined)
        return;
    var i;
    for (i = 0; i < datas.length; i++) {
        datas[i].msg = null;
        resetMessage(datas[i].children);
    }
}
/**
 * 返回支持数据校验的单元格样式格式化器
 * @param {object} value 值
 * @param {object} row 行数据
 * @returns {string} 样式
 */
function vlStyle(value, row) {
    if (row.msg) {
        return "background-color:#ffee00;color:red;";
    }
    if (row.AuditState == 2 && row.CanAudit) {
        return "color:blue;";
    }
    return "";
};

/*
图标单元格
*/
function iconCell(icon, size, tooltip) {
    if (!tooltip)
        tooltip = '';
    return "<div style='width:" + size + "px;height:" + size + "px;' class='" + icon + "' title='" + tooltip + "'></div>";
}

/*
图标单元格
*/
function iconFilter(rec, icon, click) {
    return "<a class='l-btn l-btn-plain'href='javascript:void(0)'onclick='return " + click + "'>" +
        "<span class='l-btn-left'>" +
        "<span class='l-btn-text " + icon + " l-btn-icon-left'style='width:20px'></span>" +
        "</span></a>";
}

/*
文本单元格
*/
function txtFilter(rec, txt, click) {
    return "<a class='l-btn l-btn-plain'href='javascript:void(0)'onclick='return " + click + "'>" +
        "<span class='l-btn-left'><span class='l-btn-left'>" + txt + "</span>" +
        "</a>";
}

/*
    编辑图标格式化器(表格列)
*/
editFormat = function (value, row) {
    return iconFilter(row, "icon-edit", "editDetails(" + row.ID + ")") + iconFilter(row, "icon-cancel", "deleDetails(" + row.ID + ")");
};

/*
    编辑文本格式化器(表格列)
*/
funcFilter = function (txt, id, func) {
    return "<a style='margin:5px' class='l-btn l-btn-plain' href='javascript:void(0)'onclick='return " + func + "(" + id + ");'>" +
        "<span class='l-btn-left'><span class='l-btn-left'>" + txt + "</span>" +
        "</a>";
};

/*
    编辑图标格式化器(表格列)
*/
urlFormat = function (value) {
    if (!value)
        return "-";
    return "<a target='_blank' style='margin:5px' href='" + value + "'>" + value + "</a>";
};


/*
 function (result) {
            try {
                var datas = me.execGridMethod("getData");
                if (!me.isTree)
                    datas = datas.rows;
                resetMessage(datas);
                var msgs = result.value;
                if (msgs == null || msgs.length == 0) {
                    me.execGridMethod("reload");
                } else {
                    for (i = 0; i < msgs.length; i++) {
                        var row = findGridData(msgs[i].id, datas);
                        if (row)
                            row.msg = msgs[i].msg;
                    }
                    me.execGridMethod("loadData", datas);
                    me.execGridMethod("showColumn", "msg");
                }
            } catch (e) {
                $.messager.alert('退回重做', e);
            }
        }
*/