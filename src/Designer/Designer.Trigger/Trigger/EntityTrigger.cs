﻿using Agebull.EntityModel.Config;
using System.Collections.Specialized;
using System.Linq;

namespace Agebull.EntityModel.Designer
{

    /// <summary>
    /// 实体配置触发器
    /// </summary>
    public sealed class EntityTrigger : ConfigTriggerBase<EntityConfig>
    {

        protected override void OnLoad()
        {
            foreach (var field in Target.Properties)
            {
                GlobalTrigger.OnLoad(field);
            }
            Target.MaxIdentity = Target.Properties.Count == 0 ? 0 : Target.Properties.Max(p => p.Identity);
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            switch (property)
            {
                case nameof(Target.Name):
                    Target.RaisePropertyChanged(nameof(Target.DisplayName));
                    break;
                case nameof(Target.SaveTableName):
                    Target.RaisePropertyChanged(nameof(Target.ReadTableName));
                    Target.RaisePropertyChanged(nameof(Target.DisplayName));
                    Target.RaisePropertyChanged(nameof(Target.SaveTableName));
                    break;
                case nameof(Target.ReadTableName):
                    Target.RaisePropertyChanged(nameof(Target.SaveTableName));
                    Target.RaisePropertyChanged(nameof(Target.DisplayName));
                    Target.RaisePropertyChanged(nameof(Target.SaveTableName));
                    break;
                case nameof(Target.Classify):
                    OnClassifyChanged();
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
                //case nameof(Target.Name):
                //case nameof(Target.ReadTableName):
                //case nameof(Target.SaveTableName):
                //    SyncLinkTable(oldValue, newValue);
                //    break;
                case nameof(Target.Classify):
                    if (Target.Project == null || WorkContext.WorkModel == WorkModel.Repair)
                        return;
                    var oldCls = Target.Project.Classifies.FirstOrDefault(p => p.Name == (string)oldValue);
                    oldCls?.Items.Remove(Target);
                    var newoldCls = Target.Project.Classifies.FirstOrDefault(p => p.Name == (string)newValue);
                    newoldCls?.Items.TryAdd(Target);
                    break;
                case nameof(Target.Properties):
                    if (oldValue is NotificationList<FieldConfig> ops)
                        ops.CollectionChanged -= OnPropertiesCollectionChanged;
                    break;
            }
        }

        /*
        private void SyncLinkTable(object oldValue, object newValue)
        {
            string oldName = (string)oldValue;
            if (!string.IsNullOrWhiteSpace(oldName))
                return;
            foreach (var entity in SolutionConfig.Current.Entities)
            {
                foreach (var field in entity.Properties)
                {
                    if (field.LinkTable == oldName)
                        field.LinkTable = (string) newValue;
                }
            }
        }
        */
        /// <summary>
        /// 分类改变事件处理
        /// </summary>
        private void OnClassifyChanged()
        {
            //Target.ExtendConfig.Clear();
            //Target["File_Web_Index"] = $"{Target.Classify}\\{Target.PageFolder}\\Index.aspx";
            //Target["File_Web_Action"] = $"{Target.Classify}\\{Target.PageFolder}\\Action.aspx";
            //Target["File_Web_Form"] = $"{Target.Classify}\\{Target.PageFolder}\\Form.htm";
            //Target["File_Web_Script"] = $"{Target.Classify}\\{Target.PageFolder}\\script.js";
            //Target["File_Web_Item"] = $"{Target.Classify}\\{Target.PageFolder}\\Item.aspx";
            //Target["File_Web_Details"] = $"{Target.Classify}\\{Target.PageFolder}\\Details.aspx";

            //Target["File_Model_Business"] = $"{Target.Classify}\\{Target.PageFolder}BusinessLogic";
            //Target["File_Model_Action"] = $"Page\\{Target.Classify}\\{Target.PageFolder}\\{Target.Name}PageAction";
        }

        #region 字段同步


        private bool _inPropertiesCollectionChanged;
        private void OnPropertiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (WorkContext.IsNoChangedNotify || _inPropertiesCollectionChanged)
                return;
            if (!(sender is EntityConfig entity))
                return;
            try
            {
                _inPropertiesCollectionChanged = true;

                if (!entity.IsModify)
                    entity.IsModify = true;

            }
            finally
            {
                _inPropertiesCollectionChanged = false;
            }
        }

        #endregion

    }
}


