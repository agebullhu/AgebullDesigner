using Agebull.EntityModel.Config;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder
{

    /// <summary>
    /// 代码生成基类
    /// </summary>
    public class CoderBase
    {
        public static void RepairConfigName(ConfigBase config, bool includeName)
        {
            if (includeName)
                config.Name = config.Name.ToLanguageName();
            config.Caption = config.Caption.ToLanguageName();
            config.Description = config.Description.ToLanguageName();

        }

        /// <summary>
        /// 检查并组成代码文件路径
        /// </summary>
        /// <param name="project"></param>
        /// <param name="root"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string CheckPath(ProjectConfig project, string root, params string[] names)
        {
            if (names.Length == 0)
            {
                return string.IsNullOrWhiteSpace(project.BranchFolder)
                    ? root
                    : GlobalConfig.CheckPath(root, project.BranchFolder);
            }
            if (string.IsNullOrWhiteSpace(project.BranchFolder))
                return GlobalConfig.CheckPath(root, names);
            var list = new List<string>();
            list.AddRange(names);
            list.Add(project.BranchFolder);
            return GlobalConfig.CheckPath(root, list.ToArray());
        }


        /// <summary>
        /// 检查并组成文档文件路径
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static string GetDocumentPath(ProjectConfig project)
        {
            return GlobalConfig.CheckPath(SolutionConfig.Current.RootPath, SolutionConfig.Current.DocFolder);

        }

        #region 通用代码


        protected string PropertyHeader(IPropertyConfig property)
        {
            return $@"{RemCode(property)}
        [JsonIgnore]";
        }

        protected string FieldHeader(IPropertyConfig property, bool noneJson)
        {
            if (property.IsReference && property.Option is IPropertyConfig iField && iField.Entity.IsInterface)
            {
                return RemCode(property);
            }

            var field = property.DataBaseField;
            var code = new List<string>();
            //var rule = DataRuleCode(property);
            //if(rule != null)
            //code.Add(rule);
            if (noneJson || property.NoneJson)
            {
                code.Add("JsonIgnore");
            }
            else
            {
                if (property.IsRequired || property.IsCaption || property.IsPrimaryKey || field.IsLinkCaption || field.IsLinkCaption)
                    code.Add($@"JsonProperty(""{property.JsonName}"", NullValueHandling = NullValueHandling.Include)");
                else
                    code.Add($@"JsonProperty(""{property.JsonName}"", DefaultValueHandling = DefaultValueHandling.Ignore)");
                if (property.CsType == "DateTime")
                    code.Add("JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))");
            }
            return code.Count == 0
                ? RemCode(property)
                : $@"{RemCode(property)}
        [{ string.Join(" , ", code)}]";
        }
        public static string RemCode(IPropertyConfig property, bool simple = false, int space = 8)
        {
            var field = property.DataBaseField;
            var code = new StringBuilder();
            code.AppendLine();
            code.Append(' ', space);
            code.Append("/// <summary>");
            code.AppendLine();
            code.Append(' ', space);
            code.Append($@"///  {property.Caption.ToRemString(space)}");
            code.AppendLine();
            if (!simple && property.Entity.EnableDataBase)
            {
                if (field.IsLinkKey)
                {
                    code.Append(' ', space);
                    code.AppendLine($@"///  --外键 : [{field.LinkTable}-{field.LinkField}]");
                }
                else if (field.IsLinkField)
                {
                    code.Append(' ', space);
                    code.AppendLine($@"///  --{(!field.NoStorage && property.IsCompute ? "链接" : "冗余")}字段 : [{field.LinkTable}-{field.LinkField}]");
                }
                else if (field.NoStorage)
                {
                    code.Append(' ', space);
                    code.AppendLine(@"///  -- 此字段不存储在数据库中");
                }
            }
            code.Append(' ', space);
            code.Append("/// </summary>");
            if (simple)
                return code.ToString();
            if (!string.IsNullOrWhiteSpace(property.Description)
                && property.Description != property.Name
                && property.Description != property.Caption)
            {
                code.AppendLine();
                code.Append(' ', space);
                code.Append("/// <remarks>");
                code.AppendLine();
                code.Append(' ', space);
                code.Append($"///     {property.Description.ToRemString(space)}");
                code.AppendLine();
                code.Append(' ', space);
                code.Append("/// </remarks>");
            }

            var helloCode = HelloCode(property, false);
            if (!string.IsNullOrWhiteSpace(helloCode))
            {
                code.AppendLine();
                code.Append(' ', space);
                code.Append("/// <example>");
                code.AppendLine();
                code.Append(' ', space);
                code.Append($"///     {helloCode.ToRemString(space)}");
                code.AppendLine();
                code.Append(' ', space);
                code.Append("/// </example>");
            }

            if (!string.IsNullOrWhiteSpace(property.DataRuleDesc))
            {
                code.AppendLine();
                code.Append(' ', space);
                code.Append("/// <value>");
                code.AppendLine();
                code.Append(' ', space);
                code.Append($"///     {property.DataRuleDesc.ToRemString(space)}");
                code.AppendLine();
                code.Append(' ', space);
                code.Append("/// </value>");
            }
            return code.ToString();
        }

        public string DataRuleCode(FieldConfig property)
        {
            StringBuilder code = new StringBuilder();
            var re = code.ToString();
            code.Append(@"
        DataRule(");
            bool has = false;
            if (!property.IsRequired)
            {
                code.Append("CanNull = true");
                has = true;
            }
            if (!string.IsNullOrWhiteSpace(property.Min))
            {
                if (has)
                    code.Append(',');
                else has = true;
                code.Append($@"Min = ""{ property.Min}""");
            }

            if (!string.IsNullOrWhiteSpace(property.Max))
            {
                if (has)
                    code.Append(',');
                else has = true;
                code.Append($@"Max = ""{ property.Max}""");
            }
            if (!has)
                return re;
            code.Append(")");
            return code.ToString();
        }

        public string DataRuleCode(IPropertyConfig property)
        {
            StringBuilder code = new StringBuilder();
            var re = code.ToString();
            code.Append("DataRule(");
            bool has = false;
            if (!property.IsRequired)
            {
                code.Append("CanNull = true");
                has = true;
            }
            if (!string.IsNullOrWhiteSpace(property.Min))
            {
                if (has)
                    code.Append(',');
                else has = true;
                code.Append($@"Min = ""{ property.Min}""");
            }

            if (!string.IsNullOrWhiteSpace(property.Max))
            {
                if (has)
                    code.Append(',');
                else has = true;
                code.Append($@"Max = ""{ property.Max}""");
            }
            if (!has)
                return re;
            code.Append(")");
            return code.ToString();
        }

        public static string HelloCode(IEntityConfig model)
        {
            StringBuilder code = new StringBuilder();
            bool first = true;
            code.Append($@"new {model.Name}
            {{");
            foreach (var property in model.ClientProperty)
            {
                var value = HelloCode(property);
                switch (property.CsType)
                {
                    case "string":
                        value = value == null ? "null" : $"\"{value}\"";
                        break;
                    case "DateTime":
                        value = $"DateTime.Parse(\"{value}\")";
                        break;
                }

                if (first)
                    first = false;
                else
                    code.Append(',');
                code.Append($@"
                {property.Name} = {value}");
            }
            code.Append(@"
            }");
            return code.ToString();
        }

        static string HelloCode(IPropertyConfig property, bool cs = true)
        {
            if (!string.IsNullOrWhiteSpace(property.HelloCode))
                return property.HelloCode;
            if (property.EnumConfig != null)
            {
                return cs
                    ? property.EnumConfig.Items.FirstOrDefault()?.Name ?? "None"
                    : property.EnumConfig.Items.FirstOrDefault()?.Value ?? "0";
            }
            return property.DataType switch
            {
                "String" => null,
                "Boolean" => "true",
                "DateTime" => property.IsTime ? "2012-12-21 23:59:59" : "2012-12-21",
                _ => "0",
            };
        }

        public static string HelloCode(IEntityConfig model, string name)
        {
            StringBuilder code = new StringBuilder();
            foreach (var property in model.ClientProperty.Select(p => p))
            {
                var value = HelloCode(property);
                if (property.CsType == "string")
                {
                    value = value == null ? "null" : $"\"{value}\"";
                }
                else if (property.CsType == "DateTime")
                {
                    value = $"DateTime.Parse(\"{value}\")";
                }
                code.Append($@"
            {name}.{property.Name} = {value};");
            }
            return code.ToString();
        }
        #endregion
    }
}