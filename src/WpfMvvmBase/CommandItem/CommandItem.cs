// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.Common.WpfMvvmBase
// ����:2014-11-26
// �޸�:2014-12-07
// *****************************************************/

#region ����

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     ��ʾһ������ڵ�
    /// </summary>
    public class CommandItem : CommandConfig
    {
        #region ����
        
        private ImageSource _image;

        /// <summary>
        ///     ͼ��
        /// </summary>
        public ImageSource Image
        {
            get { return _image ?? Application.Current.Resources[IconName ?? "imgDefault"] as BitmapImage; }
            set { _image = value; }
        }

        #endregion

        #region ����

        /// <summary>
        ///     ��ǩ
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        ///     ��Ӧ���������
        /// </summary>
        public object Parameter { get; set; }

        #endregion

        #region ״̬

        private bool _isBusy;

        /// <summary>
        ///     ͼ��
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
        ///     ͼ��
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

        #region ����

        private ICommand _command;

        /// <summary>
        ///     ��Ӧ������
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