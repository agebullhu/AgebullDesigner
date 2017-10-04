using System.Collections.Specialized;
using Agebull.Common.DataModel;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    /// 解决方案配置触发器
    /// </summary>
    public class SolutionTrigger : ParentConfigTrigger<SolutionConfig>
    {
        /// <summary>
        /// 载入事件处理
        /// </summary>
        protected override void OnLoad()
        {
            using (LoadingModeScope.CreateScope())
            {
                SolutionModel model = new SolutionModel
                {
                    Solution = TargetConfig
                };

                model.RepairByLoaded();
                model.ResetStatus();
                model.OnSolutionLoad();

                TargetConfig.Projects.CollectionChanged += ConfigCollectionChanged;
                TargetConfig.Enums.CollectionChanged += ConfigCollectionChanged;
                TargetConfig.ApiItems.CollectionChanged += ConfigCollectionChanged;
                TargetConfig.NotifyItems.CollectionChanged += ConfigCollectionChanged;
                TargetConfig.Entities.CollectionChanged += EntitiesCollectionChanged;

                foreach (var cfg in TargetConfig.Enums)
                {
                    GlobalTrigger.OnLoad(cfg);
                }
                foreach (var cfg in TargetConfig.ApiItems)
                {
                    GlobalTrigger.OnLoad(cfg);
                }
                foreach (var cfg in TargetConfig.NotifyItems)
                {
                    GlobalTrigger.OnLoad(cfg);
                }
            }
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            switch (property)
            {
                case nameof(TargetConfig.Entities):
                    TargetConfig.Entities.CollectionChanged += EntitiesCollectionChanged;
                    break;
                case nameof(TargetConfig.Projects):
                    TargetConfig.Projects.CollectionChanged += ConfigCollectionChanged;
                    break;
                case nameof(TargetConfig.NotifyItems):
                    TargetConfig.NotifyItems.CollectionChanged += ConfigCollectionChanged;
                    break;
                case nameof(TargetConfig.ApiItems):
                    TargetConfig.ApiItems.CollectionChanged += ConfigCollectionChanged;
                    break;
                case nameof(TargetConfig.Enums):
                    TargetConfig.Enums.CollectionChanged += ConfigCollectionChanged;
                    break;
            }
        }

        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {
            switch (property)
            {
                case nameof(TargetConfig.Entities):
                    if (oldValue != null)
                        ((INotifyCollectionChanged)oldValue).CollectionChanged -= EntitiesCollectionChanged;
                    break;
                case nameof(TargetConfig.Projects):
                    if (oldValue != null)
                        ((INotifyCollectionChanged)oldValue).CollectionChanged -= ConfigCollectionChanged;
                    break;
                case nameof(TargetConfig.NotifyItems):
                    if (oldValue != null)
                        ((INotifyCollectionChanged)oldValue).CollectionChanged -= ConfigCollectionChanged;
                    break;
                case nameof(TargetConfig.ApiItems):
                    if (oldValue != null)
                        ((INotifyCollectionChanged)oldValue).CollectionChanged -= ConfigCollectionChanged;
                    break;
                case nameof(TargetConfig.Enums):
                    if (oldValue != null)
                        ((INotifyCollectionChanged)oldValue).CollectionChanged -= ConfigCollectionChanged;
                    break;
                case nameof(TargetConfig.RootPath):
                    SolutionModel model = new SolutionModel
                    {
                        Solution = TargetConfig
                    };
                    model.CheckProjectPath((string)oldValue, (string)newValue);
                    break;
            }
        }

        private void ConfigCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                OnLoad(TargetConfig);
                return;
            }
            if (e.NewItems != null)
            {
                foreach (ConfigBase item in e.NewItems)
                    GlobalTrigger.OnAdded(TargetConfig, item);
            }
            if (e.OldItems == null)
                return;
            foreach (ConfigBase item in e.OldItems)
                GlobalTrigger.OnRemoved(TargetConfig, item);
        }

        private void EntitiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (var item in TargetConfig.Entities)
                {
                    if (item.Parent.Entities.Contains(item))
                    {
                        item.Parent.Entities.Remove(item);
                    }
                    GlobalTrigger.OnRemoved(TargetConfig, item);
                }
                foreach (var item in TargetConfig.Entities)
                {
                    GlobalTrigger.OnAdded(TargetConfig, item);
                }
                return;
            }
            if (e.NewItems != null)
                foreach (EntityConfig item in e.NewItems)
                {
                    GlobalTrigger.OnAdded(TargetConfig, item);
                }
            if (e.OldItems == null)
                return;
            foreach (EntityConfig item in e.OldItems)
            {
                if (item.Parent.Entities.Contains(item))
                {
                    item.Parent.Entities.Remove(item);
                }
                GlobalTrigger.OnRemoved(TargetConfig, item);
            }
        }


    }
}