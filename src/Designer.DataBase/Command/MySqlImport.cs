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
using System.Linq;
using System.Text;
using System.Windows.Threading;
using Agebull.Common.DataModel;
using Gboxt.Common.DataAccess.Schemas;
using MySql.Data.MySqlClient;

#endregion

namespace Agebull.Common.SimpleDesign
{
    public class MySqlImport : NotificationObject
    {
        private SolutionConfig _solution;
        private ProjectConfig _project;
        private TraceMessage _trace;
        private Dispatcher _dispatcher;
        private string _connectionString,_database;


        public void Import(TraceMessage trace, SolutionConfig solution, Dispatcher dispatcher)
        {
            var conStr = ConfigurationManager.ConnectionStrings["mysql"];
            MySqlConnectionStringBuilder cb = new MySqlConnectionStringBuilder(conStr.ConnectionString);
            _database = cb.Database;
            _trace = trace;
            _dispatcher = dispatcher;
            _solution = solution;
            _project = solution.Projects.FirstOrDefault(p => p.Name == _database);
            if (_project == null)
            {
                _project = new ProjectConfig
                {
                    Name = _database,
                    DataBaseObjectName = _database.ToUWord()
                };

                dispatcher.Invoke(() => solution.Projects.Add(_project));
            }
            _solution = solution;
            _connectionString= conStr.ConnectionString;
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
                    var entity = _solution.Entities.FirstOrDefault(p => string.Equals(p.SaveTable, table, StringComparison.OrdinalIgnoreCase));
                    if (entity == null)
                    {
                        isnew = true;
                        entity = new EntityConfig
                        {
                            ReadTableName = table,
                            Name = CoderBase.ToWordName(table),
                            Project = _database
                        };
                        _trace.Track = @"新增的表";
                        entity.Caption = BaiduFanYi.FanYi(entity.Name);
                        _dispatcher.Invoke(() =>
                        {
                            _project.Entities.Add(entity);
                            _solution.Entities.Add(entity);
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

                        InvokeInUiThread(() => entity.Properties.Add(column));
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
                        column.Caption = BaiduFanYi.FanYi(column.Name);
                        _trace.Track = $@"通过百度翻译得到中文名称:{column.Caption}";
                    }
                    else
                    {
                        CheckEnum(column);
                        if (column.EnumConfig != null)
                        {
                            column.EnumConfig.Name = column.Name + "Type";
                            column.EnumConfig.Caption = column.Caption + "自定义类型";
                            column.CustomType = column.EnumConfig.Name;
                            _trace.Track = $@"解析得到枚举类型:{column.EnumConfig.Name},参考内容{column.EnumConfig.Description}";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(column.Description))
                    {
                        column.Description = column.Caption;
                    }
                    column.Caption = column.Caption.Split(CoderBase.NoneLanguageChar, 2)[0];
                }
            }
        }


        #region 方法

        public static void CheckEnum(PropertyConfig column)
        {
            var line = column.Description?.Trim(CoderBase.NoneLanguageChar) ?? "";

            StringBuilder sb = new StringBuilder();
            StringBuilder caption = new StringBuilder();
            bool preIsNumber = false;
            bool startEnum = false;
            EnumConfig ec = new EnumConfig
            {
                Name = column.Parent.Name.ToUWord() + column.Name.ToUWord(),
                Description = column.Description,
                Caption = column.Caption,
                Items = new ConfigCollection<EnumItem>()
            };
            EnumItem ei = new EnumItem();
            foreach (var c in line)
            {
                if (c >= '0' && c <= '9')
                {
                    if (!preIsNumber)
                    {
                        if (!startEnum)
                        {
                            caption.Append(sb);
                        }
                        else if (sb.Length > 0)
                        {
                            new List<string>().Add(sb.ToString());
                            ei.Caption = sb.ToString();
                        }
                        sb = new StringBuilder();
                        startEnum = true;
                    }
                    preIsNumber = true;
                }
                else
                {
                    if (preIsNumber)
                    {
                        if (sb.Length > 0)
                        {
                            ei = new EnumItem
                            {
                                Value = sb.ToString()
                            };
                            ec.Items.Add(ei);
                            sb = new StringBuilder();
                        }
                    }
                    preIsNumber = false;
                }
                sb.Append(c);
            }

            if (!startEnum)
                return;
            if (sb.Length > 0)
            {
                if (preIsNumber)
                {
                    ec.Items.Add(new EnumItem
                    {
                        Value = sb.ToString()
                    });
                }
                else
                {
                    ei.Caption = sb.ToString();
                }
            }

            if (ec.Items.Count > 0)
            {
                ec.LinkField = column.Key;
                column.EnumConfig = ec;
                column.CustomType = ec.Name;
                column.Description = line;
                foreach (var item in ec.Items)
                {
                    if (string.IsNullOrEmpty(item.Caption))
                    {
                        column.EnumConfig = null;
                        column.CustomType = null;
                        return;
                    }
                    var arr = item.Caption.Trim(CoderBase.NoneNameChar).Split(CoderBase.NoneNameChar, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length == 0)
                    {
                        column.EnumConfig = null;
                        column.CustomType = null;
                        return;
                    }
                    item.Caption = arr[0];
                    item.Name = BaiduFanYi.FanYiWord(item.Caption.MulitReplace2("", "表示"));
                }
                if (caption.Length > 0)
                    column.Caption = caption.ToString();
            }
            else
            {
                column.EnumConfig = null;
                column.CustomType = null;
            }
        }

        #endregion

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