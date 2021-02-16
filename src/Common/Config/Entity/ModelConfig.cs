﻿/*design by:agebull designer date:2017/7/12 23:16:38*/
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
using System.Text;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 实体配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ModelConfig : EntityConfigBase, IEntityConfig
    {
        #region 系统

        /// <summary>
        /// 类型
        /// </summary>
        public string Type => "model";

        /// <summary>
        /// 表示自己的后期实现
        /// </summary>
        protected sealed override IEntityConfig IEntity => this;

        /// <summary>
        /// 构造
        /// </summary>
        public ModelConfig()
        {
            _desingSwitch = 0xFFFF;
        }

        /// <summary>
        /// 实体名称
        /// </summary>
        [DataMember, JsonProperty("entityKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string _entityKey;

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
        public bool IsUniqueUnion => Entity.IsUniqueUnion;

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <remark>
        /// 主键字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"主键字段"), Description("主键字段")]
        public PropertyConfig PrimaryColumn => WorkContext.InCoderGenerating
            ? Find(p => p.Field.IsPrimaryKey && p.Field.Entity == Entity) ?? Find()
            : Find(p => p.Field.IsPrimaryKey && p.Field.Entity == Entity);

        /// <summary>
        /// 标题字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"标题字段"), Description("标题字段")]
        public IPropertyConfig CaptionColumn => Find(p => p.IsCaption && p.Entity == Entity);

        /// <summary>
        /// 标题字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"标题字段"), Description("标题字段")]
        public IPropertyConfig ParentColumn => Find(p => p.IsParent && p.Entity == Entity);

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <remark>
        /// 主键字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        IPropertyConfig IEntityConfig.PrimaryColumn => PrimaryColumn;

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
        /// 是否关联表
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"是否关联表")]
        public bool IsLinkTable
        {
            get => Entity.IsLinkTable;
            set => Entity.IsLinkTable = value;
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
                _properties = new ConfigCollection<PropertyConfig>(this);
                RaisePropertyChanged(nameof(Properties));
                return _properties;
            }
            set
            {
                if (_properties == value)
                    return;
                BeforePropertyChanged(nameof(Properties), _properties, value);
                _properties = value;
                if (value != null)
                    value.Parent = this;
                OnPropertyChanged(nameof(Properties));
            }
        }

        IEnumerable<IPropertyConfig> IEntityConfig.Properties => Properties;

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PropertyConfig Find(string name)
        {
            return Find(p =>
                name.Equals(p.Name, StringComparison.OrdinalIgnoreCase) ||
                name.Equals(p.DataBaseField?.DbFieldName, StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public PropertyConfig Find(Func<PropertyConfig, bool> filter)
        {
            return Find(filter);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PropertyConfig Find(params string[] names)
        {
            return Find(p => names.Exist(p.Name, p.DataBaseField?.DbFieldName));
        }

        public bool Exist(Func<PropertyConfig, bool> filter)
        {
            return Properties.Any(filter);
        }
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IPropertyConfig[] FindLastAndToArray(Func<IPropertyConfig, bool> filter)
        {
            return LastProperties.Where(filter).ToArray();
        }

        public bool ExistLast(Func<IPropertyConfig, bool> filter)
        {
            return LastProperties.Any(filter);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<IPropertyConfig> WhereLast(Func<IPropertyConfig, bool> filter)
        {
            return LastProperties.Where(filter);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PropertyConfig[] FindAndToArray(Func<PropertyConfig, bool> filter)
        {
            return Where(filter).ToArray();
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<PropertyConfig> Where(Func<PropertyConfig, bool> filter)
        {
            return Where(filter);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool TryGet(out PropertyConfig field, params string[] names)
        {
            field = Find(p => names.Exist(p.Name, p.DataBaseField?.DbFieldName));
            return field != null;
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exist(params string[] names)
        {
            return Properties.Any(p => names.Exist(p.Name, p.DataBaseField?.DbFieldName));
        }
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

    }
}