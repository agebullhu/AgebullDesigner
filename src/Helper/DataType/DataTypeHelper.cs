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

        public static void ToStandard(EntityConfig entity)
        {
            if (entity.NoStandardDataType)
                return;
            foreach (var pro in entity.Properties)
            {
                ToStandard(pro);
            }
        }
        public static void ToStandard(PropertyConfig property)
        {
            DataTypeMapConfig dataType;
            string dataTypeName = property.DataType;
            if (property.IsUserId)
                dataTypeName = SolutionConfig.Current.UserIdDataType;
            if (property.IsPrimaryKey || property.IsLinkField)
                dataTypeName = SolutionConfig.Current.IdDataType;

            if (String.IsNullOrWhiteSpace(dataTypeName))
            {
                dataType = SolutionConfig.Current.DataTypeMap.FirstOrDefault(p =>
                               String.Equals(p.CSharp, property.CsType, StringComparison.OrdinalIgnoreCase)) ??
                           SolutionConfig.Current.DataTypeMap.First(p =>
                               String.Equals(p.Name, "string", StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                dataType = SolutionConfig.Current.DataTypeMap.FirstOrDefault(p =>
                               String.Equals(p.Name, dataTypeName, StringComparison.OrdinalIgnoreCase)) ??
                           SolutionConfig.Current.DataTypeMap.First(p =>
                               String.Equals(p.Name, "string", StringComparison.OrdinalIgnoreCase));
            }

            property.DataType = dataType.Name;
            switch (dataType.Name.ToLower())
            {
                case "object":
                    ToValueDataType(property, dataType);
                    break;
                case "enum":
                    ToValueDataType(property, dataType);
                    break;
                default:
                    property.CsType = dataType.CSharp;
                    property.CppType = dataType.Cpp;
                    ToValueDataType(property, dataType);
                    break;
            }
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
            string name = arg.IsEnum ? "Enum" : arg.CsType;
            var dataType = GlobalConfig.CurrentSolution.DataTypeMap.FirstOrDefault(p => string.Equals(p.CSharp, name, StringComparison.OrdinalIgnoreCase));
            if (dataType != null)
            {
                arg.DataType = dataType.Name;
                arg.CsType = dataType.CSharp;
                arg.CppType = dataType.Cpp;
                if(arg.Datalen <= 0)
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
            else
            {
                GlobalConfig.CurrentSolution.DataTypeMap.Add(new DataTypeMapConfig
                {
                    Name = arg.DataType ?? arg.CsType,
                    CSharp = arg.CsType,
                    Cpp = arg.CppType,
                    Datalen = arg.Datalen,
                    Scale = arg.Scale,
                    MySql = arg.DbType
                });
            }
        }
        public static void StandardDataType(PropertyConfig arg)
        {
            string name = arg.IsEnum ? "Enum" : arg.DataType;
            var dataType = GlobalConfig.CurrentSolution.DataTypeMap.FirstOrDefault(p =>
                String.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
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