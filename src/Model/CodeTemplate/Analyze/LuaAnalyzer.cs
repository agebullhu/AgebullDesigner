using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.CodeTemplate.LuaTemplate
{
    /// <summary>
    ///     LUA深度分析
    /// </summary>
    public class LuaAnalyzer
    {
        #region 分析

        public List<WordUnit> Words;

        /// <summary>
        ///     解析后的模板节点
        /// </summary>
        public List<AnalyzeElement> TemplateElements { get; set; }

        public AnalyzeBlock Root { get; private set; }

        private TemplateConfig Config { get; set; }
        public void DoAnalyze(List<AnalyzeElement> tElements, TemplateConfig config)
        {
            Config = config;
            TemplateElements = tElements;
            OutcomeWords();
            MergeBlock();
            ConnectBlock(Root);
            MergeSimpleBlock(Root);
            MergeStatements(Root);
            ClearEmpty(Root);
            LuaTypeAnalyzer.Analyze(Root, Config);
            Release(Root);
        }

        #endregion
        #region 语句块合并

        /// <summary>
        /// 语句块合并
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private void MergeStatements(AnalyzeBlock root)
        {
            MergeRange(root, 10);
            MergeRange(root, 11);
            MergeFor(root);
        }

        /// <summary>
        ///     For组合
        /// </summary>
        /// <returns></returns>
        public void MergeFor(AnalyzeBlock root)
        {
            var cur = root;
            Stack<AnalyzeBlock> stack = new Stack<AnalyzeBlock>();
            int step = 0;
            foreach (var u in root.ResetElements())
            {
                if (u.IsEmpty)
                    continue;
                var unit = u;
                var child = unit as AnalyzeBlock;
                if (child == null)
                {
                    var word = (WordUnit)unit;
                    if (unit.ItemFamily == CodeItemFamily.Iterator && word.ItemType == CodeItemType.Key_For)
                    {
                        stack.Push(cur);
                        var block = new AnalyzeBlock
                        {
                            Parent = cur,
                            Primary = word,
                            ItemRace = CodeItemRace.Range,
                            ItemFamily = CodeItemFamily.IteratorRange,
                            ItemType = CodeItemType.Key_For
                        };
                        cur.Append(block);
                        cur = block;
                        step = 1;
                    }
                    else if (step > 0)
                    {
                        step++;
                        if (word.ItemType == CodeItemType.Key_In)
                        {
                            cur.ItemType = CodeItemType.Key_Foreach;
                        }
                    }
                    cur.Append(unit);
                }
                else
                {
                    if (!unit.IsLock && !unit.IsUnit)
                        MergeFor((AnalyzeBlock)unit);
                    cur.Append(unit);
                    if (step == 0)
                        continue;
                    step++;
                    if (unit.Word != "do")
                        continue;
                    if (cur.ItemType != CodeItemType.Key_Foreach)
                    {
                        cur.Elements[1].ValueType = LuaDataTypeItem.NumberValue;
                    }
                    else
                    {
                        cur.Elements[1].ValueLink = cur.Elements[cur.Elements.Count - 2];
                    }
                    if (stack.Count == 0)
                    {
                        cur.IsError = true;
                        cur = root;
                    }
                    else
                    {
                        cur = stack.Pop();
                    }
                    step = 0;
                }
            }
        }

        /// <summary>
        ///     区域组合
        /// </summary>
        /// <returns></returns>
        public void MergeRange(AnalyzeBlock root, int level)
        {
            var cur = root;
            Stack<AnalyzeBlock> stack = new Stack<AnalyzeBlock>();
            foreach (var u in root.ResetElements())
            {
                if (u.IsEmpty)
                    continue;
                var unit = u;
                var child = unit as AnalyzeBlock;
                if (child != null)
                {
                    if (!unit.IsLock && !unit.IsUnit)
                        MergeRange((AnalyzeBlock)unit, level);
                    cur.Append(unit);
                    continue;
                }
                var word = unit as WordUnit;
                if (unit.JoinLevel == level)
                {
                    switch (unit.JoinFeature)
                    {
                        case JoinFeature.RangeOpen:
                            stack.Push(cur);
                            var block = new AnalyzeBlock
                            {
                                Parent = cur,
                                Primary = word
                            };
                            cur.Append(block);
                            cur.JoinFeature = JoinFeature.BeforeBy;
                            cur = block;
                            break;
                        case JoinFeature.RangeClose:
                            cur.Append(unit);
                            if (stack.Count == 0)
                            {
                                cur.IsError = true;
                                cur = root;
                            }
                            else
                            {
                                cur = stack.Pop();
                            }
                            continue;
                    }
                }
                cur.Append(unit);
            }
        }

        #endregion
        #region 串联


        private void ConnectBlock(AnalyzeBlock root)
        {
            for (int level = 1; level < 4; level++)
            {
                ConnectBlock(root, level);
            }
        }


        /// <summary>
        ///     串联
        /// </summary>
        /// <param name="root"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public void ConnectBlock(AnalyzeBlock root, int level)
        {
            AnalyzeUnitBase pre = null;
            var step = 0;
            AnalyzeBlock cur = root;
            foreach (var unit in root.ResetElements())
            {
                if (unit.IsEmpty)
                    continue;
                var block = unit as AnalyzeBlock;
                if (block != null)
                {
                    if (!unit.IsLock && !unit.IsUnit)
                        ConnectBlock(block, level);
                    pre = block;
                }
                else
                {
                    if (unit.JoinLevel == level && unit.JoinFeature == JoinFeature.Connect)
                    {
                        if (step == 0)
                        {
                            cur = new AnalyzeBlock
                            {
                                Primary = (WordUnit)unit
                            };
                            if (pre != null)
                            {
                                cur.Append(pre);
                                root.Elements.RemoveAt(root.Elements.Count - 1);
                            }
                            else
                            {
                                cur.IsError = true;
                            }
                            root.Append(cur);
                        }
                        step = 1;
                        cur.Append(unit);
                        continue;

                    }
                    pre = ((WordUnit)unit).IsPunctuate || ((WordUnit)unit).IsKeyWord ? null : unit;
                }
                switch (step)
                {
                    case 0:
                        root.Append(unit);
                        break;
                    case 1:
                        step = 2;
                        cur.Append(unit);
                        break;
                    default:
                        step = 0;
                        root.Append(unit);
                        break;
                }
            }
        }


        #endregion
        #region 简单块合并


        private void MergeSimpleBlock(AnalyzeBlock root)
        {
            int level = 1;
            for (; level < 10; level++)
            {
                MergeSimpleBlock(root, level);
            }
        }


        /// <summary>
        ///     分级组合
        ///     0 括号块组合(不使用此方法)
        ///     1 .;单词粘连组合
        ///     2 一元操作符组合(not # -)
        ///     3 串联组合(.. ^)
        ///     4 乘除余指数(* / %)
        ///     5 加减(+ -)
        ///     6 比较(== ~大小于)
        ///     7 逻辑(and)
        ///     8 逻辑(or)
        ///     9 赋值(=)
        /// </summary>
        /// <param name="root"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public void MergeSimpleBlock(AnalyzeBlock root, int level)
        {
            AnalyzeUnitBase preUnit = null;
            var joinNext = false;
            var array = root.Elements.ToArray();
            root.Elements.Clear();
            foreach (var unit in array)
            {
                if (unit.IsEmpty)
                    continue;
                if (unit.IsContent)
                {
                    root.Append(unit);
                    joinNext = false;
                    preUnit = null;
                    continue;
                }

                var block = unit as AnalyzeBlock;
                if (block != null)
                {
                    if (!unit.IsLock && !unit.IsUnit)
                        MergeSimpleBlock(block, level);
                    if (!joinNext)
                    {
                        root.Append(block);
                        preUnit = block;
                    }
                    else
                    {
                        ((AnalyzeBlock)preUnit).Append(block);
                        joinNext = false;
                    }
                    continue;
                }
                var word = (WordUnit)unit;
                if (word.JoinLevel == level)
                {
                    switch (word.JoinFeature)
                    {
                        case JoinFeature.Front:
                            var newBlock = new AnalyzeBlock
                            {
                                Primary = word
                            };
                            root.Append(newBlock);
                            newBlock.Append(unit);
                            preUnit = newBlock;
                            joinNext = true;
                            continue;
                        case JoinFeature.TowWay:
                            newBlock = new AnalyzeBlock
                            {
                                Primary = word
                            };
                            if (preUnit == null)
                            {
                                newBlock.IsError = true;
                            }
                            else
                            {
                                root.Elements.Remove(preUnit);
                                newBlock.Append(preUnit);
                            }
                            root.Append(newBlock);
                            newBlock.Append(unit);
                            preUnit = newBlock;
                            joinNext = true;
                            continue;
                    }
                }
                if (!joinNext)
                {
                    root.Append(word);
                    if (!word.IsSpace && unit.ItemRace != CodeItemRace.Assist)
                    {
                        preUnit = level <= 6 && unit.ItemRace == CodeItemRace.Range ? null : word;
                    }
                    continue;
                }
                if (word.Char == ';')//终止符号出错
                {
                    ((AnalyzeBlock)preUnit).IsError = true;
                    joinNext = false;
                    preUnit = null;
                    continue;
                }
                ((AnalyzeBlock)preUnit).Append(unit);
                if (word.IsSpace || unit.ItemRace == CodeItemRace.Assist)
                    continue;
                if (level < 6)
                    preUnit.IsError = unit.ItemRace == CodeItemRace.Range;
                joinNext = false;
            }
        }


        #endregion
        #region 块合并


        private void MergeBlock()
        {
            MergeBlockValue();
            MergeBracket(Root);
        }

        /// <summary>
        ///     常量组合
        /// </summary>
        /// <returns></returns>
        public void MergeBlockValue()
        {
            Root = new AnalyzeBlock();
            var cur = Root;
            var preType = 0;
            foreach (var word in Words)
            {
                if (word.IsEmpty)
                    continue;
                int type;
                switch (word.ItemType)
                {
                    case CodeItemType.SignleRem:
                    case CodeItemType.MulitRem:
                    case CodeItemType.TemplateConfig:
                    case CodeItemType.TemplateRem:
                        type = 1;
                        break;
                    case CodeItemType.Content:
                        type = 2;
                        break;
                    case CodeItemType.String:
                        type = 3;
                        break;
                    default:
                        if (preType > 0)
                        {
                            cur.Release();
                            cur = cur.Parent;
                            preType = 0;
                        }
                        cur.Append(word);
                        continue;
                }
                if (preType != type)
                {
                    var block = new AnalyzeBlock
                    {
                        IsUnit = true,
                        Parent = cur,
                        Primary = word,
                        IsContent = type == 2,
                        IsLock = true
                    };
                    if (type == 1)
                        block.SetRace(CodeItemRace.Assist, CodeItemFamily.Rem, CodeItemType.MulitRem);
                    else
                        block.SetRace(CodeItemRace.Value, CodeItemFamily.Constant,CodeItemType.String);
                    cur.Append(block);
                    cur = block;
                    preType = type;
                }
                cur.Append(word);
            }
            if (preType > 0)
                cur.Release();
        }

        /// <summary>
        ///     括号块组合
        /// </summary>
        /// <returns></returns>
        public void MergeBracket(AnalyzeBlock root)
        {
            var cur = root;
            Stack<CodeItemType> stack = new Stack<CodeItemType>();
            foreach (var unit in root.ResetElements())
            {
                if (unit.IsEmpty)
                    continue;
                var word = unit as WordUnit;
                if (word == null)
                {
                    cur.Append(unit);
                    continue;
                }
                if (word.IsSpace)
                    continue;
                AnalyzeBlock block;
                switch (unit.ItemType)
                {
                    case CodeItemType.Brackets31:
                        var vw = new WordUnit('.')
                        {
                            Start = unit.Start,
                            End = unit.End,
                            IsReplenish = true,
                            IsKeyWord = true,
                            PrimaryLevel = 1,
                            JoinLevel = 1,
                            JoinFeature = JoinFeature.Connect
                        };
                        vw.SetRace(CodeItemRace.Sentence, CodeItemFamily.Separator, CodeItemType.Separator_Dot);
                        cur.Append(vw);
                        stack.Push(unit.ItemType + 1);
                        block = new AnalyzeBlock
                        {
                            Parent = cur,
                            Primary = word
                        };
                        cur.Append(block);
                        cur = block;
                        break;
                    case CodeItemType.Brackets21:
                    case CodeItemType.Brackets41:
                        stack.Push(unit.ItemType + 1);
                        block = new AnalyzeBlock
                        {
                            Parent = cur,
                            Primary = word
                        };
                        cur.Append(block);
                        cur = block;
                        break;
                    case CodeItemType.Brackets22:
                    case CodeItemType.Brackets32:
                    case CodeItemType.Brackets42:
                        if (stack.Count > 0 && stack.Peek() == unit.ItemType)
                        {
                            stack.Pop();
                            unit.IsError = true;
                            cur.Append(unit);
                            cur.Release();
                            if (cur.Parent == null)
                                cur.IsError = true;
                            else
                                cur = cur.Parent;
                            continue;
                        }
                        break;
                }
                cur.Append(word);
            }
            LinkBarket(root);
        }

        private static void LinkBarket(AnalyzeBlock root)
        {
            AnalyzeUnitBase pre = null;
            AnalyzeBlock cur = null;
            // CodeItemType pretype = CodeItemType.None;
            foreach (var unit in root.ResetElements())
            {
                var block = unit as AnalyzeBlock;
                if (block == null)
                {
                    var word = (WordUnit)unit;
                    pre = word.IsPunctuate || word.IsKeyWord ? null : unit;
                    root.Append(unit);
                    //pretype = CodeItemType.None;
                    continue;
                }
                LinkBarket(block);
                switch (block.ItemType)
                {
                    case CodeItemType.Brackets21:
                        //case CodeItemType.Brackets31:
                        //if (pretype == CodeItemType.None || cur == null)
                        {
                            if (pre == null)
                            {
                                root.Append(unit);
                                break;
                            }
                            if (pre != cur)
                            {
                                cur?.Release();
                                cur = new AnalyzeBlock();
                                var wd = pre as WordUnit;
                                cur.Primary = wd ?? ((AnalyzeBlock)pre).Primary;
                                root.Elements.Remove(pre);
                                cur.Elements.Add(pre);
                                cur.ItemRace = CodeItemRace.Value;
                                cur.ItemFamily = CodeItemFamily.ValueSentence;
                                cur.ItemType = CodeItemType.Brackets21 == block.ItemType
                                    ? CodeItemType.Call
                                    : CodeItemType.Table_Child;
                                root.Elements.Add(cur);
                                pre = cur;
                            }
                            unit.Name = "_call_";
                            cur.Elements.Add(unit);
                        }
                        //root.Elements.Add(block);
                        //pretype = block.ItemType;
                        continue;
                }
                root.Append(unit);
                pre = unit;
            }
            cur?.Release();
        }

        #endregion
        #region 树整理
        public void Release(AnalyzeBlock root)
        {
            foreach (var unit in root.Elements)
            {
                if (unit.IsEmpty)
                    continue;
                if (root.IsError)
                    unit.IsError = true;
                var block = unit as AnalyzeBlock;
                if (block != null)
                {
                    Release(block);
                }
            }
        }


        public void ClearEmpty(AnalyzeBlock root)
        {
            foreach (var unit in root.ResetElements())
            {
                if (unit.IsEmpty)
                    continue;
                var block = unit as AnalyzeBlock;
                if (block != null)
                {
                    block.Parent = root;
                    ClearEmpty(block);
                    if (block.Elements.Count == 0)
                        continue;
                    block.Release();
                    root.Append(block.Elements.Count == 1 ? block.Elements[0] : unit);
                }
                else
                {
                    root.Append(unit);
                }
            }
        }


        #endregion
        #region 显示跟踪


        public string PrintTree(AnalyzeBlock root)
        {
            var builder = new StringBuilder();
            PrintTree(root, builder, 0);
            return builder.ToString();
        }

        private void PrintTree(AnalyzeBlock root, StringBuilder builder, int level)
        {
            foreach (var unit in root.Elements)
            {
                if (unit.IsContent)
                {
                    builder.AppendLine();
                    if (level > 0)
                        builder.Append('_', level);
                    builder.Append("***");
                    continue;
                }
                var block = unit as AnalyzeBlock;
                if (block != null)
                {
                    if (unit.IsLock)
                    {
                        builder.AppendLine();
                        if (level > 0)
                            builder.Append('_', level);
                        foreach (var word in block.Elements.OfType<WordUnit>())
                            builder.Append(word.IsLine ? " " : word.Word);
                        continue;
                    }
                    if (unit.IsError)
                    {
                        builder.AppendLine();
                        if (level > 0)
                            builder.Append('_', level);
                        builder.Append("×");
                    }
                    PrintTree(block, builder, level + 4);
                }
                else
                {
                    var word = (WordUnit)unit;
                    if (!word.IsSpace)
                    {
                        builder.AppendLine();
                        if (level > 0)
                            builder.Append('_', level);
                        if (unit.IsError)
                            builder.Append("×");
                        builder.Append(word.Word);
                        if (word.JoinLevel >= 0)
                        {
                            builder.AppendFormat(" [{0}]", word.JoinLevel);
                        }
                    }
                }
            }
        }


        #endregion

        #region 单词

        /// <summary>
        ///     合并单词,使之成为格式良好的代码单元
        /// </summary>
        public void AnalyzeWord()
        {
            CheckWord();

            foreach (var tm in TemplateElements)
            {
                foreach (var element in tm.Elements.ToArray())
                {
                    foreach (var word in element.Words.ToArray())
                    {
                        if (word.IsEmpty)
                            element.Words.Remove(word);
                    }
                    if (element.Words.Count == 0)
                        tm.Elements.Remove(element);
                }
            }
        }

        /// <summary>
        ///     取得结果单词
        /// </summary>
        private void OutcomeWords()
        {
            CheckWord();
            Words = new List<WordUnit>();
            foreach (var tm in TemplateElements)
            {
                foreach (var element in tm.Elements)
                {
                    foreach (var word in element.Words)
                    {
                        if (!word.IsEmpty && word.ItemFamily != CodeItemFamily.Rem)
                            Words.Add(word);
                    }
                }
            }
        }

        #region 单词类型检查

        private void CheckWord()
        {
            foreach (var tm in TemplateElements)
            {
                if (tm.IsContent)
                    continue;
                WordUnit pre = null;
                foreach (var element in tm.Elements)
                {
                    switch (element.ItemRace)
                    {
                        case CodeItemRace.Completion:
                            pre = null;
                            continue;
                    }
                    foreach (var word in element.Words)
                    {
                        if (word.IsEmpty || word.ItemFamily == CodeItemFamily.Rem)
                        {
                            continue;
                        }
                        if (word.IsSpace || word.ItemType == CodeItemType.String)
                        {
                            pre = null;
                            continue;
                        }
                        if (word.IsPunctuate)
                        {
                            CheckPunctuate(word, ref pre);
                        }
                        else
                        {
                            CheckWord(word);
                        }
                    }
                }
            }
        }

        #endregion

        #region 标点


        private void CheckPunctuate(WordUnit word, ref WordUnit pre)
        {
            switch (word.Char)
            {
                #region 其它

                case '#':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 33;
                    word.JoinLevel = 2;
                    word.ValueType = LuaDataTypeItem.NumberValue;
                    word.JoinFeature = JoinFeature.Front;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Operator, CodeItemType.StringLen);
                    pre = null;
                    break;
                case ';':
                    word.IsKeyWord = true;
                    word.SetRace(CodeItemRace.Control, CodeItemFamily.Control, CodeItemType.Separator_Semicolon);
                    pre = null;
                    break;
                default:
                    pre = null;
                    break;

                #endregion

                #region 扩号区域

                case '{':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 1;
                    word.JoinLevel = 2;
                    word.ValueType = LuaDataTypeItem.TableDefinition;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Range, CodeItemType.Brackets41);
                    pre = null;
                    break;
                case '}':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 1;
                    word.JoinLevel = 2;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Range, CodeItemType.Brackets42);
                    pre = null;
                    break;
                case '(':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 1;
                    word.JoinLevel = 3;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Range, CodeItemType.Brackets21);
                    pre = null;
                    break;
                case ')':
                    word.IsKeyWord = true;
                    word.JoinLevel = 3;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Range, CodeItemType.Brackets22);
                    pre = null;
                    break;
                case '[':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 1;
                    word.JoinLevel = 1;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Range, CodeItemType.Brackets31);
                    pre = null;
                    break;
                case ']':
                    word.IsKeyWord = true;
                    word.JoinLevel = 1;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Range, CodeItemType.Brackets32);
                    pre = null;
                    break;

                #endregion

                #region 算术计算

                case '+':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 1;
                    word.JoinLevel = 5;
                    word.ValueType = LuaDataTypeItem.NumberValue;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compute, CodeItemType.Compute_Add);
                    pre = null;
                    break;
                case '-':
                    word.IsKeyWord = true;
                    word.JoinLevel = 5;
                    word.ValueType = LuaDataTypeItem.NumberValue;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compute, CodeItemType.Compute_Sub);
                    pre = null;
                    break;
                case '*':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 1;
                    word.JoinLevel = 4;
                    word.ValueType = LuaDataTypeItem.NumberValue;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compute, CodeItemType.Compute_Mulit);
                    pre = null;
                    break;
                case '/':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 1;
                    word.JoinLevel = 4;
                    word.ValueType = LuaDataTypeItem.NumberValue;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compute, CodeItemType.Compute_Div);
                    pre = null;
                    break;
                case '%':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 1;
                    word.JoinLevel = 4;
                    word.ValueType = LuaDataTypeItem.NumberValue;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compute, CodeItemType.Compute_Mod);
                    pre = null;
                    break;
                case '^':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 1;
                    word.JoinLevel = 3;
                    word.ValueType = LuaDataTypeItem.NumberValue;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compute, CodeItemType.Compute_Exp);
                    pre = null;
                    break;

                #endregion

                #region 连接符

                case ',':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 1;
                    word.JoinLevel = 3;
                    word.JoinFeature = JoinFeature.Connect;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Separator, CodeItemType.Separator_Comma);
                    pre = null;
                    break;

                case ':':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 1;
                    word.JoinLevel = 1;
                    word.JoinFeature = JoinFeature.Connect;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Separator, CodeItemType.Separator_Colon);
                    pre = null;
                    break;
                case '.':
                    if (pre == null || pre.FirstChar != '.')
                    {
                        word.IsKeyWord = true;
                        word.PrimaryLevel = 1;
                        word.JoinLevel = 1;
                        word.JoinFeature = JoinFeature.Connect;
                        word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Separator, CodeItemType.Separator_Dot);
                        pre = word;
                    }
                    else if (pre.Chars.Count == 1)
                    {
                        pre.Append(word);
                        word.Clear();
                        pre.JoinLevel = 2;
                        pre.ValueType = LuaDataTypeItem.StringValue;
                        pre.SetRace(CodeItemRace.Sentence, CodeItemFamily.Separator, CodeItemType.Separator_StringJoin);
                    }
                    else
                    {
                        pre.Append(word);
                        word.Clear();
                        word.IsKeyWord = false;
                        pre.JoinLevel = -1;
                        pre.ValueType = LuaDataTypeItem.TableDefinition;
                        pre.JoinFeature = JoinFeature.None;
                        pre.SetRace(CodeItemRace.Variable, CodeItemFamily.Variable, CodeItemType.Variable_Arg);
                        pre = null;
                    }
                    break;
                #endregion

                #region 逻辑比较

                case '>':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 1;
                    word.ValueType = LuaDataTypeItem.BoolValue;
                    word.JoinLevel = 6;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare, CodeItemType.Compare_Greater);
                    pre = word;
                    break;
                case '<':
                    word.IsKeyWord = true;
                    word.PrimaryLevel = 1;
                    word.ValueType = LuaDataTypeItem.BoolValue;
                    word.JoinLevel = 6;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare, CodeItemType.Compare_Less);
                    pre = word;
                    break;
                case '~':
                    pre = word;
                    break;
                case '=':
                    bool canLink = false;
                    if (pre != null)
                    {
                        switch (pre.Char)
                        {
                            case '>':
                                canLink = true;
                                pre.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare, CodeItemType.Compare_GreaterEqual);
                                break;
                            case '<':
                                canLink = true;
                                pre.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare, CodeItemType.Compare_LessEqual);
                                break;
                            case '~':
                                canLink = true;
                                pre.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare, CodeItemType.Compare_NotEqual);
                                break;
                            case '=':
                                canLink = true;
                                pre.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare, CodeItemType.Compare_Equal);
                                break;
                        }
                    }
                    if (canLink)
                    {
                        pre.Append(word);
                        word.Clear();
                        pre.PrimaryLevel = 1;
                        pre.ValueType = LuaDataTypeItem.BoolValue;
                        pre.JoinLevel = 6;
                        pre.JoinFeature = JoinFeature.TowWay;
                        pre = null;
                    }
                    else
                    {
                        word.PrimaryLevel = 1;
                        word.IsKeyWord = true;
                        word.JoinLevel = 9;
                        word.JoinFeature = JoinFeature.TowWay;
                        word.SetRace(CodeItemRace.Sentence, CodeItemFamily.SetValue, CodeItemType.Separator_Equal);
                        pre = word;
                    }
                    break;

                    #endregion
            }
        }

        #endregion
        #region 关键字

        private readonly List<string> _keyWords = new List<string>
        {
            "nil",
            "true",
            "false",
            "local",
            "return",
            "function",
            "and",
            "not",
            "or",
            "if",
            "else",
            "elseif",
            "repeat",
            "until",
            "while",
            "for",
            "in",
            "then",
            "go",
            "do",
            "end",
            "break"
        };


        private void CheckWord(WordUnit word)
        {
            if (char.IsNumber(word.Chars[0]))
            {
                word.ValueType = LuaDataTypeItem.NumberValue;
                word.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.Number);
                return;
            }
            if (!_keyWords.Contains(word.Word))
            {
                word.Name = word.Word;
                word.SetRace(CodeItemRace.Variable, CodeItemFamily.Variable);
                return;
            }
            word.IsKeyWord = true;
            switch (word.Word)
            {
                default:
                    word.SetRace(CodeItemRace.None, CodeItemFamily.KeyWord);
                    break;
                case "nil":
                    word.ValueType = LuaDataTypeItem.Nil;
                    word.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.Value_Null);
                    break;
                case "true":
                    word.ValueType = LuaDataTypeItem.BoolValue;
                    word.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.Value_True);
                    break;
                case "false":
                    word.ValueType = LuaDataTypeItem.BoolValue;
                    word.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.Value_False);
                    break;
                case "and":
                    word.JoinLevel = 7;
                    word.PrimaryLevel = 1;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Logical, CodeItemType.Key_And);
                    break;
                case "or":
                    word.JoinLevel = 8;
                    word.PrimaryLevel = 1;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Logical, CodeItemType.Key_Or);
                    break;
                case "not":
                    word.JoinLevel = 2;
                    word.PrimaryLevel = 1;
                    word.JoinFeature = JoinFeature.Front;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare, CodeItemType.Key_Not);
                    break;
                case "local":
                    word.JoinLevel = 2;
                    word.JoinFeature = JoinFeature.Front;
                    word.PrimaryLevel = 33;
                    word.SetRace(CodeItemRace.Control, CodeItemFamily.Scope, CodeItemType.Key_Local);
                    break;
                case "break":
                    word.SetRace(CodeItemRace.Control, CodeItemFamily.Control, CodeItemType.Key_Break);
                    break;
                case "return":
                    word.JoinLevel = 2;
                    word.PrimaryLevel = 1;
                    word.JoinFeature = JoinFeature.Front;
                    word.SetRace(CodeItemRace.Control, CodeItemFamily.Control, CodeItemType.Key_Return);
                    break;
                case "for":
                    word.JoinLevel = 12;
                    word.PrimaryLevel = 1;
                    word.JoinFeature = JoinFeature.RangeOpen;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Iterator, CodeItemType.Key_For);
                    break;
                case "in":
                    word.JoinLevel = 9;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Control, CodeItemType.Key_In);
                    break;
                case "if":
                    word.JoinLevel = 11;
                    word.PrimaryLevel = 1;
                    word.JoinFeature = JoinFeature.RangeOpen;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Condition, CodeItemType.Key_If);
                    break;
                case "while":
                    word.JoinLevel = 11;
                    word.PrimaryLevel = 1;
                    word.JoinFeature = JoinFeature.RangeOpen;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Condition, CodeItemType.Key_While);
                    break;
                case "else":
                    word.PrimaryLevel = 3;
                    word.JoinFeature = JoinFeature.RangeShift;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Condition, CodeItemType.Key_Else);
                    break;
                case "elseif":
                    word.PrimaryLevel = 2;
                    word.JoinFeature = JoinFeature.RangeShift;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Condition, CodeItemType.Key_Elseif);
                    break;
                case "then":
                    word.JoinFeature = JoinFeature.RangeShift;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Control, CodeItemType.Key_Then);
                    break;
                case "go":
                    word.JoinFeature = JoinFeature.RangeShift;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Control, CodeItemType.Key_Go);
                    break;
                case "repeat":
                    word.JoinLevel = 10;
                    word.JoinFeature = JoinFeature.RangeOpen;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Condition, CodeItemType.Key_Repeat);
                    break;
                case "until":
                    word.JoinLevel = 10;
                    word.PrimaryLevel = 1;
                    word.JoinFeature = JoinFeature.RangeClose;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Condition, CodeItemType.Key_Until);
                    break;
                case "function":
                    word.JoinLevel = 11;
                    word.PrimaryLevel = 1;
                    word.JoinFeature = JoinFeature.RangeOpen;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.FunctionRange, CodeItemType.Key_Function);
                    break;
                case "do":
                    word.JoinLevel = 11;
                    word.PrimaryLevel = 2;
                    word.JoinFeature = JoinFeature.RangeOpen;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Range, CodeItemType.Key_Do);
                    break;
                case "end":
                    word.JoinLevel = 11;
                    word.JoinFeature = JoinFeature.RangeClose;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Range, CodeItemType.Key_End);
                    break;
            }
        }

        #endregion
        #endregion

    }
}