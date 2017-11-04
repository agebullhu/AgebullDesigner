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
            string code = $@"using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Agebull.Common.Logging;
using Gboxt.Common.DataModel;
using Yizuan.Service.Api;
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
        ApiResult<{Entity.Name}> I{Entity.Name}Api.AddNew({Entity.Name} data)
        {{
            return AddNew(data);
        }}

        /// <summary>
        ///     �޸�
        /// </summary>
        /// <param name=""data"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        ApiResult<{Entity.Name}> I{Entity.Name}Api.Update({Entity.Name} data)
        {{
            return Update(data);
        }}

        /// <summary>
        ///     ɾ��
        /// </summary>
        /// <param name=""dataKey"">��������</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        ApiResult I{Entity.Name}Api.Delete(Argument<{Entity.PrimaryColumn?.LastCsType ?? "int"}> dataKey)
        {{
            return Delete(dataKey);
        }}

        /// <summary>
        ///     ��ҳ
        /// </summary>
        ApiResult<ApiPageData<{Entity.Name}>> I{Entity.Name}Api.Query(PageArgument arg)
        {{
            return Query(arg);
        }}

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name=""arg"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        private ApiResult<{Entity.Name}> AddNew({Entity.Name} arg)
        {{
            try
            {{
                var data = ToData(arg);
                var vr = data.Validate();
                if(!vr.succeed)
                    return ApiResult<{Entity.Name}>.ErrorResult(ErrorCode.ArgumentError, vr.Messages.LinkToString(""��""));
                using (BusinessContextScope.CreateScope())
                {{
                    var bl = new {Entity.Name}BusinessLogic();
                    if (bl.AddNew(data))
                        return ApiResult<{Entity.Name}>.ErrorResult(ErrorCode.InnerError, BusinessContext.Current.LastMessage);
                    return ApiResult<{Entity.Name}>.Succees(FromData(data));
                }}
            }}
            catch (Exception e)
            {{
                LogRecorder.Exception(e);
                return ApiResult<{Entity.Name}>.ErrorResult(ErrorCode.InnerError,""�ڲ�����"");
            }}
        }}

        /// <summary>
        ///     �޸�
        /// </summary>
        /// <param name=""arg"">����</param>
        /// <returns>���Ϊ�沢���ؽ������</returns>
        private ApiResult<{Entity.Name}> Update({Entity.Name} arg)
        {{
            try
            {{
                var data = ToData(arg);
                var vr = data.Validate();
                if(!vr.succeed)
                    return ApiResult<{Entity.Name}>.ErrorResult(ErrorCode.ArgumentError, vr.Messages.LinkToString(""��""));
                using (BusinessContextScope.CreateScope())
                {{
                    var bl = new {Entity.Name}BusinessLogic();
                    if (bl.Update(data))
                        return ApiResult<{Entity.Name}>.ErrorResult(ErrorCode.InnerError, BusinessContext.Current.LastMessage);
                    return ApiResult<{Entity.Name}>.Succees(FromData(data));
                }}
            }}
            catch (Exception e)
            {{
                LogRecorder.Exception(e);
                return ApiResult<{Entity.Name}>.ErrorResult(ErrorCode.InnerError,""�ڲ�����"");
            }}
        }}

        /// <summary>
        ///     ɾ��
        /// </summary>
        /// <param name=""arg"">��������</param>
        /// <returns>���Ϊ����ֹ��������</returns>
        private ApiResult Delete(Argument<{Entity.PrimaryColumn?.LastCsType ?? "int"}> arg)
        {{
            try
            {{
                using (BusinessContextScope.CreateScope())
                {{
                    var bl = new {Entity.Name}BusinessLogic();
                    if (bl.Delete(arg.Value))
                        return ApiResult.Error(ErrorCode.InnerError, BusinessContext.Current.LastMessage);
                    return ApiResult.Succees();
                }}
            }}
            catch (Exception e)
            {{
                LogRecorder.Exception(e);
                return ApiResult.Error(ErrorCode.InnerError,""�ڲ�����"");
            }}
        }}

        /// <summary>
        ///     ��ҳ
        /// </summary>
        private ApiResult<ApiPageData<{Entity.Name}>> Query(PageArgument arg)
        {{
            try
            {{
                string message;
                if (!arg.Validate(out message))
                    return ApiResult<ApiPageData<{Entity.Name}>>.ErrorResult(ErrorCode.ArgumentError, message);
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
                return ApiResult<ApiPageData<{Entity.Name}>>.ErrorResult(ErrorCode.InnerError,""�ڲ�����"");
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
using GoodLin.OAuth.Api;
using Yizuan.Service.Api;
using Yizuan.Service.Api.WebApi;

namespace {NameSpace}.WebApi
{{
    /// <summary>
    /// {Project.Caption}API
    /// </summary>
    public partial class {Project.Name}ApiLogical : I{Project.Name}Api
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
        public ApiResult{res} {item.Name}({arg})
        {{
            var result = new ApiResult{res}();
            {item.Name}(");
                if (item.Argument != null)
                    code.Append("arg,");
                code.Append($@"result);
            return result;
        }}

        partial void {item.Name}(");
                if (item.Argument != null)
                    code.Append($"{arg},");
                code.Append($@"ApiResult{res} result);");
            }

            code.Append(@"
    }
}");
            var file = ConfigPath(Project, FileSaveConfigName, path, "Logical", $"{Project.Name}Api_Logical.Designer.cs");
            WriteFile(file, code.ToString());
        }


        private void ProjectApiExtend(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"using System;
using GoodLin.OAuth.Api;
using Yizuan.Service.Api;
using Yizuan.Service.Api.WebApi;

namespace {NameSpace}.WebApi
{{
    /// <summary>
    /// {Project.Caption}API
    /// </summary>
    partial class {Project.Name}ApiLogical
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
                code.Append(@"
        /// <param name=""result"">����ֵ</param>");
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
        partial void {item.Name}(");
                if (item.Argument != null)
                    code.Append($"{arg},");
                code.Append($@"ApiResult{res} result)
        {{
            //TO DO:ʵ��API
        }}");
            }

            code.Append(@"
    }
}");
            var file = ConfigPath(Project, FileSaveConfigName+"_2", path, "Logical", $"{Project.Name}Api_Logical.cs");
            WriteFile(file, code.ToString());
        }
    }

}

