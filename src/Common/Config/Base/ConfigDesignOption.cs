using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置的设计节点
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ConfigDesignOption : NotificationObject,IKey
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
        [DataMember, JsonProperty("_key", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _key;

        /// <summary>
        /// 标识
        /// </summary>
        /// <remark>
        /// 名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计标识"), DisplayName("标识"), Description("名称")]
        public string Key
        {
            get => _key ??= Guid.NewGuid().ToString("N");
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
        [DataMember, JsonProperty("Identity", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)] private int _identity;

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
        [DataMember, JsonProperty("Index", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)] private int _index;

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
        /// 引用对象键
        /// </summary>
        [DataMember, JsonProperty("referenceKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _referenceKey;

        /// <summary>
        /// 引用对象键
        /// </summary>
        /// <remark>
        /// 引用对象键
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("引用"), DisplayName("引用对象键"), Description("引用对象键，指内部对象的引用")]
        public string ReferenceKey
        {
            get => _referenceKey == _key ? null : _referenceKey;
            set
            {
                if (_referenceKey == value)
                    return;
                if (_referenceKey == _key)
                    _referenceKey = null;
                BeforePropertyChanged(nameof(ReferenceKey), _referenceKey, value);
                _referenceKey = value;
                if (_referenceKey == null)
                {
                    _state &= ~ConfigStateType.Reference;
                    _state &= ~ConfigStateType.Link;
                }
                else if (!_state.AnyFlags(ConfigStateType.Reference, ConfigStateType.Link))
                {
                    _state |= ConfigStateType.Reference;
                }
                OnPropertyChanged(nameof(Link));
                OnPropertyChanged(nameof(IsLink));
                OnPropertyChanged(nameof(Reference));
                OnPropertyChanged(nameof(IsReference));
                OnPropertyChanged(nameof(ReferenceKey));
                OnPropertyChanged(nameof(ReferenceConfig));
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
        /// 指内部对象的引用
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("引用"), DisplayName("引用对象"), Description("引用对象，指内部对象的引用")]
        public ConfigBase ReferenceConfig
        {
            get
            {
                if (_referenceKey == null)
                    return null;
                if (_referenceKey == _key || Config == _referenceConfig)
                {
                    _referenceKey = null;
                    _referenceConfig = null;
                    return null;
                }

                if (_referenceConfig != null)
                    return _referenceConfig;
                _referenceConfig = GlobalConfig.GetConfig(_referenceKey);

                if (_referenceConfig == null || Config == _referenceConfig)
                {
                    _referenceKey = null;
                    _referenceConfig = null;
                }
                return _referenceConfig;
            }
            set
            {
                if (_referenceConfig == value)
                    return;
                if (value != null && (value == Config || value.Key == Key))
                    value = null;
                _referenceConfig = value;
                _referenceKey = value?.Option.Key ?? null;
                if (value == null)
                {
                    _state &= ~ConfigStateType.Reference;
                    _state &= ~ConfigStateType.Link;
                }
                else if (!_state.AnyFlags(ConfigStateType.Reference, ConfigStateType.Link))
                {
                    _state |= ConfigStateType.Reference;
                }
                BeforePropertyChanged(nameof(ReferenceConfig), _referenceConfig, value);
                OnPropertyChanged(nameof(Link));
                OnPropertyChanged(nameof(IsLink));
                OnPropertyChanged(nameof(Reference));
                OnPropertyChanged(nameof(IsReference));
                OnPropertyChanged(nameof(ReferenceKey));
                OnPropertyChanged(nameof(ReferenceConfig));
            }
        }
        /// <summary>
        /// 引用对象(接口)
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public ConfigBase Reference => IsReference ? ReferenceConfig : null;

        /// <summary>
        /// 链接对象(外连接)
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public ConfigBase Link => IsLink ? ReferenceConfig : null;

        /// <summary>
        /// 引用标签
        /// </summary>
        [DataMember, JsonProperty("referenceTag", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _referenceTag;

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
        [DataMember, JsonProperty("state", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
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
        /// 是否关联对象
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("是否关联对象"), Description("是否关联对象")]
        public bool IsLink
        {
            get => _state.HasFlag(ConfigStateType.Link);
            set
            {
                BeforePropertyChanged(nameof(IsLink), IsLink, value);
                if (value)
                {
                    _state &= ~ConfigStateType.Reference;
                    _state |= ConfigStateType.Link;
                }
                else
                {
                    _state &= ~ConfigStateType.Link;
                }
                OnPropertyChanged(nameof(IsReference));
                OnPropertyChanged(nameof(IsLink));
            }
        }

        /// <summary>
        /// 是否引用对象
        /// </summary>
        /// <remark>
        /// 是否引用对象，是则永远只读
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("是否引用对象"), Description("是否引用对象")]
        public bool IsReference
        {
            get => _state.HasFlag(ConfigStateType.Reference);
            set
            {
                BeforePropertyChanged(nameof(IsReference), IsReference, value);
                if (value)
                {
                    _state &= ~ConfigStateType.Link;
                    _state |= ConfigStateType.Reference;
                }
                else
                {
                    _state &= ~ConfigStateType.Reference;
                }
                OnPropertyChanged(nameof(IsReference));
                OnPropertyChanged(nameof(IsLink));
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
            OnPropertyChanged(nameof(IsNormal));
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
                OnPropertyChanged(nameof(IsNormal));
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
                OnPropertyChanged(nameof(IsNormal));
            }
        }
        #endregion 

        #region 扩展

        /// <summary>
        /// 曾用名
        /// </summary>
        [DataMember, JsonProperty("oldNames", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)] private List<string> _oldNames;

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
            get => _oldNames ??= new List<string>();
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

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="full"></param>
        /// <returns></returns>
        public void Copy(ConfigDesignOption cfg, bool full)
        {
            if (full)
            {
                Index = cfg.Index; //序号
                ReferenceTag = cfg.ReferenceTag; //引用标签
            }

            _state = cfg._state; //状态
            ReferenceKey = cfg.ReferenceKey; //引用对象键
            ReferenceConfig = cfg.ReferenceConfig; //引用对象
            if (cfg._extendConfig != null)
            {
                ExtendConfig.Clear();
                ExtendConfig.AddRange(cfg._extendConfig);
            }

            if (cfg._extend == null)
                return;
            foreach (var items in cfg._extend)
            {
                if (!Extend.TryGetValue(items.Key, out var fItems))
                {
                    Extend.Add(items.Key, items.Value.ToDictionary(p => p.Key, p => p.Value));
                }
                else
                {
                    foreach (var item in fItems)
                    {
                        if (!items.Value.ContainsKey(item.Key))
                            items.Value.Add(item.Key, item.Value);
                        else
                            items.Value[item.Key] = item.Value;
                    }
                }
            }
        }

        #endregion

        #region 扩展配置

        /// <summary>
        /// 扩展配置
        /// </summary>
        [DataMember, JsonProperty("extend", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, Dictionary<string, string>> _extend;


        /// <summary>
        /// 扩展配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持")]
        [DisplayName("扩展配置")]
        public Dictionary<string, Dictionary<string, string>> Extend
        {
            get
            {
                if (_extend != null)
                    return _extend;
                _extend = new Dictionary<string, Dictionary<string, string>>();
                BeforePropertyChanged(nameof(Extend), null, _extend);
                return _extend;
            }
        }

        [IgnoreDataMember, JsonIgnore]
        private ConfigItemDictionary _extendDictionary;

        /// <summary>
        /// 扩展配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigItemDictionary ExtendDictionary => _extendDictionary ??= new ConfigItemDictionary(Extend);

        [IgnoreDataMember, JsonIgnore]
        private ConfigItemListBool _extendBool;

        /// <summary>
        /// 扩展配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigItemListBool ExtendConfigListBool => _extendBool ??= new ConfigItemListBool(ExtendDictionary);

        /// <summary>
        /// 扩展配置
        /// </summary>
        [DataMember, JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private NotificationList<ConfigItem> _extendConfig;


        /// <summary>
        /// 扩展配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持")]
        [DisplayName("扩展配置")]
        private NotificationList<ConfigItem> ExtendConfig
        {
            get
            {
                if (_extendConfig != null)
                    return _extendConfig;
                _extendConfig = new NotificationList<ConfigItem>();
                BeforePropertyChanged(nameof(ExtendConfig), null, _extendConfig);
                return _extendConfig;
            }
        }

        [IgnoreDataMember, JsonIgnore]
        private ConfigItemList _extendConfigList;
        /// <summary>
        /// 扩展配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigItemList ExtendConfigList => _extendConfigList ??= new ConfigItemList(ExtendConfig);

        /// <summary>
        /// 读写扩展配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get => TryGetExtendConfig(key, null);
            set
            {
                if (key == null)
                    return;
                var mv = ExtendConfig.FirstOrDefault(p => string.Equals(p.Name, key, StringComparison.OrdinalIgnoreCase));
                if (mv == null)
                {
                    ExtendConfig.Add(new ConfigItem { Name = key, Value = value });
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    ExtendConfig.Remove(mv);
                }
                else
                {
                    mv.Value = value.Trim();
                }
                RaisePropertyChanged(key);
            }
        }
        /// <summary>
        /// 读写扩展配置
        /// </summary>
        /// <param name="classify">分类</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string classify, string name]
        {
            get => ExtendDictionary[classify, name];
            set
            {
                ExtendDictionary[classify, name] = value;
                RaisePropertyChanged($"{classify}_{name}");
            }
        }

        /// <summary>
        /// 试图取得扩展配置,如果不存在或为空则加入默认值后返回
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="def">默认值</param>
        /// <returns>扩展配置</returns>

        public string TryGetExtendConfig(string key, string def)
        {
            if (key == null)
                return def;
            var mv = ExtendConfig.FirstOrDefault(p => string.Equals(p.Name, key, StringComparison.OrdinalIgnoreCase));
            if (mv != null)
                return mv.Value ??= def;
            ExtendConfig.Add(new ConfigItem { Name = key, Value = def });
            return def;
        }

        #endregion
    }
}