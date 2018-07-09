using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Designer.View;

namespace Agebull.EntityModel.Designer
{
    public sealed class EnumEditViewModel : ExtendViewModelBase<EnumEditModel>
    {
        /// <summary>
        /// �����
        /// </summary>
        public override FrameworkElement Body { get; } = new EnumEdit();


        protected override ObservableCollection<CommandItem> CreateCommands()
        {
            return new ObservableCollection<CommandItem>
            {
                new CommandItem
                {
                    Command = new DelegateCommand(Model. DoCheckFieldes),
                    Caption = "�����ı�",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                }
            };
        }
    }
}