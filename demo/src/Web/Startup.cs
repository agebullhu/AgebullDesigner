using System.Threading.Tasks;
using Agebull.Common.AppManage.BusinessLogic;
using Agebull.Common.AppManage.DataAccess;
using Agebull.Common.AppManage.WebApi.Entity;
using Agebull.Common.OAuth;
using Agebull.Common.Organizations.DataAccess;
using Agebull.Common.Configuration;
using Agebull.EntityModel.Redis;
using Agebull.Common.Ioc;
using Agebull.Common.UserCenter;
using Agebull.Common.UserCenter.BusinessLogic;
using Agebull.OAuth.Business;
using Agebull.ZeroNet.WebSocket;
using Agebull.ZeroNet.ManageApplication.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Agebull.MicroZero;
using Agebull.EntityModel.Events;
using Agebull.Common.Context;
using Agebull.Common.Organizations;
using Agebull.Common.Organizations.WebApi.Entity;
using HPC.Projects.DataAccess;

namespace Agebull.ZeroNet.ManageApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigurationManager.SetConfiguration(configuration);
            IocHelper.AddScoped<IEncrypt, NoEncrypt>();
            IocHelper.AddScoped<IPowerChecker, AuthorityChecker>();
            IocHelper.AddScoped<IRoleCache, RoleCache>(); 
            IocHelper.AddScoped<IRedis, StackExchangeRedis>();
            IocHelper.AddScoped<IEntityEventProxy, EntityEventProxy>();
            IocHelper.AddScoped<UserCenterDb, UserCenterDb>();
            IocHelper.AddScoped<AppManageDb, AppManageDb>();

            IocHelper.AddScoped<IOAuthBusiness, AuthBusiness>();
            IocHelper.AddScoped<IUserApi, UserBusinessLogical>();
            ZeroApplication.CheckOption();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            IocHelper.SetServiceCollection(services);
            IocHelper.AddScoped<AnnexDb, AnnexDb>();
            IocHelper.AddScoped<HpcSqlServerDb, HpcSqlServerDb>();
            IocHelper.AddScoped<GlobalContext, GlobalContext>();
            ZeroApplication.RegistZeroObject<Zero2WebSocketBridge>();
            ZeroApplication.Discove(typeof(UserApiController).Assembly, "User");
            ZeroApplication.Discove(typeof(AppInfoApiController).Assembly, "App");
            ZeroApplication.Discove(typeof(AuthBusiness).Assembly, "Authority");

            ZeroApplication.Initialize();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            WebSocketNotify.Binding(app);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseFileServer();

            Task.Factory.StartNew(ZeroApplication.Run);
        }
    }
}
