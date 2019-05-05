using Agebull.Common.AppManage.DataAccess;
using Agebull.Common.Context;
using Agebull.Common.Ioc;
using Agebull.Common.Organizations;
using Agebull.Common.Organizations.DataAccess;
using Agebull.EntityModel.Redis;
using Agebull.MicroZero;
using Agebull.OAuth.Business;
using HPC.Projects.DataAccess;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            IocHelper.AddScoped<IEncrypt, NoEncrypt>();
            IocHelper.AddScoped<IRedis, StackExchangeRedis>();
            //IocHelper.AddScoped<IEntityEventProxy, EntityEventProxy>();
            IocHelper.AddScoped<UserCenterDb, UserCenterDb>();
            IocHelper.AddScoped<AppManageDb, AppManageDb>();
            IocHelper.AddScoped<IOAuthBusiness, AuthBusiness>();
            IocHelper.AddScoped<IUserApi, UserBusinessLogical>();
            IocHelper.AddScoped<HpcSqlServerDb, HpcSqlServerDb>();
            IocHelper.AddScoped<GlobalContext, GlobalContext>();
            ZeroApplication.CheckOption();
            ZeroApplication.Discove(typeof(AuthBusiness).Assembly, "Authority");
            ZeroApplication.Initialize();
            ZeroApplication.RunAwaite();
        }
    }
}
