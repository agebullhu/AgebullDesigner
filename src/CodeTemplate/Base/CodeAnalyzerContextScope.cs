// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-10-26
// 修改:2014-11-08
// *****************************************************/

#region 引用

using Agebull.Common.Base;
using Agebull.Common.DataModel;

#endregion

namespace Agebull.CodeRefactor.CodeAnalyze
{
    public class CodeAnalyzerContextScope : ScopeBase
    {
        private CodeAnalyzerContext _oldContext;

        public static CodeAnalyzerContextScope CreateScope(CodeAnalyzerContext context)
        {
            CodeAnalyzerContextScope re = new CodeAnalyzerContextScope
            {
                _oldContext = CodeAnalyzerContext.Current,
            };
            CodeAnalyzerContext.Current = context ?? new CodeAnalyzerContext
            {
                Messager = TraceMessage.DefaultTrace
            };
            return re;
        }

        #region Overrides of ScopeBase

        /// <summary>
        ///     清理资源
        /// </summary>
        protected override void OnDispose()
        {
            CodeAnalyzerContext.Current = this._oldContext;
        }

        #endregion
    }
}
