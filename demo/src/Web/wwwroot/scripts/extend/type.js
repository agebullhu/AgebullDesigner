
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
    { value: "0", text: "顶级" },
    { value: "1", text: "文件夹" },
    { value: "2", text: "页面" },
    { value: "3", text: "按钮" },
    { value: "4", text: "动作" }
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
    { value: "1", text: "集团或医联体" },
    { value: "2", text: "公司或医院" },
    { value: "3", text: "部门或科室" }
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
    return arrayFormat(value, haseType);
}
/**
 * 是否的类型
 */
var okType = [
    { text: "★", value: "1" },
    { text: "-", value: "0" }
];
/**
 * 是否 格式化
 * @param {string} value 内容
 * @returns {string} 返回值
 */
function okFormat(value) {
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
    return arrayFormat(value, canType);
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

