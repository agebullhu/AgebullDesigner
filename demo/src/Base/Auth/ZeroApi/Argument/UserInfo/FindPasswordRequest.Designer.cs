/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/11/16 15:24:38*/
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
using Agebull.EntityModel.Common;


using Newtonsoft.Json;
using Agebull.MicroZero.ZeroApis;

namespace Agebull.Common.Organizations
{
    /// <summary>
    /// 找回密码请求参数
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class FindPasswordRequest  : MicroZero.ZeroApis.IApiArgument
    {
        
        #region 属性

        /// <summary>
        /// 密码
        /// </summary>
        /// <remarks>
        /// 密码
        /// </remarks>
        [DataMember , JsonProperty("UserPassword", NullValueHandling = NullValueHandling.Ignore)]
        public string UserPassword
        {
            get;
            set;
        }

        /// <summary>
        /// 短信验证码
        /// </summary>
        /// <remarks>
        /// 短信验证码
        /// </remarks>
        [DataMember , JsonProperty("SMSVerificationCode", NullValueHandling = NullValueHandling.Ignore)]
        public string SMSVerificationCode
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
            return $@"UserPassword={HttpUtility.UrlEncode(UserPassword, Encoding.UTF8)}&SMSVerificationCode={HttpUtility.UrlEncode(SMSVerificationCode, Encoding.UTF8)}&MobilePhone={HttpUtility.UrlEncode(MobilePhone, Encoding.UTF8)}";
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
        bool MicroZero.ZeroApis.IApiArgument.Validate(out string message)
        {
            var result = Validate();
            message = result.Messages.LinkToString('；');
            return result.Succeed;
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
            if(string.IsNullOrWhiteSpace(UserPassword))
                 result.AddNoEmpty("密码",nameof(UserPassword));
            if(string.IsNullOrWhiteSpace(SMSVerificationCode))
                 result.AddNoEmpty("短信验证码",nameof(SMSVerificationCode));
            if(string.IsNullOrWhiteSpace(MobilePhone))
                 result.AddNoEmpty("注册的手机号",nameof(MobilePhone));
            ValidateEx(result);
            return result;
        }
        #endregion
    }
}