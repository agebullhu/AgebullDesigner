// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// ����:2014-11-10
// �޸�:2014-11-10
// *****************************************************/

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
    public sealed class LuaTemplateParse
    {

        #region �����ı�

        public LuaWordSpliter Spliter { get; set; } = new LuaWordSpliter();


        public AnalyzeBlock Root => Spliter.Root;

        public TemplateConfig Config
        {
            get { return Spliter.Config; }
            set { Spliter.Config = value; }
        }

        public string Code => Config.Code;

        /// <summary>
        ///     �����ı�
        /// </summary>
        public void Compile()
        {
            Spliter.SplitTemplateWords(Config.Template);
            BuildCode();
        }

        /// <summary>
        ///     �����ı�
        /// </summary>
        public void Compile(string code)
        {
            Spliter.SplitTemplateWords(code);
            BuildCode();
        }

        #endregion

        #region ��������

        /// <summary>
        /// ���ɴ���
        /// </summary>
        /// <returns></returns>
        public void BuildCode()
        {
            var words = AllToCode();
            //LevelCode(words);

            StringBuilder code = new StringBuilder();
            code.Append($"function {Config.Name}()");
            foreach (var element in words)
            {
                switch (element.ItemType)
                {
                    case CodeItemType.String:
                        break;
                }
                code.Append(element.Word);
            }
            code.Append("end");
            Config.Code = code.ToString();
        }

        /// <summary>
        /// ����һ��������еĽڵ�
        /// </summary>
        /// <returns></returns>
        private WordUnit SpaceWord { get; } = new WordUnit(' ')
        {
            UnitType = TemplateUnitType.Code
        };

        private List<WordUnit> AllToCode()
        {
            List<WordUnit> elements = new List<WordUnit>();
            MergeElements(Root, elements, 1);
            return elements;
        }

        private void Append(List<WordUnit> words, WordUnit word)
        {
            words.Add(word);
        }

        private void AppendSpace(List<WordUnit> words)
        {
            if (words.Count > 0 && words[words.Count - 1].ItemFamily != CodeItemFamily.Space)
                words.Add(SpaceWord);
        }

        private void AppendLine(List<WordUnit> words, int level)
        {
            WordUnit word;
            if (words.Count == 0)
            {
                words.Add(word = new WordUnit());
            }
            else
            {
                word = new WordUnit('\r')
                {
                    UnitType = TemplateUnitType.Code
                };
                word.Append(-1, '\n');
                if (words[words.Count - 1].ItemFamily != CodeItemFamily.Space)
                    words.Add(word);
                else
                    words[words.Count - 1] = word;
            }
            for (int i = 0; i < level * 4; i++)
                word.Append(-1, ' ');
        }

        private void MergeElements(AnalyzeBlock parent, List<WordUnit> elements, int level)
        {
            bool itemInLine = false;
            switch (parent.ItemFamily)
            {
                case CodeItemFamily.TableDefault:
                    itemInLine = parent.Elements.Count > 2;
                    break;
                case CodeItemFamily.Range:
                    AppendLine(elements, level);
                    break;
                case CodeItemFamily.Block:
                    itemInLine = true;
                    ++level;
                    break;
            }
            foreach (var element in parent.Elements)
            {
                if (element.IsReplenish || element.IsEmpty)
                    continue;
                switch (element.UnitType)
                {
                    case TemplateUnitType.Code:
                        if (itemInLine)
                            AppendLine(elements, level);
                        var unit = element as WordUnit;
                        if (unit != null)
                        {
                            switch (unit.ItemType)
                            {
                                case CodeItemType.Brackets41:
                                    if (itemInLine)
                                        AppendLine(elements, level++);
                                    break;
                                case CodeItemType.Brackets42:
                                    if (itemInLine)
                                        AppendLine(elements, --level);
                                    break;
                                case CodeItemType.Key_Until:
                                case CodeItemType.Key_End:
                                case CodeItemType.Key_Return:
                                case CodeItemType.Key_Local:
                                    AppendLine(elements, level);
                                    break;
                            }
                            if (elements.Count > 0 && unit.ItemType == CodeItemType.Separator_Dot &&
                                elements[elements.Count - 1].IsSpace)
                                elements[elements.Count - 1] = unit;
                            else
                                elements.Add(unit);
                            if (itemInLine)
                                AppendLine(elements, level);
                            else if (unit.ItemType != CodeItemType.Separator_Dot)
                                AppendSpace(elements);
                        }
                        else
                        {
                            MergeElements(element as AnalyzeBlock, elements, level);
                            if (itemInLine)
                                AppendLine(elements, level);
                        }
                        break;
                    case TemplateUnitType.Value:
                    case TemplateUnitType.SimpleValue:
                        AppendLine(elements, level);
                        Append(elements, new WordUnit
                        {
                            Word = $"appendCode({element.ToCode()})"
                        });
                        AppendLine(elements, level);
                        break;
                    case TemplateUnitType.Content:
                        if (element.ItemType == CodeItemType.Content)
                        {
                            StringBuilder builder = new StringBuilder();
                            element.ToContent(builder);
                            var content = builder.ToString();
                            if (string.IsNullOrEmpty(content))
                                break;
                            AppendLine(elements, level);
                            Append(elements, new WordUnit
                            {
                                Word = $"appendCode('{content}')"
                            });
                            AppendLine(elements, level);
                        }
                        break;
                }
            }
            switch (parent.ItemFamily)
            {
                case CodeItemFamily.Range:
                    AppendLine(elements, level);
                    break;
                case CodeItemFamily.Block:
                    AppendLine(elements, --level);
                    break;
            }
        }

        #endregion

    }
}
