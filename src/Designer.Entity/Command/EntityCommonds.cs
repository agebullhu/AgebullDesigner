﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class EntityCommonds : DesignCommondBase<EntityConfig>
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
                Catalog = "升级",
                Action = ClearOldInterface,
                Caption = "清理接口强绑定",
                IconName = "tree_sum"
            });
            //commands.Add(new CommandItemBuilder<EntityConfig>
            //{
            //    Action = ToClassify,
            //    Caption = "HIS分类",
            //    Catalog = "HIS",
            //    IconName = "tree_Type",
            //    WorkView = "adv"
            //});
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = ToClass,
                Caption = "全部设置为普通类",
                Catalog = "设计",
                IconName = "tree_Type",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = EmptyFromDb,
                Caption = "空值校验同数据库",
                Catalog = "设计",
                IconName = "tree_Type",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = ToNoClass,
                Caption = "全部设置为存储类",
                Catalog = "设计",
                IconName = "tree_Type",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                IsButton = true,
                Action = IdentityByIndex,
                Catalog = "工具",
                ConfirmMessage = "如果使用了TSON序列化请立即取消,否则数据解码将混乱",
                Caption = "标识与序号相同",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                IsButton = true,
                Action = SortFields,
                Catalog = "工具",
                ConfirmMessage = "按自然顺序并从0更新序号吗?",
                Caption = "排序(自然顺序)",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                IsButton = true,
                Action = SortFieldByIndex,
                Catalog = "工具",
                ConfirmMessage = "按序号大小排序并从0更新序号吗?",
                Caption = "排序(按序号)",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                SignleSoruce = true,
                Catalog = "工具",
                IsButton = false,
                Action = SortField,
                Caption = "排序(主键标题优先)",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                SignleSoruce = true,
                IsButton = false,
                Action = SortField,
                Caption = "排序(主键标题优先,表关联临近)",
                Description = "主键-标题最前面，相同表关联的字段临近，其它按序号",
                Catalog = "工具",
                IconName = "img_filter"
            });
            commands.Add(new CommandItemBuilder
            {
                SignleSoruce = true,
                IsButton = false,
                Action = SortByGroup,
                Caption = "排序(按组)",
                Description = "主键-标题最前面，相同组的字段临近，其它按序号",
                Catalog = "工具",
                IconName = "img_filter"
            });
            commands.Add(new CommandItemBuilder
            {
                SignleSoruce = true,
                IsButton = false,
                Action = SplitTable,
                Caption = "拆分到新表",
                Catalog = "工具",
                IconName = "img_add",
                WorkView = "adv"
            });
            commands.Add(new CommandItemBuilder
            {
                Action = RepairRegular,
                IsButton = false,
                Catalog = "工具",
                Caption = "规则修复",
                SignleSoruce = true,
                Editor = "Regular",
                IconName = "tree_item",
                WorkView = "adv"
            });
            //commands.Add(new CommandItemBuilder
            //{
            //    Command = <EntityConfig>(AddFields),
            //    Caption = "新增多个字段",
            //    Signle = true,
            //    NoButton = true,
            //    Catalog = "编辑",
            //    IconName = "tree_Open"
            //});
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = AddNewProperty,
                Caption = "导入字段",
                SignleSoruce = true,
                IsButton = true,
                Catalog = "编辑",
                IconName = "tree_Open",
                SoruceView = "property"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = CopyTable,
                Caption = "复制实体",
                IsButton = true,
                SignleSoruce = true,
                Catalog = "编辑",
                IconName = "tree_Child1"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = DeleteEntity,
                Caption = "删除实体",
                IsButton = false,
                SignleSoruce = true,
                Catalog = "编辑",
                IconName = "img_del"
            });
        }

        #endregion

        public void ClearOldInterface(EntityConfig entity)
        {
            foreach (var field in entity.Properties.ToArray())
            {
                if (field.IsSystemField)
                    entity.Remove(field);
            }
        }
        void ToClassify(EntityConfig entity)
        {
            if (string.Equals(entity.Classify, "None", StringComparison.OrdinalIgnoreCase))
                entity.Classify = entity.ReadTableName.Split('_')[0];
        }
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
        

        #region 字段编辑
        public void SortByGroup(object arg)
        {
            if (Context.SelectEntity == null ||
                MessageBox.Show($"确认修改{Context.SelectEntity.ReadTableName}的字段顺序吗?", "对象编辑", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }

            var model = new EntitySorter { Entity = Context.SelectEntity };
            model.SortByGroup();
        }

        public void SortField(object arg)
        {
            if (Context.SelectEntity == null ||
                MessageBox.Show($"确认修改{Context.SelectEntity.ReadTableName}的字段顺序吗?", "对象编辑", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }

            var model = new EntitySorter { Entity = Context.SelectEntity };
            model.SortField();
        }


        public void SortFieldByIndex(EntityConfig entity)
        {
            var business = new EntitySorter { Entity = entity };
            business.SortFieldByIndex(true);
        }
        public void SortFields(EntityConfig entity)
        {
            int idx = 0;
            foreach (var field in entity.Properties.OrderBy(p => p.Identity))
                field.Index = idx++;

        }
        public void IdentityByIndex(EntityConfig entity)
        {
            var business = new EntitySorter { Entity = entity };
            business.IdentityByIndex();
        }


        #endregion


        #region 编辑表

        //private void AddRelation(PropertyConfig column)
        //{
        //    var config = new TableReleation
        //    {
        //        Parent = Context.SelectRelationTable,
        //        Name = column.Parent.Name,
        //        Friend = column.Parent.Name,
        //        ForeignKey = column.Name,
        //        PrimaryKey = Context.SelectRelationColumn.Name
        //    };
        //    if (CommandIoc.NewConfigCommand("新增关联信息", config))
        //        Context.SelectRelationTable.Releations.Add(config);
        //}

        /// <summary>
        /// 新增属性
        /// </summary>
        /// <param name="entity"></param>
        public void AddNewProperty(EntityConfig entity)
        {
            CommandIoc.AddFieldsCommand(entity);
        }

        private bool CanAddRelation(PropertyConfig column)
        {
            return column?.Parent != null && Context.SelectRelationTable != null
                   && Context.SelectRelationColumn != null
                   && Context.SelectRelationTable.Releations.All(p => p.Friend != column.Parent.Name);
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


        public void DeleteEntity(EntityConfig entity)
        {
            if (entity == null ||
                MessageBox.Show($"确认删除{entity.Name}({entity.Caption})吗?", "对象编辑", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }
            entity.Parent.Remove(entity);
        }


        public void CopyTable(EntityConfig entity)
        {
            Context.CopiedTable = new EntityConfig();
            Context.CopiedTable.CopyValue(entity);
            Context.CopiedTables.Add(Context.CopiedTable);
            RaisePropertyChanged(() => Context.CopiedTableCounts);
        }

        #endregion
    }
}
