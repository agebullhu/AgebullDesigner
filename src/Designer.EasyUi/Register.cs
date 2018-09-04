using System.ComponentModel.Composition;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;

namespace Agebull.Common.Config.Designer.EasyUi
{
    /// <summary>
    /// ÃüÁî×¢²áÆ÷
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        #region ×¢²á
        
        /// <summary>
        /// ×¢²á´úÂë
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            DesignerManager.Registe<EntityConfig, UiPanel>("EasyUi½çÃæ","Model");
        }


        #endregion
        
    }
}