using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder.CodeTemplate.LuaTemplate
{
    /// <summary>
    ///     LUA类型分析器
    /// </summary>
    public class LuaWordTypeAnalyzer
    {

        #region 运行参数

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
        /// 加入到全局字典
        /// </summary>
        /// <param name="word"></param>
        private void AddVariableToDictionary(AnalyzeUnitBase word)
        {
            if (word == null)
                return;
            if (word.ItemType == CodeItemType.Variable_Global || word.ItemType == CodeItemType.Variable_Local)
            {
                AddToDictionary(word);
            }
        }

        /// <summary>
        /// 加入到全局字典
        /// </summary>
        /// <param name="unit"></param>
        private void AddToDictionary(AnalyzeUnitBase unit)
        {
            if (unit == null || unit.ItemType == CodeItemType.Separator_Comma)
                return;
            var name = unit.Name ?? unit.Word;
            if (unit is AnalyzeBlock)//BUG
            {
                switch (unit.ItemType)
                {
                    case CodeItemType.Separator_Dot:
                    case CodeItemType.Separator_Colon:
                        break;
                    default:
                        return;//其它是不适合的情况
                }
            }
            if (!_vers.ContainsKey(name))
            {
                if (unit.ValueType == null)
                    unit.ValueType = LuaDataTypeItem.Nil;
                _vers.Add(name, unit.ValueType);
                return;
            }
            var type = _vers[name];
            if (type == LuaDataTypeItem.Nil || type == LuaDataTypeItem.Confirm)
                _vers[name] = unit.ValueType;
            else if (unit.ValueType == null || unit.ValueType == LuaDataTypeItem.Nil)
                unit.ValueType = type;
        }

        #endregion
        #region 类型推导

        /// <summary>
        ///     类型推导
        /// </summary>
        public static void Analyze(AnalyzeBlock root, TemplateConfig config)
        {
            var analyzer = new LuaWordTypeAnalyzer
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
            DoAnalyze(Root);
            TypeSet(Root);
            TypeLink(Root);
        }


        /// <summary>
        ///     类型推导
        /// </summary>
        /// <param name="parent"></param>
        private void DoAnalyze(AnalyzeBlock parent)
        {
            _tableIndex = 1;
            foreach (var block in parent.Elements.OfType<AnalyzeBlock>())
            {
                block.Parent = parent;
                if (block.ItemRace <= CodeItemRace.Assist)
                    continue;
                DoAnalyze(block);

                switch (block.ItemType)
                {
                    case CodeItemType.Key_Not:
                        block.ValueType = LuaDataTypeItem.BoolValue;
                        continue;
                    case CodeItemType.Separator_StringJoin:
                        block.ValueType = LuaDataTypeItem.StringValue;
                        continue;
                    case CodeItemType.StringLen:
                        block.ValueType = LuaDataTypeItem.NumberValue;
                        continue;
                    case CodeItemType.Print:
                        block.ValueType = LuaDataTypeItem.VoidFunction;
                        continue;
                    case CodeItemType.Table_Child:
                        AnalyzeTableChild(block);
                        break;
                    case CodeItemType.Separator_Equal:
                        SetValueBlockAnalyze(block);
                        continue;
                    case CodeItemType.Separator_Comma:
                        CheckCommaDataType(block);
                        continue;
                    case CodeItemType.Separator_Dot:
                    case CodeItemType.Separator_Colon:
                        AnalyzeTableChildValue(block);
                        continue;
                    case CodeItemType.Function:
                        CheckFunctionDataType(block);
                        continue;
                    case CodeItemType.Call:
                        AnalyzeCallBlock(block);
                        continue;
                    case CodeItemType.Table:
                        AnalyzeTableBlock(block);
                        continue;
                    case CodeItemType.Key_In:
                        CheckInType(block);
                        continue;
                }
                switch (block.ItemFamily)
                {
                    case CodeItemFamily.Compute:
                        block.ValueType = LuaDataTypeItem.NumberValue;
                        ComputeBlockAnalyze(block);
                        continue;
                    case CodeItemFamily.Compare:
                        block.ValueType = LuaDataTypeItem.BoolValue;
                        continue;
                    case CodeItemFamily.Logical:
                        block.ValueType = block.Elements[0].ValueType;
                        continue;
                }
            }
        }
        #endregion

        #region 类型推导

        /// <summary>
        /// In关键字的类型确定
        /// </summary>
        /// <param name="block"></param>
        private void CheckInType(AnalyzeBlock block)
        {
            if (block.Elements.Count > 2)
            {
                block.Elements[0].ValueType = block.Elements[block.Elements.Count - 1].ValueType
                                              ?? LuaDataTypeItem.Nil;
            }
            else
            {
                block.Elements[0].ValueType = LuaDataTypeItem.Nil;
            }
            if (block.Elements[0].ItemType == CodeItemType.Separator_Comma)
            {
                SetMulitType(block.Elements[0] as AnalyzeBlock);
                return;
            }
            if (_vers.ContainsKey(block.Elements[0].Name))
                _vers[block.Elements[0].Name] = block.Elements[0].ValueType;
            else
                _vers.Add(block.Elements[0].Name, block.Elements[0].ValueType);
        }

        /// <summary>
        /// In关键字的类型确定
        /// </summary>
        /// <param name="block"></param>
        private void SetMulitType(AnalyzeBlock block)
        {
            if (block == null || block.ItemType != CodeItemType.Separator_Comma ||
                block.ValueType == null || block.ValueType.ValueType != LuaDataType.Mulit)
                return;
            int idx = 0;
            foreach (var word in block.Elements.OfType<WordUnit>())
            {
                if (idx >= block.ValueType.Childs.Count || word.IsKeyWord || word.ItemType == CodeItemType.Separator_Comma)
                    continue;
                word.ValueType = block.ValueType.Childs[idx++];
                if (_vers.ContainsKey(word.Word))
                    _vers[word.Word] = word.ValueType;
                else
                    _vers.Add(word.Word, word.ValueType);
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
            if (block.Elements[0].Name == "pairs")
            {
                var call = (AnalyzeBlock)block.Elements[1];
                block.ValueType = new LuaDataTypeItem
                {
                    Name = call.Name,
                    ItemType = LuaDataType.Function,
                    ValueType = LuaDataType.Mulit,
                    Childs = new List<LuaDataTypeItem>
                    {
                        LuaDataTypeItem.NumberValue
                    }
                };
                if (call.Elements.Count > 2)
                    block.ValueType.Childs.Add(call.Elements[1].ValueType);
                return;
            }
            LuaDataTypeItem type;
            var name = block.Name ?? block.Word;
            if (_vers.TryGetValue(name, out type))
            {
                block.IsError = false;
                block.ValueType = type;
                return;
            }
            var cn = block.Elements[0].Word;

            if (!_vers.TryGetValue(cn, out type))
            {
                block.ValueType = LuaDataTypeItem.VoidFunction;
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
        /// <param name="table"></param>
        private void AnalyzeTableChild(AnalyzeBlock table)
        {
            var name = table.Name ?? table.Word;
            LuaDataTypeItem type;
            if (_vers.TryGetValue(name, out type))
            {
                table.ValueType = type;
                return;
            }
            var error = false;
            var index = 0;
            var item = table.Elements[index++];
            if (item.ItemType == CodeItemType.Call)
            {
                if (!AnalyzeCallBlock((AnalyzeBlock)item, out type))
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
            for (; index < table.Elements.Count; index++)
            {
                item = table.Elements[index];
                if (error)
                {
                    item.IsError = true;
                    continue;
                }
                if (item.ItemType == CodeItemType.Call)
                {
                    if (!AnalyzeCallBlock((AnalyzeBlock)item, out type))
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
                    LuaDataTypeItem chType;
                    if (_vers.TryGetValue(type.Name, out chType))
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
            table.ValueType = type;
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

                LuaDataTypeItem chtype;
                if (type.ValueType == LuaDataType.Function && _vers.TryGetValue(type.Name, out chtype))
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

        #region 计算语句分析

        /// <summary>
        ///     设置值语句块的分析
        /// </summary>
        /// <param name="parent"></param>
        private void ComputeBlockAnalyze(AnalyzeBlock parent)
        {
            foreach (var ch in parent.Elements)
            {
                ch.ValueType = LuaDataTypeItem.NumberValue;
                AddVariableToDictionary(ch);
            }
        }

        #endregion

        #region 赋值语句分析

        /// <summary>
        ///     设置值语句块的分析
        /// </summary>
        /// <param name="parent"></param>
        private void SetValueBlockAnalyze(AnalyzeBlock parent)
        {
            if (parent.Elements.Count == 0)
            {
                return;
            }
            var def = parent.Elements[0];
            var block = def as AnalyzeBlock;
            if (block != null && block.ItemType == CodeItemType.Key_Local)
            {
                def = block.Elements[1];
            }
            SetVarDataType(def, parent.Elements.Count < 3 ? null : parent.Elements[2]);
        }


        /// <summary>
        ///     赋值语句从右到左
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        private void SetVarDataType(AnalyzeUnitBase variable, AnalyzeUnitBase value)
        {
            if (variable.Name == null)
                variable.Name = variable.Word;
            var varBlock = variable as AnalyzeBlock;
            if (variable.ItemType != CodeItemType.Separator_Comma)
            {
                if (value?.ValueType != null)
                    variable.ValueType = value.ValueType.ValueType == LuaDataType.Mulit ? value.ValueType.Childs[0] : value.ValueType;
                AddToDictionary(variable);
                return;
            }
            if (varBlock == null)
                return;
            if (value == null)
            {
                foreach (var element in varBlock.Elements)
                {
                    if (element.ItemType == CodeItemType.Separator_Comma)
                        continue;
                    AddToDictionary(element);
                }
                return;
            }
            var valueBlock = value as AnalyzeBlock;
            if (valueBlock == null || valueBlock.ValueType.ValueType != LuaDataType.Mulit)
            {
                varBlock.Elements[0].ValueType = value.ValueType;
                for (var index = 1; index < varBlock.Elements.Count; index++)
                {
                    if (varBlock.Elements[index].ItemType == CodeItemType.Separator_Comma)
                        continue;
                    AddToDictionary(varBlock.Elements[index]);
                }
                return;
            }
            int idx = 0;
            foreach (var element in varBlock.Elements)
            {
                if (element.ItemType == CodeItemType.Separator_Comma)
                    continue;
                if (idx < valueBlock.ValueType.Childs.Count)
                {
                    element.ValueType = valueBlock.ValueType.Childs[idx++];
                }
                AddToDictionary(element);
            }
        }

        #endregion

        #region Table类型分析

        private int _tableIndex = 1;
        /// <summary>
        ///     函数类型返回值分析
        /// </summary>
        /// <param name="parent"></param>
        private void AnalyzeTableBlock(AnalyzeBlock parent)
        {
            parent.Name = $"__table__{_tableIndex++}";
            _vers.Add(parent.Name, parent.ValueType = new LuaDataTypeItem
            {
                Name = parent.Name,
                Type = parent.Name,
                ValueType = LuaDataType.Table,
                ItemType = LuaDataType.Table
            });
            if (parent.Elements.Count < 3)
                return;
            var mid = parent.Elements[1];
            var block = mid as AnalyzeBlock;
            if (block == null || mid.ItemType != CodeItemType.Separator_Comma)
            {
                CheckTableItem(parent, mid, 1);
                return;
            }
            int idx = 1;
            foreach (var element in block.Elements)
            {
                if (element.IsWord)
                {
                    switch (element.ItemType)
                    {
                        case CodeItemType.Brackets41:
                        case CodeItemType.Brackets42:
                        case CodeItemType.Separator_Comma:
                            continue;
                    }
                }
                idx++;
                CheckTableItem(parent, element, idx);
            }
        }

        private void CheckTableItem(AnalyzeBlock parent, AnalyzeUnitBase element, int idx)
        {
            string name;
            switch (element.ItemType)
            {
                case CodeItemType.Brackets31:
                    name = element.Name ?? element.Word;
                    element.ValueType = LuaDataTypeItem.Nil;
                    break;
                case CodeItemType.Separator_Equal:
                    var block = (AnalyzeBlock)element;
                    if (block.Elements.Count <= 1)
                    {
                        name = $"[{idx}]";
                        element.ValueType = LuaDataTypeItem.Nil;
                    }
                    else
                    {
                        var ne = block.Elements[0];
                        name = ne.Name ?? ne.Word;
                        if (ne.ItemType != CodeItemType.Brackets31)
                            name = $"['{name}']";
                        if (block.Elements.Count > 2)
                        {
                            block.ValueType = ne.ValueType = block.Elements[2].ValueType;
                        }
                        else
                        {
                            ne.ValueType = LuaDataTypeItem.Nil;
                        }
                    }
                    break;
                default:
                    name = $"[{idx}]";
                    if (element.ValueType == null)
                        element.ValueType = LuaDataTypeItem.Nil;
                    break;
            }
            if (element.ValueType == null)
                element.ValueType = LuaDataTypeItem.Nil;
            _vers.Add($"{parent.Name}{name}", element.ValueType);
        }

        #endregion

        #region 表值使用分析

        /// <summary>
        ///     分析值块的数据类型
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private void AnalyzeTableChildValue(AnalyzeBlock parent)
        {
            string name = null;
            LuaDataTypeItem type = null;
            var preCall = false;
            foreach (var item in parent.Elements)
            {
                switch (item.ItemType)
                {
                    case CodeItemType.Separator_Dot:
                    case CodeItemType.Separator_Colon:
                        continue;
                }
                if (name == null)
                {
                    name = item.Name ?? item.Word;
                    if (item.ValueType != null && item.ValueType != LuaDataTypeItem.Nil)
                    {
                        type = item.ValueType;
                        continue;
                    }
                }
                else
                {
                    switch (item.ItemType)
                    {
                        case CodeItemType.CallArgument:
                            if (!preCall)
                            {
                                preCall = true;
                                type.ItemType = LuaDataType.Function;
                                continue;
                            }
                            name = type.Type;
                            break;
                        case CodeItemType.Brackets31:
                            if (item.ValueType != null && item.ValueType != LuaDataTypeItem.Nil)
                            {
                                type = item.ValueType;
                                continue;
                            }
                            preCall = false;
                            name = $"{type.Type}{item.Word}";
                            break;
                        default:
                            if (item.ValueType != null && item.ValueType != LuaDataTypeItem.Nil)
                            {
                                type = item.ValueType;
                                continue;
                            }
                            preCall = false;
                            name = $"{type.Type}['{item.Name ?? item.Word}']";
                            break;
                    }
                }
                if (_vers.TryGetValue(name, out type) && type != null)
                {
                    item.ValueType = type;
                    name = type.Type;
                }
                else
                {
                    break;
                }
            }
            parent.ValueType = type ?? LuaDataTypeItem.Nil;
        }

        #endregion
        #region 函数类型返回值




        /// <summary>
        ///     函数类型返回值分析
        /// </summary>
        /// <param name="block"></param>
        private void CheckFunctionDataType(AnalyzeBlock block)
        {
            var ret = block.Elements.FirstOrDefault(p => p.ItemType == CodeItemType.Key_Return) as AnalyzeBlock;
            if (ret == null || ret.Elements.Count <= 1)
            {
                block.ValueType = LuaDataTypeItem.VoidFunction;
            }
            else if (ret.Elements[0].ValueType != null)
            {
                block.ValueType = ret.Elements[0].ValueType;
            }
            else
            {
                block.ValueType = new LuaDataTypeItem
                {
                    ItemType = LuaDataType.Function,
                    Name = ret.Elements[0].Word,
                    ValueType = LuaDataType.Confirm
                };
            }
        }


        #endregion

        #region 逗号组成的多个类型(不包括table)

        /// <summary>
        ///     逗号组成的多个类型
        /// </summary>
        /// <param name="block"></param>
        private void CheckCommaDataType(AnalyzeBlock block)
        {
            if (block.Parent.ItemFamily == CodeItemFamily.TableDefault)
                return;
            block.ValueType = new LuaDataTypeItem
            {
                Name = block.Name,
                ItemType = LuaDataType.Function,
                ValueType = LuaDataType.Mulit,
                Childs = new List<LuaDataTypeItem>()
            };

            foreach (var element in block.Elements)
            {
                if (element.ItemType != CodeItemType.Separator_Comma)
                    continue;
                var word = element as WordUnit;
                if (word != null)
                {
                    if (_vers.ContainsKey(word.Word))
                        word.ValueType = _vers[word.Word];
                    else
                        _vers.Add(word.Word, word.ValueType = LuaDataTypeItem.Nil);
                }
                else if (element.ValueType == null)
                    element.ValueType = LuaDataTypeItem.Nil;
                block.ValueType.Childs.Add(element.ValueType);
            }
        }

        #endregion

        #region 设置结果值

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
                if (unit.ValueType != null && unit.ValueType.ValueType != LuaDataType.Confirm)
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
                LuaDataTypeItem type;
                if (_vers.TryGetValue(word.Name, out type))
                    word.ValueType = type;
                else
                    _vers.Add(word.Name, LuaDataTypeItem.Error);
            }
        }

        #endregion

        #region 类型字典构造


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
                        Type = Config?.ModelType ?? typeof(ConfigBase).Name,
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
                ,
                {"append", LuaDataTypeItem.VoidFunction},
                {"pinYin", LuaDataTypeItem.StringFunction},
                {"shengMu", LuaDataTypeItem.StringFunction},
                {"toFieldName", LuaDataTypeItem.StringFunction},
                {"encodeBase64", LuaDataTypeItem.StringFunction},
                {"decodeBase64", LuaDataTypeItem.StringFunction},
                {"toHungaryName", LuaDataTypeItem.StringFunction},
                {"toHumpName", LuaDataTypeItem.StringFunction},
                {"toPascalName", LuaDataTypeItem.StringFunction},
                {"toUnderName", LuaDataTypeItem.StringFunction},
                {"spliteWord", LuaDataTypeItem.StringFunction},
                {"toWordName", LuaDataTypeItem.StringFunction},
                {"toLinkWordName", LuaDataTypeItem.StringFunction},
                {"toPluralism", LuaDataTypeItem.StringFunction},
                {"listToString", LuaDataTypeItem.StringFunction},
                {"dictionaryToString", LuaDataTypeItem.StringFunction},
                {"mulitReplace", LuaDataTypeItem.StringFunction},
                {"mulitReplaceOne", LuaDataTypeItem.StringFunction},
                {"isNullOrDefault", LuaDataTypeItem.StringFunction},
                {"isName", LuaDataTypeItem.StringFunction},
                {"toUWord", LuaDataTypeItem.StringFunction},
                {"toLWord", LuaDataTypeItem.StringFunction},
                {"isEquals", LuaDataTypeItem.StringFunction},
                {"getLen", LuaDataTypeItem.StringFunction},
                {"toDataBaseType", LuaDataTypeItem.StringFunction}
            };
            AddTypeDictionary(typeof(PropertyConfig));
            AddTypeDictionary(typeof(ProjectConfig));
            AddTypeDictionary(typeof(EntityConfig));
            AddTypeDictionary(typeof(EnumConfig));
        }

        /// <summary>
        ///     初始化类型字典
        /// </summary>
        private void AddTypeDictionary(Type type)
        {
            if (_vers.ContainsKey(type.Name))
                return;
            _vers.Add(type.Name, new LuaDataTypeItem
            {
                Type = type.Name,
                ItemType = LuaDataType.Table,
                ValueType = LuaDataType.Table
            });
            AddTypeDictionary(type, type.Name);
        }

        /// <summary>
        ///     初始化类型字典
        /// </summary>
        private void AddTypeDictionary(Type type, string name)
        {
            if (type == typeof(object) || type == typeof(string) || type.IsValueType)
                return;
            AddTypeDictionary(type.BaseType, name);
            foreach (
                var pro in
                type.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public |
                                   BindingFlags.DeclaredOnly))
            {
                var pn = $"{name}['{pro.Name}']";
                if (_vers.ContainsKey(pn))
                    continue;
                if (pro.PropertyType == typeof(bool))
                    _vers.Add(pn, LuaDataTypeItem.BoolValue);
                else if (pro.PropertyType == typeof(string))
                    _vers.Add(pn, LuaDataTypeItem.StringValue);
                else if (pro.PropertyType.IsValueType)
                    _vers.Add(pn, LuaDataTypeItem.NumberValue);
                else if (pro.PropertyType.IsSupperInterface(typeof(IEnumerable<>)))
                {
                    _vers.Add(pn, new LuaDataTypeItem
                    {
                        Type = pro.PropertyType.GenericTypeArguments[0].Name,
                        ItemType = LuaDataType.Iterator,
                        ValueType = LuaDataType.Table
                    });
                    AddTypeDictionary(pro.PropertyType.GenericTypeArguments[0]);
                }
                else if (pro.PropertyType.IsClass)
                {
                    _vers.Add(pn, new LuaDataTypeItem
                    {
                        Type = pro.PropertyType.Name
                    });
                    AddTypeDictionary(pro.PropertyType);
                }
            }
        }

        #endregion
    }
}