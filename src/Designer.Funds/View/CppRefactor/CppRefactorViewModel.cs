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
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;

#endregion

namespace Agebull.EntityModel.Designer
{
    public sealed class CppRefactorViewModel : ViewModelBase<CppRefactorModel>
    {
        private List<CommandItemBase> _exCommands;
        private string _systemName = "0";

        public IEnumerable<CommandItemBase> ExCommands => _exCommands ?? (_exCommands = new List<CommandItemBase>
        {
            new AsyncCommandItem<string, List<TypedefItem>>(Model.CheckTypedefPrepare, Model.DoCheckTypedef,
                Model.CheckTypedefEnd)
            {
                Source = Model,
                Caption = "分析宏类型",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new AsyncCommandItem<string, List<EntityConfig>>(Model.CheckCppPrepare, Model.DoCheckCpp, Model.CheckCppEnd)
            {
                Source = Model,
                Caption = "分析C++结构文本",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Action = arg=>Model.End(),
                Caption = "接收到系统中",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            }
        });

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