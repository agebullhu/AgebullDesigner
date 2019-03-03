/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 17:26:28*/
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
#endregion

namespace Agebull.Common.AppManage
{
    /// <summary>
    /// 系统内的应用的信息
    /// </summary>
    [DataContract]
    sealed partial class AppInfoData : EditDataObject
    {
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize()
        {
/*
            _datastate = 0;
            _isfreeze = true;
            _lastreviserid = 0;
            _authorid = 0;*/
        }

    }
}