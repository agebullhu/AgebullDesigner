using Agebull.Common.Base;
using System.Collections.Generic;

namespace Agebull.EntityModel
{
    /// <summary>
    /// �����¼���Χ,��ֹͬ���¼�����
    /// </summary>
    public class NameEventScope : ScopeBase
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
        public static NameEventScope CreateScope(object config, string category, string property)
        {
            lock (Events)
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
                return new NameEventScope(config, name);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="config"></param>
        /// <param name="property"></param>
        private NameEventScope(object config, string property)
        {
            lock (Events)
            {
                _config = config;
                _property = property;
                Events[property].Add(config);
            }
        }

        /// <summary>������Դ</summary>
        protected override void OnDispose()
        {
            lock (Events)
            {
                Events[_property].Remove(_config);
            }
        }
    }
}