using System.ComponentModel.Composition;
using Agebull.EntityModel;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer.View;

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
            DesignerManager.Registe<EnumConfig, EnumEditViewModel>("�༭ö��");
        }

    }
}