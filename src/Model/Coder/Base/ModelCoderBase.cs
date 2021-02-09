using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 表操作基类
    /// </summary>
    public abstract class ModelCoderBase : CoderWithModel
    {
        /// <summary>
        /// 实体的注释头
        /// </summary>
        public string EntityRemHeader => $@"/// <summary>
        /// {Model.Caption.ToRemString(4)}
        /// </summary>
        /// <remark>
        /// {Model.Description.ToRemString(4)}
        /// </remark>";

        /// <summary>
        /// 统一的字段名称
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string FieldName(IPropertyConfig property) => $"_{property.Name.ToLWord()}";

        /// <summary>
        /// 统一的字段名称
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string PropertyName(IPropertyConfig property) =>
            property.IsInterfaceField && property.Entity.IsInterface
            ? $"{property.Entity.EntityName}.{property.Name}"
            : property.Name;

        protected abstract bool IsClient { get; }


        protected IPropertyConfig[] Columns => Model.PublishProperty.OrderBy(p => p.Index).ToArray();

        protected IPropertyConfig[] ReadWriteColumns => Model.PublishProperty.Where(p => p.CanGet && p.CanSet).OrderBy(p => p.Index).ToArray();


        #region 通用代码

        /// <summary>
        /// 初始化实体默认值的代码
        /// </summary>
        /// <returns></returns>
        protected string DefaultValueCode()
        {
            StringBuilder code = new StringBuilder();
            foreach (var property in ReadWriteColumns.Select(p => p).Where(p => !string.IsNullOrWhiteSpace(p.Initialization)))
            {
                if (property.NoProperty)
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


        protected string ExtendInterface()
        {
            if (Model.DataTable.IsQuery)
                return null;
            var list = new List<string>();
            if (Model.DataTable.UpdateByModified)
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
                    if (entity != null && !entity.ExtendConfigListBool["CustomField"])
                    {
                        list.Add(inf);
                    }
                }
            }
            if (list.Count == 0)
                return null;
            return " : " + string.Join(" , ", list);
        }

        protected void ContentProperty(IPropertyConfig property, StringBuilder code)
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
        public {type} {property.Name}_Number
        {{
            get => ({type})this.{property.Name};
            set => this.{property.Name} = ({property.CustomType})value;
        }}");
            }
            if (property.DataType == "ByteArray")
            {
                code.Append($@"
        {FieldHeader(property, false)}
        public string {property.Name}_Base64
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