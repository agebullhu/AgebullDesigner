/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 23:17:40*/
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
using Agebull.Common.Rpc;
using Agebull.Common.WebApi;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;


#endregion

namespace Agebull.Common.OAuth.DataAccess
{
    /// <summary>
    /// APP端用户信息表
    /// </summary>
    sealed partial class UserDataAccess : DataStateTable<UserData,UserCenterDb>
    {

    }
}