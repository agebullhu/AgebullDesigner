using System.IO;
using System.Linq;
using System.Text;

using Agebull.Common.Access.ADO.Sqlite;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class SqliteAccessBuilder: CoderWithEntity
    {

        protected override string FileSaveConfigName => "File_Model_SqliteAccess";

        private string Fields()
        {
            StringBuilder sql = new StringBuilder();
            bool isFirst = true;
            foreach (var field in Entity.Properties)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sql.Append(",");
                }
                sql.AppendFormat(@"""{0}""", field.PropertyName);
            }
            return sql.ToString();
        }
        private string FullLoadSql()
        {
            StringBuilder sql = new StringBuilder(@"
SELECT");

            bool isFirst = true;
            foreach (var field in Entity.Properties)
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
    [{0}] AS [{1}]", field.PropertyName, field.PropertyName);
            }
            sql.AppendFormat(@"
FROM [{0}] ", this.Entity.SaveTable);
            return sql.ToString();
        }
        private string InsertSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
INSERT INTO [{0}]
(", this.Entity.SaveTable);
            bool isFirst = true;
            var columns = Entity.Properties.Where(p => !p.IsCompute && !p.IsIdentity);
            foreach (var field in columns)
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
    [{0}]", field.PropertyName);
            }
            sql.AppendLine();
            sql.Append(@"
)
VALUES
(");
            isFirst = true;
            foreach (var field in columns)
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
    ${0}", field.PropertyName);
            }
            sql.Append(@"
);");
            if (Entity.PrimaryColumn.IsIdentity)
                sql.Append(@"
select last_insert_rowid();");
            else
                sql.Append(@"
select NULL;");
            return sql.ToString();
        }


        private string InsertCode()
        {
            StringBuilder code = new StringBuilder();
            code.AppendFormat(@"

        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name=""entity"">实体对象</param>
        /// <param name=""cmd"">命令</param>
        /// <returns>返回真说明要取主键</returns>
        protected sealed override bool SetInsertCommand({0} entity, SQLiteCommand cmd)
        {{
            cmd.CommandText = InsertSql;", this.Entity.EntityName);

            var columns = Entity.Properties.Where(p => !p.IsCompute && !p.IsIdentity);
            foreach (var field in columns)
            {
                code.AppendFormat(@"
            cmd.Parameters.Add(new SQLiteParameter(""{0}"",DbType.{1}){{ Value = entity.{0}}});"
                    , field.PropertyName
                    , SQLiteDefault.ToDbType(field.CsType));
            }
            if (this.Entity.PrimaryColumn.IsIdentity)
                code.Append(@"
            return true;
        }");
            else
                code.Append(@"
            return false;
        }");
            return code.ToString();
        }

        private string UpdateCode()
        {

            var pk = Entity.PrimaryColumn ;
            return string.Format(@"

        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name=""entity"">实体对象</param>
        /// <param name=""cmd"">命令</param>
        protected sealed override void SetUpdateCommand({0} entity, SQLiteCommand cmd)
        {{
            StringBuilder sql = new StringBuilder();
            cmd.Parameters.Add(new SQLiteParameter(""{2}"", DbType.{3}){{ Value = entity.{2}}});
            bool isFirst = true;
            foreach (var field in entity.Status.ModifiedProperties)
            {{
                if(field == ""{2}"")
                    continue;
                cmd.Parameters.Add(new SQLiteParameter(field, GetDbType(field)){{ Value = entity.GetValue(field)}});
                if (isFirst)
                {{
                    isFirst = false;
                }}
                else
                {{
                    sql.Append("","");
                }}
                sql.AppendFormat(@""
    {{0}} = ${{0}}"", field);
            }}
            cmd.CommandText = string.Format(@""
UPDATE [{1}] SET
{{0}}
WHERE {2} = ${2};"",sql);
    }}"
                , this.Entity.EntityName
                , this.Entity.SaveTable
                , pk.PropertyName
                , SQLiteDefault.ToDbType(pk.CsType));
        }
        private string GetDbTypeCode()
        {
            StringBuilder code = new StringBuilder();
            
            var columns = Entity.Properties;
            foreach (var field in columns)
            {
                code.AppendFormat(@"
                case ""{0}"":
                    return DbType.{1};"
                    , field.PropertyName
                    , SQLiteDefault.ToDbType(field.CsType));
            }
            
            return string.Format(@"
        /// <summary>
        /// 得到字段的DbType类型
        /// </summary>
        /// <param name=""field"">字段名称</param>
        /// <returns>参数</returns>
        protected sealed override DbType GetDbType(string field)
        {{
            switch (field)
            {{{0}
            }}
            return DbType.String;
        }}", code);
        }

        private string LoadEntityCode()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"

        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name=""reader"">数据读取器</param>
        /// <param name=""entity"">读取数据的实体</param>
        protected sealed override void LoadEntity(SQLiteDataReader reader,{0} entity)
        {{
            using (new EditScope(entity.Status, EditArrestMode.All, false))
            {{"
                    , this.Entity.EntityName);
            int idx = 0;
            foreach (var field in Entity.Properties)
            {
                sql.AppendFormat(@"
                if (!reader.IsDBNull({2}))
                    entity.{0} = {1}({2});"
                    , field.PropertyName
                    , SQLiteDefault.GetDBReaderFunctionName(field.CsType), idx++);
                if (field.Nullable)
                    sql.AppendFormat(@"
                else
                    entity.{0} = null;"
                        , field.PropertyName);
                else
                    sql.AppendFormat(@"
                else
                    entity.{0} = default({1});"
                        , field.PropertyName
                        , field.CsType);
            }
            sql.Append(@"
            }
        }");
            return sql.ToString();
        }


        /// <summary>
        /// 生成实体代码
        /// </summary>
        public void EntityCode(string path)
        {
            foreach (var col in this.Entity.Properties)
            {
                if (col.CsType == "Binary")
                {
                    col.CsType = "byte[]";
                }
            }
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
    /// {1}
    /// </summary>
    partial class {2}DataAccess
    {{
        
        #region 基本SQL语句

        /// <summary>
        /// 表名
        /// </summary>
        protected sealed override string SaveTable
        {{
            get
            {{
                return @""{3}"";
            }}
        }}

        /// <summary>
        /// 主键
        /// </summary>
        public sealed override string PrimaryKey
        {{
            get
            {{
                return @""{4}"";
            }}
        }}

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadSql
        {{
            get
            {{
                return @""{5}"";
            }}
        }}

        

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSql
        {{
            get
            {{
                return @""{6}"";
            }}
        }}

        /// <summary>
        ///  所有字段
        /// </summary>
        public sealed override string[] Fields
        {{
            get
            {{
                return new string[]{{ {7} }};
            }}
        }}
        #endregion

        #region 方法实现
{8}
{9}
{10}
{11}

        #endregion
    }}
}}
"
                , NameSpace
                , this.Entity.Description
                , this.Entity.EntityName
                , this.Entity.SaveTable
                , this.Entity.Properties.First(p => p.IsPrimaryKey).PropertyName, FullLoadSql()
                , InsertSql()
                , Fields()
                , LoadEntityCode()
                , GetDbTypeCode()
                , UpdateCode()
                , InsertCode());
            string file =Path.Combine(path, this.Entity.EntityName + "DataAccess.Designer.cs");
            File.WriteAllText(file, code);
            file = Path.Combine(path, this.Entity.EntityName + "DataAccess.cs");
            if (File.Exists(file))
                return;
            code = string.Format(@"
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
    /// {1}
    /// </summary>
    public sealed partial class {2}DataAccess : SqliteCacheTable<{2}>
    {{
        
    }}
}}
"
                , NameSpace
                , this.Entity.Description
                , this.Entity.EntityName);
            File.WriteAllText(file, code);
        }
    }
}