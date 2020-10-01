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
{Project.UsingNameSpaces}

#endregion
namespace {SolutionConfig.Current.NameSpace}.DataAccess
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
        public DataAccessProvider<{Model.EntityName}> Provider {{ get; set; }}

        static EntityStruct _struct;

        /// <summary>
        /// 实体结构
        /// </summary>
        public static EntityStruct Struct => _struct ??= new EntityStruct
        {{
            IsIdentity       = {(Model.PrimaryColumn?.IsIdentity ?? false ? "true" : "false")},
            EntityName       = {Project.DataBaseObjectName}.{Model.Entity.Name}_Struct_.EntityName,
            Caption          = {Project.DataBaseObjectName}.{Model.Entity.Name}_Struct_.Caption,
            Description      = {Project.DataBaseObjectName}.{Model.Entity.Name}_Struct_.Description,
            PrimaryKey       = {Project.DataBaseObjectName}.{Model.Entity.Name}_Struct_.PrimaryKey,
            ReadTableName    = {Project.DataBaseObjectName}.{Model.Entity.Name}_Struct_.TableName,
            WriteTableName   = {Project.DataBaseObjectName}.{Model.Entity.Name}_Struct_.TableName,
            Properties       = new List<EntityProperty>
            {{
                {EntityStruct()}
            }}
        }};

        /// <summary>
        /// 配置信息
        /// </summary>
        internal static DataAccessOption Option = new DataAccessOption
        {{
            NoInjection      = true,
            IsQuery          = {(Model.IsQuery ? "true" : "false")},
            UpdateByMidified = {(Model.UpdateByModified ? "true" : "false")},
            ReadTableName    = FromSqlCode,
            WriteTableName   = ""{Model.SaveTableName}"",
            LoadFields       = LoadFields,
            UpdateFields     = UpdateFields,
            InsertSqlCode    = InsertSqlCode,
            DataSturct       = Struct
        }};

        #endregion

        #region SQL

        /// <summary>
        /// 读取的字段
        /// </summary>
        public const string FromSqlCode = @""{ReadTableName()}"";

        /// <summary>
        /// 读取的字段
        /// </summary>
        public const string LoadFields = @""{SqlMomentCoder.LoadSql(PublishDbFields)}"";

        /// <summary>
        /// 更新的字段
        /// </summary>
        public static string UpdateFields = @""{UpdateFields()}"";

        /// <summary>
        /// 写入的Sql
        /// </summary>
        public static string InsertSqlCode => @""{InsertSql()}"";

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
            var file = Path.Combine(path, $"{Model.EntityName}DataOperator.cs");
            SaveCode(file, Code());
        }

        private string InsertSql()
        {
            if (Model.IsQuery)
                return null;
            var sql = new StringBuilder();
            var columns = Model.DbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)).ToArray();
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
            return sql.ToString();
        }

        private string UpdateFields()
        {
            if (Model.IsQuery)
                return null;
            var sql = new StringBuilder();
            var columns = PublishDbFields.Where(p => p.Entity == Model.Entity && !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
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
        public void SetEntityParameter({Model.EntityName} entity, MySqlCommand cmd)
        {{");

            foreach (var property in PublishDbFields.OrderBy(p => p.Index))
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
            foreach (var property in PublishDbFields)
            {
                if (property.DbFieldName.ToLower() != property.Name.ToLower())
                    code.Append($@"
                case ""{property.DbFieldName.ToLower()}"":");

                code.Append($@"
                case ""{property.Name.ToLower()}"":
                    return (int)MySqlDbType.{MySqlHelper.ToSqlDbType(property.DbType, property.CsType)};");
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
            foreach (var property in PublishDbFields)
            {
                SqlMomentCoder.FieldReadCode(property, code, idx++);
            }
            code.Append(@"
        }");
            if (Model is ModelConfig model)
            {
                var array = model.Releations.Where(p => p.ModelType != ReleationModelType.ExtensionProperty).ToArray();
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
            var access{re.Name} = Provider.ServiceProvider.CreateDataQuery<{e.Name}>();");
                        if (re.ModelType == ReleationModelType.Children)
                            code.Append($@"
            entity.{re.Name} = await access{re.Name}.LoadByForeignKeyAsync(nameof({e.Name}.{re.ForeignKey}), entity.{re.PrimaryKey});");
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
                        foreach (var pro in model.Properties.Where(p => p.Entity == entity))
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
            }
            return code.ToString();
        }

        #region 数据结构


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
            if (!string.IsNullOrWhiteSpace(Model.ModelBase))
            {
                var modelBase = GlobalConfig.GetModel(p => p.Name == Model.ModelBase);
                EntityStruct(modelBase, properties, ref idx);
            }
            var primary = model.LastProperties.FirstOrDefault(p => p.Entity == model.Entity && p.IsPrimaryKey);

            EntityStruct(properties, ref idx, primary);

            foreach (var property in model.LastProperties.Where(p => p != model.PrimaryColumn).OrderBy(p => p.Index))
            {
                EntityStruct(properties, ref idx, property);
            }
        }

        private void EntityStruct(List<string> properties, ref int idx, IFieldConfig property)
        {
            if (property == null)
                return;

            var str = new StringBuilder("new EntityProperty(");

            if (property.IsInterfaceField)
            {
                str.Append($"DataInterface.{property.LinkTable}.{property.LinkField}");
            }
            else
            {
                str.Append($"{Project.DataBaseObjectName}.{property.Entity.Name}_Struct_.{property.Field.Name}");
            }
            str.Append($",{++idx},\"{property.Name}\",\"{property.DbFieldName}\")");
            properties.Add(str.ToString());
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

            foreach (var property in Model.LastProperties.Where(p => p.CanGet))
            {
                var names = property.GetAliasPropertys().Select(p => p.ToLower()).ToList();
                var name = property.Name.ToLower();
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

            foreach (var property in Model.LastProperties.Where(p => p.CanSet))
            {

                var names = property.GetAliasPropertys().Select(p => p.ToLower()).ToList();
                var name = property.Name.ToLower();
                if (!names.Contains(name))
                    names.Add(name);
                foreach (var alia in names)
                    code.Append($@"
            case ""{alia}"":");

                if (!string.IsNullOrWhiteSpace(property.CustomType))
                {
                    code.Append($@"
                if (value != null)
                {{
                    if(value is int)
                    {{
                        entity.{property.Name} = ({property.CustomType})(int)value;
                    }}
                    else if(value is {property.CustomType})
                    {{
                        entity.{property.Name} = ({property.CustomType})value;
                    }}
                    else
                    {{
                        var str = value.ToString();
                        {property.CustomType} val;
                        if ({property.CustomType}.TryParse(str, out val))
                        {{
                            entity.{property.Name} = val;
                        }}
                        else
                        {{
                            int vl;
                            if (int.TryParse(str, out vl))
                            {{
                                entity.{property.Name} = ({property.CustomType})vl;
                            }}
                        }}
                    }}
                }}
                return;");
                    continue;
                }

                switch (property.CsType)
                {
                    case "bool":
                    case "Boolean":
                        code.Append($@"
                if (value != null)
                {{
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {{
                        entity.{property.Name} = vl != 0;
                    }}
                    else
                    {{
                        entity.{property.Name} = Convert.ToBoolean(value);
                    }}
                }}
                return;");
                        continue;
                    case "int":
                    case "long":
                        code.Append($@"
                entity.{property.Name} = ({property.CsType})Convert.ToDecimal(value);
                return;");
                        break;
                    default:
                        code.Append($@"
                entity.{property.Name} = {ConvertCode(property, "value")};
                return;");
                        break;
                }
            }
            code.AppendLine(@"
            }
        }");

            return code.ToString();
        }


        private string ConvertCode(IFieldConfig column, string arg)
        {
            switch (column.CsType)
            {
                case "string":
                case "String":
                    return $"{arg} == null ? null : {arg}.ToString()";
                case "long":
                case "Int64":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (long?)Convert.ToInt64({arg})";
                    return $"Convert.ToInt64({arg})";
                case "int":
                case "Int32":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (int?)Convert.ToInt32({arg})";
                    return $"Convert.ToInt32({arg})";
                case "decimal":
                case "Decimal":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (decimal?)Convert.ToDecimal({arg})";
                    return $"Convert.ToDecimal({arg})";
                case "float":
                case "Float":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (float?)Convert.ToSingle({arg})";
                    return $"Convert.ToSingle({arg})";
                case "bool":
                case "Boolean":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (bool?)Convert.ToBoolean({arg})";
                    return $"Convert.ToBoolean({arg})";
                case "DateTime":
                    if (column.Nullable)
                        return $"{arg} == null ? null : (DateTime?)Convert.ToDateTime({arg})";
                    return $"Convert.ToDateTime({arg})";
            }
            return $"({column.LastCsType}){arg}";
        }

        #endregion
    }
}