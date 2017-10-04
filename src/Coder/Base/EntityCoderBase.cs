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
        protected abstract bool IsClient { get; }

        private PropertyConfig[] _columns;
        protected PropertyConfig[] Columns => _columns ?? (_columns = IsClient
                                                  ? Entity.CppProperty.ToArray()
                                                  : Entity.PublishProperty.ToArray());

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



        protected string Attribute(PropertyConfig property)
        {
            var attribute = new StringBuilder();
            attribute.Append("[IgnoreDataMember");
            if (!IsClient)
            {
                if (!property.NoneJson)
                {
                    attribute.Append($@" , JsonProperty(""{property.Name}"", NullValueHandling = NullValueHandling.Ignore)");
                    if (property.CsType == "DateTime")
                        attribute.Append(" , JsonConverter(typeof(MyDateTimeConverter))");
                }
                else
                {
                    attribute.Append(" , JsonIgnore");
                }
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

        #region 枚举属性

        protected static void EnumContentProperty(PropertyConfig property, StringBuilder code)
        {
            var enumc = GlobalConfig.GetEnum(property.CustomType);
            if (enumc == null)
                return;
            code.Append($@"
        /// <summary>
        /// {property.Caption}的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName(""{property.Caption}"")]
        public string {property.Name}_Content
        {{
            get
            {{");
            code.Append($@"
                switch({property.Name})
                {{");
            foreach (var item in enumc.Items)
            {
                code.Append(
                    $@"
                case {enumc.Name}.{item.Name}:
                    return @""{item.Caption}"";");
            }
            code.Append(@"
                default:
                    return null;
                }
            }
        }");
            code.Append($@"
        /// <summary>
        /// {property.Caption}的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        {property.AccessType} {property.CsType} {property.PropertyName}_Number
        {{
            get
            {{
                return ({property.CsType})this.{property.PropertyName};
            }}
            set
            {{
                this.{property.PropertyName} = ({property.CustomType})value;
            }}
        }}");
        }
        #endregion
    }
}