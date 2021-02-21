using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ParentConfigBase´¥·¢Æ÷
    /// </summary>
    public sealed class ChildrenTrigger : EventTrigger<IChildrenConfig>, IEventTrigger
    {
        public override void OnPropertyChanged(string property)
        {
            if (TargetConfig.IsModify && TargetConfig.Parent != null)
                TargetConfig.Parent.IsModify = true;
        }
    }
}