// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-11-24
// 修改:2014-12-07
// *****************************************************/

#region 引用

using Agebull.EntityModel;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

#endregion

namespace Agebull.Common.Mvvm
{
    /// <summary>
    ///     异步命令
    /// </summary>
    /// <typeparam name="TParameter">参数类型</typeparam>
    /// <typeparam name="TResult">命令返回值</typeparam>
    public class AsyncCommand<TParameter, TResult> : CommandBase,IAsyncCommand
    {
        #region 能否执行处理

        private EventHandler _canExecuteChanged;

        /// <summary>
        ///     参数(不为空时忽略UI传过来的参数)
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


        /// <summary>
        /// 定义用于确定此命令是否可以在其当前状态下执行的方法。
        /// </summary>
        /// <returns>
        /// 如果可以执行此命令，则为 true；否则为 false。
        /// </returns>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
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
        ///     能否执行的方法
        /// </summary>
        private readonly Func<TParameter, bool> _canExecuteAction;


        /// <summary>
        ///     命令能否执行的事件
        /// </summary>
        event EventHandler ICommand.CanExecuteChanged
        {
            add => _canExecuteChanged += value;
            remove => _canExecuteChanged -= value;
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
                Trace.WriteLine(ex);
                return false;
            }
        }

        #endregion

        #region 构造


        /// <summary>
        ///     结束时执行的方法
        /// </summary>
        private readonly Action<CommandStatus, Exception, TResult> _endAction;

        /// <summary>
        ///     执行的方法
        /// </summary>
        private readonly Func<TParameter, TResult> _executeAction;

        /// <summary>
        ///     执行前的方法
        /// </summary>
        private readonly Func<TParameter, bool> _prepareAction1;

