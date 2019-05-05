/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:36*/
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

using System.Data.Sql;

using Agebull.Common;
using Agebull.Common.OAuth;
using Agebull.EntityModel.Common;

using Agebull.EntityModel.SqlServer;
using Agebull.EntityModel.BusinessLogic.SqlServer;



using HPC.Projects.DataAccess;


#endregion

namespace HPC.Projects.BusinessLogic
{
    /// <summary>
    /// 员工绑定用户
    /// </summary>
    partial class EmployeeBindingUserBusinessLogic
    {
        
        /// <summary>
        ///     实体类型
        /// </summary>
        public override int EntityType => EmployeeBindingUserData._DataStruct_.EntityIdentity;



    }
}