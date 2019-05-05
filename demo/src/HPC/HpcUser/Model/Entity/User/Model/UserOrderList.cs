/*design by:agebull designer date:2019/4/10 10:44:52*/
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
    /// 用户订单列表
    /// </summary>
    [DataContract]
    sealed partial class UserOrderListData : EditDataObject, IIdentityData
    {
        long IIdentityData.Id { get => LID; set => LID = (int)value; }


        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize()
        {
/**/
        }

    }
}