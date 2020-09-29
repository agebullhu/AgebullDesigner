using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

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
            foreach (var item in Commands)
                action(item);
        }

        /// <summary>
        /// 字段
        /// </summary>
        [DataMember, JsonProperty("_tableReleations",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal NotificationList<EntityReleationConfig> _releations;

        /// <summary>
        /// 字段
        /// </summary>
        /// <remark>
        /// 字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"字段"), Description("字段")]
        public NotificationList<EntityReleationConfig> Releations
        {
            get
            {
                if (_releations != null)
                    return _releations;
                _releations = new NotificationList<EntityReleationConfig>();
                OnPropertyChanged(nameof(Releations));
                return _releations;
            }
            set
            {
                if (_releations == value)
                    return;
                BeforePropertyChanged(nameof(Releations), _releations, value);
                _releations = value;
                OnPropertyChanged(nameof(Releations));
            }
        }
        /// <summary>
        /// 命令集合
        /// </summary>
        [DataMember, JsonProperty("_commands",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal NotificationList<UserCommandConfig> _commands;

        /// <summary>
        /// 命令集合
        /// </summary>
        /// <remark>
        /// 命令集合,数据模型中可调用的命令
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"业务模型"), DisplayName(@"命令集合"), Description("命令集合,数据模型中可调用的命令")]
        public NotificationList<UserCommandConfig> Commands
        {
            get
            {
                if (_commands != null)
                    return _commands;
                _commands = new NotificationList<UserCommandConfig>();
                RaisePropertyChanged(nameof(Commands));
                return _commands;
            }
            set
            {
                if (_commands == value)
                    return;
                BeforePropertyChanged(nameof(Commands), _commands, value);
                _commands = value;
                OnPropertyChanged(nameof(Commands));
            }
        }
        /// <summary>
        /// 最终有效的属性
        /// </summary>
        public List<FieldConfig> LastProperties { get; set; }

        /// <summary>
        /// 公开的属性
        /// </summary>
        /// <remark>
        /// 公开的属性
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"公开的属性"), Description("公开的属性")]
        public IEnumerable<FieldConfig> PublishProperty => LastProperties?.Where(p => !p.DbInnerField);

        /// <summary>
        /// C++的属性
        /// </summary>
        /// <remark>
        /// C++的属性
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"C++的属性"), Description("C++的属性")]
        public IEnumerable<FieldConfig> CppProperty => LastProperties?.Where(p => !p.IsMiddleField && !p.DbInnerField && !p.IsSystemField);

        /// <summary>
        /// 客户端可访问的属性
        /// </summary>
        /// <remark>
        /// 客户端可访问的属性
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"客户端可访问的属性"), Description("客户端可访问的属性")]
        public IEnumerable<FieldConfig> ClientProperty => LastProperties?.Where(p => !p.DenyClient && !p.DbInnerField);

        /// <summary>
        /// 客户端可访问的属性
        /// </summary>
        /// <remark>
        /// 客户端可访问的属性
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"客户端可访问的属性"), Description("客户端可访问的属性")]
        public IEnumerable<FieldConfig> UserProperty => LastProperties?.Where(p => !p.DenyClient && !p.DbInnerField && !p.IsSystemField);

        /// <summary>
        /// 数据库字段
        /// </summary>
        /// <remark>
        /// 数据库字段
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"数据库字段"), Description("数据库字段")]
        public IEnumerable<FieldConfig> DbFields => LastProperties?.Where(p => !p.NoStorage);


        #endregion
    }
}