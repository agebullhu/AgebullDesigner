using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ParentConfigBase´¥·¢Æ÷
    /// </summary>
    public class ChildTrigger : ConfigTriggerBase<EntityChildConfig>
    {
        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {
        }

        protected override void OnPropertyChangedInner(string property)
        {
            if (TargetConfig.Parent == null)
                return;
            switch (property)
            {
                case nameof(NotificationObject.IsModify):
                    if (TargetConfig.IsModify)
                        TargetConfig.Parent.IsModify = true;
                    return;
            }
        }
    }
}