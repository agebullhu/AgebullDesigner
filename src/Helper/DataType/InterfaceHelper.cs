using System;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 数据类型辅助类
    /// </summary>
    public class InterfaceHelper
    {
        public static void CheckInterface(EntityConfig entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Interfaces))
                return;
            var interfaces = entity.Interfaces.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var iField in entity.Properties.ToArray())
            {
                if (!iField.IsReference)
                    continue;
                var refField = iField.Option.ReferenceConfig as PropertyConfig;
                if (refField == null || !refField.Parent.IsInterface || interfaces.Contains(refField.Parent.Name))
                {
                    continue;
                }
                iField.Option.IsDiscard = true;
            }
            foreach (var inf in interfaces)
            {
                var ie = GlobalConfig.GetEntity(inf);
                if (ie == null) continue;
                foreach (var iField in ie.Properties)
                {
                    var field = entity.Properties.FirstOrDefault(p => p.ReferenceKey == iField.Key || p.Name == iField.Name);
                    if (field == null)
                    {
                        entity.Add(new PropertyConfig
                        {
                            Option =
                            {
                                IsReference = true,
                                ReferenceConfig = iField
                            }
                        });
                    }
                    else
                    {
                        field.Option.IsDiscard = false;
                        field.Option.IsReference = true;
                        field.Option.ReferenceConfig = iField;
                    }
                }
            }
        }
    }
}