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
            if (property.IsPrimaryKey || property.IsRelationField)
                dataTypeName = SolutionConfig.Current.IdDataType;

            if (string.IsNullOrWhiteSpace(dataTypeName))
            {
                dataType = SolutionConfig.Current.DataTypeMap.FirstOrDefault(p =>
                               string.Equals(p.CSharp, property.CsType, StringComparison.OrdinalIgnoreCase)) ??
                           SolutionConfig.Current.DataTypeMap.First(p =>
                               string.Equals(p.Name, "string", StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                dataType = SolutionConfig.Current.DataTypeMap.FirstOrDefault(p =>
                               string.Equals(p.Name, dataTypeName, StringComparison.OrdinalIgnoreCase)) ??
                           SolutionConfig.Current.DataTypeMap.First(p =>
                               string.Equals(p.Name, "string", StringComparison.OrdinalIgnoreCase));
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
            switch (property.Parent.Parent.DbType)
            {
                case DataBaseType.SqlServer:
                    property.DbType = dataType.SqlServer;
                    break;
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
    }
}