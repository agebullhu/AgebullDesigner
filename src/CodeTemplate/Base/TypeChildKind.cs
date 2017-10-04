using System;

namespace Agebull.CodeRefactor
{
    /// <summary>
    ///     类型子级种类
    /// </summary>
    public enum TypeChildKind
    {
        /// <summary>
        ///     字段
        /// </summary>
        Field,

        /// <summary>
        ///     属性
        /// </summary>
        Property,

        /// <summary>
        ///     方法
        /// </summary>
        Method,

        /// <summary>
        ///     构造
        /// </summary>
        Constructor,

        /// <summary>
        ///     事件
        /// </summary>
        Event,

        /// <summary>
        ///     枚举
        /// </summary>
        EnumField
    }


}