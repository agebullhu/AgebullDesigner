// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Gboxt.Common.WpfMvvmBase
// 建立:2014-11-24
// 修改:2014-11-29
// *****************************************************/

#region 引用

using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using Agebull.Common.Reflection;
using Agebull.EntityModel.Config;

#endregion

namespace Agebull.EntityModel
{
    /// <summary>
    ///     表示一个树的节点
    /// </summary>
    public abstract class TreeItemBase : SimpleConfig
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }


        private bool _isUiSelected;

        private string _selectedPath;

        /// <summary>
        /// 是否选中
        /// </summary>
        protected bool isSelected;

        /// <summary>
        ///     构造
        /// </summary>
        protected TreeItemBase()
        {
            Items = new NotificationList<TreeItem>();
            Items.CollectionChanged += OnItemsCollectionChanged;
        }


        /// <summary>
        ///     层级
        /// </summary>
        public int Level
        {
            get
            {
                if (Parent is TreeRoot root)
                    return 0;
                var item = Parent as TreeItem;
                return item?.Level + 1 ?? -1;
            }
        }
        /// <summary>
        ///     根
        /// </summary>
        public TreeItemBase Parent
        {
            get;
            private set;
        }
        /// <summary>
        /// 刷新显示
        /// </summary>
        public void ReShow()
        {
            ReBuildItems();
        }
        /// <summary>
        /// 找上级节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public TreeItem<T> FindParent<T>() where T : class
        {
            if (Parent == null)
                return null;
            var item = Parent as TreeItem<T>;
            return item ?? Parent.FindParent<T>();
        }


        /// <summary>
        ///     子级
        /// </summary>
        public NotificationList<TreeItem> Items
        {
            get;
        }

        /// <summary>
        /// 找对应节点
        /// </summary>
        /// <returns></returns>
        public TreeItem Find(Func<TreeItem, bool?> func)
        {
            foreach (var item in Items)
            {
                var re = func(item);
                if (re == true)
                    return item;
                if (re == null)
                    continue;
                var ch = item.Find(func);
                if (ch != null)
                    return ch;
            }
            return null;
        }
        /// <summary>
        /// 找对应节点
        /// </summary>
        /// <returns></returns>
        public void Find(Func<TreeItem, bool> func,Action<TreeItem> action)
        {
            foreach (var item in Items)
            {
                if (func(item))
                    action(item);
                item.Find(func, action);
            }
        }
        /// <summary>
        /// 找对应节点
        /// </summary>
        /// <returns></returns>
        public void Foreach(Action<TreeItem> action)
        {
            foreach (var item in Items)
            {
                action(item);
                item.Foreach(action);
            }
        }
        private Visibility _visibility = Visibility.Visible;

        /// <summary>
        ///     可见
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
                RaisePropertyChanged(() => Visibility);
            }
        }

        private bool _isExpend;

        /// <summary>
        ///     展开
        /// </summary>
        public bool IsExpanded
        {
            get => _isExpend;
            set
            {
                if (_isExpend == value)
                {
                    return;
                }
                _isExpend = value;
                RaisePropertyChanged(() => IsExpanded);
                OnIsExpandedChanged();
            }
        }

        /// <summary>
        /// 展开状态变化的处理
        /// </summary>
        protected virtual void OnIsExpandedChanged()
        {
        }

        /// <summary>
        ///     是否被界面选中
        /// </summary>
        public bool IsUiSelected
        {
            get => _isUiSelected;
            set
            {
                if (_isUiSelected == value)
                {
                    return;
                }
                _isUiSelected = value;
                IsSelected = value;
                RaisePropertyChanged(() => IsUiSelected);
            }
        }
        

        /// <summary>
        ///     是否选中
        /// </summary>
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected == value) return;
                isSelected = value;
                RaisePropertyChanged(() => IsSelected);
                OnIsSelectChanged();
            }
        }

        /// <summary>
        ///     选择路径
        /// </summary>
        public string SelectPath
        {
            get => _selectedPath;
            set
            {
                if (_selectedPath == value)
                {
                    return;
                }
                _selectedPath = value;
                RaisePropertyChanged(() => SelectPath);
            }
        }
        /// <summary>
        /// 载入时的处理
        /// </summary>
        protected virtual void OnLoading()
        {

        }
        /// <summary>
        ///     子级节点发生变化的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null)
            {
                return;
            }
            foreach (TreeItem item in e.NewItems)
            {
                item.Parent = this;
                item.OnLoading();
                OnItemsCollectionChanged(item);
            }
        }

        protected virtual void OnSourceModify()
        {
        }

        /// <summary>
        ///     子级节点发生变化的处理
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnItemsCollectionChanged(TreeItem item)
        {

        }

        /// <summary>
        ///     当前选择发生变化
        /// </summary>
        protected virtual void OnIsSelectChanged()
        {
        }

        /// <summary>
        ///     子级选择发生变化
        /// </summary>
        /// <param name="select">是否选中</param>
        /// <param name="child">子级</param>
        /// <param name="selectItem">选中的对象</param>
        protected internal abstract void OnChildIsSelectChanged(bool select, TreeItemBase child, TreeItemBase selectItem);

        #region 自动同步子级


        /// <summary>
        ///     实际的子级对象
        /// </summary>
        private INotifyCollectionChanged _notifyItems;

        /// <summary>
        ///     实际的子级对象
        /// </summary>
        private IList _friendItems;

        /// <summary>
        ///     实际的子级对象
        /// </summary>
        public IList FriendItems
        {
            get => _friendItems;
            set
            {
                if (Equals(_friendItems, value))
                    return;
                if (_notifyItems != null)
                {
                    _notifyItems.CollectionChanged -= OnFriendItemsCollectionChanged;
                }
                _friendItems = value;
                _notifyItems = value as INotifyCollectionChanged;
                if (_notifyItems != null)
                {
                    _notifyItems.CollectionChanged += OnFriendItemsCollectionChanged;
                }

                ReBuildItems();
            }
        }

        private void ReBuildItems()
        {
            Items.Clear();
            if (!(_friendItems is IEnumerable values))
            {
                return;
            }
            foreach (var value in values)
            {
                Items.Add(CreateChild(value));
            }
        }
        /// <summary>
        ///     子级
        /// </summary>
        public Func<object, TreeItem> CreateChildFunc
        {
            get;
            set;
        }
        /// <summary>
        /// 生成子级
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public TreeItem CreateChild(object value)
        {
            TreeItem item = CreateChildFunc(value);

            var extend = value as IExtendDependencyObjects;
            extend?.Dependency.Annex(item);

            return item;
        }
        /// <summary>
        ///     子级节点发生变化的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFriendItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            WorkContext.SynchronousContext.BeginInvokeInUiThread(OnFriendItemsCollectionChanged, e);
        }

        private void OnFriendItemsCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Move:
                    return;
                case NotifyCollectionChangedAction.Reset:
                    ReBuildItems();
                    return;
            }
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    Items.Add(CreateChild(item));
                }
            }
            if (e.OldItems == null)
                return;
            foreach (var item in e.OldItems)
            {
                var ch = Items.FirstOrDefault(p => p.Source == item);
                if (ch != null)
                {
                    Items.Remove(ch);
                }
            }
        }


        #endregion
        #region 关联对象

        /// <summary>
        ///     绑定对象
        /// </summary>
        private NotificationObject _friend;

        /// <summary>
        ///     绑定对象
        /// </summary>
        public NotificationObject Friend
        {
            get => _friend;
            set
            {
                if (_friend == value)
                    return;
                if (_friend != null)
                {
                    _friend.PropertyChanged -= OnSorucePropertyChanged;
                }
                OnSourceChanged(true);
                _friend = value;
                OnSourceChanged(false);
                if (_friend != null)
                {
                    _friend.PropertyChanged += OnSorucePropertyChanged;
                }
                RaisePropertyChanged(nameof(Friend));
            }
        }
        /// <summary>
        ///     视角
        /// </summary>
        public string FriendView { get; set; }
        /// <summary>
        ///     绑定对象
        /// </summary>
        private NotificationObject _source;

        /// <summary>
        ///     绑定对象
        /// </summary>
        public NotificationObject Source
        {
            get => _source;
            set
            {
                if (_source == value)
                    return;
                if (_source != null)
                {
                    _source.PropertyChanged -= OnSorucePropertyChanged;
                }
                OnSourceChanged(true);
                _source = value;
                OnSourceChanged(false);
                if (_source != null)
                {
                    _source.PropertyChanged += OnSorucePropertyChanged;
                }
                RaisePropertyChanged(nameof(Source));
            }
        }

        /// <summary>
        ///     类型视角
        /// </summary>
        public string SoruceView { get; set; }


        protected virtual void OnSourceChanged(bool isRemove)
        {

        }

        private void OnSorucePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != _soruceItemsName)
                return;
            FriendItems = GetSoruceItems();
            if (e.PropertyName != "IsModify" || Source == null)
                return;
            OnSourceModify();
        }

        private string _soruceItemsName;

        // ReSharper disable UnusedField.Compiler
        private Func<IList> GetSoruceItems;
        // ReSharper restore UnusedField.Compiler

        private Expression<Func<IList>> _soruceItemsExpression;
        /// <summary>
        /// 子级对象（用于同步数据）
        /// </summary>
        [Browsable(false)]
        public Expression<Func<IList>> SoruceItemsExpression
        {
            get => _soruceItemsExpression;
            set
            {
                _soruceItemsExpression = value;
                if (value == null)
                {
                    _soruceItemsName = null;
                    GetSoruceItems = null;
                    Source = null;
                    return;
                }
                if (!(value.Body is MemberExpression mb))
                {
                    _soruceItemsName = null;
                    GetSoruceItems = null;
                    Source = null;
                    return;
                }
                _soruceItemsName = mb.Member.Name;
                if (mb.Expression is ConstantExpression constantExpression)
                {
                    Source = constantExpression.Value as NotificationObject;
                }
                GetSoruceItems = ReflectionHelper.GetFunc(value);
                FriendItems = GetSoruceItems();
            }
            
        }

        #endregion
        #region 扩展对象

        private ExtendObject _extend;


        /// <summary>
        ///     扩展对象
        /// </summary>
        public ExtendObject Extend
        {
            get => _extend ?? (_extend = CreateExtend());
            set
            {
                if (_extend == value)
                {
                    return;
                }
                _extend = value;
                RaisePropertyChanged(() => Extend);
            }
        }

        /// <summary>
        ///     构造扩展属性
        /// </summary>
        /// <returns></returns>
        protected virtual ExtendObject CreateExtend()
        {
            return new ExtendObject();
        }

        #endregion

    }
}
