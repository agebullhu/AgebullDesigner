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
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Aspnet_Api";


        public override string BaseCode()
        {
            var coder = new EasyUiHelperCoder
            {
                Entity = Entity,
                Project = Project
            };
            return
                $@"
using System;

using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.WebApi;
using Agebull.Common.WebApi.EasyUi;

using {NameSpace};
using {NameSpace}.BusinessLogic;
using {NameSpace}.DataAccess;

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
        {{
            var keyWord = GetArg(""keyWord"");
            if (!string.IsNullOrEmpty(keyWord))
            {{
                filter.AddAnd(p => {QueryCode()});
            }}
        }}

        /// <summary>
        /// 读取Form传过来的数据
        /// </summary>
        /// <param name=""data"">数据</param>
        /// <param name=""convert"">转化器</param>
        protected void DefaultReadFormData({Entity.EntityName} data, FormConvert convert)
        {{{coder.InputConvert()}
        }}

        #endregion
    }}
}}";
        }

        internal string QueryCode()
        {
            var code = new StringBuilder();
            bool first = true;
            foreach (var field in Entity.UserProperty.Where(p => p.CsType == "string" && !p.IsBlob))
            {
                if (first)
                    first = false;
                else
                    code.Append(" || ");
                code.Append($@"p.{field.Name}.Contains(keyWord)");
            }
            return code.ToString();
        }

        public override string Code()
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
                $@"
using System;
using System.Web.Http;

using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;
using Agebull.Common.DataModel;
using Agebull.Common.DataModel.BusinessLogic;
using Agebull.Common.WebApi;
using Agebull.Common.WebApi.EasyUi;

using {NameSpace};
using {NameSpace}.BusinessLogic;
using {NameSpace}.DataAccess;

namespace {NameSpace}.WebApi.Entity
{{
    [RoutePrefix(""{Entity.Parent.Abbreviation}/{Entity.Abbreviation}/v1"")]
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
        public ApiResponseMessage OnLoadTree()
        {
            var nodes = Business.LoadTree(this.GetIntArg(""id""));
            this.SetCustomJsonResult(nodes);
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
        [HttpPost,Route(""edit/{cmd.Name.ToLWord()}"")]
        public ApiResponseMessage On{cmd.Name}()
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
                ? Request.ToResponse(ApiResult.Error(State, Message))
                : Request.ToResponse(ApiResult.Succees());
        }");
            }

            return code.ToString();
        }

    }
}