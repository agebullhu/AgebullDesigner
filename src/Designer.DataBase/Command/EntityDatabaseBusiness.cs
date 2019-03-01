using System.Linq;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// ʵ��������
    /// </summary>
    public class EntityDatabaseBusiness : ConfigModelBase
    {
        /// <summary>
        /// ��ṹ����
        /// </summary>
        public EntityConfig Entity { get; set; }


        /// <summary>
        ///     �Զ��޸�(��ģ���޸����ݴ洢)
        /// </summary>
        public void CheckDbConfig(bool repair)
        {
            if (Entity.IsFreeze)
                return;
            if (Entity.NoDataBase)
            {
                foreach (var col in Entity.Properties)
                {
                    col.DbFieldName = null;
                    col.DbType = null;
                }
                Entity.IsModify = true;
                Entity.NoDataBase = true;
                Entity.ReadTableName = null;
                Entity.SaveTableName = null;
                return;
            }
            if (!Entity.IsInterface)
            {
                if (Entity.PrimaryColumn == null)
                {
                    var idf = Entity.Properties.FirstOrDefault(p => string.Equals(p.Name, "Id", System.StringComparison.OrdinalIgnoreCase));
                    if (idf != null)
                        idf.IsPrimaryKey = true;
                    else
                        Entity.Add(new PropertyConfig
                        {
                            Name = "Id",
                            Caption = Entity.Caption + "��ʶ",
                            Description = Entity.Caption + "��ʶ",
                            IsIdentity = true,
                            IsPrimaryKey = true,
                            DataType = SolutionConfig.Current.IdDataType
                        });
                }
                if (repair || string.IsNullOrWhiteSpace(Entity.SaveTableName))
                {
                    Entity.SaveTableName = DataBaseHelper.ToTableName(Entity);
                }
                if (repair)
                    Entity.ReadTableName = Entity.SaveTableName;
            }
            var model = new PropertyDatabaseBusiness
            {
                DataBaseType = Entity.Parent.DbType
            };
            foreach (var col in Entity.Properties)
            {
                col.Parent = Entity;
                if (col.IsDiscard)
                {
                    continue;
                }
                model.Property = col;
                model.CheckByDb(repair);
                col.IsModify = true;
            }
            CheckRelation();
            Entity.IsModify = true;
        }

        private void CheckRelation()
        {
            if (Entity.Properties.All(p => string.IsNullOrEmpty(p.LinkTable)))
            {
                return;
            }
            if (!DataBaseHelper.CheckFieldLink(Entity))
            {
                Entity.ReadTableName = Entity.SaveTable;
            }
            else if (string.IsNullOrEmpty(Entity.SaveTableName) || string.Equals(Entity.SaveTableName, Entity.ReadTableName))
            {
                Entity.ReadTableName = DataBaseHelper.ToViewName(Entity);
            }
        }
    }
}