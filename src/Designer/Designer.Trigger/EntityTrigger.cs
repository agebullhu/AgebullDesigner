using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.V2021;
using Agebull.EntityModel.RobotCoder;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置触发器
    /// </summary>
    public sealed class EntityTrigger : EventTrigger<EntityConfigBase>, IEventTrigger
    {

        public override void OnAdded(object config)
        {
            if (TargetConfig.DataTable == null)
            {
                TargetConfig.DataTable = new DataTableConfig();
                TargetConfig.DataTable.Upgrade();
            }
            var property = config as IPropertyConfig;
            CheckDbField(property);
        }

        public override void OnLoad()
        {
            CheckDbFields();
            foreach (var field in TargetConfig.IEntity.Properties)
            {
                GlobalTrigger.OnLoad(field);
            }
            TargetConfig.IEntity.MaxIdentity = TargetConfig.IEntity.Properties.Any() ? TargetConfig.IEntity.Properties.Max(p => p.Identity) : 0;
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        void IEventTrigger.OnPropertyChanged(string property)
        {
            switch (property)
            {
                case nameof(TargetConfig.Name):
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.IEntity.DisplayName));
                    break;
                case nameof(TargetConfig.Classify):
                    OnClassifyChanged();
                    break;
                case nameof(TargetConfig.EnableDataBase):
                    if (TargetConfig.EnableDataBase)
                    {
                        ConfigLoader.LoadDataTable(TargetConfig as IEntityConfig, Path.GetDirectoryName(TargetConfig.SaveFileName));
                        if (TargetConfig.DataTable == null)
                        {
                            TargetConfig.DataTable = new DataTableConfig();
                            TargetConfig.DataTable.Upgrade();
                        }
                        TargetConfig.DataTable.IsDiscard = false;
                    }
                    else
                    {
                        ConfigWriter.SaveExtendConfig(TargetConfig, TargetConfig.DataTable);
                        TargetConfig.DataTable.IsDiscard = true;
                    }
                    CheckDbFields();
                    break;
            }
        }
        void CheckDbFields()
        {
            foreach(var property in TargetConfig.IEntity.Properties)
            {
                CheckDbField(property);
            }
        }
        private void CheckDbField(IPropertyConfig property)
        {
            if (property.DataBaseField == null)
            {
                TargetConfig.DataTable.Add(property.DataBaseField = new DataBaseFieldConfig
                {
                    Property = property,
                    DbFieldName = DataBaseHelper.ToDbFieldName(property)
                });
                property.DataBaseField.Copy(property.Field);
                DataTypeHelper.StandardDataType(property);
            }
            else
            {
                if (property.DataBaseField.DbFieldName.IsMissing())
                {
                    property.DataBaseField.DbFieldName = DataBaseHelper.ToDbFieldName(property);
                }
                if (property.DataBaseField.FieldType.IsMissing())
                {
                    DataTypeHelper.StandardDataType(property);
                }
            }
            if (!TargetConfig.EnableDataBase)
            {
                property.DataBaseField.IsDiscard = true;
            }
        }


        /// <summary>
        ///     发出属性修改前事件
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        void IEventTrigger.BeforePropertyChanged(string property, object oldValue, object newValue)
        {
            switch (property)
            {
                //case nameof(Target.Name):
                //case nameof(Target.ReadTableName):
                //case nameof(Target.SaveTableName):
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

    }
}


