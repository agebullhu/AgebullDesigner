using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// TargetConfig触发器
    /// </summary>
    public class CodeGeneratorTrigger : IEventTrigger
    {
        /// <summary>
        /// 构造
        /// </summary>
        public CodeGeneratorTrigger()
        {
            TargetType = typeof(ConfigBase);
        }

        /// <summary>
        /// 目标类型
        /// </summary>
        public Type TargetType { get; }

        /// <summary>
        /// 目标类型
        /// </summary>
        public ConfigBase Target { get; private set; }

        /// <summary>
        /// 当前配置对象
        /// </summary>
        object IEventTrigger.Target
        {
            get => Target;
            set => Target = value as ConfigBase;
        }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        void IEventTrigger.OnCodeGeneratorBegin(object obj)
        {
            Target = (ConfigBase)obj;
            Target.Preorder<IEntityConfig>(CreateLast);
        }
        /// <summary>
        /// 完成代码生成
        /// </summary>
        void IEventTrigger.OnCodeGeneratorEnd()
        {
            Target.Preorder<IEntityConfig>(ClearLast);
        }


        /// <summary>
        /// 开始代码生成
        /// </summary>
        public void CreateLast(IEntityConfig entity)
        {

            entity.LastProperties = new List<IPropertyConfig>();
            int idx = 0;
            foreach (var property in entity.Properties.OrderBy(p => p.Index))
            {
                if (property.IsDelete || property.IsDiscard)
                    continue;
                property.Option.Index = ++idx;
                entity.LastProperties.TryAdd(property, p => p.Key);
                if (entity.EnableDataBase)
                    InterfaceHelper.CheckLinkField(entity, property);
            }
            InterfaceHelper.CheckLastInterface(entity, idx);
            foreach (var field in entity.DataTable.Fields.ToArray())
            {
                if (field.Property == null || field.Property.IsDelete || field.Property.IsDiscard || !entity.LastProperties.Any(pro => pro == field.Property))
                {
                    field.IsDiscard = true;
                }
                else
                {
                    field.IsDiscard = false;
                }
            }
        }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        public void ClearLast(IEntityConfig entity)
        {
            if (entity.LastProperties == null)
                return;
            foreach (var property in entity.LastProperties.Where(p=> !entity.Exist(pro=> pro == p)))
            {
                entity.DataTable.Fields.Remove(property.DataBaseField);
            }
            entity.LastProperties = null;
        }
    }
}