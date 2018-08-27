using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.SqlServer;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    public class SqlSchemaChecker : CoderBase
    {
        public ProjectConfig Project
        {
            get;
            set;
        }

        public EntityConfig Entity
        {
            get;
            set;
        }

        public void ImportProject()
        {
            var csb = new SqlConnectionStringBuilder
            {
                InitialCatalog = Project.DbSoruce,
                UserID = Project.DbUser,
                Password = Project.DbPassWord,
                DataSource = Project.DbHost
            };
            using (var connection = new SqlConnection(csb.ConnectionString))
            {
                connection.Open();
                Dictionary<string, string> tables = new Dictionary<string, string>();
                using (var cmd = new SqlCommand(table_sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(reader.GetString(0), reader.GetString(1));
                        }
                    }
                }
                foreach (var table in tables)
                {
                    CheckColumns(connection, table.Key, table.Value);
                }
                connection.Close();
            }
        }

        private const string table_sql = @"SELECT [Tables].name as [Name],[Properties].value AS [Description] 
FROM sys.tables [Tables]
LEFT OUTER JOIN sys.extended_properties AS [Properties] ON [Properties].major_id = [Tables].object_id AND [Properties].name = 'MS_Description'
WHERE  [Tables].[type]='U' AND [Properties].minor_id=0;";

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

        private SqlParameter parameter;

        public void CheckColumns()
        {
            var csb = new SqlConnectionStringBuilder
            {
                InitialCatalog = Project.DbHost,
                UserID = Project.DbUser,
                Password = Project.DbPassWord,
                DataSource = Project.DbSoruce
            };
            using (var connection = new SqlConnection(csb.ConnectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add(parameter = new SqlParameter("@entity", SqlDbType.NVarChar, 256));
                    foreach (EntityConfig schema in Project.Entities)
                    {
                        CheckColumns(cmd, schema);
                    }
                }
                connection.Close();
            }
        }

        public void CheckColumns(SqlConnection connection, string table, string description)
        {
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add(parameter = new SqlParameter("@entity", SqlDbType.NVarChar, 256));
                var ch = table.Split('_');
                var name = ch.Length > 2 ? ch[2] : table;
                var entity = new EntityConfig
                {
                    Name = name,
                    Caption = description,
                    Description = description,
                    ReadTableName = table,
                    SaveTableName = table,
                    Parent = Project
                };
                CheckColumns(cmd, entity);
                Project.Add(entity);
            }
        }
        private void CheckColumns(SqlCommand cmd, EntityConfig schema)
        {
            parameter.Value = schema.ReadTableName;
            foreach (PropertyConfig col in schema.Properties)
                col.DbIndex = 0;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string field = reader.GetString(0);
                    PropertyConfig col = schema.Properties.FirstOrDefault(p => string.Equals(p.ColumnName, field, StringComparison.OrdinalIgnoreCase));
                    if (col == null)
                    {
                        schema.Add(col = new PropertyConfig
                        {
                            Name = field,
                            ColumnName = field,
                            DbType = reader.GetString(1),
                            Parent = Entity
                        });

                        col.Datalen = Convert.ToInt32(reader.GetValue(2));
                        col.Scale = Convert.ToInt32(reader.GetValue(3));
                        if (col.Datalen <= 0)
                        {
                            col.Datalen = Convert.ToInt32(reader.GetValue(4));
                        }
                        col.CsType = ToCstringType(col.DbType);
                    }
                    else
                    {
                        col.DbType = reader.GetString(1);
                        if (col.CsType.Equals("string", StringComparison.OrdinalIgnoreCase))
                        {
                            col.Datalen = Convert.ToInt32(reader.GetValue(4));
                        }
                        else
                        {
                            col.Datalen = Convert.ToInt32(reader.GetValue(2));
                            col.Scale = Convert.ToInt32(reader.GetValue(3));
                        }
                    }
                    col.DbNullable = reader.GetBoolean(5);
                    col.DbIndex = Convert.ToInt32(reader.GetValue(6));
                    if (!reader.IsDBNull(7))
                    {
                        col.Caption = col.Description = reader.GetString(7);
                    }
                    col.IsPrimaryKey = !reader.IsDBNull(8);
                    col.IsIdentity = reader.GetBoolean(9);
                    col.IsCompute = reader.GetBoolean(10);
                }
            }
        }

        private string ToCstringType(string dbType)
        {
            switch (dbType.ToLower())
            {
                case "int":
                    return "int";
                case "bigint":
                    return "long";
                case "tinyint":
                    return "sbyte";
                case "smallint":
                    return "short";
                case "real":
                case "float":
                    return "float";
                case "double":
                    return "double";
                case "decimal":
                case "numberic":
                    return "decimal";
                case "bool":
                case "bit":
                    return "bool";
                case "char":
                    return "char";
                case "uniqueidentifier":
                    return "Guid";
                case "datetime":
                case "datetime2":
                    return "DateTime";
            }
            return "string";
        }
        private int CheckColumns2(SqlCommand cmd, EntityConfig schema)
        {
            bool result = true;
            bool hase = false;
            parameter.Value = schema.ReadTableName;
            foreach (PropertyConfig col in schema.PublishProperty)
                col.DbIndex = 0;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    hase = true;
                    string field = reader.GetString(0);
                    PropertyConfig col = schema.PublishProperty.FirstOrDefault(p => string.Equals(p.ColumnName, field, StringComparison.OrdinalIgnoreCase));
                    if (col == null)
                    {
                        result = false;
                        continue;
                    }
                    col.DbIndex = Convert.ToInt32(reader.GetValue(6));
                    if (!string.Equals(reader.GetString(1), col.DbType, StringComparison.OrdinalIgnoreCase))
                    {
                        result = false;
                        continue;
                    }
                    if (col.CsType.Equals("string", StringComparison.OrdinalIgnoreCase))
                    {
                        if (col.Datalen != Convert.ToInt32(reader.GetValue(4)))
                        {
                            result = false;
                            continue;
                        }
                    }
                    else
                    {
                        if (col.Datalen != Convert.ToInt32(reader.GetValue(2)) || col.Scale != Convert.ToInt32(reader.GetValue(3)))
                        {
                            result = false;
                            continue;
                        }
                    }
                    if (col.DbNullable != reader.GetBoolean(5))
                    {
                        result = false;
                        continue;
                    }
                    if (reader.GetBoolean(8))
                    {
                        result = false;
                    }
                }
            }
            return !hase
                ? 2
                : !result
                    ? 1
                    : schema.PublishProperty.Any(p => p.DbIndex == 0)
                        ? 1
                        : 0;
        }



        public static string MoveData(EntityConfig entity)
        {
            var code = new StringBuilder();
            code.AppendFormat(@"INSERT INTO [dbo].[{0}]({1}", entity.ReadTableName, entity.PrimaryColumn.ColumnName);
            foreach (PropertyConfig col in entity.PublishProperty) //.Where(p => p.DbIndex > 0)
            {
                if (col.IsPrimaryKey)
                    continue;
                code.AppendFormat(@",[{0}]", col.ColumnName);
            }
            code.AppendFormat(@")
SELECT {0}", entity.PrimaryColumn.ColumnName);
            foreach (PropertyConfig col in entity.PublishProperty) //.Where(p => p.DbIndex > 0)
            {
                if (col.IsPrimaryKey)
                    continue;
                code.AppendFormat(@",[{0}]", col.ColumnName);
            }
            code.AppendFormat(@" FROM {0}_OLD;", entity.ReadTableName);
            return code.ToString();
        }

        public static string SetPrimary(EntityConfig entity)
        {
            return string.Format(@"ALTER TABLE dbo.{0} ADD CONSTRAINT
PK_{0} PRIMARY KEY CLUSTERED 
(
	[{1}]
) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];", entity.ReadTableName, entity.PrimaryColumn.ColumnName);
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

        public void IntCheck()
        {
            foreach (EntityConfig schema in Project.Entities)
            {
                foreach (PropertyConfig col in schema.PublishProperty)
                {
                    if (col.CsType == "string")
                        col.DbNullable = true;
                    col.DbIndex = 0;
                }
            }
            CheckColumns();
            var csb = new SqlConnectionStringBuilder
            {
                InitialCatalog = Project.DbHost,
                UserID = Project.DbUser,
                Password = Project.DbPassWord,
                DataSource = Project.DbSoruce
            };
            using (var connection = new SqlConnection(csb.ConnectionString))
            {
                connection.Open();
                foreach (EntityConfig schema in Project.Entities)
                {
                    TraceMessage.DefaultTrace.Message2 = schema.ReadTableName;
                    foreach (PropertyConfig col in schema.PublishProperty)
                    {
                        TraceMessage.DefaultTrace.Message3 = col.ColumnName;
                        if (col.DbIndex <= 0)
                        {
                            TraceMessage.DefaultTrace.Track = "字段不存在";
                            continue;
                        }

                        bool checkInt = true;
                        bool isInt = true;
                        switch (col.CsType.ToLower())
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
                        string sql1 = $"SELECT [{col.ColumnName}] FROM [{schema.ReadTableName}]";
                        using (var cmd = new SqlCommand(sql1, connection))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.IsDBNull(0))
                                    {
                                        col.DbNullable = true;
                                    }
                                    if (checkInt)
                                    {
                                        string obj = reader[0].ToString();
                                        TraceMessage.DefaultTrace.Message4 = obj;
                                        if (obj.Contains('.') && long.Parse(obj.Split('.')[1]) > 0)
                                        {
                                            isInt = false;
                                        }
                                    }
                                    if (!isInt)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        if (checkInt && !isInt && (col.CsType != "decimal" || col.DbType != "decimal"))
                        {
                            TraceMessage.DefaultTrace.Track = "改变字段类型";
                            col.CsType = "decimal";
                            col.DbType = "decimal";
                        }
                        //if (!col.DbNullable)
                        //{
                        //    if (col.Nullable)
                        //        TraceMessage.DefaultTrace.Track = "字段已改成不为空";
                        //    col.Nullable = false;
                        //}
                        col.DbType = SqlServerHelper.ToDataBaseType(col);
                    }
                }
                connection.Close();
            }
            CheckColumns();
        }


        public void AlertTables(string host, string user, string pwd, string dbName)
        {
            TraceMessage.DefaultTrace.Message1 = "重构数据库";
            var csb = new SqlConnectionStringBuilder
            {
                InitialCatalog = dbName,
                UserID = user,
                Password = pwd,
                DataSource = host
            };
            using (var connection = new SqlConnection(csb.ConnectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add(parameter = new SqlParameter("@entity", SqlDbType.NVarChar, 256));
                    foreach (EntityConfig schema in Project.Entities)
                    {
                        TraceMessage.DefaultTrace.Message2 = schema.ReadTableName;
                        parameter.Value = schema.ReadTableName;
                        IEnumerable<string> sqls = AlertTable(cmd, schema);
                        foreach (string sq in sqls)
                        {
                            TraceMessage.DefaultTrace.Track = sq;
                            using (var cmd2 = new SqlCommand(sq, connection))
                            {
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }
                }
                connection.Close();
            }
        }
    }
}