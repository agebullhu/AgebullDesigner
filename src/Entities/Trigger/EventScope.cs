using System.Collections.Generic;
using Agebull.Common.Base;

namespace Agebull.Common.DataModel
{
    /// <summary>
    /// �¼���Χ,��ֹ�¼�����
    /// </summary>
    public class EventScope : ScopeBase
    {
        /// <summary>
        /// ��ǰ���ڴ�����¼�
        /// </summary>
        private static readonly Dictionary<NotificationObject, List<string>> Events = new Dictionary<NotificationObject, List<string>>();
        /// <summary>
        /// ��ǰ����
        /// </summary>
        private readonly NotificationObject _config;
        /// <summary>
        /// ��ǰ����
        /// </summary>
        private readonly string _property;
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="config">����</param>
        /// <param name="property">����</param>
        /// <returns>Ϊ�ձ�ʾ������,Ӧ�÷�������,��Ϊ����ʹ�������Χ</returns>
        public static EventScope CreateScope(NotificationObject config, string property)
        {
            List<string> events;
            if (Events.TryGetValue(config, out events))
            {
                if (events.Contains(property))
                    return null;
            }
            else
            {
                Events.Add(config, new List<string>());
            }
            return new EventScope(config, property);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="config"></param>
        /// <param name="property"></param>
        private EventScope(NotificationObject config, string property)
        {
            _config = config;
            _property = property;
            Events[config].Add(property);
        }

        /// <summary>������Դ</summary>
        protected override void OnDispose()
        {
            Events[_config].Remove(_property);
        }
    }
}