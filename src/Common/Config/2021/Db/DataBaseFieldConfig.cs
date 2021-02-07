using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 数据库字段
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
    public partial class DataBaseFieldConfig : FieldExtendConfig, IDataBaseFieldConfig
    {
        #region 字段

        /// <summary>
        /// 存储类型
        /// </summary>
        [DataMember, JsonProperty("dbType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbType;

        /// <summary>
        /// 存储类型
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"存储类型"), Description(@"存储类型")]
        public string DbType
        {
            get => _dbType;
            set
            {
                if (_dbType == value)
                    return;
                BeforePropertyChanged(nameof(DbType), _dbType, value);
                _dbType = value;
                OnPropertyChanged(nameof(DbType));
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
        [DataMember, JsonProperty("isMemo", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isMemo;

        /// <summary>
        /// 备注字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"备注字段"), Description(@"备注字段")]
        public bool IsMemo
        {
            get => _isMemo;
            set
            {
                if (_isMemo == value)
                    return;
                BeforePropertyChanged(nameof(IsMemo), _isMemo, value);
                _isMemo = value;
                OnPropertyChanged(nameof(IsMemo));
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
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(IDataBaseFieldConfig dest)
        {
            DbType = dest.DbType;
            DbFieldName = dest.DbFieldName;
            KeepUpdate = dest.KeepUpdate;
            DbNullable = dest.DbNullable;
            NeedDbIndex = dest.NeedDbIndex;
            Datalen = dest.Datalen;
            Scale = dest.Scale;
            IsDbIndex = dest.IsDbIndex;
            FixedLength = dest.FixedLength;
            IsMemo = dest.IsMemo;
            IsBlob = dest.IsBlob;
            DbInnerField = dest.DbInnerField;
            KeepStorageScreen = dest.KeepStorageScreen;
            CustomWrite = dest.CustomWrite;
            StorageProperty = dest.StorageProperty;
            NoStorage = dest.NoStorage;
        }
        #endregion 字段复制
    }
}
