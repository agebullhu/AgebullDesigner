using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    public sealed class ApiInterfaceBuilder : CoderWithModel
    {
        protected override bool CanWrite => true;

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Api_Interface_cs";

        /// <summary>
        ///     生成基础代码
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
    /// {Model.Description}的实体数据操作接口
    /// </summary>
    public interface I{Model.Name}Api
    {{
        /// <summary>
        ///     新增
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        ApiResultEx<{Model.Name}> AddNew({Model.Name} data);

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        ApiResultEx<{Model.Name}> Update({Model.Name} data);

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name=""dataKey"">数据主键</param>
        /// <returns>如果为否将阻止后续操作</returns>
        ApiResultEx Delete(Argument<{Model.PrimaryColumn?.LastCsType ?? "int"}> dataKey);

        /// <summary>
        ///     分页
        /// </summary>
        ApiResultEx<ApiPageData<{Model.Name}>> Query(PageArgument arg);

    }}
}}";
            var file = ConfigPath(Model, FileSaveConfigName, path, "Interface", $"I{Model.Name}EntityApi.cs");
            SaveCode(file, code);
        }

        /// <summary>
        ///     生成基础代码
        /// </summary>
        /// <remarks></remarks>
        protected override void CreateCustomCode(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"using System;
using Agebull.ZeroNet.ZeroApi;
using Gboxt.Common.DataModel;

namespace {NameSpace}
{{
    {RemCode(Project,4)}
    public interface I{Project.ApiName}
    {{");

            foreach (var item in Project.ApiItems)
            {
                var res = item.Result == null ? "ApiResultEx" : $"ApiResultEx <{item.ResultArg}>";
                var arg = item.Argument == null ? "ApiArgument" : $"ApiArgument<{ item.CallArg}>";
                code.Append(RemCode(item));
                code.Append($@"
        /// <param name=""arg"">{item.CallArg ?? "标准参数"}</param>
        /// <returns>{item.ResultArg ?? "操作结果"}</returns>
        {res} {item.Name}({arg} arg);
");
            }

            code.Append(@"
    }
}
");
            var file = ConfigPath(Project, FileSaveConfigName, path,  "Interface", $"I{Project.ApiName}.cs");
            SaveCode(file, code.ToString());
        }

    }
}