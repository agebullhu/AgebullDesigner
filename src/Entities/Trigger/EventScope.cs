using System.Collections.Generic;
using Agebull.Common.Base;

namespace Agebull.EntityModel
{
    /// <summary>
    /// 命名事件范围,防止同名事件重入
    /// </summary>
    public class NameEventScope : ScopeBase
    {
        /// <summary>
        /// 当前正在处理的事件
        /// </summary>
        private static readonly Dictionary<string, List<object>> Events = new Dictionary<string, List<object>>();
        /// <summary>
        /// 当前配置
        /// </summary>
        private readonly object _config;
        /// <summary>
        /// 当前属性
        /// </summary>
        private readonly string _property;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config">配置</param>
        /// <param name="category">分类</param>
        /// <param name="property">属性</param>
        /// <returns>为空表示已重入,应该放弃处理,不为空则使用这个范围</returns>
        public static NameEventScope CreateScope(object config,string category, string property)
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
        /// 构造
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

        /// <summary>清理资源</summary>
        protected override void OnDispose()
        {
            lock (Events)
            {
                Events[_property].Remove(_config);
            }
        }
    }
}