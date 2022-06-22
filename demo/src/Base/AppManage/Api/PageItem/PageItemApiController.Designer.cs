﻿/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/7 0:20:45*/
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

using Agebull.Common.Organizations;
using Agebull.Common.OAuth;

using Agebull.Common.AppManage;
using Agebull.Common.AppManage.BusinessLogic;
using Agebull.Common.AppManage.DataAccess;
#endregion

namespace Agebull.Common.AppManage.WebApi.Entity
{
    partial class PageItemApiController
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
            var combos = datas.Select(p => new EasyComboValues(p.Id, p.Name)).ToList();
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
                filter.AddAnd(p =>p.Name.Contains(keyWord) || 
                                   p.Caption.Contains(keyWord) || 
                                   p.Icon.Contains(keyWord) || 
                                   p.Url.Contains(keyWord) || 
                                   p.Tags.Contains(keyWord) || 
                                   p.Memo.Contains(keyWord));
            }
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(PageItemData data, FormConvert convert)
        {
            //-
            data.Id = convert.ToLong("id");
            data.Name = convert.ToString("name");
            data.Caption = convert.ToString("caption");
            data.ItemType = (PageItemType)convert.ToInteger("itemType");
            data.Index = convert.ToInteger("index");
            data.Icon = convert.ToString("icon");
            data.Url = convert.ToString("url");
            data.Tags = convert.ToString("tags");
            data.Json = convert.ToString("json");
            data.Memo = convert.ToString("memo");
            data.ParentId = convert.ToLong("parentId");
        }

        #endregion
    }
}