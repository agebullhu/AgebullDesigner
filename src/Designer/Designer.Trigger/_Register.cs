using Agebull.EntityModel;
using Agebull.EntityModel.Designer;
using System;
using System.ComponentModel.Composition;
using System.Linq;

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
            foreach (var trigger in GetType().Assembly.GetTypes().Where(p => p.IsSupperInterface(typeof(IEventTrigger))))
            {
                GlobalTrigger.RegistTrigger(trigger);
            }
        }
    }
}