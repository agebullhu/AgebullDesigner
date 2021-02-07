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
        #region 名称关联

        /// <summary>
        ///     名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"名称")]
        public override string Name
        {
            get => _name ?? Field?.Name;
            set => base.Name = value;
        }

        /// <summary>
        ///     标题
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"标题")]
        public override string Caption
        {
            get => _caption ?? Field?.Caption;
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
            get => _description ?? Field?.Description;
            set => base.Description = value;
        }

        /// <summary>
        /// 参见
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"参见")]
        public override string Remark
        {
            get => _remark ?? Field?.Remark;
            set => base.Remark = value;
        }

        #endregion

        #region 引用

        ConfigBase IChildrenConfig.Parent { get => Parent as ConfigBase; set => Parent = value as EntityExtendConfig; }

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
        [DataMember, JsonProperty("fieldKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _fieldKey;

        /// <summary>
        /// 字段键
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"字段键"), Description(@"字段键")]
        public string FieldKey
        {
            get => _fieldKey;
            set
            {
                if (_fieldKey == value)
                    return;
                BeforePropertyChanged(nameof(FieldKey), _fieldKey, value);
                _fieldKey = value;
                _field = GlobalConfig.GetConfigByKey<IFieldConfig>(_fieldKey);
                OnPropertyChanged(nameof(FieldKey));
            }
        }

        /// <summary>
        /// 字段
        /// </summary>
        [DataMember, JsonProperty("field", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal IFieldConfig _field;
        private EntityExtendConfig parent;

        /// <summary>
        /// 字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"字段"), Description(@"字段")]
        public IFieldConfig Field
        {
            get => _field ?? (_fieldKey.IsBlank() ? null : _field = GlobalConfig.GetConfigByKey<IFieldConfig>(_fieldKey));
            set
            {
                if (_field == value)
                    return;
                BeforePropertyChanged(nameof(Field), _field, value);
                _field = value;
                _fieldKey = value?.Key;
                OnPropertyChanged(nameof(Field));
                OnPropertyChanged(nameof(FieldKey));
            }
        }
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
            FieldKey = dest.FieldKey;
            Field = dest.Field;
        }
        #endregion
    }
}
