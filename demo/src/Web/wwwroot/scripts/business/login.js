
$(document).ready(function () {
    globalOptions.api.customHost = globalOptions.api.authApiHost;
    globalOptions.user.logout(true);
    browserCheck();
    //回车登录
    if (document.addEventListener) {
        document.addEventListener("keypress", function(evt) {
            enterLogin(evt);
        }, true);
    } else {
        document.attachEvent("onkeypress", function (evt) {
            enterLogin(evt);
        });
    }
    
    getDid();
});


function enterLogin(evt) {
    if (evt.keyCode === 13) {
        doLogin();
    }
    return false;
}
function doLogin() {

    var user = $("input[name='MobilePhone']").val();
    if (!user || user.trim() === "") {
        $("#cMsg").text("用户名密码不能为空");
        return false;
    }
    var pwd = $("input[name='UserPassword']").val();
    if (!pwd || pwd.trim() === "") {
        $("#cMsg").text("用户名密码不能为空");
        return false;
    }
    if (!$("#fm").form("validate"))
        return false;
    ajax_post("登录", "v1/login/account", getFormJson("#fm"), function (jsonStr) {
        var r = evalResult(jsonStr);
        if (!r)
            $("#cMsg").text("未知错误");
        else if (!r.success) {
            $("#cMsg").text(r.status.msg);
        }
        else {
            globalOptions.user.setUserInfo(r.data);
            window.location.href = "/index.html";
        }
    }, function() {
        $("#cMsg").text("未知错误");
    });
    return false;
} 
