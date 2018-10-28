// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.Common.WpfMvvmBase
// ����:2014-11-24
// �޸�:2014-12-07
// *****************************************************/

#region ����

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Agebull.EntityModel;
using Agebull.Common.Logging;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     �첽����
    /// </summary>
    /// <typeparam name="TParameter">��������</typeparam>
    /// <typeparam name="TResult">�����ֵ</typeparam>
    public class AsyncCommand<TParameter, TResult> : IAsyncCommand
    {
        #region �ܷ�ִ�д���

        private EventHandler _canExecuteChanged;

        /// <summary>
        ///     ����(��Ϊ��ʱ����UI�������Ĳ���)
        /// </summary>
        public TParameter Parameter
        {
            get => _parameter;
            set
            {
                if (Equals(_parameter, value))
                {
                    return;
                }
                _parameter = value;
                Detect = value as INotifyPropertyChanged;
                CanExecute(value);
            }
        }
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
        /// ��������ȷ���������Ƿ�������䵱ǰ״̬��ִ�еķ�����
        /// </summary>
        /// <returns>
        /// �������ִ�д������Ϊ true������Ϊ false��
        /// </returns>
        /// <param name="parameter">������ʹ�õ����ݡ�����������Ҫ�������ݣ���ö����������Ϊ null��</param>
        bool ICommand.CanExecute(object parameter)
        {
            try
            {
                return CanExecute(!Equals(Parameter, default(TParameter)) ? Parameter : (TParameter)parameter);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e, "CanExecute");
                return false;
            }
        }

        /// <summary>
        ///     �ܷ�ִ�еķ���
        /// </summary>
        private readonly Func<TParameter, bool> _canExecuteAction;


        /// <summary>
        ///     �����ܷ�ִ�е��¼�
        /// </summary>
        event EventHandler ICommand.CanExecuteChanged
        {
            add => _canExecuteChanged += value;
            remove => _canExecuteChanged -= value;
        }


        /// <summary>
        ///     ��UI�߳���ִ��
        /// </summary>
        /// <param name="action"></param>
        /// <param name="args"></param>
        private void InvokeInUiThread<T>(Action<T> action, T args)
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
                var onPropertyChanged = propertyChanged;
                onPropertyChanged?.Invoke(this, args);
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
            }
        }

        /// <summary>
        ///     Determines if the command can execute with the provided parameter by invoing the <see cref="Func{T,TResult}" />
        ///     supplied during construction.
        /// </summary>
        /// <param name="parameter">The parameter to use when determining if this command can execute.</param>
        /// <returns>Returns <see langword="true" /> if the command can execute.  <see langword="False" /> otherwise.</returns>
        private bool CanExecute(TParameter parameter)
        {
            try
            {
                return !IsBusy && (_canExecuteAction == null || _canExecuteAction(parameter));
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
                return false;
            }
        }

        /// <summary>
        ///     ������ִ��״̬�仯����Ϣ
        /// </summary>
        private void OnCanExecuteChanged()
        {
            if (_canExecuteChanged == null)
            {
                return;
            }
            InvokeInUiThread(OnCanExecuteChangedInner, this);
        }

        /// <summary>
        ///     ������ִ��״̬�仯����Ϣ
        /// </summary>
        private void OnCanExecuteChangedInner(object par)
        {
            try
            {
                _canExecuteChanged(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ͬ��������
        /// </summary>
        public ISynchronousContext Synchronous => WorkContext.SynchronousContext;

        private Visibility _visibility;

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

        /// <summary>
        ///     ����ʱִ�еķ���
        /// </summary>
        private readonly Action<CommandStatus, Exception, TResult> _endAction;

        /// <summary>
        ///     ִ�еķ���
        /// </summary>
        private readonly Func<TParameter, TResult> _executeAction;

        /// <summary>
        ///     ִ��ǰ�ķ���
        /// </summary>
        private readonly Func<TParameter, bool> _prepareAction1;

        /// <summary>
        ///     ִ��ǰ�ķ���
        /// </summary>
        private readonly Func<TParameter, Action<TParameter>, bool> _prepareAction2;

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        public AsyncCommand(Func<TParameter, TResult> executeAction)
            : this(executeAction, p => true)
        {
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        public AsyncCommand(Func<TParameter, TResult> executeAction, Func<TParameter, bool> canExecuteAction)
        {
            _canExecuteAction = canExecuteAction;
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction), @"���������Ϊ��");
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        public AsyncCommand(Func<TParameter, TResult> executeAction,
            Action<CommandStatus, Exception, TResult> endAction,
            Func<TParameter, bool> canExecuteAction)
            : this(executeAction, canExecuteAction)
        {
            _endAction = endAction;
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        public AsyncCommand(Func<TParameter, bool> prepare,
            Func<TParameter, TResult> executeAction,
            Action<CommandStatus, Exception, TResult> endAction)
            : this(executeAction)
        {
            _prepareAction1 = prepare;
            _endAction = endAction;
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        public AsyncCommand(Func<TParameter, Action<TParameter>, bool> prepare,
                Func<TParameter, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction)
            : this(executeAction)
        {
            _prepareAction2 = prepare;
            _endAction = endAction;
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        public AsyncCommand(Func<TParameter, bool> prepare,
                Func<TParameter, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction,
                Func<TParameter, bool> canExecuteAction)
            : this(executeAction, canExecuteAction)
        {
            _prepareAction1 = prepare;
            _endAction = endAction;
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        public AsyncCommand(Func<TParameter, Action<TParameter>, bool> prepare,
                Func<TParameter, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction,
                Func<TParameter, bool> canExecuteAction)
            : this(executeAction, canExecuteAction)
        {
            _prepareAction2 = prepare;
            _endAction = endAction;
        }

        #endregion

        #region ִ������


        private TParameter _parameter;

        private CommandStatus _status;

        private Task<TResult> _task;

        private CancellationToken _token;

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

        void ICommand.Execute(object parameter)
        {
            try
            {
                Execute(!Equals(Parameter, default(TParameter)) ? Parameter : (TParameter)parameter);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e, "Execute");
                throw;
            }
        }

        /// <summary>
        ///     ִ������
        /// </summary>
        /// <param name="parameter">���洫�����Ĳ���</param>
        public void Execute(TParameter parameter)
        {
            try
            {
                if (!CanExecute(parameter))
                {
                    return;
                }
                if (_prepareAction1 != null && !_prepareAction1(parameter))
                {
                    return;
                }
                if (_prepareAction2 != null && !_prepareAction2(parameter, p => parameter = p))
                {
                    return;
                }
                Status = CommandStatus.Executing;
                OnCanExecuteChanged();
                _token = new CancellationToken(false);
                _task = new Task<TResult>(DoExecute, parameter, _token);
                _task.ContinueWith(OnEnd, TaskContinuationOptions.None);
                _task.Start();
            }
            catch (Exception ex)
            {
                LogRecorder.Error(ex.Message);
                Status = CommandStatus.Faulted;
                OnCanExecuteChanged();
                _endAction?.Invoke(Status, ex, default(TResult));
            }
        }

        /// <summary>
        ///     ִ������
        /// </summary>
        /// <param name="parameter">���洫�����Ĳ���</param>
        private TResult DoExecute(object parameter)
        {
            return _executeAction(!Equals(Parameter, default(TParameter)) ? Parameter : (TParameter)parameter);
        }

        private void OnEnd(Task<TResult> task)
        {
            _task = null;
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                    //LogRecorder.Exception(task.Exception);
                    Status = CommandStatus.Faulted;
                    break;
                default:
                    Status = CommandStatus.Succeed;
                    break;
            }
            try
            {
                if (_endAction == null)
                {
                    return;
                }
                if (Synchronous == null)
                {
                    _endAction(Status, task.IsFaulted ? task.Exception : null, !task.IsFaulted ? task.Result : default(TResult));
                }
                else
                {
                    Synchronous.BeginInvokeInUiThread(_endAction, Status, task.IsFaulted ? task.Exception : null, !task.IsFaulted ? task.Result : default(TResult));
                }
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
                Status = CommandStatus.Faulted;
            }
            finally
            {
                OnCanExecuteChanged();
            }
        }

        #endregion
    }

    /// <summary>
    ///     �첽����
    /// </summary>
    /// <typeparam name="TResult">�����ֵ</typeparam>
    public sealed class AsyncCommand<TResult> : AsyncCommand<object, TResult>
    {

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        public AsyncCommand(Func<object, TResult> executeAction)
            : base(executeAction)
        {
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        public AsyncCommand(Func<object, TResult> executeAction, Func<object, bool> canExecuteAction)
                : base(executeAction, canExecuteAction)
        {
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        public AsyncCommand(Func<object, TResult> executeAction,
            Action<CommandStatus, Exception, TResult> endAction,
            Func<object, bool> canExecuteAction)
            : base(executeAction, endAction, canExecuteAction)
        {
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        public AsyncCommand(Func<object, bool> prepare,
            Func<object, TResult> executeAction,
            Action<CommandStatus, Exception, TResult> endAction)
            : base(prepare, executeAction, endAction)
        {
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        public AsyncCommand(Func<object, Action<object>, bool> prepare,
                Func<object, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction)
            : base(prepare, executeAction, endAction)
        {
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        public AsyncCommand(Func<object, bool> prepare,
                Func<object, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction,
                Func<object, bool> canExecuteAction)
            : base(prepare, executeAction, endAction, canExecuteAction)
        {
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        public AsyncCommand(Func<object, Action<object>, bool> prepare,
                Func<object, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction,
                Func<object, bool> canExecuteAction)
            : base(prepare, executeAction, endAction, canExecuteAction)
        {
        }

    }

    /// <summary>
    ///     �첽����
    /// </summary>
    public sealed class AsyncCommand : AsyncCommand<object, object>
    {


        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        public AsyncCommand(Func<object, object> executeAction)
            : base(executeAction)
        {
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        public AsyncCommand(Func<object, object> executeAction, Func<object, bool> canExecuteAction)
            : base(executeAction, canExecuteAction)
        {
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        public AsyncCommand(Func<object, object> executeAction,
            Action<CommandStatus, Exception, object> endAction,
            Func<object, bool> canExecuteAction)
            : base(executeAction, endAction, canExecuteAction)
        {
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        public AsyncCommand(Func<object, bool> prepare,
            Func<object, object> executeAction,
            Action<CommandStatus, Exception, object> endAction)
            : base(prepare, executeAction, endAction)
        {
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        public AsyncCommand(Func<object, Action<object>, bool> prepare,
                Func<object, object> executeAction,
                Action<CommandStatus, Exception, object> endAction)
            : base(prepare, executeAction, endAction)
        {
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        public AsyncCommand(Func<object, bool> prepare,
                Func<object, object> executeAction,
                Action<CommandStatus, Exception, object> endAction,
                Func<object, bool> canExecuteAction)
            : base(prepare, executeAction, endAction, canExecuteAction)
        {
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        public AsyncCommand(Func<object, Action<object>, bool> prepare,
                Func<object, object> executeAction,
                Action<CommandStatus, Exception, object> endAction,
                Func<object, bool> canExecuteAction)
            : base(prepare, executeAction, endAction, canExecuteAction)
        {
        }
    }
}
