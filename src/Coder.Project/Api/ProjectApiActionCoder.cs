using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    /// <summary>
    /// API代码生成
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ProjectApiActionCoder : CoderWithEntity, IAutoRegister
    {

        #region 继承实现
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_API_CS";

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            if (Entity.IsInternal || Entity.NoDataBase || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;
            var fileName = "ApiController.Designer.cs";
            var file = Path.Combine(path, Project.Name, Entity.Name + fileName);
            if (!string.IsNullOrWhiteSpace(Entity.Alias))
            {
                var oldFile = Path.Combine(path, Project.Name, Entity.Alias + fileName);
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
        protected override void CreateExCode(string path)
        {
            if (Entity.IsInternal || Entity.NoDataBase || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;
            var fileName = "ApiController.cs";
            var file = Path.Combine(path,Project.Name, Entity.Name + fileName);
            if (!string.IsNullOrWhiteSpace(Entity.Alias))
            {
                var oldFile = Path.Combine(path, Project.Name, Entity.Alias + fileName);
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

        #region Export
        /*
        private void ExportCsCode(string path)
        {
            var file = ConfigPath(Entity, "File_Web_Export_cs", path, $"Page\\{Entity.Parent.Name}\\{Entity.Name}", "Export.cs");
            var coder = new ExportActionCoder
            {
                Entity = Entity,
                Project = Project
            };
            WriteFile(file, coder.Code());
        }

        private string ExportAspxCode()
        {
            return $@"<%@ Page Language='C#' AutoEventWireup='true'  Inherits='{NameSpace}.{Entity.Name}Page.ExportAction' %>";
        }
        */
        #endregion

        #region 代码片断

        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Web-Api", "表单保存", "cs", ReadFormValue);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.cs", "cs", BaseCode);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.Designer.cs", "cs", ExtendCode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string ExtendCode(EntityConfig entity)
        {
            Entity = entity;
            return ExtendCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string BaseCode(EntityConfig entity)
        {
            Entity = entity;
            return BaseCode();
        }

        #endregion

        #region 代码

        static string ReadFormValue(EntityConfig entity)
        {
            var code = new StringBuilder();
            code.Append($@"
        static void ReadFormValue(I{entity.Name} entity, FormConvert convert)
        {{{ApiHelperCoder.InputConvert(entity)}
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
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.Context;
using Agebull.Common.Ioc;
using Agebull.Common.OAuth;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.EasyUI;
using Agebull.MicroZero;
using Agebull.MicroZero.ZeroApis;

{Project.UsingNameSpaces}

using {NameSpace};
using {NameSpace}.BusinessLogic;
using {NameSpace}.DataAccess;
#endregion

namespace {NameSpace}.WebApi.Entity
{{
    partial class {Entity.Name}ApiController
    {{{CommandCsCode()}

        #region 基本扩展

        /// <summary>
        ///     读取查询条件
        /// </summary>
        /// <param name=""filter"">筛选器</param>
        public override void GetQueryFilter(LambdaItem<{Entity.EntityName}> filter)
        {{{QueryCode()}
        }}

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""convert"">转化器</param>
        protected void DefaultReadFormData({Entity.EntityName} data, FormConvert convert)
        {{{ApiHelperCoder.InputConvert(Entity)}
        }}

        #endregion
    }}
}}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string QueryCode()
        {
            var code = new StringBuilder();

            code.Append(@"
            if (TryGet(""_value_"", out string value) && value != null)
            {
                var field = GetArg(""_field_"");
                ");

            var fields = Entity.ClientProperty.Where(p => !p.NoStorage && /*p.CanUserInput && */p.CsType == "string" && !p.IsBlob).ToArray();
            if (fields.Length > 0)
            {
                code.Append(@"if (string.IsNullOrWhiteSpace(field) || field == ""_any_"")
                    filter.AddAnd(p => ");
                bool first = true;
                foreach (var field in fields)
                {
                    if (first)
                        first = false;
                    else
                        code.Append(@"
                                    || ");
                    code.Append($@"p.{field.Name}.Contains(value)");
                }
                code.Append(@");
                else ");
            }
            code.Append(@"this[field] = value;
            }");
            fields = Entity.ClientProperty.Where(p => !p.NoStorage).ToArray();
            foreach (var field in fields)
            {
                if (field.IsPrimaryKey || field.IsLinkKey)
                {
                    code.Append($@"
            if(TryGetIDs(""{field.JsonName}"" , out var {field.JsonName}))
            {{
                if ({field.JsonName}.Count == 1)
                    filter.AddAnd(p => p.{field.Name} == {field.JsonName}[0]);
                else
                    filter.AddAnd(p => {field.JsonName}.Contains(p.{field.Name}));
            }}");
                }
                else if(field.CsType.ToLower() == "datetime")
                {
                    code.Append($@"
            if(TryGet(""{field.JsonName}"" , out DateTime {field.JsonName}))
            {{
                var day = {field.JsonName}.Date;
                var nextDay = day.AddDays(1);
                filter.AddAnd(p => (p.{field.Name} >= day && p.{field.Name} < nextDay));
            }}
            else 
            {{
                if(TryGet(""{field.JsonName}_begin"" , out DateTime {field.JsonName}_begin))
                {{
                    var day = {field.JsonName}_begin.Date;
                    filter.AddAnd(p => p.{field.Name} >= day);
                }}
                if(TryGet(""{field.JsonName}_end"" , out DateTime {field.JsonName}_end))
                {{
                    var day = {field.JsonName}_end.Date.AddDays(1);
                    filter.AddAnd(p => p.{field.Name} < day);
                }}
            }}");
                }
                else
                {
                    code.Append($@"
            if(TryGet(""{field.JsonName}"" , out {field.CsType} {field.JsonName}))
            {{");

                    switch (field.CsType.ToLower())
                    {
                        case "string":
                            code.Append($@"
                filter.AddAnd(p => p.{field.Name}.Contains({field.JsonName}));");
                            break;
                        default:
                            code.Append(!string.IsNullOrWhiteSpace(field.CustomType)
                                ? $@"
                filter.AddAnd(p => p.{field.Name} == ({field.CustomType}){field.JsonName});"
                                : $@"
                filter.AddAnd(p => p.{field.Name} == {field.JsonName});");
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
            var folder = !string.IsNullOrWhiteSpace(Entity.PageFolder)
                ? Entity.PageFolder.Replace('\\', '/')
                    : string.IsNullOrWhiteSpace(Entity.Classify)
                        ? Entity.Name
                        : $"{Entity.Classify}/{Entity.Name}";

            var page = $"/{Project.PageRoot}/{folder}/index.htm";

            var baseClass = "ApiController";
            if (Entity.Interfaces != null)
            {
                if (Entity.Interfaces.Contains("IStateData"))
                    baseClass = "ApiControllerForDataState";
                if (Entity.Interfaces.Contains("IAuditData"))
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
using Agebull.Common.Context;
using Agebull.Common.Ioc;
using Agebull.Common.OAuth;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.EasyUI;
using Agebull.MicroZero;
using Agebull.MicroZero.ZeroApis;

{Project.UsingNameSpaces}

using {NameSpace};
using {NameSpace}.BusinessLogic;

#endregion

namespace {NameSpace}.WebApi.Entity
{{
    /// <summary>
    ///  {ToRemString(Entity.Caption)}
    /// </summary>
    [RoutePrefix(""{Project.Abbreviation}/{Entity.Abbreviation}/v1"")]
    [ApiPage(""{page}"")]
    public partial class {Entity.Name}ApiController 
         : {baseClass}<{Entity.EntityName},{Entity.Name}BusinessLogic>
    {{
        #region 基本扩展

        /*// <summary>
        ///     取得列表数据
        /// </summary>
        protected override ApiPageData<{Entity.EntityName}> GetListData()
        {{
            var filter = new LambdaItem<{Entity.EntityName}>();
            ReadQueryFilter(filter);
            return base.GetListData(filter);
        }}*/

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""convert"">转化器</param>
        protected override void ReadFormData({Entity.EntityName} data, FormConvert convert)
        {{
            DefaultReadFormData(data,convert);
        }}

        #endregion
    }}
}}";
        }


        private string CommandCsCode()
        {
            var code = new StringBuilder();
            if (Entity.TreeUi)
            {
                code.Append(@"

        /// <summary>
        ///     载入树节点
        /// </summary>
        [Route(""edit/tree"")]
        public ApiArrayResult<EasyUiTreeNode> OnLoadTree()
        {
            var nodes = Business.LoadTree(this.GetLongArg(""id""));
            return new ApiArrayResult<EasyUiTreeNode>
            {{
               Success = true,
               ResultData = nodes
            }};
        }");
            }
            var cap = Entity.Properties.FirstOrDefault(p => p.IsCaption);
            if (cap != null)
            {
                code.Append(@"

        /// <summary>
        /// 载入下拉列表数据
        /// </summary>
        [Route(""edit/combo"")]
        public ApiArrayResult<EasyComboValues> ComboValues()
        {
            return ApiArrayResult<EasyComboValues>.Succees(Business.ComboValues());
        }");
            }
            foreach (var cmd in Entity.Commands.Where(p => !p.IsLocalAction))
            {
                code.Append($@"
        /// <summary>
        ///     {ToRemString(cmd.Caption)}
        /// </summary>
        /// <remark>
        ///     {ToRemString(cmd.Description)}
        /// </remark>
        [Route(""edit/{cmd.Name.ToLWord()}"")]
        public ApiResult On{cmd.Name}()
        {{
            InitForm();");
                code.Append(cmd.IsSingleObject
                    ? $@"
            return !this.Business.{cmd.Name}(this.GetIntArg(""id""))"
                    : $@"
            return !this.Business.Do{cmd.Name}(this.GetIntArrayArg(""selects""))");
                code.Append(@"
            return IsFailed
                ? ApiResult.Error(State, Message)
                : ApiResult.Succees();
        }");
            }
            if (code.Length == 0)
                return null;
            return $@"
        #region 设计器命令{code}

        #endregion";
        }

        #endregion
    }
}