using Agebull.MicroZero.ZeroApis;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Agebull.Common.Organizations
{
    /// <summary>
    /// AT校验请求参数
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class TokenArgument : MicroZero.ZeroApis.IApiArgument
    {
        /// <summary>
        /// AT
        /// </summary>
        [JsonProperty]
        public string Token { get; set; }


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
