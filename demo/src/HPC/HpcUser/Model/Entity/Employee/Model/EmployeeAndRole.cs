﻿/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:36*/
#region using
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
using Agebull.EntityModel.Interfaces;

#endregion

namespace HPC.Projects
{
    /// <summary>
    /// 员工和角色
    /// </summary>
    [DataContract]
    sealed partial class EmployeeAndRoleData : EditDataObject
    {
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize()
        {
/**/
        }

    }
}