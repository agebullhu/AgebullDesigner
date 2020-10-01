using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.SqlServer;
using Agebull.EntityModel.Designer;
using static System.String;

namespace Agebull.EntityModel.RobotCoder.DataBase.Sqlite
{
    /// <summary>
    /// SQL����Ƭ��
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class SqliteMomentCoder : MomentCoderBase, IAutoRegister
    {
        #region ע��

        /// <summary>
        /// ִ���Զ�ע��
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            MomentCoder.RegisteCoder("Sqlite", "���ɱ�(SQL)", "sql", p => !p.NoDataBase, CreateTable);
            MomentCoder.RegisteCoder("Sqlite", "������ͼ(SQL)", "sql", p => !p.NoDataBase, CreateView);
            MomentCoder.RegisteCoder("Sqlite", "ɾ����ͼ(SQL)", "sql", p => !p.NoDataBase, DropView);
            MomentCoder.RegisteCoder("Sqlite", "ɾ����(SQL)", "sql", p => !p.NoDataBase, DropTable);
            MomentCoder.RegisteCoder("Sqlite", "�����(SQL)", "sql", p => !p.NoDataBase, TruncateTable);
        }

        #endregion

        #region View

        public static string DropView(EntityConfig entity)
        {
            if (entity.NoDataBase || entity.SaveTable == entity.ReadTableName)
                return Empty;
            return $@"
-- {entity.Caption}
DROP VIEW [{entity.ReadTableName}];";
        }

        public static string CreateView(EntityConfig model)
        {
            DataBaseHelper.CheckFieldLink(model.Properties);
            var array = model.DbFields.Where(p => p.IsLinkField && !p.IsLinkKey).ToArray();
            if (array.Length == 0)
            {
                return $"-- {model.Caption}û���ֶ������������������ͼ";
            }
            var tables = model.DbFields.Where(p => p.IsLinkField && !p.IsLinkKey).Select(p => p.LinkTable).Distinct().Select(GlobalConfig.GetEntity).ToDictionary(p => p.Name);
            if (tables.Count == 0)
            {
                model.ReadTableName = model.SaveTable; ;
                return $"-- {model.Caption}û���ֶ������������������ͼ";
            }
            string viewName;
            if (IsNullOrWhiteSpace(model.ReadTableName) || model.ReadTableName == model.SaveTable)
            {
                viewName = DataBaseHelper.ToViewName(model);
            }
            else
            {
                viewName = model.ReadTableName;
            }
            var builder = new StringBuilder();
            builder.Append($@"
-- {model.Caption}
CREATE VIEW [{viewName}] AS 
    SELECT ");
            bool first = true;
            foreach (var field in model.DbFields)
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
                builder.AppendFormat(@"[{0}].[{1}] as [{1}]", model.SaveTable, field.DbFieldName);
            }
            builder.Append($@"
    FROM [{model.SaveTable}]");
            foreach (var table in tables.Values)
            {
                var field = model.DbFields.FirstOrDefault(p => p.IsLinkKey && (p.LinkTable == table.Name || p.LinkTable == table.SaveTable));
                if (field == null)
                    continue;
                var linkField = table.Properties.FirstOrDefault(
                    p => p.Name == field.LinkField || p.DbFieldName == field.LinkField);
                if (linkField == null)
                    continue;
                builder.AppendFormat(@"
    LEFT JOIN [{1}] [{4}] ON [{0}].[{2}] = [{4}].[{3}]"
                        , model.SaveTable, table.SaveTable, field.DbFieldName, linkField.DbFieldName, table.Name);
            }
            builder.Append(';');
            builder.AppendLine("GO");
            builder.AppendLine();
            return builder.ToString();
        }

        #endregion

        #region ��ṹ


        public static string CreateTable(EntityConfig entity)
        {
            if (entity.NoDataBase)
                return "";//�������Ϊ��ͨ�࣬�޷�����SQL
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
CREATE TABLE [{entity.SaveTable}](");
            bool isFirst = true;
            foreach (var col in entity.DbFields.Where(p => !p.IsCompute))
            {
                code.Append($@"
   {(isFirst ? "" : ",")}{FieldDefault(col)}");
                isFirst = false;
            }
            code.Append(@"
);");
            return code.ToString();
        }

        private static string FieldDefault(IFieldConfig col)
        {
            if (col.IsIdentity)
                return $"[{col.DbFieldName}] INTEGER PRIMARY KEY autoincrement -- '{col.Caption}'";
            var def = col.Initialization == null
                ? null
                : col.CsType == "string" ? $"DEFAULT '{col.Initialization}'" : $"DEFAULT {col.Initialization}";
            var nulldef = col.CsType == "string" || col.DbNullable ? " NULL" : " NOT NULL";
            return $"[{col.DbFieldName}] {SqlServerHelper.ColumnType(col)}{nulldef} {def} -- '{col.Caption}'";
        }

        public static string TruncateTable(EntityConfig entity)
        {
            if (entity.NoDataBase)
                return Empty;
            return $@"
-- {entity.Caption}
TRUNCATE TABLE [{entity.SaveTable}];
";
        }

        private static string DropTable(EntityConfig entity)
        {
            if (entity == null)
                return "";
            if (entity.NoDataBase)
                return $"-- {entity.Caption} : ����Ϊ��ͨ��(NoDataBase=true)���޷�����SQL";
            return $@"
--{entity.Caption}
DROP TABLE [{entity.SaveTable}];";
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