using Agebull.EntityModel.Config;
using System;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.UniAppComponents
{

    public static class UniAppComponentExtensions
    {
        #region ±íµ¥

        public static void FormField(this StringBuilder code, string model, bool isEdit, IPropertyConfig property, bool isReadonly, int maxColumn)
        {
            var caption = property.Caption;
            var description = property.Description;

            code.Append($@"
                    <uni-forms-item required name='{property.JsonName}' label='{caption}'>
				        <view v-if='readonly' class='form-text'>
					        <text>{property.FieldText(model)}</text>
				        </view>
				        <view v-else>");

            if (property.EnumConfig != null)
            {
                code.Append($@"
                            <picker :value='{model}.{property.JsonName}' :range='types.{property.EnumConfig.Name.ToLWord()}' @change='binddata('{property.JsonName}', $event.detail.value)'>
							    <view>{property.FieldText(model)}</view>
						    </picker>");
            }
            else if (!property.NoStorage && property.DataBaseField.IsLinkKey)
            {
                var name = GlobalConfig.GetEntity(property.DataBaseField.LinkTable)?.Name.ToLWord().ToPluralism();

                code.Append($@"
                            <picker :value='{model}.{property.JsonName}' :range='types.{property.EnumConfig.Name.ToLWord()}' @change='binddata('{property.JsonName}', $event.detail.value)'>
							    <view>{property.FieldText(model)}</view>
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