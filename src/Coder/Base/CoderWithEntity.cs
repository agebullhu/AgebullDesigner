using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// ���������
    /// </summary>
    public abstract class CoderWithEntity : CoderWithProject
    {
        /// <summary>
        /// ��ǰ����
        /// </summary>
        public sealed override ConfigBase CurrentConfig => (ConfigBase)Entity ?? base.Project;

        /// <inheritdoc />
        public sealed override ProjectConfig Project
        {
            get => Entity?.Parent ?? base.Project;
            set => base.Project = value;
        }

        /// <summary>
        /// ��ǰ�����
        /// </summary>
        public EntityConfig Entity { get; set; }

        /// <summary>
        /// �Ƿ��д
        /// </summary>
        protected override bool CanWrite => base.CanWrite && Entity != null && !Entity.IsFreeze && !Entity.IsDiscard;

        /// <summary>
        /// ȡ������������������
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <returns></returns>
        public string GetBaseCode<TBuilder>()
            where TBuilder : EntityBuilderBase, new()
        {
            using (CodeGeneratorScope.CreateScope(Entity))
            {
                var builder = new TBuilder
                {
                    Entity = Entity
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
            using (CodeGeneratorScope.CreateScope(Entity))
            {
                var builder = new TBuilder
                {
                    Entity = Entity
                };
                return builder.ExtendCode;
            }
        }

    }
}