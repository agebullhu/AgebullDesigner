using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Agebull.Common;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    internal class JsonViewModel : ExtendViewModelBase<JsonModel>
    {
        public JsonViewModel()
        {
            EditorName = "Json";
        }
        /// <summary>
        /// �����
        /// </summary>
        public override FrameworkElement Body { get; } = new JsonPanel();
    }

    internal class JsonModel : EntityDesignModel
    {
        #region ��������

        public override ObservableCollection<CommandItem> CreateCommands()
        {
            return new ObservableCollection<CommandItem>
            {
                new CommandItem
                {
                    Command = new DelegateCommand(Format1 ),
                    Caption = "ǰ�������һ��(���շ�����)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Command = new DelegateCommand(Format2 ),
                    Caption = "С�շ�����",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Command = new DelegateCommand(Format3 ),
                    Caption = "Сд�»�������(C���)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                }
            };
        }
        #endregion

        #region ��չ����

        public void Format1()
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.JsonName = null;
            }
        }
        internal void Format2()
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.JsonName = property.Name.ToLWord();
            }
        }
        internal void Format3()
        {
            foreach (var property in Context.SelectEntity.Properties)
            {
                property.JsonName = CoderBase.ToLinkWordName(property.Name, "_", false);
            }
        }
        #endregion
    }
}