using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.SqlServer;
using Agebull.EntityModel.Config.V2021;
using Agebull.EntityModel.Designer;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using static System.String;

namespace Agebull.EntityModel.RobotCoder.DataBase.Sqlerver
{
    /// <summary>
    /// SQL代码片断
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class SqlMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegisteCoder<DataTableConfig>("Sqlerver", "生成表(SQL)", "sql", CreateTable);
            CoderManager.RegisteCoder<DataTableConfig>("Sqlerver", "插入表字段(SQL)", "sql", AddColumnCode);
            CoderManager.RegisteCoder<DataTableConfig>("Sqlerver", "修改表字段(SQL)", "sql", ChangeColumnCode);
            CoderManager.RegisteCoder<DataTableConfig>("Sqlerver", "生成视图(SQL)", "sql", CreateView);
            CoderManager.RegisteCoder<ProjectConfig>("Sqlerver", "插入页面表(SQL)", "sql", PageInsertSql);
            CoderManager.RegisteCoder<DataTableConfig>("Sqlerver", "删除视图(SQL)", "sql", DropView);
            CoderManager.RegisteCoder<DataTableConfig>("Sqlerver", "删除表(SQL)", "sql", DropTable);
            CoderManager.RegisteCoder<DataTableConfig>("Sqlerver", "清除表(SQL)", "sql", TruncateTable);
        }

        #endregion

        #region 数据库

        public static string TruncateTable(DataTableConfig entity)
        {


            return $@"
/*******************************{entity.Caption}*******************************/
TRUNCATE TABLE [{entity.SaveTableName}];
";
        }

        public static string DropView(DataTableConfig entity)
        {
            if (entity.SaveTableName == entity.ReadTableName)
                return "";
            return $@"
/*******************************{entity.Caption}*******************************/
DROP VIEW [{entity.ReadTableName}];
GO";
        }
        public static string CreateView(DataTableConfig entity)
        {
            DataBaseHelper.CheckFieldLink(entity.Fields);
            var array = entity.FindAndToArray(p => p.IsLinkField && !p.IsLinkKey).ToArray();
            if (array.Length == 0)
            {
                return $"/**********{entity.Caption}**********没有字段引用其它表的无需视图**********/";
            }
            var tables = entity.FindAndToArray(p => p.IsLinkField && !p.IsLinkKey).Select(p => p.LinkTable).Distinct().Select(GlobalConfig.GetEntity).ToDictionary(p => p.Name);
            if (tables.Count == 0)
            {
                entity.ReadTableName = entity.SaveTableName; ;
                return $"/**********{entity.Caption}**********没有字段引用其它表的无需视图**********/";
            }
            string viewName;
            if (IsNullOrWhiteSpace(entity.ReadTableName) || entity.ReadTableName == entity.SaveTableName)
            {
                viewName = DataBaseHelper.ToViewName(entity.Entity);
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
            foreach (var field in entity.Fields)
            {
                if (first)
                    first = false;
                else
                    builder.Append(@",
        ");

                if (!field.IsLinkKey && !IsNullOrWhiteSpace(field.LinkTable))
                {
                    if (tables.TryGetValue(field.LinkTable, out var friend))
                    {
                        var linkField =
                            friend.Find(
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
                var field = entity.Find(p => p.IsLinkKey && (p.LinkTable == table.Name || p.LinkTable == table.SaveTableName));
                if (field == null)
                    continue;
                var linkField = table.Find(p => p.Name == field.LinkField || p.DbFieldName == field.LinkField);
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

        private static string DropTable(DataTableConfig entity)
        {
            if (entity == null)
                return "";

            return $"{entity.Caption} : 设置为普通类(EnableDataBase=true)，无法生成SQL";
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
        private string CreateTable(DataTableConfig entity)
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
VALUES(2,'{entity.Name}','{entity.Caption}','/{entity.Project.Name}/{entity.Name}/index','{entity.Description}',@pid);");
            return sb.ToString();
        }
        #endregion

        #region 表结构


        public static string CreateTableCode(DataTableConfig entity, bool signle = false)
        {
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
CREATE TABLE [{entity.SaveTableName}](");
            bool isFirst = true;
            foreach (var field in entity.FindAndToArray(p => !p.IsReadonly))
            {
                code.Append($@"
   {(isFirst ? "" : ",")}{FieldDefault(field)}");

                isFirst = false;
            }
            code.Append(@"
);");
            MemCode(entity, code);
            return code.ToString();
        }

        private static void MemCode(DataTableConfig entity, StringBuilder code)
        {
            code.Append($@"
GO
---------------------------主键-------------------------------
ALTER TABLE dbo.{entity.SaveTableName} ADD CONSTRAINT
	PK_{entity.SaveTableName} PRIMARY KEY CLUSTERED 
	(
	    {entity.Entity.PrimaryField}
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO
---------------------------注释-------------------------------
DECLARE @v sql_variant 
SET @v = N'{entity.Caption?.Replace('\'', '‘')}:{entity.Description?.Replace('\'', '‘')}'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'{
                    entity.SaveTableName
                }', NULL, NULL;");

            foreach (var col in entity.FindAndToArray(p => !p.IsReadonly))
            {
                code.Append($@"
SET @v = N'{col.Caption?.Replace('\'', '‘')}:{col.Description?.Replace('\'', '‘')}'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'{entity.SaveTableName}', N'COLUMN', N'{col.DbFieldName}';");
            }
        }

        private static string AddColumnCode(DataTableConfig entity)
        {
            if (entity == null)
                return "";

            return $"{entity.Caption} : 设置为普通类(EnableDataBase=true)，无法生成SQL";
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
ALTER TABLE [{entity.SaveTableName}]");
            bool isFirst = true;
            foreach (var col in entity.FindAndToArray(p => !p.IsReadonly))
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

        private string ChangeColumnCode(DataTableConfig entity)
        {
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
ALTER TABLE [{entity.SaveTableName}]");
            bool isFirst = true;
            foreach (var field in entity.FindAndToArray(p => !p.IsReadonly))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');
                code.Append($@"
    ALTER COLUMN [{field.DbFieldName}] {FieldDefault(field)}");
            }
            code.Append(@";");
            MemCode(entity, code);
            return code.ToString();
        }

        private static string FieldDefault(DataBaseFieldConfig field)
        {
            var def = field.Property.Initialization == null
                ? null
                : field.Property.CsType == "string" ? $"DEFAULT '{field.Property.Initialization}'" : $"DEFAULT {field.Property.Initialization}";
            var nulldef = field.Property.CsType == "string" || field.DbNullable ? " NULL" : " NOT NULL";
            var identity = field.IsIdentity ? " IDENTITY(1,1)" : null;
            return $"[{field.DbFieldName}] {SqlServerHelper.ColumnType(field)}{identity}{nulldef} {def} -- '{field.Caption}'";
        }

        #endregion

        #region 数据读取

        /// <summary>
        /// 字段数据库读取代码
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="code">代码</param>
        /// <param name="idx">序号</param>
        public static void FieldReadCode(DataBaseFieldConfig field, StringBuilder code, int idx)
        {
            if (!IsNullOrWhiteSpace(field.Property.CustomType))
            {
                code.Append($@"
                if (!reader.IsDBNull({idx}))
                    entity._{field.Name.ToLower()} = ({field.Property.CustomType})reader.GetInt32({idx});");
                return;
            }
            var type = field.Property.CsType.ToLower();
            var dbType = field.FieldType.ToLower();
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
                    entity._{field.Name.ToLower()} = {ReaderName(field.FieldType)}({idx});");
                        break;
                    default:
                        code.Append($@"
                if (!reader.IsDBNull({idx}))
                    entity._{field.Name.ToLower()} = {ReaderName(field.FieldType)}({idx}).ToString();");
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
                    code.Append($"entity._{field.Name.ToLower()} ={ReaderName(field.FieldType)}({idx});");
                    return;
                case "datetime":
                    code.Append($"try{{entity._{field.Name.ToLower()} = reader.GetMySqlDateTime({idx}).Value;}}catch{{}}");
                    return;
                //case "bool":
                //    code.Append($"entity._{field.Name.ToLower()} = reader.GetInt16({idx}) == 1;");
                //    break;
                default:
                    code.Append(
                        $"entity._{field.Name.ToLower()} = ({field.Property.CustomType ?? field.Property.CsType}){ReaderName(field.FieldType)}({idx});");
                    break;
            }

        }


        /// <summary>
        ///     取得对应类型的DBReader的方法名称
        /// </summary>
        /// <param name="csharpType">C#的类型</param>
        /// <param name="readerName">读取器的名称</param>
        /// <returns>读取方法名</returns>
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