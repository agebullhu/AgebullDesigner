using System;
using Agebull.Common.DataModel.Redis;
using Agebull.Common.Ioc;
using Agebull.ZeroNet.Core;
using Gboxt.Common.DataModel.ExtendEvents;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ZeroApplication.CheckOption();
            ZeroApplication.Discove(typeof(DemoApiControl).Assembly, "Demo");
            ZeroApplication.Initialize();

            ZeroApplication.RunAwaite();
        }
    }
}
