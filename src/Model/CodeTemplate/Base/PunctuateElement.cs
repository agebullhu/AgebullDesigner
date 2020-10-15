using System.Runtime.Serialization;
using Agebull.CodeRefactor.CodeAnalyze;

namespace Gboxt.ProjectDesign.CodeAnalyzer
{
    /// <summary>
    ///     表示一个标点的基本单元
    /// </summary>
    [DataContract]
    public class PunctuateElement : CodeElement
    {
        /// <summary>
        /// 计算符号的优先级
        /// </summary>
        public int Level
        {
            get;
            set;
        }
    }
}