using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.RobotCoder.EasyUi;

namespace Agebull.EntityModel.RobotCoder
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ExtendBuilder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Web-EasyUi","表单", "html",  EasyUiForm);
            MomentCoder.RegisteCoder("Web-EasyUi","表格", "js",  EasyUiGrid);
            MomentCoder.RegisteCoder("Web-EasyUi", "详情", "html",  EasyUiInfo);
            MomentCoder.RegisteCoder("Web-EasyUi", "MvcMenu", "htm",  MvcMenu);
        }

        #endregion


        public string EasyUiInfo(EntityConfig entity)
        {
            var jsonBuilder = new StringBuilder();
            foreach (PropertyConfig field in entity.PublishProperty)
            {
                string ext = null;
                if (field.CsType.ToLower() == "bool")
                    ext = " ? \"是\" : \"否\"";
                jsonBuilder.AppendFormat(@"
                <div class='infoField'>
                    <div class='infoLabel' title='{3}'>{0}:</div>
                    <div class='infoValue_s'>@(Model.{1}{2})</div>
                </div>"
                    , field.Caption ?? field.Name
                    , field.Name
                    , ext
                    , field.Description);
            }
            return jsonBuilder.ToString();
        }


        public string MvcMenu(EntityConfig entity)
        {
            return
                $@"
           <div iconcls='icon-page' onclick=""javascript:location.href = '@Url.Action(""Index"", ""{entity.Name}"")'"">
           {entity.Caption ?? entity.Description ?? entity.Name
                    }
           </div>";
        }

        private string EasyUiForm(EntityConfig entity)
        {
            var jsonBuilder = new StringBuilder();

            jsonBuilder.AppendFormat(@"
<form name='{0}Form' id='{0}Form'>
    <div style='width:490px;display: block;'>", entity.Name.ToLowerInvariant());
            foreach (PropertyConfig field in entity.PublishProperty)
            {
                string ext = null;
                if (field.CanUserInput && field.IsRequired)
                    ext = " data-options='required:true'";
                string type = " easyui-text";
                jsonBuilder.AppendFormat(@"
       <div class='inputField'>
            <div class='inputRegion'>
                <div class='inputLabel'>{0}:</div>
                <input name='{1}' class='inputValue inputS {4}'{3}/>
            </div>
            <div class='inputHelp'>{2}</div>
        </div>"
                    , field.Caption, field.Name, field.Description, ext, type);

            }
            jsonBuilder.Append(@"
    </div>
</form>");
            return jsonBuilder.ToString();
        }

        private string EasyUiGrid(EntityConfig entity)
        {
            var jsonBuilder = new StringBuilder();
            foreach (PropertyConfig field in entity.PublishProperty)
            {
                string align = field.CsType == "string" ? "left " : "center";
                string sortable = field.CsType == "string" ? "true " : "false";
                jsonBuilder.AppendFormat(@"
{{ width: 100, align: '{0}', sortable: {1}, field: '{2}', title: '{3}' }},"
                    , align, sortable, field.Name, field.Caption ?? field.Name);
            }
            return jsonBuilder.ToString();
        }

    }
}