// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-11-10
// 修改:2014-11-10
// *****************************************************/

/*
 仿Razor语法糖
 ***@语法糖的前导出现,必须是@与规定字符,不允许出现空格
 ***配对(成对匹配),使用先进先出的方式匹配,语法块或变量中的C#注释与字符串中的内容不参与匹配
 * 例:
 @{
    if(true)
    {
        var txt="这里的}不参与匹配";//这个注释的{{也不参与匹配
    }
 }
 <input type='text'/>
 *此时@{代码块的的结束在第六行的}处,解析结果代码类似如下
    if(true)
    {
        var txt="这里的}不参与匹配";//这个注释的{{也不参与匹配
    }
    Append(" <input type='text'/>");
 [语法块]
 1 @-...:单行代码
 2 @{...}:大括号配对内部的内容的解析为一般代码,且无@转换输出,如需要输出使用内部的Append方法
 *例:
 @{
    Append("123");
 }
 <input type='text'/>
 *结果代码(可编译通过的)
    Append("123");
    Append(" <input type='text'/>");
    *例:
 @{
    @:123
 }
 <input type='text'/>
 *结果代码(无法编译通过的)
    @:123
    Append(" <input type='text'/>");

 3 @控制关键字:
    控制关键字指if else for foreach switch,
    按C#语法要求,除无后if的else外,匹配到小括号配对,else后允许出现的if也使用这种匹配(后续内容在匹配之后)
    *如果后方还有范围内的代码时,
        A 接{然后在新行开始使用@{...}方式
        B 接{+@之后的内容都是普通代码,直到出现单独一行的有且只有两个@@的地方结束代码块
    *如果要明确指明后方内容在控制器的范围,
        C后方跟一个{表示范围开始,A方式与此相同
        D结束为成对匹配的[换行+@+}+换行],即内容中再次出现指定范围方式的控制块时,按堆栈方式匹配
    *如果不指明控制器的范围,内容范围在下一个代码块出现前结束(严重不建议此方法)
    *例:
 @if(true)
 {@
    Append("123");
@@
    <input type='text'/>
    <input type='text'/>
 @}
 *结果代码(可编译通过的)
 if(true)
 {
    Append("123");
    Append(@" <input type='text'/>
    <input type='text'/>");
 }
    *例:
 @if(true)
 <input type='text'/>
 <input type='text'/>
 @else
 <input type='text'/>
 @{ var a = 10;}
 *结果代码(可编译通过的)
 if(true)
    Append(@" <input type='text'/>
    <input type='text'/>");
 else
    Append(@" <input type='text'/>
    <input type='text'/>");
 var a = 10;
    *例:
 @if(true)
 <input type='text'/>
 <input type='text'/>
 @if(true)
 <input type='text'/>
 <input type='text'/>
 @else
 <input type='text'/>
 @{ var a = 10;}
 *结果代码(可编译通过的)
 if(true)
 {
    Append(@" <input type='text'/>
    <input type='text'/>");
     if(true)
     {
        Append(@" <input type='text'/>
        <input type='text'/>");
     }
     else
     {
     Append(@" <input type='text'/>
        <input type='text'/>");
     }
 }
 var a = 10;
 [变量]
 1 属性或字段输出
 @名称.名称:
    名称是字母数字下划线组成的包括各种文字(单词以ASCII128内的非字母数字下划线的字符结束),后方可以连接.字符(允许.前后间隔空白)以访问属性或字段
    后方的()的方法调用不支持,需要方法返回值,请用@()模式
    *例:
    <input type='@name.Length'/>
    *结果代码(可编译通过的)
     Append(@"    <input type='{0}'/>",name.Length);
 2 内容输出
   @():小括号配对中的内容直接解析为变量输出(所有一个变量输出\方法调用\强制转换均可用),如无返回值的方法调用会导致编译错误
 *例:
    <input type='@(name.ToString())'/>
    *结果代码(可编译通过的)
     Append(@"    <input type='{0}'/>",name.ToString());
 普通输出
 1两个@,输出@字符
 2 @之后不符合以上原则的,@作为普通输出,即使@+空白+{也是如此
 3 其它不符合以上语法糖的内容
 4 代码块之后的换行不输出
 5 @*...*@为注释,不输出
 
 防错建议
 1 方法调用返回值的输出,必须使用@(方法())方式
 2 方法在控制块中,如果有[换行+@+}+换行]或[换行+@+}+换行]的普通输出时,请用@@{
 3 使用属性或字段输出时,如果后方连接成单词的容易造成歧义,请使用@()方式,如:
    例子A 
    有变量 name且值为123,想要输出的结果为 str123456,写法为string str@(name)456,
    如写成str@name456,将可能使用已定义的变量name456输出或运行时因为没有这个变量出错
    例子B
    有变量 name且值为id,想要输出的结果为 str_id_name,写法为string str_@(name)_name,
    如写成str_@name_name,将可能使用已定义的变量name_name输出或运行时因为没有这个变量出错
*/
#region 引用

