using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     ����
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public sealed partial class EntityReleationConfig : ConfigBase
    {

        /// <summary>
        ///     ����
        /// </summary>
        [IgnoreDataMember,JsonIgnore, Browsable(false)]
        public string DisplayName => $"{Caption}({Name})";


        /// <summary>
        ///     �ϼ�
        /// </summary>
        [IgnoreDataMember,JsonIgnore, Browsable(false)]
        public EntityConfig Parent { get; set; }


        #region 

        /// <summary>
        /// ��������������
        /// </summary>
        [DataMember, JsonProperty("ForeignKey", NullValueHandling = NullValueHandling.Ignore)]
        internal string _foreignKey;

        /// <summary>
        /// ��������������
        /// </summary>
        /// <remark>
        /// ��������������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("��������������"), Description("��������������")]
        public string ForeignKey
        {
            get => _foreignKey;
            set
            {
                if (_foreignKey == value)
                    return;
                BeforePropertyChanged(nameof(ForeignKey), _foreignKey, value);
                _foreignKey = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ForeignKey));
            }
        }

        /// <summary>
        /// �������������Ӧ���ֶ�����
        /// </summary>
        [DataMember, JsonProperty("PrimaryKey", NullValueHandling = NullValueHandling.Ignore)]
        internal string _primaryKey;

        /// <summary>
        /// �������������Ӧ���ֶ�����
        /// </summary>
        /// <remark>
        /// �ϼ���������ǰ��������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("�������������Ӧ���ֶ�����"), Description("�ϼ���������ǰ��������")]
        public string PrimaryKey
        {
            get => _primaryKey;
            set
            {
                if (_primaryKey == value)
                    return;
                BeforePropertyChanged(nameof(PrimaryKey), _primaryKey, value);
                _primaryKey = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(PrimaryKey));
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        [DataMember, JsonProperty("Friend", NullValueHandling = NullValueHandling.Ignore)]
        internal string _friend;

        /// <summary>
        /// ������
        /// </summary>
        /// <remark>
        /// ������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("������"), Description("������")]
        public string Friend
        {
            get => _friend;
            set
            {
                if (_friend == value)
                    return;
                BeforePropertyChanged(nameof(Friend), _friend, value);
                _friend = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Friend));
            }
        }

        private const string Releation_Description = @"0[��Friend��ƽ�ȹ�ϵ��1��1],1[���ڶ�Friend�����¼���ϵ��1��1],2[1�Զ�]";

        /// <summary>
        /// ��ϵ����
        /// </summary>
        [DataMember, JsonProperty("Releation", NullValueHandling = NullValueHandling.Ignore)]
        internal int _releation;

        /// <summary>
        /// ��ϵ����
        /// </summary>
        /// <remark>
        /// 0[��Friend��ƽ�ȹ�ϵ��1��1],1[���ڶ�Friend�����¼���ϵ��1��1],2[1�Զ�]
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("��ϵ����"), Description(Releation_Description)]
        public int Releation
        {
            get => _releation;
            set
            {
                if (_releation == value)
                    return;
                BeforePropertyChanged(nameof(Releation), _releation, value);
                _releation = value;
                OnPropertyChanged(nameof(Releation));
            }
        }

        /// <summary>
        /// ��չ����
        /// </summary>
        [DataMember, JsonProperty("Condition", NullValueHandling = NullValueHandling.Ignore)]
        internal string _condition;

        /// <summary>
        /// ��չ����
        /// </summary>
        /// <remark>
        /// ��չ����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("��չ����"), Description("��չ����")]
        public string Condition
        {
            get => _condition;
            set
            {
                if (_condition == value)
                    return;
                BeforePropertyChanged(nameof(Condition), _condition, value);
                _condition = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Condition));
            }
        }
        #endregion
    }
}