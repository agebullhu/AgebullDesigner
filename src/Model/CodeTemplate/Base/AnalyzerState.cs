// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-11-08
// 修改:2014-11-08
// *****************************************************/

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     解析状态
    /// </summary>
    public enum AnalyzerState
    {
        /// <summary>
        ///     未分析
        /// </summary>
        None,

        /// <summary>
        ///     已分析本级节点
        /// </summary>
        Self,

        /// <summary>
        ///     已分析全部节点
        /// </summary>
        All
    }
}
