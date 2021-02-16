using Newtonsoft.Json;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     数据关联配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public sealed partial class ReleationConfig : ModelChildConfig
    {
        #region MyRegion
        [IgnoreDataMember, JsonIgnore]
        EntityConfig _primary;
        [IgnoreDataMember, JsonIgnore]
        EntityConfig _foreign;

        [IgnoreDataMember, JsonIgnore]
        FieldConfig _primaryField;
        [IgnoreDataMember, JsonIgnore]
        FieldConfig _foreignField;

        /// <summary>
        /// 外键实体
        /// </summary>
        public EntityConfig ForeignEntity => _foreign ??= GlobalConfig.GetEntity(ForeignTable);

        /// <summary>
        /// 实体
        /// </summary>
        public EntityConfig PrimaryEntity => _primary ??= GlobalConfig.GetEntity(PrimaryTable);

        /// <summary>
        /// 外键实体
        /// </summary>
        public FieldConfig ForeignField => _foreignField ??= ForeignEntity?.Find(p => p.Name == ForeignKey);

        /// <summary>
        /// 实体
        /// </summary>
        public FieldConfig PrimaryField => _primaryField ??= PrimaryEntity?.Find(p => p.Name == PrimaryKey);


        #endregion

        #region 

        /// <summary>
        /// 同步写入
        /// </summary>
        [DataMember, JsonProperty("canWrite", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _canWrite;

        /// <summary>
        /// 同步写入
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("同步写入"), Description("同步写入")]
        public bool CanWrite
        {
            get => _canWrite;
            set
            {
                if (_canWrite == value)
                    return;
                BeforePropertyChanged(nameof(CanWrite), _canWrite, value);
                _canWrite = value;
                OnPropertyChanged(nameof(CanWrite));
            }
        }

        /// <summary>
        /// 同步删除
        /// </summary>
        [DataMember, JsonProperty("canDelete", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool canDelete;

        /// <summary>
        /// 同步删除
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("同步删除"), Description("同步删除")]
        public bool CanDelete
        {
            get => canDelete;
            set
            {
                if (canDelete == value)
                    return;
                BeforePropertyChanged(nameof(CanDelete), canDelete, value);
                canDelete = value;
                OnPropertyChanged(nameof(CanDelete));
            }
        }

        /// <summary>
        /// 主表
        /// </summary>
        [DataMember, JsonProperty("PrimaryTable", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _primaryTable;

        /// <summary>
        /// 主表
        /// </summary>
        /// <remark>
        /// 主表
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("主表"), Description("主表")]
        public string PrimaryTable
        {
            get => _primaryTable;
            set
            {
                if (_primaryTable == value)
                    return;
                BeforePropertyChanged(nameof(PrimaryTable), _primaryTable, value);
                _primary = null; _primaryField = null;
                _primaryTable = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(PrimaryTable));
            }
        }

        /// <summary>
        /// 主表连接字段名称
        /// </summary>
        [DataMember, JsonProperty("PrimaryKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _primaryKey;

        /// <summary>
        /// 主键
        /// </summary>
        /// <remark>
        /// 主键
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("主键"), Description("主表连接字段名称")]
        public string PrimaryKey
        {
            get => _primaryKey;
            set
            {
                if (_primaryKey == value)
                    return;
                BeforePropertyChanged(nameof(PrimaryKey), _primaryKey, value);
                _primaryField = null;
                _primaryKey = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(PrimaryKey));
            }
        }

        /// <summary>
        /// 子表
        /// </summary>
        [DataMember, JsonProperty("ForeignTable", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _foreignTable;

        /// <summary>
        /// 子表
        /// </summary>
        /// <remark>
        /// 子表
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("子表"), Description("关联表")]
        public string ForeignTable
        {
            get => _foreignTable;
            set
            {
                if (_foreignTable == value)
                    return;
                BeforePropertyChanged(nameof(ForeignTable), _foreignTable, value);
                _foreign = null; _foreignField = null;
                _foreignTable = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ForeignTable));
            }
        }

        /// <summary>
        /// 关联表的外键名称
        /// </summary>
        [DataMember, JsonProperty("ForeignKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _foreignKey;

        /// <summary>
        /// 关联表的外键名称
        /// </summary>
        /// <remark>
        /// 关联表的外键名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("外键"), Description("关联表的外键名称")]
        public string ForeignKey
        {
            get => _foreignKey;
            set
            {
                if (_foreignKey == value)
                    return;
                BeforePropertyChanged(nameof(ForeignKey), _foreignKey, value);
                _foreignField = null;
                _foreignKey = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ForeignKey));
            }
        }

        /// <summary>
        /// 连接条件
        /// </summary>
        [DataMember, JsonProperty("Condition", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _Condition;

        /// <summary>
        /// 连接条件
        /// </summary>
        /// <remark>
        /// 连接条件
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("连接条件"), Description("连接条件")]
        public string Condition
        {
            get => _Condition;
            set
            {
                if (_Condition == value)
                    return;
                BeforePropertyChanged(nameof(Condition), _Condition, value);
                _Condition = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Condition));
            }
        }


        /// <summary>
        /// 表连接类型
        /// </summary>
        [DataMember, JsonProperty("JoinType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal EntityJoinType _joinType;

        /// <summary>
        /// 表连接类型
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("表连接类型")]
        public EntityJoinType JoinType
        {
            get => _joinType;
            set
            {
                if (_joinType == value)
                    return;
                BeforePropertyChanged(nameof(JoinType), _joinType, value);
                _joinType = value;
                OnPropertyChanged(nameof(JoinType));
            }
        }

        /// <summary>
        /// 关系模型类型
        /// </summary>
        [DataMember, JsonProperty("ModelType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal ReleationModelType _modelType;

        /// <summary>
        /// 关系模型类型
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("关系模型类型")]
        public ReleationModelType ModelType
        {
            get => _modelType;
            set
            {
                if (_modelType == value)
                    return;
                BeforePropertyChanged(nameof(ModelType), _modelType, value);
                _modelType = value;
                OnPropertyChanged(nameof(ModelType));
            }
        }
        #endregion

    }
}