using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 项目配置触发器
    /// </summary>
    public class ProjectTrigger : ParentConfigTrigger<ProjectConfig>
    {
        /// <summary>
        /// 载入事件处理
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
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
        }
        
        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {
        }
    }
}