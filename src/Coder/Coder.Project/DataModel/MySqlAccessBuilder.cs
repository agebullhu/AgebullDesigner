using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config.Mysql;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder.DataBase.MySql;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class MySqlAccessBuilder<TModel> : AccessBuilderBase<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_MySqlAccess";

        private string Code()
        {
            return $@"#region
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;
using Agebull.EntityModel.{Project.DbType};
{Project.UsingNameSpaces}
using static {Project.NameSpace}.DataAccess.{Project.DataBaseObjectName};

#endregion
namespace {Project.NameSpace}.DataAccess
{{
    /// <summary>
    /// {Model.Description}
    /// </summary>
    public sealed class {Model.EntityName}DataOperator : IDataOperator<{Model.EntityName}> , IEntityOperator<{Model.EntityName}>
    {{
        #region 基本信息

        /// <summary>
        /// 驱动提供者信息
        /// </summary>
        public IDataAccessProvider<{Model.EntityName}> Provider {{ get; set; }}

        #endregion

        #region 配置信息

        /// <summary>
        /// 配置信息
        /// </summary>
        public static DataAccessOption GetOption() => new DataAccessOption(TableOption);

        /// <summary>
        /// 实体结构
        /// </summary>
        readonly static EntityStruct Struct;

        /// <summary>
        /// 配置信息
        /// </summary>
        readonly static DataTableOption TableOption;

        static {Model.EntityName}DataOperator()
        {{
            Struct = new EntityStruct
            {{
                Name             = {Model.Entity.Name}_Struct_.name,
                Caption          = {Model.Entity.Name}_Struct_.caption,
                Description      = {Model.Entity.Name}_Struct_.description,
                ProjectName      = ""{Project.Name}"",
                EntityName       = ""{Model.EntityName}"",
                ReadTableName    = {Model.Entity.Name}_Struct_.tableName,
                WriteTableName   = {Model.Entity.Name}_Struct_.tableName,
                PrimaryProperty  = {Model.Entity.Name}_Struct_.primaryProperty,
                IsIdentity       = {(Model.PrimaryColumn?.IsIdentity ?? false ? "true" : "false")},{Interfaces(Model)}
                Properties       = new List<EntityProperty>
                {{
                    {EntityStruct()}
                }}
            }};

            TableOption = new DataTableOption
            {{
                IsQuery = false,
                UpdateByMidified = true,
                CanRaiseEvent=true,
                SqlBuilder = new MySqlSqlBuilder<{Model.EntityName}>(),
                DataStruct = Struct,
                ReadTableName = FromSqlCode,
                WriteTableName   = {Model.Entity.Name}_Struct_.tableName,
                LoadFields = LoadFields,
                Having = Having,
                GroupFields = GroupFields,
                UpdateFields = UpdateFields,
                InsertSqlCode = InsertSqlCode,
                DeleteSqlCode    = DeleteSqlCode
            }};
            TableOption.Initiate();
        }}

        #endregion

        #region SQL

        /// <summary>
        /// 读取的字段
        /// </summary>
        public const string LoadFields = @""{SqlMomentCoder.LoadSql(Model.DbFields)}"";

        /// <summary>
        /// 汇总条件
        /// </summary>
        public const string Having = {SqlMomentCoder.HavingSql(Model.DbFields)};

        /// <summary>
        /// 分组字段
        /// </summary>
        public const string GroupFields = {SqlMomentCoder.GroupSql(Model.DbFields)};

        /// <summary>
        /// 读取的字段
        /// </summary>
        public const string FromSqlCode = @""{ReadTableName()}"";

        /// <summary>
        /// 更新的字段
        /// </summary>
        public const string UpdateFields = @""{UpdateFields()}"";

        /// <summary>
        /// 写入的Sql
        /// </summary>
        public const string InsertSqlCode = @""{InsertSql()}"";

        /// <summary>
        /// 删除的Sql
        /// </summary>
        public const string DeleteSqlCode = @""{DeleteSql()}"";

        #endregion

        #region IDataOperator
{GetDbTypeCode()}

{LoadEntityCode()}

{CreateFullSqlParameter()}

        #endregion

        #region IEntityOperator
{GetSetValues()}
        #endregion
    }}
}}";
        }

        string ReadTableName()
        {
            var primary = Model.PrimaryColumn;
            var table = Model.ReadTableName;
            var code = new StringBuilder();
            code.Append(table);
            if (Model is ModelConfig model)
                foreach (var releation in model.Releations.Where(p => p.ModelType == ReleationModelType.ExtensionProperty))
                {
                    var entity = GlobalConfig.GetEntity(releation.ForeignTable);
                    var property = entity.Properties.FirstOrDefault(p => p.Name == releation.ForeignKey);
                    code.AppendLine();
                    code.Append(releation.JoinType == EntityJoinType.Inner ? "INNER JOIN" : "LEFT JOIN");
                    code.Append($"`{entity.ReadTableName}` ON `{table}`.`{primary.DbFieldName}` = `{entity.ReadTableName}`.`{property.DbFieldName}` {releation.Condition}");
                }
            return code.ToString();
        }


        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            if (Model.IsInterface || Model.NoDataBase)
                return;
            var file = Path.Combine(path, $"{Model.EntityName}DataOperator.cs");
            SaveCode(file, Code());
        }

        private string DeleteSql()
        {
            if (Model.Interfaces != null)
            {
                if (Model.Interfaces.Contains("ILogicDeleteData"))
                {
                    var entity = GlobalConfig.GetEntity("ILogicDeleteData");
                    return $"UPDATE `{Model.SaveTableName}` SET `{entity.Properties[0].DbFieldName}`=1 ";
                }
                if (Model.Interfaces.Contains("IStateData"))
                {
                    var entity = GlobalConfig.GetEntity("IStateData");
                    var field = entity.Properties.FirstOrDefault(p => p.Name == "DataState");
                    return $"UPDATE `{Model.SaveTableName}` SET `{field.DbFieldName}`=255 ";
                }
            }
            return $"DELETE FROM `{Model.SaveTableName}`";
        }

        private string InsertSql()
        {
            if (Model.IsQuery)
                return null;
            var sql = new StringBuilder();

            var columns = Model.PublishProperty.Where(p => p.Entity == Model.Entity &&
                    !p.IsIdentity && !p.IsCompute && !p.CustomWrite &&
                    !p.DbInnerField &&
                    !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)
                ).ToArray();
            sql.Append($@"
INSERT INTO `{Model.SaveTableName}`
(");
            var isFirst = true;
            foreach (var property in columns)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sql.Append(",");
                }
                sql.Append($@"
    `{property.DbFieldName}`");
            }
            sql.Append(@"
)
VALUES
(");
            isFirst = true;
            foreach (var property in columns)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sql.Append(",");
                }
                sql.Append($@"
    ?{property.Name}");
            }
            sql.Append(@"
);");
            if (Model.PrimaryColumn.IsIdentity)
            {
                sql.Append(@"
SELECT @@IDENTITY;");
            }
            return sql.ToString();
        }

        private string UpdateFields()
        {
            if (Model.IsQuery)
                return null;
            var isFirst = true;
            var sql = new StringBuilder();
            var columns = Model.PublishProperty.Where(p => p.Entity == Model.Entity &&
                    !p.IsIdentity && !p.IsCompute && !p.CustomWrite &&
                    !p.DbInnerField &&
                    !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)
                ).ToArray();

            foreach (var property in columns)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sql.Append(",");
                }
                sql.Append($@"
       `{property.DbFieldName}` = ?{property.Name}");
            }
            return sql.ToString();
        }

        private string CreateFullSqlParameter()
        {
            var code = new StringBuilder();
            code.Append($@"

        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name=""entity"">实体对象</param>
        /// <param name=""cmd"">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void SetEntityParameter(DbCommand cmd,{Model.EntityName} entity)
        {{");

            foreach (var property in Model.PublishProperty.Where(p => !p.DbInnerField).OrderBy(p => p.Index))
            {
                if (!string.IsNullOrWhiteSpace(property.CustomType))
                {
                    code.Append($@"
            cmd.Parameters.Add(new MySqlParameter(""{property.Name}"", ({property.CsType})entity.{property.Name}));");
                }
                else if (property.CsType.Equals("bool", StringComparison.OrdinalIgnoreCase))
                {
                    code.Append($@"
            cmd.Parameters.Add(new MySqlParameter(""{property.Name}"", entity.{property.Name} ? (byte)1 : (byte)0));");
                }
                else
                {
                    code.Append($@"
            cmd.Parameters.Add(new MySqlParameter(""{property.Name}"", entity.{property.Name}));");
                }
            }
            code.Append(@"
        }");
            return code.ToString();
        }

        private string GetDbTypeCode()
        {
            var code = new StringBuilder();
            code.Append(@"
        /// <summary>
        /// 得到字段的DbType类型
        /// </summary>
        /// <param name=""property"">字段名称</param>
        /// <returns>参数</returns>
        public int GetDbType(string property)
        {
            if(property == null) 
               return (int)MySqlDbType.VarChar;
            switch (property)
            {");

            foreach (var property in Model.DbFields)
            {
                if (property.DbFieldName.ToLower() != property.Name.ToLower())
                    code.Append($@"
                case ""{property.DbFieldName.ToLower()}"":");

                code.Append($@"
                case ""{property.Name.ToLower()}"":
                    return (int)MySqlDbType.{MySqlDataBaseHelper.ToSqlDbType(property.DbType, property.CsType)};");
            }

            code.Append(@"
                default:
                    return (int)MySqlDbType.VarChar;
            }
        }");
            return code.ToString();
        }

        private string LoadEntityCode()
        {
            var code = new StringBuilder();
            code.Append($@"

        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name=""r"">数据读取器</param>
        /// <param name=""entity"">读取数据的实体</param>
        public async Task LoadEntity(DbDataReader r,{Model.EntityName} entity)
        {{
            var reader = r as MySqlDataReader;");
            int idx = 0;
            foreach (var property in Model.DbFields.Where(p => !p.DbInnerField && !p.NoProperty && !p.KeepStorageScreen.HasFlag(StorageScreenType.Read)))
            {
                SqlMomentCoder.FieldReadCode(property, code, idx++);
            }
            code.Append(@"
        }");
            if (!(Model is ModelConfig model))
            {
                return code.ToString();
            }
            var array = model.Releations.Where(p => p.ModelType != ReleationModelType.ExtensionProperty
                        && p.ModelType != ReleationModelType.Custom).ToArray();
            if (array.Length != 0)
            {
                code.Append($@"

        /// <summary>
        /// 载入后
        /// </summary>
        /// <param name=""entity"">读取数据的实体</param>
        public async Task AfterLoad({Model.EntityName} entity)
        {{");

                foreach (var re in array)
                {
                    var e = GlobalConfig.GetEntity(re.ForeignTable);
                    code.Append($@"
            var access{re.Name} = Provider.ServiceProvider.CreateDataQuery<{e.EntityName}>();");
                    if (re.ModelType == ReleationModelType.Children)
                        code.Append($@"
            entity.{re.Name} = await access{re.Name}.AllAsync(p=>p.{re.ForeignKey} == entity.{re.PrimaryKey});");
                    else
                        code.Append($@"
            entity.{re.Name} = await access{re.Name}.FirstAsync(p=>p.{re.ForeignKey} == entity.{re.PrimaryKey});");
                }
                code.Append(@"
        }");
            }
            if (Model.IsQuery || model.Releations.Count == 0)
                return code.ToString();

            code.Append($@"

        /// <summary>
        ///     实体保存完成后期处理(Insert/Update/Delete)
        /// </summary>
        /// <param name=""entity"">实体</param>
        /// <param name=""operatorType"">操作类型</param>
        /// <remarks>
        ///     对当前对象的属性的更改,请自行保存,否则将丢失
        /// </remarks>
        public async Task AfterSave({Model.EntityName} entity, DataOperatorType operatorType)
        {{");
            foreach (var re in model.Releations)
            {
                var entity = GlobalConfig.GetEntity(re.ForeignTable);
                code.Append($@"
            var access{re.Name} = Provider.ServiceProvider.CreateDataAccess<{entity.EntityName}>();");

                if (re.ModelType == ReleationModelType.EntityProperty)
                    code.Append($@"
            if (entity.{entity.Name} == null || operatorType == DataOperatorType.Delete)
            {{
                await access{re.Name}.DeleteAsync(p => p.{re.ForeignKey} == entity.{re.PrimaryKey});
            }}
            else
            {{
                entity.{entity.Name}.{re.ForeignKey} = entity.{re.PrimaryKey};
                if (await access{re.Name}.AnyAsync(p => p.{re.ForeignKey} == entity.{re.PrimaryKey}))
                {{
                    await access{re.Name}.UpdateAsync(entity.entity.{entity.Name});
                }}
                else
                {{
                    await access{re.Name}.InsertAsync(entity.entity.{entity.Name});
                }}
            }}");
                else if (re.ModelType == ReleationModelType.Children)
                    code.Append($@"
            if (entity.{entity.Name} == null || operatorType == DataOperatorType.Delete)
            {{
                await access{re.Name}.DeleteAsync(p => p.{re.ForeignKey} == entity.{re.PrimaryKey});
            }}
            else
            {{
                foreach(var ch in entity.{entity.Name})
                {{
                    ch.{re.ForeignKey} = entity.{re.PrimaryKey};
                    if (await access{re.Name}.ExistPrimaryKeyAsync(ch.{entity.PrimaryColumn.Name}))
                        await access{re.Name}.UpdateAsync(ch);
                    else
                        await access{re.Name}.InsertAsync(ch);
                }}
            }}");
                else
                {
                    code.Append($@"
            {{  
                var ch = new {entity.EntityName}
                {{");
                    bool first = true;
                    foreach (var pro in model.PublishProperty.Where(p => p.Entity == entity))
                    {
                        if (first)
                            first = false;
                        else
                            code.Append(',');
                        code.Append($@"
                    {pro.Field.Name} = entity.{pro.Name}");
                    }
                    code.Append($@"
                }};
                ch.{re.ForeignKey} = entity.{re.PrimaryKey};
                var (hase,id) = await access{re.Name}.LoadValueAsync(p=> p.{entity.PrimaryColumn.Name} , p=>  p.{re.ForeignKey} == entity.{re.PrimaryKey});
                ch.{entity.PrimaryColumn.Name} = id;
                if (hase)
                    await access{re.Name}.UpdateAsync(ch);
                else
                    await access{re.Name}.InsertAsync(ch);
            }}");
                }
            }
            code.Append(@"
        }");
            return code.ToString();
        }

        #region 数据结构

        static string Interfaces(IEntityConfig entity)
        {
            var it = new StringBuilder();
            bool first = true;
            foreach (var i in entity.Interfaces?.Split(',', System.StringSplitOptions.RemoveEmptyEntries))
            {
                if (first)
                {
                    first = false;
                    it.Append(@"
                InterfaceFeature = new HashSet<string>{");
                }
                else
                    it.Append(',');
                it.Append($"\"{i}\"");
            }
            if (first)
                return null;
            it.Append("},");
            return it.ToString(); ;
        }


        private string EntityStruct()
        {
            var properties = new List<string>();
            var idx = 0;
            EntityStruct(Model, properties, ref idx);
            return string.Join(@",
                    ", properties);
        }

        private void EntityStruct(IEntityConfig model, List<string> properties, ref int idx)
        {
            //if (!string.IsNullOrWhiteSpace(Model.ModelBase))
            //{
            //    var modelBase = GlobalConfig.GetModel(p => p.Name == Model.ModelBase);
            //    EntityStruct(modelBase, properties, ref idx);
            //}
            var code = new StringBuilder();
            var primary = model.PrimaryColumn;
            var last = model.LastProperties.Where(p => p != primary && !p.NoStorage);
            properties.Add(DataBaseBuilder.EntityProperty(code, primary, ref idx, false));
            foreach (var property in last.Where(p => !p.IsInterfaceField).OrderBy(p => p.Index))
            {
                properties.Add(DataBaseBuilder.EntityProperty(code, property, ref idx, false));
            }
            foreach (var property in last.Where(p => p.IsInterfaceField))
            {
                properties.Add(DataBaseBuilder.EntityProperty(code, property, ref idx, false));
            }
        }

        #endregion

        #region 名称值取置

        /// <summary>
        /// </summary>
        /// <remarks></remarks>
        /// <returns></returns>
        private string GetSetValues()
        {
            var code = new StringBuilder();

            code.Append($@"

        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name=""entity""></param>
        /// <param name=""property""></param>
        object IEntityOperator<{Model.EntityName}>.GetValue({Model.EntityName} entity, string property)
        {{
            if (property == null) return null;
            return (property.Trim().ToLower()) switch
            {{");

            foreach (var property in Model.PublishProperty.Where(p => p.CanGet))
            {
                var names = property.GetAliasPropertys().Select(p => p.ToLower()).ToList();
                var name = property.Name.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                name = property.DbFieldName.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                foreach (var alias in names)
                    code.Append($@"
                ""{alias}"" => entity.{property.Name},");

            }
            code.AppendLine(@"
                _ => null
            };
        }");

            code.Append($@"    

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name=""entity""></param>
        /// <param name=""property""></param>
        /// <param name=""value""></param>
        void IEntityOperator<{Model.EntityName}>.SetValue({Model.EntityName} entity, string property, object value)
        {{
            if(property == null)
                return;
            switch(property.Trim().ToLower())
            {{");

            foreach (var property in Model.PublishProperty.Where(p => p.CanSet))
            {
                var names = property.GetAliasPropertys().Select(p => p.ToLower()).ToList();

                var varName = $"tmp{property.Name}_";

                var name = property.Name.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                name = property.DbFieldName.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                foreach (var alia in names)
                    code.Append($@"
            case ""{alia}"":");

                code.Append($@"
                if (value == null)
                     entity.{property.Name} = default;
                else if(value is {property.LastCsType} {varName})
                    entity.{property.Name} = {varName};");

                switch (property.CsType)
                {
                    case "string":
                    case "String":
                        code.Append($@"
                else
                    entity.{property.Name} = value.ToString();
                return;");
                        continue;
                    case "bool":
                    case "Boolean":
                        code.Append($@"
                else if(value is int i{property.Name})
                    entity.{property.Name} = i{property.Name} != 0;
                else if (int.TryParse(value.ToString(), out var { name}_vl))
                    entity.{ property.Name} = { name}_vl != 0; ");
                        break;
                }

                if (!string.IsNullOrWhiteSpace(property.CustomType))
                {
                    code.Append($@"
                else if(value is int i{property.Name})
                    entity.{property.Name} = ({property.CustomType})i{property.Name};
                else if (int.TryParse(value.ToString(), out i{property.Name}))
                    entity.{property.Name} = ({property.CustomType})i{property.Name};");
                }
                code.AppendLine($@"
                else if ({property.LastCsType}.TryParse(value.ToString(), out {varName}))
                    entity.{property.Name} = {varName};
                else
                    entity.{property.Name} = default;
                return;");
            }
            code.AppendLine(@"
            }
        }");

            return code.ToString();
        }

        #endregion
    }
}