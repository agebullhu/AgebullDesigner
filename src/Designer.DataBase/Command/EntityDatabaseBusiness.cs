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
                if (string.IsNullOrWhiteSpace(Entity.ReadTableName))
                {
                    string head = "tb_";
                    if (Entity.Classify != null)
                    {
                        var cls = Entity.Parent.Classifies.FirstOrDefault(p => p.Name == Entity.Classify);
                        if (cls != null)
                            head = cls.Abbreviation?.ToLower() + "_";
                    }
                    if (!string.IsNullOrWhiteSpace(Entity.Parent.Abbreviation))
                        head += Entity.Parent.Abbreviation.ToLower() + "_";
                    Entity.ReadTableName = SplitWords(Entity.Name).Select(p => p.ToLower()).LinkToString(head, "_");
                }
                Entity.SaveTableName = Entity.ReadTableName;
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
            if (string.IsNullOrEmpty(Entity.SaveTableName) || string.Equals(Entity.SaveTableName, Entity.ReadTableName))
            {
                Entity.ReadTableName = "view_" + Entity.SaveTable.Replace("tb_", "");
            }
            var tables = new Dictionary<string, EntityConfig>();

            var names = Entity.Properties.Where(p => !string.IsNullOrEmpty(p.LinkTable))
                    .Select(p => p.LinkTable)
                    .DistinctBy()
                    .ToArray();
            foreach (var name in names)
            {
                if (tables.ContainsKey(name))
                    continue;
                var table = GetEntity(p => p.SaveTable == name || p.Name == name);
                if (table != null)
                    tables.Add(name, table);
            }
            foreach (var field in Entity.PublishProperty)
            {
                if (!string.IsNullOrEmpty(field.LinkTable))
                {
                    if (tables.TryGetValue(field.LinkTable, out EntityConfig friend))
                    {
                        field.LinkTable = friend.SaveTable;
                        var linkField =
                            friend.Properties.FirstOrDefault(
                                p => p.ColumnName == field.LinkField || p.Name == field.LinkField);
                        if (linkField != null)
                        {
                            field.LinkField = linkField.ColumnName;
                            field.IsLinkKey = linkField.IsPrimaryKey;
                            field.IsLinkField = true;
                            field.IsLinkCaption = linkField.IsCaption;
                            if (!field.IsLinkKey)
                                field.IsCompute = true;
                            continue;
                        }
                    }
                }
                field.IsLinkField = false;
                field.IsLinkKey = false;
                field.IsLinkCaption = false;
            }
        }
    }
}