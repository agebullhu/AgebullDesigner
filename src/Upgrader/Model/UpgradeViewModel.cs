// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using System.Collections.Generic;
using System.Windows.Media;
using Agebull.CodeRefactor.SolutionManager;
using Agebull.Common.DataModel;
using Gboxt.Common.WpfMvvmBase.Commands;
using Application = System.Windows.Application;

#endregion

namespace Agebull.Common.SimpleDesign
{
    public sealed class UpgradeViewModel : ViewModelBase<UpgradeModel>
    {
        public List<CommandItem> UpgradeCommands => new List<CommandItem>
        {
            new CommandItem
            {
                Command = new DelegateCommand(Model.Analyze),
                Name = "分析",
                Image = Application.Current.Resources["img_add"] as ImageSource
            },
            new CommandItem
            {
                Command = new DelegateCommand(Model.UpgradeCode),
                Name = "更新代码",
                Image = Application.Current.Resources["img_code"] as ImageSource
            },
            new CommandItem
            {
                Command = new DelegateCommand(Model.Save),
                Name = "保存",
                Image = Application.Current.Resources["img_save"] as ImageSource
            },
            new CommandItem
            {
                Command = new DelegateCommand(Model.Load ),
                Name = "载入",
                Image = Application.Current.Resources["img_load"] as ImageSource
            }
        };
    }
}