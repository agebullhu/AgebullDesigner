// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-10-23
// 修改:2014-11-08
// *****************************************************/

#region 引用

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     关键字相关的常量
    /// </summary>
    public static class KeyWordHelper
    {
        public static readonly string[] KeyWords =
        {
            "abstract", "as", "base", "break", "case", "catch", "checked", "class", "const", "continue",
            "default", "delegate", "do", "else", "enum", "event", "explicit", "extern", "false", "finally",
            "fixed", "for", "foreach", "goto", "if", "implicit", "in", "interface", "internal", "is", "lock",
            "namespace", "new", "null", "operator", "out", "override", "params", "private", "protected",
            "public", "readonly", "ref", "return", "sealed", "sizeof", "stackalloc", "static", "struct",
            "switch", "this", "throw", "true", "try", "typeof", "unchecked", "unsafe", "using", "virtual",
            "volatile", "while",
            "add", "alias", "ascending", "descending", "dynamic", "get", "global", "group", "into", "join", "let",
            "orderby", "partial", "remove", "select", "set", "value", "var", "where", "yield","cast"
        };

        public static readonly string[] TypeWords =
        {
            "bool", "byte", "char", "decimal", "double", "float", "int", "long", "Object", "sbyte", "short",
            "string", "uint", "ulong", "ushort", "void",
            "object", "Float", "Double", "Decimal", "Byte", "SByte", "String", "Char", "Guid", "DateTime",
            "Int16", "Int32", "Int64", "UInt16", "UInt32", "UInt64"
        };

        private static bool IsConditionKeyWord(CodeElement element)
        {
            switch (element.Word)
            {
                case "switch":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Condition;
                    element.ItemType = CodeItemType.Key_Switch;
                    return true;
                case "while":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Condition;
                    element.ItemType = CodeItemType.Key_While;
                    return true;
                case "do":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Condition;
                    element.ItemType = CodeItemType.Key_Do;
                    return true;
                case "default":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Condition;
                    element.ItemType = CodeItemType.Key_Default;
                    return true;
                case "case":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Condition;
                    element.ItemType = CodeItemType.Key_Case;
                    return true;
                case "if":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Condition;
                    element.ItemType = CodeItemType.Key_If;
                    return true;
                case "else":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Condition;
                    element.ItemType = CodeItemType.Key_Else;
                    return true;
                case "for":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Iterator;
                    element.ItemType = CodeItemType.Key_For;
                    return true;
                case "foreach":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Iterator;
                    element.ItemType = CodeItemType.Key_Foreach;
                    return true;
                case "in":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_In;
                    return true;
                case "try":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Control;
                    element.ItemType = CodeItemType.Key_Try;
                    return true;
                case "catch":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Control;
                    element.ItemType = CodeItemType.Key_Catch;
                    return true;
                case "throw":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Control;
                    element.ItemType = CodeItemType.Key_Throw;
                    return true;
                case "finally":
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_Finally;
                    return true;
            }
            return false;
        }

        private static bool IsSystemType(CodeElement element)
        {
            switch (element.Word)
            {
                case "void":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Void;
                    return true;
                case "dynamic":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Key_Dynamic;
                    return true;
                case "var":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Key_Var;
                    return true;
                case "Boolean":
                case "bool":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Boolean;
                    element.Word = "bool";
                    return true;
                case "Object":
                case "object":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Object;
                    element.Word = "object";
                    return true;
                case "float":
                case "Float":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Float;
                    element.Word = "float";
                    return true;
                case "double":
                case "Double":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Double;
                    element.Word = "double";
                    return true;
                case "decimal":
                case "Decimal":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Decimal;
                    element.Word = "decimal";
                    return true;
                case "byte":
                case "Byte":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Byte;
                    element.Word = "byte";
                    return true;
                case "sbyte":
                case "SByte":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Sbyte;
                    element.Word = "sbyte";
                    return true;
                case "string":
                case "String":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_String;
                    element.Word = "string";
                    return true;
                case "Char":
                case "char":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Char;
                    element.Word = "char";
                    return true;
                case "Guid":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Guid;
                    return true;
                case "DateTime":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Datetime;
                    return true;
                case "short":
                case "Int16":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Int16;
                    element.Word = "short";
                    return true;
                case "int":
                case "Int32":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Int32;
                    element.Word = "int";
                    return true;
                case "long":
                case "Int64":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Int64;
                    element.Word = "long";
                    return true;
                case "IntPtr":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_IntPtr;
                    element.Word = "IntPtr";
                    return true;
                case "ushort":
                case "UInt16":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Uint16;
                    element.Word = "ushort";
                    return true;
                case "uint":
                case "UInt32":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Uint32;
                    element.Word = "uint";
                    return true;
                case "ulong":
                case "UInt64":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_Uint64;
                    element.Word = "ulong";
                    return true;
                case "UIntPtr":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_UIntPtr;
                    element.Word = "UIntPtr";
                    return true;
                case "BigInteger":
                    element.ItemRace = CodeItemRace.Type;
                    element.ItemFamily = CodeItemFamily.SystemType;
                    element.ItemType = CodeItemType.Var_BigInteger;
                    element.Word = "BigInteger";
                    return true;
            }

            return false;
        }

        private static bool IsSystemValue(CodeElement element)
        {
            switch (element.Word)
            {
                case "base":
                    element.ItemRace = CodeItemRace.Value;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_Base;
                    return true;
                case "this":
                    element.ItemRace = CodeItemRace.Value;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_This;
                    return true;
                case "null":
                    element.ItemRace = CodeItemRace.Value;
                    element.ItemFamily = CodeItemFamily.Constant;
                    element.ItemType = CodeItemType.Value_Null;
                    return true;
                case "true":
                    element.ItemRace = CodeItemRace.Value;
                    element.ItemFamily = CodeItemFamily.Constant;
                    element.ItemType = CodeItemType.Value_True;
                    return true;
                case "false":
                    element.ItemRace = CodeItemRace.Value;
                    element.ItemFamily = CodeItemFamily.Constant;
                    element.ItemType = CodeItemType.Value_False;
                    return true;
            }
            return false;
        }

        private static bool IsAccess(CodeElement element)
        {
            switch (element.Word)
            {
                case "implicit":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Implicit;
                    return true;
                case "explicit":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Explicit;
                    return true;
                case "extern":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Extern;
                    return true;
                case "fixed":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Fixed;
                    return true;
                case "partial":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Partial;
                    return true;
                case "internal":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Internal;
                    return true;
                case "private":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Private;
                    return true;
                case "protected":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Protected;
                    return true;
                case "public":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Public;
                    return true;
                case "sealed":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Sealed;
                    return true;
                case "readonly":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Readonly;
                    return true;
                case "out":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Out;
                    return true;
                case "ref":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Ref;
                    return true;
                case "static":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Access;
                    element.ItemType = CodeItemType.Key_Static;
                    return true;
            }
            return false;
        }

        private static bool IsContextKeyword(CodeElement element)
        {
            switch (element.Word)
            {
                case "value":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Value;
                    return true;
                case "global":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Global;
                    return true;

                case "get":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Get;
                    return true;
                case "set":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Set;
                    return true;

                    #region Lambda

                case "form":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Form;
                    return true;
                case "add":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Add;
                    return true;
                case "ascending":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Ascending;
                    return true;
                case "descending":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Descending;
                    return true;
                case "group":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Group;
                    return true;
                case "into":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Into;
                    return true;
                case "join":
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Join;
                    return true;
                case "let":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Let;
                    return true;
                case "orderby":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Orderby;
                    return true;
                case "remove":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Remove;
                    return true;
                case "select":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Select;
                    return true;
                case "where":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Where;
                    return true;
                case "yield":
                    element.ItemRace = CodeItemRace.None;
                    element.ItemFamily = CodeItemFamily.ContextKeyWord;
                    element.ItemType = CodeItemType.Key_Yield;
                    return true;

                    #endregion
            }
            return false;
        }

        private static bool IsMemoryKeyword(CodeElement element)
        {
            switch (element.Word)
            {
                case "const":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Memory;
                    element.ItemType = CodeItemType.Key_Const;
                    return true;
                case "stackalloc":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Memory;
                    element.ItemType = CodeItemType.Key_Stackalloc;
                    return true;
                case "checked":
                    element.ItemFamily = CodeItemFamily.Memory;
                    element.ItemType = CodeItemType.Key_Checked;
                    return true;
                case "unchecked":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Memory;
                    element.ItemType = CodeItemType.Key_Unchecked;
                    return true;
                case "lock":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Memory;
                    element.ItemType = CodeItemType.Key_Lock;
                    return true;
                case "unsafe":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Memory;
                    element.ItemType = CodeItemType.Key_Unsafe;
                    return true;
                case "volatile":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Memory;
                    element.ItemType = CodeItemType.Key_Volatile;
                    return true;
            }
            return false;
        }

        private static bool IsControlKeyWord(CodeElement element)
        {
            switch (element.Word)
            {
                case "goto":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Control;
                    element.ItemType = CodeItemType.Key_Goto;
                    return true;
                case "break":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Control;
                    element.ItemType = CodeItemType.Key_Break;
                    return true;
                case "continue":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Control;
                    element.ItemType = CodeItemType.Key_Continue;
                    return true;
                case "return":
                    element.ItemFamily = CodeItemFamily.Control;
                    element.ItemType = CodeItemType.Key_Return;
                    return true;
            }
            return false;
        }

        private static bool IsTypeKeyWord(CodeElement element)
        {
            switch (element.Word)
            {
                case "namespace":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Type;
                    element.ItemType = CodeItemType.Key_Namespace;
                    return true;
                case "struct":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Type;
                    element.ItemType = CodeItemType.Key_Struct;
                    return true;
                case "class":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Type;
                    element.ItemType = CodeItemType.Key_Class;
                    return true;
                case "delegate":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Type;
                    element.ItemType = CodeItemType.Key_Delegate;
                    return true;
                case "enum":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Type;
                    element.ItemType = CodeItemType.Key_Enum;
                    return true;
                case "interface":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.Type;
                    element.ItemType = CodeItemType.Key_Interface;
                    return true;
                case "event":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.TypeChildBlock;
                    element.ItemType = CodeItemType.Key_Event;
                    return true;
                case "typeof":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_Typeof;
                    return true;
                case "as":
                    element.ItemRace = CodeItemRace.Punctuate;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_As;
                    return true;
                case "is":
                    element.ItemRace = CodeItemRace.Punctuate;
                    element.ItemFamily = CodeItemFamily.Compute;
                    element.ItemType = CodeItemType.Key_Is;
                    return true;
                case "abstract":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_Abstract;
                    return true;
                case "override":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_Override;
                    return true;
                case "virtual":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_Virtual;
                    return true;
            }
            return false;
        }

        public static bool CheckKeyWord(CodeElement element)
        {
            if (IsConditionKeyWord(element))
            {
                return true;
            }
            if (IsSystemType(element))
            {
                return true;
            }
            if (IsSystemValue(element))
            {
                return true;
            }
            if (IsAccess(element))
            {
                return true;
            }
            if (IsContextKeyword(element))
            {
                return true;
            }
            if (IsMemoryKeyword(element))
            {
                return true;
            }
            if (IsControlKeyWord(element))
            {
                return true;
            }
            if (IsTypeKeyWord(element))
            {
                return true;
            }
            switch (element.Word)
            {
                case "new":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_New;
                    return true;
                case "sizeof":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_Sizeof;
                    return true;
                case "operator":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_Operator;
                    return true;
                case "params":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_Params;
                    return true;
                case "using":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_Using;
                    return true;
                case "alias":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_Alias;
                    return true;
                case "cast":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_Cast;
                    return true;
                case "await":
                    element.ItemRace = CodeItemRace.KeyWord;
                    element.ItemFamily = CodeItemFamily.KeyWord;
                    element.ItemType = CodeItemType.Key_Await;
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        ///     分析特性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ObjectFeature CheckFeature(CodeItemType type)
        {
            switch (type)
            {
                case CodeItemType.Key_Partial:
                    return ObjectFeature.Partial;

                case CodeItemType.Key_Sealed:
                    return ObjectFeature.Sealed;

                case CodeItemType.Key_Static:
                    return ObjectFeature.Static;

                case CodeItemType.Key_Abstract:
                    return ObjectFeature.Abstract;

                case CodeItemType.Key_Override:
                    return ObjectFeature.Override;

                case CodeItemType.Key_Virtual:
                    return ObjectFeature.Virtual;

                case CodeItemType.Key_New:
                    return ObjectFeature.New;

                case CodeItemType.Key_Implicit:
                    return ObjectFeature.Implicit;

                case CodeItemType.Key_Explicit:
                    return ObjectFeature.Explicit;

                case CodeItemType.Key_Const:
                    return ObjectFeature.Const;

                case CodeItemType.Key_Readonly:
                    return ObjectFeature.ReadOnly;

                case CodeItemType.Key_Ref:
                    return ObjectFeature.Ref;

                case CodeItemType.Key_Out:
                    return ObjectFeature.Out;

                case CodeItemType.Key_Extern:
                    return ObjectFeature.Extern;

                case CodeItemType.Key_Unchecked:
                    return ObjectFeature.Unchecked;

                case CodeItemType.Key_Unsafe:
                    return ObjectFeature.Unsafe;

                case CodeItemType.Key_Volatile:
                    return ObjectFeature.Volatile;

                case CodeItemType.Key_Lock:
                    return ObjectFeature.Lock;

                case CodeItemType.Key_Stackalloc:
                    return ObjectFeature.Stackalloc;
                case CodeItemType.Key_Params:
                    return ObjectFeature.Params;
                case CodeItemType.Key_In:
                    return ObjectFeature.In;
                case CodeItemType.Key_Operator:
                    return ObjectFeature.Operator;
            }
            return ObjectFeature.None;
        }

        public static AccessType CheckAccessType(CodeItemType type)
        {
            switch (type)
            {
                case CodeItemType.Key_Public:
                    return AccessType.Public;
                case CodeItemType.Key_Private:
                    return AccessType.Private;
                case CodeItemType.Key_Protected:
                    return AccessType.Protected;
                case CodeItemType.Key_Internal:
                    return AccessType.Internal;
            }
            return AccessType.Private;
        }

        /// <summary>
        ///     分析特性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static void CheckFeature(object obj, IEnumerable<CodeItem> elements)
        {
            foreach (CodeItem el in elements.Where(p => p.ItemRace == CodeItemRace.KeyWord))
            {
                CheckFeature(obj, el.ItemType);
            }
        }

        /// <summary>
        ///     分析特性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool CheckFeature(object obj, CodeItemType type)
        {
            IFeature f = obj as IFeature;
            if (f != null)
            {
                switch (type)
                {
                    case CodeItemType.Key_Partial:
                        f.Feature |= ObjectFeature.Partial;
                        return true;
                    case CodeItemType.Key_Sealed:
                        f.Feature |= ObjectFeature.Sealed;
                        return true;
                    case CodeItemType.Key_Static:
                        f.Feature |= ObjectFeature.Static;
                        return true;
                    case CodeItemType.Key_Abstract:
                        f.Feature |= ObjectFeature.Abstract;
                        return true;
                    case CodeItemType.Key_Override:
                        f.Feature |= ObjectFeature.Override;
                        return true;
                    case CodeItemType.Key_Virtual:
                        f.Feature |= ObjectFeature.Virtual;
                        return true;
                    case CodeItemType.Key_New:
                        f.Feature |= ObjectFeature.New;
                        return true;
                    case CodeItemType.Key_Implicit:
                        f.Feature |= ObjectFeature.Implicit;
                        return true;
                    case CodeItemType.Key_Explicit:
                        f.Feature |= ObjectFeature.Explicit;
                        return true;
                    case CodeItemType.Key_Const:
                        f.Feature |= ObjectFeature.Const;
                        return true;
                    case CodeItemType.Key_Readonly:
                        f.Feature |= ObjectFeature.ReadOnly;
                        return true;
                    case CodeItemType.Key_Ref:
                        f.Feature |= ObjectFeature.Ref;
                        return true;
                    case CodeItemType.Key_Out:
                        f.Feature |= ObjectFeature.Out;
                        return true;
                    case CodeItemType.Key_Extern:
                        f.Feature |= ObjectFeature.Extern;
                        return true;
                    case CodeItemType.Key_Unchecked:
                        f.Feature |= ObjectFeature.Unchecked;
                        return true;
                    case CodeItemType.Key_Unsafe:
                        f.Feature |= ObjectFeature.Unsafe;
                        return true;
                    case CodeItemType.Key_Volatile:
                        f.Feature |= ObjectFeature.Volatile;
                        return true;
                    case CodeItemType.Key_Lock:
                        f.Feature |= ObjectFeature.Lock;
                        return true;
                    case CodeItemType.Key_Stackalloc:
                        f.Feature |= ObjectFeature.Stackalloc;
                        return true;
                    case CodeItemType.Key_Params:
                        f.Feature |= ObjectFeature.Params;
                        return true;
                    case CodeItemType.Key_In:
                        f.Feature |= ObjectFeature.In;
                        return true;
                    case CodeItemType.Key_Operator:
                        f.Feature |= ObjectFeature.Operator;
                        return true;
                }
            }
            IAccess a = obj as IAccess;
            if (a == null)
            {
                return false;
            }
            switch (type)
            {
                case CodeItemType.Key_Public:
                    a.AccessType |= AccessType.Public;
                    return true;
                case CodeItemType.Key_Private:
                    a.AccessType |= AccessType.Private;
                    return true;
                case CodeItemType.Key_Protected:
                    a.AccessType |= AccessType.Protected;
                    return true;
                case CodeItemType.Key_Internal:
                    a.AccessType |= AccessType.Internal;
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 检查优先级
        /// </summary>
        internal static void CheckLevel(this CodeElement element)
        {
            switch (element.ItemRace)
            {
                case CodeItemRace.None:
                case CodeItemRace.Type:
                case CodeItemRace.KeyWord:
                    element.Level = 1;
                    element.IsWord = true;
                    return;
                case CodeItemRace.Value:
                    element.Level = 1;
                    element.IsWord = false;
                    return;
            }
            element.IsWord = false;
            switch (element.Word)
            {
                case "～":
                case "!":
                case "++":
                case "--":
                case "??":
                //case "is"://文档上说是6,可是它应该不是吧
                //case "as":
                    element.Level = 2;
                    break;
                case "*":
                case "/":
                case "%":
                    element.Level = 3;
                    break;
                case "+":
                case "-":
                    element.Level = 4;
                    break;
                case "<<":
                case ">>":
                    element.Level = 5;
                    break;
                case "<":
                case ">":
                case "<=":
                case ">=":
                    element.Level = 6;
                    break;
                case "==":
                case "!=":
                    element.Level = 7;
                    break;
                case "&":
                    element.Level = 8;
                    break;
                case "^":
                    element.Level = 9;
                    break;
                case "|":
                    element.Level = 10;
                    break;
                case "&&":
                    element.Level = 11;
                    break;
                case "||":
                    element.Level = 12;
                    break;
                case "?":
                case ":":
                    element.Level = 13;
                    break;
                case "=":
                case "*=":
                case "+=":
                case "-=":
                case "/=":
                case "%=":
                case ">>=":
                case "<<=":
                case "&=":
                case "|=":
                case "^=":
                case "~=":
                    element.Level = 14;
                    break;
            }
        }
    }
    /// <summary>
    /// 表示变量
    /// </summary>
    public interface IVariable : IFeature
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name
        {
            get;
        }

        ///// <summary>
        ///// 类型
        ///// </summary>

        //TypeSentence Type
        //{
        //    get;
        //}
        /// <summary>
        /// 节点类型
        /// </summary>
        CodeItemType ItemType
        {
            get;
        }
    }
    public interface IFeature
    {
        /// <summary>
        ///     特性
        /// </summary>
        ObjectFeature Feature
        {
            get;
            set;
        }
    }

    public interface IAccess
    {
        /// <summary>
        ///     访问类型
        /// </summary>
        AccessType AccessType
        {
            get;
            set;
        }
    }

}
