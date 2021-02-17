using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.Mysql;
using Agebull.EntityModel.Config.SqlServer;
using Agebull.EntityModel.Config.V2021;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class EntityStructBuilder : ModelBuilderBase
    {
        #region 基础

        /// <inheritdoc />
        public override string BaseCode => EntityStruct();

        /// <inheritdoc />
        protected override string Folder => "Struct";

        int DbType(DataBaseFieldConfig field)
        {
            if (Project.DbType == DataBaseType.SqlServer)
                return (int)SqlServerHelper.ToSqlDbType(field.FieldType, field.Property.CsType);
            return (int)MySqlDataBaseHelper.ToSqlDbType(field.FieldType, field.Property.CsType);
        }
        #endregion

        #region 数据结构

        private string EntityStruct()
        {
            if (Model.PrimaryColumn == null)
                return null;
            bool isFirst = true;
            int idx = 0;
            var codeConst = new StringBuilder();
            var codeStruct = new StringBuilder();
            EntityStruct(Model, codeStruct, codeConst, ref isFirst, ref idx);
            return $@"
        #region 数据结构

        /// <summary>
        /// 实体结构
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public override EntityStruct __Struct
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
            public const string EntityName = @""{Model.Name}"";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @""{Model.Caption}"";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @""{Model.Description}"";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x{Model.Identity:X};
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = ""{Model.PrimaryColumn.Name}"";
            
            {codeConst}

            /// <summary>
            /// 实体结构
            /// </summary>
            public static EntityStruct Struct = new EntityStruct
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

        private void EntityStruct(IEntityConfig entity, StringBuilder codeStruct, StringBuilder codeConst, ref bool isFirst, ref int idx)
        {
            if (entity == null || entity.DataTable == null)
                return;
            if (!string.IsNullOrWhiteSpace(entity.ModelBase))
                EntityStruct(Project.Models.FirstOrDefault(p => p.Name == entity.ModelBase), codeStruct, codeConst, ref isFirst, ref idx);

            var primary = entity.PrimaryColumn;
            if (primary != null)
            {
                codeConst.Append(PropertyIndex(primary, ref idx));
                PropertyStruct(codeStruct, primary, ref isFirst);
            }

            foreach (var property in entity.WhereLast(p => p != primary).OrderBy(p => p.Index))
            {
                codeConst.Append(PropertyIndex(property, ref idx));
                PropertyStruct(codeStruct, property, ref isFirst);
            }
            /*
            foreach (var property in entity.WhereLast(p => p != entity.PrimaryColumn && !entity.PublishProperty.Any(pp => p == pp)).OrderBy(p => p.Index))
            {
                codeConst.Append(PropertyIndex(property, ref idx));
                PropertyStruct(codeStruct, property, ref isFirst);
            }*/
        }

        private string PropertyIndex(IPropertyConfig property, ref int idx)
        {
            return $@"

            /// <summary>
            /// {property.Caption.ToRemString()}的数字标识
            /// </summary>
            public const int {property.Name} = {property.Identity};
            
            /// <summary>
            /// {property.Caption.ToRemString()}的实时记录顺序
            /// </summary>
            public const int Real_{property.Name} = {idx++};";
        }

        private void PropertyStruct(StringBuilder codeStruct, IPropertyConfig property, ref bool isFirst)
        {
            if (isFirst)
                isFirst = false;
            else
                codeStruct.Append(',');

            var featrue = new List<string>();

            var field = property.DataBaseField;
            if (!property.NoStorage)
            {
                featrue.Add("PropertyFeatrue.DbCloumn");
                if (!field.DbInnerField)
                {
                    if (property.IsInterfaceField)
                    {
                        featrue.Add("PropertyFeatrue.Interface");
                        if (!property.NoProperty)
                            featrue.Add("PropertyFeatrue.Property");
                    }
                    else
                    {
                        featrue.Add("PropertyFeatrue.Property");
                    }
                }
            }

            codeStruct.Append($@"
                    {{
                        Real_{property.Name},
                        new PropertySturct
                        {{
                            Index        = {property.Name},
                            Featrue      = {(featrue.Count == 0 ? "PropertyFeatrue.None" : string.Join(" | ", featrue))},
                            Link         = ""{field?.LinkField}"",
                            Name         = ""{property.Name}"",
                            Caption      = @""{property.Caption}"",
                            JsonName     = ""{property.JsonName}"",
                            ColumnName   = ""{field?.DbFieldName}"",
                            PropertyType = typeof({property.CustomType ?? property.CsType}),
                            CanNull      = {(property.Nullable ? "true" : "false")},
                            ValueType    = PropertyValueType.{CsharpHelper.PropertyValueType(property)},
                            DbType       = {DbType(field)},
                            CanImport    = {(property.ExtendConfigListBool["easyui", "CanImport"] ? "true" : "false")},
                            CanExport    = {(property.ExtendConfigListBool["easyui", "CanExport"] ? "true" : "false")},
                            Description  = @""{property.Description}""
                        }}
                    }}");
        }

        #endregion
    }
}