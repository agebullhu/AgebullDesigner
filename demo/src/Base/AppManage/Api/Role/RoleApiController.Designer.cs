/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/10/5 23:06:24*/
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
using Agebull.MicroZero.ZeroApis;


using Agebull.Common.OAuth;

using Agebull.Common.OAuth;
using Agebull.Common.OAuth.BusinessLogic;
using Agebull.Common.OAuth.DataAccess;
using Agebull.Common.AppManage;
using Agebull.EntityModel.EasyUI;
using Agebull.Common.Context;

namespace Agebull.Common.UserCenter.WebApi.Entity
{
    partial class RoleApiController
    {
        #region 设计器命令

        /// <summary>下拉列表</summary>
        /// <returns></returns>
        
        [Route("edit/combo")]
        public List<EasyComboValues> ComboData()
        {
            GlobalContext.Current.IsManageMode = false;
            var datas = Business.All();
            var combos = datas.Select(p => new EasyComboValues(p.Id, p.Caption)).ToList();
            combos.Insert(0,new EasyComboValues(0, "-"));
            return (combos);
        }

        #endregion

        #region 基本扩展
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected ApiPageData<RoleData> DefaultGetListData()
        {
            var filter = new LambdaItem<RoleData>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name="filter">筛选器</param>
        public void SetKeywordFilter(LambdaItem<RoleData> filter)
        {
            var keyWord = GetArg("keyWord");
            if (!string.IsNullOrEmpty(keyWord))
            {
                filter.AddAnd(p =>p.Name.Contains(keyWord) || 
                                   p.Caption.Contains(keyWord) || 
                                   p.Memo.Contains(keyWord));
            }
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(RoleData data, FormConvert convert)
        {
            //数据
            data.Name = convert.ToString("Role");
            data.Memo = convert.ToString("Memo");
            //-
            data.Caption = convert.ToString("Caption");
        }

        #endregion
    }
}