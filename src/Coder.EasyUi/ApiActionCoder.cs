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
        protected override string FileSaveConfigName => "File_Aspnet_Action";


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
using Gboxt.Common.DataModel.BusinessLogic;
using Gboxt.Common.DataModel.MySql;
using Gboxt.Common.WebUI;

using {NameSpace};
using {NameSpace}.BusinessLogic;
using {NameSpace}.DataAccess;

namespace {NameSpace}.{Entity.Name}Page
{{
    partial class Action
    {{
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected void DefaultGetListData()
        {{
            var filter = new LambdaItem<{Entity.EntityName}>();
            SetKeywordFilter(filter);
            base.GetListData(filter);
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
        #region 设计器命令
{CommandCsCode()}
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
            var baseClass = "ApiPageBaseEx";
            if (Entity.Interfaces != null)
            {
                if (Entity.Interfaces.Contains("IStateData"))
                    baseClass = "ApiPageBaseForDataState";
                if (Entity.Interfaces.Contains("IAuditData"))
                    baseClass = "ApiPageBaseForAudit";
            }
            return
                $@"
using System;

using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.BusinessLogic;
using Gboxt.Common.DataModel.MySql;
using Gboxt.Common.WebUI;

using {NameSpace};
using {NameSpace}.BusinessLogic;
using {NameSpace}.DataAccess;

namespace {NameSpace}.{Entity.Name}Page
{{
    public partial class Action : {baseClass}<{Entity.EntityName}, {Entity.Name}DataAccess, {Entity.Name}BusinessLogic>
    {{
        /// <summary>
        /// 构造
        /// </summary>
        public Action()
        {{
            AllAction = true;
            IsPublicPage = true;
        }}
        /// <summary>
        ///     取得列表数据
        /// </summary>
        protected override void GetListData()
        {{
            DefaultGetListData();
        }}

        /// <summary>
        ///     执行操作
        /// </summary>
        /// <param name=""action"">传入的动作参数,已转为小写</param>
        protected override void DoActinEx(string action)
        {{
            DefaultActin(action);
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
    }}
}}";
        }


        private string CommandCsCode()
        {
            var code = new StringBuilder();
            code.Append(@"

        /// <summary>
        ///     执行操作
        /// </summary>
        /// <param name=""action"">传入的动作参数,已转为小写</param>
        void DefaultActin(string action)
        { 
            switch (action)
            {");
            if (Entity.TreeUi)
            {
                code.Append(@"
                case ""tree"":
                    OnLoadTree();
                    break;");
            }
            foreach (var cmd in Entity.Commands.Where(p => !p.IsLocalAction))
                code.Append($@"
                case ""{cmd.Name.ToLower()}"":
                    On{cmd.Name}();
                    break;");
            code.Append(@"
                default:
                    base.DoActinEx(action);
                    break;
            }
        }");
            if (Entity.TreeUi)
            {
                code.Append(@"

        /// <summary>
        ///     载入树节点
        /// </summary>
        private void OnLoadTree()
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
        private void On{cmd.Name}()
        {{");
                if (cmd.IsSingleObject)
                    code.Append($@"
            if (!this.Business.{cmd.Name}(this.GetIntArg(""id"")))");
                else
                    code.Append($@"
            if (!this.Business.Do{cmd.Name}(this.GetIntArrayArg(""selects"")))");
                code.Append(@"
            {
                 this.SetFailed(BusinessContext.Current.LastMessage);
            }
        }");
            }

            return code.ToString();
        }

    }
}