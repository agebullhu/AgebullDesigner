﻿/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/11/14 14:32:21*/
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
    /// 修改头像参数
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class AvatarRequest  : MicroZero.ZeroApis.IApiArgument
    {
        
        #region 属性

        /// <summary>
        /// 头像地址
        /// </summary>
        /// <remarks>
        /// 头像地址
        /// </remarks>
        [DataMember , JsonProperty("AvatarUrlPath", NullValueHandling = NullValueHandling.Ignore)]
        public string AvatarUrlPath
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
            return $@"AvatarUrlPath={HttpUtility.UrlEncode(AvatarUrlPath, Encoding.UTF8)}";
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
            if(string.IsNullOrWhiteSpace(AvatarUrlPath))
                 result.AddNoEmpty("头像地址",nameof(AvatarUrlPath));
            ValidateEx(result);
            return result;
        }
        #endregion
    }
}