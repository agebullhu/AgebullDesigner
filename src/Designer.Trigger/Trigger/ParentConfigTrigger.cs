using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ParentConfigBase������
    /// </summary>
    public abstract class ParentConfigTrigger<TConfig> : ConfigTriggerBase<TConfig>
        where TConfig : ParentConfigBase
    {
    }
}