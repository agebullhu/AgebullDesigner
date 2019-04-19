using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Agebull.Common.Organizations
{
    /// <summary>
    /// 请求分配DeviceId请求参数
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class DeviceArgument : MicroZero.ZeroApis.IApiArgument
    {
        /// <summary>
        /// 请求者应用
        /// </summary>
        [JsonProperty("app")]
        public string App { get; set; }

        /// <summary>
        /// 请求者应用标识
        /// </summary>
        [JsonProperty("appKey")]
        public string AppKey { get; set; }

        /// <summary>
        /// 请求者的设备号
        /// </summary>
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        /// <summary>
        /// 请求者操作系统
        /// </summary>
        [JsonProperty("os")]
        public string Os { get; set; }

        /// <summary>
        /// 请求者操作系统
        /// </summary>
        [JsonIgnore]
        public string Browser { get; set; }

        
        /// <summary>
        /// 设备信息
        /// </summary>
        [JsonIgnore]
        public string DeviceInformation { get; set; }


        /// <summary>
        /// 当前请求的实现版本号
        /// </summary>
        [JsonIgnore]
        public int Version { get; set; }

        /// <summary>
        /// 转为Form的文本
        /// </summary>
        /// <returns></returns>
        public string ToFormString()
        {
            StringBuilder code = new StringBuilder();
            code.Append($"os={Os}&app={App}&appKey={AppKey}&deviceId={DeviceId}");
            return code.ToString();
        }

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