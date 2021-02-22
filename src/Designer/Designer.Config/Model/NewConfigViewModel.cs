// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze.Application
// 建立:2014-11-20
// 修改:2014-11-27
// *****************************************************/

#region 引用

using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System.Windows;
using System.Windows.Media;

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
        CommandItemBase cancelCommand;
        public CommandItemBase CancelCommand => cancelCommand ??= new CommandItem
        {
            Action = DoCancel,
            Caption = "取消",
            IconName = "取消"
        };
        CommandItemBase okCommand;
        public CommandItemBase OkCommand => okCommand??= new CommandItem
        {
            Action = DoClose,
            Caption = "完成",
            IconName  = "完成"
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