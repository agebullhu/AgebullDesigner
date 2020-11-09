// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Gboxt.Common.SimpleDataAccess
// 建立:2014-12-03
// 修改:2014-12-03
// *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.Mysql;
using Agebull.EntityModel.RobotCoder;
using MySql.Data.MySqlClient;


#endregion

namespace Agebull.EntityModel.Designer
{
    public class MySqlImport : NotificationObject
    {
        private ProjectConfig _project;
        private TraceMessage _trace;
        private Dispatcher _dispatcher;
        private string _connectionString, _database;


        public void Import(TraceMessage trace, ProjectConfig project, Dispatcher dispatcher)
        {
            _project = project;
            _database = project.DbSoruce;
            _trace = trace;
            _dispatcher = dispatcher;
            var csb = new MySqlConnectionStringBuilder
            {
                Server = project.DbHost,
                UserID = project.DbUser,
                Password = project.DbPassWord,
                Database = project.DbSoruce,
                SslMode = MySqlSslMode.None,
                Port = project.DbPort
            };
            _connectionString = csb.ConnectionString;
            //_connectionString = $"Database={project.DbSoruce};Data Source={project.DbHost};SslMode=none;User Id={project.DbUser};Password={project.DbPassWord};CharSet=utf8;port=3306;Compress=false;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";

            DoImport();
        }


        public void Import(TraceMessage trace, EntityConfig entity, Dispatcher dispatcher)
        {
            _project = entity.Parent;
            _database = entity.Parent.DbSoruce;
            _trace = trace;
            _dispatcher = dispatcher;
            var csb = new MySqlConnectionStringBuilder
            {
                Server = entity.Parent.DbHost,
                UserID = entity.Parent.DbUser,
                Password = entity.Parent.DbPassWord,
                Database = entity.Parent.DbSoruce,
                SslMode = MySqlSslMode.None,
                Port = entity.Parent.DbPort
            };
            _connectionString = csb.ConnectionString;

            using var connection = new MySqlConnection(_connectionString);

            _trace.Track = "正在连接...";
            connection.Open();
            _trace.Track = "连接成功";
            _trace.Message1 = entity.SaveTableName;
            LoadColumn(connection, entity, false);
            CheckForeignKey(connection, entity);
            _trace.Message1 = "完成";
        }

        private void DoImport()
        {
            _trace.Message1 = "连接数据库";
            _trace.Track = _connectionString;
            using MySqlConnection connection = new MySqlConnection(_connectionString);

            _trace.Track = "正在连接...";
            connection.Open();
            _trace.Track = "连接成功";
            var tables = ReadTableNames(connection);
            _trace.Message1 = "分析表结构";
            foreach (var table in tables)
            {
                CheckTable(connection, table.Key, table.Value);
            }
            foreach (var table in tables)
            {
                CheckForeignKey(connection, _project.Find(table.Key));
            }
            _trace.Message1 = "完成";
        }
        #region 表

        private Dictionary<string, string> ReadTableNames(MySqlConnection connection)
        {
            var tables = new Dictionary<string, string>();
            _trace.Message1 = "分析数据表";
            using var cmd = connection.CreateCommand();
            _trace.Message2 = "读取表名";
            _trace.Track = "正在读取表名...";
            cmd.CommandText = TableInfoSql;

            using var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                _trace.Message1 = "没有任何表";
                return tables;
            }
            while (reader.Read())
            {
                tables.Add(reader.GetString(0), reader.GetString(1));
            }
            _trace.Track = $@"读取成功({tables.Count})";
            return tables;
        }

        private void CheckTable(MySqlConnection connection,string table,string desc)
        {
            bool isnew = false;
            var entity = _project.Find(table);
            if (entity == null)
            {
                isnew = true;
                entity = new EntityConfig
                {
                    ReadTableName = table,
                    SaveTableName = table,
                    Name = NameHelper.ToWordName(table)
                };
                if (!string.IsNullOrWhiteSpace(desc))
                {
                    var vl = desc.Split(NameHelper.NoneLanguageChar, 2);
                    entity.Caption = vl[0];
                    entity.Description = desc;
                }
                _dispatcher.Invoke(() =>
                {
                    _project.Add(entity);
                    entity.Classify = table.SpliteWord()[0];
                });
            }
            _trace.Message2 = entity.Caption ?? entity.Name;
            //_trace.Message3 = "列分析";
            LoadColumn(connection, entity, isnew);
        }
        #endregion

        #region 字段

        private void LoadColumn(MySqlConnection connection, EntityConfig entity, bool isNewEntity)
        {
            using var cmd = connection.CreateCommand();
            cmd.CommandText = ColumnInfoSql(entity.SaveTableName);

            using var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                return;
            while (reader.Read())
            {
                var field = reader.GetString(0);
                if (field == null)
                    continue;
                ReadColumn(entity, isNewEntity, reader, field);
            }
        }