        /// <summary>
        ///     执行前的方法
        /// </summary>
        private readonly Func<TParameter, Action<TParameter>, bool> _prepareAction2;

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        public AsyncCommand(Func<TParameter, TResult> executeAction)
            : this(executeAction, p => true)
        {
        }
        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="canExecuteAction">能否执行的方法.</param>
        public AsyncCommand(Func<TParameter, TResult> executeAction, Func<TParameter, bool> canExecuteAction)
        {
            _canExecuteAction = canExecuteAction;
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction), @"命令方法不能为空");
        }
        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="canExecuteAction">能否执行的方法.</param>
        public AsyncCommand(Func<TParameter, TResult> executeAction,
            Action<CommandStatus, Exception, TResult> endAction,
            Func<TParameter, bool> canExecuteAction)
            : this(executeAction, canExecuteAction)
        {
            _endAction = endAction;
        }
        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="prepare">执行前的方法</param>
        public AsyncCommand(Func<TParameter, bool> prepare,
            Func<TParameter, TResult> executeAction,
            Action<CommandStatus, Exception, TResult> endAction)
            : this(executeAction)
        {
            _prepareAction1 = prepare;
            _endAction = endAction;
        }
        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="prepare">执行前的方法</param>
        public AsyncCommand(Func<TParameter, Action<TParameter>, bool> prepare,
                Func<TParameter, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction)
            : this(executeAction)
        {
            _prepareAction2 = prepare;
            _endAction = endAction;
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="prepare">执行前的方法</param>
        /// <param name="canExecuteAction">能否执行的方法.</param>
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
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="prepare">执行前的方法</param>
        /// <param name="canExecuteAction">能否执行的方法.</param>
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

        #region 执行命令


        private TParameter _parameter;

        private Task<TResult> _task;

        private CancellationToken _token;


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
        ///     执行命令
        /// </summary>
        /// <param name="parameter">界面传过来的参数</param>
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
                Trace.WriteLine(ex.Message);
                Status = CommandStatus.Faulted;
                OnCanExecuteChanged();
                _endAction?.Invoke(Status, ex, default);
            }
        }

        /// <summary>
        ///     执行命令
        /// </summary>
        /// <param name="parameter">界面传过来的参数</param>
        private TResult DoExecute(object parameter)
        {
            return _executeAction(!Equals(Parameter, default(TParameter)) ? Parameter : (TParameter)parameter);
        }

        private void OnEnd(Task<TResult> task)
        {
            _task = null;
            Status = task.Status switch
            {
                TaskStatus.Faulted => CommandStatus.Faulted,//Trace.WriteLine(task.Exception);
                _ => CommandStatus.Succeed,
            };
            try
            {
                if (_endAction == null)
                {
                    return;
                }
                if (Synchronous == null)
                {
                    _endAction(Status, task.IsFaulted ? task.Exception : null, !task.IsFaulted ? task.Result : default);
                }
                else
                {
                    Synchronous.BeginInvokeInUiThread(_endAction, Status, task.IsFaulted ? task.Exception : null, !task.IsFaulted ? task.Result : default);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
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
    ///     异步命令
    /// </summary>
    /// <typeparam name="TResult">命令返回值</typeparam>
    public sealed class AsyncCommand<TResult> : AsyncCommand<object, TResult>
    {

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        public AsyncCommand(Func<object, TResult> executeAction)
            : base(executeAction)
        {
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="canExecuteAction">能否执行的方法.</param>
        public AsyncCommand(Func<object, TResult> executeAction, Func<object, bool> canExecuteAction)
                : base(executeAction, canExecuteAction)
        {
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="canExecuteAction">能否执行的方法.</param>
        public AsyncCommand(Func<object, TResult> executeAction,
            Action<CommandStatus, Exception, TResult> endAction,
            Func<object, bool> canExecuteAction)
            : base(executeAction, endAction, canExecuteAction)
        {
        }
        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="prepare">执行前的方法</param>
        public AsyncCommand(Func<object, bool> prepare,
            Func<object, TResult> executeAction,
            Action<CommandStatus, Exception, TResult> endAction)
            : base(prepare, executeAction, endAction)
        {
        }
        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="prepare">执行前的方法</param>
        public AsyncCommand(Func<object, Action<object>, bool> prepare,
                Func<object, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction)
            : base(prepare, executeAction, endAction)
        {
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="prepare">执行前的方法</param>
        /// <param name="canExecuteAction">能否执行的方法.</param>
        public AsyncCommand(Func<object, bool> prepare,
                Func<object, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction,
                Func<object, bool> canExecuteAction)
            : base(prepare, executeAction, endAction, canExecuteAction)
        {
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="prepare">执行前的方法</param>
        /// <param name="canExecuteAction">能否执行的方法.</param>
        public AsyncCommand(Func<object, Action<object>, bool> prepare,
                Func<object, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction,
                Func<object, bool> canExecuteAction)
            : base(prepare, executeAction, endAction, canExecuteAction)
        {
        }

    }

    /// <summary>
    ///     异步命令
    /// </summary>
    public sealed class AsyncCommand : AsyncCommand<object, object>
    {


        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        public AsyncCommand(Func<object, object> executeAction)
            : base(executeAction)
        {
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="canExecuteAction">能否执行的方法.</param>
        public AsyncCommand(Func<object, object> executeAction, Func<object, bool> canExecuteAction)
            : base(executeAction, canExecuteAction)
        {
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="canExecuteAction">能否执行的方法.</param>
        public AsyncCommand(Func<object, object> executeAction,
            Action<CommandStatus, Exception, object> endAction,
            Func<object, bool> canExecuteAction)
            : base(executeAction, endAction, canExecuteAction)
        {
        }
        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="prepare">执行前的方法</param>
        public AsyncCommand(Func<object, bool> prepare,
            Func<object, object> executeAction,
            Action<CommandStatus, Exception, object> endAction)
            : base(prepare, executeAction, endAction)
        {
        }
        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="prepare">执行前的方法</param>
        public AsyncCommand(Func<object, Action<object>, bool> prepare,
                Func<object, object> executeAction,
                Action<CommandStatus, Exception, object> endAction)
            : base(prepare, executeAction, endAction)
        {
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="prepare">执行前的方法</param>
        /// <param name="canExecuteAction">能否执行的方法.</param>
        public AsyncCommand(Func<object, bool> prepare,
                Func<object, object> executeAction,
                Action<CommandStatus, Exception, object> endAction,
                Func<object, bool> canExecuteAction)
            : base(prepare, executeAction, endAction, canExecuteAction)
        {
        }

        /// <summary>
        ///     构造
        /// </summary>
        /// <param name="executeAction">命令执行的方法.</param>
        /// <param name="endAction">结束时执行的方法</param>
        /// <param name="prepare">执行前的方法</param>
        /// <param name="canExecuteAction">能否执行的方法.</param>
        public AsyncCommand(Func<object, Action<object>, bool> prepare,
                Func<object, object> executeAction,
                Action<CommandStatus, Exception, object> endAction,
                Func<object, bool> canExecuteAction)
            : base(prepare, executeAction, endAction, canExecuteAction)
        {
        }
    }
}
