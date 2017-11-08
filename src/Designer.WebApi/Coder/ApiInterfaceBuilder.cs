using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    public sealed class ApiInterfaceBuilder : CoderWithEntity
    {
        protected override bool CanWrite => true;

        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Api_Interface_cs";

        /// <summary>
        ///     ���ɻ�������
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            if (Entity.ExtendConfigListBool["NoApi"])
                return;
            var code = $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

using Gboxt.Common.DataModel;
using Yizuan.Service.Api;


namespace {NameSpace}.WebApi.EntityApi
{{
    /// <summary>
    /// {Entity.Description}��ʵ�����ݲ����ӿ�
    /// </summary>
    public interface I{Entity.Name}Api
    {{
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        ApiResult<{Entity.Name}> AddNew({Entity.Name} data);

        /// <summary>
        ///     �޸�
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        ApiResult<{Entity.Name}> Update({Entity.Name} data);

        /// <summary>
        ///     ɾ��
        /// </summary>
        /// <param name=""dataKey"">��������</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        ApiResult Delete(Argument<{Entity.PrimaryColumn?.LastCsType ?? "int"}> dataKey);

        /// <summary>
        ///     ��ҳ
        /// </summary>
        ApiResult<ApiPageData<{Entity.Name}>> Query(PageArgument arg);

    }}
}}";
            var file = ConfigPath(Entity, FileSaveConfigName, path, "Interface", $"I{Entity.Name}EntityApi.cs");
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
    /// �����֤����API
    /// </summary>
    public interface I{Project.Name}Api
    {{");

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
        ApiResult{res} {item.Name}({arg});");
            }

            code.Append(@"
    }
}
");
            var file = ConfigPath(Project, FileSaveConfigName, path,  "Interface", $"I{Project.Name}Api.cs");
            SaveCode(file, code.ToString());
        }

    }
}