using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 字段规则配置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
    public partial class FieldRuleConfig : FieldExtendConfig, IFieldRuleConfig
    {
        #region 字段

        /// <summary>
        /// 规则说明
        /// </summary>
        [DataMember, JsonProperty("dataRuleDesc", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _dataRuleDesc;

        /// <summary>
        /// 规则说明
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"规则说明"), Description(@"规则说明")]
        public string DataRuleDesc
        {
            get => _dataRuleDesc;
            set
            {
                if (_dataRuleDesc == value)
                    return;
                BeforePropertyChanged(nameof(DataRuleDesc), _dataRuleDesc, value);
                _dataRuleDesc = value;
                OnPropertyChanged(nameof(DataRuleDesc));
            }
        }

        /// <summary>
        /// 规则说明
        /// </summary>
        [DataMember, JsonProperty("autoDataRuleDesc", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _autoDataRuleDesc;

        /// <summary>
        /// 规则说明
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"规则说明"), Description(@"规则说明")]
        public string AutoDataRuleDesc
        {
            get => _autoDataRuleDesc;
            set
            {
                if (_autoDataRuleDesc == value)
                    return;
                BeforePropertyChanged(nameof(AutoDataRuleDesc), _autoDataRuleDesc, value);
                _autoDataRuleDesc = value;
                OnPropertyChanged(nameof(AutoDataRuleDesc));
            }
        }

        /// <summary>
        /// 校验代码
        /// </summary>/// <remarks>
        /// 校验代码,本字段用{0}代替
        /// </remarks>
        [DataMember, JsonProperty("validateCode", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _validateCode;

        /// <summary>
        /// 校验代码
        /// </summary>/// <remarks>
        /// 校验代码,本字段用{0}代替
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"校验代码"), Description(@"校验代码,本字段用{0}代替")]
        public string ValidateCode
        {
            get => _validateCode;
            set
            {
                if (_validateCode == value)
                    return;
                BeforePropertyChanged(nameof(ValidateCode), _validateCode, value);
                _validateCode = value;
                OnPropertyChanged(nameof(ValidateCode));
            }
        }

        /// <summary>
        /// 能否为空
        /// </summary>/// <remarks>
        /// 这是数据相关的逻辑,表示在存储时必须写入数据,否则逻辑不正确
        /// </remarks>
        [DataMember, JsonProperty("canEmpty", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _canEmpty;

        /// <summary>
        /// 能否为空
        /// </summary>/// <remarks>
        /// 这是数据相关的逻辑,表示在存储时必须写入数据,否则逻辑不正确
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"能否为空"), Description(@"这是数据相关的逻辑,表示在存储时必须写入数据,否则逻辑不正确")]
        public bool CanEmpty
        {
            get => _canEmpty;
            set
            {
                if (_canEmpty == value)
                    return;
                BeforePropertyChanged(nameof(CanEmpty), _canEmpty, value);
                _canEmpty = value;
                OnPropertyChanged(nameof(CanEmpty));
            }
        }

        /// <summary>
        /// 必填字段
        /// </summary>
        [DataMember, JsonProperty("isRequired", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isRequired;

        /// <summary>
        /// 必填字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"必填字段"), Description(@"必填字段")]
        public bool IsRequired
        {
            get => _isRequired;
            set
            {
                if (_isRequired == value)
                    return;
                BeforePropertyChanged(nameof(IsRequired), _isRequired, value);
                _isRequired = value;
                OnPropertyChanged(nameof(IsRequired));
            }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        [DataMember, JsonProperty("max", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _max;

        /// <summary>
        /// 最大值
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"最大值"), Description(@"最大值")]
        public string Max
        {
            get => _max;
            set
            {
                if (_max == value)
                    return;
                BeforePropertyChanged(nameof(Max), _max, value);
                _max = value;
                OnPropertyChanged(nameof(Max));
            }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        [DataMember, JsonProperty("min", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _min;

        /// <summary>
        /// 最小值
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"最小值"), Description(@"最小值")]
        public string Min
        {
            get => _min;
            set
            {
                if (_min == value)
                    return;
                BeforePropertyChanged(nameof(Min), _min, value);
                _min = value;
                OnPropertyChanged(nameof(Min));
            }
        }
        #endregion 字段

        #region 兼容性升级

        /// <summary>
        /// 兼容性升级
        /// </summary>
        public void Upgrade()
        {
            //Copy(Entity);
        }

        #endregion

        #region 字段复制

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(IFieldRuleConfig dest)
        {
            CopyFrom((SimpleConfig)dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is IFieldRuleConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(IFieldRuleConfig dest)
        {
            DataRuleDesc = dest.DataRuleDesc;
            AutoDataRuleDesc = dest.AutoDataRuleDesc;
            ValidateCode = dest.ValidateCode;
            CanEmpty = dest.CanEmpty;
            IsRequired = dest.IsRequired;
            Max = dest.Max;
            Min = dest.Min;
        }
        #endregion 字段复制
    }
}

