/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/1 15:31:53*/
#region
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
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.DataModel;
using Agebull.Common.WebApi;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;

using Agebull.Common.WebApi.Auth;
#endregion

namespace Agebull.Common.AppManage.DataAccess
{
    /// <summary>
    /// 本地数据库
    /// </summary>
    public partial class AppManageDb
    {
        /// <summary>
        /// 构造
        /// </summary>
        static AppManageDb()
        {
            /*tableSql = new Dictionary<string, TableSql>(StringComparer.OrdinalIgnoreCase)
            {AppManageDb
            };*/
            DataUpdateHandler.RegisterUpdateHandler(new MySqlDataTrigger());
            DataUpdateHandler.RegisterUpdateHandler(new RedisDataTrigger());
        }

        /// <summary>
        /// 构造
        /// </summary>
        public AppManageDb()
        {
            Name = @"AppManage";
            Caption = @"应用管理";
            Description = @"应用管理";
            Initialize();
            //RegistToEntityPool();
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();
    }
}