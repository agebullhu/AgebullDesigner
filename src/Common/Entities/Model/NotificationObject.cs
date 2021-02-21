// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-12-09
// 修改:2014-12-09
// *****************************************************/

#region 引用

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

#endregion

namespace Agebull.EntityModel
{

    /// <summary>
    ///     有属性通知的对象
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract class NotificationObject : INotifyPropertyChanged, IModifyObject
    {
        #region 构造

        /// <summary>
        /// 构造
        /// </summary>
        protected NotificationObject()
        {
            ValueRecords  = new ModifyRecord { Me = this };
            //GlobalTrigger.OnCreate(this);
        }

        #endregion

        #region 属性修改通知

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
        ///     发出属性修改事件
        /// </summary>
        /// <param name="action">属性字段</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            RaisePropertyChanged(GetPropertyName(action));
        }

        /// <summary>
        ///     发出属性修改事件
        /// </summary>
        /// <param name="propertyName">属性</param>
        public void RaisePropertyChanged(string propertyName)
        {
            RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     发出属性修改事件(不受阻止模式影响)
        /// </summary>
        /// <param name="args">属性</param>
        protected void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            if (WorkContext.IsNoChangedNotify)
                return;
            InvokeInUiThread(RaisePropertyChangedInner, args);
        }

        private string preName;
        /// <summary>
        ///     发出属性修改事件(不受阻止模式影响)
        /// </summary>
        /// <param name="args">属性</param>
        protected void RaisePropertyChangedInner(PropertyChangedEventArgs args)
        {
            if (WorkContext.IsNoChangedNotify || preName == args.PropertyName)
                return;
            preName = args.PropertyName;
            try
            {
                propertyChanged?.Invoke(this, args);
                GlobalTrigger.OnPropertyChanged(this, args.PropertyName);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, "NotificationObject.RaisePropertyChangedInner");
            }
            finally
            {
                preName = null;
            }
        }

        /// <param name="action">属性字段</param>
        protected static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            return expression.Member.Name;
        }


        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="propertyName">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        public void BeforePropertyChange(string propertyName, object oldValue, object newValue)
        {
            if (WorkContext.InLoding || WorkContext.InSaving)
                return;
            ValueRecords.Record(propertyName, oldValue, newValue);
            GlobalTrigger.BeforePropertyChange(this, propertyName, oldValue, newValue);
        }

        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="propertyName">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected void BeforePropertyChange(string propertyName, string oldValue, string newValue)
        {
            if (WorkContext.InLoding || WorkContext.InSaving)
                return;
            ValueRecords.Record(propertyName, oldValue, newValue);
            GlobalTrigger.BeforePropertyChange(this, propertyName, oldValue, newValue);
        }

        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="propertyName">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected void BeforePropertyChange(string propertyName, bool oldValue, bool newValue)
        {
            if (WorkContext.InLoding || WorkContext.InSaving)
                return;
            ValueRecords.Record(propertyName, oldValue, newValue);
            GlobalTrigger.BeforePropertyChange(this, propertyName, oldValue, newValue);
        }

        /// <summary>
        ///     发出属性修改事件
        /// </summary>
        /// <param name="propertyName">属性</param>
        public void OnPropertyChanged(string propertyName)
        {
            if (WorkContext.InLoding)
                return;
            RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));
            CheckModify();
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
        public static void InvokeInUiThread<T>(Action<T> action, T args)
        {
            try
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
            catch
            {
            }
        }


        #endregion

        #region 修改事件

        [JsonIgnore]
        public ModifyRecord ValueRecords { get; }

        [JsonIgnore]
        protected bool _isModify;
        /// <summary>
        ///     冻结
        /// </summary>
        [JsonIgnore, Category("系统"), DisplayName("已修改")]
        public bool IsModify => _isModify;

        public virtual void ResetModify(bool isSaved)
        {
            _isModify = false;
            ValueRecords.Reset(isSaved);
            InvokeInUiThread(RaiseStatusChangedInner, new IsModifyEventArgs(IsModify));
        }

        public virtual void SetIsNew()
        {
            _isModify = true;
            ValueRecords.SetIsNew();
            InvokeInUiThread(RaiseStatusChangedInner, new IsModifyEventArgs(IsModify));
        }

        public virtual void CheckModify()
        {
            ValueRecords.Check();
            var md = ValueRecords.IsModify;
            if (md != _isModify)
            {
                _isModify = md;
                InvokeInUiThread(RaiseStatusChangedInner, new IsModifyEventArgs(IsModify));
            }
        }

        /// <summary>
        ///     状态变化事件
        /// </summary>
        private event IsModifyEventHandler statusChanged;

        /// <summary>
        ///     状态变化事件
        /// </summary>
        public event IsModifyEventHandler IsModifyChanged
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
        private void RaiseStatusChangedInner(IsModifyEventArgs args)
        {
            if (WorkContext.InLoding)
                return;
            try
            {
                statusChanged?.Invoke(this, args);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, "NotificationObject.RaiseStatusChangedInner");
                throw;
            }
        }

        #endregion
    }

}
