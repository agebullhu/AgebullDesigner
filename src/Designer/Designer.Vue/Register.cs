using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer.Vue.View;

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
            DesignerManager.Registe<EntityConfig, UiPanel>("�û�����", "Model");
            DesignerManager.Registe<ModelConfig, UiPanel>("�û�����", "Model");
            DesignerManager.Registe<EntityConfig, CommandPanel>("����༭", "Model");
            DesignerManager.Registe<ModelConfig, CommandPanel>("����༭", "Model");
            DesignerManager.Registe<EntityConfig, JsonPanel>("���л�����", "Entity", "Model");
            DesignerManager.Registe<ModelConfig, JsonPanel>("���л�����", "Model", "Model");
        }
    }
}