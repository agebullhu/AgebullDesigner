using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.Common.Access.ADO.Sqlite;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign.WebApplition
{
    public sealed class SqlServerAccessBuilder : CoderWithEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string Name => "File_Model_SqlServerAccess";

        private string SqlCode()
        {
            return string.Format(@"
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId
        {{
            get {{ return {6}.Table_{8}; }}
        }}

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {{
            get
            {{
                return @""{0}"";
            }}
        }}

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {{
            get
            {{
                return @""{1}"";
            }}
        }}

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey
        {{
            get
            {{
                return @""{2}"";
            }}
        }}

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {{
            get
            {{
                return @""{3}"";
            }}
        }}

        

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode
        {{
            get
            {{
                return @""{4}"";
            }}
        }}

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode
        {{
            get
            {{
                return @""{5}"";
            }}
        }}

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        internal string GetModifiedSqlCode({8} data)
        {{
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return "";"";
            StringBuilder sql = new StringBuilder();{7}
            return sql.ToString();
        }}

        #endregion
"
                , this.Entity.ReadTableName
                , this.Entity.SaveTable
                , this.Entity.PrimaryColumn.PropertyName
                , this.FullLoadSql()
                , this.InsertSql()
                , this.UpdateSql()
                , this.Project.DataBaseObjectName
                , this.UpdateSqlByModify()
                , this.Entity.EntityName);
        }

        private string FieldCode()
        {
            return string.Format(@"
        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{{ {0} }};

        /// <summary>
        ///  所有字段
        /// </summary>
        public sealed override string[] Fields
        {{
            get
            {{
                return _fields;
            }}
        }}

        /// <summary>
        ///  字段字典
        /// </summary>
        public static Dictionary<string, string> fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {{{1}
        }};

        /// <summary>
        ///  字段字典
        /// </summary>
        public sealed override Dictionary<string, string> FieldMap
        {{
            get {{ return fieldMap ; }}
        }}
        #endregion"
                , this.Fields()
                , this.FieldMap());
        }

        private string CreateCode()
        {
            var innerCode = string.Format(@"{0}{1}

        #region 方法实现
{2}
{3}
{4}
{5}
{6}
{7}
        #endregion
"
                , this.SqlCode()
                , this.FieldCode()
                , this.LoadEntityCode()
                , this.GetDbTypeCode()
                , this.CreateFullSqlParameter()
                , this.UpdateCode()
                , this.InsertCode()
                , this.CreateScope());

            return string.Format(@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Gboxt.Common.DataModel;

using Gboxt.Common.DataModel.SqlServer;


namespace {0}.DataAccess
{{
    /// <summary>
    /// {1}
    /// </summary>
    {2} partial class {3}DataAccess
    {{{4}{5}
    }}

    sealed partial class {6}
    {{
{7}
{8}
{9}
    }}
}}
"
                , this.NameSpace
                , this.Entity.Description
                , this.Entity.IsInternal ? "internal" : "public"
                , this.Entity.EntityName
                , null //ec.MakeSource()
                , innerCode
                , this.Project.DataBaseObjectName
                , this.TableSql()
                , this.TableObject()
                , TablesEnum());
        }

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            var file = Path.Combine(path, this.Entity.EntityName + "DataAccess.Designer.cs");
            if (this.Entity.IsClass)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                return;
            }
            this.SaveCode(file, this.CreateCode());
        }
        

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            var file = Path.Combine(path, this.Entity.EntityName + "DataAccess.cs");
            if (this.Entity.IsClass)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                return;
            }
            var code = string.Format(@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Gboxt.Common.DataModel.SqlServer;

namespace {0}.DataAccess
{{
    /// <summary>
    /// {1}
    /// </summary>
    sealed partial class {2}DataAccess : SqlServerTable<{2}>
    {{

    }}
}}
"
                , this.NameSpace
                , this.Entity.Description
                , this.Entity.EntityName);
            this.SaveCode(file, code);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string TablesEnum()
        {
            return $@"

        /// <summary>
        /// {Entity.Caption}({Entity.ReadTableName}):{Entity.Description}
        /// </summary>
        public const int Table_{Entity.Name} = 0x{Entity.Index:x};";
        }
        private string TableObject()
        {
            var name = this.Entity.EntityName.ToPluralism();
            return string.Format(@"

        /// <summary>
        /// {0}数据访问对象
        /// </summary>
        private {2}DataAccess _{1};

        /// <summary>
        /// {0}数据访问对象
        /// </summary>
        {4} {2}DataAccess {3}
        {{
            get
            {{
                return this._{1} ?? ( this._{1} = new {2}DataAccess{{ DataBase = this}});
            }}
        }}"
                , this.Entity.Description
                , name.ToLWord()
                , this.Entity.EntityName
                , name
                , this.Entity.IsInternal ? "internal" : "public");
        }

        private string TableSql()
        {
            return string.Format(@"

        /// <summary>
        /// {0}的结构语句
        /// </summary>
        private TableSql _{1}Sql = new TableSql
        {{
            TableName = ""{1}"",
            PimaryKey = ""{2}""
        }};", this.Entity.Description, this.Entity.ReadTableName, this.Entity.PrimaryColumn.PropertyName);
        }

        private string Fields()
        {
            var sql = new StringBuilder();
            var isFirst = true;
            foreach (var field in this.Entity.DbFields)
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

        private string FieldMap()
        {
            var sql = new StringBuilder();
            var isFirst = true;
            var names = new List<string>();
            this.FieldMap(this.Entity, sql, names, ref isFirst);
            return sql.ToString();
        }

        private void FieldMap(EntityConfig table, StringBuilder sql, List<string> names, ref bool isFirst)
        {
            if (table == null)
            {
                return;
            }
            if (!string.IsNullOrEmpty(table.ModelBase))
            {
                this.FieldMap(this.Project.Entities.FirstOrDefault(p => p.EntityName == table.ModelBase), sql, names, ref isFirst);
            }
            foreach (var field in table.DbFields)
            {
                if (names.Contains(field.PropertyName))
                {
                    continue;
                }
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sql.Append(",");
                }
                sql.AppendFormat(@"
            {{ ""{0}"" , ""{1}"" }}", field.PropertyName, field.ColumnName);
                names.Add(field.PropertyName);

                var alias = field.GetAliasPropertys();
                foreach (var a in alias)
                {
                    if (names.Contains(a))
                    {
                        continue;
                    }
                    names.Add(a);
                    sql.AppendFormat(@",
            {{ ""{0}"" , ""{1}"" }}", a, field.ColumnName);
                }
            }
            if (!table.DbFields.Any(p => p.PropertyName.Equals("Id", StringComparison.OrdinalIgnoreCase)))
            {
                sql.AppendFormat(@",
            {{ ""Id"" , ""{0}"" }}", table.PrimaryColumn.ColumnName);
            }
        }

        private string FullLoadSql()
        {
            var sql = new StringBuilder();

            var isFirst = true;
            foreach (var field in this.Entity.DbFields)
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
    [{0}] AS [{1}]", field.ColumnName, field.PropertyName);
            }
            return sql.ToString();
        }

        /// <summary>
        /// 字段唯一条件(主键或组合键)
        /// </summary>
        /// <returns></returns>
        private string UniqueCondition()
        {
            if (!this.Entity.DbFields.Any(p => p.UniqueIndex > 0))
                return string.Format(@"[{0}] = @{1}", this.Entity.PrimaryColumn.ColumnName, this.Entity.PrimaryColumn.PropertyName);

            var code = new StringBuilder();
            var uniqueFields = this.Entity.DbFields.Where(p => p.UniqueIndex > 0).OrderBy(p => p.UniqueIndex).ToArray();
            var isFirst = true;
            foreach (var col in uniqueFields)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    code.Append(" AND ");
                }
                code.AppendFormat("{0}=@{1}", col.ColumnName, col.PropertyName);
            }
            return code.ToString();
        }
        private string InsertSql()
        {
            if (!this.Entity.DbFields.Any(p => p.UniqueIndex > 0))
            {
                return this.OnlyInsertSql();
            }
            var code = new StringBuilder();
            code.AppendFormat(@"
DECLARE @__myId INT;
SELECT @__myId = [{0}] FROM [{1}] WHERE {2}", this.Entity.PrimaryColumn.ColumnName, this.Entity.SaveTable, UniqueCondition());

            code.AppendFormat(@"
IF @__myId IS NULL
BEGIN{0}
    SET @__myId = {3};
END
ELSE
BEGIN
    SET @{2}=@__myId;{1}
END
SELECT @__myId;"
                , this.OnlyInsertSql(true)
                , this.UpdateSql(true)
                , this.Entity.PrimaryColumn.PropertyName
                , this.Entity.PrimaryColumn.IsIdentity ? "SCOPE_IDENTITY()" : this.Entity.PrimaryColumn.ColumnName);
            return code.ToString();
        }

        private string OnlyInsertSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            var columns = this.Entity.DbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)).ToArray();
            sql.AppendFormat(@"
INSERT INTO [{0}]
(", this.Entity.SaveTable);
            var isFirst = true;
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
    [{0}]", field.ColumnName);
            }
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
    @{0}", field.PropertyName);
            }
            sql.Append(@"
);");
            if (isInner)
            {
                sql.Replace("\n", "\n    ");
            }
            else if (this.Entity.PrimaryColumn.IsIdentity)
            {
                sql.Append(@"
SELECT SCOPE_IDENTITY();");
            }

            return sql.ToString();
        }

        private string UpdateSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            IEnumerable<PropertyConfig> columns = this.Entity.DbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
            sql.AppendFormat(@"
UPDATE [{0}] SET", this.Entity.SaveTable);
            var isFirst = true;
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
       [{0}] = @{1}", field.ColumnName, field.PropertyName);
            }
            sql.AppendFormat(@"
 WHERE {0};", this.UniqueCondition());
            if (isInner)
            {
                sql.Replace("\n", "\n    ");
            }
            return sql.ToString();
        }

        private string UpdateSqlByModify(bool isInner = false)
        {
            var code = new StringBuilder();
            IEnumerable<PropertyConfig> columns = this.Entity.DbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
            code.AppendFormat(@"
            sql.AppendLine(""UPDATE [{0}] SET"");", Entity.SaveTable);


            foreach (var field in columns)
            {
                code.AppendFormat(@"
            //{0}
            if (data.__EntityStatus.ModifiedProperties[{1}.Real_{2}] > 0)
                sql.AppendLine(""       [{3}] = @{2}"");", field.Caption, Entity.EntityName, field.PropertyName, field.ColumnName);
            }
            code.AppendFormat(@"
            sql.Append("" WHERE {0};"");", this.UniqueCondition());
            return code.ToString();
        }

        private string PropertyName(PropertyConfig col, string pre)
        {
            return col.CustomType == null ? $"{pre}{col.PropertyName}" : $"({col.CustomType}){pre}{col.PropertyName}";
        }

        private string PropertyName2(PropertyConfig col, string pre)
        {
            return col.CustomType == null ? $"{pre}{col.PropertyName}" : $"({col.CsType}){pre}{col.PropertyName}";
        }

        private string CreateScope()
        {
            return string.Format(@"

        /// <summary>
        /// 构造一个缺省可用的数据库对象
        /// </summary>
        /// <returns></returns>
        protected override SqlServerDataBase CreateDefaultDataBase()
        {{
            return {1}.Default ?? new {1}();
        }}
        
        /// <summary>
        /// 生成数据库访问范围
        /// </summary>
        internal static SqlServerDataTableScope<{0}> CreateScope()
        {{
            var db = {1}.Default ?? new {1}();
            return SqlServerDataTableScope<{0}>.CreateScope(db, db.{2});
        }}", this.Entity.EntityName, this.Project.DataBaseObjectName, this.Entity.EntityName.ToPluralism());
        }

        private string CreateFullSqlParameter()
        {
            var code = new StringBuilder();
            code.AppendFormat(@"

        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name=""entity"">实体对象</param>
        /// <param name=""cmd"">命令</param>
        /// <returns>返回真说明要取主键</returns>
        private void CreateFullSqlParameter({0} entity, SqlCommand cmd)
        {{", this.Entity.EntityName);

            var isFirstNull = true;
            foreach (var field in this.Entity.DbFields.OrderBy(p => p.Index))
            {
                code.AppendFormat(@"
            //{2:D2}:{0}({1})", field.Caption, field.PropertyName, field.Index + 1);
                if (!string.IsNullOrWhiteSpace(field.CustomType))
                {
                    code.AppendFormat(@"
            cmd.Parameters.Add(new SqlParameter(""{0}"",SqlDbType.Int){{ Value = (int)entity.{0}}});"
                        , field.PropertyName);
                    continue;
                }
                switch (field.CsType)
                {
                    case "String":
                    case "string":
                        code.AppendFormat(@"
            {3}isNull = string.IsNullOrWhiteSpace({1});
            {3}parameter = new SqlParameter(""{0}"",SqlDbType.NVarChar , isNull ? 10 : ({1}).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = {2};
            cmd.Parameters.Add(parameter);"
                            , field.PropertyName
                            , this.PropertyName(field, "entity.")
                            , this.PropertyName2(field, "entity.")
                            , isFirstNull ? "var " : "");
                        break;
                    case "byte[]":
                    case "Byte[]":
                        code.AppendFormat(@"
            {3}isNull = {1} == null || {1}.Length == 0;
            {3}parameter = new SqlParameter(""{0}"",SqlDbType.VarBinary , isNull ? 10 : {1}.Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = {2};
            cmd.Parameters.Add(parameter);"
                            , field.PropertyName
                            , this.PropertyName(field, "entity.")
                            , this.PropertyName2(field, "entity.")
                            , isFirstNull ? "var " : "");
                        break;
                    case "DateTime":
                        field.DbNullable = true;
                        if (field.Nullable)
                        {
                            code.AppendFormat(@"
            {1}isNull = entity.{0} == null || entity.{0}.Value.Year < 1900;
            {1}parameter = new SqlParameter(""{0}"",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.{0};
            cmd.Parameters.Add(parameter);"
                                , field.PropertyName
                                , isFirstNull ? "var " : "");
                        }
                        else
                        {
                            code.AppendFormat(@"
            {1}isNull = entity.{0}.Year < 1900;
            {1}parameter = new SqlParameter(""{0}"",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.{0};
            cmd.Parameters.Add(parameter);"
                                , field.PropertyName
                                , isFirstNull ? "var " : "");
                        }
                        break;
                    default:
                        if (!field.Nullable)
                        {
                            code.AppendFormat(@"
            cmd.Parameters.Add(new SqlParameter(""{0}"",SqlDbType.{1}){{ Value = entity.{0}}});"
                                , field.PropertyName
                                , ToSqlDbType(field.CsType));
                            continue;
                        }
                        code.AppendFormat(@"
            {0}isNull = entity.{1} == null;
            {0}parameter = new SqlParameter(""{1}"",SqlDbType.{2});
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = {3};
            cmd.Parameters.Add(parameter);"
                            , isFirstNull ? "var " : ""
                            , field.PropertyName
                            , ToSqlDbType(field.CsType)
                            , this.PropertyName2(field, "entity."));
                        break;
                }
                if (isFirstNull)
                {
                    isFirstNull = false;
                }
            }
            code.Append(@"
        }");
            return code.ToString();
        }

        private string InsertCode()
        {
            return$@"

        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name=""entity"">实体对象</param>
        /// <param name=""cmd"">命令</param>
        /// <returns>返回真说明要取主键</returns>
        protected sealed override bool SetInsertCommand({this.Entity.EntityName} entity, SqlCommand cmd)
        {{
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return {(this.Entity.PrimaryColumn.IsIdentity).ToString().ToLower()};
        }}";
        }

        private string UpdateCode()
        {
            return$@"

        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name=""entity"">实体对象</param>
        /// <param name=""cmd"">命令</param>
        protected sealed override void SetUpdateCommand({this.Entity.EntityName} entity, SqlCommand cmd)
        {{
            cmd.CommandText = {(this.Entity.UpdateByModified ? "GetModifiedSqlCode(entity)" : "UpdateSqlCode")};
            CreateFullSqlParameter(entity,cmd);
        }}";
        }

        private string GetDbTypeCode()
        {
            var code = new StringBuilder();
            foreach (var field in this.Entity.DbFields)
            {
                code.AppendFormat(@"
                case ""{0}"":
                    return SqlDbType.{1};"
                    , field.PropertyName
                    , ToSqlDbType(field.CsType));
            }

            return$@"
        /// <summary>
        /// 得到字段的DbType类型
        /// </summary>
        /// <param name=""field"">字段名称</param>
        /// <returns>参数</returns>
        protected sealed override SqlDbType GetDbType(string field)
        {{
            switch (field)
            {{{code}
            }}
            return SqlDbType.NVarChar;
        }}";
        }

        private string LoadEntityCode()
        {
            var code = new StringBuilder();
            code.AppendFormat(@"

        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name=""reader"">数据读取器</param>
        /// <param name=""entity"">读取数据的实体</param>
        protected sealed override void LoadEntity(SqlDataReader reader,{0} entity)
        {{
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {{"
                , this.Entity.EntityName);
            var idx = 0;
            foreach (var field in this.Entity.DbFields)
            {
                if (!string.IsNullOrWhiteSpace(field.CustomType))
                {
                    code.AppendFormat(@"
                if (!reader.IsDBNull({2}))
                    entity._{0} = ({1})reader.GetInt32({2});"
                        , field.PropertyName.ToLower()
                        , field.CustomType
                        , idx++);
                    continue;
                }
                switch (field.CsType.ToLower())
                {
                    case "byte[]":
                        code.AppendFormat(@"
                if (!reader.IsDBNull({1}))
                    entity._{0} = reader.GetSqlBinary({1}).Value;"
                            , field.PropertyName.ToLower()
                            , idx++);
                        continue;
                    case "string":
                        if (field.DbType.ToLower() == "nvarchar")
                        {
                            code.AppendFormat(@"
                if (!reader.IsDBNull({2}))
                    entity._{0} = {1}({2});"
                                , field.PropertyName.ToLower()
                                , CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)
                                , idx++);
                        }
                        else
                        {
                            code.AppendFormat(@"
                if (!reader.IsDBNull({2}))
                    entity._{0} = {1}({2}).ToString();"
                                , field.PropertyName.ToLower()
                                , CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)
                                , idx++);
                        }
                        continue;
                    case "decimal":
                        if (field.DbNullable)
                        {
                            code.AppendFormat(@"
                if (!reader.IsDBNull({2}))
                    entity._{0} = (decimal){1}({2});"
                                , field.PropertyName.ToLower()
                                , CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)
                                , idx++);
                        }
                        else
                        {
                            code.AppendFormat(@"
                entity._{0} = (decimal){1}({2});"
                                , field.PropertyName.ToLower()
                                , CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)
                                , idx++);
                        }
                        continue;
                }
                if (field.DbNullable)
                {
                    if (string.Equals(field.CsType, field.DbType, StringComparison.OrdinalIgnoreCase))
                    {
                        code.AppendFormat(@"
                if (!reader.IsDBNull({2}))
                    entity._{0} = {1}({2});"
                            , field.PropertyName.ToLower()
                            , CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)
                            , idx++);
                    }
                    else if (field.CsType.ToLower() == "bool" && field.DbType.ToLower() == "int")
                    {
                        code.AppendFormat(@"
                if (!reader.IsDBNull({2}))
                    entity._{0} = {1}({2}) == 1;"
                            , field.PropertyName.ToLower()
                            , CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)
                            , idx++);
                    }
                    else if (field.CsType.ToLower() == "decimal")
                    {
                        code.AppendFormat(@"
                if (!reader.IsDBNull({2}))
                    entity._{0} = new decimal({1}({2}));"
                            , field.PropertyName.ToLower()
                            , CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)
                            , idx++);
                    }
                    else
                    {
                        code.AppendFormat(@"
                if (!reader.IsDBNull({3}))
                    entity._{0} = ({1}){2}({3});"
                            , field.PropertyName.ToLower()
                            , field.CsType
                            , CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)
                            , idx++);
                    }
                }
                else
                {
                    if (field.CsType.ToLower() == field.DbType.ToLower())
                    {
                        code.AppendFormat(@"
                entity._{0} = ({3}){1}({2});"
                            , field.PropertyName.ToLower()
                            , CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)
                            , idx++
                            , field.CustomType ?? field.CsType);
                    }
                    else if (field.CsType.ToLower() == "bool" && field.DbType.ToLower() == "int")
                    {
                        code.AppendFormat(@"
                entity._{0} = {1}({2}) == 1;"
                            , field.PropertyName.ToLower()
                            , CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)
                            , idx++);
                    }
                    else if (field.CsType.ToLower() == "decimal")
                    {
                        code.AppendFormat(@"
                entity._{0} = new decimal({1}({2}));"
                            , field.PropertyName.ToLower()
                            , CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)
                            , idx++);
                    }
                    else
                    {
                        code.AppendFormat(@"
                entity._{0} = ({1}){2}({3});"
                            , field.PropertyName.ToLower()
                            , field.CustomType ?? field.CsType
                            , CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)
                            , idx++);
                    }
                }
            }
            code.Append(@"
            }
        }");
            return code.ToString();
        }

        /// <summary>
        ///     从C#的类型转为DBType
        /// </summary>
        /// <param name="csharpType"> </param>
        public static SqlDbType ToSqlDbType(string csharpType)
        {
            switch (csharpType)
            {
                case "Boolean":
                case "bool":
                    return SqlDbType.Bit;
                case "byte":
                case "Byte":
                case "sbyte":
                case "SByte":
                case "Char":
                case "char":
                    return SqlDbType.Char;
                case "short":
                case "Int16":
                case "ushort":
                case "UInt16":
                    return SqlDbType.SmallInt;
                case "int":
                case "Int32":
                case "IntPtr":
                case "uint":
                case "UInt32":
                case "UIntPtr":
                    return SqlDbType.Int;
                case "long":
                case "Int64":
                case "ulong":
                case "UInt64":
                    return SqlDbType.BigInt;
                case "decimal":
                case "Decimal":
                case "float":
                case "Float":
                case "double":
                case "Double":
                    return SqlDbType.Decimal;
                case "Guid":
                    return SqlDbType.UniqueIdentifier;
                case "DateTime":
                    return SqlDbType.DateTime;
                case "String":
                case "string":
                    return SqlDbType.NVarChar;
                case "Binary":
                case "byte[]":
                case "Byte[]":
                    return SqlDbType.Binary;
                default:
                    return SqlDbType.Binary;
            }
        }
    }
}