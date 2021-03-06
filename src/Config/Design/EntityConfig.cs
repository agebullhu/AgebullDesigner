using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    public sealed partial class EntityConfig
    {
        #region 子级
        /// <summary>
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Add(PropertyConfig propertyConfig)
        {
            if (Properties.Any(p=>p.Name== propertyConfig.Name) ||!Properties.TryAdd(propertyConfig))
                return;
            propertyConfig.Parent = this;
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
        public void Add(EntityReleationConfig propertyConfig)
        {
            propertyConfig.Parent = this;
            Releations.TryAdd(propertyConfig);
        }
        /// <summary>
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Add(UserCommandConfig propertyConfig)
        {
            propertyConfig.Parent = this;
            Commands.TryAdd(propertyConfig);
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
        public void Remove(UserCommandConfig propertyConfig)
        {
            Commands.Remove(propertyConfig);
        }

        /// <summary>
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Remove(EntityReleationConfig propertyConfig)
        {
            Releations.Remove(propertyConfig);
        }
        #endregion

        #region 子级

        /// <summary>
        /// 遍历子级
        /// </summary>
        public override void ForeachChild(Action<ConfigBase> action)
        {
            if (WorkContext.InCoderGenerating)
            {
                foreach (var item in LastProperties)
                    action(item);
            }
            else
            {
                foreach (var item in Properties)
                    action(item);
            }
        }

        /// <summary>
        /// 最终有效的属性
        /// </summary>
        public List<PropertyConfig> LastProperties { get; set; }

        /// <summary>
        /// 开始代码生成
        /// </summary>
        public void CreateLast()
        {
            LastProperties = new List<PropertyConfig>();
            int idx = 0;
            foreach (var pro in Properties.OrderBy(p => p.Index))
            {
                if (pro.IsDelete || pro.IsDiscard)
                    continue;
                pro.Option.Index = ++idx;
                LastProperties.Add(pro);
            }
        }

        /// <summary>
        /// 公开的属性
        /// </summary>
        /// <remark>
        /// 公开的属性
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"公开的属性"), Description("公开的属性")]
        public IEnumerable<PropertyConfig> PublishProperty => LastProperties?.Where(p => !p.DbInnerField);

        /// <summary>
        /// C++的属性
        /// </summary>
        /// <remark>
        /// C++的属性
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"C++的属性"), Description("C++的属性")]
        public IEnumerable<PropertyConfig> CppProperty => LastProperties?.Where(p => !p.IsMiddleField && !p.DbInnerField && !p.IsSystemField);

        /// <summary>
        /// 客户端可访问的属性
        /// </summary>
        /// <remark>
        /// 客户端可访问的属性
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"客户端可访问的属性"), Description("客户端可访问的属性")]
        public IEnumerable<PropertyConfig> ClientProperty => LastProperties?.Where(p => !p.DenyClient && !p.DbInnerField);

        /// <summary>
        /// 客户端可访问的属性
        /// </summary>
        /// <remark>
        /// 客户端可访问的属性
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"客户端可访问的属性"), Description("客户端可访问的属性")]
        public IEnumerable<PropertyConfig> UserProperty => LastProperties?.Where(p => !p.DenyClient && !p.DbInnerField && !p.IsSystemField);

        /// <summary>
        /// 数据库字段
        /// </summary>
        /// <remark>
        /// 数据库字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"数据库字段"), Description("数据库字段")]
        public IEnumerable<PropertyConfig> DbFields => LastProperties?.Where(p => !p.NoStorage);


        #endregion
    }
}