﻿/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/1 13:44:38*/
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
using Agebull.EntityModel.Common;


using Agebull.Common.OAuth;

using Agebull.EntityModel.MySql.BusinessLogic;
using Agebull.Common.AppManage.DataAccess;

using MySql.Data.MySqlClient;
using Agebull.EntityModel.MySql;

#endregion

namespace Agebull.Common.AppManage.BusinessLogic
{
    /// <summary>
    /// 系统内的应用的信息
    /// </summary>
    partial class AppInfoBusinessLogic
    {
        
        /// <summary>
        ///     实体类型
        /// </summary>
        public override int EntityType => AppInfoData._DataStruct_.EntityIdentity;



    }
}