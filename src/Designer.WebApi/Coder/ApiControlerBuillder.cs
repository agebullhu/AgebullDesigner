using System.IO;
using System.Text;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{


    public sealed class ApiControlerBuillder : EntityCoderBase
    {
        /// <summary>
        /// �Ƿ��д
        /// </summary>
        protected override bool CanWrite => true;

        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Api_Controller_cs";

        /// <summary>
        /// �Ƿ�ͻ��˴���
        /// </summary>
        protected override bool IsClient => true;

        /// <summary>
        ///     ����ʵ�����
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
    /// �����֤����API
    /// </summary>
    public class {Entity.Name}ApiController : ApiController
    {{
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        [Route(""entity/{Entity.Name}/AddNew"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public <ApiResultEx<{Entity.Name}>> AddNew([FromBody]{Entity.Name} data)
        {{
            var lg = new {Entity.Name}ApiLogical() as I{Entity.Name}Api;
            var result = lg.AddNew(data);
            return Request.ToResponse(result);
        }}

        /// <summary>
        ///     �޸�
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        [Route(""entity/{Entity.Name}/Update"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public <ApiResultEx<{Entity.Name}>> Update([FromBody]{Entity.Name} data)
        {{
            var lg = new {Entity.Name}ApiLogical() as I{Entity.Name}Api;
            var result = lg.Update(data);
            return Request.ToResponse(result);
        }}

        /// <summary>
        ///     ɾ��
        /// </summary>
        /// <param name=""dataKey"">��������</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        [Route(""entity/{Entity.Name}/Delete"")]
        [ApiAccessOptionFilter(ApiAccessOption.Internal | ApiAccessOption.Public | ApiAccessOption.Anymouse)]
        public  Delete([FromBody]Argument<int> dataKey)
        {{
            var lg = new {Entity.Name}ApiLogical() as I{Entity.Name}Api;
            var result = lg.Delete(dataKey);
            return Request.ToResponse(result);
        }}

        /// <summary>
        ///     ��ҳ
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
        ///     ����ʵ�����
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
        /// <param name=""arg"">{item.CallArg ?? "��׼����"}</param>
        /// <returns>{item.ResultArg ?? "�������"}</returns>
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
        /// <param name=""arg"">{item.CallArg ?? "��׼����"}</param>
        /// <returns>{item.ResultArg ?? "�������"}</returns>
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
        /// <param name=""arg"">{item.CallArg ?? "��׼����"}</param>
        /// <returns>{item.ResultArg ?? "�������"}</returns>
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
