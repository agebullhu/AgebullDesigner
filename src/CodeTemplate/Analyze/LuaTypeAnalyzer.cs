using System.Collections.Generic;
using System.Linq;

namespace Agebull.EntityModel.RobotCoder.CodeTemplate.LuaTemplate
{
    /// <summary>
    ///     LUA类型分析器
    /// </summary>
    public class LuaTypeAnalyzer
    {
        #region 连接类型分析

        /// <summary>
        ///     连接类型分析
        /// </summary>
        /// <param name="parent"></param>
        private void TypeLink(AnalyzeBlock parent)
        {
            foreach (var unit in parent.Elements)
            {
                if (unit.IsEmpty)
                    continue;
                var block = unit as AnalyzeBlock;
                if (block != null)
                    TypeLink(block);
                if (unit.ValueLink != null)
                    unit.ValueType = unit.ValueLink.ValueType;
            }
        }

        #endregion

        #region 设置结果值

        /// <summary>
        ///     设置结果值
        /// </summary>
        /// <param name="parent"></param>
        private void TypeSet(AnalyzeBlock parent)
        {
            for (var index = parent.Elements.Count - 1; index >= 0; index--)
            {
                var unit = parent.Elements[index];
                if (unit.IsEmpty)
                    continue;
                var block = unit as AnalyzeBlock;
                if (block != null)
                    TypeSet(block);
                if (unit.ItemFamily == CodeItemFamily.ValueSentence &&
                    unit.ItemType == CodeItemType.Table_Child)
                {
                    if (!_vers.ContainsKey(unit.Name))
                        _vers.Add(unit.Name, LuaDataTypeItem.Confirm);
                    continue;
                }
                if (unit.ItemType == CodeItemType.String)
                {
                    unit.ValueType = LuaDataTypeItem.StringValue;
                    continue;
                }
                if (unit.ItemType == CodeItemType.Number)
                {
                    unit.ValueType = LuaDataTypeItem.NumberValue;
                    continue;
                }
                if (unit.ItemFamily != CodeItemFamily.Variable)
                    continue;
                var word = unit as WordUnit;
                if (word == null || word.IsPunctuate)
                    continue;
                if (_vers.TryGetValue(word.Name, out LuaDataTypeItem type))
                    word.ValueType = type;
                else
                    _vers.Add(word.Name, LuaDataTypeItem.Error);
            }
        }

        #endregion

        #region 类型推导

        /// <summary>
        ///     类型推导
        /// </summary>
        public static void Analyze(AnalyzeBlock root, TemplateConfig config)
        {
            var analyzer = new LuaTypeAnalyzer
            {
                Root = root,
                Config = config
            };
            analyzer.DoAnalyze();
        }

        /// <summary>
        ///     类型推导
        /// </summary>
        private void DoAnalyze()
        {
            CreateTypeDictionary();
            BlockAnalyze(Root);
            TypeSet(Root);
            TypeLink(Root);
        }

        #endregion

        #region 参数

        /// <summary>
        ///     语法树的根
        /// </summary>
        private AnalyzeBlock Root { get; set; }

        /// <summary>
        ///     当前配置对象
        /// </summary>
        private TemplateConfig Config { get; set; }

        /// <summary>
        ///     类型字典
        /// </summary>
        private Dictionary<string, LuaDataTypeItem> _vers;

        /// <summary>
        ///     初始化类型字典
        /// </summary>
        private void CreateTypeDictionary()
        {
            _vers = new Dictionary<string, LuaDataTypeItem>
            {
                {
                    "string", LuaDataTypeItem.StringValue
                },
                {
                    "number", LuaDataTypeItem.NumberValue
                },
                {
                    "bool", LuaDataTypeItem.BoolValue
                },
                {
                    "nil", LuaDataTypeItem.Nil
                },
                {
                    "model", new LuaDataTypeItem
                    {
                        Name = "model",
                        Type = Config?.ModelType,
                        ItemType = LuaDataType.Table,
                        ValueType = LuaDataType.Table
                    }
                },
                {
                    "__code", LuaDataTypeItem.StringValue
                },
                {
                    "tostring", LuaDataTypeItem.StringFunction
                },
                {
                    "string['format']", LuaDataTypeItem.StringFunction
                },
                {
                    Config?.Name ?? "main", LuaDataTypeItem.StringValue
                }
            };
        }

        #endregion

        #region 类型推导

