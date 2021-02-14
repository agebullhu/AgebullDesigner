using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    public sealed partial class EntityConfig
    {
        #region 子级
        /// <summary>
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Add(FieldConfig propertyConfig)
        {
            if (Properties.Any(p => p.Name == propertyConfig.Name) || !Properties.TryAdd(propertyConfig))
                return;
            propertyConfig.Entity = this;
            if (WorkContext.InLoding || WorkContext.InSaving || WorkContext.InRepair)
                return;
            propertyConfig.Identity = ++MaxIdentity;
            propertyConfig.Index = Properties.Count == 0 ? 1 : Properties.Max(p => p.Index) + 1;
            MaxIdentity = Properties.Max(p => p.Identity);
        }

        /// <summary>
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Remove(FieldConfig propertyConfig)
        {
            Properties.Remove(propertyConfig);
        }

        #endregion

        #region 子级

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