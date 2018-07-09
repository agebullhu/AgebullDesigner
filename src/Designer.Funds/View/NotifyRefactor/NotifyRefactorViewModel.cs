// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class NotifyRefactorViewModel : ViewModelBase<NotifyRefactorModel>
    {
        private List<CommandItem> _exCommands;

        public IEnumerable<CommandItem> ExCommands => _exCommands ?? (_exCommands = new List<CommandItem>
        {
            new CommandItem
            {
                Command = new AsyncCommand<string, List<NotifyItem>>
                    (Model.CheckNotifyPrepare, Model.DoCheckNotify, Model.CheckNotifyEnd)
                {
                    Detect = Model
                },
                Caption = "分析接口",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new DelegateCommand(Model.End),
                Caption = "接收到系统中",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            }
        });
    }
}