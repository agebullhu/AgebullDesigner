/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 23:17:39*/
#region
using System.Runtime.Serialization;
using Agebull.EntityModel.Common;
using Newtonsoft.Json;
using System.Runtime.Serialization;
#endregion

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 用于支持用户的账户名密码登录
    /// </summary>
    [DataContract]
    sealed partial class AccountData : EditDataObject
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