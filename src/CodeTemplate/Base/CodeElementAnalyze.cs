// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-04-13
// 修改:2014-11-08
// *****************************************************/

#region 引用

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     代码单元分析器
    /// </summary>
    internal sealed class CodeElementAnalyze
    {
        /// <summary>
        ///     分析后的代码单元
        /// </summary>
        private List<CodeElement> _codeElements;

        /// <summary>
        ///     分析文本
        /// </summary>
        public static List<CodeElement> Analyze()
        {
            CodeElementAnalyze s = new CodeElementAnalyze();
            return s.AnalyzeInner();
        }

        /// <summary>
        ///     分析文本
        /// </summary>
        private List<CodeElement> AnalyzeInner()
        {
            this.MergeDot();
            this.CheckRegion();

            IEnumerable<CodeElement> els = this._codeElements;
            if (this._codeElements.Count > 2 && this._codeElements[0].Start == this._codeElements[1].Start)
            {
                els = this._codeElements.Skip(1);
            }
            return els.Where(p => !p.Skip && p.ItemType != CodeItemType.Space).ToList();
        }

        /// <summary>
        ///     合并连接的数字或词组
        /// </summary>
        private void MergeDot()
        {
            this._codeElements = new List<CodeElement>();
            List<CodeElement> temps = new List<CodeElement>();
            bool curIsDot = false;
            foreach (CodeElement element in CodeAnalyzerContext.Current.Words)
            {
                if (element.ItemType == CodeItemType.Punctuate_Dot)
                {
                    if (temps.Count == 0)
                    {
                        this._codeElements.Add(element);
                        curIsDot = false;
                        continue;
                    }
                    temps.Add(element);
                    curIsDot = true;
                    var ex = temps[0] as DotCodeElement;
                    if (ex == null)
                    {
                        ex = new DotCodeElement();
                        ex.AddBaseElement(temps);
                        ex.Word = string.Concat(temps.Where(p => !p.IsAssist).Select(p => p.Word));
                        ex.Start = temps[0].Start;
                    }
                    else
                    {
                        ex.AddBaseElement(temps.Skip(1));
                        ex.Word = string.Concat(temps.Select(p => p.Word));
                    }
                    temps.Clear();
                    temps.Add(ex);
                    ex.End = element.End;
                    continue;
                }
                switch (element.ItemRace)
                {
                    case CodeItemRace.Assist:
                    case CodeItemRace.Punctuate:
                        if (temps.Count > 0)
                        {
                            this._codeElements.AddRange(temps);
                            temps.Clear();
                        }
                        this._codeElements.Add(element);
                        curIsDot = false;
                        continue;
                }
                switch (element.ItemFamily)
                {
                    case CodeItemFamily.Range:
                    case CodeItemFamily.Logical:
                    case CodeItemFamily.Compute:
                    case CodeItemFamily.BitCompute:
                    case CodeItemFamily.Separator:
                    case CodeItemFamily.TypeChildBlock:
                        if (temps.Count > 0)
                        {
                            this._codeElements.AddRange(temps);
                            temps.Clear();
                        }
                        this._codeElements.Add(element);
                        curIsDot = false;
                        continue;
                    case CodeItemFamily.Rem:
                        if (curIsDot)
                        {
                            this._codeElements.Add(element); //注释给前面
                        }
                        else
                        {
                            if (temps.Count == 0)
                            {
                                this._codeElements.Add(element); //注释给前面
                            }
                            else
                            {
                                temps.Add(element);
                            }
                        }
                        continue;
                    case CodeItemFamily.Space:
                        if (!curIsDot)
                        {
                            if (temps.Count == 0)
                            {
                                this._codeElements.Add(element); //空白给前面
                            }
                            else
                            {
                                temps.Add(element);
                            }
                        }
                        else
                        {
                            this._codeElements.Add(element);
                        } //当前为点,直接跳过空白
                        continue;
                    default:
                        if (curIsDot)
                        {
                            curIsDot = false;
                            DotCodeElement ve = temps[0] as DotCodeElement;
                            if (ve == null)
                            {
                                continue;
                            }
                            ve.Word += element.Word;
                            ve.AddBaseElement(element);
                            ve.End = element.End;
                        }
                        else
                        {
                            if (temps.Count > 0)
                            {
                                this._codeElements.AddRange(temps);
                                temps.Clear();
                            }
                            temps.Add(element);
                        }
                        continue;
                }
            }
            this._codeElements.AddRange(temps);
        }

        /// <summary>
        ///     合并块
        /// </summary>
        private void CheckRegion()
        {
            FixStack<string> stack = new FixStack<string>();
            stack.SetFix(string.Empty);
            foreach (CodeElement element in this._codeElements)
            {
                RegionElement region = element as RegionElement;
                if (region != null)
                {
                    if (region.IsEnd)
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(region.Name);
                    }
                }
                else if (!element.Skip)
                {
                    element.Region = stack.Current;
                }
            }
        }
    }
}
