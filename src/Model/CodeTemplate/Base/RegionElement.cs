// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// ����:2014-10-23
// �޸�:2014-11-08
// *****************************************************/

using System.Runtime.Serialization;

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     ��ʾ����Ľڵ�
    /// </summary>
    [DataContract]
    public sealed class RegionElement : SharpElement
    {
        /// <summary>
        /// ����
        /// </summary>
        public RegionElement()
        {
            this.ItemFamily = CodeItemFamily.Region;
        }

        /// <summary>
        ///     �Ƿ����
        /// </summary>
        [DataMember]
        public bool IsEnd
        {
            get;
            set;
        }
    }
}
