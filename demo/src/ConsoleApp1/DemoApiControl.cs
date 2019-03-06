using Agebull.ZeroNet.ZeroApi;
using System;
using System.Collections.Generic;
using System.Text;
using Gboxt.Common.DataModel;

namespace ConsoleApp1
{
    [RoutePrefix("api/v1")]
    class DemoApiControl : ApiController
    {
        [Route("test")]
        public string Test1()
        {
            return "hello zero net";
        }


        [Route("test/2")]
        public ApiResult Test(Arg arg)
        {
            return ApiResult.Ok;
        }
    }

    public class Arg : IApiArgument
    {
        public bool Validate(out string message)
        {
            message = "No";
            return true;
        }
    }
}
