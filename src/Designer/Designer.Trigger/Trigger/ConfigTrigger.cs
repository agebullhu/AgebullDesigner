using System;
using System.Diagnostics;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// TargetConfig触发器
    /// </summary>
    public sealed class ConfigTrigger : ConfigTriggerBase<ConfigBase>
    {
        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace,
                $"[{oldValue}] => [{newValue}]",
                $"属性修改({Target.GetType().Name }) : {TargetConfig.Name}.{property}");
            switch (property)
            {
                case nameof(TargetConfig.Name):
                    var option = TargetConfig.Option;
                    var now = (string)newValue;
                    if (!string.IsNullOrWhiteSpace(now) && !option.OldNames.Contains(now) && now != "NewField")
                        option.OldNames.Add(now);
                    option.RaisePropertyChanged(nameof(option.NameHistory));
                    break;
            }
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
        }
    }
}