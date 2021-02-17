using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Agebull.EntityModel
{
    /// <summary>
    /// 全局触发器
    /// </summary>
    public static class GlobalTrigger
    {
        /// <summary>
        /// 线程调度器
        /// </summary>
        public static Dispatcher Dispatcher
        {
            get;
            set;
        }

        /// <summary>
        /// 触发器
        /// </summary>
        private static readonly List<Func<IEventTrigger>> TriggerCreaters = new List<Func<IEventTrigger>>();

        /// <summary>
        /// 触发器
        /// </summary>
        private static readonly List<IEventTrigger> Triggers = new List<IEventTrigger>();

        /// <summary>
        /// 注册触发器
        /// </summary>
        /// <typeparam name="TTrigger"></typeparam>
        public static void RegistTrigger<TTrigger>() where TTrigger : IEventTrigger, new()
        {
            TriggerCreaters.Add(() => new TTrigger());
            Triggers.Add(new TTrigger());
        }

        /// <summary>
        /// 注册触发器
        /// </summary>
        public static void RegistTrigger(Type type)
        {
            TriggerCreaters.Add(() => ReflectionExtend.Generate(type) as IEventTrigger);
            Triggers.Add(ReflectionExtend.Generate(type) as IEventTrigger);
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
        public static void OnPropertyChanged(object config, string property)
        {
            if (WorkContext.IsNoChangedNotify || config == null)
                return;
            var type = config.GetType();
            var scope = NameEventScope.CreateScope(config, "Global", property);
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType == null || trigger.TargetType == type || type.IsSubclassOf(trigger.TargetType))
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
        public static void BeforePropertyChanged(object config, string property, object oldValue, object newValue)
        {
            if (WorkContext.IsNoChangedNotify || config == null)
                return;
            var type = config.GetType();
            var scope = NameEventScope.CreateScope(config, "Global", property);
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType == null || trigger.TargetType == type || type.IsSubclassOf(trigger.TargetType))
                        trigger.BeforePropertyChanged(config, property, oldValue, newValue);
                }
            }
        }

        /// <summary>
        /// 构造事件处理
        /// </summary>
        /// <param name="config"></param>
        public static void OnLoad(object config)
        {
            if (config == null)
                return;
            var type = config.GetType();
            var scope = NameEventScope.CreateScope(config, "Global", nameof(OnLoad));
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType == null || trigger.TargetType == type || type.IsSubclassOf(trigger.TargetType))
                        trigger.OnLoad(config);
                }
            }
        }

        /// <summary>
        /// 构造事件处理
        /// </summary>
        /// <param name="config"></param>
        public static void OnCtor(object config)
        {
            if (WorkContext.IsNoChangedNotify || config == null)
                return;
            Dispatcher.Invoke(() => OnCreate(config));
        }

        /// <summary>
        /// 构造事件处理
        /// </summary>
        /// <param name="config"></param>
        public static void OnCreate(object config)
        {
            if (config == null)
                return;
            var scope = NameEventScope.CreateScope(config, "Global", nameof(OnCreate));
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType.IsFrientType(config))
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
        public static void OnAdded(object parent, object config)
        {
            if (config == null)
                return;
            var scope = NameEventScope.CreateScope(config, "Global", nameof(OnAdded));
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType.IsFrientType(config))
                        trigger.OnAdded(config, config);
                }
            }
            OnLoad(config);
        }
        /// <summary>
        /// 删除事件处理
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parent"></param>
        public static void OnRemoved(object parent, object config)
        {
            if (config == null)
                return;
            var scope = NameEventScope.CreateScope(config, "Global", nameof(OnRemoved));
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType.IsFrientType(config))
                        trigger.OnRemoved(config, config);
                }
            }
        }

        #region 代码生成范围

        /// <summary>
        /// 规整对象
        /// </summary>
        public static void Regularize(object config)
        {
            if (config == null)
                return;
            var scope = NameEventScope.CreateScope(config, "Global", nameof(OnCodeGeneratorBegin));
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType.IsFrientType(config))
                        trigger.Regularize(config);
                }
            }
        }

        /// <summary>
        /// 规整对象
        /// </summary>
        public static void DoRegularize(object config)
        {
            if (config == null)
                return;
            foreach (var trigger in Triggers)
            {
                if (trigger.TargetType.IsFrientType(config))
                    trigger.Regularize(config);
            }
        }
        /// <summary>
        /// 开始代码生成
        /// </summary>
        public static void OnCodeGeneratorBegin(object config)
        {
            if (config == null)
                return;
            var scope = NameEventScope.CreateScope(config, "Global", nameof(OnCodeGeneratorBegin));
            if (scope == null)
                return;
            using (scope)
            {
                //DoRegularize(config);
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType.IsFrientType(config))
                        trigger.OnCodeGeneratorBegin(config);
                }
            }
        }

        private static readonly object My = new object();

        /// <summary>
        /// 完成代码生成
        /// </summary>
        public static void OnCodeGeneratorEnd(object config)
        {
            var scope = NameEventScope.CreateScope(config, "Global", nameof(OnCodeGeneratorBegin));
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    trigger.OnCodeGeneratorEnd(config);
                }
            }
        }

        #endregion
    }
}