/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:52*/
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
    /// 用户日志
    /// </summary>
    public sealed partial class UserOrderLogBusinessLogic : UiBusinessLogicBase<UserOrderLogData,UserOrderLogDataAccess,HpcSqlServerDb>
    {


        /*// <summary>
        ///     保存前的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaving(UserOrderLogData data, bool isAdd)
        {
             return base.OnSaving(data, isAdd);
        }

        /// <summary>
        ///     保存完成后的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaved(UserOrderLogData data, bool isAdd)
        {
             return base.OnSaved(data, isAdd);
        }
        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool LastSavedByUser(UserOrderLogData data, bool isAdd)
        {
            return base.LastSavedByUser(data, isAdd);
        }

        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool PrepareSaveByUser(UserOrderLogData data, bool isAdd)
        {
            return base.PrepareSaveByUser(data, isAdd);
        }*/
    }
}