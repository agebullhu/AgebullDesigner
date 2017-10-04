// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-11-26
// 修改:2014-12-07
// *****************************************************/

#region 引用

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     表示一个命令节点
    /// </summary>
    public class CommandItem : CommandConfig
    {
        #region 设置
        
        private ImageSource _image;

        /// <summary>
        ///     图标
        /// </summary>
        public ImageSource Image
        {
            get { return _image ?? Application.Current.Resources[IconName ?? "imgDefault"] as BitmapImage; }
            set { _image = value; }
        }

        #endregion

        #region 参数

        /// <summary>
        ///     标签
        /// </summary>
        public object Tag { get; set; }

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
            get { return _isBusy; }
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
        ///     图标
        /// </summary>
        public Visibility Visibility
        {
            get { return _visibility; }
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
            get { return _command; }
            set
            {
                _command = value;
                var pp = value as INotifyPropertyChanged;
                if (pp != null)
                    pp.PropertyChanged += OnCommandPropertyChanged;
            }
        }

        #endregion
    }
}