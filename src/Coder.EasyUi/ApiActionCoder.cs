using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.EasyUi
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ApiActionCoder : EasyUiCoderBase
    {
        protected override string LangName => "cs";

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileName => "ApiController.Designer.cs";

        /// <summary>
        /// 名称
        /// </summary>
        protected override string ExFileName => "ApiController.cs";


        protected override string BaseCode()
        {
            var coder = new EasyUiHelperCoder();
            return
                $@"#region
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
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.Rpc;
using Agebull.Common.WebApi;
using Agebull.ZeroNet.ZeroApi;
using Agebull.Common.DataModel.WebUI;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;

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

        internal string QueryCode()
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

        protected override string ExtendCode()
        {
            var baseClass = "ApiController";
            if (Entity.Interfaces != null)
            {
                if (Entity.Interfaces.Contains("IStateData"))
                    baseClass = "ApiControllerForDataState";
                if (Entity.Interfaces.Contains("IAuditData"))
                    baseClass = "ApiControllerForAudit";
            }
            return
                $@"#region
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
using Agebull.Common.DataModel.WebUI;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.Rpc;
using Agebull.Common.WebApi;
using Agebull.ZeroNet.ZeroApi;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;

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
    [RoutePrefix(""{Project.Abbreviation?? Project.Name}/{Entity.Abbreviation ?? Entity.Name}/v1"")]
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
        [HttpPost,Route(""edit/tree"")]
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

            var caption = Entity.Properties.FirstOrDefault(p => p.IsCaption);
            if (caption!=null)
            {
        code.Append($@"
        /// <summary>下拉列表</summary>
        /// <returns></returns>
        [HttpPost]
        [Route(""edit/combo"")]
        public ApiArrayResult<EasyComboValues> ComboData()
        {{
            GlobalContext.Current.IsManageMode = false;
            var datas = Business.All();
            var combos = datas.Select(p => new EasyComboValues(p.{Entity.PrimaryColumn.Name}, p.{caption.Name})).ToList();
            combos.Insert(0,new EasyComboValues(0, ""-""));
            return new ApiArrayResult<EasyComboValues>
            {{
               Success = true,
               ResultData = combos
            }};
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
        [HttpPost,Route(""edit/{cmd.Name.ToLWord()}"")]
        public ApiResult On{cmd.Name}()
        {{
            InitForm();");
                if (cmd.IsSingleObject)
                    code.Append($@"
            return !this.Business.{cmd.Name}(this.GetIntArg(""id""))");
                else
                    code.Append($@"
            return !this.Business.Do{cmd.Name}(this.GetIntArrayArg(""selects""))");
                code.Append(@"
            return IsFailed
                ? ApiResult.Error(State, Message)
                : ApiResult.Succees();
        }");
            }

            return code.ToString();
        }

    }
}