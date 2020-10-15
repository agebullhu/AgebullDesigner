using System.IO;
using System.Text;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{
    public sealed class EnumBuilder : CoderWithProject
    {
        #region �������
        /// <summary>
        /// �Ƿ��д
        /// </summary>
        protected override bool CanWrite => true;

        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_API_Enum_Base_cs";

        /// <summary>
        ///     ����ʵ�����
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            string code = $@"
using System;
namespace {NameSpace}
{{
    {EnumMomentCoder.EnumFunc(Project)}
    /// <summary>
    /// ö�ٸ���
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
        ///     ������չ����
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