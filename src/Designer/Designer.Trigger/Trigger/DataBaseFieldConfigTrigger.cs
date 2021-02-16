using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.V2021;
using Agebull.EntityModel.RobotCoder;
using System;
using System.Linq;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 属性配置触发器
    /// </summary>
    public sealed class DataBaseFieldConfigTrigger : ConfigTriggerBase<DataBaseFieldConfig>
    {
        protected override void OnLoad()
        {
            if (Target.LinkTable.IsPresent() && Target.LinkField.IsPresent())
            {
                Target.Option.IsLink = true;
            }
        }

        /// <summary>
        /// 属性事件处理
        /// </summary>
        /// <param name="property"></param>
        protected override void OnPropertyChangedInner(string property)
        {
            if (Target?.Entity == null)
                return;
            switch (property)
            {
                case nameof(Target.Name):
                case nameof(Target.DbFieldName):
                    SyncLinkField(field => field.LinkField = Target.Name);
                    break;
                //case nameof(Target.CanEmpty):
                //    if (!Target.CanEmpty)
                //        Target.IsRequired = true;
                //    break;
                case nameof(Target.LinkField):
                case nameof(Target.IsLinkKey):
                case nameof(Target.IsLinkCaption):
                case nameof(Target.IsLinkField):
                    CheckLinkField();
                    break;
                    //case nameof(Target.Nullable):
                    //    Target.RaisePropertyChanged(nameof(Target.DbNullable));
                    //    break;
            }
        }

        private void CheckLinkField()
        {
            if (Target.IsLinkField && !string.IsNullOrWhiteSpace(Target.LinkTable))
            {
                var table = GlobalConfig.GetEntity(Target.LinkTable);
                if (table != null)
                {
                    if (Target.IsLinkKey)
                    {
                        Target.LinkField = table.PrimaryField;
                        Target.Option.IsLink = true;
                        Target.Option.ReferenceConfig = table.PrimaryColumn;
                        return;
                    }
                    var field = table.Find(Target.LinkField);
                    if (field != null)
                    {
                        Target.Option.IsLink = true;
                        Target.Option.ReferenceConfig = field;
                        return;
                    }
                }
            }

            Target.Option.IsLink = false;
            Target.Option.ReferenceConfig = null;
        }

        private void SyncLinkField(Action<DataBaseFieldConfig> action)
        {
            string saveTable = Target.Parent.SaveTableName;
            string name = Target.Entity.Name;
            foreach (var entity in SolutionConfig.Current.Entities.Where(p => p != Target.Entity))
            {
                foreach (var field in entity.DataTable.FindAndToArray(p => p.IsLinkField &&
                    (p.LinkTable == saveTable || p.LinkTable == name) &&
                    (p.LinkField == Target.DbFieldName || p.LinkField == Target.Name)))
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
            if (Target?.Entity == null)
                return;
            switch (property)
            {
                case nameof(Target.CsType):
                    //if(string.IsNullOrWhiteSpace(newValue))
                    break;
                case nameof(Target.FieldType):
                    DataTypeHelper.ToStandardByDbType(Target, newValue?.ToString());
                    //SyncLinkField(field => field.DbType = Target.DbType);
                    break;
                case nameof(Target.DbNullable):
                    SyncLinkField(field => field.DbNullable = true);
                    break;
            }
        }
    }
}