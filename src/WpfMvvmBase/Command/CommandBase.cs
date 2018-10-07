using System;
using System.ComponentModel;
using System.Windows;
using Agebull.EntityModel;
using Agebull.Common.Logging;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     �������
    /// </summary>
    public class CommandBase : INotifyPropertyChanged, IStatus
    {
        #region �ܷ�ִ�д���

        private EventHandler _canExecuteChanged;

        private INotifyPropertyChanged _detect;

        /// <summary>
        ///     ����ִ��״̬�仯�Ķ���
        /// </summary>
        public INotifyPropertyChanged Detect
        {
            get => _detect;
            set
            {
                if (_detect != null)
                {
                    _detect.PropertyChanged += OnDetectPropertyChanged;
                }
                if (Equals(_detect, value))
                {
                    return;
                }
                _detect = value;
                value.PropertyChanged += OnDetectPropertyChanged;
                OnCanExecuteChanged();
            }
        }

        private void OnDetectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }
        
        /// <summary>
        ///     �����ܷ�ִ�е��¼�
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => _canExecuteChanged += value;
            remove => _canExecuteChanged -= value;
        }

        /// <summary>
        ///     ������ִ��״̬�仯����Ϣ
        /// </summary>
        protected void OnCanExecuteChanged()
        {
            if (_canExecuteChanged == null)
            {
                return;
            }
            InvokeInUiThread<object>(OnCanExecuteChangedInner, null);
        }

        /// <summary>
        ///     ������ִ��״̬�仯����Ϣ
        /// </summary>
        protected void OnCanExecuteChangedInner(object par)
        {
            try
            {
                _canExecuteChanged(this, par as  EventArgs);
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
            }
        }

        #endregion


        #region ״̬
        private Visibility _visibility;

        /// <inheritdoc />
        /// <summary>
        ///     ͼ��
        /// </summary>
        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                if (_visibility == value)
                {
                    return;
                }
                _visibility = value;
                RaisePropertyChanged(nameof(Visibility));
            }
        }

        private CommandStatus _status;

        /// <summary>
        ///     ��ǰ״̬
        /// </summary>
        public CommandStatus Status
        {
            get => _status;
            set
            {
                if (_status == value)
                {
                    return;
                }
                _status = value;
                RaisePropertyChanged(nameof(Status));
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        /// <summary>
        ///     �Ƿ���æ
        /// </summary>
        public bool IsBusy => Status == CommandStatus.Executing;

        #endregion

        #region PropertyChanged

        /// <summary>
        /// ͬ��������
        /// </summary>
        public ISynchronousContext Synchronous
        {
            get;
            set;
        }


        /// <summary>
        ///     ��UI�߳���ִ��
        /// </summary>
        /// <param name="action"></param>
        /// <param name="args"></param>
        protected void InvokeInUiThread<T>(Action<T> action, T args)
        {
            if (Synchronous == null)
            {
                action(args);
            }
            else
            {
                Synchronous.BeginInvokeInUiThread(action, args);
            }
        }

        /// <summary>
        ///     �����޸��¼�(����Ϊ�ձ�ʾɾ��)
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                propertyChanged -= value;
                propertyChanged += value;
            }
            remove => propertyChanged -= value;
        }

        /// <summary>
        ///     �����޸��¼�
        /// </summary>
        private event PropertyChangedEventHandler propertyChanged;

        /// <summary>
        ///     ���������޸��¼�(������ֹģʽӰ��)
        /// </summary>
        /// <param name="propertyName">����</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (propertyChanged == null)
            {
                return;
            }
            InvokeInUiThread(RaisePropertyChangedInner, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     ���������޸��¼�
        /// </summary>
        /// <param name="args">����</param>
        private void RaisePropertyChangedInner(PropertyChangedEventArgs args)
        {
            try
            {
                propertyChanged?.Invoke(this, args);
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
            }
        }

        #endregion
    }
}