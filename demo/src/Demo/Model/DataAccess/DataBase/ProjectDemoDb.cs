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
using Agebull.Common.DataModel;
using Agebull.Common.WebApi;
using Gboxt.Common.DataModel;


using MySql.Data.MySqlClient;
using Gboxt.Common.DataModel.MySql;
#endregion

namespace Agebull.EntityModel.Demo.DataAccess
{
    /// <summary>
    /// 本地数据库
    /// </summary>
    sealed partial class ProjectDemoDb : MySqlDataBase
    {
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize()
        {
        }
        
        /// <summary>
        /// 生成缺省数据库
        /// </summary>
        public static void CreateDefault()
        {
            Default = new ProjectDemoDb();
        }

        /// <summary>
        /// 缺省强类型数据库
        /// </summary>
        public static ProjectDemoDb Default
        {
            get;
            set;
        }

        /// <summary>
        /// 读取连接字符串
        /// </summary>
        /// <returns></returns>
        protected override string LoadConnectionStringSetting()
        {
            return ConfigurationManager.AppSettings["ProjectDemoDb"];
        }
    }
}