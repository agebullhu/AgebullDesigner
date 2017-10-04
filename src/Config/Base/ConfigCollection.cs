using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Agebull.EntityModel
{
    /// <summary>
    ///     ��ʾһ���Զ��洢���б����
    /// </summary>
    public class ConfigCollection<TConfig> : ObservableCollection<TConfig> 
        where TConfig : NotificationObject, new()
    {
        #region �����޸�

        /// <summary>
        ///     ���������޸��¼�
        /// </summary>
        /// <param name="action">�����ֶ�</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            OnPropertyChanged(GetPropertyName(action));
        }

        /// <param name="action">�����ֶ�</param>
        protected static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            return expression.Member.Name;
        }
        /// <summary>
        ///     ���������޸��¼�
        /// </summary>
        /// <param name="action">�����ֶ�</param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            OnPropertyChanged(GetPropertyName(action));
        }


        /// <summary>
        ///     ���������޸��¼�
        /// </summary>
        /// <param name="propertyName">����</param>
        public void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }


        /// <summary>
        ///     ���������޸��¼�
        /// </summary>
        /// <param name="propertyName">����</param>
        private void OnPropertyChanged(string propertyName)
        {
            InvokeInUiThread(OnPropertyChanged, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region ״̬�޸��¼�

        /// <summary>
        ///     ����״̬�仯�¼�
        /// </summary>
        /// <param name="action">�����ֶ�</param>
        public void RaiseStatusChanged<T>(Expression<Func<T>> action)
        {
            RaiseStatusChanged(GetPropertyName(action));
        }

        /// <summary>
        ///     ����״̬�仯�¼�
        /// </summary>
        /// <param name="status">״̬</param>
        public void RaiseStatusChanged(NotificationStatusType status)
        {
            InvokeInUiThread(RaiseStatusChangedInner, new PropertyChangedEventArgs(status.ToString()));
        }

        /// <summary>
        ///     ����״̬�仯�¼�
        /// </summary>
        /// <param name="statusName">״̬</param>
        public void RaiseStatusChanged(string statusName)
        {
            InvokeInUiThread(RaiseStatusChangedInner, new PropertyChangedEventArgs(statusName));
        }
        /// <summary>
        ///     ״̬�仯�¼�
        /// </summary>
        private event PropertyChangedEventHandler statusChanged;

        /// <summary>
        ///     ״̬�仯�¼�
        /// </summary>
        public event PropertyChangedEventHandler StatusChanged
        {
            add
            {
                statusChanged -= value;
                statusChanged += value;
            }
            remove
            {
                statusChanged -= value;
            }
        }
        /// <summary>
        ///     ����״̬�仯�¼�
        /// </summary>
        /// <param name="args">����</param>
        private void RaiseStatusChangedInner(PropertyChangedEventArgs args)
        {
            if (statusChanged == null)
            {
                return;
            }
            try
            {
                statusChanged(this, args);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, "ObjectCollection.RaiseStatusChangedInner");
                throw;
            }
        }


        #endregion
        #region UI�߳�ͬ��֧��

        /// <summary>
        ///     ��UI�߳���ִ��
        /// </summary>
        /// <param name="action"></param>
        public void InvokeInUiThread(Action action)
        {
            if (WorkContext.SynchronousContext == null)
            {
                action();
            }
            else
            {
                WorkContext.SynchronousContext.InvokeInUiThread(action);
            }
        }

        /// <summary>
        ///     ��UI�߳���ִ��
        /// </summary>
        /// <param name="action"></param>
        public void BeginInvokeInUiThread(Action action)
        {
            if (WorkContext.SynchronousContext==null)
            {
                action();
            }
            else
            {
                WorkContext.SynchronousContext.BeginInvokeInUiThread(action);
            }
        }

        /// <summary>
        ///     ��UI�߳���ִ��
        /// </summary>
        /// <param name="action"></param>
        /// <param name="args"></param>
        public void BeginInvokeInUiThread<T>(Action<T> action, T args)
        {
            if (WorkContext.SynchronousContext==null)
            {
                action(args);
            }
            else
            {
                WorkContext.SynchronousContext.BeginInvokeInUiThread(action, args);
            }
        }

        /// <summary>
        ///     ��UI�߳���ִ��
        /// </summary>
        /// <param name="action"></param>
        /// <param name="args"></param>
        public void InvokeInUiThread<T>(Action<T> action, T args)
        {
            if (WorkContext.SynchronousContext==null)
            {
                action(args);
            }
            else
            {
                WorkContext.SynchronousContext.InvokeInUiThread(action, args);
            }
        }

        #endregion
    }
}