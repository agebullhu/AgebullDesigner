// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-11-26
// 修改:2014-12-07
// *****************************************************/

#region 引用

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Agebull.EntityModel;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     表示一个命令节点
    /// </summary>
    public class CommandItem : CommandConfig
    {
        #region 设置

        /// <summary>
        ///     图标
        /// </summary>
        ImageSource _image;

        /// <summary>
        ///     图标
        /// </summary>
        public ImageSource Image
        {
            get => IsRoot
                ? null
                : _image ?? (_image = IconName == null ? null : Application.Current.Resources[IconName] as ImageSource);
            set => _image = value;
        }

        /// <summary>
        ///     是否根
        /// </summary>
        public bool IsRoot { get; set; }

        /// <summary>
        ///     是否线
        /// </summary>
        public bool IsLine { get; set; }

        /// <summary>
        /// 表示分隔线
        /// </summary>
        public static CommandItem Line { get; } = new CommandItem
        {
            IsLine = true
        };


        private bool _isChecked;

        /// <summary>
        ///     是否选中
        /// </summary>
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                RaisePropertyChanged(nameof(IsChecked));
            }
        }

        public object Source { get; set; }
        #endregion

        #region 参数

        /// <summary>
        ///     命令的目标类型
        /// </summary>
        public Type TargetType => SourceType;


        /// <summary>
        ///     对应的命令参数
        /// </summary>
        public object Parameter { get; set; }

        #endregion

        #region 状态

        private bool _isBusy;

        /// <summary>
        ///     图标
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy == value)
                    return;
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        private Visibility _visibility;

        /// <summary>
        ///     可见
        /// </summary>
        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                if (_visibility == value)
                    return;
                _visibility = value;
                RaisePropertyChanged(() => Visibility);
            }
        }

        private void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var cmd = Command as IStatusCommand;
            if (cmd == null)
                return;
            switch (e.PropertyName)
            {
                case "IsBusy":
                    IsBusy = cmd.IsBusy;
                    break;
                case "Visibility":
                    Visibility = cmd.Visibility;
                    break;
            }
        }

        #endregion

        #region 命令

        private ICommand _command;

        /// <summary>
        ///     对应的命令
        /// </summary>
        public ICommand Command
        {
            get => _command;
            set
            {
                _command = value;
                if (value is INotifyPropertyChanged pp)
                    pp.PropertyChanged += OnCommandPropertyChanged;
            }
        }

        #endregion

        #region 子级

        /// <summary>
        /// 所有按钮
        /// </summary>
        public ObservableCollection<CommandItem> Items { get; set; } = new ObservableCollection<CommandItem>();

        #endregion
    }


    /// <summary>
    ///     表示一个命令节点
    /// </summary>
    public class CommandItem<TTargetType> : CommandConfig
    {
        /// <summary>
        /// 构造
        /// </summary>
        public CommandItem()
        {
            SourceType = typeof(TTargetType);
        }
    }
}