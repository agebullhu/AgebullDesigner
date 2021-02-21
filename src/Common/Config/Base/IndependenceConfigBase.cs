using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     表示可以独立的一个配置对象
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class IndependenceConfigBase : FileConfigBase
    {
        /// <summary>
        /// 简称
        /// </summary>
        [DataMember, JsonProperty("_abbreviation", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _abbreviation;

        /// <summary>
        /// 简称
        /// </summary>
        /// <remark>
        /// 简称
        /// </remark>
        [JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"简称"), Description(@"简称")]
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
        /// 全局项目
        /// </summary>
        [DataMember, JsonProperty("IsGlobal", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isGlobal;

        /// <summary>
        /// 全局项目
        /// </summary>
        /// <remark>
        /// 全局项目,是作为设计器支持的基础项目
        /// </remark>
        [JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"全局项目"), Description("全局项目,是作为设计器支持的基础项目")]
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
        /// 最大字段标识号
        /// </summary>
        [DataMember, JsonProperty("MaxIdentity", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _maxIdentity;

        /// <summary>
        /// 最大字段标识号
        /// </summary>
        /// <remark>
        /// 最大字段标识号
        /// </remark>
        [JsonIgnore]
        [Category(@"系统"), DisplayName(@"最大字段标识号"), Description("最大字段标识号")]
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