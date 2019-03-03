
$(document).ready(
    function () {
        setTimeout(function () {
            extendUe();
            createHideUeditor();
        }, 1000);
    });


var hide_ueditor;
var hide_ueditor_ready = false;
function createHideUeditor() {
    if (hide_ueditor)
        return;
    try {
        hide_ueditor = new UE.ui.Editor();
        hide_ueditor.render("__ueditor_hide__");
        hide_ueditor.ready(function () {
            hide_ueditor.setDisabled();
            hide_ueditor.hide();//隐藏UE框体
            hide_ueditor_ready = true;
        });
    } catch (e) {
        setTimeout(createHideUeditor, 100);
    }
}
function showImageUpload(succeed) {
    if (!hide_ueditor_ready) {
        setTimeout(() => showImageUploadInner(succeed), 100);
        return;
    }
    showImageUploadInner(succeed);
}
function showImageUploadInner(succeed) {
    var dlg = hide_ueditor.getDialog("insertimage");
    dlg.extend_end = function (arg) {
        if (succeed)
            succeed(arg[0].src);
    };
    dlg.on("show", function () {
        this.getDom().style.zIndex = 20004;
    });
    dlg.render();
    dlg.open();
}
function showAttachmentUpload() {
    createHideUeditor();
    if (!hide_ueditor_ready) {
        setTimeout(showAttachmentUpload, 100);
        return;
    }
    var dlg = hide_ueditor.getDialog("attachment");
    dlg.render();
    dlg.open();
}

function extendUe() {
    if (!window.UE || !window.UE.Editor) {
        setTimeout(extendUe, 200);
        return;
    }
    window.UE.Editor.prototype.loadServerConfig = function() {
        var me = this;
        me.fireEvent("serverConfigLoaded");
        me._serverConfigLoaded = true;
    };
}
