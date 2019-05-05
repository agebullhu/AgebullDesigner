
using System;
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
            config.Foreach<EntityConfig>(p=> p.CreateLast());
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