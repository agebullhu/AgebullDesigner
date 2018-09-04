using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    internal class JsonViewModel : ExtendViewModelBase<JsonModel>
    {
        public JsonViewModel()
        {
            EditorName = "Json";
        }
    }

    internal class JsonModel : EntityDesignModel
    {
        #region ��������

        public override NotificationList<CommandItemBase> CreateCommands()
        {
            return new NotificationList<CommandItemBase>
            {
                new CommandItem
                {
                    Action = Format1,
                    IsButton=true,
                    Caption = "ǰ�������һ��(���շ�����)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Action = Format2,
                    IsButton=true,
                    Caption = "С�շ�����",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Action = Format3,
                    IsButton=true,
                    Caption = "Сд�»�������(C���)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                }
            };
        }
        #endregion

        #region ��չ����

        public void Format1(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.JsonName = null;
            }
        }
        internal void Format2(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.JsonName = property.Name.ToLWord();
            }
        }
        internal void Format3(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.JsonName = CoderBase.ToLinkWordName(property.Name, "_", false);
            }
        }
        #endregion
    }
}