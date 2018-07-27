using System.IO;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// ���������
    /// </summary>
    public abstract class CoderWithEntity : CoderWithProject
    {
        /// <summary>
        /// ȡ������������������
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <returns></returns>
        public string GetBaseCode<TBuilder>()
            where TBuilder : EntityBuilderBase, new()
        {
            using (CodeGeneratorScope.CreateScope())
            {
                var builder = new TBuilder
                {
                    Entity = Entity,
                    Project = Project
                };
                return builder.BaseCode;
            }
        }
        /// <summary>
        /// ȡ������������������
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <returns></returns>
        public string GetExtendCode<TBuilder>()
            where TBuilder : EntityBuilderBase, new()
        {
            using (CodeGeneratorScope.CreateScope())
            {
                var builder = new TBuilder
                {
                    Entity = Entity,
                    Project = Project
                };
                return builder.ExtendCode;
            }
        }

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public sealed override ConfigBase CurrentConfig => (ConfigBase)Entity ?? Project;

        /// <summary>
        /// ��ǰ�����
        /// </summary>
        public EntityConfig Entity { get; set; }

        /// <summary>
        /// �Ƿ��д
        /// </summary>
        protected override bool CanWrite => base.CanWrite && Entity != null && !Entity.IsFreeze && !Entity.IsDiscard;
    }
}