using Agebull.EntityModel.Config.V2021;
using System.Diagnostics;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// ʵ��������
    /// </summary>
    public class DataTableApplication : ConfigModelBase
    {
        /// <summary>
        /// ��ṹ����
        /// </summary>
        public DataTableConfig DataTable { get; set; }

        /// <summary>
        /// ��ṹ����
        /// </summary>
        public IEntityConfig Entity => DataTable.Entity;


        /// <summary>
        ///     �Զ��޸�(��ģ���޸����ݴ洢)
        /// </summary>
        public void StandardName(bool repair)
        {

            if (DataTable.IsFreeze || Entity.IsInterface)
                return;
            if (!Entity.EnableDataBase)
            {
                foreach (var field in DataTable.Fields)
                {
                    field.DbFieldName = null;
                    field.FieldType = null;
                }

                DataTable.IsModify = true;
                DataTable.ReadTableName = null;
                DataTable.SaveTableName = null;
                return;
            }
            if (DataTable.SaveTableName == DataTable.ReadTableName)
                DataTable.ReadTableName = null;

            if (string.IsNullOrWhiteSpace(DataTable.OldName))
                DataTable.OldName = DataTable.Alias ?? DataTable.SaveTableName;

            var name = DataBaseHelper.ToTableName(Entity);
            if (DataTable.SaveTableName == null || name != DataTable.SaveTableName)
            {
                DataTable.SaveTableName = name;
            }
            foreach (var field in DataTable.Fields)
            {
                if (field.IsDiscard)
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(field.OldName))
                    field.OldName = field.DbFieldName;
                field.DbFieldName = DataBaseHelper.ToDbFieldName(field.Property);
            }
        }


        /// <summary>
        ///     �Զ��޸�(��ģ���޸����ݴ洢)
        /// </summary>
        public void CheckDbConfig(bool repair)
        {
            if (DataTable.IsFreeze || Entity.IsInterface)
                return;
            if (!Entity.EnableDataBase)
            {
                foreach (var field in DataTable.Fields)
                {
                    field.DbFieldName = null;
                    field.FieldType = null;
                }

                DataTable.IsModify = true;
                DataTable.ReadTableName = null;
                DataTable.SaveTableName = null;
                return;
            }

            if (Entity.PrimaryColumn == null)
            {
                var idf = Entity.Entity.Find("Id");
                if (idf != null)
                    idf.IsPrimaryKey = true;
                else
                    Entity.Entity.Add(new FieldConfig
                    {
                        Name = "Id",
                        Caption = DataTable.Caption + "ID",
                        JsonName = "id",
                        DbFieldName = "id",
                        IsIdentity = true,
                        IsPrimaryKey = true,
                        DataType = SolutionConfig.Current.IdDataType
                    });
            }

            if (repair || string.IsNullOrWhiteSpace(DataTable.SaveTableName))
            {
                if (DataTable.ReadTableName == DataTable.SaveTableName)
                    DataTable.ReadTableName = null;
                DataTable.SaveTableName = DataBaseHelper.ToTableName(Entity);
            }
            var model = new PropertyDatabaseBusiness
            {
                Entity = DataTable.Entity
            };
            foreach (var field in DataTable.Fields)
            {
                if (field.IsDiscard)
                {
                    continue;
                }
                model.Field = field;
                model.CheckByDb(repair);
            }
            DataBaseHelper.CheckFieldLink(Entity.DataTable.Fields);
            CheckIndex();
        }

        public void CheckRelation()
        {
        }

        public void CheckIndex()
        {
            foreach (var field in DataTable.Fields)
            {
                if (field.NoStorage)
                    continue;
                if (field.IsLinkKey || field.Property.IsCaption || field.Property.IsEnum)
                {
                    field.IsDbIndex = true;
                    Trace.WriteLine($"--{field.Caption}:{field.DbFieldName}");
                }
            }
        }
    }
}