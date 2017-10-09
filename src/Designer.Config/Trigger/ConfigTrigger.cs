using System;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// TargetConfig触发器
    /// </summary>
    public class ConfigTrigger : ConfigBaseTrigger<ConfigBase>
    {
        /// <summary>
        /// 载入事件处理
        /// </summary>
        protected override void OnLoad()
        {
            if (TargetConfig.Key == Guid.Empty)
                TargetConfig.Key = Guid.NewGuid();

            GlobalConfig.AddConfig(TargetConfig);
        }

        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {
            switch (property)
            {
                case nameof(TargetConfig.Key):
                    GlobalConfig.ConfigDictionary.Remove((Guid)oldValue);
                    break;
                case nameof(TargetConfig.Name):
                    var now = (string)newValue;
                    if (!string.IsNullOrWhiteSpace(now) && !TargetConfig.OldNames.Contains(now) && now != "NewField")
                        TargetConfig.OldNames.Add(now);
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.NameHistory));
                    break;
            }
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            switch (property)
            {
                case nameof(TargetConfig.Key):
                    GlobalConfig.ConfigDictionary.Add(TargetConfig.Key, TargetConfig);
                    break;
            }
        }
    }
}