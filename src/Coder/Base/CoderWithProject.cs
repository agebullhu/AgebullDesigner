using System.Collections.Generic;
using Gboxt.Common.DataAccess.Schemas;

namespace Agebull.Common.SimpleDesign
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
        protected override bool CanWrite => Project != null && !Project.IsFreeze && !Project.Discard;

        /// <summary>
        ///     数据库配置对象
        /// </summary>
        public ProjectConfig Project { get; set; }

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