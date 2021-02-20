using Agebull.EntityModel.Config;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Agebull.EntityModel
{
    /// <summary>
    ///     表示一个自动存储的列表对象
    /// </summary>
    public class ConfigCollection<TConfig> : NotificationList<TConfig>
        where TConfig : ConfigBase, IChildrenConfig
    {
        #region 构造

        public ConfigCollection()
        {

        }

        public ConfigCollection(ConfigBase parent)
        {
            Parent = parent;
        }

        #endregion

        #region 子级同步

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