// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-10-23
// 修改:2014-11-08
// *****************************************************/

namespace Agebull.EntityModel.RobotCoder.CodeTemplate
{
    /// <summary>
    ///     表示代码基元的家族
    /// </summary>
    public enum CodeItemFamily
    {
        /// <summary>
        ///     未分类
        /// </summary>
        None,

        #region 辅助文本

        /// <summary>
        ///     注释
        /// </summary>
        Rem,

        /// <summary>
        ///     空白
        /// </summary>
        Space,

        /// <summary>
        ///     转义字符
        /// </summary>
        Sharp,

        #endregion

        #region 值
        /// <summary>
        /// 常量
        /// </summary>
        Constant,


        #endregion

        #region 关键字

        /// <summary>
        ///     其它关键字
        /// </summary>
        KeyWord,


        /// <summary>
        ///  运算符
        /// </summary>
        Operator,

        /// <summary>
        ///     条件关键字
        /// </summary>
        Condition,

        /// <summary>
        ///     迭代关键字
        /// </summary>
        Iterator,

        #endregion

        #region 控制符


        /// <summary>
        ///  变量范围
        /// </summary>
        Scope,


        /// <summary>
        ///     控制关键字
        /// </summary>
        Control,

        #endregion
        #region 语句
        /// <summary>
        ///     表定义
        /// </summary>
        TableDefault,


        /// <summary>
        ///     值语句
        /// </summary>
        ValueSentence,

        /// <summary>
        ///     赋值
        /// </summary>
        SetValue,

        /// <summary>
        /// 分隔符
        /// </summary>
        Separator,

        /// <summary>
        ///     逻辑
        /// </summary>
        Logical,

        /// <summary>
        ///     比较
        /// </summary>
        Compare,

        /// <summary>
        ///     计算
        /// </summary>
        Compute,

        /// <summary>
        ///     调用
        /// </summary>
        Call,

        #endregion

        #region 变量
        /// <summary>
        ///     变量
        /// </summary>
        Variable,
        #endregion

        #region 区域

        /// <summary>
        ///     区域
        /// </summary>
        Range,

        /// <summary>
        ///     括号区域
        /// </summary>
        BracketRange,

        /// <summary>
        ///     区域
        /// </summary>
        Block,


        /// <summary>
        ///     表定义区域
        /// </summary>
        TableRange,

        /// <summary>
        ///     方法区域
        /// </summary>
        FunctionRange,

        /// <summary>
        ///     条件区域
        /// </summary>
        ConditionRange,

        /// <summary>
        ///     迭代区域
        /// </summary>
        IteratorRange,

        #endregion
    }
}
