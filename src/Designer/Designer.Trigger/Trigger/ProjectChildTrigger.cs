using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ParentConfigBase������
    /// </summary>
    public sealed class ProjectChildTrigger : ConfigTriggerBase<ProjectChildConfigBase>
    {
        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {
        }

        /// <summary>
        /// �����¼�����
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