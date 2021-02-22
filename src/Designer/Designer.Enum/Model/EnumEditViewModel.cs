using Agebull.Common.Mvvm;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Agebull.EntityModel.Designer
{
    public sealed class EnumEditViewModel : EditorViewModelBase<EnumEditModel>
    {
        public override void CreateCommands(IList<CommandItemBase> commands)
        {
            commands.Append(new SimpleCommandItem
            {
                IsButton = true,
                Action = Model.DoCheckFieldes,
                Caption = "�����ı�(Value Name Caption Description)",
                IconName = "����"
            },
            new SimpleCommandItem
            {
                IsButton = true,
                Action = Model.DoFormatCSharp,
                Caption = "�����ı�(C#����)",
                IconName = "C#"
            });
            base.CreateCommands(commands);
        }
    }
}