        /// <summary>
        ///     类型推导
        /// </summary>
        /// <param name="parent"></param>
        private void BlockAnalyze(AnalyzeBlock parent)
        {
            foreach (var block in parent.Elements.OfType<AnalyzeBlock>())
            {
                if (block.IsEmpty || block.ItemRace == CodeItemRace.Assist || block.ItemFamily == CodeItemFamily.Constant)
                    continue;
                BlockAnalyze(block);
                switch (block.ItemFamily)
                {
                    case CodeItemFamily.SetValue:
                        SetValueBlockAnalyze(block);
                        continue;
                    case CodeItemFamily.ValueSentence:
                        switch (block.ItemType)
                        {
                            case CodeItemType.Table_Child:
                                AnalyzeTableChild(block);
                                break;
                            case CodeItemType.Call:
                                AnalyzeCallBlock(block);
                                break;
                        }
                        break;
                }
            }
        }


        #endregion

        #region 分析方法调用块
        /// <summary>
        ///     分析方法调用块(不在子级连接中)
        /// </summary>
        /// <param name="block"></param>
        private void AnalyzeCallBlock(AnalyzeBlock block)
        {
            if (block.Parent.ItemType == CodeItemType.Table_Child)
                return;
            var name = block.Name ?? block.Word;
            if (_vers.TryGetValue(name, out LuaDataTypeItem type))
            {
                block.IsError = false;
                block.ValueType = type;
                return;
            }
            var cn = block.Elements[0].Word;

            if (!_vers.TryGetValue(cn, out type))
            {
                block.IsError = true;
                block.ValueType = LuaDataTypeItem.Error;
                block.Elements[0].ValueType = LuaDataTypeItem.Confirm;
                return;
            }
            block.Elements[0].ValueType = type;
            block.Elements[1].ValueType = type;
            var error = false;
            for (var index = 2; index < block.Elements.Count; index++)
                if (error)
                {
                    block.Elements[index].IsError = true;
                }
                else if (type.ValueType != LuaDataType.Function || !_vers.TryGetValue(type.Name, out type))
                {
                    error = true;
                    block.Elements[index].IsError = true;
                    block.ValueType = LuaDataTypeItem.Error;
                }
                else
                {
                    block.Elements[index].ValueType = type;
                }
            if (error)
            {
                block.IsError = true;
                block.ValueType = LuaDataTypeItem.Error;
                return;
            }
            _vers.Add(name, type);
            block.IsError = false;
            block.ValueType = type;
        }


        #endregion

        #region 表子级分析
        /// <summary>
        ///     表子级分析
        /// </summary>
        /// <param name="block"></param>
        private void AnalyzeTableChild(AnalyzeBlock block)
        {
            var name = block.Name ?? block.Word;
            if (_vers.TryGetValue(name, out LuaDataTypeItem type))
            {
                block.ValueType = type;
                return;
            }
            var error = false;
            var index = 0;
            var item = block.Elements[index++];
            if (item.ItemType == CodeItemType.Call)
            {
                if (!AnalyzeCallBlock((AnalyzeBlock) item, out type))
                    error = true;
            }
            else
            {
                if (_vers.TryGetValue(name, out type))
                {
                    item.ValueType = type;
                }
                else
                {
                    error = true;
                    item.ValueType = type = LuaDataTypeItem.Error;
                }
            }
            for (; index < block.Elements.Count; index++)
            {
                item = block.Elements[index];
                if (error)
                {
                    item.IsError = true;
                    continue;
                }
                if (item.ItemType == CodeItemType.Call)
                {
                    if (!AnalyzeCallBlock((AnalyzeBlock) item, out type))
                    {
                        item.IsError = error = true;
                    }
                    else
                    {
                        item.IsError = false;
                        item.ValueType = type;
                    }
                }
                else
                {
                    if (type.ValueType != LuaDataType.Table)
                    {
                        item.IsError = error = true;
                        continue;
                    }
                    if (_vers.TryGetValue(type.Name, out LuaDataTypeItem chType))
                    {
                        item.IsError = false;
                        item.ValueType = type = chType;
                    }
                    else
                    {
                        error = true;
                        item.IsError = true;
                        item.ValueType = type = LuaDataTypeItem.Error;
                    }
                }
            }
            if (error || type == null)
                type = LuaDataTypeItem.Error;
            _vers.Add(name, type);
            block.ValueType = type;
        }

        /// <summary>
        ///     分析方法调用块(在子级连接中)
        /// </summary>
        /// <param name="block"></param>
        /// <param name="type"></param>
        private bool AnalyzeCallBlock(AnalyzeBlock block, out LuaDataTypeItem type)
        {
            var error = false;
            var i = 0;
            if (_vers.TryGetValue(block.Elements[i].Word, out type))
            {
                block.Elements[0].ValueType = type;
                block.Elements[1].ValueType = type;
                i = 2;
            }
            else
            {
                type = LuaDataTypeItem.Error;
                block.IsError = error = true;
                i = 1;
            }
            for (; i < block.Elements.Count; i++)
            {
                if (error)
                {
                    block.Elements[i].IsError = true;
                    continue;
                }

                if (type.ValueType == LuaDataType.Function && _vers.TryGetValue(type.Name, out LuaDataTypeItem chtype))
                {
                    block.Elements[i].ValueType = type;
                    type = chtype;
                }
                else
                {
                    block.Elements[i].IsError = error = true;
                }
            }
            block.IsError = error;
            block.ValueType = type;
            return !error;
        }
        #endregion

