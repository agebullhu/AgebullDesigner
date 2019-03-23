
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Common;

using Agebull.MicroZero.ZeroApis;

namespace Agebull.Common.Organizations
{
    /// <summary>
    /// 登陆结果
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class LoginResponse : IApiResultData, IApiArgument
    {

        #region 属性


        /// <summary>
        /// 访问令牌
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("AccessToken", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"访问令牌")]
        public string AccessToken
        {
            get;
            set;
        }


        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("RefreshToken", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"刷新令牌")]
        public string RefreshToken
        {
            get;
            set;
        }


        /// <summary>
        /// 用户对象存储的用户信息
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("Profile", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"用户对象存储的用户信息")]
        public PersonPublishInfo Profile
        {
            get;
            set;
        }


        /// <summary>
        /// 用户的微信at
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("WechatAccessToken", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"用户的微信at")]
        public string WechatAccessToken
        {
            get;
            set;
        }


        /// <summary>
        /// 用户的微信at过期时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("WechatAccessTokenExpires", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MyDateTimeConverter)), DisplayName(@"用户的微信at过期时间")]
        public DateTime WechatAccessTokenExpires
        {
            get;
            set;
        }


        /// <summary>
        /// 用户的微信rt
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("WechatRefreshToken", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"用户的微信rt")]
        public string WechatRefreshToken
        {
            get;
            set;
        }


        /// <summary>
        /// 微信公共号AppId
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("WechatAppId", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"微信公共号AppId")]
        public string WechatAppId
        {
            get;
            set;
        }
        #endregion


        #region 数据校验
        /// <summary>数据校验</summary>
        /// <param name="message">返回的消息</param>
        /// <returns>成功则返回真</returns>
        bool MicroZero.ZeroApis.IApiArgument.Validate(out string message)
        {
            var result = Validate();
            message = result.Messages.LinkToString('；');
            return result.succeed;
        }

        /// <summary>
        /// 扩展校验
        /// </summary>
        /// <param name="result">结果存放处</param>
        partial void ValidateEx(ValidateResult result);

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <returns>数据校验对象</returns>
        public ValidateResult Validate()
        {
            var result = new ValidateResult();
            if (!string.IsNullOrWhiteSpace(AccessToken))
            {
                if (AccessToken.Length > 200)
                    result.Add("访问令牌", nameof(AccessToken), $"不能多于200个字");
            }
            if (!string.IsNullOrWhiteSpace(RefreshToken))
            {
                if (RefreshToken.Length > 200)
                    result.Add("刷新令牌", nameof(RefreshToken), $"不能多于200个字");
            }
            if (!string.IsNullOrWhiteSpace(WechatAccessToken))
            {
                if (WechatAccessToken.Length > 200)
                    result.Add("用户的微信at", nameof(WechatAccessToken), $"不能多于200个字");
            }
            if (!string.IsNullOrWhiteSpace(WechatRefreshToken))
            {
                if (WechatRefreshToken.Length > 200)
                    result.Add("用户的微信rt", nameof(WechatRefreshToken), $"不能多于200个字");
            }
            if (!string.IsNullOrWhiteSpace(WechatAppId))
            {
                if (WechatAppId.Length > 200)
                    result.Add("微信公共号AppId", nameof(WechatAppId), $"不能多于200个字");
            }
            ValidateEx(result);
            return result;
        }
        #endregion
    }
}
