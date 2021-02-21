using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 数据表配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class DataTableConfig : EntityExtendConfig, IDataTable
    {
        #region 设计

        /// <summary>
        /// 取文件名
        /// </summary>
        /// <returns></returns>
        public override string GetFileName() => GetFileName(_entity);

        /// <summary>
        /// 取文件名
        /// </summary>
        /// <returns></returns>
        public static string GetFileName(IEntityConfig entity) => $"{entity?.Name?.ToName('_')}.{entity?.Type}.datatable.json";

        #endregion

        #region 子级

        /// <summary>
        /// 字段列表
        /// </summary>
        [DataMember, JsonProperty("fields", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal ConfigCollection<DataBaseFieldConfig> _fields;

        /// <summary>
        /// 字段列表
        /// </summary>
        /// <remark>
        /// 字段列表
        /// </remark>
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"字段列表"), Description("字段列表")]
        public ConfigCollection<DataBaseFieldConfig> Fields
        {
            get
            {
                if (_fields != null)
                {
                    if(_fields.Parent == null)
                    {
                        _fields.OnLoad(nameof(Fields), this);
                    }
                    return _fields;
                }
                _fields = new ConfigCollection<DataBaseFieldConfig>(this,nameof(Fields));
                RaisePropertyChanged(nameof(Fields));
                return _fields;
            }
            set
            {
                if (_fields == value)
                {
                    if (_fields.Parent == null)
                    {
                        _fields.Name = nameof(Fields);
                        _fields.Parent = this;
                    }
                    return;
                }
                BeforePropertyChange(nameof(Fields), _fields, value);
                _fields = value;
                if (value != null)
                {
                    value.Name = nameof(Fields);
                    value.Parent = this;
                }
                OnPropertyChanged(nameof(Fields));
            }
        }

        /// <summary>
        /// 标题字段
        /// </summary>
        /// <returns></returns>
        public DataBaseFieldConfig CaptionField => Entity.CaptionColumn == null ? null : Fields.FirstOrDefault(p => p.Property == Entity.CaptionColumn || p.Property.Field == Entity.CaptionColumn.Field);

        /// <summary>
        /// 主键字段
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public DataBaseFieldConfig PrimaryField => Fields.FirstOrDefault(p => p.Property == Entity.PrimaryColumn || p.Property.Field == Entity.PrimaryColumn.Field);

        /// <summary>
        /// 取字段
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public DataBaseFieldConfig this[IPropertyConfig property] => Fields.FirstOrDefault(p => p.Property == property || p.Property.Field == property.Field);

        /// <summary>
        /// 查找字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void Add(DataBaseFieldConfig field)
        {
            if (!Fields.Any(p => p.Property == field.Property))
            {
                Fields.Add(field);
            }
        }

        /// <summary>
        /// 查找字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataBaseFieldConfig Find(string name)
        {
            return Fields.FirstOrDefault(p => name.IsMe(p.Name) || name.IsMe(p.DbFieldName));
        }

        /// <summary>
        /// 查找字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataBaseFieldConfig FindLast(string name)
        {
            return Fields.FirstOrDefault(p => p.IsActive && !p.NoStorage && name.IsOnce(p.Name,p.DbFieldName));
        }

        /// <summary>
        /// 查找字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataBaseFieldConfig Find(Func<DataBaseFieldConfig, bool> filter)
        {
            return Fields.FirstOrDefault(filter);
        }

        /// <summary>
        /// 查找字段（排除暂存的删除或废弃内容）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataBaseFieldConfig FindLast(Func<DataBaseFieldConfig, bool> filter)
        {
            return Fields.FirstOrDefault(p => p.IsActive && !p.NoStorage && filter(p));
        }

        /// <summary>
        /// 查找字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataBaseFieldConfig[] FindAndToArray(Func<DataBaseFieldConfig, bool> filter)
        {
            return Fields.Where(filter).ToArray();
        }

        /// <summary>
        /// 查找字段（排除暂存的删除或废弃内容）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataBaseFieldConfig[] FindLastAndToArray(Func<DataBaseFieldConfig, bool> filter)
        {
            return Fields.Where(p => p.IsActive && !p.NoStorage && filter(p)).ToArray();
        }

        /// <summary>
        /// 查找字段（排除暂存的删除或废弃内容）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<DataBaseFieldConfig> Last()
        {
            return Fields.Where(p => p.IsActive && !p.NoStorage);
        }
        /// <summary>
        /// 查找字段（排除暂存的删除或废弃内容）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<DataBaseFieldConfig> WhereLast(Func<DataBaseFieldConfig, bool> filter)
        {
            return Fields.Where(p => p.IsActive && !p.NoStorage && filter(p));
        }

        /// <summary>
        /// 查找字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<DataBaseFieldConfig> Where(Func<DataBaseFieldConfig, bool> filter)
        {
            return Fields.Where(filter);
        }

        /// <summary>
        /// 最终是否存在（排除暂存的删除或废弃内容）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool AnyLast(Func<DataBaseFieldConfig, bool> filter)
        {
            return Fields.Any(p => p.IsActive && filter(p));
        }

        /// <summary>
        /// 查找字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Any(Func<DataBaseFieldConfig, bool> filter)
        {
            return Fields.Any(p => filter(p));
        }

        /// <summary>
        /// 查找字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataBaseFieldConfig Find(params string[] names)
        {
            return Fields.FirstOrDefault(p => names.Exist(p.Name, p.DbFieldName));
        }

        /// <summary>
        /// 查找字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool TryGet(Func<DataBaseFieldConfig, bool> filter, out DataBaseFieldConfig field)
        {
            field = Fields.FirstOrDefault(filter);
            return field != null;
        }


        /// <summary>
        /// 查找字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool TryGet(out DataBaseFieldConfig field, params string[] names)
        {
            field = Fields.FirstOrDefault(p => names.Exist(p.Name, p.DbFieldName));
            return field != null;
        }

        /// <summary>
        /// 查找字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exist(params string[] names)
        {
            return Fields.Any(p => names.Exist(p.Name, p.DbFieldName));
        }

        #endregion

        #region 字段

        /// <summary>
        /// 存储表名
        /// </summary>/// <remarks>
        /// 存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行
        /// </remarks>
        [DataMember, JsonProperty("readTableName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _readTableName;

        /// <summary>
        /// 存储表名
        /// </summary>/// <remarks>
        /// 存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行
        /// </remarks>
        [JsonIgnore]
        [DisplayName(@"存储表名"), Description(@"存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行")]
        public string ReadTableName
        {
            get => _readTableName;
            set
            {
                if (_readTableName == value)
                    return;
                BeforePropertyChange(nameof(ReadTableName), _readTableName, value);
                _readTableName = value;
                OnPropertyChanged(nameof(ReadTableName));
            }
        }

        /// <summary>
        /// 存储表名
        /// </summary>
        [DataMember, JsonProperty("saveTableName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _saveTableName;

        /// <summary>
        /// 存储表名
        /// </summary>
        [JsonIgnore]
        [DisplayName(@"存储表名"), Description(@"存储表名")]
        public string SaveTableName
        {
            get => _saveTableName;
            set
            {
                if (_saveTableName == value)
                    return;
                BeforePropertyChange(nameof(SaveTableName), _saveTableName, value);
                _saveTableName = value;
                OnPropertyChanged(nameof(SaveTableName));
            }
        }

        /// <summary>
        /// 数据库编号
        /// </summary>
        [DataMember, JsonProperty("dbIndex", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _dbIndex;

        /// <summary>
        /// 数据库编号
        /// </summary>
        [JsonIgnore]
        [DisplayName(@"数据库编号"), Description(@"数据库编号")]
        public int DbIndex
        {
            get => _dbIndex;
            set
            {
                if (_dbIndex == value)
                    return;
                BeforePropertyChange(nameof(DbIndex), _dbIndex, value);
                _dbIndex = value;
                OnPropertyChanged(nameof(DbIndex));
            }
        }

        /// <summary>
        /// 按修改更新
        /// </summary>
        [DataMember, JsonProperty("updateByModified", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _updateByModified;

        /// <summary>
        /// 按修改更新
        /// </summary>
        [JsonIgnore]
        [DisplayName(@"按修改更新"), Description(@"按修改更新")]
        public bool UpdateByModified
        {
            get => _updateByModified;
            set
            {
                if (_updateByModified == value)
                    return;
                BeforePropertyChange(nameof(UpdateByModified), _updateByModified, value);
                _updateByModified = value;
                OnPropertyChanged(nameof(UpdateByModified));
            }
        }

        /// <summary>
        /// 是否视图
        /// </summary>
        [DataMember, JsonProperty("isView", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isView;

        /// <summary>
        /// 是否视图
        /// </summary>
        [JsonIgnore]
        [DisplayName(@"是否视图"), Description(@"是否视图")]
        public bool IsView
        {
            get => _isView;
            set
            {
                if (_isView == value)
                    return;
                BeforePropertyChange(nameof(IsView), _isView, value);
                _isView = value;
                OnPropertyChanged(nameof(IsView));
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
        [JsonIgnore]
        [DisplayName(@"是否查询"), Description(@"是否查询")]
        public bool IsQuery
        {
            get => _isQuery;
            set
            {
                if (_isQuery == value)
                    return;
                BeforePropertyChange(nameof(IsQuery), _isQuery, value);
                _isQuery = value;
                OnPropertyChanged(nameof(IsQuery));
            }
        }

        /// <summary>
        /// 启用数据事件
        /// </summary>
        [DataMember, JsonProperty("enableDataEvent", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _enableDataEvent;

        /// <summary>
        /// 启用数据事件
        /// </summary>
        [JsonIgnore]
        [DisplayName(@"启用数据事件"), Description(@"启用数据事件")]
        public bool EnableDataEvent
        {
            get => _enableDataEvent;
            set
            {
                if (_enableDataEvent == value)
                    return;
                BeforePropertyChange(nameof(EnableDataEvent), _enableDataEvent, value);
                _enableDataEvent = value;
                OnPropertyChanged(nameof(EnableDataEvent));
            }
        }

        #endregion 字段

        #region 兼容性升级

        /// <summary>
        /// 兼容性升级
        /// </summary>
        public static DataTableConfig Create(IEntityConfig entity)
        {
            var table = new DataTableConfig
            {
                Entity = entity
            };
            table.Upgrade();
            return table;
        }
        /// <summary>
        /// 兼容性升级
        /// </summary>
        public void Upgrade()
        {
            if (Entity == null)
                return;
            Copy((ConfigBase)Entity);
        }

        #endregion

        #region 字段复制

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is DataTableConfig cfg)
                CopyProperty(cfg);
            if (dest is EntityConfig entity)
                CopyProperty(entity);
            if (dest is ModelConfig model)
                CopyProperty(model);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(DataTableConfig dest)
        {
            ReadTableName = dest.ReadTableName;
            SaveTableName = dest.SaveTableName;
            DbIndex = dest.DbIndex;
            UpdateByModified = dest.UpdateByModified;
            IsView = dest.IsView;
            IsQuery = dest.IsQuery;
            EnableDataEvent = dest.EnableDataEvent;
            _fields = new ConfigCollection<DataBaseFieldConfig>(this, nameof(Fields));
            if (dest is DataTableConfig dataTable)
                foreach (var field in dataTable.Fields)
                {
                    var uiField = new DataBaseFieldConfig
                    {
                        Parent = this,
                        Property = field.Property
                    };
                    uiField.Copy(field);
                    _fields.Add(uiField);
                }
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(EntityConfigBase dest)
        {
            ReadTableName = dest._readTableName;
            SaveTableName = dest._saveTableName;
            DbIndex = dest._dbIndex;
            UpdateByModified = dest._updateByModified;
            IsView = dest.IsView;
            IsQuery = dest.IsQuery;
            EnableDataEvent = dest.EnableDataEvent;
            _fields = new ConfigCollection<DataBaseFieldConfig>(this, nameof(Fields));
            foreach (var property in dest.IEntity.Properties)
            {
                var field = new DataBaseFieldConfig
                {
                    Parent = this,
                    Property = property
                };
                field.Copy(property.Field);
                property.DataBaseField = field;
                _fields.Add(field);
            }
        }
        #endregion 字段复制

    }
}