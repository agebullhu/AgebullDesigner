/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 23:17:40*/
#region




using Agebull.Common.OAuth.DataAccess;
using Agebull.EntityModel.MySql.BusinessLogic;
#endregion

namespace Agebull.Common.OAuth.BusinessLogic
{
    /// <summary>
    /// 用户的个人信息
    /// </summary>
    public sealed partial class PersonBusinessLogic : BusinessLogicByStateData<PersonData,PersonDataAccess,UserCenterDb>
    {


        /*// <summary>
        ///     保存前的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaving(PersonData data, bool isAdd)
        {
             return base.OnSaving(data, isAdd);
        }

        /// <summary>
        ///     保存完成后的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaved(PersonData data, bool isAdd)
        {
             return base.OnSaved(data, isAdd);
        }
        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool LastSavedByUser(PersonData data, bool isAdd)
        {
            return base.LastSavedByUser(data, isAdd);
        }

        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool PrepareSaveByUser(PersonData data, bool isAdd)
        {
            return base.PrepareSaveByUser(data, isAdd);
        }*/
    }
}