using System;

namespace Agebull.EntityModel
{

    /// <summary>
    /// 事件触发器
    /// </summary>
    public abstract class EventTrigger<TTarget> 
        where TTarget : class
    {
        /// <summary>
        /// 构造
        /// </summary>
        protected EventTrigger()
        {
            TargetType = typeof(TTarget);
        }

        /// <summary>
        /// 目标类型
        /// </summary>
        public Type TargetType { get; }

        /// <summary>
        /// 目标类型
        /// </summary>
        public TTarget TargetConfig { get; private set; }

        /// <summary>
        /// 当前配置对象
        /// </summary>
        public object Target
        {
            get => TargetConfig;
            set => TargetConfig = value as TTarget;
        }

        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        public virtual void BeforePropertyChange(string property, object oldValue, object newValue)
        {
        }


        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        public virtual void OnPropertyChanged(string property)
        {

        }

        /// <summary>
        /// 载入事件处理
        /// </summary>
        public virtual void OnCreate()
        {
        }


        /// <summary>
        /// 载入事件处理
        /// </summary>
        public virtual void OnLoad()
        {
        }

        /// <summary>
        /// 加入事件处理
        /// </summary>
        /// <param name="parent"></param>
        public virtual void OnAdded(object parent)
        {
        }


        /// <summary>
        /// 删除事件处理
        /// </summary>
        /// <param name="config"></param>
        public virtual void OnRemoved(object parent)
        {
        }

    }
}
