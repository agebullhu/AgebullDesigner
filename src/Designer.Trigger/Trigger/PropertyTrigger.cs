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
                    SyncLinkField(field => field.LinkField = TargetConfig.DbFieldName);
                    break;
                case nameof(TargetConfig.CanEmpty):
                    if (!TargetConfig.CanEmpty)
                        TargetConfig.IsRequired = true;
                    break;
                case nameof(TargetConfig.LinkField):
                    if (!string.IsNullOrWhiteSpace(TargetConfig.LinkField))
                    {
                        TargetConfig.Option.IsLink = true;
                    }
                    else if(!TargetConfig.Option.IsReference)
                    {
                        TargetConfig.Option.ReferenceConfig = null;
                    }
                    break;
                case nameof(TargetConfig.Nullable):
                    TargetConfig.RaisePropertyChanged(nameof(TargetConfig.DbNullable));
                    break;
            }
        }

        private void SyncLinkField(Action<FieldConfig> action)
        {
            string saveTable = TargetConfig.Entity.SaveTable;
            string name = TargetConfig.Entity.Name;
            foreach (var entity in SolutionConfig.Current.Entities.Where(p => p != TargetConfig.Entity))
            {
                foreach (var field in entity.Properties)
                {
                    if ((field.IsLinkField || field.IsLinkKey || field.IsLinkCaption) &&//连接类型
                        (field.LinkTable == saveTable || field.LinkTable == name) &&//表
                        (field.LinkField == TargetConfig.DbFieldName || field.LinkField == TargetConfig.Name))//字段
                        //field.Option.ReferenceKey = TargetConfig.Key;
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