using System;
using System.Windows.Input;
using System.Windows.Threading;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel
{
    /// <summary>
    /// MVVMģʽ�Ļ���
    /// </summary>
    public abstract class MvvmBase : NotificationObject
    {
        /// <summary>
        /// ͬ��������
        /// </summary>
        public virtual ISynchronousContext Synchronous
        {
            get;
            set;
        }
        /// <summary>
        /// �̵߳�����
        /// </summary>
        public virtual Dispatcher Dispatcher
        {
            get;
            set;
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        protected ICommand CreateAsyncCommand<TParameter, TResult>(Func<TParameter, TResult> executeAction)
            where TParameter : class
        {
            return new AsyncCommand<TParameter, TResult>(executeAction);
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        protected ICommand CreateAsyncCommand<TParameter, TResult>(Func<TParameter, TResult> executeAction, 
            Func<TParameter, bool> canExecuteAction)
            where TParameter : class
        {
            return new AsyncCommand<TParameter, TResult>(executeAction,canExecuteAction);
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        protected ICommand CreateAsyncCommand<TParameter, TResult>(Func<TParameter, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction,
                Func<TParameter, bool> canExecuteAction)
            where TParameter : class
        {
            return new AsyncCommand<TParameter, TResult>(executeAction,endAction,canExecuteAction);
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        protected ICommand CreateAsyncCommand<TParameter, TResult>(Func<TParameter, bool> prepare,
                Func<TParameter, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction)
            where TParameter : class
        {
            return new AsyncCommand<TParameter, TResult>(prepare,executeAction,endAction);
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        protected ICommand CreateAsyncCommand<TParameter, TResult>(Func<TParameter, Action<TParameter>, bool> prepare,
                Func<TParameter, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction)
            where TParameter : class
        {
            return new AsyncCommand<TParameter, TResult>(prepare, executeAction, endAction);
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        protected ICommand CreateAsyncCommand<TParameter, TResult>(Func<TParameter, bool> prepare,
                Func<TParameter, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction,
                Func<TParameter, bool> canExecuteAction)
            where TParameter : class
        {
            return new AsyncCommand<TParameter, TResult>(prepare,executeAction,endAction,canExecuteAction);
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="executeAction">����ִ�еķ���.</param>
        /// <param name="endAction">����ʱִ�еķ���</param>
        /// <param name="prepare">ִ��ǰ�ķ���</param>
        /// <param name="canExecuteAction">�ܷ�ִ�еķ���.</param>
        protected ICommand CreateAsyncCommand<TParameter, TResult>(Func<TParameter, Action<TParameter>, bool> prepare,
                Func<TParameter, TResult> executeAction,
                Action<CommandStatus, Exception, TResult> endAction,
                Func<TParameter, bool> canExecuteAction)
            where TParameter : class
        {
            return new AsyncCommand<TParameter, TResult>(prepare,executeAction,endAction,canExecuteAction);
        }
    }
}