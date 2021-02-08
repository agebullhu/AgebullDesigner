using System;
using System.Linq;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.V2021;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 数据类型辅助类
    /// </summary>
    public static class DataTypeHelper
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

                case DataBaseType.Sqlite:
                    return SolutionConfig.Current.DataTypeMap.FirstOrDefault(p =>
                       !p.NoDbCheck && string.Equals(p.Sqlite, type, StringComparison.OrdinalIgnoreCase));
            }
            return null;
        }


        public static void ToStandard(IEntityConfig entity)
        {
            using (WorkModelScope.CreateScope(WorkModel.Repair))
                foreach (var property in entity.Properties)
                {
                    var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);

                    if (property.CsType == "unit")
                        property.CsType = "long";
                    if (!string.IsNullOrWhiteSpace(property.CustomType))
                        property.DataType = "Enum";
                    if (string.Equals(property.DataType, "Enum", StringComparison.OrdinalIgnoreCase))
                        property.CsType = "int";

                    DataTypeMapConfig dataType;
                    if (property.IsPrimaryKey || (field != null && field.IsLinkField))
                        dataType = FindByName(SolutionConfig.Current.IdDataType);
                    else
                        dataType = FindByCSharp(property.CsType);

                    property.DataType = dataType.Name;
                    property.CsType = dataType.CSharp;
                    property.CppType = dataType.Cpp;
                }
        }

        public static void ToStandardByDbType(IEntityConfig entity)
        {
            using (WorkModelScope.CreateScope(WorkModel.Repair))
                foreach (var property in entity.DataTable.Fields)
                {
                    ToStandardByDbType(property);
                }
        }

        private static void ToStandardByDbType(DataBaseFieldConfig property)
        {
            ToStandardByDbType(property, property.FieldType);
        }
        public static void ToStandardByDbType(DataBaseFieldConfig field, string dbType)
        {
            if (field.NoStorage || string.IsNullOrWhiteSpace(dbType))
                return;
            DataTypeMapConfig dataType = FindByDb(field.Entity.Project.DbType, dbType);
            if (dataType == null)
                return;
            field.Property.DataType = dataType.Name;
            field.Property.CsType = dataType.CSharp;
            field.Property.CppType = dataType.Cpp;
        }

        public static void ToStandard(IPropertyConfig property)
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
        public static void ToValueDataType(IPropertyConfig property, DataTypeMapConfig dataType)
        {
            if (property.Entity == null)
                return;
            var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
            if (field != null)
                field.FieldType = property.Entity.Project.DbType switch
                {
                    DataBaseType.SqlServer => dataType.SqlServer,
                    DataBaseType.Sqlite => dataType.Sqlite,
                    _ => dataType.MySql,
                };
        }

        public static void CsDataType(IPropertyConfig property)
        {
            DataTypeMapConfig dataType;
            if (property.CustomType != null)
            {
                property.IsEnum = true;
            }
            if (property.IsEnum)
            {
                dataType = GlobalConfig.CurrentSolution.DataTypeMap.FirstOrDefault(p => p.Name == "Enum");
            }
            else if (property.CsType == "int")
            {
                dataType = GlobalConfig.CurrentSolution.DataTypeMap.FirstOrDefault(p => p.Name == "Int32");
            }
            else
            {
                dataType = GlobalConfig.CurrentSolution.DataTypeMap.FirstOrDefault(p => string.Equals(p.CSharp, property.CsType, StringComparison.OrdinalIgnoreCase));
            }
            bool updataDataType = dataType == null;
            if (dataType == null)
            {
                GlobalConfig.CurrentSolution.DataTypeMap.Add(dataType = new DataTypeMapConfig
                {
                    Name = property.DataType ?? property.CsType,
                    CSharp = property.CsType,
                    Cpp = property.CppType,
                });
            }
            property.DataType = dataType.Name;
            property.CsType = dataType.CSharp;
            property.CppType = dataType.Cpp;

            var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
            if (field == null)
                return;
            if (updataDataType)
            {
                dataType.Datalen = field.Datalen;
                dataType.Scale = field.Scale;
                dataType.MySql = field.FieldType;
            }
            if (field.Datalen <= 0)
                field.Datalen = dataType.Datalen;
            field.Scale = dataType.Scale;
            if (property.Entity == null)
                field.FieldType = dataType.MySql;
            else
                field.FieldType = property.Entity.Project.DbType switch
                {
                    DataBaseType.SqlServer => dataType.SqlServer,
                    _ => dataType.MySql,
                };
            if (field.FieldType.Contains('('))
            {
                var words = field.FieldType.Split(new[] { ',', '(', ')', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                field.FieldType = words[0];
                if (words.Length > 1 && int.TryParse(words[1], out var len))
                    field.Datalen = len;

                if (words.Length > 2 && int.TryParse(words[2], out var scale))
                    field.Scale = scale;
            }
        }
        public static void StandardDataType(IPropertyConfig property)
        {
            string name = property.IsEnum ? "Enum" : property.DataType;
            var dataType = GlobalConfig.CurrentSolution.DataTypeMap.FirstOrDefault(p =>
                string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
            bool updataDataType = dataType == null;
            var field = property.Entity?.DataTable.Fields.FirstOrDefault(p => p.Property == property);
            
            if (updataDataType)
            {
                dataType.Datalen = field.Datalen;
                dataType.Scale = field.Scale;
                dataType.MySql = field.FieldType;
            }
            if (dataType == null)
            {
                GlobalConfig.CurrentSolution.DataTypeMap.Add(new DataTypeMapConfig
                {
                    Name = property.DataType,
                    CSharp = property.CsType,
                    Cpp = property.CppType,
                });
                if (field == null)
                    return;
            }
            else
            {
                property.DataType = dataType.Name;
                property.CsType = dataType.CSharp;
                property.CppType = dataType.Cpp;

                if (field == null)
                    return;
                field.Datalen = dataType.Datalen;
                field.Scale = dataType.Scale;
                if (property.Entity == null)
                    field.FieldType = dataType.MySql;
                else
                    field.FieldType = property.Entity.Project.DbType switch
                    {
                        DataBaseType.SqlServer => dataType.SqlServer,
                        DataBaseType.Sqlite => dataType.Sqlite,
                        _ => dataType.MySql,
                    };
            }
            if (updataDataType)
            {
                dataType.Datalen = field.Datalen;
                dataType.Scale = field.Scale;
                dataType.MySql = field.FieldType;
            }
        }
        public static bool IsType(this IPropertyConfig property, string type)
        {
            return property.CsType?.Equals(type, StringComparison.OrdinalIgnoreCase) ?? false;
        }
    }
}