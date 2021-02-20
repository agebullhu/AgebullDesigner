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
            EditorManager.Registe2<IEntityConfig, VuePanel>("Vue½çÃæ", "Html", "UI");
            EditorManager.Registe2<IEntityConfig, CommandPanel>("ÃüÁî±à¼­", "Html", "UI");
            EditorManager.Registe2<IEntityConfig, JsonPanel>("ĞòÁĞ»¯ÉèÖÃ","Html", "Model", "UI");
        }
    }
}