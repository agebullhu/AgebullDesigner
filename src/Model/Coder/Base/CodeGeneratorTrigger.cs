
using System;
using System.Collections.Generic;
using System.Linq;
using Agebull.Common;
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
            config.Foreach<ModelConfig>(CreateLast);
        }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        public void CreateLast(EntityConfig entity)
        {
            entity.LastProperties = new List<IFieldConfig>();
            int idx = 0;
            foreach (var field in entity.Properties.OrderBy(p => p.Index))
            {
                if (field.IsDelete || field.IsDiscard)
                    continue;
                field.Option.Index = ++idx;
                entity.LastProperties.TryAdd(field,p=>p.Key);
                InterfaceHelper.CheckLinkField(entity, field);
            }
            InterfaceHelper.CheckLastInterface(entity,idx);
        }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        public void CreateLast(ModelConfig model)
        {
            model.LastProperties = new List<IFieldConfig>();
            int idx = 0;
            foreach (var property in model.Properties.OrderBy(p => p.Index))
            {
                if (property.IsDelete || property.IsDiscard )
                    continue;
                property.Option.Index = ++idx;
                InterfaceHelper.CheckLinkField(model, property);
                model.LastProperties.TryAdd(property, p => p.Key);
            }
            InterfaceHelper.CheckLastInterface(model, idx);
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