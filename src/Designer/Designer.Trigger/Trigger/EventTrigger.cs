using System;

namespace Agebull.EntityModel
{

    /// <summary>
    /// 事件触发器
    /// </summary>
    public abstract class EventTrigger<TTarget> : IEventTrigger
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
        public TTarget Target { get; private set; }

        /// <summary>
        /// 当前配置对象
        /// </summary>
        object IEventTrigger.Target
        {
            get => Target;
            set => Target = value as TTarget;
        }

        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        void IEventTrigger.BeforePropertyChanged(string property, object oldValue, object newValue)
        {
            BeforePropertyChanged(property, oldValue, newValue);
        }

        void IEventTrigger.OnPropertyChanged(string property)
        {
            OnPropertyChanged(property);
        }

        void IEventTrigger.OnCreate()
        {
            OnCreate();
        }
        void IEventTrigger.OnLoad()
        {
            OnLoad();
        }
        void IEventTrigger.OnAdded(object config)
        {
            OnAdded(config);
        }
        void IEventTrigger.OnRemoved(object config)
        {
            OnRemoved(config);
        }

        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected virtual void BeforePropertyChanged(string property, object oldValue, object newValue)
        {
        }


        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected virtual void OnPropertyChanged(string property)
        {

        }

        /// <summary>
        /// 载入事件处理
        /// </summary>
        protected virtual void OnCreate()
        {
        }


        /// <summary>
        /// 载入事件处理
        /// </summary>
        protected virtual void OnLoad()
        {
        }

        /// <summary>
        /// 加入事件处理
        /// </summary>
        /// <param name="config"></param>
        protected virtual void OnAdded(object config)
        {
        }


        /// <summary>
        /// 删除事件处理
        /// </summary>
        /// <param name="config"></param>
        protected virtual void OnRemoved(object config)
        {
        }

    }
}
