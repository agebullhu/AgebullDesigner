using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    partial class ModelConfig
    {
        #region 子级

        /// <summary>
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Add(PropertyConfig propertyConfig)
        {
            if (Properties.Any(p => p.Name == propertyConfig.Name) || !Properties.TryAdd(propertyConfig))
                return;
            if (WorkContext.InLoding || WorkContext.InSaving || WorkContext.InRepair)
                return;
            propertyConfig.Model = this;
            propertyConfig.Identity = ++MaxIdentity;
            propertyConfig.Index = Properties.Count == 0 ? 1 : Properties.Max(p => p.Index) + 1;
            MaxIdentity = Properties.Max(p => p.Identity);
        }

        /// <summary>
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Remove(PropertyConfig propertyConfig)
        {
            Properties.Remove(propertyConfig);
        }

        /// <summary>
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Add(ReleationConfig propertyConfig)
        {
            propertyConfig.Parent = this;
            Releations.TryAdd(propertyConfig);
        }
        /// <summary>
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Remove(ReleationConfig propertyConfig)
        {
            Releations.Remove(propertyConfig);
        }
        #endregion

        #region 子级

        /// <summary>
        /// 数据关联配置
        /// </summary>
        [DataMember, JsonProperty("releations", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal ConfigCollection<ReleationConfig> _releations;

        /// <summary>
        /// 数据关联配置
        /// </summary>
        /// <remark>
        /// 数据关联配置
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"数据关联配置"), Description("数据关联配置")]
        public ConfigCollection<ReleationConfig> Releations
        {
            get
            {
                if (_releations != null)
                    return _releations;
                _releations = new ConfigCollection<ReleationConfig>(this);
                OnPropertyChanged(nameof(Releations));
                return _releations;
            }
            set
            {
                if (_releations == value)
                    return;
                BeforePropertyChanged(nameof(Releations), _releations, value);
                _releations = value;
                if (value != null)
                    value.Parent = this;
                OnPropertyChanged(nameof(Releations));
            }
        }

        /// <summary>
        /// 最终有效的属性
        /// </summary>
        public List<IPropertyConfig> LastProperties { get; set; }

        /// <summary>
        /// 公开的属性
        /// </summary>
        /// <remark>
        /// 公开的属性
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"公开的属性"), Description("公开的属性")]
        public IEnumerable<IPropertyConfig> PublishProperty => LastProperties?.Where(p => !p.NoProperty);

        /// <summary>
        /// 用户属性
        /// </summary>
        /// <remark>
        /// 用户属性
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"用户属性"), Description("用户属性")]
        public IEnumerable<IPropertyConfig> UserProperty => LastProperties?.Where(p => !p.NoProperty && !p.IsMiddleField && !p.IsSystemField);

        /// <summary>
        /// C++的属性
        /// </summary>
        /// <remark>
        /// C++的属性
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"C++的属性"), Description("C++的属性")]
        public IEnumerable<IPropertyConfig> CppProperty => LastProperties?.Where(p => !p.NoProperty && !p.IsMiddleField && !p.IsSystemField);

        /// <summary>
        /// 客户端可访问的属性
        /// </summary>
        /// <remark>
        /// 客户端可访问的属性
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"客户端可访问的属性"), Description("客户端可访问的属性")]
        public IEnumerable<IPropertyConfig> ClientProperty => LastProperties?.Where(p => p.UserSee);

        #endregion
    }
}