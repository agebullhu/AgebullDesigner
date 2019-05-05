/*design by:agebull designer date:2017/7/12 22:06:40*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 解决方案配置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class SolutionConfig : ParentConfigBase
    {
        #region 设计器支持

        /// <summary>
        /// 详细输出
        /// </summary>
        [IgnoreDataMember, JsonProperty("detailTrace")]
        internal bool _detailTrace;

        /// <summary>
        /// 详细输出
        /// </summary>
        /// <remark>
        /// 输出最详细的跟踪消息
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"详细输出"), Description("输出最详细的跟踪消息")]
        public bool DetailTrace
        {
            get => _detailTrace;
            set
            {
                if (_detailTrace == value)
                    return;
                BeforePropertyChanged(nameof(DetailTrace), _detailTrace, value);
                _detailTrace = value;
                OnPropertyChanged(nameof(DetailTrace));
            }
        }
        /// <summary>
        /// 上帝模式
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal bool _godMode = true;

        /// <summary>
        /// 上帝模式
        /// </summary>
        /// <remark>
        /// 可以任意修改任意配置的上帝模式
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"上帝模式"), Description("可以任意修改任意配置的上帝模式")]
        public bool GodMode
        {
            get => _godMode;
            set
            {
                if (_godMode == value)
                    return;
                BeforePropertyChanged(nameof(GodMode), _godMode, value);
                _godMode = value;
                OnPropertyChanged(nameof(GodMode));
            }
        }
        /// <summary>
        /// 生成校验代码
        /// </summary>
        [IgnoreDataMember, JsonProperty("haseValidateCode")]
        internal bool _haseValidateCode;

        /// <summary>
        /// 生成校验代码
        /// </summary>
        /// <remark>
        /// 可以任意修改任意配置的生成校验代码
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"设计器支持"), DisplayName(@"生成校验代码")]
        public bool HaseValidateCode
        {
            get => _haseValidateCode;
            set
            {
                if (_haseValidateCode == value)
                    return;
                BeforePropertyChanged(nameof(HaseValidateCode), _haseValidateCode, value);
                _haseValidateCode = value;
                OnPropertyChanged(nameof(HaseValidateCode));
            }
        }
        #endregion
        #region 对象集合

        /// <summary>
        /// 枚举集合
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal NotificationList<EnumConfig> _enums;

        /// <summary>
        /// 枚举集合
        /// </summary>
        /// <remark>
        /// 枚举集合
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"枚举集合"), Description("枚举集合")]
        public IEnumerable<EnumConfig> Enums => EnumList;

        /// <summary>
        /// 枚举集合
        /// </summary>
        /// <remark>
        /// 枚举集合
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
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
                BeforePropertyChanged(nameof(Enums), _enums, value);
                _enums = value;
                RaisePropertyChanged(nameof(Enums));
            }
        }
        /// <summary>
        /// 实体集合
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal NotificationList<EntityConfig> _entities;

        /// <summary>
        /// 实体集合
        /// </summary>
        /// <remark>
        /// 所有表设置
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"实体集合"), Description("所有表设置")]
        public IEnumerable<EntityConfig> Entities => EntityList;

        /// <summary>
        /// 实体集合
        /// </summary>
        /// <remark>
        /// 所有表设置
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
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
                BeforePropertyChanged(nameof(Entities), _entities, value);
                _entities = value;
                RaisePropertyChanged(nameof(Entities));
            }
        }
        /// <summary>
        /// 项目集合
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal NotificationList<ProjectConfig> _projects;

        /// <summary>
        /// 项目集合
        /// </summary>
        /// <remark>
        /// 项目
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"项目集合"), Description("项目")]
        public IEnumerable<ProjectConfig> Projects => ProjectList;
        /// <summary>
        /// 项目集合
        /// </summary>
        /// <remark>
        /// 项目
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"项目集合"), Description("项目")]
        public NotificationList<ProjectConfig> ProjectList
        {
            get
            {
                if (_projects != null)
                    return _projects;
                _projects = new NotificationList<ProjectConfig>();
                RaisePropertyChanged(nameof(Projects));
                return _projects;
            }
            set
            {
                if (_projects == value)
                    return;
                BeforePropertyChanged(nameof(Projects), _projects, value);
                _projects = value;
                RaisePropertyChanged(nameof(Projects));
            }
        }

        /// <summary>
        /// API集合
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal NotificationList<ApiItem> _apiItems;

        /// <summary>
        /// API集合
        /// </summary>
        /// <remark>
        /// 对应的API集合
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"API集合"), Description("对应的API集合")]
        public IEnumerable<ApiItem> ApiItems => ApiList;

        /// <summary>
        /// API集合
        /// </summary>
        /// <remark>
        /// 对应的API集合
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
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
                BeforePropertyChanged(nameof(ApiItems), _apiItems, value);
                _apiItems = value;
                RaisePropertyChanged(nameof(ApiItems));
            }
        }
        #endregion
        #region 代码生成

        /// <summary>
        /// 解决方案根路径
        /// </summary>
        [DataMember, JsonProperty("_rootPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _rootPath;

        /// <summary>
        /// 解决方案根路径
        /// </summary>
        /// <remark>
        /// 解决方案根路径
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"代码生成"), DisplayName(@"解决方案根路径"), Description("解决方案根路径")]
        public string RootPath
        {
            get => _rootPath ?? string.Empty ;
            set
            {
                if (_rootPath == value)
                    return;
                BeforePropertyChanged(nameof(RootPath), _rootPath, value);
                _rootPath = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(RootPath));
                OnPropertyChanged(nameof(AutoPagePath));
            }
        }

        /// <summary>
        /// 代码文件夹名称
        /// </summary>
        [DataMember, JsonProperty("pagePath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _pagePath;

        /// <summary>
        /// 管理页面根路径
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"管理页面根路径"), Description("管理页面根路径")]
        public string PagePath
        {
            get => WorkContext.InCoderGenerating ? _pagePath ?? Path.Combine(RootPath, "page") : _pagePath;
            set
            {
                if (_pagePath == value)
                    return;
                BeforePropertyChanged(nameof(PagePath), _pagePath, value);
                _pagePath = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(PagePath));
            }
        }
        /// <summary>
        /// 管理页面根路径
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public string AutoPagePath => Path.Combine(RootPath, "page");

        /// <summary>
        /// 文档文件夹名称
        /// </summary>
        [DataMember, JsonProperty("docFolder", NullValueHandling = NullValueHandling.Ignore)]
        internal string _docFolder;

        /// <summary>
        /// 文档文件夹名称
        /// </summary>
        /// <remark>
        /// 文档文件夹名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"文档文件夹名称"), Description("文档文件夹名称")]
        public string DocFolder
        {
            get => WorkContext.InCoderGenerating ? _docFolder ?? "doc" : _docFolder;
            set
            {
                if (_docFolder == value)
                    return;
                BeforePropertyChanged(nameof(DocFolder), _docFolder, value);
                _docFolder = string.IsNullOrWhiteSpace(value) || value == "doc" ? null : value.Trim();
                OnPropertyChanged(nameof(DocFolder));
            }
        }

        /// <summary>
        /// 代码文件夹名称
        /// </summary>
        [DataMember, JsonProperty("_srcFolder", NullValueHandling = NullValueHandling.Ignore)]
        internal string _srcFolder;

        /// <summary>
        /// 代码文件夹名称
        /// </summary>
        /// <remark>
        /// 代码文件夹名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"代码文件夹名称"), Description("代码文件夹名称")]
        public string SrcFolder
        {
            get => WorkContext.InCoderGenerating ? _srcFolder ?? "src" : _srcFolder;
            set
            {
                if (_srcFolder == value)
                    return;
                BeforePropertyChanged(nameof(SrcFolder), _srcFolder, value);
                _srcFolder = string.IsNullOrWhiteSpace(value) || value == "src" ? null : value.Trim();
                OnPropertyChanged(nameof(SrcFolder));
            }
        }

        /// <summary>
        /// 解决方案命名空间
        /// </summary>
        [DataMember, JsonProperty("_nameSpace", NullValueHandling = NullValueHandling.Ignore)]
        internal string _nameSpace;

        /// <summary>
        /// 解决方案命名空间
        /// </summary>
        /// <remark>
        /// 解决方案根路径
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"代码生成"), DisplayName(@"解决方案命名空间"), Description("解决方案根路径")]
        public string NameSpace
        {
            get => _nameSpace;
            set
            {
                if (_nameSpace == value)
                    return;
                BeforePropertyChanged(nameof(NameSpace), _nameSpace, value);
                _nameSpace = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(NameSpace));
            }
        }
        #endregion
        #region 系统设置

        /// <summary>
        /// 数据类型映射
        /// </summary>
        /*[DataMember, JsonProperty("dataTypeMap", NullValueHandling = NullValueHandling.Ignore)]*/
        NotificationList<DataTypeMapConfig> _dataTypeMap;

        /// <summary>
        /// 数据类型映射
        /// </summary>
        /// <remark>
        /// 数据类型映射
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
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
                BeforePropertyChanged(nameof(DataTypeMap), _dataTypeMap, value);
                _dataTypeMap = value;
                OnPropertyChanged(nameof(DataTypeMap));
            }
        }

        /// <summary>
        /// 解决方案类型
        /// </summary>
        [DataMember, JsonProperty("SolutionType", NullValueHandling = NullValueHandling.Ignore)]
        internal SolutionType _solutionType;

        /// <summary>
        /// 解决方案类型
        /// </summary>
        /// <remark>
        /// 解决方案类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"系统设置"), DisplayName(@"解决方案类型"), Description("解决方案类型")]
        public SolutionType SolutionType
        {
            get => _solutionType;
            set
            {
                if (_solutionType == value)
                    return;
                BeforePropertyChanged(nameof(SolutionType), _solutionType, value);
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
        [IgnoreDataMember, JsonIgnore]
        [Category(@"系统设置"), DisplayName(@"是否一般WEB应用"), Description("是否一般WEB应用")]
        public bool IsWeb => SolutionType == SolutionType.Web;


        [DataMember, JsonProperty("idDataType", NullValueHandling = NullValueHandling.Ignore)]
        private string _idDataType = "long";

        /// <summary>
        /// 主键数据类型
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"系统设置"), DisplayName(@"主键数据类型"), Description("主键数据类型")]
        public string IdDataType
        {
            get => _idDataType;
            set
            {
                if (_idDataType == value)
                    return;
                BeforePropertyChanged(nameof(IdDataType), _idDataType, value);
                _idDataType = value;
                OnPropertyChanged(nameof(IdDataType));
            }
        }


        [DataMember, JsonProperty("userIdDataType", NullValueHandling = NullValueHandling.Ignore)]
        private string _userIdDataType = "long";

        /// <summary>
        /// 用户标识数据类型
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"系统设置"), DisplayName(@"用户标识数据类型"), Description("用户标识数据类型")]
        public string UserIdDataType
        {
            get => _userIdDataType;
            set
            {
                if (_userIdDataType == value)
                    return;
                BeforePropertyChanged(nameof(UserIdDataType), _userIdDataType, value);
                _userIdDataType = value;
                OnPropertyChanged(nameof(UserIdDataType));
            }
        }
        [IgnoreDataMember, JsonIgnore]
        private string _workView;

        /// <summary>
        /// 工作视角
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public string WorkView
        {
            get => _workView;
            set
            {
                _workView = string.IsNullOrWhiteSpace(value) ? null : value.ToLower();
                OnPropertyChanged(nameof(WorkView));
                OnPropertyChanged(nameof(IsApiWorkView));
                OnPropertyChanged(nameof(IsEntityWorkView));
                OnPropertyChanged(nameof(IsModelWorkView));
                OnPropertyChanged(nameof(IsDataBaseWorkView));
                OnPropertyChanged(nameof(AdvancedView));
            }
        }

        [IgnoreDataMember, JsonIgnore]
        private bool _advancedView;
        /// <summary>
        /// 高级视角
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public bool AdvancedView
        {
            get => _advancedView;
            set
            {
                _advancedView = value;
                OnPropertyChanged(nameof(AdvancedView));
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