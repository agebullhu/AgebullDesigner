using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.V2021;
using System.IO;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置触发器
    /// </summary>
    public sealed class EntityBaseTrigger : ConfigTriggerBase<EntityConfigBase>, IEventTrigger
    {
        public override void OnAdded(object config)
        {
            if (TargetConfig.EnableDataBase)
            {
                var property = config as IPropertyConfig;
                var dbField = new DataBaseFieldConfig
                {
                    Property = property
                };
                dbField.Copy(TargetConfig);
                property.DataBaseField = dbField;
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
                    /*case nameof(Target.EnableUI):
                        if (Target.EnableUI)
                        {
                            ConfigLoader.LoadPage(Target as IEntityConfig, Path.GetDirectoryName(Target.SaveFileName));
                            if (Target.Page == null)
                            {
                                Target.Page = new PageConfig();
                                Target.Page.Upgrade();
                            }
                        }
                        else
                        {
                            ConfigWriter.SaveExtendConfig(Target, Target.Page);
                            Target.Page = null;
                        }
                        break;*/
            }
        }

    }
}


