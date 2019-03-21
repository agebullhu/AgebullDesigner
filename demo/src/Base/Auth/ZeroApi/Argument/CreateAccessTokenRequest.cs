using Agebull.MicroZero.ZeroApis;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 生成AT的请求参数
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class CreateAccessTokenRequest : MicroZero.ZeroApis.IApiArgument
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonProperty]
        public long UserId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [JsonProperty]
        public string DeviceId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [JsonProperty]
        public string Type { get; set; }

        /// <summary>转为Form的文本</summary>
        /// <returns></returns>
        public string ToFormString()
        {
            return $"UserId={UserId}&DeviceId={DeviceId}&IsRegist={Type}";
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
