// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-11-02
// 修改:2014-11-08
// *****************************************************/

namespace Agebull.EntityModel.RobotCoder.CodeTemplate
{
    /// <summary>
    ///     表示代码基元的种族(家族分得还太细)
    /// </summary>
    public enum CodeItemRace
    {
        /// <summary>
        ///     未分类
        /// </summary>
        None,

        /// <summary>
        ///     辅助对象
        /// </summary>
        Assist,
        /// <summary>
        ///     值
        /// </summary>
        Value,

        /// <summary>
        /// 补全代码(源代码中不存在的)
        /// </summary>
        Completion,


        /// <summary>
        ///     类型
        /// </summary>
        Type,

        /// <summary>
        ///     变量
        /// </summary>
        Variable,

        /// <summary>
        ///     控制符
        /// </summary>
        Control,

        /// <summary>
        ///     语句
        /// </summary>
        Sentence,

        /// <summary>
        ///     区域
        /// </summary>
        Range
    }
}
