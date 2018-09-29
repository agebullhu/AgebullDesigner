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
            var codeConst = new StringBuilder();
            var codeStruct = new StringBuilder();
            EntityStruct(Entity, codeStruct, codeConst, ref isFirst);
            return $@"
        #region 数据结构
        
        {codeConst}

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
        public static readonly EntitySturct __struct = new EntitySturct
        {{
            EntityName = ""{Entity.Name}"",
            Caption=@""{Entity.Caption}"",
            Description=@""{Entity.Description}"",
            PrimaryKey = ""{Entity.PrimaryColumn.Name}"",
            EntityType = 0x{Entity.Identity:X},
            Properties = new Dictionary<int, PropertySturct>
            {{{codeStruct}
            }}
        }};
        #endregion
";
        }

        private void EntityStruct(EntityConfig table, StringBuilder codeStruct, StringBuilder codeConst, ref bool isFirst)
        {
            if (table == null)
                return;
            if (!string.IsNullOrEmpty(table.ModelBase))
                EntityStruct(Project.Entities.FirstOrDefault(p => p.Name == table.ModelBase), codeStruct, codeConst, ref isFirst);

            foreach (PropertyConfig property in table.PublishProperty)
            {
                if (isFirst)
                    isFirst = false;
                else
                    codeStruct.Append(',');

                codeStruct.Append($@"
                {{
                    Real_{property.Name},
                    new PropertySturct
                    {{
                        Index = Index_{property.Name},
                        Name = ""{property.Name}"",
                        Title = ""{property.Caption}"",
                        Caption=@""{property.Caption}"",
                        Description=@""{property.Description}"",
                        ColumnName = ""{property.ColumnName}"",
                        PropertyType = typeof({property.CustomType ?? property.CsType}),
                        CanNull = {(property.Nullable ? "true" : "false")},
                        ValueType = PropertyValueType.{CsharpHelper.PropertyValueType(property)},
                        CanImport = {(property.ExtendConfigListBool["easyui","CanImport"] ? "true" : "false")},
                        CanExport = {(property.ExtendConfigListBool["easyui", "CanExport"] ? "true" : "false")}
                    }}
                }}");
            }
            codeConst.Clear();
            foreach (PropertyConfig property in table.PublishProperty)
            {
                codeConst.Append($@"

        /// <summary>
        /// {property.Caption}的数字标识
        /// </summary>
        public const byte Index_{property.Name} = {property.Index};");
            }

        }

        #endregion
    }
}