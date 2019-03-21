/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/9/2 18:26:24*/
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
using Agebull.EntityModel.Common;
using Agebull.EntityModel.MySql.BusinessLogic;


using Agebull.EntityModel.Common;
using Agebull.EntityModel.MySql;

using Agebull.Common.OAuth;

using Agebull.Common.OAuth;
using Agebull.Common.OAuth.BusinessLogic;
using Agebull.Common.OAuth.DataAccess;
using Agebull.MicroZero.ZeroApis;

namespace Agebull.Common.OAuth.WebApi.Entity
{
    partial class OrganizePositionApiController
    {
        #region 设计器命令


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
            {{
                filter.AddAnd(p => p.Position.Contains(keyWord) || p.Department.Contains(keyWord) || p.Role.Contains(keyWord) || p.Memo.Contains(keyWord));
            }}
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(OrganizePositionData data, FormConvert convert)
        {
            //职位
            data.Position = convert.ToString("Position");
            data.RoleId = convert.ToLong("RoleId");
            //数据
            data.DepartmentId = convert.ToLong("DepartmentId");
            //备注
            data.Memo = convert.ToString("Memo");
        }

        #endregion
    }
}