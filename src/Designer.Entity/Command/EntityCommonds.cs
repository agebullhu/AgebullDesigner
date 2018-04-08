using System;
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
            commands.Add(new CommandItemBuilder
            {
                Signle = false,
                Command = new DelegateCommand(SortFieldByIndex),
                Name = "按序号重排",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Signle = true,
                NoButton = true,
                Command = new DelegateCommand<EntityConfig>(SortField),
                Name = "重排字段(主键-标题最前面，相同表关联的字段临近，其它按序号)",
                //Catalog = "字段",
                IconName = "img_filter"
            });
            commands.Add(new CommandItemBuilder
            {
                Signle = true,
                NoButton = true,
                Command = new DelegateCommand(SortByGroup),
                Name = "按组重新排",
                //Catalog = "字段",
                IconName = "img_filter"
            });
            commands.Add(new CommandItemBuilder
            {
                Signle = true,
                NoButton = true,
                Command = new DelegateCommand(SplitTable),
                Name = "拆分到新表",
                //Catalog = "字段",
                IconName = "img_add"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(RepairRegular),
                Name = "规则修复",
                Signle = true,
                NoButton = true,
                //Catalog = "数据校验",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand<EntityConfig>(AddNewProperty),
                Name = "新增字段",
                Signle = true,
                NoButton = true,
                //Catalog = "字段",
                IconName = "tree_Open"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand<EntityConfig>(AddFields),
                Name = "新增多个字段",
                Signle = true,
                NoButton = true,
                //Catalog = "字段",
                IconName = "tree_Open"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand<EntityConfig>(CopyTable),
                Name = "复制表",
                Signle = true,
                NoButton = true,
                //Catalog = "字段",
                IconName = "tree_Child1"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand<EntityConfig>(DeleteTable),
                Name = "删除表",
                Signle = true,
                NoButton = true,
                IconName = "img_del"
            });//
        }

        #endregion

        

        #region 字段编辑
        public void SortByGroup()
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

        public void SortField(EntityConfig entity)
        {
            if (entity == null ||
                MessageBox.Show($"确认修改{entity.ReadTableName}的字段顺序吗?", "对象编辑", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }

            var model = new EntitySorter { Entity = entity };
            model.SortField();
        }


        public void SortFieldByIndex()
        {
            var result = MessageBox.Show("是按序号大小排序并从0更新序号,否仅按序号大小排序", "排序", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
            {
                return;
            }
            var business = new EntitySorter();
            Foreach(e =>
            {
                business.Entity = e;
                business.SortFieldByIndex(result == MessageBoxResult.Yes);
            });
        }

        
        #endregion


        #region 编辑表

        private void AddRelation(PropertyConfig column)
        {
            var config = new TableReleation
            {
                Parent = Context.SelectRelationTable,
                Name = column.Parent.Name,
                Friend = column.Parent.Name,
                ForeignKey = column.Name,
                PrimaryKey = Context.SelectRelationColumn.Name
            };
            if (CommandIoc.NewConfigCommand("新增关联信息", config))
                Context.SelectRelationTable.Releations.Add(config);
        }

        /// <summary>
        /// 新增属性
        /// </summary>
        /// <param name="entity"></param>
        public void AddNewProperty(EntityConfig entity)
        {
            var config = new PropertyConfig
            {
                Parent = entity,
                Name = "NewField",
                CsType = "string"
            };
            if (CommandIoc.NewConfigCommand("新增字段", config))
                entity.Properties.Add(config);
        }

        private bool CanAddRelation(PropertyConfig column)
        {
            return column?.Parent != null && Context.SelectRelationTable != null
                   && Context.SelectRelationColumn != null
                   && Context.SelectRelationTable.Releations.All(p => p.Friend != column.Parent.Name);
        }
        public void RepairRegular()
        {
            var result = MessageBox.Show("是重置规划,否仅检查并修改不正确的设置项", "规则检查", MessageBoxButton.YesNoCancel);
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
        public void SplitTable()
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
                kc.CopyFrom(oldTable.PrimaryColumn);
                newTable.Properties.Add(kc);
            }
            foreach (var col in Context.SelectColumns.OfType<PropertyConfig>().ToArray())
            {
                oldTable.Properties.Remove(col);
                newTable.Properties.Add(col);
            }
            Context.Entities.Add(newTable);
            Model.Tree.SetSelect(newTable);
            Context.SelectColumns = null;
        }


        public void DeleteTable(EntityConfig entity)
        {
            if (entity == null ||
                MessageBox.Show($"确认删除{entity.ReadTableName}吗?", "对象编辑", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
            {
                return;
            }
            Context.Entities.Remove(entity);
            Context.SelectColumns = null;
        }


        public void AddFields(EntityConfig entity)
        {
            if (entity == null)
            {
                return;
            }
            var nentity = CommandIoc.AddFieldsCommand();
            if (nentity == null)
            {
                return;
            }
            entity.Properties.AddRange(nentity.Properties);
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
