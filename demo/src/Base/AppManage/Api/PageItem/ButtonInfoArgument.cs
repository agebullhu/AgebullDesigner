using Newtonsoft.Json;


namespace Agebull.Common.AppManage
{
    /// <summary>
    /// 页面动作自动保存参数
    /// </summary>
    [JsonObject]
    public class ButtonInfoArgument
    {
        /// <summary>
        /// 页面标识
        /// </summary>
        [JsonProperty("pid")]
        public long PageId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("element")]
        public string Element { get; set; }
    }
}