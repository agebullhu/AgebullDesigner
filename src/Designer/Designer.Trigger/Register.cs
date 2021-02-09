using System.ComponentModel.Composition;
using System.Linq;
using Agebull.EntityModel;
using Agebull.EntityModel.Designer;

namespace Agebull.Common.Config.Designer.EasyUi
{
    /// <summary>
    /// ÃüÁî×¢²áÆ÷
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class Register : IAutoRegister
    {
        /// <summary>
        /// ×¢²á´úÂë
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            foreach(var trigger in GetType().Assembly.GetTypes().Where(p =>p.IsSealed && p.IsSubclassOf(typeof(EventTrigger))))
            {
                GlobalTrigger.RegistTrigger(trigger);
            }
        }
    }
}