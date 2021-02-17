using Agebull.EntityModel.Config.V2021;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置基础
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public abstract partial class EntityConfigBase : ProjectChildConfigBase
    {
        #region 视角开关

        /// <summary>
        /// 构造
        /// </summary>
        public EntityConfigBase()
        {
            _desingSwitch = 0xFFFF;
        }

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

        /// <summary>
        /// 分类
        /// </summary>
        [DataMember, JsonProperty("_Classify", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _classify;

        /// <summary>
        /// 分类
        /// </summary>
        /// <remark>
        /// 分类(仅引用可行)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("*设计"), DisplayName("分类"), Description("分类(仅引用可行)")]
        public string Classify
        {
            get => _classify ?? "None";
            set
            {
                if (_classify == value)
                    return;
                BeforePropertyChanged(nameof(Classify), _classify, value);
                _classify = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Classify));
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
            get => _dataTable ?? (IEntity.EnableDataBase ? _dataTable=DataTableConfig.Create(IEntity): null);
            set
            {
                if (_dataTable == value)
                    return;
                BeforePropertyChanged(nameof(DataTable), _dataTable, value);
                _dataTable = value;
                if (_dataTable != null)
                    _dataTable.Entity = IEntity;
                OnPropertyChanged(nameof(DataTable));
            }
        }

        /// <summary>
        /// 命令集合
        /// </summary>
        [DataMember, JsonProperty("_commands", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal ConfigCollection<UserCommandConfig> _commands;

        /// <summary>
        /// 命令集合
        /// </summary>
        /// <remark>
        /// 命令集合,数据模型中可调用的命令
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"扩展对象"), DisplayName(@"命令集合"), Description("命令集合,数据模型中可调用的命令")]
        public ConfigCollection<UserCommandConfig> Commands
        {
            get
            {
                if (_commands != null)
                    return _commands;
                _commands = new ConfigCollection<UserCommandConfig>(this);
                RaisePropertyChanged(nameof(Commands));
                return _commands;
            }
            set
            {
                if (_commands == value)
                    return;
                BeforePropertyChanged(nameof(Commands), _commands, value);
                _commands = value;
                if (value != null)
                    value.Parent = this;
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

        /// <summary>
        /// 表示自己的后期实现
        /// </summary>
        public IEntityConfig IEntity => this as IEntityConfig;

        /// <summary>
        /// 是否视图（兼容性属性）
        /// </summary>
        public bool IsView { get; set; }

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
        #region 数据库
#pragma warning disable CS0649
        /// <summary>
        /// 存储表名(设计录入)
        /// </summary>
        [DataMember, JsonProperty("_tableName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _readTableName;

        /// <summary>
        /// 存储表名
        /// </summary>
        [DataMember, JsonProperty("_saveTableName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _saveTableName;

        /// <summary>
        /// 数据库编号
        /// </summary>
        [DataMember, JsonProperty("_dbIndex", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _dbIndex;


        /// <summary>
        /// 按修改更新
        /// </summary>
        [DataMember, JsonProperty("UpdateByModified", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _updateByModified;

        #endregion

#pragma warning restore CS0469
        #endregion

        #region 视图

        /// <summary>
        /// 接口名称
        /// </summary>
        [DataMember, JsonProperty("_apiName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _apiName;

        /// <summary>
        /// 接口名称
        /// </summary>
        /// <remark>
        /// 接口名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"接口名称"), Description("接口名称")]
        public string ApiName
        {
            get => WorkContext.InCoderGenerating ? _apiName ?? Abbreviation : _apiName;
            set
            {
                if (_apiName == value)
                    return;
                if (value == Name)
                    value = null;
                BeforePropertyChanged(nameof(ApiName), _apiName, value);
                _apiName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ApiName));
            }
        }


        /// <summary>
        /// 界面只读
        /// </summary>
        [DataMember, JsonProperty("isUiReadOnly", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isUiReadOnly;

        /// <summary>
        /// 界面只读
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"界面只读"), Description("界面只读")]
        public bool IsUiReadOnly
        {
            get => _isUiReadOnly;
            set
            {
                if (_isUiReadOnly == value)
                    return;
                BeforePropertyChanged(nameof(IsUiReadOnly), _isUiReadOnly, value);
                _isUiReadOnly = value;
                OnPropertyChanged(nameof(IsUiReadOnly));
            }
        }
        /// <summary>
        /// 页面文件夹名称
        /// </summary>
        [DataMember, JsonProperty("PageFolder", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _pageFolder;

        /// <summary>
        /// 页面文件夹名称
        /// </summary>
        /// <remark>
        /// 页面文件夹名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"页面文件夹名称"), Description("页面文件夹名称")]
        public string PageFolder
        {
            get => WorkContext.InCoderGenerating ? _pageFolder ?? Name.ToLWord() : _pageFolder;
            set
            {
                if (_pageFolder == value)
                    return;
                BeforePropertyChanged(nameof(PageFolder), _pageFolder, value);
                _pageFolder = string.IsNullOrWhiteSpace(value) || value == Name ? null : value.Trim();
                OnPropertyChanged(nameof(PageFolder));
            }
        }

        /// <summary>
        /// 树形界面
        /// </summary>
        [DataMember, JsonProperty("TreeUi", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _treeUi;

        /// <summary>
        /// 树形界面
        /// </summary>
        /// <remark>
        /// 树形界面
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"树形界面"), Description("树形界面")]
        public bool TreeUi
        {
            get => _treeUi;
            set
            {
                if (_treeUi == value)
                    return;
                BeforePropertyChanged(nameof(TreeUi), _treeUi, value);
                _treeUi = value;
                OnPropertyChanged(nameof(TreeUi));
            }
        }

        /// <summary>
        /// 详细编辑页面
        /// </summary>
        [DataMember, JsonProperty("detailsPage", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _detailsPage;

        /// <summary>
        /// 详细编辑页面
        /// </summary>
        [IgnoreDataMember, JsonIgnore, Category(@"用户界面")]
        public bool DetailsPage
        {
            get => _detailsPage;
            set
            {
                if (_detailsPage == value)
                    return;
                BeforePropertyChanged(nameof(DetailsPage), _detailsPage, value);
                _detailsPage = value;
                OnPropertyChanged(nameof(DetailsPage));
            }
        }

        /// <summary>
        /// 编辑页面分几列
        /// </summary>
        [DataMember, JsonProperty("FormCloumn", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal int _formCloumn;

        /// <summary>
        /// 编辑页面分几列
        /// </summary>
        /// <remark>
        /// 编辑页面分几列
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"编辑页面分几列"), Description("编辑页面分几列")]
        public int FormCloumn
        {
            get => _formCloumn;
            set
            {
                if (_formCloumn == value)
                    return;
                BeforePropertyChanged(nameof(FormCloumn), _formCloumn, value);
                _formCloumn = value;
                OnPropertyChanged(nameof(FormCloumn));
            }
        }

        [DataMember, JsonProperty("OrderField", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _orderField;

        [DataMember, JsonProperty("OrderDesc", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _orderDesc;

        /// <summary>
        /// 默认排序字段
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"默认排序字段"), Description("默认排序字段")]
        public string OrderField
        {
            get => _orderField ?? IEntity.PrimaryField;
            set
            {
                if (_orderField == value)
                    return;
                BeforePropertyChanged(nameof(OrderField), _orderField, value);
                _orderField = value;
                OnPropertyChanged(nameof(OrderField));
            }
        }

        /// <summary>
        /// 默认反序
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"默认反序"), Description("默认反序")]
        public bool OrderDesc
        {
            get => _orderDesc;
            set
            {
                if (_orderDesc == value)
                    return;
                BeforePropertyChanged(nameof(OrderDesc), _orderDesc, value);
                _orderDesc = value;
                OnPropertyChanged(nameof(OrderDesc));
            }
        }

        [DataMember, JsonProperty("formQuery", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _formQuery;

        /// <summary>
        /// 表单查询
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"用户界面"), DisplayName(@"表单查询"), Description("表单查询")]
        public bool FormQuery
        {
            get => _formQuery;
            set
            {
                if (_formQuery == value)
                    return;
                BeforePropertyChanged(nameof(FormQuery), _formQuery, value);
                _formQuery = value;
                OnPropertyChanged(nameof(FormQuery));
            }
        }

        #endregion


        #region C++

        /// <summary>
        /// C++名称
        /// </summary>
        [DataMember, JsonProperty("CppName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _cppName;

        /// <summary>
        /// C++名称
        /// </summary>
        /// <remark>
        /// C++字段名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"C++"), DisplayName(@"C++名称"), Description("C++字段名称")]
        public string CppName
        {
            get => WorkContext.InCoderGenerating ? _cppName ?? Name : _cppName;
            set
            {
                if (Name == value)
                    value = null;
                if (_cppName == value)
                    return;
                BeforePropertyChanged(nameof(CppName), _cppName, value);
                _cppName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(CppName));
            }
        }
        #endregion
    }
}