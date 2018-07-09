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
    public sealed class CppRefactorViewModel : ViewModelBase<CppRefactorModel>
    {
        private List<CommandItem> _exCommands;

        public IEnumerable<CommandItem> ExCommands => _exCommands ?? (_exCommands = new List<CommandItem>
        {
            new CommandItem
            {
                Command = new AsyncCommand<string, List<TypedefItem>>
                    (Model.CheckTypedefPrepare, Model.DoCheckTypedef, Model.CheckTypedefEnd)
                {
                    Detect = Model
                },
                Caption = "分析宏类型",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new AsyncCommand<string, List<EntityConfig>>
                    (Model.CheckCppPrepare, Model.DoCheckCpp, Model.CheckCppEnd)
                {
                    Detect = Model
                },
                Caption = "分析C++结构文本",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new DelegateCommand(Model.End),
                Caption = "接收到系统中",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            }
        });
        private string _systemName = "0";
        /// <summary>
        ///     当前文件名
        /// </summary>
        public string SystemName
        {
            get => _systemName;
            set
            {
                if (_systemName == value)
                    return;
                _systemName = value;
                RaisePropertyChanged(() => SystemName);
            }
        }
    }
}