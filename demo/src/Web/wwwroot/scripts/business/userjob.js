/*design by:agebull designer date:2017/6/12 2:26:47*/
/*
    提交的通用操作界面
*/
var SubmitJob = {
    /**
     * 命令执行地址前缀
     */
    cmdPath: "/Workflow/UserJob/Action.aspx",
    /**
     * 表单地址
     */
    formUrl: "/Workflow/UserJob/Form.aspx",
    /*
        录入界面载入时执行控件初始化
    */
    onFormUiLoaded: function () {
        $("#Title").textbox({ required: true, validType: ["strLimit[0,50]"] });
        $("#Message").textbox({ validType: ["strLimit[0,50]"] });
        $("#FromUserName").textbox({ validType: ["strLimit[0,50]"] });
        $("#ToUserName").textbox({ validType: ["strLimit[0,50]"] });
    },
    
    /**
     * 提交审核
     * @param {int} pid 页面ID
     * @param {int} selectIds 当前选定ID
     * @returns {Array<int>} ids 提交的页面
     */
    doIt: function (subUrl, pid, eid, selectIds, callback, title) {
        if (!title) {
            title = "提交审核";
        }
        var me = this;
        me.selectIds = selectIds;
        var editor = new CardEditor();;
        editor.ex = me;
        editor.onUiLoaded = me.onFormUiLoaded;
        editor.afterSave = function (succeed,data) {
            if (succeed)
                doOperator(title, subUrl, { action: "submit", selects: selectIds, job: data }, callback);
            else
                showWarning("未知错误");
        };
        editor.uiUrl = me.formUrl + "?pid=" + pid + "&eid=" + eid;
        editor.dataUrl = me.cmdPath + "?action=details&pid= " + pid + "&eid=" + eid + "&ids=" + selectIds + "&id=";
        editor.saveUrl = me.cmdPath + "?action=addnew&pid= " + pid + "&eid=" + eid + "&ids=" + selectIds + "&id=";
        editor.title = title;
        editor.dataId = 0;
        editor.show();
    }
};

/**
 * 提交审核
 * @param {Array<int>} selectIds 当前选定ID
 * @returns {string} subUrl 提交的页面
 */
function doSubmitJob(selectIds, subUrl, callback) {

    doSilentOperator2("数据校验", subUrl + "validate", { selects: selectIds }, function (vres) {
        if (!vres.succeed) {
            showWarning(vres.message || vres.message2);
            if (callback)
                callback(vres);
            return;
        }
        doSilentOperator2("数据支持", subUrl + "eid", {   }, function (eres) {
            if (!eres.succeed) {
                showWarning(vres.message || vres.message2);
                if (callback)
                    callback(vres);
                return;
            }
            var userJob = Object.create(SubmitJob);
            userJob.doIt(subUrl, eres.value.pageId, eres.value.entityType, selectIds, callback);
        });
    });
    
}