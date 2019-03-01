using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 扩展节点
    /// </summary>
    public class ExtendItem : NotificationObject
    {
        public ConfigBase Config { get; set; }
        
        public string Name { get; set; }

        public string Value
        {
            get { return Config[Name]; }
            set
            {
                if (value == Config[Name])
                {
                    return;
                }
                Config[Name] = value;
                RaisePropertyChanged(nameof(value));
            }
        }
    }
    /// <summary>
    /// 扩展属性模型
    /// </summary>
    public class ExtendConfigModel : DesignModelBase
    {
        public void DeleteEnum(ConfigTreeItem<PropertyConfig> p)
        {
            p.Model.CustomType = null;
            if (p.Model.EnumConfig == null)
                return;
            p.Model.EnumConfig.IsDelete = true;
            p.Model.EnumConfig = null;
            p.ReShow();
        }
        /// <summary>
        /// 扩展属性
        /// </summary>
        public ObservableCollection<ExtendItem> ExtendItems { get; private set; }

        /// <summary>
        /// 上下文属性变化
        /// </summary>
        protected override void OnContextPropertyChanged(string name)
        {
            switch (name)
            {
                case "SelectConfig":
                    UpdateItems();
                    break;
            }
        }

        private void UpdateItems()
        {
            if (Context.SelectConfig == null)
            {
                ExtendItems = new ConfigCollection<ExtendItem>();
            }
            else
            {
                var items = new ConfigCollection<ExtendItem>();
                var keys = Context.SelectConfig.ExtendConfig.Select(p => p.Name).DistinctBy();
                items.AddRange(keys.Select(p => new ExtendItem
                {
                    Name = p,
                    Config = Context.SelectConfig
                }));
                ExtendItems = items;
            }
            RaisePropertyChanged(nameof(ExtendItems));
        }

        protected override void CreateCommands(List<CommandItem> commands)
        {
            commands.Add(new CommandItem
            {
                Command = new DelegateCommand(Add),
                Name = "增加",
                Image = Application.Current.Resources["tree_item"] as ImageSource
            });
        }
        public string NewName { get; set; }

        private void Add()
        {
            if (Context.SelectConfig == null)
                return;
            if (Context.SelectConfig == null)
            {
                MessageBox.Show("添加", "当前配置不可用");
                return;
            }
            if (string.IsNullOrWhiteSpace(NewName))
            {
                MessageBox.Show("添加", "新名称不能为空");
                return;
            }
            if (Context.SelectConfig.ExtendConfig.Any(p => string.Equals(p.Name, NewName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("添加", "已存在同名内容");
                return;
            }
            ExtendItems.Add(new ExtendItem
            {
                Name = NewName,
                Config = Context.SelectConfig
            });
        }
    }
}
