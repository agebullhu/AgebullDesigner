// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用


#endregion

using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Agebull.EntityModel.Designer
{
    internal class CommandViewModel : EditorViewModelBase<CommandModel>
    {
        public CommandViewModel()
        {
            EditorName = "Command";
        }

    }

    internal class CommandModel : DesignModelBase
    {
        #region 操作命令

        public CommandModel()
        {
            Model = DataModelDesignModel.Current;
            Context = DataModelDesignModel.Current?.Context;
        }

        #endregion

        public override void CreateCommands(IList<CommandItemBase> commands)
        {
            commands.Add(new SimpleCommandItem
            {
                IsButton=true,
                SignleSoruce = true,
                Action = AddCommand,
                Caption = "新增命令",
                IconName = "新增"
            });
            commands.Add(new SimpleCommandItem
            {
                IsButton = true,
                SignleSoruce = true,
                Action = RemoveCommand,
                Caption = "删除命令",
                IconName = "删除"
            });
        }

        /// <summary>
        /// 新增命令
        /// </summary>
        /// <param name="entity"></param>
        public void AddCommand()
        {
            if (Model.CreateNew("新增命令", out UserCommandConfig config))
                Context.SelectModel.Add(config);
        }

        /// <summary>
        /// 新增命令
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveCommand()
        {
            if (Context.SelectModel == null || Context.SelectColumns == null ||
                MessageBox.Show("确认删除所选行吗?", "对象编辑", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            foreach (var col in Context.SelectColumns.OfType<UserCommandConfig>().ToArray())
            {
                Context.SelectModel.Commands.Remove(col);
            }
            Context.SelectColumns = null;
        }

    }
}
