using Agebull.EntityModel.Config;
using System.Diagnostics;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// Target触发器
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
                $"属性修改({Target.GetType().Name }) : {Target.Name}.{property}");
            switch (property)
            {
                case nameof(Target.Name):
                    var option = Target.Option;
                    var now = (string)newValue;
                    if (!string.IsNullOrWhiteSpace(now) && !option.OldNames.Contains(now) && now != "NewField")
                        option.OldNames.Add(now);
                    option.RaisePropertyChanged(nameof(option.NameHistory));
                    break;
            }
        }
    }
}