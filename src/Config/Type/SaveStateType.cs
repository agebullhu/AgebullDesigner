using System;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 配置状态类型
    /// </summary>
    [Flags]
    public enum ConfigStateType
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0x0,
        /// <summary>
        /// 已删除
        /// </summary>
        Delete = 0x2,
        /// <summary>
        /// 已锁定
        /// </summary>
        Freeze = 0x4,
        /// <summary>
        /// 已废弃
        /// </summary>
        Discard = 0x8,
        /// <summary>
        /// 引用
        /// </summary>
        /// <remarks>
        /// 来源引用对象,不可更改
        /// </remarks>
        Reference = 0x10,
        /// <summary>
        /// 引用
        /// </summary>
        /// <remarks>
        /// 预定义对象,不可更改
        /// </remarks>
        Predefined = 0x20,
        /// <summary>
        /// 引用
        /// </summary>
        /// <remarks>
        /// 锁定
        /// </remarks>
        Lock = 0x10000
    }
}