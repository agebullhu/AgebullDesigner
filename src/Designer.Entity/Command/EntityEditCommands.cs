// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 导入导出相关模型
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class EntityEditCommands : DesignCommondBase<EntityConfig>
    { 
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            commands.Add(new CommandItemBuilder
            {
                Action = SaveEntity,
                Caption = "保存所选对象",
                SignleSoruce = true,
                IsButton = true,
                Catalog = "编辑",
                ConfirmMessage= "确认强制保存吗?\n要知道这存在一定破坏性!",
                IconName = "tree_item"
            });

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

        /// <summary>
        /// 新增属性
        /// </summary>
        /// <param name="entity"></param>
        public void AddNewProperty(EntityConfig entity)
        {
            CommandIoc.AddFieldsCommand(entity);
        }

        /// <summary>
        /// 强制保存
        /// </summary>
        public void SaveEntity(object arg)
        {
            ConfigWriter writer = new ConfigWriter
            {
                Solution = Context.Solution,
            };
            if (Context.SelectProject != null)
            {
                writer.SaveProject(Context.SelectProject, Path.GetDirectoryName(Context.Solution.SaveFileName));
                return;
            }
            var tables = Context.GetSelectEntities();
            foreach (var entity in tables)
            {
                writer.SaveEntity(entity, Path.GetDirectoryName(Context.Solution.SaveFileName),true);
            }
        }

    }
}
