using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ParentConfigBase������
    /// </summary>
    public class ProjectChildTrigger : ConfigTriggerBase<ProjectChildConfigBase>
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
            if (TargetConfig.Project == null)
                return;
            switch (property)
            {
                case nameof(NotificationObject.IsModify):
                    if (TargetConfig.IsModify)
                        TargetConfig.Project.IsModify = true;
                    return;
            }
        }
    }
}