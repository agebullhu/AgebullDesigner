// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Gboxt.Common.WpfMvvmBase
// ����:2014-11-24
// �޸�:2014-11-29
// *****************************************************/

#region ����

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Agebull.Common.Mvvm;

#endregion

namespace Agebull.EntityModel
{
    /// <summary>
    ///     ��ʾһ�����ĸ��ڵ�
    /// </summary>
    public sealed class TreeRoot : TreeItemBase
    {
        public static TreeRoot Root;

        private TreeItem _selectItem;

        /// <summary>
        ///     ����
        /// </summary>
        public TreeRoot()
        {
            Root = this;
        }
        /// <summary>
        /// ȫ������
        /// </summary>
        public readonly Dictionary<string, object> GlobalConfig = new Dictionary<string, object>();

        /// <summary>
        /// �Ҷ�Ӧ�ڵ�
        /// </summary>
        /// <returns></returns>
        public TreeItem Find(NotificationObject obj)
        {
            return Items.Select(child => child.Find(obj)).FirstOrDefault(item => item != null);
        }


        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="items">�ڵ�</param>
        public TreeRoot(IEnumerable<TreeItem> items)
        {
            if (items == null)
            {
                return;
            }
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        #region ��ǰѡ�еĽڵ�

        /// <summary>
        ///     ��ǰѡ�еĽڵ�
        /// </summary>
        public TreeItem SelectItem
        {
            get => _selectItem;
            set
            {
                if (_selectItem == value)
                {
                    return;
                }
                _selectItem?.ClearCommandList();
                _selectItem = value;
                _selectItem?.CreateCommandList();
                RaisePropertyChanged(() => SelectItem);
                SelectItemChanged?.Invoke(value, EventArgs.Empty);
            }
        }

        public event EventHandler SelectItemChanged;
        #endregion

        #region ѡ��

        /// <summary>
        ///     �Ӽ�ѡ�����仯
        /// </summary>
        /// <param name="select">�Ƿ�ѡ��</param>
        /// <param name="child">�Ӽ�</param>
        /// <param name="selectItem">ѡ�еĶ���</param>
        protected internal override void OnChildIsSelectChanged(bool select, TreeItemBase child, TreeItemBase selectItem)
        {
            SelectPath = IsSelected ? null : child.SelectPath;
            if (isSelected != select)
            {
                isSelected = select;
                RaisePropertyChanged(() => IsSelected);
            }
            SelectItem = selectItem as TreeItem;
        }

        #endregion

        #region ��չ����

        private NotificationList<CommandItemBase> _commands;

        private ModelFunctionDictionary<TreeRoot> _modelFunction;

        /// <summary>
        ///     ���������ֵ�
        /// </summary>
        [IgnoreDataMember]
        public ModelFunctionDictionary<TreeRoot> ModelFunction
        {
            get => _modelFunction ?? (_modelFunction = new ModelFunctionDictionary<TreeRoot>() );
            set => _modelFunction = value;
        }

        /// <summary>
        ///     ��Ӧ�������
        /// </summary>
        public NotificationList<CommandItemBase> Commands
        {
            get => _commands ?? (_commands = new NotificationList<CommandItemBase>() );
            set
            {
                if (_commands == value)
                {
                    return;
                }
                _commands = value;
                RaisePropertyChanged(() => Commands);
            }
        }

        #endregion
        
    }
}
