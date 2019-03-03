/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 23:09:14*/
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
    partial class GovernmentAreaApiController
    {
        #region 设计器命令


        #endregion

        #region 基本扩展
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected ApiPageData<GovernmentAreaData> DefaultGetListData()
        {
            var filter = new LambdaItem<GovernmentAreaData>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name="filter">筛选器</param>
        public void SetKeywordFilter(LambdaItem<GovernmentAreaData> filter)
        {
            var keyWord = GetArg("keyWord");
            if (!string.IsNullOrEmpty(keyWord))
            {
                filter.AddAnd(p =>p.Code.Contains(keyWord) || 
                                   p.FullName.Contains(keyWord) || 
                                   p.ShortName.Contains(keyWord) || 
                                   p.TreeName.Contains(keyWord) || 
                                   p.Memo.Contains(keyWord));
            }
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(GovernmentAreaData data, FormConvert convert)
        {
            //数据
            data.Id = convert.ToLong("id");
            data.Code = convert.ToString("code");
            data.FullName = convert.ToString("fullName");
            data.ShortName = convert.ToString("shortName");
            data.TreeName = convert.ToString("treeName");
            data.OrgLevel = convert.ToInteger("orgLevel");
            data.LevelIndex = convert.ToInteger("levelIndex");
            data.ParentId = convert.ToLong("parentId");
            data.Memo = convert.ToString("memo");
        }

        #endregion
    }
}