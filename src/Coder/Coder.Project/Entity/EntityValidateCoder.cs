using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder
{
    public class EntityValidateCoder
    {

        /// <summary>
        /// 当前表对象
        /// </summary>
        public IEntityConfig Model { get; set; }

        public string Code(IEnumerable<IPropertyConfig> fields)
        {
            var code = new StringBuilder();
            var configs = fields as IPropertyConfig[] ?? fields.ToArray();
            foreach (var property in configs.Where(p => !string.IsNullOrWhiteSpace(p.EmptyValue)))
            {
                ConvertEmptyValue(code, property);
            }

            foreach (var property in configs)
            {
                switch (property.CsType)
                {
                    case "string":
                        StringCheck(code, property);
                        continue;
                    case "int":
                    case "uint":
                    case "short":
                    case "double":
                    case "float":
                    case "long":
                    case "ulong":
                    case "decimal":
                        NumberCheck(code, property);
                        break;
                    case "DateTime":
                        DateTimeCheck(code, property);
                        break;
                }
            }
            return code.ToString();
        }

        static string EmptyCode(IPropertyConfig property)
        {
            var msg = property.Option["EmptyMessage"];
            return string.IsNullOrWhiteSpace(msg)
                ? $"result.AddNoEmpty(\"{property.Caption}\",nameof({property.Name}));"
                : $"result.Add(\"{property.Caption}\",nameof({property.Name}),\"{msg}\");";
        }

        private static void DateTimeCheck(StringBuilder code, IPropertyConfig property)
        {
            if (!property.CanEmpty || property.IsRequired)
            {
                code.Append(property.Nullable
                    ? $@"
            if({property.Name} == null)
                 {EmptyCode(property)}"
                    : $@"
            if({property.Name} == DateTime.MinValue)
                 {EmptyCode(property)}");
            }
            if (property.Max == null && property.Min == null)
                return;
            code.Append(!property.CanEmpty || property.IsRequired
                ? @"
            else 
            {"
                : property.Nullable
                    ? $@"
            if({property.Name} != null)
            {{"
                    : "");

            var msg = property.Option["ErrorMessage"];
            if (property.Max != null && property.Min != null)
            {
                code.Append($@"
                if({property.Name} > new DateTime({property.Max}) ||{property.Name} < new DateTime({property.Min}))");
                code.Append(string.IsNullOrWhiteSpace(msg)
                    ? $@"
                    result.Add(""{property.Caption}"",nameof({property.Name}),$""不能大于{property.Max}或小于{property.Min}"");"
                    : $@"
                    result.Add(""{property.Caption}"",nameof({property.Name}),$""{msg}"");");
            }
            else if (property.Max != null)
            {
                code.Append($@"
                if({property.Name} > new DateTime({property.Max}))");
                code.Append(string.IsNullOrWhiteSpace(msg)
                    ? $@"
                    result.Add(""{property.Caption}"",nameof({property.Name}),$""不能大于{property.Max}"");"
                    : $@"
                    result.Add(""{property.Caption}"",nameof({property.Name}),$""{msg}"");");
            }
            else if (property.Min != null)
            {
                code.Append($@"
                if({property.Name} < new DateTime({property.Min}))");
                code.Append(string.IsNullOrWhiteSpace(msg)
                    ? $@"
                    result.Add(""{property.Caption}"",nameof({property.Name}),$""不能小于{property.Min}"");"
                    : $@"
                    result.Add(""{property.Caption}"",nameof({property.Name}),$""{msg}"");");
            }
            if (!property.CanEmpty || property.IsRequired || property.Nullable)
                code.Append(@"
            }");
        }

        private static void NumberCheck(StringBuilder code, IPropertyConfig property)
        {
            if (property.Nullable && (!property.CanEmpty || property.IsRequired))
            {
                code.Append($@"
            if({property.Name} == null)
                 {EmptyCode(property)}");
            }

            bool isMin = decimal.TryParse(property.Min, out var min);
            bool isMax = decimal.TryParse(property.Max, out var max);
            if (!isMin && !isMax)
                return;
            if (property.Nullable)
            {
                if (property.CanEmpty && !property.IsRequired)
                    code.Append($@"
            if({property.Name} != null)");
                code.Append(@"
            {");
            }

            string last = property.CsType == "decimal" ? "M" : "";

            var msg = property.Option["ErrorMessage"];
            if (isMin && isMax)
            {
                code.Append($@"
            if({property.Name} > {max}{last} ||{property.Name} < {min}{last})");
                code.Append(string.IsNullOrWhiteSpace(msg)
                    ? $@"
                result.Add(""{property.Caption}"",nameof({property.Name}),$""不能大于{max}或小于{min}"");"
                    : $@"
                result.Add(""{property.Caption}"",nameof({property.Name}),$""{msg}"");");
            }
            else if (isMax)
            {
                code.Append($@"
            if({property.Name} > {max}{last})");
                code.Append(string.IsNullOrWhiteSpace(msg)
                    ? $@"
                result.Add(""{property.Caption}"",nameof({property.Name}),$""不能大于{max}"");"
                    : $@"
                result.Add(""{property.Caption}"",nameof({property.Name}),$""{msg}"");");
            }
            else
            {
                code.Append($@"
            if({property.Name} < {min}{last})");
                code.Append(string.IsNullOrWhiteSpace(msg)
                    ? $@"
                result.Add(""{property.Caption}"",nameof({property.Name}),$""不能小于{min}"");"
                    : $@"
                result.Add(""{property.Caption}"",nameof({property.Name}),$""{msg}"");");
            }
            if (property.Nullable)
                code.Append(@"
            }");
        }

        private static void StringCheck(StringBuilder code, IPropertyConfig property)
        {
            var field = property.DataBaseField;
            if (!property.CanEmpty || property.IsRequired)
            {
                code.Append($@"

            if(string.IsNullOrWhiteSpace({property.Name}))
                {EmptyCode(property)}");
            }

            if (field.Datalen <= 0 && property.Min == null)
                return;

            if (!property.CanEmpty || property.IsRequired)
                code.Append(@"
            else if(");
            else
                code.Append($@"

            if(!string.IsNullOrWhiteSpace({property.Name}) && ");

            var msg = property.Option["ErrorMessage"];
            if (field.Datalen > 0 && property.Min != null)
            {
                code.Append($@"({property.Name}.Length > {field.Datalen} || {property.Name}.Length < {property.Min}))");
                code.Append(string.IsNullOrWhiteSpace(msg)
                ? $@"
                result.Add(""{property.Caption}"",nameof({property.Name}),$""不能少于{field.Datalen}或多于{property.Min}个字"");"
                : $@"
                result.Add(""{property.Caption}"",nameof({property.Name}),$""{msg}"");");
            }
            else if (field.Datalen > 0)
            {
                code.Append($@"{property.Name}.Length > {field.Datalen})");
                code.Append(string.IsNullOrWhiteSpace(msg)
                ? $@"
                result.Add(""{property.Caption}"",nameof({property.Name}),$""不能多于{field.Datalen}个字"");"
                : $@"
                result.Add(""{property.Caption}"",nameof({property.Name}),$""{msg}"");");
            }
            else
            {
                code.Append($@"{property.Name}.Length < {property.Min})");
                code.Append(string.IsNullOrWhiteSpace(msg)
                ? $@"
                result.Add(""{property.Caption}"",nameof({property.Name}),$""不能少于{property.Min}个字"");"
                : $@"
                result.Add(""{property.Caption}"",nameof({property.Name}),$""{msg}"");");
            }
        }

        private static void ConvertEmptyValue(StringBuilder code, IPropertyConfig property)
        {
            var ems = property.EmptyValue.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            code.Append(@"
            if(");
            bool isFirst = true;
            foreach (var em in ems)
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(@" || ");
                switch (property.CsType)
                {
                    case "string":
                        code.Append($@"{property.Name} == ""{em}""");
                        break;
                    case "Guid":
                        code.Append($@"{property.Name} == new Guid(""{em}"")");
                        break;
                    case "DataTime":
                        code.Append($@"{property.Name} == DataTime.Parse(""{em}"")");
                        break;
                    //case "int":
                    //case "long":
                    //case "decimal":
                    //case "float":
                    //case "double":
                    default:
                        code.Append($@"{property.Name} == {em}");
                        break;
                }
            }
            if (property.CanEmpty || property.CsType == "string")
            {
                code.Append($@")
                {property.Name} = null;");
            }
            else
            {
                code.Append($@")
                {property.Name} = default({property.CsType});");
            }
        }
    }
}