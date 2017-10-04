namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    /// 组合特性
    /// </summary>
    public enum JoinFeature
    {
        /// <summary>
        /// 不组合
        /// </summary>
        None,
        /// <summary>
        /// 与后面组合
        /// </summary>
        Before,
        /// <summary>
        /// 与前面的组合
        /// </summary>
        Front,
        /// <summary>
        /// 前后组合
        /// </summary>
        TowWay,
        /// <summary>
        /// 串连
        /// </summary>
        Connect,
        /// <summary>
        /// 与后面的特定单词组合
        /// </summary>
        BeforeBy,
        /// <summary>
        /// 与前面的特定单词组合
        /// </summary>
        FrontBy,
        /// <summary>
        /// 前后组合
        /// </summary>
        TowWayBy,
        /// <summary>
        /// 闭合区间组合
        /// </summary>
        CloseRange,
        /// <summary>
        /// 块开始
        /// </summary>
        BlockOpen,
        /// <summary>
        /// 块结束
        /// </summary>
        BlockClose,
        /// <summary>
        /// 括号开始
        /// </summary>
        BracketOpen,
        /// <summary>
        /// 括号结束
        /// </summary>
        BracketClose, 
        /// <summary>
        /// 区域开始
        /// </summary>
        RangeOpen,
        /// <summary>
        /// 区域转变
        /// </summary>
        RangeShift,
        /// <summary>
        /// 区域结束
        /// </summary>
        RangeClose
    }
}