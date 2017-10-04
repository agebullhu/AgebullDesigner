using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.Common.LUA;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.CodeTemplate
{
    /// <summary>
    ///     代码模板转换器基础
    /// </summary>
    public abstract class TemplateParseBase
    {
        public void MergeLanWord()
        {
            TemplateElements.Clear();
            ContentElement = null;
            var el = new AnalyzeElement();
            char pre = '\0';
            for (CurWordIndex = 0; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                MergeLanWord(el, ref pre);
            }
            TemplateElements.Add(el);
        }
        #region 过程对象

        public TemplateConfig Config { get; set; }


        /// <summary>
        ///     解析前的单词节点
        /// </summary>
        public List<WordElement> WordElements;

        /// <summary>
        ///     当前单词的位置
        /// </summary>
        protected int CurWordIndex;
        /// <summary>
        ///     解析后的模板节点
        /// </summary>
        public readonly List<AnalyzeElement> TemplateElements = new List<AnalyzeElement>();

        #endregion

        #region 内容节点

        /// <summary>
        /// 当前内容节点
        /// </summary>
        protected AnalyzeElement ContentElement { get; private set; }

        /// <summary>
        /// 重置当前内容节点
        /// </summary>
        /// <param name="element"></param>
        protected void ResetContentElement(WordElement element = null)
        {
            ContentElement = new AnalyzeElement(element)
            {
                IsContent = true
            };

            TemplateElements.Add(ContentElement);
        }

        #endregion

        #region 内容处理

        /// <summary>
        ///     分析文本
        /// </summary>
        protected void Compile()
        {
            if (Config == null)
                return;
            if (string.IsNullOrWhiteSpace(Config.Template))
            {
                if (string.IsNullOrWhiteSpace(Config.TemplatePath) || !File.Exists(Config.TemplatePath))
                    return;
                Config.Template = File.ReadAllText(Config.TemplatePath);
            }
            if (string.IsNullOrWhiteSpace(Config.Template))
                return;
            WordElements = WordAnalyze.Analyze(Config.Template);
            AnalyzeWord();
            Config.Code = BuildCode();
        }

        /// <summary>
        ///     合并单词,使之成为格式良好的代码单元
        /// </summary>
        public virtual void AnalyzeWord()
        {
            TemplateElements.Clear();
            ResetContentElement();
            for (CurWordIndex = 0; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                var cur = WordElements[CurWordIndex];
                if (cur.Char == '@' && CheckEmbeddedCode())
                {
                    continue;
                }
                ContentElement.Append(WordElements[CurWordIndex]);
            }
            foreach (var tm in TemplateElements)
            {
                if (tm.IsContent)
                {
                    foreach (var element in tm.Elements)
                    {
                        element.Words.ForEach(p =>
                        {
                            p.SetRace(CodeItemRace.Value, CodeItemFamily.Constant,CodeItemType.Content);
                            p.IsContent = true;
                        });
                    }
                }
                tm.Elements.ForEach(p => p.Release());
            }
        }
        
        #endregion

        #region 代码组合

        /// <summary>
        ///     生成代码
        /// </summary>
        /// <returns></returns>
        protected abstract string BuildCode();

        #endregion

        #region 嵌入代码处理

        /// <summary>
        ///     查找代码嵌入
        /// </summary>
        /// <returns></returns>
        protected bool CheckEmbeddedCode()
        {
            if (++CurWordIndex >= WordElements.Count)
                return false;
            var codeElement = WordElements[CurWordIndex];
            return codeElement.IsPunctuate
                ? CheckPunctuateStartCode()
                : CheckWordStartCode();
        }

        /// <summary>
        ///     检查标点打头的代码块
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckPunctuateStartCode()
        {
            var codeElement = new AnalyzeElement();
            switch (WordElements[CurWordIndex].Char)
            {
                case '@':
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    return false; //两个@为一个@
                default:
                    CurWordIndex--; //其它标点回退,此时@为普通字符
                    return false;
                case '*': //@*...*@为注释,跳过
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    SkipRazorRem();
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    return true;
                case '#': //@#...#@为文档配置
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    ReadFileRem();
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    return true;
                case '-': //单行@-表示单行代码
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    if (!IsWithLineStart())
                    {
                        CurWordIndex--; //其它标点回退,此时@为普通字符
                        return false;
                    }
                    if (IsWithLineEnd())
                        return true; //跳过空语句
                    MergeToLineEnd(codeElement);
                    codeElement.SetRace(CodeItemRace.Assist, CodeItemFamily.Rem);
                    break;
                case '(': //@()小括号配对变量块
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.Sharp);
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
                    FindCouple(codeElement, '(', ')', false);
                    codeElement.IsBlock = true;
                    foreach (var item in codeElement.Elements)
                        item.Words.ForEach(p => p.IsBlock = true);
                    if (CurWordIndex < WordElements.Count)
                        WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
                    break;
                case '{': //@{}多行代码块
                    if (!IsWithLineStart() || !IsWithLineEnd())
                    {
                        CurWordIndex--; //其它标点回退,此时@为普通字符,{会下一次被普通处理
                        return false;
                    }
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
                    FindCouple(codeElement, '{', '}', false);
                    if(CurWordIndex < WordElements.Count)
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
                    break;
            }
            JoinCode(codeElement);
            return true;
        }

        /// <summary>
        ///     检查单词打头的代码块
        /// </summary>
        /// <returns></returns>
        protected abstract bool CheckWordStartCode();

        /// <summary>
        ///     组合代码
        /// </summary>
        /// <param name="codeElement">代码块</param>
        protected virtual void JoinCode(AnalyzeElement codeElement)
        {
            codeElement.ItemRace = CodeItemRace.Sentence;
            TemplateElements.Add(codeElement);
            ResetContentElement();
        }

        #endregion

        #region 字符控制

        /// <summary>
        /// 生成一个代表分行的节点
        /// </summary>
        /// <returns></returns>
        protected WordElement CreateLineElement()
        {
            return new WordElement(new WordUnit('\n')
            {
                IsReplenish = true
            });
        }
        /// <summary>
        ///     是否处在行头
        /// </summary>
        /// <param name="skip">跳过几个单元</param>
        /// <returns></returns>
        protected bool IsWithLineStart(int skip = 1)
        {
            return CurWordIndex == skip ||
                   WordElements[CurWordIndex - skip - 1].IsLine;
        }

        /// <summary>
        ///     是否处在行尾
        /// </summary>
        /// <param name="skip">跳过几个单元</param>
        /// <returns></returns>
        protected bool IsWithLineEnd(int skip = 0)
        {
            return CurWordIndex + skip + 1 == WordElements.Count ||
                   WordElements[CurWordIndex + skip + 1].IsLine;
        }

        /// <summary>
        ///     前进到下一个字符
        /// </summary>
        /// <param name="codeElement">当前合并内容</param>
        /// <param name="word">这个字符期望的内容(不影响合并,但影响返回值)</param>
        /// <param name="canSpace">是否允许空白符间隔</param>
        /// <returns></returns>
        protected bool GoNextWithWord(AnalyzeElement codeElement, string word, bool canSpace = true)
        {
            CurWordIndex++;
            if (CurWordIndex == WordElements.Count)
                return false;
            if (canSpace && WordElements[CurWordIndex].IsSpace)
            {
                codeElement.Append(WordElements[CurWordIndex++]);
            }
            if (CurWordIndex == WordElements.Count)
                return false;
            codeElement.Append(WordElements[CurWordIndex]);
            return WordElements[CurWordIndex].RealWord == word;
        }

        /// <summary>
        ///     下一个字符是否为期望字符,如果是,合并之
        /// </summary>
        /// <param name="codeElement">当前合并内容</param>
        /// <param name="word">这个字符期望的内容</param>
        /// <param name="canSpace">是否允许空白符间隔</param>
        /// <returns></returns>
        protected bool TryGoNextWithWord(AnalyzeElement codeElement, string word, bool canSpace)
        {
            var idx = CurWordIndex;
            idx++;
            if (idx == WordElements.Count)
                return false;
            if (canSpace && WordElements[idx].IsSpace)
                idx++;
            if (idx >= WordElements.Count || WordElements[idx].RealWord != word)
                return false;
            while (CurWordIndex < idx)
                codeElement.Append(WordElements[++CurWordIndex]);
            return true;
        }

        /// <summary>
        ///     直到期望字符出现结束
        /// </summary>
        /// <param name="codeElement">当前合并内容</param>
        /// <param name="word">这个字符期望的内容</param>
        /// <returns></returns>
        protected void StepEndWord(AnalyzeElement codeElement, string word)
        {
            while (++CurWordIndex < WordElements.Count)
            {
                var el = WordElements[CurWordIndex];
                codeElement.Append(el);
                if (el.RealWord == word)
                    return;
            }
        }
        /// <summary>
        ///     下一个字符是否符合期望条件(不改变当前顺序)
        /// </summary>
        /// <param name="word">期望出现的字符</param>
        /// <param name="canSpace">是否允许空白符间隔</param>
        /// <returns></returns>
        protected bool NextWithCondition(string word, bool canSpace)
        {
            return NextWithCondition(p => p.Word == word, canSpace);
        }
        /// <summary>
        ///     下一个字符是否符合期望条件(不改变当前顺序)
        /// </summary>
        /// <param name="condition">是否期望条件</param>
        /// <param name="canSpace">是否允许空白符间隔</param>
        /// <returns></returns>
        protected bool NextWithCondition(Func<WordElement, bool> condition, bool canSpace)
        {
            var idx = CurWordIndex;
            idx++;
            if (idx == WordElements.Count)
                return false;
            if (canSpace && WordElements[idx].IsSpace)
                idx++;
            if (idx >= WordElements.Count || !condition(WordElements[idx]))
                return false;
            return true;
        }
        /// <summary>
        ///     下一个字符是否符合期望条件
        /// </summary>
        /// <param name="codeElement">当前合并内容</param>
        /// <param name="condition">是否期望条件</param>
        /// <param name="canSpace">是否允许空白符间隔</param>
        /// <returns></returns>
        protected bool TryGoNextWithCondition(AnalyzeElement codeElement, Func<WordElement, bool> condition, bool canSpace)
        {
            return TryGoNextWithCondition(codeElement, condition, null, canSpace);
        }

        /// <summary>
        ///     下一个字符是否符合期望条件
        /// </summary>
        /// <param name="codeElement">当前合并内容</param>
        /// <param name="condition">是否期望条件</param>
        /// <param name="succeedAction">如果符合条件,加入节点前的操作</param>
        /// <param name="canSpace">是否允许空白符间隔</param>
        /// <returns></returns>
        protected bool TryGoNextWithCondition(AnalyzeElement codeElement,
            Func<WordElement, bool> condition,
            Action<AnalyzeElement> succeedAction,
            bool canSpace)
        {
            var idx = CurWordIndex;
            idx++;
            if (idx == WordElements.Count)
                return false;
            if (canSpace && WordElements[idx].IsSpace)
                idx++;
            if (idx >= WordElements.Count || !condition(WordElements[idx]))
                return false;
            succeedAction?.Invoke(codeElement);
            while (CurWordIndex < idx)
                codeElement.Append(WordElements[++CurWordIndex]);
            return true;
        }

        /// <summary>
        ///     处理当前语言内容
        /// </summary>
        /// <param name="codeElement">语言节点</param>
        /// <param name="preChar"></param>
        public abstract void MergeLanWord(AnalyzeElement codeElement, ref char preChar);

        /// <summary>
        ///     查找成对的符号
        /// </summary>
        /// <param name="codeElement">语言节点</param>
        /// <param name="start">开始字符</param>
        /// <param name="end">结束</param>
        /// <param name="includeStartEnd">结果是否包括前后字符</param>
        /// <param name="skipCur">是否先行跳过当前字符</param>
        /// <returns></returns>
        protected void FindCouple(AnalyzeElement codeElement, char start, char end, bool includeStartEnd, bool skipCur = false)
        {

            WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
            if (skipCur)
                CurWordIndex++;
            //确认开始字符是否当前字符,如果不是,当前可为空白且下一个必须是前导字符
            if (WordElements[CurWordIndex].Char != start)
            {
                if (CurWordIndex + 1 < WordElements.Count && WordElements[CurWordIndex + 1].Char == start)
                {
                    if (includeStartEnd)
                        codeElement.Append(WordElements[CurWordIndex]);
                    CurWordIndex++;
                    if (includeStartEnd)
                        codeElement.Append(WordElements[CurWordIndex]);
                }
                else
                {
                    return;
                }
            }
            else if (includeStartEnd)
            {
                codeElement.Append(WordElements[CurWordIndex]);
            }
            var block = 1;
            var preChar = '\0';
            for (CurWordIndex++; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                var curElement = WordElements[CurWordIndex];
                if (curElement.Char == start)
                    block++;
                else if (curElement.Char == end)
                    --block;
                if (block == 0) //成对结束
                {
                    curElement.SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
                    if (includeStartEnd)
                        codeElement.Append(curElement);
                    return;
                }
                //标点处理,以防止特殊情况的污染(注释,字符串,字符)
                MergeLanWord(codeElement, ref preChar);
            }
        }

        /// <summary>
        ///     处理多行嵌入代码
        /// </summary>
        /// <param name="codeElement"></param>
        /// <remarks>
        /// 如果后面是@,则后续还是代码,直到一个有且只有两个@字符的行
        /// </remarks>
        protected void CheckMulitCode(AnalyzeElement codeElement)
        {
            if (!NextWithCondition("@", false))
                return;
            var preChar = '\0';
            WordElements[++CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
            var waiting = new List<WordElement>();
            for (CurWordIndex++; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                if (WordElements[CurWordIndex].IsLine)
                {
                    if (waiting.Count == 3)
                    {
                        waiting[1].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
                        waiting[2].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
                        CurWordIndex--;
                        return;
                    }
                    foreach (var item in waiting)
                        codeElement.Append(item);
                    waiting.Clear();
                    waiting.Add(WordElements[CurWordIndex]);
                    continue;
                }
                if (waiting.Count > 0 && waiting.Count < 3 && WordElements[CurWordIndex].Char == '@')
                {
                    waiting.Add(WordElements[CurWordIndex]);
                    continue;
                }
                foreach (var item in waiting)
                    codeElement.Append(item);
                waiting.Clear();
                MergeLanWord(codeElement, ref preChar);
            }
            foreach (var item in waiting)//补回去
                codeElement.Append(item);
        }

        /// <summary>
        ///     查找单个变量
        /// </summary>
        /// <param name="codeElement">代码块</param>
        /// <param name="joinChar">变量连接字符</param>
        /// <returns></returns>
        protected void FindVariable(AnalyzeElement codeElement, params char[] joinChar)
        {
            codeElement.ItemRace = CodeItemRace.Variable;
            codeElement.Append(WordElements[CurWordIndex]);
            if (CurWordIndex + 1 == WordElements.Count)
                return;
            var word = WordElements[++CurWordIndex];
            //第一个字符后不跟期望标点,不可再拼接
            if (!word.IsPunctuate || !joinChar.Any(p => p == word.Char))
            {
                //当前标点作为普通内容,故做回退处理
                --CurWordIndex;
                return;
            }
            for (; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                word = WordElements[CurWordIndex];
                if (word.IsPunctuate)
                {
                    //当前为分隔符且后一个为字符
                    if (!joinChar.Any(p => p == word.Char) || !NextWithCondition(p => p.IsWord, false))
                    {
                        //当前标点作为普通内容,故做回退处理
                        --CurWordIndex;
                        return;
                    }
                    codeElement.Append(word);
                }
                else
                {
                    codeElement.Append(word);
                    //当前为字符且后一个为分隔符
                    if (!NextWithCondition(p => joinChar.Any(ch => ch == p.Char), false))
                        return;
                }
            }
        }

        #endregion

        #region 语言泛方法

        /// <summary>
        ///     检查是否为代码块结束标记,不同语言不同,C#是@},LUA是@end
        /// </summary>
        /// <param name="codeElement">代码块</param>
        /// <returns></returns>
        protected bool CheckIsCodeBlockEnd(AnalyzeElement codeElement)
        {
            if (IsWithLineStart() && IsWithLineEnd())
            {
                ContentElement.Elements.Remove(WordElements[CurWordIndex - 2]);//空行
                ContentElement.Elements.Remove(WordElements[CurWordIndex - 1]);//空行
                codeElement.ItemRace = CodeItemRace.Range;
                codeElement.Append(WordElements[CurWordIndex]);
                JoinCode(codeElement);
                return true;
            }
            CurWordIndex--;
            return false;
        }

        /// <summary>
        ///     合并字符
        /// </summary>
        /// <param name="codeElement">代码块</param>
        /// <param name="sign">字符使用哪个符号(一般都是"或')</param>
        protected void MergeString(WordElement codeElement, char sign)
        {
            var preShift = false;
            for (CurWordIndex++; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                var curElement = WordElements[CurWordIndex];
                codeElement.Append(curElement);
                if (preShift)
                {
                    preShift = false; //前方为转义字符不处理
                    continue;
                }
                if (curElement.Char == '\\')
                {
                    preShift = true;
                    continue;
                }
                if (curElement.Char == sign)
                    break; //成对出现,结束字符串识别
            }
            codeElement.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.String);
        }

        /// <summary>
        ///     合并到行尾结束的对象
        /// </summary>
        /// <param name="codeElement">代码块</param>
        protected void MergeToLineEnd(AnalyzeElement codeElement)
        {
            for (CurWordIndex++; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                var curElement = WordElements[CurWordIndex];
                if (curElement.IsLine) //行已结束
                {
                    --CurWordIndex; //回退
                    return;
                }
                codeElement.Append(curElement);
            }
        }

        /// <summary>
        ///     跳过Razor注释@*...*@
        /// </summary>
        protected void SkipRazorRem()
        {
            var preChar = '\0';
            if (++CurWordIndex >= WordElements.Count)
                return;
            for (++CurWordIndex; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.Rem);
                var curElement = WordElements[CurWordIndex];
                if (preChar == '*' && curElement.Char == '@')
                {
                    break;
                }
                preChar = curElement.Char;
            }
        }


        #endregion

        #region 杂注


        /// <summary>
        ///     读取文档杂注@#...#@
        /// </summary>
        protected void ReadFileRem()
        {
            if (++CurWordIndex >= WordElements.Count)
                return;
            WordElement code = new WordElement();
            bool shfit = false;
            for (++CurWordIndex; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.Rem);
                var curElement = WordElements[CurWordIndex];

                if (shfit && curElement.Char == '@')
                    break;
                shfit = curElement.Char == '#';

                code.Append(curElement);
            }
            if (code.Words.Count == 0)
                return;
            code.Release();
            CheckConfig(code);
        }

        private void CheckConfig(WordElement code)
        {
            string name = null;
            StringBuilder value = new StringBuilder();
            int step = 0;
            char bc = '\0';
            char prechar = '\0';
            foreach (var word in code.Words)
            {
                switch (step)
                {
                    case 0://取得名称,并在出现第一个冒时时进入第二步
                        if (word.IsSpace)
                        {
                            continue;
                        }
                        if (!word.IsPunctuate)
                        {
                            name = word.Word;
                            word.ItemRace = CodeItemRace.Variable;
                        }
                        else if (word.Char == ':')
                        {
                            step = 1;
                            word.ItemRace = CodeItemRace.Assist;
                            word.ItemFamily = CodeItemFamily.Range;
                            value.Clear();
                        }
                        continue;
                    case 1://检测值是以哪种方式存在
                        if (word.IsLine)//空说明,跳过
                        {
                            step = 0;
                            continue;
                        }
                        if (word.IsSpace)
                        {
                            continue;
                        }
                        switch (word.Char)
                        {
                            case '\'':
                            case '\"':
                                step = 3;//有引号的
                                bc = word.Char;
                                prechar = '\0';
                                break;
                            default:
                                value.Append(word.Word);
                                step = 2;//没引号的
                                break;
                        }
                        break;
                    case 2://没引号的,到行结束
                        if (word.IsLine)
                        {
                            SetConfig(name, value.ToString());
                            step = 0;
                            name = null;
                            break;
                        }
                        value.Append(word.Word);
                        break;
                    case 3://有引号的,到引号结束,使用转义
                        if (prechar == '\\')
                        {
                            value.Remove(value.Length - 1, 1);
                            value.Append(word.Word);
                            break;
                        }
                        if (word.Char == bc)
                        {
                            SetConfig(name, value.ToString());
                            step = 0;
                            name = null;
                            break;
                        }
                        value.Append(word.Word);
                        break;
                }
                word.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.String);
            }
            SetConfig(name, value.ToString());
        }

        private void SetConfig(string name, string value)
        {
            if (Config == null)
                Config = new TemplateConfig();
            if (name == null)
                return;
            switch (name.ToLower())
            {
                case "name":
                case "名称":
                    Config.Name = value.FromLuaChar();
                    break;
                case "caption":
                case "标题":
                    Config.Caption = value.FromLuaChar();
                    break;
                case "desc":
                case "description":
                case "说明":
                case "备注":
                case "模板说明":
                    Config.Description = value.FromLuaChar();
                    break;
                case "classify":
                case "类别":
                case "分类":
                    Config.Classify = value.FromLuaChar();
                    break;
                case "condition":
                case "执行条件":
                    Config.Condition = value.FromLuaChar();
                    break;
                case "language":
                case "语言":
                case "目标语言":
                    Config.Language = value.FromLuaChar();
                    break;
                case "savesath":
                case "codesavesath":
                case "代码保存路径":
                case "代码保存":
                case "保存路径":
                    Config.CodeSavePath = value.FromLuaChar();
                    break;
                case "type":
                case "model":
                case "modeltype":
                case "参数":
                case "参数类型":
                case "数据模型":
                case "模型":
                case "模型类型":
                    var type = value.FromLuaChar();
                    switch (type.ToLower())
                    {
                        case "字段":
                        case "field":
                        case "property":
                        case "propertyconfig":
                            Config.ModelType = typeof(PropertyConfig).Name;
                            break;
                        case "实体":
                        case "entity":
                        case "entityconfig":
                            Config.ModelType = typeof(EntityConfig).Name;
                            break;
                        case "枚举":
                        case "enum":
                        case "enumconfig":
                            Config.ModelType = typeof(EnumConfig).Name;
                            break;
                        case "类型":
                        case "类型定义":
                        case "type":
                        case "typedef":
                        case "typedefitem":
                        case "typeconfig":
                        case "typedefconfig":
                            Config.ModelType = typeof(TypedefItem).Name;
                            break;
                        case "命令":
                        case "用户命令":
                        case "按钮":
                        case "btn":
                        case "button":
                        case "cmd":
                        case "command":
                            Config.ModelType = typeof(UserCommandConfig).Name;
                            break;
                        case "项目":
                        case "project":
                        case "projectconfig":
                            Config.ModelType = typeof(ProjectConfig).Name;
                            break;
                        case "解决方案":
                        case "solution":
                        case "solutionconfig":
                            Config.ModelType = typeof(SolutionConfig).Name;
                            break;
                        default:
                            Config.ModelType = type;
                            break;
                    }
                    break;
            }
        }
        #endregion
    }
}