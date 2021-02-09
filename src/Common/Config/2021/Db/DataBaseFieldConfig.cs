using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 数据库字段
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
    public partial class DataBaseFieldConfig : FieldExtendConfig<DataTableConfig>, IDataBaseFieldConfig
    {
        #region 字段属性同步

        /// <summary>
        ///     唯一索引
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public bool UniqueIndex { get => Property.UniqueIndex; set => Property.UniqueIndex = value; }

        /// <summary>
        ///     是否主键
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public bool IsPrimaryKey { get => Property.IsPrimaryKey; set => Property.IsPrimaryKey = value; }

        /// <summary>
        ///     是否标题
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public bool IsCaption { get => Property.IsCaption; set => Property.IsCaption = value; }
        
        /// <summary>
        ///     是否空值
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public bool Nullable { get => Property.Nullable; set => Property.Nullable = value; }
        
        #endregion
        #region 字段

        /// <summary>
        /// 存储类型
        /// </summary>
        [DataMember, JsonProperty("fieldType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _fieldType;

        /// <summary>
        /// 存储类型
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"存储类型"), Description(@"存储类型")]
        public string FieldType
        {
            get => _fieldType;
            set
            {
                if (_fieldType == value)
                    return;
                BeforePropertyChanged(nameof(FieldType), _fieldType, value);
                _fieldType = value;
                OnPropertyChanged(nameof(FieldType));
            }
        }

        /// <summary>
        /// 数据库字段名称
        /// </summary>
        [DataMember, JsonProperty("dbFieldName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbFieldName;

        /// <summary>
        /// 数据库字段名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"数据库字段名称"), Description(@"数据库字段名称")]
        public string DbFieldName
        {
            get => _dbFieldName;
            set
            {
                if (_dbFieldName == value)
                    return;
                BeforePropertyChanged(nameof(DbFieldName), _dbFieldName, value);
                _dbFieldName = value;
                OnPropertyChanged(nameof(DbFieldName));
            }
        }

        /// <summary>
        /// 不更新
        /// </summary>
        [DataMember, JsonProperty("keepUpdate", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _keepUpdate;

        /// <summary>
        /// 不更新
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"不更新"), Description(@"不更新")]
        public bool KeepUpdate
        {
            get => _keepUpdate;
            set
            {
                if (_keepUpdate == value)
                    return;
                BeforePropertyChanged(nameof(KeepUpdate), _keepUpdate, value);
                _keepUpdate = value;
                OnPropertyChanged(nameof(KeepUpdate));
            }
        }

        /// <summary>
        /// 能否存储空值
        /// </summary>/// <remarks>
        /// 如为真,在存储空值读取时使用语言类型的默认值
        /// </remarks>
        [DataMember, JsonProperty("dbNullable", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _dbNullable;

        /// <summary>
        /// 能否存储空值
        /// </summary>/// <remarks>
        /// 如为真,在存储空值读取时使用语言类型的默认值
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"能否存储空值"), Description(@"如为真,在存储空值读取时使用语言类型的默认值")]
        public bool DbNullable
        {
            get => _dbNullable;
            set
            {
                if (_dbNullable == value)
                    return;
                BeforePropertyChanged(nameof(DbNullable), _dbNullable, value);
                _dbNullable = value;
                OnPropertyChanged(nameof(DbNullable));
            }
        }

        /// <summary>
        /// 需要构建索引
        /// </summary>
        [DataMember, JsonProperty("needDbIndex", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _needDbIndex;

        /// <summary>
        /// 需要构建索引
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"需要构建索引"), Description(@"需要构建索引")]
        public bool NeedDbIndex
        {
            get => _needDbIndex;
            set
            {
                if (_needDbIndex == value)
                    return;
                BeforePropertyChanged(nameof(NeedDbIndex), _needDbIndex, value);
                _needDbIndex = value;
                OnPropertyChanged(nameof(NeedDbIndex));
            }
        }

        /// <summary>
        /// 数据长度
        /// </summary>/// <remarks>
        /// 文本或二进制存储的最大长度
        /// </remarks>
        [DataMember, JsonProperty("datalen", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _datalen;

        /// <summary>
        /// 数据长度
        /// </summary>/// <remarks>
        /// 文本或二进制存储的最大长度
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"数据长度"), Description(@"文本或二进制存储的最大长度")]
        public int Datalen
        {
            get => _datalen;
            set
            {
                if (_datalen == value)
                    return;
                BeforePropertyChanged(nameof(Datalen), _datalen, value);
                _datalen = value;
                OnPropertyChanged(nameof(Datalen));
            }
        }

        /// <summary>
        /// 存储精度
        /// </summary>
        [DataMember, JsonProperty("scale", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _scale;

        /// <summary>
        /// 存储精度
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"存储精度"), Description(@"存储精度")]
        public int Scale
        {
            get => _scale;
            set
            {
                if (_scale == value)
                    return;
                BeforePropertyChanged(nameof(Scale), _scale, value);
                _scale = value;
                OnPropertyChanged(nameof(Scale));
            }
        }

        /// <summary>
        /// 是否数据库索引
        /// </summary>
        [DataMember, JsonProperty("isDbIndex", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isDbIndex;

        /// <summary>
        /// 是否数据库索引
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"是否数据库索引"), Description(@"是否数据库索引")]
        public bool IsDbIndex
        {
            get => _isDbIndex;
            set
            {
                if (_isDbIndex == value)
                    return;
                BeforePropertyChanged(nameof(IsDbIndex), _isDbIndex, value);
                _isDbIndex = value;
                OnPropertyChanged(nameof(IsDbIndex));
            }
        }

        /// <summary>
        /// 固定长度
        /// </summary>/// <remarks>
        /// 是否固定长度字符串
        /// </remarks>
        [DataMember, JsonProperty("fixedLength", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _fixedLength;

        /// <summary>
        /// 固定长度
        /// </summary>/// <remarks>
        /// 是否固定长度字符串
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"固定长度"), Description(@"是否固定长度字符串")]
        public bool FixedLength
        {
            get => _fixedLength;
            set
            {
                if (_fixedLength == value)
                    return;
                BeforePropertyChanged(nameof(FixedLength), _fixedLength, value);
                _fixedLength = value;
                OnPropertyChanged(nameof(FixedLength));
            }
        }

        /// <summary>
        /// 备注字段
        /// </summary>
        [DataMember, JsonProperty("isText", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isText;

        /// <summary>
        /// 长文本
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"备注字段"), Description(@"备注字段")]
        public bool IsText
        {
            get => _isText;
            set
            {
                if (_isText == value)
                    return;
                BeforePropertyChanged(nameof(IsText), _isText, value);
                _isText = value;
                OnPropertyChanged(nameof(IsText));
            }
        }

        /// <summary>
        /// 大数据
        /// </summary>
        [DataMember, JsonProperty("isBlob", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isBlob;

        /// <summary>
        /// 大数据
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"大数据"), Description(@"大数据")]
        public bool IsBlob
        {
            get => _isBlob;
            set
            {
                if (_isBlob == value)
                    return;
                BeforePropertyChanged(nameof(IsBlob), _isBlob, value);
                _isBlob = value;
                OnPropertyChanged(nameof(IsBlob));
            }
        }

        /// <summary>
        /// 内部字段(数据库)
        /// </summary>/// <remarks>
        /// 数据库内部字段,如果为真,仅支持在SQL的语句中出现此字段,不支持外部的读写
        /// </remarks>
        [DataMember, JsonProperty("dbInnerField", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _dbInnerField;

        /// <summary>
        /// 内部字段(数据库)
        /// </summary>/// <remarks>
        /// 数据库内部字段,如果为真,仅支持在SQL的语句中出现此字段,不支持外部的读写
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"内部字段(数据库)"), Description(@"数据库内部字段,如果为真,仅支持在SQL的语句中出现此字段,不支持外部的读写")]
        public bool DbInnerField
        {
            get => _dbInnerField;
            set
            {
                if (_dbInnerField == value)
                    return;
                BeforePropertyChanged(nameof(DbInnerField), _dbInnerField, value);
                _dbInnerField = value;
                OnPropertyChanged(nameof(DbInnerField));
            }
        }

        /// <summary>
        /// 存储数据时跳过的场景
        /// </summary>
        [DataMember, JsonProperty("keepStorageScreen", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal StorageScreenType _keepStorageScreen;

        /// <summary>
        /// 存储数据时跳过的场景
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"存储数据时跳过的场景"), Description(@"存储数据时跳过的场景")]
        public StorageScreenType KeepStorageScreen
        {
            get => _keepStorageScreen;
            set
            {
                if (_keepStorageScreen == value)
                    return;
                BeforePropertyChanged(nameof(KeepStorageScreen), _keepStorageScreen, value);
                _keepStorageScreen = value;
                OnPropertyChanged(nameof(KeepStorageScreen));
            }
        }

        /// <summary>
        /// 自定义保存
        /// </summary>/// <remarks>
        /// 自定义保存,如果为真,数据库的写入忽略这个字段,数据的写入由代码自行维护
        /// </remarks>
        [DataMember, JsonProperty("customWrite", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _customWrite;

        /// <summary>
        /// 自定义保存
        /// </summary>/// <remarks>
        /// 自定义保存,如果为真,数据库的写入忽略这个字段,数据的写入由代码自行维护
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"自定义保存"), Description(@"自定义保存,如果为真,数据库的写入忽略这个字段,数据的写入由代码自行维护")]
        public bool CustomWrite
        {
            get => _customWrite;
            set
            {
                if (_customWrite == value)
                    return;
                BeforePropertyChanged(nameof(CustomWrite), _customWrite, value);
                _customWrite = value;
                OnPropertyChanged(nameof(CustomWrite));
            }
        }

        /// <summary>
        /// 存储值读写字段
        /// </summary>/// <remarks>
        /// 存储值读写字段(internal),即使用非基础类型时,当发生读写数据库操作时使用的字段,字段为文本(JSON或XML)类型,使用序列化方法读写
        /// </remarks>
        [DataMember, JsonProperty("storageProperty", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _storageProperty;

        /// <summary>
        /// 存储值读写字段
        /// </summary>/// <remarks>
        /// 存储值读写字段(internal),即使用非基础类型时,当发生读写数据库操作时使用的字段,字段为文本(JSON或XML)类型,使用序列化方法读写
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"存储值读写字段"), Description(@"存储值读写字段(internal),即使用非基础类型时,当发生读写数据库操作时使用的字段,字段为文本(JSON或XML)类型,使用序列化方法读写")]
        public string StorageProperty
        {
            get => _storageProperty;
            set
            {
                if (_storageProperty == value)
                    return;
                BeforePropertyChanged(nameof(StorageProperty), _storageProperty, value);
                _storageProperty = value;
                OnPropertyChanged(nameof(StorageProperty));
            }
        }

        /// <summary>
        /// 非数据库字段
        /// </summary>/// <remarks>
        /// 是否非数据库字段,如果为真,数据库的读写均忽略这个字段
        /// </remarks>
        [DataMember, JsonProperty("noStorage", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _noStorage;

        /// <summary>
        /// 非数据库字段
        /// </summary>/// <remarks>
        /// 是否非数据库字段,如果为真,数据库的读写均忽略这个字段
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"非数据库字段"), Description(@"是否非数据库字段,如果为真,数据库的读写均忽略这个字段")]
        public bool NoStorage
        {
            get => _noStorage;
            set
            {
                if (_noStorage == value)
                    return;
                BeforePropertyChanged(nameof(NoStorage), _noStorage, value);
                _noStorage = value;
                OnPropertyChanged(nameof(NoStorage));
            }
        }

        /// <summary>
        /// 自增字段
        /// </summary>/// <remarks>
        /// 自增列,通过数据库(或REDIS)自动增加
        /// </remarks>
        [DataMember, JsonProperty("isIdentity", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isIdentity;

        /// <summary>
        /// 自增字段
        /// </summary>/// <remarks>
        /// 自增列,通过数据库(或REDIS)自动增加
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"自增字段"), Description(@"自增列,通过数据库(或REDIS)自动增加")]
        public bool IsIdentity
        {
            get => _isIdentity;
            set
            {
                if (_isIdentity == value)
                    return;
                BeforePropertyChanged(nameof(IsIdentity), _isIdentity, value);
                _isIdentity = value;
                OnPropertyChanged(nameof(IsIdentity));
            }
        }

        /// <summary>
        /// 汇总方法
        /// </summary>
        [DataMember, JsonProperty("function", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _function;

        /// <summary>
        /// 汇总方法
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"汇总方法"), Description(@"汇总方法")]
        public string Function
        {
            get => _function;
            set
            {
                if (_function == value)
                    return;
                BeforePropertyChanged(nameof(Function), _function, value);
                _function = value;
                OnPropertyChanged(nameof(Function));
            }
        }

        /// <summary>
        /// 汇总条件
        /// </summary>
        [DataMember, JsonProperty("having", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _having;

        /// <summary>
        /// 汇总条件
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"汇总条件"), Description(@"汇总条件")]
        public string Having
        {
            get => _having;
            set
            {
                if (_having == value)
                    return;
                BeforePropertyChanged(nameof(Having), _having, value);
                _having = value;
                OnPropertyChanged(nameof(Having));
            }
        }

        /// <summary>
        /// 值类型
        /// </summary>
        [DataMember, JsonProperty("valueType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _valueType;

        /// <summary>
        /// 值类型
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"值类型"), Description(@"值类型")]
        public string ValueType
        {
            get => _valueType;
            set
            {
                if (_valueType == value)
                    return;
                BeforePropertyChanged(nameof(ValueType), _valueType, value);
                _valueType = value;
                OnPropertyChanged(nameof(ValueType));
            }
        }

        /// <summary>
        /// 上级外键
        /// </summary>
        [DataMember, JsonProperty("isParent", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isParent;

        /// <summary>
        /// 上级外键
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"上级外键"), Description(@"上级外键")]
        public bool IsParent
        {
            get => _isParent;
            set
            {
                if (_isParent == value)
                    return;
                BeforePropertyChanged(nameof(IsParent), _isParent, value);
                _isParent = value;
                OnPropertyChanged(nameof(IsParent));
            }
        }

        /// <summary>
        /// 连接字段
        /// </summary>
        [DataMember, JsonProperty("isLinkField", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isLinkField;

        /// <summary>
        /// 连接字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"连接字段"), Description(@"连接字段")]
        public bool IsLinkField
        {
            get => _isLinkField;
            set
            {
                if (_isLinkField == value)
                    return;
                BeforePropertyChanged(nameof(IsLinkField), _isLinkField, value);
                _isLinkField = value;
                OnPropertyChanged(nameof(IsLinkField));
            }
        }

        /// <summary>
        /// 关联表名
        /// </summary>
        [DataMember, JsonProperty("linkTable", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _linkTable;

        /// <summary>
        /// 关联表名
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"关联表名"), Description(@"关联表名")]
        public string LinkTable
        {
            get => _linkTable;
            set
            {
                if (_linkTable == value)
                    return;
                BeforePropertyChanged(nameof(LinkTable), _linkTable, value);
                _linkTable = value;
                OnPropertyChanged(nameof(LinkTable));
            }
        }

        /// <summary>
        /// 关联表主键
        /// </summary>/// <remarks>
        /// 关联表主键,即与另一个实体关联的外键
        /// </remarks>
        [DataMember, JsonProperty("isLinkKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isLinkKey;

        /// <summary>
        /// 关联表主键
        /// </summary>/// <remarks>
        /// 关联表主键,即与另一个实体关联的外键
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"关联表主键"), Description(@"关联表主键,即与另一个实体关联的外键")]
        public bool IsLinkKey
        {
            get => _isLinkKey;
            set
            {
                if (_isLinkKey == value)
                    return;
                BeforePropertyChanged(nameof(IsLinkKey), _isLinkKey, value);
                _isLinkKey = value;
                OnPropertyChanged(nameof(IsLinkKey));
            }
        }

        /// <summary>
        /// 关联表标题
        /// </summary>/// <remarks>
        /// 关联表标题,即此字段为关联表的标题内容
        /// </remarks>
        [DataMember, JsonProperty("isLinkCaption", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isLinkCaption;

        /// <summary>
        /// 关联表标题
        /// </summary>/// <remarks>
        /// 关联表标题,即此字段为关联表的标题内容
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"关联表标题"), Description(@"关联表标题,即此字段为关联表的标题内容")]
        public bool IsLinkCaption
        {
            get => _isLinkCaption;
            set
            {
                if (_isLinkCaption == value)
                    return;
                BeforePropertyChanged(nameof(IsLinkCaption), _isLinkCaption, value);
                _isLinkCaption = value;
                OnPropertyChanged(nameof(IsLinkCaption));
            }
        }

        /// <summary>
        /// 关联字段名称
        /// </summary>/// <remarks>
        /// 关联字段名称,即在关联表中的字段名称
        /// </remarks>
        [DataMember, JsonProperty("linkField", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _linkField;

        /// <summary>
        /// 关联字段名称
        /// </summary>/// <remarks>
        /// 关联字段名称,即在关联表中的字段名称
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"关联字段名称"), Description(@"关联字段名称,即在关联表中的字段名称")]
        public string LinkField
        {
            get => _linkField;
            set
            {
                if (_linkField == value)
                    return;
                BeforePropertyChanged(nameof(LinkField), _linkField, value);
                _linkField = value;
                OnPropertyChanged(nameof(LinkField));
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>/// <remarks>
        /// 指数据只可读,无法写入的场景,如此字段为汇总字段
        /// </remarks>
        [DataMember, JsonProperty("isReadonly", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isReadonly;

        /// <summary>
        /// 是否只读
        /// </summary>/// <remarks>
        /// 指数据只可读,无法写入的场景,如此字段为汇总字段
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"是否只读"), Description(@"指数据只可读,无法写入的场景,如此字段为汇总字段")]
        public bool IsReadonly
        {
            get => _isReadonly;
            set
            {
                if (_isReadonly == value)
                    return;
                BeforePropertyChanged(nameof(IsReadonly), _isReadonly, value);
                _isReadonly = value;
                OnPropertyChanged(nameof(IsReadonly));
            }
        }
        #endregion 字段

        #region 兼容性升级

        /// <summary>
        /// 兼容性升级
        /// </summary>
        public void Upgrade()
        {
            //Copy(Entity);
        }

        #endregion

        #region 字段复制

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(IDataBaseFieldConfig dest)
        {
            CopyFrom((SimpleConfig)dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is IDataBaseFieldConfig cfg)
                CopyProperty(cfg);
            if (dest is FieldConfig cefg)
                CopyProperty(cefg);
            if (dest is PropertyConfig p)
                CopyProperty(p);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(IDataBaseFieldConfig dest)
        {
            FieldType = dest.FieldType;
            DbFieldName = dest.DbFieldName;
            KeepUpdate = dest.KeepUpdate;
            DbNullable = dest.DbNullable;
            NeedDbIndex = dest.NeedDbIndex;
            Datalen = dest.Datalen;
            Scale = dest.Scale;
            IsDbIndex = dest.IsDbIndex;
            FixedLength = dest.FixedLength;
            IsText = dest.IsText;
            IsBlob = dest.IsBlob;
            DbInnerField = dest.DbInnerField;
            KeepStorageScreen = dest.KeepStorageScreen;
            CustomWrite = dest.CustomWrite;
            StorageProperty = dest.StorageProperty;
            NoStorage = dest.NoStorage;
            IsIdentity = dest.IsIdentity;
            Function = dest.Function;
            Having = dest.Having;
            ValueType = dest.ValueType;
            IsParent = dest.IsParent;
            IsLinkField = dest.IsLinkField;
            LinkTable = dest.LinkTable;
            IsLinkKey = dest.IsLinkKey;
            IsLinkCaption = dest.IsLinkCaption;
            LinkField = dest.LinkField;
            IsReadonly = dest.IsReadonly;
        }


        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(FieldConfig dest)
        {
            FieldType = dest.FieldType;
            DbFieldName = dest.DbFieldName;
            KeepUpdate = dest.KeepUpdate;
            DbNullable = dest.DbNullable;
            NeedDbIndex = dest.NeedDbIndex;
            Datalen = dest.Datalen;
            Scale = dest.Scale;
            IsDbIndex = dest.IsDbIndex;
            FixedLength = dest.FixedLength;
            IsText = dest.IsMemo;
            IsBlob = dest.IsBlob;
            DbInnerField = dest.DbInnerField;
            KeepStorageScreen = dest.KeepStorageScreen;
            CustomWrite = dest.CustomWrite;
            StorageProperty = dest.StorageProperty;
            NoStorage = dest.NoStorage;
            IsIdentity = dest.IsIdentity;
            Function = dest.Function;
            Having = dest.Having;
            ValueType = dest.ValueType;
            IsParent = dest.IsParent;
            IsLinkField = dest.IsLinkField;
            LinkTable = dest.LinkTable;
            IsLinkKey = dest.IsLinkKey;
            IsLinkCaption = dest.IsLinkCaption;
            LinkField = dest.LinkField;
            IsReadonly = dest.IsReadonly;
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(PropertyConfig dest)
        {
            FieldType = dest.FieldType;
            DbFieldName = dest.DbFieldName;
            KeepUpdate = dest.KeepUpdate;
            DbNullable = dest.DbNullable;
            NeedDbIndex = dest.NeedDbIndex;
            Datalen = dest.Datalen;
            Scale = dest.Scale;
            IsDbIndex = dest.IsDbIndex;
            FixedLength = dest.FixedLength;
            IsText = dest.IsMemo;
            IsBlob = dest.IsBlob;
            DbInnerField = dest.DbInnerField;
            KeepStorageScreen = dest.KeepStorageScreen;
            CustomWrite = dest.CustomWrite;
            StorageProperty = dest.StorageProperty;
            NoStorage = dest.NoStorage;
            IsIdentity = dest.IsIdentity;
            Function = dest.Function;
            Having = dest.Having;
            ValueType = dest.ValueType;
            IsParent = dest.IsParent;
            IsLinkField = dest.IsLinkField;
            LinkTable = dest.LinkTable;
            IsLinkKey = dest.IsLinkKey;
            IsLinkCaption = dest.IsLinkCaption;
            LinkField = dest.LinkField;
            IsReadonly = dest.IsReadonly;
        }
        #endregion 字段复制
    }
}

