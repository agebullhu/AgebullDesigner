/*design by:agebull designer date:2018/9/28 20:25:28*/

using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.AppManage.DataAccess;

namespace Agebull.Common.AppManage.BusinessLogic
{
    /// <summary>
    /// 系统内的应用的信息
    /// </summary>
    public sealed partial class AppInfoBusinessLogic : BusinessLogicByStateData<AppInfoData, AppInfoDataAccess, AppManageDb>
    {

        /*// <summary>
        ///     保存前的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaving(AppInfoData data, bool isAdd)
        {
            return base.OnSaving(data, isAdd);
        }

        /// <summary>
        ///     保存完成后的操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool OnSaved(AppInfoData data, bool isAdd)
        {
             return base.OnSaved(data, isAdd);
        }
        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool LastSavedByUser(AppInfoData data, bool isAdd)
        {
            return base.LastSavedByUser(data, isAdd);
        }

        /// <summary>
        ///     被用户编辑的数据的保存前操作
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isAdd">是否为新增</param>
        /// <returns>如果为否将阻止后续操作</returns>
        protected override bool PrepareSaveByUser(AppInfoData data, bool isAdd)
        {
            return base.PrepareSaveByUser(data, isAdd);
        }*/
    }
}