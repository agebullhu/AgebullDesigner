
using Agebull.EntityModel.Config.V2021;

namespace Agebull.EntityModel.Config
{
    internal class PropertyDatabaseBusiness
    {
        public DataBaseFieldConfig Field { get; set; }

        public IPropertyConfig Property => Field.Property;

        public IEntityConfig Entity { get; set; }

        internal void CheckByDb(bool repair = false)
        {
            if (!Entity.EnableDataBase)
            {
                Field.DbFieldName = null;
                Field.FieldType = null;
                return;
            }
            if (repair)
                Field.DbFieldName = DataBaseHelper.ToDbFieldName(Field.Property);
            if (repair || string.IsNullOrWhiteSpace(Field.FieldType))
            {
                RepairDbType();
            }
            if (Property.IsPrimaryKey)
            {
                Property.Nullable = false;
                Field.DbNullable = false;
                Property.CanEmpty = false;
            }
            if (Field.FieldType != null)
                Field.FieldType = Field.FieldType.ToUpper();

            if (Property.Initialization == "getdate")
                Property.Initialization = "now()";

        }

        internal void RepairDbType()
        {
            RobotCoder.DataTypeHelper.ToStandard(Property);
            if (Field.FieldType == null)
                return;
            
            switch (Field.FieldType = Field.FieldType.ToUpper())
            {
                case "EMPTY":
                    Field.NoStorage = true;
                    break;
                case "BINARY":
                case "VARBINARY":
                    if (Field.IsBlob)
                    {
                        Field.FieldType = "LONGBLOB";
                    }
                    else if (Field.Datalen >= 500)
                    {
                        Field.Datalen = 0;
                        Field.FieldType = "BLOB";
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
                        Field.FieldType = "LONGTEXT";
                    }
                    else if (Field.IsMemo)
                    {
                        Field.Datalen = 0;
                        Field.FieldType = "TEXT";
                    }
                    else if (Field.Datalen >= 500)
                    {
                        Field.Datalen = 0;
                        Field.FieldType = "TEXT";
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