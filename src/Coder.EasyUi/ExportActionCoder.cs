using System.ComponentModel.Composition;

namespace Agebull.Common.SimpleDesign
{
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class ExportActionCoder : EasyUiCoderBase
    {
        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Aspnet_Export";

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
        /// ����������
        /// </summary>
        protected override string Name => ""{Entity.Caption}"";

        /// <summary>
        /// ��ǰ����ɸѡ��
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
                filter.AddAnd(p => {new ApiActionCoder { Entity = Entity }.QueryCode()});
            }}
            return filter;
        }}
    }}
}}";
        }
    }
}