using System.ComponentModel.Composition;
using Agebull.EntityModel;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer.View;

namespace Agebull.Common.Config.Designer.EasyUi
{
    /// <summary>
    /// 关系连接检查
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
            DesignerManager.Registe<EnumConfig, EnumEditViewModel>("编辑枚举");
        }

    }
}