using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class EasyUiIndexPageCoder : EasyUiCoderBase
    {
        protected override string LangName => "html";

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileName => "index.html";

        protected override string BaseCode()
        {
            var cls = Entity.Parent.Classifies.FirstOrDefault(p => p.Name == Entity.Classify);
            var folder = cls == null
                ? $"{Entity.Parent.Caption} > {Entity.Caption}"
                : $"{Entity.Parent.Caption} > {cls.Caption } > {Entity.Caption}";
            return $@"<!DOCTYPE html>
<html>
<head><meta name='renderer' content='webkit' /><meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=21' />
<title>{folder}
</title>
	<meta charset='utf-8' />
	<meta name='viewport' content='width=device-width' />
	<meta name='renderer' content='webkit' />
	<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
	<link rel='stylesheet' type='text/css' href='/styles/css/default.css?v=20190105' />
    <!--JQuery-->
    <script type='text/javascript' src='/scripts/jquery-3.3.1.min.js?v=20190105'></script>
    <script type='text/javascript'>
        window.UEDITOR_HOME_URL = '/ueditor/';
    </script>
    <!--ueditor-->
    <script type='text/javascript' charset='utf-8' src='/ueditor/ueditor.config.js?v=20190105'></script>
    <script type='text/javascript' charset='utf-8' src='/ueditor/ueditor.all.min.js?v=20190105'> </script>
    <script type='text/javascript' charset='utf-8' src='/ueditor/ueditor.parse.min.js?v=20190105'> </script>
    <script type='text/javascript' charset='utf-8' src='/ueditor/lang/zh-cn/zh-cn.js?v=20190105'></script>
    <script type='text/javascript' src='/ueditor/third-party/codemirror/codemirror.js'></script>
    <script type='text/javascript' src='/ueditor/third-party/zeroclipboard/ZeroClipboard.min.js?v=20190105'></script>
    <!--EasyUI-->
    
    <link rel='stylesheet' type='text/css' href='/scripts/themes/metro/easyui.css?v=20190105' /><link rel='stylesheet' type='text/css' href='/scripts/themes/icon.css?v=20190105' />
    <script type='text/javascript' src='/scripts/jquery.easyui.min.js?v=20190105'></script>
    <script type='text/javascript' src='/scripts/datagrid-detailview.js?v=20190105'></script>
    <script type='text/javascript' src='/scripts/locale/easyui-lang-zh_CN.js?v=20190105'></script>
    <!--Extend-->
    <script type='text/javascript' src='/scripts/extend/core.js?v=20190105'></script>
    <script type='text/javascript' src='/scripts/extend/button.js?v=20190105'></script>
    <script type='text/javascript' src='/scripts/extend/ajax.js?v=20190105'></script>
    <script type='text/javascript' src='/scripts/extend/extend.js?v=20190105'></script>
    
    <script type='text/javascript' src='/scripts/extend/page.js?v=20190105'></script>
    <script type='text/javascript' src='/scripts/extend/dialog.js?v=20190105'></script>
    <script type='text/javascript' src='/scripts/extend/tree.js?v=20190105'></script>
    <script type='text/javascript' src='/scripts/extend/grid_panel.js'></script>
    <script type='text/javascript' src='/scripts/extend/card_panel.js?v=20190105'></script>
    <script type='text/javascript' src='/scripts/extend/type.js?v=20190105'></script>
    <script type='text/javascript' src='/scripts/extend/ueditor_ex.js?v=20190105'></script>
    <!--Business-->
    <script type='text/javascript' src='/scripts/business/type.js?v=20190105'></script>
    <script type='text/javascript' src='/scripts/business/business.js?v=20190105'></script>
    <script type='text/javascript' src='/scripts/business/userjob.js?v=20190105'></script>
    <link rel='stylesheet' type='text/css' href='/styles/model/Site.css?v=20190105' /><link rel='stylesheet' type='text/css' href='/styles/model/icon.css?v=20190105' />
    <style type='text/css'>
        .parentInput {{
            border: 0 white solid;
            color: red;
            text-align: right;
        }}

        .selectInput {{
            border: 0 white solid;
            color: blue;
            text-align: right;
        }}

        .editInput {{
            border-bottom: 1px black solid;
            border-left: 0 black solid;
            border-right: 0 black solid;
            border-top: 0 black solid;
            text-align: right;
        }}
    </style>
    <script type='text/javascript' src='/{Project.PageFolder ?? Project.Name}/{Entity.Option["File_Web_Script_js"]?.Replace('\\', '/')}'></script>
</head>
<body>
    <div id='rBody' class='content_body' style='visibility: hidden;'>
    {(Entity.TreeUi ? Tree : Grid)}
    </div>
    <div style='display: none; visibility: collapse;'>
        <div id='xx_dialog_region_xx' style='display: none; visibility: collapse;'></div>
    </div>
    <div id='__loading__'>
        <div class='panel-loading busy_body'>正在拼命执行中...</div>
    </div>
    <div id='__ueditor_hide__' style='width: 1px; height: 1px; left: 0px; top: 0; visibility: hidden; position: absolute; z-index: 99999'></div>
</body>
</html>";
        }

        private string Tree => $@"
    <div id='layout' class='easyui-layout'>
        <div data-options=""collapsible:false,region:'west',split:true"" style='width: 200px;'>
            <ul id='tree'></ul>
        </div>
        <div class='my_panel'data-options=""region:'center',border:false"">{Grid}
        </div>
    </div>";

        private string Grid => Entity.IsUiReadOnly
            ? $@"
    <div id='pageToolbarEx'>
        <div id='regCommand' style='inline-block;'>
            <a id='btnEdit' href='javascript:void(0)'>查看</a>
        </div>
        <div id='regQuery' class='toolbar_line'>
            <label class='queryLabel'>关键字</label>
            <input id = 'qKeyWord' class='inputValue inputS easyui-textbox' />{ExtQueryHtmlCode()}
            <a id = 'btnQuery' href='javascript:void(0)'>查询</a>
        </div>
    </div>
    <div id='grid'></div>"
            : $@"
    <div id='pageToolbarEx'>
        <div id='regCommand' style='inline-block;'>
            <a id='btnAdd' href='javascript:void(0)'>新增</a>
            <a id='btnEdit' href='javascript:void(0)'>修改</a>
            <a id='btnDelete' href='javascript:void(0)'>删除</a>{CommandHtmlCode()}
        </div>
        <div id='regQuery' class='toolbar_line'>
            <label class='queryLabel'>关键字</label>
            <input id = 'qKeyWord' class='inputValue inputS easyui-textbox' />{ExtQueryHtmlCode()}
            <a id = 'btnQuery' href='javascript:void(0)'>查询</a>
        </div>
    </div>
    <div id='grid'></div>";

        private string ExtQueryHtmlCode()
        {
            if (Entity.Interfaces == null) return null;
            var code = new StringBuilder();
            if (Entity.Interfaces.Contains("IStateData"))
                code.Append(@"
            <label class='queryLabel'>数据状态:</label>
            <label class='queryLabel'>
                <input id='qAudit' class='inputValue_SSS inputS easyui-combobox' 
                       data-options=""valueField:'value',textField:'text',data:dataStateType"" />
            </label>");
            if (Entity.Interfaces.Contains("IAudit"))
            {
                code.Append(@"
            <label class='queryLabel'>审核状态:</label>
            <label class='queryLabel'>
                <input id='qAudit' class='inputValue_SSS inputS easyui-combobox' 
                       data-options=""valueField:'value',textField:'text',data:auditType"" />
            </label>");
            }
            return code.ToString();
        }

        private string CommandHtmlCode()
        {
            var code = new StringBuilder();

            if (Entity.Interfaces != null)
            {
                if (Entity.Interfaces.Contains("IAuditData"))
                    code.Append(@"
        <div style='display: inline;'><div class='toolbarSpace'></div></div>
        <a id='btnValidate' href='javascript:void(0)'>数据校验</a>
        <div style='display: inline;'><div class='toolbarSpace'></div></div>
        <a id='btnAuditSubmit' href='javascript:void(0)'>提交</a>
        <a id='btnPullback' href='javascript:void(0)' title='拉回已提交的数据,仅在提交后十分钟内且上级未进行审核时有效' class='easyui-tooltip'>拉回</a>
        <div style='display: inline;'><div class='toolbarSpace'></div></div>
        <a id='btnAuditBack' href='javascript:void(0)'>退回</a>
        <a id='btnAuditPass' href='javascript:void(0)'>通过</a>
        <a id='btnAuditDeny' href='javascript:void(0)'>否决</a>
        <div style='display: inline;'><div class='toolbarSpace'></div></div>
        <a id='btnReAudit' href='javascript:void(0)'>重做</a>");
                if (Entity.Interfaces.Contains("IStateData"))
                    code.Append(@"
        <div style='display: inline;'><div class='toolbarSpace'></div></div>
        <a id='btnEnable' href='javascript:void(0)'>启用</a>
        <a id='btnDisable' href='javascript:void(0)'>禁用</a>
        <div style='display: inline;'><div class='toolbarSpace'></div></div>
        <a id='btnDiscard' href='javascript:void(0)'>废弃</a>
        <a id='btnReset' href='javascript:void(0)'>重置</a>");
            }
            if (Entity.Commands.Count > 0)
                code.Append(@"
        <div style='display: inline;'><div class='toolbarSpace'></div></div>");
            foreach (var cmd in Entity.Commands)
            {
                code.Append($@"
            <a id='btn{cmd.Name}' href='javascript: void(0)'>{cmd.Caption}</a>");
            }

            return code.ToString();
        }
    }
}