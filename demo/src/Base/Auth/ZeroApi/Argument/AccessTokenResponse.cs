using System.Runtime.Serialization;
using Agebull.Common.OAuth;
using Newtonsoft.Json;

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 返回与AT相关的请求
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class AccessTokenResponse  
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

        /// <summary>
        /// 用户
        /// </summary>
        [JsonProperty]
        public LoginUserInfo Profile { get; set; }

    }
}