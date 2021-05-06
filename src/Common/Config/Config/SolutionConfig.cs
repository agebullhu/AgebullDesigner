/*design by:agebull designer date:2017/7/12 22:06:40*/
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
using System.IO;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 解决方案配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class SolutionConfig : IndependenceConfigBase
    {
        #region 设计器支持

        /// <summary>
        /// 详细输出
        /// </summary>
        [JsonIgnore, JsonProperty("detailTrace")]
        internal bool _detailTrace;

        /// <summary>
        /// 详细输出
        /// </summary>
        /// <remark>
        /// 输出最详细的跟踪消息
        /// </remark>
        [JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"详细输出"), Description("输出最详细的跟踪消息")]
        public bool DetailTrace
        {
            get => _detailTrace;
            set
            {
                if (_detailTrace == value)
                    return;
                BeforePropertyChange(nameof(DetailTrace), _detailTrace, value);
                _detailTrace = value;
                RaisePropertyChanged(nameof(DetailTrace));
            }
        }
        /// <summary>
        /// 上帝模式
        /// </summary>
        [JsonIgnore]
        internal bool _godMode = true;

        /// <summary>
        /// 上帝模式
        /// </summary>
        /// <remark>
        /// 可以任意修改任意配置的上帝模式
        /// </remark>
        [JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"上帝模式"), Description("可以任意修改任意配置的上帝模式")]
        public bool GodMode
        {
            get => _godMode;
            set
            {
                if (_godMode == value)
                    return;
                BeforePropertyChange(nameof(GodMode), _godMode, value);
                _godMode = value;
                RaisePropertyChanged(nameof(GodMode));
            }
        }
        #endregion
        #region 对象集合

        /// <summary>
        /// 枚举集合
        /// </summary>
        [JsonIgnore]
        internal NotificationList<EnumConfig> _enums;

        /// <summary>
        /// 枚举集合
        /// </summary>
        /// <remark>
        /// 枚举集合
        /// </remark>
        [JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"枚举集合"), Description("枚举集合")]
        public IEnumerable<EnumConfig> Enums => EnumList;

        /// <summary>
        /// 枚举集合
        /// </summary>
        /// <remark>
        /// 枚举集合
        /// </remark>
        [JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"枚举集合"), Description("枚举集合")]
        public NotificationList<EnumConfig> EnumList
        {
            get
            {
                if (_enums != null)
                    return _enums;
                _enums = new NotificationList<EnumConfig>();
                RaisePropertyChanged(nameof(Enums));
                return _enums;
            }
            set
            {
                if (_enums == value)
                    return;
                BeforePropertyChange(nameof(Enums), _enums, value);
                _enums = value;
                RaisePropertyChanged(nameof(Enums));
            }
        }
        /// <summary>
        /// 实体集合
        /// </summary>
        [JsonIgnore]
        internal NotificationList<EntityConfig> _entities;

        /// <summary>
        /// 实体集合
        /// </summary>
        /// <remark>
        /// 所有表设置
        /// </remark>
        [JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"实体集合"), Description("所有表设置")]
        public IEnumerable<EntityConfig> Entities => EntityList;

        /// <summary>
        /// 实体集合
        /// </summary>
        /// <remark>
        /// 所有表设置
        /// </remark>
        [JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"实体集合"), Description("所有表设置")]
        public NotificationList<EntityConfig> EntityList
        {
            get
            {
                if (_entities != null)
                    return _entities;
                _entities = new NotificationList<EntityConfig>();
                RaisePropertyChanged(nameof(Entities));
                return _entities;
            }
            set
            {
                if (_entities == value)
                    return;
                BeforePropertyChange(nameof(Entities), _entities, value);
                _entities = value;
                RaisePropertyChanged(nameof(Entities));
            }
        }


        /// <summary>
        /// 模型集合
        /// </summary>
        [JsonIgnore]
        internal NotificationList<ModelConfig> _models;

        /// <summary>
        /// 模型集合
        /// </summary>
        /// <remark>
        /// 所有表设置
        /// </remark>
        [JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"模型集合"), Description("所有表设置")]
        public IEnumerable<ModelConfig> Models => ModelList;

        /// <summary>
        /// 模型集合
        /// </summary>
        /// <remark>
        /// 所有表设置
        /// </remark>
        [JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"模型集合"), Description("所有表设置")]
        public NotificationList<ModelConfig> ModelList
        {
            get
            {
                if (_models != null)
                    return _models;
                _models = new NotificationList<ModelConfig>();
                RaisePropertyChanged(nameof(Models));
                return _models;
            }
            set
            {
                if (_models == value)
                    return;
                BeforePropertyChange(nameof(Models), _models, value);
                _models = value;
                RaisePropertyChanged(nameof(Models));
            }
        }

        /// <summary>
        /// 项目集合
        /// </summary>
        [JsonIgnore]
        internal ConfigCollection<ProjectConfig> _projects;

        /// <summary>
        /// 项目集合
        /// </summary>
        /// <remark>
        /// 项目
        /// </remark>
        [JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"项目集合"), Description("项目")]
        public IEnumerable<ProjectConfig> Projects => ProjectList;
        /// <summary>
        /// 项目集合
        /// </summary>
        /// <remark>
        /// 项目
        /// </remark>
        [JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"项目集合"), Description("项目")]
        public ConfigCollection<ProjectConfig> ProjectList
        {
            get
            {
                if (_projects != null)
                    return _projects;
                _projects = new ConfigCollection<ProjectConfig>(this, nameof(ProjectList));
                RaisePropertyChanged(nameof(Projects));
                RaisePropertyChanged(nameof(ProjectList));
                return _projects;
            }
            set
            {
                if (_projects == value)
                    return;
                BeforePropertyChange(nameof(ProjectList), _projects, value);
                _projects = value;
                if (value != null)
                {
                    value.Name = nameof(ProjectList);
                    value.Parent = this;
                }
                RaisePropertyChanged(nameof(Projects));
                RaisePropertyChanged(nameof(ProjectList));
            }
        }

        /// <summary>
        /// API集合
        /// </summary>
        [JsonIgnore]
        internal NotificationList<ApiItem> _apiItems;

        /// <summary>
        /// API集合
        /// </summary>
        /// <remark>
        /// 对应的API集合
        /// </remark>
        [JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"API集合"), Description("对应的API集合")]
        public IEnumerable<ApiItem> ApiItems => ApiList;

        /// <summary>
        /// API集合
        /// </summary>
        /// <remark>
        /// 对应的API集合
        /// </remark>
        [JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"API集合"), Description("对应的API集合")]
        public NotificationList<ApiItem> ApiList
        {
            get
            {
                if (_apiItems != null)
                    return _apiItems;
                _apiItems = new NotificationList<ApiItem>();
                RaisePropertyChanged(nameof(ApiItems));
                return _apiItems;
            }
            set
            {
                if (_apiItems == value)
                    return;
                BeforePropertyChange(nameof(ApiItems), _apiItems, value);
                _apiItems = value;
                RaisePropertyChanged(nameof(ApiItems));
            }
        }
        #endregion
        #region 代码生成

        /// <summary>
        /// 解决方案根路径
        /// </summary>
        [DataMember, JsonProperty("_rootPath", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _rootPath;

        /// <summary>
        /// 解决方案根路径
        /// </summary>
        /// <remark>
        /// 解决方案根路径
        /// </remark>
        [JsonIgnore]
        [Category(@"代码生成"), DisplayName(@"解决方案根路径"), Description("解决方案根路径")]
        public string RootPath
        {
            get => _rootPath ?? string.Empty;
            set
            {
                if (_rootPath == value)
                    return;
                BeforePropertyChange(nameof(RootPath), _rootPath, value);
                _rootPath = value.SafeTrim();
                OnPropertyChanged(nameof(RootPath));
                OnPropertyChanged(nameof(AutoPagePath));
            }
        }

        /// <summary>
        /// 代码文件夹名称
        /// </summary>
        [DataMember, JsonProperty("pagePath", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _pagePath;

        /// <summary>
        /// 管理页面根路径
        /// </summary>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"管理页面根路径"), Description("管理页面根路径")]
        public string PagePath
        {
            get => WorkContext.InCoderGenerating ? _pagePath ?? Path.Combine(RootPath, "page") : _pagePath;
            set
            {
                if (_pagePath == value)
                    return;
                BeforePropertyChange(nameof(PagePath), _pagePath, value);
                _pagePath = value.SafeTrim();
                OnPropertyChanged(nameof(PagePath));
            }
        }
        /// <summary>
        /// 管理页面根路径
        /// </summary>
        [JsonIgnore]
        public string AutoPagePath => Path.Combine(RootPath, "page");

        /// <summary>
        /// 文档文件夹名称
        /// </summary>
        [DataMember, JsonProperty("docFolder", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _docFolder;

        /// <summary>
        /// 文档文件夹名称
        /// </summary>
        /// <remark>
        /// 文档文件夹名称
        /// </remark>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"文档文件夹名称"), Description("文档文件夹名称")]
        public string DocFolder
        {
            get => WorkContext.InCoderGenerating ? _docFolder ?? "doc" : _docFolder;
            set
            {
                if (_docFolder == value)
                    return;
                BeforePropertyChange(nameof(DocFolder), _docFolder, value);
                _docFolder = string.IsNullOrWhiteSpace(value) || value == "doc" ? null : value.Trim();
                OnPropertyChanged(nameof(DocFolder));
            }
        }

        /// <summary>
        /// 代码文件夹名称
        /// </summary>
        [DataMember, JsonProperty("_srcFolder", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _srcFolder;

        /// <summary>
        /// 代码文件夹名称
        /// </summary>
        /// <remark>
        /// 代码文件夹名称
        /// </remark>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"代码文件夹名称"), Description("代码文件夹名称")]
        public string SrcFolder
        {
            get => WorkContext.InCoderGenerating ? _srcFolder ?? "src" : _srcFolder;
            set
            {
                if (_srcFolder == value)
                    return;
                BeforePropertyChange(nameof(SrcFolder), _srcFolder, value);
                _srcFolder = string.IsNullOrWhiteSpace(value) || value == "src" ? null : value.Trim();
                OnPropertyChanged(nameof(SrcFolder));
            }
        }

        /// <summary>
        /// 解决方案命名空间
        /// </summary>
        [DataMember, JsonProperty("_nameSpace", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _nameSpace;

        /// <summary>
        /// 解决方案命名空间
        /// </summary>
        /// <remark>
        /// 解决方案根路径
        /// </remark>
        [JsonIgnore]
        [Category(@"代码生成"), DisplayName(@"解决方案命名空间"), Description("解决方案根路径")]
        public string NameSpace
        {
            get => _nameSpace;
            set
            {
                if (_nameSpace == value)
                    return;
                BeforePropertyChange(nameof(NameSpace), _nameSpace, value);
                _nameSpace = value.SafeTrim();
                OnPropertyChanged(nameof(NameSpace));
            }
        }
        #endregion
        #region 系统设置

        /// <summary>
        /// 数据类型映射
        /// </summary>
        /*[DataMember, JsonProperty("dataTypeMap",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]*/
        NotificationList<DataTypeMapConfig> _dataTypeMap;

        /// <summary>
        /// 数据类型映射
        /// </summary>
        /// <remark>
        /// 数据类型映射
        /// </remark>
        [JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"数据类型映射"), Description("数据类型映射")]
        public NotificationList<DataTypeMapConfig> DataTypeMap
        {
            get
            {
                if (_dataTypeMap != null)
                    return _dataTypeMap;
                _dataTypeMap = new NotificationList<DataTypeMapConfig>();
                _dataTypeMap.AddRange(DataTypeMapConfig.DataTypeMap);
                OnPropertyChanged(nameof(DataTypeMap));
                return _dataTypeMap;
            }
            set
            {
                if (_dataTypeMap == value)
                    return;
                BeforePropertyChange(nameof(DataTypeMap), _dataTypeMap, value);
                _dataTypeMap = value;
                OnPropertyChanged(nameof(DataTypeMap));
            }
        }

        /// <summary>
        /// 解决方案类型
        /// </summary>
        [DataMember, JsonProperty("SolutionType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal SolutionType _solutionType;

        /// <summary>
        /// 解决方案类型
        /// </summary>
        /// <remark>
        /// 解决方案类型
        /// </remark>
        [JsonIgnore]
        [Category(@"系统设置"), DisplayName(@"解决方案类型"), Description("解决方案类型")]
        public SolutionType SolutionType
        {
            get => _solutionType;
            set
            {
                if (_solutionType == value)
                    return;
                BeforePropertyChange(nameof(SolutionType), _solutionType, value);
                _solutionType = value;
                OnPropertyChanged(nameof(SolutionType));
            }
        }

        /// <summary>
        /// 是否一般WEB应用
        /// </summary>
        /// <remark>
        /// 是否一般WEB应用
        /// </remark>
        [JsonIgnore]
        [Category(@"系统设置"), DisplayName(@"是否一般WEB应用"), Description("是否一般WEB应用")]
        public bool IsWeb => SolutionType == SolutionType.Web;


        [DataMember, JsonProperty("idDataType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _idDataType = "long";

        /// <summary>
        /// 主键数据类型
        /// </summary>
        [JsonIgnore]
        [Category(@"系统设置"), DisplayName(@"主键数据类型"), Description("主键数据类型")]
        public string IdDataType
        {
            get => _idDataType;
            set
            {
                if (_idDataType == value)
                    return;
                BeforePropertyChange(nameof(IdDataType), _idDataType, value);
                _idDataType = value;
                OnPropertyChanged(nameof(IdDataType));
            }
        }


        [DataMember, JsonProperty("userIdDataType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private string _userIdDataType = "long";

        /// <summary>
        /// 用户标识数据类型
        /// </summary>
        [JsonIgnore]
        [Category(@"系统设置"), DisplayName(@"用户标识数据类型"), Description("用户标识数据类型")]
        public string UserIdDataType
        {
            get => _userIdDataType;
            set
            {
                if (_userIdDataType == value)
                    return;
                BeforePropertyChange(nameof(UserIdDataType), _userIdDataType, value);
                _userIdDataType = value;
                OnPropertyChanged(nameof(UserIdDataType));
            }
        }

        [DataMember, JsonProperty("headerView", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        private HeaderVisiable _headerView = HeaderVisiable.General;

        /// <summary>
        /// 标题视角
        /// </summary>
        [JsonIgnore]
        public HeaderVisiable HeaderView
        {
            get => _headerView;
            set
            {
                _headerView = value;
                OnPropertyChanged(nameof(HeaderVisiable));
            }
        }

        [JsonIgnore]
        private string _workView;

        /// <summary>
        /// 工作视角
        /// </summary>
        [JsonIgnore]
        public string WorkView
        {
            get => _workView;
            set
            {
                _workView = string.IsNullOrWhiteSpace(value) ? null : value.ToLower();
                RaisePropertyChanged(nameof(WorkView));
                RaisePropertyChanged(nameof(IsApiWorkView));
                RaisePropertyChanged(nameof(IsEntityWorkView));
                RaisePropertyChanged(nameof(IsModelWorkView));
                RaisePropertyChanged(nameof(IsDataBaseWorkView));
                RaisePropertyChanged(nameof(AdvancedView));
            }
        }

        [JsonIgnore]
        private bool _advancedView;
        /// <summary>
        /// 高级视角
        /// </summary>
        [JsonIgnore]
        public bool AdvancedView
        {
            get => _advancedView;
            set
            {
                _advancedView = value;
                RaisePropertyChanged(nameof(AdvancedView));
            }
        }

        /// <summary>
        /// 工作视角
        /// </summary>
        public bool IsApiWorkView => string.IsNullOrWhiteSpace(_workView) || _workView.Contains("api");

        /// <summary>
        /// 工作视角
        /// </summary>
        public bool IsEntityWorkView => string.IsNullOrWhiteSpace(_workView) || _workView.Contains("entity");

        /// <summary>
        /// 工作视角
        /// </summary>
        public bool IsModelWorkView => string.IsNullOrWhiteSpace(_workView) || _workView.Contains("model");

        /// <summary>
        /// 工作视角
        /// </summary>
        public bool IsDataBaseWorkView => string.IsNullOrWhiteSpace(_workView) || _workView.Contains("database");

        #endregion

    }
}