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
using Gboxt.Common.DataModel;
using Agebull.Common.WebApi;

using Agebull.Common.WebApi.Auth;

using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.AppManage.DataAccess;

using MySql.Data.MySqlClient;
using Gboxt.Common.DataModel.MySql;

#endregion

namespace Agebull.Common.AppManage.BusinessLogic
{
    /// <summary>
    /// 角色权限
    /// </summary>
    partial class RolePowerBusinessLogic
    {
        
        /// <summary>
        ///     实体类型
        /// </summary>
        public override int EntityType => RolePowerData._DataStruct_.EntityIdentity;



    }
}