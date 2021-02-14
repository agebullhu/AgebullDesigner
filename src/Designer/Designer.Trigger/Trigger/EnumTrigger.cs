using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <inheritdoc />
    /// <summary>
    /// 枚举配置触发器
    /// </summary>
    public sealed class EnumTrigger : ParentConfigTrigger<EnumConfig>
    {
        protected override void OnLoad()
        {
            TargetConfig.Option.IsReference = false;
            TargetConfig.Option.IsLink = false;
            TargetConfig.Option.ReferenceKey = null;
            TargetConfig.Option.ReferenceConfig = null;
        }

        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {
        }

        protected override void OnPropertyChangedInner(string property)
        {
        }
    }
}


