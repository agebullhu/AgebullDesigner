using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置的设计节点
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ConfigDesignOption : NotificationObject
    {
        #region 设计

        /// <summary>
        ///     当前实例
        /// </summary>
        public SolutionConfig Solution => SolutionConfig.Current;

        /// <summary>
        ///     当前类型
        /// </summary>
        public string Type => Config.GetType().Name;

        /// <summary>
        /// 对应的配置
        /// </summary>
        public ConfigBase Config { get; internal set; }

        /// <summary>
        ///     是否基于ParentConfigBase
        /// </summary>
        public bool IsParent => Config.GetType().IsSubclassOf(typeof(ParentConfigBase));

        private bool _isSelect;

        /// <summary>
        ///     是否选中
        /// </summary>
        public bool IsSelect
        {
            get => _isSelect;
            set
            {
                if (_isSelect == value)
                    return;
                BeforePropertyChanged(nameof(IsSelect), _isSelect, value);
                _isSelect = value;
                OnPropertyChanged(nameof(IsSelect));
            }
        }

        #endregion

        #region 设计标识


        /// <summary>
        /// 标识
        /// </summary>
        [DataMember, JsonProperty("_key", NullValueHandling = NullValueHandling.Ignore)]
        private Guid _key;

        /// <summary>
        /// 标识
        /// </summary>
        /// <remark>
        /// 名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计标识"), DisplayName("标识"), Description("名称")]
        public Guid Key
        {
            get => _key == Guid.Empty ? (_key = Guid.NewGuid()) : _key;
            set
            {
                if (_key == value)
                    return;
                BeforePropertyChanged(nameof(Key), _key, value);
                _key = value;
                OnPropertyChanged(nameof(Key));
            }
        }

        /// <summary>
        /// 唯一标识
        /// </summary>
        [DataMember, JsonProperty("Identity", NullValueHandling = NullValueHandling.Ignore)] private int _identity;

        /// <summary>
        /// 唯一标识
        /// </summary>
        /// <remark>
        /// 唯一标识
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计标识"), DisplayName("唯一标识"), Description("唯一标识")]
        public int Identity
        {
            get => _identity;
            set
            {
                if (_identity == value)
                    return;
                BeforePropertyChanged(nameof(Identity), _identity, value);
                _identity = value;
                OnPropertyChanged(nameof(Identity));
            }
        }

        /// <summary>
        /// 序号
        /// </summary>
        [DataMember, JsonProperty("Index", NullValueHandling = NullValueHandling.Ignore)] private int _index;

        /// <summary>
        /// 序号
        /// </summary>
        /// <remark>
        /// 序号
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计标识"), DisplayName("序号"), Description("序号")]
        public int Index
        {
            get => _index;
            set
            {
                if (_index == value)
                    return;
                BeforePropertyChanged(nameof(Index), _index, value);
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        #endregion

        #region 引用对象


        /// <summary>
        /// 引用标签
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public ConfigBase LastConfig => ReferenceConfig ?? Config;


        /// <summary>
        /// 引用对象键
        /// </summary>
        [DataMember, JsonProperty("referenceKey", NullValueHandling = NullValueHandling.Ignore)]
        private Guid _referenceKey;

        /// <summary>
        /// 引用对象键
        /// </summary>
        /// <remark>
        /// 引用对象键
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("引用"), DisplayName("引用对象键"), Description("引用对象键，指内部对象的引用")]
        public Guid ReferenceKey
        {
            get => _referenceKey;
            set
            {
                if (_referenceKey == value)
                    return;
                BeforePropertyChanged(nameof(ReferenceKey), _referenceKey, value);
                _referenceKey = value;
                OnPropertyChanged(nameof(ReferenceKey));
            }
        }


        /// <summary>
        /// 引用对象
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private ConfigBase _referenceConfig;

        /// <summary>
        /// 引用对象
        /// </summary>
        /// <remark>
        /// 引用对象
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("引用"), DisplayName("引用对象"), Description("引用对象，指内部对象的引用")]
        public ConfigBase ReferenceConfig
        {
            get => _referenceConfig ?? (_referenceConfig =
                       _referenceKey == Guid.Empty
                           ? null
                           : GlobalConfig.GetConfig(_referenceKey));
            set
            {
                if (_referenceConfig == value)
                    return;
                BeforePropertyChanged(nameof(ReferenceConfig), _referenceConfig, value);
                _referenceConfig = value;
                ReferenceKey = value?.Option.Key ?? Guid.Empty;
                OnPropertyChanged(nameof(ReferenceConfig));
            }
        }
        /// <summary>
        /// 引用标签
        /// </summary>
        [DataMember, JsonProperty("referenceTag", NullValueHandling = NullValueHandling.Ignore)] private string _referenceTag;

        /// <summary>
        /// 引用标签
        /// </summary>
        /// <remark>
        /// 引用标签
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("引用"), DisplayName("引用标签"), Description("引用标签")]
        public string ReferenceTag
        {
            get => _referenceTag;
            set
            {
                if (_referenceTag == value)
                    return;
                BeforePropertyChanged(nameof(ReferenceTag), _referenceTag, value);
                _referenceTag = value;
                OnPropertyChanged(nameof(ReferenceTag));
            }
        }
        #endregion
        #region 状态

        /// <summary>
        ///     状态
        /// </summary>
        [DataMember, JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        private ConfigStateType _state;

        /// <summary>
        /// 是否预定义对象
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false), ReadOnly(true)]
        public bool IsPredefined
        {
            get => _state.HasFlag(ConfigStateType.Predefined);
            set
            {
                BeforePropertyChanged(nameof(IsPredefined), IsPredefined, value);
                if (value)
                    _state |= ConfigStateType.Predefined;
                else
                    _state &= ~ConfigStateType.Predefined;
                OnPropertyChanged(nameof(IsPredefined));
            }
        }

        /// <summary>
        /// 是否参照对象
        /// </summary>
        /// <remark>
        /// 是否参照对象，是则永远只读
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("是否参照对象"), Description("是否参照对象，是则永远只读")]
        public bool IsReference
        {
            get => _state.HasFlag(ConfigStateType.Reference);
            set
            {
                BeforePropertyChanged(nameof(IsReference), IsReference, value);
                if (value)
                    _state |= ConfigStateType.Reference;
                else
                    _state &= ~ConfigStateType.Reference;
                OnPropertyChanged(nameof(IsReference));
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        /// <remark>
        /// 废弃,参照对象,冻结,引用对象,预定义对象，保存后被设置只读
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        public bool IsReadonly => _state.HasFlags(ConfigStateType.Delete, ConfigStateType.Freeze, ConfigStateType.Reference, ConfigStateType.Discard, ConfigStateType.Predefined);


        /// <summary>
        /// 是否锁定
        /// </summary>
        /// <remark>
        /// 废弃,参照对象,冻结,引用对象,预定义对象，保存后被设置只读
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        public bool IsLock => _state.HasFlag(ConfigStateType.Lock);


        /// <summary>
        /// 是否需要立即锁定
        /// </summary>
        /// <remark>
        /// 废弃,参照对象,冻结,引用对象,预定义对象，保存后被设置锁定
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        public bool CanLock => _state.HasFlags(ConfigStateType.Delete, ConfigStateType.Freeze, ConfigStateType.Reference, ConfigStateType.Discard, ConfigStateType.Predefined);

        /// <summary>
        /// 锁定对象
        /// </summary>
        public void LockConfig()
        {
            _state |= ConfigStateType.Lock;
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(CanLock));
        }

        /// <summary>
        /// 重置状态
        /// </summary>
        public void ResetState()
        {
            bool pred = IsPredefined;
            _state = ConfigStateType.None;
            IsPredefined = pred;
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsDiscard));
            OnPropertyChanged(nameof(IsFreeze));
            OnPropertyChanged(nameof(IsDelete));
            OnPropertyChanged(nameof(CanLock));
            OnPropertyChanged(nameof(IsReadonly));
            OnPropertyChanged(nameof(IsReference));
            OnPropertyChanged(nameof(IsPredefined));
        }
        /// <summary>
        /// 是否正常
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("是否正常"), Description("是否正常")]
        public bool IsNormal => !_state.HasFlags(ConfigStateType.Discard, ConfigStateType.Delete);

        /// <summary>
        /// 废弃
        /// </summary>
        /// <remark>
        /// 废弃
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("废弃"), Description("废弃")]
        public bool IsDiscard
        {
            get => _state.HasFlag(ConfigStateType.Discard);
            set
            {
                BeforePropertyChanged(nameof(IsDiscard), IsDiscard, value);
                if (value)
                    _state |= ConfigStateType.Discard;
                else
                    _state &= ~ConfigStateType.Discard;
                OnPropertyChanged(nameof(IsDiscard));
            }
        }

        /// <summary>
        /// 冻结
        /// </summary>
        /// <remark>
        /// 如为真,此配置的更改将不生成代码
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("冻结"), Description("如为真,此配置的更改将不生成代码")]
        public bool IsFreeze
        {
            get => _state.HasFlag(ConfigStateType.Freeze);
            set
            {
                BeforePropertyChanged(nameof(IsFreeze), IsFreeze, value);
                if (value)
                    _state |= ConfigStateType.Freeze;
                else
                    _state &= ~ConfigStateType.Freeze;
                OnPropertyChanged(nameof(IsFreeze));
            }
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <remark>
        /// 如为真,保存时删除
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("标记删除"), Description("如为真,保存时删除")]
        public bool IsDelete
        {
            get => _state.HasFlag(ConfigStateType.Delete);
            set
            {
                BeforePropertyChanged(nameof(IsDelete), IsDelete, value);
                if (value)
                    _state |= ConfigStateType.Delete;
                else
                    _state &= ~ConfigStateType.Delete;
                OnPropertyChanged(nameof(IsDelete));
            }
        }
        #endregion 

        #region 扩展

        /// <summary>
        /// 曾用名
        /// </summary>
        [DataMember, JsonProperty("oldNames", NullValueHandling = NullValueHandling.Ignore)] private List<string> _oldNames;

        /// <summary>
        /// 曾用名
        /// </summary>
        /// <remark>
        /// 曾用名
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("运行时"), DisplayName("曾用名"), Description("曾用名")]
        public List<string> OldNames
        {
            get => _oldNames ?? (_oldNames = new List<string>());
            set
            {
                if (_oldNames == value)
                    return;
                BeforePropertyChanged(nameof(OldNames), _oldNames, value);
                _oldNames = value;
                OnPropertyChanged(nameof(OldNames));
            }
        }

        /// <summary>
        ///     曾用名
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [ReadOnly(true)]
        [Category("设计器支持")]
        [DisplayName(@"曾用名")]
        [Description("曾用名")]
        public string NameHistory => OldNames.LinkToString(",");

        #endregion

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public void Copy(ConfigDesignOption cfg)
        {
            Index = cfg.Index;//序号
            ReferenceKey = cfg.ReferenceKey;//引用对象键
            ReferenceConfig = cfg.ReferenceConfig;//引用对象
            ReferenceTag = cfg.ReferenceTag;//引用标签
            _state = cfg._state;//状态
        }
    }
}