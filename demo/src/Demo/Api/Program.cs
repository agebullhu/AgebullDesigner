using Agebull.Common.AppManage.DataAccess;
using Agebull.Common.AppManage.WebApi.Entity;
using Agebull.EntityModel.Redis;
using Agebull.Common.Ioc;
using Agebull.Common.OAuth;
using Agebull.Common.OAuth.DataAccess;
using Agebull.OAuth.Business;
using Agebull.EntityModel.Events;
using Agebull.MicroZero;
using Agebull.MicroZero.Entity;

namespace DemoDataModel
{
    class Program
    {
        static void Main(string[] args)
        {
            IocHelper.AddScoped<IRedis, StackExchangeRedis>();
            IocHelper.AddSingleton<IRedis, StackExchangeRedis>();
            IocHelper.AddTransient<IEntityEventProxy, EntityEventProxy>();
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
