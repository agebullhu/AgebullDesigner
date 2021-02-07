using System.Collections.Specialized;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ��Ŀ���ô�����
    /// </summary>
    public class ProjectTrigger : ParentConfigTrigger<ProjectConfig>
    {
        /// <summary>
        /// �����¼�����
        /// </summary>
        protected override void OnLoad()
        {
            TargetConfig.Entities.CollectionChanged += OnEntityCollectionChanged;
            TargetConfig.Enums.CollectionChanged += OnEnumCollectionChanged;
            TargetConfig.ApiItems.CollectionChanged += OnApiCollectionChanged;
        }
        
        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
        }
        
        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
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