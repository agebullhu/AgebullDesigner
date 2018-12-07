using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 分类触发器
    /// </summary>
    public class ClassifyTrigger : ParentConfigTrigger<EntityClassify>
    {
        /// <summary>
        /// 载入事件处理
        /// </summary>
        protected override void OnLoad()
        {
        }
        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            switch (property)
            {
                case nameof(TargetConfig.Name):
                    using (WorkModelScope.CreateScope(WorkModel.Repair))
                    {
                        foreach (var entity in TargetConfig.Items.ToArray())
                            entity.Classify = TargetConfig.Name;
                    }
                    break;
            }
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