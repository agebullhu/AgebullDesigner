
using System;
using System.Linq;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// TargetConfig������
    /// </summary>
    public class CodeGeneratorTrigger : EventTrigger
    {
        private ConfigBase config;
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        public override void OnCodeGeneratorBegin(NotificationObject obj)
        {
            config = (ConfigBase)obj;
            config.Foreach<EntityConfig>(CreateLast);
        }

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        public void CreateLast(EntityConfig entity)
        {
            entity.LastProperties = new System.Collections.Generic.List<PropertyConfig>();
            InterfaceHelper.CheckInterface(entity);
            //DataTypeHelper.ToStandard(entity);
            int idx = 0;
            foreach (var pro in entity.Properties.OrderBy(p => p.Index))
            {
                if (pro.IsDelete || pro.IsDiscard)
                    continue;
                pro.Option.Index = ++idx;
                entity.LastProperties.Add(pro);
            }
        }

        /// <summary>
        /// ��ɴ�������
        /// </summary>
        public override void OnCodeGeneratorEnd()
        {
            config.Foreach<EntityConfig>(entity => entity.LastProperties = null);
        }
    }
}