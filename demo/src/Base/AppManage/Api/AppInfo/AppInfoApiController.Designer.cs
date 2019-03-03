/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 17:27:14*/
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
using Agebull.Common.DataModel;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.Rpc;
using Agebull.Common.WebApi;
using Agebull.ZeroNet.ZeroApi;
using Agebull.Common.DataModel.WebUI;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;

using Agebull.Common.WebApi.Auth;

using Agebull.Common.AppManage;
using Agebull.Common.AppManage.BusinessLogic;
using Agebull.Common.AppManage.DataAccess;
#endregion

namespace Agebull.Common.AppManage.WebApi.Entity
{
    partial class AppInfoApiController
    {
        #region 设计器命令


        #endregion

        #region 基本扩展
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected ApiPageData<AppInfoData> DefaultGetListData()
        {
            var filter = new LambdaItem<AppInfoData>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name="filter">筛选器</param>
        public void SetKeywordFilter(LambdaItem<AppInfoData> filter)
        {
            var keyWord = GetArg("keyWord");
            if (!string.IsNullOrEmpty(keyWord))
            {
                filter.AddAnd(p =>p.Organization.Contains(keyWord) || 
                                   p.ShortName.Contains(keyWord) || 
                                   p.FullName.Contains(keyWord) || 
                                   p.AppId.Contains(keyWord) || 
                                   p.ManagOrgcode.Contains(keyWord) || 
                                   p.ManagOrgname.Contains(keyWord) || 
                                   p.CityCode.Contains(keyWord) || 
                                   p.DistrictCode.Contains(keyWord) || 
                                   p.OrgAddress.Contains(keyWord) || 
                                   p.LawPersonname.Contains(keyWord) || 
                                   p.LawPersontel.Contains(keyWord) || 
                                   p.ContactName.Contains(keyWord) || 
                                   p.ContactTel.Contains(keyWord) || 
                                   p.SuperOrgcode.Contains(keyWord) || 
                                   p.UpdateUserid.Contains(keyWord) || 
                                   p.UpdateUsername.Contains(keyWord) || 
                                   p.Memo.Contains(keyWord));
            }
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(AppInfoData data, FormConvert convert)
        {
            //-
            data.Id = convert.ToLong("id");
            data.OrgId = convert.ToLong("orgId");
            data.ShortName = convert.ToString("shortName");
            data.FullName = convert.ToString("fullName");
            data.Classify = (ClassifyType)convert.ToInteger("classify");
            data.AppId = convert.ToString("appId");
            data.ManagOrgcode = convert.ToString("managOrgcode");
            data.ManagOrgname = convert.ToString("managOrgname");
            data.CityCode = convert.ToString("cityCode");
            data.DistrictCode = convert.ToString("districtCode");
            data.OrgAddress = convert.ToString("orgAddress");
            data.LawPersonname = convert.ToString("lawPersonname");
            data.LawPersontel = convert.ToString("lawPersontel");
            data.ContactName = convert.ToString("contactName");
            data.ContactTel = convert.ToString("contactTel");
            data.SuperOrgcode = convert.ToString("superOrgcode");
            data.UpdateDate = convert.ToDateTime("updateDate");
            data.UpdateUserid = convert.ToString("updateUserid");
            data.UpdateUsername = convert.ToString("updateUsername");
            data.Memo = convert.ToString("memo");
        }

        #endregion
    }
}