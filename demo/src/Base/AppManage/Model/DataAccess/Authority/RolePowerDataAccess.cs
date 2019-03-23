/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/22 10:16:57*/
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

using MySql.Data.MySqlClient;
using Agebull.EntityModel.MySql;

using Agebull.Common;
using Agebull.Common.OAuth;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;

using Agebull.Common.Organizations;
using Agebull.Common.OAuth;
#endregion

namespace Agebull.Common.AppManage.DataAccess
{
    /// <summary>
    /// 角色权限
    /// </summary>
    sealed partial class RolePowerDataAccess : MySqlTable<RolePowerData,AppManageDb>
    {

    }
}