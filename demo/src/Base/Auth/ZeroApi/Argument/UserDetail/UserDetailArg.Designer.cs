
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

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 获取用户信息参数
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class UserDetailArg : IApiResultData, IApiArgument
    {

        #region 属性


        /// <summary>
        /// UserId
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"UserId")]
        public long UserId
        {
            get;
            set;
        }


        /// <summary>
        /// OpenId
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("openId", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"OpenId")]
        public string OpenId
        {
            get;
            set;
        }


        /// <summary>
        /// access token
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("at", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"access token")]
        public string At
        {
            get;
            set;
        }


        /// <summary>
        /// 返回数据的描述
        /// </summary>
        /// <remarks>
        /// （标志位，可同时传递多个，例如6）2：微信用户信息   4：当前就诊人信息
        /// </remarks>
        [DataRule(CanNull = true)]
        [DataMember, JsonProperty("dataDesc", NullValueHandling = NullValueHandling.Ignore), DisplayName(@"返回数据的描述")]
        public long DataDesc
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
            ValidateEx(result);
            return result;
        }
        #endregion
    }
}
