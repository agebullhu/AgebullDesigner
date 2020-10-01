using System.IO;
using System.Text;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{


    public sealed class ApiControlerBuillder : ModelCoderBase
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
        protected override void CreateDesignerCode(string path)
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
    public class {Model.Name}ApiController : ApiController
    {{
        /// <summary>
        ///     新增
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        [Route(""entity/{Model.Name}/AddNew"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public <ApiResultEx<{Model.Name}>> AddNew([FromBody]{Model.Name} data)
        {{
            var lg = new {Model.Name}ApiLogical() as I{Model.Name}Api;
            var result = lg.AddNew(data);
            return Request.ToResponse(result);
        }}

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        [Route(""entity/{Model.Name}/Update"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public <ApiResultEx<{Model.Name}>> Update([FromBody]{Model.Name} data)
        {{
            var lg = new {Model.Name}ApiLogical() as I{Model.Name}Api;
            var result = lg.Update(data);
            return Request.ToResponse(result);
        }}

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name=""dataKey"">数据主键</param>
        /// <returns>如果为否将阻止后续操作</returns>
        [Route(""entity/{Model.Name}/Delete"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public  Delete([FromBody]Argument<int> dataKey)
        {{
            var lg = new {Model.Name}ApiLogical() as I{Model.Name}Api;
            var result = lg.Delete(dataKey);
            return Request.ToResponse(result);
        }}

        /// <summary>
        ///     分页
        /// </summary>
        [Route(""entity/{Model.Name}/Query"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public <ApiResultEx<ApiPageData<{Model.Name}>>> Query([FromBody]PageArgument arg)
        {{
            var lg = new {Model.Name}ApiLogical() as I{Model.Name}Api;
            var result = lg.Query(arg);
            return Request.ToResponse(result);
        }}

    }}
}}
";
            var file = ConfigPath(Model, FileSaveConfigName, path, "Controllers", $"{Model.Name}Controller.cs");
            SaveCode(file, code);
        }


        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateCustomCode(string path)
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
