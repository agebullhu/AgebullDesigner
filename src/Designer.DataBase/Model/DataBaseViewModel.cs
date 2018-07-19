using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Designer;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.Common.Config.Designer.DataBase.Mysql
{
    internal class DataBaseViewModel : ExtendViewModelBase<DataBaseModel>
    {
        public DataBaseViewModel()
        {
            EditorName = "DataBase";
        }

    }
    internal class DataBaseModel : EntityDesignModel
    {
        #region ��������

        public override ObservableCollection<CommandItemBase> CreateCommands()
        {
            return new ObservableCollection<CommandItemBase>
            {
                new CommandItem
                {
                    Action = (Format1 ),
                    IsButton=true,
                    Caption = "���շ�����",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Action = (Format2 ),
                    IsButton=true,
                    Caption = "С�շ�����",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Action = (Format3 ),
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
                property.ColumnName = property.Name;
            }
        }
        internal void Format2(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.ColumnName = property.Name.ToLWord();
            }
        }
        internal void Format3(object arg)
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.ColumnName = CoderBase.ToLinkWordName(property.Name, "_", false);
            }
        }
        #endregion
    }
}