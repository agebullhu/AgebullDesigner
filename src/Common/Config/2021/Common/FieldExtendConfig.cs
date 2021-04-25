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
    public partial class FieldExtendConfig<TParent> : ConfigBase, IChildrenConfig
        where TParent : EntityExtendConfig
    {
        #region 引用

        ISimpleConfig IChildrenConfig.Parent { get => Parent; set => Parent = value as TParent; }

        [JsonIgnore]
        private TParent parent;

        /// <summary>
        /// 上级
        /// </summary>
        public TParent Parent
        {
            get => parent; 
            set
            {
                parent = value;
                RaisePropertyChanged(nameof(Parent));
                RaisePropertyChanged("IChildrenConfig.Parent");
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
        [JsonIgnore]
        [DisplayName(@"字段键"), Description(@"字段键")]
        public string PropertyKey
        {
            get => _propertyKey;
            set
            {
                if (_propertyKey == value)
                    return;
                BeforePropertyChange(nameof(PropertyKey), _propertyKey, value);
                _propertyKey = value;
                _property = GlobalConfig.GetConfigByKey<IPropertyConfig>(_propertyKey);
                RaisePropertyChanged(nameof(Property));
                OnPropertyChanged(nameof(PropertyKey));
            }
        }

        /// <summary>
        /// 字段
        /// </summary>
        [JsonIgnore]
        internal IPropertyConfig _property;
        /// <summary>
        /// 字段
        /// </summary>
        [JsonIgnore]
        [DisplayName(@"字段"), Description(@"字段")]
        public IPropertyConfig Property
        {
            get => _property ?? (_propertyKey.IsMissing() 
                ? null
                : _property = SetProperty(GlobalConfig.GetConfigByKey<IPropertyConfig>(_propertyKey)));
            set
            {
                if (_property == value)
                    return;
                BeforePropertyChange(nameof(Property), _property, value);
                _property = SetProperty(value);
                _propertyKey = value?.Key;
                RaisePropertyChanged(nameof(Property));
                OnPropertyChanged(nameof(PropertyKey));
            }
        }
        /// <summary>
        /// 实体的上级
        /// </summary>
        public IEntityConfig Entity => Property?.Entity;

        protected virtual IPropertyConfig SetProperty(IPropertyConfig property)
        {
            return property;
        }

        #endregion

        #region 名称关联

        /// <summary>
        ///     名称
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"名称")]
        public override string Name
        {
            get => Property?.Name;
            set => base.Name = value;
        }

        /// <summary>
        ///     标题
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"标题")]
        public override string Caption
        {
            get => Property?.Caption;
            set
            {
                base.Caption = value;
            }
        }

        /// <summary>
        ///     说明
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"说明")]
        public override string Description
        {
            get => Property?.Description;
            set => base.Description = value;
        }

        #endregion

        #region 字段属性同步

        /// <summary>
        ///     Json名称
        /// </summary>
        [JsonIgnore]
        public string JsonName { get => Property.JsonName; set => Property.JsonName = value; }

        /// <summary>
        ///     初始值
        /// </summary>
        [JsonIgnore]
        public string Initialization { get => Property.Initialization; set => Property.Initialization = value; }

        /// <summary>
        ///     类型名称
        /// </summary>
        [JsonIgnore]
        public string DataType => Property.DataType;
        /// <summary>
        ///     类型名称
        /// </summary>
        [JsonIgnore]
        public string CsType => Property.CsType;
        
        /// <summary>
        ///     类型名称
        /// </summary>
        [JsonIgnore]
        public string LastCsType => Property.LastCsType;

        /// <summary>
        ///     自定义类型名称
        /// </summary>
        [JsonIgnore]
        public string CustomType => Property.CustomType;

        #endregion

        #region 框架支持
        /*
        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is FieldExtendConfig<TParent> cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(FieldExtendConfig<TParent> dest)
        {
            CopyFrom(dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(FieldExtendConfig<TParent> dest)
        {
            PropertyKey = dest.PropertyKey;
            Property = dest.Property;
        }*/
        #endregion
    }
}
