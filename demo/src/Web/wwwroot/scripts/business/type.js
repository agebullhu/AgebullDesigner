
/**
 * 处方类型
 * @param {string} value 内容
 * @returns {string} 返回值
 */
var prescriptionType = [
    { value: "", text: "-" },
    { value: "0", text: "未确定" },
    { value: "1", text: "检查" },
    { value: "2", text: "外带药品" },
    { value: "3", text: "输液" },
    { value: "4", text: "院内治疗" },
    { value: "5", text: "住院" }
];

/**
 * 处方类型之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function prescriptionTypeFormat(value) {
    return arrayFormat(value, prescriptionType);
}

/**
 * 挂号类型
 */
var isPreType = [
    { value: "", text: "-" },
    { value: "0", text: "全部" },
    { value: "1", text: "当日挂号" },
    { value: "2", text: "预约挂号" }
];

/**
 * 挂号类型之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function isPreTypeFormat(value) {
    return arrayFormat(value, isPreType);
}


/**
 * 挂号类型
 */
var regType = [
    { value: "", text: "-" },
    { value: "QB", text: "全部" },
    { value: "PT", text: "普通门诊" },
    { value: "ZJ", text: "专家门诊" }
];

/**
 * 挂号类型之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function regTypeFormat(value) {
    return arrayFormat(value, regType);
}


/**
 * 午别
 */
var noonCode = [
    { value: "0", text: "-" },
    { value: "1", text: "上午" },
    { value: "2", text: "下午" },
    { value: "3", text: "全天" }
];

/**
 * 午别之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function noonCodeFormat(value) {
    return arrayFormat(value, noonCode);
}


/**
 * 挂号状态
 */
var regStatus = [
    { value: "0", text: "-" },
    { value: "1", text: "已支付" },
    { value: "2", text: "未支付" },
    { value: "3", text: "已取消" },
    { value: "4", text: "已失效" }
];

/**
 * 挂号状态之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function regStatusFormat(value) {
    return arrayFormat(value, regStatus);
}


/**
 * 挂号类型
 */
var registType = [
    { value: "0", text: "-" },
    { value: "1", text: "挂号" },
    { value: "2", text: "预约挂号" }
];

/**
 * 挂号类型之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function registTypeFormat(value) {
    return arrayFormat(value, registType);
}


/**
 * 账单来源
 */
var billSourceType = [
    { value: "0", text: "-" },
    { value: "1", text: "挂号" },
    { value: "2", text: "处方" },
    { value: "3", text: "其它" }
];

/**
 * 账单来源之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function billSourceTypeFormat(value) {
    return arrayFormat(value, billSourceType);
}


/**
 * 挂号状态
 */
var regStatusType = [
    { value: "0", text: "-" },
    { value: "1", text: "已支付" },
    { value: "2", text: "未支付" },
    { value: "3", text: "已取消" },
    { value: "4", text: "已失效" }
];

/**
 * 挂号状态之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function regStatusTypeFormat(value) {
    return arrayFormat(value, regStatusType);
}

/**
 * 挂号状态
 */
var payStatusType = [
    { value: "0", text: "-" },
    { value: "1", text: "已支付" },
    { value: "2", text: "未支付" },
    { value: "3", text: "已取消" },
    { value: "4", text: "已失效" }
];
/**
 * 挂号状态之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function payStatusTypeFormat(value) {
    return arrayFormat(value, payStatusType);
}

/**
 * 排队状态类型
 */
var queueStateType = [
    { value: "0", text: "未开始" },
    { value: "1", text: "排队" },
    { value: "2", text: "到号" },
    { value: "3", text: "检查中" },
    { value: "4", text: "结束看诊" },
    { value: "5", text: "取消" },
    { value: "6", text: "过号" }
];

/**
 * 排队状态类型之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function queueStateTypeFormat(value) {
    return arrayFormat(value, queueStateType);
}


/**
 * 检查类型
 */
var checkType = [
    { value: "1", text: "检验" },
    { value: "2", text: "检查" }
];

/**
 * 检查类型之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function checkTypeFormat(value) {
    return arrayFormat(value, checkType);
}


/**
 * 检查状态
 */
var checkStatus = [
    { value: "0", text: "检查中" },
    { value: "1", text: "检查完成" }
];

/**
 * 检查状态之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function checkStatusFormat(value) {
    return arrayFormat(value, checkStatus);
}


/**
 * 机构类型
 */
var organizationType = [
    { value: 0, text: '-' },
    { value: 1, text: '行政区域' },
    { value: 2, text: '机构' },
    { value: 3, text: '部门' }
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
 * 应用归类类型
 */
var classifyType = [
    { value: 0, text: '无权限' },
    { value: 1, text: '后台管理' },
    { value: 2, text: '线上应用' }
];

/**
 * 应用归类类型之表格格式化方法
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function classifyTypeFormat(value) {
    return arrayFormat(value, classifyType);
}

