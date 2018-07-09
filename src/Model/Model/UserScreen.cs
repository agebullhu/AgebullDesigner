using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 保存用户的操作现场
    /// </summary>
    [JsonObject]
    public class UserScreen
    {
        /// <summary>
        /// 当前设计页面
        /// </summary>
        [JsonProperty]
        public string NowEditor { get; set; }
        /// <summary>
        /// 工作视图
        /// </summary>
        [JsonProperty]
        public string WorkView { get; set; }
    }
}