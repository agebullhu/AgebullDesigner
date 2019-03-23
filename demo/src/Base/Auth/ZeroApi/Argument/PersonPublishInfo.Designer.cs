/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/11/16 14:16:42*/
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

namespace Agebull.Common.Organizations
{
    /// <summary>
    /// 用户的公开信息
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class PersonPublishInfo  : MicroZero.ZeroApis.IApiArgument
    {
        
        #region 属性

        /// <summary>
        /// 头像
        /// </summary>
        /// <remarks>
        /// 头像
        /// </remarks>
        [DataMember , JsonProperty("AvatarUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string AvatarUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 昵称
        /// </summary>
        /// <remarks>
        /// 昵称
        /// </remarks>
        [DataMember, JsonProperty("IsRegist", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsRegist
        {
            get;
            set;
        }

        /// <summary>
        /// 昵称
        /// </summary>
        /// <remarks>
        /// 昵称
        /// </remarks>
        [DataMember , JsonProperty("NickName", NullValueHandling = NullValueHandling.Ignore)]
        public string NickName
        {
            get;
            set;
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        /// <remarks>
        /// 所在县Id
        /// </remarks>
        [DataMember , JsonProperty("PhoneNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber
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
            return $@"AvatarUrl={HttpUtility.UrlEncode(AvatarUrl, Encoding.UTF8)}&NickName={HttpUtility.UrlEncode(NickName, Encoding.UTF8)}&PhoneNumber={HttpUtility.UrlEncode(PhoneNumber, Encoding.UTF8)}";
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
            if(string.IsNullOrWhiteSpace(AvatarUrl))
                 result.AddNoEmpty("头像",nameof(AvatarUrl));
            else 
            {
                if(AvatarUrl.Length > 200)
                    result.Add("头像",nameof(AvatarUrl),$"不能多于200个字");
            }
            if(string.IsNullOrWhiteSpace(NickName))
                 result.AddNoEmpty("昵称",nameof(NickName));
            else 
            {
                if(NickName.Length > 100)
                    result.Add("昵称",nameof(NickName),$"不能多于100个字");
            }
            if(string.IsNullOrWhiteSpace(PhoneNumber))
                 result.AddNoEmpty("手机号码",nameof(PhoneNumber));
            else 
            {
                if(PhoneNumber.Length > 10)
                    result.Add("手机号码",nameof(PhoneNumber),$"不能多于10个字");
            }
            ValidateEx(result);
            return result;
        }
        #endregion
    }
}