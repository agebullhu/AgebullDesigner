/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/9/2 16:30:04*/
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

using Agebull.Common.AppManage;
using Agebull.Common.AppManage.BusinessLogic;
using Agebull.Common.AppManage.DataAccess;
using Agebull.MicroZero.ZeroApis;

namespace Agebull.Common.AppManage.Entity
{
    partial class PageItemApiController
    {
        #region 设计器命令


        #endregion

        #region 基本扩展
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected ApiPageData<PageItemData> DefaultGetListData()
        {
            var filter = new LambdaItem<PageItemData>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name="filter">筛选器</param>
        public void SetKeywordFilter(LambdaItem<PageItemData> filter)
        {
            var keyWord = GetArg("keyWord");
            if (!string.IsNullOrEmpty(keyWord))
            {
                filter.AddAnd(p =>
                    p.Name.Contains(keyWord) || p.Caption.Contains(keyWord) || p.Icon.Contains(keyWord) ||
                    p.Url.Contains(keyWord) || p.ExtendValue.Contains(keyWord) || p.Memo.Contains(keyWord));
            }
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(PageItemData data, FormConvert convert)
        {
            //数据֪
            data.Name = convert.ToString("Name");
            data.Caption = convert.ToString("Caption");
            data.ItemType = (PageItemType)convert.ToInteger("ItemType");
            data.Index = convert.ToInteger("Index");
            data.Icon = convert.ToString("Icon");
            data.Url = convert.ToString("Url");
            data.ExtendValue = convert.ToString("ExtendValue");
            data.Json = convert.ToString("Json");
            data.Memo = convert.ToString("Memo");
            data.ParentId = convert.ToLong("ParentId");
        }

        #endregion
    }
}