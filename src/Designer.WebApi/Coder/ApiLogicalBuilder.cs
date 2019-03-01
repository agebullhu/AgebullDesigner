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
        protected override void CreateBaCode(string path)
        {
            if (Entity.ExtendConfigListBool["NoApi"])
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
    /// {Entity.Description}ʵ�����APIʵ��
    /// </summary>
    public partial class {Entity.Name}ApiLogical : I{Entity.Name}Api
    {{
        
        /// <summary>
        /// ����ת��
        /// </summary>
        /// <param name=""src"">����</param>
        /// <returns>���ݿ����</returns>
        private {Entity.Name}Data ToData({Entity.Name} src)
        {{
            return new {Entity.Name}Data
            {{
{CopyCode()}
            }};
        }}

        /// <summary>
        /// ����ת��
        /// </summary>
        /// <param name=""src"">���ݿ����</param>
        /// <returns>����</returns>
        private {Entity.Name} FromData({Entity.Name}Data src)
        {{
            return new {Entity.Name}
            {{
{CopyCode()}
            }};
        }}
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        ApiResultEx<{Entity.Name}> I{Entity.Name}Api.AddNew({Entity.Name} data)
        {{
            return AddNew(data);
        }}

        /// <summary>
        ///     �޸�
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        ApiResultEx<{Entity.Name}> I{Entity.Name}Api.Update({Entity.Name} data)
        {{
            return Update(data);
        }}

        /// <summary>
        ///     ɾ��
        /// </summary>
        /// <param name=""dataKey"">��������</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        ApiResultEx I{Entity.Name}Api.Delete(Argument<{Entity.PrimaryColumn?.LastCsType ?? "int"}> dataKey)
        {{
            return Delete(dataKey);
        }}

        /// <summary>
        ///     ��ҳ
        /// </summary>
        ApiResultEx<ApiPageData<{Entity.Name}>> I{Entity.Name}Api.Query(PageArgument arg)
        {{
            return Query(arg);
        }}

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name=""arg"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        private ApiResultEx<{Entity.Name}> AddNew({Entity.Name} arg)
        {{
            try
            {{
                var data = ToData(arg);
                var vr = data.Validate();
                if(!vr.succeed)
                    return ApiResultEx<{Entity.Name}>.ErrorResult(ErrorCode.ArgumentError, vr.Messages.LinkToString(""��""));
                using (BusinessContextScope.CreateScope())
                {{
                    var bl = new {Entity.Name}BusinessLogic();
                    if (bl.AddNew(data))
                        return ApiResultEx<{Entity.Name}>.ErrorResult(ErrorCode.InnerError, BusinessContext.Current.LastMessage);
                    return ApiResultEx<{Entity.Name}>.Succees(FromData(data));
                }}
            }}
            catch (Exception e)
            {{
                LogRecorder.Exception(e);
                return ApiResultEx<{Entity.Name}>.ErrorResult(ErrorCode.InnerError,""�ڲ�����"");
            }}
        }}

        /// <summary>
        ///     �޸�
        /// </summary>
        /// <param name=""arg"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        private ApiResultEx<{Entity.Name}> Update({Entity.Name} arg)
        {{
            try
            {{
                var data = ToData(arg);
                var vr = data.Validate();
                if(!vr.succeed)
                    return ApiResultEx<{Entity.Name}>.ErrorResult(ErrorCode.ArgumentError, vr.Messages.LinkToString(""��""));
                using (BusinessContextScope.CreateScope())
                {{
                    var bl = new {Entity.Name}BusinessLogic();
                    if (bl.Update(data))
                        return ApiResultEx<{Entity.Name}>.ErrorResult(ErrorCode.InnerError, BusinessContext.Current.LastMessage);
                    return ApiResultEx<{Entity.Name}>.Succees(FromData(data));
                }}
            }}
            catch (Exception e)
            {{
                LogRecorder.Exception(e);
                return ApiResultEx<{Entity.Name}>.ErrorResult(ErrorCode.InnerError,""�ڲ�����"");
            }}
        }}

        /// <summary>
        ///     ɾ��
        /// </summary>
        /// <param name=""arg"">��������</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        private ApiResultEx Delete(Argument<{Entity.PrimaryColumn?.LastCsType ?? "int"}> arg)
        {{
            try
            {{
                using (BusinessContextScope.CreateScope())
                {{
                    var bl = new {Entity.Name}BusinessLogic();
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
        private ApiResultEx<ApiPageData<{Entity.Name}>> Query(PageArgument arg)
        {{
            try
            {{
                string message;
                if (!arg.Validate(out message))
                    return ApiResultEx<ApiPageData<{Entity.Name}>>.ErrorResult(ErrorCode.ArgumentError, message);
                using (BusinessContextScope.CreateScope())
                {{
                    var bl = new {Entity.Name}BusinessLogic();
                    var data = bl.PageData(arg.Page, arg.PageSize, arg.Order, arg.Desc, null);
                    return new ApiPageResult<{Entity.Name}>
                    {{
                        Result = true,
                        ResultData = new ApiPageData<{Entity.Name}>
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
                return ApiResultEx<ApiPageData<{Entity.Name}>>.ErrorResult(ErrorCode.InnerError,""�ڲ�����"");
            }}
        }}
    }}
}}";
            var file = ConfigPath(Entity, FileSaveConfigName, path, "Logical", $"{Entity.Name}ApiLogical.cs");

            SaveCode(file, code);
        }

        string CopyCode()
        {
            StringBuilder code = new StringBuilder();
            bool isFirst = true;
            foreach (var property in Entity.Properties.Where(p => p.CanUserInput))
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
        protected override void CreateExCode(string path)
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

