using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    public sealed class ApiProxyBuilder<TModel> : CoderWithModel<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        protected override bool CanWrite => true;
        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Api_Proxy_cs";

        /// <summary>
        ///     ���ɻ�������
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            if (Model.ExtendConfigListBool["NoApi"])
                return;
            var code = $@"
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.DataModel;
using Gboxt.Common.DataModel;
using Agebull.Common.WebApi;

{Project.UsingNameSpaces}

namespace {NameSpace}.WebApi.EntityApi
{{
    /// <summary>
    /// {Model.Description}��ʵ�����ݲ����ӿڴ�����
    /// </summary>
    public class {Model.Name}ApiProxy : I{Model.Name}Api
    {{
        
        /// <summary>
        ///     �����ַ
        /// </summary>
        public string Host
        {{
            get
            {{
                return Caller.Host;
            }}
            set
            {{
                Caller.Host = value;
            }}
        }}
        
        /// <summary>
        ///     ������ö���
        /// </summary>
        public readonly WebApiCaller Caller = new WebApiCaller();

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        public ApiResultEx<{Model.Name}> AddNew({Model.Name} data)
        {{
            return Caller.Post<{Model.Name}>(""entity/{Model.Name}/AddNew"", data);
        }}

        /// <summary>
        ///     �޸�
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        public ApiResultEx<{Model.Name}> Update({Model.Name} data)
        {{
            return Caller.Post<{Model.Name}>(""entity/{Model.Name}/Update"", data);
        }}

        /// <summary>
        ///     ɾ��
        /// </summary>
        /// <param name=""dataKey"">��������</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        public ApiResultEx Delete(Argument<{Model.PrimaryColumn?.LastCsType ?? "int"}> dataKey)
        {{
            return Caller.Post(""entity/{Model.Name}/Delete"", dataKey);
        }}

        /// <summary>
        ///     ��ҳ
        /// </summary>
        /// <param name=""arg"">��ҳ����</param>
        /// <returns>�������</returns>
        public ApiResultEx<ApiPageData<{Model.Name}>> Query(PageArgument arg)
        {{
            return Caller.Post<ApiPageData<{Model.Name}>>(""entity/{Model.Name}/Query"", arg);
        }}
    }}
}}";
            var file = ConfigPath(Model, FileSaveConfigName, path, "Proxy", $"{Model.Name}EntityApi.cs");
            SaveCode(file, code);
        }

        /// <summary>
        ///     ���ɻ�������
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"using System;
using Agebull.Common.Logging;
using Agebull.Common.WebApi;
using Agebull.ZeroNet.ZeroApi;
using Gboxt.Common.DataModel;
using Newtonsoft.Json;

namespace {NameSpace}
{{
    
    {RemCode(Project, 4)}
    public class {Project.ApiName}Proxy : I{Project.ApiName}
    {{
");

            foreach (var item in Project.ApiItems)
            {
                var res = item.Result == null ? "ApiResultEx" : "ApiResultEx <" + item.ResultArg + ">";
                var arg = item.Argument == null ? "ApiArgument" : $"ApiArgument<{ item.CallArg}>";

                code.Append(RemCode(item));
                code.Append($@"
        /// <param name=""arg"">{item.CallArg ?? "��׼����"}</param>
        /// <returns>{item.ResultArg ?? "�������"}</returns>
        public {res} {item.Name}({arg} arg)
        {{
            if (!arg.Validate(out var message))
                return {res}.Error(ErrorCode.LogicalError, message);
            return ApiClient.Call<{ arg},{ res}>(""{Project.ApiName}"", ""{item.RoutePath}"",arg);
        }}
");
            }

            code.Append(@"
    }
}");
            var file = ConfigPath(Project, FileSaveConfigName, path, "Proxy", $"{Project.ApiName}Proxy.cs");
            SaveCode(file, code.ToString());
        }

    }
}