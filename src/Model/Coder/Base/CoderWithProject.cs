using Agebull.EntityModel.Config;
using System.Collections.Generic;

namespace Agebull.EntityModel.RobotCoder
{

    /// <summary>
    ///     数据配置对象操作基类
    /// </summary>
    public abstract class CoderWithProject : FileCoder
    {
        /// <summary>
        /// 当前对象
        /// </summary>
        public override ConfigBase CurrentConfig => Project;

        /// <summary>
        ///     是否可写
        /// </summary>
        protected override bool CanWrite => Project != null && !Project.IsFreeze && !Project.IsDiscard;

        /// <summary>
        ///     数据库配置对象
        /// </summary>
        public virtual ProjectConfig Project { get; set; }

        /// <summary>
        ///     表配置集合
        /// </summary>
        public IEnumerable<EntityConfig> Entities => Project.Entities;

        /// <summary>
        ///     命名空间
        /// </summary>
        public string NameSpace => Project.NameSpace;

    }
}