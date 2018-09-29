using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     ���õ���ƽڵ�
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ConfigDesignOption : NotificationObject
    {
        #region ���

        /// <summary>
        ///     ��ǰʵ��
        /// </summary>
        public SolutionConfig Solution => SolutionConfig.Current;

        /// <summary>
        ///     ��ǰ����
        /// </summary>
        public string Type => Config.GetType().Name;

        /// <summary>
        /// ��Ӧ������
        /// </summary>
        public ConfigBase Config { get; internal set; }

        /// <summary>
        ///     �Ƿ����ParentConfigBase
        /// </summary>
        public bool IsParent => Config.GetType().IsSubclassOf(typeof(ParentConfigBase));

        private bool _isSelect;

        /// <summary>
        ///     �Ƿ�ѡ��
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

        #region ��Ʊ�ʶ


        /// <summary>
        /// ��ʶ
        /// </summary>
        [DataMember, JsonProperty("_key", NullValueHandling = NullValueHandling.Ignore)]
        private Guid _key;

        /// <summary>
        /// ��ʶ
        /// </summary>
        /// <remark>
        /// ����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("��Ʊ�ʶ"), DisplayName("��ʶ"), Description("����")]
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
        /// Ψһ��ʶ
        /// </summary>
        [DataMember, JsonProperty("Identity", NullValueHandling = NullValueHandling.Ignore)] private int _identity;

        /// <summary>
        /// Ψһ��ʶ
        /// </summary>
        /// <remark>
        /// Ψһ��ʶ
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("��Ʊ�ʶ"), DisplayName("Ψһ��ʶ"), Description("Ψһ��ʶ")]
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
        /// ���
        /// </summary>
        [DataMember, JsonProperty("Index", NullValueHandling = NullValueHandling.Ignore)] private int _index;

        /// <summary>
        /// ���
        /// </summary>
        /// <remark>
        /// ���
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("��Ʊ�ʶ"), DisplayName("���"), Description("���")]
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

        #region ���ö���


        /// <summary>
        /// ���ö����
        /// </summary>
        [DataMember, JsonProperty("referenceKey", NullValueHandling = NullValueHandling.Ignore)]
        private Guid _referenceKey;

        /// <summary>
        /// ���ö����
        /// </summary>
        /// <remark>
        /// ���ö����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("����"), DisplayName("���ö����"), Description("���ö������ָ�ڲ����������")]
        public Guid ReferenceKey
        {
            get => _referenceKey == _key ? Guid.Empty : _referenceKey;
            set
            {
                if (_referenceKey == value)
                    return;
                if (_referenceKey == _key)
                    _referenceKey = Guid.Empty;
                BeforePropertyChanged(nameof(ReferenceKey), _referenceKey, value);
                _referenceKey = value;
                IsReference = value != Guid.Empty;
                OnPropertyChanged(nameof(ReferenceKey));
            }
        }

        /// <summary>
        /// ���ö���
        /// </summary>
        /// <remark>
        /// ���ö���
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        public ConfigBase Reference => IsReference ? ReferenceConfig : null;


        /// <summary>
        /// ���ö���
        /// </summary>
        /// <remark>
        /// ���ö���
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        public ConfigBase Link => IsLink ? ReferenceConfig : null;

        /// <summary>
        /// ���ö���
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private ConfigBase _referenceConfig;

        /// <summary>
        /// ���ö���
        /// </summary>
        /// <remark>
        /// ���ö���
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("����"), DisplayName("���ö���"), Description("���ö���ָ�ڲ����������")]
        public ConfigBase ReferenceConfig
        {
            get
            {
                if (_referenceKey == Guid.Empty || _referenceKey == _key)
                    return null;
                if (Config == _referenceConfig)
                {
                    ReferenceKey = Guid.Empty;
                    _referenceConfig = null;
                    return null;
                }
                return _referenceConfig ?? (_referenceConfig = GlobalConfig.GetConfig(_referenceKey));
            }
            set
            {
                if (_referenceConfig == value)
                    return;
                if (value == Config || value.Key == Key)
                    value = null;
                BeforePropertyChanged(nameof(ReferenceConfig), _referenceConfig, value);
                _referenceConfig = value;
                ReferenceKey = value?.Option.Key ?? Guid.Empty;
                if (value == null)
                {
                    IsLink = false;
                    IsReference = false;
                }
                else if (!IsReference)
                {
                    IsLink = true;
                }
                OnPropertyChanged(nameof(ReferenceConfig));
            }
        }
        /// <summary>
        /// ���ñ�ǩ
        /// </summary>
        [DataMember, JsonProperty("referenceTag", NullValueHandling = NullValueHandling.Ignore)] private string _referenceTag;

        /// <summary>
        /// ���ñ�ǩ
        /// </summary>
        /// <remark>
        /// ���ñ�ǩ
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("����"), DisplayName("���ñ�ǩ"), Description("���ñ�ǩ")]
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

        #region ״̬

        /// <summary>
        ///     ״̬
        /// </summary>
        [DataMember, JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        private ConfigStateType _state;

        /// <summary>
        /// �Ƿ�Ԥ�������
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
        /// �Ƿ��������
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("�Ƿ��������"), Description("�Ƿ��������")]
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
        /// �Ƿ����ö���
        /// </summary>
        /// <remark>
        /// �Ƿ����ö���������Զֻ��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("�Ƿ����ö���"), Description("�Ƿ����ö���")]
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
        /// �Ƿ�ֻ��
        /// </summary>
        /// <remark>
        /// ����,���ն���,����,���ö���,Ԥ������󣬱��������ֻ��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        public bool IsReadonly => _state.HasFlags(ConfigStateType.Delete, ConfigStateType.Freeze, ConfigStateType.Reference, ConfigStateType.Discard, ConfigStateType.Predefined);


        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <remark>
        /// ����,���ն���,����,���ö���,Ԥ������󣬱��������ֻ��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        public bool IsLock => _state.HasFlag(ConfigStateType.Lock);


        /// <summary>
        /// �Ƿ���Ҫ��������
        /// </summary>
        /// <remark>
        /// ����,���ն���,����,���ö���,Ԥ������󣬱������������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        public bool CanLock => _state.HasFlags(ConfigStateType.Delete, ConfigStateType.Freeze, ConfigStateType.Reference, ConfigStateType.Discard, ConfigStateType.Predefined);

        /// <summary>
        /// ��������
        /// </summary>
        public void LockConfig()
        {
            _state |= ConfigStateType.Lock;
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(CanLock));
        }

        /// <summary>
        /// ����״̬
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
        /// �Ƿ�����
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("�Ƿ�����"), Description("�Ƿ�����")]
        public bool IsNormal => !_state.HasFlags(ConfigStateType.Discard, ConfigStateType.Delete);

        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("����"), Description("����")]
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
        /// ����
        /// </summary>
        /// <remark>
        /// ��Ϊ��,�����õĸ��Ľ������ɴ���
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("����"), Description("��Ϊ��,�����õĸ��Ľ������ɴ���")]
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
        /// ���ɾ��
        /// </summary>
        /// <remark>
        /// ��Ϊ��,����ʱɾ��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("���ɾ��"), Description("��Ϊ��,����ʱɾ��")]
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

        #region ��չ

        /// <summary>
        /// ������
        /// </summary>
        [DataMember, JsonProperty("oldNames", NullValueHandling = NullValueHandling.Ignore)] private List<string> _oldNames;

        /// <summary>
        /// ������
        /// </summary>
        /// <remark>
        /// ������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("����ʱ"), DisplayName("������"), Description("������")]
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
        ///     ������
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [ReadOnly(true)]
        [Category("�����֧��")]
        [DisplayName(@"������")]
        [Description("������")]
        public string NameHistory => OldNames.LinkToString(",");

        /// <summary>
        /// �ֶθ���
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public void Copy(ConfigDesignOption cfg)
        {
            Index = cfg.Index;//���
            _state = cfg._state;//״̬
            ReferenceKey = cfg.ReferenceKey;//���ö����
            ReferenceConfig = cfg.ReferenceConfig;//���ö���
            ReferenceTag = cfg.ReferenceTag;//���ñ�ǩ
            if (cfg._extendConfig == null)
                return;
            foreach (var ex in cfg._extendConfig)
            {
                var old = ExtendConfig.FirstOrDefault(p => p.Name == ex.Name);
                if (old == null)
                    ExtendConfig.Add(ex);
            }
        }
        #endregion


        #region ��չ����

        /// <summary>
        /// ��չ����
        /// </summary>
        [DataMember, JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        private NotificationList<ConfigItem> _extendConfig;


        /// <summary>
        /// ��չ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��")]
        [DisplayName("��չ����")]
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
        /// ��չ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigItemList ExtendConfigList => _extendConfigList ?? (_extendConfigList = new ConfigItemList(ExtendConfig));

        /// <summary>
        /// ��д��չ����
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
        /// ��ͼȡ����չ����,��������ڻ�Ϊ�������Ĭ��ֵ�󷵻�
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="def">Ĭ��ֵ</param>
        /// <returns>��չ����</returns>

        public string TryGetExtendConfig(string key, string def)
        {
            if (key == null)
                return def;
            var mv = ExtendConfig.FirstOrDefault(p => string.Equals(p.Name, key, StringComparison.OrdinalIgnoreCase));
            if (mv != null)
                return mv.Value ?? (mv.Value = def);
            ExtendConfig.Add(new ConfigItem { Name = key, Value = def });
            return def;
        }

        #endregion
    }
}