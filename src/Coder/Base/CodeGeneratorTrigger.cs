
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
            InterfaceHelper.CheckInterface(entity);
            //DataTypeHelper.ToStandard(entity);
            int idx = 0;
            foreach (var pro in entity.Properties.OrderBy(p => p.Index))
            {
                if (pro.IsDelete || pro.IsDiscard)
                    continue;
                var last = pro.Option.LastConfig as PropertyConfig;
                last.Option.Index = ++idx;
                entity.LastProperties.Add(last);
            }
        }
    }
}