using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;

using Agebull.Common.Reflection;

using Gboxt.Common.SimpleDataAccess.SQLite;

namespace Agebull.Common.Access.ADO.Sqlite
{
    /// <summary>
    /// 节点编译器
    /// </summary>
    public static class SQLiteDefault
    {
        /// <summary>
        ///   从C#的类型转为SQLite的类型
        /// </summary>
        /// <param name="csharpType"> C#的类型</param>
        public static string ToDataBaseType(string csharpType)
        {
            switch (csharpType)
            {
                case "long":
                case "Int64":
                case "ulong":
                case "UInt64":
                    return "NUMERIC";
                case "float":
                case "Float":
                case "double":
                case "Double":
                case "decimal":
                case "Decimal":
                    return "REAL";
                case "Boolean":
                case "bool":
                case "byte":
                case "Byte":
                case "sbyte":
                case "SByte":
                case "short":
                case "Int16":
                case "ushort":
                case "UInt16":
                case "int":
                case "Int32":
                case "uint":
                case "IntPtr":
                case "UInt32":
                case "UIntPtr":
                    return "INTEGER";
                case "Binary":
                case "byte[]":
                case "Byte[]":
                    return "BLOB";
                default:
                    return "TEXT";
            }
        }

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



        /// <summary>
        /// 取得对应类型的DBReader的方法名称
        /// </summary>
        /// <param name="csharpType">C#的类型</param>
        /// <returns>读取方法名</returns>
        public static string ToCSharpType(string csharpType)
        {
            switch (csharpType)
            {
                case "Char":
                case "Byte":
                case "Byte[]":
                case "Decimal":
                case "Double":
                case "Float":
                    return csharpType.ToLower();
                case "Boolean":
                    return "bool";
                case "Int16":
                    return "short";
                case "UInt16":
                    return "ushort";
                case "Int32":
                    return "int";
                case "UInt32":
                    return "uint";
                case "Int64":
                    return "long";
                case "UInt64":
                    return "ulong";
                case "String":
                    return "string";
                default:
                    return csharpType;
            }
        }
        /// <summary>
        /// 生成SQLite参数
        /// </summary>
        /// <param name="csharpType">C#的类型</param>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns>参数</returns>
        public static DbParameter CreateParameter<T>(string csharpType, string parameterName,object value)
        {
            return new SQLiteParameter(parameterName, ToDbType(csharpType))
            {
                Value = value
            };

        }

        /// <summary>
        /// 生成SQLite参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns>参数</returns>
        public static DbParameter CreateParameter<T>(string parameterName,T value)
        {
            return new SQLiteParameter(parameterName, ToDbType(ReflectionHelper.GetTypeName(typeof(T))))
            {
                Value = value
            };

        }
        /// <summary>
        ///   从C#的类型转为DBType
        /// </summary>
        /// <param name="csharpType"> </param>
        public static SqlDbType ToSqlDbType(string csharpType)
        {
            switch (csharpType)
            {
                case "long":
                case "Int64":
                case "ulong":
                case "UInt64":
                    return SqlDbType.BigInt;
                case "float":
                case "Float":
                case "double":
                case "Double":
                    return SqlDbType.Float;
                case "decimal":
                case "Decimal":
                    return SqlDbType.Decimal;
                case "Boolean":
                case "bool":
                    return SqlDbType.Bit;
                case "byte":
                case "Byte":
                case "sbyte":
                case "SByte":
                case "short":
                case "Int16":
                case "ushort":
                case "UInt16":
                    return SqlDbType.SmallInt;
                //case "Guid":
                //    return SqlDbType.UniqueIdentifier;
                //case "DateTime":
                //    return SqlDbType.DateTime2;
                case "int":
                case "Int32":
                case "uint":
                case "IntPtr":
                case "UInt32":
                case "UIntPtr":
                    return SqlDbType.Int;
                case "Binary":
                case "byte[]":
                case "Byte[]":
                    return SqlDbType.Binary;
                default:
                    return SqlDbType.NVarChar;
            }
        }


        /// <summary>
        ///   从C#的类型转为DBType
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
