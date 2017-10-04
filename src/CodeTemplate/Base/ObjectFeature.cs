using System;

namespace Agebull.CodeRefactor
{
    /// <summary>
    ///     对象特点
    /// </summary>
    [Flags]
    public enum ObjectFeature
    {
        None = 0x0,

        Abstract = 0x1,

        Override = 0x2,

        Virtual = 0x4,

        New = 0x8,

        Static = 0x10,

        Const = 0x20,

        ReadOnly = 0x40,

        Ref = 0x80,

        Out = 0x100,

        Sealed = 0x200,

        Explicit = 0x400,

        Implicit = 0x800,

        Extern = 0x1000,

        Partial = 0x2000,

        Unchecked = 0x4000,

        Unsafe = 0x8000,

        Volatile = 0x10000,

        Lock = 0x20000,

        Stackalloc = 0x40000,

        Params = 0x80000,

        In = 0x100000,

        Operator = 0x200000,

        /// <summary>
        /// 表示堆栈变量
        /// </summary>
        StackVariable = 0x400000
    }
}