using System.Collections.Generic;
using System.Text;
using Agebull.CodeRefactor.CodeAnalyze;

#endregion

namespace Agebull.CodeRefactor.CodeTemplate.LUA
{
    /// <summary>
    ///     代码单元分析器
    /// </summary>
    public sealed class TemplateParse : TemplateParseBase
    {

        #region 分析文本

        /// <summary>
        ///     分析文本
        /// </summary>
        public void Analyze()
        {
            Compile();
        }

        #endregion

        #region 代码生成

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <returns></returns>
        protected override string BuildCode()
        {
            var elements1 = AllToCode();
            LevelCode(elements1);

            StringBuilder code = new StringBuilder();
            code.AppendLine(@"if not __code then  __code = '' end");
            code.AppendLine($"function {Config.Name}()");
            foreach (var element in elements1)
            {
                code.Append(element.Code());
            }
            code.AppendLine();
            code.AppendLine("    return __code");
            code.AppendLine("end");
            return code.ToString();
        }

        private void LevelCode(List<AnalyzeElement> elements1)
        {
            int level = 4;
            foreach (var aelement in elements1)
            {
                if (aelement.Elements[0].ItemType != CodeItemType.Line)
                {
                    aelement.Insert(0, CreateLineElement());
                }
                WordUnit pre = null;
                foreach (var element in aelement.Elements)
                {
                    switch (element.ItemRace)
                    {
                        case CodeItemRace.Value:
                        case CodeItemRace.Variable:
                        case CodeItemRace.Assist:
                            pre = null;
                            continue;
                    }
                    foreach (var word in element.Words)
                    {
                        if (word.IsPunctuate)
                        {
                            if (!word.IsSpace)
                            {
                                pre = null;
                            }
                            else if (word.IsLine)
                            {
                                pre = word;
                                pre.Level = level;
                            }
                            continue;
                        }
                        switch (word.Word)
                        {
                            case "end":
                            case "until":
                            case "elseif":
                                level -= 4;
                                if (pre != null)
                                {
                                    pre.Level = level;
                                }
                                break;
                            case "else":
                                if (pre != null && level > 4)
                                {
                                    pre.Level = level - 4;
                                }
                                break;
                            case "do":
                            case "go":
                            case "then":
                            case "repeat":
                                level += 4;
                                break;
                        }
                        pre = null;
                    }
                }
            }
        }

        public List<AnalyzeElement> AllToCode()
        {
            var elements = MergeElements();
            int idx = 0;
            List<AnalyzeElement> result = new List<AnalyzeElement>();
            foreach (var element in elements)
            {
                if (!element.IsContent)
                {
                    result.Add(element);
                    continue;
                }
                string content = element.Content();
                if (string.IsNullOrWhiteSpace(content))
                    continue;
                if (idx == 0)
                    content = content.TrimStart();
                var newElement = new AnalyzeElement();
                newElement.Append($"str{++idx} = ");
                newElement.Append("[[");
                newElement.Append(content);
                newElement.Append("]]");
                newElement.Append(CreateLineElement());
                if (element.Assists.Count > 0)
                {
                    var format = new StringBuilder();
                    format.Append($"str{++idx} = string.format(str{idx - 1}");
                    foreach (var arg in element.Assists)
                    {
                        format.AppendFormat(" , tostring({0})", arg.Code().Trim());
                    }
                    format.Append(")");
                    newElement.Append(format.ToString());
                    newElement.Append(CreateLineElement());
                }
                newElement.Append($"__code = __code .. str{idx}");
                result.Add(newElement);
            }
            return result;
        }

        public List<AnalyzeElement> MergeElements()
        {
            foreach (var element in TemplateElements)
            {
                element.Elements.ForEach(p => p.Release());
            }
            List<AnalyzeElement> elements = new List<AnalyzeElement>();
            AnalyzeElement preValue = null;
            foreach (var element in TemplateElements)
            {
                if (element.Elements.Count == 0)
                    continue;
                if (element.IsContent)
                {
                    if (preValue != null)
                    {
                        element.Elements.ForEach(p => preValue.Append(p));
                    }
                    else
                    {
                        elements.Add(element);
                        preValue = element;
                    }
                }
                else if (element.IsBlock)
                {
                    if (preValue == null)
                    {
                        elements.Add(preValue = new AnalyzeElement
                        {
                            IsContent = true
                        });
                    }
                    preValue.Append("%s");
                    preValue.AddAssist(element);
                }
                else
                {
                    preValue = null;
                    elements.Add(element);
                }
            }
            return elements;
        }

        #endregion

        #region 语法糖检查

