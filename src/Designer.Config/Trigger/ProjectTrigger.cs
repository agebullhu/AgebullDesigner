using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ��Ŀ���ô�����
    /// </summary>
    public class ProjectTrigger : ParentConfigTrigger<ProjectConfig>
    {
        /// <summary>
        /// �����¼�����
        /// </summary>
        protected override void OnLoad()
        {
            using (WorkModelScope.CreateScope(WorkModel.Repair))
            {
                foreach (var cfg in TargetConfig.Classifies)
                {
                    GlobalTrigger.OnLoad(cfg);
                }
                TargetConfig.IsReference = TargetConfig.Entities.Count > 0 && TargetConfig.Entities.All(p => p.IsReference);
                foreach (var entity in TargetConfig.Entities)
                {
                    entity.Project = TargetConfig.Name;
                    entity.Parent = TargetConfig;
                    foreach (var field in entity.Properties)
                        field.Parent = entity;
                }
                foreach (var entity in TargetConfig.ApiItems)
                {
                    entity.Project = TargetConfig.Name;
                    entity.Parent = TargetConfig;
                }
                foreach (var entity in TargetConfig.Enums)
                {
                    entity.Project = TargetConfig.Name;
                    entity.Parent = TargetConfig;
                }
            }
        }
        
        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
        }
        
        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {
        }
    }
}