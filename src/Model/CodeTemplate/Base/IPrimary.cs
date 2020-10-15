using System.Runtime.Serialization;

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    /// 有主要节点的对象
    /// </summary>
    public interface IPrimary
    {

        /// <summary>
        ///     核心结点
        /// </summary>
        [DataMember]
        CodeItem Primary
        {
            get;
            set;
        }
        
        /// <summary>
        ///     核心结点
        /// </summary>
        [IgnoreDataMember]
        CodeElement PrimaryElement
        {
            get;
        }
    }
}