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
        private static readonly List<Func<IEventTrigger>> TriggerCreaters = new();

        /// <summary>
        /// ������
        /// </summary>
        private static readonly List<IEventTrigger> Triggers = new();

        /// <summary>
        /// ע�ᴥ����
        /// </summary>
        /// <typeparam name="TTrigger"></typeparam>
        public static void RegistTrigger<TTrigger>() where TTrigger : IEventTrigger, new()
        {
            TriggerCreaters.Add(() => new TTrigger());
            Triggers.Add(new TTrigger());
        }

        /// <summary>
        /// ע�ᴥ����
        /// </summary>
        public static void RegistTrigger(Type type)
        {
            TriggerCreaters.Add(() => ReflectionExtend.Generate(type) as IEventTrigger);
            Triggers.Add(ReflectionExtend.Generate(type) as IEventTrigger);
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
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="config"></param>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
        public static void BeforePropertyChange(object config, string property, object oldValue, object newValue)
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
                        trigger.BeforePropertyChange(config, property, oldValue, newValue);
                }
            }
        }

        /// <summary>
        /// �����¼�����
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
                using var s = WorkModelScope.CreateScope(WorkModel.Loding);
                foreach (var trigger in Triggers.ToArray())
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
        public static void OnCtor(object config)
        {
            if (WorkContext.IsNoChangedNotify || config == null)
                return;

            Dispatcher.Invoke(() => OnCreate(config));
        }

        /// <summary>
        /// �����¼�����
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
        /// �����¼�����
        /// </summary>
        /// <param name="config"></param>
        /// <param name="parent"></param>
        public static void OnAdded(object parent, object config)
        {
            if (parent == null)
                return;
            var scope = NameEventScope.CreateScope(parent, "Global", nameof(OnAdded));
            if (scope == null)
                return;
            using (scope)
            {
                foreach (var trigger in Triggers)
                {
                    if (trigger.TargetType.IsFrientType(config))
                        trigger.OnAdded(parent, config);
                }
            }
            OnLoad(config);
        }
        /// <summary>
        /// ɾ���¼�����
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
                        trigger.OnRemoved(parent, config);
                }
            }
        }

        #region �������ɷ�Χ

        /// <summary>
        /// ��������
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
        /// ��������
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
        /// ��ʼ��������
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

        private static readonly object My = new();

        /// <summary>
        /// ��ɴ�������
        /// </summary>
        public static void OnCodeGeneratorEnd(object config)
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
                        trigger.OnCodeGeneratorEnd(config);
                }
            }
        }

        #endregion
    }
}