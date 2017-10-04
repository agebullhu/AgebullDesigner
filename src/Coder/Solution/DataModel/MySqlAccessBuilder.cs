using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.Common.Defaults.Mysql;
using Agebull.Common.SimpleDesign;
using Agebull.Common.SimpleDesign.MySql;
using Gboxt.Common.DataAccess.Schemas;
using Agebull.Common.Defaults;
using MySql.Data.MySqlClient;

namespace Agebull.Common.SimpleDesign.WebApplition
{
    public sealed class MySqlAccessBuilder : CoderWithEntity
    {

        /// <summary>
        ///     公开的数据库字段
        /// </summary>
        private IEnumerable<PropertyConfig> PublishDbFields
        {
            get
            {
                return Entity.Properties.Where(p => !p.Discard && !p.NoStorage && !p.DbInnerField &&
                                                  !string.Equals(p.DbType, "EMPTY", StringComparison.OrdinalIgnoreCase));
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        protected override string Name => "File_Model_MySqlAccess";

        private string SqlCode()
        {
            return $@"
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId
        {{
            get {{ return {this.Project.DataBaseObjectName}.Table_{this.Entity.Name}; }}
        }}

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {{
            get
            {{
                return @""{this.Entity.ReadTableName}"";
            }}
        }}

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {{
            get
            {{
                return @""{this.Entity.SaveTable}"";
            }}
        }}

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey
        {{
            get
            {{
                return @""{this.Entity.PrimaryColumn.Name}"";
            }}
        }}

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {{
            get
            {{
                return @""{SqlMomentCoder.LoadSql(PublishDbFields)}"";
            }}
        }}

        

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode
        {{
            get
            {{
                return @""{this.InsertSql()}"";
            }}
        }}

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode
        {{
            get
            {{
                return @""{this.UpdateSql()}"";
            }}
        }}

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        internal string GetModifiedSqlCode({this.Entity.EntityName} data)
        {{
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return "";"";
            StringBuilder sql = new StringBuilder();{this.UpdateSqlByModify()}
            return sql.ToString();
        }}

        #endregion
";
        }

        private string SimpleCode()
        {
            var fields = Entity.DbFields.Where(p => p.ExtendConfigListBool["db_simple"]).ToArray();
            var code = new StringBuilder();
            code.Append($@"
        #region 简单读取

        /// <summary>
        /// SQL语句
        /// </summary>
        public override string SimpleFields
        {{
            get
            {{
                return @""{SqlMomentCoder.LoadSql(fields)}"";
            }}
        }}


        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name=""reader"">数据读取器</param>
        /// <param name=""entity"">读取数据的实体</param>
        public override void SimpleLoad(MySqlDataReader reader,{this.Entity.EntityName} entity)
        {{
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {{");

            var idx = 0;
            foreach (var field in fields)
            {
                SqlMomentCoder.FieldReadCode(field, code, idx++);
            }
            code.Append(@"
            }
        }
        #endregion");
            return code.ToString();
        }

        private string FieldCode()
        {
            return $@"
        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{{ {this.Fields()} }};

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
        {{{this.FieldMap()}
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
            var innerCode = $@"
{this.SqlCode()}
{this.FieldCode()}
        #region 方法实现
{this.LoadEntityCode()}
{this.GetDbTypeCode()}
{this.CreateFullSqlParameter()}
{this.UpdateCode()}
{this.InsertCode()}
{this.CreateScope()}
        #endregion
{SimpleCode()}
";

            return $@"
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Linq;
using System.Text;
using Gboxt.Common.DataModel;

using MySql.Data.MySqlClient;
using Gboxt.Common.DataModel.MySql;


namespace {this.NameSpace}.DataAccess
{{
    /// <summary>
    /// {this.Entity.Description}
    /// </summary>
    {(this.Entity.IsInternal ? "internal" : "public")} partial class {this.Entity.Name}DataAccess
    {{{innerCode}
    }}

    partial class {this.Project.DataBaseObjectName}
    {{
{this.TablesEnum()}
{this.TableSql()}
{this.TableObject()}
    }}
}}
";
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
        /// <summary>
        ///     生成基础代码
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            var file = Path.Combine(path, this.Entity.Name + "DataAccess.Designer.cs");
            if (this.Entity.IsClass)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                return;
            }
            SaveCode(file, this.CreateCode());
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            var file = Path.Combine(path, $"{this.Entity.Name}DataAccess.cs");
            if (this.Entity.IsClass)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                return;
            }
            string baseClass = "MySqlTable";
            if (Entity.Interfaces != null)
            {
                if (Entity.Interfaces.Contains("IRowScopeData"))
                    baseClass = "RowScopeDataAccess";
                else if (Entity.Interfaces.Contains("IStateData"))
                    baseClass = "DataStateTable";
            }
            var code = $@"
using System;
using System.Data;
using System.Linq;

namespace {this.NameSpace}.DataAccess
{{
    using Gboxt.Common.DataModel;
    using Gboxt.Common.DataModel.MySql;
    /// <summary>
    /// {this.Entity.Description}
    /// </summary>
    sealed partial class {this.Entity.Name}DataAccess : {baseClass}<{this.Entity.EntityName}>
    {{

    }}
}}";
            SaveCode(file, code);
        }
        

        private string TableObject()
        {
            var name = this.Entity.Name.ToPluralism();
            return ($@"

        /// <summary>
        /// {this.Entity.Description}数据访问对象
        /// </summary>
        private {this.Entity.Name}DataAccess _{name.ToLWord()};

        /// <summary>
        /// {this.Entity.Description}数据访问对象
        /// </summary>
        {(this.Entity.IsInternal ? "internal" : "public")} {this.Entity.Name}DataAccess {name}
        {{
            get
            {{
                return this._{name.ToLWord()} ?? ( this._{name.ToLWord()} = new {this.Entity.Name}DataAccess{{ DataBase = this}});
            }}
        }}");
        }

        private string TableSql()
        {
            return ($@"

        /// <summary>
        /// {this.Entity.Description}的结构语句
        /// </summary>
        private TableSql _{this.Entity.ReadTableName}Sql = new TableSql
        {{
            TableName = ""{this.Entity.ReadTableName}"",
            PimaryKey = ""{this.Entity.PrimaryColumn.Name}""
        }};");
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
                sql.Append($@"""{field.Name}""");
            }
            return sql.ToString();
        }

        private string FieldMap()
        {
            var sql = new StringBuilder();
            var names = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            this.FieldMap(this.Entity, names);
            var isFirst = true;
            foreach (var field in names)
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
            {{ ""{field.Key}"" , ""{field.Value}"" }}");
            }
            return sql.ToString();
        }


        private void FieldMap(EntityConfig entity, Dictionary<string, string> names)
        {
            if (entity == null)
            {
                return;
            }
            if (!string.IsNullOrEmpty(entity.ModelBase))
            {
                this.FieldMap(this.Project.Entities.FirstOrDefault(p => p.Name == entity.ModelBase), names);
            }
            foreach (var field in entity.DbFields)
            {
                if (!names.ContainsKey(field.Name))
                {
                    names.Add(field.Name, field.ColumnName);
                }
                if (!names.ContainsKey(field.ColumnName))
                {
                    names.Add(field.ColumnName, field.ColumnName);
                }
                foreach (var alia in field.GetAliasPropertys())
                {
                    if (!names.ContainsKey(alia))
                    {
                        names.Add(alia, field.ColumnName);
                    }
                }
            }
            if (!names.ContainsKey("Id"))
            {
                names.Add("Id", entity.PrimaryColumn.ColumnName);
            }
        }



        /// <summary>
        /// 字段唯一条件(主键或组合键)
        /// </summary>
        /// <returns></returns>
        private string UniqueCondition()
        {
            if (!PublishDbFields.Any(p => p.UniqueIndex > 0))
                return $@"`{this.Entity.PrimaryColumn.ColumnName}` = ?{this.Entity.PrimaryColumn.Name}";

            var code = new StringBuilder();
            var uniqueFields = PublishDbFields.Where(p => p.UniqueIndex > 0).OrderBy(p => p.UniqueIndex).ToArray();
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
                code.Append($"`{col.ColumnName}`=?{col.Name}");
            }
            return code.ToString();
        }
        private string InsertSql()
        {
            if (!PublishDbFields.Any(p => p.UniqueIndex > 0))
            {
                return this.OnlyInsertSql();
            }
            var code = new StringBuilder();
            code.Append($@"
DECLARE ?__myId INT(4);
SELECT ?__myId = `{this.Entity.PrimaryColumn.ColumnName}` FROM `{this.Entity.SaveTable}` WHERE {UniqueCondition()}");

            code.Append($@"
IF ?__myId IS NULL
BEGIN{this.OnlyInsertSql(true)}
    SET ?__myId = {(this.Entity.PrimaryColumn.IsIdentity ? "@@IDENTITY" : this.Entity.PrimaryColumn.ColumnName)};
END
ELSE
BEGIN
    SET ?{this.Entity.PrimaryColumn.Name}=?__myId;{this.UpdateSql(true)}
END
SELECT ?__myId;");
            return code.ToString();
        }

        private string OnlyInsertSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            var columns = PublishDbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)).ToArray();
            sql.AppendFormat(@"
INSERT INTO `{0}`
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
    `{0}`", field.ColumnName);
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
    ?{0}", field.Name);
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
SELECT @@IDENTITY;");
            }

            return sql.ToString();
        }

        private string UpdateSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            IEnumerable<PropertyConfig> columns = PublishDbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
            sql.Append($@"
UPDATE `{this.Entity.SaveTable}` SET");
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
       `{field.ColumnName}` = ?{field.Name}");
            }
            sql.Append($@"
 WHERE {this.UniqueCondition()};");
            if (isInner)
            {
                sql.Replace("\n", "\n    ");
            }
            return sql.ToString();
        }

        private string UpdateSqlByModify()
        {
            var code = new StringBuilder();
            IEnumerable<PropertyConfig> columns = PublishDbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
            code.Append($@"
            sql.AppendLine(""UPDATE `{Entity.SaveTable}` SET"");");


            foreach (var field in columns)
            {
                code.Append($@"
            //{field.Caption}
            if (data.__EntityStatus.ModifiedProperties[{Entity.EntityName}.Real_{field.Name}] > 0)
                sql.AppendLine(""       `{field.ColumnName}` = ?{field.Name}"");");
            }
            code.AppendFormat(@"
            sql.Append("" WHERE {0};"");", this.UniqueCondition());
            return code.ToString();
        }

        private static string CustomName(PropertyConfig col, string pre)
        {
            return col.CustomType == null ? $"{pre}{col.Name}" : $"({col.CustomType}){pre}{col.Name}";
        }

        private static string PropertyName(PropertyConfig col, string pre)
        {
            return col.CustomType == null ? $"{pre}{col.Name}" : $"({col.CsType}){pre}{col.Name}";
        }

        private string CreateScope()
        {
            return $@"

        /// <summary>
        /// 构造一个缺省可用的数据库对象
        /// </summary>
        /// <returns></returns>
        protected override MySqlDataBase CreateDefaultDataBase()
        {{
            return {this.Project.DataBaseObjectName}.Default ?? new {this.Project.DataBaseObjectName}();
        }}
        
        /// <summary>
        /// 生成数据库访问范围
        /// </summary>
        internal static MySqlDataTableScope<{this.Entity.EntityName}> CreateScope()
        {{
            var db = {this.Project.DataBaseObjectName}.Default ?? new {this.Project.DataBaseObjectName}();
            return MySqlDataTableScope<{this.Entity.EntityName}>.CreateScope(db, db.{this.Entity.Name.ToPluralism()});
        }}";
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
        private void CreateFullSqlParameter({this.Entity.EntityName} entity, MySqlCommand cmd)
        {{");

            var isFirstNull = true;
            foreach (var field in PublishDbFields.OrderBy(p => p.Index))
            {
                code.AppendFormat(@"
            //{2:D2}:{0}({1})", field.Caption, field.Name, field.Index + 1);
                if (!string.IsNullOrWhiteSpace(field.CustomType))
                {
                    code.Append($@"
            cmd.Parameters.Add(new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlDataBaseHelper.ToSqlDbType(field)}){{ Value = (int)entity.{field.Name}}});");
                    continue;
                }
                switch (field.CsType)
                {
                    case "String":
                    case "string":
                        code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = string.IsNullOrWhiteSpace({CustomName(field, "entity.")});
            {(isFirstNull ? "var " : "")}parameter = new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlDataBaseHelper.ToSqlDbType(field)} , isNull ? 10 : ({CustomName(field, "entity.")}).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = {PropertyName(field, "entity.")};
            cmd.Parameters.Add(parameter);");
                        break;
                    case "byte[]":
                    case "Byte[]":
                        code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = {CustomName(field, "entity.")} == null || {CustomName(field, "entity.")}.Length == 0;
            {(isFirstNull ? "var " : "")}parameter = new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlDataBaseHelper.ToSqlDbType(field)} , isNull ? 10 : {CustomName(field, "entity.")}.Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = {PropertyName(field, "entity.")};
            cmd.Parameters.Add(parameter);");
                        break;
                    case "DateTime":
                        field.DbNullable = true;
                        if (field.Nullable)
                        {
                            code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = entity.{field.Name} == null || entity.{field.Name}.Value.Year < 1900;
            {(isFirstNull ? "var " : "")}parameter = new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlDataBaseHelper.ToSqlDbType(field)});
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.{field.Name};
            cmd.Parameters.Add(parameter);");
                        }
                        else
                        {
                            code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = entity.{field.Name}.Year < 1900;
            {(isFirstNull ? "var " : "")}parameter = new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlDataBaseHelper.ToSqlDbType(field)});
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.{field.Name};
            cmd.Parameters.Add(parameter);");
                        }
                        break;
                    case "bool":
                        if (!field.Nullable)
                        {
                            code.Append($@"
            cmd.Parameters.Add(new MySqlParameter(""{field.Name}"",MySqlDbType.Byte) {{ Value = entity.{field.Name} ? (byte)1 : (byte)0 }});");
                            continue;
                        }
                        code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = entity.{field.Name} == null;
            {(isFirstNull ? "var " : "")}parameter = new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlDataBaseHelper.ToSqlDbType(field)});
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.{field.Name} ? (byte)1 : (byte)0;
            cmd.Parameters.Add(parameter);");
                        break;
                    default:
                        if (!field.Nullable)
                        {
                            code.Append($@"
            cmd.Parameters.Add(new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlDataBaseHelper.ToSqlDbType(field)}){{ Value = entity.{field.Name}}});");
                            continue;
                        }
                        code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = entity.{field.Name} == null;
            {(isFirstNull ? "var " : "")}parameter = new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlDataBaseHelper.ToSqlDbType(field)});
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = {PropertyName(field, "entity.")};
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
        protected sealed override bool SetInsertCommand({this.Entity.EntityName} entity, MySqlCommand cmd)
        {{
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return {this.Entity.PrimaryColumn.IsIdentity.ToString().ToLower()};
        }}";
        }

        private string UpdateCode()
        {
            return
                $@"

        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name=""entity"">实体对象</param>
        /// <param name=""cmd"">命令</param>
        protected sealed override void SetUpdateCommand({this.Entity.EntityName} entity, MySqlCommand cmd)
        {{
            cmd.CommandText = {(this.Entity.UpdateByModified ? "GetModifiedSqlCode(entity)" : "UpdateSqlCode")};
            CreateFullSqlParameter(entity,cmd);
        }}";
        }

        private string GetDbTypeCode()
        {
            var code = new StringBuilder();
            foreach (var field in PublishDbFields)
            {
                code.AppendFormat(@"
                case ""{0}"":
                    return MySqlDbType.{1};"
                    , field.Name
                    , MySqlDataBaseHelper.ToSqlDbType(field));
            }

            return
                $@"
        /// <summary>
        /// 得到字段的DbType类型
        /// </summary>
        /// <param name=""field"">字段名称</param>
        /// <returns>参数</returns>
        protected sealed override MySqlDbType GetDbType(string field)
        {{
            switch (field)
            {{{code}
            }}
            return MySqlDbType.VarChar;
        }}";
        }

        private string LoadEntityCode()
        {
            var code = new StringBuilder();
            code.Append($@"

        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name=""reader"">数据读取器</param>
        /// <param name=""entity"">读取数据的实体</param>
        protected sealed override void LoadEntity(MySqlDataReader reader,{this.Entity.EntityName} entity)
        {{
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {{");
            var idx = 0;
            foreach (var field in PublishDbFields)
            {
                SqlMomentCoder.FieldReadCode(field, code, idx++);
            }
            code.Append(@"
            }
        }");
            return code.ToString();
        }

        ///// <summary>
        /////     从C#的类型转为DBType
        ///// </summary>
        ///// <param name="csharpType"> </param>
        //public static MySqlDbType ToSqlDbType(string csharpType)
        //{
        //    switch (csharpType)
        //    {
        //        case "Boolean":
        //        case "bool":
        //            return MySqlDbType.Bit;
        //        case "byte":
        //        case "Byte":
        //        case "sbyte":
        //        case "SByte":
        //            return MySqlDbType.Int16;
        //        case "Char":
        //        case "char":
        //            return MySqlDbType.Byte;
        //        case "short":
        //        case "Int16":
        //        case "ushort":
        //        case "UInt16":
        //            return MySqlDbType.Int16;
        //        case "int":
        //        case "Int32":
        //        case "IntPtr":
        //        case "uint":
        //        case "UInt32":
        //        case "UIntPtr":
        //            return MySqlDbType.Int32;
        //        case "long":
        //        case "Int64":
        //        case "ulong":
        //        case "UInt64":
        //            return MySqlDbType.Int64;
        //        case "decimal":
        //        case "Decimal":
        //        case "float":
        //        case "Float":
        //        case "double":
        //        case "Double":
        //            return MySqlDbType.Decimal;
        //        case "Guid":
        //            return MySqlDbType.Guid;
        //        case "DateTime":
        //            return MySqlDbType.DateTime;
        //        case "String":
        //        case "string":
        //            return MySqlDbType.VarString;
        //        case "Binary":
        //        case "byte[]":
        //        case "Byte[]":
        //            return MySqlDbType.Binary;
        //        default:
        //            return MySqlDbType.Binary;
        //    }
        //}
    }
}