using System;
using System.Collections.Generic;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// �������͸�����
    /// </summary>
    public class InterfaceHelper
    {
        public static void CheckInterface(EntityConfig entity, List<PropertyConfig> properties)
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
                    var field = entity.Properties.FirstOrDefault(p => p.ReferenceKey == iField.Key || p.Name == iField.Name);
                    properties.Add(iField);
                }
            }
        }

        public static void ClearInterface(EntityConfig entity)
        {
            var interfaces = entity.Interfaces.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var iField in entity.Properties.ToArray())
            {
                if (iField.IsInterfaceField ||
                    iField.Option.ReferenceConfig is PropertyConfig refField &&
                    (refField.Parent.IsInterface || interfaces.Contains(refField.Parent.Name)))
                {
                    entity.Remove(iField);
                }
            }
        }
    }
}