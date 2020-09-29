using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{


    public sealed class ApiLogicalBuilder : EntityCoderBase
    {
        /// <summary>
        /// 是否可写
        /// </summary>
        protected override bool CanWrite => true;

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_API_Logical_cs";
        /// <summary>
        /// 是否客户端代码
        /// </summary>
        protected override bool IsClient => true;

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            if (Model.ExtendConfigListBool["NoApi"])
                return;
            string code = $@"
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

using {NameSpace}.BusinessLogic;

namespace {NameSpace}.WebApi.EntityApi
{{
    /// <summary>
    /// {Model.Description}实体操作API实现
    /// </summary>
    public partial class {Model.Name}ApiLogical : I{Model.Name}Api
    {{
        
        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name=""src"">参数</param>
        /// <returns>数据库对象</returns>
        private {Model.Name}Data ToData({Model.Name} src)
        {{
            return new {Model.Name}Data
            {{
{CopyCode()}
            }};
        }}

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name=""src"">数据库对象</param>
        /// <returns>参数</returns>
        private {Model.Name} FromData({Model.Name}Data src)
        {{
            return new {Model.Name}
            {{
{CopyCode()}
            }};
        }}
        /// <summary>
        ///     新增
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        ApiResultEx<{Model.Name}> I{Model.Name}Api.AddNew({Model.Name} data)
        {{
            return AddNew(data);
        }}

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        ApiResultEx<{Model.Name}> I{Model.Name}Api.Update({Model.Name} data)
        {{
            return Update(data);
        }}

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name=""dataKey"">数据主键</param>
        /// <returns>如果为否将阻止后续操作</returns>
        ApiResultEx I{Model.Name}Api.Delete(Argument<{Model.PrimaryColumn?.LastCsType ?? "int"}> dataKey)
        {{
            return Delete(dataKey);
        }}

        /// <summary>
        ///     分页
        /// </summary>
        ApiResultEx<ApiPageData<{Model.Name}>> I{Model.Name}Api.Query(PageArgument arg)
        {{
            return Query(arg);
        }}

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name=""arg"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        private ApiResultEx<{Model.Name}> AddNew({Model.Name} arg)
        {{
            try
            {{
                var data = ToData(arg);
                var vr = data.Validate();
                if(!vr.succeed)
                    return ApiResultEx<{Model.Name}>.ErrorResult(ErrorCode.ArgumentError, vr.Messages.LinkToString(""；""));
                using (BusinessContextScope.CreateScope())
                {{
                    var bl = new {Model.Name}BusinessLogic();
                    if (bl.AddNew(data))
                        return ApiResultEx<{Model.Name}>.ErrorResult(ErrorCode.InnerError, BusinessContext.Current.LastMessage);
                    return ApiResultEx<{Model.Name}>.Succees(FromData(data));
                }}
            }}
            catch (Exception e)
            {{
                LogRecorder.Exception(e);
                return ApiResultEx<{Model.Name}>.ErrorResult(ErrorCode.InnerError,""内部错误"");
            }}
        }}

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name=""arg"">数据</param>
        /// <returns>如果为真并返回结果数据</returns>
        private ApiResultEx<{Model.Name}> Update({Model.Name} arg)
        {{
            try
            {{
                var data = ToData(arg);
                var vr = data.Validate();
                if(!vr.succeed)
                    return ApiResultEx<{Model.Name}>.ErrorResult(ErrorCode.ArgumentError, vr.Messages.LinkToString(""；""));
                using (BusinessContextScope.CreateScope())
                {{
                    var bl = new {Model.Name}BusinessLogic();
                    if (bl.Update(data))
                        return ApiResultEx<{Model.Name}>.ErrorResult(ErrorCode.InnerError, BusinessContext.Current.LastMessage);
                    return ApiResultEx<{Model.Name}>.Succees(FromData(data));
                }}
            }}
            catch (Exception e)
            {{
                LogRecorder.Exception(e);
                return ApiResultEx<{Model.Name}>.ErrorResult(ErrorCode.InnerError,""内部错误"");
            }}
        }}

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name=""arg"">数据主键</param>
        /// <returns>如果为否将阻止后续操作</returns>
        private ApiResultEx Delete(Argument<{Model.PrimaryColumn?.LastCsType ?? "int"}> arg)
        {{
            try
            {{
                using (BusinessContextScope.CreateScope())
                {{
                    var bl = new {Model.Name}BusinessLogic();
                    if (bl.Delete(arg.Value))
                        return ApiResultEx.Error(ErrorCode.InnerError, BusinessContext.Current.LastMessage);
                    return ApiResultEx.Succees();
                }}
            }}
            catch (Exception e)
            {{
                LogRecorder.Exception(e);
                return ApiResultEx.Error(ErrorCode.InnerError,""内部错误"");
            }}
        }}

        /// <summary>
        ///     分页
        /// </summary>
        private ApiResultEx<ApiPageData<{Model.Name}>> Query(PageArgument arg)
        {{
            try
            {{
                string message;
                if (!arg.Validate(out message))
                    return ApiResultEx<ApiPageData<{Model.Name}>>.ErrorResult(ErrorCode.ArgumentError, message);
                using (BusinessContextScope.CreateScope())
                {{
                    var bl = new {Model.Name}BusinessLogic();
                    var data = bl.PageData(arg.Page, arg.PageSize, arg.Order, arg.Desc, null);
                    return new ApiPageResult<{Model.Name}>
                    {{
                        Result = true,
                        ResultData = new ApiPageData<{Model.Name}>
                        {{
                            PageSize = arg.PageSize,
                            PageIndex = arg.Page,
                            RowCount = data.Total,
                            Rows = data.Data.Select(FromData).ToList()
                        }}
                    }};
                }}
            }}
            catch (Exception e)
            {{
                LogRecorder.Exception(e);
                return ApiResultEx<ApiPageData<{Model.Name}>>.ErrorResult(ErrorCode.InnerError,""内部错误"");
            }}
        }}
    }}
}}";
            var file = ConfigPath(Model, FileSaveConfigName, path, "Logical", $"{Model.Name}ApiLogical.cs");

            SaveCode(file, code);
        }

        string CopyCode()
        {
            StringBuilder code = new StringBuilder();
            bool isFirst = true;
            foreach (var property in Model.LastProperties.Where(p => p.CanUserInput))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.AppendLine(",");
                code.Append($"                {property.Name} = src.{property.Name}");
            }
            return code.ToString();
        }


        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            ProjectApiBase(path);
            ProjectApiExtend(path);
        }

        private void ProjectApiBase(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"using System;
using System.Linq;
using Gboxt.Common.DataModel;

namespace {NameSpace}
{{
   {RemCode(Project)}
    public partial class {Project.ApiName}Logical : I{Project.ApiName}
    {{");

            foreach (var item in Project.ApiItems)
            {
                var res = item.Result == null ? "ApiResultEx" : "ApiResultEx <" + item.ResultArg + ">";
                var arg = item.Argument == null ? "ApiArgument" : $"ApiArgument<{ item.CallArg}>";

                code.Append(RemCode(item));
                code.Append($@"
        /// <param name=""arg"">{item.CallArg ?? "标准参数"}</param>
        /// <returns>{item.ResultArg ?? "操作结果"}</returns>
        public {res} {item.Name}({arg} arg)
        {{
            var result = new {res}(arg);
            if (!arg.Validate(out var message))
                return result.Error(ErrorCode.LogicalError, message);
            {item.Name}(arg , result);
            return result;
        }}

        partial void {item.Name}({arg} arg, {res} result);
");
            }

            code.Append(@"
    }
}");
            var file = ConfigPath(Project, FileSaveConfigName, path, "Logical", $"{Project.ApiName}_Logical.Designer.cs");
            WriteFile(file, code.ToString());
        }


        private void ProjectApiExtend(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"using System;
using Agebull.ZeroNet.ZeroApi;
using Gboxt.Common.DataModel;

namespace {NameSpace}
{{
   {RemCode(Project)}
    partial class {Project.ApiName}Logical
    {{");

            foreach (var item in Project.ApiItems)
            {
                var res = item.Result == null ? "ApiResultEx" : "ApiResultEx <" + item.ResultArg + ">";
                var arg = item.Argument == null ? "ApiArgument" : $"ApiArgument<{ item.CallArg}>";

                code.Append(RemCode(item));
                code.Append($@"
        /// <param name=""arg"">{item.CallArg ?? "标准参数"}</param>
        /// <param name=""result"">{item.ResultArg ?? "操作结果"}</param>
        partial void {item.Name}({arg} arg, {res} result)
        {{");
                if (item.Result != null)
                {
                    code.Append(@"
            result.Data = ");
                    using (CodeGeneratorScope.CreateScope(item.Result))
                    {
                        code.Append(HelloCode(item.Result));
                    }
                    code.Append(";");
                }
                code.Append(@"
        }");
            }

            code.Append(@"
    }
}");
            var file = ConfigPath(Project, FileSaveConfigName + "_2", path, "Logical", $"{Project.ApiName}_Logical.cs");
            WriteFile(file, code.ToString());
        }

    }

}

