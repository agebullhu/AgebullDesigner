// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// ����:2014-11-10
// �޸�:2014-11-10
// *****************************************************/

/*
 ��Razor�﷨��
 ***@�﷨�ǵ�ǰ������,������@��涨�ַ�,��������ֿո�
 ***���(�ɶ�ƥ��),ʹ���Ƚ��ȳ��ķ�ʽƥ��,�﷨�������е�C#ע�����ַ����е����ݲ�����ƥ��
 * ��:
 @{
    if(true)
    {
        var txt="�����}������ƥ��";//���ע�͵�{{Ҳ������ƥ��
    }
 }
 <input type='text'/>
 *��ʱ@{�����ĵĽ����ڵ����е�}��,�������������������
    if(true)
    {
        var txt="�����}������ƥ��";//���ע�͵�{{Ҳ������ƥ��
    }
    Append(" <input type='text'/>");
 [�﷨��]
 1 @-...:���д���
 2 @{...}:����������ڲ������ݵĽ���Ϊһ�����,����@ת�����,����Ҫ���ʹ���ڲ���Append����
 *��:
 @{
    Append("123");
 }
 <input type='text'/>
 *�������(�ɱ���ͨ����)
    Append("123");
    Append(" <input type='text'/>");
    *��:
 @{
    @:123
 }
 <input type='text'/>
 *�������(�޷�����ͨ����)
    @:123
    Append(" <input type='text'/>");

 3 @���ƹؼ���:
    ���ƹؼ���ָif else for foreach switch,
    ��C#�﷨Ҫ��,���޺�if��else��,ƥ�䵽С�������,else��������ֵ�ifҲʹ������ƥ��(����������ƥ��֮��)
    *����󷽻��з�Χ�ڵĴ���ʱ,
        A ��{Ȼ�������п�ʼʹ��@{...}��ʽ
        B ��{+@֮������ݶ�����ͨ����,ֱ�����ֵ���һ�е�����ֻ������@@�ĵط����������
    *���Ҫ��ȷָ���������ڿ������ķ�Χ,
        C�󷽸�һ��{��ʾ��Χ��ʼ,A��ʽ�����ͬ
        D����Ϊ�ɶ�ƥ���[����+@+}+����],���������ٴγ���ָ����Χ��ʽ�Ŀ��ƿ�ʱ,����ջ��ʽƥ��
    *�����ָ���������ķ�Χ,���ݷ�Χ����һ����������ǰ����(���ز�����˷���)
    *��:
 @if(true)
 {@
    Append("123");
@@
    <input type='text'/>
    <input type='text'/>
 @}
 *�������(�ɱ���ͨ����)
 if(true)
 {
    Append("123");
    Append(@" <input type='text'/>
    <input type='text'/>");
 }
    *��:
 @if(true)
 <input type='text'/>
 <input type='text'/>
 @else
 <input type='text'/>
 @{ var a = 10;}
 *�������(�ɱ���ͨ����)
 if(true)
    Append(@" <input type='text'/>
    <input type='text'/>");
 else
    Append(@" <input type='text'/>
    <input type='text'/>");
 var a = 10;
    *��:
 @if(true)
 <input type='text'/>
 <input type='text'/>
 @if(true)
 <input type='text'/>
 <input type='text'/>
 @else
 <input type='text'/>
 @{ var a = 10;}
 *�������(�ɱ���ͨ����)
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
 [����]
 1 ���Ի��ֶ����
 @����.����:
    ��������ĸ�����»�����ɵİ�����������(������ASCII128�ڵķ���ĸ�����»��ߵ��ַ�����),�󷽿�������.�ַ�(����.ǰ�����հ�)�Է������Ի��ֶ�
    �󷽵�()�ķ������ò�֧��,��Ҫ��������ֵ,����@()ģʽ
    *��:
    <input type='@name.Length'/>
    *�������(�ɱ���ͨ����)
     Append(@"    <input type='{0}'/>",name.Length);
 2 �������
   @():С��������е�����ֱ�ӽ���Ϊ�������(����һ���������\��������\ǿ��ת��������),���޷���ֵ�ķ������ûᵼ�±������
 *��:
    <input type='@(name.ToString())'/>
    *�������(�ɱ���ͨ����)
     Append(@"    <input type='{0}'/>",name.ToString());
 ��ͨ���
 1����@,���@�ַ�
 2 @֮�󲻷�������ԭ���,@��Ϊ��ͨ���,��ʹ@+�հ�+{Ҳ�����
 3 ���������������﷨�ǵ�����
 4 �����֮��Ļ��в����
 5 @*...*@Ϊע��,�����
 
 ������
 1 �������÷���ֵ�����,����ʹ��@(����())��ʽ
 2 �����ڿ��ƿ���,�����[����+@+}+����]��[����+@+}+����]����ͨ���ʱ,����@@{
 3 ʹ�����Ի��ֶ����ʱ,��������ӳɵ��ʵ������������,��ʹ��@()��ʽ,��:
    ����A 
    �б��� name��ֵΪ123,��Ҫ����Ľ��Ϊ str123456,д��Ϊstring str@(name)456,
    ��д��str@name456,������ʹ���Ѷ���ı���name456���������ʱ��Ϊû�������������
    ����B
    �б��� name��ֵΪid,��Ҫ����Ľ��Ϊ str_id_name,д��Ϊstring str_@(name)_name,
    ��д��str_@name_name,������ʹ���Ѷ���ı���name_name���������ʱ��Ϊû�������������
*/
#region ����

using System.Collections.Generic;
using System.Text;
using Agebull.CodeRefactor.CodeAnalyze;

#endregion

namespace Agebull.CodeRefactor.CodeTemplate.LUA
{
    /// <summary>
    ///     ���뵥Ԫ������
    /// </summary>
    public sealed class TemplateParse : TemplateParseBase
    {

        #region �����ı�

        /// <summary>
        ///     �����ı�
        /// </summary>
        public void Analyze()
        {
            Compile();
        }

        #endregion

        #region ��������

        /// <summary>
        /// ���ɴ���
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

        #region �﷨�Ǽ��

        /// <summary>
        ///     ��鵥�ʴ�ͷ�Ĵ����
        /// </summary>
        /// <returns></returns>
        protected override bool CheckWordStartCode()
        {
            WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
            var codeElement = new AnalyzeElement();
            switch (WordElements[CurWordIndex].RealWord)
            {
                case "end": //@end��ʾLUA��Χ����
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


        #region �ϲ�LUA����

        /// <summary>
        ///     ����ǰ��������
        /// </summary>
        /// <param name="codeElement">���Խڵ�</param>
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
                #region ע��

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

                #region �ַ���

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
        ///     �ϲ������ı�
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
        ///     �ϲ�ע��
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
            if (a == '[' && b == '[') //����ע��
            {
                bool isShift = false;
                for (CurWordIndex++; CurWordIndex < WordElements.Count; CurWordIndex++)
                {
                    element.Append(WordElements[++CurWordIndex]);
                    if (WordElements[CurWordIndex].Char == ']') //����ע�ͽ���
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
                    if (WordElements[CurWordIndex].IsLine) //���ѽ���
                    {
                        --CurWordIndex; //����
                        return;
                    }
                    element.Append(WordElements[++CurWordIndex]);
                }
            }
        }

        #endregion

        #region LUA��ȷ���

        

        #endregion
    }
}
