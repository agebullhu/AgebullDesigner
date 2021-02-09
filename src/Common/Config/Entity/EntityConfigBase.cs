using Agebull.EntityModel.Config.V2021;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置基础
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class EntityConfigBase : ProjectChildConfigBase
    {
        /// <summary>
        /// 表示自己的后期实现
        /// </summary>
        protected abstract IEntityConfig Me { get; }

        #region 视角开关

        /// <summary>
        /// 设计视角组合存储对象
        /// </summary>
        [DataMember, JsonProperty("desingSwitch")]
        internal int _desingSwitch;

        void SetDesingSwitch(int value, bool enable) => _desingSwitch = enable ? _desingSwitch | value : _desingSwitch & ~value;

        /// <summary>
        /// 启用数据校验
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"视角开关"), DisplayName(@"启用数据校验"), Description("启用数据校验")]
        public bool EnableValidate
        {
            get => (_desingSwitch & 0x1) == 0x1;
            set
            {
                BeforePropertyChanged(nameof(EnableValidate), _parent, value);
                SetDesingSwitch(0x1, value);
                OnPropertyChanged(nameof(EnableValidate));
            }
        }

        /// <summary>
        /// 启用数据事件
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"视角开关"), DisplayName(@"启用数据事件"), Description("启用数据事件")]
        public bool EnableDataEvent
        {
            get => (_desingSwitch & 0x2) == 0x2;
            set
            {
                BeforePropertyChanged(nameof(EnableDataEvent), _parent, value);
                SetDesingSwitch(0x2, value);
                OnPropertyChanged(nameof(EnableDataEvent));
            }
        }

        /// <summary>
        /// 启用数据库
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"视角开关"), DisplayName(@"启用数据库"), Description("启用数据库")]
        public bool EnableDataBase
        {
            get => (_desingSwitch & 0x4) == 0x4;
            set
            {
                BeforePropertyChanged(nameof(EnableDataBase), _parent, value);
                SetDesingSwitch(0x4, value);
                OnPropertyChanged(nameof(EnableDataBase));
            }
        }
        /// <summary>
        /// 启用编辑API
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"视角开关"), DisplayName(@"启用编辑接口"), Description("启用编辑接口")]
        public bool EnableEditApi
        {
            get => (_desingSwitch & 0x8) == 0x8;
            set
            {
                BeforePropertyChanged(nameof(EnableEditApi), _parent, value);
                SetDesingSwitch(0x8, value);
                OnPropertyChanged(nameof(EnableEditApi));
            }
        }

        /// <summary>
        /// 启用用户界面
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"视角开关"), DisplayName(@"启用用户界面"), Description("启用用户界面")]
        public bool EnableUI
        {
            get => (_desingSwitch & 0x10) == 0x10;
            set
            {
                BeforePropertyChanged(nameof(EnableUI), _parent, value);
                SetDesingSwitch(0x10, value);
                OnPropertyChanged(nameof(EnableUI));
            }
        }

        #endregion

        #region V2021
        /*
        /// <summary>
        /// 页面配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal PageConfig _page;

        /// <summary>
        /// 页面配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"扩展对象"), DisplayName(@"页面配置"), Description("页面配置")]
        public PageConfig Page
        {
            get => _page;
            set
            {
                if (_page == value)
                    return;
                BeforePropertyChanged(nameof(Page), _page, value);
                _page = value;
                if (_page != null)
                    _page.Entity = Me;
                OnPropertyChanged(nameof(Page));
            }
        }
        */
        /// <summary>
        /// 数据表配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal DataTableConfig _dataTable;

        /// <summary>
        /// 数据表配置
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"扩展对象"), DisplayName(@"数据表配置"), Description("数据表配置")]
        public DataTableConfig DataTable
        {
            get => _dataTable;
            set
            {
                if (_dataTable == value)
                    return;
                BeforePropertyChanged(nameof(DataTable), _dataTable, value);
                _dataTable = value;
                if (_dataTable != null)
                    _dataTable.Entity = Me;
                OnPropertyChanged(nameof(DataTable));
            }
        }

        /// <summary>
        /// 命令集合
        /// </summary>
        [DataMember, JsonProperty("_commands", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal NotificationList<UserCommandConfig> _commands;

        /// <summary>
        /// 命令集合
        /// </summary>
        /// <remark>
        /// 命令集合,数据模型中可调用的命令
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"扩展对象"), DisplayName(@"命令集合"), Description("命令集合,数据模型中可调用的命令")]
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
        /// 加入子级
        /// </summary>
        /// <param name="propertyConfig"></param>
        public void Add(UserCommandConfig propertyConfig)
        {
            propertyConfig.Parent = this as IEntityConfig;
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

        #endregion

        #region 兼容性

        public bool IsView { get; set; }
        #endregion
    }
}