using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置触发器
    /// </summary>
    public class EntityTrigger : ParentConfigTrigger<EntityConfig>
    {
        /// <summary>
        /// 载入事件处理
        /// </summary>
        protected override void OnLoad()
        {
            TargetConfig.LastProperties.Clear();
            TargetConfig.LastProperties.AddRange(TargetConfig.Properties.Where(p => !p.Discard));
            foreach (var property in TargetConfig.Properties)
            {
                property.StatusChanged += OnFieldStatusChanged;
            }
            TargetConfig.Properties.CollectionChanged += OnPropertiesCollectionChanged;
        }

        /// <summary>
        /// 载入事件处理
        /// </summary>
        protected override void OnCreate()
        {
            if (!SolutionConfig.Current.Entities.Contains(TargetConfig))
                SolutionConfig.Current.Entities.Add(TargetConfig);
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            switch (property)
            {
                case nameof(TargetConfig.LastProperties):
                    TargetConfig.Properties.CollectionChanged += OnPropertiesCollectionChanged;
                    break;
                case nameof(TargetConfig.Name):
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.Name));
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.DisplayName));
                    break;
                case nameof(TargetConfig.SaveTableName):
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.ReadTableName));
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.DisplayName));
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.SaveTable));
                    break;
                case nameof(TargetConfig.ReadTableName):
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.SaveTableName));
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.DisplayName));
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.SaveTable));
                    break;
                case nameof(TargetConfig.Classify):
                    OnClassifyChanged();
                    break;
                case nameof(TargetConfig.Project):
                    CheckEntityProject();
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
                case nameof(TargetConfig.SaveTableName):
                    SyncLinkTable(oldValue, newValue, (field, old, value) => field.LinkTable = (string)value);
                    break;
                case nameof(TargetConfig.Properties):
                    SyncProperties(oldValue, newValue);
                    break;
            }
        }

        private void SyncProperties(object oldValue, object newValue)
        {
            var ops = (ObservableCollection<PropertyConfig>)oldValue;
            if (ops != null)
                ops.CollectionChanged -= OnPropertiesCollectionChanged;
            var nps = (ObservableCollection<PropertyConfig>)newValue;
            if (nps != null)
                nps.CollectionChanged += OnPropertiesCollectionChanged;
            TargetConfig.LastProperties.Clear();
            TargetConfig.LastProperties.AddRange(TargetConfig.Properties.Where(p => !p.Discard));
        }

        private void SyncLinkTable(object oldValue, object newValue, Action<PropertyConfig, object, object> action)
        {
            string oldName = (string)oldValue;
            foreach (var entity in SolutionConfig.Current.Entities)
            {
                foreach (var field in entity.Properties)
                {
                    if (field.LinkTable == oldName)
                        action(field, oldValue, newValue);
                }
            }
        }

        /// <summary>
        /// 分类改变事件处理
        /// </summary>
        private void OnClassifyChanged()
        {
            TargetConfig.ExtendConfig.Clear();
            TargetConfig["File_Web_Index"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\Index.aspx";
            TargetConfig["File_Web_Action"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\Action.aspx";
            TargetConfig["File_Web_Form"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\Form.htm";
            TargetConfig["File_Web_Script"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\script.js";
            TargetConfig["File_Web_Item"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\Item.aspx";
            TargetConfig["File_Web_Details"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\Details.aspx";

            TargetConfig["File_Model_Business"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}BusinessLogic";
            TargetConfig["File_Model_Action"] = $"Page\\{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\{TargetConfig.Name}PageAction";
        }
        private static bool _inPropertiesCollectionChanged;
        private static void OnPropertiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (NotificationObject.IsLoadingMode || _inPropertiesCollectionChanged)
                return;
            var entity = sender as EntityConfig;
            if (entity == null)
                return;
            try
            {
                _inPropertiesCollectionChanged = true;

                if (!entity.IsModify)
                    entity.IsModify = true;

                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Reset:
                        entity.LastProperties.Clear();
                        entity.LastProperties.AddRange(entity.Properties.Where(p => !p.Discard));
                        foreach (var property in entity.LastProperties)
                        {
                            property.StatusChanged += OnFieldStatusChanged;
                            GlobalTrigger.OnLoad(property);
                        }
                        return;
                }
                if (e.NewItems != null && e.NewItems.Count > 0)
                {
                    foreach (PropertyConfig property in e.NewItems)
                    {
                        property.Identity = ++entity.MaxIdentity;
                        property.Index = entity.Properties.Max(p => p.Index) + 1;
                        entity.LastProperties.Add(property);
                        property.StatusChanged += OnFieldStatusChanged;
                        GlobalTrigger.OnAdded(entity, property);
                    }
                }
                if (e.OldItems != null && e.OldItems.Count > 0)
                {
                    foreach (PropertyConfig property in e.OldItems)
                    {
                        entity.LastProperties.Remove(property);
                        property.StatusChanged -= OnFieldStatusChanged;
                        GlobalTrigger.OnRemoved(entity, property);
                    }
                }
            }
            finally
            {
                _inPropertiesCollectionChanged = false;
            }
        }

        private void CheckEntityProject()
        {
            if (NotificationObject.IsLoadingMode)
            {
                return;
            }
            EntityConfig entity = TargetConfig;
            if (entity.Parent != null && entity.Parent.Entities.Contains(entity))
                entity.Parent.Entities.Remove(entity);
            if (string.IsNullOrWhiteSpace(entity.Project))
            {
                entity.Project = "默认";
            }
            var project = SolutionConfig.Current.Projects.FirstOrDefault(
                p => string.Equals(p.Name, entity.Project, StringComparison.OrdinalIgnoreCase));
            if (project == null)
            {
                var friend = SolutionConfig.Current.Projects[0];
                SolutionConfig.Current.Projects.Add(project = new ProjectConfig
                {
                    Name = entity.Project,
                    Caption = entity.Project,
                    ModelPath = friend.ModelPath,
                    PagePath = friend.PagePath,
                    DataBaseObjectName = friend.DataBaseObjectName,
                    Entities = new ConfigCollection<EntityConfig>
                    {
                        entity
                    }
                });
                entity.Parent = project;
                return;
            }
            entity.Parent = project;
            if (project.Entities == null)
                project.Entities = new ConfigCollection<EntityConfig>();
            if (!project.Entities.Contains(entity))
            {
                project.Entities.Add(entity);
            }
        }


        private static void OnFieldStatusChanged(object sender, PropertyChangedEventArgs e)
        {
            if (NotificationObject.IsLoadingMode)
            {
                return;
            }
            var property = sender as PropertyConfig;
            if (property?.Parent == null)
                return;
            if (property.IsModify && !property.Parent.IsModify)
                property.Parent.IsModify = true;
        }
    }
}


