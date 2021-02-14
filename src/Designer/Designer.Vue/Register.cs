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
            EditorManager.Registe<IEntityConfig, VuePanel>("Vue����", "UI");
            EditorManager.Registe<IEntityConfig, CommandPanel>("����༭", "UI");
            EditorManager.Registe<IEntityConfig, JsonPanel>("���л�����", "Model", "UI");
        }
    }
}