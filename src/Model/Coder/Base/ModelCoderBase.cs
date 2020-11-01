using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 表操作基类
    /// </summary>
    public abstract class ModelCoderBase<TModel> : CoderWithModel<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        /// <summary>
        /// 实体的注释头
        /// </summary>
        public string EntityRemHeader => $@"/// <summary>
        /// {ToRemString(Model.Caption, 4)}
        /// </summary>
        /// <remark>
        /// {ToRemString(Model.Description, 4)}
        /// </remark>";

        /// <summary>
        /// 统一的字段名称
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string FieldName(IFieldConfig property) => $"_{property.Name.ToLWord()}";

        /// <summary>
        /// 统一的字段名称
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string PropertyName(IFieldConfig property) =>
            property.IsInterfaceField && property.Entity.InterfaceInner
            ? $"{property.Entity.EntityName}.{property.Name}"
            : property.Name;

        protected abstract bool IsClient { get; }

        private IFieldConfig[] _columns;
        protected IFieldConfig[] Columns => _columns ??= Model.PublishProperty.ToArray();

        private IFieldConfig[] _rwcolumns;
        protected IFieldConfig[] ReadWriteColumns => _rwcolumns ??= Columns.Where(p => p.CanGet && p.CanSet).ToArray();

        /// <summary>
        /// 初始化实体默认值的代码
        /// </summary>
        /// <returns></returns>
        protected string DefaultValueCode()
        {
            StringBuilder code = new StringBuilder();
            foreach (var property in ReadWriteColumns.Select(p => p).Where(p => !string.IsNullOrWhiteSpace(p.Initialization)))
            {
                if (IsClient && property.DenyClient)
                    continue;
                if (property.CsType == "bool")
                    code.AppendFormat(@"
            _{0} = {1};", property.Name.ToLower(), property.Initialization == "b'0'" ? "false" : "true");
                else
                    code.AppendFormat(@"
            _{0} = {1};", property.Name.ToLower(), property.Initialization);
            }
            if (Model.PrimaryColumn == null || !Model.PrimaryColumn.IsGlobalKey)
                return code.ToString();
            code.AppendFormat(@"
            this.{0} = Guid.NewGuid();", Model.PrimaryColumn.Name);
            return code.ToString();
        }


        protected string PropertyHeader(IFieldConfig property)
        {
            return $@"{RemCode(property)}
        [JsonIgnore]";
        }

        protected string FieldHeader(IFieldConfig property, bool isInterface,bool noneJson)
        {
            if (isInterface)
            {
                return RemCode(property);
            }

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
                code.Add($@"JsonProperty(""{property.JsonName}"")");
                if (property.CsType == "DateTime")
                    code.Add("JsonConverter(typeof(MyDateTimeConverter))");
            }
            return code.Count == 0
                ? RemCode(property)
                : $@"{RemCode(property)}
        [{ string.Join(" , ", code)}]";
        }
        public static string RemCode(IFieldConfig property, bool simple = false, int space = 8)
        {
            var code = new StringBuilder();
            code.AppendLine();
            code.Append(' ', space);
            code.Append("/// <summary>");
            code.AppendLine();
            code.Append(' ', space);
            code.Append($@"///  {ToRemString(property.Caption, space)}");
            code.AppendLine();
            if (!simple && !property.Entity.NoDataBase)
            {
                if (property.IsLinkKey)
                {
                    code.Append(' ', space);
                    code.AppendLine($@"///  --外键 : [{property.LinkTable}-{property.LinkField}]");
                }
                else if (property.IsLinkField)
                {
                    code.Append(' ', space);
                    code.AppendLine($@"///  --{(!property.NoStorage && property.IsCompute ? "链接" : "冗余")}字段 : [{property.LinkTable}-{property.LinkField}]");
                }
                else if (property.NoStorage)
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
                code.Append($"///     {ToRemString(property.Description, space)}");
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
                code.Append($"///     {ToRemString(helloCode, space)}");
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
                code.Append($"///     {ToRemString(property.DataRuleDesc, space)}");
                code.AppendLine();
                code.Append(' ', space);
                code.Append("/// </value>");
            }
            return code.ToString();
        }

        public string DataRuleCode(FieldConfig property)
        {
            if (Model.IsQuery)
                return null;
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

        public string DataRuleCode(IFieldConfig property)
        {
            if (Model.IsQuery)
                return null;
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

        static string HelloCode(IFieldConfig property, bool cs = true)
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

        #region 枚举属性


        protected string ExtendInterface()
        {
            if (Model.IsQuery)
                return null;
            var list = new List<string>();
            if (Model.UpdateByModified)
                list.Add("IEditStatus");
            if (Model.PrimaryColumn != null)
            {
                if (Model.PrimaryColumn.IsGlobalKey)
                    list.Add("IGlobalKey");
                else
                    list.Add($"IIdentityData<{Model.PrimaryColumn.CsType}>");
            }
            if (Model.IsUniqueUnion)
            {
                list.Add("IUnionUniqueEntity");
            }

            if (Model.Interfaces != null)
            {
                var infs = Model.Interfaces.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var inf in infs)
                {
                    var entity = GlobalConfig.GetEntity(inf);
                    if (entity == null || !entity.ExtendConfigListBool["NoApi"])
                    {
                        list.Add(inf);
                    }
                }
            }
            if (list.Count == 0)
                return null;
            return " : " + string.Join(" , ", list);
        }

        protected void ContentProperty(IFieldConfig property, StringBuilder code)
        {
            if (property.EnumConfig != null && property.CsType != "string")
            {
                var type = property.CsType == "enum" ? "int" : property.CsType;

                code.Append($@"
        /// <summary>
        /// {property.Caption}的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName(""{property.Caption}"")]
        public string {property.Name}_Content => {property.Name}.ToCaption();

        /// <summary>
        /// {property.Caption}的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        {property.AccessType} {type} {property.Name}_Number
        {{
            get => ({type})this.{property.Name};
            set => this.{property.Name} = ({property.CustomType})value;
        }}");
            }
            if (property.DataType == "ByteArray")
            {
                code.Append($@"
        {FieldHeader(property, false, false)}
        {property.AccessType} string {property.Name}_Base64
        {{
            get
            {{
                return this.{FieldName(property)} == null
                       ? null
                       : Convert.ToBase64String({property.Name});
            }}
            set
            {{
                this.{property.Name} = string.IsNullOrWhiteSpace(value)
                       ? null
                       : Convert.FromBase64String(value);
            }}
        }}");
            }
        }
        #endregion
    }
}