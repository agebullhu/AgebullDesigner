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
            MomentCoder.RegisteCoder("Web-EasyUi","表单", "html", cfg => DoCode(cfg, EasyUiForm));
            MomentCoder.RegisteCoder("Web-EasyUi", "表单保存", "cs", cfg => DoCode(cfg, InputConvert4));
            MomentCoder.RegisteCoder("Web-EasyUi","表格", "js", cfg => DoCode(cfg, EasyUiGrid));
            MomentCoder.RegisteCoder("Web-EasyUi", "详情", "html", cfg => DoCode(cfg, EasyUiInfo));
            MomentCoder.RegisteCoder("Web-EasyUi", "MvcMenu", "htm", cfg => DoCode(cfg, MvcMenu));
        }
        string DoCode(ConfigBase config, Func<string> coder)
        {
            Entity = config as EntityConfig;
            if (Entity == null)
                return null;
            return coder();
        }
        #endregion

        public string InputConvert4()
        {
            return EasyUiHelperCoder.InputConvert4(Entity);
        }

        public string EasyUiInfo()
        {
            var jsonBuilder = new StringBuilder();
            foreach (PropertyConfig field in Entity.PublishProperty)
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


        public string MvcMenu()
        {
            return
                $@"
           <div iconcls='icon-page' onclick=""javascript:location.href = '@Url.Action(""Index"", ""{Entity.Name}"")'"">
           {Entity.Caption ?? Entity.Description ?? Entity.Name
                    }
           </div>";
        }

        private string EasyUiForm()
        {
            var jsonBuilder = new StringBuilder();

            jsonBuilder.AppendFormat(@"
<form name='{0}Form' id='{0}Form'>
    <div style='width:490px;display: block;'>", Entity.Name.ToLowerInvariant());
            foreach (PropertyConfig field in Entity.PublishProperty)
            {
                string ext = null;
                if (field.IsRequired)
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

        private string EasyUiGrid()
        {
            var jsonBuilder = new StringBuilder();
            foreach (PropertyConfig field in Entity.PublishProperty)
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