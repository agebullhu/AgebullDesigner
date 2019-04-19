
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
    /// 缓存图片验证码参数
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class CacheImgReq : IApiResultData, IApiArgument
    {

        #region 属性


        /// <summary>
        /// RpcToken
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("rpcToken", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"RpcToken")]
        public string RpcToken
        {
            get;
            set;
        }


        /// <summary>
        /// 验证码
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("verificationCode", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"验证码")]
        public string VerificationCode
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
            if (!string.IsNullOrWhiteSpace(RpcToken))
            {
                if (RpcToken.Length > 200)
                    result.Add("RpcToken", nameof(RpcToken), $"不能多于200个字");
            }
            if (!string.IsNullOrWhiteSpace(VerificationCode))
            {
                if (VerificationCode.Length > 200)
                    result.Add("验证码", nameof(VerificationCode), $"不能多于200个字");
            }
            ValidateEx(result);
            return result;
        }
        #endregion
    }
}
