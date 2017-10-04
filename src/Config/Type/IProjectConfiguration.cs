using System.Collections.Generic;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    ///     项目配置
    /// </summary>
    public interface IProjectConfiguration
    {
        /// <summary>
        ///     数据库名称
        /// </summary>
        string DataBaseObjectName { get; }

        /// <summary>
        ///     关联的表结构
        /// </summary>
        IEnumerable<EntityConfig> Schemas { get; }

        /// <summary>
        ///     页面代码路径
        /// </summary>
        string PagePath { get; }

        /// <summary>
        ///     模型代码路径
        /// </summary>
        string ModelPath { get; }

        /// <summary>
        ///     代码地址
        /// </summary>
        string CodePath { get; }

        /// <summary>
        ///     命名空间
        /// </summary>
        string NameSpace { get; }

        /// <summary>
        ///     运行时只读
        /// </summary>
        bool ReadOnly { get; }

        /// <summary>
        ///     数据库类型
        /// </summary>
        DataBaseType DbType { get; }

        /// <summary>
        ///     数据库地址
        /// </summary>
        string DbHost { get; }

        /// <summary>
        ///     数据库名称
        /// </summary>
        string DbSoruce { get; }

        /// <summary>
        ///     数据库名称
        /// </summary>
        string DbPassWord { get; }

        /// <summary>
        ///     数据库名称
        /// </summary>
        string DbUser { get; }
        
    }
    
}