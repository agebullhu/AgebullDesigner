using Agebull.EntityModel.Config;
using System;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.VueComponents
{

    public static class VueComponentExtensions
    {
        #region 格式化器

        public static string Formater(this IPropertyConfig property)
        {
            var field = property.DataBaseField;
            if (property.DataFormater != null)
            {
                return $" | {property.DataFormater}";
            }
            if (property.EnumConfig != null)
            {
                return $" | {property.EnumConfig.Name.ToLWord()}Formater";
            }
            else if (field.IsLinkKey)
            {
                return $" | {field.LinkTable.ToLWord()}Formater(data_self)";
            }
            else if (property.CsType == nameof(DateTime))
            {
                return property.IsTime ? "| formatTime" : "| formatDate";
            }
            else if (property.CsType == "bool")
            {
                return " | boolFormater";
            }
            else if (property.IsMoney)
            {
                return " | formatMoney";
            }
            else if (property.CsType == "decimal")
            {
                return " | thousandsNumber";
            }
            return null;
        }
        #endregion

        #region Helper

        public static string HtmlAttribute(this string attr, string value)
        {
            return value.IsMissing() ? "" : $" {attr}= '{value}'";
        }
        public static void HtmlAttribute(this StringBuilder code, string attr, string value)
        {
            code.Append(value.IsMissing() ? "" : $" {attr}= '{value}'");
        }

        #endregion
        #region 表单

        public static void FormField(this StringBuilder code, string model, bool isEdit, IPropertyConfig property, bool isReadonly, int maxColumn)
        {
            var caption = property.Caption;
            var description = property.Description;

            code.Append($@"
    <el-form-item label='{caption}' prop='{property.JsonName}'");
            if (isEdit)
            {
                var sapn = property.FormCloumnSapn > maxColumn ? maxColumn : property.FormCloumnSapn;
                switch (sapn)
                {
                    case 2:
                        if (property.MulitLine)
                            code.HtmlAttribute("style", "width: 813px;display: block");
                        else
                            code.HtmlAttribute("style", "width: 813px;");
                        break;
                    case 3:
                        if (property.MulitLine)
                            code.HtmlAttribute("style", "width: 1228px;display: block");
                        else
                            code.HtmlAttribute("style", "width: 1228px;");
                        break;
                    case 4:
                        if (property.MulitLine)
                            code.HtmlAttribute("style", "width: 1640px;display: block");
                        else
                            code.HtmlAttribute("style", "width: 1640px;");
                        break;
                    default:
                        if (property.MulitLine)
                            code.HtmlAttribute("style", "display: block");
                        break;
                }
            }
            code.Append(">");

            if (isReadonly)
            {
                ReadonlyField(code, model, property);
            }
            else
                EditField(code, model, isEdit, property, description);
            code.Append(@"
    </el-form-item>");
        }
        static void ReadonlyField(StringBuilder code, string model, IPropertyConfig property)
        {
            code.Append($@"
        <span>{property.Prefix}{{{{{model}.{property.JsonName}{property.Formater()}}}}}{property.Suffix}</span>");
        }
        static void EditField(StringBuilder code, string model, bool isEdit, IPropertyConfig property, string description)
        {
            void SetDisabled(bool disabled)
            {
                code.Append("placeholder".HtmlAttribute(property.Description));
                code.Append(" clearable");
                if (!isEdit)
                {
                    return;
                }
                if (disabled)
                {
                    code.Append(property.IsUserReadOnly ? " disabled" : " :disabled='form.readonly'");
                }
                else
                {
                    code.Append(property.IsUserReadOnly ? " readonly" : " :readonly='form.readonly'");
                }
            }
            if (property.EnumConfig != null)
            {
                code.Append($@"
        <el-select v-model='{model}.{property.JsonName}'");
                SetDisabled(true);
                code.Append($@" style='width:100%'>
            <el-option :key='0' :value='0' label='-'></el-option>
            <el-option v-for='item in types.{property.EnumConfig.Name.ToLWord()}' :key='item.value' :label='item.label' :value='item.value'></el-option>
        </el-select>");
            }
            else if (!property.NoStorage && property.DataBaseField.IsLinkKey)
            {
                var name = GlobalConfig.GetEntity(property.DataBaseField.LinkTable)?.Name.ToLWord().ToPluralism();
                code.Append($@"
        <el-select v-model='{model}.{property.JsonName}'");
                SetDisabled(true);
                code.Append($@" style='width:100%'>
            <el-option :key='0' :value='0' label='-'></el-option>
            <template v-for='item in combos.{name}'>
                <el-option :key='item.id' :value='item.id' :label='item.text'></el-option>
            </template>
        </el-select>");
            }
            else if (property.CsType == "bool")
            {
                code.Append($@"
        <el-switch v-model='{model}.{property.JsonName}'");
                SetDisabled(true);
                code.Append(@"></el-switch>");
            }
            else if (property.CsType == nameof(DateTime))
            {
                code.Append($@"
        <el-date-picker v-model='{model}.{property.JsonName}'");
                code.Append(property.IsTime
                    ? "value-format='yyyy-MM-ddTHH:mm:ss' type='datetime'"
                    : "value-format='yyyy-MM-dd' type='date'");
                SetDisabled(false);
                code.Append(@" style='width:100%'></el-date-picker>");
            }
            else if (property.CsType == "string")
            {
                code.Append($@"
        <el-input v-model='{model}.{property.JsonName}'auto-complete='off'");

                if (isEdit && property.MulitLine)
                {
                    code.Append($" type='textarea' rows='{property.Rows}'");
                }
                SetDisabled(false);

                code.Append("></el-input>");
            }
            else
            {
                code.Append($@"
        <el-input v-model='{model}.{property.JsonName}' auto-complete='off'");
                SetDisabled(false);

                code.Append("></el-input>");
            }
        }
        #endregion
    }
}