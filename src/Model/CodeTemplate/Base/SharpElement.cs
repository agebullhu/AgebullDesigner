// /*****************************************************
// (c)2008-2013 Copy right www.Gboxt.com
// ����:bull2
// ����:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// ����:2014-10-23
// �޸�:2014-11-08
// *****************************************************/

#region ����

using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Agebull.CodeRefactor.CodeAnalyze
{
    /// <summary>
    ///     ��ʾ#�Ľڵ�
    /// </summary>
    [DataContract]
    public class SharpElement : MulitCodeElement
    {
        /// <summary>
        /// ����
        /// </summary>
        public SharpElement()
        {
            this.Skip = true;
            this.ItemRace = CodeItemRace.Assist;
            this.ItemType = CodeItemType.Sharp;
            this.ItemFamily = CodeItemFamily.Sharp;
        }

        /// <summary>
        ///     �Ƿ�����ɷ���
        /// </summary>
        public override AnalyzerState State
        {
            get
            {
                return AnalyzerState.All;
            }
        }

        /// <summary>
        ///     ����
        /// </summary>
        [DataMember]
        public string Name
        {
            get;
            set;
        }
    }
}
