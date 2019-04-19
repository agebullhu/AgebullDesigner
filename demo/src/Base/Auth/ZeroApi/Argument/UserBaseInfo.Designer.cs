/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/11/28 16:44:12*/
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
    /// 用户基本信息
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class UserBaseInfo  : MicroZero.ZeroApis.IApiArgument
    {
        
        #region 属性

        /// <summary>
        /// 用户ID
        /// </summary>
        /// <remarks>
        /// 用户ID
        /// </remarks>
        [DataMember , JsonProperty("UserId", NullValueHandling = NullValueHandling.Ignore)]
        public long UserId
        {
            get;
            set;
        }

        /// <summary>
        /// 用户手机
        /// </summary>
        /// <remarks>
        /// 用户手机
        /// </remarks>
        [DataMember , JsonProperty("Phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone
        {
            get;
            set;
        }

        /// <summary>
        /// 开放标识
        /// </summary>
        /// <remarks>
        /// 开放标识
        /// </remarks>
        [DataMember , JsonProperty("OpenId", NullValueHandling = NullValueHandling.Ignore)]
        public string OpenId
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
            return $@"UserId={UserId}&Phone={HttpUtility.UrlEncode(Phone, Encoding.UTF8)}&OpenId={HttpUtility.UrlEncode(OpenId, Encoding.UTF8)}";
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
            if(string.IsNullOrWhiteSpace(Phone))
                 result.AddNoEmpty("用户手机",nameof(Phone));
            if(string.IsNullOrWhiteSpace(OpenId))
                 result.AddNoEmpty("开放标识",nameof(OpenId));
            ValidateEx(result);
            return result;
        }
        #endregion
    }
}