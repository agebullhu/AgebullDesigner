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
    public sealed class PropertyTrigger : ConfigTriggerBase<FieldConfigBase>, IEventTrigger
    {
        public override void OnAdded(object parent)
        {
            var property = TargetConfig as IPropertyConfig;
            CheckDbField((IEntityConfig)parent, property);
        }

        public static void CheckDbField(IEntityConfig parent, IPropertyConfig property)
        {
            if (property.DataBaseField == null)
            {
                parent.DataTable.Add(property.DataBaseField = new DataBaseFieldConfig
                {
                    Property = property,
                    Parent = parent.DataTable,
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
            if (!parent.EnableDataBase)
            {
                property.DataBaseField.IsDiscard = true;
            }
        }

        /// <summary>
        /// 规整对象
        /// </summary>
        public void Regularize()
        {
            var property = TargetConfig.Me;
            if (!property.NoStorage)
            {
                if (property.DataBaseField.DbInnerField)
                    property.NoProperty = true;
                if (!property.DataBaseField.DbNullable)
                {
                    property.Nullable = false;
                    if (property.CsType.IsMe("string"))
                        property.CanEmpty = false;
                }
                if (property.CsType.IsMe("string"))
                {
                    if (property.DataBaseField.FixedLength)
                        property.Max = property.Min = property.DataBaseField.Datalen.ToString();
                    else if (property.DataBaseField.Datalen > 0)
                        property.Max = property.DataBaseField.Datalen.ToString();
                }
            }
            if (property.NoProperty)
            {
                property.CanSet = false;
                property.CanGet = false;
            }

            if (property.IsPrivateField || property.IsMiddleField || property.NoneJson)
            {
                property.InnerField = true;
            }

            if (property.ComputeGetCode.IsPresent() || property.ComputeSetCode.IsPresent())
            {
                property.IsCustomCompute = true;
            }
            if (property.IsCustomCompute)
            {
                property.IsCompute = true;
            }

            if (property.NoProperty || property.InnerField || property.IsCompute || !property.CanSet)
                property.IsUserReadOnly = true;

            if (!property.CsType.IsMe("string"))
            {
                property.Rows = 0;
                property.MulitLine = false;
            }
            if (property.CsType.IsMe("string") && (!property.CanEmpty || property.IsRequired))
                property.UiRequired = true;

            if (!property.UserSee)
            {
                property.NoneGrid = true;
                property.NoneDetails = true;
                property.GridDetails = false;
                property.IsUserReadOnly = true;
            }
        }
    }
}