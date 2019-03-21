/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/6 10:43:20*/
#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.Configuration;
using Agebull.EntityModel.Common;

using Agebull.EntityModel.Common;
using Agebull.EntityModel.MySql;
using Agebull.EntityModel.Redis;
using Agebull.EntityModel.Events;


#endregion

namespace Agebull.MicroZero.Demo.DataAccess
{
    /// <summary>
    /// 本地数据库
    /// </summary>
    public partial class ProjectDemoDb
    {
        /// <summary>
        /// 构造
        /// </summary>
        static ProjectDemoDb()
        {
            /*tableSql = new Dictionary<string, TableSql>(StringComparer.OrdinalIgnoreCase)
            {ProjectDemoDb
            };*/
            DataUpdateHandler.RegisterUpdateHandler(new MySqlDataTrigger());
            DataUpdateHandler.RegisterUpdateHandler(new RedisDataTrigger());
        }

        /// <summary>
        /// 构造
        /// </summary>
        public ProjectDemoDb()
        {
            Name = @"ProjectDemo";
            Caption = @"项目演示";
            Description = @"项目演示";
            Initialize();
            //RegistToEntityPool();
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();
    }
}