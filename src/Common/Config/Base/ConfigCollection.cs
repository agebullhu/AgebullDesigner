using Agebull.EntityModel.Config;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Agebull.EntityModel
{
    /// <summary>
    ///     表示一个自动存储的列表对象
    /// </summary>
    public class ConfigCollection<TConfig> : NotificationList<TConfig>
        where TConfig : ConfigBase, IChildrenConfig, new()
    {
        public ConfigBase Parent { get; set; }

        public ConfigCollection()
        {

        }

        public ConfigCollection(ConfigBase parent)
        {
            Parent = parent;
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="TDest"></typeparam>
        /// <returns></returns>
        public void DoForeach<T>(Action<T> action, bool doAction)
        {
            foreach (var item in this)
            {
                item.Foreach(action, doAction);
            }
        }


        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="TDest"></typeparam>
        /// <returns></returns>
        public void OnLoad(ConfigBase parent)
        {
            Parent = parent;
            foreach (var item in this)
            {
                item.Parent = parent;
                item.OnLoad();
            }
        }

        #region 属性修改

        /// <summary>
        ///     通过提供的参数引发 <see cref="E:System.Collections.ObjectModel.NotificationList`1.CollectionChanged" /> 事件。
        /// </summary>
        /// <param name="e">要引发事件的自变量。</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (WorkContext.InLoding)
                return;
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                GlobalTrigger.OnLoad(Parent);
                return;
            }
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.OfType<TConfig>())
                {
                    if (Parent != null)
                        item.Parent = Parent;
                    GlobalTrigger.OnAdded(Parent, item);
                    item.OnLoad();
                }
            }
            if (e.OldItems == null)
                return;
            foreach (var item in e.OldItems)
                GlobalTrigger.OnRemoved(Parent, item);
            base.OnCollectionChanged(e);
        }

        /// <summary>
        ///     发出属性修改事件
        /// </summary>
        /// <param name="action">属性字段</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            OnPropertyChanged(GetPropertyName(action));
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
        ///     发出属性修改事件
        /// </summary>
        /// <param name="propertyName">属性</param>
        public void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }


        /// <summary>
        ///     发出属性修改事件
        /// </summary>
        /// <param name="propertyName">属性</param>
        private void OnPropertyChanged(string propertyName)
        {
            InvokeInUiThread(OnPropertyChanged, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region 状态修改事件

        /// <summary>
        ///     发出状态变化事件
        /// </summary>
        /// <param name="action">属性字段</param>
        public void RaiseStatusChanged<T>(Expression<Func<T>> action)
        {
            RaiseStatusChanged(GetPropertyName(action));
        }

        /// <summary>
        ///     发出状态变化事件
        /// </summary>
        /// <param name="status">状态</param>
        public void RaiseStatusChanged(NotificationStatusType status)
        {
            InvokeInUiThread(RaiseStatusChangedInner, new PropertyChangedEventArgs(status.ToString()));
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
                Trace.WriteLine(ex, "ObjectCollection.RaiseStatusChangedInner");
                throw;
            }
        }


        #endregion
        #region UI线程同步支持

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

        #endregion
    }
}