using System.IO;
using System.Linq;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 表操作基类
    /// </summary>
    public abstract class CoderWithEntity : CoderWithProject
    {
        /// <summary>
        /// 取得其它生成器的能力
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <returns></returns>
        public string GetBaseCode<TBuilder>()
            where TBuilder : EntityBuilderBase, new()
        {
            using (WorkModelScope.CreateScope(WorkModel.Coder))
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
        /// 取得其它生成器的能力
        /// </summary>
        /// <typeparam name="TBuilder"></typeparam>
        /// <returns></returns>
        public string GetExtendCode<TBuilder>()
            where TBuilder : EntityBuilderBase, new()
        {
            using (WorkModelScope.CreateScope(WorkModel.Coder))
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
        /// 当前对象
        /// </summary>
        public sealed override ConfigBase CurrentConfig => (ConfigBase)Entity ?? Project;

        /// <summary>
        /// 当前表对象
        /// </summary>
        public EntityConfig Entity
        {
            get => _entity;
            set
            {
                _entity = value;
                if (value == null)
                    return;
                foreach (var property in value.Properties.Where(p => !p.Discard))
                {
                    if (property.IsReference)
                    {
                        var pp = GlobalConfig.GetConfig<PropertyConfig>(property.ReferenceKey);
                        if (pp != null)
                        {
                            property.CopyConfig(pp);
                        }
                    }
                }
            }
        }

        private EntityConfig _entity;

        /// <summary>
        /// 是否可写
        /// </summary>
        protected override bool CanWrite => base.CanWrite && Entity != null && !Entity.IsFreeze && !Entity.Discard;
    }
}