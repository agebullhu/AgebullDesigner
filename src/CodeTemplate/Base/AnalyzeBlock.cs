using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     分析时使用的单元
    /// </summary>
    public sealed class AnalyzeBlock : AnalyzeUnitBase
    {
        #region 定义

        /// <summary>
        /// 主节点
        /// </summary>
        private WordUnit _primary;

        /// <summary>
        /// 主节点
        /// </summary>
        public WordUnit Primary
        {
            get { return _primary; }
            set
            {
                _primary = value;
                OnPrimarySeted();
            }
        }

        /// <summary>
        /// 所有节点
        /// </summary>
        private ObservableCollection<AnalyzeUnitBase> _elements = new ObservableCollection<AnalyzeUnitBase>();

        /// <summary>
        /// 所有节点
        /// </summary>
        public ObservableCollection<AnalyzeUnitBase> Elements => _elements;

        /// <summary>
        /// 重置并返回重置前的节点
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<AnalyzeUnitBase> ResetElements()
        {
            var re = _elements;
            _elements = new ObservableCollection<AnalyzeUnitBase>();
            return re;
        }

        #endregion

        #region 节点添加

        public Action<AnalyzeBlock, AnalyzeUnitBase> OnElementAppend;
        /// <summary>
        ///     合并单元
        /// </summary>
        /// <param name="unit">分析单元</param>
        public void Append(AnalyzeUnitBase unit)
        {
            if (unit == null || Elements.Contains(unit))
                return;
            if ((Start < 0 || Start > unit.Start) && unit.Start >= 0)
                Start = unit.Start;
            if ((End < 0 || End < unit.End) && unit.End >= 0)
                End = unit.End;
            Elements.Add(unit);
            unit.Parent = this;
            OnElementAppend?.Invoke(this, unit);
        }

        /// <summary>
        ///     合并单元
        /// </summary>
        /// <param name="unit">分析单元</param>
        public void Append(WordUnit unit)
        {
            if (unit == null || Elements.Contains(unit))
                return;
            if ((Start < 0 || Start > unit.Start) && unit.Start >= 0)
                Start = unit.Start;
            if ((End < 0 || End < unit.End) && unit.End >= 0)
                End = unit.End;
            Elements.Add(unit);
            unit.Parent = this;
            OnElementAppend?.Invoke(this, unit);
            //if (Primary == null || unit.PrimaryLevel < Primary.PrimaryLevel)
            //    Primary = unit;
        }

        #endregion

        #region 主节点

        /// <summary>
        /// 主节点设置的处理
        /// </summary>
        private void OnPrimarySeted()
        {
            if (Primary == null)
                return;
            Word = Primary.Word;
            JoinLevel = Primary.JoinLevel;
            JoinFeature = Primary.JoinFeature;
            SetRace(Primary.ItemRace, Primary.ItemFamily, Primary.ItemType);
            if (!Primary.IsKeyWord)
                return;
            switch (Primary.ItemType)
            {
                //case CodeItemType.Brackets31:
                case CodeItemType.Separator_Dot:
                case CodeItemType.Separator_Colon:
                    IsUnit = true;
                    ItemFamily = CodeItemFamily.ValueSentence;
                    ItemType = CodeItemType.Table_Child;
                    return;
                case CodeItemType.Separator_Equal:
                    SetRace(CodeItemRace.Sentence, CodeItemFamily.SetValue, CodeItemType.SetValueSentence);
                    return;
                case CodeItemType.Key_In:
                    SetRace(CodeItemRace.Sentence, CodeItemFamily.SetValue, CodeItemType.InValueSentence);
                    return;
                case CodeItemType.Key_Not:
                    IsUnit = true;
                    ValueType = LuaDataTypeItem.BoolValue;
                    SetRace(CodeItemRace.Sentence, CodeItemFamily.ValueSentence, CodeItemType.LogicalSentence);
                    return;
                //case CodeItemType.Brackets21:
                //    ItemType = CodeItemType.Call;
                //    return;
                case CodeItemType.Brackets41:
                    ValueType = LuaDataTypeItem.TableDefinition;
                    SetRace(CodeItemRace.Variable, CodeItemFamily.TableDefault, CodeItemType.Table);
                    return;
                case CodeItemType.Key_Function:
                    ItemType = CodeItemType.Function;
                    return;
            }
            switch (Primary.ItemFamily)
            {
                case CodeItemFamily.Compute:
                    ValueType = LuaDataTypeItem.NumberValue;
                    SetRace(CodeItemRace.Sentence, CodeItemFamily.ValueSentence, CodeItemType.ComputeSentence);
                    return;
                case CodeItemFamily.Compare:
                    ValueType = LuaDataTypeItem.BoolValue;
                    SetRace(CodeItemRace.Sentence, CodeItemFamily.ValueSentence, CodeItemType.CompareSentence);
                    return;
                case CodeItemFamily.Logical:
                    SetRace(CodeItemRace.Sentence, CodeItemFamily.ValueSentence, CodeItemType.LogicalSentence);
                    return;
            }
        }

        #endregion

        #region 杂项
        /// <summary>
        /// 需要下一个来合并
        /// </summary>
        public bool NeedNext { get; set; }
        /// <summary>
        ///     是否为空
        /// </summary>
        public override bool IsEmpty => Elements.Count == 0;

        /// <summary>
        /// 到文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name ?? (Elements.Count == 0 ? "" : string.Concat(Elements));
        }
        /// <summary>
        /// 到文本
        /// </summary>
        /// <returns></returns>
        public override void ToContent(StringBuilder builder)
        {
            foreach (var child in  Elements)
            {
                child.ToContent(builder);
            }
        }

        /// <summary>
        /// 到文本
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            return Elements.Count == 0 ? " " : Elements.Select(p => p.ToCode()).LinkToString(' ');
        }
        #endregion

        #region 构造终结

        /// <summary>
        /// 构造终结
        /// </summary>
        public void Release()
        {
            if (Elements.Count == 0)
            {
                IsError = true;
                return;
            }
            if (Elements.Any(p => p.Start >= 0))
                Start = Elements.Where(p => p.Start >= 0).Min(p => p.Start);
            End = Elements.Max(p => p.End);
            if (Primary == null)
            {
                return;
            }

            if (Primary.ItemType == CodeItemType.String)
            {
                ReleaseString();
                return;
            }
            switch (ItemType)
            {
                case CodeItemType.Call:
                    ReleaseCall();
                    return;
                case CodeItemType.Table_Child:
                    ReleaseTableChild();
                    return;
            }
            switch (Primary.ItemType)
            {
                case CodeItemType.Key_Function:
                    ReleaseFunction();
                    return;
                case CodeItemType.Brackets31:
                    Name = Elements[1].ItemType == CodeItemType.String
                        ? $"['{Elements[1].Word}']"
                        : $"[{Elements[1].Word}]";
                    return;
            }
        }

        /// <summary>
        /// 文本的构造终结
        /// </summary>
        private void ReleaseString()
        {
            ValueType = LuaDataTypeItem.StringValue;
            if (Elements.Count <= 0)
            {
                Word = "";
                Name = "EmptyString";
                return;
            }
            //this.Name = "string";
            var sb = new StringBuilder();
            var words = Elements.OfType<WordUnit>().ToArray();
            if (words[0].Char == '[')
            {
                for (int i = 2; i < Elements.Count - 2; i++)
                {
                    sb.Append(words[i].Word);
                }
            }
            else
            {
                for (int i = 1; i < Elements.Count - 1; i++)
                {
                    sb.Append(words[i].Word);
                }
            }
            Word = sb.ToString();
        }

        /// <summary>
        /// 函数块的构造终结
        /// </summary>
        private void ReleaseFunction()
        {
            if (Elements.Count > 1 && Elements[1].ItemType == CodeItemType.Call &&
                ((AnalyzeBlock)Elements[1]).Primary != null)
            {
                _primary = ((AnalyzeBlock)Elements[1]).Primary;
                _primary?.SetRace(CodeItemRace.Variable, CodeItemFamily.Variable, CodeItemType.Function);
                ((AnalyzeBlock)Elements[1]).SetRace(CodeItemRace.Variable, CodeItemFamily.Variable,
                    CodeItemType.Function);

                Name = Elements[1].Name = _primary?.Name;
            }
        }

        /// <summary>
        /// 调用块的构造终结
        /// </summary>
        private void ReleaseCall()
        {
            SetRace(CodeItemRace.Variable, CodeItemFamily.ValueSentence, CodeItemType.Call);
            var unit = Elements[0] as WordUnit;
            var isChild = Parent != null && !Parent.IsEmpty && Parent.Elements[0] != this &&
                          Parent.ItemType == CodeItemType.Table_Child;

            if (unit != null && !unit.IsKeyWord && !unit.IsPunctuate)
            {
                _primary = unit;
            }
            if (isChild)
            {
                Name = $"['{Elements[0].Word}']";
            }
            else
            {
                Name = Elements[0].Name ?? Elements[0].Word;
            }
            Primary.Name = Name;
            if (Elements.Count > 1)
            {
                Elements[1].Name = "_call_";
                for (var i = 2; i < Elements.Count; i++)
                {
                    Elements[i].Name = "_call_";
                }
                if (Elements.Count > 2)
                    Name += Elements.Count - 2;
            }
        }

        /// <summary>
        /// 表子级的构造终结
        /// </summary>
        private void ReleaseTableChild()
        {
            if (CodeItemFamily.ValueSentence != ItemFamily)
                return;
            SetRace(CodeItemRace.Value, CodeItemFamily.ValueSentence, CodeItemType.Table_Child);
            var sb = new StringBuilder();
            sb.Append(Elements[0].Word);
            for (int i = 1; i < Elements.Count; i++)
            {
                var cur = Elements[i];
                if (cur.ItemFamily == CodeItemFamily.Separator || cur.ItemType == CodeItemType.Brackets32)
                    continue;
                if (cur.ItemType != CodeItemType.Table_Child && cur.ItemType != CodeItemType.Call)
                {
                    if (cur.ItemType == CodeItemType.Brackets31)
                    {
                        cur.SetRace(CodeItemRace.Variable, CodeItemFamily.Variable, CodeItemType.Table_Child);
                    }
                    else if (cur.ItemType == CodeItemType.Brackets21)
                    {
                        cur.SetRace(CodeItemRace.Variable, CodeItemFamily.Variable, CodeItemType.Call);
                        cur.Name = "_call_";
                    }
                    else
                    {
                        cur.Name = $"['{cur.Word}']";
                        cur.SetRace(CodeItemRace.Variable, CodeItemFamily.Variable, CodeItemType.Table_Child);
                    }
                }
                sb.Append(cur.Name);
            }
            Name = sb.ToString();
        }

        #endregion

    }
}