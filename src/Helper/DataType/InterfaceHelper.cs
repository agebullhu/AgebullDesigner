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
            foreach (var inf in interfaces)
            {
                var ie = GlobalConfig.GetEntity(inf);
                if (ie == null) continue;
                foreach (var iField in ie.Properties)
                {
                    var field = entity.Properties.FirstOrDefault(p => p.ReferenceKey == iField.Key || p.Name == iField.Name);
                    if (field == null)
                    {
                        entity.Add(field = new PropertyConfig());
                    }
                    field.CopyFromProperty(iField,false, false, false);
                    field.Option.ReferenceConfig = iField;
                }
            }
        }
    }
}