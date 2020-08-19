using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config.Mysql;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder.DataBase.MySql;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class MySqlAccessBuilder : AccessBuilderBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_MySqlAccess";

        private string SqlCode()
        {
            return $@"
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => {Entity.EntityName}._DataStruct_.EntityIdentity;

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
        protected sealed override string PrimaryKey => {Entity.EntityName}._DataStruct_.EntityPrimaryKey;

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
                return $@""{InsertSql()}"";
            }}
        }}

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode
        {{
            get
            {{
                return $@""{UpdateSql()}"";
            }}
        }}

        /*// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode({Entity.EntityName} data)
        {{
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return "";"";
            StringBuilder sql = new StringBuilder();{UpdateSqlByModify()}
            return sql.ToString();
        }}*/

        #endregion
";
        }

        private string SimpleCode()
        {
            var fields = Entity.DbFields.Where(p => p.ExtendConfigListBool["easyui", "simple"]).ToArray();
            if (fields.Length == 0)
                return "";
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
        public override void SimpleLoad(MySqlDataReader reader,{Entity.EntityName} entity)
        {{");
            int idx = 0;
            foreach (var field in fields)
            {
                SqlMomentCoder.FieldReadCode(Entity, field, code, idx++);
            }
            code.Append(@"
        }

        #endregion");
            return code.ToString();
        }

        private string BaseCode()
        {
            StringBuilder alias = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Entity.Alias))
            {
                alias.Append($@"
    /// <summary>
    /// {Entity.Description} 别名
    /// </summary>
    {(Entity.IsInternal ? "internal" : "public")} sealed class {Entity.Alias}DataAccess : {Entity.Name}DataAccess
    {{
    }}");
            }

            //{CreateScope()}
            var innerCode = $@"
{SqlCode()}
{FieldCode()}
        #region 方法实现
{LoadEntityCode()}
{GetDbTypeCode()}
{CreateFullSqlParameter()}
{UpdateCode()}
{InsertCode()}

        #endregion
{SimpleCode()}
";

            return $@"#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

using MySql.Data.MySqlClient;
using Agebull.EntityModel.MySql;

using Agebull.Common;

using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;
{Project.UsingNameSpaces}
using {SolutionConfig.Current.NameSpace}.DataAccess;

#endregion

namespace {NameSpace}.DataAccess
{{
    /// <summary>
    /// {Entity.Description}
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
        {innerCode}
    }}{alias}
{DataBaseExtend()}
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
            SaveCode(file, BaseCode());
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateExCode(string path)
        {
            var file = Path.Combine(path, $"{Entity.Name}DataAccess.cs");
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
            var code = CreateExtendCode();
            SaveCode(file, code);
        }

        private string CreateExtendCode()
        {
            string baseClass = "MySqlTable";
            if (Entity.Interfaces != null)
            {
                if (Entity.Interfaces.Contains("IRowScopeData"))
                    baseClass = "RowScopeDataAccess";
                //else if (Entity.Interfaces.Contains("IHistoryData"))
                //    baseClass = "HitoryTable";
                else if (Entity.Interfaces.Contains("IStateData"))
                    baseClass = "DataStateTable";
                else if (Entity.Interfaces.Contains("ILogicDeleteData"))
                    baseClass = "LogicDeleteTable";
            }
            
            var code = $@"#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

using MySql.Data.MySqlClient;
using Agebull.EntityModel.MySql;

using Agebull.Common;

using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;

{Project.UsingNameSpaces}
using {SolutionConfig.Current.NameSpace}.DataAccess;

#endregion

namespace {NameSpace}.DataAccess
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    partial class {Entity.Name}DataAccess : {baseClass}<{Entity.EntityName},{Project.DataBaseObjectName}>
    {{
    }}
}}";
            return code;
        }

        /// <summary>
        /// 字段唯一条件(主键或组合键)
        /// </summary>
        /// <returns></returns>
        private string UniqueCondition()
        {
            var code = new StringBuilder();
            if (!PublishDbFields.Any(p => p.UniqueIndex > 0))
            {
                code.Append($@"`{Entity.PrimaryColumn.DbFieldName}` = ?{Entity.PrimaryColumn.Name}");
            }
            else
            {
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

                    code.Append($"`{col.DbFieldName}`=?{col.Name}");
                }
            }
            if (Entity.Interfaces?.Contains("IStateData") == true)
            {
                var f = Entity.Properties.FirstOrDefault(p => p.Name == "IsFreeze");
                if (f != null)
                    code.Append($@" AND `{f.DbFieldName}` = 0");
                var s = Entity.Properties.FirstOrDefault(p => p.Name == "DataState");
                if (s != null)
                    code.Append($@" AND `{s.DbFieldName}` < 255");
            }
            return code.ToString();
        }
        private string InsertSql()
        {
            return OnlyInsertSql();
            /*if (!PublishDbFields.Any(p => p.UniqueIndex > 0))
            {
                return OnlyInsertSql();
            }
            var code = new StringBuilder();
            code.Append($@"
DECLARE ?__myId INT(4);
SELECT ?__myId = `{Entity.PrimaryColumn.DbFieldName}` FROM `{{ContextWriteTable}}` WHERE {UniqueCondition()}");

            code.Append($@"
IF ?__myId IS NULL
BEGIN{}
    SET ?__myId = {(Entity.PrimaryColumn.IsIdentity ? "@@IDENTITY" : Entity.PrimaryColumn.PropertyName)};
END
ELSE
BEGIN
    SET ?{Entity.PrimaryColumn.PropertyName}=?__myId;{UpdateSql(true)}
END
SELECT ?__myId;");
            return code.ToString();*/
        }

        private string OnlyInsertSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            var columns = PublishDbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Insert)).ToArray();
            sql.Append(@"
INSERT INTO `{ContextWriteTable}`
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
    `{field.DbFieldName}`");
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
    ?{field.Name}");
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
SELECT @@IDENTITY;");
            }

            return sql.ToString();
        }

        private string UpdateSql(bool isInner = false)
        {
            var sql = new StringBuilder();
            IEnumerable<PropertyConfig> columns = PublishDbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
            sql.Append(@"
UPDATE `{ContextWriteTable}` SET");
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
       `{field.DbFieldName}` = ?{field.Name}");
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
            IEnumerable<PropertyConfig> columns = PublishDbFields.Where(p => !p.IsIdentity && !p.IsCompute && !p.CustomWrite && !p.KeepStorageScreen.HasFlag(StorageScreenType.Update)).ToArray();
            code.Append(@"
            sql.AppendLine(""UPDATE `{ContextWriteTable}` SET"");");


            foreach (var field in columns)
            {
                code.Append($@"
            //{field.Caption}
            if (data.__EntityStatus.ModifiedProperties[{Entity.EntityName}._DataStruct_.Real_{field.Name}] > 0)
                sql.AppendLine(""       `{field.DbFieldName}` = ?{field.Name}"");");
            }
            code.Append($@"
            sql.Append("" WHERE {UniqueCondition()};"");");
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

        /*private string CreateScope()
        {
            return $@"

        /// <summary>
        /// 构造一个缺省可用的数据库对象
        /// </summary>
        /// <returns></returns>
        protected override MySqlDataBase CreateDefaultDataBase()
        {{
            return {Project.DataBaseObjectName}.Default ?? new {Project.DataBaseObjectName}();
        }}
        
        /// <summary>
        /// 生成数据库访问范围
        /// </summary>
        public static MySqlDataTableScope<{Entity.EntityName}> CreateScope()
        {{
            var db = {Project.DataBaseObjectName}.Default ?? new {Project.DataBaseObjectName}();
            return MySqlDataTableScope<{Entity.EntityName}>.CreateScope(db, db.{Entity.Name.ToPluralism()});
        }}";
        }*/

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
        public void CreateFullSqlParameter({Entity.EntityName} entity, MySqlCommand cmd)
        {{");

            var isFirstNull = true;
            foreach (var field in PublishDbFields.OrderBy(p => p.Index))
            {
                code.Append($@"
            //{field.Index + 1:D2}:{field.Caption}({field.Name})");
                if (!string.IsNullOrWhiteSpace(field.CustomType))
                {
                    code.Append($@"
            cmd.Parameters.Add(new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlHelper.ToSqlDbType(field)}){{ Value = (int)entity.{field.Name}}});");
                    continue;
                }
                switch (field.CsType)
                {
                    case "String":
                    case "string":
                        code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = string.IsNullOrWhiteSpace({CustomName(field, "entity.")});
            {(isFirstNull ? "var " : "")}parameter = new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlHelper.ToSqlDbType(field)} , isNull ? 10 : ({CustomName(field, "entity.")}).Length);
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
            {(isFirstNull ? "var " : "")}parameter = new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlHelper.ToSqlDbType(field)} , isNull ? 10 : {CustomName(field, "entity.")}.Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = {PropertyName(field, "entity.")};
            cmd.Parameters.Add(parameter);");
                        break;
                    case "DateTime":
                        field.DbNullable = true;
                        code.Append(field.Nullable
                            ? $@"
            {(isFirstNull ? "var " : "")}isNull = entity.{field.Name} == null || entity.{field.Name}.Value.Year < 1900;
            {(isFirstNull ? "var " : "")}parameter = new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlHelper.ToSqlDbType(field)});
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.{field.Name};
            cmd.Parameters.Add(parameter);"
                            : $@"
            {(isFirstNull ? "var " : "")}isNull = entity.{field.Name}.Year < 1900;
            {(isFirstNull ? "var " : "")}parameter = new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlHelper.ToSqlDbType(field)});
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.{field.Name};
            cmd.Parameters.Add(parameter);");
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
            {(isFirstNull ? "var " : "")}parameter = new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlHelper.ToSqlDbType(field)});
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
            cmd.Parameters.Add(new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlHelper.ToSqlDbType(field)}){{ Value = entity.{field.Name}}});");
                            continue;
                        }
                        code.Append($@"
            {(isFirstNull ? "var " : "")}isNull = entity.{field.Name} == null;
            {(isFirstNull ? "var " : "")}parameter = new MySqlParameter(""{field.Name}"",MySqlDbType.{MySqlHelper.ToSqlDbType(field)});
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
            return $@"

        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name=""entity"">实体对象</param>
        /// <param name=""cmd"">命令</param>
        /// <returns>返回真说明要取主键</returns>
        protected sealed override bool SetInsertCommand({Entity.EntityName} entity, MySqlCommand cmd)
        {{
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return {Entity.PrimaryColumn.IsIdentity.ToString().ToLower()};
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
        protected sealed override void SetUpdateCommand({Entity.EntityName} entity, MySqlCommand cmd)
        {{
            //cmd.CommandText = {(Entity.UpdateByModified ? "GetModifiedSqlCode(entity)" : "UpdateSqlCode")};
            CreateFullSqlParameter(entity,cmd);
        }}";
        }

        private string GetDbTypeCode()
        {
            var code = new StringBuilder();
            foreach (var field in PublishDbFields)
            {
                if(field.DbFieldName != field.Name)
                    code.Append($@"
                case ""{field.DbFieldName}"":");
                code.Append($@"
                case ""{field.Name}"":
                    return MySqlDbType.{MySqlHelper.ToSqlDbType(field)};");
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
        protected sealed override void LoadEntity(MySqlDataReader reader,{Entity.EntityName} entity)
        {{");
            int idx = 0;
            foreach (var field in PublishDbFields)
            {
                SqlMomentCoder.FieldReadCode(Entity, field, code, idx++);
            }
            code.Append(@"
        }");
            return code.ToString();
        }
        
    }
}