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

using System.Text;

#endregion

namespace Agebull.EntityModel.RobotCoder.CodeTemplate.CSharp
{
    /// <summary>
    ///     ���뵥Ԫ������
    /// </summary>
    public sealed class TemplateParse : TemplateParseBase
    {

        #region �����ı�

        /// <summary>
        /// ���ɴ���
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


        #region �ϲ�����

        ///// <summary>
        /////     �ϲ���ͨ�ı����ݿ�
        ///// </summary>
        ///// <param name="curElement">Ҫ�ϲ�����</param>
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
        //        /// �Ե�ǰ���ԵĴ��뷽ʽ����ͨ�ı�����ƴ�ӵĴ���
        //        /// </summary>
        //        /// <param name="content">��ͨ�����ı�</param>
        //        /// <returns></returns>
        //        protected override string AppendContentCode(string content)
        //        {
        //            return $@"{CodeSpace}strResult.Append($@""
        //{content}"");";
        //        }

        /// <summary>
        /// ������ͷ�Ĵ����
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
        /// ��鵥�ʴ�ͷ�Ĵ����
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
                    //��ͼ���Һ�����if
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
        ///// ��ϴ���
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
        //        CurWordIndex--; //�հ׻���ȥ
        //    }
        //}



        #endregion

        #region ���ⵥԪ�����

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

                #region �ַ���

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
        ///     �ϲ������ı�
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
                    --CurWordIndex;//����
                    return;
                }
                codeElement.Append(curElement);
            }
        }

        /// <summary>
        ///     �ϲ�����ע��
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
                if (isShift && curElement.Char == '/')//ע�ͽ���
                {
                    return;
                }
                isShift = curElement.Char == '*';
            }
        }
        /// <summary>
        ///     �ϲ�ע��
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
                if (WordElements[CurWordIndex].IsLine) //���ѽ���
                {
                    --CurWordIndex; //����
                    return;
                }
                element.Word += WordElements[CurWordIndex].RealWord;
            }
        }

        #endregion
    }
}
