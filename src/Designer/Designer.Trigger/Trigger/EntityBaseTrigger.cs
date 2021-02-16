using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.V2021;
using System.IO;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 实体配置触发器
    /// </summary>
    public sealed class EntityBaseTrigger : ConfigTriggerBase<EntityConfigBase>
    {
        protected override void OnAdded(object config)
        {
            if (Target.EnableDataBase)
            {
                var property = config as IPropertyConfig;
                var dbField = new DataBaseFieldConfig
                {
                    Property = property
                };
                dbField.Copy(Target);
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
                case nameof(Target.EnableDataBase):
                    if (Target.EnableDataBase)
                    {
                        ConfigLoader.LoadDataTable(Target as IEntityConfig, Path.GetDirectoryName(Target.SaveFileName));
                        if (Target.DataTable == null)
                        {
                            Target.DataTable = new DataTableConfig();
                            Target.DataTable.Upgrade();
                        }
                    }
                    else
                    {
                        ConfigWriter.SaveExtendConfig(Target, Target.DataTable);
                        Target.DataTable = null;
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


