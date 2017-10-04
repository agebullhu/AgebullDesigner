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

using System.Text;

#endregion

namespace Agebull.EntityModel.RobotCoder.CodeTemplate.CSharp
{
    /// <summary>
    ///     代码单元分析器
    /// </summary>
    public sealed class TemplateParse : TemplateParseBase
    {

        #region 分析文本

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <returns></returns>
        protected override string BuildCode()
        {
            StringBuilder codeElement = new StringBuilder();
            codeElement.AppendLine($"public string {Config.Name}()");
            codeElement.AppendLine("{");
            codeElement.AppendLine("    StringBuilder strResult = new StringBuilder();");
            //foreach (var line in Codes)
            //{
            //    codeElement.AppendLine(line);
            //}
            codeElement.AppendLine("    return strResult.ToString();");
            codeElement.AppendLine("}");
            return codeElement.ToString();
        }

        #endregion


        #region 合并单词

        ///// <summary>
        /////     合并普通文本内容块
        ///// </summary>
        ///// <param name="curElement">要合并部分</param>
        //protected override void MergeContentElement(WordElement curElement)
        //{
        //    if (curElement == null)
        //        return;
        //    if (curElement.Element.IsPunctuate)
        //    {
        //        switch (curElement.Char)
        //        {
        //            case '{':
        //            case '}':
        //            case '\"':
        //                curElement.Append(curElement.Element.End, curElement.Char);
        //                break;
        //        }
        //    }
        //    if (contentElement == null)
        //        contentElement = curElement;
        //    else
        //        contentElement.Merge(curElement);
        //}

        //        /// <summary>
        //        /// 以当前语言的代码方式的普通文本内容拼接的代码
        //        /// </summary>
        //        /// <param name="content">普通内容文本</param>
        //        /// <returns></returns>
        //        protected override string AppendContentCode(string content)
        //        {
        //            return $@"{CodeSpace}strResult.Append($@""
        //{content}"");";
        //        }

        /// <summary>
        /// 检查标点打头的代码块
        /// </summary>
        /// <returns></returns>
        protected override bool CheckPunctuateStartCode()
        {
            if (WordElements[CurWordIndex].Char == '}')
            {
                var codeElement = new AnalyzeElement();
                return CheckIsCodeBlockEnd(codeElement);
            }
            return base.CheckPunctuateStartCode();
        }

        /// <summary>
        /// 检查单词打头的代码块
        /// </summary>
        /// <returns></returns>
        protected override bool CheckWordStartCode()
        {
            var codeElement = new AnalyzeElement();
            switch (WordElements[CurWordIndex].RealWord)
            {
                case "if":
                case "for":
                case "foreach":
                case "switch":
                    codeElement.ItemRace = CodeItemRace.Range;
                    codeElement.Append(WordElements[CurWordIndex]);
                    FindCouple(codeElement, '(', ')', true, true);
                    TryGoNextWithWord(codeElement, "{", true);
                    CheckMulitCode(codeElement);
                    break;
                case "else":
                    //试图查找后续的if
                    codeElement.ItemRace = CodeItemRace.Range;
                    codeElement.Append(WordElements[CurWordIndex]);
                    if (TryGoNextWithWord(codeElement, "if", true))
                    {
                        FindCouple(codeElement, '(', ')', true, true);
                    }
                    TryGoNextWithWord(codeElement, "{", true);
                    CheckMulitCode(codeElement);
                    break;
                default:
                    FindVariable(codeElement, '.');
                    break;
            }
            JoinCode(codeElement);
            return true;
        }

        ///// <summary>
        ///// 组合代码
        ///// </summary>
        ///// <param name="isCodeBlock"></param>
        ///// <param name="codeElement"></param>
        //void JoinCode(bool isCodeBlock, WordElement codeElement)
        //{
        //    if (contentElement != null)
        //    {
        //        if (isCodeBlock)
        //        {
        //            AppendContentCode(contentElement);
        //            contentElement = null;
        //        }
        //        else
        //        {
        //            contentElement.Append(codeElement.Element.End, $"{{{codeElement.RealWord.Trim()}}}");
        //        }
        //    }
        //    else if (!isCodeBlock)
        //    {
        //        Codes.Add($"strResult.Append({codeElement.RealWord.Trim()});");
        //    }
        //    if (isCodeBlock && codeElement != null)
        //    {
        //        Codes.Add(codeElement.RealWord);
        //    }
        //    if (CurWordIndex < this.WordElements.Count && this.WordElements[CurWordIndex].Element.IsSpace)
        //    {
        //        CurWordIndex--; //空白还回去
        //    }
        //}



        #endregion

        #region 特殊单元的组合

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

                case '/':
                    if (preChar != '/')
                    {
                        preChar = '/';
                        return;
                    }
                    MergeSinleRem(codeElement);
                    break;
                case '*':
                    if (preChar == '/')
                    {
                        MergeMulitRem(codeElement);
                    }
                    break;

                #endregion

                #region 字符串

                case '@':
                    preChar = '@';
                    return;
                case '\"':
                    if (preChar == '@')
                    {
                        var block = codeElement.Elements[codeElement.Elements.Count - 2];
                        block.Append(cur);
                        codeElement.Elements.RemoveAt(codeElement.Elements.Count - 1);
                        MergeMulitString(block);
                    }
                    else
                    {
                        MergeString(cur, '\"');
                    }
                    break;
                case '\'':
                    MergeString(cur, '\'');
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
            bool isShift = false;
            for (CurWordIndex++; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                WordElement curElement = WordElements[CurWordIndex];
                if (curElement.Char == '\"')
                {
                    isShift = !isShift;
                    codeElement.Append(curElement);
                    continue;
                }
                if (isShift)
                {
                    --CurWordIndex;//回退
                    return;
                }
                codeElement.Append(curElement);
            }
        }

        /// <summary>
        ///     合并多行注释
        /// </summary>
        /// <param name="codeElement"></param>
        private void MergeMulitRem(AnalyzeElement codeElement)
        {
            var last = codeElement.Elements[codeElement.Elements.Count - 1];
            codeElement.Elements.RemoveAt(codeElement.Elements.Count - 1);
            var element = codeElement.Elements[codeElement.Elements.Count - 1];
            element.ItemRace = CodeItemRace.Assist;
            element.ItemFamily = CodeItemFamily.Rem;
            element.Word += last.Word;
            codeElement.CurrentElement = element;

            bool isShift = false;
            for (CurWordIndex++; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                WordElement curElement = WordElements[CurWordIndex];
                element.Word += curElement.RealWord;
                if (isShift && curElement.Char == '/')//注释结束
                {
                    return;
                }
                isShift = curElement.Char == '*';
            }
        }
        /// <summary>
        ///     合并注释
        /// </summary>
        /// <param name="codeElement"></param>
        private void MergeSinleRem(AnalyzeElement codeElement)
        {
            var last = codeElement.Elements[codeElement.Elements.Count - 1];
            codeElement.Elements.RemoveAt(codeElement.Elements.Count - 1);
            var element = codeElement.Elements[codeElement.Elements.Count - 1];
            element.ItemRace = CodeItemRace.Assist;
            element.ItemFamily = CodeItemFamily.Rem;
            element.Word += last.Word;
            codeElement.CurrentElement = element;

            for (CurWordIndex++; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                if (WordElements[CurWordIndex].IsLine) //行已结束
                {
                    --CurWordIndex; //回退
                    return;
                }
                element.Word += WordElements[CurWordIndex].RealWord;
            }
        }

        #endregion
    }
}
