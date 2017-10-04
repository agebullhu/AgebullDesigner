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
    ///     配置基础
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public class ConfigBase : SimpleConfig
    {
        #region 设计

        /// <summary>
        ///     当前实例
        /// </summary>
        public SolutionConfig Solution => SolutionConfig.Current;

        /// <summary>
        ///     当前类型
        /// </summary>
        public virtual string Type => GetType().Name;
        
        #endregion

        #region 扩展配置

        /// <summary>
        /// 扩展配置
        /// </summary>
        [DataMember, JsonProperty(NullValueHandling = NullValueHandling.Ignore)] private List<ConfigItem> _extendConfig;


        /// <summary>
        /// 扩展配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持")]
        [DisplayName("扩展配置")]
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
        /// 扩展配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigItemList ExtendConfigList => _extendConfigList ?? (_extendConfigList = new ConfigItemList(ExtendConfig));

        [IgnoreDataMember, JsonIgnore]
        private ConfigItemListBool _extendConfigListBool;

        /// <summary>
        /// 扩展配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigItemListBool ExtendConfigListBool => _extendConfigListBool ?? (_extendConfigListBool = new ConfigItemListBool(ExtendConfig));

        /// <summary>
        /// 读写扩展配置
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
        /// 试图取得扩展配置,如果不存在或为空则加入默认值后返回
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="def">默认值</param>
        /// <returns>扩展配置</returns>

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

        #region 扩展配置

        /// <summary>
        /// 扩展配置
        /// </summary>
        [DataMember, JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, Dictionary<string, string>> _extendDictionary;


        /// <summary>
        /// 扩展配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持")]
        [DisplayName("扩展配置")]
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
        /// 扩展配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Browsable(false)]
        public ConfigItemDictionary Extend => _extendDictionary2 ?? (_extendDictionary2 = new ConfigItemDictionary(ExtendDictionary));


        /// <summary>
        /// 读写扩展配置
        /// </summary>
        /// <param name="classify">分类</param>
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
        #region 设计标识


        /// <summary>
        /// 标识
        /// </summary>
        [DataMember, JsonProperty("_key", NullValueHandling = NullValueHandling.Ignore)] private Guid _key;

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
        /// 编号
        /// </summary>
        [DataMember, JsonProperty("Index", NullValueHandling = NullValueHandling.Ignore)] private int _index;

        /// <summary>
        /// 编号
        /// </summary>
        /// <remark>
        /// 编号
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计标识"), DisplayName("编号"), Description("编号")]
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
        #region 系统
        /// <summary>
        /// 是否预定义对象
        /// </summary>
        [IgnoreDataMember, JsonIgnore,Browsable( false),ReadOnly(true)]
        public bool IsPredefined;


        /// <summary>
        /// 引用对象键
        /// </summary>
        [DataMember, JsonProperty("ReferenceKey", NullValueHandling = NullValueHandling.Ignore)] private Guid _referenceKey;

        /// <summary>
        /// 引用对象键
        /// </summary>
        /// <remark>
        /// 引用对象键
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("引用对象键"), Description("引用对象键，指内部对象的引用")]
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
        /// 是否参照对象
        /// </summary>
        [DataMember, JsonProperty("_isReference", NullValueHandling = NullValueHandling.Ignore)] private bool _isReference;

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
        /// 废弃
        /// </summary>
        [DataMember, JsonProperty("Discard", NullValueHandling = NullValueHandling.Ignore)] private bool _discard;

        /// <summary>
        /// 废弃
        /// </summary>
        /// <remark>
        /// 废弃
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("废弃"), Description("废弃")]
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
        /// 冻结
        /// </summary>
        [DataMember, JsonProperty("IsFreeze", NullValueHandling = NullValueHandling.Ignore)] private bool _isFreeze;

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
        /// 标记删除
        /// </summary>
        [DataMember, JsonProperty("_isDelete", NullValueHandling = NullValueHandling.Ignore)] private bool _isDelete;

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
        #endregion 系统 

        #region 扩展
        /// <summary>
        ///     原始状态
        /// </summary>
        [IgnoreDataMember, JsonIgnore] public ConfigStateType OriginalState;

        /// <summary>
        /// 标签（对应关系等）
        /// </summary>
        [DataMember, JsonProperty("Tag", NullValueHandling = NullValueHandling.Ignore)] private string _tag;

        /// <summary>
        /// 标签（对应关系等）
        /// </summary>
        /// <remark>
        /// 值
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("*设计"), DisplayName("标签（对应关系等）"), Description("值")]
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
        /// 曾用名
        /// </summary>
        [DataMember, JsonProperty("_oldNames", NullValueHandling = NullValueHandling.Ignore)] private List<string> _oldNames;

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
        ///     曾用名
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [ReadOnly(true)]
        [Category("设计器支持")]
        [DisplayName(@"曾用名")]
        [Description("曾用名")]
        public string NameHistory => OldNames.LinkToString(",");

        /// <summary>返回表示当前对象的字符串。</summary>
        /// <returns>表示当前对象的字符串。</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"{Caption}:{Name}";
        }

        #endregion
        
        #region LUA结构支持

        /// <summary>
        ///     LUA结构支持
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
        ///     LUA结构支持
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