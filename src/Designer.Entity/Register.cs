using System.ComponentModel.Composition;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;

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
            DesignerManager.Registe<EntityConfig, FieldsViewModel>("字段基本");
            DesignerManager.Registe<EntityConfig, CppFieldsViewModel>("字段类型");
            DesignerManager.Registe<EntityConfig, RegularViewModel>("数据校验");
            DesignerManager.Registe<EntityConfig, ModelViewModel>("实体模型");
            DesignerManager.Registe<EntityConfig, AllFieldsViewModel>("所有字段");
        }
    }
}