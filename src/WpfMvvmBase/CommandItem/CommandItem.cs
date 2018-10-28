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
    public abstract class CommandItemBase : CommandConfig
    {

        #region ״̬

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
                : _image ?? (_image = Application.Current.Resources[IconName ?? "imgDefault"] as ImageSource);
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
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public object Source { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public object Parameter => Source;
        #endregion

        #region ����

        private ICommand _command;


        /// <summary>
        ///     ��Ӧ������
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
        /// ִ��
        /// </summary>
        public abstract void Execute(object arg);
        /// <summary>
        /// ׼������
        /// </summary>
        public Func<CommandItemBase, bool> OnPrepare { get; set; }

        #endregion

        #region �Ӽ�

        /// <summary>
        /// ���а�ť
        /// </summary>
        public NotificationList<CommandItemBase> Items { get; set; } = new NotificationList<CommandItemBase>();

        #endregion

    }


    /// <summary>
    ///     ��ʾһ������ڵ�
    /// </summary>
    public class CommandItem : CommandItemBase
    {
        #region ����

        public CommandItem()
        {
            SignleSoruce = true;
            Command = new DelegateCommand<object>(DoAction);
        }

        /// <summary>
        ///     ��Ӧ������
        /// </summary>
        public Action<object> Action
        {
            get;
            set;
        }


        void DoAction(object arg)
        {
            if (DoConfirm && MessageBox.Show(ConfirmMessage ?? $"ȷ��ִ�С�{Caption ?? Name}��������?", "����༭", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            if (OnPrepare == null || OnPrepare(this))
                Action?.Invoke(arg);
        }
        /// <summary>
        /// ִ��
        /// </summary>
        public override void Execute(object arg)
        {
            Action?.Invoke(arg);
        }
        #endregion
    }

    /// <summary>
    ///     ��ʾһ������ڵ�
    /// </summary>
    public class CommandItem<TArgument> : CommandItemBase
        where TArgument : class
    {
        #region ����

        public CommandItem()
        {
            SignleSoruce = true;
            Command = new DelegateCommand(DoAction);
        }

        /// <summary>
        ///     ��Ӧ������
        /// </summary>
        public Action<TArgument> Action
        {
            get;
            set;
        }


        /// <summary>
        ///     ��Ӧ������
        /// </summary>
        public TArgument Argument => Source as TArgument;

        void DoAction()
        {
            if (!string.IsNullOrWhiteSpace(ConfirmMessage) && MessageBox.Show(ConfirmMessage, "����༭", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }
            if (OnPrepare == null || OnPrepare(this))
                Action?.Invoke(Argument);
        }
        /// <summary>
        /// ִ��
        /// </summary>
        public override void Execute(object arg)
        {
            Action?.Invoke(arg as TArgument);
        }
        #endregion
    }
}