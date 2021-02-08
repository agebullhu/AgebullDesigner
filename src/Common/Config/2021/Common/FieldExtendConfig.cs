using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 基于字段的扩展设置
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
    public partial class FieldExtendConfig : ConfigBase, IChildrenConfig
    {
        #region 引用

        ConfigBase IChildrenConfig.Parent { get => Parent as ConfigBase; set => Parent = value as EntityExtendConfig; }

        [IgnoreDataMember, JsonIgnore]
        private EntityExtendConfig parent;

        /// <summary>
        /// 上级
        /// </summary>
        public EntityExtendConfig Parent
        {
            get => parent; set
            {
                parent = value;
                OnPropertyChanged(nameof(Parent));
                OnPropertyChanged("IChildrenConfig.Parent");
            }
        }

        /// <summary>
        /// 字段键
        /// </summary>
        [DataMember, JsonProperty("propertyKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _propertyKey;

        /// <summary>
        /// 字段键
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"字段键"), Description(@"字段键")]
        public string PropertyKey
        {
            get => _propertyKey;
            set
            {
                if (_propertyKey == value)
                    return;
                BeforePropertyChanged(nameof(PropertyKey), _propertyKey, value);
                _propertyKey = value;
                _property = GlobalConfig.GetConfigByKey<IPropertyConfig>(_propertyKey);
                OnPropertyChanged(nameof(PropertyKey));
            }
        }

        /// <summary>
        /// 字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal IPropertyConfig _property;
        /// <summary>
        /// 字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"字段"), Description(@"字段")]
        public IPropertyConfig Property
        {
            get => _property ?? (_propertyKey.IsBlank() ? null : _property = GlobalConfig.GetConfigByKey<IPropertyConfig>(_propertyKey));
            set
            {
                if (_property == value)
                    return;
                BeforePropertyChanged(nameof(Property), _property, value);
                _property = value;
                _propertyKey = value?.Key;
                OnPropertyChanged(nameof(Property));
                OnPropertyChanged(nameof(PropertyKey));
            }
        }
        /// <summary>
        /// 实体的上级
        /// </summary>
        public IEntityConfig Entity => Property?.Entity;

        #endregion

        #region 名称关联

        /// <summary>
        ///     名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"名称")]
        public override string Name
        {
            get => Property?.Name;
            set => base.Name = value;
        }

        /// <summary>
        ///     标题
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"标题")]
        public override string Caption
        {
            get => _caption ?? Property?.Caption;
            set
            {
                base.Caption = value;
            }
        }

        /// <summary>
        ///     说明
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"说明")]
        public override string Description
        {
            get => _description ?? Property?.Description;
            set => base.Description = value;
        }

        /// <summary>
        /// 参见
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"参见")]
        public override string Remark
        {
            get => _remark ?? Property?.Remark;
            set => base.Remark = value;
        }


        /// <summary>
        ///     是否主键
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public bool IsPrimaryKey => Property.IsPrimaryKey;
        /// <summary>
        ///     是否空值
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public bool Nullable => Property.Nullable;

        /// <summary>
        ///     初始值
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public string Initialization { get => Property.Initialization; set => Property.Initialization = value; }

        /// <summary>
        ///     类型名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public string CsType => Property.CsType;
        /// <summary>
        ///     类型名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public string LastCsType => Property.LastCsType;

        /// <summary>
        ///     自定义类型名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public string CustomType => Property.CustomType;

        #endregion

        #region 框架支持

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is FieldExtendConfig cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(FieldExtendConfig dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(FieldExtendConfig dest)
        {
            PropertyKey = dest.PropertyKey;
            Property = dest.Property;
        }
        #endregion
    }
}
