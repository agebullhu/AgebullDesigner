using System;
using System.Collections.Generic;

namespace Agebull.Common.DataModel
{
    /// <summary>
    /// 全局触发器
    /// </summary>
    public static class GlobalTrigger
    {
        /// <summary>
        /// 触发器
        /// </summary>
        private static readonly List<Func<EventTrigger>> TriggerCreaters = new List<Func<EventTrigger>>();

        /// <summary>
        /// 触发器
        /// </summary>
        private static readonly List<EventTrigger> Triggers = new List<EventTrigger>();

        /// <summary>
        /// 注册触发器
        /// </summary>
        /// <typeparam name="TTrigger"></typeparam>
        public static void RegistTrigger<TTrigger>() where TTrigger : EventTrigger, new()
        {
            TriggerCreaters.Add(() => new TTrigger());
            Triggers.Add(new TTrigger());
        }

        /// <summary>
        /// 重置
        /// </summary>
        public static void Reset()
        {
            Triggers.Clear();
            foreach (var creater in TriggerCreaters)
                Triggers.Add(creater());
        }


        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="config"></param>
        /// <param name="property"></param>
        public static void OnPropertyChanged(NotificationObject config, string property)
        {
            if (NotificationObject.IsLoadingMode)
                return;
            var type = config.GetType();
            var scope = EventScope.CreateScope(config, property);
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType == type || type.IsSubclassOf(trigger.TargetType))
                        trigger.OnPropertyChanged(config, property);
                }
            }
        }

        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        public static void BeforePropertyChanged(NotificationObject config, string property, object oldValue, object newValue)
        {
            if (NotificationObject.IsLoadingMode)
                return;
            var type = config.GetType();
            var scope = EventScope.CreateScope(config, property);
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType == type || type.IsSubclassOf(trigger.TargetType))
                        trigger.BeforePropertyChanged(config, property, oldValue, newValue);
                }
            }
        }

        /// <summary>
        /// 构造事件处理
        /// </summary>
        /// <param name="config"></param>
        public static void OnLoad(NotificationObject config)
        {
            var type = config.GetType();
            var scope = EventScope.CreateScope(config, "_load_");
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType == type || type.IsSubclassOf(trigger.TargetType))
                        trigger.OnLoad(config);
                }
            }
        }

        /// <summary>
        /// 构造事件处理
        /// </summary>
        /// <param name="config"></param>
        public static void OnCreate(NotificationObject config)
        {
            if (NotificationObject.IsLoadingMode)
                return;
            var type = config.GetType();
            var scope = EventScope.CreateScope(config, "_create_");
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType == type || type.IsSubclassOf(trigger.TargetType))
                        trigger.OnCreate(config);
                }
            }
            OnLoad(config);
        }

        /// <summary>
        /// 加入事件处理
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parent"></param>
        public static void OnAdded(NotificationObject parent, NotificationObject config)
        {
            var type = config.GetType();
            var scope = EventScope.CreateScope(config, "_add_");
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType == type || type.IsSubclassOf(trigger.TargetType))
                        trigger.OnAdded(config, config);
                }
            }
        }
        /// <summary>
        /// 删除事件处理
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parent"></param>
        public static void OnRemoved(NotificationObject parent, NotificationObject config)
        {
            var type = config.GetType();
            var scope = EventScope.CreateScope(config, "_remove_");
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType == type || type.IsSubclassOf(trigger.TargetType))
                        trigger.OnRemoved(config, config);
                }
            }
        }
    }
}