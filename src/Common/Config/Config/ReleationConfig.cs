using Newtonsoft.Json;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     ���ݹ�������
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
        /// ���ʵ��
        /// </summary>
        public EntityConfig ForeignEntity => _foreign ??= GlobalConfig.GetEntity(ForeignTable);

        /// <summary>
        /// ʵ��
        /// </summary>
        public EntityConfig PrimaryEntity => _primary ??= GlobalConfig.GetEntity(PrimaryTable);

        /// <summary>
        /// ���ʵ��
        /// </summary>
        public FieldConfig ForeignField => _foreignField ??= ForeignEntity?.Find(p => p.Name == ForeignKey);

        /// <summary>
        /// ʵ��
        /// </summary>
        public FieldConfig PrimaryField => _primaryField ??= PrimaryEntity?.Find(p => p.Name == PrimaryKey);


        #endregion

        #region 

        /// <summary>
        /// ͬ��д��
        /// </summary>
        [DataMember, JsonProperty("canWrite", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _canWrite;

        /// <summary>
        /// ͬ��д��
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("ͬ��д��"), Description("ͬ��д��")]
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
        /// ͬ��ɾ��
        /// </summary>
        [DataMember, JsonProperty("canDelete", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool canDelete;

        /// <summary>
        /// ͬ��ɾ��
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("ͬ��ɾ��"), Description("ͬ��ɾ��")]
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
        /// ����
        /// </summary>
        [DataMember, JsonProperty("PrimaryTable", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _primaryTable;

        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("����"), Description("����")]
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
        /// ���������ֶ�����
        /// </summary>
        [DataMember, JsonProperty("PrimaryKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _primaryKey;

        /// <summary>
        /// ����
        /// </summary>
        /// <remark>
        /// ����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("����"), Description("���������ֶ�����")]
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
        /// �ӱ�
        /// </summary>
        [DataMember, JsonProperty("ForeignTable", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _foreignTable;

        /// <summary>
        /// �ӱ�
        /// </summary>
        /// <remark>
        /// �ӱ�
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("�ӱ�"), Description("������")]
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
        /// ��������������
        /// </summary>
        [DataMember, JsonProperty("ForeignKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _foreignKey;

        /// <summary>
        /// ��������������
        /// </summary>
        /// <remark>
        /// ��������������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("���"), Description("��������������")]
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
        /// ��������
        /// </summary>
        [DataMember, JsonProperty("Condition", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _Condition;

        /// <summary>
        /// ��������
        /// </summary>
        /// <remark>
        /// ��������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("��������"), Description("��������")]
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
        /// ����������
        /// </summary>
        [DataMember, JsonProperty("JoinType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal EntityJoinType _joinType;

        /// <summary>
        /// ����������
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("����������")]
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
        /// ��ϵģ������
        /// </summary>
        [DataMember, JsonProperty("ModelType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal ReleationModelType _modelType;

        /// <summary>
        /// ��ϵģ������
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category("���ݿ�"), DisplayName("��ϵģ������")]
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