using Newtonsoft.Json;
using System.Collections.Generic;

namespace Agebull.Common.AppManage
{
    /// <summary>
    /// 页面信息
    /// </summary>
    [JsonObject]
    public class PageInfo
    {
        /// <summary>
        /// 无内部权限控制,内部控制全部可访问
        /// </summary>
        [JsonProperty("allButton")]
        public bool AllAction { get; set; }
        /// <summary>
        /// 页面标识
        /// </summary>
        [JsonProperty("pageId")]
        public long PageId { get; set; }
        /// <summary>
        /// 历史查询参数
        /// </summary>
        [JsonProperty("preQueryArgs")]
        public string HistoryQueryArgs { get; set; }
        /// <summary>
        /// 允许访问的页面内行为
        /// </summary>
        [JsonProperty("buttons")]
        public List<string> RoleAllowActions { get; set; }
    }
}