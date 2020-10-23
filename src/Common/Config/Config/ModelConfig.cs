/*design by:agebull designer date:2017/7/12 23:16:38*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 实体配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ModelConfig : ProjectChildConfigBase, IEntityConfig
    {

        #region 系统

        /// <summary>
        /// 实体名称
        /// </summary>
        [DataMember, JsonProperty("entityKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public Guid _entityKey;

        /// <summary>
        /// 实体名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal EntityConfig _entity;

        /// <summary>
        /// 实体名称
        /// </summary>
        /// <remark>
        /// 实体名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"实体名称"), Description("实体名称")]
        public EntityConfig Entity
        {
            get => _entity ??= GlobalConfig.GetEntity(p => p.Key == _entityKey);
            set
            {
                if (value == _entity)
                    return;
                BeforePropertyChanged(nameof(Entity), _entity, value);
                _entity = value;
                _entityKey = value.Key;
                OnPropertyChanged(nameof(Entity));
            }
        }

        /// <summary>
        ///     名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"名称")]
        public override string Name
        {
            get => _name ?? Entity?.Name;
            set => base.Name = value;
        }

        /// <summary>
        ///     标题
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"标题")]
        public override string Caption
        {
            get => _caption ?? Entity?.Caption;
            set => base.Caption = value;
        }

        /// <summary>
        ///     说明
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"说明")]
        public override string Description
        {
            get => _description ?? Entity?.Description;
            set => base.Description = value;
        }

        /// <summary>
        /// 参见
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category("设计支持"), DisplayName(@"参见")]
        public override string Remark
        {
            get => _remark ?? Entity?.Remark;
            set => base.Remark = value;
        }


        /// <summary>
        /// 阻止编辑
        /// </summary>
        [DataMember, JsonProperty("DenyScope", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal AccessScopeType? _denyScope;

        /// <summary>
        /// 阻止编辑
        /// </summary>
        /// <remark>
        /// 阻止使用的范围
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"系统"), DisplayName(@"阻止编辑"), Description("阻止使用的范围")]
        public AccessScopeType DenyScope
        {
            get => _denyScope == null ? Entity.DenyScope : _denyScope.Value;
            set
            {
                if (_denyScope == value)
                    return;
                BeforePropertyChanged(nameof(DenyScope), _denyScope, value);
                _denyScope = value;
                OnPropertyChanged(nameof(DenyScope));
            }
        }

        /// <summary>
        /// 最大字段标识号
        /// </summary>
        [DataMember, JsonProperty("MaxIdentity", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _maxIdentity;

        /// <summary>
        /// 最大字段标识号
        /// </summary>
        /// <remark>
        /// 最大字段标识号
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"系统"), DisplayName(@"最大字段标识号"), Description("最大字段标识号")]
        public int MaxIdentity
        {
            get => _maxIdentity;
            set
            {
                if (_maxIdentity == value)
                    return;
                BeforePropertyChanged(nameof(MaxIdentity), _maxIdentity, value);
                _maxIdentity = value;
                OnPropertyChanged(nameof(MaxIdentity));
            }
        }
        #endregion

        #region 数据标识

        /// <summary>
        /// 是否存在属性组合唯一值
        /// </summary>
        /// <remark>
        /// 是否存在属性组合唯一值
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"是否存在属性组合唯一值"), Description("是否存在属性组合唯一值")]
        public bool IsUniqueUnion => Properties.Count > 0 && Properties.Count(p => p.UniqueIndex > 0) > 1;

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <remark>
        /// 主键字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"主键字段"), Description("主键字段")]
        public PropertyConfig PrimaryColumn => Properties.FirstOrDefault(p => Entity.PrimaryColumn == p.Field);

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <remark>
        /// 主键字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        IFieldConfig IEntityConfig.PrimaryColumn => PrimaryColumn == null || PrimaryColumn.IsDiscard ? null : PrimaryColumn;

        /// <summary>
        /// 是否有主键
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"是否有主键"), Description("是否有主键")]
        public bool HasePrimaryKey => Entity.HasePrimaryKey;

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <remark>
        /// 主键字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"主键字段"), Description("主键字段")]
        public string PrimaryField => PrimaryColumn?.Name;

        /// <summary>
        /// Redis唯一键模板
        /// </summary>
        [DataMember, JsonProperty("RedisKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _redisKey;

        /// <summary>
        /// Redis唯一键模板
        /// </summary>
        /// <remark>
        /// 保存在Redis中使用的键模板
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"Redis唯一键模板"), Description("保存在Redis中使用的键模板")]
        public string RedisKey
        {
            get => _redisKey ?? Entity.RedisKey;
            set
            {
                if (_redisKey == value)
                    return;
                BeforePropertyChanged(nameof(RedisKey), _redisKey, value);
                _redisKey = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(RedisKey));
            }
        }
        #endregion

        #region 数据模型

        /// <summary>
        /// 实体名称
        /// </summary>
        [DataMember, JsonProperty("EntityName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _entityName;

        /// <summary>
        /// 实体名称
        /// </summary>
        /// <remark>
        /// 实体名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"实体名称"), Description("实体名称")]
        public string EntityName
        {
            get => WorkContext.InCoderGenerating ? (string.IsNullOrWhiteSpace(_entityName) ? $"{Name}Model" : _entityName) : _entityName;
            set
            {
                if (value == Name)
                    value = null;
                if (_entityName == value)
                    return;
                BeforePropertyChanged(nameof(EntityName), _entityName, value);
                _entityName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(EntityName));
            }
        }

        /// <summary>
        /// 是否查询
        /// </summary>
        [DataMember, JsonProperty("isQuery", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isQuery;

        /// <summary>
        /// 是否查询
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"是否查询"), Description("是否查询")]
        public bool IsQuery
        {
            get => _isQuery;
            set
            {
                if (_isQuery == value)
                    return;
                BeforePropertyChanged(nameof(IsQuery), _isQuery, value);
                _isQuery = value;
                OnPropertyChanged(nameof(IsQuery));
            }
        }

        /// <summary>
        /// 参考类型
        /// </summary>
        [DataMember, JsonProperty("ReferenceType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _referenceType;
        /// <summary>
        /// 参考类型
        /// </summary>
        /// <remark>
        /// 字段类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"参考类型(C#)"), Description("字段类型")]
        public string ReferenceType
        {
            get => _referenceType;
            set
            {
                if (_referenceType == value)
                    return;
                BeforePropertyChanged(nameof(ReferenceType), _referenceType, value);
                _referenceType = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ReferenceType));
            }
        }

        /// <summary>
        /// 模型
        /// </summary>
        [DataMember, JsonProperty("ModelInclude", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _modelInclude;

        /// <summary>
        /// 模型
        /// </summary>
        /// <remark>
        /// 模型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"模型"), Description("模型")]
        public string ModelInclude
        {
            get => _modelInclude ?? Entity.ModelInclude;
            set
            {
                if (_modelInclude == value)
                    return;
                BeforePropertyChanged(nameof(ModelInclude), _modelInclude, value);
                _modelInclude = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ModelInclude));
            }
        }

        /// <summary>
        /// 基类
        /// </summary>
        [DataMember, JsonProperty("ModelBase", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _modelBase;

        /// <summary>
        /// 基类
        /// </summary>
        /// <remark>
        /// 模型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"基类"), Description("模型")]
        public string ModelBase
        {
            get => _modelBase ?? Entity.ModelBase;
            set
            {
                if (_modelBase == value)
                    return;
                BeforePropertyChanged(nameof(ModelBase), _modelBase, value);
                _modelBase = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ModelBase));
            }
        }

        /// <summary>
        /// 数据版本
        /// </summary>
        [DataMember, JsonProperty("_dataVersion", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _dataVersion;

        /// <summary>
        /// 数据版本
        /// </summary>
        /// <remark>
        /// 数据版本
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"数据版本"), Description("数据版本")]
        public int DataVersion
        {
            get => _dataVersion;
            set
            {
                if (_dataVersion == value)
                    return;
                BeforePropertyChanged(nameof(DataVersion), _dataVersion, value);
                _dataVersion = value;
                OnPropertyChanged(nameof(DataVersion));
            }
        }

        /// <summary>
        /// 内部数据
        /// </summary>
        [DataMember, JsonProperty("_isInternal", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isInternal;

        /// <summary>
        /// 内部数据
        /// </summary>
        /// <remark>
        /// 服务器内部数据,即只在服务器内部使用
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"内部数据"), Description("服务器内部数据,即只在服务器内部使用")]
        public bool IsInternal
        {
            get => _isInternal;
            set
            {
                if (_isInternal == value)
                    return;
                BeforePropertyChanged(nameof(IsInternal), _isInternal, value);
                _isInternal = value;
                OnPropertyChanged(nameof(IsInternal));
            }
        }

        /// <summary>
        /// 是否类
        /// </summary>
        [DataMember, JsonProperty("noDataBase", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _noDataBase;

        /// <summary>
        /// 无数据库支持
        /// </summary>
        /// <remark>
        /// 无数据库支持
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"无数据库支持"), Description("无数据库支持")]
        public bool NoDataBase
        {
            get => _noDataBase;
            set
            {
                if (_noDataBase == value)
                    return;
                BeforePropertyChanged(nameof(NoDataBase), _noDataBase, value);
                _noDataBase = value;
                OnPropertyChanged(nameof(NoDataBase));
            }
        }

        /// <summary>
        /// 继承的接口集合
        /// </summary>
        [DataMember, JsonProperty("Interfaces", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _interfaces;

        /// <summary>
        /// 继承的接口集合
        /// </summary>
        /// <remark>
        /// 说明
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"继承的接口集合"), Description("说明")]
        public string Interfaces
        {
            get => _interfaces ?? "";
            set
            {
                if (_interfaces == value)
                    return;
                BeforePropertyChanged(nameof(Interfaces), _interfaces, value);
                _interfaces = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Interfaces));
            }
        }
        #endregion

        #region 设计器支持


        /// <summary>
        /// 列序号起始值
        /// </summary>
        [DataMember, JsonProperty("ColumnIndexStart", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _columnIndexStart;

        /// <summary>
        /// 列序号起始值
        /// </summary>
        /// <remark>
        /// 列序号起始值
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"列序号起始值"), Description("列序号起始值")]
        public int ColumnIndexStart
        {
            get => _columnIndexStart;
            set
            {
                if (_columnIndexStart == value)
                    return;
                BeforePropertyChanged(nameof(ColumnIndexStart), _columnIndexStart, value);
                _columnIndexStart = value;
                OnPropertyChanged(nameof(ColumnIndexStart));
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        /// <remark>
        /// 名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"名称"), Description("名称")]
        public string DisplayName => $"{Caption}({EntityName}:{ReadTableName}";

        /// <summary>
        /// 不同版本读数据的代码
        /// </summary>
        [DataMember, JsonProperty("_readCoreCodes", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal Dictionary<int, string> _readCoreCodes;

        /// <summary>
        /// 不同版本读数据的代码
        /// </summary>
        /// <remark>
        /// 不同版本读数据的代码
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"不同版本读数据的代码"), Description("不同版本读数据的代码")]
        public Dictionary<int, string> ReadCoreCodes
        {
            get => _readCoreCodes;
            set
            {
                if (_readCoreCodes == value)
                    return;
                BeforePropertyChanged(nameof(ReadCoreCodes), _readCoreCodes, value);
                _readCoreCodes = value;
                OnPropertyChanged(nameof(ReadCoreCodes));
            }
        }

        /// <summary>
        /// 接口定义
        /// </summary>
        [DataMember, JsonProperty("IsInterface", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isInterface;

        /// <summary>
        /// 接口定义
        /// </summary>
        /// <remark>
        /// 作为系统的接口的定义
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"接口定义"), Description("作为系统的接口的定义")]
        public bool IsInterface
        {
            get => _isInterface;
            set
            {
                if (_isInterface == value)
                    return;
                BeforePropertyChanged(nameof(IsInterface), _isInterface, value);
                _isInterface = value;
                OnPropertyChanged(nameof(IsInterface));
            }
        }

        /// <summary>
        /// 接口是否为显示实现
        /// </summary>
        [DataMember, JsonProperty("_interfaceInner", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _interfaceInner;

        /// <summary>
        /// 接口是否为显示实现
        /// </summary>
        /// <remark>
        /// 接口是否为显示实现
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"接口是否为显示实现"), Description("接口是否为显示实现")]
        public bool InterfaceInner
        {
            get => _interfaceInner;
            set
            {
                if (_interfaceInner == value)
                    return;
                BeforePropertyChanged(nameof(InterfaceInner), _interfaceInner, value);
                _interfaceInner = value;
                OnPropertyChanged(nameof(InterfaceInner));
            }
        }


        /// <summary>
        /// 生成校验代码
        /// </summary>
        [IgnoreDataMember, JsonProperty("haseValidateCode")]
        internal bool _haseValidateCode;

        /// <summary>
        /// 生成校验代码
        /// </summary>
        /// <remark>
        /// 可以任意修改任意配置的生成校验代码
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"生成校验代码")]
        public bool HaseValidateCode
        {
            get => _haseValidateCode;
            set
            {
                if (_haseValidateCode == value)
                    return;
                BeforePropertyChanged(nameof(HaseValidateCode), _haseValidateCode, value);
                _haseValidateCode = value;
                OnPropertyChanged(nameof(HaseValidateCode));
            }
        }
        #endregion

        #region 子级

        /// <summary>
        /// 字段列表
        /// </summary>
        [DataMember, JsonProperty("properties", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal ConfigCollection<PropertyConfig> _properties;

        /// <summary>
        /// 字段列表
        /// </summary>
        /// <remark>
        /// 字段列表
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"字段列表"), Description("字段列表")]
        public ConfigCollection<PropertyConfig> Properties
        {
            get
            {
                if (_properties != null)
                    return _properties;
                _properties = new ConfigCollection<PropertyConfig>();
                RaisePropertyChanged(nameof(Properties));
                return _properties;
            }
            set
            {
                if (_properties == value)
                    return;
                BeforePropertyChanged(nameof(Properties), _properties, value);
                _properties = value;
                OnPropertyChanged(nameof(Properties));
            }
        }

        IEnumerable<IFieldConfig> IEntityConfig.Properties => Properties;

        #endregion

        #region 数据库

        /// <summary>
        /// 存储表名(设计录入)的说明文字
        /// </summary>
        const string ReadTableName_Description = @"存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行";

        /// <summary>
        /// 存储表名(设计录入)
        /// </summary>
        [DataMember, JsonProperty("_tableName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _readTableName;

        /// <summary>
        /// 存储表名(设计录入)
        /// </summary>
        /// <remark>
        /// 存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"存储表名(设计录入)"), Description(ReadTableName_Description)]
        public string ReadTableName
        {
            get => _readTableName ?? Entity.ReadTableName;
            set
            {
                if (_readTableName == value)
                    return;
                BeforePropertyChanged(nameof(ReadTableName), _readTableName, value);
                _readTableName = string.IsNullOrWhiteSpace(value) || value.Equals(_readTableName, System.StringComparison.OrdinalIgnoreCase)
                    ? null
                    : value.Trim();
                OnPropertyChanged(nameof(ReadTableName));
            }
        }

        /// <summary>
        /// 存储表名
        /// </summary>
        [DataMember, JsonProperty("_saveTableName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _saveTableName;

        /// <summary>
        /// 存储表名
        /// </summary>
        /// <remark>
        /// 存储表名
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"存储表名"), Description("存储表名")]
        public string SaveTableName
        {
            get => _saveTableName ?? Entity.SaveTableName;
            set
            {
                if (_saveTableName == value)
                    return;
                BeforePropertyChanged(nameof(SaveTableName), _saveTableName, value);
                _saveTableName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(SaveTableName));
            }
        }

        /// <summary>
        /// 数据库编号
        /// </summary>
        [DataMember, JsonProperty("_dbIndex", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _dbIndex;

        /// <summary>
        /// 数据库编号
        /// </summary>
        /// <remark>
        /// 数据库编号
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"数据库编号"), Description("数据库编号")]
        public int DbIndex
        {
            get => _dbIndex;
            set
            {
                if (_dbIndex == value)
                    return;
                BeforePropertyChanged(nameof(DbIndex), _dbIndex, value);
                _dbIndex = value;
                OnPropertyChanged(nameof(DbIndex));
            }
        }

        /// <summary>
        /// 按修改更新
        /// </summary>
        [DataMember, JsonProperty("UpdateByModified", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _updateByModified;

        /// <summary>
        /// 按修改更新
        /// </summary>
        /// <remark>
        /// 按修改更新
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据库"), DisplayName(@"按修改更新"), Description("按修改更新")]
        public bool UpdateByModified
        {
            get => _updateByModified;
            set
            {
                if (_updateByModified == value)
                    return;
                BeforePropertyChanged(nameof(UpdateByModified), _updateByModified, value);
                _updateByModified = value;
                OnPropertyChanged(nameof(UpdateByModified));
            }
        }
        #endregion

        #region 用户界面

        /// <summary>
        /// 接口名称
        /// </summary>
        [DataMember, JsonProperty("_apiName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _apiName;

        /// <summary>
        /// 接口名称
        /// </summary>
        /// <remark>
        /// 接口名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"接口名称"), Description("接口名称")]
        public string ApiName
        {
            get => WorkContext.InCoderGenerating ? _apiName ?? Abbreviation : _apiName;
            set
            {
                if (_apiName == value)
                    return;
                if (value == Name)
                    value = null;
                BeforePropertyChanged(nameof(ApiName), _apiName, value);
                _apiName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ApiName));
            }
        }

        /// <summary>
        /// 是否有界面
        /// </summary>
        [DataMember, JsonProperty("haseEasyUi", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _haseEasyUi;

        /// <summary>
        /// 界面只读
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"是否有界面"), Description("是否有界面")]
        public bool HaseEasyUi
        {
            get => _haseEasyUi;
            set
            {
                if (_haseEasyUi == value)
                    return;
                BeforePropertyChanged(nameof(HaseEasyUi), _haseEasyUi, value);
                _haseEasyUi = value;
                OnPropertyChanged(nameof(HaseEasyUi));
            }
        }
        /// <summary>
        /// 界面只读
        /// </summary>
        [DataMember, JsonProperty("isUiReadOnly", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isUiReadOnly;

        /// <summary>
        /// 界面只读
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"界面只读"), Description("界面只读")]
        public bool IsUiReadOnly
        {
            get => _isUiReadOnly || _isQuery;
            set
            {
                if (_isUiReadOnly == value)
                    return;
                BeforePropertyChanged(nameof(IsUiReadOnly), _isUiReadOnly, value);
                _isUiReadOnly = value;
                OnPropertyChanged(nameof(IsUiReadOnly));
            }
        }
        /// <summary>
        /// 页面文件夹名称
        /// </summary>
        [DataMember, JsonProperty("PageFolder", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _pageFolder;

        /// <summary>
        /// 页面文件夹名称
        /// </summary>
        /// <remark>
        /// 页面文件夹名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"页面文件夹名称"), Description("页面文件夹名称")]
        public string PageFolder
        {
            get => _pageFolder;
            set
            {
                if (_pageFolder == value)
                    return;
                BeforePropertyChanged(nameof(PageFolder), _pageFolder, value);
                _pageFolder = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(PageFolder));
            }
        }


        /// <summary>
        /// 树形界面
        /// </summary>
        [DataMember, JsonProperty("TreeUi", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _treeUi;

        /// <summary>
        /// 树形界面
        /// </summary>
        /// <remark>
        /// 树形界面
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"树形界面"), Description("树形界面")]
        public bool TreeUi
        {
            get => _treeUi;
            set
            {
                if (_treeUi == value)
                    return;
                BeforePropertyChanged(nameof(TreeUi), _treeUi, value);
                _treeUi = value;
                OnPropertyChanged(nameof(TreeUi));
            }
        }

        /// <summary>
        /// 编辑页面最大化
        /// </summary>
        [DataMember, JsonProperty("MaxForm", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _maxForm;

        /// <summary>
        /// 编辑页面最大化
        /// </summary>
        /// <remark>
        /// 编辑页面最大化
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"编辑页面最大化"), Description("编辑页面最大化")]
        public bool MaxForm
        {
            get => _maxForm;
            set
            {
                if (_maxForm == value)
                    return;
                BeforePropertyChanged(nameof(MaxForm), _maxForm, value);
                _maxForm = value;
                OnPropertyChanged(nameof(MaxForm));
            }
        }

        /// <summary>
        /// 编辑页面分几列
        /// </summary>
        [DataMember, JsonProperty("FormCloumn", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _formCloumn;

        /// <summary>
        /// 编辑页面分几列
        /// </summary>
        /// <remark>
        /// 编辑页面分几列
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"编辑页面分几列"), Description("编辑页面分几列")]
        public int FormCloumn
        {
            get => _formCloumn;
            set
            {
                if (_formCloumn == value)
                    return;
                BeforePropertyChanged(nameof(FormCloumn), _formCloumn, value);
                _formCloumn = value;
                OnPropertyChanged(nameof(FormCloumn));
            }
        }

        /// <summary>
        /// 列表详细页
        /// </summary>
        [DataMember, JsonProperty("ListDetails", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _listDetails;

        /// <summary>
        /// 列表详细页
        /// </summary>
        /// <remark>
        /// 列表详细页
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"列表详细页"), Description("列表详细页")]
        public bool ListDetails
        {
            get => _listDetails;
            set
            {
                if (_listDetails == value)
                    return;
                BeforePropertyChanged(nameof(ListDetails), _listDetails, value);
                _listDetails = value;
                OnPropertyChanged(nameof(ListDetails));
            }
        }

        /// <summary>
        /// 主键正序
        /// </summary>
        [DataMember, JsonProperty("NoSort", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _noSort;

        /// <summary>
        /// 主键正序
        /// </summary>
        /// <remark>
        /// 主键正序
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"主键正序"), Description("主键正序")]
        public bool NoSort
        {
            get => _noSort;
            set
            {
                if (_noSort == value)
                    return;
                BeforePropertyChanged(nameof(NoSort), _noSort, value);
                _noSort = value;
                OnPropertyChanged(nameof(NoSort));
            }
        }

        /// <summary>
        /// 主页面类型
        /// </summary>
        [DataMember, JsonProperty("PanelType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal PanelType _panelType;

        /// <summary>
        /// 主页面类型
        /// </summary>
        /// <remark>
        /// 主页面类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"主页面类型"), Description("主页面类型")]
        public PanelType PanelType
        {
            get => _panelType;
            set
            {
                if (_panelType == value)
                    return;
                BeforePropertyChanged(nameof(PanelType), _panelType, value);
                _panelType = value;
                OnPropertyChanged(nameof(PanelType));
            }
        }
        #endregion 

        #region C++

        /// <summary>
        /// C++名称
        /// </summary>
        [DataMember, JsonProperty("CppName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _cppName;

        /// <summary>
        /// C++名称
        /// </summary>
        /// <remark>
        /// C++字段名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"C++"), DisplayName(@"C++名称"), Description("C++字段名称")]
        public string CppName
        {
            get => _cppName ?? Entity.CppName;
            set
            {
                if (Name == value)
                    value = null;
                if (_cppName == value)
                    return;
                BeforePropertyChanged(nameof(CppName), _cppName, value);
                _cppName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(CppName));
            }
        }
        #endregion

    }
}