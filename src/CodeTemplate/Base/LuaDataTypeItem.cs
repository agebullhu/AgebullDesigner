using System.Collections.Generic;

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     LUA数据类型表节点
    /// </summary>
    public class LuaDataTypeItem
    {
        public static readonly LuaDataTypeItem Nil = new LuaDataTypeItem
        {
            Type = "nil",
            ItemType = LuaDataType.BaseType,
            ValueType = LuaDataType.Nil
        };

        public static readonly LuaDataTypeItem Error = new LuaDataTypeItem
        {
            ValueType = LuaDataType.Error
        };

        public static readonly LuaDataTypeItem Confirm = new LuaDataTypeItem
        {
            ValueType = LuaDataType.Confirm,
            ItemType = LuaDataType.Error
        };

        public static readonly LuaDataTypeItem TableDefinition = new LuaDataTypeItem
        {
            ValueType = LuaDataType.Confirm,
            ItemType = LuaDataType.Table
        };

        public static readonly LuaDataTypeItem FunctionConfirm = new LuaDataTypeItem
        {
            ValueType = LuaDataType.Confirm,
            ItemType = LuaDataType.Function
        };

        public static readonly LuaDataTypeItem StringValue = new LuaDataTypeItem
        {
            Type = "string",
            ItemType = LuaDataType.BaseType,
            ValueType = LuaDataType.String
        };

        public static readonly LuaDataTypeItem NumberValue = new LuaDataTypeItem
        {
            Type = "number",
            ItemType = LuaDataType.BaseType,
            ValueType = LuaDataType.Number
        };

        public static readonly LuaDataTypeItem BoolValue = new LuaDataTypeItem
        {
            Type = "bool",
            ItemType = LuaDataType.BaseType,
            ValueType = LuaDataType.Bool
        };

        public static readonly LuaDataTypeItem StringFunction = new LuaDataTypeItem
        {
            ItemType = LuaDataType.Function,
            ValueType = LuaDataType.String
        };

        public static readonly LuaDataTypeItem NumberFunction = new LuaDataTypeItem
        {
            ItemType = LuaDataType.Function,
            ValueType = LuaDataType.Number
        };

        public static readonly LuaDataTypeItem VoidFunction = new LuaDataTypeItem
        {
            ItemType = LuaDataType.Function,
            ValueType = LuaDataType.Void
        };

        public static readonly LuaDataTypeItem BoolFunction = new LuaDataTypeItem
        {
            ItemType = LuaDataType.Function,
            ValueType = LuaDataType.Bool
        };

        /// <summary>
        ///     名称(Table或Function需要)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     名称(Table或Function需要)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     节点类型
        /// </summary>
        public LuaDataType ItemType { get; set; }

        /// <summary>
        ///     值类型
        /// </summary>
        public LuaDataType ValueType { get; set; }

        public override string ToString()
        {
            return Type ?? ItemType + "-" + ValueType;
        }
        /// <summary>
        /// ValueType 为 Mulit时有子级
        /// </summary>
        public List<LuaDataTypeItem> Childs { get; set; }
    }
}