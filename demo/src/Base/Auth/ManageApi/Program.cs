using Agebull.Common.AppManage.DataAccess;
using Agebull.Common.AppManage.WebApi.Entity;
using Agebull.Common.DataModel.Redis;
using Agebull.Common.Ioc;
using Agebull.Common.Organizations.DataAccess;
using Agebull.Common.Rpc;
using Agebull.Common.Organizations;
using Agebull.Common.Organizations.BusinessLogic;
using Agebull.Common.WebApi;
using Agebull.Common.WebApi.Auth;
using Agebull.ZeroNet.Core;
using Gboxt.Common.DataModel.ExtendEvents;


namespace ApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IocHelper.AddScoped<GlobalContext, BusinessContext>();
            ZeroApplication.CheckOption();
            ZeroApplication.Discove(typeof(Program).Assembly, "UserManage");
            ZeroApplication.Discove(typeof(AppInfoApiController).Assembly, "AppManage");
            IocHelper.AddSingleton<IPowerChecker, AuthorityChecker>();
            //IocHelper.AddScoped<IRoleCache, RoleCache>();
            IocHelper.AddScoped<IEntityEventProxy, EntityEventProxy>();
            IocHelper.AddScoped<UserCenterDb, UserCenterDb>();
            IocHelper.AddSingleton<IRedis, StackExchangeRedis>();
            IocHelper.AddScoped<UserCenterDb, UserCenterDb>();
            IocHelper.AddScoped<AppManageDb, AppManageDb>();
            IocHelper.AddSingleton<IEntityEventProxy, EntityEventProxy>();
            IocHelper.Update();
            ZeroApplication.Initialize();
            ZeroApplication.RunAwaite();
        }
    }
}
