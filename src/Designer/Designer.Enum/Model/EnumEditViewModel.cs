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
                Caption = "分析文本(Value Name Caption Description)",
                IconName = "分析"
            },
            new SimpleCommandItem
            {
                IsButton = true,
                Action = Model.DoFormatCSharp,
                Caption = "分析文本(C#代码)",
                IconName = "C#"
            });
            base.CreateCommands(commands);
        }
    }
}