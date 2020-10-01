using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public class EntityValidateCoder
    {

        /// <summary>
        /// 当前表对象
        /// </summary>
        public ModelConfig Model { get; set; }

        public string Code(IEnumerable<PropertyConfig> fields)
        {
            var code = new StringBuilder();
            var configs = fields as PropertyConfig[] ?? fields.ToArray();
            foreach (var field in configs.Where(p => !string.IsNullOrWhiteSpace(p.EmptyValue)))
            {
                ConvertEmptyValue(code, field);
            }

            foreach (var field in configs)
            {
                switch (field.CsType)
                {
                    case "string":
                        StringCheck(code, field);
                        continue;
                    case "int":
                    case "uint":
                    case "short":
                    case "double":
                    case "float":
                    case "long":
                    case "ulong":
                    case "decimal":
                        NumberCheck(code, field);
                        break;
                    case "DateTime":
                        DateTimeCheck(code, field);
                        break;
                }
            }
            return code.ToString();
        }

        static string EmptyCode(PropertyConfig field)
        {
            var msg = field["EmptyMessage"];
            return string.IsNullOrWhiteSpace(msg)
                ? $"result.AddNoEmpty(\"{field.Caption}\",nameof({field.Name}));"
                : $"result.Add(\"{field.Caption}\",nameof({field.Name}),\"{msg}\");";
        }

        private static void DateTimeCheck(StringBuilder code, PropertyConfig field)
        {
            if (!field.CanEmpty || field.IsRequired)
            {
                code.Append(field.Nullable
                    ? $@"
            if({field.Name} == null)
                 {EmptyCode(field)}"
                    : $@"
            if({field.Name} == DateTime.MinValue)
                 {EmptyCode(field)}");
            }
            if (field.Max == null && field.Min == null)
                return;
            code.Append(!field.CanEmpty || field.IsRequired
                ? @"
            else 
            {"
                : field.Nullable
                    ? $@"
            if({field.Name} != null)
            {{"
                    : "");

            var msg = field["ErrorMessage"];
            if (field.Max != null && field.Min != null)
            {
                code.Append($@"
                if({field.Name} > new DateTime({field.Max}) ||{field.Name} < new DateTime({field.Min}))");
                code.Append(string.IsNullOrWhiteSpace(msg)
                    ? $@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""不能大于{field.Max}或小于{field.Min}"");"
                    : $@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            else if (field.Max != null)
            {
                code.Append($@"
                if({field.Name} > new DateTime({field.Max}))");
                code.Append(string.IsNullOrWhiteSpace(msg)
                    ? $@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""不能大于{field.Max}"");"
                    : $@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            else if (field.Min != null)
            {
                code.Append($@"
                if({field.Name} < new DateTime({field.Min}))");
                code.Append(string.IsNullOrWhiteSpace(msg)
                    ? $@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""不能小于{field.Min}"");"
                    : $@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            if (!field.CanEmpty || field.IsRequired || field.Nullable)
                code.Append(@"
            }");
        }

        private static void NumberCheck(StringBuilder code, PropertyConfig field)
        {
            if (field.Nullable && (!field.CanEmpty || field.IsRequired))
            {
                code.Append($@"
            if({field.Name} == null)
                 {EmptyCode(field)}");
            }

            bool isMin = decimal.TryParse(field.Min, out var min);
            bool isMax = decimal.TryParse(field.Max, out var max);
            if (!isMin && !isMax)
                return;
            if (field.Nullable)
            {
                if (field.CanEmpty && !field.IsRequired)
                    code.Append($@"
            if({field.Name} != null)");
                code.Append(@"
            {");
            }

            string last = field.CsType == "decimal" ? "M" : "";

            var msg = field["ErrorMessage"];
            if (isMin && isMax)
            {
                code.Append($@"
            if({field.Name} > {max}{last} ||{field.Name} < {min}{last})");
                code.Append(string.IsNullOrWhiteSpace(msg)
                    ? $@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""不能大于{max}或小于{min}"");"
                    : $@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            else if (isMax)
            {
                code.Append($@"
            if({field.Name} > {max}{last})");
                code.Append(string.IsNullOrWhiteSpace(msg)
                    ? $@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""不能大于{max}"");"
                    : $@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            else
            {
                code.Append($@"
            if({field.Name} < {min}{last})");
                code.Append(string.IsNullOrWhiteSpace(msg)
                    ? $@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""不能小于{min}"");"
                    : $@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            if (field.Nullable)
                code.Append(@"
            }");
        }

        private static void StringCheck(StringBuilder code, PropertyConfig field)
        {
            if (!field.CanEmpty || field.IsRequired)
            {
                code.Append($@"

            if(string.IsNullOrWhiteSpace({field.Name}))
                {EmptyCode(field)}");
            }

            if (field.Datalen <= 0 && field.Min == null)
                return;

            if (!field.CanEmpty || field.IsRequired)
                code.Append(@"
            else if(");
            else
                code.Append($@"

            if(!string.IsNullOrWhiteSpace({field.Name}) && ");

            var msg = field["ErrorMessage"];
            if (field.Datalen > 0 && field.Min != null)
            {
                code.Append($@"({field.Name}.Length > {field.Datalen} || {field.Name}.Length < {field.Min}))");
                code.Append(string.IsNullOrWhiteSpace(msg)
                ? $@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""不能少于{field.Datalen}或多于{field.Min}个字"");"
                : $@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            else if (field.Datalen > 0)
            {
                code.Append($@"{field.Name}.Length > {field.Datalen})");
                code.Append(string.IsNullOrWhiteSpace(msg)
                ? $@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""不能多于{field.Datalen}个字"");"
                : $@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            else
            {
                code.Append($@"{field.Name}.Length < {field.Min})");
                code.Append(string.IsNullOrWhiteSpace(msg)
                ? $@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""不能少于{field.Min}个字"");"
                : $@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
        }

        private static void ConvertEmptyValue(StringBuilder code, PropertyConfig field)
        {
            var ems = field.EmptyValue.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            code.Append(@"
            if(");
            bool isFirst = true;
            foreach (var em in ems)
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(@" || ");
                switch (field.CsType)
                {
                    case "string":
                        code.Append($@"{field.Name} == ""{em}""");
                        break;
                    case "Guid":
                        code.Append($@"{field.Name} == new Guid(""{em}"")");
                        break;
                    case "DataTime":
                        code.Append($@"{field.Name} == DataTime.Parse(""{em}"")");
                        break;
                    //case "int":
                    //case "long":
                    //case "decimal":
                    //case "float":
                    //case "double":
                    default:
                        code.Append($@"{field.Name} == {em}");
                        break;
                }
            }
            if (field.CanEmpty || field.CsType == "string")
            {
                code.Append($@")
                {field.Name} = null;");
            }
            else
            {
                code.Append($@")
                {field.Name} = default({field.CsType});");
            }
        }
    }
}