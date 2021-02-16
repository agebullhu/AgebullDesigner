using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <inheritdoc />
    /// <summary>
    /// 枚举配置触发器
    /// </summary>
    public sealed class EnumTrigger : ConfigTriggerBase<EnumConfig>
    {
        protected override void OnLoad()
        {
            Target.Option.IsReference = false;
            Target.Option.IsLink = false;
            Target.Option.ReferenceKey = null;
            Target.Option.ReferenceConfig = null;
        }

        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {
        }

        protected override void OnPropertyChangedInner(string property)
        {
        }
    }
}


