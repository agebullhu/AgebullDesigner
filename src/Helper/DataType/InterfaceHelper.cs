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
                if (!(iField.Option.ReferenceConfig is PropertyConfig refField) ||
                    !refField.Parent.IsInterface || interfaces.Contains(refField.Parent.Name))
                {
                    continue;
                }
                iField.Option.IsDiscard = true;
            }
            foreach (var inf in interfaces)
            {
                var ie = GlobalConfig.GetEntity(inf);
                if (ie == null) continue;
                foreach (var iField in ie.Properties.ToArray())
                {
                    var field = entity.Properties.FirstOrDefault(p => p.ReferenceKey == iField.Key || p.Name == iField.Name);
                    if (field == null)
                    {
                        field = new PropertyConfig
                        {
                            Option = new ConfigDesignOption
                            {
                                ReferenceKey = iField.Key
                            }
                        };
                        field.CopyFromProperty(iField,false,true,false);
                        entity.Add(field);
                    }
                    else
                    {
                        field.Option.ReferenceConfig = iField;
                    }
                    field.Option.IsDiscard = false;
                    field.Option.IsReference = true;
                }
            }
        }
    }
}