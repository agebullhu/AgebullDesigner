// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Gboxt.Common.WpfMvvmBase
// ����:2014-11-24
// �޸�:2014-11-29
// *****************************************************/

#region ����

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Agebull.Common.Reflection;

#endregion

namespace Agebull.EntityModel
{
    /// <summary>
    ///     ��ʾһ�����Ľڵ�
    /// </summary>
    public abstract class TreeItemBase : NotificationObject
    {
        private bool _isUiSelected;

        private string _selectedPath;

        /// <summary>
        /// �Ƿ�ѡ��
        /// </summary>
        protected bool isSelected;

        /// <summary>
        ///     ����
        /// </summary>
        protected TreeItemBase()
        {
            Items = new ObservableCollection<TreeItem>();
            Items.CollectionChanged += OnItemsCollectionChanged;
        }


        /// <summary>
        ///     �㼶
        /// </summary>
        public int Level
        {
            get
            {
                var root = Parent as TreeRoot;
                if (root != null)
                    return 0;
                var item = Parent as TreeItem;
                return item?.Level + 1 ?? -1;
            }
        }
        /// <summary>
        ///     ��
        /// </summary>
        public TreeItemBase Parent
        {
            get;
            private set;
        }
        /// <summary>
        /// ˢ����ʾ
        /// </summary>
        public void ReShow()
        {
            ReBuildItems();
        }
        /// <summary>
        /// ���ϼ��ڵ�
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
        ///     �Ӽ�
        /// </summary>
        public ObservableCollection<TreeItem> Items
        {
            get;
        }

        /// <summary>
        /// �Ҷ�Ӧ�ڵ�
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
        ///     �Ƿ񱻽���ѡ��
        /// </summary>
        public bool IsUiSelected
        {
            get
            {
                return _isUiSelected;
            }
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
        ///     �Ƿ�ѡ��
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    RaisePropertyChanged(() => IsSelected);
                }
                OnIsSelectChanged();
            }
        }

        /// <summary>
        ///     ѡ��·��
        /// </summary>
        public string SelectPath
        {
            get
            {
                return _selectedPath;
            }
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
        /// ����ʱ�Ĵ���
        /// </summary>
        protected virtual void OnLoading()
        {

        }
        /// <summary>
        ///     �Ӽ��ڵ㷢���仯�Ĵ���
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
        ///     �Ӽ��ڵ㷢���仯�Ĵ���
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnItemsCollectionChanged(TreeItem item)
        {

        }

        /// <summary>
        ///     ��ǰѡ�����仯
        /// </summary>
        protected virtual void OnIsSelectChanged()
        {
        }

        /// <summary>
        ///     �Ӽ�ѡ�����仯
        /// </summary>
        /// <param name="select">�Ƿ�ѡ��</param>
        /// <param name="child">�Ӽ�</param>
        /// <param name="selectItem">ѡ�еĶ���</param>
        protected internal abstract void OnChildIsSelectChanged(bool select, TreeItemBase child, TreeItemBase selectItem);

        #region �Զ�ͬ���Ӽ�


        /// <summary>
        ///     ʵ�ʵ��Ӽ�����
        /// </summary>
        private INotifyCollectionChanged _notifyItems;

        /// <summary>
        ///     ʵ�ʵ��Ӽ�����
        /// </summary>
        private IList _friendItems;

        /// <summary>
        ///     ʵ�ʵ��Ӽ�����
        /// </summary>
        public IList FriendItems
        {
            get
            {
                return _friendItems;
            }
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
            var values = _friendItems as IEnumerable;
            if (values == null)
            {
                return;
            }
            foreach (var value in values)
            {
                Items.Add(CreateChild(value));
            }
        }
        /// <summary>
        ///     �Ӽ�
        /// </summary>
        public Func<object, TreeItem> CreateChildFunc
        {
            get;
            set;
        }
        /// <summary>
        /// �����Ӽ�
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public TreeItem CreateChild(object value)
        {
            TreeItem item = CreateChildFunc == null
                ? new TreeItem(value)
                : CreateChildFunc(value);

            var extend = value as IExtendDependencyObjects;
            extend?.Dependency.Annex(item);

            return item;
        }
        /// <summary>
        ///     �Ӽ��ڵ㷢���仯�Ĵ���
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
        #region ��������

        /// <summary>
        ///     �󶨶���
        /// </summary>
        private NotificationObject __source;

        /// <summary>
        ///     �󶨶���
        /// </summary>
        public NotificationObject Source
        {
            get
            {
                return __source;
            }
            set
            {
                if (__source == value)
                    return;
                if (__source != null)
                {
                    __source.PropertyChanged -= OnSorucePropertyChanged;
                    OnSourceChanged(true);
                }
                __source = value;
                if (__source != null)
                {
                    OnSourceChanged(false);
                    __source.PropertyChanged += OnSorucePropertyChanged;
                }
                RaisePropertyChanged(nameof(NotificationObject));
            }
        }

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
        /// ȡ�������ݵķ���
        /// </summary>
        [Browsable(false)]
        public Expression<Func<IList>> SoruceItemsExpression
        {
            get
            {
                return _soruceItemsExpression;
            }
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
                var mb = value.Body as MemberExpression;
                if (mb == null)
                {
                    _soruceItemsName = null;
                    GetSoruceItems = null;
                    Source = null;
                    return;
                }
                _soruceItemsName = mb.Member.Name;
                var constantExpression = mb.Expression as ConstantExpression;
                if (constantExpression != null)
                {
                    Source = constantExpression.Value as NotificationObject;
                }
                GetSoruceItems = ReflectionHelper.GetFunc(value);
                FriendItems = GetSoruceItems();
            }
        }

        #endregion
        #region ��չ����

        private ExtendObject _extend;


        /// <summary>
        ///     ��չ����
        /// </summary>
        public ExtendObject Extend
        {
            get
            {
                return _extend ?? (_extend = CreateExtend());
            }
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
        ///     ������չ����
        /// </summary>
        /// <returns></returns>
        protected virtual ExtendObject CreateExtend()
        {
            return new ExtendObject();
        }

        #endregion

    }
}
