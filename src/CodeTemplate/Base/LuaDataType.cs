namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    /// LUA的数据类型
    /// </summary>
    public enum LuaDataType
    {
        /// <summary>
        /// 未知
        /// </summary>
        None,
        /// <summary>
        /// 空
        /// </summary>
        Void,
        /// <summary>
        /// 空
        /// </summary>
        Nil,
        /// <summary>
        /// 布尔
        /// </summary>
        Bool,
        /// <summary>
        /// 数字
        /// </summary>
        Number,
        /// <summary>
        /// 文本
        /// </summary>
        String,
        /// <summary>
        /// 方法类型
        /// </summary>
        Function,
        /// <summary>
        /// 表类型
        /// </summary>
        Table,
        /// <summary>
        /// 迭代类型
        /// </summary>
        Iterator,
        /// <summary>
        /// 多个子级
        /// </summary>
        Mulit,
        /// <summary>
        /// 基础类型
        /// </summary>
        BaseType,
        /// <summary>
        /// 待定
        /// </summary>
        Confirm,
        /// <summary>
        /// 错误
        /// </summary>
        Error
    }
}