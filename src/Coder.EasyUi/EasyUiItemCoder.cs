using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class EasyUiItemCoder : EasyUiCoderBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Aspnet_Item_aspx";
        private bool isTemplate => Entity.Properties.Any(p => p.Name == "ProjectTemplateId");
        public override string Code()
        {
            Prepare();
            return $@"<%@ Page Language='C#' AutoEventWireup='true' Inherits='Gboxt.Common.WebUI.{(isTemplate ? "TemplatePage" : "PublishPage")}' %>
<%@ Import Namespace = ""Gboxt.Common.DataModel"" %>
<%@ Import Namespace=""{Project.NameSpace}"" %>
<%@ Import Namespace = ""{Project.NameSpace}.BusinessLogic"" %>
<%@ Import Namespace = ""{Project.NameSpace}.DataAccess"" %>
<%
    var id = GetIntAnyArg(""id"", ""_a_"");
    var business = new {Entity.Name}BusinessLogic();
    var details = business.Details(id);
    string style = null;{Extend()}
%>
<div class='details_range' style='<%=style%>'>{Fields}<br/>
{Norm}<br/>
{Memos}
</div>";
        }

        private string Norm;
        private string Memos;
        /// <summary>
        ///     生成Form录入字段界面
        /// </summary>
        private string Extend()
        {
            StringBuilder code = new StringBuilder();
            if (Entity.Interfaces != null && Entity.Interfaces.Contains("IAudit"))
            {
                code.Append(@"
    if (details.AuditState == AuditStateType.Pass)
    {
        style = @""background-image: url(""""/Styles/model/icons/audit_pass.png""""); "";
    }
    else if (details.AuditState == AuditStateType.Deny)
    {
        style = @""background-image: url(""""/Styles/model/icons/audit_deny.png"""");"";
    }");
            }
            return code.ToString();
        }

        private string Fields;
        /// <summary>
        ///     生成Form录入字段界面
        /// </summary>
        private void Prepare()
        {
            var fields = Entity.ClientProperty.Where(p => !p.IsPrimaryKey && !p.IsSystemField).ToArray();
            StringBuilder code = new StringBuilder(); 
            foreach (var field in fields.Where(p => p.Name != "Memo"))
            {
                if (field.IsLinkKey)
                {
                    continue;
                }
                if (field.MulitLine)
                {
                    code.Append(Memo(field));
                }
                else if (field.InputType == "editor")
                {
                    code.Append(Rich(field));
                }
                else if (field.CsType == "DateTime")
                {
                    code.Append(Date(field));
                }
                else if (field.IsMoney)
                {
                    code.Append(Money(field));
                }
                else if (field.EnumConfig != null)
                {
                    code.Append(EnumConfig(field));
                }
                else if (field.CsType == "bool")
                {
                    code.Append(Bool(field));
                }
                else
                {
                    code.Append(Field(field));
                }
            }
            Fields = code.ToString();
            code = new StringBuilder(); 
            foreach (var field in fields.Where(p => p.Name == "Memo"))
            {
                code.Append(Memo(field));
            }
            Memos = code.ToString();

            if (isTemplate)
            {

                Norm = @"
        <% 
        if (details.Norms.Count > 0)
        {
            foreach (var value in details.Norms)
            {
                var norm = GetNorm(value.NormId);
                if (norm.Type == NormType.RichText)
                {
        %>
        <div class='details_block'>
            <span class='details_label_s'><%= norm.Norm %></span><br />
            <span class='details_rich_value'><%= value.Content %></span>
        </div>
        <%
                }
                else if (norm.Type == NormType.File)
                {
        %>
        <a title='<%=norm.Norm %>' href='<%=value.Content %>'>
            <img style='border: 0px' src='/Styles/model/icons/file.png'/><%=norm.Norm %>
        </a>
        <%
                }
                else
                {
        %>
        <div class='details_block'>
            <span class='details_label_s'><%= norm.Norm %></span>
            <span><%= NormHtml(value.Text,norm) %><%= norm.Unit %></span>
        </div>
        <%
                }
             }
        }%>";
            }
        }

        private string Rich(PropertyConfig field)
        {
            return $@"
        <div class='details_block'>
            <span class='details_label_s'>{field.Caption}</span><br />
            <span class='details_value'>{field.Prefix}<%= details.{field.Name} %>{field.Suffix}</span>
        </div> ";
        }

        private string Memo(PropertyConfig field)
        {
            return $@"
        <div class='details_block'>
            <span class='details_label_s'>{field.Caption}</span><br />
            <span class='details_value'>{field.Prefix}<%= ToHtmlParagraph(details.{field.Name}) %>{field.Suffix}</span>
        </div>";
            //        return $@"
            //<% if (!string.IsNullOrEmpty({field.Name})){{%>
            //<fieldset>
            //    <legend>{field.Caption ?? field.Name}</legend>
            //    <%= ToHtmlParagraph(details.{field.Name}) %>
            //</fieldset>
            //<% }}%>";
        }

        private string Field(PropertyConfig field)
        {
            return $@"
        <div class='details_block'>
            <div class='details_label_s'>{field.Caption ?? field.Name}：</div>
            <span>{field.Prefix}<%= details.{field.Name} %>{field.Suffix}</span>
        </div>";
        }

        string Money(PropertyConfig field)
        {
            return $@"
        <div class='details_block'>
            <div class='details_label_s'>{field.Caption ?? field.Name}：</div>
            <span>{field.Prefix}<%= ToMoney(details.{field.Name}) %>{field.Suffix}</span>
        </div>";
        }
        string EnumConfig(PropertyConfig field)
        {
            return $@"
        <div class='details_block'>
            <div class='details_label_s'>{field.Caption ?? field.Name}：</div>
            <span>{field.Prefix}<%= details.{field.Name}_Content %>{field.Suffix}</span>
        </div>";
        }
        string Bool(PropertyConfig field)
        {
            return $@"
        <div class='details_block'>
            <div class='details_label_s'>{field.Caption ?? field.Name}：</div>
            <span>{field.Prefix}<%= (details.{field.Name} ?""是"" : ""否"") %>{field.Suffix}</span>
        </div>";
        }
        string Date(PropertyConfig field)
        {
            return $@"
        <div class='details_block'>
            <div class='details_label_s'>{field.Caption ?? field.Name}：</div>
            <span>{field.Prefix}<%= ToHtmlDate(details.{field.Name}) %>{field.Suffix}</span>
        </div>";
        }

    }
}