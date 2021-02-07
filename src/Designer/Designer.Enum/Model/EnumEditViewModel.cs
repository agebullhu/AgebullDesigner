using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Agebull.Common;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    public sealed class EnumEditViewModel : EditorViewModelBase<EnumEditModel>
    {
        public override void CreateCommands(IList<CommandItemBase> commands)
        {
            commands.Append(new CommandItem
            {
                IsButton = true,
                Action = Model.DoCheckFieldes,
                Caption = "分析文本(Value Name Caption Description)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                IsButton = true,
                Action = Model.DoFormatCSharp,
                Caption = "分析文本(C#代码)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            });
            base.CreateCommands(commands);
        }
    }
}