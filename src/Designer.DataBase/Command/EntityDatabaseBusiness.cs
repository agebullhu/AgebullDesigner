using System.Collections.Generic;
using System.Linq;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 实体排序器
    /// </summary>
    public class EntityDatabaseBusiness: ConfigModelBase
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
            if (Entity.IsFreeze)
                return;
            if (Entity.IsReference)
            {
                foreach (var col in Entity.Properties)
                {
                    col.ColumnName = null;
                    col.DbType = null;
                }
                Entity.IsModify = true;
                Entity.IsClass = true;
                Entity.ReadTableName = null;
                Entity.SaveTableName = null;
                return;
            }
            if (Entity.IsClass)
            {
                Entity.ReadTableName = null;
                Entity.SaveTableName = null;
            }
            else
            {
                if (Entity.PrimaryColumn == null)
                {
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
                    /*if (Entity.Classify != null)
                    {
                        var cls = Entity.Parent.Classifies.FirstOrDefault(p => p.Name == Entity.Classify);
                        if (cls != null)
                            head = cls.Abbreviation?.ToLower() + "_";
                    }*/
                    //if (!string.IsNullOrWhiteSpace(Entity.Parent.Abbreviation))
                    //    head += Entity.Parent.Abbreviation.ToLower() + "_";
                    Entity.ReadTableName = head + Entity.Name;//SplitWords(Entity.Name).Select(p => p.ToLower()).LinkToString(head, "_");
                }
                Entity.SaveTableName = Entity.ReadTableName;
            }
            PropertyDatabaseBusiness model = new PropertyDatabaseBusiness();
            foreach (var col in Entity.Properties)
            {
                if (col.Discard)
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
            //if (!Entity.IsReference)
            //    CheckRelation();
            Entity.IsModify = true;
        }

        private void CheckRelation()
        {
            if (Entity.DbFields.All(p => string.IsNullOrEmpty(p.LinkTable)))
            {
                return;
            }
            if (string.IsNullOrEmpty(Entity.SaveTableName) || string.Equals(Entity.SaveTableName, Entity.ReadTableName))
            {
                Entity.ReadTableName = "view_" + Entity.SaveTable.Replace("tb_", "");
            }
            var tables = new Dictionary<string, EntityConfig>();

            var names =
                Entity.DbFields.Where(p => !string.IsNullOrEmpty(p.LinkTable))
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
                    EntityConfig friend;
                    if (tables.TryGetValue(field.LinkTable, out friend))
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

    internal class PropertyDatabaseBusiness
    {
        public PropertyConfig Property { get; set; }

        internal void CheckByDb(bool repair = false)
        {
            if (Property.Parent.IsClass)
            {
                Property.ColumnName = null;
                Property.DbType = null;
            }
            else
            {
                if (repair || string.IsNullOrWhiteSpace(Property.ColumnName))
                    Property.ColumnName = GlobalConfig.ToName(GlobalConfig.SplitWords(Property.Name).Select(p => p.ToLower()).ToList());
                if (repair || string.IsNullOrWhiteSpace(Property.DbType))
                    Property.DbType = DataBaseHelper.ToDataBaseType(Property);
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