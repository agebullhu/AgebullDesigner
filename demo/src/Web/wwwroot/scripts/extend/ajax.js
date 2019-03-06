var apihost = "http://zero.yizuanbao.cn/";

var globalOptions = {
    /**
     * api访问配置
     */
    api: {
        /**
         * Demo的api访问地址
         */
        projectDemoHost:"Demo/",
        /**
         * api访问基地址
         */
        defApiHost: 'User/',
        /**
         * api访问基地址
         */
        userApiHost: 'User/',
        /**
         * 用户中心api访问基地址
         */
        authApiHost: 'Authority/',
        /**
         * 页面api访问基地址
         */
        appApiHost: 'App/',
        /**
        * 超时时间设置为10秒
        */
        timeOut: 30000,
        /**
         * 自定义api访问地址
         */
        customHost: null
    },

    /**
	 * 组合并得到一个正确的api调用的url
     * @param {string} addr 地址
     * @param {string} host 主机
	 * @returns {string} url
	 */
    geturl: function (addr, host) {
        var href;
        if (!host)
            href = globalOptions.api.customHost
                ? globalOptions.api.customHost
                : globalOptions.api.defApiHost;
        else
            href = host;
        
        return apihost + href + addr;
    },

    /**
	 * 系统操作
	 */
    user: {
		/**
		 * 客户端令牌
		 * @returns {string} 令牌
		 */
        getToken: function () {
            if (localStorage.sys_access_token && localStorage.sys_access_token !== 'undefined')
                return localStorage.sys_access_token;
            if (localStorage.sys_device_id && localStorage.sys_device_id !== 'undefined')
                return localStorage.sys_device_id;
            return "";
        },
		/**
		 * 客户端设备标识
		 * @returns {string} 设备标识
		 */
        getDeviceId: function () {
            return localStorage.sys_device_id;
        },
		/**
		 * 客户端设备标识
		 * @param {string} did 设备标识
		 */
        setDeviceId: function (did) {
            localStorage.sys_device_id = did;
            console.log("DeviceId:" + did);
        },
		/**
		 * 保存客户端缓存登录者信息
		 * @param {object} loginInfo 登录者信息
		 */
        setUserInfo: function (loginInfo) {
            localStorage.sys_access_token = loginInfo.AccessToken;
            localStorage.sys_refresh_token = loginInfo.RefreshToken;
            localStorage.AccessToken = "Bearer " + loginInfo.AccessToken;
            sessionStorage.AccessToken = "Bearer " + loginInfo.AccessToken;
            if (loginInfo.Profile) {
                localStorage.sys_user_id = loginInfo.Profile.UserId;
                localStorage.sys_phone = loginInfo.Profile.PhoneNumber;
                localStorage.sys_nick_name = loginInfo.Profile.NickName;
                localStorage.sys_avatar_url = loginInfo.Profile.AvatarUrl;
                if (loginInfo.ExpiresIn)
                    localStorage.sys_expires_in = loginInfo.ExpiresIn;
            }
        },
		/**
		 * 登录状态检查
		 * @returns {void}  
		 */
        isLogin: function () {
            return localStorage.sys_access_token;
        },
        logout: function (inLogin) {
            localStorage.removeItem("sys_access_token");
            localStorage.removeItem("sys_refresh_token");
            localStorage.removeItem("sys_user_id");
            localStorage.removeItem("sys_phone");
            localStorage.removeItem("sys_nick_name");
            localStorage.removeItem("sys_avatar_url");
            localStorage.removeItem("sys_expires_in");
            localStorage.removeItem("sys_diacounts");
            localStorage.removeItem("sys_usercounts");
            if (!inLogin)
                location.href = "/login.html";
        },
		/**
		 * 清除运行时缓存信息
		 * @param {int} type 1 指刚进去app时，有些缓存信息只有在刚进入app时才清除一次
		 * @returns {void}  
		 * 
		 */
        clearTempValue: function (type) {
            localStorage.removeItem("temp");
            localStorage.removeItem("tmp_nav_paths");
            localStorage.removeItem("tmp_diamondNum");
            localStorage.removeItem("tmp_diamondSource");
            localStorage.removeItem("tmp_everyId");
            localStorage.removeItem("tmp_everyVerSion");
            localStorage.removeItem("tmp_VerifyImgId");
            localStorage.removeItem("tmp_VerifyCodeContent");
            localStorage.removeItem("tmp_pho_num");
            localStorage.removeItem("tmp_verifyImgId");
            localStorage.removeItem("tmp_typid");
            localStorage.removeItem("tmp_images_check");

            if (type === 1) {
                localStorage.removeItem("tmp_did_lock");
                localStorage.removeItem("tmp_token_lock");
                localStorage.removeItem("tmp_tokenOK");
                localStorage.removeItem("tmp_hasInviteShow");
                localStorage.removeItem("tmp_sdkIsShow");
                localStorage.removeItem("tmp_backdoor_lock");
            }
        },
		/**
		 * 缓存NickName
		 * @param {string} value NickName
		 * @returns {void}
		 */
        setNickName: function (value) {
            localStorage.sys_nick_name = value;
        },
		/**
		 * 取缓存NickName
		 * @returns {string} NickName
		 */
        getNickName: function () {
            return localStorage.sys_nick_name;
        },
		/**
		 * 缓存用户头像
		 * @param {string} value 用户头像
		 * @returns {void} 
		 */
        setAvatarUrl: function (value) {
            localStorage.sys_avatar_url = value;
        },
		/**
		 * 取缓存用户头像
		 * @returns {object}  用户头像
		 */
        getAvatarUrl: function () {
            return localStorage.sys_avatar_url;
        },
		/**
		 * 取得登录用户信息
		 * @returns {object}  登录用户信息
		 */
        info: function () {
            return {
                userId: localStorage.sys_user_id,
                phone: localStorage.sys_phone,
                nickName: localStorage.sys_nick_name,
                avatarUrl: localStorage.sys_avatar_url,
                expiresIn: localStorage.sys_expires_in
            };
        },
		/**
		 * 取得用户ID
		 * @returns {int}  用户ID
		 */
        userId: function () {
            return localStorage.sys_user_id;
        }
    }
};

