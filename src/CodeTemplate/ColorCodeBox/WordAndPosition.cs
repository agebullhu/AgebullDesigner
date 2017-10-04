namespace Agebull.CodeRefactor.CodeTemplate
{
    /// <summary>
    ///   匹配到的单词的结构体
    /// </summary>
    public class WordAndPosition
    {
        /// <summary>
        ///  类型:0 一般,1字符串,2注释,3在XML中为对象头,4在XML中为对象尾,5为属性尾,6为=号,7为属性,8为值
        /// </summary>
        public int Type;

        /// <summary>
        ///   匹配的单词
        /// </summary>
        public string Word;

        /// <summary>
        ///   起始位置
        /// </summary>
        public int Position;

        /// <summary>
        ///   单词长度
        /// </summary>
        public int Length;

        /// <summary>
        ///   是否文本
        /// </summary>
        public bool IsString;

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"{Word} - {Position} - {Length}";
        }
    }
}