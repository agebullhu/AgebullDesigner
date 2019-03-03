using System;
using Agebull.ZeroNet.ZeroApi;
using Agebull.Common.DataModel.WebUI;
using Agebull.Common.Ioc;
using Agebull.Common.Logging;
using Agebull.Common.OAuth.BusinessLogic;
using Agebull.Common.Rpc;
using Agebull.Common.UserCenter.BusinessLogic;
using Gboxt.Common.DataModel;
using IRoleCache = Agebull.Common.AppManage.BusinessLogic.IRoleCache;

#pragma warning disable 168

namespace Agebull.Common.WebApi.Auth
{
    /// <summary>
    /// 身份验证服务API
    /// </summary>
    public class SystemApiController : ApiController
    {
        /// <summary>
        ///     返回页面树
        /// </summary>
        [Route("v1/page/tree")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Customer)]
        public ApiResult TreeNode()
        {
            var data = RoleCache.Instance.LoadRoleMenu(GlobalContext.Current.User.RoleId);
            return new ApiArrayResult<EasyUiTreeNode> { ResultData = data };
        }

        /// <summary>
        /// 同步用户权限
        /// </summary>
        [ Route("v1/sys/flush_cache")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Customer)]
        public ApiResult FlushCache()
        {
            using (SystemModelScope.CreateScope())
            {
                try
                {
                    OrganizationBusinessLogic.Cache();
                    IocHelper.Create<IRoleCache>().Cache();
                    return ApiResult.Ok;
                }
                catch (Exception e)
                {
                    LogRecorder.Exception(e);
                    return ApiResult.LocalError;
                }
            }
        }
    }
}