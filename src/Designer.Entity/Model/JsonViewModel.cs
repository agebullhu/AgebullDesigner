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
        /// 主面板
        /// </summary>
        public override FrameworkElement Body { get; } = new JsonPanel();
    }

    internal class JsonModel : EntityDesignModel
    {
        #region 操作命令

        public override ObservableCollection<CommandItem> CreateCommands()
        {
            return new ObservableCollection<CommandItem>
            {
                new CommandItem
                {
                    Command = new DelegateCommand(Format1 ),
                    Caption = "前后端名称一致(大驼峰名称)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Command = new DelegateCommand(Format2 ),
                    Caption = "小驼峰名称",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                },
                new CommandItem
                {
                    Command = new DelegateCommand(Format3 ),
                    Caption = "小写下划线名称(C风格)",
                    Image = Application.Current.Resources["tree_Assembly"] as ImageSource
                }
            };
        }
        #endregion

        #region 扩展代码

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