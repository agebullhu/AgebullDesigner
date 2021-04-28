using Agebull.EntityModel.Designer;
using System.ComponentModel.Composition;

namespace Agebull.EntityModel.RobotCoder.UniAppComponents
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class UniAppMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region зЂВс
        const string folder = "uni-app";
        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegisteCoder(folder, "form", "html", UniAppDetailsComponent.FormCode);
            CoderManager.RegisteCoder(folder, "rules", "js", UniAppDetailsComponent.RulesCode);
        }

        #endregion

    }
}