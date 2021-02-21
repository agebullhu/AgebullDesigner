using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     ��ʾ���Զ�����һ�����ö���
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class IndependenceConfigBase : FileConfigBase
    {
        /// <summary>
        /// ���
        /// </summary>
        [DataMember, JsonProperty("_abbreviation", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _abbreviation;

        /// <summary>
        /// ���
        /// </summary>
        /// <remark>
        /// ���
        /// </remark>
        [JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"���"), Description(@"���")]
        public string Abbreviation
        {
            get => WorkContext.InCoderGenerating ? _abbreviation ?? Name.ToLWord() : _abbreviation;
            set
            {
                if (_abbreviation == value)
                    return;
                BeforePropertyChange(nameof(Abbreviation), _abbreviation, value);
                _abbreviation = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Abbreviation));
            }
        }
        /// <summary>
        /// ȫ����Ŀ
        /// </summary>
        [DataMember, JsonProperty("IsGlobal", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isGlobal;

        /// <summary>
        /// ȫ����Ŀ
        /// </summary>
        /// <remark>
        /// ȫ����Ŀ,����Ϊ�����֧�ֵĻ�����Ŀ
        /// </remark>
        [JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"ȫ����Ŀ"), Description("ȫ����Ŀ,����Ϊ�����֧�ֵĻ�����Ŀ")]
        public bool IsGlobal
        {
            get => _isGlobal;
            set
            {
                if (_isGlobal == value)
                    return;
                BeforePropertyChange(nameof(IsGlobal), _isGlobal, value);
                _isGlobal = value;
                OnPropertyChanged(nameof(IsGlobal));
            }
        }

        /// <summary>
        /// ����ֶα�ʶ��
        /// </summary>
        [DataMember, JsonProperty("MaxIdentity", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _maxIdentity;

        /// <summary>
        /// ����ֶα�ʶ��
        /// </summary>
        /// <remark>
        /// ����ֶα�ʶ��
        /// </remark>
        [JsonIgnore]
        [Category(@"ϵͳ"), DisplayName(@"����ֶα�ʶ��"), Description("����ֶα�ʶ��")]
        public int MaxIdentity
        {
            get => _maxIdentity;
            set
            {
                if (_maxIdentity == value)
                    return;
                BeforePropertyChange(nameof(MaxIdentity), _maxIdentity, value);
                _maxIdentity = value;
                OnPropertyChanged(nameof(MaxIdentity));
            }
        }
    }
}