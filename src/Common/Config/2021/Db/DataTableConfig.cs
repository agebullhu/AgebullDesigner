using Newtonsoft.Json;
using System;
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
        public static string GetFileName(IEntityConfig entity) => entity?.Name.Trim().Replace(' ', '_').Replace('>', '_').Replace('<', '_') + ".datatable.json";

        #endregion

        #region IDataTable

        /// <summary>
        /// 存储表名(设计录入)
        /// </summary>/// <remarks>
        /// 存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行
        /// </remarks>
        [DataMember, JsonProperty("readTableName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _readTableName;

        /// <summary>
        /// 存储表名(设计录入)
        /// </summary>/// <remarks>
        /// 存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行
        /// </remarks>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"存储表名(设计录入)"), Description(@"存储表名,即实体对应的数据库表.因为模型可能直接使用视图,但增删改还在基础的表中时行,而不在视图中时行")]
        public string ReadTableName
        {
            get => _readTableName;
            set
            {
                if (_readTableName == value)
                    return;
                BeforePropertyChanged(nameof(ReadTableName), _readTableName, value);
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
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"存储表名"), Description(@"存储表名")]
        public string SaveTableName
        {
            get => _saveTableName;
            set
            {
                if (_saveTableName == value)
                    return;
                BeforePropertyChanged(nameof(SaveTableName), _saveTableName, value);
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
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"数据库编号"), Description(@"数据库编号")]
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
        [DataMember, JsonProperty("updateByModified", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _updateByModified;

        /// <summary>
        /// 按修改更新
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(@"按修改更新"), Description(@"按修改更新")]
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
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"字段列表"), Description("字段列表")]
        public ConfigCollection<DataBaseFieldConfig> Fields
        {
            get
            {
                if (_fields != null)
                    return _fields;
                _fields = new ConfigCollection<DataBaseFieldConfig>();
                RaisePropertyChanged(nameof(Fields));
                return _fields;
            }
            set
            {
                if (_fields == value)
                    return;
                BeforePropertyChanged(nameof(Fields), _fields, value);
                _fields = value;
                OnPropertyChanged(nameof(Fields));
            }
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void Add(DataBaseFieldConfig field)
        {
            if (!Fields.Any(p => p.Property == field.Property))
            {
                field.Parent = this;
                Fields.Add(field);
            }
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataBaseFieldConfig Find(string name)
        {
            return Fields.FirstOrDefault(p => name.IsMe(p.Property.Name));
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataBaseFieldConfig Find(params string[] names)
        {
            return Fields.FirstOrDefault(p => names.Exist(p.Property.Name));
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool TryGet(out DataBaseFieldConfig field, params string[] names)
        {
            field = Fields.FirstOrDefault(p => names.Exist(p.Property.Name));
            return field != null;
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exist(params string[] names)
        {
            return Fields.Any(p => names.Exist(p.Property.Name));
        }

        #endregion

        #region 兼容性升级

        /// <summary>
        /// 兼容性升级
        /// </summary>
        public void Upgrade()
        {
            Copy(Entity);
            _fields = new ConfigCollection<DataBaseFieldConfig>();
            foreach (var field in Entity.Properties)
            {
                var uiField = new DataBaseFieldConfig
                {
                    Property = field
                };
                uiField.Copy(field as SimpleConfig);
                _fields.Add(uiField);
            }
        }

        #endregion

        #region 复制

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        protected override void CopyFrom(SimpleConfig dest)
        {
            base.CopyFrom(dest);
            if (dest is IDataTable cfg)
                CopyProperty(cfg);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        public void Copy(IDataTable dest)
        {
            Copy((SimpleConfig)dest);
        }

        /// <summary>
        /// 字段复制
        /// </summary>
        /// <param name="dest">复制源</param>
        /// <returns></returns>
        public void CopyProperty(IDataTable dest)
        {
            ReadTableName = dest.ReadTableName;
            SaveTableName = dest.SaveTableName;
            DbIndex = dest.DbIndex;
            UpdateByModified = dest.UpdateByModified;
            _fields = new ConfigCollection<DataBaseFieldConfig>();
            if (dest is DataTableConfig dataTable)
                foreach (var field in dataTable.Fields)
                {
                    var uiField = new DataBaseFieldConfig();
                    uiField.Copy(field);
                    _fields.Add(uiField);
                }
        }
        #endregion
    }
}