using Agebull.EntityModel.Config;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    /// <summary>
    /// API代码生成
    /// </summary>
    public class ProjectApiActionCoder : CoderWithModel
    {

        #region 继承实现
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_API_CS";

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            if (Model.IsInterface || Model.DataTable.IsQuery)
                return;

            var fileName = "ApiController.Designer.cs";

            var file = Path.Combine(path, Model.Name + fileName);
            if (!string.IsNullOrWhiteSpace(Model.Alias))
            {
                var oldFile = Path.Combine(path, Project.Name, Model.Alias + fileName);
                if (File.Exists(oldFile))
                {
                    Directory.Move(oldFile, file);
                }
            }
            //var file = ConfigPath(Entity, "File_Edit_Api_d_cs", path, Entity.Classify, $"{Entity.Name}ApiController");
            WriteFile(file, BaseCode());
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            var fileName = "ApiController.cs";
            var file = Path.Combine(path, Model.Name + fileName);
            if (!string.IsNullOrWhiteSpace(Model.Alias))
            {
                var oldFile = Path.Combine(path, Project.Name, Model.Alias + fileName);
                if (File.Exists(oldFile))
                {
                    Directory.Move(oldFile, file);
                }
            }
            //var file = ConfigPath(Entity, "File_Edit_Api_d_cs", path, Entity.Classify, $"{Entity.Name}ApiController");
            WriteFile(file, ExtendCode());
            //ExportCsCode(path);
        }

        #endregion

        #region 代码片断

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string ExtendCode(IEntityConfig entity)
        {
            Model = entity;
            return ExtendCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string BaseCode(IEntityConfig entity)
        {
            Model = entity;
            return BaseCode();
        }

        #endregion

        #region 代码

        internal static string ReadFormValue(IEntityConfig entity)
        {
            var code = new StringBuilder();
            code.Append($@"
        static void ReadFormValue(I{entity.Name} entity, FormConvert convert)
        {{{InputConvert(entity)}
        }}");
            return code.ToString();
        }

        private string BaseCode()
        {
            return
                $@"#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Agebull.Common.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.Ioc;

using Agebull.EntityModel.Common;
using Agebull.EntityModel.Vue;
using ZeroTeam.MessageMVC.ModelApi;
using ZeroTeam.MessageMVC.ZeroApis;

{Project.UsingNameSpaces}

using {NameSpace};
#endregion

namespace {NameSpace}.WebApi
{{
    partial class {Model.Name}ApiController
    {{
        #region 基本扩展

        /// <summary>
        /// 转换方法
        /// </summary>
        /// <param name=""value"">文本</param>
        /// <returns></returns>
        protected override (bool, {Model.PrimaryColumn.CsType}) Convert(string value)
        {{
            return {ConvertCode()};
        }}

        /// <summary>
        ///     读取查询条件
        /// </summary>
        /// <param name=""filter"">筛选器</param>
        public override void GetQueryFilter(LambdaItem<{Model.EntityName}> filter)
        {{{QueryCode()}
        }}

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""convert"">转化器</param>
        private void DefaultReadFormData({Model.EntityName} data, FormConvert convert)
        {{{InputConvert(Model)}
        }}

        #endregion
{CommandCsCode()}
    }}
}}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ConvertCode()
        {
            return Model.PrimaryColumn.IsType("string")
                ? "(true , value)"
                : $@"{Model.PrimaryColumn.CsType}.TryParse(value,out var key) ? (true , key) : (false , key)";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string QueryCode()
        {
            var code = new StringBuilder();
            if (!Model.EnableDataBase)
                return "";
            var fields = Model.DataTable.Fields.Where(p => !p.NoStorage && p.Property.CanUserQuery);
            code.Append(@"
            if (RequestArgumentConvert.TryGet(""_value_"", out string _value_)  && !string.IsNullOrEmpty(_value_))
            {
                var field = RequestArgumentConvert.GetString(""_field_"");
                ");

            var querys = fields.Where(p => p.Property.CsType == "string" && !p.IsLinkKey && !p.IsBlob).ToArray();
            if (querys.Length > 0)
            {
                code.Append(@"if (string.IsNullOrWhiteSpace(field) || field == ""_any_"")
                    filter.AddAnd(p => ");
                bool first = true;
                foreach (var pro in querys)
                {
                    if (first)
                        first = false;
                    else
                        code.Append(@"
                                    || ");
                    code.Append($@"p.{pro.Name}.Like(_value_)");
                }
                code.Append(@");
                else ");
            }
            code.Append(@"RequestArgumentConvert.SetArgument(field,_value_);
            }");

            foreach (var field in fields)
            {
                var pro = field.Property;
                if (pro.Field.IsPrimaryKey || field.IsLinkKey)
                {
                    if (pro.CsType == "string")
                        code.Append($@"
            if(RequestArgumentConvert.TryGetIDs<string>(""{pro.JsonName}"" , p=>(true,p) , out var {pro.JsonName}))
            {{
                if ({pro.JsonName}.Count == 1)
                    filter.AddAnd(p => p.{pro.Name} == {pro.JsonName}[0]);
                else
                    filter.AddAnd(p => p.{pro.Name}.In({pro.JsonName}));
            }}");
                    else
                        code.Append($@"
            if(RequestArgumentConvert.TryGetIDs(""{pro.JsonName}"" , out var {pro.JsonName}))
            {{
                if ({pro.JsonName}.Count == 1)
                    filter.AddAnd(p => p.{pro.Name} == {pro.JsonName}[0]);
                else
                    filter.AddAnd(p => p.{pro.Name}.In({pro.JsonName}));
            }}");
                }
                else if (pro.CsType.ToLower() == "datetime")
                {
                    code.Append($@"
            if(RequestArgumentConvert.TryGet(""{pro.JsonName}"" , out DateTime {pro.JsonName}))
            {{
                var day = {pro.JsonName}.Date;
                var nextDay = day.AddDays(1);
                filter.AddAnd(p => (p.{pro.Name} >= day && p.{pro.Name} < nextDay));
            }}
            else 
            {{
                if(RequestArgumentConvert.TryGet(""{pro.JsonName}_begin"" , out DateTime {pro.JsonName}_begin))
                {{
                    var day = {pro.JsonName}_begin.Date;
                    filter.AddAnd(p => p.{pro.Name} >= day);
                }}
                if(RequestArgumentConvert.TryGet(""{pro.JsonName}_end"" , out DateTime {pro.JsonName}_end))
                {{
                    var day = {pro.JsonName}_end.Date.AddDays(1);
                    filter.AddAnd(p => p.{pro.Name} < day);
                }}
            }}");
                }
                else if (pro.IsEnum)
                {
                    code.Append($@"
            if(RequestArgumentConvert.TryGetEnum<{pro.CustomType}>(""{pro.JsonName}"" , out {pro.CustomType} {pro.JsonName}))
            {{
                filter.AddAnd(p => p.{pro.Name} == {pro.JsonName});
            }}");
                }
                else
                {
                    code.Append($@"
            if(RequestArgumentConvert.TryGet(""{pro.JsonName}"" , out {pro.CsType} {pro.JsonName}))
            {{");

                    switch (pro.CsType.ToLower())
                    {
                        case "string":
                            code.Append($@"
                filter.AddAnd(p => p.{pro.Name}.Like({pro.JsonName}));");
                            break;
                        default:
                            code.Append(!string.IsNullOrWhiteSpace(pro.CustomType)
                                ? $@"
                filter.AddAnd(p => p.{pro.Name} == ({pro.CustomType}){pro.JsonName});"
                                : $@"
                filter.AddAnd(p => p.{pro.Name} == {pro.JsonName});");
                            break;
                    }
                    code.Append(@"
            }");
                }
            }
            return code.ToString();
        }

        private string ExtendCode()
        {
            var page = $"/{Model.Project.PageRoot}/{Model.PagePath('/')}/index.htm".CheckUrlPath();

            var baseClass = "ApiController";
            if (Model.Interfaces != null)
            {
                if (Model.Interfaces.Contains("IStateData"))
                    baseClass = "ApiControllerForDataState";
                if (Model.Interfaces.Contains("IAuditData"))
                    baseClass = "ApiControllerForAudit";
            }
            return $@"#region
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
using Agebull.Common.Ioc;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Vue;
using ZeroTeam.MessageMVC.ModelApi;
using ZeroTeam.MessageMVC.ZeroApis;
using System.Threading.Tasks;

{Project.UsingNameSpaces}

using {NameSpace};

#endregion

namespace {NameSpace}.WebApi
{{
    /// <summary>
    ///  {Model.Caption.ToRemString()}
    /// </summary>
    [Service(""{Project.ServiceName}"")]
    [Route(""{Model.ApiName}/v1"")]
    [ApiPage(""{page}"")]
    public sealed partial class {Model.Name}ApiController 
         : {baseClass}<{Model.EntityName},{Model.PrimaryColumn.CsType},{Model.EntityName}BusinessLogic>
    {{
        #region 基本扩展

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""convert"">转化器</param>
        protected override Task ReadFormData({Model.EntityName} data, FormConvert convert)
        {{
            DefaultReadFormData(data,convert);
            return Task.CompletedTask;
        }}

        #endregion
    }}
}}";
        }


        private string CommandCsCode()
        {
            var code = new StringBuilder();
            if (Model.TreeUi)
            {
                code.Append($@"
        /// <summary>
        ///     载入树节点
        /// </summary>
        [Route(""edit/tree"")]
        public async Task<IApiResult<List<TreeNode>>> OnLoadTree()
        {{
            var nodes = await Business.LoadTree();
            return ApiResultHelper.Succees(nodes);
        }}");
            }
            if (Model.CaptionColumn != null)
            {
                code.Append($@"

        /// <summary>
        /// 载入下拉列表数据
        /// </summary>
        [Route(""edit/combo"")]
        public async Task<IApiResult<List<DataItem<{Model.PrimaryColumn.LastCsType}>>>> ComboValues()
        {{
            var nodes = await Business.ComboValues();
            return ApiResultHelper.Succees(nodes);
        }}");
            }
            foreach (var cmd in Model.Commands.Where(p => !p.IsLocalAction))
            {
                if (cmd.IsSingleObject)
                {
                    code.Append($@"
        /// <summary>
        ///     {cmd.Caption.ToRemString()}
        /// </summary>
        /// <remark>
        ///     {cmd.Description.ToRemString()}
        /// </remark>
        /// <param name=""id"">选择的ID</param>
        [Route(""{cmd.Api}"")]
        public async Task<IApiResult> On{cmd.Name}(long id) => await");
                    if (cmd.ServiceCommand.IsMissing())
                        code.Append($@" this.Business.{cmd.Name}(id)  ? ApiResultHelper.Succees() : ApiResultHelper.Helper.ArgumentError;");
                    else
                        code.Append($@" {cmd.ServiceCommand}(id);");
                }
                else if (cmd.IsMulitOperator)
                {
                    code.Append($@"
        /// <summary>
        ///     {cmd.Caption.ToRemString()}
        /// </summary>
        /// <remark>
        ///     {cmd.Description.ToRemString()}
        /// </remark>
        /// <param name=""selects"">选择的ID列表</param>
        [Route(""{cmd.Api}""),ApiOption(ApiOption.DictionaryArgument)]
        public async Task<IApiResult> On{cmd.Name}(string selects)
        {{
            if (!RequestArgumentConvert.TryGetIDs(""selects"",out var ids))
                return ApiResultHelper.Helper.ArgumentError;");

                    if (cmd.ServiceCommand.IsMissing())
                        code.Append($@"
            return await this.Business.{cmd.Name}(ids) 
                   ? ApiResultHelper.Succees()
                   : ApiResultHelper.Helper.ArgumentError;
        }}");
                    else
                        code.Append($@"
            return await {cmd.ServiceCommand}(ids);
            }}");
                }
                else
                {
                    code.Append($@"
        /// <summary>
        ///     {cmd.Caption.ToRemString()}
        /// </summary>
        /// <remark>
        ///     {cmd.Description.ToRemString()}
        /// </remark>
        [Route(""{cmd.Api}"")]
        public async Task<IApiResult> On{cmd.Name}() => await");
                    if (cmd.ServiceCommand.IsMissing())
                        code.Append($@" this.Business.{cmd.Name}()  ? ApiResultHelper.Succees() : ApiResultHelper.Helper.ArgumentError;");
                    else
                        code.Append($@" {cmd.ServiceCommand}();");
                }
            }
            if (code.Length == 0)
                return null;
            return $@"
        #region 设计器命令
        {code}
        #endregion";
        }

        #endregion

        /// <summary>
        /// 输入值转换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string InputConvert(IEntityConfig model)
        {
            if (model.IsUiReadOnly)
                return null;
            var fields = model.ClientProperty.Where(p => !p.IsUserReadOnly).ToArray();
            var code = new StringBuilder();
            foreach (var group in fields.GroupBy(p => p.Group))
            {
                code.Append($@"
            //{group.Key ?? "普通字段"}");
                foreach (var pro in group.OrderBy(p => p.Index))
                {
                    var field = model.DataTable.Fields.FirstOrDefault(p => p.Property == pro);
                    if (pro == model.PrimaryColumn)
                    {
                        continue;
                    }
                    code.Append(@"
            if(");
                    if (pro.IsPrimaryKey || (field != null && field.KeepUpdate))
                    {
                        code.Append(@"!convert.IsUpdata && ");

                    }
                    if (field != null && field.KeepStorageScreen == StorageScreenType.Insert)
                    {
                        code.Append(@"convert.IsUpdata && ");
                    }
                    switch (pro.DataType)
                    {
                        case "ByteArray" when pro.IsImage:
                            code.Append($@"convert.TryGetValue(""{pro.JsonName}"" , out string file))
            {{
                if (string.IsNullOrWhiteSpace(file))
                    data.{pro.Name}_Base64 = null;
                else if(file != ""*"" && file.Length< 100 && file[0] == '/')
                {{
                    using(var call = new WebApiCaller(ConfigurationManager.AppSettings[""ManageAddress""]))
                    {{
                        var result = call.Get<string>(""api/v1/ueditor/action"", $""action=base64&url={{file}}"");
                        data.{pro.Name}_Base64 = result.Success ? result.ResultData : null;
                    }}
                }}
            }}");
                            continue;
                        case "ByteArray":
                            code.Append($@"convert.TryGetValue(""{pro.JsonName}"" , out string {pro.Name}))
                data.{pro.Name}_Base64 = {pro.Name};");
                            continue;
                    }
                    if (pro.IsEnum && !string.IsNullOrWhiteSpace(pro.CustomType))
                    {
                        code.Append($@"convert.TryGetEnum(""{pro.JsonName}"" , out {pro.CustomType} {pro.Name}))
                data.{pro.Name} = {pro.Name};");
                    }
                    else if (!string.IsNullOrWhiteSpace(pro.CustomType))
                    {
                        code.Append($@"convert.TryGetValue(""{pro.JsonName}"" , out {pro.CsType} {pro.Name}))
                data.{pro.Name} = ({pro.CustomType}){pro.Name};");
                    }
                    else
                    {
                        code.Append($@"convert.TryGetValue(""{pro.JsonName}"" , out {pro.CsType} {pro.Name}))
                data.{pro.Name} = {pro.Name};");
                    }
                }
            }
            return code.ToString();
        }
    }
}