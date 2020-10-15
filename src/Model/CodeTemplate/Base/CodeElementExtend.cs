// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-10-23
// 修改:2014-11-08
// *****************************************************/

using System.Text;

namespace Agebull.CodeRefactor.CodeAnalyze
{
    public static class CodeElementExtend
    {
        /// <summary>
        ///     对于简单对象,剥离包装
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        public static CodeElement ToElement(this CodeItem el)
        {
            return CodeElement.ToElement(el);
        }

        /// <summary>
        ///     对于简单对象,剥离包装
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        public static string GetWord(this CodeItem el)
        {
            CodeElement e = CodeElement.ToElement(el);
            return e == null ? null : e.Word;
        }

        /// <summary>
        ///     对于简单对象,剥离包装
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        public static string GetAllCode(this CodeItem el)
        {
            CodeElement e = CodeElement.ToElement(el);
            if (e == null)
                return null;
            StringBuilder code = new StringBuilder();
            code.AppendFormat(@"
行{0}列{1}", e.Line, e.Column);
            GetString(el, code);
            return code.ToString();
        }

        /// <summary>
        ///     对于简单对象,剥离包装
        /// </summary>
        /// <param name="el"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        static void GetString(CodeItem el, StringBuilder code)
        {
            var element = el as CodeElement;
            if (element != null)
            {
                code.Append(element.Word);
                return;
            }
            //var comb = el as ElementCombination;
            //if (comb == null)
            //{
            //    return;
            //}
            //foreach (var ch in comb.Elements)
            //{
            //    GetString(ch, code);
            //}
        }
    }
}
