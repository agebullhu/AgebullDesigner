using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Agebull.EntityModel.Config.V2021
{
    /// <summary>
    /// 数据库字段
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
    public partial class CommandConfig : EntityExtendConfig
    {
        #region 子级

        /// <summary>
        /// 字段列表
        /// </summary>
        [DataMember, JsonProperty("properties", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal ConfigCollection<DataBaseFieldConfig> _properties;

        /// <summary>
        /// 字段列表
        /// </summary>
        /// <remark>
        /// 字段列表
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"字段列表"), Description("字段列表")]
        public ConfigCollection<DataBaseFieldConfig> Properties
        {
            get
            {
                if (_properties != null)
                    return _properties;
                _properties = new ConfigCollection<DataBaseFieldConfig>();
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

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void Add(DataBaseFieldConfig field)
        {
            if (!Properties.Any(p => p.Property == field.Property))
            {
                field.Parent = this;
                Properties.Add(field);
            }
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataBaseFieldConfig Find(string name)
        {
            return Properties.FirstOrDefault(p => name.IsMe(p.Name) || name.IsMe(p.DbFieldName));
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataBaseFieldConfig Find(params string[] names)
        {
            return Properties.FirstOrDefault(p => names.Exist(p.Name, p.DbFieldName));
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool TryGet(out DataBaseFieldConfig field, params string[] names)
        {
            field = Properties.FirstOrDefault(p => names.Exist(p.Name, p.DbFieldName));
            return field != null;
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exist(params string[] names)
        {
            return Properties.Any(p => names.Exist(p.Name, p.DbFieldName));
        }

        #endregion

        #region 兼容性升级

        /// <summary>
        /// 兼容性升级
        /// </summary>
        public void Upgrade()
        {
            Copy((ConfigBase)Entity);
            _properties = new ConfigCollection<DataBaseFieldConfig>();
            foreach (var field in Entity.Properties)
            {
                var uiField = new DataBaseFieldConfig
                {
                    Property = field
                };
                uiField.Copy(field as SimpleConfig);
                _properties.Add(uiField);
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
            _properties = new ConfigCollection<DataBaseFieldConfig>();
            if (dest is DataTableConfig dataTable)
                foreach (var field in dataTable.Fields)
                {
                    var uiField = new DataBaseFieldConfig();
                    uiField.Copy(field);
                    _properties.Add(uiField);
                }
        }
        #endregion
    }
}
