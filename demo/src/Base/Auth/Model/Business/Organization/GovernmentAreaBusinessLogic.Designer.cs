/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 23:07:34*/
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
using Agebull.Common.OAuth.DataAccess;

using MySql.Data.MySqlClient;
using Agebull.EntityModel.MySql;

#endregion

namespace Agebull.Common.OAuth.BusinessLogic
{
    /// <summary>
    /// 行政区域
    /// </summary>
    partial class GovernmentAreaBusinessLogic
    {
        
        /// <summary>
        ///     实体类型
        /// </summary>
        public override int EntityType => GovernmentAreaData._DataStruct_.EntityIdentity;



    }
}