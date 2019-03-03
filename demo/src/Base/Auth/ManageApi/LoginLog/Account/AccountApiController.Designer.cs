/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/9/2 18:08:58*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Web.Http;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.DataModel;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.WebApi;
using Agebull.Common.WebApi.EasyUi;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;







namespace Agebull.Common.WebApi.Auth.WebApi.Entity
{
    partial class AccountApiController
    {
        #region 设计器命令


        #endregion

        #region 基本扩展
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected ApiPageData<AccountData> DefaultGetListData()
        {
            var filter = new LambdaItem<AccountData>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name="filter">筛选器</param>
        public void SetKeywordFilter(LambdaItem<AccountData> filter)
        {
            var keyWord = GetArg("keyWord");
            if (!string.IsNullOrEmpty(keyWord))
            {{
                filter.AddAnd(p => p.AccountName.Contains(keyWord));
            }}
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(AccountData data, FormConvert convert)
        {
            //数据֪
            data.RoleId = convert.ToLong("RoleId");
            data.UserId = convert.ToLong("UserId");
            data.AccountName = convert.ToString("AccountName");
        }

        #endregion
    }
}