using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Agebull.Common.Logging;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     �޲����ı�׼ί������
    /// </summary>
    public sealed class SimpleCommand<TArgument> : IStatusCommand
    {
        public TArgument Argument { get; set; }

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
            RaisePropertyChangedInner(new PropertyChangedEventArgs(propertyName));
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


        /// <summary>
        ///     ���ӻ�
        /// </summary>
        public Visibility Visibility => _canExecuteAction == null || _canExecuteAction() ? Visibility.Visible : Visibility.Collapsed;


        /// <summary>
        ///     ���executeAction�ܷ�ִ��״̬�ķ���
        /// </summary>
        private readonly Func<bool> _canExecuteAction;

        /// <summary>
        ///     �������巽��
        /// </summary>
        private readonly Action<TArgument> _executeAction;

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="argument">����</param>
        /// <param name="executeAction">�������巽��</param>
        public SimpleCommand(TArgument argument, Action<TArgument> executeAction)
            : this(argument, executeAction, () => true)
        {
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="argument">����</param>
        /// <param name="executeAction">�������巽��</param>
        /// <param name="canExecuteAction">���executeAction�ܷ�ִ��״̬�ķ���</param>
        public SimpleCommand(TArgument argument, Action<TArgument> executeAction, Func<bool> canExecuteAction)
        {
            Argument = argument;
            if (executeAction == null || canExecuteAction == null)
            {
                throw new ArgumentNullException(nameof(executeAction), @"���������Ϊ��");
            }
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="argument">����</param>
        /// <param name="executeAction">�������巽��</param>
        /// <param name="canExecuteAction">���executeAction�ܷ�ִ��״̬�ķ���</param>
        /// <param name="model">���仯�¼���ģ��</param>
        public SimpleCommand(TArgument argument, Action<TArgument> executeAction, INotifyPropertyChanged model, Func<bool> canExecuteAction)
            : this(argument, executeAction, canExecuteAction)
        {
            model.PropertyChanged += OnModelPropertyChanged;
        }

        /// <summary>
        ///     �Ƿ���æ
        /// </summary>
        public bool IsBusy => false;

        /// <summary>
        ///     ��ǰ״̬
        /// </summary>
        public CommandStatus Status => CommandStatus.None;

        /// <summary>
        ///     �����ڵ��ô�����ʱ���õķ�����
        /// </summary>
        /// <param name="parameter">������ʹ�õ����ݡ�����������Ҫ�������ݣ���ö����������Ϊ null��</param>
        void ICommand.Execute(object parameter)
        {
            _executeAction(Argument);
        }

        /// <summary>
        ///     ��ִ��״̬�仯���¼�
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     ��������ȷ���������Ƿ�������䵱ǰ״̬��ִ�еķ�����
        /// </summary>
        /// <returns>
        ///     �������ִ�д������Ϊ true������Ϊ false��
        /// </returns>
        /// <param name="parameter">������ʹ�õ����ݡ�����������Ҫ�������ݣ���ö����������Ϊ null��</param>
        bool ICommand.CanExecute(object parameter)
        {
            return _canExecuteAction == null || _canExecuteAction();
        }

        /// <summary>
        ///     ���ݰ󶨵�ģ�͸��¿�ִ��״̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(Visibility));
        }

        /// <summary>
        ///     ������ִ��״̬�仯���¼�
        /// </summary>
        private void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     ������ִ��״̬�仯���¼�
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }
    }
}