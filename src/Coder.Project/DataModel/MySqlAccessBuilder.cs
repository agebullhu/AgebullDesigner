using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config.Mysql;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder.DataBase;

namespace Agebull.EntityModel.RobotCoder
{
    public sealed class MySqlAccessBuilder : CoderWithEntity
    {

        /// <summary>
        ///     ���������ݿ��ֶ�
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
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_Model_MySqlAccess";

        private string SqlCode()
        {
            return $@"
        #region ����SQL���

        /// <summary>
        /// ���Ψһ��ʶ
        /// </summary>
        public override int TableId
        {{
            get {{ return {Project.DataBaseObjectName}.Table_{Entity.Name}; }}
        }}

        /// <summary>
        /// ��ȡ����
        /// </summary>
        protected sealed override string ReadTableName
        {{
            get
            {{
                return @""{Entity.ReadTableName}"";
            }}
        }}

        /// <summary>
        /// д�����
        /// </summary>
        protected sealed override string WriteTableName
        {{
            get
            {{
                return @""{Entity.SaveTable}"";
            }}
        }}

        /// <summary>
        /// ����
        /// </summary>
        protected sealed override string PrimaryKey
        {{
            get
            {{
                return @""{Entity.PrimaryColumn.Name}"";
            }}
        }}

        /// <summary>
        /// ȫ���ȡ��SQL���
        /// </summary>
        protected sealed override string FullLoadFields
        {{
            get
            {{
                return @""{SqlMomentCoder.LoadSql(PublishDbFields)}"";
            }}
        }}

        

        /// <summary>
        /// �����SQL���
        /// </summary>
        protected sealed override string InsertSqlCode
        {{
            get
            {{
                return @""{InsertSql()}"";
            }}
        }}

        /// <summary>
        /// ȫ�����µ�SQL���
        /// </summary>
        protected sealed override string UpdateSqlCode
        {{
            get
            {{
                return @""{UpdateSql()}"";
            }}
        }}

        /// <summary>
        /// ȡ�ý����µ�SQL���
        /// </summary>
        internal string GetModifiedSqlCode({Entity.EntityName} data)
        {{
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return "";"";
            StringBuilder sql = new StringBuilder();{UpdateSqlByModify()}
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
        #region �򵥶�ȡ

        /// <summary>
        /// SQL���
        /// </summary>
        public override string SimpleFields
        {{
            get
            {{
                return @""{SqlMomentCoder.LoadSql(fields)}"";
            }}
        }}


        /// <summary>
        /// ��������
        /// </summary>
        /// <param name=""reader"">���ݶ�ȡ��</param>
        /// <param name=""entity"">��ȡ���ݵ�ʵ��</param>
        public override void SimpleLoad(MySqlDataReader reader,{Entity.EntityName} entity)
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
        #region �ֶ�

        /// <summary>
        ///  �����ֶ�
        /// </summary>
        static string[] _fields = new string[]{{ {Fields()} }};

        /// <summary>
        ///  �����ֶ�
        /// </summary>
        public sealed override string[] Fields
        {{
            get
            {{
                return _fields;
            }}
        }}

        /// <summary>
        ///  �ֶ��ֵ�
        /// </summary>
        public static Dictionary<string, string> fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {{{FieldMap()}
        }};

        /// <summary>
        ///  �ֶ��ֵ�
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
{SqlCode()}
{FieldCode()}
        #region ����ʵ��
{LoadEntityCode()}
{GetDbTypeCode()}
{CreateFullSqlParameter()}
{UpdateCode()}
{InsertCode()}
{CreateScope()}
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


namespace {NameSpace}.DataAccess
{{
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    {(Entity.IsInternal ? "internal" : "public")} partial class {Entity.Name}DataAccess
    {{{innerCode}
    }}

    partial class {Project.DataBaseObjectName}
    {{
{TablesEnum()}
{TableSql()}
{TableObject()}
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
        ///     ���ɻ�������
        /// </summary>
        protected override void CreateBaCode(string path)
        {
            var file = Path.Combine(path, Entity.Name + "DataAccess.Designer.cs");
            if (Entity.IsClass)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                return;
            }
            SaveCode(file, CreateCode());
        }

        /// <summary>
        ///     ������չ����
        /// </summary>
        protected override void CreateExCode(string path)
        {
            var file = Path.Combine(path, $"{Entity.Name}DataAccess.cs");
            if (Entity.IsClass)
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

namespace {NameSpace}.DataAccess
{{
    using Gboxt.Common.DataModel;
    using Gboxt.Common.DataModel.MySql;
    /// <summary>
    /// {Entity.Description}
    /// </summary>
    sealed partial class {Entity.Name}DataAccess : {baseClass}<{Entity.EntityName}>
    {{

    }}
}}";
            SaveCode(file, code);
        }
        

        private string TableObject()
        {
            var name = Entity.Name.ToPluralism();
            return ($@"

        /// <summary>
        /// {Entity.Description}���ݷ��ʶ���
        /// </summary>
        private {Entity.Name}DataAccess _{name.ToLWord()};

        /// <summary>
        /// {Entity.Description}���ݷ��ʶ���
        /// </summary>
        {(Entity.IsInternal ? "internal" : "public")} {Entity.Name}DataAccess {name}
        {{
            get
            {{
                return this._{name.ToLWord()} ?? ( this._{name.ToLWord()} = new {Entity.Name}DataAccess{{ DataBase = this}});
            }}
        }}");
        }

        private string TableSql()
        {
            return ($@"

        /// <summary>
        /// {Entity.Description}�Ľṹ���
        /// </summary>
        private TableSql _{Entity.ReadTableName}Sql = new TableSql
        {{
            TableName = ""{Entity.ReadTableName}"",
            PimaryKey = ""{Entity.PrimaryColumn.Name}""
        }};");
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
                sql.Append($@"""{field.Name}""");
            }
            return sql.ToString();
        }

        private string FieldMap()
        {
            var sql = new StringBuilder();
            var names = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            FieldMap(Entity, names);
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
                FieldMap(Project.Entities.FirstOrDefault(p => p.Name == entity.ModelBase), names);
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
        /// �ֶ�Ψһ����(��������ϼ�)
        /// </summary>
        /// <returns></returns>
        private string UniqueCondition()
        {
            if (!PublishDbFields.Any(p => p.UniqueIndex > 0))
                return $@"`{Entity.PrimaryColumn.ColumnName}` = ?{Entity.PrimaryColumn.Name}";

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
                return OnlyInsertSql();
            }
            var code = new StringBuilder();
            code.Append($@"
DECLARE ?__myId INT(4);
SELECT ?__myId = `{Entity.PrimaryColumn.ColumnName}` FROM `{Entity.SaveTable}` WHERE {UniqueCondition()}");

            code.Append($@"
IF ?__myId IS NULL
BEGIN{OnlyInsertSql(true)}
    SET ?__myId = {(Entity.PrimaryColumn.IsIdentity ? "@@IDENTITY" : Entity.PrimaryColumn.ColumnName)};
END
ELSE
BEGIN
    SET ?{Entity.PrimaryColumn.Name}=?__myId;{UpdateSql(true)}
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
(", Entity.SaveTable);
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
            sql.Append($@"
UPDATE `{Entity.SaveTable}` SET");
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
            sql.Append("" WHERE {0};"");", UniqueCondition());
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
        /// ����һ��ȱʡ���õ����ݿ����
        /// </summary>
        /// <returns></returns>
        protected override MySqlDataBase CreateDefaultDataBase()
        {{
            return {Project.DataBaseObjectName}.Default ?? new {Project.DataBaseObjectName}();
        }}
        
        /// <summary>
        /// �������ݿ���ʷ�Χ
        /// </summary>
        internal static MySqlDataTableScope<{Entity.EntityName}> CreateScope()
        {{
            var db = {Project.DataBaseObjectName}.Default ?? new {Project.DataBaseObjectName}();
            return MySqlDataTableScope<{Entity.EntityName}>.CreateScope(db, db.{Entity.Name.ToPluralism()});
        }}";
        }

        private string CreateFullSqlParameter()
        {
            var code = new StringBuilder();
            code.Append($@"

        /// <summary>
        /// ���ò������ݵ�����
        /// </summary>
        /// <param name=""entity"">ʵ�����</param>
        /// <param name=""cmd"">����</param>
        /// <returns>������˵��Ҫȡ����</returns>
        private void CreateFullSqlParameter({Entity.EntityName} entity, MySqlCommand cmd)
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
        /// ���ò������ݵ�����
        /// </summary>
        /// <param name=""entity"">ʵ�����</param>
        /// <param name=""cmd"">����</param>
        /// <returns>������˵��Ҫȡ����</returns>
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
        /// ���ø������ݵ�����
        /// </summary>
        /// <param name=""entity"">ʵ�����</param>
        /// <param name=""cmd"">����</param>
        protected sealed override void SetUpdateCommand({Entity.EntityName} entity, MySqlCommand cmd)
        {{
            cmd.CommandText = {(Entity.UpdateByModified ? "GetModifiedSqlCode(entity)" : "UpdateSqlCode")};
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
        /// �õ��ֶε�DbType����
        /// </summary>
        /// <param name=""field"">�ֶ�����</param>
        /// <returns>����</returns>
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
        /// ��������
        /// </summary>
        /// <param name=""reader"">���ݶ�ȡ��</param>
        /// <param name=""entity"">��ȡ���ݵ�ʵ��</param>
        protected sealed override void LoadEntity(MySqlDataReader reader,{Entity.EntityName} entity)
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
        /////     ��C#������תΪDBType
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