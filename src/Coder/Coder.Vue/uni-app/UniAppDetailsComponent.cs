using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.UniAppComponents
{
    public class UniAppDetailsComponent
    {
        #region Html

        public static string FormCode(IEntityConfig model)
        {
            var code = new StringBuilder();
            code.Append($@"<template>
	<view class='form'>
		<uni-forms :value='details' ref='form' validate-trigger='bind' err-show-type='undertext'>");
            foreach (var property in model.ClientProperty.Where(p => !p.NoneDetails).ToArray())
            {
                code.FormField("details", true, property, model.IsUiReadOnly || property.IsUserReadOnly, model.FormCloumn);
            }
            code.Append(@"
        </uni-forms>
	</view>
</template>");
            return code.ToString();
        }

        #endregion


        #region 规则

        /// <summary>
        ///     生成Form校验规则
        /// </summary>
        /// <param name="model"></param>
        public static string RulesCode(IEntityConfig model)
        {
            var code = new StringBuilder();
            var first = true;
            code.Append(@"rules: {");
            var columns = model.ClientProperty.Where(p => !p.IsUserReadOnly);
            foreach (var property in columns)
            {
                var field = property;
                var sub = new StringBuilder();
                var dot = "";
                if (field.UiRequired)
                {
                    sub.Append($@"
            {{ required: true, message: '请填写{property.Caption}'}}");
                    dot = @",";
                }
                switch (field.CsType)
                {
                    case "string":
                        StringCheck(field, sub, ref dot);
                        break;
                    case "int":
                    case "long":
                    case "uint":
                    case "ulong":
                    case "short":
                    case "ushort":
                    case "decimal":
                    case "float":
                    case "double":
                        NumberCheck(field, sub, ref dot);
                        break;
                    /*case "DateTime":
                        DateTimeCheck(field, sub, ref dot);
                        break;*/
                }
                if (dot.IsMissing())
                    continue;
                if (first)
                    first = false;
                else code.Append(',');
                code.Append($@"
    {property.JsonName}: {{
        label: '{property.Caption}',
        rules:[{sub}]
    }}");
            }
            code.Append(@"
}");
            return code.ToString();
        }
        private static void DateTimeCheck(IPropertyConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max.IsPresent() && field.Min.IsPresent())
                code.Append($@"{dot}
            {{ format:'number', min: {field.Min}, max: {field.Max}, message: '时间从 {field.Min} 到 {field.Max} 之间'}}");
            else if (field.Max.IsPresent())
                code.Append($@"{dot}
            {{ format:'number',max: {field.Max}, message: '时间不大于 {field.Max}'}}");
            else if (field.Min.IsPresent())
                code.Append($@"{dot}
            {{ format:'number',min: {field.Min}, message: '时间不小于 {field.Min}'}}");
            else return;
            dot = @",";
        }

        private static void NumberCheck(IPropertyConfig field, StringBuilder code, ref string dot)
        {
            if (field.Max.IsPresent() && field.Min.IsPresent())
                code.Append($@"{dot}
            {{ minimum: {field.Min}, maximum: {field.Max}, message: '数值从 {field.Min} 到 {field.Max} 之间'}}");
            else if (field.Max.IsPresent())
                code.Append($@"{dot}
            {{ maximum: {field.Max}, message: '数值不大于 {field.Max}'}}");
            else if (field.Min.IsPresent())
                code.Append($@"{dot}
            {{ minimum: {field.Min}, message: '数值不小于 {field.Min}'}}");
            else return;
            dot = @",";
        }

        private static void StringCheck(IPropertyConfig property, StringBuilder code, ref string dot)
        {
            if (property.Max.IsPresent() && property.Min.IsPresent())
                code.Append($@"{dot}
            {{ minLength: {property.Min}, maxLength: {property.Max}, message: '长度在 {property.Min} 到 {property.Max} 个字符'}}");
            else if (property.Max.IsPresent())
                code.Append($@"{dot}
            {{ maxLength: {property.Max}, message: '长度不大于 {property.Max} 个字符'}}");
            else if (property.Min.IsPresent())
                code.Append($@"{dot}
            {{ minLength: {property.Min}, message: '长度不小于 {property.Min} 个字符'}}");
            else return;
            dot = @",";
        }

        #endregion
    }
}