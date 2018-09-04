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
        protected override string LangName => "aspx";

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileName => "Index.aspx";

        protected override string BaseCode()
        {
            var cls = Entity.Parent.Classifies.FirstOrDefault(p => p.Name == Entity.Classify);
            var folder = cls == null
                ? $"{Entity.Parent.Caption} > {Entity.Caption}"
                : $"{Entity.Parent.Caption} > {cls.Caption } > {Entity.Caption}";
            return $@"<%@ Page Title='' Language='C#' MasterPageFile='~/JquerySite.Master' AutoEventWireup='true' Inherits='System.Web.UI.Page'%>
<asp:Content ID='cPagePathRegion' ContentPlaceHolderID='PagePathRegion' runat='server'>
{folder}
</asp:Content>
<asp:Content ID='cScriptRegion' ContentPlaceHolderID='ScriptRegion' runat='server'>
    <script type='text/javascript' src='/{Project.PageFolder ?? Project.Name}/{Entity.Option["File_Web_Script_js"]?.Replace('\\', '/')}'></script>
</asp:Content>
<asp:Content ID='cBodyRegion' ContentPlaceHolderID='BodyRegion' runat='server'>{(Entity.TreeUi ? Tree : Grid)}
</asp:Content>";
        }

        private string Tree => $@"
    <div id='layout' class='easyui-layout'>
        <div data-options=""collapsible:false,region:'west',split:true"" style='width: 200px;'>
            <ul id='tree'></ul>
        </div>
        <div class='my_panel'data-options=""region:'center',border:false"">{Grid}
        </div>
    </div>";

        private string Grid => $@"
    <div id='pageToolbarEx'>
        <div id='regCommand' style='display: block;'>
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