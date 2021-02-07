using System;
using System.Linq;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 属性配置触发器
    /// </summary>
    public class FieldTrigger : ConfigTriggerBase<FieldConfig>
    {
        protected override void OnLoad()
        {
            if (!string.IsNullOrWhiteSpace(TargetConfig?.LinkField))
            {
                TargetConfig.Option.IsLink = true;
            }
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            if (TargetConfig?.Entity == null)
                return;
            switch (property)
            {
                case nameof(TargetConfig.Name):
                case nameof(TargetConfig.DbFieldName):
                    SyncLinkField(field => field.LinkField = TargetConfig.Name);
                    break;
                case nameof(TargetConfig.CanEmpty):
                    if (!TargetConfig.CanEmpty)
                        TargetConfig.IsRequired = true;
                    break;
                case nameof(TargetConfig.LinkField):
                case nameof(TargetConfig.IsLinkKey):
                case nameof(TargetConfig.IsLinkCaption):
                case nameof(TargetConfig.IsLinkField):
                    CheckLinkField();
                    break;
                case nameof(TargetConfig.Nullable):
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.DbNullable));
                    break;
            }
        }

        private void CheckLinkField()
        {
            if (TargetConfig.IsLinkField && !string.IsNullOrWhiteSpace(TargetConfig.LinkTable))
            {
                var table = GlobalConfig.GetEntity(TargetConfig.LinkTable);
                if (table != null)
                {
                    if (TargetConfig.IsLinkKey)
                    {
                        TargetConfig.LinkField = table.PrimaryField;
                        TargetConfig.Option.IsLink = true;
                        TargetConfig.Option.ReferenceConfig = table.PrimaryColumn;
                        return;
                    }
                    var field = table.Find(TargetConfig.LinkField);
                    if (field != null)
                    {
                        TargetConfig.Option.IsLink = true;
                        TargetConfig.Option.ReferenceConfig = field;
                        return;
                    }
                }
            }

            TargetConfig.Option.IsLink = false;
            TargetConfig.Option.ReferenceConfig = null;
        }

        private void SyncLinkField(Action<FieldConfig> action)
        {
            string saveTable = TargetConfig.Entity.SaveTableName;
            string name = TargetConfig.Entity.Name;
            foreach (var entity in SolutionConfig.Current.Entities.Where(p => p != TargetConfig.Entity))
            {
                foreach (var field in entity.Properties.Where(p => p.IsLinkField &&
                    (p.LinkTable == saveTable || p.LinkTable == name) &&
                    (p.LinkField == TargetConfig.DbFieldName || p.LinkField == TargetConfig.Name)))
                {
                    action(field);
                }
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
            if (TargetConfig?.Entity == null)
                return;
            switch (property)
            {
                case nameof(TargetConfig.CsType):
                    //if(string.IsNullOrWhiteSpace(newValue))
                    break;
                case nameof(TargetConfig.DbType):
                    DataTypeHelper.ToStandardByDbType(TargetConfig, newValue?.ToString());
                    //SyncLinkField(field => field.DbType = TargetConfig.DbType);
                    break;
                case nameof(TargetConfig.DbNullable):
                    SyncLinkField(field => field.DbNullable = true);
                    break;
            }
        }
    }
}