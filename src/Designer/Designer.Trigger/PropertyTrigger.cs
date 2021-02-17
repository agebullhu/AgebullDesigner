using Agebull.EntityModel.Config;
using System;
using System.Linq;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 属性配置触发器
    /// </summary>
    public sealed class PropertyTrigger : ConfigTriggerBase<FieldConfigBase>, IEventTrigger
    {

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

            if (property.Nullable && property.DataBaseField != null)
            {
                property.DataBaseField.DbNullable = false;
            }
            if (property.ComputeGetCode.IsPresent() || property.ComputeSetCode.IsPresent())
            {
                property.IsCustomCompute = true;
            }
            if (property.IsCustomCompute)
            {
                property.IsCompute = true;
            }
            if (!property.IsCompute && property.DataBaseField != null)
            {
                property.DataBaseField.IsDiscard = true;
            }
            if (property.NoStorage || property.InnerField)
                property.CanUserQuery = false;
            if (property.NoProperty || property.InnerField || property.IsCompute)
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