        /// <summary>
        ///     检查单词打头的代码块
        /// </summary>
        /// <returns></returns>
        protected override bool CheckWordStartCode()
        {
            WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
            var codeElement = new AnalyzeElement();
            switch (WordElements[CurWordIndex].RealWord)
            {
                case "end": //@end表示LUA范围结束
                    return CheckIsCodeBlockEnd(codeElement);
                case "if":
                case "elseif":
                    codeElement.Append(WordElements[CurWordIndex]);
                    //FindCouple(codeElement, '(', ')', true, true);
                    //TryGoNextWithWord(codeElement, "then", true);
                    StepEndWord(codeElement, "then");
                    CheckMulitCode(codeElement);
                    break;
                case "for":
                    codeElement.Append(WordElements[CurWordIndex]);
                    StepEndWord(codeElement, "do");
                    CheckMulitCode(codeElement);
                    break;
                case "while":
                    codeElement.Append(WordElements[CurWordIndex]);
                    //FindCouple(codeElement, '(', ')', true, true);
                    //TryGoNextWithWord(codeElement, "go", true);
                    StepEndWord(codeElement, "go");
                    CheckMulitCode(codeElement);
                    break;
                case "else":
                case "repeat":
                    codeElement.Append(WordElements[CurWordIndex]);
                    CheckMulitCode(codeElement);
                    break;
                case "until":
                    codeElement.Append(WordElements[CurWordIndex]);
                    FindCouple(codeElement, '(', ')', true, true);
                    break;
                default:
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.Sharp);
                    FindVariable(codeElement, '.', ':');
                    codeElement.IsBlock = true;
                    foreach (var item in codeElement.Elements)
                        item.Words.ForEach(p => p.IsBlock = true);
                    break;
            }
            JoinCode(codeElement);
            return true;
        }

        #endregion


        #region 合并LUA内容

        /// <summary>
        ///     处理当前语言内容
        /// </summary>
        /// <param name="codeElement">语言节点</param>
        /// <param name="preChar"></param>
        public override void MergeLanWord(AnalyzeElement codeElement, ref char preChar)
        {
            WordElement cur = WordElements[CurWordIndex];
            codeElement.Append(cur);
            if (!cur.IsPunctuate)
            {
                preChar = '\0';
                return;
            }
            switch (cur.Char)
            {
                #region 注释

                case '-':
                    if (preChar != '-')
                    {
                        preChar = '-';
                        return;
                    }
                    {
                        var block = codeElement.Elements[codeElement.Elements.Count - 2];
                        block.Append(cur);
                        codeElement.Elements.RemoveAt(codeElement.Elements.Count - 1);
                        MergeLuaRem(block);
                        block.SetRace(CodeItemRace.Assist, CodeItemFamily.Rem);
                    }
                    break;

                #endregion

                #region 字符串

                case '\"':
                case '\'':
                    MergeString(cur, cur.Char);
                    break;
                case '[':
                    if (preChar != '[')
                    {
                        preChar = '[';
                        return;
                    }
                    {
                        var block = codeElement.Elements[codeElement.Elements.Count - 2];
                        block.Append(cur);
                        codeElement.Elements.RemoveAt(codeElement.Elements.Count - 1);
                        MergeMulitString(block);
                        block.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.String);
                    }
                    break;

                    #endregion
            }
            preChar = '\0';
        }


        /// <summary>
        ///     合并多行文本
        /// </summary>
        /// <param name="codeElement"></param>
        private void MergeMulitString(WordElement codeElement)
        {
            bool preShift = false;
            for (CurWordIndex++; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                WordElement cur = WordElements[CurWordIndex];
                codeElement.Append(cur);
                if (cur.IsPunctuate && cur.Char == ']')
                {
                    if (preShift)
                        return;
                    preShift = true;
                }
                else
                {
                    preShift = false;
                }
            }
        }

        /// <summary>
        ///     合并注释
        /// </summary>
        /// <param name="element"></param>
        private void MergeLuaRem(WordElement element)
        {
            if (IsWithLineEnd())
            {
                return;
            }
            element.Append(WordElements[++CurWordIndex]);
            var a = WordElements[CurWordIndex].Char;
            if (IsWithLineEnd())
            {
                return;
            }
            element.Append(WordElements[++CurWordIndex]);
            var b = WordElements[CurWordIndex].Char;
            if (a == '[' && b == '[') //多行注释
            {
                bool isShift = false;
                for (CurWordIndex++; CurWordIndex < WordElements.Count; CurWordIndex++)
                {
                    element.Append(WordElements[++CurWordIndex]);
                    if (WordElements[CurWordIndex].Char == ']') //多行注释结束
                    {
                        if (isShift)
                            return;
                        isShift = true;
                    }
                    else if (isShift)
                    {
                        isShift = false;
                    }
                }
            }
            else
            {
                for (CurWordIndex++; CurWordIndex < WordElements.Count; CurWordIndex++)
                {
                    if (WordElements[CurWordIndex].IsLine) //行已结束
                    {
                        --CurWordIndex; //回退
                        return;
                    }
                    element.Append(WordElements[++CurWordIndex]);
                }
            }
        }

        #endregion

        #region LUA深度分析

        

        #endregion
    }
}
