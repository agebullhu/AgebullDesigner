/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/5 13:54:14*/
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
using Agebull.EntityModel.Common;
using Agebull.EntityModel.MySql.BusinessLogic;
using Agebull.MicroZero.ZeroApis;



using Agebull.EntityModel.Common;
using Agebull.EntityModel.MySql;



using Agebull.MicroZero.Demo;
using Agebull.MicroZero.Demo.BusinessLogic;
using Agebull.MicroZero.Demo.DataAccess;
#endregion

namespace Agebull.MicroZero.Entity
{
    partial class DemoEntityApiController
    {
        #region 设计器命令


        #endregion

        #region 基本扩展
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected ApiPageData<DemoEntityData> DefaultGetListData()
        {
            var filter = new LambdaItem<DemoEntityData>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name="filter">筛选器</param>
        public void SetKeywordFilter(LambdaItem<DemoEntityData> filter)
        {
            var keyWord = GetArg("keyWord");
            if (!string.IsNullOrEmpty(keyWord))
            {
                filter.AddAnd(p =>p.Name.Contains(keyWord) || 
                                   p.Memo.Contains(keyWord));
            }
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(DemoEntityData data, FormConvert convert)
        {
            //-
            data.Id = convert.ToLong("id");
            data.Name = convert.ToString("name");
            data.Price = convert.ToDecimal("price");
            data.Value = convert.ToInteger("value");
            data.Memo = convert.ToString("memo");
        }

        #endregion
    }
}