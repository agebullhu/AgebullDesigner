using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Agebull.EntityModel
{
    /// <summary>
    /// ȫ�ִ�����
    /// </summary>
    public static class GlobalTrigger
    {
        /// <summary>
        /// �̵߳�����
        /// </summary>
        public static Dispatcher Dispatcher
        {
            get;
            set;
        }
        /// <summary>
        /// ������
        /// </summary>
        private static readonly List<Func<EventTrigger>> TriggerCreaters = new List<Func<EventTrigger>>();

        /// <summary>
        /// ������
        /// </summary>
        private static readonly List<EventTrigger> Triggers = new List<EventTrigger>();

        /// <summary>
        /// ע�ᴥ����
        /// </summary>
        /// <typeparam name="TTrigger"></typeparam>
        public static void RegistTrigger<TTrigger>() where TTrigger : EventTrigger, new()
        {
            TriggerCreaters.Add(() => new TTrigger());
            Triggers.Add(new TTrigger());
        }

        /// <summary>
        /// ����
        /// </summary>
        public static void Reset()
        {
            Triggers.Clear();
            foreach (var creater in TriggerCreaters)
                Triggers.Add(creater());
        }


        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="config"></param>
        /// <param name="property"></param>
        public static void OnPropertyChanged(NotificationObject config, string property)
        {
            if (WorkContext.IsNoChangedNotify || config ==null)
                return;
            var type = config.GetType();
            var scope = EventScope.CreateScope(config, "Global", property);
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
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="config"></param>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        public static void BeforePropertyChanged(NotificationObject config, string property, object oldValue, object newValue)
        {
            if (WorkContext.IsNoChangedNotify || config == null)
                return;
            var type = config.GetType();
            var scope = EventScope.CreateScope(config, "Global_", property);
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType==null || trigger.TargetType == type || type.IsSubclassOf(trigger.TargetType))
                        trigger.BeforePropertyChanged(config, property, oldValue, newValue);
                }
            }
        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="config"></param>
        public static void OnLoad(NotificationObject config)
        {
            if (config == null)
                return;
            var type = config.GetType();
            var scope = EventScope.CreateScope(config, "Global", "OnLoad");
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
        /// �����¼�����
        /// </summary>
        /// <param name="config"></param>
        public static void OnCtor(NotificationObject config)
        {
            if (WorkContext.IsNoChangedNotify || config == null)
                return;
            Dispatcher.Invoke(() => OnCreate(config));
        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="config"></param>
        public static void OnCreate(NotificationObject config)
        {
            if (config == null)
                return;
            var type = config.GetType();
            var scope = EventScope.CreateScope(config, "Global", "OnCreate");
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType == null || trigger.TargetType == type || type.IsSubclassOf(trigger.TargetType))
                        trigger.OnCreate(config);
                }
            }
            if (WorkContext.IsNoChangedNotify)
                return;
            OnLoad(config);
        }
        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parent"></param>
        public static void OnAdded(NotificationObject parent, NotificationObject config)
        {
            if (config == null)
                return;
            var scope = EventScope.CreateScope(config, "Global", "OnAdded");
            if (scope == null)
                return;
            using (scope)
            {
                var type = config.GetType();
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType == null || trigger.TargetType == type || type.IsSubclassOf(trigger.TargetType))
                        trigger.OnAdded(config, config);
                }
            }
        }
        /// <summary>
        /// ɾ���¼�����
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parent"></param>
        public static void OnRemoved(NotificationObject parent, NotificationObject config)
        {
            if (config == null)
                return;
            var scope = EventScope.CreateScope(config, "Global", "OnRemoved");
            if (scope == null)
                return;
            using (scope)
            {
                var type = config.GetType();
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType == null || trigger.TargetType == type || type.IsSubclassOf(trigger.TargetType))
                        trigger.OnRemoved(config, config);
                }
            }
        }

        #region �������ɷ�Χ

        private static readonly object My = new object();

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        public static void OnCodeGeneratorBegin(NotificationObject config)
        {
            if (config == null)
                return;
            var scope = EventScope.CreateScope(My, "Global", "OnCodeGeneratorBegin");
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    trigger.OnCodeGeneratorBegin(config);
                }
            }
        }
        /// <summary>
        /// ��ɴ�������
        /// </summary>
        public static void OnCodeGeneratorEnd()
        {
            var scope = EventScope.CreateScope(My, "Global", "OnCodeGeneratorEnd");
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    trigger.OnCodeGeneratorEnd();
                }
            }
        }

        #endregion
    }
}