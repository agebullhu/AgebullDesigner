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
    partial class UserApiController
    {
        #region 设计器命令


        #endregion

        #region 基本扩展
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected ApiPageData<UserData> DefaultGetListData()
        {
            var filter = new LambdaItem<UserData>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name="filter">筛选器</param>
        public void SetKeywordFilter(LambdaItem<UserData> filter)
        {
            var keyWord = GetArg("keyWord");
            if (!string.IsNullOrEmpty(keyWord))
            {
                filter.AddAnd(p =>p.OpenId.Contains(keyWord) || 
                                   p.Os.Contains(keyWord) || 
                                   p.App.Contains(keyWord) || 
                                   p.DeviceId.Contains(keyWord) || 
                                   p.Channel.Contains(keyWord) || 
                                   p.TraceMark.Contains(keyWord));
            }
        }

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="convert">转化器</param>
        protected void DefaultReadFormData(UserData data, FormConvert convert)
        {
            //
            data.UserId = convert.ToLong("UserId");
            data.UserType = (UserType)convert.ToInteger("UserType");
            data.OpenId = convert.ToString("OpenId");
            //-
            data.Status = (UserStatusType)convert.ToInteger("Status");
            data.AuthorizeScreen = (AuthorizeType)convert.ToInteger("AuthorizeScreen");
            //来源
            data.RegistSoure = (AuthorizeType)convert.ToInteger("RegistSoure");
            data.Os = convert.ToString("Os");
            data.App = convert.ToString("App");
            data.DeviceId = convert.ToString("DeviceId");
            data.Channel = convert.ToString("Channel");
            data.TraceMark = convert.ToString("TraceMark");
        }

        #endregion
    }
}