using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Documents;

using Agebull.Common.Access.ADO.Sqlite;

using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDataAccess.Design
{
    public sealed class DataBaseBuilder
    {
        public string NameSpace
        {
            get;
            set;
        }
        public IEnumerable<TableSchema> Schemas
        {
            get;
            set;
        }

        private string CtorCode()
        {
            StringBuilder sql = new StringBuilder();
            bool isFirst = true;
            foreach (var schema in Schemas)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sql.Append(",");
                }
                sql.AppendFormat(@"
                {{""{0}"",_{1}Sql}}", schema.EntityName, schema.TableName);
            }
            return sql.ToString();
        }

        private string TableSqlCode()
        {
            StringBuilder sql = new StringBuilder();
            foreach (var schema in Schemas)
            {
                sql.AppendLine(TableSql(schema));
            }
            return sql.ToString();
        }
        private string TablesCode()
        {
            StringBuilder code = new StringBuilder();
            foreach (var schema in this.Schemas)
            {
                string name = schema.EntityName.ToPluralism();
                code.AppendFormat(@"

        /// <summary>
        /// {0}数据访问对象
        /// </summary>
        private {2}DataAccess _{1};

        /// <summary>
        /// {0}数据访问对象
        /// </summary>
        public {2}DataAccess {3}
        {{
            get
            {{
                return this._{1} ?? ( this._{1} = new {2}DataAccess{{ DataBase = this}});
            }}
        }}"
                    , schema.Description
                    , name.ToLWord()
                    , schema.EntityName
                    , name);
            }
            return code.ToString();
        }

        private string TableSql(TableSchema schema)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
CREATE TABLE [{0}](", schema.TableName);

            bool isFirst = true;
            var pk = schema.PrimaryColumn;
            if (pk != null)
            {
                sql.AppendLine();
                isFirst = false;
                sql.AppendFormat(@"    [{0}]", pk.PropertyName);
                if (pk.IsIdentity)
                {
                    sql.Append(" INTEGER PRIMARY KEY AUTOINCREMENT");
                }
                else
                {
                    sql.AppendFormat("{0} NOT NULL PRIMARY KEY", SQLiteDefault.ToDataBaseType(pk.PropertyType.Type));
                }
            }
            foreach (var field in schema.ColumnSchemas.Where(p => p.PropertyType != null && !p.IsPrimaryKey))
            {
                if (isFirst)
                {
                    sql.AppendLine();
                    isFirst = false;
                }
                else
                {
                    sql.AppendLine(",");
                }
                sql.AppendFormat(@"    [{0}] {1}", field.PropertyName, SQLiteDefault.ToDataBaseType(field.PropertyType.Type));
                if (!field.PropertyType.Nullable)
                {
                    switch (field.PropertyType.Type.ToLower())
                    {
                        case "string":
                        case "Binary":
                        case "byte[]":
                            break;
                        default:
                            sql.Append(" NOT NULL");
                            break;
                    }
                }
            }
            sql.AppendLine();
            sql.Append(")");
            return string.Format(@"

        /// <summary>
        /// {0}的结构语句
        /// </summary>
        private TableSql _{1}Sql = new TableSql
        {{
            TableName = ""{1}"",
            PimaryKey = ""{2}"",
            CreateSql = @""{3}"",
        }};", schema.Description, schema.TableName, schema.PrimaryColumn.PropertyName, sql);
        }


        /// <summary>
        /// 生成实体代码
        /// </summary>
        public void EntityCode(string path)
        {
            var code = string.Format(@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

using Agebull.Common.DataModel;

using Gboxt.Common.SimpleDataAccess.SQLite;

namespace {0}.DataAccess
{{
    /// <summary>
    /// 本地数据库
    /// </summary>
    partial class LocalDataBase
    {{
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public LocalDataBase()
        {{
            tableSql = new Dictionary<string, TableSql>(StringComparer.OrdinalIgnoreCase)
            {{{1}
            }};
            Initialize();
        }}
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();

        #endregion

        #region 表SQL
{2}
        #endregion

        #region 表对象
{3}
        #endregion
    }}
}}
"
                , NameSpace
                , CtorCode()
                , TableSqlCode()
                , TablesCode());
            string file = Path.Combine(path, "LocalDataBase.Designer.cs");
            File.WriteAllText(file, code);
            file = Path.Combine(path, "LocalDataBase.cs");
            if (File.Exists(file))
                return;
            code = string.Format(@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

using Agebull.Common.DataModel;

using Gboxt.Common.SimpleDataAccess.SQLite;

namespace {0}.DataAccess
{{
    /// <summary>
    /// 本地数据库
    /// </summary>
    public sealed partial class LocalDataBase : SqliteDataBase
    {{
        
        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize()
        {{
            FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,""storage.db"");
        }}
        
        /// <summary>
        /// 生成缺省数据库
        /// </summary>
        public static void CreateDefault()
        {{
            DefaultDataBase = Default = new LocalDataBase();
            DefaultDataBase.CheckDataBase();
        }}

        /// <summary>
        /// 缺省强类型数据库
        /// </summary>
        public static LocalDataBase Default
        {{
            get;
            private set;
        }}
    }}
}}
"
                , NameSpace);
            File.WriteAllText(file, code);
        }
    }
}