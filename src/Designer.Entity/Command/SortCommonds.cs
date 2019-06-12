using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 排序命令
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class SortCommonds : DesignCommondBase<EntityConfig>
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
                IsButton = true,
                Action = IdentityByIndex,
                Catalog = "排序",
                ConfirmMessage = "如果使用了TSON序列化请立即取消,否则数据解码将混乱",
                Caption = "标识与序号相同",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                IsButton = true,
                Action = SortFields,
                Catalog = "排序",
                ConfirmMessage = "按自然顺序并从0更新序号吗?",
                Caption = "排序(自然顺序)",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                IsButton = true,
                Action = SortFieldByIndex,
                Catalog = "排序",
                ConfirmMessage = "按序号大小排序并从0更新序号吗?",
                Caption = "排序(按序号)",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                
                Catalog = "排序",
                IsButton = false,
                Action = SortField,
                Caption = "排序(主键标题优先)",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                
                IsButton = false,
                Action = SortField,
                Caption = "排序(主键标题优先,表关联临近)",
                Description = "主键-标题最前面，相同表关联的字段临近，其它按序号",
                Catalog = "排序",
                IconName = "img_filter"
            });
            commands.Add(new CommandItemBuilder
            {
                
                IsButton = false,
                Action = SortByGroup,
                Caption = "排序(按组)",
                Description = "主键-标题最前面，相同组的字段临近，其它按序号",
                Catalog = "排序",
                IconName = "img_filter"
            });
        }

        #endregion

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

    }
}