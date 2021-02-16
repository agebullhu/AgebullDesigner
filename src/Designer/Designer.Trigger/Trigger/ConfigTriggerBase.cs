using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// Target触发器
    /// </summary>
    public class ConfigTriggerBase<TConfig> : EventTrigger<TConfig>
        where TConfig : ConfigBase
    {
        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected sealed override void BeforePropertyChanged(string property, object oldValue, object newValue)
        {
            if (Target.Option.IsReadonly)
                return;
            BeforePropertyChangedInner(property, oldValue, newValue);
        }

        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected virtual void BeforePropertyChangedInner(string property, object oldValue, object newValue) { }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected sealed override void OnPropertyChanged(string property)
        {
            if (Target.Option.IsReadonly)
                return;
            OnPropertyChangedInner(property);
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected virtual void OnPropertyChangedInner(string property) { }
    }
}