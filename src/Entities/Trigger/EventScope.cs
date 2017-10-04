using System.Collections.Generic;
using Agebull.Common.Base;

namespace Agebull.Common.DataModel
{
    /// <summary>
    /// 事件范围,防止事件重入
    /// </summary>
    public class EventScope : ScopeBase
    {
        /// <summary>
        /// 当前正在处理的事件
        /// </summary>
        private static readonly Dictionary<NotificationObject, List<string>> Events = new Dictionary<NotificationObject, List<string>>();
        /// <summary>
        /// 当前配置
        /// </summary>
        private readonly NotificationObject _config;
        /// <summary>
        /// 当前属性
        /// </summary>
        private readonly string _property;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config">配置</param>
        /// <param name="property">属性</param>
        /// <returns>为空表示已重入,应该放弃处理,不为空则使用这个范围</returns>
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
        /// 构造
        /// </summary>
        /// <param name="config"></param>
        /// <param name="property"></param>
        private EventScope(NotificationObject config, string property)
        {
            _config = config;
            _property = property;
            Events[config].Add(property);
        }

        /// <summary>清理资源</summary>
        protected override void OnDispose()
        {
            Events[_config].Remove(_property);
        }
    }
}