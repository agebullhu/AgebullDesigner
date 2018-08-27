using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            foreach (var inf in interfaces)
            {
                var ie = GlobalConfig.GetEntity(inf);
                if (ie != null)
                {
                    foreach (var field in ie.Properties)
                    {
                        var old = entity.Properties.FirstOrDefault(p => p.ReferenceKey == field.Key || p.Name == field.Name);
                        if (old == null)
                            entity.Add(old = new PropertyConfig
                            {
                                Name = field.Name,
                                Caption = field.Caption
                            });
                        old.CopyFrom(field);
                        old.ReferenceKey = field.Key;
                        old.Option.IsReference = true;
                        old.Option.ReferenceConfig = field;
                    }
                }
            }
        }
    }
}