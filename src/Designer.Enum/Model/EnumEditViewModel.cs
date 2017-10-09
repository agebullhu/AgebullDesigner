using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    public sealed class EnumEditViewModel : ViewModelBase<EnumEditModel>
    {

        private List<CommandItem> _exCommands;

        public IEnumerable<CommandItem> ExCommands => _exCommands ?? (_exCommands = new List<CommandItem>
        {
            new CommandItem
            {
                Command = new AsyncCommand<string, List<EnumItem>>
                    (Model.CheckFieldesPrepare, Model.DoCheckFieldes, Model.CheckFieldesEnd)
                {
                    Detect = Model
                },
                Name = "�����ı�",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            },
            new CommandItem
            {
                Command = new DelegateCommand(DoClose),
                Name = "���",
                Image = Application.Current.Resources["tree_Assembly"] as ImageSource
            }
        });

        private void DoClose()
        {
            if (string.IsNullOrWhiteSpace(Model.Config.Name) || Model.Config.Items.Count == 0)
            {
                MessageBox.Show("ʵ������Ϊ�ջ�û���ֶ�,����");
                return;
            }
            var window = (Window)View;
            window.DialogResult = true;
        }
    }
}