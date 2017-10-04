// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Gboxt.Common.SimpleDataAccess
// 建立:2014-12-03
// 修改:2014-12-03
// *****************************************************/

#region 引用

using System;

#endregion

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     字段使用范围类型
    /// </summary>
    [Flags]
    public enum AccessScopeType
    {
        /// <summary>
        /// 不公开
        /// </summary>
        None = 0x0,
        /// <summary>
        /// 客户端
        /// </summary>
        Client = 0x1,
        /// <summary>
        /// 服务端
        /// </summary>
        Server = 0x2,
        /// <summary>
        /// 全公开
        /// </summary>
        All = 0x3
    }

    /// <summary>
    /// 存储场景类型
    /// </summary>
    [Flags]
    public enum StorageScreenType
    {
        None=0x0,
        Insert=0x1,
        Update=0x2,
        All= Insert| Update
    }
}