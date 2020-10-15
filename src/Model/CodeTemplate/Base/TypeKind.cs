using System;

namespace Agebull.CodeRefactor
{
    /// <summary>
    ///     类型种类
    /// </summary>
    public enum TypeKind
    {
        /// <summary>
        ///     类
        /// </summary>
        Class,

        /// <summary>
        ///     结构
        /// </summary>
        Struct,

        /// <summary>
        ///     接口
        /// </summary>
        Interface,

        /// <summary>
        ///     枚举
        /// </summary>
        Enum,

        /// <summary>
        ///     委托
        /// </summary>
        Delegate
    }
}