using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Designer.View;

namespace Agebull.EntityModel.Designer
{
    public sealed class EnumEditViewModel : ExtendViewModelBase<EnumEditModel>
    {
        protected override ObservableCollection<CommandItemBase> CreateCommands()
        {
            return new ObservableCollection<CommandItemBase>
            {
                new CommandItem
                {
                    IsButton=true,
                    Action = Model. DoCheckFieldes,
                    Caption = "分析文本(Value Name Caption Description)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    IsButton=true,
                    Action = Model.DoFormatCSharp,
                    Caption = "分析文本(C#代码)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                }
            };
        }
    }
}