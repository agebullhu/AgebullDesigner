using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <inheritdoc />
    /// <summary>
    /// 枚举配置触发器
    /// </summary>
    public sealed class EnumTrigger : EventTrigger<EnumConfig>, IEventTrigger
    {
        public override void OnLoad()
        {
            TargetConfig.Option.IsReference = false;
            TargetConfig.Option.IsLink = false;
            TargetConfig.Option.ReferenceKey = null;
            TargetConfig.Option.ReferenceConfig = null;
        }
    }
}


