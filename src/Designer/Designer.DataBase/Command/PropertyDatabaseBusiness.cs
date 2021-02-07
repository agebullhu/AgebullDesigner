
using Agebull.EntityModel.Config.V2021;

namespace Agebull.EntityModel.Config
{
    internal class PropertyDatabaseBusiness
    {
        public DataBaseFieldConfig Field { get; set; }

        public IFieldConfig Property => Field.Field;

        public IEntityConfig Entity { get; set; }

        internal void CheckByDb(bool repair = false)
        {
            if (!Entity.EnableDataBase)
            {
                Field.DbFieldName = null;
                Field.DbType = null;
                return;
            }
            if (repair)
                Field.DbFieldName = DataBaseHelper.ToDbFieldName(Field.Field);
            if (repair || string.IsNullOrWhiteSpace(Field.DbType))
            {
                RepairDbType();
            }
            if (Property.IsPrimaryKey)
            {
                Property.Nullable = false;
                Field.DbNullable = false;
                Property.CanEmpty = false;
            }
            if (Field.DbType != null)
                Field.DbType = Field.DbType.ToUpper();

            if (Property.Initialization == "getdate")
                Property.Initialization = "now()";

        }

        internal void RepairDbType()
        {
            RobotCoder.DataTypeHelper.ToStandard(Property);
            if (Field.DbType == null)
                return;
            
            switch (Field.DbType = Field.DbType.ToUpper())
            {
                case "EMPTY":
                    Field.NoStorage = true;
                    break;
                case "BINARY":
                case "VARBINARY":
                    if (Field.IsBlob)
                    {
                        Field.DbType = "LONGBLOB";
                    }
                    else if (Field.Datalen >= 500)
                    {
                        Field.Datalen = 0;
                        Field.DbType = "BLOB";
                    }
                    else if (Field.Datalen <= 0)
                    {
                        Field.Datalen = 200;
                    }
                    return;
                case "CHAR":
                case "VARCHAR":
                case "NVARCHAR":
                    if (Field.IsBlob)
                    {
                        Field.DbType = "LONGTEXT";
                    }
                    else if (Field.IsMemo)
                    {
                        Field.Datalen = 0;
                        Field.DbType = "TEXT";
                    }
                    else if (Field.Datalen >= 500)
                    {
                        Field.Datalen = 0;
                        Field.DbType = "TEXT";
                    }
                    else if (Field.Datalen <= 0)
                    {
                        Field.Datalen = 200;
                    }
                    return;
            }
        }
    }
}