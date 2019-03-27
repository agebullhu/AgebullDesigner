using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class SqlServerAccessBuilder : CoderWithEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_SqlServerAccess";

        private string SqlCode()
        {
            return $@"
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId
        {{
            get {{ return {Project.DataBaseObjectName}.Table_{Entity.Name}; }}
        }}

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {{
            get
            {{
                return @""{Entity.ReadTableName}"";
            }}
        }}

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {{
            get
            {{
                return @""{Entity.SaveTable}"";
            }}
        }}

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey
        {{
            get
            {{
                return @""{Entity.PrimaryColumn.PropertyName}"";
            }}
        }}

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {{
            get
            {{
                return @""{FullLoadSql()}"";
            }}
        }}

        

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode
        {{
            get
            {{
                return @""{InsertSql()}"";
            }}
        }}

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode
        {{
            get
            {{
                return @""{UpdateSql()}"";
            }}
        }}

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode({Entity.EntityName} data)
        {{
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return "";"";
            StringBuilder sql = new StringBuilder();{UpdateSqlByModify()}
            return sql.ToString();
        }}

        #endregion
";
        }

        private string FieldCode()
        {
            return $@"
        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{{ {Fields()} }};

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
        {{{FieldMap()}
        }};

        /// <summary>
        ///  字段字典
        /// </summary>
        public sealed override Dictionary<string, string> FieldMap
        {{
            get {{ return fieldMap ; }}
        }}
        #endregion";
        }

        private string CreateCode()
        {
            var innerCode = $@"{SqlCode()}{FieldCode()}

        #region 方法实现
{LoadEntityCode()}
{GetDbTypeCode()}
{CreateFullSqlParameter()}
{UpdateCode()}
{InsertCode()}
        #endregion
";

            return $@"
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;
using Agebull.Common;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.SqlServer;
using Agebull.EntityModel.Redis;
using Agebull.EntityModel.EasyUI;

{Project.UsingNameSpaces}

namespace {NameSpace}.DataAccess
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    {(Entity.IsInternal ? "internal" : "public")} partial class {Entity.Name}DataAccess
    {{{null}{innerCode}
    }}

    sealed partial class {Project.DataBaseObjectName}
    {{
{TableSql()}
{TableObject()}
{TablesEnum()}
    }}
}}
";
        }

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            var file = Path.Combine(path, Entity.Name + "DataAccess.Designer.cs");
            if (Entity.NoDataBase)
            {
                if (File.Exists(file))
                {
                    Directory.Move(file, file + ".bak");
                }
                return;
            }
            SaveCode(file, CreateCode());
        }
        

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            var file = Path.Combine(path, Entity.Name + "DataAccess.cs");
            if (Entity.NoDataBase)
            {
                if (File.Exists(file))
                {
                    Directory.Move(file, file + ".bak");
                }
                return;
            }
            var code = $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Agebull.EntityModel.SqlServer;

