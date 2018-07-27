
using System;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// TargetConfig触发器
    /// </summary>
    public class CodeGeneratorTrigger : EventTrigger
    {

        /// <summary>
        /// 开始代码生成
        /// </summary>
        public override void OnCodeGeneratorBegin()
        {
            SolutionConfig.Current.Foreach<EntityConfig>(CreateLast);
        }
        /// <summary>
        /// 完成代码生成
        /// </summary>
        public override void OnCodeGeneratorEnd()
        {
            SolutionConfig.Current.Foreach<EntityConfig>(entity => entity.LastProperties = null);
        }


        /// <summary>
        /// 完成代码生成
        /// </summary>
        public void CreateLast(EntityConfig entity)
        {
            entity.LastProperties = new System.Collections.Generic.List<PropertyConfig>();
            InterfaceHelper.JoinInterface(entity);
            DataTypeHelper.ToStandard(entity);
            foreach (var pro in entity.Properties)
            {
                if (pro.IsDelete || pro.IsDiscard)
                    continue;
                entity.LastProperties.Add(pro.Option.LastConfig as PropertyConfig);
            }
        }

        void CheckInterfaces()
        {

        }
    }
}