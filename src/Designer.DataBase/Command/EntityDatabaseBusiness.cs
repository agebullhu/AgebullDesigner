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
            if (Entity.IsFreeze || Entity.IsInterface)
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

            if (Entity.PrimaryColumn == null)
            {
                var idf = Entity.Properties.FirstOrDefault(p =>
                    string.Equals(p.Name, "Id", System.StringComparison.OrdinalIgnoreCase));
                if (idf != null)
                    idf.IsPrimaryKey = true;
                else
                    Entity.Add(new PropertyConfig
                    {
                        Name = "Id",
                        Caption = Entity.Caption + "���",
                        IsIdentity = true,
                        IsPrimaryKey = true,
                        DataType = SolutionConfig.Current.IdDataType
                    });
            }

            if (repair || string.IsNullOrWhiteSpace(Entity.SaveTableName))
            {
                Entity.SaveTableName = DataBaseHelper.ToTableName(Entity);
            }
            CheckRelation(repair);
            var model = new PropertyDatabaseBusiness();
            foreach (var col in Entity.Properties)
            {
                col.Parent = Entity;
                if (col.IsDiscard)
                {
                    continue;
                }
                model.Property = col;
                model.CheckByDb(repair);
            }
        }

        public void CheckRelation()
        {
            CheckRelation(false);
        }
        public void CheckRelation(bool repair)
        {
            DataBaseHelper.CheckFieldLink(Entity);
            if (Entity.Properties.Any(p => p.IsLinkField && !p.IsLinkKey))
            {
                if (Entity.ReadTableName == Entity.SaveTableName || repair)
                {
                    Entity.ReadTableName = DataBaseHelper.ToViewName(Entity);
                }
            }
            else
            {
                Entity.ReadTableName = null;
            }
        }
    }
}