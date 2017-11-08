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
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Api_Interface_cs";

        /// <summary>
        ///     生成基础代码
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
    /// {Entity.Description}的实体数据操作接口
    /// </summary>
    public interface I{Entity.Name}Api
    {{
        /// <summary>
        ///     新增
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        ApiResult<{Entity.Name}> AddNew({Entity.Name} data);

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        ApiResult<{Entity.Name}> Update({Entity.Name} data);

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name=""dataKey"">数据主键</param>
        /// <returns>如果为否将阻止后续操作</returns>
        ApiResult Delete(Argument<{Entity.PrimaryColumn?.LastCsType ?? "int"}> dataKey);

        /// <summary>
        ///     分页
        /// </summary>
        ApiResult<ApiPageData<{Entity.Name}>> Query(PageArgument arg);

    }}
}}";
            var file = ConfigPath(Entity, FileSaveConfigName, path, "Interface", $"I{Entity.Name}EntityApi.cs");
            SaveCode(file, code);
        }

        /// <summary>
        ///     生成基础代码
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
    /// 身份验证服务API
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
        /// <returns>操作结果</returns>");
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