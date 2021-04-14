using Agebull.EntityModel.Config;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Agebull.EntityModel
{
    /// <summary>
    ///     表示一个自动存储的列表对象
    /// </summary>
    public class ConfigCollection<TConfig> : NotificationList<TConfig>, IModifyObject
        where TConfig : ConfigBase, IChildrenConfig
    {
        #region 构造

        [JsonIgnore]
        public ModifyRecord ValueRecords { get; }

        public ConfigCollection()
        {
            ValueRecords = new ModifyRecord
            {
                Me = this
            };
        }

        public ConfigCollection(ConfigBase parent, string name = null)
        {
            Name = name;
            Parent = parent;
            Parent.ValueRecords.Add(Name, this);
        }

        #endregion

        #region 子级同步

        public string Name { get; set; }
        public ConfigBase Parent { get; set; }

        private void OnRemove(TConfig item)
        {
            item.IsModifyChanged -= OnItemIsModifyChanged;

            haseNewOrRemove = true;
            CheckModify();
            GlobalTrigger.OnRemoved(Parent, item);
            GlobalConfig.RemoveConfig(item.Option);
        }


        private void OnAdd(TConfig item)
        {
            haseNewOrRemove = true;
            GlobalConfig.AddConfig(item.Option);
            item.Index = Count == 0 ? 1 : this.Max(p => p.Index) + 1;
            item.Parent = Parent;
            item.IsModifyChanged += OnItemIsModifyChanged;
            item.OnLoad();
            item.SetIsNew();
            CheckModify();

            if (Parent == null)
                return;
            GlobalTrigger.OnAdded(Parent, item);
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="TDest"></typeparam>
        /// <returns></returns>
        public void ForeachChild<T>(Action<T> action, bool doAction)
            where T : class
        {
            if (doAction && this is T t)
            {
                action(t);
            }
            foreach (var item in this)
            {
                item.Foreach(action, doAction);
            }
        }

        /// <summary>
        /// 向下遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="preorder">true:先根遍历 false:后根遍历</param>
        public void DoForeachDown<T>(Action<T> action, bool preorder)
            where T : class
        {
            if (preorder && this is T t1)
                action(t1);
            foreach (var item in this)
            {
                item.Foreach(action, preorder);
            }
            if (!preorder && this is T t2)
                action(t2);
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <returns></returns>
        public void OnLoad(string name, ConfigBase parent)
        {
            Name = name;
            Parent = parent;
            foreach (var item in this.ToArray())
            {
                item.Parent = parent;
                item.IsModifyChanged += OnItemIsModifyChanged;
                item.OnLoad();
            }
            Parent.ValueRecords.Add(Name, this);
        }


        #endregion

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
                haseNewOrRemove = true;
                GlobalTrigger.OnLoad(Parent);
                CheckModify();
            }
            else
            {
                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems.OfType<TConfig>())
                    {
                        OnAdd(item);
                    }
                }
                if (e.OldItems != null)
                {
                    foreach (var item in e.OldItems.OfType<TConfig>())
                    {
                        OnRemove(item);
                    }
                }
            }
            base.OnCollectionChanged(e);
        }

        #endregion

        #region 状态修改事件

        /// <summary>
        ///     冻结
        /// </summary>
        [JsonIgnore]
        public bool IsModify => modified > 0 || isNew || haseNewOrRemove;

        public void ResetModify(bool isSaved)
        {
            modified = 0;
            isNew = !isSaved;
            haseNewOrRemove = false;
            Parent.ValueRecords.Add(Name, false);
            if (WorkContext.InLoding)
                return;
            NotificationObject.InvokeInUiThread(RaiseStatusChangedInner, new IsModifyEventArgs(IsModify));
        }

        public void SetIsNew()
        {
            isNew = true;
            modified = Count;
            haseNewOrRemove = false;
            Parent.ValueRecords.Add(Name, false);
            if (WorkContext.InLoding)
                return;
            NotificationObject.InvokeInUiThread(RaiseStatusChangedInner, new IsModifyEventArgs(IsModify));
        }

        [JsonIgnore]
        int modified;
        [JsonIgnore]
        bool isNew;
        [JsonIgnore]
        bool haseNewOrRemove;

        public void CheckModify()
        {
            foreach (var item in this)
                item.CheckModify();

            modified = this.Count(p => p.IsModify);
            Parent.ValueRecords.Add(Name, IsModify);
            Parent.CheckModify();
            //NotificationObject.InvokeInUiThread(RaiseStatusChangedInner, new IsModifyEventArgs(IsModify));
        }

        private void OnItemIsModifyChanged(object sender, IsModifyEventArgs e)
        {
            CheckModify();
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