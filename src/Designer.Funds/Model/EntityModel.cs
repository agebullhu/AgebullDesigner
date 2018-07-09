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
            commands.Add(new CommandItemBuilder
            {
                Signle = true,
                NoButton = true,
                Command = new DelegateCommand(CheckDouble),
                Caption = "修复数据精度",
                Editor = "C++字段",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Signle = true,
                NoButton = true,
                Editor = "C++字段",
                Command = new DelegateCommand(RepairByArrayLen),
                Caption = "修复文本长度",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(RepairRegular),
                Caption = "C++引用修复",
                Signle = true,
                NoButton = true,
                Editor = "C++字段",
                IconName = "tree_item"
            });
        }

        #region 修复
        public void RepairRegular()
        {
            var result = MessageBox.Show("选择【是】重置选项,【否】仅检查并修复不正确的设置项", "C++引用修复", MessageBoxButton.YesNoCancel);
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
                business.RepairCpp(result == MessageBoxResult.Yes);
            }
        }
        public void CheckDouble()
        {
            if (MessageBox.Show("确认将Double转为精确值吗?\n要知道这存在一定破坏性!", "对象编辑", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
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
                business.CheckDouble();
            }
        }

        public void RepairByArrayLen()
        {
            if (MessageBox.Show("确认修复文本长度吗?\n要知道这存在一定破坏性!", "对象编辑", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes)
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
                business.RepairByArrayLen();
            }
        }

        #endregion

    }
}