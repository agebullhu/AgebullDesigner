using System;

namespace Agebull.CodeRefactor
{
    /// <summary>
    ///     访问类型
    /// </summary>
    [Flags]
    public enum AccessType
    {
        /// <summary>
        ///     私有
        /// </summary>
        Private,

        /// <summary>
        ///     公开
        /// </summary>
        Public = 0x1,

        /// <summary>
        ///     程序集公开
        /// </summary>
        Internal = 0x2,

        /// <summary>
        ///     保护
        /// </summary>
        Protected = 0x4,

        /// <summary>
        ///     程序集公开和保护
        /// </summary>
        InternalProtected = Internal | Protected
    }
}