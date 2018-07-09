using System.Data;
using Agebull.EntityModel.Config;
using MySql.Data.MySqlClient;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    ///     代码生成器缺省支持
    /// </summary>
    public static class CodeBuilderDefault
    {
        /// <summary>
        /// 得到标准的C#类型
        /// </summary>
        /// <param name="csType"></param>
        /// <returns></returns>
        public static string GetStdCsType(string csType)
        {
            switch (csType.ToLower())
            {
                case "char":
                case "byte":
                case "double":
                case "decimal":
                case "string":
                    return csType.ToLower();
                case "boolean":
                    return "bool";
                case "int16":
                    return "short";
                case "int32":
                    return "int";
                case "int64":
                    return "long";
                case "uint16":
                    return "ushort";
                case "uint32":
                    return "uint";
                case "uint64":
                    return "uint";
                case "single":
                    return "float";
                default:
                    return csType;
            }
        }
        public static string PropertyValueType(PropertyConfig col)
        {
            switch (col.CsType.ToLower())
            {
                case "bit":
                case "bool":
                case "boolean":
                case "byte":
                case "char":
                case "short":
                case "int16":
                case "int":
                case "int32":
                case "bigint":
                case "long":
                case "int64":
                case "datetime":
                case "datetime2":
                case "decimal":
                case "numeric":
                case "real":
                case "double":
                case "float":
                case "guid":
                case "uniqueidentifier":
                    return col.Nullable ? "Nullable" : "Value";

                case "nchar":
                case "varchar":
                case "nvarchar":
                case "string":
                case "text":
                    return "String";
                default:
                    return "Object";
            }
        }

        /// <summary>
        ///     从C#的类型转为SQLite的类型
        /// </summary>
        /// <param name="csharpType"> C#的类型</param>
        public static string ToDataBaseType(string csharpType)
        {
            switch (csharpType.ToLower())
            {
                case "byte":
                case "sbyte":
                    return "tinyint";
                case "short":
                case "int16":
                case "ushort":
                case "uint16":
                    return "smallint";
                case "int":
                case "int32":
                case "uint":
                case "uint32":
                    return "int";
                case "long":
                case "int64":
                case "ulong":
                case "uint64":
                    return "BIGINT";
                case "decimal":
                    return "decimal";
                case "double":
                    return "double";
                case "float":
                    return "float";
                case "bool":
                case "boolean":
                    return "bit";
                case "byte[]":
                case "binary":
                    return "image";
                case "char":
                    return "NCHAR";
                case "string":
                    return "varchar";
                case "datetime":
                    return "DateTime2";
                case "guid":
                    return "uniqueidentifier";
                default:
                    return csharpType;
            }
        }
        /// <summary>
        ///     是否合理的C#类型
        /// </summary>
        /// <param name="type"> 类型</param>
        public static bool IsCsType(string type)
        {
            switch (type.ToLower())
            {
                case "char":
                case "byte":
                case "sbyte":
                case "byte[]":
                case "decimal":
                case "double":
                case "float":
                case "single":
                case "string":
                case "bool":
                case "boolean":
                case "short":
                case "int16":
                case "ushort":
                case "uint16":
                case "int":
                case "int32":
                case "uint":
                case "uint32":
                case "long":
                case "ulong":
                case "int64":
                case "uint64":
                case "datetime":
                case "Guid":
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        ///     是否合理的数据库类型
        /// </summary>
        /// <param name="type"> 类型</param>
        public static bool IsDataBaseType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return false;
            }
            switch (type.ToLower())
            {
                case "tinyint":
                case "smallint":
                case "int":
                case "bigint":
                case "decimal":
                case "double":
                case "money":
                case "real":
                case "float":
                case "bit":
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
                case "longtext":
                case "uniqueidentifier":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        ///     取得对应类型的DBReader的方法名称
        /// </summary>
        /// <param name="csharpType">C#的类型</param>
        /// <param name="readerName">读取器的名称</param>
        /// <returns>读取方法名</returns>
        public static string GetDBReaderFunctionName(string csharpType, string readerName = "reader")
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
                    return readerName + ".GetDateTime";

                case "decimal":
                case "numeric":
                    return readerName + ".GetDecimal";

                case "real":
                case "double":
                case "float":
                    return readerName + ".GetDouble";

                    //return readerName + ".GetFloat";

                case "guid":
                case "uniqueidentifier":
                    return readerName + ".GetGuid";

                case "nchar":
                case "varchar":
                case "nvarchar":
                case "string":
                case "text":
                case "longtext":
                    
                    return readerName + ".GetString";

                default:
                    return $"/*({csharpType})*/{readerName}.GetValue";
            }
        }


        /// <summary>
        ///     取得对应类型的DBReader的方法名称
        /// </summary>
        /// <param name="csharpType">C#的类型</param>
        /// <returns>读取方法名</returns>
        public static string GetDefault(string csharpType)
        {
            switch (csharpType.ToLower())
            {
                case "bit":
                case "bool":
                case "boolean":
                    return "false";

                case "int":
                case "Int32":
                case "short":
                case "int16":
                case "char":
                case "byte":
                    return "0";
                case "bigint":
                case "long":
                case "int64":
                    return "0L";
                case "datetime":
                case "datetime2":
                    return "DateTime.MinValue";

                case "decimal":
                case "numeric":
                    return "0M";
                case "real":
                case "double":
                    return "0D";

                case "float":
                    return "0F";

                case "guid":
                case "uniqueidentifier":
                    return "Guid.Empty ";
                case "string":
                case "byte[]":
                    return "null";
                default:
                    return $"default({csharpType})";
            }
        }

        /// <summary>
        ///     取得对应类型的二进制长度
        /// </summary>
        /// <param name="csharpType">C#的类型</param>
        /// <returns>读取方法名</returns>
        public static int ByteLen(string csharpType)
        {
            switch (csharpType.ToLower())
            {
                case "bool":
                case "byte":
                    return 1;
                case "short":
                case "ushort":
                    return 2;
                case "guid":
                case "decimal":
                    return 16;
                case "long":
                case "ulong":
                case "double":
                case "datetime":
                    return 8;
                case "int":
                case "uint":
                case "float":
                    return 4;
                case "char":
                    return 255;
                case "string":
                    return 254;
                default:
                    return 0;
            }
        }

        #region 读取类型

        /// <summary>
        ///     取得对应类型的二进制长度
        /// </summary>
        /// <param name="csharpType">C#的类型</param>
        /// <returns>读取方法名</returns>
        public static string GetByteLen(string csharpType)
        {
            switch (csharpType.ToLower())
            {
                case "bool":
                case "byte":
                    return "reader.BaseStream.Position += 1;";
                case "short":
                case "ushort":
                    return "reader.BaseStream.Position += 2;";
                case "guid":
                case "decimal":
                    return "reader.BaseStream.Position += 16;";
                case "long":
                case "ulong":
                case "double":
                    return "reader.BaseStream.Position += 8;";
                case "int":
                case "uint":
                case "float":
                    return "reader.BaseStream.Position += 4;";
                case "char":
                    return "reader.ReadChar();";
                case "string":
                    return "reader.ReadString();";
                default:
                    return @"throw new Exception(""binary value error!"")";
            }
        }

        #endregion

        /// <summary>
        ///  到标准的.Net名称
        /// </summary>
        /// <param name="csharpType">C#的类型</param>
        /// <returns>读取方法名</returns>
        public static string ToNetFrameType(string csharpType)
        {
            switch (csharpType.TrimEnd('?').ToLower())
            {
                case "char":
                    return "Char";
                case "byte":
                    return "Byte";
                case "decimal":
                    return "Decimal";
                case "double":
                    return "Double";
                case "float":
                case "Single":
                    return "Single";
                case "string":
                    return "String";
                case "bool":
                    return "Boolean";
                case "short":
                    return "Int16";
                case "ushort":
                    return "UInt16";
                case "int":
                    return "Int32";
                case "uint":
                    return "UInt32";
                case "long":
                    return "Int64";
                case "ulong":
                    return "UInt64";
                default:
                    return csharpType.TrimEnd('?');
            }
        }

        /// <summary>
        ///     从C#的类型转为DBType
        /// </summary>
        /// <param name="csharpType"> </param>
        public static MySqlDbType ToSqlDbType(string csharpType)
        {
            switch (csharpType)
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
                    return MySqlDbType.Bit;
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
        ///     从C#的类型转为DBType
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
    }
}

