using Agebull.EntityModel.Config;
using System.Linq;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 分类触发器
    /// </summary>
    public sealed class ClassifyTrigger : ConfigTriggerBase<EntityClassify>
    {
        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            switch (property)
            {
                case nameof(Target.Name):
                    using (WorkModelScope.CreateScope(WorkModel.Repair))
                    {
                        foreach (var entity in Target.Items.ToArray())
                            entity.Classify = Target.Name;
                    }
                    break;
            }
        }
    }
}