using System.Linq;
using Agebull.Common.Context;
using Agebull.Common.Ioc;
using Agebull.Common.OAuth;
using Agebull.Common.Organizations;
using Agebull.EntityModel.Common;
using Agebull.MicroZero;
using Agebull.MicroZero.ZeroApis;
using HPC.Projects.BusinessLogic;
using HPC.Projects.DataAccess;
using Newtonsoft.Json;

namespace Agebull.UserCenter.WebApi
{
    /// <summary>
    ///     场景API服务
    /// </summary>
    public class SceneApiControllers : ApiController
    {
        /// <summary>
        ///     管理员获取当前站点下可用的组织单元（default页和多店统计页）
        /// </summary>
        /// <remarks>
        ///     测试：http://localhost:13239/WebService/Permit/Login_Manager.ashx?action=EmpGetSiteHasOrgList
        /// </remarks>
        /// <returns></returns>
        [Route("v2/org/list")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Employe)]
        public string GetSiteOrgList()
        {
            //获得雇员的角色列表
            var erAccess = new EmployeeAndRoleDataAccess();
            var roleArr = erAccess.LoadValues(q => q.RoleRID,
                q => q.SiteSID == GlobalContext.Customer.GroupId && q.EmpEID == GlobalContext.Customer.UserId);
            if (roleArr.Count == 0)
                return
                    MessageProtocol.getReturnMessage(MessageProtocol.StateError, "该用户没有配置角色信息！");
            //获得站点开通的导航页面的列表(去重)
            var proAccess = new PermitRoleHasOrgDataAccess();
            var oids = proAccess.LoadValues(p => p.OrgOID, q => roleArr.Contains(q.RoleRID));
            var oAccess = new OrganizationDataAccess();
            var orgs = oAccess.All(p => oids.Contains(p.OID));


            if (orgs.Count == 0)
                return
                    MessageProtocol.getReturnMessage(MessageProtocol.StateError, "该用户拥有的角色下没有配置组织单元！");
            var orgList = orgs.Select(o => new
            {
                OrgOID = o.OID,
                o.OrgName
            });
            return MessageProtocol.getReturnMessage(MessageProtocol.StateSuccess, JsonConvert.SerializeObject(orgList));
        }

        /// <summary>
        ///     设置场景下的站点
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [Route("v1/scene/org/site")]
        [ApiAccessOptionFilter(ApiAccessOption.Public | ApiAccessOption.Employe)]
        public string SetEmployeeSiteID(Argument args)
        {
            if (string.IsNullOrEmpty(args.Value))
                return MessageProtocol.getReturnMessage(MessageProtocol.StateError, "请传入站点编号！");
            if (args.Value.Length != 19)
                return MessageProtocol.getReturnMessage(MessageProtocol.StateError, "站点编号位数不对，请确认后重新传入！");
            if (!long.TryParse(args.Value, out var sid))
                return MessageProtocol.getReturnMessage(MessageProtocol.StateError, "站点编号位数不对，请确认后重新传入！");

            var access = new EmployeeHasSiteDataAccess();

            var currentSite = access.FirstOrDefault(q => q.EmpEID == GlobalContext.Customer.UserId && q.SiteSID == sid);

            if (currentSite == null)
                return MessageProtocol.getReturnMessage(MessageProtocol.StateError, "该用户没有配置传入的站点编号，请确认后重新传入！");

            var eAccess = new EmployeeDataAccess();
            eAccess.SetValue(p => p.SiteSID, sid, GlobalContext.Customer.UserId);

            var info = UserHelper.GetLoginUserInfo(GlobalContext.Customer.UserId);
            info.GroupId = sid;
            UserHelper.UpdateCache(info);
            return MessageProtocol.getReturnMessage(MessageProtocol.StateSuccess, "站点设置成功！");
        }

        /// <summary>
        ///     管理员设置当前组织单元（default页和多店统计页）
        /// </summary>
        /// <param name="args"></param>
        /// <remarks>
        ///     测试：http://localhost:13239/WebService/Permit/Login_Manager.ashx?action=EmpSetOrgOID&OrgOID=12345
        /// </remarks>
        /// <returns></returns>
        [Route("v1/scene/org/org")]
        [ApiAccessOptionFilter(ApiAccessOption.Public | ApiAccessOption.Employe)]
        public string SetEmployeeOrgID(Argument args)
        {
            if (string.IsNullOrEmpty(args.Value))
                return MessageProtocol.getReturnMessage(MessageProtocol.StateError, "请传入组织单元编号！");
            if (args.Value.Length != 19)
                return MessageProtocol.getReturnMessage(MessageProtocol.StateError, "组织单元编号位数不对，请确认后重新传入！");
            if (!long.TryParse(args.Value, out var oid))
                return MessageProtocol.getReturnMessage(MessageProtocol.StateError, "站点编号位数不对，请确认后重新传入！");

            var rAccess = new EmployeeAndRoleDataAccess();
            //获得雇员的角色列表
            var roleList = rAccess.All(q =>
                q.SiteSID == GlobalContext.Customer.GroupId && q.EmpEID == GlobalContext.Customer.UserId);
            if (roleList.Count == 0)
                return MessageProtocol.getReturnMessage(MessageProtocol.StateError, "该用户在该站点下没有配置传入的组织单元编号，请确认后重新传入！");

            var roleArr = roleList.Select(q => q.RoleRID).ToList();
            var roAccess = new PermitRoleHasOrgDataAccess();
            //获得站点开通的导航页面的列表(去重)
            if (!roAccess.Any(q => roleArr.Contains(q.RoleRID) && q.OrgOID == oid))
                return MessageProtocol.getReturnMessage(MessageProtocol.StateError, "该用户在该站点下没有配置传入的组织单元编号，请确认后重新传入！");

            var eAccess = new EmployeeDataAccess();
            eAccess.SetValue(p => p.OrgOID, oid, GlobalContext.Customer.UserId);

            var info = UserHelper.GetLoginUserInfo(GlobalContext.Customer.UserId);
            info.OrganizationId = oid;
            UserHelper.UpdateCache(info);

            return MessageProtocol.getReturnMessage(MessageProtocol.StateSuccess, "组织单元设置成功！");
        }

        /// <summary>
        ///     开始场景并获得令牌
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [Route("v1/scene/begin")]
        [ApiAccessOptionFilter(ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiValueResult BeginScene(BeginSceneArgument args)
        {
            return new ApiValueResult();
        }

        /// <summary>
        ///     结束场景并销毁令牌
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [Route("v1/scene/close")]
        [ApiAccessOptionFilter(ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiValueResult CloseScene(ScreenTokenArgument args)
        {
            return new ApiValueResult();
        }

        /// <summary>
        ///     校验场景并得到用户信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [Route("v1/scene/verify")]
        [ApiAccessOptionFilter(ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResult<LoginUserInfo> VerifySceneToken(ScreenTokenArgument args)
        {
            string message = null;
            if (args == null || !args.Validate(out message))
                return ApiResult<LoginUserInfo>.ErrorResult(ErrorCode.Auth_AccessToken_TimeOut, message);
            var bl = IocHelper.Create<IOAuthBusiness>();
            var result = bl.VerifyAccessToken(new TokenArgument {Token = args.AccessToken});
            return result;
        }
    }
}