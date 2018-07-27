using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 扩展节点
    /// </summary>
    [Export(typeof(IAutoRegister))]
    [ExportMetadata("Symbol", '%')]
    internal sealed class EntityModel : DesignCommondBase<EntityConfig>
    {
        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected override void CreateCommands(List<ICommandItemBuilder> commands)
        {
            //if (SolutionConfig.Current.SolutionType != SolutionType.Cpp)
            //    return;
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                SignleSoruce = true,
                
                Action = (CheckDouble),
                Caption = "修复数据精度",
                Editor = "C++字段",
                IconName = "tree_item",
                ConfirmMessage= "确认将Double转为精确值吗?\n要知道这存在一定破坏性!"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                SignleSoruce = true,
                
                Editor = "C++字段",
                Action = (RepairByArrayLen),
                Caption = "修复文本长度",
                IconName = "tree_item",
                ConfirmMessage = "确认修复文本长度吗?\n要知道这存在一定破坏性!"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (RepairRegular),
                Caption = "C++引用修复",
                SignleSoruce = true,
                
                Editor = "C++字段",
                IconName = "tree_item",
                ConfirmMessage = "确认修复C++引用吗?"
            });
            commands.Add(new CommandItemBuilder<EntityConfig>
            {
                Action = (ResetRegular),
                Caption = "C++引用重置",
                SignleSoruce = true,
                
                Editor = "C++字段",
                IconName = "tree_item",
                ConfirmMessage = "确认重置C++引用吗?\n要知道这存在一定破坏性!"
            });
        }

        #region 修复
        public void RepairRegular(EntityConfig entity)
        {
            EntityBusinessModel business = new EntityBusinessModel
            {
                Entity = entity
            };
            business.RepairCpp(false);
        }
        public void ResetRegular(EntityConfig entity)
        {
            EntityBusinessModel business = new EntityBusinessModel
            {
                Entity = entity
            };
            business.RepairCpp(true);
        }
        public void CheckDouble(EntityConfig entity)
        {
            EntityBusinessModel business = new EntityBusinessModel
            {
                Entity = entity
            };
            business.CheckDouble();
        }

        public void RepairByArrayLen(EntityConfig entity)
        {
            EntityBusinessModel business = new EntityBusinessModel
            {
                Entity = entity
            };
            business.RepairByArrayLen();
        }

        #endregion

    }
}