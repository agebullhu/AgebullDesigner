﻿/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/21 22:02:24*/
#region




<<<<<<< HEAD:demo/src/Base/Auth/Model/Business/User/WechatBusinessLogic.cs
using MySql.Data.MySqlClient;
using Gboxt.Common.DataModel.MySql;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.Organizations.DataAccess;
#endregion

namespace Agebull.Common.Organizations.BusinessLogic
=======
using Agebull.EntityModel.MySql.BusinessLogic;
using Agebull.MicroZero.Demo.DataAccess;
#endregion

namespace Agebull.MicroZero.Demo.BusinessLogic
>>>>>>> dad19e146e305fb88b1600da6828c94bbc6b8c63:demo/src/Demo/Model/Business/None/DemoEntityBusinessLogic.cs
{
    /// <summary>
    /// 微信联合认证关联的用户信息
    /// </summary>
    public sealed partial class WechatBusinessLogic : BusinessLogicByStateData<WechatData,WechatDataAccess,UserCenterDb>
    {


        /*// <summary>
        ///     保存前的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaving(WechatData data, bool isAdd)
        {
             return base.OnSaving(data, isAdd);
        }

        /// <summary>
        ///     保存完成后的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaved(WechatData data, bool isAdd)
        {
             return base.OnSaved(data, isAdd);
        }
        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool LastSavedByUser(WechatData data, bool isAdd)
        {
            return base.LastSavedByUser(data, isAdd);
        }

        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool PrepareSaveByUser(WechatData data, bool isAdd)
        {
            return base.PrepareSaveByUser(data, isAdd);
        }*/
    }
}