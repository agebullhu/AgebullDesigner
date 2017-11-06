using System;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityValidateBuilder : EntityBuilderBase
    {
        public override string BaseCode=> ValidateCode();

        protected override string Folder => "Validate";
        
        #region 数据校验

        public string ValidateCode()
        {
            return $@"

        /// <summary>
        /// 扩展校验
        /// </summary>
        /// <param name=""result"">结果存放处</param>
        partial void ValidateEx(ValidateResult result);

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <param name=""result"">结果存放处</param>
        public override void Validate(ValidateResult result)
        {{
            {(Entity.IsClass || Entity.PrimaryColumn== null ? "" : "result.Id = " + Entity.PrimaryColumn.Name + ".ToString()") }; 
            base.Validate(result);{Code()}
            ValidateEx(result);
        }}";
        }

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

        public static void DateTimeCheck(StringBuilder code, PropertyConfig field)
        {
            if (!field.CanEmpty)
            {
                code.Append($@"
            if({field.Name} == DateTime.MinValue)
                 result.AddNoEmpty(""{field.Caption}"",nameof({field.Name}));");
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

            if (field.Max != null && field.Min != null)
            {
                code.Append($@"
                if({field.Name} > new DateTime({field.Max}) ||{field.Name} < new DateTime({field.Min}))
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""不能大于{field.Max}或小于{field.Min}"");");
            }
            else if (field.Max != null)
            {
                if (!field.CanEmpty)
                    code.Append(@" else ");
                code.Append($@"
                if({field.Name} > new DateTime({field.Max}))
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""不能大于{field.Max}"");");
            }
            else if (field.Min != null)
            {
                if (!field.CanEmpty)
                    code.Append(@" else ");
                code.Append($@"
                if({field.Name} < new DateTime({field.Min}))
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""不能小于{field.Min}"");");
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
            if (field.CanEmpty && field.Nullable)
                code.Append($@"
            if((ulong){field.Name} != null)
            {{");
            if (max > 1 && field.Min != null)
            {
                code.Append($@"
            if((ulong){field.Name} > {max}UL ||(ulong){field.Name} < {field.Min})
                result.Add(""{field.Caption}"",nameof({field.Name}),$""不能大于{max}UL或小于{field.Min}"");");
            }
            else if (max > 1)
            {
                code.Append($@"
            if((ulong){field.Name} > {max}UL)
                result.Add(""{field.Caption}"",nameof({field.Name}),$""不能大于{max}UL"");");
            }
            else if (field.Min != null)
            {
                code.Append($@"
            if((ulong){field.Name} < {field.Min})
                result.Add(""{field.Caption}"",nameof({field.Name}),$""不能小于{field.Min}"");");
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
                 result.AddNoEmpty(""{field.Caption}"",nameof({field.Name}));");
            }
            if (field.Datalen > 0 || field.Min != null)
            {
                if (field.CanEmpty)
                    code.Append($@"
            if({field.Name} != null)
            {{");
                else
                    code.Append(@"
            else 
            {");

                if (field.Datalen > 0 && field.Min != null)
                {
                    code.Append($@"
                if({field.Name}.Length > {field.Datalen} ||{field.Name}.Length < {field.Min})
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""不能少于{field.Datalen}或多于{field.Min}个字"");");
                }
                else if (field.Datalen > 0)
                {
                    code.Append($@"
                if({field.Name}.Length > {field.Datalen})
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""不能多于{field.Datalen}个字"");");
                }
                else
                {
                    code.Append($@"
                if({field.Name}.Length < {field.Min})
                   result.Add(""{field.Caption}"",nameof({field.Name}),$""不能少于{field.Min}个字"");");
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

        #endregion

    }
}