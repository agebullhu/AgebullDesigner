using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 数据类型辅助类
    /// </summary>
    public class InterfaceHelper
    {
        public static void CheckInterface(EntityConfig entity, IList<IFieldConfig> properties)
        {
            if (string.IsNullOrWhiteSpace(entity.Interfaces))
                return;
            var interfaces = entity.Interfaces.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var inf in interfaces)
            {
                var ie = GlobalConfig.GetEntity(inf);
                if (ie == null)
                    continue;
                foreach (var iField in ie.Properties.ToArray())
                {
                    if (entity.Properties.Any(p => p.Name == iField.Name))
                        continue;
                    var field = new FieldConfig();
                    field.Copy(iField);
                    field.LinkField = iField.Name;
                    field.LinkTable = ie.Name;
                    field.ReferenceKey = iField.Key;
                    field.Entity = entity;
                    properties.Add(field);
                }
            }
        }

        public static void ClearInterface(EntityConfig entity)
        {
            var interfaces = entity.Interfaces.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var iField in entity.Properties.ToArray())
            {
                if (iField.IsInterfaceField ||
                    iField.Option.ReferenceConfig is FieldConfig refField &&
                    (refField.Entity.IsInterface || interfaces.Contains(refField.Entity.Name)))
                {
                    entity.Remove(iField);
                }
            }
        }
    }
}