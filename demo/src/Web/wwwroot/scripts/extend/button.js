
function createButtonByTitle(title, btn, icon, func) {
    if (func) {
        $(btn).linkbutton({
            text: title,
            plain: true,
            iconCls: icon,
            onClick: func
        });
    } else {
        $(btn).linkbutton({
            text: title,
            plain: true,
            iconCls: icon
        });
    }
    return true;
}


function createButton(btn, icon, func) {
    if (func) {
        $(btn).linkbutton({
            plain: true,
            iconCls: icon,
            onClick: func
        });
    } else {
        $(btn).linkbutton({
            plain: true,
            iconCls: icon
        });
    }
    return true;
}

function showButton(name) {
    if (!mainPageOptions.allButton && mainPageOptions.userButtons.indexOf(name) < 0) {
        return;
    } else {
        $(name).show();
    }
}

function hideButton(name) {
    if (!mainPageOptions.allButton && mainPageOptions.userButtons.indexOf(name) < 0) {
        return;
    } else {
        $(name).hide();
    }
}

function disableButton(name) {
    try {
        if (!mainPageOptions.allButton && mainPageOptions.userButtons.indexOf(name) < 0) {
            return;
        } else {
            $(name).linkbutton("disable");
        }
    } catch (e) {
        console.error("%s.%s() ： %s", typeof (this), "enableButton", e);
    }
}
function enable(name) {
    $(name).removeAttr("disabled");//要变成Enable，JQuery只能这么写  
}
function disable(name) {
    $(name).attr("disabled", "disabled");//再改成disabled  
}
function enableButton(name) {
    try {
        if (!mainPageOptions.allButton && mainPageOptions.userButtons.indexOf(name) < 0) {
            return;
        } else {
            $(name).linkbutton("enable");
        }
    } catch (e) {
        console.error("%s.%s() ： %s", typeof (this), "enableButton", e);
    }
}

/**
 * 生成权限相关的铵钮:要显示或隐藏的节点名称与按钮名称不一致时使用
 * @param {string} title 按钮标题
 * @param {string} action 动作名称（因特殊处理原因，要加#号）
 * @param {string} region 要显示或隐藏的节点名称 
 * @param {string} btn 按钮名称
 * @param {string} icon 按钮图标
 * @param {function} func 按钮点击处理方法
 * @returns {void} 
 */
function createRegionButton(title, region, action, btn, icon, func) {
    if (!mainPageOptions.checkRole(title, region, action)) {
        $(region).hide();
        return false;
    }
    return createButton(btn, icon, func);
}

/**
 * 生成权限相关的铵钮:要显示或隐藏的节点名称与按钮名称相同时使用
 * @param {string} title 按钮标题
 * @param {string} action 动作名称（因特殊处理原因，要加#号）
 * @param {string} btn 按钮名称
 * @param {string} icon 按钮图标
 * @param {function} func 按钮点击处理方法
 * @returns {void} 
 */
function createRoleButton(title, action, btn, icon, func) {
    if (!mainPageOptions.checkRole(title, btn, action)) {
        $(btn).hide();
        return false;
    }
    return createButtonByTitle(title, btn, icon, func);
}
/**
 * 生成权限相关的铵钮:按钮名称与权限名称不相同的（即权限中的按钮名称是动作的）
 * @param {string} title 按钮标题
 * @param {string} action 动作名称（因特殊处理原因，要加#号）
 * @param {string} region 要显示或隐藏的节点名称 
 * @param {string} btn 按钮名称
 * @param {string} icon 按钮图标
 * @param {function} func 按钮点击处理方法
 * @returns {void} 
 */
function createButtonByAction(title, action, region, btn, icon, func) {
    if (!mainPageOptions.checkRole(title, region, action)) {
        $(btn).hide();
        return false;
    }
    return createButtonByTitle(title, btn, icon, func);
}
/**
 * 生成权限相关的铵钮:没有权限时不隐藏按钮
 * @param {string} title 按钮标题
 * @param {string} action 动作名称（因特殊处理原因，要加#号）
 * @param {string} btn 按钮名称
 * @param {string} icon 按钮图标
 * @param {function} func 按钮点击处理方法
 * @returns {void} 
 */
function createRoleButtonNoHide(title, action, btn, icon, func) {
    if (!mainPageOptions.checkRole(title, btn, action)) {
        return false;
    }
    return createButtonByTitle(title, btn, icon, func);
}

/**
 * 生成权限相关的铵钮:无后台动作时使用（如页面跳转）
 * @param {string} title 按钮标题
 * @param {string} btn 按钮名称
 * @param {string} icon 按钮图标
 * @param {function} func 按钮点击处理方法
 * @returns {void} 
 */
function createSimpleRoleButton(title, btn, icon, func) {
    if (!mainPageOptions.checkRole(title, btn)) {
        $(btn).hide();
        return false;
    }
    return createButtonByTitle(title, btn, icon, func);
}
