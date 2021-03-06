using System.IO;
using System.Text;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{


    public sealed class ApiControlerBuillder : EntityCoderBase
    {
        /// <summary>
        /// 是否可写
        /// </summary>
        protected override bool CanWrite => true;

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Api_Controller_cs";

        /// <summary>
        /// 是否客户端代码
        /// </summary>
        protected override bool IsClient => true;

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {

            string code = $@"using System;
using System.Web.Http;
using GoodLin.Common.Ioc;
using Yizuan.Service.Api;
using Yizuan.Service.Api.WebApi;

namespace {NameSpace}.WebApi.EntityApi
{{
    /// <summary>
    /// 身份验证服务API
    /// </summary>
    public class {Entity.Name}ApiController : ApiController
    {{
        /// <summary>
        ///     新增
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        [Route(""entity/{Entity.Name}/AddNew"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public <ApiResultEx<{Entity.Name}>> AddNew([FromBody]{Entity.Name} data)
        {{
            var lg = new {Entity.Name}ApiLogical() as I{Entity.Name}Api;
            var result = lg.AddNew(data);
            return Request.ToResponse(result);
        }}

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        [Route(""entity/{Entity.Name}/Update"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public <ApiResultEx<{Entity.Name}>> Update([FromBody]{Entity.Name} data)
        {{
            var lg = new {Entity.Name}ApiLogical() as I{Entity.Name}Api;
            var result = lg.Update(data);
            return Request.ToResponse(result);
        }}

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name=""dataKey"">数据主键</param>
        /// <returns>如果为否将阻止后续操作</returns>
        [Route(""entity/{Entity.Name}/Delete"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public  Delete([FromBody]Argument<int> dataKey)
        {{
            var lg = new {Entity.Name}ApiLogical() as I{Entity.Name}Api;
            var result = lg.Delete(dataKey);
            return Request.ToResponse(result);
        }}

        /// <summary>
        ///     分页
        /// </summary>
        [Route(""entity/{Entity.Name}/Query"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public <ApiResultEx<ApiPageData<{Entity.Name}>>> Query([FromBody]PageArgument arg)
        {{
            var lg = new {Entity.Name}ApiLogical() as I{Entity.Name}Api;
            var result = lg.Query(arg);
            return Request.ToResponse(result);
        }}

    }}
}}
";
            var file = ConfigPath(Entity, FileSaveConfigName, path, "Controllers", $"{Entity.Name}Controller.cs");
            SaveCode(file, code);
        }


        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            if (Project.ApiItems.Count == 0)
                return;
            StringBuilder code = new StringBuilder();
            code.Append($@"using System;
using System.Collections.Generic;
using Agebull.Common.Ioc;
using Agebull.ZeroNet.ZeroApi;
using Gboxt.Common.DataModel;
using Newtonsoft.Json;

namespace {NameSpace}
{{
    {RemCode(Project, 4)}
    public class {Project.ApiName}Controller : ApiController
    {{");

            foreach (var item in Project.ApiItems)
            {
                var arg = item.Argument == null ? "ApiArgument" : $"ApiArgument<{ item.CallArg}>";

                code.Append(RemCode(item));
                code.Append($@"
        /// <param name=""arg"">{item.CallArg ?? "标准参数"}</param>
        /// <returns>{item.ResultArg ?? "操作结果"}</returns>
        [Route(""{item.RoutePath.Trim('\\', '/')}"")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public ApiResultEx {item.Name}({arg} arg)
        {{
            var lg = IocHelper.Create<I{Project.ApiName}>();
            return lg.{item.Name}(arg);
        }}
");
            }

            foreach (var item in Project.ApiItems)
            {
                var arg = item.Argument == null ? "ApiArgument" : $"ApiArgument<{ item.CallArg}>";

                code.Append(RemCode(item));
                code.Append($@"
        /// <param name=""arg"">{item.CallArg ?? "标准参数"}</param>
        /// <returns>{item.ResultArg ?? "操作结果"}</returns>
        [Route(""{item.RoutePath.Trim('\\','/')}/json"")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public string {item.Name}_Json({arg} arg)
        {{
            var lg = IocHelper.Create<I{Project.ApiName}>();
            var res = lg.{item.Name}(arg);
            return JsonConvert.SerializeObject(res,Formatting.Indented);
        }}
");
            }

            foreach (var item in Project.ApiItems)
            {
                var arg = item.Argument == null ? "ApiArgument" : $"ApiArgument<{ item.CallArg}>";

                code.Append(RemCode(item));
                code.Append($@"
        /// <param name=""arg"">{item.CallArg ?? "标准参数"}</param>
        /// <returns>{item.ResultArg ?? "操作结果"}</returns>
        [Route(""{item.RoutePath.Trim('\\', '/')}/xml"")]
        //[ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public string {item.Name}_Xml({arg} arg)
        {{
            var lg = IocHelper.Create<I{Project.ApiName}>();
            var res = lg.{item.Name}(arg);
            return JsonConvert.DeserializeXmlNode(JsonConvert.SerializeObject(res), ""xml"").OuterXml;
        }}
");
            }
            code.Append(@"
    }
}
");
            var file = ConfigPath(Project, FileSaveConfigName, path, "Controllers", $"{Project.Name}_Controller.cs");
            WriteFile(file, code.ToString());
        }
    }

}
