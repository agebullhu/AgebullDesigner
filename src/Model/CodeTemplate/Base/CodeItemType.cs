// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// 作者:bull2
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2014-10-23
// 修改:2014-11-08
// *****************************************************/

namespace Agebull.EntityModel.RobotCoder.CodeTemplate
{
    // ReSharper disable InconsistentNaming
    /// <summary>
    ///     代码基本单元类型
    /// </summary>
    public enum CodeItemType
    {
        /// <summary>
        ///     未知
        /// </summary>
        None,

        #region 空白

        /// <summary>
        ///     空字符串
        /// </summary>
        Space,

        /// <summary>
        ///     换行
        /// </summary>
        Line,

        #endregion

        #region 转义
        
        /// <summary>
        ///     块转义字符
        /// </summary>
        BlockSharp,

        /// <summary>
        ///     值转义字符
        /// </summary>
        ValueSharp,

        #endregion

        #region 常量

        /// <summary>
        ///     字符
        /// </summary>
        Char,

        /// <summary>
        ///     文本
        /// </summary>
        String,

        /// <summary>
        ///     内容
        /// </summary>
        Content,

        /// <summary>
        ///     数字
        /// </summary>
        Number,

        /// <summary>
        ///     常量
        /// </summary>
        Constant,

        #endregion

        #region 注释

        /// <summary>
        ///     单选注释
        /// </summary>
        SignleRem,

        /// <summary>
        ///     多行注释
        /// </summary>
        MulitRem,

        /// <summary>
        ///     模板配置
        /// </summary>
        TemplateConfig,

        /// <summary>
        ///     模板注释
        /// </summary>
        TemplateRem,

        #endregion

        #region 关键字

        Value_Null,
        Value_True,
        Value_False,
        Key_Local,
        Key_Return,
        Key_Function,
        Key_And,
        Key_Not,
        Key_Or,
        Key_If,
        Key_Else,
        Key_Elseif,
        Key_Repeat,
        Key_Until,
        Key_While,
        Key_For,
        Key_Foreach,
        Key_In,
        Key_Then,
        Key_Go,
        Key_Do,
        Key_End,
        Key_Break,

        #endregion

        #region 类型

        Key_Var,

        Var_Void,

        Var_Object,

        Var_Boolean,

        Var_Float,

        Var_Double,

        Var_Decimal,

        Var_Byte,

        Var_Sbyte,

        Var_String,

        Var_Char,

        Var_Guid,

        Var_Datetime,

        Var_Int16,

        Var_Int32,

        Var_Int64,

        Var_Uint16,

        Var_Uint32,

        Var_Uint64,

        Var_BigInteger,

        Var_IntPtr,

        Var_UIntPtr,

        Var_Array,

        Var_Nullable,

        #endregion

        #region 变量

        /// <summary>
        ///     变量
        /// </summary>
        Variable_Global,

        /// <summary>
        ///     局部变量
        /// </summary>
        Variable_Local,

        /// <summary>
        ///     变长参数arg
        /// </summary>
        Variable_Arg,

        /// <summary>
        ///     表对象的子级
        /// </summary>
        Table_Child,

        /// <summary>
        ///     表定义
        /// </summary>
        Table,

        #endregion

        #region 运算符

        /// <summary>
        ///     文本长度符
        /// </summary>
        StringLen,


        /// <summary>
        ///     显示关键字
        /// </summary>
        Print,

        #endregion

        #region 分隔符

        /// <summary>
        ///     等于号
        /// </summary>
        Separator_Equal,

        /// <summary>
        ///     点
        /// </summary>
        Separator_Dot,

        /// <summary>
        ///     冒号
        /// </summary>
        Separator_Colon,

        /// <summary>
        ///     逗号
        /// </summary>
        Separator_Comma,

        /// <summary>
        ///     文本连接符
        /// </summary>
        Separator_StringJoin,


        /// <summary>
        ///     分号
        /// </summary>
        Separator_Semicolon,

        #endregion

        #region 计算符号

        /// <summary>
        ///     加
        /// </summary>
        Compute_Add,

        /// <summary>
        ///     减
        /// </summary>
        Compute_Sub,

        /// <summary>
        ///     乘
        /// </summary>
        Compute_Mulit,

        /// <summary>
        ///     除
        /// </summary>
        Compute_Div,

        /// <summary>
        ///     求余
        /// </summary>
        Compute_Mod,

        /// <summary>
        ///     指数
        /// </summary>
        Compute_Exp,

        #endregion

        #region 比较符号

        /// <summary>
        ///     等于
        /// </summary>
        Compare_Equal,

        /// <summary>
        ///     大于
        /// </summary>
        Compare_Greater,

        /// <summary>
        ///     大等于
        /// </summary>
        Compare_GreaterEqual,

        /// <summary>
        ///     小于
        /// </summary>
        Compare_Less,

        /// <summary>
        ///     小等于
        /// </summary>
        Compare_LessEqual,

        /// <summary>
        ///     不等于
        /// </summary>
        Compare_NotEqual,

        #endregion

        #region 括号

        /// <summary>
        ///     尖括号正
        /// </summary>
        Brackets11,

        /// <summary>
        ///     尖括号反
        /// </summary>
        Brackets12,

        /// <summary>
        ///     小括号正
        /// </summary>
        Brackets21,

        /// <summary>
        ///     小括号反
        /// </summary>
        Brackets22,

        /// <summary>
        ///     中括号正
        /// </summary>
        Brackets31,

        /// <summary>
        ///     中括号反
        /// </summary>
        Brackets32,

        /// <summary>
        ///     大括号正
        /// </summary>
        Brackets41,

        /// <summary>
        ///     大括号反
        /// </summary>
        Brackets42,

        #endregion

        #region 区域

        /// <summary>
        ///     区域
        /// </summary>
        Range,

        /// <summary>
        ///     下标区域
        /// </summary>
        InferiorRange,

        /// <summary>
        ///     方法
        /// </summary>
        Function,

        /// <summary>
        ///     条件
        /// </summary>
        Condition,

        /// <summary>
        ///     调用参数
        /// </summary>
        CallArgument,

        /// <summary>
        ///     参数定义
        /// </summary>
        Argument,

        #endregion

        #region 语句

        /// <summary>
        ///     普通语句
        /// </summary>
        Sentence,

        /// <summary>
        ///     赋值语句
        /// </summary>
        SetValueSentence,

        /// <summary>
        ///     赋值语句
        /// </summary>
        InValueSentence,

        /// <summary>
        ///     逻辑(AND OR)语句
        /// </summary>
        LogicalSentence,

        /// <summary>
        ///     数学计算语句
        /// </summary>
        ComputeSentence,

        /// <summary>
        ///     比较语句(包括NOT)
        /// </summary>
        CompareSentence,

        /// <summary>
        ///     方法调用语句
        /// </summary>
        Call

        #endregion
    }
}