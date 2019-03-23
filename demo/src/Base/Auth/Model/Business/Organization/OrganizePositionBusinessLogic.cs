using Agebull.Common.Organizations.DataAccess;
using Agebull.EntityModel.MySql.BusinessLogic;

namespace Agebull.Common.Organizations.BusinessLogic
{
    /// <summary>
    /// 职位组织关联
    /// </summary>
    public sealed partial class OrganizePositionBusinessLogic : BusinessLogicByStateData<OrganizePositionData, OrganizePositionDataAccess, UserCenterDb>
    {
        #region 缓存

        public OrganizePositionBusinessLogic()
        {
            unityStateChanged = true;
        }
        protected override bool OnSaving(OrganizePositionData data, bool isAdd)
        {
            return base.OnSaving(data, isAdd);
        }
        /// <summary>
        ///     内部命令执行完成后的处理
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="cmd">命令</param>
        protected override void OnInnerCommand(OrganizePositionData data, BusinessCommandType cmd)
        {
            OrganizationBusinessLogic.Cache();
        }

        #endregion
    }
}
