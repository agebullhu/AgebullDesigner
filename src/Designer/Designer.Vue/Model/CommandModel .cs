// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 配置:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-29
// *****************************************************/

#region 引用

using Agebull.Common;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

#endregion

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

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        public override void CreateCommands(IList<CommandItemBase> commands)
        {
            commands.Append(new CommandItem
            {
                Catalog = "新增命令",
                IsButton = true,
                SignleSoruce = true,
                Caption = "新增命令",
                Action = AddCommand,
                IconName = "img_add"
            });
            base.CreateCommands(commands);
        }

        #endregion

        void AddCommand(object arg)
        {
            Context.SelectEntity.Commands.Add(new UserCommandConfig());
        }
    }
}
