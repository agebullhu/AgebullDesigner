using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Agebull.EntityModel.Config;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    ///     ���û���
    /// </summary>
    public class ConfigDataModel<TConfig> : NotificationObject
        where TConfig : ConfigBase, new()
    {
        #region ���
        
        /// <summary>
        ///     ��ǰʵ��
        /// </summary>
        public SolutionConfig Solution => SolutionConfig.Current;

        /// <summary>
        ///     ��ǰ����
        /// </summary>
        public string Type => Config?.GetType().Name;

        /// <summary>
        /// ���ö����
        /// </summary>
        [DataMember, JsonProperty("ModelKey", NullValueHandling = NullValueHandling.Ignore)]
        internal Guid _modelKey;

        /// <summary>
        /// ���ö����
        /// </summary>
        /// <remark>
        /// ���ö����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("ϵͳ"), DisplayName("���ö����"), Description("���ö������ָʵ�ʰ�װ�����ö��������")]
        public Guid ModelKey
        {
            get => _modelKey;
            set
            {
                if (_modelKey == value)
                    return;
                BeforePropertyChanged(nameof(ModelKey), _modelKey, value);
                _modelKey = value;
                OnPropertyChanged(nameof(ModelKey));
            }
        }

        private TConfig _config;

        /// <summary>
        /// ʵ�ʰ�װ�����ö���
        /// </summary>
        public TConfig Config
        {
            get
            {
                if (_config != null)
                    return _config;
                if (!GlobalConfig.ConfigDictionary.TryGetValue(ModelKey, out ConfigBase model) || !(model is TConfig))
                {
                    TraceMessage.DefaultTrace.Track = $"���ü�{ModelKey}��Ч";
                    ModelKey = Guid.Empty;
                }
                else
                {
                    _config = (TConfig)model;
                }
                return _config;
            }
            set
            {
                if (_config == value)
                    return;
                BeforePropertyChanged(nameof(Config), _config, value);
                _config = value;
                OnPropertyChanged(nameof(Config));
            }
        }

        #endregion

        #region ��չ����

        /// <summary>
        /// ��չ����
        /// </summary>
        [DataMember, JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        protected List<ConfigItem> _extendConfig;


        /// <summary>
        /// ��չ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("ϵͳ")]
        [DisplayName("��չ����")]
        public List<ConfigItem> ExtendConfig
        {
            get
            {
                if (_extendConfig != null)
                    return _extendConfig;
                _extendConfig = new List<ConfigItem>();
                BeforePropertyChanged(nameof(ExtendConfig), null, _extendConfig);
                return _extendConfig;
            }
            set
            {
                if (_extendConfig == value)
                    return;
                BeforePropertyChanged(nameof(ExtendConfig), _extendConfig, value);
                _extendConfig = value;
                OnPropertyChanged(nameof(ExtendConfig));
            }
        }

        [IgnoreDataMember, JsonIgnore]
        private ConfigItemList _extendConfigList;
        /// <summary>
        /// ��չ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigItemList ExtendConfigList => _extendConfigList ?? (_extendConfigList = new ConfigItemList(ExtendConfig));

        [IgnoreDataMember, JsonIgnore]
        private ConfigItemListBool _extendConfigListBool;

        /// <summary>
        /// ��չ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigItemListBool ExtendConfigListBool => _extendConfigListBool ?? (_extendConfigListBool = new ConfigItemListBool(ExtendConfig));

        /// <summary>
        /// ��д��չ����
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                return key == null ? null : ExtendConfig.FirstOrDefault(p => p.Name == key)?.Value;
            }
            set
            {
                if (key == null)
                    return;
                var mv = ExtendConfig.FirstOrDefault(p => p.Name == key);
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
            var mv = ExtendConfig.FirstOrDefault(p => p.Name == key);
            if (mv != null)
                return mv.Value ?? (mv.Value = def);
            ExtendConfig.Add(new ConfigItem { Name = key, Value = def });
            return def;
        }

        #endregion

        #region ϵͳ

        /// <summary>
        /// �Ƿ�Ԥ�������
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false), ReadOnly(true)]
        public bool IsPredefined;
        
        /// <summary>
        /// �Ƿ���ն���
        /// </summary>
        [DataMember, JsonProperty("_isReference", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isReference;

        /// <summary>
        /// �Ƿ���ն���
        /// </summary>
        /// <remark>
        /// �Ƿ���ն���������Զֻ��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("ϵͳ"), DisplayName("�Ƿ���ն���"), Description("�Ƿ���ն���������Զֻ��")]
        public bool IsReference
        {
            get => _isReference;
            set
            {
                if (_isReference == value)
                    return;
                BeforePropertyChanged(nameof(IsReference), _isReference, value);
                _isReference = value;
                OnPropertyChanged(nameof(IsReference));
            }
        }

        /// <summary>
        /// ��ʶ
        /// </summary>
        [DataMember, JsonProperty("_key", NullValueHandling = NullValueHandling.Ignore)]
        internal Guid _key;

        /// <summary>
        /// ��ʶ
        /// </summary>
        /// <remark>
        /// ����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("ϵͳ"), DisplayName("��ʶ"), Description("����")]
        public Guid Key
        {
            get => _key;
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
        [DataMember, JsonProperty("Identity", NullValueHandling = NullValueHandling.Ignore)]
        internal int _identity;

        /// <summary>
        /// Ψһ��ʶ
        /// </summary>
        /// <remark>
        /// Ψһ��ʶ
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("ϵͳ"), DisplayName("Ψһ��ʶ"), Description("Ψһ��ʶ")]
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
        [DataMember, JsonProperty("Index", NullValueHandling = NullValueHandling.Ignore)]
        internal int _index;

        /// <summary>
        /// ���
        /// </summary>
        /// <remark>
        /// ���
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("ϵͳ"), DisplayName("���"), Description("���")]
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

        /// <summary>
        /// ����
        /// </summary>
        [DataMember, JsonProperty("Discard", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _discard;

        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("ϵͳ"), DisplayName("����"), Description("����")]
        public bool Discard
        {
            get => _discard;
            set
            {
                if (_discard == value)
                    return;
                BeforePropertyChanged(nameof(Discard), _discard, value);
                _discard = value;
                OnPropertyChanged(nameof(Discard));
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        [DataMember, JsonProperty("IsFreeze", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isFreeze;

        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ��Ϊ��,�����õĸ��Ľ������ɴ���
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("ϵͳ"), DisplayName("����"), Description("��Ϊ��,�����õĸ��Ľ������ɴ���")]
        public bool IsFreeze
        {
            get => _isFreeze;
            set
            {
                if (_isFreeze == value)
                    return;
                BeforePropertyChanged(nameof(IsFreeze), _isFreeze, value);
                _isFreeze = value;
                OnPropertyChanged(nameof(IsFreeze));
            }
        }

        /// <summary>
        /// ���ɾ��
        /// </summary>
        [DataMember, JsonProperty("_isDelete", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isDelete;

        /// <summary>
        /// ���ɾ��
        /// </summary>
        /// <remark>
        /// ��Ϊ��,����ʱɾ��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("ϵͳ"), DisplayName("���ɾ��"), Description("��Ϊ��,����ʱɾ��")]
        public bool IsDelete
        {
            get => _isDelete;
            set
            {
                if (_isDelete == value)
                    return;
                BeforePropertyChanged(nameof(IsDelete), _isDelete, value);
                _isDelete = value;
                OnPropertyChanged(nameof(IsDelete));
            }
        }
        #endregion ϵͳ 

        #region ��չ
        
        /// <summary>���ر�ʾ��ǰ������ַ�����</summary>
        /// <returns>��ʾ��ǰ������ַ�����</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return Config?.ToString() ?? "������";
        }

        #endregion
        
    }
}