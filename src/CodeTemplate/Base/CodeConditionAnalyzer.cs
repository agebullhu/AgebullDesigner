// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-04-13
// 修改:2014-11-08
// *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     代码编译条件分析器
    /// </summary>
    public class CodeConditionAnalyzer
    {
        #region 查找条件编译符号

        /// <summary>
        ///     查找条件编译符号(参数来自CodeAnalyzerContext的上下文)
        /// </summary>
        public static void FindCodeAnalyzerContextCondition()
        {
            foreach (CodeElement p in CodeAnalyzerContext.Current.Words.Where(p => p.ItemType == CodeItemType.Sharp_IF))
            {
                string[] curCondition = p.Word.Split(new[]
                {
                    ' ', '\t'
                }, 2)[1].Split(new[]
                {
                    ' ', '\t'
                },
                        StringSplitOptions.RemoveEmptyEntries);
                foreach (string c in curCondition)
                {
                    switch (c)
                    {
                        case "!":
                        case "(":
                        case ")":
                        case "&&":
                        case "||":
                            continue;
                    }
                    CodeAnalyzerContext.Current.IncludeCondition.Add(c[0] == '!' ? c.Substring(1) : c);
                }
            }
        }

        #endregion

        #region 条件编译内容排除

        /// <summary>
        ///     移除非条件范围的代码(参数来自CodeAnalyzerContext的上下文)
        /// </summary>
        public static void SkipCondition()
        {
            if (CodeAnalyzerContext.Current.Words == null || CodeAnalyzerContext.Current.Words.Count == 0 ||
                CodeAnalyzerContext.Current.IncludeCondition == null || CodeAnalyzerContext.Current.IncludeCondition.Count == 0)
            {
                return;
            }

            RemoveCondition();
            StringBuilder sb = new StringBuilder();
            foreach (CodeElement element in CodeAnalyzerContext.Current.Words)
            {
                if (element.Skip || element.Word == "\r")
                {
                    continue;
                }
                element.Start = sb.Length;
                sb.Append(element.Word);
                element.End = sb.Length - 1;
            }
            CodeAnalyzerContext.Current.AnalyzeDocument = sb.ToString();
        }

        /// <summary>
        ///     移除非条件范围的代码(参数来自CodeAnalyzerContext的上下文)
        /// </summary>
        private static void RemoveCondition()
        {
            CodeElement[] arr = CodeAnalyzerContext.Current.Words.ToArray();
            List<CodeElement> elements = CodeAnalyzerContext.Current.Words;
            elements.Clear();

            int keepCnt = 0;
            FixStack<bool> conditionStack = new FixStack<bool>();
            conditionStack.SetFix(false);
            FixStack<List<string>> logicalStack = new FixStack<List<string>>();
            string condition = string.Empty;
            foreach (CodeElement el in arr)
            {
                switch (el.ItemType)
                {
                    case CodeItemType.Sharp_IF:
                        el.Skip = true;
                        if (conditionStack.Current)
                        {
                            keepCnt++;
                            break;
                        }
                        SharpElement sharp = el as SharpElement;
                        List<string> words = GetConditionWords(sharp.Name);
                        conditionStack.Push(!CheckCondition(words));
                        if (conditionStack.Current)
                        {
                            keepCnt = 1;
                        }
                        else
                        {
                            if (!logicalStack.IsEmpty)
                            {
                                words.Insert(0, "&&");
                            }
                            words.Insert(0, "(");
                            words.Add(")");
                            condition += words.LinkToString();
                            logicalStack.Push(words);
                        }
                        break;
                    case CodeItemType.Sharp_Else:
                        el.Skip = true;
                        if (keepCnt > 1)
                        {
                            break;
                        }
                        bool kp = conditionStack.Current;
                        conditionStack.Pop();
                        conditionStack.Push(!kp);
                        if (logicalStack.Current != null && logicalStack.Current.Count > 0)
                        {
                            logicalStack.Current.Insert(logicalStack.Current[0] == "&&" ? 1 : 0, "!");
                        }
                        condition = string.Empty;
                        foreach (List<string> cd in logicalStack.Stack)
                        {
                            condition += cd.LinkToString();
                        }
                        break;
                    case CodeItemType.Sharp_EndIf:
                        el.Skip = true;
                        keepCnt--;
                        if (keepCnt > 0)
                        {
                            break;
                        }
                        logicalStack.Pop();
                        condition = string.Empty;
                        foreach (List<string> cd in logicalStack.Stack)
                        {
                            condition += cd.LinkToString();
                        }
                        conditionStack.Pop();
                        break;
                    default:
                        if (conditionStack.Current)
                        {
                            el.Skip = true;
                        }
                        else
                        {
                            if (!el.Skip)
                            {
                                el.Condition = condition;
                                elements.Add(el);
                            }
                        }
                        break;
                }
            }
        }

        private static List<string> GetConditionWords(string condition)
        {
            string[] words = condition.Split(new[]
            {
                ' ', '\t'
            }, StringSplitOptions.RemoveEmptyEntries);
            List<string> conditionWords = new List<string>();
            foreach (string word in words)
            {
                char pre = (char) 0;
                StringBuilder sb = new StringBuilder();
                foreach (char ch in word)
                {
                    switch (ch)
                    {
                        case '!':
                            if (sb.Length > 0)
                            {
                                conditionWords.Add(sb.ToString());
                            }
                            sb.Clear();
                            conditionWords.Add("!");
                            break;
                        case '(':
                            if (sb.Length > 0)
                            {
                                conditionWords.Add(sb.ToString());
                            }
                            sb.Clear();
                            conditionWords.Add("(");
                            break;
                        case ')':
                            if (sb.Length > 0)
                            {
                                conditionWords.Add(sb.ToString());
                            }
                            sb.Clear();
                            conditionWords.Add(")");
                            break;
                        case '&':
                            if (pre == '&')
                            {
                                conditionWords[conditionWords.Count - 1] = "&&";
                            }
                            else
                            {
                                if (sb.Length > 0)
                                {
                                    conditionWords.Add(sb.ToString());
                                }
                                sb.Clear();
                                conditionWords.Add("&");
                            }
                            break;
                        case '|':
                            if (pre == '|')
                            {
                                conditionWords[conditionWords.Count - 1] = "||";
                            }
                            else
                            {
                                if (sb.Length > 0)
                                {
                                    conditionWords.Add(sb.ToString());
                                }
                                sb.Clear();
                                conditionWords.Add("|");
                            }
                            break;
                        case ' ':
                        case '\t':
                            break;
                        default:
                            sb.Append(ch);
                            break;
                    }
                    pre = ch;
                }
                if (sb.Length > 0)
                {
                    conditionWords.Add(sb.ToString());
                }
            }
            return conditionWords;
        }

        /// <summary>
        ///     编译条件检查
        /// </summary>
        /// <param name="conditionWords">编译条件</param>
        /// <returns>是否符合条件</returns>
        private static bool CheckCondition(IEnumerable<string> conditionWords)
        {
            FixStack<List<LogicalCheckItem>> logicalStack = new FixStack<List<LogicalCheckItem>>();
            logicalStack.Push(new List<LogicalCheckItem>());
            bool nowIsNot = false;
            int nowLogical = 0;
            foreach (string word in conditionWords)
            {
                switch (word)
                {
                    case "!":
                        nowIsNot = true;
                        continue;
                    case "(":
                        logicalStack.Current.Add(new LogicalCheckItem
                        {
                            IsNo = nowIsNot,
                            Logical = nowLogical
                        });
                        logicalStack.Push(new List<LogicalCheckItem>());
                        nowLogical = 0;
                        continue;
                    case ")":
                        bool value = CheckValue(logicalStack.Current);
                        logicalStack.Pop();
                        LogicalCheckItem cnt = logicalStack.Current.Last();
                        cnt.Value = cnt.IsNo ? !value : value;
                        continue;
                    case "&&":
                        nowLogical = 1;
                        continue;
                    case "||":
                        nowLogical = 2;
                        continue;
                }
                logicalStack.Current.Add(new LogicalCheckItem
                {
                    IsNo = nowIsNot,
                    Word = word,
                    Logical = nowLogical
                });
            }
            return CheckValue(logicalStack.Current);
        }

        private static bool CheckValue(IList<LogicalCheckItem> items)
        {
            LogicalCheckItem pre = items[0];
            CheckValue(pre);
            if (items.Count == 1)
            {
                return pre.Value.Value;
            }
            LogicalCheckItem[] array = items.Skip(1).ToArray();
            //处理与关系
            List<bool> values = new List<bool>
            {
                pre.Value.Value
            };
            foreach (LogicalCheckItem item in array)
            {
                //与逻辑的,直接和前一个进行与计算
                if (item.Logical == 1)
                {
                    if (values[values.Count - 1]) //值为真才需要计算,否则任何计算都是否
                    {
                        values[values.Count - 1] = CheckValue(item);
                    }
                }
                else
                {
                    //或逻辑已有真值,直接返回
                    if (values.Any(p => p))
                    {
                        return true;
                    }
                    //或逻辑的,保存在列表中或返回
                    values.Add(CheckValue(item));
                }
            }
            //与关系只要一个真就可以
            return values.Any(p => p);
        }

        private static bool CheckValue(LogicalCheckItem item)
        {
            if (item.Value != null)
            {
                return item.Value.Value;
            }
            bool value = CodeAnalyzerContext.Current.IncludeCondition.Contains(item.Word);
            item.Value = item.IsNo ? !value : value;
            return item.Value.Value;
        }

        private sealed class LogicalCheckItem
        {
            public bool IsNo
            {
                get;
                set;
            }

            public int Logical
            {
                get;
                set;
            }

            public string Word
            {
                get;
                set;
            }

            public bool? Value
            {
                get;
                set;
            }
        }

        #endregion
    }
}
