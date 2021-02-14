using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer.Vue.View;
using System.ComponentModel.Composition;

namespace Agebull.EntityModel.Designer.Vue
{
    /// <summary>
    /// ÃüÁî×¢²áÆ÷
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        /// <summary>
        /// ×¢²á´úÂë
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            EditorManager.Registe<IEntityConfig, VuePanel>("Vue½çÃæ", "UI");
            EditorManager.Registe<IEntityConfig, CommandPanel>("ÃüÁî±à¼­", "UI");
            EditorManager.Registe<IEntityConfig, JsonPanel>("ĞòÁĞ»¯ÉèÖÃ", "Model", "UI");
        }
    }
}