
using System;
using System.Collections.Generic;
using System.Linq;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// TargetConfig触发器
    /// </summary>
    public class CodeGeneratorTrigger : EventTrigger
    {
        private ConfigBase config;
        /// <summary>
        /// 开始代码生成
        /// </summary>
        public override void OnCodeGeneratorBegin(NotificationObject obj)
        {
            config = (ConfigBase)obj;
            config.Foreach<EntityConfig>(CreateLast);
        }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        public void CreateLast(EntityConfig entity)
        {
            entity.LastProperties = new List<PropertyConfig>();
            int idx = 0;
            foreach (var pro in entity.Properties.OrderBy(p => p.Index))
            {
                if (pro.IsDelete || pro.IsDiscard)
                    continue;
                pro.Option.Index = ++idx;
                entity.LastProperties.Add(pro);
            }
            InterfaceHelper.CheckInterface(entity, entity.LastProperties);
        }

        /// <summary>
        /// 完成代码生成
        /// </summary>
        public override void OnCodeGeneratorEnd()
        {
            config.Foreach<EntityConfig>(entity => entity.LastProperties = null);
        }
    }
}