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
using Agebull.ZeroNet.ZeroApi;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.DataModel;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.WebApi;

using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;

using Agebull.Common.WebApi.Auth;

using Agebull.Common.OAuth;
using Agebull.Common.OAuth.BusinessLogic;
using Agebull.Common.OAuth.DataAccess;

namespace Agebull.Common.UserCenter.WebApi.Entity
{
    partial class PositionPersonnelApiController
    {
        #region 设计器命令


        #endregion

        #region 基本扩展
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected ApiPageData<PositionPersonnelData> DefaultGetListData()
        {
            var filter = new LambdaItem<PositionPersonnelData>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name="filter">筛选器</param>
        public void SetKeywordFilter(LambdaItem<PositionPersonnelData> filter)
        {
            var keyWord = GetArg("keyWord");
            if (!string.IsNullOrEmpty(keyWord))
            {{
                filter.AddAnd(p => p.RealName.Contains(keyWord) || p.Appellation.Contains(keyWord) || p.Position.Contains(keyWord) || p.PhoneNumber.Contains(keyWord) || p.Organization.Contains(keyWord) || p.Department.Contains(keyWord) || p.Role.Contains(keyWord) || p.Memo.Contains(keyWord));
            }}
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(PositionPersonnelData data, FormConvert convert)
        {
            //数据
            data.Appellation = convert.ToString("Appellation");
            data.OrganizePositionId = convert.ToInteger("OrganizePositionId");
            //备注
            data.Memo = convert.ToString("Memo");
        }

        #endregion
    }
}