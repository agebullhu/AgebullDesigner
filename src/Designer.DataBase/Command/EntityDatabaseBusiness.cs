using System.Collections.Generic;
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
        ///     自动修复(从模型修复数据存储)
        /// </summary>
        public void CheckDbConfig(bool repair)
        {
            if (Entity.IsFreeze || Entity.NoDataBase)
                return;
            if (Entity.IsReference)
            {
                foreach (var col in Entity.Properties)
                {
                    col.ColumnName = null;
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
                            Caption = Entity.Caption + "ID",
                            Description = Entity.Caption + "ID",
                            IsPrimaryKey = true,
                            IsIdentity = true,
                            CsType = "int",
                            CppType = "int",
                            Parent = Entity
                        });
                }
                if (repair || string.IsNullOrWhiteSpace(Entity.SaveTableName))
                {
                    Entity.SaveTableName = DataBaseHelper.ToTableName(Entity);
                }
                Entity.ReadTableName = Entity.SaveTableName;
            }
            var model = new PropertyDatabaseBusiness
            {
                DataBaseType = Entity.Parent.DbType
            };
            foreach (var col in Entity.Properties)
            {
                if (col.IsDiscard)
                {
                    continue;
                }
                col.Parent = Entity;
                col.IsIdentity = col.IsPrimaryKey;
                if (col.Initialization == "getdate")
                    col.Initialization = "now()";
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
                Entity.ReadTableName = "view_" + Entity.SaveTable.Replace("tb_", "");
            }
        }
    }
}