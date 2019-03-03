
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
    else
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
    if (str == null)
        return null;
    str = str.trim();
    return str == "" ? null : str;
}

/**
 * 转转为日期对象
 * @param {obj} str   文本
 * @returns {string} 处理后文本
 */
function NewDate(str) {
    var date = new Date();
    if (!str || str === "0001-01-01T00:00:00")
        return date;
    var stra = str.split("T");
    if (stra.length < 2)
        return date;
    var ds = stra[0].split("-");
    if (parseInt(ds[0]) <= 1)
        return date;
    date.setUTCFullYear(ds[0], ds[1] - 1, ds[2]);
    var ts = stra[1].split(":");
    var ms = ts[2] ? [0] : ts[2].split(".");
    date.setHours(parseInt(ts[0]), parseInt(ts[1]), parseInt(ms[0]), ms.length > 1 ? parseInt(ms[1]) : 0);
    return date;
}

/**
 * 日期格式化扩展
 * @param {obj} fmt 格式定义
 * @returns {string} 处理后文本
 */
Date.prototype.format = function (fmt) { //author: meizz 
    var year = this.getFullYear();
    if (!year || year <= 1900)
        return "";
    if (!this.fmta)
        this.fmta = {
            "M+": this.getMonth() + 1, //月份 
            "d+": this.getDate(), //日 
            "h+": this.getHours(), //小时 
            "m+": this.getMinutes(), //分 
            "s+": this.getSeconds(), //秒 
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
            "S": this.getMilliseconds() //毫秒 
        };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (year + "").substr(4 - RegExp.$1.length));
    for (var k in this.fmta) {
        if (this.fmta.hasOwnProperty(k)) {
            if (new RegExp("(" + k + ")").test(fmt)) {
                fmt = fmt.replace(RegExp.$1,
                    (RegExp.$1.length == 1)
                    ? (this.fmta[k])
                    : (("00" + this.fmta[k]).substr(("" + this.fmta[k]).length)));
            }
        }
    }
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
    if (len == idx + 1)
        return txt + "00";
    if (len == idx + 2)
        return txt + "0";
    if (len == idx + 3)
        return txt;
    return txt.substr(0, txt.indexOf(".") + 3);
}

function dateFormat(value) {
    if (!value || value == "0001-01-01T00:00:00" || value == "1900-01-01T00:00:00")
        return "";
    var date = NewDate(value);
    if (!date)
        return "";
    return date.format("yyyy-MM-dd");
}

function dateTimeFormat(value) {
    if (!value || value == "0001-01-01T00:00:00" || value == "1900-01-01T00:00:00")
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
