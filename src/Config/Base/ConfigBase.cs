using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Agebull.Common.LUA;
using Newtonsoft.Json;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    ///     ���û���
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ConfigBase : SimpleConfig
    {
        #region ���

        /// <summary>
        ///     ��ǰʵ��
        /// </summary>
        public SolutionConfig Solution => SolutionConfig.Current;

        /// <summary>
        ///     ��ǰ����
        /// </summary>
        public virtual string Type => GetType().Name;
        
        #endregion

        #region ��չ����

        /// <summary>
        /// ��չ����
        /// </summary>
        [DataMember, JsonProperty(NullValueHandling = NullValueHandling.Ignore)] private List<ConfigItem> _extendConfig;


        /// <summary>
        /// ��չ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��")]
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

        #region ��չ����

        /// <summary>
        /// ��չ����
        /// </summary>
        [DataMember, JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, Dictionary<string, string>> _extendDictionary;


        /// <summary>
        /// ��չ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��")]
        [DisplayName("��չ����")]
        public Dictionary<string, Dictionary<string, string>> ExtendDictionary
        {
            get
            {
                if (_extendDictionary != null)
                    return _extendDictionary;
                _extendDictionary = new Dictionary<string, Dictionary<string, string>>();
                BeforePropertyChanged(nameof(ExtendDictionary), null, _extendDictionary);
                return _extendDictionary;
            }
            set
            {
                if (_extendDictionary == value)
                    return;
                BeforePropertyChanged(nameof(ExtendDictionary), _extendDictionary, value);
                _extendDictionary = value;
                OnPropertyChanged(nameof(ExtendDictionary));
            }
        }

        [IgnoreDataMember, JsonIgnore]
        private ConfigItemDictionary _extendDictionary2;
        /// <summary>
        /// ��չ����
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigItemDictionary Extend => _extendDictionary2 ?? (_extendDictionary2 = new ConfigItemDictionary(ExtendDictionary));


        /// <summary>
        /// ��д��չ����
        /// </summary>
        /// <param name="classify">����</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string classify, string name]
        {
            get
            {
                return Extend[classify,name];
            }
            set
            {
                Extend[classify, name] = value;
                RaisePropertyChanged($"{classify}.{name}");
            }
        }

        #endregion
        #region ��Ʊ�ʶ


        /// <summary>
        /// ��ʶ
        /// </summary>
        [DataMember, JsonProperty("_key", NullValueHandling = NullValueHandling.Ignore)] private Guid _key;

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
            get
            {
                return _key;
            }
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
            get
            {
                return _identity;
            }
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
            get
            {
                return _index;
            }
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
        #region ϵͳ
        /// <summary>
        /// �Ƿ�Ԥ�������
        /// </summary>
        [IgnoreDataMember, JsonIgnore,Browsable( false),ReadOnly(true)]
        public bool IsPredefined;


        /// <summary>
        /// ���ö����
        /// </summary>
        [DataMember, JsonProperty("ReferenceKey", NullValueHandling = NullValueHandling.Ignore)] private Guid _referenceKey;

        /// <summary>
        /// ���ö����
        /// </summary>
        /// <remark>
        /// ���ö����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("���ö����"), Description("���ö������ָ�ڲ����������")]
        public Guid ReferenceKey
        {
            get
            {
                return _referenceKey;
            }
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
        /// �Ƿ���ն���
        /// </summary>
        [DataMember, JsonProperty("_isReference", NullValueHandling = NullValueHandling.Ignore)] private bool _isReference;

        /// <summary>
        /// �Ƿ���ն���
        /// </summary>
        /// <remark>
        /// �Ƿ���ն���������Զֻ��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("�Ƿ���ն���"), Description("�Ƿ���ն���������Զֻ��")]
        public bool IsReference
        {
            get
            {
                return _isReference;
            }
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
        /// ����
        /// </summary>
        [DataMember, JsonProperty("Discard", NullValueHandling = NullValueHandling.Ignore)] private bool _discard;

        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("�����֧��"), DisplayName("����"), Description("����")]
        public bool Discard
        {
            get
            {
                return _discard;
            }
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
        [DataMember, JsonProperty("IsFreeze", NullValueHandling = NullValueHandling.Ignore)] private bool _isFreeze;

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
            get
            {
                return _isFreeze;
            }
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
        [DataMember, JsonProperty("_isDelete", NullValueHandling = NullValueHandling.Ignore)] private bool _isDelete;

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
            get
            {
                return _isDelete;
            }
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
        /// <summary>
        ///     ԭʼ״̬
        /// </summary>
        [IgnoreDataMember, JsonIgnore] public ConfigStateType OriginalState;

        /// <summary>
        /// ��ǩ����Ӧ��ϵ�ȣ�
        /// </summary>
        [DataMember, JsonProperty("Tag", NullValueHandling = NullValueHandling.Ignore)] private string _tag;

        /// <summary>
        /// ��ǩ����Ӧ��ϵ�ȣ�
        /// </summary>
        /// <remark>
        /// ֵ
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("*���"), DisplayName("��ǩ����Ӧ��ϵ�ȣ�"), Description("ֵ")]
        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                if (_tag == value)
                    return;
                BeforePropertyChanged(nameof(Tag), _tag, value);
                _tag = value;
                OnPropertyChanged(nameof(Tag));
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        [DataMember, JsonProperty("_oldNames", NullValueHandling = NullValueHandling.Ignore)] private List<string> _oldNames;

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
            get
            {
                return _oldNames ??(_oldNames = new List<string>());
            }
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

        /// <summary>���ر�ʾ��ǰ������ַ�����</summary>
        /// <returns>��ʾ��ǰ������ַ�����</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"{Caption}:{Name}";
        }

        #endregion
        
        #region LUA�ṹ֧��

        /// <summary>
        ///     LUA�ṹ֧��
        /// </summary>
        /// <returns></returns>
        public virtual void GetLuaStruct(StringBuilder code)
        {
            if (!string.IsNullOrWhiteSpace(Name))
                code.AppendLine($@"['Name'] = '{Name.ToLuaString()}',");

            if (!string.IsNullOrWhiteSpace(Caption))
                code.AppendLine($@"['Caption'] = ""{Caption.ToLuaString()}"",");

            if (!string.IsNullOrWhiteSpace(Description))
                code.AppendLine($@"['Description'] = '{Description.ToLuaString()}',");

            code.AppendLine($@"['IsReference'] = {IsReference.ToString().ToLower()},");

            code.AppendLine($@"['Key'] = '{Key}',");

            code.AppendLine($@"['Identity'] = {Identity},");

            code.AppendLine($@"['Index'] = {Index},");

            //code.AppendLine($@"['Discard'] = {(Discard.ToString().ToLower())},");

            //code.AppendLine($@"['IsFreeze'] = {(IsFreeze.ToString().ToLower())},");

            //code.AppendLine($@"['IsDelete'] = {(IsDelete.ToString().ToLower())},");

            if (!string.IsNullOrWhiteSpace(Tag))
                code.AppendLine($@"['Tag'] = '{Tag.ToLuaString()}',");

            //if (!string.IsNullOrWhiteSpace(NameHistory))
            //    code.AppendLine($@"['NameHistory'] = ""{NameHistory.ToLuaString()}"",");

            //int idx = 0;
            //code.Append("'OldNames':'{");
            //foreach (var val in OldNames)
            //    code.AppendLine($@"{++idx}:{val.GetLuaStruct()},");
        }

        /// <summary>
        ///     LUA�ṹ֧��
        /// </summary>
        /// <returns></returns>
        public string GetLuaStruct()
        {
            var code = new StringBuilder();
            GetLuaStruct(code);
            return "{" + code.ToString().TrimEnd('\r', '\n', ' ', '\t', ',') + "}";
        }

        #endregion
    }
}