using System;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 数据类型辅助类
    /// </summary>
    public class DataTypeHelper
    {
        static DataTypeMapConfig StringConfig => SolutionConfig.Current.DataTypeMap.First(p =>
            !p.NoDbCheck && string.Equals(p.Name, "string", StringComparison.OrdinalIgnoreCase));

        public static DataTypeMapConfig FindByCSharp(string cs) => SolutionConfig.Current.DataTypeMap.FirstOrDefault(p =>
                                                                       !p.NoDbCheck && string.Equals(p.CSharp, cs, StringComparison.OrdinalIgnoreCase)) ?? StringConfig;

        public static DataTypeMapConfig FindByName(string type) => SolutionConfig.Current.DataTypeMap.FirstOrDefault(p =>
                                                                       !p.NoDbCheck && string.Equals(p.Name, type, StringComparison.OrdinalIgnoreCase)) ?? StringConfig;

        public static DataTypeMapConfig FindByDb(DataBaseType db, string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return null;
            type = type.Trim().Split(new char[] { '(', '[', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }, StringSplitOptions.RemoveEmptyEntries)[0];
            switch (db)
            {
                case DataBaseType.SqlServer:
                    if (type.Equals("varchar", StringComparison.OrdinalIgnoreCase))
                        type = "nvarchar";
                    return SolutionConfig.Current.DataTypeMap.FirstOrDefault(p =>
                       !p.NoDbCheck && string.Equals(p.SqlServer, type, StringComparison.OrdinalIgnoreCase));

                case DataBaseType.MySql:
                    return SolutionConfig.Current.DataTypeMap.FirstOrDefault(p =>
                       !p.NoDbCheck && string.Equals(p.MySql, type, StringComparison.OrdinalIgnoreCase));

                case DataBaseType.Oracle:
                    return SolutionConfig.Current.DataTypeMap.FirstOrDefault(p =>
                       !p.NoDbCheck && string.Equals(p.Oracle, type, StringComparison.OrdinalIgnoreCase));

                case DataBaseType.Sqlite:
                    return SolutionConfig.Current.DataTypeMap.FirstOrDefault(p =>
                       !p.NoDbCheck && string.Equals(p.Sqlite, type, StringComparison.OrdinalIgnoreCase));
            }
            return null;
        }


        public static void ToStandard(EntityConfig entity)
        {
            using (WorkModelScope.CreateScope(WorkModel.Repair))
                foreach (var property in entity.Properties)
                {
                    if (property.CsType == "unit")
                        property.CsType = "long";
                    if (!string.IsNullOrWhiteSpace(property.CustomType))
                        property.DataType = "Enum";
                    if (string.Equals(property.DataType, "Enum", StringComparison.OrdinalIgnoreCase))
                        property.CsType = "int";

                    DataTypeMapConfig dataType;
                    if (property.IsPrimaryKey || property.IsLinkField)
                        dataType = FindByName(SolutionConfig.Current.IdDataType);
                    else if (property.IsUserId)
                        dataType = FindByName(SolutionConfig.Current.IdDataType);
                    else
                        dataType = FindByCSharp(property.CsType);

                    property.DataType = dataType.Name;
                    property.CsType = dataType.CSharp;
                    property.CppType = dataType.Cpp;
                }
        }

        public static void ToStandardByDbType(EntityConfig entity)
        {
            using (WorkModelScope.CreateScope(WorkModel.Repair))
                foreach (var property in entity.Properties)
                {
                    ToStandardByDbType(property);
                }
        }

        private static void ToStandardByDbType(PropertyConfig property)
        {
            ToStandardByDbType(property,property.DbType);
        }
        public static void ToStandardByDbType(PropertyConfig property,string dbType)
        {
            if (property.NoStorage || string.IsNullOrWhiteSpace(dbType))
                return;
            DataTypeMapConfig dataType = FindByDb(property.Parent.Parent.DbType, dbType);
            if (dataType == null)
                return;
            property.DataType = dataType.Name;
            property.CsType = dataType.CSharp;
            property.CppType = dataType.Cpp;
        }

        public static void ToStandard(PropertyConfig property)
        {
            var dataType = string.IsNullOrWhiteSpace(property.DataType) ? FindByCSharp(property.CsType) : FindByName(property.DataType);
            if (dataType.Name.ToLower() != "object")
            {
                property.DataType = dataType.Name;
                property.CsType = dataType.CSharp;
                property.CppType = dataType.Cpp;
            }
            ToValueDataType(property, dataType);
        }
        public static void ToValueDataType(PropertyConfig property, DataTypeMapConfig dataType)
        {
            if (property.Parent == null)
                return;
            switch (property.Parent.Parent.DbType)
            {
                case DataBaseType.SqlServer:
                    property.DbType = dataType.SqlServer;
                    break;
                default:
                case DataBaseType.MySql:
                    property.DbType = dataType.MySql;
                    break;
                case DataBaseType.Oracle:
                    property.DbType = dataType.Oracle;
                    break;
                case DataBaseType.Sqlite:
                    property.DbType = dataType.Sqlite;
                    break;
            }
        }

        public static void CsDataType(PropertyConfig arg)
        {
            DataTypeMapConfig dataType;
            if (arg.CustomType != null)
            {
                arg.IsEnum = true;
            }
            if (arg.IsEnum)
            {
                dataType = GlobalConfig.CurrentSolution.DataTypeMap.FirstOrDefault(p => p.Name == "Enum");
            }
            else if (arg.CsType == "int")
            {
                dataType = GlobalConfig.CurrentSolution.DataTypeMap.FirstOrDefault(p => p.Name == "Int32");
            }
            else
            {
                dataType = GlobalConfig.CurrentSolution.DataTypeMap.FirstOrDefault(p => string.Equals(p.CSharp, arg.CsType, StringComparison.OrdinalIgnoreCase));
            }

            if (dataType == null)
            {
                GlobalConfig.CurrentSolution.DataTypeMap.Add(dataType = new DataTypeMapConfig
                {
                    Name = arg.DataType ?? arg.CsType,
                    CSharp = arg.CsType,
                    Cpp = arg.CppType,
                    Datalen = arg.Datalen,
                    Scale = arg.Scale,
                    MySql = arg.DbType
                });
            }
            arg.DataType = dataType.Name;
            arg.CsType = dataType.CSharp;
            arg.CppType = dataType.Cpp;
            if (arg.Datalen <= 0)
                arg.Datalen = dataType.Datalen;
            arg.Scale = dataType.Scale;
            if (arg.Parent == null)
                arg.DbType = dataType.MySql;
            else
                switch (arg.Parent.Parent.DbType)
                {
                    case DataBaseType.SqlServer:
                        arg.DbType = dataType.SqlServer;
                        break;
                    default:
                        arg.DbType = dataType.MySql;
                        break;
                }

            if (arg.DbType.Contains('('))
            {
                var words = arg.DbType.Split(new[] { ',', '(', ')', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                arg.DbType = words[0];
                if (words.Length > 1 && int.TryParse(words[1], out var len))
                    arg.Datalen = len;

                if (words.Length > 2 && int.TryParse(words[2], out var scale))
                    arg.Scale = scale;
            }
        }
        public static void StandardDataType(PropertyConfig arg)
        {
            string name = arg.IsEnum ? "Enum" : arg.DataType;
            var dataType = GlobalConfig.CurrentSolution.DataTypeMap.FirstOrDefault(p =>
                string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
            if (dataType == null)
            {
                GlobalConfig.CurrentSolution.DataTypeMap.Add(new DataTypeMapConfig
                {
                    Name = arg.DataType,
                    CSharp = arg.CsType,
                    Cpp = arg.CppType,
                    Datalen = arg.Datalen,
                    Scale = arg.Scale,
                    MySql = arg.DbType
                });
            }
            else
            {
                arg.DataType = dataType.Name;
                arg.CsType = dataType.CSharp;
                arg.CppType = dataType.Cpp;
                arg.Datalen = dataType.Datalen;
                arg.Scale = dataType.Scale;
                if (arg.Parent == null)
                    arg.DbType = dataType.MySql;
                else
                    switch (arg.Parent.Parent.DbType)
                    {
                        case DataBaseType.SqlServer:
                            arg.DbType = dataType.SqlServer;
                            break;
                        default:
                            arg.DbType = dataType.MySql;
                            break;
                    }
            }
        }
    }
}