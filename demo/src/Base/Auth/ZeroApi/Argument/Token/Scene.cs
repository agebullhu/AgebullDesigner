using Agebull.MicroZero.ZeroApis;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 场景类型
    /// </summary>
    public enum AppSceneType
    {
        /// <summary>
        /// 未知
        /// </summary>
        None,
        /// <summary>
        /// 页面
        /// </summary>
        Page,
        /// <summary>
        /// 流程
        /// </summary>
        Flow,
        /// <summary>
        /// 任务
        /// </summary>
        Task
    }

    /// <summary>
    /// 开始场景请求参数
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class BeginSceneArgument : IApiArgument
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        [JsonProperty("app")]
        public string AppName { get; set; }

        /// <summary>
        /// 场景名称
        /// </summary>
        [JsonProperty("scene")]
        public string SceneName { get; set; }

        /// <summary>
        /// 场景类型
        /// </summary>
        [JsonProperty("type")]
        public AppSceneType SceneType { get; set; }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <param name="message">返回的消息</param>
        /// <returns>成功则返回真</returns>
        public bool Validate(out string message)
        {
            message = null;
            return true;
        }
    }

    /// <summary>
    /// 场景校验请求参数
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ScreenTokenArgument : IApiArgument
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        [JsonProperty("at")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 场景令牌
        /// </summary>
        [JsonProperty("st")]
        public string ScreenToken { get; set; }

        /// <summary>
        /// Api名称
        /// </summary>
        [JsonProperty("api")]
        public string ApiName { get; set; }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <param name="message">返回的消息</param>
        /// <returns>成功则返回真</returns>
        public bool Validate(out string message)
        {
            message = null;
            return true;
        }
    }

}
