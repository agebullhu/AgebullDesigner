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
    public class ProjectApiActionCoderRegister : IAutoRegister
    {
        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Web-Api", "表单读取", "cs", ProjectApiActionCoder<EntityConfig>.ReadFormValue);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.cs", "cs", new ProjectApiActionCoder<EntityConfig>().BaseCode);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.Designer.cs", "cs", new ProjectApiActionCoder<EntityConfig>().ExtendCode);
            
            MomentCoder.RegisteCoder("Web-Api", "表单读取", "cs", ProjectApiActionCoder<ModelConfig>.ReadFormValue);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.cs", "cs", new ProjectApiActionCoder<ModelConfig>().BaseCode);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.Designer.cs", "cs",new ProjectApiActionCoder<ModelConfig>().ExtendCode);
        }
    }
    /// <summary>
    /// API代码生成
    /// </summary>
    public class ProjectApiActionCoder<TModel> : CoderWithModel<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
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
            if (Model.IsInterface || Model.IsQuery)
                return;
            if (Model.IsInternal || Model.NoDataBase || Model.DenyScope.HasFlag(AccessScopeType.Client))
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
            if (Model.IsInternal || Model.NoDataBase || Model.DenyScope.HasFlag(AccessScopeType.Client))
                return;
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
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string ExtendCode(TModel entity)
        {
            Model = entity;
            return ExtendCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string BaseCode(TModel entity)
        {
            Model = entity;
            return BaseCode();
        }

        #endregion

        #region 代码

        internal static string ReadFormValue(TModel entity)
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
using Agebull.Common.Ioc;

using Agebull.EntityModel.Common;
using Agebull.EntityModel.EasyUI;
using ZeroTeam.MessageMVC.ZeroApis;
using Agebull.MicroZero.ZeroApis;

{Project.UsingNameSpaces}

using {NameSpace};
using {NameSpace}.BusinessLogic;
#endregion

namespace {NameSpace}.WebApi.Entity
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
        protected void DefaultReadFormData({Model.EntityName} data, FormConvert convert)
        {{{ApiHelperCoder.InputConvert(Model)}
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

            code.Append(@"
            if (RequestArgumentConvert.TryGet(""_value_"", out string value) && value != null)
            {
                var pro. = RequestArgumentConvert.GetString(""_field_"");
                ");

            var properties = Model.ClientProperty.Where(p => !p.NoStorage && /*p.CanUserInput && */p.CsType == "string" && !p.IsLinkKey && !p.IsBlob).ToArray();
            if (properties.Length > 0)
            {
                code.Append(@"if (string.IsNullOrWhiteSpace(pro.) || pro. == ""_any_"")
                    filter.AddAnd(p => ");
                bool first = true;
                foreach (var pro in properties)
                {
                    if (first)
                        first = false;
                    else
                        code.Append(@"
                                    || ");
                    code.Append($@"p.{pro.Name}.Contains(value)");
                }
                code.Append(@");
                else ");
            }
            code.Append(@"RequestArgumentConvert.SetArgument(pro.,value);
            }");
            properties = Model.ClientProperty.Where(p => !p.NoStorage).ToArray();
            foreach (var pro in properties)
            {
                if (pro.IsPrimaryKey || pro.IsLinkKey)
                {
                    if (pro.CsType == "string")
                        code.Append($@"
            if(RequestArgumentConvert.TryGetIDs<string>(""{pro.JsonName}"" , p=>(true,p) , out var {pro.JsonName}))
            {{
                if ({pro.JsonName}.Count == 1)
                    filter.AddAnd(p => p.{pro.Name} == {pro.JsonName}[0]);
                else
                    filter.AddAnd(p => {pro.JsonName}.Contains(p.{pro.Name}));
            }}");
                    else
                        code.Append($@"
            if(RequestArgumentConvert.TryGetIDs(""{pro.JsonName}"" , out var {pro.JsonName}))
            {{
                if ({pro.JsonName}.Count == 1)
                    filter.AddAnd(p => p.{pro.Name} == {pro.JsonName}[0]);
                else
                    filter.AddAnd(p => {pro.JsonName}.Contains(p.{pro.Name}));
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
                filter.AddAnd(p => p.{pro.Name}.Contains({pro.JsonName}));");
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
            var page = $"/{Model.Parent.PageRoot}/{Model.PagePath('/')}/index.htm";

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
using Agebull.EntityModel.EasyUI;
using ZeroTeam.MessageMVC.ZeroApis;
using Agebull.MicroZero.ZeroApis;

{Project.UsingNameSpaces}

using {NameSpace};
using {NameSpace}.BusinessLogic;

#endregion

namespace {NameSpace}.WebApi.Entity
{{
    /// <summary>
    ///  {ToRemString(Model.Caption)}
    /// </summary>
    [Route(""{Model.ApiName}/v1"")]
    [ApiPage(""{page}"")]
    public partial class {Model.Name}ApiController 
         : {baseClass}<{Model.EntityName},{Model.PrimaryColumn.CsType},{Model.Name}BusinessLogic>
    {{
        #region 基本扩展

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""convert"">转化器</param>
        protected override void ReadFormData({Model.EntityName} data, FormConvert convert)
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
            if (Model.TreeUi)
            {
                code.Append(@"

        /// <summary>
        ///     载入树节点
        /// </summary>
        [Route(""edit/tree"")]
        public IApiResult<List<EasyUiTreeNode>> OnLoadTree()
        {
            var nodes = Business.LoadTree(this.GetLongArg(""id""));
            return ApiResultHelper.Succees(nodes);
        }");
            }
            var cap = Model.LastProperties.FirstOrDefault(p => p.IsCaption);
            if (cap != null)
            {
                code.Append(@"

        /// <summary>
        /// 载入下拉列表数据
        /// </summary>
        [Route(""edit/combo"")]
        public IApiResult<List<EasyComboValues>> ComboValues()
        {
            return ApiResultHelper.Succees(Business.ComboValues());
        }");
            }
            if (Model is ModelConfig model)
                foreach (var cmd in model.Commands.Where(p => !p.IsLocalAction))
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
        #region 设计器命令
        /*
        {code}
        */
        #endregion";
        }

        #endregion
    }
}