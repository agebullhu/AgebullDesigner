using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;
using Gboxt.Common.WpfMvvmBase.Commands;

namespace Agebull.CodeRefactor.SolutionManager
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
            commands.Add(new CommandItemBuilder
            {
                Signle = true,
                NoButton = true,
                Command = new DelegateCommand(CheckDouble),
                Name = "修复数据精度",
                Catalog = "规则",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Signle = true,
                NoButton = true,
                Catalog = "规则",
                Command = new DelegateCommand(RepairByArrayLen),
                Name = "修复文本长度",
                IconName = "tree_item"
            });
            commands.Add(new CommandItemBuilder
            {
                Command = new DelegateCommand(RepairRegular),
                Name = "C++引用修复",
                Signle = true,
                NoButton = true,
                Catalog = "C++",
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