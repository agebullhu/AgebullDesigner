using Agebull.Common.AppManage.DataAccess;
using Agebull.Common.AppManage.Entity;
using Agebull.Common.AppManage.WebApi.Entity;
using Agebull.Common.DataModel.Redis;
using Agebull.Common.Ioc;
using Agebull.Common.OAuth;
using Agebull.Common.OAuth.DataAccess;
using Agebull.Common.UserCenter.WebApi.Entity;
using Agebull.EntityModel.Demo.WebApi.Entity;
using Agebull.ZeroNet.Core;
using Agebull.OAuth.Business;
using Gboxt.Common.DataModel.ExtendEvents;

namespace DemoDataModel
{
    class Program
    {
        static void Main(string[] args)
        {
            IocHelper.AddScoped<IRedis, StackExchangeRedis>();
            IocHelper.AddScoped<IEntityEventProxy, EntityEventProxy>();
            IocHelper.AddScoped<UserCenterDb, UserCenterDb>();
            IocHelper.AddScoped<AppManageDb, AppManageDb>();

            IocHelper.AddScoped<IOAuthBusiness, AuthBusiness>();
            IocHelper.AddScoped<IUserInfoApi, UserInfoApiLogical>();
            ZeroApplication.CheckOption();
            ZeroApplication.Discove(typeof(DemoEntityApiController).Assembly, "Demo");
            ZeroApplication.Discove(typeof(AppInfoApiController).Assembly, "AppManage");
            ZeroApplication.Discove(typeof(AuthBusiness).Assembly, "Authority");
            ZeroApplication.Initialize();

            ZeroApplication.RunAwaite();
        }
    }
}
