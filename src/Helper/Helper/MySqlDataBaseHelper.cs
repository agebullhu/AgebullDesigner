using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Agebull.EntityModel.Config.Mysql
{
    /// <summary>
    ///     ʵ��ʵ�ֽӿڵ�����
    /// </summary>
    public class MySqlHelper
    {

        /// <summary>
        /// ��ҳ���������͵��б�
        /// </summary>
        public static List<ComboItem<string>> DataTypeList = new List<ComboItem<string>>
        {
            new ComboItem<string>
            {
                name = "����(BOOL)",
                value= "BOOL"
            },
            new ComboItem<string>
            {
                name = "8λ����(TINYINT)",
                value= "TINYINT"
            },
            new ComboItem<string>
            {
                name = "16λ����(SMALLINT)",
                value= "SMALLINT"
            },
            new ComboItem<string>
            {
                name = "32λ����(INT)",
                value= "INT"
            },
            new ComboItem<string>
            {
                name = "64λ����(BIGINT)",
                value= "BIGINT"
            },
            new ComboItem<string>
            {
                name = "ʵ��(DECIMAL)",
                value= "DECIMAL"
            },
            new ComboItem<string>
            {
                name = "����С��(FLOAT)",
                value= "FLOAT"
            },
            new ComboItem<string>
            {
                name = "˫��С��(DOUBLE)",
                value= "DOUBLE"
            },
            new ComboItem<string>
            {
                name = "����ʱ��(DATETIME)",
                value= "DATETIME"
            },
            new ComboItem<string>
            {
                name = "�����ı�(CHAR)",
                value= "CHAR"
            },
            new ComboItem<string>
            {
                name = "�����ı�(NCHAR)",
                value= "NCHAR"
            },
            new ComboItem<string>
            {
                name = "�䳤�ı�(VARCHAR)",
                value= "VARCHAR"
            },
            new ComboItem<string>
            {
                name = "�䳤�ı�(NVARCHAR)",
                value= "NVARCHAR"
            },
            new ComboItem<string>
            {
                name = "���ı�(TEXT)",
                value= "TEXT"
            },
            new ComboItem<string>
            {
                name = "�����ı�(LONGTEXT)",
                value= "LONGTEXT"
            },
            new ComboItem<string>
            {
                name = "������(BLOB)",
                value= "BLOB"
            },
            new ComboItem<string>
            {
                name = "��������(LONGBLOB)",
                value= "LONGBLOB"
            }
        };

        /// <summary>
        ///     ��C#������תΪDBType
        /// </summary>
        /// <param name="field"> </param>
        public static MySqlDbType ToSqlDbType(PropertyConfig field)
        {
            if (field.DbType != null)
                switch (field.DbType.ToLower())
                {
                    case "boolean":
                    case "bool":
                    case "byte":
                    case "sbyte":
                    case "tinyint":
                        return MySqlDbType.Byte;
                    case "short":
                    case "int16":
                    case "smallint":
                        return MySqlDbType.Int16;
                    case "ushort":
                    case "uint16":
                        return MySqlDbType.UInt16;
                    case "uint":
                    case "uint32":
                        return MySqlDbType.UInt32;
                    case "int":
                        return MySqlDbType.Int32;
                    case "bigint":
                    case "long":
                    case "int64":
                        return MySqlDbType.Int64;
                    case "ulong":
                    case "uint64":
                        return MySqlDbType.UInt64;
                    case "money":
                    case "decimal":
                        return MySqlDbType.Decimal;
                    case "double":
                        return MySqlDbType.Double;
                    case "real":
                    case "float":
                        return MySqlDbType.Float;
                    case "char":
                    case "nchar":
                        return MySqlDbType.String;
                    case "varstring":
                    case "varchar":
                    case "nvarchar":
                        return MySqlDbType.VarString;
                    case "text":
                    case "ntext":
                        return MySqlDbType.Text;
                    case "longtext":
                        return MySqlDbType.LongText;
                    case "datetime":
                    case "datetime2":
                    case "smalldatetime":
                        return MySqlDbType.DateTime;
                    case "uuid":
                    case "guid":
                    case "uniqueidentifier":
                        return MySqlDbType.Guid;
                    case "binary":
                    case "image":
                    case "blob":
                        return MySqlDbType.Blob;
                    case "longblob":
                    case "byte[]":
                        return MySqlDbType.LongBlob;
                    default:
                        return MySqlDbType.VarString;
                }
            switch (field.CsType)
            {
                case "long":
                case "Int64":
                case "ulong":
                case "UInt64":
                    return MySqlDbType.Int64;
                case "float":
                case "Float":
                case "double":
                case "Double":
                    return MySqlDbType.Float;
                case "decimal":
                case "Decimal":
                    return MySqlDbType.Decimal;
                case "Boolean":
                case "bool":
                case "byte":
                case "Byte":
                case "sbyte":
                case "SByte":
                    return MySqlDbType.Byte;
                case "short":
                case "Int16":
                case "ushort":
                case "UInt16":
                    return MySqlDbType.Int16;
                //case "Guid":
                //    return MySqlDbType.UniqueIdentifier;
                //case "DateTime":
                //    return MySqlDbType.DateTime2;
                case "int":
                case "Int32":
                case "uint":
                case "IntPtr":
                case "UInt32":
                case "UIntPtr":
                    return MySqlDbType.Int32;
                case "Binary":
                case "byte[]":
                case "Byte[]":
                    return MySqlDbType.Binary;
                default:
                    return MySqlDbType.VarString;
            }
        }



        /// <summary>
        ///     ��C#������תΪDBType
        /// </summary>
        /// <param name="csharpType"> </param>
        public static DbType ToDbType(string csharpType)
        {
            switch (csharpType)
            {
                case "Boolean":
                case "bool":
                    return DbType.Boolean;
                case "byte":
                case "Byte":
                    return DbType.Byte;
                case "sbyte":
                case "SByte":
                    return DbType.SByte;
                case "short":
                case "Int16":
                    return DbType.Int16;
                case "ushort":
                case "UInt16":
                    return DbType.UInt16;
                case "long":
                case "Int64":
                    return DbType.Int64;
                case "ulong":
                case "UInt64":
                    return DbType.UInt64;
                case "float":
                case "Float":
                    return DbType.Single;
                case "double":
                case "Double":
                    return DbType.Double;
                case "decimal":
                case "Decimal":
                    return DbType.Decimal;
                //case "Guid":
                //    return DbType.Guid;
                //case "DateTime":
                //    return DbType.DateTime2;
                case "int":
                case "Int32":
                case "IntPtr":
                    return DbType.Int32;
                case "uint":
                case "UInt32":
                case "UIntPtr":
                    return DbType.Int32;
                case "Binary":
                case "byte[]":
                case "Byte[]":
                    return DbType.Binary;
                default:
                    return DbType.String;
            }
        }

        /// <summary>
        ///     ��C#������תΪSQLite������
        /// </summary>
        /// <param name="property">�ֶ�</param>
        public static string ToDataBaseType(PropertyConfig property)
        {
            switch (property.CsType.ToLower())
            {
                case "byte":
                case "sbyte":
                    return "TINYINT";
                case "short":
                case "int16":
                case "ushort":
                case "uint16":
                    return "SMALLINT";
                case "int":
                case "int32":
                case "uint":
                case "uint32":
                    return "INT";
                case "long":
                case "int64":
                case "ulong":
                case "uint64":
                    return "BIGINT";
                case "decimal":
                    return "DECIMAL";
                case "double":
                case "float":
                    return "FLOAT";
                case "bool":
                case "boolean":
                    return "BOOL";
                case "byte[]":
                case "binary":
                    return property.IsBlob ? "LONGBLOB" : "BLOB";
                case "char":
                    return "NCHAR";
                case "string":
                    return property.IsBlob ? "LONGTEXT" : property.IsMemo ? "TEXT" : "NVARCHAR";
                case "datetime":
                    return "DATETIME";
                case "guid":
                    return "VARCHAR";
                default:
                    return property.CsType.ToUpper();
            }
        }

        /// <summary>
        ///     ��C#������תΪSQLite������
        /// </summary>
        /// <param name="csharpType"> C#������</param>
        public static string ToDataBaseType(string csharpType)
        {
            switch (csharpType.ToLower())
            {
                case "byte":
                case "sbyte":
                    return "TINYINT";
                case "short":
                case "int16":
                case "ushort":
                case "uint16":
                    return "SMALLINT";
                case "int":
                case "int32":
                case "uint":
                case "uint32":
                    return "INT";
                case "long":
                case "int64":
                case "ulong":
                case "uint64":
                    return "BIGINT";
                case "decimal":
                    return "DECIMAL";
                case "double":
                case "float":
                    return "FLOAT";
                case "bool":
                case "boolean":
                    return "BOOL";
                case "byte[]":
                case "binary":
                    return "IMAGE";
                case "char":
                    return "NCHAR";
                case "string":
                    return "NVARCHAR";
                case "datetime":
                    return "DATETIME";
                case "guid":
                    return "VARCHAR";
                default:
                    return csharpType.ToUpper();
            }
        }
        /// <summary>
        ///     �Ƿ��������ݿ�����
        /// </summary>
        /// <param name="type"> ����</param>
        public static bool IsDataBaseType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return false;
            }
            switch (type.ToLower())
            {
                case "tinyint":
                case "longtext":
                case "smallint":
                case "int":
                case "bigint":
                case "decimal":
                case "double":
                case "money":
                case "real":
                case "float":
                case "bool":
                case "binary":
                case "image":
                case "char":
                case "nchar":
                case "varchar":
                case "varstring":
                case "nvarchar":
                case "text":
                case "ntext":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                case "guid":
                case "uniqueidentifier":
                    return true;
                default:
                    return false;
            }
        }


        /// <summary>
        ///     ��C#������תΪMy sql������
        /// </summary>
        /// <param name="column"> C#������</param>
        public static string ColumnType(PropertyConfig column)
        {
            switch (column.DbType.ToLower())
            {
                case "decimal":
                case "numeric":
                    if (column.Datalen <= 10 || column.Datalen > 18)
                        column.Datalen = 18;
                    if (column.Scale <= 0 || column.Scale > 18)
                        column.Scale = 8;
                    return $"DECIMAL({column.Datalen},{column.Scale})";
                case "binary":
                case "varbinary":
                    if (column.IsBlob)
                        return "LONGBLOB";
                    if (column.Datalen <= 0 || column.Datalen >= 4000)
                        return "BLOB";
                    else
                        return $"{column.DbType.ToUpper()}({column.Datalen})";
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                    if (column.IsBlob)
                        return "LONGTEXT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci";
                    if (column.Datalen < 0 || column.Datalen > 1000)
                        return "TEXT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci";
                    return $"{column.DbType.ToUpper()}({column.Datalen}) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci";
                default:
                    return column.DbType.ToUpper();
            }
        }
    }
}