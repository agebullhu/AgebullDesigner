// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-12-09
// 修改:2014-12-09
// *****************************************************/

#region 引用

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

namespace Agebull.EntityModel
{
    /// <summary>
    ///     有属性通知的对象
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract class NotificationObject : INotifyPropertyChanged
    {
        #region 构造

        ///// <summary>
        ///// 构造
        ///// </summary>
        //protected NotificationObject()
        //{
        //    GlobalTrigger.OnCreate(this);
        //}

        #endregion

        #region 属性修改通知

#if CLIENT
        [IgnoreDataMember]
        private bool _isModify;
        /// <summary>
        ///     冻结
        /// </summary>
        [IgnoreDataMember, Category("系统"), DisplayName("已修改")]
        public bool IsModify
        {
            get { return _isModify; }
            set
            {
                _isModify = value;
                if (WorkContext.IsNoChangedNotify)
                    return;
#if CLIENT
                RaisePropertyChangedEventInner(nameof(IsModify));
#endif
                OnPropertyChangedInner(nameof(IsModify));
            }
        }

        /// <summary>
        ///     属性修改事件
        /// </summary>
        private event PropertyChangedEventHandler propertyChanged;
        /// <summary>
        ///     属性修改事件(属性为空表示删除)
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
        ///     发出属性修改事件(不受阻止模式影响)
        /// </summary>
        /// <param name="args">属性</param>
        private void RaisePropertyChangedInner(PropertyChangedEventArgs args)
        {
            try
            {
                propertyChanged?.Invoke(this, args);
                GlobalTrigger.OnPropertyChanged(this, args.PropertyName);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, "NotificationObject.RaisePropertyChangedInner");
            }
        }

        /// <summary>
        ///     发出属性修改事件
        /// </summary>
        /// <param name="propertyName">属性</param>
        protected virtual void RaisePropertyChangedEventInner(string propertyName)
        {
            InvokeInUiThread(RaisePropertyChangedInner, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        ///     发出属性修改事件
        /// </summary>
        /// <param name="action">属性字段</param>
        public void RaisePropertyChangedEvent<T>(Expression<Func<T>> action)
        {
            RaisePropertyChangedEventInner(GetPropertyName(action));
        }
        /// <summary>
        ///     发出属性修改事件
        /// </summary>
        /// <param name="propertyName">属性</param>
        public void RaisePropertyChangedEvent(string propertyName)
        {
            RaisePropertyChangedEventInner(propertyName);
        }
#endif
        /// <summary>
        ///     发出属性修改事件
        /// </summary>
        /// <param name="action">属性字段</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            OnPropertyChanged(GetPropertyName(action));
        }

        /// <summary>
        ///     发出属性修改事件
        /// </summary>
        /// <param name="propertyName">属性</param>
        public void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }

        /// <param name="action">属性字段</param>
        protected static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            return expression.Member.Name;
        }
        /// <summary>
        ///     发出属性修改事件
        /// </summary>
        /// <param name="action">属性字段</param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            OnPropertyChanged(GetPropertyName(action));
        }

        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="propertyName">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected void BeforePropertyChanged(string propertyName, object oldValue, object newValue)
        {
            if (WorkContext.IsNoChangedNotify)
                return;
            GlobalTrigger.BeforePropertyChanged(this, propertyName, oldValue, newValue);
        }
        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="propertyName">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected void BeforePropertyChanged(string propertyName, string oldValue, string newValue)
        {
            if (WorkContext.IsNoChangedNotify)
                return;
            GlobalTrigger.BeforePropertyChanged(this, propertyName, oldValue, newValue);
        }
        /// <summary>
        ///     发出属性修改事件
        /// </summary>
        /// <param name="propertyName">属性</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (WorkContext.IsNoChangedNotify)
                return;
            RecordModifiedInner(propertyName);
#if CLIENT
            RaisePropertyChangedEventInner(propertyName);
