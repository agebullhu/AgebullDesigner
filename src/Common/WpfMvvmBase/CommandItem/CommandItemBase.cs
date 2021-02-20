// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-11-26
// 修改:2014-12-07
// *****************************************************/

#region 引用

using Agebull.EntityModel;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     表示一个命令节点
    /// </summary>
    public abstract class CommandItemBase : CommandConfig
    {
        #region 状态

        string AutoIcon(string name)
        {
            if(name == null)
                return "Icon";
            if (name.Contains("新增") || name.Contains("增加"))
                return "新增";
            if (name.Contains("清除") || name.Contains("删除"))
                return "删除";
            if (name.Contains("排序"))
                return "排序";
            return "Icon";
        }

        /// <summary>
        ///     图标
        /// </summary>
        public string Image
        {
            get => IsRoot
                ? null
                : IconMap.Instance[IconName ?? AutoIcon(Caption)];
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
        #endregion

        #region 参数
        /// <summary>
        /// 参数
        /// </summary>
        public object Source { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public object Parameter => Source;
        #endregion

        #region 命令

        private ICommand _command;


        /// <summary>
        ///     对应的命令
        /// </summary>
        public ICommand Command
        {
            get => _command;
            protected set
            {
                _command = value;
                if (value is INotifyPropertyChanged pp)
                    pp.PropertyChanged += OnCommandPropertyChanged;
            }
        }

        protected void OnCommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(sender is IStatusCommand cmd))
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
        /// <summary>
        /// 执行
        /// </summary>
        public abstract void Execute(object arg);
        /// <summary>
        /// 准备动作
        /// </summary>
        public Func<CommandItemBase, bool> OnPrepare { get; set; }

        #endregion

        #region 子级

        /// <summary>
        /// 所有按钮
        /// </summary>
        public NotificationList<CommandItemBase> Items { get; set; } = new NotificationList<CommandItemBase>();

        #endregion

        #region 辅助

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public static void ShowMessageBox(string title, string message)
        {
            WorkContext.SynchronousContext.InvokeInUiThread(() => MessageBox.Show(message, title));
        }
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void ShowMessageBox(string message)
        {
            WorkContext.SynchronousContext.InvokeInUiThread(() => MessageBox.Show(message, Caption ?? Name));
        }
        #endregion
    }

}