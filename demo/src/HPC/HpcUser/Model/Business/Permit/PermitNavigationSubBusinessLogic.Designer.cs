/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/12 21:30:55*/
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
    /// 导航关联
    /// </summary>
    partial class PermitNavigationSubBusinessLogic
    {
        
        /// <summary>
        ///     实体类型
        /// </summary>
        public override int EntityType => PermitNavigationSubData._DataStruct_.EntityIdentity;



    }
}