using Agebull.EntityModel.Config;
using System;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.UniAppComponents
{

    public static class UniAppComponentExtensions
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
            <uni-forms-item required name='{property.JsonName}' label='{caption}'>
				<view v-if='readonly' class='form-text'>
					<text>{{{model}.{property.JsonName}}}</text>
				</view>
				<view v-else>");

            if (property.EnumConfig != null)
            {
                code.Append($@"
                        <picker :value='{model}.{property.JsonName}' :range='types.{property.EnumConfig.Name.ToLWord()}' @change='binddata('{property.JsonName}', $event.detail.value)'>
							<view>{{ {model}.{property.JsonName} ? types.{property.EnumConfig.Name.ToLWord()}[{model}.{property.JsonName}] : '请选择{caption}' }}</view>
						</picker>");
            }
            else if (!property.NoStorage && property.DataBaseField.IsLinkKey)
            {
                var name = GlobalConfig.GetEntity(property.DataBaseField.LinkTable)?.Name.ToLWord().ToPluralism();

                code.Append($@"
                        <picker :value='{model}.{property.JsonName}' :range='types.{property.EnumConfig.Name.ToLWord()}' @change='binddata('{property.JsonName}', $event.detail.value)'>
							<view>{{ {model}.{property.JsonName} ? types.{property.EnumConfig.Name.ToLWord()}[{model}.{property.JsonName}] : '请选择{caption}' }}</view>
						</picker>");
            }
            else if (property.CsType == "bool")
            {
                code.Append($@"
                    <switch :checked='{model}.{property.JsonName}' @change='this.{model}.{property.JsonName}=$event.detail.value'></switch>");
            }
            else if (property.CsType == nameof(DateTime))
            {
                var type = property.IsTime ? "datetime" : "date";
                code.Append($@"
					<uni-datetime-picker type='{type}' v-model='{model}.{property.JsonName}'></uni-datetime-picker>");
            }
            else
            {
                code.Append($@"
                    <uni-easyinput v-model='{model}.{property.JsonName}' placeholder='{description}'></uni-easyinput>");
            }
            code.Append($@"
				</view>
			</uni-forms-item>");
        }

        #endregion
    }
}