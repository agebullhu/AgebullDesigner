using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Agebull.Common.Organizations
{
    /// <summary>
    /// 刷新AT请求参数
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class RefreshAccessTokenRequest : MicroZero.ZeroApis.IApiArgument
    {
        /// <summary>
        /// AT
        /// </summary>
        [JsonProperty]
        public string AccessToken { get; set; }
        /// <summary>
        /// RT
        /// </summary>
        [JsonProperty]
        public string RefreshToken { get; set; }


        /// <summary>转为Form的文本</summary>
        /// <returns></returns>
        public string ToFormString()
        {
            //return $"RefreshToken={RefreshToken}";
            return $"AccessToken={AccessToken}&RefreshToken={RefreshToken}";
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
