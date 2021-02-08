using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ParentConfigBase´¥·¢Æ÷
    /// </summary>
    public class ChildrenTrigger : EventTrigger
    {
        protected override void OnPropertyChanged(string property)
        {
            if (Target.IsModify && Target is IChildrenConfig children && children.Parent !=null)
                children.Parent.IsModify = true;
        }
    }
}