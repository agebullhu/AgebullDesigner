// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-10-26
// 修改:2014-11-08
// *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using Agebull.Common.DataModel;

#endregion

namespace Agebull.CodeRefactor.CodeAnalyze
{
    public sealed class CodeAnalyzerContext
    {
        [ThreadStatic]
        private static CodeAnalyzerContext _current;

        public static CodeAnalyzerContext Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
            }
        }

        public TraceMessage Messager { get; set; }

        /// <summary>
        ///     文件名
        /// </summary>
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        ///     文件路径
        /// </summary>
        public string PathName
        {
            get;
            set;
        }

        /// <summary>
        ///     分析处理后文档内容
        /// </summary>
        public string AnalyzeDocument
        {
            get;
            set;
        }

        /// <summary>
        ///     应该包含的编译条件
        /// </summary>
        public List<string> IncludeCondition
        {
            get;
            set;
        }

        /// <summary>
        ///     分析到的单词
        /// </summary>
        public List<CodeElement> Words
        {
            get;
            set;
        }

        /// <summary>
        ///     基本节点
        /// </summary>
        public List<CodeElement> CodeElements
        {
            get;
            set;
        }

        public string GetCode(int start, int end)
        {
            if (string.IsNullOrEmpty(this.AnalyzeDocument))
            {
                return null;
            }

            if (start < 0 || start >= this.AnalyzeDocument.Length ||
                end <= 0 || end >= this.AnalyzeDocument.Length)
            {
                return null;
            }
            if (start > 0)
            {
                for (start -= 1; start >= 0; start--)
                {
                    switch (this.AnalyzeDocument[start])
                    {
                        case '\t':
                        case ' ':
                            continue;
                    }
                    start++;
                    break;
                }
            }
            return this.AnalyzeDocument.Substring(start, end - start + 1);
        }
    }
}
