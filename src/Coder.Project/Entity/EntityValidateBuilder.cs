using System;
using System.Linq;
using System.Text;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign.Coder.Cs
{
    public sealed class EntityValidateBuilder : EntityBuilderBase
    {
        public override string BaseCode=> ValidateCode();

        protected override string Folder => "Validate";
        
        #region ����У��

        public string ValidateCode()
        {
            return $@"

        /// <summary>
        /// ��չУ��
        /// </summary>
        /// <param name=""result"">�����Ŵ�</param>
        partial void ValidateEx(ValidateResult result);

        /// <summary>
        /// ����У��
        /// </summary>
        /// <param name=""result"">�����Ŵ�</param>
        public override void Validate(ValidateResult result)
        {{
            result.Id = Id; 
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

        private static void DateTimeCheck(StringBuilder code, PropertyConfig field)
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
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""���ܴ���{field.Max}��С��{field.Min}"");");
            }
            else if (field.Max != null)
            {
                if (!field.CanEmpty)
                    code.Append(@" else ");
                code.Append($@"
                if({field.Name} > new DateTime({field.Max}))
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""���ܴ���{field.Max}"");");
            }
            else if (field.Min != null)
            {
                if (!field.CanEmpty)
                    code.Append(@" else ");
                code.Append($@"
                if({field.Name} < new DateTime({field.Min}))
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""����С��{field.Min}"");");
            }
            code.Append(@"
            }");
        }

        private static void NumberCheck(StringBuilder code, PropertyConfig field)
        {
            if (field.Datalen > 0 || field.Min != null)
            {
                if (field.CanEmpty)
                    code.Append($@"
            if({field.Name} != null)
            {{");
                if (field.Datalen > 0 && field.Min != null)
                {
                    code.Append($@"
            if({field.Name} > {field.Datalen} ||{field.Name} < {field.Min})
                result.Add(""{field.Caption}"",nameof({field.Name}),$""���ܴ���{field.Datalen}��С��{field.Min}"");");
                }
                else if (field.Datalen > 0)
                {
                    code.Append($@"
            if({field.Name} > {field.Datalen})
                result.Add(""{field.Caption}"",nameof({field.Name}),$""���ܴ���{field.Datalen}"");");
                }
                else if (field.Min != null)
                {
                    code.Append($@"
            if({field.Name} < {field.Min})
                result.Add(""{field.Caption}"",nameof({field.Name}),$""����С��{field.Min}"");");
                }
                if (field.CanEmpty)
                    code.Append(@"
            }");
            }
        }

        private static void StringCheck(StringBuilder code, PropertyConfig field)
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
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""��������{field.Datalen}����{field.Min}����"");");
                }
                else if (field.Datalen > 0)
                {
                    code.Append($@"
                if({field.Name}.Length > {field.Datalen})
                    result.Add(""{field.Caption}"",nameof({field.Name}),$""���ܶ���{field.Datalen}����"");");
                }
                else
                {
                    code.Append($@"
                if({field.Name}.Length < {field.Min})
                   result.Add(""{field.Caption}"",nameof({field.Name}),$""��������{field.Min}����"");");
                }
                code.Append(@"
            }");
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

        #endregion

    }
}