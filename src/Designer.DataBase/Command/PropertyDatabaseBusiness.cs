using Agebull.EntityModel.Config.Mysql;
using Agebull.EntityModel.Config.SqlServer;
using System.Linq;

namespace Agebull.EntityModel.Config
{
    internal class PropertyDatabaseBusiness
    {
        public DataBaseType DataBaseType { get; set; }
        public PropertyConfig Property { get; set; }

        internal void CheckByDb(bool repair = false)
        {
            if (Property.Parent.NoDataBase)
            {
                Property.ColumnName = null;
                Property.DbType = null;
            }
            else
            {
                if (repair || string.IsNullOrWhiteSpace(Property.ColumnName))
                    Property.ColumnName = GlobalConfig.ToName(GlobalConfig.SplitWords(Property.Name).Select(p => p.ToLower()).ToList());
                if (repair || string.IsNullOrWhiteSpace(Property.DbType))
                    Property.DbType = DataBaseType == DataBaseType.SqlServer
                        ? SqlServerHelper.ToDataBaseType(Property)
                        : MySqlHelper.ToDataBaseType(Property);
                if (Property.DbType != null)
                {
                    switch (Property.DbType = Property.DbType.ToUpper())
                    {
                        case "EMPTY":
                            Property.NoStorage = true;
                            break;
                        case "BINARY":
                        case "VARBINARY":
                            if (Property.IsBlob)
                            {
                                Property.Datalen = 0;
                                Property.DbType = "LONGBLOB";
                            }
                            else if (Property.Datalen >= 500)
                            {
                                Property.Datalen = 0;
                                Property.DbType = "BLOB";
                            }
                            else if (Property.Datalen <= 0)
                            {
                                Property.Datalen = 200;
                            }
                            break;
                        case "CHAR":
                        case "VARCHAR":
                        case "NVARCHAR":
                            if (Property.IsBlob)
                            {
                                Property.Datalen = 0;
                                Property.DbType = "LONGTEXT";
                            }
                            else if (Property.IsMemo)
                            {
                                Property.Datalen = 0;
                                Property.DbType = "TEXT";
                            }
                            else if (Property.Datalen >= 500)
                            {
                                Property.Datalen = 0;
                                Property.DbType = "TEXT";
                            }
                            else if (Property.Datalen <= 0)
                            {
                                Property.Datalen = 200;
                            }
                            break;
                    }
                }
                if (Property.IsPrimaryKey || Property.IsCaption)
                {
                    Property.Nullable = false;
                    Property.DbNullable = false;
                }
                if (Property.IsPrimaryKey || Property.IsCaption)
                {
                    Property.CanEmpty = false;
                }

                //else if (repair)
                //{
                //    Property.DbNullable = true;
                //}
            }
        }

    }
}