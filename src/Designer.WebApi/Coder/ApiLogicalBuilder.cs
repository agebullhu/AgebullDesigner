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
        /// �Ƿ��д
        /// </summary>
        protected override bool CanWrite => true;

        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_API_Logical_cs";
        /// <summary>
        /// �Ƿ�ͻ��˴���
        /// </summary>
        protected override bool IsClient => true;

        /// <summary>
        ///     ����ʵ�����
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
    /// {Model.Description}ʵ�����APIʵ��
    /// </summary>
    public partial class {Model.Name}ApiLogical : I{Model.Name}Api
    {{
        
        /// <summary>
        /// ����ת��
        /// </summary>
        /// <param name=""src"">����</param>
        /// <returns>���ݿ����</returns>
        private {Model.Name}Data ToData({Model.Name} src)
        {{
            return new {Model.Name}Data
            {{
{CopyCode()}
            }};
        }}

        /// <summary>
        /// ����ת��
        /// </summary>
        /// <param name=""src"">���ݿ����</param>
        /// <returns>����</returns>
        private {Model.Name} FromData({Model.Name}Data src)
        {{
            return new {Model.Name}
            {{
{CopyCode()}
            }};
        }}
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        ApiResultEx<{Model.Name}> I{Model.Name}Api.AddNew({Model.Name} data)
        {{
            return AddNew(data);
        }}

        /// <summary>
        ///     �޸�
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        ApiResultEx<{Model.Name}> I{Model.Name}Api.Update({Model.Name} data)
        {{
            return Update(data);
        }}

        /// <summary>
        ///     ɾ��
        /// </summary>
        /// <param name=""dataKey"">��������</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        ApiResultEx I{Model.Name}Api.Delete(Argument<{Model.PrimaryColumn?.LastCsType ?? "int"}> dataKey)
        {{
            return Delete(dataKey);
        }}

        /// <summary>
        ///     ��ҳ
        /// </summary>
        ApiResultEx<ApiPageData<{Model.Name}>> I{Model.Name}Api.Query(PageArgument arg)
        {{
            return Query(arg);
        }}

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name=""arg"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        private ApiResultEx<{Model.Name}> AddNew({Model.Name} arg)
        {{
            try
            {{
                var data = ToData(arg);
                var vr = data.Validate();
                if(!vr.succeed)
                    return ApiResultEx<{Model.Name}>.ErrorResult(ErrorCode.ArgumentError, vr.Messages.LinkToString(""��""));
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
                return ApiResultEx<{Model.Name}>.ErrorResult(ErrorCode.InnerError,""�ڲ�����"");
            }}
        }}

        /// <summary>
        ///     �޸�
        /// </summary>
        /// <param name=""arg"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        private ApiResultEx<{Model.Name}> Update({Model.Name} arg)
        {{
            try
            {{
                var data = ToData(arg);
                var vr = data.Validate();
                if(!vr.succeed)
                    return ApiResultEx<{Model.Name}>.ErrorResult(ErrorCode.ArgumentError, vr.Messages.LinkToString(""��""));
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
                return ApiResultEx<{Model.Name}>.ErrorResult(ErrorCode.InnerError,""�ڲ�����"");
            }}
        }}

        /// <summary>
        ///     ɾ��
        /// </summary>
        /// <param name=""arg"">��������</param>
        /// <returns>���Ϊ����ֹ��������</returns>
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
                return ApiResultEx.Error(ErrorCode.InnerError,""�ڲ�����"");
            }}
        }}

        /// <summary>
        ///     ��ҳ
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
                return ApiResultEx<ApiPageData<{Model.Name}>>.ErrorResult(ErrorCode.InnerError,""�ڲ�����"");
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
        ///     ����ʵ�����
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
        /// <param name=""arg"">{item.CallArg ?? "��׼����"}</param>
        /// <returns>{item.ResultArg ?? "�������"}</returns>
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
        /// <param name=""arg"">{item.CallArg ?? "��׼����"}</param>
        /// <param name=""result"">{item.ResultArg ?? "�������"}</param>
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

