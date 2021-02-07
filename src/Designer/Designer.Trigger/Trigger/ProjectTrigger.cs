using System.Collections.Specialized;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 项目配置触发器
    /// </summary>
    public class ProjectTrigger : ParentConfigTrigger<ProjectConfig>
    {
        /// <summary>
        /// 载入事件处理
        /// </summary>
        protected override void OnLoad()
        {
            TargetConfig.Entities.CollectionChanged += OnEntityCollectionChanged;
            TargetConfig.Enums.CollectionChanged += OnEnumCollectionChanged;
            TargetConfig.ApiItems.CollectionChanged += OnApiCollectionChanged;
        }
        
        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
        }
        
        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        protected override void BeforePropertyChangedInner(string property, object oldValue, object newValue)
        {
        }

        #region OnEntityCollectionChanged

        private bool _inEntityCollectionChanged;
        private void OnEntityCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (WorkContext.IsNoChangedNotify || _inEntityCollectionChanged)
                return;
            if (!(sender is ProjectConfig project))
                return;
            try
            {
                _inEntityCollectionChanged = true;

                if (!project.IsModify)
                    project.IsModify = true;

                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Reset:
                        foreach (var entity in project.Entities)
                        {
                            GlobalTrigger.OnLoad(entity);
                        }
                        return;
                }
                if (e.NewItems != null && e.NewItems.Count > 0)
                {
                    foreach (EntityConfig entity in e.NewItems)
                    {
                        GlobalTrigger.OnLoad(entity);
                    }
                }
                if (e.OldItems != null && e.OldItems.Count > 0)
                {
                    foreach (EntityConfig entity in e.OldItems)
                    {
                        GlobalTrigger.OnRemoved(project, entity);
                    }
                }
            }
            finally
            {
                _inEntityCollectionChanged = false;
            }
        }


        #endregion


        #region OnApiItemCollectionChanged

        private bool _inApiItemCollectionChanged;
        private void OnApiCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (WorkContext.IsNoChangedNotify || _inApiItemCollectionChanged)
                return;
            if (!(sender is ProjectConfig project))
                return;
            try
            {
                _inApiItemCollectionChanged = true;

                if (!project.IsModify)
                    project.IsModify = true;

                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Reset:
                        foreach (var entity in project.Entities)
                        {
                            GlobalTrigger.OnLoad(entity);
                        }
                        return;
                }
                if (e.NewItems != null && e.NewItems.Count > 0)
                {
                    foreach (ApiItem entity in e.NewItems)
                    {
                        GlobalTrigger.OnLoad(entity);
                    }
                }
                if (e.OldItems != null && e.OldItems.Count > 0)
                {
                    foreach (ApiItem entity in e.OldItems)
                    {
                        GlobalTrigger.OnRemoved(project, entity);
                    }
                }
            }
            finally
            {
                _inApiItemCollectionChanged = false;
            }
        }


        #endregion


        #region OnEnumCollectionChanged

        private bool _inEnumCollectionChanged;
        private void OnEnumCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (WorkContext.IsNoChangedNotify || _inEnumCollectionChanged)
                return;
            if (!(sender is ProjectConfig project))
                return;
            try
            {
                _inEnumCollectionChanged = true;

                if (!project.IsModify)
                    project.IsModify = true;

                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Reset:
                        foreach (var entity in project.Entities)
                        {
                            GlobalTrigger.OnLoad(entity);
                        }
                        return;
                }
                if (e.NewItems != null && e.NewItems.Count > 0)
                {
                    foreach (EnumConfig entity in e.NewItems)
                    {
                        GlobalTrigger.OnLoad(entity);
                    }
                }
                if (e.OldItems != null && e.OldItems.Count > 0)
                {
                    foreach (EnumConfig entity in e.OldItems)
                    {
                        GlobalTrigger.OnRemoved(project, entity);
                    }
                }
            }
            finally
            {
                _inEnumCollectionChanged = false;
            }
        }


        #endregion
    }
}