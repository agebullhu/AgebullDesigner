globalOptions.appId = "10V6WMADM";
//ajax_ex.baseURL = 'http://localhost';
function showIframe(url) {
    if (!url)
        return;
    try {
        console.log(url);
        var ws = document.getElementById("work_frame");
        if (url.indexOf("?") > 0)
            ws.src = url + "&__t=" + version;
        else
            ws.src = url + "?__t=" + version;
    } catch (e) {
        console.error(e);
    }
}
function ready() {
    var vue_option = {
        el: '#work_space',
        data: {
            isCollapse: true,
            cwid: '',
            ctxt: "",
            user: localStorage.user_nick_name,
            nodes: []
        },
        methods: {
            collapse() {
                vue_option.data.isCollapse = !vue_option.data.isCollapse;
                vue_option.data.cwid = vue_option.data.isCollapse ? '' : '260px';
                vue_option.data.ctxt = vue_option.data.isCollapse ? '' : vue_option.data.user;
            },
            menu_select(index) {
                switch (index) {
                    case 'menu_water_bill':
                        showIframe('/html/monitor/water/index.htm');
                        break;
                    case 'menu_TaskExecution':
                        showIframe('/html/plan-task/taskExecution/index.htm');
                        break;
                    case 'menu_TaskInfo':
                        showIframe('/html/plan-task/taskOption/index.htm');
                        break;
                    case 'menu_log_record':
                        showIframe('/html/log/record/index.htm');
                        return;
                    case '_sys_home':
                        showIframe('/home.html');
                        return;
                    case '_sys_pwd':
                        this.form.visible = true;
                        break;
                    case '_sys_logout':
                        this.onLogout();
                        break;

                    case 'menu_config_center_domain':
                        showIframe('/html/config/domain/index.htm');
                        break;
                    case 'menu_config_center_app':
                        showIframe('/html/config/app/index.htm');
                        break;
                    case 'menu_config_center_section':
                        showIframe('/html/config/section/index.htm');
                        break;

                    case 'menu_archive_org':
                        showIframe('/html/archives/orgInfo/index.htm');
                        break;
                    case 'menu_ArchiveTemplate':
                        showIframe('/html/archives/archiveTemplate/index.htm');
                        break;

                    case 'menu_BusinessArchive':
                        showIframe('/html/archives/businessArchive/index.htm');
                        break;

                    case 'menu_BusinessArchiveSection':
                        showIframe('/html/archives/businessArchiveSection/index.htm');
                        break;
                    case 'menu_ArchiveClassify':
                        showIframe('/html/archives/archiveClassify/index.htm');
                        break;
                    case 'menu_SectionTemplate':
                        showIframe('/html/archives/sectionTemplate/index.htm');
                        break;

                }
                for (var idx = 0; idx < this.nodes.length; idx++) {
                    var node = this.nodes[idx];
                    if (node.tag && node.id === index) {
                        showIframe(node.tag);
                        return;
                    }
                    if (!node.children)
                        return;
                    var nodes = node.children;
                    for (var j = 0; j < nodes.length; j++) {
                        node = nodes[j];
                        if (node.tag && node.id === index) {
                            showIframe(node.tag);
                            return;
                        }
                    }
                }
            }
        }
    };
    vueObject = new Vue(vue_option);
   /* ajax_post("执行", `/appManage/v1/res/menu`, { appid: "" }, function (result) {
        if (result.success) {
            vue_option.data.nodes = result.data;
        }
    });*/
}
ready();