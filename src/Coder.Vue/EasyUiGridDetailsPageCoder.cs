using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.VUE
{

    public class VueListDetailsPageCoder : VueCoderBase
    {
        /// <summary>
        /// Ãû³Æ
        /// </summary>
        protected override string FileSaveConfigName => "File_Vue_List_Details_aspx";

        public override string Code()
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"
<%@ Page Language='C#' AutoEventWireup='true' Inherits='Gboxt.Common.WebUI.UiPage' %>

<%@ Import Namespace=""{Project.NameSpace}"" %>
<%@ Import Namespace = ""{Project.NameSpace}.BusinessLogic"" %>
<%
    var business = new {Entity.Name}BusinessLogic();
    var id = GetIntArg(""id"");
    var details = business.Details(id) ?? new {Entity.EntityName}();
%>
<div class='details_range'>");
            foreach (var field in Entity.ClientProperty.Where(p => p.GridDetails))
            {
                code.Append($@"
    <div class='details_block'>
        <label class='details_label'>{field.Caption}</label>");
                if (string.IsNullOrEmpty(field.GridDetailsCode))
                    code.Append($@"
        <label class='details_value'><%=details.{field.Name}%></label>");
                else
                {
                    var uc = string.Format(field.GridDetailsCode, $@"<%=details.{field.Name}%>", field.Description);
                    code.Append($@"
        <label class='details_value'>{uc}</label>" );
                }
                code.Append(@"
    </div>");
            }
            code.Append(@"
</div>");
            return code.ToString();
        }

    }
}