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
        private string _connectionString,_database;


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
                Database = project.DbSoruce
            };
            _connectionString = csb.ConnectionString;
            DoImport();
        }

        private void DoImport()
        {
            _trace.Message1 = "连接数据库";
            _trace.Track = _connectionString;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                _trace.Track = "正在连接...";
                connection.Open();
                _trace.Track = "连接成功";
                var tables = new List<string>();

                _trace.Message1 = "分析数据表";
                using (var cmd = connection.CreateCommand())
                {
                    _trace.Message2 = "读取表名";
                    _trace.Track = "正在读取表名...";
                    cmd.CommandText =
                        $@"select Table_Name from information_schema.tables where table_schema='{_database}' and table_type='base table';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            _trace.Message1 = "没有任何表";
                            return;
                        }
                        while (reader.Read())
                        {
                            tables.Add(reader.GetString(0));
                        }
                    }
                    _trace.Track = $@"读取成功({tables.Count})";
                }
                _trace.Message1 = "分析表结构";
                foreach (var t in tables)
                {
                    string table = t;
                    bool isnew = false;
                    _trace.Message2 = table;
                    var entity = GlobalConfig.GetEntity(p => string.Equals(p.SaveTable, table, StringComparison.OrdinalIgnoreCase));
                    if (entity == null)
                    {
                        isnew = true;
                        entity = new EntityConfig
                        {
                            ReadTableName = table,
                            Name = CoderBase.ToWordName(table)
                        };
                        _trace.Track = @"新增的表";
                        _dispatcher.Invoke(() =>
                        {
                            _project.Add(entity);
                        });
                    }
                    _trace.Message3 = "列分析";
                    using (var cmd = connection.CreateCommand())
                    {
                        LoadColumn(_database, cmd, table, entity, isnew);
                    }

                }
            }
            _trace.Message1 = "完成";
        }
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
        private void LoadColumn(string db, MySqlCommand cmd, string table, EntityConfig entity, bool isNewEntity)
        {
            cmd.CommandText =
                $@"select COLUMN_NAME,IS_Nullable,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,COLUMN_KEY,COLUMN_COMMENT,EXTRA
from information_schema.columns where table_schema='{
                    db}' and table_name='{table}'";
            using (var reader = cmd.ExecuteReader())
            {
                if (!reader.HasRows)
                    return;
                while (reader.Read())
                {
                    var field = reader.GetString(0);
                    _trace.Message4 = field;
                    if (field == null)
                        continue;
                    var dbType = reader.GetString(2);
                    var column = entity.Properties.FirstOrDefault(p => string.Equals(p.ColumnName, field, StringComparison.OrdinalIgnoreCase));
                    bool isNew = isNewEntity;
                    if (column == null)
                    {
                        _trace.Track = @"新字段";
                        isNew = true;
                        column = new PropertyConfig
                        {
                            ColumnName = field,
                            DbType = dbType,
                            CsType = ToCstringType(dbType),
                            Parent=entity 
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
                        _trace.Track = $@"字段类型变更:{column.DbType }->{dbType}";
                        column.DbType = dbType;
                        column.CsType = ToCstringType(column.DbType);
                    }
                    column.DbNullable = reader.GetString(1) == "YES";
                    column.IsPrimaryKey = reader.GetString(4) == "PRI";

                    if (!reader.IsDBNull(3))
                    {
                        column.Datalen = (int)reader.GetInt64(3);
                    }

                    if (!reader.IsDBNull(6))
                    {
                        var ext = reader.GetString(6);
                        column.IsIdentity = ext.Contains("auto_increment");
                        if (column.IsIdentity)
                            _trace.Track = @"自增列";
                    }
                    if (!isNew)
                        continue;
                    _trace.Track = @"分析属性名称";
                    switch (column.DbType.ToLower())
                    {
                        case "varchar":
                        case "longtext":
                            column.Name = FirstBy(column.ColumnName, "m_str", "M_str", "m_", "M_");
                            break;
                        case "int":
                        case "tinyint":
                            column.Name = FirstBy(column.ColumnName, "m_b", "m_n", "m_", "M_");
                            break;
                        case "double":
                            column.Name = FirstBy(column.ColumnName, "m_d", "m_", "M_");
                            break;
                        default:
                            column.Name = FirstBy(column.ColumnName, "m_", "M_");
                            break;
                    }
                    column.Name = CoderBase.ToWordName(column.Name ?? column.ColumnName);
                    _trace.Track = $@"属性名称:{column.Name}";
                    if (string.IsNullOrWhiteSpace(column.Caption))
                    {
                        column.Caption = column.Name;
                    }
                    if (string.IsNullOrWhiteSpace(column.Description))
                    {
                        column.Description = column.Caption;
                    }
                    column.Caption = column.Caption.Split(CoderBase.NoneLanguageChar, 2)[0];
                }
            }
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


        private string ToCstringType(string dbType)
        {
            switch (dbType)
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
                case "BOOL":
                    return "bool";
                case "char":
                    return "char";
                case "datetime":
                    return "DateTime";
            }
            return "string";
        }
    }
}