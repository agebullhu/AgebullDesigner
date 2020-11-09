using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.SqlServer;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class SqlServerAccessBuilder<TModel> : AccessBuilderBase<TModel>
        where TModel : ProjectChildConfigBase, IEntityConfig
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
        public override int TableId => 0x{Model.Identity:X};//{Project.DataBaseObjectName}.Table_{Model.Name};

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @""{Model.ReadTableName}"";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @""{Model.SaveTableName}"";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @""{Model.PrimaryColumn.Name}"";

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

        /*// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode({Model.EntityName} data)
        {{
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return "";"";
            StringBuilder sql = new StringBuilder();{UpdateSqlByModify()}
            return sql.ToString();
        }}*/

        #endregion
";
        }

        private string CreateBaseCode()
        {
            StringBuilder alias = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Model.Alias))
            {
                alias.Append($@"
    /// <summary>
    /// {Model.Description} 别名
    /// </summary>
    {(Model.IsInternal ? "internal" : "public")} sealed class {Model.Alias}DataAccess : {Model.Name}DataAccess
    {{
    }}");
            }
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

{Project.UsingNameSpaces}
using {Project.NameSpace}.DataAccess;

namespace {NameSpace}.DataAccess
{{
    /// <summary>
    /// {Model.Caption}
    /// </summary>
    {(Model.IsInternal ? "internal" : "public")} partial class {Model.Name}DataAccess
    {{
        /// <summary>
        /// 构造
        /// </summary>
        public {Model.Name}DataAccess()
        {{
            Name = {Model.EntityName}._DataStruct_.EntityName;
            Caption = {Model.EntityName}._DataStruct_.EntityCaption;
            Description = {Model.EntityName}._DataStruct_.EntityDescription;
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
    }}{alias}
{DataBaseExtend()}
}}
";
        }

        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            var file = Path.Combine(path, Model.Name + "DataAccess.Designer.cs");
            if (Model.NoDataBase)
            {
                if (File.Exists(file))
                {
                    Directory.Move(file, file + ".bak");
                }
                else if (!string.IsNullOrWhiteSpace(Model.Alias))
                {
                    var oldFile = Path.Combine(path, Model.Alias + "DataAccess.Designer.cs");
                    if (File.Exists(oldFile))
                    {
                        Directory.Move(oldFile, file + ".bak");
                    }
                }
                return;
            }
            if (!string.IsNullOrWhiteSpace(Model.Alias))
            {
                var oldFile = Path.Combine(path, Model.Alias + "DataAccess.Designer.cs");
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
        protected override void CreateCustomCode(string path)
        {
            var file = Path.Combine(path, Model.Name + "DataAccess.cs");
            if (Model.NoDataBase)
            {
                if (File.Exists(file))
                {
                    Directory.Move(file, file + ".bak");
                }
                else if (!string.IsNullOrWhiteSpace(Model.Alias))
                {
                    var oldFile = Path.Combine(path, Model.Alias + "DataAccess.cs");
                    if (File.Exists(oldFile))
                    {
                        Directory.Move(oldFile, file + ".bak");
                    }
                }
                return;
            }
            if (!string.IsNullOrWhiteSpace(Model.Alias))
            {
                var oldFile = Path.Combine(path, Model.Alias + "DataAccess.cs");
                if (File.Exists(oldFile))
                {
                    Directory.Move(oldFile, file);
                }
            }

            var baseClass = "SqlServerTable";
            if (Model.Interfaces?.Contains("IStateData") ?? false)
            {
                baseClass = "DataStateTable";
            }
            var code = $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Agebull.EntityModel.SqlServer;
using {Project.NameSpace}.DataAccess;

namespace {NameSpace}.DataAccess
{{
    /// <summary>
    /// {Model.Caption}
    /// </summary>
    partial class {Model.Name}DataAccess : {baseClass}<{Model.EntityName},{Project.DataBaseObjectName}>
    {{
        /// <summary>
        /// 构造单个读取命令
        /// </summary>
        /// <summary>编译查询条件</summary>
        /// <param name=""lambda"">条件</param>
        /// <returns>命令对象</returns>
        public SqlCommand CreateCommand(Expression<Func<{Model.EntityName}, bool>> lambda)
        {{
            var convert = Compile(lambda);
            return CreateOnceCommand(convert.ConditionSql, KeyField, false, convert.Parameters);
        }}
    }}
}}
";
            SaveCode(file, code);
        }


        private string FullLoadSql()
        {
            var sql = new StringBuilder();

            var isFirst = true;
            foreach (var property in Model.DbFields)
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
    [{property.DbFieldName}] AS [{property.Name}]");
            }
            return sql.ToString();
        }

        /// <summary>
        /// 字段唯一条件(主键或组合键)
        /// </summary>
        /// <returns></returns>
        private string UniqueCondition()
        {
            if (!Model.DbFields.Any(p => p.UniqueIndex))
                return $@"[{Model.PrimaryColumn.DbFieldName}] = @{Model.PrimaryColumn.Name}";

            var code = new StringBuilder();
            var uniqueFields = Model.DbFields.Where(p => p.UniqueIndex).OrderBy(p => p.UniqueIndex).ToArray();
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
                code.AppendFormat("{0}=@{1}", col.DbFieldName, col.Name);
            }
            return code.ToString();
        }
        private string InsertSql()
        {
            if (!Model.DbFields.Any(p => p.UniqueIndex))
            {
                return OnlyInsertSql();
            }
            var code = new StringBuilder();
            code.Append($@"
DECLARE @__myId INT;
SELECT @__myId = [{Model.PrimaryColumn.DbFieldName}] FROM [{{ContextWriteTable}}] WHERE {UniqueCondition()}");

            code.Append($@"
IF @__myId IS NULL
BEGIN{OnlyInsertSql(true)}
    SET @__myId = {(Model.PrimaryColumn.IsIdentity ? "SCOPE_IDENTITY()" : ("@" + Model.PrimaryColumn.DbFieldName))};
END
ELSE
BEGIN
    SET @{Model.PrimaryColumn.Name}=@__myId;{UpdateSql(true)}
END
SELECT @__myId;");
            return code.ToString();
        }

        private string OnlyInsertSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            var columns = Model.DbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)).ToArray();
            sql.Append(@"
INSERT INTO [{ContextWriteTable}]
(");
            var isFirst = true;
            foreach (var property in columns)
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
    [{property.DbFieldName}]");
            }
            sql.Append(@"
)
VALUES
(");
            isFirst = true;
            foreach (var property in columns)
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
    @{property.Name}");
            }
            sql.Append(@"
);");
            if (isInner)
            {
                sql.Replace("\n", "\n    ");
            }
            else if (Model.PrimaryColumn.IsIdentity)
            {
                sql.Append(@"
SELECT SCOPE_IDENTITY();");
            }

            return sql.ToString();
        }

        private string UpdateSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            var columns = Model.DbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
            sql.Append(@"
UPDATE [{ContextWriteTable}] SET");
            var isFirst = true;
            foreach (var property in columns)
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
       [{property.DbFieldName}] = @{property.Name}");
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
            var columns = Model.DbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
            code.Append(@"
            sql.AppendLine($""UPDATE [{ContextWriteTable}] SET"");");


            foreach (var property in columns)
            {
                code.Append($@"
            //{property.Caption}
            if (data.__EntityStatus.ModifiedProperties[{Model.EntityName}._DataStruct_.Real_{property.Name}] > 0)
                sql.AppendLine(""       [{property.DbFieldName}] = @{property.Name}"");");
            }
            code.Append($@"
            sql.Append("" WHERE {UniqueCondition()};"");");
            return code.ToString();
        }

        private string Name(IFieldConfig col, string pre)
        {
            return col.CustomType == null ? $"{pre}{col.Name}" : $"({col.CustomType}){pre}{col.Name}";
        }

        private string PropertyName2(IFieldConfig col, string pre)
        {
            return col.CustomType == null ? $"{pre}{col.Name}" : $"({col.CsType}){pre}{col.Name}";
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
        public void CreateFullSqlParameter({Model.EntityName} entity, SqlCommand cmd)
        {{");

            var isFirstNull = true;
            foreach (var property in Model.DbFields.OrderBy(p => p.Index))
            {
                code.Append($@"
            //{property.Index + 1:D2}:{property.Caption}({property.Name})");
                if (!string.IsNullOrWhiteSpace(property.CustomType))
                {
                    code.Append($@"
            cmd.Parameters.Add(new SqlParameter(""{property.Name}"",SqlDbType.Int){{ Value = (int)entity.{property.Name}}});");
                    continue;
                }
                switch (property.CsType)
                {
                    case "String":
                    case "string":
                        code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = string.IsNullOrWhiteSpace({Name(property, "entity.")});
            {(isFirstNull ? "var " : "")}parameter = new SqlParameter(""{property.Name}"",SqlDbType.NVarChar , isNull ? 10 : ({Name(property, "entity.")}).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = {PropertyName2(property, "entity.")};
            cmd.Parameters.Add(parameter);");
                        break;
                    case "byte[]":
                    case "Byte[]":
                        code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = {Name(property, "entity.")} == null || {Name(property, "entity.")}.Length == 0;
            {(isFirstNull ? "var " : "")}parameter = new SqlParameter(""{property.Name}"",SqlDbType.VarBinary , isNull ? 10 : {Name(property, "entity.")}.Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = {PropertyName2(property, "entity.")};
            cmd.Parameters.Add(parameter);");
                        break;
                    case "DateTime":
                        code.Append(property.Nullable
                                ? $@"
            {(isFirstNull ? "var " : "")}isNull = entity.{property.Name} == null || entity.{property.Name}.Value.Year < 1900;
            {(isFirstNull ? "var " : "")}parameter = new SqlParameter(""{property.Name}"",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.{property.Name};
            cmd.Parameters.Add(parameter);"
                                : $@"
            {(isFirstNull ? "var " : "")}isNull = entity.{property.Name}.Year < 1900;
            {(isFirstNull ? "var " : "")}parameter = new SqlParameter(""{property.Name}"",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.{property.Name};
            cmd.Parameters.Add(parameter);");
                        break;
                    default:
                        if (!property.Nullable)
                        {
                            code.Append($@"
            cmd.Parameters.Add(new SqlParameter(""{property.Name}"",SqlDbType.{SqlServerHelper.ToSqlDbType(property.DbType, property.CsType)}){{ Value = entity.{property.Name}}});");
                            continue;
                        }
                        code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = entity.{property.Name} == null;
            {(isFirstNull ? "var " : "")}parameter = new SqlParameter(""{property.Name}"",SqlDbType.{SqlServerHelper.ToSqlDbType(property.DbType, property.CsType)});
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = {PropertyName2(property, "entity.")};
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
            return $@"

        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name=""entity"">实体对象</param>
        /// <param name=""cmd"">命令</param>
        /// <returns>返回真说明要取主键</returns>
        protected sealed override bool SetInsertCommand({Model.EntityName} entity, SqlCommand cmd)
        {{
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return {Model.PrimaryColumn.IsIdentity.ToString().ToLower()};
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
        protected sealed override void SetUpdateCommand({Model.EntityName} entity, SqlCommand cmd)
        {{
            cmd.CommandText = {(Model.UpdateByModified ? "GetModifiedSqlCode(entity)" : "UpdateSqlCode")};
            CreateFullSqlParameter(entity,cmd);
        }}";
        }

        private string GetDbTypeCode()
        {
            var code = new StringBuilder();
            foreach (var property in Model.DbFields)
            {
                if (property.DbFieldName != property.Name)
                    code.Append($@"
                case ""{property.DbFieldName}"":");
                code.AppendFormat(@"
                case ""{0}"":
                    return SqlDbType.{1};"
                    , property.Name
                    , SqlServerHelper.ToSqlDbType(property.DbType, property.CsType));
            }

            return $@"
        /// <summary>
        /// 得到字段的DbType类型
        /// </summary>
        /// <param name=""property"">字段名称</param>
        /// <returns>参数</returns>
        protected sealed override SqlDbType GetDbType(string property)
        {{
            switch (property)
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
        {{"
                , Model.EntityName);
            var idx = 0;
            foreach (var property in Model.DbFields)
            {
                string fieldName = property.Name.ToLWord();
                if (!string.IsNullOrWhiteSpace(property.CustomType))
                {
                    code.AppendFormat(@"
            if (!reader.IsDBNull({2}))
                entity._{0} = ({1})reader.GetInt32({2});"
                        , fieldName
                        , property.CustomType
                        , idx++);
                    continue;
                }
                switch (property.CsType.ToLower())
                {
                    case "byte[]":
                        code.AppendFormat(@"
            if (!reader.IsDBNull({1}))
                entity._{0} = reader.GetSqlBinary({1}).Value;"
                            , fieldName
                            , idx++);
                        continue;
                    case "string":
                        code.AppendFormat(property.DbType?.ToLower() == "nvarchar"
                                ? @"
            if (!reader.IsDBNull({2}))
                entity._{0} = {1}({2});"
                                : @"
            if (!reader.IsDBNull({2}))
                entity._{0} = {1}({2}).ToString();"
                            , fieldName
                            , GetDBReaderFunctionName(property.DbType)
                            , idx++);
                        continue;
                    case "decimal":
                        code.AppendFormat(/*property.DbNullable? */
                                @"
            if (!reader.IsDBNull({2}))
                entity._{0} = (decimal){1}({2});"
                            /*: @"
            entity._{0} = (decimal){1}({2});"*/
                            , fieldName
                            , GetDBReaderFunctionName(property.DbType)
                            , idx++);
                        continue;
                }
                //if (property.DbNullable)
                {
                    if (string.Equals(property.CsType, property.DbType, StringComparison.OrdinalIgnoreCase))
                    {
                        code.AppendFormat(@"
            if (!reader.IsDBNull({2}))
                entity._{0} = {1}({2});"
                            , fieldName
                            , GetDBReaderFunctionName(property.DbType)
                            , idx++);
                    }
                    else if (property.CsType.ToLower() == "bool" && property.DbType.ToLower() == "int")
                    {
                        code.AppendFormat(@"
            if (!reader.IsDBNull({2}))
                entity._{0} = {1}({2}) == 1;"
                            , fieldName
                            , GetDBReaderFunctionName(property.DbType)
                            , idx++);
                    }
                    else if (property.CsType.ToLower() == "decimal")
                    {
                        code.AppendFormat(@"
            if (!reader.IsDBNull({2}))
                entity._{0} = new decimal({1}({2}));"
                            , fieldName
                            , GetDBReaderFunctionName(property.DbType)
                            , idx++);
                    }
                    else
                    {
                        code.AppendFormat(@"
            if (!reader.IsDBNull({3}))
                entity._{0} = ({1}){2}({3});"
                            , fieldName
                            , property.CsType
                            , GetDBReaderFunctionName(property.DbType)
                            , idx++);
                    }
                }
                /*else
                {
                    if (property.CsType.ToLower() == property.DbType.ToLower())
                    {
                        code.AppendFormat(@"
                entity._{0} = ({3}){1}({2});"
                            , fieldName
                            , CodeBuilderDefault.GetDBReaderFunctionName(property.DbType)
                            , idx++
                            , property.CustomType ?? property.CsType);
                    }
                    else if (property.CsType.ToLower() == "bool" && property.DbType.ToLower() == "int")
                    {
                        code.Append($@"
                entity._{fieldName} = {CodeBuilderDefault.GetDBReaderFunctionName(property.DbType)}({idx++}) == 1;");
                    }
                    else if (property.CsType.ToLower() == "decimal")
                    {
                        code.Append($@"
                entity._{fieldName} = new decimal({CodeBuilderDefault.GetDBReaderFunctionName(property.DbType)}({idx++}));");
                    }
                    else
                    {
                        code.Append($@"
                entity._{fieldName} = ({property.CustomType ?? property.CsType}){CodeBuilderDefault.GetDBReaderFunctionName(property.DbType)}({idx++});");
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
    }
}