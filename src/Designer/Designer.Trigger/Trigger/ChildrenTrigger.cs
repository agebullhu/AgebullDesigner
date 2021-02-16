using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ParentConfigBase������
    /// </summary>
    public sealed class ChildrenTrigger : EventTrigger<IChildrenConfig>
    {
        protected override void OnPropertyChanged(string property)
        {
            if (Target.IsModify && Target.Parent != null)
                Target.Parent.IsModify = true;
        }
    }
}