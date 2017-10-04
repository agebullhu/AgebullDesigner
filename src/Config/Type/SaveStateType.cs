using System;

namespace Gboxt.Common.DataAccess.Schemas
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
        /// 引用
        /// </summary>
        /// <remarks>
        /// 来源是导入的第三方对象,不可更改
        /// </remarks>
        IsReference = 0x1,
        /// <summary>
        /// 已删除
        /// </summary>
        IsDelete = 0x2,
        /// <summary>
        /// 已锁定
        /// </summary>
        IsFreeze = 0x4,
        /// <summary>
        /// 已废弃
        /// </summary>
        IsDiscard = 0x8
    }
}