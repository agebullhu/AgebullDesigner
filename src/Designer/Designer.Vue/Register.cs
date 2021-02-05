using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer.Vue.View;

namespace Agebull.EntityModel.Designer.Vue
{
    /// <summary>
    /// 命令注册器
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            DesignerManager.Registe<EntityConfig, UiPanel>("用户界面", "Model");
            DesignerManager.Registe<ModelConfig, UiPanel>("用户界面", "Model");
            DesignerManager.Registe<EntityConfig, CommandPanel>("命令编辑", "Model");
            DesignerManager.Registe<ModelConfig, CommandPanel>("命令编辑", "Model");
            DesignerManager.Registe<EntityConfig, JsonPanel>("序列化设置", "Entity", "Model");
            DesignerManager.Registe<ModelConfig, JsonPanel>("序列化设置", "Model", "Model");
        }
    }
}