/*
 

        /// <summary>
        /// 取得对应类型的DBReader的方法名称
        /// </summary>
        /// <param name="csharpType">C#的类型</param>
        /// <param name="readerName">读取器的名称</param>
        /// <returns>读取方法名</returns>
        public static string GetDBReaderFunctionName(string csharpType,string readerName = "reader")
        {
            switch (csharpType)
            {
                case "bool":
                case "Boolean":
                    return readerName + ".GetBoolean";
                    
                case "byte":
                case "Byte":
                    return readerName + ".GetByte";
                    
                //case "byte[]":
                //case "Byte[]":
                //case "Binary":
                //    return readerName + ".GetBytes";
                //    
                case "char":
                case "Char":
                    return readerName + ".GetChar";
                    
                //case "char[]":
                //case "Char[]":
                //    return readerName + ".GetChars";
                //    
                case "DateTime":
                    return readerName + ".GetDateTime";
                    
                case "decimal":
                case "Decimal":
                    return readerName + ".GetDecimal";
                    
                case "double":
                case "Double":
                    return readerName + ".GetDouble";
                    
                case "float":
                case "Float":
                    return readerName + ".GetFloat";
                    
                case "Guid":
                    return readerName + ".GetGuid";
                    
                case "short":
                case "Int16":
                    return readerName + ".GetInt16";
                    
                case "int":
                case "Int32":
                    return readerName + ".GetInt32";
                    
                case "long":
                case "Int64":
                    return readerName + ".GetInt64";
                    
                case "string":
                case "String":
                    return readerName + ".GetString";
                    
                default:
                    return string.Format("({0}){1}.GetValue", csharpType,readerName);
                    
            }
        }
 */