#endif
            OnPropertyChangedInner(propertyName);
            OnStatusChanged(NotificationStatusType.Modified);
            if (!IsModify && propertyName != nameof(IsModify))
            {
                IsModify = true;
            }
        }

        /// <summary>
        ///     记录属性修改
        /// </summary>
        /// <param name="propertyName">属性</param>
        protected virtual void RecordModifiedInner(string propertyName)
        {
        }

        /// <summary>
        ///     属性修改处理
        /// </summary>
        /// <param name="propertyName">属性</param>
        protected virtual void OnPropertyChangedInner(string propertyName)
        {
        }

        #endregion

        #region UI线程同步支持

#if CLIENT
        /// <summary>
        ///     在UI线程中执行
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
        ///     在UI线程中执行
        /// </summary>
        /// <param name="action"></param>
        public void BeginInvokeInUiThread(Action action)
        {
            if (WorkContext.SynchronousContext == null)
            {
                action();
            }
            else
            {
                WorkContext.SynchronousContext.BeginInvokeInUiThread(action);
            }
        }

        /// <summary>
        ///     在UI线程中执行
        /// </summary>
        /// <param name="action"></param>
        /// <param name="args"></param>
        public void BeginInvokeInUiThread<T>(Action<T> action, T args)
        {
            if (WorkContext.SynchronousContext == null)
            {
                action(args);
            }
            else
            {
                WorkContext.SynchronousContext.BeginInvokeInUiThread(action, args);
            }
        }

        /// <summary>
        ///     在UI线程中执行
        /// </summary>
        /// <param name="action"></param>
        /// <param name="args"></param>
        public void InvokeInUiThread<T>(Action<T> action, T args)
        {
            if (WorkContext.SynchronousContext == null)
            {
                action(args);
            }
            else
            {
                WorkContext.SynchronousContext.InvokeInUiThread(action, args);
            }
        }
#endif

        #endregion

        #region 状态修改事件

#if CLIENT
        /// <summary>
        ///     状态变化事件
        /// </summary>
        private event PropertyChangedEventHandler statusChanged;

        /// <summary>
        ///     状态变化事件
        /// </summary>
        public event PropertyChangedEventHandler StatusChanged
        {
            add
            {
                statusChanged -= value;
                statusChanged += value;
            }
            remove => statusChanged -= value;
        }

        /// <summary>
        ///     发出状态变化事件
        /// </summary>
        /// <param name="args">属性</param>
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
                Trace.WriteLine(ex, "NotificationObject.RaiseStatusChangedInner");
                throw;
            }
        }


        /// <summary>
        ///     发出状态变化事件
        /// </summary>
        /// <param name="statusName">状态</param>
        public void RaiseStatusChanged(string statusName)
        {
            InvokeInUiThread(RaiseStatusChangedInner, new PropertyChangedEventArgs(statusName));
        }

        /// <summary>
        ///     发出状态变化事件
        /// </summary>
        /// <param name="status">状态</param>
        public void RaiseStatusChanged(NotificationStatusType status)
        {
            RaiseStatusChanged(status.ToString());
        }

#endif

        /// <summary>
        ///     发出状态变化事件
        /// </summary>
        /// <param name="action">属性字段</param>
        public void OnStatusChanged<T>(Expression<Func<T>> action)
        {
            OnStatusChanged(GetPropertyName(action));
        }

        /// <summary>
        ///     发出状态变化事件
        /// </summary>
        /// <param name="status">状态</param>
        public void OnStatusChanged(NotificationStatusType status)
        {
            OnStatusChangedInner(status);
#if CLIENT
            RaiseStatusChanged(status);
#endif
        }

        /// <summary>
        ///     发出状态变化事件
        /// </summary>
        /// <param name="status">状态</param>
        public void OnStatusChanged(string status)
        {
            OnStatusChangedInner(status);
#if CLIENT
            RaiseStatusChanged(status);
#endif
        }


        /// <summary>
        ///    状态变化处理
        /// </summary>
        /// <param name="status">状态</param>
        protected virtual void OnStatusChangedInner(NotificationStatusType status)
        {

        }

        /// <summary>
        ///    状态变化处理
        /// </summary>
        /// <param name="status">状态</param>
        protected virtual void OnStatusChangedInner(string status)
        {

        }
        #endregion
    }
}
