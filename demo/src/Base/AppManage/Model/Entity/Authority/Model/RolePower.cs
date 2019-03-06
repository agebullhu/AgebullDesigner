/*design by:agebull designer date:2018/9/2 11:44:40*/
using System;
using System.Runtime.Serialization;
using Agebull.Common.WebApi.Auth;
using Gboxt.Common.DataModel;

namespace Agebull.Common.AppManage
{
    /// <summary>
    /// 角色权限
    /// </summary>
    [DataContract]
    sealed partial class RolePowerData : EditDataObject, IRolePower
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