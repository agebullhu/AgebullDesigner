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

        /// <summary>
        /// 实体结构
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public override EntitySturct __Struct
        {{
            get
            {{
                return _DataStruct_.Struct;
            }}
        }}
        /// <summary>
        /// 实体结构
        /// </summary>
        public class _DataStruct_
        {{
            /// <summary>
            /// 实体名称
            /// </summary>
            public const string EntityName = @""{Entity.Name}"";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @""{Entity.Caption}"";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @""{Entity.Description}"";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x{Entity.Identity:X};
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = ""{Entity.PrimaryColumn.Name}"";
            
            {codeConst}

            /// <summary>
            /// 实体结构
            /// </summary>
            public static readonly EntitySturct Struct = new EntitySturct
            {{
                EntityName = EntityName,
                Caption    = EntityCaption,
                Description= EntityDescription,
                PrimaryKey = EntityPrimaryKey,
                EntityType = EntityIdentity,
                Properties = new Dictionary<int, PropertySturct>
                {{{codeStruct}
                }}
            }};
        }}
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
                            Index        = {property.Name},
                            Name         = ""{property.Name}"",
                            Title        = ""{property.Caption}"",
                            Caption      = @""{property.Caption}"",
                            Description  = @""{property.Description}"",
                            ColumnName   = ""{property.DbFieldName}"",
                            PropertyType = typeof({property.CustomType ?? property.CsType}),
                            CanNull      = {(property.Nullable ? "true" : "false")},
                            ValueType    = PropertyValueType.{CsharpHelper.PropertyValueType(property)},
                            CanImport    = {(property.ExtendConfigListBool["easyui", "CanImport"] ? "true" : "false")},
                            CanExport    = {(property.ExtendConfigListBool["easyui", "CanExport"] ? "true" : "false")}
                        }}
                    }}");
            }
            codeConst.Clear();
            int idx = 0;
            if (table.PrimaryColumn != null)
            {
                codeConst.Append($@"
            /// <summary>
            /// {ToRemString(table.PrimaryColumn.Caption)}的数字标识
            /// </summary>
            public const byte {table.PrimaryColumn.Name} = {table.PrimaryColumn.Identity};
            
            /// <summary>
            /// {ToRemString(table.PrimaryColumn.Caption)}的实时记录顺序
            /// </summary>
            public const int Real_{table.PrimaryColumn.Name} = {idx++};");
            }

            foreach (PropertyConfig property in table.PublishProperty.Where(p => p != table.PrimaryColumn).OrderBy(p => p.Index))
            {
                codeConst.Append($@"

            /// <summary>
            /// {ToRemString(property.Caption)}的数字标识
            /// </summary>
            public const byte {property.Name} = {property.Identity};
            
            /// <summary>
            /// {ToRemString(property.Caption)}的实时记录顺序
            /// </summary>
            public const int Real_{property.Name} = {idx++};");
            }

        }

        #endregion
    }
}