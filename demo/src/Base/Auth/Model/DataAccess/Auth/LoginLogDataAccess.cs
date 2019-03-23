/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/22 9:58:45*/
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

using Agebull.Common.OAuth;
#endregion

namespace Agebull.Common.Organizations.DataAccess
{
    /// <summary>
    /// 用户登录日志
    /// </summary>
    sealed partial class LoginLogDataAccess : MySqlTable<LoginLogData,UserCenterDb>
    {

    }
}