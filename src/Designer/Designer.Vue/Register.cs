using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer.Vue.View;
using System.ComponentModel.Composition;

namespace Agebull.EntityModel.Designer.Vue
{
    /// <summary>
    /// ����ע����
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        /// <summary>
        /// ע�����
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            EditorManager.Registe2<IEntityConfig, VuePanel>("Vue����", "Html", "UI");
            EditorManager.Registe2<IEntityConfig, CommandPanel>("����༭", "Html", "UI");
            EditorManager.Registe2<IEntityConfig, JsonPanel>("���л�����","Html", "Model", "UI");
        }
    }
}