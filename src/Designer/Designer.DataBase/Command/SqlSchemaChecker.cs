using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.SqlServer;
using Agebull.EntityModel.Config.V2021;
using Agebull.EntityModel.RobotCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.Designer
{
    public class SqlSchemaChecker : CoderBase
    {
        #region 数据迁移

        public static string MoveData(EntityConfig entity)
        {
            var code = new StringBuilder();
            var pri = entity.DataTable.PrimaryField;
            code.AppendFormat(@"INSERT INTO [dbo].[{0}]({1}", entity.DataTable.ReadTableName, pri.DbFieldName);
            foreach (var field in entity.DataTable.Fields) //.Where(p => p.DbIndex > 0)
            {
                if (field.IsPrimaryKey)
                    continue;
                code.AppendFormat(@",[{0}]", field.DbFieldName);
            }

            code.AppendFormat(@")
SELECT {0}", pri.DbFieldName);
            foreach (var field in entity.DataTable.Fields) //.Where(p => p.DbIndex > 0)
            {
                if (field.IsPrimaryKey)
                    continue;
                code.AppendFormat(@",[{0}]", field.DbFieldName);
            }

            code.AppendFormat(@" FROM {0}_OLD;", entity.DataTable.ReadTableName);
            return code.ToString();
        }

        #endregion

        #region 数据结构读取

        private SqlParameter parameter;

        public ProjectConfig Project { get; set; }

        public EntityConfig Entity { get; set; }

        public void ImportProject()
        {
            var csb = new SqlConnectionStringBuilder
            {
                InitialCatalog = Project.DbSoruce,
                UserID = Project.DbUser,
                Password = Project.DbPassWord,
                DataSource = Project.DbHost
            };
            try
            {
                using var connection = new SqlConnection(csb.ConnectionString);
                connection.Open();
                var tables = new Dictionary<string, string>();
                using (var cmd = new SqlCommand(table_sql, connection))
                {
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        if (tables.ContainsKey(name))
                            continue;
                        //var idx = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                        var des = reader.IsDBNull(1) ? null : reader.GetString(1);
                        tables.Add(name, des);
                    }
                }
                foreach (var table in tables)
                    CheckColumns(connection, table.Key, table.Value);
                connection.Close();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
        }

        private void CheckColumns(SqlConnection connection, string table, string description)
        {
            TraceMessage.DefaultTrace.Track = table;
            using var cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Add(parameter = new SqlParameter("@entity", SqlDbType.NVarChar, 256) { Value = table });
            var ch = GlobalConfig.SplitWords(table);
            if (ch[0].Equals("tb", StringComparison.OrdinalIgnoreCase))
                ch.RemoveAt(0);
            var name = GlobalConfig.ToName(ch);
            var entity = GlobalConfig.Find(name);
            if (entity == null)
                return;
            foreach (var field in entity.Properties)
            {
                field.NoStorage = true;
            }

            entity.DataTable.ReadTableName = entity.DataTable.SaveTableName = table;
            CheckColumns(cmd, entity);
        }

        private void CheckColumns(SqlCommand cmd, IEntityConfig entity)
        {
            entity.DataTable ??= new DataTableConfig();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var name = reader.GetString(0);
                var field = entity.DataTable.Find(name);
                var property = field?.Property;
                if (field == null)
                {
                    Trace.WriteLine($" ++++ {field}");
                    property = new FieldConfig
                    {
                        Name = name,
                        Entity = Entity
                    };
                    entity.Entity.Add(property as FieldConfig);//触发器会自动加入数据库字段
                    field = property.DataBaseField;
                    if (!reader.IsDBNull(7))
                        field.Caption = field.Description = reader.GetString(7);
                }
                else
                {
                    Trace.WriteLine($" ==== {field.Name}");
                    field.Option.ReferenceKey = null;
                    field.Option.IsLink = false;
                    field.DbFieldName = name;

                }
                field.NoStorage = false;
                field.FieldType = reader.GetString(1);
                field.DbNullable = reader.GetBoolean(5);
                field.Datalen = Convert.ToInt32(reader.GetValue(2));
                field.Scale = Convert.ToInt32(reader.GetValue(3));
                property.IsPrimaryKey = !reader.IsDBNull(8);
                property.DataBaseField = field;
                field.IsIdentity = reader.GetBoolean(9);
                field.IsReadonly = reader.GetBoolean(10);
                if (field.CsType == null)
                    DataTypeHelper.ToStandardByDbType(field, field.FieldType);

                if (field.CsType != null && field.CsType.Equals("string", StringComparison.OrdinalIgnoreCase))
                {
                    field.Datalen = Convert.ToInt32(reader.GetValue(4));
                    field.Scale = 0;
                }
            }
        }

        #region SQL

        private const string table_sql =
            @"SELECT [Tables].name as [Name],[Properties].value AS [Description],[Properties].minor_id
FROM sys.tables [Tables]
LEFT OUTER JOIN sys.extended_properties AS [Properties] ON [Properties].major_id = [Tables].object_id AND [Properties].name = 'MS_Description'
WHERE  [Tables].[type]='U'
ORDER BY [Properties].minor_id;";

        private const string sql = @"
SELECT  [ColumnName] = [Columns].name ,
        [SystemTypeName] = [Types].name ,
        [Precision] = [Columns].precision ,
        [Scale] = [Columns].scale ,
        [Lenght] = [Columns].max_length ,
        [IsNullable] = [Columns].is_nullable ,
        [ColumnId] = [Columns].column_id,
        [Description] = [Properties].value,
		[PkName] = [PK].COLUMN_NAME	,
        [IsIdentity] = [Columns].is_identity,
        [IsComputed] = [Columns].is_computed,
        [IsRowGUIDCol] = [Columns].is_rowguidcol
FROM    sys.tables AS [Tables]
        INNER JOIN sys.columns AS [Columns] ON [Tables].object_id = [Columns].object_id
		LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE  [PK] ON [PK].TABLE_NAME=[Tables].[name] 
		                                                   AND [PK].COLUMN_NAME=[Columns].[name]
        INNER JOIN sys.types AS [Types] ON [Columns].system_type_id = [Types].system_type_id  
                                       AND is_user_defined = 0
                                       AND [Types].name <> 'sysname'
        LEFT OUTER JOIN sys.extended_properties AS [Properties] ON [Properties].major_id = [Tables].object_id
                                                               AND [Properties].minor_id = [Columns].column_id
                                                               AND [Properties].name = 'MS_Description'

WHERE [Tables].name = @entity
ORDER BY [Tables].object_id, [Columns].column_id";

        #endregion

        #endregion

        #region 数据类型检查

        public void IntCheck()
        {
            foreach (var schema in Project.Entities)
            {
                foreach (var field in schema.DataTable.Fields)
                {
                    if (field.CsType == "string")
                        field.DbNullable = true;
                }
            }

            var csb = new SqlConnectionStringBuilder
            {
                InitialCatalog = Project.DbHost,
                UserID = Project.DbUser,
                Password = Project.DbPassWord,
                DataSource = Project.DbSoruce
            };
            using var connection = new SqlConnection(csb.ConnectionString);
            connection.Open();
            foreach (var schema in Project.Entities)
            {
                TraceMessage.DefaultTrace.Message2 = schema.DataTable.ReadTableName;
                foreach (var property in schema.Properties)
                {
                    var field = property.DataBaseField;
                    TraceMessage.DefaultTrace.Message3 = field.DbFieldName;

                    var checkInt = true;
                    var isInt = true;
                    switch (property.CsType.ToLower())
                    {
                        case "guid":
                        case "datetime":
                        case "string":
                        case "bool":
                        case "boolean":
                            isInt = false;
                            checkInt = false;
                            break;
                    }

                    var sql1 = $"SELECT [{field.DbFieldName}] FROM [{schema.DataTable.ReadTableName}]";
                    using (var cmd = new SqlCommand(sql1, connection))
                    {
                        using var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(0)) field.DbNullable = true;
                            if (checkInt)
                            {
                                var obj = reader[0].ToString();
                                TraceMessage.DefaultTrace.Message4 = obj;
                                if (obj.Contains('.') && long.Parse(obj.Split('.')[1]) > 0) isInt = false;
                            }

                            if (!isInt) break;
                        }
                    }

                    if (checkInt && !isInt && (property.CsType != "decimal" || field.FieldType != "decimal"))
                    {
                        TraceMessage.DefaultTrace.Track = "改变字段类型";
                        property.CsType = "decimal";
                        field.FieldType = "decimal";
                    }

                    //if (!field.DbNullable)
                    //{
                    //    if (field.Nullable)
                    //        TraceMessage.DefaultTrace.Track = "字段已改成不为空";
                    //    field.Nullable = false;
                    //}
                    field.FieldType = SqlServerHelper.ToDataBaseType(property);
                }
            }

            CheckColumns(connection);
            connection.Close();
        }

        public void CheckColumns()
        {
            var csb = new SqlConnectionStringBuilder
            {
                InitialCatalog = Project.DbHost,
                UserID = Project.DbUser,
                Password = Project.DbPassWord,
                DataSource = Project.DbSoruce
            };
            using var connection = new SqlConnection(csb.ConnectionString);
            connection.Open();
            CheckColumns(connection);
            connection.Close();
        }

        private void CheckColumns(SqlConnection connection)
        {
            using var cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Add(parameter = new SqlParameter("@entity", SqlDbType.NVarChar, 256));
            foreach (var schema in Project.Entities) CheckColumns(cmd, schema);
        }

        public static string SetPrimary(IEntityConfig entity)
        {
            return string.Format(@"ALTER TABLE dbo.{0} ADD CONSTRAINT
PK_{0} PRIMARY KEY CLUSTERED 
(
	[{1}]
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];",
                entity.DataTable.ReadTableName, entity.PrimaryColumn.DataBaseField.DbFieldName);
        }

        #endregion

        #region 重写数据结构

        /// <summary>
        ///     重写数据结构
        /// </summary>
        /// <param name="host"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <param name="dbName"></param>
        public void AlertTables(string host, string user, string pwd, string dbName)
        {
            TraceMessage.DefaultTrace.Message1 = "重写数据结构";
            var csb = new SqlConnectionStringBuilder
            {
                InitialCatalog = dbName,
                UserID = user,
                Password = pwd,
                DataSource = host
            };
            using var connection = new SqlConnection(csb.ConnectionString);
            connection.Open();
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add(parameter = new SqlParameter("@entity", SqlDbType.NVarChar, 256));
                foreach (var schema in Project.Entities)
                {
                    TraceMessage.DefaultTrace.Message2 = schema.DataTable.ReadTableName;
                    parameter.Value = schema.DataTable.ReadTableName;
                    var sqls = AlertTable(cmd, schema);
                    foreach (var sq in sqls)
                    {
                        TraceMessage.DefaultTrace.Track = sq;
                        using var cmd2 = new SqlCommand(sq, connection);
                        cmd2.ExecuteNonQuery();
                    }
                }
            }

            connection.Close();
        }


        public IEnumerable<string> AlertTable(SqlCommand cmd, EntityConfig entity)
        {
            var sqls = new List<string>();
            /*switch (CheckColumns2(cmd, entity))
            {
                case 0:
                    TraceMessage.DefaultTrace.Message4 = "无改变";
                    break;
                case 1:
                    TraceMessage.DefaultTrace.Message4 = "已改变";
                    sqls.Add(SqlMomentCoder.CreateTableCode(entity, true));
                    sqls.Insert(0, String.Format("EXECUTE sp_rename N'dbo.{0}', N'{0}_Old', 'OBJECT';", entity.ReadTableName));
                    sqls.Add(MoveData(entity));
                    sqls.Add($"DROP TABLE dbo.{entity.ReadTableName}_Old;");
                    sqls.Add(SetPrimary(entity));
                    break;
                case 2:
                    TraceMessage.DefaultTrace.Message4 = "新增加";
                    sqls.Add(SqlMomentCoder.CreateTableCode(entity, true));
                    sqls.Add(SetPrimary(entity));
                    break;
            }*/
            return sqls;
        }

        #endregion
    }
}