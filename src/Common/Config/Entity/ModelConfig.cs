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
        /// 实体名称
        /// </summary>
        [DataMember, JsonProperty("entityKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string _entityKey;

        /// <summary>
        /// 实体名称
        /// </summary>
        [JsonIgnore]
        internal EntityConfig _entity;

        /// <summary>
        /// 实体名称
        /// </summary>
        /// <remark>
        /// 实体名称
        /// </remark>
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"实体名称"), Description("实体名称")]
        public EntityConfig Entity
        {
            get => _entity ??= GlobalConfig.GetEntity(p => p.Key == _entityKey);
            set
            {
                if (value == _entity)
                    return;
                BeforePropertyChange(nameof(Entity), _entity, value);
                _entity = value;
                _entityKey = value.Key;
                OnPropertyChanged(nameof(Entity));
            }
        }

        /// <summary>
        ///     名称
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"名称")]
        public override string Name
        {
            get => _name ?? Entity?.Name;
            set => base.Name = value;
        }

        /// <summary>
        ///     标题
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"标题")]
        public override string Caption
        {
            get => _caption ?? Entity?.Caption;
            set => base.Caption = value;
        }

        /// <summary>
        ///     说明
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"说明")]
        public override string Description
        {
            get => _description ?? Entity?.Description;
            set => base.Description = value;
        }

        /// <summary>
        /// 参见
        /// </summary>
        [JsonIgnore, Category("设计支持"), DisplayName(@"参见")]
        public override string Remark
        {
            get => _remark ?? Entity?.Remark;
            set => base.Remark = value;
        }

        #endregion

        #region 数据标识

        /// <summary>
        /// 是否存在属性组合唯一值
        /// </summary>
        /// <remark>
        /// 是否存在属性组合唯一值
        /// </remark>
        [JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"是否存在属性组合唯一值"), Description("是否存在属性组合唯一值")]
        public bool IsUniqueUnion => Entity.IsUniqueUnion;

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <remark>
        /// 主键字段
        /// </remark>
        [JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"主键字段"), Description("主键字段")]
        public PropertyConfig PrimaryColumn => WorkContext.InCoderGenerating
            ? Find(p => p.Field.IsPrimaryKey && p.Field.Entity == Entity) ?? Find()
            : Find(p => p.Field.IsPrimaryKey && p.Field.Entity == Entity);

        /// <summary>
        /// 标题字段
        /// </summary>
        [JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"标题字段"), Description("标题字段")]
        public IPropertyConfig CaptionColumn => Find(p => p.IsCaption && p.Entity == Entity);

        /// <summary>
        /// 标题字段
        /// </summary>
        [JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"标题字段"), Description("标题字段")]
        public IPropertyConfig ParentColumn => Find(p => p.IsParent && p.Entity == Entity);

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <remark>
        /// 主键字段
        /// </remark>
        [JsonIgnore]
        IPropertyConfig IEntityConfig.PrimaryColumn => PrimaryColumn;

        /// <summary>
        /// 是否有主键
        /// </summary>
        [JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"是否有主键"), Description("是否有主键")]
        public bool HasePrimaryKey => Entity.HasePrimaryKey;

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <remark>
        /// 主键字段
        /// </remark>
        [JsonIgnore]
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
        [JsonIgnore]
        [Category(@"数据标识"), DisplayName(@"Redis唯一键模板"), Description("保存在Redis中使用的键模板")]
        public string RedisKey
        {
            get => _redisKey ?? Entity.RedisKey;
            set
            {
                if (_redisKey == value)
                    return;
                BeforePropertyChange(nameof(RedisKey), _redisKey, value);
                _redisKey = value.SafeTrim();
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
        [JsonIgnore]
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
                BeforePropertyChange(nameof(EntityName), _entityName, value);
                _entityName = value.SafeTrim();
                OnPropertyChanged(nameof(EntityName));
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
        [JsonIgnore]
        [Category(@"模型设计(C#)"), DisplayName(@"参考类型(C#)"), Description("字段类型")]
        public string ReferenceType
        {
            get => _referenceType;
            set
            {
                if (_referenceType == value)
                    return;
                BeforePropertyChange(nameof(ReferenceType), _referenceType, value);
                _referenceType = value.SafeTrim();
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
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"模型"), Description("模型")]
        public string ModelInclude
        {
            get => _modelInclude ?? Entity.ModelInclude;
            set
            {
                if (_modelInclude == value)
                    return;
                BeforePropertyChange(nameof(ModelInclude), _modelInclude, value);
                _modelInclude = value.SafeTrim();
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
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"基类"), Description("模型")]
        public string ModelBase
        {
            get => _modelBase ?? Entity.ModelBase;
            set
            {
                if (_modelBase == value)
                    return;
                BeforePropertyChange(nameof(ModelBase), _modelBase, value);
                _modelBase = value.SafeTrim();
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
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"数据版本"), Description("数据版本")]
        public int DataVersion
        {
            get => _dataVersion;
            set
            {
                if (_dataVersion == value)
                    return;
                BeforePropertyChange(nameof(DataVersion), _dataVersion, value);
                _dataVersion = value;
                OnPropertyChanged(nameof(DataVersion));
            }
        }


        /// <summary>
        /// 是否关联表
        /// </summary>
        [JsonIgnore]
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
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"继承的接口集合"), Description("说明")]
        public string Interfaces
        {
            get => _interfaces ?? "";
            set
            {
                if (_interfaces == value)
                    return;
                BeforePropertyChange(nameof(Interfaces), _interfaces, value);
                _interfaces = value.SafeTrim();
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
        [JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"列序号起始值"), Description("列序号起始值")]
        public int ColumnIndexStart
        {
            get => _columnIndexStart;
            set
            {
                if (_columnIndexStart == value)
                    return;
                BeforePropertyChange(nameof(ColumnIndexStart), _columnIndexStart, value);
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
        [JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"名称"), Description("名称")]
        public string DisplayName => $"{Caption}({EntityName}:{DataTable.ReadTableName}";

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
        [JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"不同版本读数据的代码"), Description("不同版本读数据的代码")]
        public Dictionary<int, string> ReadCoreCodes
        {
            get => _readCoreCodes;
            set
            {
                if (_readCoreCodes == value)
                    return;
                BeforePropertyChange(nameof(ReadCoreCodes), _readCoreCodes, value);
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
        [JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"接口定义"), Description("作为系统的接口的定义")]
        public bool IsInterface
        {
            get => _isInterface;
            set
            {
                if (_isInterface == value)
                    return;
                BeforePropertyChange(nameof(IsInterface), _isInterface, value);
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
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"字段列表"), Description("字段列表")]
        public ConfigCollection<PropertyConfig> Properties
        {
            get
            {
                if (_properties != null)
                    return _properties;
                _properties = new ConfigCollection<PropertyConfig>(this, nameof(Properties));
                RaisePropertyChanged(nameof(Properties));
                return _properties;
            }
            set
            {
                if (_properties == value)
                    return;
                BeforePropertyChange(nameof(Properties), _properties, value);
                _properties = value;
                if (value != null)
                {
                    value.Name = nameof(Properties);
                    value.Parent = this;
                }
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
            return Properties.FirstOrDefault(p =>
                name.IsOnce(p.Name,p.DataBaseField?.DbFieldName));
        }
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public PropertyConfig Find(Func<PropertyConfig, bool> filter)
        {
            return Properties.FirstOrDefault(filter);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PropertyConfig Find(params string[] names)
        {
            return Properties.FirstOrDefault(p => names.Exist(p.Name, p.DataBaseField?.DbFieldName));
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
            return Properties.Where(filter).ToArray();
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<PropertyConfig> Where(Func<PropertyConfig, bool> filter)
        {
            return Properties.Where(filter);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool TryGet(out PropertyConfig field, params string[] names)
        {
            field = Properties.FirstOrDefault(p => names.Exist(p.Name, p.DataBaseField?.DbFieldName));
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

    }
}