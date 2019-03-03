
function showServerNoFind(title) {
    showTip(title, "服务器无法访问");
}

/**
 * 显示提示（右下角自动隐藏那种）
 * @param {string} title 标题
 * @param {string} message 消息
 * @returns {void} 
 */
function showTip(title, message) {
    $.messager.show({
        title: !title ? "提示" : title,
        msg: '<div class="tip_block"><img src="/images/succeed.png" /><span>' + message + "</span></div>",
        showType: "fade",
        showSpeed: 0,
        width: 180, height: 120,
        timeout: 3000,
        style: {
            left: 0,
            right: "",
            top: "",
            bottom: -document.body.scrollTop - document.documentElement.scrollTop
        }
    });
}
/**
 * 显示警告（右下角自动隐藏那种）
 * @param {string} message 消息
 * @returns {void} 
 */
function showWarning(message) {
    $.messager.show({
        title: "警告",
        msg: '<div style="font-size:14px;"><img src="/images/warning.png" style="float:left"/><span>' + message + "</span></div>",
        showType: "fade",
        showSpeed: 0,
        width: 300, height: 200,
        timeout: 30000,
        style: {
            left: 0,
            right: "",
            top: "",
            bottom: -document.body.scrollTop - document.documentElement.scrollTop
        }
    });
}

/**
 * 显示提示（对话框类型）
 * @param {string} title 标题
 * @param {string} message 消息
 * @returns {void} 
 */
function showMessage(title, message) {
    $.messager.alert(title == null ? "提示" : title, message);
}



/*
对话框方便
*/
function openDialog(dlgid, title) {
    $(dlgid).dialog("open").dialog("setTitle", title);
}

function closeDialog(dlgid) {
    $(dlgid).dialog("close");
}

function loadPanel(id, url, wid, hei, cached, onload) {
    $(id).panel({
        width: wid,
        height: hei,
        border: false,
        cache: cached,
        loadingMessage: "正在载入……",
        href: url,
        onLoad: onload
    });
}

function loadPanel2(id, url, onload) {
    $(id).panel({
        width: "auot",
        height: "auot",
        border: false,
        cache: false,
        loadingMessage: "正在载入……",
        href: url,
        onLoad: function () {
            if (onload != null)
                onload();
        }
    });
}

function ajaxLoadUi(id, url, onload, always) {
    if (currentPage === url && !always)
        return;
    currentPage = url;
    $(id).panel({
        width: "auot",
        height: "auot",
        border: false,
        cache: false,
        loadingMessage: "正在载入……",
        href: url,
        onLoad: function () {
            if (onload != null)
                onload();
        }
    });
}
