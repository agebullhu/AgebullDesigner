/*design by:agebull designer date:2018/9/2 12:32:49*/


using Agebull.Common.DataModel.WebUI;

namespace Agebull.Common.AppManage.BusinessLogic
{
    /// <summary>
    /// 角色缓存
    /// </summary>
    public interface IRoleCache
    {
        /// <summary>
        /// 缓存角色
        /// </summary>
        void Cache();
        /// <summary>
        /// 缓存角色
        /// </summary>
        /// <param name="roleId"></param>
        void Cache(long roleId);
        /// <summary>
        /// 缓存页面审批者
        /// </summary>
        void CachePageAuditUser();
        /// <summary>
        /// 缓存数据类型对应用户
        /// </summary>
        void CacheTypeUser();
        /// <summary>
        /// 载入能力树
        /// </summary>
        EasyUiTreeNode LoadPowerTree();
    }
}