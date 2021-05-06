using Agebull.EntityModel.Designer;
using System.ComponentModel.Composition;

namespace Agebull.EntityModel.RobotCoder.VueComponents
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class VueComponentMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region зЂВс
        const string folder = "Web-Vue-Component";
        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegisteCoder(folder, "common.js", "js", CommonComponent.JsCode);
            CoderManager.RegisteCoder(folder, "index.css", "css", IndexComponent.CssCode);
            CoderManager.RegisteCoder(folder, "index.htm", "html", IndexComponent.HtmlCode);
            CoderManager.RegisteCoder(folder, "index.js", "js", IndexComponent.JsCode);
            CoderManager.RegisteCoder(folder, "form.htm", "html", FormComponent.HtmlCode);
            CoderManager.RegisteCoder(folder, "form.js", "js", FormComponent.JsCode);
            CoderManager.RegisteCoder(folder, "list.htm", "html", ListComponent.HtmlCode);
            CoderManager.RegisteCoder(folder, "list.js", "js", ListComponent.JsCode);
            CoderManager.RegisteCoder(folder, "menu", "js", VueComponentExtensions.HtmlMenu);
        }

        #endregion

    }
}