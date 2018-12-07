using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ParentConfigBase´¥·¢Æ÷
    /// </summary>
    public abstract class ParentConfigTrigger<TConfig> : ConfigTriggerBase<TConfig>
        where TConfig : ParentConfigBase
    {
    }
}