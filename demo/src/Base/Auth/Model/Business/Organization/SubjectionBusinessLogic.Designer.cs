﻿/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/11/14 19:10:09*/
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
using Agebull.EntityModel.Common;




using Agebull.EntityModel.MySql.BusinessLogic;
using Agebull.Common.Organizations.DataAccess;

using MySql.Data.MySqlClient;
using Agebull.EntityModel.MySql;

#endregion

namespace Agebull.Common.Organizations.BusinessLogic
{
    /// <summary>
    /// 行级权限关联
    /// </summary>
    partial class SubjectionBusinessLogic
    {
        
        /// <summary>
        ///     实体类型
        /// </summary>
        public override int EntityType => SubjectionData._DataStruct_.EntityIdentity;



    }
}