/*design by:agebull designer date:2017/7/12 22:06:40*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using Agebull.EntityModel.Config.V2021;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 属性配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class FieldConfigBase : ConfigBase
    {
        /// <summary>
        /// 自已
        /// </summary>
        public abstract IPropertyConfig Me { get; }

        #region 兼容性

        public bool CanImport { get => ExtendConfigListBool["easyui_CanImport"]; set => ExtendConfigListBool["easyui_CanImport"] = value; }
        public bool CanExport { get => ExtendConfigListBool["easyui_CanExport"]; set => ExtendConfigListBool["easyui_CanExport"] = value; }
        public bool UserHint { get => ExtendConfigListBool["user_help"]; set => ExtendConfigListBool["user_help"] = value; }

        /// <summary>
        /// 是否只读
        /// </summary>/// <remarks>
        /// 指数据只可读,无法写入的场景,如此字段为汇总字段
        /// </remarks>
        public bool IsReadonly { get; set; }



        /// <summary>
        /// 值类型
        /// </summary>
        [DataMember, JsonProperty("valueType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _valueType;

        /// <summary>
        /// 值类型
        /// </summary>
        [JsonIgnore]
        [DisplayName(@"值类型"), Description(@"值类型")]
        public string ValueType
        {
            get => _valueType;
            set
            {
                if (_valueType == value)
                    return;
                BeforePropertyChange(nameof(ValueType), _valueType, value);
                _valueType = value;
                OnPropertyChanged(nameof(ValueType));
            }
        }
        #endregion

        #region 反向链接

        /// <summary>
        /// 内部字段
        /// </summary>
        [DataMember, JsonProperty("_innerField", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _innerField;

        /// <summary>
        /// 内部字段,用户不可见
        /// </summary>
        /// <remark>
        /// 是否内部字段,即非用户字段,不呈现给用户
        /// </remark>
        [JsonIgnore]
        [Category(@"模型设计"), DisplayName(@"内部字段"), Description("是否内部字段,即非用户字段,不呈现给用户")]
        public bool InnerField
        {
            get => _innerField;
            set
            {
                if (_innerField == value)
                    return;
                BeforePropertyChange(nameof(InnerField), _innerField, value);
                _innerField = value;
                OnPropertyChanged(nameof(InnerField));
            }
        }

        /// <summary>
        /// 数据字段
        /// </summary>
        [JsonIgnore]
        internal DataBaseFieldConfig _dataBaseField;

        /// <summary>
        /// 数据字段
        /// </summary>
        [JsonIgnore]
        [Category(@"扩展对象"), DisplayName(@"数据字段"), Description("数据字段")]
        public DataBaseFieldConfig DataBaseField
        {
            get => _dataBaseField;
            set
            {
                if (_dataBaseField == value)
                    return;
                BeforePropertyChange(nameof(DataBaseField), _dataBaseField, value);
                _dataBaseField = value;
                if (_dataBaseField != null)
                    _dataBaseField.Property = Me;
                OnPropertyChanged(nameof(DataBaseField));
                OnPropertyChanged(nameof(IsLinkField));
            }
        }


        /// <summary>
        /// 是否外部链接字段
        /// </summary>
        /// <remark>
        /// 是否外部链接字段,如果为真,此字段也在其它表中
        /// </remark>
        [JsonIgnore]
        [Category(@"数据库"), DisplayName(@"是否外部链接字段"), Description(@"是否外部链接字段,如果为真,此字段也在其它表中")]
        public bool IsLinkField
        {
            get => Me.EnableDataBase && _dataBaseField != null && _dataBaseField.IsLinkField;
            set
            {
                if (_dataBaseField == null)
                    return;
                if (_dataBaseField.IsLinkField == value)
                    return;
                _dataBaseField.IsLinkField = value;
                OnPropertyChanged(nameof(IsLinkField));
                OnPropertyChanged(nameof(NoStorage));
            }
        }

        /// <summary>
        /// 自增字段的说明文字
        /// </summary>
        const string IsIdentity_Description = @"自增列,通过数据库(或REDIS)自动增加";

        /// <summary>
        /// 自增字段
        /// </summary>
        /// <remark>
        /// 自增列,通过数据库(或REDIS)自动增加
        /// </remark>
        [JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"自增字段"), Description(IsIdentity_Description)]
        public bool IsIdentity
        {
            get => _dataBaseField != null && _dataBaseField.IsIdentity;
            set
            {
                if (_dataBaseField == null)
                    return;
                _dataBaseField.IsIdentity = value;
                OnPropertyChanged(nameof(IsIdentity));
            }
        }

        /// <summary>
        /// 数据库的读写忽略这个字段
        /// </summary>
        /// <remark>
        /// 是否非数据库字段,如果为真,数据库的读写均忽略这个字段
        /// </remark>
        [JsonIgnore]
        [Category(@"数据库"), DisplayName(@"非数据库字段"), Description(@"是否非数据库字段,如果为真,数据库的读写均忽略这个字段")]
        public bool NoStorage
        {
            get => !Me.EnableDataBase || _dataBaseField == null || !_dataBaseField.IsActive || _dataBaseField.NoStorage;
            set
            {
                if (_dataBaseField == null)
                    return;
                if (_dataBaseField.NoStorage == value)
                    return;
                _dataBaseField.NoStorage = value;
                OnPropertyChanged(nameof(NoStorage));
                OnPropertyChanged(nameof(IsLinkField));
            }
        }

        #endregion

        #region 更新需要
#pragma warning disable CS0649 
        #region 数据库

        /// <summary>
        /// 数据库索引
        /// </summary>
        [DataMember, JsonProperty("isDbIndex", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isDbIndex;

        /// <summary>
        /// 数据库字段名称
        /// </summary>
        [DataMember, JsonProperty("_columnName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbFieldName;

        /// <summary>
        /// 能否存储空值
        /// </summary>
        [DataMember, JsonProperty("_dbNullable", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _dbNullable;

        /// <summary>
        /// 存储类型
        /// </summary>
        [DataMember, JsonProperty("DbType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbType;

        /// <summary>
        /// 数据长度
        /// </summary>
        [DataMember, JsonProperty("_datalen", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _datalen;


        /// <summary>
        /// 存储精度
        /// </summary>
        [DataMember, JsonProperty("Scale", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _scale;

        /// <summary>
        /// 固定长度
        /// </summary>
        [DataMember, JsonProperty("FixedLength", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _fixedLength;

        /// <summary>
        /// 备注字段
        /// </summary>
        [DataMember, JsonProperty("IsMemo", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isMemo;

        /// <summary>
        /// 大数据
        /// </summary>
        [DataMember, JsonProperty("IsBlob", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isBlob;

        /// <summary>
        /// 内部字段(数据库)
        /// </summary>
        [DataMember, JsonProperty("DbInnerField", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _dbInnerField;

        /// <summary>
        /// *跳过保存的场景
        /// </summary>
        [DataMember, JsonProperty("KeepStorageScreen", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal StorageScreenType _keepStorageScreen;

        /// <summary>
        /// 自定义保存
        /// </summary>
        [DataMember, JsonProperty("CustomWrite", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _customWrite;

        /// <summary>
        /// 存储值读写字段
        /// </summary>
        [DataMember, JsonProperty("StorageProperty", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _storageProperty;

        /// <summary>
        /// 自增字段
        /// </summary>
        [DataMember, JsonProperty("IsIdentity", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isIdentity;

        /// <summary>
        /// 关联表名
        /// </summary>
        [DataMember, JsonProperty("LinkTable", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _linkTable;

        /// <summary>
        /// 关联表主键
        /// </summary>
        [DataMember, JsonProperty("IsLinkKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isLinkKey;

        /// <summary>
        /// 关联表标题
        /// </summary>
        [DataMember, JsonProperty("IsLinkCaption", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isLinkCaption;

        /// <summary>
        /// 对应客户ID
        /// </summary>
        [DataMember, JsonProperty("IsUserId", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isUserId;

        /// <summary>
        /// 关联字段名称
        /// </summary>
        [DataMember, JsonProperty("LinkField", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _linkField;

        #endregion

        #region 汇总支持

        /// <summary>
        /// 汇总方法
        /// </summary>
        [DataMember, JsonProperty("function", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _function;


        /// <summary>
        /// 汇总条件
        /// </summary>
        [DataMember, JsonProperty("having", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _having;

        #endregion

#pragma warning restore CS0649
        #endregion
    }
}