function ajax_post(title, api, data, onSucceed, onFailed, host) {
    showBusy(title);
    var href = globalOptions.geturl(api,host);
    console.log(href);

    $.ajax({
        url: href,
        type: 'post',
        dataType: 'json',
        data: data,
        timeout: globalOptions.api.timeout,
        headers: {
            "Authorization": "Bearer " + globalOptions.user.getToken()
        },
        /**
         * 成功回调
         * @param {string} jsonStr 文本
         * @returns {void} 
         */
        success: function (jsonStr) {
            try {
                if (onSucceed)
                    onSucceed(jsonStr);
            } catch (ex) {
                console.exception(ex);
                showMessage(title, "结果处理失败!");
            }
        },
        /**
         * 异常回调
         * @param {object} xhr  xhr
         * @param {object} type type
         * @param {object} errorThrown errorThrown
         * @returns {void}  
         */
        error: function (xhr, type, errorThrown) {
            console.log(api + "(error):" + type);
            showServerNoFind(title);
            if (onFailed)
                onFailed();
        },
        complete: function () {
            hideBusy();
        }
    });
}

function checkSysErrorCode(result) {
    if (!result || !result.status) {
        return false;
    }
    switch (result.status.code) {
        case 40036:
            refreshAt();
            return true;
        case 40022:
        case 40001:
        case 40083:
        case 40082:
        case 40081:
        case 40421:
            showLogout();
            return true;
    }
    return false;
}
function checkApiStatus(title,result, tip) {
    if (!result || !result.status) {
        if (tip)
            showTip(title, "发生未知错误，操作失败!");
    }
    if (checkSysErrorCode(result)) {
        return;
    }
    if (!tip)
        return;
    if (result.status.msg) {
        showTip(title, result.status.msg + '!');
    } else {
        showTip(title, "发生未知错误，操作失败!");
    }
}


function evalResult(result) {
    if (!result)
        return null;
    try {
        if (typeof result === "string") {
            console.log(result);
            return eval("(" + result + ")");
        }
    } catch (ex) {
        console.log(ex);
        return null;
    }
    return result;
}

