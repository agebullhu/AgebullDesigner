using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ParentConfigBase触发器
    /// </summary>
    public abstract class ParentConfigTrigger<TConfig> : ConfigBaseTrigger<TConfig>
        where TConfig : ParentConfigBase
    {

        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected override void BeforePropertyChanged(string property, object oldValue, object newValue)
        {
            if (TargetConfig.IsPredefined)
                return;
            switch (property)
            {
                case nameof(TargetConfig.Name):
                    var now = (string)newValue;
                    if (!string.IsNullOrWhiteSpace(now) && !TargetConfig.OldNames.Contains(now) && now != "NewField")
                        TargetConfig.OldNames.Add(now);
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.NameHistory));
                    break;
            }
            BeforePropertyChangedInner(property, oldValue, newValue);
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChanged(string property)
        {
            base.OnPropertyChanged(property);
            if (TargetConfig.MyChilds == null)
                return;
            switch (property)
            {
                //case nameof(IsDelete):
                //    foreach (var item in MyChilds.OfType<ParentConfigBase>())
                //        item.IsDelete = IsDelete;
                //    return;
                case nameof(TargetConfig.IsModify):
                    if (!TargetConfig.IsModify)
                    {
                        foreach (var item in TargetConfig.MyChilds)
                            item.IsModify = false;
                    }
                    return;
                case nameof(TargetConfig.IsReference):
                    foreach (var item in TargetConfig.MyChilds.OfType<ParentConfigBase>())
                        item.IsReference = TargetConfig.IsReference;
                    return;
                case nameof(TargetConfig.IsFreeze):
                    foreach (var item in TargetConfig.MyChilds.OfType<ParentConfigBase>())
                        item.IsFreeze = TargetConfig.IsFreeze;
                    return;
                    //case nameof(Discard):
                    //    foreach (var item in TargetConfig.MyChilds.OfType<ParentConfigBase>())
                    //        item.Discard = TargetConfig.Discard;
                    //    return;
            }
        }

    }
}