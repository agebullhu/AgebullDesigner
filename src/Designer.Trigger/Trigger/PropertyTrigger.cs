using System;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// �������ô�����
    /// </summary>
    public class PropertyTrigger : ConfigTriggerBase<PropertyConfig>
    {
        protected override void OnLoad()
        {
            if (!string.IsNullOrEmpty(TargetConfig?.LinkField))
            {
                TargetConfig.Option.IsLink = true;
            }
        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            if (TargetConfig?.Parent == null)
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
                    if (!string.IsNullOrEmpty(TargetConfig.LinkField))
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

        private void SyncLinkField(Action<PropertyConfig> action)
        {
            string saveTable = TargetConfig.Parent.SaveTable;
            string name = TargetConfig.Parent.Name;
            foreach (var entity in SolutionConfig.Current.Entities.Where(p => p != TargetConfig.Parent))
            {
                foreach (var field in entity.Properties)
                {
                    if ((field.LinkTable == saveTable || field.LinkTable == name) &&
                        (field.IsLinkField || field.IsLinkKey || field.IsLinkCaption) &&
                        (field.LinkField == TargetConfig.DbFieldName || field.LinkField == TargetConfig.Name))

                        action(field);

                }
            }
        }
        /// <summary>
        ///     ���������޸�ǰ�¼�
        /// </summary>
        /// <param name="property">����</param>
        /// <param name="oldValue">��ֵ</param>
        /// <param name="newValue">��ֵ</param>
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