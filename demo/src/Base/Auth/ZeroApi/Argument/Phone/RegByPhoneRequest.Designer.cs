/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/11/14 14:32:21*/
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.Web;
using Agebull.Common;
using Gboxt.Common.DataModel;


using Newtonsoft.Json;

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 基于手机号的注册请求参数
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class RegByPhoneRequest  : IApiArgument
    {
        
        #region 属性

        /// <summary>
        /// 短信验证码
        /// </summary>
        /// <remarks>
        /// 短信验证码（6位数字）
        /// </remarks>
        [DataMember , JsonProperty("SMSVerificationCode", NullValueHandling = NullValueHandling.Ignore)]
        public string SMSVerificationCode
        {
            get;
            set;
        }

        /// <summary>
        /// 登陆密码
        /// </summary>
        /// <remarks>
        /// 登陆密码（6-16）
        /// </remarks>
        [DataMember , JsonProperty("UserPassword", NullValueHandling = NullValueHandling.Ignore)]
        public string UserPassword
        {
            get;
            set;
        }

        /// <summary>
        /// 活动跟踪码
        /// </summary>
        /// <remarks>
        /// 活动跟踪码
        /// </remarks>
        [DataMember , JsonProperty("TraceMark", NullValueHandling = NullValueHandling.Ignore)]
        public string TraceMark
        {
            get;
            set;
        }

        /// <summary>
        /// 渠道
        /// </summary>
        /// <remarks>
        /// 渠道
        /// </remarks>
        [DataMember , JsonProperty("Channel", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel
        {
            get;
            set;
        }

        /// <summary>
        /// 注册的手机号
        /// </summary>
        /// <remarks>
        /// 注册的手机号
        /// </remarks>
        [DataMember , JsonProperty("MobilePhone", NullValueHandling = NullValueHandling.Ignore)]
        public string MobilePhone
        {
            get;
            set;
        }
        #endregion
        
        #region 转为符合HTTP协议的FORM的文本
        /// <summary>转为符合HTTP协议的FORM的文本</summary>
        /// <returns>符合HTTP协议的FORM的文本</returns>
        public string ToFormString()
        {
            return $@"SMSVerificationCode={HttpUtility.UrlEncode(SMSVerificationCode, Encoding.UTF8)}&UserPassword={HttpUtility.UrlEncode(UserPassword, Encoding.UTF8)}&TraceMark={HttpUtility.UrlEncode(TraceMark, Encoding.UTF8)}&Channel={HttpUtility.UrlEncode(Channel, Encoding.UTF8)}&MobilePhone={HttpUtility.UrlEncode(MobilePhone, Encoding.UTF8)}";
        }

        /// <summary>到文本</summary>
        /// <returns>文本</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return ToFormString();
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
            if(string.IsNullOrWhiteSpace(SMSVerificationCode))
                 result.AddNoEmpty("短信验证码",nameof(SMSVerificationCode));
            if(string.IsNullOrWhiteSpace(UserPassword))
                 result.AddNoEmpty("登陆密码",nameof(UserPassword));
            if(string.IsNullOrWhiteSpace(MobilePhone))
                 result.AddNoEmpty("注册的手机号",nameof(MobilePhone));
            ValidateEx(result);
            return result;
        }
        #endregion
    }
}