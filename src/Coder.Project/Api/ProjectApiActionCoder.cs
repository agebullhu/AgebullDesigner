using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
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
            var file = ConfigPath(Entity, "File_Web_Api_cs", path, Entity.Name, $"{Entity.Name}ApiController");
            WriteFile(file + ".Designer.cs", BaseCode());
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            if (Entity.IsInternal || Entity.NoDataBase || Entity.DenyScope.HasFlag(AccessScopeType.Client))
                return;
            var file = ConfigPath(Entity, "File_Web_Api_cs", path, Entity.Name, $"{Entity.Name}ApiController");
            WriteFile(file + ".cs", ExtendCode());
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
            MomentCoder.RegisteCoder("Web-Api", "表单保存", "cs", ApiHelperCoder.InputConvert4);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.cs", "cs", BaseCode);
            MomentCoder.RegisteCoder("Web-Api", "ApiController.Designer.cs", "cs", ExtendCode);
        }
        public string ExtendCode(EntityConfig entity)
        {
            Entity = entity;
            return ExtendCode();
        }

        public string BaseCode(EntityConfig entity)
        {
            Entity = entity;
            return BaseCode();
        }

        #endregion

        #region 代码

        private string BaseCode()
        {
            var coder = new ApiHelperCoder();
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
    {{
        #region 设计器命令
{CommandCsCode()}

        #endregion

        #region 基本扩展
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected ApiPageData<{Entity.EntityName}> DefaultGetListData()
        {{
            var filter = new LambdaItem<{Entity.EntityName}>();
            SetKeywordFilter(filter);
            return base.GetListData(filter);
        }}

        /// <summary>
        ///     关键字查询缺省实现
        /// </summary>
        /// <param name=""filter"">筛选器</param>
        public void SetKeywordFilter(LambdaItem<{Entity.EntityName}> filter)
        {{{QueryCode()}
        }}

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""convert"">转化器</param>
        protected void DefaultReadFormData({Entity.EntityName} data, FormConvert convert)
        {{{coder.InputConvert(Entity)}
        }}

        #endregion
    }}
}}";
        }

        public string QueryCode()
        {
            var fields = Entity.UserProperty.Where(p => !p.DbInnerField && !p.IsSystemField && p.CsType == "string" && !p.IsBlob).ToArray();
            if (fields.Length == 0)
                return "";
            var code = new StringBuilder(@"
            var keyWord = GetArg(""keyWord"");
            if (!string.IsNullOrEmpty(keyWord))
            {
                filter.AddAnd(p =>");
            bool first = true;
            foreach (var field in fields)
            {
                if (first)
                    first = false;
                else
                    code.Append(@" || 
                                   ");
                code.Append($@"p.{field.Name}.Contains(keyWord)");
            }
            code.Append(@");
            }");
            return code.ToString();
        }

        private string ExtendCode()
        {
            var folder = !string.IsNullOrWhiteSpace(Entity.PageFolder)
                ? Entity.PageFolder.Replace('\\', '/')
                    : string.IsNullOrEmpty(Entity.Classify)
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
using {NameSpace}.DataAccess;
#endregion

namespace {NameSpace}.WebApi.Entity
{{
    /// <summary>
    ///  {ToRemString(Entity.Caption)}
    /// </summary>
    [RoutePrefix(""{Project.Abbreviation}/{Entity.Abbreviation}/v1"")]
    [ApiPage(""{page}"")]
    public partial class {Entity.Name}ApiController : {baseClass}<{Entity.EntityName}, {Entity.Name}DataAccess, {Entity.Parent.DataBaseObjectName}, {Entity.Name}BusinessLogic>
    {{
        #region 基本扩展

        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected override ApiPageData<{Entity.EntityName}> GetListData()
        {{
            return DefaultGetListData();
        }}

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
            var nodes = Business.LoadTree(this.GetIntArg(""id""));
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
        {");
                code.Append(Entity.Interfaces.Contains("IStateData")
                    ? @"
            var datas = Business.All(p=>p.DataState <= EntityModel.Common.DataStateType.Enable);"
                    : @"
            var datas = Business.All();");
                code.Append($@"
            return ApiArrayResult<EasyComboValues>.Succees(datas.Count == 0
                ? new System.Collections.Generic.List<EasyComboValues>()
                : datas.OrderBy(p => p.{cap.Name}).Select(p => new EasyComboValues
                {{
                    Key = p.{Entity.PrimaryField},
                    Value = p.{cap.Name}
                }}).ToList());
        }}");
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

            return code.ToString();
        }

        #endregion

    }
}