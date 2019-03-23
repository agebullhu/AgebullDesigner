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
    partial class OrganizePositionApiController
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
            var combos = datas.Select(p => new EasyComboValues(p.Id, p.Position)).ToList();
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
        protected ApiPageData<OrganizePositionData> DefaultGetListData()
        {
            var filter = new LambdaItem<OrganizePositionData>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name="filter">筛选器</param>
        public void SetKeywordFilter(LambdaItem<OrganizePositionData> filter)
        {
            var keyWord = GetArg("keyWord");
            if (!string.IsNullOrEmpty(keyWord))
            {
                filter.AddAnd(p =>p.Position.Contains(keyWord) || 
                                   p.Department.Contains(keyWord) || 
                                   p.Role.Contains(keyWord) || 
                                   p.Memo.Contains(keyWord));
            }
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(OrganizePositionData data, FormConvert convert)
        {
            //数据
            data.Id = convert.ToLong("Id");
            data.DepartmentId = convert.ToLong("DepartmentId");
            data.BoundaryId = convert.ToLong("OrgId");
            //职位
            data.Position = convert.ToString("Position");
            //备注
            data.Memo = convert.ToString("Memo");
        }

        #endregion
    }
}