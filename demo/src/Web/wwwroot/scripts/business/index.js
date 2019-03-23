var version = 20190105;
if (!globalOptions.user.isLogin()) {
    globalOptions.user.logout();
}

var vue_option = {
    el: '#work_space',
    data: {
        ws_active: false,
        isCollapse: false,
        cwid: '260px',
        ctxt:"操作菜单",
        user: '',
        nodes: []
    },
    filters: {
        formatDate(time) {
            var date = new Date(time);
            return formatDate(date, 'MM-dd hh:mm:ss');
        },
        formatUnixDate(unix) {
            if (unix === 0)
                return "*";
            var date = new Date(unix * 1000);
            return formatDate(date, 'MM-dd hh:mm:ss');
        },
        formatNumber(number) {
            if (number) {
                return number.toFixed(4);
            } else {
                return "0.0";
            }
        },
        thousandsNumber(number) {
            if (number) {
                return toThousandsInt(number);
            } else {
                return "0";
            }
        },
        formatNumber1(number) {
            if (number) {
                return number.toFixed(4);
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
        }
    },
    methods: {
        collapse() {
            vue_option.data.isCollapse = !vue_option.data.isCollapse;
            vue_option.data.cwid = vue_option.data.isCollapse ? '' : '260px';
            vue_option.data.ctxt = vue_option.data.isCollapse ? '' : vue_option.data.user;
        },
        go_home() {
            vue_option.data.isCollapse = true;
            vue_option.data.cwid = vue_option.data.isCollapse ? '' : '260px';
            vue_option.data.ctxt = vue_option.data.isCollapse ? '' : vue_option.data.user;
            showIframe('/help.html');
        },
        logout() {
            onLogout();
        },
        onModifyPassword() {
            onModifyPassword();
        },
        menu_select(index) {
            onNodeClick(index);
            //location.href = "/Doc/Index/" + index;
        }
    }
};


function ready() {
    //browserCheck();
    var user = globalOptions.user.info();
    vue_option.data.ctxt= vue_option.data.user = user.nickName;
    //$('#lbUserName').text(user.nickName);
    //$('#lbUserInfo').text(user.nickName);
    //初始化函数
    $(window).resize();
    ajaxLoadValue("载入菜单", "v1/page/tree", {}, function (data) {
        if (data)
            vue_option.data.nodes = data;
    });

    var vm = new Vue(vue_option);
}


var custom_options = {
    new_window: false,
    hide_menu: false
};
function onNewTab() {
    custom_options.new_window = !custom_options.new_window;
    $("#mm2-nt").linkbutton({
        text: (custom_options.new_window ? "√" : "") + "页面新标签打开"
    });
}
function onAutoHide() {
    custom_options.hide_menu = !custom_options.hide_menu;
    $("#mm2-mh").linkbutton({
        text: (custom_options.hide_menu ? "√" : "") + "菜单自动隐藏"
    });
    if (custom_options.hide_menu)
        $(document.body).layout("collapse", "west");
    else
        $(document.body).layout("expand", "west");
}
function openByTab(url) {

    var a = $("#newTab");
    a.attr("href", url);
    var e = document.createEvent("MouseEvents");
    e.initEvent("click", true, true);
    a.get(0).dispatchEvent(e);
}

function showIframe(url) {
    if (!url)
        return;
    //if (custom_options.new_window) {
    //    openByTab(url);
    //    return;
    //}
    //$("#__content_loading__").css("visibility", "visible");

    console.log(url + ".htm");
    var ws = document.getElementById("work_frame");
    ws.contentWindow.document.write("");//清空iframe的内容
    $(ws).one("load", onWorkSpaceLoad);
    if (url.indexOf("?") > 0)
        ws.src = url + "&__t=" + version;
    else
        ws.src = url + "?__t=" + version;
}
function onWorkSpaceLoad() {
    //$("#__content_loading__").css("visibility", "hidden");
    return true;
}

function onNodeClick(url) {
    if (url) {
        //var name = GetTreePath(node);
        //$("#txtPage").html(name);
        //$("#txtPage").attr("title", name);
        showIframe(url + ".htm");
    }
}
function onLogout() {
    $.messager.confirm("退出登录", "你确定要退出系统并后退到登录页面吗?", function (yn) {
        if (yn)
            globalOptions.user.logout();
    });
}

function onModifyPassword() {
    $("#pwdDialog").dialog({
        iconCls: "pag-list",
        modal: true,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        resizable: false,
        closed: true,
        href: "/system/user/pwd.htm"
    });
    $("#pwdDialog").dialog("open");

    //var userName = $("#hiddenUserName").val();
    //$("#userName").val(userName);
    //$("#oldPwd").focus();
}

//确认修改密码
function changeEnter() {
    var enterPwd = $("#enterPwd").val();
    var newPwd = $("#newPwd").val();
    var oldPwd = $("#oldPwd").val();

    if (oldPwd == "" || oldPwd == null) {
        $.messager.alert("提示", "原始密码未输入，请输入原始密码");
        $("#oldPwd").focus();
        return;
    }

    if (!(enterPwd === newPwd)) {
        $.messager.alert("提示", "密码确认不一致，请重新输入");
        $("#enterPwd").focus();
        return;
    }

    ajaxOperator("修改密码", "/Api/Data.aspx?action=m",
        {
            oldPwd: encodeURI(oldPwd),
            newPwd: encodeURI(enterPwd)
        },
        function () {
            $("#pwdDialog").window("close");
            $("#grid").datagrid("reload");
        });
}

//取消修改密码
function changeCancel() {
    $("#pwdDialog").window("close");
}

ready();