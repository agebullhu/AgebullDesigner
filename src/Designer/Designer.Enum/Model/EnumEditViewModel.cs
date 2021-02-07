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
                Caption = "�����ı�(Value Name Caption Description)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                IsButton = true,
                Action = Model.DoFormatCSharp,
                Caption = "�����ı�(C#����)",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            });
            base.CreateCommands(commands);
        }
    }
}