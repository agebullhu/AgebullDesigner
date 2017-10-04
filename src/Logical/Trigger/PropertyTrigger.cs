using System;
using System.Linq;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    /// 属性配置触发器
    /// </summary>
    public class PropertyTrigger : ConfigBaseTrigger<PropertyConfig>
    {
        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            if (TargetConfig?.Parent == null)
                return;
            switch (property)
            {
                case nameof(TargetConfig.Name):
                case nameof(TargetConfig.ColumnName):
                    SyncLinkField(field => field.LinkField = TargetConfig.ColumnName);
                    break;
                case nameof(TargetConfig.CanEmpty):
                    if (!TargetConfig.CanEmpty)
                        TargetConfig.IsRequired = true;
                    break;
                case nameof(TargetConfig.Nullable):
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.DbNullable));
                    break;
            }
        }

        private void SyncLinkField(Action<PropertyConfig> action)
        {
            string saveTable = TargetConfig.Parent.SaveTable;
            foreach (var entity in SolutionConfig.Current.Entities.Where(p => p != TargetConfig.Parent))
            {
                foreach (var field in entity.Properties)
                {
                    if (field.LinkTable == saveTable && (field.IsLinkField || field.IsLinkKey || field.IsLinkCaption)
                        && (field.LinkField == TargetConfig.ColumnName || field.LinkField == TargetConfig.Name))
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
            if (TargetConfig?.Parent == null)
                return;
            switch (property)
            {
                case nameof(TargetConfig.CsType):
                    //if(string.IsNullOrWhiteSpace(newValue))
                    break;
                case nameof(TargetConfig.DbType):
                    SyncLinkField(field => field.DbType = TargetConfig.DbType);
                    break;
                case nameof(TargetConfig.DbNullable):
                    SyncLinkField(field => field.DbNullable = true);
                    break;
            }
        }
    }
}