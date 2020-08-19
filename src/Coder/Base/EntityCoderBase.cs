using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// ���������
    /// </summary>
    public abstract class EntityCoderBase : CoderWithEntity
    {
        /// <summary>
        /// ʵ���ע��ͷ
        /// </summary>
        public string EntityRemHeader => $@"/// <summary>
        /// {ToRemString(Entity.Caption, 4)}
        /// </summary>
        /// <remark>
        /// {ToRemString(Entity.Description, 4)}
        /// </remark>";

        /// <summary>
        /// ͳһ���ֶ�����
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string FieldName(PropertyConfig property) => $"_{property.Name.ToLWord()}";

        protected abstract bool IsClient { get; }

        private PropertyConfig[] _columns;
        protected PropertyConfig[] Columns => _columns ??= Entity.PublishProperty.ToArray();

        private PropertyConfig[] _rwcolumns;
        protected PropertyConfig[] ReadWriteColumns => _rwcolumns ??= Columns.Where(p => p.CanGet && p.CanSet).ToArray();
        /// <summary>
        /// ��ʼ��ʵ��Ĭ��ֵ�Ĵ���
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


        protected static string PropertyHeader(PropertyConfig property, bool isInterface=false, bool? json = null)
        {
            return FieldHeader(property,!isInterface,json);
        }

        protected static string FieldHeader(PropertyConfig property, bool isInterface, bool? json = null)
        {
            var code = new StringBuilder();
            code.Append(RemCode(property));
            if (!isInterface)
            {
                code.Append(@"
        [IgnoreDataMember,JsonIgnore]");
            }
            else
            {
                code.Append(DataRuleCode(property));
                code.Append(@"
        [DataMember");
                if (json == null)
                    json = !property.NoneJson;
                if (json.Value)
                {
                    code.Append($@" , JsonProperty(""{property.JsonName}"", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling= DefaultValueHandling.Ignore)");
                    if (property.CsType == "DateTime")
                        code.Append(" , JsonConverter(typeof(MyDateTimeConverter))");
                }
                else
                {
                    code.Append(" , JsonIgnore");
                }
                if (property.IsBlob || property.InnerField)
                    code.Append(" , Browsable(false)");
                if (property.ReadOnly)
                    code.Append(" , ReadOnly(true)");
                if (property.Caption != null)
                    code.AppendFormat(@" , DisplayName(@""{0}"")", property.Caption);
                code.Append("]");
            }
            return code.ToString();
        }
        public static string RemCode(PropertyConfig property, bool simple = false, int space = 8)
        {
            var code = new StringBuilder();
            code.AppendLine();
            code.Append(' ', space);
            code.Append("/// <summary>");
            code.AppendLine();
            code.Append(' ', space);
            code.Append($@"///  {ToRemString(property.Caption, space)}");
            code.AppendLine();
            if (!simple && !property.Parent.NoDataBase)
            {
                if (property.IsLinkKey)
                {
                    code.Append(' ', space);
                    code.AppendLine($@"///  --��� : [{property.LinkTable}-{property.LinkField}]");
                }
                else if (property.IsLinkField)
                {
                    code.Append(' ', space);
                    code.AppendLine($@"///  --{(!property.NoStorage && property.IsCompute ? "����" : "����")}�ֶ� : [{property.LinkTable}-{property.LinkField}]");
                }
                else if (property.NoStorage)
                {
                    code.Append(' ', space);
                    code.AppendLine(@"///  -- ���ֶβ��洢�����ݿ���");
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

        public static string DataRuleCode(PropertyConfig property)
        {
            StringBuilder code = new StringBuilder();
            var re = code.ToString();
            code.Append(@"
        [DataRule(");
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
            code.Append(")]");
            return code.ToString();
        }
        public static string HelloCode(EntityConfig entity)
        {
            StringBuilder code = new StringBuilder();
            bool first = true;
            code.Append($@"new {entity.Name}
            {{");
            foreach (var property in entity.ClientProperty)
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

        static string HelloCode(PropertyConfig property, bool cs = true)
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
        public static string HelloCode(EntityConfig entity, string name)
        {
            StringBuilder code = new StringBuilder();
            foreach (var property in entity.ClientProperty)
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
        #region ö������

        protected static void ContentProperty(PropertyConfig property, StringBuilder code)
        {
            var enumc = GlobalConfig.GetEnum(property.CustomType);
            if (enumc != null)
            {
                var type = property.CsType == "enum" ? "int" : property.CsType;

                code.Append($@"
        /// <summary>
        /// {property.Caption}�Ŀɶ�����
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName(""{property.Caption}"")]
        public string {property.Name}_Content => {property.Name}.ToCaption();

        /// <summary>
        /// {property.Caption}����������
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
        {PropertyHeader(property,false, true)}
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