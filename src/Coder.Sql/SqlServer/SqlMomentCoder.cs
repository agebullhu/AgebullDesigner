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
            MomentCoder.RegisteCoder("Sqlerver", "生成表(SQL)", cfg => RunByEntity(cfg, CreateTable));
            MomentCoder.RegisteCoder("Sqlerver", "插入表字段(SQL)", cfg => RunByEntity(cfg, AddColumnCode));
            MomentCoder.RegisteCoder("Sqlerver", "修改表字段(SQL)", cfg => Run(cfg, ChangeColumnCode));
            MomentCoder.RegisteCoder("Sqlerver", "生成视图(SQL)", cfg => Run(cfg, CreateView));
            MomentCoder.RegisteCoder("Sqlerver", "插入页面表(SQL)", PageInsertSql);
            MomentCoder.RegisteCoder("Sqlerver", "删除视图(SQL)", cfg => Run(cfg, DropView));
            MomentCoder.RegisteCoder("Sqlerver", "删除表(SQL)", cfg => RunByEntity(cfg, DropTable));
            MomentCoder.RegisteCoder("Sqlerver", "清除表(SQL)", cfg => RunByEntity(cfg, TruncateTable));
        }

        #endregion

        #region 数据库
        private string TruncateTable()
        {
            return TruncateTable(Entity);
        }

        public static string TruncateTable(EntityConfig entity)
        {
            return $@"
/*******************************{entity.Caption}*******************************/
TRUNCATE TABLE [{entity.SaveTable}];
";
        }

        public static string DropView(EntityConfig entity)
        {
            if (entity.SaveTable == entity.ReadTableName)
                return Empty;
            return $@"
/*******************************{entity.Caption}*******************************/
DROP VIEW [{entity.ReadTableName}];
";
        }
        public static string CreateView(EntityConfig entity)
        {
            if (entity.DbFields.All(p => IsNullOrEmpty(p.LinkTable)))
                return null;
            var tables = new Dictionary<string, EntityConfig>();
            foreach (var field in entity.DbFields)
            {
                if (string.IsNullOrWhiteSpace(field.LinkTable))
                    continue;
                if (field.LinkTable == entity.Name || field.LinkTable == entity.ReadTableName ||
                    field.LinkTable == entity.SaveTableName)
                {
                    continue;
                }
                if (tables.ContainsKey(field.LinkTable))
                    continue;
                string name = field.LinkTable;
                var table = GlobalConfig.GetEntity(p => p.SaveTableName == name || p.ReadTableName == name || p.Name == name);
                if (table==null || table == entity)
                {
                    continue;
                }
                tables.Add(name, table);
            }
            string viewName;
            if (string.IsNullOrWhiteSpace(entity.ReadTableName) || entity.ReadTableName == entity.SaveTable)
            {
                viewName = "view_" + GlobalConfig.ToLinkWordName(entity.Name, "_", false);
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
            foreach (PropertyConfig field in entity.PublishProperty)
            {
                if (first)
                    first = false;
                else
                    builder.Append(@",
        ");

                if (!field.IsLinkKey && !IsNullOrEmpty(field.LinkTable))
                {
                    EntityConfig friend;
                    if (tables.TryGetValue(field.LinkTable, out friend))
                    {
                        var linkField =
                            friend.Properties.FirstOrDefault(
                                p => p.ColumnName == field.LinkField || p.Name == field.LinkField);
                        if (linkField != null)
                        {
                            builder.AppendFormat(@"[{0}].[{1}] as [{2}]", friend.Name, linkField.ColumnName, field.ColumnName);
                            continue;
                        }
                    }
                }
                builder.AppendFormat(@"[{0}].[{1}] as [{1}]", entity.SaveTable, field.ColumnName);
            }
            builder.Append($@"
    FROM [{entity.SaveTable}]");
            foreach (var table in tables.Values)
            {
                var field = entity.Properties.FirstOrDefault(p => p.IsLinkKey && p.LinkTable == table.SaveTable);
                if (field == null)
                    continue;
                var linkField = table.Properties.FirstOrDefault(
                    p => p.Name == field.LinkField || p.ColumnName == field.LinkField);
                if (linkField == null)
                    continue;
                builder.AppendFormat(@"
    LEFT JOIN [{1}] [{4}] ON [{0}].[{2}] = [{4}].[{3}]"
                        , entity.SaveTable, table.SaveTable, field.ColumnName, linkField.ColumnName, table.Name);
            }
            builder.Append(';');
            builder.AppendLine();
            return builder.ToString();
        }
        private string DropTable()
        {
            return DropTable(Entity);
        }
        private static string DropTable(EntityConfig entity)
        {
            return $@"
/*******************************{entity.Caption}*******************************/
DROP TABLE [{entity.SaveTable}];";
            //            return string.Format(@"
            //truncate TABLE dbo.{0};
            //GO
            //--INSERT INTO COC_NEW.dbo.{0} value({1})
            //--SELECT {1} FROM coc.dbo.{0};
            //--GO", Entity.TableName, Entity.PublishProperty.Select(p => p.ColumnName).LinkToString(','));
        }
        private string CreateTable()
        {
            return CreateTableCode(Entity);
            //var builder = new StringBuilder();
            //builder.AppendLine(DropTable());
            //builder.AppendLine(SqlSchemaChecker.CreateTableCode(Entity));
            //builder.AppendLine(DropView(Entity));
            //builder.AppendLine(CreateView(Entity));

            //return builder.ToString();
        }
        private string AddColumnCode()
        {
            return AddColumnCode(Entity);
        }

        private static string PageInsertSql(ConfigBase config)
        {
            var projectConfig = config as ProjectConfig;
            if (projectConfig != null)
                return PageInsertSql(projectConfig);
            var soluction = config as SolutionConfig;
            if (soluction == null)
                return null;
            StringBuilder code = new StringBuilder();
            foreach (var project in SolutionConfig.Current.Projects)
                code.AppendLine(PageInsertSql(project));
            return code.ToString();
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
            foreach (var entity in project.Entities.Where(p => !p.IsClass))
                sb.Append($@"
INSERT INTO [tb_sys_page_item[ ([ItemType],[Name],[Caption],[Url],[Memo],[ParentId])
VALUES(2,'{entity.Name}','{entity.Caption}','/{entity.Parent.Name}/{entity.Name}/Index.aspx','{entity.Description}',@pid);");
            return sb.ToString();
        }
        #endregion

        #region 表结构


        public static string CreateTableCode(EntityConfig entity, bool signle = false)
        {
            if (entity.IsClass)
                return "这个设置为普通类，无法生成SQL";
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
CREATE TABLE [{entity.SaveTable}](");
            bool isFirst = true;
            foreach (PropertyConfig col in entity.DbFields.Where(p => !p.IsCompute))
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
---------------------------主键-------------------------------
ALTER TABLE dbo.{entity.SaveTableName} ADD CONSTRAINT
	PK_{entity.SaveTableName} PRIMARY KEY CLUSTERED 
	(
	    {entity.PrimaryField}
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO
---------------------------注释-------------------------------
DECLARE @v sql_variant 
SET @v = N'{entity.Caption?.Replace('\'', '‘')}:{entity.Description?.Replace('\'', '‘')}'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'{
                    entity.SaveTableName
                }', NULL, NULL;");

            foreach (PropertyConfig col in entity.DbFields.Where(p => !p.IsCompute))
            {
                code.Append($@"
SET @v = N'{col.Caption?.Replace('\'', '‘')}:{col.Description?.Replace('\'', '‘')}'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Table_1', N'COLUMN', N'{
                        col.ColumnName
                    }';");
            }
        }

        private static string AddColumnCode(EntityConfig entity)
        {
            if (entity.IsClass)
                return null;
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
ALTER TABLE [{entity.SaveTable}]");
            bool isFirst = true;
            foreach (PropertyConfig col in entity.DbFields.Where(p => !p.IsCompute))
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
            if (entity == null || entity.IsClass)
                return null;
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
ALTER TABLE [{entity.SaveTable}]");
            bool isFirst = true;
            foreach (PropertyConfig col in entity.DbFields.Where(p => !p.IsCompute))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');
                code.Append($@"
    ALTER COLUMN [{col.ColumnName}] {FieldDefault(col)}");
            }
            code.Append(@";");
            MemCode(entity, code);
            return code.ToString();
        }

        private static string FieldDefault(PropertyConfig col)
        {
            var def = (col.Initialization == null)
                ? null
                : col.CsType == "string" ? $"DEFAULT '{col.Initialization}'" : $"DEFAULT {col.Initialization}";
            var nulldef = col.CsType == "string" || col.DbNullable ? " NULL" : " NOT NULL";
            var identity = (col.IsIdentity ? " IDENTITY(1,1)" : null);
            return $"[{col.ColumnName}] {SqlServerHelper.ColumnType(col)}{identity}{nulldef} {def} -- '{col.Caption}'";
        }

        #endregion

        #region 数据读取

        public static string LoadEntityCode(EntityConfig entity, IEnumerable<PropertyConfig> fields)
        {
            var code = new StringBuilder();
            code.Append($@"

        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name=""reader"">数据读取器</param>
        /// <param name=""entity"">读取数据的实体</param>
        public void LoadEntity(MySqlDataReader reader,{entity.EntityName} entity)
        {{
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {{");
            var idx = 0;
            foreach (var field in fields)
            {
                FieldReadCode(field, code, idx++);
            }
            code.Append(@"
            }
        }");
            return code.ToString();
        }

        public static string LoadSql(IEnumerable<PropertyConfig> fields)
        {
            var sql = new StringBuilder();

            var isFirst = true;
            foreach (var field in fields)
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
    [{0}] AS [{1}]", field.ColumnName, field.Name);
            }
            return sql.ToString();
        }

        /// <summary>
        /// 字段数据库读取代码
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="code">代码</param>
        /// <param name="idx">序号</param>
        public static void FieldReadCode(PropertyConfig field, StringBuilder code, int idx)
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
            if (field.DbNullable)
                code.Append($@"
                if (!reader.IsDBNull({idx}))
                    ");
            else
                code.Append(@"
                ");

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