using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.SqlServer;
using Agebull.EntityModel.Designer;
using static System.String;

namespace Agebull.EntityModel.RobotCoder.DataBase.Sqlerver
{
    /// <summary>
    /// SQL����Ƭ��
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class SqlMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region ע��

        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Sqlerver", "���ɱ�(SQL)", "sql", p => !p.EnableDataBase, CreateTable);
            MomentCoder.RegisteCoder("Sqlerver", "������ֶ�(SQL)", "sql", p => !p.EnableDataBase, AddColumnCode);
            MomentCoder.RegisteCoder("Sqlerver", "�޸ı��ֶ�(SQL)", "sql", p => !p.EnableDataBase, ChangeColumnCode);
            MomentCoder.RegisteCoder("Sqlerver", "������ͼ(SQL)", "sql", p => !p.EnableDataBase, CreateView);
            MomentCoder.RegisteCoder<ProjectConfig>("Sqlerver", "����ҳ���(SQL)", "sql", PageInsertSql);
            MomentCoder.RegisteCoder("Sqlerver", "ɾ����ͼ(SQL)", "sql", p => !p.EnableDataBase, DropView);
            MomentCoder.RegisteCoder("Sqlerver", "ɾ����(SQL)", "sql", p => !p.EnableDataBase, DropTable);
            MomentCoder.RegisteCoder("Sqlerver", "�����(SQL)", "sql", p => !p.EnableDataBase, TruncateTable);
        }

        #endregion

        #region ���ݿ�

        public static string TruncateTable(EntityConfig entity)
        {
            if (entity.EnableDataBase)
                return Empty;
            return $@"
/*******************************{entity.Caption}*******************************/
TRUNCATE TABLE [{entity.SaveTableName}];
";
        }

        public static string DropView(ModelConfig entity)
        {
            if (entity.EnableDataBase || entity.SaveTableName == entity.ReadTableName)
                return Empty;
            return $@"
/*******************************{entity.Caption}*******************************/
DROP VIEW [{entity.ReadTableName}];
GO";
        }
        public static string CreateView(EntityConfig entity)
        {
            DataBaseHelper.CheckFieldLink(entity.Properties);
            var array = entity.DbFields.Where(p => p.IsLinkField && !p.IsLinkKey).ToArray();
            if (array.Length == 0)
            {
                return $"/**********{entity.Caption}**********û���ֶ������������������ͼ**********/";
            }
            var tables = entity.DbFields.Where(p => p.IsLinkField && !p.IsLinkKey).Select(p => p.LinkTable).Distinct().Select(GlobalConfig.GetEntity).ToDictionary(p => p.Name);
            if (tables.Count == 0)
            {
                entity.ReadTableName = entity.SaveTableName; ;
                return $"/**********{entity.Caption}**********û���ֶ������������������ͼ**********/";
            }
            string viewName;
            if (IsNullOrWhiteSpace(entity.ReadTableName) || entity.ReadTableName == entity.SaveTableName)
            {
                viewName = DataBaseHelper.ToViewName(entity);
            }
            else
            {
                viewName = entity.ReadTableName;
            }
            var builder = new StringBuilder();
            builder.Append($@"
/*******************************{entity.Caption}*******************************/
CREATE VIEW [{viewName}] AS 
    SELECT ");
            bool first = true;
            foreach (var field in entity.DbFields)
            {
                if (first)
                    first = false;
                else
                    builder.Append(@",
        ");

                if (!field.IsLinkKey && !IsNullOrWhiteSpace(field.LinkTable))
                {
                    if (tables.TryGetValue(field.LinkTable, out EntityConfig friend))
                    {
                        var linkField =
                            friend.DbFields.FirstOrDefault(
                                p => p.DbFieldName == field.LinkField || p.Name == field.LinkField);
                        if (linkField != null)
                        {
                            builder.AppendFormat(@"[{0}].[{1}] as [{2}]", friend.Name, linkField.DbFieldName, field.DbFieldName);
                            continue;
                        }
                    }
                }
                builder.AppendFormat(@"[{0}].[{1}] as [{1}]", entity.SaveTableName, field.DbFieldName);
            }
            builder.Append($@"
    FROM [{entity.SaveTableName}]");
            foreach (var table in tables.Values)
            {
                var field = entity.DbFields.FirstOrDefault(p => p.IsLinkKey && (p.LinkTable == table.Name || p.LinkTable == table.SaveTableName));
                if (field == null)
                    continue;
                var linkField = table.Properties.FirstOrDefault(
                    p => p.Name == field.LinkField || p.DbFieldName == field.LinkField);
                if (linkField == null)
                    continue;
                builder.AppendFormat(@"
    LEFT JOIN [{1}] [{4}] ON [{0}].[{2}] = [{4}].[{3}]"
                        , entity.SaveTableName, table.SaveTableName, field.DbFieldName, linkField.DbFieldName, table.Name);
            }
            builder.Append(';');
            builder.AppendLine("GO");
            builder.AppendLine();
            return builder.ToString();
        }

        private static string DropTable(EntityConfig entity)
        {
            if (entity == null)
                return "";
            if (entity.EnableDataBase)
                return $"{entity.Caption} : ����Ϊ��ͨ��(EnableDataBase=true)���޷�����SQL";
            return $@"
/*******************************{entity.Caption}*******************************/
DROP TABLE [{entity.SaveTableName}];
GO";
            //            return string.Format(@"
            //truncate TABLE dbo.{0};
            //GO
            //--INSERT INTO COC_NEW.dbo.{0} value({1})
            //--SELECT {1} FROM coc.dbo.{0};
            //--GO", Entity.TableName, Entity.PublishProperty.Select(p => p.ColumnName).LinkToString(','));
        }
        private string CreateTable(EntityConfig entity)
        {
            return CreateTableCode(entity);
            //var builder = new StringBuilder();
            //builder.AppendLine(DropTable());
            //builder.AppendLine(SqlSchemaChecker.CreateTableCode(Entity));
            //builder.AppendLine(DropView(Entity));
            //builder.AppendLine(CreateView(Entity));

            //return builder.ToString();
        }


        private static string PageInsertSql(ProjectConfig project)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                $@"
/*{project.Caption}*/
INSERT INTO [tb_sys_page_item] ([ItemType],[Name],[Caption],[Url],[Memo],[ParentId])
VALUES(0,'{project.Caption}','{project.Caption}',NULL,'{project.Description}',0);
set @pid = @@IDENTITY;");
            foreach (var entity in project.Entities.Where(p => !p.EnableDataBase))
                sb.Append($@"
INSERT INTO [tb_sys_page_item[ ([ItemType],[Name],[Caption],[Url],[Memo],[ParentId])
VALUES(2,'{entity.Name}','{entity.Caption}','/{entity.Parent.Name}/{entity.Name}/index','{entity.Description}',@pid);");
            return sb.ToString();
        }
        #endregion

        #region ��ṹ


        public static string CreateTableCode(EntityConfig entity, bool signle = false)
        {
            if (entity.EnableDataBase)
                return "";//�������Ϊ��ͨ�࣬�޷�����SQL
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
CREATE TABLE [{entity.SaveTableName}](");
            bool isFirst = true;
            foreach (var col in entity.DbFields.Where(p => !p.IsCompute))
            {
                code.Append($@"
   {(isFirst ? "" : ",")}{FieldDefault(col)}");

                isFirst = false;
            }
            code.Append(@"
);");
            MemCode(entity, code);
            return code.ToString();
        }

        private static void MemCode(EntityConfig entity, StringBuilder code)
        {
            code.Append($@"
GO
---------------------------����-------------------------------
ALTER TABLE dbo.{entity.SaveTableName} ADD CONSTRAINT
	PK_{entity.SaveTableName} PRIMARY KEY CLUSTERED 
	(
	    {entity.PrimaryField}
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO
---------------------------ע��-------------------------------
DECLARE @v sql_variant 
SET @v = N'{entity.Caption?.Replace('\'', '��')}:{entity.Description?.Replace('\'', '��')}'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'{
                    entity.SaveTableName
                }', NULL, NULL;");

            foreach (var col in entity.DbFields.Where(p => !p.IsCompute))
            {
                code.Append($@"
SET @v = N'{col.Caption?.Replace('\'', '��')}:{col.Description?.Replace('\'', '��')}'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'{entity.SaveTableName}', N'COLUMN', N'{col.DbFieldName}';");
            }
        }

        private static string AddColumnCode(EntityConfig entity)
        {
            if (entity == null)
                return "";
            if (entity.EnableDataBase)
                return $"{entity.Caption} : ����Ϊ��ͨ��(EnableDataBase=true)���޷�����SQL";
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
ALTER TABLE [{entity.SaveTableName}]");
            bool isFirst = true;
            foreach (var col in entity.DbFields.Where(p => !p.IsCompute))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');
                code.Append($@"
    ADD {FieldDefault(col)}");
            }
            code.AppendLine(@";");

            MemCode(entity, code);
            return code.ToString();
        }

        private string ChangeColumnCode(EntityConfig entity)
        {
            if (entity == null)
                return "";
            if (entity.EnableDataBase)
                return $"{entity.Caption} : ����Ϊ��ͨ��(EnableDataBase=true)���޷�����SQL";
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
ALTER TABLE [{entity.SaveTableName}]");
            bool isFirst = true;
            foreach (var col in entity.DbFields.Where(p => !p.IsCompute))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');
                code.Append($@"
    ALTER COLUMN [{col.DbFieldName}] {FieldDefault(col)}");
            }
            code.Append(@";");
            MemCode(entity, code);
            return code.ToString();
        }

        private static string FieldDefault(IFieldConfig col)
        {
            var def = col.Initialization == null
                ? null
                : col.CsType == "string" ? $"DEFAULT '{col.Initialization}'" : $"DEFAULT {col.Initialization}";
            var nulldef = col.CsType == "string" || col.DbNullable ? " NULL" : " NOT NULL";
            var identity = col.IsIdentity ? " IDENTITY(1,1)" : null;
            return $"[{col.DbFieldName}] {SqlServerHelper.ColumnType(col)}{identity}{nulldef} {def} -- '{col.Caption}'";
        }

        #endregion

        #region ���ݶ�ȡ

        /// <summary>
        /// �ֶ����ݿ��ȡ����
        /// </summary>
        /// <param name="field">�ֶ�</param>
        /// <param name="code">����</param>
        /// <param name="idx">���</param>
        public static void FieldReadCode(IFieldConfig field, StringBuilder code, int idx)
        {
            if (!IsNullOrWhiteSpace(field.CustomType))
            {
                code.Append($@"
                if (!reader.IsDBNull({idx}))
                    entity._{field.Name.ToLower()} = ({field.CustomType})reader.GetInt32({idx});");
                return;
            }
            var type = field.CsType.ToLower();
            var dbType = field.DbType.ToLower();
            if (type == "byte[]")
            {
                code.Append($@"
                if (!reader.IsDBNull({idx}))
                    entity._{field.Name.ToLower()} = reader.GetSqlBinary({idx}).Value;");
                return;
            }
            if (type == "string")
            {
                switch (dbType)
                {
                    case "varchar":
                    case "varstring":
                        code.Append($@"
                if (!reader.IsDBNull({idx}))
                    entity._{field.Name.ToLower()} = {ReaderName(field.DbType)}({idx});");
                        break;
                    default:
                        code.Append($@"
                if (!reader.IsDBNull({idx}))
                    entity._{field.Name.ToLower()} = {ReaderName(field.DbType)}({idx}).ToString();");
                        break;
                }
                return;
            }
            //if (field.DbNullable)
            code.Append($@"
                if (!reader.IsDBNull({idx}))
                    ");
            //else
            //    code.Append(@"
            //    ");

            switch (type)
            {
                case "decimal":
                    code.Append($"entity._{field.Name.ToLower()} ={ReaderName(field.DbType)}({idx});");
                    return;
                case "datetime":
                    code.Append($"try{{entity._{field.Name.ToLower()} = reader.GetMySqlDateTime({idx}).Value;}}catch{{}}");
                    return;
                //case "bool":
                //    code.Append($"entity._{field.Name.ToLower()} = reader.GetInt16({idx}) == 1;");
                //    break;
                default:
                    code.Append(
                        $"entity._{field.Name.ToLower()} = ({field.CustomType ?? field.CsType}){ReaderName(field.DbType)}({idx});");
                    break;
            }

        }


        /// <summary>
        ///     ȡ�ö�Ӧ���͵�DBReader�ķ�������
        /// </summary>
        /// <param name="csharpType">C#������</param>
        /// <param name="readerName">��ȡ��������</param>
        /// <returns>��ȡ������</returns>
        public static string ReaderName(string csharpType, string readerName = "reader")
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
                    return readerName + ".GetMySqlDateTime";

                case "decimal":
                case "numeric":
                    return readerName + ".GetDecimal";

                case "real":
                case "double":
                    return readerName + ".GetDouble";
                case "float":
                    return readerName + ".GetFloat";

                case "guid":
                case "uniqueidentifier":
                    return readerName + ".GetGuid";

                case "nchar":
                case "varchar":
                case "nvarchar":
                case "string":
                case "text":
                    return readerName + ".GetString";

                default:
                    return $"/*({csharpType})*/{readerName}.GetValue";
            }
        }

        #endregion
    }
}