using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

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
        public ConfigBase()
        {
            Option.Config = this;
            ValueRecords.Add(nameof(Option), _option);
            GlobalTrigger.OnCtor(this);
        }

        /// <summary>
        /// 联合编辑配置
        /// </summary>
        [JsonIgnore]
        public virtual ConfigBase Friend => null;

        [JsonIgnore]
        private ConfigDesignOption _option;

        /// <summary>
        /// 配置
        /// </summary>
        [DataMember, JsonProperty("option", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public ConfigDesignOption Option
        {
            get
            {
                if (_option != null)
                    return _option;
                _option ??= new ConfigDesignOption { Config = this };
                BeforePropertyChange(nameof(Option), null, _option);
                return _option;
            }
            set
            {
                if (value == null)
                    return;
                BeforePropertyChange(nameof(Option), _option, value);
                _option = value;
                _option.Config = this;
                ValueRecords.Add(nameof(Option), _option);
                OnPropertyChanged(nameof(Option));
            }
        }

        /// <summary>
        ///     当前实例
        /// </summary>
        public SolutionConfig Solution => Option.Solution;

        /// <summary>
        /// 别名
        /// </summary>
        [DataMember, JsonProperty("Alias", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _alias;

        /// <summary>
        /// 别名
        /// </summary>
        /// <remark>
        /// 属性别名
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"别名"), Description("属性别名")]
        public string Alias
        {
            get => _alias == Name ? null : _alias;
            set
            {
                if (_alias == value)
                    return;
                BeforePropertyChange(nameof(Alias), _alias, value);
                _alias = value.SafeTrim();
                OnPropertyChanged(nameof(Alias));
            }
        }


        /// <summary>
        /// 原始名称
        /// </summary>
        [DataMember, JsonProperty("oldName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _oldName;

        /// <summary>
        /// 别名
        /// </summary>
        /// <remark>
        /// 属性别名
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"别名"), Description("属性别名")]
        public string OldName
        {
            get => _oldName;
            set
            {
                if (_oldName == value)
                    return;
                BeforePropertyChange(nameof(OldName), _oldName, value);
                _oldName = value?.Trim();
                OnPropertyChanged(nameof(OldName));
            }
        }
        #endregion

        #region 继承
        /*
        /// <summary>
        ///     名称
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"名称")]
        public sealed override string Name
        {
            get => Option.Reference != null ? Option.Reference.Name : base.Name;
            set => base.Name = value;
        }

        /// <summary>
        ///     标题
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"标题")]
        public sealed override string Caption
        {
            get => Option.Reference != null ? Option.Reference.Caption : base.Caption;
            set => base.Caption = value;
        }

        /// <summary>
        ///     说明
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"说明")]
        public sealed override string Description
        {
            get => Option.Reference != null ? Option.Reference.Description : base.Description;
            set => base.Description = value;
        }

        /// <summary>
        /// 参见
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"参见")]
        public sealed override string Remark
        {
            get => Option.Reference != null ? Option.Reference.Remark : base.Remark;
            set => base.Remark = value;
        }*/
        #endregion


        #region 设计标识

        /// <summary>
        /// 标识
        /// </summary>
        /// <remark>
        /// 名称
        /// </remark>
        [JsonIgnore]
        [Category("设计标识"), DisplayName("标识"), Description("名称")]
        public override string Key => Option.Key;

        /// <summary>
        /// 唯一标识
        /// </summary>
        /// <remark>
        /// 唯一标识
        /// </remark>
        [JsonIgnore]
        [Category("设计标识"), DisplayName("唯一标识"), Description("唯一标识")]
        public int Identity
        {
            get => Option.Identity;
            set => Option.Identity = value;
        }

        /// <summary>
        /// 编号
        /// </summary>
        /// <remark>
        /// 编号
        /// </remark>
        [JsonIgnore]
        [Category("设计标识"), DisplayName("编号"), Description("编号")]
        public int Index
        {
            get => Option.Index;
            set => Option.Index = value;
        }
        #endregion

        #region 系统

        /// <summary>
        /// 是否活动
        /// </summary>
        [JsonIgnore, Browsable(false), ReadOnly(true)]
        [Category("设计器支持"), DisplayName("是否活动")]
        public bool IsActive => Option.IsNormal;

        /// <summary>
        /// 引用对象键
        /// </summary>
        /// <remark>
        /// 引用对象键
        /// </remark>
        [JsonIgnore]
        [Category("设计器支持"), DisplayName("引用对象键"), Description("引用对象键，指内部对象的引用")]
        public string ReferenceKey
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
        [JsonIgnore]
        [Category("设计器支持"), DisplayName("是否参照对象"), Description("是否参照对象，是则永远只读")]
        public bool IsReference => Option.IsReference;

        /// <summary>
        /// 废弃
        /// </summary>
        /// <remark>
        /// 废弃
        /// </remark>
        [JsonIgnore]
        [Category("设计器支持"), DisplayName("废弃"), Description("废弃")]
        public bool IsDiscard
        {
            get => Option.IsDiscard;
            set => Option.IsDiscard = value;
        }

        /// <summary>
        /// 冻结
        /// </summary>
        /// <remark>
        /// 如为真,此配置的更改将不生成代码
        /// </remark>
        [JsonIgnore]
        [Category("设计器支持"), DisplayName("冻结"), Description("如为真,此配置的更改将不生成代码")]
        public bool IsFreeze
        {
            get => Option.IsFreeze;
            set => Option.IsFreeze = value;
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        /// <remark>
        /// 如为真,保存时删除
        /// </remark>
        [JsonIgnore]
        [Category("设计器支持"), DisplayName("标记删除"), Description("如为真,保存时删除")]
        public bool IsDelete
        {
            get => Option.IsDelete;
            set => Option.IsDelete = value;
        }
        #endregion 系统 

        #region 扩展配置

        /// <summary>
        /// 读写扩展配置
        /// </summary>
        /// <param name="classify">分类</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string classify, string name]
        {
            get => Option[classify, name];
            set
            {
                Option[classify, name] = value;
                RaisePropertyChanged($"{classify}_{name}");
            }
        }
        /// <summary>
        /// 读写扩展配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get => Option[name];
            set
            {
                Option[name] = value;
                RaisePropertyChanged(name);
            }
        }

        /// <summary>
        /// 标签（对应关系等）
        /// </summary>
        /// <remark>
        /// 值
        /// </remark>
        [JsonIgnore]
        [Category("*设计"), DisplayName("标签（对应关系等）"), Description("值")]
        public string Tag
        {
            get => this["Tag"];
            set => this["Tag"] = value;
        }

        /// <summary>
        /// 扩展配置
        /// </summary>
        [JsonIgnore, Browsable(false)]
        public ConfigItemListBool ExtendConfigListBool => Option.ExtendConfigListBool;

        /// <summary>
        /// 扩展配置
        /// </summary>
        [JsonIgnore, Browsable(false)]
        public ConfigItemList ExtendConfigList => Option.ExtendConfigList;

        #endregion
    }
}