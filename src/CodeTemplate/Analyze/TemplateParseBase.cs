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
    ///     ����ģ��ת��������
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
        #region ���̶���

        public TemplateConfig Config { get; set; }


        /// <summary>
        ///     ����ǰ�ĵ��ʽڵ�
        /// </summary>
        public List<WordElement> WordElements;

        /// <summary>
        ///     ��ǰ���ʵ�λ��
        /// </summary>
        protected int CurWordIndex;
        /// <summary>
        ///     �������ģ��ڵ�
        /// </summary>
        public readonly List<AnalyzeElement> TemplateElements = new List<AnalyzeElement>();

        #endregion

        #region ���ݽڵ�

        /// <summary>
        /// ��ǰ���ݽڵ�
        /// </summary>
        protected AnalyzeElement ContentElement { get; private set; }

        /// <summary>
        /// ���õ�ǰ���ݽڵ�
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

        #region ���ݴ���

        /// <summary>
        ///     �����ı�
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
        ///     �ϲ�����,ʹ֮��Ϊ��ʽ���õĴ��뵥Ԫ
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

        #region �������

        /// <summary>
        ///     ���ɴ���
        /// </summary>
        /// <returns></returns>
        protected abstract string BuildCode();

        #endregion

        #region Ƕ����봦��

        /// <summary>
        ///     ���Ҵ���Ƕ��
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
        ///     ������ͷ�Ĵ����
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckPunctuateStartCode()
        {
            var codeElement = new AnalyzeElement();
            switch (WordElements[CurWordIndex].Char)
            {
                case '@':
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    return false; //����@Ϊһ��@
                default:
                    CurWordIndex--; //����������,��ʱ@Ϊ��ͨ�ַ�
                    return false;
                case '*': //@*...*@Ϊע��,����
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    SkipRazorRem();
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    return true;
                case '#': //@#...#@Ϊ�ĵ�����
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    ReadFileRem();
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    return true;
                case '-': //����@-��ʾ���д���
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.None);
                    if (!IsWithLineStart())
                    {
                        CurWordIndex--; //����������,��ʱ@Ϊ��ͨ�ַ�
                        return false;
                    }
                    if (IsWithLineEnd())
                        return true; //���������
                    MergeToLineEnd(codeElement);
                    codeElement.SetRace(CodeItemRace.Assist, CodeItemFamily.Rem);
                    break;
                case '(': //@()С������Ա�����
                    WordElements[CurWordIndex - 1].SetRace(CodeItemRace.Assist, CodeItemFamily.Sharp);
                    WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
                    FindCouple(codeElement, '(', ')', false);
                    codeElement.IsBlock = true;
                    foreach (var item in codeElement.Elements)
                        item.Words.ForEach(p => p.IsBlock = true);
                    if (CurWordIndex < WordElements.Count)
                        WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
                    break;
                case '{': //@{}���д����
                    if (!IsWithLineStart() || !IsWithLineEnd())
                    {
                        CurWordIndex--; //����������,��ʱ@Ϊ��ͨ�ַ�,{����һ�α���ͨ����
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
        ///     ��鵥�ʴ�ͷ�Ĵ����
        /// </summary>
        /// <returns></returns>
        protected abstract bool CheckWordStartCode();

        /// <summary>
        ///     ��ϴ���
        /// </summary>
        /// <param name="codeElement">�����</param>
        protected virtual void JoinCode(AnalyzeElement codeElement)
        {
            codeElement.ItemRace = CodeItemRace.Sentence;
            TemplateElements.Add(codeElement);
            ResetContentElement();
        }

        #endregion

        #region �ַ�����

        /// <summary>
        /// ����һ��������еĽڵ�
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
        ///     �Ƿ�����ͷ
        /// </summary>
        /// <param name="skip">����������Ԫ</param>
        /// <returns></returns>
        protected bool IsWithLineStart(int skip = 1)
        {
            return CurWordIndex == skip ||
                   WordElements[CurWordIndex - skip - 1].IsLine;
        }

        /// <summary>
        ///     �Ƿ�����β
        /// </summary>
        /// <param name="skip">����������Ԫ</param>
        /// <returns></returns>
        protected bool IsWithLineEnd(int skip = 0)
        {
            return CurWordIndex + skip + 1 == WordElements.Count ||
                   WordElements[CurWordIndex + skip + 1].IsLine;
        }

        /// <summary>
        ///     ǰ������һ���ַ�
        /// </summary>
        /// <param name="codeElement">��ǰ�ϲ�����</param>
        /// <param name="word">����ַ�����������(��Ӱ��ϲ�,��Ӱ�췵��ֵ)</param>
        /// <param name="canSpace">�Ƿ�����հ׷����</param>
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
        ///     ��һ���ַ��Ƿ�Ϊ�����ַ�,�����,�ϲ�֮
        /// </summary>
        /// <param name="codeElement">��ǰ�ϲ�����</param>
        /// <param name="word">����ַ�����������</param>
        /// <param name="canSpace">�Ƿ�����հ׷����</param>
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
        ///     ֱ�������ַ����ֽ���
        /// </summary>
        /// <param name="codeElement">��ǰ�ϲ�����</param>
        /// <param name="word">����ַ�����������</param>
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
        ///     ��һ���ַ��Ƿ������������(���ı䵱ǰ˳��)
        /// </summary>
        /// <param name="word">�������ֵ��ַ�</param>
        /// <param name="canSpace">�Ƿ�����հ׷����</param>
        /// <returns></returns>
        protected bool NextWithCondition(string word, bool canSpace)
        {
            return NextWithCondition(p => p.Word == word, canSpace);
        }
        /// <summary>
        ///     ��һ���ַ��Ƿ������������(���ı䵱ǰ˳��)
        /// </summary>
        /// <param name="condition">�Ƿ���������</param>
        /// <param name="canSpace">�Ƿ�����հ׷����</param>
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
        ///     ��һ���ַ��Ƿ������������
        /// </summary>
        /// <param name="codeElement">��ǰ�ϲ�����</param>
        /// <param name="condition">�Ƿ���������</param>
        /// <param name="canSpace">�Ƿ�����հ׷����</param>
        /// <returns></returns>
        protected bool TryGoNextWithCondition(AnalyzeElement codeElement, Func<WordElement, bool> condition, bool canSpace)
        {
            return TryGoNextWithCondition(codeElement, condition, null, canSpace);
        }

        /// <summary>
        ///     ��һ���ַ��Ƿ������������
        /// </summary>
        /// <param name="codeElement">��ǰ�ϲ�����</param>
        /// <param name="condition">�Ƿ���������</param>
        /// <param name="succeedAction">�����������,����ڵ�ǰ�Ĳ���</param>
        /// <param name="canSpace">�Ƿ�����հ׷����</param>
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
        ///     ����ǰ��������
        /// </summary>
        /// <param name="codeElement">���Խڵ�</param>
        /// <param name="preChar"></param>
        public abstract void MergeLanWord(AnalyzeElement codeElement, ref char preChar);

        /// <summary>
        ///     ���ҳɶԵķ���
        /// </summary>
        /// <param name="codeElement">���Խڵ�</param>
        /// <param name="start">��ʼ�ַ�</param>
        /// <param name="end">����</param>
        /// <param name="includeStartEnd">����Ƿ����ǰ���ַ�</param>
        /// <param name="skipCur">�Ƿ�����������ǰ�ַ�</param>
        /// <returns></returns>
        protected void FindCouple(AnalyzeElement codeElement, char start, char end, bool includeStartEnd, bool skipCur = false)
        {

            WordElements[CurWordIndex].SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
            if (skipCur)
                CurWordIndex++;
            //ȷ�Ͽ�ʼ�ַ��Ƿ�ǰ�ַ�,�������,��ǰ��Ϊ�հ�����һ��������ǰ���ַ�
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
                if (block == 0) //�ɶԽ���
                {
                    curElement.SetRace(CodeItemRace.Assist, CodeItemFamily.Range);
                    if (includeStartEnd)
                        codeElement.Append(curElement);
                    return;
                }
                //��㴦��,�Է�ֹ�����������Ⱦ(ע��,�ַ���,�ַ�)
                MergeLanWord(codeElement, ref preChar);
            }
        }

        /// <summary>
        ///     �������Ƕ�����
        /// </summary>
        /// <param name="codeElement"></param>
        /// <remarks>
        /// ���������@,��������Ǵ���,ֱ��һ������ֻ������@�ַ�����
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
            foreach (var item in waiting)//����ȥ
                codeElement.Append(item);
        }

        /// <summary>
        ///     ���ҵ�������
        /// </summary>
        /// <param name="codeElement">�����</param>
        /// <param name="joinChar">���������ַ�</param>
        /// <returns></returns>
        protected void FindVariable(AnalyzeElement codeElement, params char[] joinChar)
        {
            codeElement.ItemRace = CodeItemRace.Variable;
            codeElement.Append(WordElements[CurWordIndex]);
            if (CurWordIndex + 1 == WordElements.Count)
                return;
            var word = WordElements[++CurWordIndex];
            //��һ���ַ��󲻸��������,������ƴ��
            if (!word.IsPunctuate || !joinChar.Any(p => p == word.Char))
            {
                //��ǰ�����Ϊ��ͨ����,�������˴���
                --CurWordIndex;
                return;
            }
            for (; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                word = WordElements[CurWordIndex];
                if (word.IsPunctuate)
                {
                    //��ǰΪ�ָ����Һ�һ��Ϊ�ַ�
                    if (!joinChar.Any(p => p == word.Char) || !NextWithCondition(p => p.IsWord, false))
                    {
                        //��ǰ�����Ϊ��ͨ����,�������˴���
                        --CurWordIndex;
                        return;
                    }
                    codeElement.Append(word);
                }
                else
                {
                    codeElement.Append(word);
                    //��ǰΪ�ַ��Һ�һ��Ϊ�ָ���
                    if (!NextWithCondition(p => joinChar.Any(ch => ch == p.Char), false))
                        return;
                }
            }
        }

        #endregion

        #region ���Է�����

        /// <summary>
        ///     ����Ƿ�Ϊ�����������,��ͬ���Բ�ͬ,C#��@},LUA��@end
        /// </summary>
        /// <param name="codeElement">�����</param>
        /// <returns></returns>
        protected bool CheckIsCodeBlockEnd(AnalyzeElement codeElement)
        {
            if (IsWithLineStart() && IsWithLineEnd())
            {
                ContentElement.Elements.Remove(WordElements[CurWordIndex - 2]);//����
                ContentElement.Elements.Remove(WordElements[CurWordIndex - 1]);//����
                codeElement.ItemRace = CodeItemRace.Range;
                codeElement.Append(WordElements[CurWordIndex]);
                JoinCode(codeElement);
                return true;
            }
            CurWordIndex--;
            return false;
        }

        /// <summary>
        ///     �ϲ��ַ�
        /// </summary>
        /// <param name="codeElement">�����</param>
        /// <param name="sign">�ַ�ʹ���ĸ�����(һ�㶼��"��')</param>
        protected void MergeString(WordElement codeElement, char sign)
        {
            var preShift = false;
            for (CurWordIndex++; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                var curElement = WordElements[CurWordIndex];
                codeElement.Append(curElement);
                if (preShift)
                {
                    preShift = false; //ǰ��Ϊת���ַ�������
                    continue;
                }
                if (curElement.Char == '\\')
                {
                    preShift = true;
                    continue;
                }
                if (curElement.Char == sign)
                    break; //�ɶԳ���,�����ַ���ʶ��
            }
            codeElement.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.String);
        }

        /// <summary>
        ///     �ϲ�����β�����Ķ���
        /// </summary>
        /// <param name="codeElement">�����</param>
        protected void MergeToLineEnd(AnalyzeElement codeElement)
        {
            for (CurWordIndex++; CurWordIndex < WordElements.Count; CurWordIndex++)
            {
                var curElement = WordElements[CurWordIndex];
                if (curElement.IsLine) //���ѽ���
                {
                    --CurWordIndex; //����
                    return;
                }
                codeElement.Append(curElement);
            }
        }

        /// <summary>
        ///     ����Razorע��@*...*@
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

        #region ��ע


        /// <summary>
        ///     ��ȡ�ĵ���ע@#...#@
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
                    case 0://ȡ������,���ڳ��ֵ�һ��ðʱʱ����ڶ���
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
                    case 1://���ֵ�������ַ�ʽ����
                        if (word.IsLine)//��˵��,����
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
                                step = 3;//�����ŵ�
                                bc = word.Char;
                                prechar = '\0';
                                break;
                            default:
                                value.Append(word.Word);
                                step = 2;//û���ŵ�
                                break;
                        }
                        break;
                    case 2://û���ŵ�,���н���
                        if (word.IsLine)
                        {
                            SetConfig(name, value.ToString());
                            step = 0;
                            name = null;
                            break;
                        }
                        value.Append(word.Word);
                        break;
                    case 3://�����ŵ�,�����Ž���,ʹ��ת��
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
                case "����":
                    Config.Name = value.FromLuaChar();
                    break;
                case "caption":
                case "����":
                    Config.Caption = value.FromLuaChar();
                    break;
                case "desc":
                case "description":
                case "˵��":
                case "��ע":
                case "ģ��˵��":
                    Config.Description = value.FromLuaChar();
                    break;
                case "classify":
                case "���":
                case "����":
                    Config.Classify = value.FromLuaChar();
                    break;
                case "condition":
                case "ִ������":
                    Config.Condition = value.FromLuaChar();
                    break;
                case "language":
                case "����":
                case "Ŀ������":
                    Config.Language = value.FromLuaChar();
                    break;
                case "savesath":
                case "codesavesath":
                case "���뱣��·��":
                case "���뱣��":
                case "����·��":
                    Config.CodeSavePath = value.FromLuaChar();
                    break;
                case "type":
                case "model":
                case "modeltype":
                case "����":
                case "��������":
                case "����ģ��":
                case "ģ��":
                case "ģ������":
                    var type = value.FromLuaChar();
                    switch (type.ToLower())
                    {
                        case "�ֶ�":
                        case "field":
                        case "property":
                        case "propertyconfig":
                            Config.ModelType = typeof(PropertyConfig).Name;
                            break;
                        case "ʵ��":
                        case "entity":
                        case "entityconfig":
                            Config.ModelType = typeof(EntityConfig).Name;
                            break;
                        case "ö��":
                        case "enum":
                        case "enumconfig":
                            Config.ModelType = typeof(EnumConfig).Name;
                            break;
                        case "����":
                        case "���Ͷ���":
                        case "type":
                        case "typedef":
                        case "typedefitem":
                        case "typeconfig":
                        case "typedefconfig":
                            Config.ModelType = typeof(TypedefItem).Name;
                            break;
                        case "����":
                        case "�û�����":
                        case "��ť":
                        case "btn":
                        case "button":
                        case "cmd":
                        case "command":
                            Config.ModelType = typeof(UserCommandConfig).Name;
                            break;
                        case "��Ŀ":
                        case "project":
                        case "projectconfig":
                            Config.ModelType = typeof(ProjectConfig).Name;
                            break;
                        case "�������":
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