function ajaxComplete(title, jsonStr) {
    try {
        var result = evalResult(jsonStr);
        if (!result) {
            showTip(title, "未知错误!");
            return false;
        }
        else if (result.success) {
            showTip(title, "操作成功!");
            return result;
        }
        if (!checkSysErrorCode(result)) {
            if (result.status && result.status.msg) {
                showTip(title, result.status.msg + '!');
            } else {
                showTip(title, "发生未知错误，操作失败!");
            }
        }
    } catch (ex) {
        showTip(title, "发生未知错误，操作失败!");
    }
    return false;
}

function ajaxCompleteByOrgMessage(title, jsonStr) {
    try {
        var result = evalResult(jsonStr);
        if (result && result.success) {
            return result;
        }
        if (!checkSysErrorCode(result)) {
            if (result.status && result.status.msg) {
                showTip(title, result.status.msg + '!');
            } else {
                showTip(title, "发生未知错误，操作失败!");
            }
        }
    } catch (ex) {
        showMessage(title, "操作失败!");
    }
    return false;
}

function ajaxLoadString(title, url, onSucceed, arg, host) {
    ajax_post(title, url, arg, onSucceed, host);
}

function call_ajax(title, url, data, onSucceed, onFailed, host, tip) {
    ajax_post(title, url, data, function (jsonStr) {
        var result = evalResult(jsonStr);
        if (!result || !result.success) {
            checkApiStatus(title,result, tip);
        } else if (onSucceed) {
            onSucceed(result);
        }
    }, onFailed, host);
}
function ajaxLoadValue(title, url, args, onSucceed, host) {
    ajax_post(title, url, args, function (jsonStr) {
        var result = evalResult(jsonStr);
        if (!result || !result.success) {
            checkApiStatus(title,result, true);
        } else if (onSucceed) {
            onSucceed(result.data);
        }
    }, null, host);
}

function doPost(title, url, data, onSucceed, onFailed, host) {
    call_ajax(title, url, data, onSucceed, onFailed, host, true);
}
function doOperator(title, url, data, onSucceed, host) {
    call_ajax(title, url, data, onSucceed, null, host, true);
}

function ajaxOperator(title, url, data, onSucceed, host) {
    call_ajax(title, url, data, onSucceed, null, host, true);
}

function ajaxLoadScript(title, url, args, onSucceed, host) {
    call_ajax(title, url, data, onSucceed, null, host, false);
}

//执行无结果提示操作
function doSilentOperator(title, url, data, onSucceed, onFailed, host) {
    call_ajax(title, url, data, onSucceed, onFailed, host, false);
}
//执行无结果提示操作
function doSilentOperator2(title, url, data, onSucceed, onFailed, host) {
    call_ajax(title, url, data, onSucceed, onFailed, host, false);
}

/**
 * 执行远程操作
 * @public 
 * @param {string} title 操作标题
 * @param {string} url 远程URL
 * @param {object} arg 参数
 * @param {function} callBack 执行完成后的回调方法
 * @param {string} confirmMessage 确认操作的消息
 * @param {string} host 如Api的站点名称与配置不同,则设置
 * @returns {void} 
 */
function call_remote(title, url, arg, callBack, confirmMessage, host) {

    if (!confirmMessage)
        confirmMessage = "确定要执行操作吗?";
    $.messager.confirm(title, confirmMessage, function (s) {
        if (s) {
            doOperator(title, url, arg, callBack, host);
        }
    });
}

