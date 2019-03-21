/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 17:26:29*/
#region
using System.Runtime.Serialization;
using Agebull.EntityModel.Common;
#endregion

namespace Agebull.Common.AppManage
{
    /// <summary>
    /// 角色
    /// </summary>
    [DataContract]
    sealed partial class RoleData : EditDataObject
    {
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize()
        {
/*
            _isfreeze = true;
            _datastate = 0;
            _lastreviserid = 0;
            _authorid = 0;*/
        }

    }
}