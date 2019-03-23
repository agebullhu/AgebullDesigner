/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/22 10:27:49*/
#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Agebull.Common.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.Context;
using Agebull.Common.Ioc;
using Agebull.Common.OAuth;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.EasyUI;
using Agebull.MicroZero;
using Agebull.MicroZero.ZeroApis;

using Agebull.Common.OAuth;

using Agebull.Common.Organizations;
using Agebull.Common.Organizations.BusinessLogic;
using Agebull.Common.Organizations.DataAccess;
#endregion

namespace Agebull.Common.Organizations.WebApi.Entity
{
    partial class PersonApiController
    {
        #region 设计器命令


        #endregion

        #region 基本扩展
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected ApiPageData<PersonData> DefaultGetListData()
        {
            var filter = new LambdaItem<PersonData>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name="filter">筛选器</param>
        public void SetKeywordFilter(LambdaItem<PersonData> filter)
        {
            var keyWord = GetArg("keyWord");
            if (!string.IsNullOrEmpty(keyWord))
            {
                filter.AddAnd(p =>p.NickName.Contains(keyWord) || 
                                   p.IdCard.Contains(keyWord) || 
                                   p.RealName.Contains(keyWord) || 
                                   p.AvatarUrl.Contains(keyWord) || 
                                   p.PhoneNumber.Contains(keyWord) || 
                                   p.Nation.Contains(keyWord) || 
                                   p.Tel.Contains(keyWord) || 
                                   p.Email.Contains(keyWord) || 
                                   p.HomeAddress.Contains(keyWord));
            }
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(PersonData data, FormConvert convert)
        {
            //-
            data.Icon_Base64 = convert.ToString("icon");
            data.NickName = convert.ToString("NickName");
            data.IdCard = convert.ToString("IdCard");
            data.CertType = (CardType)convert.ToInteger("certType");
            data.RealName = convert.ToString("RealName");
            data.AvatarUrl = convert.ToString("AvatarUrl");
            data.PhoneNumber = convert.ToString("phoneNumber");
            data.Birthday = convert.ToDateTime("birthday");
            data.Nation = convert.ToString("nation");
            data.Tel = convert.ToString("tel");
            data.Email = convert.ToString("email");
            data.HomeAddress = convert.ToString("homeAddress");
        }

        #endregion
    }
}