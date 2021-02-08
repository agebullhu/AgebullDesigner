using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.SqlServer;
using Agebull.EntityModel.Config.V2021;
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
            CoderManager.RegisteCoder<DataTableConfig>("Sqlite", "���ɱ�(SQL)", "sql", CreateTable);
            CoderManager.RegisteCoder<DataTableConfig>("Sqlite", "������ͼ(SQL)", "sql", CreateView);
            CoderManager.RegisteCoder<DataTableConfig>("Sqlite", "ɾ����ͼ(SQL)", "sql", DropView);
            CoderManager.RegisteCoder<DataTableConfig>("Sqlite", "ɾ����(SQL)", "sql", DropTable);
            CoderManager.RegisteCoder<DataTableConfig>("Sqlite", "�����(SQL)", "sql", TruncateTable);
        }

        #endregion

        #region View

        public static string DropView(DataTableConfig entity)
        {
            return $@"
-- {entity.Caption}
DROP VIEW [{entity.ReadTableName}];";
        }

        public static string CreateView(DataTableConfig model)
        {
            DataBaseHelper.CheckFieldLink(model.Fields);
            var array = model.Fields.Where(p => p.IsLinkField && !p.IsLinkKey).ToArray();
            if (array.Length == 0)
            {
                return $"-- {model.Caption}û���ֶ������������������ͼ";
            }
            var tables = model.Fields.Where(p => p.IsLinkField && !p.IsLinkKey).Select(p => p.LinkTable).Distinct().Select(GlobalConfig.GetEntity).ToDictionary(p => p.Name);
            if (tables.Count == 0)
            {
                model.ReadTableName = model.SaveTableName; ;
                return $"-- {model.Caption}û���ֶ������������������ͼ";
            }
            string viewName;
            if (IsNullOrWhiteSpace(model.ReadTableName) || model.ReadTableName == model.SaveTableName)
            {
                viewName = DataBaseHelper.ToViewName(model.Entity);
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
            foreach (var field in model.Fields)
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
                            friend.Properties.FirstOrDefault(
                                p => p.DbFieldName == field.LinkField || p.Name == field.LinkField);
                        if (linkField != null)
                        {
                            builder.AppendFormat(@"[{0}].[{1}] as [{2}]", friend.Name, linkField.DbFieldName, field.DbFieldName);
                            continue;
                        }
                    }
                }
                builder.AppendFormat(@"[{0}].[{1}] as [{1}]", model.SaveTableName, field.DbFieldName);
            }
            builder.Append($@"
    FROM [{model.SaveTableName}]");
            foreach (var table in tables.Values)
            {
                var field = model.Fields.FirstOrDefault(p => p.IsLinkKey && (p.LinkTable == table.Name || p.LinkTable == table.SaveTableName));
                if (field == null)
                    continue;
                var linkField = table.Properties.FirstOrDefault(
                    p => p.Name == field.LinkField || p.DbFieldName == field.LinkField);
                if (linkField == null)
                    continue;
                builder.AppendFormat(@"
    LEFT JOIN [{1}] [{4}] ON [{0}].[{2}] = [{4}].[{3}]"
                        , model.SaveTableName, table.SaveTableName, field.DbFieldName, linkField.DbFieldName, table.Name);
            }
            builder.Append(';');
            builder.AppendLine("GO");
            builder.AppendLine();
            return builder.ToString();
        }

        #endregion

        #region ��ṹ


        public static string CreateTable(DataTableConfig entity)
        {
            var code = new StringBuilder();
            code.Append($@"
/*{entity.Caption}*/
CREATE TABLE [{entity.SaveTableName}](");
            bool isFirst = true;
            foreach (var col in entity.Fields.Where(p => !p.Property.IsCompute))
            {
                code.Append($@"
   {(isFirst ? "" : ",")}{FieldDefault(col)}");
                isFirst = false;
            }
            code.Append(@"
);");
            return code.ToString();
        }

        private static string FieldDefault(DataBaseFieldConfig col)
        {
            if (col.IsIdentity)
                return $"[{col.DbFieldName}] INTEGER PRIMARY KEY autoincrement -- '{col.Caption}'";
            var def = col.Property.Initialization == null
                ? null
                : col.Property.CsType == "string" ? $"DEFAULT '{col.Property.Initialization}'" : $"DEFAULT {col.Property.Initialization}";
            var nulldef = col.Property.CsType == "string" || col.DbNullable ? " NULL" : " NOT NULL";
            return $"[{col.DbFieldName}] {SqlServerHelper.ColumnType(col)}{nulldef} {def} -- '{col.Caption}'";
        }

        public static string TruncateTable(DataTableConfig entity)
        {
            return $@"
-- {entity.Caption}
TRUNCATE TABLE [{entity.SaveTableName}];
";
        }

        private static string DropTable(DataTableConfig entity)
        {
            return $@"
--{entity.Caption}
DROP TABLE [{entity.SaveTableName}];";
        }

        #endregion

        #region ���ݶ�ȡ

        /// <summary>
        /// �ֶ����ݿ��ȡ����
        /// </summary>
        /// <param name="field">�ֶ�</param>
        /// <param name="code">����</param>
        /// <param name="idx">���</param>
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