function getFormJson(id) {
    var dataJson = {};
    var test = $(id).serializeArray();
    for (var i = 0; i < test.length; i++) {
        var nameT = test[i].name;
        var valueT = test[i].value;
        dataJson[nameT] = valueT.trim();
    }
    /*隐藏*/
    $(id + " input[type='hidden']").each(function () {
        var nameT = $(this).attr('name');
        if (!nameT)
            nameT = $(this).attr('id');
        var valueT = $(this).val();
        dataJson[nameT] = valueT;
    });
    /*获取复选框的值*/
    $(id + " input[type='checkbox']").each(function () {
        var nameT = $(this).attr('name');
        if (!nameT)
            nameT = $(this).attr('id');
        if (!$(this).is(':checked')) {
            dataJson[nameT] = "0";
        } else {
            dataJson[nameT] = "1";
        }
    });
    return dataJson;
}
var isBusy = false;
function showBusy(title) {
    isBusy = true;
    setTimeout(300, function () {
        if (!isBusy)
            return;
        var busy = document.getElementById("__loading__");
        if (busy) {
            $("#__loading__").css("visibility", "visible");
        } else {
            $.messager.progress({
                title: title,
                msg: "正在处理,请稍候……",
                text: "**************************",
                interval: 800
            });
        }
    });
}
function hideBusy() {
    isBusy = false;
    var busy = document.getElementById("__loading__");
    if (busy) {
        $("#__loading__").css("visibility", "hidden");
    } else {
        $.messager.progress("close");
    }
}
function showLogout() {
    $.messager.confirm("登录过期", "登录过期,需要跳转到登录页面重新登录！", function (r) {
        if (r)
            globalOptions.user.logout();
    });
}
function getDid() {
    console.log("Refresh device ID(...)");
    ajax_post("初始化",
        "v1/refresh/did",
        {
            deviceId: globalOptions.user.getDeviceId()
        },
        function (jsonStr) {
            var r = evalResult(jsonStr);
            if (r && r.success) {
                globalOptions.user.setDeviceId(r.data);
                return;
            }
            if (r) {
                if (r.status.code === 40022) {
                    localStorage.sys_device_id = "";
                    getDid();
                } else {
                    console.log("Refresh device ID(error):" + r.status.msg);
                }
            } else {
                console.log("Refresh device ID(null):" + jsonStr);
            }
        },
        null,
        globalOptions.api.authApiHost);
}

function refreshAt() {
    console.log("Refresh access token(...)");
    ajax_post("刷新令牌",
        "v1/refresh/at",
        {
            AccessToken: localStorage.sys_access_token,
            RefreshToken: localStorage.sys_refresh_token
        },
        function (jsonStr) {
            var r = evalResult(jsonStr);
            if (r && r.success) {
                localStorage.sys_access_token = r.AccessToken;
                localStorage.sys_refresh_token = r.RefreshToken;
                return;
            }
            if (r) {
                console.log("Refresh access token(error):" + r.status.msg);
            } else {
                console.log("Refresh device ID(error):" + r.status.msg);
            }
            globalOptions.user.logout();
        },
        function () {
            showLogout();
        },
        globalOptions.api.authApiHost
    );
}

/**
 * 从远程载入下拉列表的数据
 * @param {string} eid id
 * @param {string} url 地址
 * @param {function} callback 成功后的回调
 * @param {boolean} tree 是否树控件
 * @param {string} txtField 文本字段
 * @param {string} host 地址根
 */
function comboRemote(eid, url, callback, tree, txtField, host) {
    doSilentOperator("", url, null, function (res) {
        if (res.success && res.data)
            setComboData(eid, res.data, callback, tree, txtField);
        else
            setComboData(eid, { id: 0, text: '数据下载失败' }, callback, tree);
    }, null, host);
}
/**
 * 设置下拉列表的数据
 * @param {string} eid id
 * @param {object} data 数据
 * @param {function} callback 成功后的回调
 * @param {boolean} tree 是否树控件
 * @param {string} txtField 文本字段
 */
function setComboData(eid, data, callback, tree, txtField) {
    if (!txtField)
        txtField = "text";
    try {
        var vl;
        if (tree) {
            vl = $(eid).combotree("getValue");
            $(eid).combotree({ valueField: "id", textField: txtField, data: data });
            $(eid).combotree("setValue", vl);
            if (callback)
                callback(data, data);
        } else {
            vl = $(eid).combobox("getValue");
            $(eid).combobox({ valueField: "id", textField: txtField, data: data });
            $(eid).combobox("setValue", vl);
            if (callback)
                callback(data, vl);
        }
    } catch (e) {
        console.error("%s.%s() ： %s", typeof (this), "setComboData", e);
        alert(eid + "***" + e);
    }
}