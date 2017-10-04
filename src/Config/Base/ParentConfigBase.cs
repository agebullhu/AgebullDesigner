using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    ///     ���û���
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class ParentConfigBase : FileConfigBase
    {
        /// <summary>
        /// �Ӽ�
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public abstract IEnumerable<ConfigBase> MyChilds { get; }


        /// <summary>
        /// ���
        /// </summary>
        [DataMember, JsonProperty("_abbreviation", NullValueHandling = NullValueHandling.Ignore)]
        private string _abbreviation;

        /// <summary>
        /// ���
        /// </summary>
        /// <remark>
        /// ���
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"���"), Description(@"���")]
        public string Abbreviation
        {
            get
            {
                return _abbreviation;
            }
            set
            {
                if (_abbreviation == value)
                    return;
                BeforePropertyChanged(nameof(Abbreviation), _abbreviation, value);
                _abbreviation = value;
                OnPropertyChanged(nameof(Abbreviation));
            }
        }
        /// <summary>
        /// ȫ����Ŀ
        /// </summary>
        [DataMember, JsonProperty("IsGlobal", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isGlobal;

        /// <summary>
        /// ȫ����Ŀ
        /// </summary>
        /// <remark>
        /// ȫ����Ŀ,����Ϊ�����֧�ֵĻ�����Ŀ
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"�����֧��"), DisplayName(@"ȫ����Ŀ"), Description("ȫ����Ŀ,����Ϊ�����֧�ֵĻ�����Ŀ")]
        public bool IsGlobal
        {
            get
            {
                return _isGlobal;
            }
            set
            {
                if (_isGlobal == value)
                    return;
                BeforePropertyChanged(nameof(IsGlobal), _isGlobal, value);
                _isGlobal = value;
                OnPropertyChanged(nameof(IsGlobal));
            }
        }
    }
}