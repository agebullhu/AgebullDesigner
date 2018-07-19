using System.Collections.Generic;
using Agebull.Common.Base;

namespace Agebull.EntityModel
{
    /// <summary>
    /// �¼���Χ,��ֹ�¼�����
    /// </summary>
    public class EventScope : ScopeBase
    {
        /// <summary>
        /// ��ǰ���ڴ�����¼�
        /// </summary>
        private static readonly Dictionary<string, List<object>> Events = new Dictionary<string, List<object>>();
        /// <summary>
        /// ��ǰ����
        /// </summary>
        private readonly object _config;
        /// <summary>
        /// ��ǰ����
        /// </summary>
        private readonly string _property;

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="config">����</param>
        /// <param name="category">����</param>
        /// <param name="property">����</param>
        /// <returns>Ϊ�ձ�ʾ������,Ӧ�÷�������,��Ϊ����ʹ�������Χ</returns>
        public static EventScope CreateScope(object config,string category, string property)
        {
            string name = $"{category}.{property}";
            if (Events.TryGetValue(name, out var configs))
            {
                if (configs.Contains(config))
                    return null;
            }
            else
            {
                Events.Add(name, new List<object>());
            }
            return new EventScope(config, name);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="config"></param>
        /// <param name="property"></param>
        private EventScope(object config, string property)
        {
            _config = config;
            _property = property;
            Events[property].Add(config);
        }

        /// <summary>������Դ</summary>
        protected override void OnDispose()
        {
            Events[_property].Remove(_config);
        }
    }
}