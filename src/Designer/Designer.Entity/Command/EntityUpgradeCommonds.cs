using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class EntityUpgradeCommonds : DesignCommondBase<EntityConfig>
    {

        #region 操作命令

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = ToStandardName,
                Caption = "规范名称",
                Catalog = "修复",
                IconName = "tree_Type"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = ResetView,
                Caption = "重置视角",
                Catalog = "修复",
                IconName = "tree_Type"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = EmptyFromDb,
                Caption = "空值校验同数据库",
                Catalog = "实体",
                IconName = "tree_Type",
                WorkView = "adv"
            });

            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                SignleSoruce = true,
                IsButton = false,
                Action = SplitTable,
                Caption = "拆分到新表",
                SoruceView = "entity",
                Catalog = "实体",
                IconName = "img_add",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = RepairRegular,
                IsButton = false,
                Catalog = "实体",
                Caption = "规则修复",
                SoruceView = "entity",
                SignleSoruce = true,
                Editor = "Regular",
                IconName = "tree_item",
                WorkView = "adv"
            });

            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = DataTypeHelper.ToStandard,
                Caption = "按C#类型标准检查并修复",
                SoruceView = "entity",
                Catalog = "实体",
                IconName = "tree_Type",
                WorkView = "adv"
            });

            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = DataTypeHelper.ToStandardByDbType,
                Caption = "按数据库类型标准检查并修复",
                SoruceView = "entity",
                Catalog = "实体",
                IconName = "tree_Type",
                WorkView = "adv"
            });

            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = LinkKeyFirst,
                Caption = "外键排前",
                SoruceView = "entity",
                Catalog = "排序",
                IconName = "tree_Type",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = LinkKeyLast,
                Caption = "外键排后",
                SoruceView = "entity",
                Catalog = "排序",
                IconName = "tree_Type",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = FindRelationTable,
                Caption = "查找关联表",
                SoruceView = "entity",
                Catalog = "实体",
                IconName = "tree_Type",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = CheckName,
                Caption = "名称中的[标识]统一替换为[编号]",
                Catalog = "字段",
                IconName = "tree_item"
            });

            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = IdentityToBigint,
                Caption = "自增字段转为BIGINT类型",
                Catalog = "字段",
                IconName = "tree_item"
            });
        }

        #endregion

        /// <summary>
        /// 查找关联表
        /// </summary>
        public void FindRelationTable(EntityConfig entity)
        {
            if (entity.Properties.All(p => p.IsPrimaryKey || p.IsLinkKey))
            {
                entity.IsLinkTable = true;
                Trace.WriteLine(entity.Caption);
            }
        }

        /// <summary>
        /// 查找关联表
        /// </summary>
        public void IdentityToBigint(EntityConfig entity)
        {
            foreach (var field in entity.Properties.Where(p => p.IsIdentity))
            {
                field.FieldType = "BIGINT";
                field.DataType = "Int64";
                field.CsType = "long";
            }
            if (entity.IsLinkTable)
            {
                var field = entity.PrimaryColumn;

                field.FieldType = "BIGINT";
                field.DataType = "Int64";
                field.CsType = "long";
            }
        }


        /// <summary>
        /// 外键排前面
        /// </summary>
        public void LinkKeyFirst(EntityConfig entity)
        {
            foreach (var property in entity.Properties.Where(p => p.IsLinkKey))
            {
                property.Index = 1;
            }
        }
        /// <summary>
        /// 外键排前面
        /// </summary>
        public void LinkKeyLast(EntityConfig entity)
        {
            foreach (var property in entity.Properties.Where(p => p.IsLinkKey))
            {
                property.Index = 256;
            }
        }

        /// <summary>
        /// 数据对象名称检查
        /// </summary>
        public void CheckName(EntityConfig entity)
        {
            foreach (var property in entity.Properties)
            {
                if (string.IsNullOrWhiteSpace(property.Caption))
                    return;
                if (property.IsPrimaryKey && property.Caption == "主键")
                    property.Caption = $"{entity.Caption}ID";
                else
                    property.Caption = property.Caption.Replace("标识", "编号");

                if (!property.IsLinkField)
                    continue;
                var link = GlobalConfig.GetEntity(property.LinkTable);
                var fi = link?.Properties.FirstOrDefault(p => p.Name == property.LinkField);
                if (fi == null)
                    continue;
                if (property.IsLinkKey)
                    property.Caption = $"{link.Caption}ID";
                else if (fi.Caption.Contains(link.Caption))
                    property.Caption = fi.Caption;
            }
        }

        void ResetView(IEntityConfig entity)
        {
            entity.EnableDataBase = true;
            entity.EnableEditApi = true;
            entity.EnableUI = true;
        }

        void ToStandardName(EntityConfig entity)
        {
            var name = entity.Name;
            var words = GlobalConfig.ToWords(entity.Name);
            if (words[0].Equals("GL", StringComparison.OrdinalIgnoreCase))
            {
                words.RemoveAt(0);
            }
            if (words[0].Equals(entity.Project.Name, StringComparison.OrdinalIgnoreCase))
            {
                words.RemoveAt(0);
            }
            entity.Name = GlobalConfig.ToName(words);
            entity.SaveTableName = DataBaseHelper.ToTableName(entity);
            if (entity.ReadTableName == name)
                entity.ReadTableName = null;
            if (entity.OldName == null)
                entity.OldName = name;
        }


        void EmptyFromDb(EntityConfig entity)
        {
            foreach (var field in entity.Properties)
            {
                field.CanEmpty = field.DbNullable;
                field.IsRequired = !field.DbNullable;
            }
        }

        public void RepairRegular(EntityConfig entity)
        {
            var result = MessageBox.Show("是重置规则,否仅检查并修改不正确的设置项", "规则检查", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
            {
                return;
            }

            EntityBusinessModel business = new EntityBusinessModel
            {
                Entity = entity
            };
            business.RepairRegular(result == MessageBoxResult.Yes);
        }
        public void SplitTable(EntityConfig entity)
        {
            if (!(Context.SelectEntity is EntityConfig oldTable) ||
                Context.SelectColumns == null || Context.SelectColumns.Count == 0)
            {
                return;
            }
            var newTable = new EntityConfig();
            newTable.Copy(oldTable);
            if (!CommandIoc.NewConfigCommand("拆分到新实体", newTable))
                return;
            if (oldTable.PrimaryColumn != null)
            {
                var kc = new FieldConfig();
                kc.Copy(oldTable.PrimaryColumn);
                newTable.Add(kc);
            }
            foreach (var col in Context.SelectColumns.OfType<FieldConfig>().ToArray())
            {
                oldTable.Remove(col);
                newTable.Add(col);
            }
            Context.SelectProject.Add(newTable);
            Model.Tree.SetSelectEntity(newTable);
            Context.SelectColumns = null;
        }
    }
}