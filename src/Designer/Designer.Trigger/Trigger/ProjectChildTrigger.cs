using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ParentConfigBase触发器
    /// </summary>
    public sealed class ProjectChildTrigger : ConfigTriggerBase<ProjectChildConfigBase>
    {
        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            if (Target.Project == null)
                return;
            switch (property)
            {
                case nameof(NotificationObject.IsModify):
                    if (Target.IsModify)
                        Target.Project.IsModify = true;
                    return;
            }
        }
    }
}