        private void ReadColumn(EntityConfig entity, bool isNewEntity, MySqlDataReader reader, string field)
        {
            _trace.Message3 = $"【{field}】";
            var dbType = reader.GetString(2);
            var column = entity.Find(field);
            bool isNew = isNewEntity;
            if (column == null)
            {
                _trace.Track = @"--新字段";
                isNew = true;
                column = new FieldConfig
                {
                    DbFieldName = field,
                    DbType = dbType,
                    CsType = MySqlDataBaseHelper.ToCSharpType(dbType),
                    Entity = entity
                };
                InvokeInUiThread(() => entity.Add(column));
                if (!reader.IsDBNull(5))
                {
                    column.Caption = reader.GetString(5);
                }
                column.Description = column.Caption;
            }
            else if (column.DbType != dbType)
            {
                _trace.Track = $@"--字段类型变更:{column.DbType }->{dbType}";
                column.DbType = dbType;
                column.CsType = MySqlDataBaseHelper.ToCSharpType(column.DbType);
            }
            column.DbNullable = reader.GetString(1) == "YES";
            column.IsPrimaryKey = reader.GetString(4) == "PRI";

            if (column.CsType == "string" && !reader.IsDBNull(3))
            {
                column.Datalen = (int)reader.GetInt64(3);
            }

            if (!reader.IsDBNull(6))
            {
                var ext = reader.GetString(6);
                column.IsIdentity = ext.Contains("auto_increment", StringComparison.OrdinalIgnoreCase);
            }
            if (column.CsType != "string")
            {
                if (!reader.IsDBNull(7))
                {
                    column.Datalen = (int)reader.GetInt64(7);
                }
                if (!reader.IsDBNull(8))
                {
                    column.Scale = (int)reader.GetInt64(8);
                }
            }
            if (isNew)
            {
                CheckName(column);
            }
            _trace.Track = $"--Name:{column.Name} Caption:{column.Caption}";
        }

        private void CheckName(FieldConfig column)
        {
            switch (column.DbType.ToLower())
            {
                case "varchar":
                case "longtext":
                    column.Name = FirstBy(column.DbFieldName, "m_str", "M_str", "m_", "M_");
                    break;
                case "int":
                case "tinyint":
                    column.Name = FirstBy(column.DbFieldName, "m_b", "m_n", "m_", "M_");
                    break;
                case "double":
                    column.Name = FirstBy(column.DbFieldName, "m_d", "m_", "M_");
                    break;
                default:
                    column.Name = FirstBy(column.DbFieldName, "m_", "M_");
                    break;
            }
            column.Name = NameHelper.ToWordName(column.Name ?? column.DbFieldName);

            if (string.IsNullOrWhiteSpace(column.Caption))
            {
                return;
            }
            var vl = column.Caption;
            var vls = vl.Split(NameHelper.NoneLanguageChar, 2);
            column.Caption = vls[0];
            column.Description = vl;
        }

        private string FirstBy(string str, params string[] args)
        {
            foreach (var a in args)
            {
                if (str.IndexOf(a, StringComparison.Ordinal) == 0)
                    return str.Substring(a.Length);
            }
            return str;
        }

        #endregion

        #region 外键

        private void CheckForeignKey(MySqlConnection connection,EntityConfig entity)
        {
            using var cmd = connection.CreateCommand();
            cmd.CommandText = KeyInfoSql(entity.SaveTableName);

            using var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                return;
            while (reader.Read())
            {
                var field = reader.GetString(0);
                var property = entity.Find(field);
                if (property == null)
                    continue;
                property.IsLinkField = true;
                property.IsLinkKey = true;
                property.LinkTable = reader.GetString(1);
                property.LinkField = reader.GetString(2);
            }
        }
        #endregion

        #region SQL
        string ColumnInfoSql(string table) => $@"
SELECT column_name,is_nullable,data_type,character_maximum_length,column_key,column_comment,extra,numeric_precision,numeric_scale
FROM information_schema.columns 
WHERE table_schema='{_database}' and table_name='{table}'";

        string TableInfoSql => $@"
SELECT table_name,table_comment 
FROM information_schema.tables
WHERE table_schema='{_database}' and table_type='base table';";

        string KeyInfoSql(string table) => $@"
SELECT column_name, referenced_table_name, referenced_column_name
FROM information_schema.KEY_COLUMN_USAGE t
WHERE constraint_name <> 'PRIMARY' AND table_name='{table}'";
        /*
`TABLE_CATALOG` AS `TABLE_CATALOG`,
`TABLE_SCHEMA` AS `TABLE_SCHEMA`,
`TABLE_NAME` AS `TABLE_NAME`,
`COLUMN_NAME` AS `COLUMN_NAME`,
`ORDINAL_POSITION` AS `ORDINAL_POSITION`,
`COLUMN_DEFAULT` AS `COLUMN_DEFAULT`,
`IS_NULLABLE` AS `IS_NULLABLE`,
`DATA_TYPE` AS `DATA_TYPE`,
`CHARACTER_MAXIMUM_LENGTH` AS `CHARACTER_MAXIMUM_LENGTH`,
`CHARACTER_OCTET_LENGTH` AS `CHARACTER_OCTET_LENGTH`,
`NUMERIC_PRECISION` AS `NUMERIC_PRECISION`,
`NUMERIC_SCALE` AS `NUMERIC_SCALE`,
`DATETIME_PRECISION` AS `DATETIME_PRECISION`,
`CHARACTER_SET_NAME` AS `CHARACTER_SET_NAME`,
`COLLATION_NAME` AS `COLLATION_NAME`,
`COLUMN_TYPE` AS `COLUMN_TYPE`,
`COLUMN_KEY` AS `COLUMN_KEY`,
`EXTRA` AS `EXTRA`,
`PRIVILEGES` AS `PRIVILEGES`,
`COLUMN_COMMENT` AS `COLUMN_COMMENT`,
`GENERATION_EXPRESSION` AS `GENERATION_EXPRESSION`
    */
        #endregion
    }
}