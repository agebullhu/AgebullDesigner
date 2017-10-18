using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    public sealed class ApiProxyBuilder : CoderWithEntity
    {
        protected override bool CanWrite => true;
        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Api_Proxy_cs";

        /// <summary>
        ///     ���ɻ�������
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            var code = $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

using Gboxt.Common.DataModel;
using Yizuan.Service.Api;
using Yizuan.Service.Api.WebApi;


namespace {NameSpace}.WebApi.EntityApi
{{
    /// <summary>
    /// {Entity.Description}��ʵ�����ݲ����ӿڴ�����
    /// </summary>
    public class {Entity.Name}ApiProxy : I{Entity.Name}Api
    {{
        
        /// <summary>
        ///     �����ַ
        /// </summary>
        public string Host
        {{
            get
            {{
                return caller.Host;
            }}
            set
            {{
                caller.Host = value;
            }}
        }}
        
        /// <summary>
        ///     ������ö���
        /// </summary>
        WebApiCaller caller = new WebApiCaller();

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        public ApiResult<{Entity.Name}> AddNew({Entity.Name} data)
        {{
            return caller.Post<{Entity.Name}>(""entity/{Entity.Name}/AddNew"", data);
        }}

        /// <summary>
        ///     �޸�
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        public ApiResult<{Entity.Name}> Update({Entity.Name} data)
        {{
            return caller.Post<{Entity.Name}>(""entity/{Entity.Name}/Update"", data);
        }}

        /// <summary>
        ///     ɾ��
        /// </summary>
        /// <param name=""dataKey"">��������</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        public ApiResult Delete(Argument<{Entity.PrimaryColumn?.LastCsType ?? "int"}> dataKey)
        {{
            return caller.Post(""entity/{Entity.Name}/Delete"", dataKey);
        }}

        /// <summary>
        ///     ��ҳ
        /// </summary>
        /// <param name=""arg"">��ҳ����</param>
        /// <returns>�������</returns>
        public ApiResult<ApiPageData<{Entity.Name}>> Query(PageArgument arg)
        {{
            return caller.Post<ApiPageData<{Entity.Name}>>(""entity/{Entity.Name}/Query"", arg);
        }}
    }}
}}";
            var file = ConfigPath(Entity, FileSaveConfigName, path, "Proxy", $"{Entity.Name}EntityApi.cs");
            SaveCode(file, code);
        }

        /// <summary>
        ///     ���ɻ�������
        /// </summary>
        protected override void CreateExCode(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"using System;
using System.Web.Http;
using GoodLin.Common.Ioc;
using GoodLin.OAuth.Api;
using Yizuan.Service.Api;
using Yizuan.Service.Api.WebApi;

namespace {NameSpace}.WebApi
{{
    /// <summary>
    /// {Project.Caption}����API
    /// </summary>
    public class {Project.Name}ApiProxy : I{Project.Name}Api
    {{
        /// <summary>
        ///     �����ַ
        /// </summary>
        public string Host
        {{
            get
            {{
                return caller.Host;
            }}
            set
            {{
                caller.Host = value;
            }}
        }}
        
        /// <summary>
        ///     ������ö���
        /// </summary>
        WebApiCaller caller = new WebApiCaller();");

            foreach (var item in Project.ApiItems)
            {
                code.Append($@"
        /// <summary>
        ///     {item.Caption}:{item.Description}:
        /// </summary>");
                if (item.Argument != null)
                {
                    code.Append($@"
        /// <param name=""arg"">{item.Argument?.Caption}</param>");
                }
                if (item.Result == null)
                {
                    code.Append(@"
        /// <returns>�������</returns>");
                }
                else
                {
                    code.Append($@"
        /// <returns>{item.Result.Caption}</returns>");
                }
                var res = item.Result == null ? null : ("<" + item.Result.Name + ">");
                var arg = item.Argument == null ? null : ($"{item.Argument.Name} arg");

                code.Append($@"
        public ApiResult{res} {item.Name}({arg})
        {{
            return caller.Post{res}({item.RoutePath}, arg);
        }}");
            }

            code.Append(@"
    }
}");
            var file = ConfigPath(Project, FileSaveConfigName, path, "Proxy", $"{Project.Name}ApiProxy.cs");
            SaveCode(file, code.ToString());
        }

    }
}