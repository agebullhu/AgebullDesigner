using System.Runtime.Serialization;
using System.Text;

namespace Agebull.EntityModel.RobotCoder.CodeTemplate
{
    public class AnalyzeUnitBase
    {
        #region ����


        /// <summary>
        ///     ����(������)
        /// </summary>
        [DataMember]
        public CodeItemRace ItemRace { get; set; }

        /// <summary>
        ///     �����������
        /// </summary>
        [DataMember]
        public CodeItemFamily ItemFamily { get; set; }

        /// <summary>
        ///     ��Ԫ����
        /// </summary>
        [DataMember]
        public CodeItemType ItemType { get; set; }

        public void SetRace(CodeItemRace race = CodeItemRace.None, CodeItemFamily family = CodeItemFamily.None, CodeItemType type = CodeItemType.None)
        {
            ItemRace = race;
            ItemFamily = family;
            ItemType = type;
        }
        #endregion

        #region �ĵ�λ��

        public virtual string Word { get; set; }

        public int Level { get; set; } = -1;

        /// <summary>
        ///     �ļ��е��к�
        /// </summary>
        [DataMember]
        public int Line { get; set; } = -1;

        /// <summary>
        ///     ���е��к�
        /// </summary>
        [DataMember]
        public int Column { get; set; } = -1;
        
        /// <summary>
        ///     �ļ��е���ʼλ��
        /// </summary>
        [DataMember]
        public int Start { get; set; } = -1;

        /// <summary>
        ///     �ļ��еĽ���λ��
        /// </summary>
        [DataMember]
        public int End { get; set; } = -1;

        /// <summary>
        ///     �ı�����
        /// </summary>
        [IgnoreDataMember]
        public int Lenght => Start < 0 || End < 0 ? 0 : End - Start + 1;

        #endregion

        #region ����

        /// <summary>
        /// �ϼ��ڵ�
        /// </summary>
        public AnalyzeBlock Parent { get; set; }

        /// <summary>
        ///     �Ƿ�Ϊ��
        /// </summary>
        public virtual bool IsEmpty { get; } = false;

        /// <summary>
        ///     �Ƿ����
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        ///     �Ƿ�ȫ�ڵ�
        /// </summary>
        [DataMember]
        public bool IsReplenish { get; set; }

        /// <summary>
        ///     �Ƿ��
        /// </summary>
        public bool IsBlock { get; set; }

        /// <summary>
        ///     �Ƿ�����
        /// </summary>
        public bool IsLock { get; set; }

        /// <summary>
        ///     �Ƿ�����
        /// </summary>
        public bool IsContent { get; set; }

        /// <summary>
        ///     ��������
        /// </summary>
        public TemplateUnitType UnitType { get; set; }
        #endregion

        #region ������չ

        /// <summary>
        ///     ֵ��������
        /// </summary>
        public AnalyzeUnitBase ValueLink { get; set; }


        /// <summary>
        ///     ֵ����
        /// </summary>
        public LuaDataTypeItem ValueType
        {
            get; set;
        }

        /// <summary>
        ///     ����
        /// </summary>
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// ���ı�
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Word;
        }
        /// <summary>
        /// �Ƿ������Ԫ
        /// </summary>
        public virtual bool IsUnit { get; set; }
        #endregion

        #region ��չ

        /// <summary>
        ///     �Ƿ���Ҫ���
        /// </summary>
        [DataMember]
        public virtual bool NeedSentence { get; set; }

        /// <summary>
        ///     �Ƿ���Ե��ɵ���
        /// </summary>
        [DataMember]
        public virtual bool IsWord { get; set; }

        /// <summary>
        ///     ��ϵȼ�
        /// </summary>
        [DataMember]
        public int JoinLevel { get; set; } = -1;

        /// <summary>
        ///     �������
        /// </summary>
        [DataMember]
        public JoinFeature JoinFeature { get; set; }

        /// <summary>
        ///     ��Ϻ������
        /// </summary>
        [DataMember]
        public JoinFeature JoinType { get; set; }

        /// <summary>
        /// ���ı�
        /// </summary>
        /// <returns></returns>
        public virtual string ToCode()
        {
            return Word;
        }

        /// <summary>
        /// ���ı�
        /// </summary>
        /// <returns></returns>
        public virtual void ToContent(StringBuilder builder)
        {
            builder.Append(Word);
        }
        
        #endregion
    }
}