namespace {NameSpace}.DataAccess
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    sealed partial class {Entity.Name}DataAccess : SqlServerTable<{Entity.EntityName},{Project.DataBaseObjectName}>
    {{

    }}
}}
";
            SaveCode(file, code);
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
            var name = Entity.Name.ToPluralism();
            return $@"

        /// <summary>
        /// {Entity.Description}数据访问对象
        /// </summary>
        private {Entity.Name}DataAccess _{name.ToLWord()};

        /// <summary>
        /// {Entity.Description}数据访问对象
        /// </summary>
        {(Entity.IsInternal ? "internal" : "public")} {Entity.Name}DataAccess {name}
        {{
            get
            {{
                return this._{name.ToLWord()} ?? ( this._{name.ToLWord()} = new {Entity.Name}DataAccess{{ DataBase = this}});
            }}
        }}";
        }

        private string TableSql()
        {
            return $@"

        /// <summary>
        /// {Entity.Description}的结构语句
        /// </summary>
        private TableSql _{Entity.ReadTableName}Sql = new TableSql
        {{
            TableName = ""{Entity.ReadTableName}"",
            PimaryKey = ""{Entity.PrimaryColumn.PropertyName}""
        }};";
        }

        private string Fields()
        {
            var sql = new StringBuilder();
            var isFirst = true;
            foreach (var field in Entity.DbFields)
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
            FieldMap(Entity, sql, names, ref isFirst);
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
                FieldMap(Project.Entities.FirstOrDefault(p => p.EntityName == table.ModelBase), sql, names, ref isFirst);
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
                sql.Append($@"
            {{ ""{field.PropertyName}"" , ""{field.DbFieldName}"" }}");
                names.Add(field.PropertyName);

                var alias = field.GetAliasPropertys();
                foreach (var a in alias)
                {
                    if (names.Contains(a))
                    {
                        continue;
                    }
                    names.Add(a);
                    sql.Append($@",
            {{ ""{a}"" , ""{field.DbFieldName}"" }}");
                }
            }
            if (!table.DbFields.Any(p => p.PropertyName.Equals("Id", StringComparison.OrdinalIgnoreCase)))
            {
                sql.Append($@",
            {{ ""Id"" , ""{table.PrimaryColumn.DbFieldName}"" }}");
            }
        }

        private string FullLoadSql()
        {
            var sql = new StringBuilder();

            var isFirst = true;
            foreach (var field in Entity.DbFields)
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
    [{0}] AS [{1}]", field.DbFieldName, field.PropertyName);
            }
            return sql.ToString();
        }

        /// <summary>
        /// 字段唯一条件(主键或组合键)
        /// </summary>
        /// <returns></returns>
        private string UniqueCondition()
        {
            if (!Entity.DbFields.Any(p => p.UniqueIndex > 0))
                return $@"[{Entity.PrimaryColumn.DbFieldName}] = @{Entity.PrimaryColumn.PropertyName}";

            var code = new StringBuilder();
            var uniqueFields = Entity.DbFields.Where(p => p.UniqueIndex > 0).OrderBy(p => p.UniqueIndex).ToArray();
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
                code.AppendFormat("{0}=@{1}", col.DbFieldName, col.PropertyName);
            }
            return code.ToString();
        }
        private string InsertSql()
        {
            if (!Entity.DbFields.Any(p => p.UniqueIndex > 0))
            {
                return OnlyInsertSql();
            }
            var code = new StringBuilder();
            code.Append($@"
DECLARE @__myId INT;
SELECT @__myId = [{Entity.PrimaryColumn.DbFieldName}] FROM [{{ContextWriteTable}}] WHERE {UniqueCondition()}");

            code.Append($@"
IF @__myId IS NULL
BEGIN{OnlyInsertSql(true)}
    SET @__myId = {(Entity.PrimaryColumn.IsIdentity ? "SCOPE_IDENTITY()" : ("@"+ Entity.PrimaryColumn.DbFieldName))};
END
ELSE
BEGIN
    SET @{Entity.PrimaryColumn.PropertyName}=@__myId;{UpdateSql(true)}
END
SELECT @__myId;");
            return code.ToString();
        }

        private string OnlyInsertSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            var columns = Entity.DbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)).ToArray();
            sql.Append(@"
INSERT INTO [{ContextWriteTable}]
(");
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
                sql.Append($@"
    [{field.DbFieldName}]");
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
                sql.Append($@"
    @{field.PropertyName}");
            }
            sql.Append(@"
);");
            if (isInner)
            {
                sql.Replace("\n", "\n    ");
            }
            else if (Entity.PrimaryColumn.IsIdentity)
            {
                sql.Append(@"
SELECT SCOPE_IDENTITY();");
            }

            return sql.ToString();
        }

        private string UpdateSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            IEnumerable<PropertyConfig> columns = Entity.DbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
            sql.Append(@"
UPDATE [{ContextWriteTable}] SET");
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
                sql.Append($@"
       [{field.DbFieldName}] = @{field.PropertyName}");
            }
            sql.Append($@"
 WHERE {UniqueCondition()};");
            if (isInner)
            {
                sql.Replace("\n", "\n    ");
            }
            return sql.ToString();
        }

        private string UpdateSqlByModify()
        {
            var code = new StringBuilder();
            IEnumerable<PropertyConfig> columns = Entity.DbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
            code.Append(@"
            sql.AppendLine(""UPDATE [{ContextWriteTable}] SET"");");


            foreach (var field in columns)
            {
                code.Append($@"
            //{field.Caption}
            if (data.__EntityStatus.ModifiedProperties[{Entity.EntityName}._DataStruct_.Real_{field.PropertyName}] > 0)
                sql.AppendLine(""       [{field.DbFieldName}] = @{field.PropertyName}"");");
            }
            code.Append($@"
            sql.Append("" WHERE {UniqueCondition()};"");");
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
        public void CreateFullSqlParameter({Entity.EntityName} entity, SqlCommand cmd)
        {{");

            var isFirstNull = true;
            foreach (var field in Entity.DbFields.OrderBy(p => p.Index))
            {
                code.Append($@"
            //{field.Index + 1:D2}:{field.Caption}({field.PropertyName})");
                if (!string.IsNullOrWhiteSpace(field.CustomType))
                {
                    code.Append($@"
            cmd.Parameters.Add(new SqlParameter(""{field.PropertyName}"",SqlDbType.Int){{ Value = (int)entity.{field.PropertyName}}});");
                    continue;
                }
                switch (field.CsType)
                {
                    case "String":
                    case "string":
                        code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = string.IsNullOrWhiteSpace({PropertyName(field, "entity.")});
            {(isFirstNull ? "var " : "")}parameter = new SqlParameter(""{field.PropertyName}"",SqlDbType.NVarChar , isNull ? 10 : ({PropertyName(field, "entity.")}).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = {PropertyName2(field, "entity.")};
            cmd.Parameters.Add(parameter);");
                        break;
                    case "byte[]":
                    case "Byte[]":
                        code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = {PropertyName(field, "entity.")} == null || {PropertyName(field, "entity.")}.Length == 0;
            {(isFirstNull ? "var " : "")}parameter = new SqlParameter(""{field.PropertyName}"",SqlDbType.VarBinary , isNull ? 10 : {PropertyName(field, "entity.")}.Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = {PropertyName2(field, "entity.")};
            cmd.Parameters.Add(parameter);");
                        break;
                    case "DateTime":
                        field.DbNullable = true;
                        code.Append(field.Nullable
                                ? string.Format($@"
            {(isFirstNull ? "var " : "")}isNull = entity.{field.PropertyName} == null || entity.{field.PropertyName}.Value.Year < 1900;
            {(isFirstNull ? "var " : "")}parameter = new SqlParameter(""{field.PropertyName}"",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.{field.PropertyName};
            cmd.Parameters.Add(parameter);")
                                : string.Format($@"
            {(isFirstNull ? "var " : "")}isNull = entity.{field.PropertyName}.Year < 1900;
            {(isFirstNull ? "var " : "")}parameter = new SqlParameter(""{field.PropertyName}"",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.{field.PropertyName};
            cmd.Parameters.Add(parameter);)"));
                        break;
                    default:
                        if (!field.Nullable)
                        {
                            code.Append($@"
            cmd.Parameters.Add(new SqlParameter(""{field.PropertyName}"",SqlDbType.{ToSqlDbType(field.CsType)}){{ Value = entity.{field.PropertyName}}});");
                            continue;
                        }
                        code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = entity.{field.PropertyName} == null;
            {(isFirstNull ? "var " : "")}parameter = new SqlParameter(""{field.PropertyName}"",SqlDbType.{ToSqlDbType(field.CsType)});
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = {PropertyName2(field, "entity.")};
            cmd.Parameters.Add(parameter);");
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
        protected sealed override bool SetInsertCommand({Entity.EntityName} entity, SqlCommand cmd)
        {{
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return {Entity.PrimaryColumn.IsIdentity.ToString().ToLower()};
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
        protected sealed override void SetUpdateCommand({Entity.EntityName} entity, SqlCommand cmd)
        {{
            cmd.CommandText = {(Entity.UpdateByModified ? "GetModifiedSqlCode(entity)" : "UpdateSqlCode")};
            CreateFullSqlParameter(entity,cmd);
        }}";
        }

        private string GetDbTypeCode()
        {
            var code = new StringBuilder();
            foreach (var field in Entity.DbFields)
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
                , Entity.EntityName);
            var idx = 0;
            foreach (var field in Entity.DbFields)
            {
                string fieldName = field.PropertyName.ToLWord();
                if (!string.IsNullOrWhiteSpace(field.CustomType))
                {
                    code.AppendFormat(@"
                if (!reader.IsDBNull({2}))
                    entity._{0} = ({1})reader.GetInt32({2});"
                        , fieldName
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
                            , fieldName
                            , idx++);
                        continue;
                    case "string":
                        code.AppendFormat(field.DbType?.ToLower() == "nvarchar"
                                ? @"
                if (!reader.IsDBNull({2}))
                    entity._{0} = {1}({2});"
                                : @"
                if (!reader.IsDBNull({2}))
                    entity._{0} = {1}({2}).ToString();"
                            , fieldName
                            , GetDBReaderFunctionName(field.DbType)
                            , idx++);
                        continue;
                    case "decimal":
                        code.AppendFormat(/*field.DbNullable? */
                                @"
                if (!reader.IsDBNull({2}))
                    entity._{0} = (decimal){1}({2});"
                                /*: @"
                entity._{0} = (decimal){1}({2});"*/
                            , fieldName
                            , GetDBReaderFunctionName(field.DbType)
                            , idx++);
                        continue;
                }
                //if (field.DbNullable)
                {
                    if (string.Equals(field.CsType, field.DbType, StringComparison.OrdinalIgnoreCase))
                    {
                        code.AppendFormat(@"
                if (!reader.IsDBNull({2}))
                    entity._{0} = {1}({2});"
                            , fieldName
                            , GetDBReaderFunctionName(field.DbType)
                            , idx++);
                    }
                    else if (field.CsType.ToLower() == "bool" && field.DbType.ToLower() == "int")
                    {
                        code.AppendFormat(@"
                if (!reader.IsDBNull({2}))
                    entity._{0} = {1}({2}) == 1;"
                            , fieldName
                            , GetDBReaderFunctionName(field.DbType)
                            , idx++);
                    }
                    else if (field.CsType.ToLower() == "decimal")
                    {
                        code.AppendFormat(@"
                if (!reader.IsDBNull({2}))
                    entity._{0} = new decimal({1}({2}));"
                            , fieldName
                            , GetDBReaderFunctionName(field.DbType)
                            , idx++);
                    }
                    else
                    {
                        code.AppendFormat(@"
                if (!reader.IsDBNull({3}))
                    entity._{0} = ({1}){2}({3});"
                            , fieldName
                            , field.CsType
                            , GetDBReaderFunctionName(field.DbType)
                            , idx++);
                    }
                }
                /*else
                {
                    if (field.CsType.ToLower() == field.DbType.ToLower())
                    {
                        code.AppendFormat(@"
                entity._{0} = ({3}){1}({2});"
                            , fieldName
                            , CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)
                            , idx++
                            , field.CustomType ?? field.CsType);
                    }
                    else if (field.CsType.ToLower() == "bool" && field.DbType.ToLower() == "int")
                    {
                        code.Append($@"
                entity._{fieldName} = {CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)}({idx++}) == 1;");
                    }
                    else if (field.CsType.ToLower() == "decimal")
                    {
                        code.Append($@"
                entity._{fieldName} = new decimal({CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)}({idx++}));");
                    }
                    else
                    {
                        code.Append($@"
                entity._{fieldName} = ({field.CustomType ?? field.CsType}){CodeBuilderDefault.GetDBReaderFunctionName(field.DbType)}({idx++});");
                    }
                }*/
            }
            code.Append(@"
            }
        }");
            return code.ToString();
        }

        /// <summary>
        ///     取得对应类型的DBReader的方法名称
        /// </summary>
        /// <param name="csharpType">C#的类型</param>
        /// <param name="readerName">读取器的名称</param>
        /// <returns>读取方法名</returns>
        public static string GetDBReaderFunctionName(string csharpType, string readerName = "reader")
        {
            switch (csharpType.ToLower())
            {
                case "bit":
                case "bool":
                case "boolean":
                    return readerName + ".GetBoolean";

                case "byte":
                    return readerName + ".GetByte";

                case "byte[]":
                case "binary":
                    return readerName + ".GetBytes";
                case "char":
                    return readerName + ".GetChar";

                case "short":
                case "int16":
                    return readerName + ".GetInt16";

                case "int":
                case "int32":
                    return readerName + ".GetInt32";

                case "bigint":
                case "long":
                case "int64":
                    return readerName + ".GetInt64";

                case "datetime":
                case "datetime2":
                    return readerName + ".GetDateTime";

                case "decimal":
                case "numeric":
                    return readerName + ".GetDecimal";

                case "real":
                case "float":
                    return $"(float){readerName}.GetDouble";
                case "double":
                    return readerName + ".GetDouble";

                case "guid":
                case "uniqueidentifier":
                    return readerName + ".GetGuid";

                case "nchar":
                case "varchar":
                case "nvarchar":
                case "string":
                case "text":
                case "longtext":

                    return readerName + ".GetString";

                default:
                    return $"/*({csharpType})*/{readerName}.GetValue";
            }
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