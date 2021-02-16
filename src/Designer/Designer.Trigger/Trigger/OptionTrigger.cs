using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// Target触发器
    /// </summary>
    public sealed class OptionTrigger : EventTrigger<ConfigDesignOption>
    {
        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected override void BeforePropertyChanged(string property, object oldValue, object newValue)
        {
            switch (property)
            {
                case nameof(ConfigDesignOption.Key):
                    GlobalConfig.RemoveConfig(Target);
                    break;
            }
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChanged(string property)
        {
            switch (property)
            {
                case nameof(ConfigDesignOption.Key):
                    GlobalConfig.AddConfig(Target);
                    break;
                case nameof(ConfigDesignOption.IsDiscard):
                case nameof(ConfigDesignOption.IsDelete):
                    GlobalConfig.RemoveConfig(Target);
                    break;
                case nameof(ConfigDesignOption.IsModify):
                    if (Target.IsModify && Target.Config != null)
                        Target.Config.IsModify = true;
                    break;
            }
        }
    }
}