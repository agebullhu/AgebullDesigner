/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 23:21:23*/
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



using Agebull.Common.OAuth;
using Agebull.Common.OAuth.BusinessLogic;
using Agebull.Common.OAuth.DataAccess;
#endregion

namespace Agebull.Common.OAuth.WebApi.Entity
{
    partial class OrganizationApiController
    {
        #region 设计器命令

        /// <summary>下拉列表</summary>
        /// <returns></returns>
        [HttpPost]
        [Route("edit/combo")]
        public ApiArrayResult<EasyComboValues> ComboData()
        {
            GlobalContext.Current.IsManageMode = false;
            var datas = Business.All();
            var combos = datas.Select(p => new EasyComboValues(p.Id, p.ShortName)).ToList();
            combos.Insert(0,new EasyComboValues(0, "-"));
            return new ApiArrayResult<EasyComboValues>
            {
               Success = true,
               ResultData = combos
            };
        }

        #endregion

        #region 基本扩展
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected ApiPageData<OrganizationData> DefaultGetListData()
        {
            var filter = new LambdaItem<OrganizationData>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name="filter">筛选器</param>
        public void SetKeywordFilter(LambdaItem<OrganizationData> filter)
        {
            var keyWord = GetArg("keyWord");
            if (!string.IsNullOrEmpty(keyWord))
            {
                filter.AddAnd(p =>p.Code.Contains(keyWord) || 
                                   p.FullName.Contains(keyWord) || 
                                   p.ShortName.Contains(keyWord) || 
                                   p.TreeName.Contains(keyWord) || 
                                   p.Memo.Contains(keyWord) || 
                                   p.SuperOrgcode.Contains(keyWord) || 
                                   p.ManagOrgcode.Contains(keyWord) || 
                                   p.ManagOrgname.Contains(keyWord) || 
                                   p.CityCode.Contains(keyWord) || 
                                   p.DistrictCode.Contains(keyWord) || 
                                   p.OrgAddress.Contains(keyWord) || 
                                   p.LawPersonname.Contains(keyWord) || 
                                   p.LawPersontel.Contains(keyWord) || 
                                   p.ContactName.Contains(keyWord) || 
                                   p.ContactTel.Contains(keyWord) || 
                                   p.Area.Contains(keyWord));
            }
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(OrganizationData data, FormConvert convert)
        {
            //数据
            data.Id = convert.ToLong("Id");
            data.Type = (OrganizationType)convert.ToInteger("Type");
            data.Code = convert.ToString("Code");
            data.FullName = convert.ToString("FullName");
            data.ShortName = convert.ToString("ShortName");
            data.TreeName = convert.ToString("TreeName");
            data.OrgLevel = convert.ToInteger("OrgLevel");
            data.LevelIndex = convert.ToInteger("LevelIndex");
            data.ParentId = convert.ToLong("parentId");
            data.BoundaryId = convert.ToLong("OrgId");
            data.Memo = convert.ToString("Memo");
            data.AreaId = convert.ToLong("areaId");
            //-
            data.SuperOrgcode = convert.ToString("super_orgcode");
            data.ManagOrgcode = convert.ToString("manag_orgcode");
            data.ManagOrgname = convert.ToString("manag_orgname");
            data.CityCode = convert.ToString("city_code");
            data.DistrictCode = convert.ToString("district_code");
            data.OrgAddress = convert.ToString("org_address");
            data.LawPersonname = convert.ToString("law_personname");
            data.LawPersontel = convert.ToString("law_persontel");
            data.ContactName = convert.ToString("contact_name");
            data.ContactTel = convert.ToString("contact_tel");
        }

        #endregion
    }
}