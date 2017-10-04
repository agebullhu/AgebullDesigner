using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel
{
    /// <summary>
    ///     ���ڵ�ģ��
    /// </summary>
    public class TreeItem : TreeItemBase
    {
        /// <summary>
        /// ����δ������ӽڵ�
        /// </summary>
        protected static readonly TreeItem LodingItem = new TreeItem("...");


        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="source"></param>
        public TreeItem(object source)
        {
            var pp = source as INotifyPropertyChanged;
            if (pp != null)
            {
                pp.PropertyChanged += OnModelPropertyChanged;
            }
            Source = source as NotificationObject;
        }

        /// <summary>
        ///     ����
        /// </summary>
        public string Catalog
        {
            get;
            set;
        }
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="header"></param>
        public TreeItem(string header)
        {
            Header = header;
        }


        public T FindParentModel<T>() where T : class
        {
            var itemModel = Parent as TreeItem;
            if (itemModel == null)
                return null;
            var model = itemModel.Source as T;
            return model ?? itemModel.FindParentModel<T>();
        }
        /// <summary>
        ///     ��
        /// </summary>
        protected internal TreeRoot Root
        {
            get
            {
                var root = Parent as TreeRoot;
                if (root != null)
                    return root;
                var item = Parent as TreeItem;
                return item?.Root;
            }
        }


        private string _header;

        private bool _isExpend;

        /// <summary>
        ///     ����
        /// </summary>
        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                if (_header == value)
                {
                    return;
                }
                _header = value;
                RaisePropertyChanged(() => Header);
            }
        }



        /// <summary>
        /// �Ҷ�Ӧ�ڵ�
        /// </summary>
        /// <returns></returns>
        public TreeItem Find(NotificationObject obj)
        {
            if (obj == null || Items.Count == 0)
                return null;
            if (Source == obj)
                return this;
            return Items.Select(child => child.Find(obj)).FirstOrDefault(item => item != null);
        }

        /// <summary>
        ///     �Ӽ��Ƿ�������
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return _isExpend;
            }
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
        /// չ��״̬�仯�Ĵ���
        /// </summary>
        protected virtual void OnIsExpandedChanged()
        {
        }

        protected override void OnSourceChanged(bool isRemove)
        {
            if (_commands == null)
                return;
            if (isRemove)
            {
                foreach (var cmd in _commands.Where(p => p.Tag == Source).ToArray())
                {
                    _commands.Remove(cmd);
                }
            }
            else
            {
                var actions = CommandCoefficient.Coefficient(Source);
                foreach (var action in actions)
                {
                    action.Tag = Source;
                    _commands.Add(action);
                }
            }
        }

        /// <summary>
        ///     ��Ӧ�������
        /// </summary>
        public IEnumerable<CommandItem> Buttons => _commands?.Where(p => !p.NoButton && Catalog == p.Catalog);

        /// <summary>
        ///     ��Ӧ�������
        /// </summary>
        public IEnumerable<CommandItem> Menus => _commands?.Where(p => p.NoButton && Catalog== p.Catalog );

        private List<CommandItem> _commands;
        

        /// <summary>
        /// ���������б�
        /// </summary>
        public void CreateCommandList()
        {
            _commands = new List<CommandItem>();
            var actions = CommandCoefficient.Coefficient(Source);
            foreach (var action in actions)
            {
                action.Tag = Source;
                action.Parameter = Source;
                _commands.Add(action);
            }
            CreateCommandList(_commands);
            RaisePropertyChanged(nameof(Buttons));
            RaisePropertyChanged(nameof(Menus));
        }

        /// <summary>
        /// ���������б�
        /// </summary>
        protected virtual void CreateCommandList(List<CommandItem> commands)
        {
        }

        /// <summary>
        /// ���������б�
        /// </summary>
        public void ClearCommandList()
        {
            if (_commands == null)
                return;
            foreach (var command in _commands)
            {
                command.Tag = null;
                command.Parameter = null;
            }
            _commands.Clear();
            _commands = null;
        }
        private string _soruceType;


        /// <summary>
        ///     ������
        /// </summary>
        public string SoruceType
        {
            get
            {
                return _soruceType;
            }
            set
            {
                if (_soruceType == value)
                {
                    return;
                }
                _soruceType = value;
                RaisePropertyChanged(() => SoruceType);
                RaisePropertyChanged(() => SoruceTypeIcon);
            }
        }

        private FontWeight _font;
        /// <summary>
        ///     �����С
        /// </summary>
        public FontWeight FontWeight
        {
            get
            {
                return _font;
            }
            set
            {
                if (Equals(_font, value))
                {
                    return;
                }
                _font = value;
                RaisePropertyChanged(() => FontWeight);
            }
        }

        private Brush _color = Brushes.Black;
        /// <summary>
        ///     ������ɫ
        /// </summary>
        public Brush Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (Equals(_color, value))
                {
                    return;
                }
                _color = value;
                RaisePropertyChanged(() => Color);
            }
        }

        private Brush _bcolor = Brushes.Transparent;
        /// <summary>
        ///     ������ɫ
        /// </summary>
        public Brush BackgroundColor
        {
            get
            {
                return _bcolor;
            }
            set
            {
                if (Equals(_bcolor, value))
                {
                    return;
                }
                _bcolor = value;
                RaisePropertyChanged(() => BackgroundColor);
            }
        }

        private BitmapImage _soruceTypeIcon;
        protected BitmapImage _baseIcon;
        /// <summary>
        ///     ������
        /// </summary>
        public BitmapImage SoruceTypeIcon
        {
            get
            {
                return _soruceTypeIcon;
            }
            set
            {
                if (Equals(_soruceTypeIcon, value))
                {
                    return;
                }
                if (_baseIcon == null)
                    _baseIcon = value;
                _soruceTypeIcon = value;
                RaisePropertyChanged(() => SoruceTypeIcon);
            }
        }
        protected BitmapImage _statusIcon;
        /// <summary>
        ///     ������
        /// </summary>
        public BitmapImage StatusIcon
        {
            get
            {
                return _statusIcon;
            }
            set
            {
                if (Equals(_statusIcon, value))
                {
                    return;
                }
                _statusIcon = value;
                RaisePropertyChanged(() => StatusIcon);
            }
        }


        private CommandStatus _childsStatus;
        /// <summary>
        ///     �Ӽ�����״̬
        /// </summary>
        public CommandStatus ChildsStatus
        {
            get
            {
                return _childsStatus;
            }
            set
            {
                if (_childsStatus == value)
                {
                    return;
                }
                _childsStatus = value;
                RaisePropertyChanged(() => ChildsStatus);
                RaisePropertyChanged(() => IsBusy);
                RaisePropertyChanged(() => ChildsStatusIcon);
            }
        }

        /// <summary>
        ///     �Ӽ�����״̬
        /// </summary>
        public bool IsBusy => ChildsStatus == CommandStatus.Executing;

        /// <summary>
        ///     ������
        /// </summary>
        public BitmapImage ChildsStatusIcon => Application.Current.Resources[$"async_{ChildsStatus}"] as BitmapImage;

        #region �����Զ�����

        private IFunctionDictionary _modelFunction;


        /// <summary>
        /// �����ֵ�
        /// </summary>
        public virtual IFunctionDictionary ModelDelegates => _modelFunction ?? (_modelFunction = new ModelFunctionDictionary<object>());

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (HeaderField != null && HeaderField.Contains(e.PropertyName))
            {
                BeginInvokeInUiThread(SyncHeaderAutomatic);
            }
            if (StatusField != null && StatusField.Contains(e.PropertyName))
            {
                BeginInvokeInUiThread(SyncStatusImageAutomatic);
            }
            if (ColorField != null && ColorField.Contains(e.PropertyName))
            {
                BeginInvokeInUiThread(SyncColorAutomatic);
            }
        }

        /// <summary>
        /// ͼ����ص��ֶ�
        /// </summary>
        public string StatusField
        {
            get;
            set;
        }

        /// <summary>
        /// ��ɫ��ص��ֶ�
        /// </summary>
        public string ColorField
        {
            get;
            set;
        }

        /// <summary>
        /// ͬ���Զ�����
        /// </summary>
        protected virtual void SyncStatusImageAutomatic()
        {
            StatusIcon = ModelDelegates.TryExecute<BitmapImage>();
        }

        /// <summary>
        /// ģ��������ص��ֶ�
        /// </summary>
        public string HeaderField
        {
            get;
            set;
        }

        /// <summary>
        /// ͬ���Զ�����
        /// </summary>
        protected virtual void SyncColorAutomatic()
        {
            Color = ModelDelegates.TryExecute<Brush>();
        }

        /// <summary>
        /// ͬ���Զ�����
        /// </summary>
        protected virtual void SyncHeaderAutomatic()
        {
            Header = ModelDelegates.TryExecute<string>();
        }
        #endregion

        #region ѡ��

        /// <summary>
        ///     ��ǰѡ�����仯
        /// </summary>
        protected sealed override void OnIsSelectChanged()
        {
            SelectPath = IsSelected ? Header : null;
            if (!IsSelected)
            {
                SelectPath = null;
            }
            Parent?.OnChildIsSelectChanged(IsSelected, this, this);
        }

        /// <summary>
        ///     �Ӽ�ѡ�����仯
        /// </summary>
        /// <param name="select">�Ƿ�ѡ��</param>
        /// <param name="child">�Ӽ�</param>
        /// <param name="selectItem">ѡ�еĶ���</param>
        protected internal sealed override void OnChildIsSelectChanged(bool select, TreeItemBase child, TreeItemBase selectItem)
        {
            SelectPath = IsSelected ? null : Header + " > " + child.SelectPath;
            if (isSelected != select)
            {
                isSelected = select;
                RaisePropertyChanged(() => IsSelected);
            }
            Parent?.OnChildIsSelectChanged(IsSelected, this, selectItem);
        }

        #endregion

    }
}