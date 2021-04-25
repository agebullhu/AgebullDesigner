using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// TargetConfig������
    /// </summary>
    public class CodeGeneratorTrigger : IEventTrigger
    {
        /// <summary>
        /// ����
        /// </summary>
        public CodeGeneratorTrigger()
        {
            TargetType = typeof(ConfigBase);
        }

        /// <summary>
        /// Ŀ������
        /// </summary>
        public Type TargetType { get; }

        /// <summary>
        /// Ŀ������
        /// </summary>
        public ConfigBase Target { get; private set; }

        /// <summary>
        /// ��ǰ���ö���
        /// </summary>
        object IEventTrigger.Target
        {
            get => Target;
            set => Target = value as ConfigBase;
        }

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        void IEventTrigger.OnCodeGeneratorBegin(object obj)
        {
            Target = (ConfigBase)obj;
            Target.Preorder<IEntityConfig>(CodeGeneratorBegin);
        }

        /// <summary>
        /// ��ɴ�������
        /// </summary>
        void IEventTrigger.OnCodeGeneratorEnd()
        {
            Target.Preorder<IEntityConfig>(OnCodeGeneratorEnd);
        }

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        void CodeGeneratorBegin(IEntityConfig entity)
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
                GlobalTrigger.Regularize(field);
            }
            foreach (var property in entity.LastProperties)
            {
                GlobalTrigger.Regularize(property);
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        void OnCodeGeneratorEnd(IEntityConfig entity)
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