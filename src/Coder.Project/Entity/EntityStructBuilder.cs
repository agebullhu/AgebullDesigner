using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityStructBuilder : EntityBuilderBase
    {
        #region 基础

        public override string BaseCode => EntityStruct();

        protected override string Folder => "Struct";

        #endregion

        #region 数据结构

        private string EntityStruct()
        {
            if (Entity.PrimaryColumn == null)
                return null;
            bool isFirst = true;
            var code = new StringBuilder();
            var code2 = new StringBuilder();
            EntityStruct(Entity, code, code2, ref isFirst);
            return $@"
        {code2}

        /// <summary>
        /// 实体结构
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public override EntitySturct __Struct
        {{
            get
            {{
                return __struct;
            }}
        }}

        /// <summary>
        /// 实体结构
        /// </summary>
        [IgnoreDataMember]
        static readonly EntitySturct __struct = new EntitySturct
        {{
            EntityName = ""{Entity.Name}"",
            PrimaryKey = ""{Entity.PrimaryColumn.Name}"",
            EntityType = 0x{Entity.Identity:X},
            Properties = new Dictionary<int, PropertySturct>
            {{{code}
            }}
        }};
";
        }

        private void EntityStruct(EntityConfig table, StringBuilder code, StringBuilder code2, ref bool isFirst)
        {
            if (table == null)
                return;
            if (!string.IsNullOrEmpty(table.ModelBase))
                EntityStruct(Project.Entities.FirstOrDefault(p => p.Name == table.ModelBase), code, code2, ref isFirst);

            foreach (PropertyConfig property in table.PublishProperty)
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');

                code.AppendFormat(@"
                {{
                    Real_{0},
                    new PropertySturct
                    {{
                        Index = Index_{0},
                        Name = ""{0}"",
                        Title = ""{5}"",
                        ColumnName = ""{4}"",
                        PropertyType = typeof({1}),
                        CanNull = {2},
                        ValueType = PropertyValueType.{3},
                        CanImport = {6},
                        CanExport = {7}
                    }}
                }}", property.Name
                    , property.CustomType ?? property.CsType
                    , property.Nullable ? "true" : "false"
                    , CsharpHelper.PropertyValueType(property)
                    , property.ColumnName
                    , property.Caption
                    , property["CanImport"] == "1" ? "true" : "false"
                    , property["CanExport"] == "1" ? "true" : "false");
            }
            code2.Clear();
            foreach (PropertyConfig property in table.PublishProperty)
            {
                code2.AppendFormat(@"
        public const byte Index_{0} = {1};", property.Name, property.Index);
            }

        }

        #endregion
    }
}