        #region 负值语句分析


        /// <summary>
        ///     设置值语句块的分析
        /// </summary>
        /// <param name="parent"></param>
        private void SetValueBlockAnalyze(AnalyzeBlock parent)
        {
            var vard = parent.Elements[0];
            var varv = parent.Elements.Count < 3 ? null : parent.Elements[2];

            if (vard.Word != ",")
            {
                SetVarDataType(vard as WordUnit, varv);
                return;
            }
            var blocka = vard as AnalyzeBlock;
            if (blocka == null || blocka.Elements.Count == 0)
                return;
            var blockb = varv as AnalyzeBlock;
            if (blockb == null)
            {
                SetVarDataType(blocka.Elements[0] as WordUnit, varv);
                for (var i = 1; i < blocka.Elements.Count; i++)
                    SetVarDataType(blocka.Elements[i] as WordUnit, null);
            }
            else
            {
                for (var i = 1; i < blocka.Elements.Count; i++)
                    SetVarDataType(blocka.Elements[i] as WordUnit,
                        i >= blockb.Elements.Count ? null : blockb.Elements[i]);
            }
        }

        /// <summary>
        ///     类型对等设置
        /// </summary>
        /// <param name="def"></param>
        /// <param name="value"></param>
        private void SetVarDataType(WordUnit def, AnalyzeUnitBase value)
        {
            if (def == null)
                return;
            if (def.Name == null)
                def.Name = def.Word;
            if (!_vers.ContainsKey(def.Name))
                _vers.Add(def.Name, AnalyzeValueBlock(value));
            else if (_vers[def.Name] == LuaDataTypeItem.Confirm)
                _vers[def.Name] = AnalyzeValueBlock(value);
        }

        /// <summary>
        ///     分析值块的数据类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private LuaDataTypeItem AnalyzeValueBlock(AnalyzeUnitBase value)
        {
            if (value == null)
                return LuaDataTypeItem.Confirm;
            var name = value.Name ?? value.Word;
            if (name == null)
                return LuaDataTypeItem.Nil;
            if (value.ValueType != null)
                return value.ValueType;
            if (_vers.ContainsKey(name))
                return _vers[name]; //已有
            var word = value as WordUnit;
            if (word != null)
                return null;

            var block = (AnalyzeBlock)value;
            switch (block.ItemFamily)
            {
                case CodeItemFamily.Compare:
                    return LuaDataTypeItem.BoolValue;
                case CodeItemFamily.Compute:
                    return LuaDataTypeItem.NumberValue;
                case CodeItemFamily.Logical:
                    return AnalyzeValueBlock(block.Elements[0]);
            }
            switch (block.ItemType)
            {
                case CodeItemType.Number:
                    return LuaDataTypeItem.NumberValue;
                case CodeItemType.String:
                    return LuaDataTypeItem.StringValue;
                case CodeItemType.Separator_StringJoin:
                    return LuaDataTypeItem.StringValue;
                case CodeItemType.StringLen:
                    return LuaDataTypeItem.NumberValue;
                case CodeItemType.LogicalSentence:
                    return AnalyzeValueBlock(block.Elements[0]);
                case CodeItemType.Brackets41:
                    return LuaDataTypeItem.TableDefinition;
                case CodeItemType.Call:
                    if (block.Elements[0].ItemType != CodeItemType.Brackets21)
                    {
                        if (_vers.ContainsKey(block.Elements[0].Word))
                            return _vers[block.Elements[0].Word]; //已有
                        block.Elements[0].ValueType = LuaDataTypeItem.FunctionConfirm;
                        return new LuaDataTypeItem
                        {
                            Name = block.Elements[0].Word,
                            ItemType = LuaDataType.Confirm,
                            ValueType = LuaDataType.Function
                        };
                    }
                    return null;
                case CodeItemType.Table_Child:
                    return new LuaDataTypeItem
                    {
                        Name = block.Elements[0].Word,
                        ItemType = LuaDataType.Confirm,
                        ValueType = LuaDataType.Error
                    };
            }
            return LuaDataTypeItem.Error; //无法处理的
        }

        #endregion
    }
}