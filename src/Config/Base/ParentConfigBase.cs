using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     ���û���
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class ParentConfigBase : FileConfigBase
    {
        /// <summary>
        /// �����Ӽ�
        /// </summary>
        public abstract void ForeachChild(Action<ConfigBase> action);


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
            get => _abbreviation;
            set
            {
                if (_abbreviation == value)
                    return;
                BeforePropertyChanged(nameof(Abbreviation), _abbreviation, value);
                _abbreviation = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
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
            get => _isGlobal;
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