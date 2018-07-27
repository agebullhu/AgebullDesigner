using System;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public class EntityValidateCoder
    {
        public EntityConfig Entity { get; set; }

        public string Code()
        {
            var code = new StringBuilder();
            var fields = Entity.PublishProperty.Where(p => p.CanUserInput).ToArray();
            foreach (PropertyConfig field in fields.Where(p => !string.IsNullOrWhiteSpace(p.EmptyValue)))
            {
                ConvertEmptyValue(code, field);
            }

            foreach (PropertyConfig field in fields.Where(p => string.IsNullOrWhiteSpace(p.ExtendRole)))
            {
                switch (field.CsType)
                {
                    case "string":
                        StringCheck(code, field);
                        continue;
                    case "int":
                    case "long":
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

        public static void DateTimeCheck(StringBuilder code, PropertyConfig field)
        {
            if (!field.CanEmpty)
            {
                if (field.Nullable)
                {
                    code.Append($@"
            if({field.Name} == null)
                 {EmptyCode(field)}");
                }
                else
                {
                    code.Append($@"
            if({field.Name} == DateTime.MinValue)
                 {EmptyCode(field)}");
                }
            }
            if (field.Max == null && field.Min == null)
                return;
            if (field.CanEmpty)
                code.Append($@"
            if({field.Name} != null)
            {{");
            else
                code.Append(@"
            else 
            {");

            var msg = field["ErrorMessage"];
            if (field.Max != null && field.Min != null)
            {
                code.Append($@"
                if({field.Name} > new DateTime({field.Max}) ||{field.Name} < new DateTime({field.Min}))");
                if (string.IsNullOrWhiteSpace(msg))
                    code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""不能大于{field.Max}或小于{field.Min}"");");
                else
                    code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            else if (field.Max != null)
            {
                if (!field.CanEmpty)
                    code.Append(@" else ");
                code.Append($@"
                if({field.Name} > new DateTime({field.Max}))");
                if (string.IsNullOrWhiteSpace(msg))
                    code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""不能大于{field.Max}"");");
                else
                    code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            else if (field.Min != null)
            {
                if (!field.CanEmpty)
                    code.Append(@" else ");
                code.Append($@"
                if({field.Name} < new DateTime({field.Min}))");
                if (string.IsNullOrWhiteSpace(msg))
                    code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""不能小于{field.Min}"");");
                else
                    code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            code.Append(@"
            }");
        }

        public static void NumberCheck(StringBuilder code, PropertyConfig field)
        {
            if (field.Datalen <= 0 && field.Min == null)
                return;
            ulong max = 1;
            for (int i = 0; i < field.Datalen; i++)
            {
                max *= 10;
            }
            var msg = field["ErrorMessage"];
            if (field.CanEmpty && field.Nullable)
                code.Append($@"
            if((ulong){field.Name} != null)
            {{");
            if (max > 1 && field.Min != null)
            {
                code.Append($@"
            if((ulong){field.Name} > {max}UL ||(ulong){field.Name} < {field.Min})");
                if (string.IsNullOrWhiteSpace(msg))
                    code.Append($@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""不能大于{max}UL或小于{field.Min}"");");
                else
                    code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            else if (max > 1)
            {
                code.Append($@"
            if((ulong){field.Name} > {max}UL)");
                if (string.IsNullOrWhiteSpace(msg))
                    code.Append($@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""不能大于{max}UL"");");
                else
                    code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            else if (field.Min != null)
            {
                code.Append($@"
            if((ulong){field.Name} < {field.Min})");
                if (string.IsNullOrWhiteSpace(msg))
                    code.Append($@"
                result.Add(""{field.Caption}"",nameof({field.Name}),$""不能小于{field.Min}"");");
                else
                    code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
            }
            if (field.CanEmpty && field.Nullable)
                code.Append(@"
            }");
        }

        public static void StringCheck(StringBuilder code, PropertyConfig field)
        {
            if (!field.CanEmpty)
            {
                code.Append($@"
            if(string.IsNullOrWhiteSpace({field.Name}))
                 {EmptyCode(field)}");
            }
            if (field.Datalen > 0 || field.Min != null)
            {
                if (field.CanEmpty)
                    code.Append($@"
            if(!string.IsNullOrWhiteSpace({field.Name}))
            {{");
                else
                    code.Append(@"
            else 
            {");

                var msg = field["ErrorMessage"];
                if (field.Datalen > 0 && field.Min != null)
                {
                    code.Append($@"
                if({field.Name}.Length > {field.Datalen} ||{field.Name}.Length < {field.Min})");
                    if (string.IsNullOrWhiteSpace(msg))
                        code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""不能少于{field.Datalen}或多于{field.Min}个字"");");
                    else
                        code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
                }
                else if (field.Datalen > 0)
                {
                    code.Append($@"
                if({field.Name}.Length > {field.Datalen})");
                    if (string.IsNullOrWhiteSpace(msg))
                        code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""不能多于{field.Datalen}个字"");");
                    else
                        code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
                }
                else
                {
                    code.Append($@"
                if({field.Name}.Length < {field.Min})");
                    if (string.IsNullOrWhiteSpace(msg))
                        code.Append($@"
                   result.Add(""{field.Caption}"",nameof({field.Name}),$""不能少于{field.Min}个字"");");
                    else
                        code.Append($@"
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""{msg}"");");
                }
                code.Append(@"
            }");
            }
        }

        public static void ConvertEmptyValue(StringBuilder code, PropertyConfig field)
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