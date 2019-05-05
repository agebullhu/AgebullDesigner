using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

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
                Action = ToClass,
                Caption = "全部设置为普通类",
                Catalog = "修复",
                IconName = "tree_Type",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = EmptyFromDb,
                Caption = "空值校验同数据库",
                Catalog = "修复",
                IconName = "tree_Type",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = ToNoClass,
                Caption = "全部设置为存储类",
                Catalog = "修复",
                IconName = "tree_Type",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder
            {
                SignleSoruce = true,
                IsButton = false,
                Action = SplitTable,
                Caption = "拆分到新表",
                Catalog = "修复",
                IconName = "img_add",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder
            {
                Action = RepairRegular,
                IsButton = false,
                Catalog = "修复",
                Caption = "规则修复",
                SignleSoruce = true,
                Editor = "Regular",
                IconName = "tree_item",
                WorkView = "adv"
            });

            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = DataTypeHelper.ToStandard,
                Caption = "按数据类型标准检查并修复",
                Catalog = "修复",
                IconName = "tree_Type",
                WorkView = "adv"
            });
        }

        #endregion

        void ToClass(EntityConfig entity)
        {
            entity.NoDataBase = true;
        }
        void ToNoClass(EntityConfig entity)
        {
            entity.NoDataBase = false;
        }
        void EmptyFromDb(EntityConfig entity)
        {
            foreach (var field in entity.Properties)
            {
                field.CanEmpty = field.DbNullable;
                field.IsRequired = !field.DbNullable;
            }
        }

        public void RepairRegular(object arg)
        {
            var result = MessageBox.Show("是重置规则,否仅检查并修改不正确的设置项", "规则检查", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
            {
                return;
            }
            var tables = Context.GetSelectEntities();
            foreach (var entity in tables)
            {
                EntityBusinessModel business = new EntityBusinessModel
                {
                    Entity = entity
                };
                business.RepairRegular(result == MessageBoxResult.Yes);
            }
        }
        public void SplitTable(object arg)
        {
            if (Context.SelectEntity == null || Context.SelectColumns == null || Context.SelectColumns.Count == 0)
            {
                return;
            }
            var oldTable = Context.SelectEntity;
            var newTable = new EntityConfig();
            newTable.CopyValue(oldTable, true);
            if (!CommandIoc.NewConfigCommand("拆分到新实体", newTable))
                return;
            if (oldTable.PrimaryColumn != null)
            {
                var kc = new PropertyConfig();
                kc.CopyFromProperty(oldTable.PrimaryColumn, true, true, true);
                newTable.Add(kc);
            }
            foreach (var col in Context.SelectColumns.OfType<PropertyConfig>().ToArray())
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