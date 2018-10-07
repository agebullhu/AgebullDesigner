using System;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     实体实现接口的命令
    /// </summary>
    public static class CsharpHelper
    {
        public static void CheckType(PropertyConfig column, string type)
        {
            if (type.Contains('?'))
                column.Nullable = true;

            column.Datalen = 0;
            column.IsDictionary = column.IsArray = false;
            column.IsIdentity = false;
            column.DbNullable = true;
            column.CanEmpty = true;
            column.CsType = null;
            column.ReferenceType = null;

            bool first = true;
            StringBuilder code = new StringBuilder();
            char prePunctuation = '\0';
            int strLen = 0;
            for (int i = 0; i < type.Length; i++)
            {
                if (char.IsPunctuation(type[i]))
                {
                    if (i == 0)
                    {
                        CheckPropertyType(column, "string");
                        return;
                    }
                    if (first)
                    {
                        CheckPropertyType(column, code.ToString());
                        code.Clear();
                        strLen = 0;
                        first = column.CsType == null;
                    }
                    else if (prePunctuation == '[' && type[i] == ']')
                    {
                        if (strLen > 0 && int.TryParse(code.ToString().Trim(), out var len))
                            column.ArrayLen = len.ToString();
                        column.IsArray = true;
                    }
                    else if (strLen > 0 && int.TryParse(code.ToString().Trim(), out var len))
                    {
                        if (column.Datalen <= 0)
                            column.Datalen = len;
                        else
                            column.Scale = len;
                    }

                    prePunctuation = type[i];
                    switch (type[i])
                    {
                        case '@':
                            column.IsIdentity = true;
                            break;
                        case '#':
                            column.DbNullable = false;
                            break;
                        case '!':
                            column.CanEmpty = false;
                            break;
                    }
                    continue;
                }
                code.Append(type[i]);
                strLen++;
            }
            if (first)
                CheckPropertyType(column, code.ToString());
        }

        private static void CheckPropertyType(PropertyConfig column, string type)
        {
            switch (type.ToLower())
            {
                case "t":
                case "s":
                case "string":
                case "nvarchar":
                    column.CsType = "string";
                    column.DataType = "String";
                    column.Datalen = 200;
                    break;
                case "ls":
                    column.CsType = "string";
                    column.DataType = "String";
                    column.IsBlob = true;
                    break;
                case "b":
                case "bit":
                case "bool":
                case "boolean":
                    column.CsType = "bool";
                    column.DataType = "Boolean";
                    break;
                case "i":
                case "int":
                case "int32":
                    column.CsType = "int";
                    column.DataType = "Int32";
                    break;
                case "l":
                case "long":
                case "bigint":
                case "int64":
                    column.CsType = "long";
                    column.DataType = "Int64";
                    break;
                case "d":
                case "decimal":
                case "money":
                case "numeric":
                    column.CsType = "decimal";
                    column.DataType = "Decimal";
                    break;
                case "f":
                case "float":
                case "double":
                    column.CsType = "double";
                    column.DataType = "Double";
                    break;
                case "p":
                    column.IsPrimaryKey = true;
                    column.CsType = "long";
                    column.DataType = "Int64";
                    break;
                case "datetime":
                case "time":
                case "date":
                    column.CsType = "DateTime";
                    column.DataType = "DateTime";
                    break;
                case "list":
                case "ilist":
                case "collection":
                case "icollection":
                case "ienumerable":
                case "arraylist":
                case "stack":
                case "queue":
                case "sortedlist":
                case "observablecollection":
                    column.ReferenceType = type;
                    column.IsArray = true;
                    break;
                case "dictionary":
                    column.ReferenceType = type;
                    column.IsDictionary = true;
                    break;
                default:
                    column.CsType = "object";
                    column.DataType = "Object";
                    column.CustomType = type;
                    break;
            }
        }


        /// <summary>
        /// 得到标准的C#类型
        /// </summary>
        /// <param name="csType"></param>
        /// <returns></returns>
        public static string GetStdCsType(string csType)
        {
            switch (csType.ToLower())
            {
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
                case "long long":
                    return "long";
                case "uint16":
                    return "ushort";
                case "uint32":
                case "unsigned int":
                case "unsigned long":
                    return "uint";
                case "uint64":
                    return "uint";
                case "single":
                    return "float";
                case "byte":
                case "unsigned char":
                    return "byte";
                case "unsigned long long":
                    return "ulong";
                case "unsigned short":
                    return "ushort";
                case "char":
                    return "string";
                default:
                    return csType;
            }
        }
        public static string PropertyValueType(PropertyConfig col)
        {
            switch (col.CsType.ToLower())
            {
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
                case "longtext":
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
                case "guid":
                    return true;
            }
            return GlobalConfig.GetEntity(type) != null;
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
    }
}