using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.V2021;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace Agebull.EntityModel.Designer
{

    /// <summary>
    /// 实体配置触发器
    /// </summary>
    public sealed class EntityBaseTrigger : ParentConfigTrigger<EntityConfigBase>
    {
        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            switch (property)
            {
                case nameof(TargetConfig.EnableDataBase):
                    if (TargetConfig.EnableDataBase)
                    {
                        ConfigLoader.LoadDataTable(TargetConfig as IEntityConfig, Path.GetDirectoryName(TargetConfig.SaveFileName));
                        if (TargetConfig.DataTable == null)
                        {
                            TargetConfig.DataTable = new DataTableConfig();
                            TargetConfig.DataTable.Upgrade();
                        }
                    }
                    else
                    {
                        ConfigWriter.SaveExtendConfig(TargetConfig, TargetConfig.DataTable);
                        TargetConfig.DataTable = null;
                    }
                    break;
                    /*case nameof(TargetConfig.EnableUI):
                        if (TargetConfig.EnableUI)
                        {
                            ConfigLoader.LoadPage(TargetConfig as IEntityConfig, Path.GetDirectoryName(TargetConfig.SaveFileName));
                            if (TargetConfig.Page == null)
                            {
                                TargetConfig.Page = new PageConfig();
                                TargetConfig.Page.Upgrade();
                            }
                        }
                        else
                        {
                            ConfigWriter.SaveExtendConfig(TargetConfig, TargetConfig.Page);
                            TargetConfig.Page = null;
                        }
                        break;*/
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

        }

    }

    /// <summary>
    /// 实体配置触发器
    /// </summary>
    public sealed class EntityTrigger : ParentConfigTrigger<EntityConfig>
    {

        protected override void OnLoad()
        {
            foreach (var field in TargetConfig.Properties)
            {
                GlobalTrigger.OnLoad(field);
            }
            TargetConfig.MaxIdentity = TargetConfig.Properties.Count == 0 ? 0 : TargetConfig.Properties.Max(p => p.Identity);
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            switch (property)
            {
                case nameof(TargetConfig.Name):
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.DisplayName));
                    break;
                case nameof(TargetConfig.SaveTableName):
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.ReadTableName));
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.DisplayName));
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.SaveTableName));
                    break;
                case nameof(TargetConfig.ReadTableName):
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.SaveTableName));
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.DisplayName));
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.SaveTableName));
                    break;
                case nameof(TargetConfig.Classify):
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
                //case nameof(TargetConfig.Name):
                //case nameof(TargetConfig.ReadTableName):
                //case nameof(TargetConfig.SaveTableName):
                //    SyncLinkTable(oldValue, newValue);
                //    break;
                case nameof(TargetConfig.Classify):
                    if (TargetConfig.Project == null || WorkContext.WorkModel == WorkModel.Repair)
                        return;
                    var oldCls = TargetConfig.Project.Classifies.FirstOrDefault(p => p.Name == (string)oldValue);
                    oldCls?.Items.Remove(TargetConfig);
                    var newoldCls = TargetConfig.Project.Classifies.FirstOrDefault(p => p.Name == (string)newValue);
                    newoldCls?.Items.TryAdd(TargetConfig);
                    break;
                case nameof(TargetConfig.Properties):
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
            //TargetConfig.ExtendConfig.Clear();
            //TargetConfig["File_Web_Index"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\Index.aspx";
            //TargetConfig["File_Web_Action"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\Action.aspx";
            //TargetConfig["File_Web_Form"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\Form.htm";
            //TargetConfig["File_Web_Script"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\script.js";
            //TargetConfig["File_Web_Item"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\Item.aspx";
            //TargetConfig["File_Web_Details"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\Details.aspx";

            //TargetConfig["File_Model_Business"] = $"{TargetConfig.Classify}\\{TargetConfig.PageFolder}BusinessLogic";
            //TargetConfig["File_Model_Action"] = $"Page\\{TargetConfig.Classify}\\{TargetConfig.PageFolder}\\{TargetConfig.Name}PageAction";
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


