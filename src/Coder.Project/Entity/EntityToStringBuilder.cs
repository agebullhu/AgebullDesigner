using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityToStringBuilder<TModel> : ModelBuilderBase<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        #region ����

        public override string BaseCode => $@"
        #region �ı���ʽ��
        {ToStringCode()}
        #endregion";

        protected override string Folder => "ToString";

        #endregion

        #region �ı�

        private object ToStringCode()
        {
            var code = new StringBuilder();
            code.Append(@"

        /// <summary>
        /// ��ʾΪ�ı�
        /// </summary>
        /// <returns>�ı�</returns>
        public override string ToString()
        {
            return $@""");
            foreach (var field in Model.UserProperty)
                ToStringCode(code, field);
            code.Append(@""";
        }");
            return code.ToString();
        }

        private void ToStringCode(StringBuilder code, IFieldConfig field)
        {
            var caption = string.IsNullOrWhiteSpace(field.Caption) ? field.Name : field.Caption;
            if (string.IsNullOrWhiteSpace(field.ArrayLen))
            {
                if (field.EnumConfig != null && field.CsType != "string")
                    code.Append($@"
[{caption}]:{{{field.Name}_Content}}");
                else
                    code.Append($@"
[{caption}]:{{{field.Name}}}");
            }
            else
            {
                if (field.EnumConfig != null && field.CsType != "string")
                    code.Append($@"
[{caption}]:{{string.Join("","", {field.Name}_Content)}}");
                else
                    code.Append($@"
[{caption}]:{{string.Join("","", {field.Name})}}");
            }
        }


        #endregion
    }
}