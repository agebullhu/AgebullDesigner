using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.Sqlite;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class SqliteAccessBuilder : AccessBuilderBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_SqliteAccess";

        private string SqlCode()
        {
            return $@"
        #region 基本SQL语句

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @""{Entity.ReadTableName}"";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @""{Entity.SaveTable}"";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @""{Entity.PrimaryColumn.PropertyName}"";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @""{FullLoadSql()}"";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@""{InsertSql()}"";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@""{UpdateSql()}"";

        #endregion
";
        }

        private string CreateBaseCode()
        {
            return $@"
using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Sqlite;

{Project.UsingNameSpaces}
using {SolutionConfig.Current.NameSpace}.DataAccess;

namespace {NameSpace}.DataAccess
{{
    /// <summary>
    /// {Entity.Caption}
    /// </summary>
    {(Entity.IsInternal ? "internal" : "public")} partial class {Entity.Name}DataAccess
    {{
        /// <summary>
        /// 构造
        /// </summary>
        public {Entity.Name}DataAccess()
        {{
            Name = {Entity.EntityName}._DataStruct_.EntityName;
            Caption = {Entity.EntityName}._DataStruct_.EntityCaption;
            Description = {Entity.EntityName}._DataStruct_.EntityDescription;
        }}
{SqlCode()}
{FieldCode()}
        #region 方法实现
{LoadEntityCode()}
{GetDbTypeCode()}
{CreateFullSqlParameter()}
{UpdateCode()}
{InsertCode()}
        #endregion
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
                else if (!string.IsNullOrWhiteSpace(Entity.Alias))
                {
                    var oldFile = Path.Combine(path, Entity.Alias + "DataAccess.Designer.cs");
                    if (File.Exists(oldFile))
                    {
                        Directory.Move(oldFile, file + ".bak");
                    }
                }
                return;
            }
            if (!string.IsNullOrWhiteSpace(Entity.Alias))
            {
                var oldFile = Path.Combine(path, Entity.Alias + "DataAccess.Designer.cs");
                if (File.Exists(oldFile))
                {
                    Directory.Move(oldFile, file);
                }
            }
            SaveCode(file, CreateBaseCode());
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
                else if (!string.IsNullOrWhiteSpace(Entity.Alias))
                {
                    var oldFile = Path.Combine(path, Entity.Alias + "DataAccess.cs");
                    if (File.Exists(oldFile))
                    {
                        Directory.Move(oldFile, file + ".bak");
                    }
                }
                return;
            }
            if (!string.IsNullOrWhiteSpace(Entity.Alias))
            {
                var oldFile = Path.Combine(path, Entity.Alias + "DataAccess.cs");
                if (File.Exists(oldFile))
                {
                    Directory.Move(oldFile, file);
                }
            }

            var baseClass = "SqliteTable";
            if (Entity.Interfaces?.Contains("IStateData") ?? false)
            {
                baseClass = "DataStateTable";
            }
            var code = $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Data.Sqlite;
using Agebull.EntityModel.Sqlite;

using {SolutionConfig.Current.NameSpace}.DataAccess;

namespace {NameSpace}.DataAccess
{{
    /// <summary>
    /// {Entity.Caption}
    /// </summary>
    partial class {Entity.Name}DataAccess : {baseClass}<{Entity.EntityName},{Project.DataBaseObjectName}>
    {{
    }}
}}
";
            SaveCode(file, code);
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
                sql.Append($@"
    [{field.DbFieldName}] AS [{field.PropertyName}]");
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
            if (Entity.PrimaryColumn.IsIdentity)
            {
                sql.Append(@"
SELECT last_insert_rowid();");
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
        public void CreateFullSqlParameter({Entity.EntityName} entity, SqliteCommand cmd)
        {{");

            foreach (var field in Entity.DbFields.OrderBy(p => p.Index))
            {
                bool checkNull = field.Nullable;
                string nullCondition = $"entity.{field.PropertyName} == null";
                string pre = "";
                if (!string.IsNullOrWhiteSpace(field.CustomType))
                {
                    pre = "(int)";
                }

                switch (field.CsType)
                {
                    case "String":
                    case "string":
                    case "byte[]":
                    case "Byte[]":
                        checkNull = true;
                        break;
                    case "DateTime":
                        checkNull = true;
                        if (field.Nullable)
                            nullCondition = $"entity.{field.PropertyName} == null || entity.{field.PropertyName}.Value == DateTime.MinValue";
                        else
                            nullCondition = $"entity.{field.PropertyName} == DateTime.MinValue";
                        break;
                }
                if (checkNull)
                    code.Append($@"
            //{field.Index + 1:D2}:{field.Caption}
            cmd.Parameters.Add(new SqliteParameter(""{field.PropertyName}"" , {nullCondition} ? (object)DBNull.Value : {pre}entity.{field.PropertyName}));");
                else
                    code.Append($@"
            //{field.Index + 1:D2}:{field.Caption}
            cmd.Parameters.Add(new SqliteParameter(""{field.PropertyName}"" , {pre}entity.{field.PropertyName}));");
            }
            code.Append(@"
        }");
            return code.ToString();
        }

        private string InsertCode()
        {
            return $@"

        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name=""entity"">实体对象</param>
        /// <param name=""cmd"">命令</param>
        /// <returns>返回真说明要取主键</returns>
        protected sealed override bool SetInsertCommand({Entity.EntityName} entity, SqliteCommand cmd)
        {{
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return {Entity.PrimaryColumn.IsIdentity.ToString().ToLower()};
        }}";
        }

        private string UpdateCode()
        {
            return $@"

        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name=""entity"">实体对象</param>
        /// <param name=""cmd"">命令</param>
        protected sealed override void SetUpdateCommand({Entity.EntityName} entity, SqliteCommand cmd)
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
                var type = SqliteHelper.ToSqlDbType(field);
                if (type == Microsoft.Data.Sqlite.SqliteType.Text)
                    continue;
                if (field.DbFieldName != field.Name)
                    code.Append($@"
                case ""{field.DbFieldName}"":");
                code.Append($@"
                case ""{field.PropertyName}"":
                     return SqliteType.{type};");
            }

            return $@"
        /// <summary>
        /// 得到字段的DbType类型
        /// </summary>
        /// <param name=""field"">字段名称</param>
        /// <returns>参数</returns>
        protected sealed override SqliteType GetDbType(string field)
        {{
            switch (field)
            {{{code}
            }}
            return SqliteType.Text;
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
        protected sealed override void LoadEntity(SqliteDataReader reader,{0} entity)
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
                        code.AppendFormat(field.DbType?.ToLower() == "text"
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
                    return readerName + ".GetFloat";
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
    }
}