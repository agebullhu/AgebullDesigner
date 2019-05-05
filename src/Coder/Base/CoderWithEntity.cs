using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 表操作基类
    /// </summary>
    public abstract class CoderWithEntity : CoderWithProject
    {
        /// <summary>
        /// 当前对象
        /// </summary>
        public sealed override ConfigBase CurrentConfig => (ConfigBase)Entity ?? base.Project;

        /// <inheritdoc />
        public sealed override ProjectConfig Project
        {
            get => Entity?.Parent ?? base.Project;
            set => base.Project = value;
        }

        private EntityConfig _entity;

        /// <summary>
        /// 当前表对象
        /// </summary>
        public EntityConfig Entity
        {
            get => _entity;
            set
            {
                _entity = value;
                _entity?.CreateLast();
            }
        }

        /// <summary>
        /// 是否可写
        /// </summary>
        protected override bool CanWrite => base.CanWrite && Entity != null && !Entity.IsFreeze && !Entity.IsDiscard;

        /// <summary>
        /// 取得其它生成器的能力
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
        /// 取得其它生成器的能力
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