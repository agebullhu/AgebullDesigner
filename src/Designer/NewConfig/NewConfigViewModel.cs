// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Agebull.Common.DataModel;
using Gboxt.Common.DataAccess.Schemas;
using Gboxt.Common.WpfMvvmBase.Commands;

#endregion

namespace Agebull.CodeRefactor.SolutionManager
{
    /// <summary>
    /// 新增配置
    /// </summary>
    public sealed class NewConfigViewModel : ViewModelBase<NewConfigModel>
    {
        private ConfigBase _config;

        /// <summary>
        /// 生成的表格对象
        /// </summary>
        public ConfigBase Config
        {
            get { return _config; }
            set
            {
                if (_config == value)
                    return;
                if (value.Key == Guid.Empty)
                    value.Key = Guid.NewGuid();
                _config = value;
                RaisePropertyChanged(nameof(Config));
            }
        }


        /// <summary>
        /// 构造命令列表
        /// </summary>
        /// <returns></returns>
        protected override ObservableCollection<CommandItem> CreateCommands()
        {
            return new ObservableCollection<CommandItem>
            {
                new CommandItem
                {
                    Command = new DelegateCommand(DoInitKey),
                    Name = "更新标识",
                    Image = Application.Current.Resources["img_add"] as ImageSource
                },
                new CommandItem
                {
                    Command = new DelegateCommand(DoCancel),
                    Name = "取消",
                    Image = Application.Current.Resources["img_error"] as ImageSource
                },
                new CommandItem
                {
                    Command = new DelegateCommand(DoClose),
                    Name = "完成",
                    Image = Application.Current.Resources["imgSave"] as ImageSource
                }
            };
        }

        private void DoCancel()
        {
            var window = (Window)View;
            window.DialogResult = false;
        }

        private void DoInitKey()
        {
            Config.Key = Guid.NewGuid();
        }

        private void DoClose()
        {
            if (string.IsNullOrWhiteSpace(Config.Name))
            {
                MessageBox.Show("对象名称必填,请检查");
                return;
            }
            var window = (Window)View;
            window.DialogResult = true;
        }
    }
}