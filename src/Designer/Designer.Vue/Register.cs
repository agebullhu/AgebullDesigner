using System.ComponentModel.Composition;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer.Vue
{
    /// <summary>
    /// 命令注册器
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        #region 注册
        
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            DesignerManager.Registe<EntityConfig, UiPanel>("用户界面", "Model");
        }


        #endregion
        
    }
}