using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Agebull.Common.Logging;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel
{


    /// <summary>
    /// ��ʾTask��ʾ�첽�������
    /// </summary>
    public class StepTackProxy : ITackProxy
    {
        /// <summary>
        /// ��ʾTask��ʾ�첽��������һ��ִ��ʵ��
        /// </summary>
        private sealed class TackStepItem
        {

            /// <summary>
            /// �����ʶ,����첽ִ�н�����ʶ��ͬ,���ִ�н����������
            /// </summary>
            public Guid StepKey
            {
                get;
                set;
            }


            /// <summary>
            /// ִ�еķ���
            /// </summary>
            public Action Action
            {
                get;
                set;
            }

        }

        /// <summary>
        ///     ͬ�������̵߳�����
        /// </summary>
        public Dispatcher Dispatcher
        {
            get;
            set;
        }

        /// <summary>
        /// �����ʶ,����첽ִ�н�����ʶ��ͬ,���ִ�н����������
        /// </summary>
        public Guid StepKey
        {
            get;
            set;
        }

        /// <summary>
        ///     ��ǰ״̬
        /// </summary>
        public CommandStatus Status
        {
            get;
            set;
        }


        /// <summary>
        ///     ����ִ�з���
        /// </summary>
        public Action Action
        {
            get;
            set;
        }

        /// <summary>
        /// �ܷ�ִ��
        /// </summary>
        /// <returns>null��ʾȡ������,treu��ʾ��������ִ��,false��ʾӦ�õȴ�ǰһ��������ɺ�ִ��</returns>
        public bool? CanDo()
        {
            return true;
        }

        private Task _task;

        private CancellationToken _token;

        public void Run(Action task)
        {
            try
            {
                Status = CommandStatus.Executing;
                _token = new CancellationToken(false);
                _task = new Task(DoExecute, new TackStepItem { StepKey = StepKey, Action = task }, _token);
                _task.ContinueWith(OnEnd, TaskContinuationOptions.None);
                _task.Start();
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
                Status = CommandStatus.Faulted;
                Exist();
            }
        }
        private void DoExecute(object arg)
        {
            lock (this)
            {
                var item = arg as TackStepItem;
                // ReSharper disable PossibleNullReferenceException
                if (item.StepKey == StepKey)
                    item.Action();
                // ReSharper restore PossibleNullReferenceException
            }
        }

        private void OnEnd(Task task)
        {
            lock (this)
            {
                // ReSharper disable PossibleNullReferenceException
                var item = task.AsyncState as TackStepItem;
                if (item.StepKey != StepKey)
                    return;
                // ReSharper restore PossibleNullReferenceException
                _task = null;
                switch (task.Status)
                {
                    case TaskStatus.Faulted:
                        LogRecorder.Exception(task.Exception);
                        Status = CommandStatus.Faulted;
                        break;
                    default:
                        Status = CommandStatus.Succeed;
                        break;
                }
                Dispatcher.BeginInvoke(new Action(Exist));
            }
        }

        public void Exist()
        {
            if (Action != null)
                Action();
        }
    }



    /// <summary>
    /// ��ʾTask��ʾ�첽�������(��ִ����ɵĲ����ʶ����ͬʱִ�н����������)
    /// </summary>
    public sealed class StepTackProxy<TResult> : ITackProxy<TResult>
    {
        /// <summary>
        /// ��ʾTask��ʾ�첽��������һ��ִ��ʵ��
        /// </summary>
        private sealed class TackStepItem
        {

            /// <summary>
            /// �����ʶ,����첽ִ�н�����ʶ��ͬ,���ִ�н����������
            /// </summary>
            public Guid StepKey
            {
                get;
                set;
            }


            /// <summary>
            /// ִ�еķ���
            /// </summary>
            public Func<TResult> Action
            {
                get;
                set;
            }


        }

        /// <summary>
        ///     ͬ�������̵߳�����
        /// </summary>
        public Dispatcher Dispatcher
        {
            get;
            set;
        }

        /// <summary>
        /// �����ʶ,����첽ִ�н�����ʶ��ͬ,���ִ�н����������
        /// </summary>
        public Guid StepKey
        {
            get;
            set;
        }

        /// <summary>
        ///     ��ǰ״̬
        /// </summary>
        public CommandStatus Status
        {
            get;
            set;
        }

        /// <summary>
        ///     ����ִ�з���
        /// </summary>
        public Action<TResult> Action
        {
            get;
            set;
        }

        public void Run(Func<TResult> func)
        {
            try
            {
                Status = CommandStatus.Executing;
                var task = new Task<TResult>(DoExecute, new TackStepItem { StepKey = StepKey, Action = func });
                task.ContinueWith(OnEnd, TaskContinuationOptions.None);
                task.Start();
            }
            catch (Exception ex)
            {
                LogRecorder.Exception(ex);
                Status = CommandStatus.Faulted;
                Exist();
            }
        }
        private TResult DoExecute(object arg)
        {
            lock (this)
            {
                var item = arg as TackStepItem;
                // ReSharper disable PossibleNullReferenceException
                return item.StepKey != StepKey ? default(TResult) : item.Action();
                // ReSharper restore PossibleNullReferenceException
            }
        }
        private void OnEnd(Task<TResult> task)
        {
            // ReSharper disable PossibleNullReferenceException
            lock (this)
            {
                var item = task.AsyncState as TackStepItem;
                if (item.StepKey != StepKey)
                    return;
                switch (task.Status)
                {
                    case TaskStatus.Faulted:
                        LogRecorder.Exception(task.Exception);
                        Status = CommandStatus.Faulted;
                        break;
                    default:
                        Status = CommandStatus.Succeed;
                        break;
                }
                Dispatcher.BeginInvoke(new Action<TResult>(Exist), task.Result);
            }
            // ReSharper restore PossibleNullReferenceException
        }
        /// <summary>
        /// ִ�����
        /// </summary>
        /// <param name="result"></param>
        public void Exist(TResult result = default(TResult ))
        {
            if (Action != null)
                Action(result);
        }
    }
}