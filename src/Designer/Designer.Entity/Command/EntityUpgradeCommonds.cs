using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.V2021;
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
    internal class EntityUpgradeCommonds : DesignCommondBase<IEntityConfig>
    {

        #region 操作命令

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = ToStandardName,
                Caption = "规范名称",
                Catalog = "修复",
                IconName = "规范"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = ResetView,
                Caption = "重置视角",
                Catalog = "修复",
                IconName = "视角"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = EmptyFromDb,
                Caption = "空值校验同数据库",
                Catalog = "实体",
                IconName = "检查",
                WorkView = "adv"
            });

            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                SignleSoruce = true,
                IsButton = false,
                Action = SplitTable,
                Caption = "拆分到新表",
                SoruceView = "entity",
                Catalog = "实体",
                IconName = "拆分",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = RepairRegular,
                IsButton = false,
                Catalog = "实体",
                Caption = "规则修复",
                SoruceView = "entity",
                SignleSoruce = true,
                Editor = "Regular",
                IconName = "修复",
                WorkView = "adv"
            });

            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = DataTypeHelper.ToStandard,
                Caption = "按C#类型标准检查并修复",
                SoruceView = "entity",
                Catalog = "实体",
                IconName = "C#",
                WorkView = "adv"
            });

            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = DataTypeHelper.ToStandardByDbType,
                Caption = "按数据库类型标准检查并修复",
                SoruceView = "entity",
                Catalog = "实体",
                IconName = "数据库",
                WorkView = "adv"
            });

            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = LinkKeyFirst,
                Caption = "外键排前",
                SoruceView = "entity",
                Catalog = "排序",
                IconName = "排序",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = LinkKeyLast,
                Caption = "外键排后",
                SoruceView = "entity",
                Catalog = "排序",
                IconName = "排序",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = FindRelationTable,
                Caption = "查找关联表",
                SoruceView = "entity",
                Catalog = "实体",
                IconName = "查找",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = CheckName,
                Caption = "名称中的[标识]统一替换为[编号]",
                Catalog = "字段",
                IconName = "字段"
            });

            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                Action = IdentityToBigint,
                Caption = "自增字段转为BIGINT类型",
                Catalog = "字段",
                IconName = "字段"
            });
        }

        #endregion

        /// <summary>
        /// 查找关联表
        /// </summary>
        public void FindRelationTable(IEntityConfig entity)
        {
            if (entity.Properties.All(p => p.IsPrimaryKey || p.DataBaseField.IsLinkKey))
            {
                entity.IsLinkTable = true;
                Trace.WriteLine(entity.Caption);
            }
        }

        /// <summary>
        /// 查找关联表
        /// </summary>
        public void IdentityToBigint(IEntityConfig entity)
        {
            foreach (var field in entity.Where(p => p.IsIdentity))
            {
                field.DataBaseField.FieldType = "BIGINT";
                field.DataType = "Int64";
                field.CsType = "long";
            }
            if (entity.IsLinkTable)
            {
                var field = entity.PrimaryColumn;

                field.DataBaseField.FieldType = "BIGINT";
                field.DataType = "Int64";
                field.CsType = "long";
            }
        }


        /// <summary>
        /// 外键排前面
        /// </summary>
        public void LinkKeyFirst(IEntityConfig entity)
        {
            foreach (var property in entity.Where(p => p.DataBaseField.IsLinkKey))
            {
                property.Index = 1;
            }
        }

        /// <summary>
        /// 外键排前面
        /// </summary>
        public void LinkKeyLast(IEntityConfig entity)
        {
            foreach (var property in entity.Where(p => p.DataBaseField.IsLinkKey))
            {
                property.Index = 256;
            }
        }

        /// <summary>
        /// 数据对象名称检查
        /// </summary>
        public void CheckName(IEntityConfig entity)
        {
            foreach (var property in entity.Properties)
            {
                if (string.IsNullOrWhiteSpace(property.Caption))
                    return;
                if (property.IsPrimaryKey && property.Caption == "主键")
                    property.Caption = $"{entity.Caption}ID";
                else
                    property.Caption = property.Caption.Replace("标识", "编号");

                var field = property.DataBaseField;
                if (field == null || !field.IsLinkField)
                    continue;
                var link = GlobalConfig.GetEntity(field.LinkTable);
                var fi = link?.DataTable.Find(field.LinkField);
                if (fi == null)
                    continue;
                if (field.IsLinkKey)
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

        void ToStandardName(IEntityConfig entity)
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
            entity.DataTable.SaveTableName = DataBaseHelper.ToTableName(entity);
            if (entity.DataTable.ReadTableName == name)
                entity.DataTable.ReadTableName = null;
            if (entity.OldName == null)
                entity.OldName = name;
        }


        void EmptyFromDb(IEntityConfig entity)
        {
            foreach (var property in entity.Properties)
            {
                property.CanEmpty = property.DataBaseField.DbNullable;
                property.IsRequired = !property.DataBaseField.DbNullable;
            }
        }

        public void RepairRegular(IEntityConfig entity)
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
        public void SplitTable(IEntityConfig entity)
        {
            if (!(Context.SelectEntity is IEntityConfig oldTable) ||
                Context.SelectColumns == null || Context.SelectColumns.Count == 0)
            {
                return;
            }
            var newTable = new EntityConfig();
            newTable.Copy(oldTable.Entity);
            if (!CommandIoc.NewConfigCommand("拆分到新实体", newTable))
                return;
            if (oldTable.PrimaryColumn != null)
            {
                var kc = new FieldConfig();
                kc.Copy(oldTable.PrimaryColumn);
                newTable.Add(kc);//触发器会自动加入数据库字段
                kc.DataBaseField.Copy(oldTable.PrimaryColumn.Field);
                kc.DataBaseField.IsIdentity = false;
                kc.DataBaseField.IsPrimaryKey = false;
                kc.DataBaseField.LinkTable = oldTable.Name;
                kc.DataBaseField.LinkField = oldTable.PrimaryColumn.Name;
                kc.DataBaseField.IsLinkField = true;
            }
            foreach (var col in Context.SelectColumns.OfType<FieldConfig>().ToArray())
            {
                oldTable.Entity.Remove(col);
                newTable.Add(col);
            }
            Context.SelectProject.Add(newTable);
            Model.Tree.SetSelectEntity(newTable);
            Context.SelectColumns = null;
        }
    }
}