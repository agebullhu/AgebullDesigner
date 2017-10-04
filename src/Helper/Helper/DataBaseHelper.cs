using System.Collections.Generic;
using System.Data;
using Agebull.EntityModel.Config.SqlServer;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     实体实现接口的命令
    /// </summary>
    public class DataBaseHelper
    {

        /// <summary>
        /// 主页面类型类型的列表
        /// </summary>
        public static List<ComboItem<string>> DataTypeList => SqlServerHelper.DataTypeList;


        /// <summary>
        ///     从C#的类型转为DBType
        /// </summary>
        /// <param name="csharpType"> </param>
        public static DbType ToDbType(string csharpType)
        {
            return SqlServerHelper.ToDbType(csharpType);
        }

        /// <summary>
        ///     从C#的类型转为SQLite的类型
        /// </summary>
        /// <param name="property">字段</param>
        public static string ToDataBaseType(PropertyConfig property)
        {
            return SqlServerHelper.ToDataBaseType(property);
        }

        /// <summary>
        ///     从C#的类型转为SQLite的类型
        /// </summary>
        /// <param name="csharpType"> C#的类型</param>
        public static string ToDataBaseType(string csharpType)
        {
            return SqlServerHelper.ToDataBaseType(csharpType);
        }

        /// <summary>
        ///     是否合理的数据库类型
        /// </summary>
        /// <param name="type"> 类型</param>
        public static bool IsDataBaseType(string type)
        {
            return SqlServerHelper.IsDataBaseType(type);
        }


        /// <summary>
        ///     从C#的类型转为My sql的类型
        /// </summary>
        /// <param name="column"> C#的类型</param>
        public static string ColumnType(PropertyConfig column)
        {
            return SqlServerHelper.ColumnType(column);
        }
    }
}

namespace Agebull.EntityModel.Config.Mysql
{
}
namespace Agebull.EntityModel.Config.SqlServer
{
}