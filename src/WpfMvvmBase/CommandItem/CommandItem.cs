// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.Common.WpfMvvmBase
// ����:2014-11-26
// �޸�:2014-12-07
// *****************************************************/

#region ����

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
    ///     ��ʾһ������ڵ�
    /// </summary>
    public class CommandItem : CommandConfig
    {
        #region ����

        /// <summary>
        ///     ͼ��
        /// </summary>
        ImageSource _image;

        /// <summary>
        ///     ͼ��
        /// </summary>
        public ImageSource Image
        {
            get => IsRoot
                ? null
                : _image ?? (_image = IconName == null ? null : Application.Current.Resources[IconName] as ImageSource);
            set => _image = value;
        }

        /// <summary>
        ///     �Ƿ��
        /// </summary>
        public bool IsRoot { get; set; }

        /// <summary>
        ///     �Ƿ���
        /// </summary>
        public bool IsLine { get; set; }

        /// <summary>
        /// ��ʾ�ָ���
        /// </summary>
        public static CommandItem Line { get; } = new CommandItem
        {
            IsLine = true
        };


        private bool _isChecked;

        /// <summary>
        ///     �Ƿ�ѡ��
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

        #region ����

        /// <summary>
        ///     �����Ŀ������
        /// </summary>
        public Type TargetType => SourceType;


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
        ///     �ɼ�
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

        #region ����

        private ICommand _command;

        /// <summary>
        ///     ��Ӧ������
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

        #region �Ӽ�

        /// <summary>
        /// ���а�ť
        /// </summary>
        public ObservableCollection<CommandItem> Items { get; set; } = new ObservableCollection<CommandItem>();

        #endregion
    }


    /// <summary>
    ///     ��ʾһ������ڵ�
    /// </summary>
    public class CommandItem<TTargetType> : CommandConfig
    {
        /// <summary>
        /// ����
        /// </summary>
        public CommandItem()
        {
            SourceType = typeof(TTargetType);
        }
    }
}