using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 排序命令
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal class EntityEntitySortCommonds : DesignCommondBase<IEntityConfig>
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
                IsButton = false,
                TargetType = typeof(ProjectChildConfigBase),
                Action = SortField,
                Catalog = "排序",
                ConfirmMessage = "按自然顺序并从0更新序号吗?",
                Caption = "排序(自然顺序)",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                IsButton = true,
                TargetType = typeof(ProjectChildConfigBase),
                Action = SortFieldByIndex,
                Catalog = "排序",
                ConfirmMessage = "按序号大小排序并从0更新序号吗?",
                Caption = "按序号排序",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder<IEntityConfig>
            {
                TargetType = typeof(ProjectChildConfigBase),
                Action = SortByGroup,
                Caption = "按组排序",
                Description = "主键-标题最前面，相同组的字段临近，其它按序号",
                ConfirmMessage = "确认？",
                Catalog = "排序",
                IconName = "img_filter"
            });
        }

        #endregion

        #region 字段编辑

        void SortByGroup(IEntityConfig entity)
        {
            var business = new EntitySorter { Entity = entity };
            business.SortByGroup();
        }

        void SortField(IEntityConfig entity)
        {
            var business = new EntitySorter { Entity = entity };
            business.SortField();
        }


        void SortFieldByIndex(IEntityConfig entity)
        {
            var business = new EntitySorter { Entity = entity };
            business.SortFieldByIndex();
        }

        #endregion

    }

}