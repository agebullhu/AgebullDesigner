using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// TargetConfig触发器
    /// </summary>
    public class OptionTrigger : EventTrigger
    {
        /// <summary>
        /// 构造
        /// </summary>
        public OptionTrigger()
        {
            TargetType = typeof(ConfigDesignOption);
        }

        /// <summary>
        /// 当前对象
        /// </summary>
        public ConfigDesignOption Option => (ConfigDesignOption)Target;


        /// <summary>
        /// 当前对象
        /// </summary>
        public ConfigBase Config => Option?.Config;

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
                    GlobalConfig.RemoveConfig(Option);
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
                    GlobalConfig.AddConfig(Option);
                    break;
                case nameof(ConfigDesignOption.IsDiscard):
                case nameof(ConfigDesignOption.IsDelete):
                    GlobalConfig.RemoveConfig(Option);
                    break;
            }
        }
    }
}