using Agebull.EntityModel.Config;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Agebull.EntityModel
{
    /// <summary>
    ///     ��ʾһ���Զ��洢���б����
    /// </summary>
    public class ConfigCollection<TConfig> : NotificationList<TConfig>
        where TConfig : ConfigBase, IChildrenConfig
    {
        #region ����

        public ConfigCollection()
        {

        }

        public ConfigCollection(ConfigBase parent)
        {
            Parent = parent;
        }

        #endregion

        #region �Ӽ�ͬ��

        public ConfigBase Parent { get; set; }

        private void OnRemove(TConfig item)
        {
            item.PropertyChanged -= Item_PropertyChanged;
            GlobalTrigger.OnRemoved(Parent, item);
            Parent.IsModify = true;
            GlobalConfig.RemoveConfig(item.Option);
        }

        private void OnAdd(TConfig item)
        {
            if (Parent == null)
                return;
            GlobalConfig.AddConfig(item.Option);
            item.Index = Count == 0 ? 1 : this.Max(p => p.Index) + 1;
            item.Parent = Parent;
            item.PropertyChanged += Item_PropertyChanged;
            GlobalTrigger.OnAdded(Parent, item);
            item.OnLoad();
            item.IsModify = true;
            Parent.IsModify = true;
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Parent == null)
                return;
            var cfg = sender as ConfigBase;
            if (e.PropertyName == nameof(ConfigBase.IsModify))
            {
                if (cfg.IsModify)
                    Parent.IsModify = true;
            }
        }

        /// <summary>
        /// ����
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
        /// ����
        /// </summary>
        /// <returns></returns>
        public void OnLoad(ConfigBase parent)
        {
            Parent = parent;
            foreach (var item in this)
            {
                item.Parent = parent;
                item.PropertyChanged += Item_PropertyChanged;
                item.OnLoad();
            }
        }
        #endregion

        #region �����޸�

        /// <summary>
        ///     ͨ���ṩ�Ĳ������� <see cref="E:System.Collections.ObjectModel.NotificationList`1.CollectionChanged" /> �¼���
        /// </summary>
        /// <param name="e">Ҫ�����¼����Ա�����</param>
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
                    OnAdd(item);
                }
            }
            if (e.OldItems == null)
                return;
            foreach (var item in e.OldItems.OfType<TConfig>())
            {
                OnRemove(item);
            }
            base.OnCollectionChanged(e);
        }

        #endregion

    }
}