// 所在工程：AgebullProjectDesigner
// 整理用户：bull2
// 建立时间：2012-10-30 4:13
// 整理时间：2012-10-30 4:16

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;
using Agebull.EntityModel.RobotCoder.CodeTemplate;
using Agebull.EntityModel.RobotCoder.CodeTemplate.LuaTemplate;

namespace Agebull.EntityModel.Designer
{

    /// <summary>
    ///   语言编辑器基类
    /// </summary>
    public class LanguageEditor : RichTextBox
    {

        /// <summary>
        ///   编辑框的文本内容，重写该属性的主要目的是监视到文本框的赋值，以便及时着色
        /// </summary>
        public new string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                if (AutoAnalyze)
                    Analyze();
            }
        }
        public bool IsLua { get; set; }
        private Font _bfont, _lfont, _font, _efont;

        private void Analyze()
        {
            var analyze = new LuaWordSpliter();
            analyze.SplitLuaWords(Text);

            LuaTemplateParse parser = new LuaTemplateParse
            {
                Spliter = analyze
            };
            parser.BuildCode();

            ShowWords(analyze.Words);
        }

        public bool AutoAnalyze { get; set; } = true;

        public void ShowErrorWords(List<WordUnit> words)
        {
            if (_efont == null)
            {
                _efont = new Font(Font, FontStyle.Strikeout);
            }
            var cur = SelectionStart;
            //this.Select(0, Text.Length);
            //this.SelectionFont = _font;
            foreach (var word in words)
            {
                if (!word.IsError)
                {
                    continue;
                }
                Select(word.Start, word.Lenght);
                SelectionFont = _efont;
            }
            Select(cur,0);
        }

        public void ShowWords(List<WordUnit> words)
        {
            if (_efont == null)
            {
                _efont = new Font(Font, FontStyle.Strikeout);
                _bfont = new Font(Font, FontStyle.Bold);
                _lfont = new Font(Font, FontStyle.Underline);
                _font = new Font(Font.Name, Font.Size);
            }
            Select(0, Text.Length);
            SelectionColor = Color.Black;
            SelectionBackColor = Color.White;
            SelectionFont = _font;
            foreach (var word in words)
            {
                if (word.IsEmpty || word.IsReplenish)
                {
                    continue;
                }
                Select(word.Start + word.EmptyChar, word.Lenght - word.EmptyChar * 2);
                if (word.ItemType == CodeItemType.String)
                    SelectionFont = _lfont;
                if (word.UnitType < TemplateUnitType.Code && word.UnitType >= TemplateUnitType.Content ||
                   word.ItemType == CodeItemType.ValueSharp)
                {
                    SelectionBackColor = Color.LightSteelBlue;
                }
                if (word.ItemType == CodeItemType.Space)
                {
                    continue;
                }
                if (word.IsError)
                    SelectionFont = _efont;
                if (word.IsKeyWord)
                {
                    switch (word.ItemFamily)
                    {
                        case CodeItemFamily.Compute:
                        case CodeItemFamily.Logical:
                            SelectionColor = Color.RoyalBlue;
                            break;
                        case CodeItemFamily.Constant:
                            SelectionColor = Color.Green;
                            break;
                        default:
                            if (word.IsKeyWord)
                                SelectionColor = Color.Blue;
                            break;
                    }
                    continue;
                }
                
                switch (word.ItemRace)
                {
                    case CodeItemRace.Variable:
                        SelectionColor = Color.DimGray;
                        break;
                    case CodeItemRace.Assist:
                        switch (word.ItemFamily)
                        {
                            case CodeItemFamily.Rem:
                                SelectionColor = Color.RoyalBlue;
                                break;
                            case CodeItemFamily.Sharp:
                                switch (word.ItemType)
                                {
                                    case CodeItemType.ValueSharp:
                                        SelectionColor = Color.Silver;
                                        break;
                                    default:
                                        SelectionColor = Color.DarkRed;
                                        break;
                                }
                                break;
                            default:
                                SelectionColor = Color.YellowGreen;
                                break;
                        }
                        break;
                    case CodeItemRace.Value:
                        switch (word.ItemType)
                        {
                            case CodeItemType.String:
                                SelectionColor = Color.Red;
                                break;
                            default:
                                SelectionColor = Color.Green;
                                break;
                        }
                        break;
                }
            }
        }
    }
}
