using System.ComponentModel.Composition;
using Agebull.EntityModel;
using Agebull.EntityModel.Designer;

namespace Agebull.Common.Config.Designer.EasyUi
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
            GlobalTrigger.RegistTrigger<ConfigTrigger>();
            GlobalTrigger.RegistTrigger<OptionTrigger>();
            GlobalTrigger.RegistTrigger<PropertyTrigger>();
            GlobalTrigger.RegistTrigger<EntityTrigger>();
            GlobalTrigger.RegistTrigger<EntityChildTrigger>();
            GlobalTrigger.RegistTrigger<ProjectTrigger>();
            GlobalTrigger.RegistTrigger<ProjectChildTrigger>();
            GlobalTrigger.RegistTrigger<SolutionTrigger>();
            GlobalTrigger.RegistTrigger<EnumTrigger>();
        }
    }
}