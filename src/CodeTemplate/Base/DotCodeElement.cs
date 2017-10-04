using System.Runtime.Serialization;

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     点连接的组合单元(需要后期分解的)
    /// </summary>
    [DataContract]
    public sealed class DotCodeElement : MulitCodeElement
    {
        /// <summary>
        /// 是否纯的命名空间(类型已分离)
        /// </summary>
        [DataMember]
        public bool IsNameSapce
        {
            get;
            set;
        }
    }
}