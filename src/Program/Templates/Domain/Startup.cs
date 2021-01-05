using Agebull.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Agebull.Common.Ioc;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Events;
using ZeroTeam.MessageMVC;
using ZeroTeam.MessageMVC.Http;
using ZeroTeam.MessageMVC.Messages;
using ZeroTeam.MessageMVC.RedisMQ;
using ZeroTeam.MessageMVC.Web;
using @namespace.DataAccess;
using @namespace.WebApi;

namespace @namespace
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.BindingMessageMvc();
            IocRegist(services);
            services.AddMessageMvcRedisEvent();
            services.AddMessageMvcHttp();
            services.AddMessageMvc();
        }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="_"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment _)
        {
            app.UseStaticFiles();
            app.UseDefaultFiles("/index.html");
            app.UseWebSocketNotify();
            app.UseMessageMVC(true);
        }


        private static void IocRegist(IServiceCollection services)
        {
            services.BindingMessageMvc();
            //services.AddSingleton<IJsonSerializeProxy, JsonSerializeProxy>();
            services.AddSingleton<IIdGenerator, SnowFlakeIdGenerator>();

            @dbRegist
            services.AddScoped(typeof(IOperatorInjection<>), typeof(OperatorInjection<>));

            services.AddTransient<ISerializeProxy, NewtonJsonSerializeProxy>();
            services.AddTransient<IJsonSerializeProxy, NewtonJsonSerializeProxy>();
        }
    }
}