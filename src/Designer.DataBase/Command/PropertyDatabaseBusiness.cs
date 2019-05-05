
namespace Agebull.EntityModel.Config
{
    internal class PropertyDatabaseBusiness
    {
        public PropertyConfig Property { get; set; }

        internal void CheckByDb(bool repair = false)
        {
            if (Property.Parent.NoDataBase)
            {
                Property.DbFieldName = null;
                Property.DbType = null;
                return;
            }
            if (repair)
                Property.DbFieldName = DataBaseHelper.ToDbFieldName(Property.Name);
            if (repair || string.IsNullOrWhiteSpace(Property.DbType))
            {
                RepairDbType();
            }
            if (Property.IsPrimaryKey)
            {
                Property.Nullable = false;
                Property.DbNullable = false;
                Property.CanEmpty = false;
            }
            if (Property.DbType != null)
                Property.DbType = Property.DbType.ToUpper();

            if (Property.Initialization == "getdate")
                Property.Initialization = "now()";

        }

        internal void RepairDbType()
        {
            RobotCoder.DataTypeHelper.ToStandard(Property);
            if (Property.DbType == null)
                return;
            Property.Datalen = 0;
            switch (Property.DbType = Property.DbType.ToUpper())
            {
                case "EMPTY":
                    Property.NoStorage = true;
                    break;
                case "BINARY":
                case "VARBINARY":
                    if (Property.IsBlob)
                    {
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
    }
}