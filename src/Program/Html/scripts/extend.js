//页面版本
var version = new Date().getTime();
//数据扩展
function extend(default_item, new_item) {
    if (new_item) {
        for (var i in new_item) {
            if (new_item[i]) {
                if (!default_item[i]) {
                    default_item[i] = new_item[i];
                    continue;
                }
                //console.log(typeof default_item[i]);
                if (typeof default_item[i] === 'object')
                    extend(default_item[i], new_item[i])
                else
                    default_item[i] = new_item[i];
            }
        }
    }
    return default_item;
}
//ajax扩展
var ajax_ex = {
    baseURL: "http://www.zeroteam.com.cn:8000",
    config(title, action) {
        if (!action)
            action = "";
        if (!title)
            title = "";
        else
            title = encodeURIComponent(title);
        var page = encodeURIComponent(document.title);
        return {
            baseURL: ajax_ex.baseURL,
            timeout: 30000,
            headers: {
                "x-zmvc-app": globalOptions.appId,
                "x-zmvc-page-title": page,
                "x-zmvc-action-code": action,
                "x-zmvc-action-title": title,
                "Authorization": globalOptions.getAuthorization()
            }
        };
    },
    /**
     * ajax 操作
     * @param {string} title 标题
     * @param {string} url 地址
     * @param {function} args 参数
     * @param {function} callback 成功回调
     * @param {function} failed 失败回调
     * @param {any} tip 是否显示提示
     */
    post(title, url, args, callback, failed, tip, action) {
        this.doAjax(title, url, args, callback, failed, tip, action);
    },
    doAjax(title, url, args, callback, failed, tip, action) {
        var that = this;
        var redo = function () {
            that.doAjax(title, url, args, callback, failed, tip);
        }
        common.showBusy(title);
        axios.post(url, args, that.config(title, action)).then(resp => {
            common.hideBusy();
            try {
                var res = that.evalResult(resp.data);
                if (!that.checkApiStatus(title, url, res, resp.status, tip, redo))
                    return;
                if (callback)
                    callback(res);
            } catch (ex) {
                console.error(`${url} : ${ex}`);
                common.showError(title, "结果处理失败!");
            }
        }).catch(err => {
            common.hideBusy();
            console.log(`${url} : ${err}`);
            if (!err.response) {
                common.showError(title, "网络错误");
                return;
            }
            if (err.response.data) {
                try {
                    var res = that.evalResult(err.response.data);
                    if (!that.checkApiStatus(title, url, res, err.response.status, tip, redo))
                        return;
                    if (failed)
                        failed(res);
                } catch (ex) {
                    console.error(`${url} : ${ex}`);
                    common.showError(title, "结果处理失败!");
                }
                return;
            }
            if (!that.checkApiStatus(title, url, null, err.response.status, tip, redo))
                return;
            if (failed)
                failed();
        });
    },
    /**
     * 校验API返回的标准状态
     * @param {string} title 任务标题
     * @param {string} url 原始URL
     * @param {string} result 返回值
     * @param {string} tip 是否显示提示
     * @param {Function} callback 令牌恢复时的回调
     * @returns {boolean} true表示可以继续后续操作,false表示应中断操作
     */
    checkApiStatus(title, url, result, status, tip, callback) {
        if (result)
            switch (result.code) {
                case OperatorStatusCode.Queue:
                case OperatorStatusCode.Success:
                    return true;
            }

        switch (status) {
            case 403:
                console.log(`${url} : 拒绝访问`);
                if (tip)
                    common.showError(title, "拒绝访问!");
                return true;
            case 404:
                console.log(`${url} : 页面不存在`);
                if (tip)
                    common.showError(title, "页面不存在!");
                return true;
            case 503:
                console.log(`${url} : 服务器拒绝操作`);
                if (tip)
                    common.showError(title, "服务器拒绝操作!");
                return true;
        }
        if (!result) {
            console.log(`${url} : 无返回内容`);
            if (tip)
                common.showError(title, "发生未知错误，操作失败!");
            return true;
        }
        if (result.code == OperatorStatusCode.TokenTimeOut) {//登录过期
            console.log(`${url} : ${result.message}`);

            globalOptions.user.refreshAt(callback);
            return false;
        }
        else if (result.message) {
            console.log(`${url} : ${result.message}`);
            if (tip)
                common.showError(title, result.message);
        }
        else if (result.code == 404) {
            console.log(`${url} : ${result.message}`);
            if (tip)
                common.showError(title, `接口(${url})不通!`);
        } else {
            console.log(`${url} : "发生未知错误，操作失败!"`);
            if (tip)
                common.showError(title, "发生未知错误，操作失败!");
        }
        return true;
    },
    /**
     * 转换返回值
     * @param {any} result 返回值对象或文本
     * @returns {object} 对象
     */
    evalResult(result) {
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
};

/**
 * 原始AJAX
 * @param {string} title 任务标题
 * @param {string} api 调用的API
 * @param {string} args 参数
 * @param {Function} onSucceed 成功回调
 * @param {Function} onFailed 失败回调
 * @param {Function} action 动作名称
 */
function ajax_page_post(title, api, args, onSucceed, onFailed) {
    ajax_ex.post(title, api, args, onSucceed, onFailed, true, "");
}

/**
 * 原始AJAX
 * @param {string} title 任务标题
 * @param {string} api 调用的API
 * @param {string} args 参数
 * @param {Function} onSucceed 成功回调
 * @param {Function} onFailed 失败回调
 * @param {Function} action 动作名称
 */
function ajax_action_post(title, action, api, args, onSucceed, onFailed) {
    ajax_ex.post(title, api, args, onSucceed, onFailed, true, action);
}


/**
 * 原始AJAX
 * @param {string} title 任务标题
 * @param {string} api 调用的API
 * @param {string} args 参数
 * @param {Function} onSucceed 成功回调
 * @param {Function} onFailed 失败回调
 */
function ajax_post(title, api, args, onSucceed, onFailed) {
    ajax_ex.post(title, api, args, onSucceed, onFailed, true);
}

/**
 * 取数据
 * @param {string} title 任务标题
 * @param {string} api 调用的API
 * @param {string} args 参数
 * @param {Function} onSucceed 成功回调
 * @param {Function} onFailed 失败回调
 */
function ajax_load(title, api, args, onSucceed, onFailed) {
    ajax_ex.post(title, api, args, res => {
        if (res.success)
            onSucceed(res.data);
        else if (onFailed)
            onFailed(res);
    }, onFailed, true);
}

/**
 * 远程调用(无提示)
 * @param {string} title 任务标题
 * @param {string} url 调用的API
 * @param {string} args 参数
 * @param {Function} onSucceed 成功回调
 * @param {Function} onFailed 失败回调
 */
function silentCall(title, url, args, onSucceed, onFailed) {
    ajax_ex.post(title, url, args, onSucceed, onFailed, false);
}

/**
 * 执行远程操作,操作显示一个确认框
 * @public 
 * @param {string} title 操作标题
 * @param {string} url 远程URL
 * @param {object} args 参数
 * @param {function} onSucceed 执行完成后的回调方法
 * @param {string} confirmMessage 确认操作的消息
 * @returns {void} 
 */
function confirmCall(title, url, args, onSucceed, confirmMessage) {
    if (!confirmMessage)
        confirmMessage = "确定要执行操作吗?";
    common.confirm(title, confirmMessage, () => {
        ajax_ex.post(title, url, args, onSucceed, null, true);
    });
}


//全局配置
var globalOptions = {
    /**
     * 应用标识
     */
    appId: '1Q430N9AM',
    /**
     * 客户端令牌
     * @returns {string} 令牌
     */
    getAuthorization: function () {
        return `Bearer ${this.user.getDeviceToken()}&${this.user.getAccessToken()}`;
    },
    /**
     * 客户端令牌
     * @returns {string} 令牌
     */
    getToken: function () {
        return this.user.getAccessToken();
    },
    /**
     * 系统操作
     */
    user: {
        /**
         * 客户端令牌
         * @returns {string} 令牌
         */
        getAccessToken: function () {
            var vl = localStorage.getItem('user_access_token');
            return vl ? vl : "";
        },
        /**
         * 客户端设备标识
         * @returns {string} 设备标识
         */
        getDeviceToken: function () {
            var vl = localStorage.getItem('device_token');
            return vl ? vl : "";
        },
        /**
         * 客户端设备标识
         * @param {string} did 设备标识
         */
        setDeviceToken: function (did) {
            if (did)
                localStorage.setItem('device_token', did);
            else
                localStorage.removeItem('device_token');
        },
        /**
         * 保存客户端缓存登录者信息
         * @param {object} loginInfo 登录者信息
         */
        setUserInfo: function (loginInfo) {

            localStorage.user_access_token = loginInfo.accessToken;
            localStorage.user_refresh_token = loginInfo.refreshToken;
            if (loginInfo.profile) {
                if (loginInfo.profile.uid)
                    localStorage.user_open_id = loginInfo.profile.uid;
                if (loginInfo.profile.oid)
                    localStorage.user_organization_id = loginInfo.profile.oid;
                if (loginInfo.profile.org)
                    localStorage.user_organization = loginInfo.profile.org;
                if (loginInfo.profile.phone)
                    localStorage.user_phone = loginInfo.profile.phone;
                if (loginInfo.profile.nickName)
                    localStorage.user_nick_name = loginInfo.profile.nickName;
                if (loginInfo.profile.avatarUrl)
                    localStorage.user_avatar_url = loginInfo.profile.avatarUrl;
            }
        },
        /**
         * 清除运行时缓存信息
         * @param {int} type 1 指刚进去app时，有些缓存信息只有在刚进入app时才清除一次
         * @returns {void}  
         * 
         */
        clearTempValue: function () {
            for (var i = localStorage.length - 1; i >= 0; i--) {
                var key = localStorage.key(i);
                if (key.length > 4 && key.substr(0, 4) === "tmp_")
                    localStorage.removeItem();
            }
        },
        /**
         * 缓存NickName
         * @param {string} value NickName
         * @returns {void}
         */
        setNickName: function (value) {
            localStorage.user_nick_name = value;
        },
        /**
         * 取缓存NickName
         * @returns {string} NickName
         */
        getNickName: function () {
            return localStorage.user_nick_name;
        },
        /**
         * 缓存用户头像
         * @param {string} value 用户头像
         * @returns {void} 
         */
        setAvatarUrl: function (value) {
            localStorage.user_avatar_url = value;
        },
        /**
         * 取缓存用户头像
         * @returns {object}  用户头像
         */
        getAvatarUrl: function () {
            return localStorage.user_avatar_url;
        },
        /**
         * 取得登录用户信息
         * @returns {object}  登录用户信息
         */
        info: function () {
            return {
                userId: localStorage.user_open_id,
                phone: localStorage.user_phone,
                nickName: localStorage.user_nick_name,
                organization: localStorage.organization,
                avatarUrl: localStorage.user_avatar_url,
                expiresIn: localStorage.user_expires_in
            };
        },
        /**
         * 取得用户组织ID
         * @returns {int}  用户ID
         */
        organizationId: function () {
            return localStorage.user_organization_id;
        },
        /**
         * 取得用户ID
         * @returns {int}  用户ID
         */
        openId: function () {
            return localStorage.user_open_id;
        },
        goLogin(win) {
            if (win.top !== win.self) {
                if (window.top.location.host) {
                    return this.goLogin(win.parent);
                }
            }
            if (win.location.href != "/login.html")
                win.location.href = "/login.html";
        },
        showLogout() {
            var that = this;
            common.confirm("登录过期", "当前登录已经过期,需要重新登录,是否继续?", () => {
                that.logout(false);
            });
        },
        /**
         * 登录状态检查
         * @returns {void}  
         */
        isLogin: function () {
            return localStorage.getItem("user_access_token");
        },
        logout: function (inLogout) {
            if (this.getAccessToken()) {
                silentCall("退出登录", "/Authority/v1/login/logout", null, res => {
                    for (var i = localStorage.length - 1; i >= 0; i--) {
                        var key = localStorage.key(i);
                        if (key.length > 5 && key.substr(0, 5) === "user_") {
                            localStorage.removeItem(key);
                        }
                    }
                    localStorage.removeItem('user_access_token');
                    localStorage.removeItem('user_refresh_token');
                    this.goLogin(window);
                });
            }
            else if (!inLogout)
                this.goLogin(window);
        },
        inRefreshDid: false,
        refreshDidCallback: [],
        refreshDid(callback) {
            var that = this;
            that.refreshDidCallback.push(callback);
            if (that.inRefreshDid) {
                return;
            }
            that.inRefreshDid = true;
            console.log("更新设备标识...");
            ajax_ex.doAjax("更新设备标识", "Authority/v1/did/refresh", {
                appId: globalOptions.appId,
                did: that.getDeviceToken()
            }, function (r) {
                var calls = that.refreshDidCallback;
                that.refreshDidCallback = [];
                that.inRefreshDid = false;
                if (r && r.success) {
                    that.setDeviceToken(r.data);
                    for (var idx = 0; idx < calls.length; idx++) {
                        if (calls[idx])
                            calls[idx]();
                    }
                    return;
                }
                localStorage.removeItem('device_token');
            }, function () {
                that.inRefreshDid = false;
            });
        },
        inRefreshAt: false,
        refreshAtCallback: [],
        refreshAt(callback) {
            var that = this;
            that.refreshAtCallback.push(callback);
            if (that.inRefreshAt) {
                return;
            }
            that.inRefreshAt = true;
            console.log(`刷新令牌${that.getAccessToken()}`);
            ajax_post("刷新令牌", "/Authority/v1/at/refresh", {
                AccessToken: that.getAccessToken(),
                RefreshToken: localStorage.user_refresh_token
            }, function (res) {
                var calls = that.refreshAtCallback;
                that.refreshAtCallback = [];
                that.inRefreshAt = false;
                if (res && res.success) {
                    localStorage.user_access_token = res.data.accessToken;
                    localStorage.user_refresh_token = res.data.refreshToken;
                    for (var idx = 0; idx < calls.length; idx++) {
                        if (calls[idx])
                            calls[idx]();
                    }
                    return;
                }
                that.logout();
            }, function () {
                that.inRefreshAt = false;
                that.logout();
            });
        }
    }
};
//公共对话框
var common = {
    /**
     * 用户确认
     * @param {string} title 标题
     * @param {string} message 消息
     * @param {Function} callback 确认后的回调
     * @returns {void}
     */
    confirm(title, message, callback) {
        vueObject.$confirm(message, title, {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning'
        }).then(() => {
            callback();
        });
    },
    /**
     * 显示提示
     * @param {string} title 标题
     * @param {string} message 消息
     * @returns {void} 
     */
    showTip(title, message) {
        vueObject.$notify({
            title: title,
            message: message,
            duration: 0
        });
    },
    /**
     * 显示提示
     * @param {string} title 标题
     * @param {string} message 消息
     * @param {string} type 类型
     * @returns {void} 
     */
    showMessage(title, message, type) {
        if (!type) {
            type = 'success';
        }
        vueObject.$notify({
            title: title,
            message: message,
            type: type,
            duration: 2000
        });
    },
    /**
     * 显示提示
     * @param {string} title 标题
     * @param {string} message 消息
     * @param {string} type 类型
     * @returns {void} 
     */
    showError(title, message) {
        this.showMessage(title, message, 'error');
    },
    /**
     * 显示提示
     * @param {object} res 返回值 标题
     * @returns {void} 
     */
    showStatus(res) {
        if (res.success) {
            this.showMessage(null, res.status && res.status.msg
                ? res.status.msg
                : "请求成功");
        }
        else {
            this.showMessage(null, res.status && res.status.msg
                ? res.status.msg
                : "网络错误", "error");
        }
    },
    loading: null,
    busyNum: 0,
    isBusy: false,
    showBusy(title) {
        if (this.isBusy || this.loading) {
            ++this.busyNum;
            return;
        }
        this.busyNum = 1;
        this.isBusy = true;
        var that = this;
        setTimeout(function () {
            if (!that.isBusy || this.busyNum === 0)
                return;
            that.loading = vueObject.$loading({
                lock: true,
                text: `【${title}】...`,
                spinner: 'el-icon-loading',
                background: 'rgba(0, 0, 0, 0.7)'
            });
            if (!that.isBusy || this.busyNum === 0) {
                this.loading.close();
                this.loading = null;
            }
        }, 300);
    },
    hideBusy() {
        if (--this.busyNum === 0) {
            this.isBusy = false;
            if (this.loading) {
                this.loading.close();
                this.loading = null;
            }
        }
    }
};

/**
 * 通过数据取可读文本
 * @param {object} val 值
 * @param {object} array 键值对应的数组
 * @param {string} err 未命中的文本
 * @returns {string} 可读文本
 */
function arrayFormat(val, array, err) {
    if (!val)
        val = "0";
    else if (typeof val !== "string")
        val = val.toString();
    for (var i = 0; i < array.length; i++) {
        if (array[i].value.toString() === val)
            return array[i].text;
    }
    return err ? "错误" + val : err;
}


/**
 * 截断文本空白
 * @param {string} str  文本
 * @returns {string} 处理后文本
 */
function doTrim(str) {
    if (!str)
        return null;
    if (typeof str !== "string")
        return str.toString();
    str = str.trim();
    return !str ? null : str;
}
/**
 * 获取上一个月
 *
 * @date 格式为yyyy-mm-dd的日期，如：2014-01-25
 */
function getPreMonth() {

    let today = new Date();
    var year = today.getFullYear(); //获取当前日期中月的天数
    var month = today.getMonth();
    if (month == 0) {
        year = parseInt(year) - 1;
        month = 11;
    }
    return new Date(year, month).format('yyyy-MM-dd');
}


/**
 * 转转为日期对象
 * @param {obj} dt   文本
 * @returns {string} 处理后文本
 */
function NewDate(dt) {
    if (dt instanceof Date)
        return dt;
    if (!dt)
        return new Date();
    var date = new Date();
    var str = dt.toString();
    if (!str || str.indexOf("0001-01-01T00:00:00") === 0)
        return date;
    var stra = str.split("T");
    if (stra.length < 2)
        return date;
    var ds = stra[0].split("-");
    if (parseInt(ds[0]) <= 1)
        return date;
    date.setUTCFullYear(ds[0], ds[1] - 1, ds[2]);
    var ts = stra[1].split(":");
    var ms = ts[2] ? ts[2].split(".") : [0];
    date.setHours(parseInt(ts[0]), parseInt(ts[1]), parseInt(ms[0]), ms.length > 1 ? parseInt(ms[1]) : 0);
    return date;
}

/**
 * 日期格式化扩展
 * @param {obj} fmt 格式定义
 * @returns {string} 处理后文本
 */
// ReSharper disable once NativeTypePrototypeExtending
Date.prototype.format = function (fmt) { //author: meizz 
    var year = this.getFullYear();
    if (!year || year <= 100)
        return null;
    var date = this;
    var o = {
        "M+": date.getMonth() + 1,                 //月份   
        "d+": date.getDate(),                    //日   
        "h+": date.getHours(),                   //小时   
        "m+": date.getMinutes(),                 //分   
        "s+": date.getSeconds(),                 //秒   
        "q+": Math.floor((date.getMonth() + 3) / 3), //季度   
        "S": date.getMilliseconds()             //毫秒   
    };

    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
};

/**
 * 中文金额转换
 * @param {obj} n 金额
 * @returns {string} 处理后文本
 */
function ChinessMoneyInner(n) {
    var unit = "仟佰拾万仟佰拾亿仟佰拾万仟佰拾元角分", str = "";
    n += "00";
    var p = n.indexOf(".");
    if (p >= 0)
        n = n.substring(0, p) + n.substr(p + 1, 2);
    unit = unit.substr(unit.length - n.length);
    for (var i = 0; i < n.length; i++)
        str += "零壹贰叁肆伍陆柒捌玖".charAt(n.charAt(i)) + unit.charAt(i);
    return str.replace(/零(仟|佰|拾|角)/g, "零").replace(/(零)+/g, "零").replace(/零(万|亿|元)/g, "$1").replace(/(亿)万|壹(拾)/g, "$1$2").replace(/^元零?|零分/g, "").replace(/元$/g, "元整");
}
/**
 * 中文金额转换
 * @param {obj} n 金额
 * @returns {string} 处理后文本
 */
function ChinessMoney(n) {
    if (!/^(0|[1-9]\d*)(\.\d+)?$/.test(n))
        return "零元";
    if (n <= 0)
        return "零元";
    return ChinessMoneyInner(n);
}
/**
 * 中文金额转换
 * @param {obj} n 金额
 * @returns {string} 处理后文本
 */
function ChinessMoney2(n) {
    if (!/^(0|[1-9]\d*)(\.\d+)?$/.test(n))
        return "零元";
    if (n <= 0)
        return "零元";
    return ChinessMoneyInner(n) + "(￥" + toThousands(n) + ")";
}

/**
 * 千分符数字表示法
 * @param {num} num 数字
 * @returns {string} 格式化内容
 */
function toThousands(num) {
    if (!num)
        return "0.00";
    num = num.toString();
    var result;
    var p = num.indexOf(".");
    if (p >= 0) {
        result = num.substr(p, num.length - p);
        num = num.substring(0, p);
    } else {
        result = ".00";
    }
    while (num.length > 3) {
        result = "," + num.slice(-3) + result;
        num = num.slice(0, num.length - 3);
    }
    if (num) {
        result = num + result;
    }
    return result;
}

/**
 * 千分符数字表示法
 * @param {num} num 数字
 * @returns {string} 格式化内容
 */
function toThousandsInt(num) {
    if (!num)
        return "0";
    num = num.toString();
    var result;
    var p = num.indexOf(".");
    if (p >= 0) {
        result = num.substr(p, num.length - p);
        num = num.substring(0, p);
    } else {
        result = "";
    }
    while (num.length > 3) {
        result = "," + num.slice(-3) + result;
        num = num.slice(0, num.length - 3);
    }
    if (num) {
        result = num + result;
    }
    return result;
}


function thousandsFormat(value) {
    if (!value)
        return ""; //"0.00";
    var txt = value.toString();
    var len = txt.length;
    var idx = txt.indexOf(".");
    if (idx <= 0) {
        txt += ".00";
        idx = len;
    } else if (len === idx + 1)
        txt += "00"; //补小数点后面两个0
    else if (len === idx + 2)
        txt += "0"; //补小数点后面一个0
    return toThousandsInt(txt.slice(0, idx)) + txt.slice(idx, idx + 3);
}

function moneyFormat(value) {
    return thousandsFormat(value);
}

function inputMoneyFormat(value) {
    if (!value)
        return "0.00";
    var txt = value.toString();
    var len = txt.length;
    var idx = txt.indexOf(".");
    if (idx < 0)
        return txt + ".00";
    if (len === idx + 1)
        return txt + "00";
    if (len === idx + 2)
        return txt + "0";
    if (len === idx + 3)
        return txt;
    return txt.substr(0, txt.indexOf(".") + 3);
}

function dateFormat(value) {
    if (!value || value === "0001-01-01T00:00:00" || value === "1900-01-01T00:00:00")
        return "";
    var date = NewDate(value);
    if (!date)
        return "";
    return date.format("yyyy-MM-dd");
}

function dateTimeFormat(value) {
    if (!value || value === "0001-01-01T00:00:00" || value === "1900-01-01T00:00:00")
        return "";
    var date = NewDate(value);
    if (!date)
        return "";
    return date.format("MM-dd hh:mm:ss");
}

function percentFormat(value) {
    var n = parseFloat(value);
    if (isNaN(n))
        return value;
    if (n === 0)
        return "-";
    return (Math.round(n * 10000) / 100).toFixed(2) + "%";
}
function percentFormat2(value) {
    var n = parseFloat(value);
    if (isNaN(n))
        return value;
    if (n === 0)
        return "-";
    return value + "%";
}
//去左右空格;
function trimStr(str, whitespace) {
    if (!str || str.length < 1)
        return '';

    while (whitespace.indexOf(str[0]) !== -1) {
        if (str.length === 1)
            return '';
        str = str.substring(1);
    }
    while (whitespace.indexOf(str.length - 1) !== -1) {
        if (str.length === 1)
            return '';
        str = str.substring(0, str.length - 1);
    }
    return str;
}
/**
 * 附件枚举类型
 */
var annexType = [
    { value: "0", text: "未知" },
    { value: "1", text: "Word文档" },
    { value: "2", text: "Excel文档" },
    { value: "3", text: "PDF文档" },
    { value: "4", text: "声音文件" },
    { value: "5", text: "视频文件" },
    { value: "6", text: "图片文件" },
    { value: "7", text: "PPT文件" },
    { value: "8", text: "WPS文件" },
    { value: "9", text: "文本文件" }
];

/**
 * 附件枚举类型 格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function annexTypeFormat(value) {
    return arrayFormat(value, annexType);
}


/**
 * 用户任务分类枚举类型
 */
var userJobType = [
    { value: "0", text: "未指定" },
    { value: "1", text: "编辑任务" },
    { value: "2", text: "审核任务" },
    { value: "3", text: "数据维护" },
    { value: "4", text: "其它命令" }
];

/**
 * 用户任务分类 格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function userJobTypeFormat(value) {
    return arrayFormat(value, userJobType);
}


/**
 * 节点类型枚举类型
 */
var itemType = [
    { value: "0", text: "顶级目录" },
    { value: "1", text: "普通目录" },
    { value: "2", text: "页面" },
    { value: "3", text: "按钮" },
    { value: "4", text: "API" }
];

/**
 * 节点类型 格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function itemTypeFormat(value) {
    return arrayFormat(value, itemType);
}

/**
 * 权限枚举类型
 */
var rolePowerType = [
    { value: "0", text: "未设置" },
    { value: "1", text: "允许" },
    { value: "2", text: "拒绝" }
];

/**
 * 权限枚举类型 格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function rolePowerTypeFormat(value) {
    return arrayFormat(value, rolePowerType);
}

/**
 * 机构类型
 */
var organizationType = [
    { value: "0", text: "-" },
    { value: "1", text: "集团" },
    { value: "2", text: "公司" },
    { value: "3", text: "部门" }
];

/**
 * 机构类型之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function organizationTypeFormat(value) {
    return arrayFormat(value, organizationType);
}


/**
 * 用户认证类型
 */
var authorizeType = [
    { value: "0", text: "无" },
    { value: "1", text: "手机认证" },
    { value: "2", text: "账户认证" },
    { value: "4", text: "微信认证" },
    { value: "8", text: "微博认证" },
    { value: "16", text: "身份证" },
    { value: "32", text: "活体认证" }
];

/**
 * 用户认证类型 格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function authorizeTypeFormat(value) {
    return arrayFormat(value, authorizeType);
}

/**
 * 性别
 */
var sexType = [
    { value: "0", text: "-" },
    { value: "1", text: "女" },
    { value: "2", text: "男" }
];

/**
 * 性别之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function sexTypeFormat(value) {
    return arrayFormat(value, sexType);
}

/**
 * 性别 格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function sexFormat(value) {
    return arrayFormat(value, sexType);
}

/**
 * 用户状态
 */
var userStatusType = [
    { value: "0", text: "访客" },
    { value: "1", text: "注册用户" },
    { value: "2", text: "黑名单" }
];

/**
 * 用户状态 格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function userStatusTypeFormat(value) {
    return arrayFormat(value, userStatusType);
}

/**
 * 星期几
 */
var weekDayType = [
    { value: "1", text: "星期一" },
    { value: "2", text: "星期二" },
    { value: "3", text: "星期三" },
    { value: "4", text: "星期四" },
    { value: "5", text: "星期五" },
    { value: "6", text: "星期六" },
    { value: "7", text: "星期天" }
];
/**
 * 星期几格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function weekDayTypeFormat(value) {
    return arrayFormat(value, weekDayType);
}

/**
 * 证件类型
 */
var certificateType2 = [
    { value: "0", text: "-" },
    { value: "1", text: "身份证" },
    { value: "2", text: "驾驶证" },
    { value: "3", text: "军官证" },
    { value: "4", text: "护照" },
    { value: "5", text: "营业执照" },
    { value: "6", text: "其它证件" }
];
/**
 * 证件类型格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function certificateTypeFormat(value) {
    return arrayFormat(value, certificateType2);
}

function boolFormat(val) {
    if (val)
        return "是";
    else
        return "否";
}
/**
 * 是否的类型
 */
var yesnoType = [
    { text: "是", value: true },
    { text: "否", value: false }
];
/**
 * 是否格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function yesnoFormat(value) {
    if (typeof value === 'undefined')
        value = false;
    return arrayFormat(value, yesnoType);
}

/**
 * 是否的类型
 */
var haseType = [
    { text: "有", value: true },
    { text: "无", value: false }
];
/**
 * 有无格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function haseFormat(value) {
    if (typeof value === 'undefined')
        value = false;
    return arrayFormat(value, haseType);
}
/**
 * 是否的类型
 */
var okType = [
    { text: "★", value: true },
    { text: "☆", value: false }
];
/**
 * 是否 格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function okFormat(value) {
    if (typeof value === 'undefined')
        value = false;
    return arrayFormat(value, okType);
}
/**
 * 拒绝/允许的类型
 */
var canType = [
    { text: "允许", value: true },
    { text: "拒绝", value: false }
];

/**
 * 拒绝/允许格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function canFormat(value) {
    if (typeof value === 'undefined')
        value = false;
    return arrayFormat(value, canType);
}

/**
 * 行级权限类型
 */
var subjectionType = [
    { value: "0", text: "没有任何权限制" },
    { value: "1", text: "仅限本人的数据" },
    { value: "2", text: "本部门的数据" },
    { value: "3", text: "本部门及下级的数据" },
    { value: "4", text: "本区域的数据" },
    { value: "5", text: "本区域及下级区域与部门的数据" },
    { value: "6", text: "自定义" }
];

/**
 * 行级权限类型 格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function subjectionTypeFormat(value) {
    return arrayFormat(value, subjectionType);
}


/**
 * 行级权限类型
 */
var dataScopeType = [
    { value: "0", text: "-" },
    { text: "无限制", value: "0" },
    { text: "本人", value: "1" },
    { text: "本区域", value: "4" },
    { text: "本区域及下级", value: "5" }
];

/**
 * 命令类型枚举类型
 */
var jobCommandType = [
    { value: "0", text: "未指定" },
    { value: "1", text: "新增" },
    { value: "2", text: "编辑" },
    { value: "3", text: "阅读" },
    { value: "4", text: "校验" },
    { value: "5", text: "提交" },
    { value: "6", text: "删除" },
    { value: "7", text: "启用" },
    { value: "8", text: "禁用" },
    { value: "9", text: "废弃" },
    { value: "10", text: "还原" },
    { value: "11", text: "审批" },
    { value: "12", text: "退回" },
    { value: "13", text: "通过" },
    { value: "14", text: "否决" },
    { value: "15", text: "归档" },
    { value: "16", text: "其它" }
];

/**
 * 命令类型枚举类型之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function jobCommandTypeFormat(value) {
    return arrayFormat(value, jobCommandType);
}

/**
 * 工作内容状态枚举类型
 */
var jobStatusType = [
    { value: "0", text: "未开始" },
    { value: "1", text: "已发出" },
    { value: "2", text: "已接受" },
    { value: "3", text: "挂起" },
    { value: "16", text: "完成" },
    { value: "17", text: "失败" },
    { value: "18", text: "未命中" }
];

/**
 * 工作内容状态枚举类型之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function jobStatusTypeFormat(value) {
    return arrayFormat(value, jobStatusType);
}


/**
 * 数据状态
 */
var dataStateType = [
    { value: 0x100, text: "-" },
    { value: "0", text: "草稿" },
    { value: "1", text: "启用" },
    { value: "2", text: "停用" },
    { value: 0xE.toString(), text: "查看" },
    { value: 0xF.toString(), text: "锁定" },
    { value: 0x10.toString(), text: "废弃" },
    { value: 0xFF.toString(), text: "删除" }
];
/**
 * 数据状态
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function dataStateFormat(value) {
    return arrayFormat(value, dataStateType);
}

/**
 * 审核状态
 */
var auditType = [
    { value: 0x100.toString(), text: "-" },
    { value: "0", text: "草稿" },
    { value: "1", text: "重做" },
    { value: "2", text: "提交" },
    { value: "3", text: "否决" },
    { value: "4", text: "通过" },
    { value: "5", text: "结束" },
    { value: 0x10.toString(), text: "废弃" },
    { value: 0x11.toString(), text: "未审核" },
    { value: 0x12.toString(), text: "未结束" },
    { value: 0x13.toString(), text: "停用" },
    { value: 0xFF.toString(), text: "删除" }
];

/**
 * 审核状态
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function auditFormat(value) {
    return arrayFormat(value, auditType);
}

function auditIconFormat(value, row) {

    switch (row.AuditState) {
        case 0:
            return dataStateIcon(value, row);
        case 1:
            return dataStateIcon(value, row, "icon_a_again");
        case 2:
            return iconCell("icon_a_submit", 16);
        case 3:
            return iconCell("icon_a_deny", 16);
        case 4:
            return dataStateIcon(value, row, "icon_a_pass");
        case 5:
            return iconCell("icon_a_end", 16);
    }
    if (row.IsFreeze) {
        return iconCell("icon_a_end", 16);
    }
    return iconCell("icon-cus", 16, row._info_);
}
function dataStateIconFormat(value, row) {
    return dataStateIcon(value, row, null);
}

function dataStateIcon(value, row, ena) {
    switch (row.dataState) {
        case 0:
            return iconCell("icon_a_none", 16, row._info_);
        case 1:
            return iconCell(ena ? ena : "icon-enable", 16, row._info_);
        case 2:
            return iconCell("icon-disable", 16, row._info_);
        case 0xE:
            return iconCell("icon-cus", 16, row._info_);
        case 0xF:
            return iconCell("icon_a_end", 16, row._info_);
        case 0x10:
            return iconCell("icon-discard", 16, row._info_);
        case 255:
            return iconCell("icon-delete", 16, row._info_);
    }
    if (row.IsFreeze) {
        return iconCell("icon_a_end", 16, row._info_);
    }
    return iconCell("icon-cus", 16, row._info_);
}

function jsonHighlight(json) {
    try {
        if (!json)
            return '&nbsp';
        if (json[0] != '{')
            return json;
        json = json.replace(/&/g, '&').replace(/</g, '<').replace(/>/g, '>').replace(/\}/g, '<br/>}');//.replace(/\{/g, '<br/>{').replace(/[\,]/g, ',<br/>')
        return json.replace(/("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g, function (match) {
            var cls = 'number';
            if (/^"/.test(match)) {
                if (/:$/.test(match)) {
                    cls = 'key';
                } else {
                    cls = 'string';
                }
            } else if (/true|false/.test(match)) {
                cls = 'boolean';
            } else if (/null/.test(match)) {
                cls = 'null';
            }
            return (cls == 'key' ? '<br/>' : '') + '<span class="' + cls + '">' + match + '</span>';
        });
    } catch (e) {
        return json;
    }
}
/** 操作状态码
*/
var OperatorStatusCode = {
    /// <summary>
    ///     正在排队
    /// </summary>
    Queue: 1,

    /// <summary>
    ///     正确
    /// </summary>
    Success: 0,

    /// <summary>
    ///     参数错误
    /// </summary>
    ArgumentError: -1,

    /// <summary>
    ///     发生处理业务错误
    /// </summary>
    BusinessError: -2,

    /// <summary>
    ///     发生未处理业务异常
    /// </summary>
    BusinessException: -3,

    /// <summary>
    ///     发生未处理系统异常
    /// </summary>
    UnhandleException: -4,

    /// <summary>
    ///     网络错误
    /// </summary>
    NetworkError: -5,

    /// <summary>
    ///     执行超时
    /// </summary>
    TimeOut: -6,

    /// <summary>
    ///     拒绝访问
    /// </summary>
    DenyAccess: -7,

    /// <summary>
    ///     未知的Token
    /// </summary>
    TokenUnknow: -8,

    /// <summary>
    ///     令牌过期
    /// </summary>
    TokenTimeOut: -9,

    /// <summary>
    ///     系统未就绪
    /// </summary>
    NoReady: -0xA,

    /// <summary>
    ///     异常中止
    /// </summary>
    Ignore: -0xB,

    /// <summary>
    ///     重试
    /// </summary>
    ReTry: -0xC,

    /// <summary>
    ///     方法不存在
    /// </summary>
    NoFind: -0xD,

    /// <summary>
    ///     服务不可用
    /// </summary>
    Unavailable: -0xE,

    /// <summary>
    ///     未知结果
    /// </summary>
    Unknow: 0xF
};