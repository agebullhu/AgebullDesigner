using System;
using System.Collections.Generic;

namespace Agebull.Common.DataModel
{
    /// <summary>
    /// ȫ�ִ�����
    /// </summary>
    public static class GlobalTrigger
    {
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
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="config"></param>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
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
        /// �����¼�����
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
        /// �����¼�����
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
        /// �����¼�����
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
        /// ɾ���¼�����
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