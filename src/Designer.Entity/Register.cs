using System.ComponentModel.Composition;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;

namespace Agebull.Common.Config.Designer.EasyUi
{
    /// <summary>
    /// ��ϵ���Ӽ��
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
            DesignerManager.Registe<EntityConfig, FieldsViewModel>("�ֶλ���");
            DesignerManager.Registe<EntityConfig, CppFieldsViewModel>("�ֶ�����");
            DesignerManager.Registe<EntityConfig, RegularViewModel>("����У��");
            DesignerManager.Registe<EntityConfig, ModelViewModel>("ʵ��ģ��");
            DesignerManager.Registe<EntityConfig, AllFieldsViewModel>("�����ֶ�");
        }
    }
}