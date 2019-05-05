/*design by:agebull designer date:2019/4/10 10:44:36*/
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

using System.Data.Sql;

using Agebull.Common;
using Agebull.Common.Configuration;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;
using Agebull.EntityModel.Events;
using Agebull.EntityModel.Redis;
using Agebull.EntityModel.SqlServer;


#endregion

namespace HPC.Projects.DataAccess
{
    /// <summary>
    /// 本地数据库
    /// </summary>
    public partial class HpcSqlServerDb
    {
        /// <summary>
        /// 构造
        /// </summary>
        static HpcSqlServerDb()
        {
            /*tableSql = new Dictionary<string, TableSql>(StringComparer.OrdinalIgnoreCase)
            {HpcSqlserverDb
            };*/
            //DataUpdateHandler.RegisterUpdateHandler(new MySqlDataTrigger());
            DataUpdateHandler.RegisterUpdateHandler(new RedisDataTrigger());
        }

        /// <summary>
        /// 构造
        /// </summary>
        public HpcSqlServerDb()
        {
            Name = @"hpc";
            Caption = @"荟品仓";
            Description = @"荟品仓V1";
            Initialize();
            //RegistToEntityPool();
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();
    }
}