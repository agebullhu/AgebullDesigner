using Agebull.Common.DataModel;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    /// TargetConfig触发器
    /// </summary>
    public abstract class ConfigBaseTrigger<TConfig> : EventTrigger
        where TConfig : ConfigBase
    {
        /// <summary>
        /// 构造
        /// </summary>
        protected ConfigBaseTrigger()
        {
            TargetType = typeof(TConfig);
        }

        /// <summary>
        /// 当前对象
        /// </summary>
        public TConfig TargetConfig => (TConfig)Target;

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
            BeforePropertyChangedInner(property, oldValue, newValue);
        }

        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected abstract void BeforePropertyChangedInner(string property, object oldValue, object newValue);

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChanged(string property)
        {
            if (TargetConfig.IsPredefined)
                return;
            OnPropertyChangedInner(property);
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected abstract void OnPropertyChangedInner(string property);
    }
    /// <summary>
    /// TargetConfig触发器
    /// </summary>
    public abstract class ConfigBaseTriggerEx<TConfig> : ConfigBaseTrigger<TConfig>
        where TConfig : ConfigBase
    {
        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {

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