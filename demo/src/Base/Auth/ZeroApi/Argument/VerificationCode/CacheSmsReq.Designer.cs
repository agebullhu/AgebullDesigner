
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
using Agebull.Common.DataModel;
using Gboxt.Common.DataModel;
using Agebull.Common.WebApi;



namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 缓存短信验证码参数
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class CacheSmsReq : IApiResultData, IApiArgument
    {

        #region 属性


        /// <summary>
        /// 手机号
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"手机号")]
        public string Phone
        {
            get;
            set;
        }


        /// <summary>
        /// 短信验证码
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("smsVerificationCode", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"短信验证码")]
        public string SmsVerificationCode
        {
            get;
            set;
        }
        #endregion


        #region 数据校验
        /// <summary>数据校验</summary>
        /// <param name="message">返回的消息</param>
        /// <returns>成功则返回真</returns>
        bool IApiArgument.Validate(out string message)
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
            if (!string.IsNullOrWhiteSpace(Phone))
            {
                if (Phone.Length > 200)
                    result.Add("手机号", nameof(Phone), $"不能多于200个字");
            }
            if (!string.IsNullOrWhiteSpace(SmsVerificationCode))
            {
                if (SmsVerificationCode.Length > 200)
                    result.Add("短信验证码", nameof(SmsVerificationCode), $"不能多于200个字");
            }
            ValidateEx(result);
            return result;
        }
        #endregion
    }
}
