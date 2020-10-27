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
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel.Designer
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
            get => _config;
            set
            {
                if (_config == value)
                    return;
                _config = value;
                RaisePropertyChanged(nameof(Config));
            }
        }

        public CommandItemBase CancelCommand => new CommandItem
        {
            Action = DoCancel,
            Caption = "取消",
            Image = Application.Current.Resources["img_error"] as ImageSource
        };

        public CommandItemBase OkCommand => new CommandItem
        {
            Action = DoClose,
            Caption = "完成",
            Image = Application.Current.Resources["imgSave"] as ImageSource
        };

        private void DoCancel(object arg)
        {
            var window = (Window)View;
            window.DialogResult = false;
        }

        private void DoClose(object arg)
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