using System;
using Agebull.Common.Configuration;
using Agebull.ZeroNet.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Agebull.ZeroNet.ManageApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigurationManager.BasePath = Environment.CurrentDirectory;
            CreateWebHostBuilder(args).Build().Run();
            ZeroApplication.Shutdown();
        }
        


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(ConfigurationManager.Root)
                .UseStartup<Startup>();
    }


}
