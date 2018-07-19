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

        public static void JoinInterface(EntityConfig entity)
        {
            if (entity.Interfaces == null)
                return;
            foreach (var inf in entity.Interfaces.Split(NameHelper.SplitChar, StringSplitOptions.RemoveEmptyEntries))
            {
                JoinInterface(entity,inf);
            }
        }
        public static void JoinInterface(EntityConfig entity, string inf)
        {
            var friend = GlobalConfig.GetEntity(inf);
            if (friend == null)
                return;
            foreach (var field in friend.Properties)
            {
                if (entity.LastProperties.Any(p => p.ReferenceKey == field.Key))
                    continue;
                var cpy = new PropertyConfig();
                cpy.Copy(field);
                cpy.Option.IsReference = true;
                cpy.ReferenceKey = field.Key;
                cpy.IsInterfaceField = true;
                cpy.Group = inf;
                entity.LastProperties.Add(cpy);
            }
        }
    }
}