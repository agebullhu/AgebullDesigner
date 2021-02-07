// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.Common.WpfMvvmBase
// 建立:2014-12-09
// 修改:2014-12-09
// *****************************************************/

#region 引用

using Agebull.EntityModel.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;

#endregion

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     有属性通知的对象
    /// </summary>
    public class ProjectChildList<TParent, TConfig> : NotificationList<ProjectConfig, TConfig>
            where TParent : ConfigBase
            where TConfig : ConfigBase, IChildrenConfig
    {
    }

    /// <summary>
    ///     有属性通知的对象
    /// </summary>
    public class NotificationList<TParent, TConfig> : Collection<TConfig>, INotifyCollectionChanged, INotifyPropertyChanged, IChildrenConfig
            where TParent : ConfigBase
            where TConfig : ConfigBase, IChildrenConfig
    {
        #region 构造

        private const string IndexerName = "Item[]";

        /// <summary>
        ///     初始化 <see cref="TConfig:System.Collections.ObjectModel.NotificationList`1" /> 类的新实例，该类包含从指定列表中复制的元素。
        /// </summary>
        public NotificationList()
        {
        }

        /// <summary>
        ///     初始化 <see cref="TConfig:System.Collections.ObjectModel.NotificationList`1" /> 类的新实例，该类包含从指定列表中复制的元素。
        /// </summary>
        /// <param name="list">从中复制元素的列表。</param>
        /// <exception cref="TConfig:System.ArgumentNullException">
        ///     <paramref name="list" /> 参数不能为 <see langword="null" />。
        /// </exception>
        public NotificationList(IList<TConfig> list)
            : base(list)
        {
            CopyFrom(list);
        }

        /// <summary>
        ///     初始化 <see cref="TConfig:System.Collections.ObjectModel.NotificationList`1" /> 类的新实例，该类包含从指定集合中复制的元素。
        /// </summary>
        /// <param name="collection">从中复制元素的集合。</param>
        /// <exception cref="TConfig:System.ArgumentNullException">
        ///     <paramref name="collection" /> 参数不能为 <see langword="null" />。
        /// </exception>
        public NotificationList(IEnumerable<TConfig> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            CopyFrom(collection);
        }

        #endregion

        #region 线程完全

        private readonly SimpleMonitor _monitor = new SimpleMonitor();


        /// <summary>不允许可重入的更改此集合的尝试。</summary>
        /// <returns>
        ///     可用于释放对象的 <see cref="TConfig:System.IDisposable" /> 对象。
        /// </returns>
        protected IDisposable BlockReentrancy()
        {
            _monitor.Enter();
            return _monitor;
        }

        /// <summary>检查企图更改此集合的可重入尝试。</summary>
        /// <exception cref="TConfig:System.InvalidOperationException">
        ///     如果调用了 <see cref="M:System.Collections.ObjectModel.NotificationList`1.BlockReentrancy" />，其中尚未释放
        ///     <see cref="TConfig:System.IDisposable" /> 返回值。
        ///     通常情况下，这意味着在发生 <see cref="E:System.Collections.ObjectModel.NotificationList`1.CollectionChanged" /> 事件时更多次尝试更改此集合。
        ///     但是，这取决于派生的类何时选择调用 <see cref="M:System.Collections.ObjectModel.NotificationList`1.BlockReentrancy" />。
        /// </exception>
        protected void CheckReentrancy()
        {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            if (_monitor.Busy && CollectionChanged != null && CollectionChanged.GetInvocationList().Length > 1)
                throw new InvalidOperationException("ObservableCollectionReentrancyNotAllowed");
        }


        [Serializable]
        private class SimpleMonitor : IDisposable
        {
            private int _busyCount;

            public bool Busy => _busyCount > 0;

            public void Dispose()
            {
                --_busyCount;
            }

            public void Enter()
            {
                ++_busyCount;
            }
        }


        #endregion

        #region 扩展方法

        private void CopyFrom(IEnumerable<TConfig> collection)
        {
            if (collection == null)
                return;
            foreach (var obj in collection)
                Add(obj);
        }

        public void AddRange(IEnumerable<TConfig> collection)
        {
            if (collection == null)
                return;
            foreach (var obj in collection)
                Add(obj);
        }

        /// <summary>
        ///    在集合中加入
        /// </summary>
        /// <param name="item">节点</param>
        /// <returns>false表示之前已存在集合中，true表示新加入集合</returns>
        public void DoAdd(TConfig item)
        {
            base.Add(item);
        }

        /// <summary>
        ///     在集合中加入
        /// </summary>
        /// <param name="item">节点</param>
        /// <returns>false表示之前已存在集合中，true表示新加入集合</returns>
        public new void Add(TConfig item)
        {
            base.Add(item);
            item.Parent = Parent;
            item.OnLoad();
        }

        /// <summary>
        ///     如果不在集合中就加入
        /// </summary>
        /// <param name="item">节点</param>
        /// <returns>false表示之前已存在集合中，true表示新加入集合</returns>
        public bool TryAdd(TConfig item)
        {
            if (Contains(item))
                return false;
            Add(item);
            return true;
        }

        /// <summary>将指定索引处的项移动到集合中的新位置。</summary>
        /// <param name="oldIndex">从零开始的索引，指定项的新位置。</param>
        /// <param name="newIndex">从零开始的索引，指定项的新位置。</param>
        public void Move(int oldIndex, int newIndex)
        {
            if (oldIndex == newIndex)
                return;
            if (oldIndex >= 0 && oldIndex < Count && newIndex >= 0 && newIndex < Count)
                throw new ArgumentException("oldIndex或newIndex都应该大等于0且小于总数");
            MoveItem(oldIndex, newIndex);
        }

        #endregion

        #region 集合动作源

        /// <summary>从集合中移除所有项。</summary>
        protected override void ClearItems()
        {
            CheckReentrancy();
            base.ClearItems();
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionReset();
        }

        /// <summary>删除集合内指定索引处的项。</summary>
        /// <param name="index">要移除的元素的从零开始的索引。</param>
        protected override void RemoveItem(int index)
        {
            CheckReentrancy();
            var obj = this[index];
            base.RemoveItem(index);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, obj, index);
        }

        /// <summary>将一项插入集合中指定索引处。</summary>
        /// <param name="index">
        ///     应插入 <paramref name="item" /> 的从零开始的索引。
        /// </param>
        /// <param name="item">要插入的对象。</param>
        protected override void InsertItem(int index, TConfig item)
        {
            CheckReentrancy();
            base.InsertItem(index, item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        /// <summary>替换指定索引处的元素。</summary>
        /// <param name="index">待替换元素的从零开始的索引。</param>
        /// <param name="item">位于指定索引处的元素的新值。</param>
        protected override void SetItem(int index, TConfig item)
        {
            CheckReentrancy();
            var obj = this[index];
            base.SetItem(index, item);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Replace, obj, item, index);
        }

        /// <summary>将指定索引处的项移动到集合中的新位置。</summary>
        /// <param name="oldIndex">从零开始的索引，指定项的新位置。</param>
        /// <param name="newIndex">从零开始的索引，指定项的新位置。</param>
        protected virtual void MoveItem(int oldIndex, int newIndex)
        {
            CheckReentrancy();
            var obj = this[oldIndex];
            base.RemoveItem(oldIndex);
            base.InsertItem(newIndex, obj);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(NotifyCollectionChangedAction.Move, obj, newIndex, oldIndex);
        }

        #endregion

        #region 集合动作通知



        /// <summary>当添加、移除、变更、移动了某个项时，或当刷新了整个列表时发生。</summary>
        public virtual event NotifyCollectionChangedEventHandler CollectionChanged;


        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => PropertyChanged += value;

            remove => PropertyChanged -= value;
        }


        /// <summary>
        ///     通过提供的参数引发 <see cref="E:System.Collections.ObjectModel.NotificationList`1.PropertyChanged" /> 事件。
        /// </summary>
        /// <param name="e">要引发事件的参数。</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged == null)
                return;
            if (WorkContext.SynchronousContext == null)
                PropertyChanged.Invoke(this, e);
            else
                WorkContext.SynchronousContext.BeginInvokeInUiThread(() => { PropertyChanged.Invoke(this, e); });
        }

        /// <summary>在属性值更改时发生。</summary>
        protected virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     通过提供的参数引发 <see cref="E:System.Collections.ObjectModel.NotificationList`1.CollectionChanged" /> 事件。
        /// </summary>
        /// <param name="e">要引发事件的自变量。</param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged == null)
                return;
            if (WorkContext.SynchronousContext == null)
                CollectionChanged.Invoke(this, e);
            else
                WorkContext.SynchronousContext.BeginInvokeInUiThread(() => { CollectionChanged.Invoke(this, e); });
        }

        private void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem,
            int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }

        private void OnCollectionReset()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        #endregion

        #region 配置相关操作

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="TDest"></typeparam>
        /// <returns></returns>
        public void ActionForeach<TDest>(Action<TDest> action)
            where TDest : IConfigIterator
        {
            foreach (var item in this)
            {
                item.Foreach(action);
            }
        }
        ConfigBase IChildrenConfig.Parent { get => Parent; set => Parent = value as TParent; }

        /// <summary>
        /// 上级
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal TParent _parent;

        /// <summary>
        /// 上级
        /// </summary>
        /// <remark>
        /// 上级
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"上级"), Description("上级")]
        public TParent Parent
        {
            get;
            set;
        }
        #endregion

    }
}