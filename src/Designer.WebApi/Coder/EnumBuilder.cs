using System.IO;
using System.Text;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    public sealed class EnumBuilder : CoderWithProject
    {
        #region 主体代码
        /// <summary>
        /// 是否可写
        /// </summary>
        protected override bool CanWrite => true;

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_API_Enum_Base_cs";

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            string code = $@"
using System;
namespace {NameSpace}
{{
    {EnumMomentCoder.EnumFunc(Project)}
    /// <summary>
    /// 枚举辅助
    /// </summary>
    public static class EnumHelper
    {{
        {CaptionCode()}
    }}
}}";
            var file = Path.Combine(path, "enum.cs");
            WriteFile(file, code);
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            
        }

        private string CaptionCode()
        {
            var code = new StringBuilder();
            foreach (var en in Project.Enums)
            {
                code.AppendLine(EnumMomentCoder.ToCaption(en));
            }
            return code.ToString();
        }

        #endregion

    }
}