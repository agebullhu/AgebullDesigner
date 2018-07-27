using System.ComponentModel.Composition;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder.VUE
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ExportActionCoder : VueCoderBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Vue_Export";

        public override string Code()
        {
            return $@"
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.MySql;
using Gboxt.Common.WebUI;

using {NameSpace}.DataAccess;

namespace {NameSpace}.{Entity.Name}Page
{{
    /// <summary>
    /// {Entity.Caption}
    /// </summary>
    public class ExportAction : ExportPageBase<{Entity.EntityName}, {Entity.Name}DataAccess>
    {{
        /// <summary>
        /// 导出表名称
        /// </summary>
        protected override string Name => ""{Entity.Caption}"";

        /// <summary>
        /// 当前数据筛选器
        /// </summary>
        /// <returns></returns>
        protected override LambdaItem<{Entity.EntityName}> GetFilter()
        {{
            var filter = new LambdaItem<{Entity.EntityName}>
            {{
                {(Entity.Interfaces?.Contains("IStateData") ?? false ? "Root = p => p.DataState <= DataStateType.Discard" : null)}
            }};
            var keyWord = GetArg(""keyWord"");
            if (!string.IsNullOrEmpty(keyWord))
            {{
                filter.AddAnd(p => {new VueCoder { Entity = Entity }.QueryCode()});
            }}
            return filter;
        }}
    }}
}}";
        }
    }
}