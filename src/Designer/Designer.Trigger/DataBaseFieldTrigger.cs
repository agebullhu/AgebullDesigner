using Agebull.EntityModel.Config;
using Agebull.EntityModel.Config.Mysql;
using Agebull.EntityModel.Config.V2021;
using Agebull.EntityModel.RobotCoder;
using System;
using System.Text;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 属性配置触发器
    /// </summary>
    public sealed class DataBaseFieldTrigger : ConfigTriggerBase<DataBaseFieldConfig>, IEventTrigger
    {
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
                case nameof(TargetConfig.FieldType):
                    DataTypeHelper.ToStandardByDbType(TargetConfig, newValue?.ToString());
                    break;
            }
        }

        /// <summary>
        /// 规整对象
        /// </summary>
        public void Regularize()
        {
            #region FieldType
            if (TargetConfig.DbFieldName.IsMissing())
            {
                TargetConfig.DbFieldName = DataBaseHelper.ToDbFieldName(TargetConfig.Property);
            }
            if (TargetConfig.FieldType.IsMissing())
            {
                DataTypeHelper.ToStandard(TargetConfig.Property);
            }
            if (TargetConfig.CsType.IsMe("string") && !TargetConfig.IsText && !TargetConfig.IsBlob && TargetConfig.Datalen <= 0)
                TargetConfig.Datalen = 200;

            #endregion

            if (TargetConfig.IsText || TargetConfig.IsBlob || TargetConfig.Datalen < 0)
            {
                TargetConfig.Datalen = 0;
                TargetConfig.Property.CanEmpty = true;
                TargetConfig.Property.IsRequired = false;
            }
            else if (TargetConfig.Datalen < 0)
            {
                TargetConfig.Datalen = 0;
            }
            if (!TargetConfig.FieldType.IsOnce("decimal", "number"))
                TargetConfig.Scale = 0;
            if (TargetConfig.IsIdentity)
                TargetConfig.IsReadonly = true;

            #region KeepStorageScreen
            if (TargetConfig.DbInnerField)
                TargetConfig.KeepStorageScreen = StorageScreenType.Read | StorageScreenType.Insert | StorageScreenType.Update;
            else if (TargetConfig.CustomWrite || TargetConfig.IsIdentity || TargetConfig.IsReadonly)
                TargetConfig.KeepStorageScreen |= StorageScreenType.Insert | StorageScreenType.Update;
            else if (TargetConfig.UniqueIndex || TargetConfig.IsPrimaryKey)
                TargetConfig.KeepStorageScreen |= StorageScreenType.Update;

            #endregion
            #region IsLinkField
            if (TargetConfig.IsParent)
            {
                TargetConfig.IsLinkField = false;
                TargetConfig.LinkTable = null;
                TargetConfig.LinkField = null;
                var field = TargetConfig.Entity.DataTable.PrimaryField;
                if (field != null)
                {

                    TargetConfig.DbNullable = field.DbNullable;
                    TargetConfig.FieldType = field.FieldType;
                    TargetConfig.Datalen = field.Datalen;
                    TargetConfig.Scale = field.Scale;
                    TargetConfig.Property.CsType = field.CsType;
                    TargetConfig.Property.DataType = field.DataType;
                }
                TargetConfig.Property.Option.IsLink = false;
                TargetConfig.Property.Option.ReferenceConfig = null;
            }
            else if (TargetConfig.IsLinkKey || TargetConfig.IsLinkCaption)
            {
                TargetConfig.IsLinkField = true;
            }
            else if (TargetConfig.LinkTable.IsPresent() && TargetConfig.LinkField.IsPresent())
            {
                TargetConfig.Option.IsLink = true;
            }
            if (TargetConfig.Property.Max.IsMissing() && TargetConfig.DataType == "String" && TargetConfig.Datalen > 0 && !TargetConfig.IsBlob && !TargetConfig.IsText)
                TargetConfig.Property.Max = TargetConfig.Datalen.ToString();

            if (TargetConfig.IsLinkField)
            {
                var entity = GlobalConfig.GetEntity(TargetConfig.LinkTable);
                if (entity == null)
                {
                    TargetConfig.Property.Option.IsLink = false;
                    TargetConfig.Property.Option.ReferenceConfig = null;
                }
                else
                {
                    FieldConfig field;
                    if (TargetConfig.IsLinkKey)
                    {
                        field = entity.PrimaryColumn.Field;
                    }
                    else if (TargetConfig.IsLinkCaption)
                    {
                        field = entity.CaptionColumn.Field;
                    }
                    else
                    {
                        field = entity.Find(TargetConfig.LinkField);
                    }
                    if (field == null)
                    {
                        TargetConfig.Property.Option.IsLink = false;
                        TargetConfig.Property.Option.ReferenceConfig = null;
                    }
                    else
                    {
                        TargetConfig.LinkField = field.Name;
                        TargetConfig.DbNullable = field.DataBaseField.DbNullable;
                        TargetConfig.FieldType = field.DataBaseField.FieldType;
                        TargetConfig.Datalen = field.DataBaseField.Datalen;
                        TargetConfig.Scale = field.DataBaseField.Scale;
                        TargetConfig.Property.CsType = field.CsType;
                        TargetConfig.Property.DataType = field.DataType;

                        TargetConfig.Property.Option.IsLink = true;
                        TargetConfig.Property.Option.ReferenceConfig = field;
                    }
                }
            }
            #endregion

            if (TargetConfig.DbInnerField)
                TargetConfig.Property.NoProperty = true;

            if (TargetConfig.IsReadonly)
                TargetConfig.Property.IsUserReadOnly = true;
            TargetConfig.Option.State = TargetConfig.Property.Option.State;
        }
    }
}