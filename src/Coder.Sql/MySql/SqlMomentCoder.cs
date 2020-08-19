using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.Mysql;
using Agebull.EntityModel.Designer;
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
            MomentCoder.RegisteCoder<EntityConfig>("MySql", "生成表(SQL)", "sql", p => !p.NoDataBase, CreateTable);
            MomentCoder.RegisteCoder("MySql", "插入表字段(SQL)", "sql", p => !p.NoDataBase, AddColumnCode);
            MomentCoder.RegisteCoder("MySql", "插入缺少字段(SQL)", "sql", p => !p.NoDataBase, ChangeColumnCode2);
            MomentCoder.RegisteCoder("MySql", "修改表字段(SQL)", "sql", p => !p.NoDataBase, ChangeColumnCode);
            MomentCoder.RegisteCoder("MySql", "修改表名(SQL)", "sql", p => !p.NoDataBase, ReName);
            MomentCoder.RegisteCoder("MySql", "修改字段名(SQL)", "sql", p => !p.NoDataBase, ChangeColumnName);
            MomentCoder.RegisteCoder("MySql", "修改主键字段名(SQL)", "sql", p => !p.NoDataBase, ChangeColumnName2);
            MomentCoder.RegisteCoder("MySql", "修改BOOL字段(SQL)", "sql", p => !p.NoDataBase, ChangeBoolColumnCode);
            MomentCoder.RegisteCoder("MySql", "生成视图(SQL)", "sql", p => !p.NoDataBase, CreateView);
            MomentCoder.RegisteCoder<ProjectConfig>("MySql", "插入页面表(SQL)", "sql", PageInsertSql);
            MomentCoder.RegisteCoder("MySql", "删除视图(SQL)", "sql", p => !p.NoDataBase, DropView);
            MomentCoder.RegisteCoder("MySql", "删除表(SQL)", "sql", p => !p.NoDataBase, DropTable);
            MomentCoder.RegisteCoder("MySql", "清除表(SQL)", "sql", p => !p.NoDataBase, TruncateTable);
            MomentCoder.RegisteCoder("MySql", "添加全部索引", "sql", p => !p.NoDataBase, AddIndex);
            MomentCoder.RegisteCoder("MySql", "添加关键索引", "sql", p => !p.NoDataBase, AddRefIndex);
        }
        #endregion

        #region 名称规范


        public static string ReName(EntityConfig entity)
        {
            return IsNullOrWhiteSpace(entity.OldName)
                ? null
                : $@"
ALTER TABLE `{entity.OldName}` RENAME TO `{entity.SaveTable}`;";
        }

        #endregion

        #region 索引


        public static string AddIndex(EntityConfig entity)
        {
            var code = new StringBuilder();
            code.Append($@"
ALTER TABLE  `{entity.SaveTable}`");
            bool isFirst = true;
            foreach (var property in entity.DbFields.Where(p => p.CreateDbIndex))
            {
                if (isFirst)
                    isFirst = false;
                else code.Append(',');
                if (property.IsPrimaryKey || property.IsIdentity)
                {
                    code.Append($@"
    ADD UNIQUE (`{property.DbFieldName}`)");
                    continue;
                }
                if (property.IsMemo || property.IsBlob)
                {
                    code.Append($@"
    ADD FULLTEXT (`{property.DbFieldName}`)");
                    continue;
                }
                code.Append($@"
    ADD INDEX {property.Name}_Index (`{property.DbFieldName}`)");
            }

            if (entity.DbFields.Any(p => p.UniqueIndex > 0))
            {
                isFirst = true;
                code.Append($@"
    ADD INDEX {entity.Name}_Unique_Index (");
                foreach (var property in entity.DbFields.Where(p => p.UniqueIndex > 0))
                {
                    if (isFirst)
                        isFirst = false;
                    else code.Append(',');
                    code.Append($"`{property.DbFieldName}`");
                }
                code.Append(")");
            }
            code.Append(";");
            return code.ToString();
        }

        public static string AddRefIndex(EntityConfig entity)
        {
            var code = new StringBuilder();
            code.Append($@"
ALTER TABLE  `{entity.SaveTable}`");
            bool isFirst = true;
            foreach (var property in entity.DbFields.Where(p => p.CreateDbIndex && p.IsSystemField))
            {
                if (isFirst)
                    isFirst = false;
                else code.Append(',');
                if (property.IsPrimaryKey || property.IsIdentity)
                {
                    code.Append($@"
    ADD UNIQUE (`{property.DbFieldName}`)");
                    continue;
                }
                if (property.IsMemo || property.IsBlob)
                {
                    code.Append($@"
    ADD FULLTEXT (`{property.DbFieldName}`)");
                    continue;
                }
                code.Append($@"
    ADD INDEX {property.Name}_Index (`{property.DbFieldName}`)");
            }

            if (entity.DbFields.Any(p => p.UniqueIndex > 0))
            {
                isFirst = true;
                code.Append($@"
    ADD INDEX {entity.Name}_Unique_Index (");
                foreach (var property in entity.DbFields.Where(p => p.UniqueIndex > 0))
                {
                    if (isFirst)
                        isFirst = false;
                    else code.Append(',');
                    code.Append($"`{property.DbFieldName}`");
                }
                code.Append(")");
            }
            code.Append(";");
            return code.ToString();
        }

        #endregion


        #region 数据库
        public static string TruncateTable(EntityConfig entity)
        {
            return $@"
/*******************************{entity.Caption}*******************************/
TRUNCATE TABLE `{entity.SaveTable}`;
";
        }


        public static string DropView(EntityConfig entity)
        {
            if (entity.SaveTable == entity.ReadTableName)
                return Empty;
            return $@"
/*******************************{entity.Caption}*******************************/
DROP VIEW `{entity.ReadTableName}`;
";
        }
        public static string CreateView(EntityConfig entity)
        {
            DataBaseHelper.CheckFieldLink(entity);
            var array = entity.DbFields.Where(p => p.IsLinkField && !p.IsLinkKey).ToArray();
            if (array.Length == 0)
            {
                return $"/**********{entity.Caption}**********没有字段引用其它表的无需视图**********/";
            }
            var tables = entity.DbFields.Where(p => p.IsLinkField && !p.IsLinkKey).Select(p => p.LinkTable).Distinct().Select(GlobalConfig.GetEntity).ToDictionary(p => p.Name);
            if (tables.Count == 0)
            {
                entity.ReadTableName = entity.SaveTable; ;
                return $"/**********{entity.Caption}**********没有字段引用其它表的无需视图**********/";
            }
            string viewName;
            if (IsNullOrWhiteSpace(entity.ReadTableName) || entity.ReadTableName == entity.SaveTable)
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
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `{viewName}` AS 
    SELECT ");
            bool first = true;
            foreach (PropertyConfig field in entity.DbFields)
            {
                if (first)
                    first = false;
                else
                    builder.Append(@",
        ");

                if (field.IsLinkField && !field.IsLinkKey && !IsNullOrWhiteSpace(field.LinkTable))
                {
                    if (tables.TryGetValue(field.LinkTable, out EntityConfig friend))
                    {
                        var linkField = friend.Properties.FirstOrDefault(p => p.DbFieldName == field.LinkField || p.Name == field.LinkField);
                        if (linkField != null)
                        {
                            builder.Append($@"`{friend.Name}`.`{linkField.DbFieldName}` as `{field.DbFieldName}`");
                            continue;
                        }
                    }
                }
                builder.Append($@"`{entity.Name}`.`{field.DbFieldName}` as `{field.DbFieldName}`");
            }
            builder.Append($@"
    FROM `{entity.SaveTable}` `{entity.Name}`");
            foreach (var table in tables.Values)
            {
                var field = entity.DbFields.FirstOrDefault(p => p.IsLinkKey && (p.LinkTable == table.SaveTable || p.LinkTable == table.Name));
                if (field == null)
                    continue;
                var linkField = table.Properties.FirstOrDefault(p => p.Name == field.LinkField || p.DbFieldName == field.LinkField);
                if (linkField == null)
                    continue;
                builder.Append($@"
    LEFT JOIN `{table.SaveTable}` `{table.Name}` ON `{entity.Name}`.`{field.DbFieldName}` = `{table.Name}`.`{linkField.DbFieldName}`");
            }
            builder.Append(';');
            builder.AppendLine();
            return builder.ToString();
        }
        private static string DropTable(EntityConfig entity)
        {
            if (entity.NoDataBase)
                return Empty;
            return $@"
/*******************************{entity.Caption}*******************************/
DROP TABLE `{entity.SaveTable}`;";
            //            return string.Format(@"
            //truncate TABLE dbo.{0};
            //GO
            //--INSERT INTO COC_NEW.dbo.{0} value({1})
            //--SELECT {1} FROM coc.dbo.{0};
            //--GO", Entity.TableName, Entity.PublishProperty.Select(p => p.ColumnName).LinkToString(','));
        }
        private string CreateTable(EntityConfig config)
        {
            return CreateTableCode(config);
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
INSERT INTO `tb_app_page_item` (`item_type`,`name`,`caption`,`url`,`memo`,`parent_id`)
VALUES(0,'{project.Caption}','{project.Caption}',NULL,'{project.Description}',0);
set @pid = @@IDENTITY;");
            foreach (var entity in project.Entities.Where(p => !p.NoDataBase))
                sb.Append($@"
INSERT INTO `tb_app_page_item` (`item_type`,`name`,`caption`,`url`,`memo`,`parent_id`)
VALUES(2,'{entity.Name}','{entity.Caption}','/{entity.Parent.Name}/{entity.Classify}/{entity.Name}/index','{entity.Description}',@pid);");
            return sb.ToString();
        }
        #endregion

        #region 表结构


        public static string CreateTableCode(EntityConfig entity, bool signle = false)
        {
            if (entity == null)
                return "";
            if (entity.NoDataBase)
                return $"/*{entity.Caption} : 普通类(NoDataBase=true)无法生成SQL*/";
            var code = new StringBuilder();
            code.AppendFormat(@"
/*{1}*/
CREATE TABLE `{0}`("
                , entity.SaveTable
                , entity.Caption);

            bool isFirst = true;
            foreach (PropertyConfig col in entity.DbFields.Where(p => !p.IsCompute))
            {
                code.Append($@"
   {(isFirst ? "" : ",")}{FieldDefault(col)}");
                isFirst = false;
            }
            if (entity.PrimaryColumn != null)
                code.Append($@"
    ,PRIMARY KEY (`{entity.PrimaryColumn.DbFieldName}`)");
            //if (entity.PrimaryColumn.IsIdentity)

            code.Append($@"
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 DEFAULT COLLATE utf8mb4_unicode_ci COMMENT '{entity.Caption}'");
            //if (entity.PrimaryColumn.IsIdentity)
            //    code.Append(@" AUTO_INCREMENT=1");
            code.Append(@";");
            return code.ToString();
        }

        private static string AddColumnCode(EntityConfig entity)
        {
            if (entity == null)
                return "";
            if (entity.NoDataBase)
                return $"/*{entity.Caption} : 普通类(NoDataBase=true)无法生成SQL*/";
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
ALTER TABLE `{entity.SaveTable}`");
            bool isFirst = true;
            foreach (PropertyConfig col in entity.DbFields.Where(p => !p.IsCompute))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');
                code.Append($@"
    ADD COLUMN {FieldDefault(col)}");
            }
            code.Append(@";");
            return code.ToString();
        }

        private static string AddColumnCode2(EntityConfig entity)
        {
            if (entity == null || entity.NoDataBase || !entity.Interfaces.Contains("IHistory"))
                return "";
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
ALTER TABLE `{entity.SaveTable}`
    ADD COLUMN `author` varchar(255) NULL ,
    ADD COLUMN `last_reviser` varchar(255) NULL;");
            return code.ToString();
        }


        //static string ChangeColumnCode(ConfigBase config)
        //{
        //    return ChangeColumnCode(config as EntityConfig);
        //}

        private string ChangeBoolColumnCode(EntityConfig entity)
        {
            if (entity == null)
                return "";
            if (entity.NoDataBase)
                return $"/*{entity.Caption} : 普通类(NoDataBase=true)无法生成SQL*/";
            var code = new StringBuilder();
            foreach (var ent in SolutionConfig.Current.Entities)
                ChangeBoolColumnCode(code, ent);
            return code.ToString();
        }

        private void ChangeBoolColumnCode(StringBuilder code, EntityConfig entity)
        {
            if (entity == null || entity.NoDataBase)
                return;
            var fields = entity.DbFields.Where(p => !p.IsCompute && p.CsType == "bool").ToArray();

            code.Append($@"
/*{entity.Caption}*/
ALTER TABLE `{entity.SaveTable}`");
            bool isFirst = true;
            foreach (PropertyConfig col in fields)
            {
                col.DbType = "BOOL";
                col.Initialization = "0";
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');
                code.Append($@"
    CHANGE COLUMN `{col.DbFieldName}` {FieldDefault(col)}");
            }
            code.Append(@";");
        }

        private string ChangeColumnName2(EntityConfig entity)
        {
            if (entity == null)
                return "";
            if (entity.NoDataBase)
                return $"/*{entity.Caption} : 普通类(NoDataBase=true)无法生成SQL*/";
            var code = new StringBuilder();
            if (entity.PrimaryColumn != null)
            {
                code.Append($@"
/*{entity.Caption}*/
ALTER TABLE `{entity.SaveTable}`
    CHANGE COLUMN `{entity.PrimaryColumn.OldName}` {FieldDefault(entity.PrimaryColumn)};");
            }
            return code.ToString();
        }
        private string ChangeColumnName(EntityConfig entity)
        {
            if (entity == null)
                return "";
            if (entity.NoDataBase)
                return $"/*{entity.Caption} : 普通类(NoDataBase=true)无法生成SQL*/";
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
ALTER TABLE `{entity.SaveTable}`");
            bool isFirst = true;
            foreach (PropertyConfig col in entity.DbFields.Where(p => !p.IsCompute))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');
                code.Append($@"
    CHANGE COLUMN `{col.OldName}` {FieldDefault(col)}");
            }
            code.Append(@";");
            return code.ToString();
        }

        private string ChangeColumnCode(EntityConfig entity)
        {
            if (entity == null)
                return "";
            if (entity.NoDataBase)
                return $"/*{entity.Caption} : 普通类(NoDataBase=true)无法生成SQL*/";
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
ALTER TABLE `{entity.SaveTable}`");
            bool isFirst = true;
            foreach (PropertyConfig col in entity.DbFields.Where(p => !p.IsCompute))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(',');
                code.Append($@"
    CHANGE COLUMN `{col.DbFieldName}` {FieldDefault(col)}");
            }
            code.Append(@";");
            return code.ToString();
        }
        private static string ChangeColumnCode2(EntityConfig entity)
        {
            if (entity == null || entity.NoDataBase || !entity.Interfaces.Contains("IHistory"))
                return "";
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
ALTER TABLE `{entity.SaveTable}`
    CHANGE COLUMN `last_reviser_id` `last_reviser_id` BIGINT NULL DEFAULT 0 COMMENT '最后修改者标识',
    CHANGE COLUMN `author_id` `author_id` BIGINT NOT NULL DEFAULT 0 COMMENT '制作人标识';");
            return code.ToString();
        }

        private static string FieldDefault(PropertyConfig col)
        {
            return $"`{col.DbFieldName}` {MySqlHelper.ColumnType(col)}{NullKeyWord(col)} {ColumnDefault(col)} COMMENT '{col.Caption}'";
        }

        private static string NullKeyWord(PropertyConfig col)
        {
            return col.CsType == "string" || col.DbNullable ? " NULL" : " NOT NULL";
        }

        private static string ColumnDefault(PropertyConfig col)
        {
            if (col.IsIdentity)
                return " AUTO_INCREMENT";
            if (col.IsPrimaryKey)
                return Empty;
            if (col.Initialization == null)
            {
                if (col.DbNullable)
                    return null;
                if (col.CsType != "string")
                    col.Initialization = "0";
            }
            return col.CsType == "string" ? $"DEFAULT '{col.Initialization}'" : $"DEFAULT {col.Initialization}";
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
        {{");
            int idx = 0;
            foreach (var field in fields)
            {
                FieldReadCode(entity, field, code, idx++);
            }
            code.Append(@"
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
    `{0}` AS `{1}`", field.DbFieldName, field.Name);
            }
            return sql.ToString();
        }

        /// <summary>
        /// 字段数据库读取代码
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="field">字段</param>
        /// <param name="code">代码</param>
        /// <param name="idx"></param>
        public static void FieldReadCode(EntityConfig entity, PropertyConfig field, StringBuilder code, int idx)
        {
            if (!IsNullOrWhiteSpace(field.CustomType))
            {
                code.Append($@"
            if (!reader.IsDBNull({idx}))
                entity._{field.Name.ToLWord()} = ({field.CustomType})reader.GetInt32({idx});");
                return;
            }
            var type = field.CsType.ToLower();
            var dbType = field.DbType.ToLower();
            switch (type)
            {
                case "byte[]":
                    code.Append(field.IsImage
                        ? $@"
            if (GlobalContext.Current.Feature != 1 && !reader.IsDBNull({idx}))
                entity._{field.Name.ToLWord()} = (byte[])reader[{idx}];"
                        : $@"
            if (!reader.IsDBNull({idx}))
                entity._{field.Name.ToLWord()} = (byte[])reader[{idx}];");
                    return;
                case "string":
                    switch (dbType)
                    {
                        case "varchar":
                        case "varstring":
                            code.Append($@"
            if (!reader.IsDBNull({idx}))
                entity._{field.Name.ToLWord()} = {ReaderName(field.DbType)}({idx});");
                            break;
                        default:
                            code.Append($@"
            if (!reader.IsDBNull({idx}))
                entity._{field.Name.ToLWord()} = {ReaderName(field.DbType)}({idx}).ToString();");
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
                    code.Append($"entity._{field.Name.ToLWord()} ={ReaderName(field.DbType)}({idx});");
                    return;
                case "datetime":
                    code.Append($"try{{entity._{field.Name.ToLWord()} = reader.GetMySqlDateTime({idx}).Value;}}catch{{}}");
                    return;
                case "bool":
                    code.Append(BoolReader(field,idx));
                    break;
                default:
                    code.Append(
                        $"entity._{field.Name.ToLWord()} = ({field.CustomType ?? field.CsType}){ReaderName(field.DbType)}({idx});");
                    break;
            }

        }


        /// <summary>
        ///     取得对应类型的DBReader的方法名称
        /// </summary>
        /// <param name="csharpType">C#的类型</param>
        /// <param name="readerName">读取器的名称</param>
        /// <returns>读取方法名</returns>
        static string BoolReader(PropertyConfig field,int idx)
        {
            switch (field.DbType.ToLower())
            {
                case "bit":
                case "bool":
                case "boolean":
                case "tinyint":
                case "byte":
                    return $"entity._{field.Name.ToLWord()} = reader.GetBoolean({idx});";
                case "short":
                case "int16":
                    return $"entity._{field.Name.ToLWord()} = (int)reader.GetInt16({idx}) == 1;";
                case "int":
                case "int32":
                    return $"entity._{field.Name.ToLWord()} = (int)reader.GetInt32({idx}) == 1;";
                case "bigint":
                case "long":
                case "int64":
                    return $"entity._{field.Name.ToLWord()} = (int)reader.GetInt64({idx}) == 1L;";
                case "nchar":
                case "varchar":
                case "nvarchar":
                case "string":
                case "text":
                    return @$"entity._{field.Name.ToLWord()} = (int)reader.GetString({idx}) == ""True"";";
                default:
                    return $"entity._{field.Name.ToLWord()} = (int)reader.GetInt32({idx}) == 1;";
            }
        }


        /// <summary>
        ///     取得对应类型的DBReader的方法名称
        /// </summary>
        /// <param name="csharpType">C#的类型</param>
        /// <param name="readerName">读取器的名称</param>
        /// <returns>读取方法名</returns>
        static string ReaderName(string type, string readerName = "reader")
        {
            switch (type.ToLower())
            {
                case "bit":
                case "bool":
                case "boolean":
                case "tinyint":
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
                    return $"/*({type})*/{readerName}.GetValue";
            }
        }

        #endregion
    }
}