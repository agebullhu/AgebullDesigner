using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.Mysql;
using Agebull.EntityModel.Config.V2021;
using Agebull.EntityModel.Designer;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using static System.String;

namespace Agebull.EntityModel.RobotCoder.DataBase.MySql
{
    /// <summary>
    /// SQL代码片断
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    public class SqlMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region 注册

        /// <summary>
        /// 执行自动注册
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "生成表(SQL)", "sql", CreateTable);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "插入表字段(SQL)", "sql", AddColumnCode);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "插入缺少字段(SQL)", "sql", ChangeColumnCode2);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "修改表字段(SQL)", "sql", ChangeColumnCode);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "修改表名(SQL)", "sql", ReName);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "修改字段名(SQL)", "sql", ChangeColumnName);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "修改主键字段名(SQL)", "sql", ChangeColumnName2);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "修改BOOL字段(SQL)", "sql", ChangeBoolColumnCode);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "生成视图(SQL)", "sql", CreateView);
            CoderManager.RegisteCoder<ProjectConfig>("MySql", "插入页面表(SQL)", "sql", PageInsertSql);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "删除视图(SQL)", "sql", DropView);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "删除表(SQL)", "sql", DropTable);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "清除表(SQL)", "sql", TruncateTable);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "添加全部索引", "sql", AddIndex);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "添加关键索引", "sql", AddRefIndex);
            CoderManager.RegisteCoder<DataTableConfig>("MySql", "添加外键", "sql", AddRelation);
        }
        #endregion

        #region 名称规范


        public static string ReName(DataTableConfig dataTable)
        {
            return IsNullOrWhiteSpace(dataTable.OldName)
                ? null
                : $@"
ALTER TABLE `{dataTable.OldName}` RENAME TO `{dataTable.SaveTableName}`;";
        }

        #endregion

        #region 索引


        public static string AddIndex(DataTableConfig dataTable)
        {
            var code = new StringBuilder();
            code.Append($@"
ALTER TABLE  `{dataTable.SaveTableName}`");
            bool isFirst = true;
            foreach (var field in dataTable.FindLastAndToArray(p => p.NeedDbIndex))
            {
                if (isFirst)
                    isFirst = false;
                else code.Append(',');
                if (field.Property.IsPrimaryKey || field.IsIdentity)
                {
                    code.Append($@"
    ADD UNIQUE (`{field.DbFieldName}`)");
                    continue;
                }
                if (field.IsText || field.IsBlob)
                {
                    code.Append($@"
    ADD FULLTEXT (`{field.DbFieldName}`)");
                    continue;
                }
                code.Append($@"
    ADD INDEX {field.Name}_Index (`{field.DbFieldName}`)");
            }

            if (dataTable.AnyLast(p => p.Property.UniqueIndex))
            {
                isFirst = true;
                code.Append($@"
    ADD INDEX {dataTable.Name}_Unique_Index (");
                foreach (var field in dataTable.FindLastAndToArray(p => p.Property.UniqueIndex))
                {
                    if (isFirst)
                        isFirst = false;
                    else code.Append(',');
                    code.Append($"`{field.DbFieldName}`");
                }
                code.Append(')');
            }
            code.Append(';');
            return code.ToString();
        }

        public static string AddRefIndex(DataTableConfig dataTable)
        {
            var code = new StringBuilder();
            code.Append($@"
ALTER TABLE  `{dataTable.SaveTableName}`");
            bool isFirst = true;
            foreach (var field in dataTable.FindLastAndToArray(p => p.NeedDbIndex && p.Property.IsSystemField))
            {
                if (isFirst)
                    isFirst = false;
                else code.Append(',');
                if (field.Property.IsPrimaryKey || field.IsIdentity)
                {
                    code.Append($@"
    ADD UNIQUE (`{field.DbFieldName}`)");
                    continue;
                }
                if (field.IsText || field.IsBlob)
                {
                    code.Append($@"
    ADD FULLTEXT (`{field.DbFieldName}`)");
                    continue;
                }
                code.Append($@"
    ADD INDEX {field.Name}_Index (`{field.DbFieldName}`)");
            }

            if (dataTable.AnyLast(p => p.Property.UniqueIndex))
            {
                isFirst = true;
                code.Append($@"
    ADD INDEX {dataTable.Name}_Unique_Index (");
                foreach (var field in dataTable.FindLastAndToArray(p => p.Property.UniqueIndex))
                {
                    if (isFirst)
                        isFirst = false;
                    else code.Append(',');
                    code.Append($"`{field.DbFieldName}`");
                }
                code.Append(')');
            }
            code.Append(';');
            return code.ToString();
        }

        #endregion
        #region 外键


        public static string AddRelation(DataTableConfig dataTable)
        {
            var fields = dataTable.FindLastAndToArray(p => p.IsLinkKey).ToArray();
            if (fields.Length == 0)
                return "";
            var code = new StringBuilder();
            code.Append($@"
/*{dataTable.Caption}*/
ALTER TABLE  `{dataTable.SaveTableName}`");
            bool isFirst = true;
            foreach (var field in fields)
            {
                var rela = (dataTable.Entity.Project.Find(field.LinkTable) ?? GlobalConfig.GetEntity(field.LinkTable)).DataTable;
                if (rela == null)
                    continue;
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');

                code.Append($@"
    /*{rela.Caption}: {field.DbFieldName} <=> {rela.PrimaryField.DbFieldName}*/
    ADD CONSTRAINT `{rela.Name}` FOREIGN KEY (`{field.DbFieldName}`) 
        REFERENCES `{rela.SaveTableName}` (`{rela.PrimaryField.DbFieldName}`)");

            }
            code.Append(';');
            return code.ToString();
        }


        #endregion
        #region 数据库
        public static string TruncateTable(DataTableConfig dataTable)
        {
            return $@"
/*******************************{dataTable.Caption}*******************************/
TRUNCATE TABLE `{dataTable.SaveTableName}`;
";
        }


        public static string DropView(DataTableConfig dataTable)
        {
            if (dataTable.SaveTableName == dataTable.ReadTableName)
                return Empty;
            return $@"
/*******************************{dataTable.Caption}*******************************/
DROP VIEW `{dataTable.ReadTableName}`;
";
        }
        public static string CreateView(DataTableConfig dataTable)
        {
            var array = dataTable.FindLastAndToArray(p => p.IsLinkField && !p.IsLinkKey).ToArray();
            if (array.Length == 0)
            {
                return $"/**********{dataTable.Caption}**********没有字段引用其它表的无需视图**********/";
            }
            var tables = dataTable.FindLastAndToArray(p => p.IsLinkField && !p.IsLinkKey).Select(p => p.LinkTable).Distinct().Select(GlobalConfig.GetEntity).ToDictionary(p => p.Name);
            if (tables.Count == 0)
            {
                dataTable.ReadTableName = dataTable.SaveTableName; ;
                return $"/**********{dataTable.Caption}**********没有字段引用其它表的无需视图**********/";
            }
            string viewName;
            if (IsNullOrWhiteSpace(dataTable.ReadTableName) || dataTable.ReadTableName == dataTable.SaveTableName)
            {
                viewName = DataBaseHelper.ToViewName(dataTable.Entity);
            }
            else
            {
                viewName = dataTable.ReadTableName;
            }
            var builder = new StringBuilder();
            builder.Append($@"
/*******************************{dataTable.Caption}*******************************/
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `{viewName}` AS 
    SELECT ");
            bool first = true;
            foreach (var field in dataTable.Last())
            {
                if (first)
                    first = false;
                else
                    builder.Append(@",
        ");

                if (field.IsLinkField && !field.IsLinkKey && !IsNullOrWhiteSpace(field.LinkTable))
                {
                    if (tables.TryGetValue(field.LinkTable, out var friend))
                    {
                        var linkField = friend.DataTable.Find(p => p.DbFieldName == field.LinkField || p.Name == field.LinkField);
                        if (linkField != null)
                        {
                            builder.Append($@"`{friend.Name}`.`{linkField.DbFieldName}` as `{field.DbFieldName}`");
                            continue;
                        }
                    }
                }
                builder.Append($@"`{dataTable.Name}`.`{field.DbFieldName}` as `{field.DbFieldName}`");
            }
            builder.Append($@"
    FROM `{dataTable.SaveTableName}` `{dataTable.Name}`");
            foreach (var table in tables.Values)
            {
                var field = dataTable.FindLast(p => p.IsLinkKey && (p.LinkTable == table.DataTable.SaveTableName || p.LinkTable == table.Name));
                if (field == null)
                    continue;
                var linkField = table.DataTable.FindLast(p => p.Name == field.LinkField || p.DbFieldName == field.LinkField);
                if (linkField == null)
                    continue;
                builder.Append($@"
    LEFT JOIN `{table.DataTable.SaveTableName}` `{table.Name}` ON `{dataTable.Name}`.`{field.DbFieldName}` = `{table.Name}`.`{linkField.DbFieldName}`");
            }
            builder.Append(';');
            builder.AppendLine();
            return builder.ToString();
        }
        private static string DropTable(DataTableConfig dataTable)
        {
            return $@"
/*******************************{dataTable.Caption}*******************************/
DROP TABLE IF EXISTS `{dataTable.SaveTableName}`;";
            //            return string.Format(@"
            //truncate TABLE dbo.{0};
            //GO
            //--INSERT INTO COC_NEW.dbo.{0} value({1})
            //--SELECT {1} FROM coc.dbo.{0};
            //--GO", Entity.TableName, Entity.PublishProperty.Select(p => p.ColumnName).LinkToString(','));
        }
        private string CreateTable(DataTableConfig dataTable)
        {
            return CreateTableCode(dataTable);
            //var builder = new StringBuilder();
            //builder.AppendLine(DropTable());
            //builder.AppendLine(SqlSchemaChecker.CreateTableCode(Entity));
            //builder.AppendLine(DropView(Entity));
            //builder.AppendLine(CreateView(Entity));

            //return builder.ToString();
        }


        private static string PageInsertSql(ProjectConfig project)
        {
            StringBuilder sb = new();
            sb.Append(
                $@"
/*{project.Caption}*/
INSERT INTO `tb_app_page_item` (`item_type`,`name`,`caption`,`url`,`memo`,`parent_id`)
VALUES(0,'{project.Caption}','{project.Caption}',NULL,'{project.Description}',0);
set @pid = @@IDENTITY;");
            foreach (var dataTable in project.Entities.Where(p => p.EnableDataBase))
                sb.Append($@"
INSERT INTO `tb_app_page_item` (`item_type`,`name`,`caption`,`url`,`memo`,`parent_id`)
VALUES(2,'{dataTable.Name}','{dataTable.Caption}','/{dataTable.Project.Name}/{dataTable.Classify}/{dataTable.Name}/index','{dataTable.Description}',@pid);");
            return sb.ToString();
        }
        #endregion

        #region 表结构


        public static string CreateTableCode(DataTableConfig dataTable)
        {
            if (dataTable == null || !dataTable.Entity.EnableDataBase)
                return "";

            var code = new StringBuilder();
            code.Append($@"
/*{dataTable.Caption}*/
CREATE TABLE `{dataTable.SaveTableName}`(");

            bool isFirst = true;
            foreach (var field in dataTable.Last())
            {
                code.Append($@"
   {(isFirst ? "" : ",")}{FieldDefault(field)}");
                isFirst = false;
            }
            var primary = dataTable.PrimaryField;
            if (dataTable.Entity.PrimaryColumn != null)
                code.Append($@"
    , PRIMARY KEY (`{primary.DbFieldName}`)");
            //if (dataTable.PrimaryColumn.IsIdentity)
            var uns = dataTable.FindLastAndToArray(p => p.Property.UniqueIndex);
            if (uns.Any())
            {
                code.Append($@"
    , UNIQUE INDEX `{uns.Select(p => p.DbFieldName).LinkToString('-')}`(");
                bool first = true;
                foreach (var un in uns)
                {
                    if (first) first = false;
                    else code.Append(',');
                    code.Append($"`{un.DbFieldName}`");
                }
                code.Append(@") USING BTREE");
            }
            code.Append($@"
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 DEFAULT COLLATE utf8mb4_unicode_ci COMMENT '{dataTable.Caption}'");
            //if (dataTable.PrimaryColumn.IsIdentity)
            //    code.Append(@" AUTO_INCREMENT=1");
            code.Append(';');
            return code.ToString();
        }

        private static string AddColumnCode(DataTableConfig dataTable)
        {
            if (dataTable == null)
                return "";
            var code = new StringBuilder();
            code.Append($@"
/*{dataTable.Caption}*/
ALTER TABLE `{dataTable.SaveTableName}`");
            bool isFirst = true;
            foreach (var col in dataTable.Last())
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');
                code.Append($@"
    ADD COLUMN {FieldDefault(col)}");
            }
            code.Append(';');
            return code.ToString();
        }

        private string ChangeBoolColumnCode(DataTableConfig dataTable)
        {
            if (dataTable == null)
                return "";
            var code = new StringBuilder();
            foreach (var ent in SolutionConfig.Current.Entities)
                ChangeBoolColumnCode(code, ent.DataTable);
            return code.ToString();
        }

        private void ChangeBoolColumnCode(StringBuilder code, DataTableConfig dataTable)
        {
            if (dataTable == null)
                return;
            var fields = dataTable.FindLastAndToArray(p => !p.IsReadonly && p.Property.CsType == "bool").ToArray();

            code.Append($@"
/*{dataTable.Caption}*/
ALTER TABLE `{dataTable.SaveTableName}`");
            bool isFirst = true;
            foreach (var col in fields)
            {
                col.FieldType = "BOOL";
                col.Property.Initialization = "0";
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');
                code.Append($@"
    CHANGE COLUMN `{col.DbFieldName}` {FieldDefault(col)}");
            }
            code.Append(';');
        }

        private string ChangeColumnName2(DataTableConfig dataTable)
        {
            if (dataTable == null)
                return "";
            var code = new StringBuilder();
            if (dataTable.Entity.PrimaryColumn != null)
            {
                var primary = dataTable.PrimaryField;
                code.Append($@"
/*{dataTable.Caption}*/
ALTER TABLE `{dataTable.SaveTableName}`
    CHANGE COLUMN `{primary.OldName}` {FieldDefault(primary)};");
            }
            return code.ToString();
        }
        private string ChangeColumnName(DataTableConfig dataTable)
        {
            if (dataTable == null)
                return "";
            var code = new StringBuilder();
            code.Append($@"
/*{dataTable.Caption}*/
ALTER TABLE `{dataTable.SaveTableName}`");
            bool isFirst = true;
            foreach (var col in dataTable.FindLastAndToArray(p => !p.IsReadonly))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');
                code.Append($@"
    CHANGE COLUMN `{col.OldName}` {FieldDefault(col)}");
            }
            code.Append(';');
            return code.ToString();
        }

        private string ChangeColumnCode(DataTableConfig dataTable)
        {
            if (dataTable == null)
                return "";
            var code = new StringBuilder();
            code.Append($@"
/*{dataTable.Caption}*/
ALTER TABLE `{dataTable.SaveTableName}`");
            bool isFirst = true;
            foreach (var col in dataTable.FindLastAndToArray(p => !p.IsReadonly))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');
                code.Append($@"
    CHANGE COLUMN `{col.DbFieldName}` {FieldDefault(col)}");
            }
            code.Append(';');
            return code.ToString();
        }

        private static string ChangeColumnCode2(DataTableConfig dataTable)
        {
            if (dataTable == null || !dataTable.Entity.Interfaces.Contains("IHistory"))
                return "";
            var code = new StringBuilder();
            code.Append($@"
/*{dataTable.Caption}*/
ALTER TABLE `{dataTable.SaveTableName}`
    CHANGE COLUMN `last_reviser_id` `last_reviser_id` BIGINT NULL DEFAULT 0 COMMENT '最后修改者标识',
    CHANGE COLUMN `author_id` `author_id` BIGINT NOT NULL DEFAULT 0 COMMENT '制作人标识';");
            return code.ToString();
        }

        private static string FieldDefault(DataBaseFieldConfig col)
        {
            return $"`{col.DbFieldName}` {MySqlDataBaseHelper.ColumnType(col)}{NullKeyWord(col)} {ColumnDefault(col)} COMMENT '{col.Caption}'";
        }

        private static string NullKeyWord(DataBaseFieldConfig col)
        {
            return !col.Property.IsPrimaryKey && !col.Property.UniqueIndex && !col.Property.IsGlobalKey && (col.Property.CsType == "string" || col.DbNullable)
                ? " NULL" : " NOT NULL";
        }

        private static string ColumnDefault(DataBaseFieldConfig col)
        {
            if (col.IsIdentity)
                return " AUTO_INCREMENT";
            if (col.Property.IsPrimaryKey)
                return Empty;
            if (col.Property.Initialization.IsMissing())
            {
                if (col.DbNullable)
                    return null;
                if (col.Property.CsType.IsNumberType())
                    return "DEFAULT 0";
                return null;
            }
            return col.Property.CsType == "string" ? $"DEFAULT '{col.Property.Initialization}'" : $"DEFAULT {col.Property.Initialization}";
        }

        #endregion

        #region 数据读取

        public static string LoadEntityCode(DataTableConfig dataTable, IEnumerable<DataBaseFieldConfig> fields)
        {
            var code = new StringBuilder();
            code.Append($@"

        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name=""reader"">数据读取器</param>
        /// <param name=""entity"">读取数据的实体</param>
        public void LoadEntity(MySqlDataReader reader,{dataTable.Entity.EntityName} entity)
        {{");
            int idx = 0;
            foreach (var field in fields)
            {
                FieldReadCode(field, code, idx++);
            }
            code.Append(@"
        }");
            return code.ToString();
        }

        public static string LoadSql(DataTableConfig dataTable)
        {
            var fields = dataTable.FindLastAndToArray(p => !p.DbInnerField && !p.NoStorage && !p.KeepStorageScreen.HasFlag(StorageScreenType.Read)).ToArray();
            var isFirst = true;
            var all = fields.All(p => p.Property.IsInterfaceField || p.Entity == dataTable.Entity);
            var lines = new List<string>();
            var sql = new StringBuilder();
            foreach (var field in fields)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sql.Append(',');
                }
                var head = all
                    ? ""
                    : $"`{field.Entity.DataTable.ReadTableName}`.";
                if (!IsNullOrEmpty(field.Function))
                {
                    sql.Append($"{field.Function}({head}`{field.DbFieldName}`) AS `{field.DbFieldName}`");
                }
                else
                {
                    sql.Append($"{head}`{field.DbFieldName}`");
                    if (field.DbFieldName != field.DbFieldName)
                        sql.Append($" AS `{field.DbFieldName}`");
                }
                if (sql.Length > 100)
                {
                    lines.Add(sql.ToString());
                    sql.Clear();
                }
            }
            if (sql.Length > 0)
                lines.Add(sql.ToString());
            return Join("\r\n", lines);
        }

        public static string HavingSql(DataTableConfig dataTable)
        {
            var fields = dataTable.FindLastAndToArray(p => !IsNullOrEmpty(p.Having) && !IsNullOrEmpty(p.Function) &&
            !p.KeepStorageScreen.HasFlag(StorageScreenType.Read)).ToArray();
            if (fields.Length == 0)
                return "null";
            var all = fields.All(p => p.Property.IsInterfaceField || p.Entity == dataTable.Entity);
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
                    sql.Append(',');
                }
                var head = all
                    ? ""
                    : $"`{field.Entity.DataTable.ReadTableName}`.";
                sql.Append($"`{field.Function}({head}`{field.DbFieldName}`) {field.Having}");
            }
            return isFirst ? "null" : $"\"\\nHAVING {sql}\"";
        }

        public static string GroupSql(DataTableConfig dataTable)
        {
            var fields = dataTable.FindLastAndToArray(p => p.Function.IsPresent()).ToArray();
            if (fields.Length == 0)
                return "null";
            var all = fields.All(p => p.Property.IsInterfaceField || p.Entity == dataTable.Entity);
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
                    sql.Append(',');
                }
                var head = all
                    ? ""
                    : $"`{field.Entity.DataTable.ReadTableName}`.";
                sql.Append($"{head}.`{field.DbFieldName}`");
            }
            return isFirst ? "null" : $"\"\\nGROUP BY {sql}\"";
        }

        /// <summary>
        /// 字段数据库读取代码
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="field">字段</param>
        /// <param name="code">代码</param>
        /// <param name="idx"></param>
        public static void FieldReadCode(DataBaseFieldConfig field, StringBuilder code, int idx)
        {
            if (field.Property.CsType.ToLower() == "string")
            {
                switch (field.FieldType.ToLower())
                {
                    case "char":
                    case "varchar":
                    case "text":
                    case "longtext":
                    case "varstring":
                        code.Append($@"
            if (reader.IsDBNull({idx}))
                entity.{field.Name} = null;
            else
                entity.{field.Name} = await reader.GetFieldValueAsync<string>({idx});");
                        break;
                    default:
                        code.Append($@"
            if (reader.IsDBNull({idx}))
                entity.{field.Name} = null;
            else
                entity.{field.Name} = (await reader.GetFieldValueAsync<{field.Property.CsType}>({idx})).ToString();");
                        break;
                }
                return;

            }

            if (!IsNullOrWhiteSpace(field.Property.CustomType))
            {
                if (field.DbNullable)
                    code.Append($@"
            if (reader.IsDBNull({idx}))
                entity.{field.Name} = default;
            else 
                entity.{field.Name} = ({field.Property.CustomType})(await reader.GetFieldValueAsync<int>({idx}));");
                else
                    code.Append($@"
            entity.{field.Name} = ({field.Property.CustomType})(await reader.GetFieldValueAsync<int>({idx}));");
                return;
            }

            if (field.DbNullable)
                code.Append($@"
            if (reader.IsDBNull({idx}))
                entity.{field.Name} = default;
            else
                entity.{field.Name} = await reader.GetFieldValueAsync<{field.Property.CsType}>({idx});");
            else
                code.Append($@"
            entity.{field.Name} = await reader.GetFieldValueAsync<{field.Property.CsType}>({idx});");

        }
        /// <summary>
        /// 字段数据库读取代码
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="property">字段</param>
        /// <param name="code">代码</param>
        /// <param name="idx"></param>
        public static void FieldReadCode(PropertyConfig property, StringBuilder code, int idx)
        {

            if (property.CsType.IsMe("string"))
            {
                switch (property.DataBaseField.FieldType.ToLower())
                {
                    case "char":
                    case "varchar":
                    case "text":
                    case "longtext":
                    case "varstring":
                        code.Append($@"
            if (reader.IsDBNull({idx}))
                entity.{property.Name} = null;
            else
                entity.{property.Name} = await reader.GetFieldValueAsync<string>({idx});");
                        break;
                    default:
                        code.Append($@"
            if (reader.IsDBNull({idx}))
                entity.{property.Name} = null;
            else
                entity.{property.Name} = (await reader.GetFieldValueAsync<{property.CsType}>({idx})).ToString();");
                        break;
                }
                return;

            }

            if (property.CustomType.IsPresent())
            {
                code.Append($@"
            entity.{property.Name} = ({property.CustomType})(await reader.GetFieldValueAsync<int>({idx}));");
                return;
            }

            if (property.DataBaseField.DbNullable)
                code.Append($@"
            if (reader.IsDBNull({idx}))
                entity.{property.Name} = default;
            else
                entity.{property.Name} = await reader.GetFieldValueAsync<{property.CsType}>({idx});");
            else
                code.Append($@"
            entity.{property.Name} = await reader.GetFieldValueAsync<{property.CsType}>({idx});");

        }

        #endregion
    }
}