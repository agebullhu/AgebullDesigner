using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 表操作基类
    /// </summary>
    public abstract class EntityCoderBase : CoderWithEntity
    {
        /// <summary>
        /// 统一的字段名称
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string FieldName(PropertyConfig property) => $"_{property.Name.ToLWord()}";

        protected abstract bool IsClient { get; }

        private PropertyConfig[] _columns;
        protected PropertyConfig[] Columns => _columns ?? (_columns = Entity.PublishProperty.ToArray());

        private PropertyConfig[] _rwcolumns;
        protected PropertyConfig[] ReadWriteColumns
        {
            get { return _rwcolumns ?? (_rwcolumns = Columns.Where(p => p.CanGet && p.CanSet).ToArray()); }
        }
        /// <summary>
        /// 初始化实体默认值的代码
        /// </summary>
        /// <returns></returns>
        protected string DefaultValueCode()
        {
            StringBuilder code = new StringBuilder();
            foreach (PropertyConfig property in ReadWriteColumns.Where(p => !string.IsNullOrWhiteSpace(p.Initialization)))
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
            if (Entity.PrimaryColumn == null || !Entity.PrimaryColumn.IsGlobalKey)
                return code.ToString();
            code.AppendFormat(@"
            this.{0} = Guid.NewGuid();", Entity.PrimaryColumn.Name);
            return code.ToString();
        }



        protected static string PropertyHeader(PropertyConfig property, string jsonName = null, bool? json = null)
        {
            var attribute = new StringBuilder();
            attribute.Append(RemCode(property));

            attribute.Append(@"
        [DataMember");
            if (json == null)
                json = !property.NoneJson;
            if (json.Value)
            {
                attribute.Append($@" , JsonProperty(""{jsonName ?? property.JsonName}"", NullValueHandling = NullValueHandling.Ignore)");
                if (property.CsType == "DateTime")
                    attribute.Append(" , JsonConverter(typeof(MyDateTimeConverter))");
            }
            else
            {
                attribute.Append(" , JsonIgnore");
            }
            if (property.IsBlob || property.InnerField)
                attribute.Append(" , Browsable(false)");
            if (property.ReadOnly)
                attribute.Append(" , ReadOnly(true)");
            if (property.Caption != null)
                attribute.AppendFormat(@" , DisplayName(@""{0}"")", property.Caption);
            attribute.Append("]");
            return attribute.ToString();
        }

        public static string RemCode(PropertyConfig property)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"
        /// <summary>
        /// {ToRemString(property.Caption)}
        /// </summary>");
            if (!string.IsNullOrEmpty(property.Description) &&
                !string.Equals(property.Description, property.Caption, StringComparison.OrdinalIgnoreCase))
            {
                code.Append($@"
        /// <remarks>
        /// {ToRemString(property.Description)}
        /// </remarks>");
            }

            if (!string.IsNullOrEmpty(property.HelloCode))
            {
                code.Append($@"
        /// <example>
        /// {ToRemString(property.HelloCode)}
        /// </example>");
            }

            if (!string.IsNullOrEmpty(property.DataRuleDesc))
            {
                code.Append($@"
        /// <value>
        /// {ToRemString(property.DataRuleDesc)}
        /// </value>");
            }

            var re = code.ToString();
            code.Append(@"
        [DataRule(");
            bool has = false;
            if (property.CanEmpty || !property.IsRequired)
            {
                code.Append("CanNull = true");
                has = true;
            }

            if (property.CsType == "DateTime")
            {
                if (!string.IsNullOrEmpty(property.Min))
                {
                    if (has)
                        code.Append(',');
                    else has = true;
                    code.Append($@"MinDate = DateTime.Parse(""{property.Min}"")");
                }

                if (!string.IsNullOrEmpty(property.Max))
                {
                    if (has)
                        code.Append(',');
                    else has = true;
                    code.Append($@"MaxDate = DateTime.Parse(""{property.Max}"")");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(property.Min))
                {
                    if (has)
                        code.Append(',');
                    else has = true;
                    code.Append($@"Min = {property.Min}");
                }

                if (!string.IsNullOrEmpty(property.Max))
                {
                    if (has)
                        code.Append(',');
                    else has = true;
                    code.Append($@"Max = {property.Max}");
                }
            }
            if (!has)
                return re;
            code.Append(")]");
            return code.ToString();
        }
        public static string HelloCode(EntityConfig entity)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"new {entity.Name}
            {{");
            foreach (var property in entity.LastProperties.Where(p => p.CanUserInput))
            {
                var value = property.HelloCode;
                if (property.CsType == "string")
                {
                    value = value == null ? "null" : $"\"{value}\"";
                }
                else if (property.CsType == "DateTime")
                {
                    value = value == null ? DateTime.Today.ToString(CultureInfo.InvariantCulture) : $"DateTime.Parse(\"{value}\")";
                }
                else if (property.CustomType != null)
                {
                    value = value == null ? $"default({property.CustomType})" : $"{property.CustomType}.{value}";
                }
                else
                {
                    value = value ?? $"default({property.CsType})";
                }
                code.Append($@"
                {property.Name} = {value},");
            }
            code.Append(@"
            }");
            return code.ToString();
        }

        public static string HelloCode(EntityConfig entity, string name)
        {
            StringBuilder code = new StringBuilder();
            foreach (var property in entity.ClientProperty)
            {
                var value = property.HelloCode;
                if (property.CsType == "string")
                {
                    value = value == null ? "null" : $"\"{value}\"";
                }
                else if (property.CsType == "DateTime")
                {
                    value = value == null ? DateTime.Today.ToString(CultureInfo.InvariantCulture) : $"DateTime.Parse(\"{value}\")";
                }
                else if (property.CustomType != null)
                {
                    value = value == null ? $"default({property.CustomType})" : $"{property.CustomType}.{value}";
                }
                else
                {
                    value = value ?? $"default({property.CsType})";
                }
                code.Append($@"
            {name}.{property.Name} = {value};");
            }
            return code.ToString();
        }
        #region 枚举属性

        protected static void ContentProperty(PropertyConfig property, StringBuilder code)
        {
            var enumc = GlobalConfig.GetEnum(property.CustomType);
            if (enumc != null)
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
        {property.AccessType} {type} {property.PropertyName}_Number
        {{
            get => ({type})this.{property.PropertyName};
            set => this.{property.PropertyName} = ({property.CustomType})value;
        }}");
            }
            if (property.DataType == "ByteArray")
            {
                code.Append($@"
        {PropertyHeader(property, null, true)}
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