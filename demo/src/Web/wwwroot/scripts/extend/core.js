
//Object.create方法的跨浏览器处理
if (!Object.create) {
    Object.create = function (o, properties) {
        if (typeof o !== "object" && typeof o !== "function") throw new TypeError("Object prototype may only be an Object: " + o);
        else if (o === null) throw new Error("This browser's implementation of Object.create is a shim and doesn't support 'null' as the first argument.");

        if (typeof properties != "undefined") throw new Error("This browser's implementation of Object.create is a shim and doesn't support a second argument.");

        function F() { }

        F.prototype = o;

        return new F();
    };
}
/*
'代码名:' + nav.appCodeName+
'次级版本:' + nav.appMinorVersion+
'名称:' + nav.appName+
'平台和版本信息:' + nav.appVersion+
'当前语言:' + nav.browserLanguage+
'指明浏览器中是否启用 cookie 的布尔值:' + nav.cookieEnabled+
'浏览器系统的 CPU 等级:' + nav.cpuClass+
'指明系统是否处于脱机模式的布尔值:' + nav.onLine+
'运行操作系统平台:' + nav.platform+
' OS 使用的默认语言:' + nav.systemLanguage+
'由客户机发送服务器的 user-agent 头部的值:' + nav.userAgent+
' OS 的自然语言设置:' + nav.userLanguage+



*/
function browserInfo() {
    var nav = navigator;
    return "代码名:" + nav.appCodeName +
        "<br/>次级版本:" + nav.appMinorVersion +
        "<br/>名称:" + nav.appName +
        "<br/>平台和版本信息:" + nav.appVersion +
        "<br/>当前语言:" + nav.browserLanguage +
        "<br/>指明浏览器中是否启用 cookie 的布尔值:" + nav.cookieEnabled +
        "<br/>浏览器系统的 CPU 等级:" + nav.cpuClass +
        "<br/>指明系统是否处于脱机模式的布尔值:" + nav.onLine +
        "<br/>运行操作系统平台:" + nav.platform +
        "<br/>OS 使用的默认语言:" + nav.systemLanguage +
        "<br/>由客户机发送服务器的 user-agent 头部的值:" + nav.userAgent +
        "<br/>OS 的自然语言设置:" + nav.userLanguage;
}
function browserCheck() {
    var nav = navigator;
    if (navigator.appMinorVersion != undefined) {
        showBrowserWaring();
    }
    else if (navigator.appName === "Microsoft Internet Explorer") {//百度 兼容模式
        showBrowserWaring();
    }
    else if (checkBrowser("MSIE")) {//百度 兼容模式
        showBrowserWaring();
    }
    else if (checkBrowser("QQBrowser")) {
        if (!checkBrowser("AppleWebKit"))
            showBrowserWaring();
    } 
    else if (!checkBrowser("Gecko")) {
        showBrowserWaring();
    }
}
function checkBrowser(val) {
    return navigator.userAgent.indexOf(val) >= 0;
}
function showBrowserWaring() {
    $("#xx_dialog_region_xx").dialog({
        title: "警告",
        href: "/waring.html",
        width: 720,
        height: 560,
        closed: false,
        cache: false,
        modal: true
    });
}
window.o99 = { width: "100%", height: "100%" }


function fireKeyEvent(evtType, keyCode) {
    var win = document.defaultView || document.parentWindow,
        evtObj;

    if (document.createEvent) {
        if (win.KeyEvent) {
            evtObj = document.createEvent("KeyEvents");
            evtObj.initKeyEvent(evtType, true, true, win, false, false, false, false, keyCode, 0);
        }
        else {
            evtObj = document.createEvent("UIEvents");
            Object.defineProperty(evtObj, "keyCode", {
                get: function () { return this.keyCodeVal; }
            });
            Object.defineProperty(evtObj, "which", {
                get: function () { return this.keyCodeVal; }
            });
            evtObj.initUIEvent(evtType, true, true, win, 1);
            evtObj.keyCodeVal = keyCode;
            if (evtObj.keyCode !== keyCode) {
                console.log("keyCode " + evtObj.keyCode + " 和 (" + evtObj.which + ") 不匹配");
            }
        }
        document.dispatchEvent(evtObj);
    }
    else if (document.createEventObject) {
        evtObj = document.createEventObject();
        evtObj.keyCode = keyCode;
        el.fireEvent("on" + evtType, evtObj);
    }
}
function doKeySave() {
    window.dialog_input.save();
    var par = $(document.activeElement).parents(".inputRegion").parent().parent().find(".inputRegion");
    if (!par[0]) {
        return;
    }
    var nd = $(par[0]).find("input");
    if (!nd[0]) {
        nd = par.find("textarea");
        if (!nd[0]) {
            return;
        }
    }
    nd.focus();
    nd.select();
}
function fireFoxHandler(evt) {
    if (evt.keyCode == 13) {
        var par = $(document.activeElement).parents(".inputRegion").parent().next();
        if (!par[0]) {
            doKeySave();
            return;
        }
        while (par[0].tagName == "BR") {
            par = par.next();
            if (!par[0]) {
                doKeySave();
                return;
            }
        }
        var nd = par.find("input");
        if (!nd[0]) {
            nd = par.find("textarea");
            if (!nd[0]) {
                doKeySave();
                return;
            }
        }
        nd.focus();
        nd.select();
        return false;
    }
}

//处理键盘事件 禁止后退键（Backspace）密码或单行、多行文本框除外
//site www.jbxue.com
function banBackSpace(e) {
    var ev = e || window.event;//获取event对象
    var obj = ev.target || ev.srcElement;//获取事件源
    var t = obj.type || obj.getAttribute("type");//获取事件源类型
    //获取作为判断条件的事件类型
    var vReadOnly = obj.readOnly;
    var vDisabled = obj.disabled;
    //处理undefined值情况
    vReadOnly = (vReadOnly == undefined) ? false : vReadOnly;
    vDisabled = (vDisabled == undefined) ? true : vDisabled;
    //当敲Backspace键时，事件源类型为密码或单行、多行文本的，
    //并且readOnly属性为true或disabled属性为true的，则退格键失效
    var flag1 = ev.keyCode == 8 && (t == "password" || t == "text" || t == "textarea") && (vReadOnly == true || vDisabled == true);
    //当敲Backspace键时，事件源类型非密码或单行、多行文本的，则退格键失效
    var flag2 = ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea";
    //判断
    if (flag2 || flag1)
        return false;
    return true;
}
//禁止退格键 作用于Firefox、Opera
document.onkeypress = banBackSpace;
//禁止退格键 作用于IE、Chrome
document.onkeydown = banBackSpace;

