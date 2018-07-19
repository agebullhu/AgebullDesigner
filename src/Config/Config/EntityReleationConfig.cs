using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     关联
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public sealed partial class EntityReleationConfig : ConfigBase
    {

        /// <summary>
        ///     名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore, Browsable(false)]
        public string DisplayName => $"{Caption}({Name})";


        /// <summary>
        ///     上级
        /// </summary>
        [IgnoreDataMember,JsonIgnore, Browsable(false)]
        public EntityConfig Parent { get; set; }


        #region 

        /// <summary>
        /// 关联表的外键名称
        /// </summary>
        [DataMember, JsonProperty("ForeignKey", NullValueHandling = NullValueHandling.Ignore)]
        internal string _foreignKey;

        /// <summary>
        /// 关联表的外键名称
        /// </summary>
        /// <remark>
        /// 关联表的外键名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("关联表的外键名称"), Description("关联表的外键名称")]
        public string ForeignKey
        {
            get => _foreignKey;
            set
            {
                if (_foreignKey == value)
                    return;
                BeforePropertyChanged(nameof(ForeignKey), _foreignKey, value);
                _foreignKey = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ForeignKey));
            }
        }

        /// <summary>
        /// 与关联表的外键对应的字段名称
        /// </summary>
        [DataMember, JsonProperty("PrimaryKey", NullValueHandling = NullValueHandling.Ignore)]
        internal string _primaryKey;

        /// <summary>
        /// 与关联表的外键对应的字段名称
        /// </summary>
        /// <remark>
        /// 上级关联到当前对象名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("与关联表的外键对应的字段名称"), Description("上级关联到当前对象名称")]
        public string PrimaryKey
        {
            get => _primaryKey;
            set
            {
                if (_primaryKey == value)
                    return;
                BeforePropertyChanged(nameof(PrimaryKey), _primaryKey, value);
                _primaryKey = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(PrimaryKey));
            }
        }

        /// <summary>
        /// 关联表
        /// </summary>
        [DataMember, JsonProperty("Friend", NullValueHandling = NullValueHandling.Ignore)]
        internal string _friend;

        /// <summary>
        /// 关联表
        /// </summary>
        /// <remark>
        /// 关联表
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("关联表"), Description("关联表")]
        public string Friend
        {
            get => _friend;
            set
            {
                if (_friend == value)
                    return;
                BeforePropertyChanged(nameof(Friend), _friend, value);
                _friend = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Friend));
            }
        }

        private const string Releation_Description = @"0[与Friend的平等关系的1对1],1[存在对Friend的上下级关系的1对1],2[1对多]";

        /// <summary>
        /// 关系类型
        /// </summary>
        [DataMember, JsonProperty("Releation", NullValueHandling = NullValueHandling.Ignore)]
        internal int _releation;

        /// <summary>
        /// 关系类型
        /// </summary>
        /// <remark>
        /// 0[与Friend的平等关系的1对1],1[存在对Friend的上下级关系的1对1],2[1对多]
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("关系类型"), Description(Releation_Description)]
        public int Releation
        {
            get => _releation;
            set
            {
                if (_releation == value)
                    return;
                BeforePropertyChanged(nameof(Releation), _releation, value);
                _releation = value;
                OnPropertyChanged(nameof(Releation));
            }
        }

        /// <summary>
        /// 扩展条件
        /// </summary>
        [DataMember, JsonProperty("Condition", NullValueHandling = NullValueHandling.Ignore)]
        internal string _condition;

        /// <summary>
        /// 扩展条件
        /// </summary>
        /// <remark>
        /// 扩展条件
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("扩展条件"), Description("扩展条件")]
        public string Condition
        {
            get => _condition;
            set
            {
                if (_condition == value)
                    return;
                BeforePropertyChanged(nameof(Condition), _condition, value);
                _condition = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Condition));
            }
        }
        #endregion
    }
}