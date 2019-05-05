using System.Linq;
using Agebull.Common.Context;
using Agebull.Common.Ioc;
using Agebull.Common.Organizations;
using Agebull.MicroZero;

using Agebull.MicroZero.ZeroApis;
using HPC.Projects.BusinessLogic;
using HPC.Projects.DataAccess;
using Newtonsoft.Json;

namespace Agebull.UserCenter.WebApi
{
    /// <summary>
    /// 场景API服务
    /// </summary>
    public class HpcApiControllers : ApiController
    {

        /// <summary>
        ///  使用账号密码验证码登录
        /// </summary>
        /// <param name="arg">基于手机的账号登录参数</param>
        /// <returns>登录返回数据</returns>
        [Route("v1/login/hpc")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public string HpcLogin(PhoneLoginRequest arg)
        {
            var lg = IocHelper.Create<IUserApi>();
            var result = lg.HpcLogin(arg);
            return result;
        }


        /// <summary>
        ///     页根据token获得用户默认信息：站点名称、管理员名称、默认菜单
        /// </summary>
        /// <remarks>
        ///     测试：http://localhost:13239/WebService/Permit/Login_Manager.ashx?action=getDefaultInfoByToken
        /// </remarks>
        /// <returns></returns>
        [Route("v2/token/info")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Employe)]
        public string GetDefaultInfoByToken()
        {
            var oAccess = new OrganizationDataAccess();
            var currentOrg = oAccess.FirstOrDefault(GlobalContext.Current.User.OrganizationId);
            var eAccess = new EmployeeDataAccess();
            var currentEmp = eAccess.FirstOrDefault(GlobalContext.Current.User.UserId);
            var sAccess = new SiteDataAccess();
            var currentSite = sAccess.FirstOrDefault(GlobalContext.Current.User.GroupId);


            //获得雇员的角色列表
            var erAccess = new EmployeeAndRoleDataAccess();
            var roleArr = erAccess.LoadValues(q => q.RoleRID,
                it => it.SiteSID == GlobalContext.Current.User.GroupId &&
                      it.EmpEID == GlobalContext.Current.User.UserId);

            if (roleArr.Count == 0) return MessageProtocol.getReturnMessage(MessageProtocol.StateError, "该用户还没有角色信息！");
            //获得站点开通的导航页面的列表(去重)
            var rnAccess = new PermitRoleAndNavigationDataAccess();
            //获得导航完整信息（新写法）
            var subSidArr = rnAccess.LoadValues(q => q.NavigationSubSID, it => roleArr.Contains(it.RoleRID));

            if (subSidArr.Count == 0)
                return MessageProtocol.getReturnMessage(MessageProtocol.StateError, "该用户角色下还没有配置导航页面！");

            subSidArr = subSidArr.Distinct().ToList();
            var rsAccess = new PermitNavigationSubDataAccess();
            var sids = rsAccess.LoadValues(p => p.NavigationNID, q => subSidArr.Contains(q.SID));
            var nAccess = new PermitNavigationAllDataAccess();
            var nlist = nAccess.All(p => p.IsShow && sids.Contains(p.NID));

            var navList = nlist.OrderBy(q => q.Sort).Select(n => new
            {
                id = n.NID.ToString(),
                pid = n.PID.ToString(),
                name = n.MenuName,
                title = n.MenuTitle,
                url = n.MenuUrl,
                icon = n.Icon,
                sort = n.Sort,
                isShow = n.IsShow
            });

            var newObj = new
            {
                SiteSID = currentSite?.SID.ToString(),
                SiteName = currentSite?.SiteNameWhole,
                currentSite?.SiteLogo,
                OrgOID = currentOrg?.OID.ToString(),
                currentOrg?.OrgName,
                OrgAddress = currentOrg?.Address,
                currentEmp?.EmployeeName,
                navList = navList.ToList()
            };

            return
                MessageProtocol.getReturnMessage(MessageProtocol.StateSuccess,
                    JsonConvert.SerializeObject(newObj));
        }

    }
}
