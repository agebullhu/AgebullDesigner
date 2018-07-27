using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置基础
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ConfigBase : SimpleConfig
    {
        #region 设计
        /// <summary>
        /// 构造
        /// </summary>
        protected ConfigBase()
        {
            GlobalTrigger.OnCtor(this);
            Option.Config = this;
        }


        [IgnoreDataMember, JsonIgnore]
        private ConfigDesignOption _option;

        /// <summary>
        /// 配置
        /// </summary>
        [DataMember, JsonProperty("option", NullValueHandling = NullValueHandling.Ignore)]
        public ConfigDesignOption Option
        {
            get => _option ?? (_option = new ConfigDesignOption { Config = this });
            set
            {
                _option = value;
                if (_option == null)
                    _option = new ConfigDesignOption
                    {
                        Config = this
                    };
                else
                    _option.Config = this;
                OnPropertyChanged(nameof(Option));
            }
        }

        /// <summary>
        ///     当前实例
        /// </summary>
        public SolutionConfig Solution => Option.Solution;

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
            get => Extend[classify, name];
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
        /// <remark>
        /// 名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计标识"), DisplayName("标识"), Description("名称")]
        public Guid Key => Option.Key;

        /// <summary>
        /// 唯一标识
        /// </summary>
        /// <remark>
        /// 唯一标识
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计标识"), DisplayName("唯一标识"), Description("唯一标识")]
        public int Identity => Option.Identity;

        /// <summary>
        /// 编号
        /// </summary>
        /// <remark>
        /// 编号
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计标识"), DisplayName("编号"), Description("编号")]
        public int Index => Option.Index;
        #endregion
        #region 系统

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
            get => Option.ReferenceKey;
            set => Option.ReferenceKey = value;
        }

        /// <summary>
        /// 是否参照对象
        /// </summary>
        /// <remark>
        /// 是否参照对象，是则永远只读
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("是否参照对象"), Description("是否参照对象，是则永远只读")]
        public bool IsReference => Option.IsReference;

        /// <summary>
        /// 废弃
        /// </summary>
        /// <remark>
        /// 废弃
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("废弃"), Description("废弃")]
        public bool IsDiscard => Option.IsDiscard;

        /// <summary>
        /// 冻结
        /// </summary>
        /// <remark>
        /// 如为真,此配置的更改将不生成代码
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("冻结"), Description("如为真,此配置的更改将不生成代码")]
        public bool IsFreeze => Option.IsFreeze;

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <remark>
        /// 如为真,保存时删除
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("设计器支持"), DisplayName("标记删除"), Description("如为真,保存时删除")]
        public bool IsDelete => Option.IsDelete;
        #endregion 系统 

        #region 扩展

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
            get => ExtendConfigList["Tag"];
            set => ExtendConfigList["Tag"] = value;
        }

        #endregion
    }
}