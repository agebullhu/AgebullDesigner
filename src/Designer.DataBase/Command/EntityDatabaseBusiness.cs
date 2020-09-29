using System.Linq;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 实体排序器
    /// </summary>
    public class EntityDatabaseBusiness : ConfigModelBase
    {
        /// <summary>
        /// 表结构对象
        /// </summary>
        public EntityConfig Entity { get; set; }


        /// <summary>
        ///     规范名称
        /// </summary>
        public void StandardName()
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
            if (Entity.SaveTableName == Entity.ReadTableName)
                Entity.ReadTableName = null;

            if (string.IsNullOrWhiteSpace(Entity.OldName))
                Entity.OldName = Entity.Alias ?? Entity.SaveTableName;

            var name = DataBaseHelper.ToTableName(Entity);
            if (Entity.SaveTableName == null || name != Entity.SaveTableName)
            {
                Entity.SaveTableName = name;
            }
            foreach (var col in Entity.Properties)
            {
                col.Entity = Entity;
                if (col.IsDiscard)
                {
                    continue;
                }
                if(string.IsNullOrWhiteSpace(col.OldName))
                col.OldName = col.DbFieldName;
                col.DbFieldName = DataBaseHelper.ToDbFieldName(col.Name);
            }
        }


        /// <summary>
        ///     自动修复(从模型修复数据存储)
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
                    Entity.Add(new FieldConfig
                    {
                        Name = "Id",
                        Caption = Entity.Caption + "编号",
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
                col.Entity = Entity;
                if (col.IsDiscard)
                {
                    continue;
                }
                model.Field = col;
                model.CheckByDb(repair);
            }
        }

        public void CheckRelation()
        {
            CheckRelation(false);
        }
        public void CheckRelation(bool repair)
        {
            DataBaseHelper.CheckFieldLink(Entity.Properties);
            //if (Entity.Fields.Any(p => p.IsLinkField && !p.IsLinkKey))
            //{
            //    if (Entity.ReadTableName == Entity.SaveTableName || repair)
            //    {
            //        Entity.ReadTableName = DataBaseHelper.ToViewName(Entity);
            //    }
            //}
            //else
            //{
            //    Entity.ReadTableName = null;
            //}
        }
    }
}