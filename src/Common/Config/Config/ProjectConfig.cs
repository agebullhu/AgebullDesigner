﻿/*design by:agebull designer date:2017/7/12 22:06:39*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using Agebull.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 项目设置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ProjectConfig : IndependenceConfigBase,IChildrenConfig
    {
        #region 子级

        /// <summary>
        /// 实体分组
        /// </summary>
        [DataMember, JsonProperty("Classifies", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal ConfigCollection<EntityClassify> _classifies;

        /// <summary>
        /// 实体分组
        /// </summary>
        /// <remark>
        /// 实体分组
        /// </remark>
        [JsonIgnore]
        [Category(@"子级"), DisplayName(@"实体分组"), Description("实体分组")]
        public ConfigCollection<EntityClassify> Classifies
        {
            get
            {
                if (_classifies != null)
                    return _classifies;
                _classifies = new ConfigCollection<EntityClassify>(this, nameof(Classifies));
                RaisePropertyChanged(nameof(Classifies));
                return _classifies;
            }
            set
            {
                if (_classifies == value)
                    return;
                BeforePropertyChange(nameof(Classifies), _classifies, value);
                _classifies = value;
                if (value != null)
                {
                    value.Name = nameof(Classifies);
                    value.Parent = this;
                }
                RaisePropertyChanged(nameof(Classifies));
            }
        }

        /// <summary>
        /// 模型集合
        /// </summary>
        [JsonIgnore]
        internal ConfigCollection<ModelConfig> _models;


        /// <summary>
        /// 模型集合
        /// </summary>
        /// <remark>
        /// 模型集合
        /// </remark>
        [JsonIgnore]
        [Category(@"子级"), DisplayName(@"模型集合"), Description("模型集合")]
        public ConfigCollection<ModelConfig> Models
        {
            get
            {
                if (_models != null)
                    return _models;
                _models = new ConfigCollection<ModelConfig>(this, nameof(Models));
                RaisePropertyChanged(nameof(Models));
                return _models;
            }
            set
            {
                if (_models == value)
                    return;
                BeforePropertyChange(nameof(Models), _models, value);
                _models = value;
                if (value != null)
                {
                    value.Name = nameof(Models);
                    value.Parent = this;
                }
                RaisePropertyChanged(nameof(Models));
            }
        }
        /// <summary>
        /// 查找实体(先本项目再全解决方案)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public EntityConfig Find(params string[] names)
        {
            return names.Length == 0
                ? null
                : Entities.FirstOrDefault(p => names.Exist(p.Name,p.DataTable?.ReadTableName,p.DataTable?.SaveTableName))
                ?? GlobalConfig.Find(names);
        }

        /// <summary>
        /// 实体集合
        /// </summary>
        [JsonIgnore]
        internal ConfigCollection<EntityConfig> _entities;


        /// <summary>
        /// 实体集合
        /// </summary>
        /// <remark>
        /// 实体集合
        /// </remark>
        [JsonIgnore]
        [Category(@"子级"), DisplayName(@"实体集合"), Description("实体集合")]
        public ConfigCollection<EntityConfig> Entities
        {
            get
            {
                if (_entities != null)
                    return _entities;
                _entities = new ConfigCollection<EntityConfig>(this,nameof(Entities));
                RaisePropertyChanged(nameof(Entities));
                return _entities;
            }
            set
            {
                if (_entities == value)
                    return;
                BeforePropertyChange(nameof(Entities), _entities, value);
                _entities = value;
                if (value != null)
                {
                    value.Name = nameof(Entities);
                    value.Parent = this;
                }
                RaisePropertyChanged(nameof(Entities));
            }
        }

        /// <summary>
        /// API节点集合
        /// </summary>
        [JsonIgnore]
        internal ConfigCollection<ApiItem> _apiItems;

        /// <summary>
        /// API节点集合
        /// </summary>
        /// <remark>
        /// API节点集合
        /// </remark>
        [JsonIgnore]
        [Category(@"子级"), DisplayName(@"API节点集合"), Description("API节点集合")]
        public ConfigCollection<ApiItem> ApiItems
        {
            get
            {
                if (_apiItems != null)
                    return _apiItems;
                _apiItems = new ConfigCollection<ApiItem>(this, nameof(ApiItems));
                RaisePropertyChanged(nameof(ApiItems));
                return _apiItems;
            }
            set
            {
                if (_apiItems == value)
                    return;
                BeforePropertyChange(nameof(ApiItems), _apiItems, value);
                _apiItems = value;
                if (value != null)
                {
                    value.Name = nameof(ApiItems);
                    value.Parent = this;
                }
                RaisePropertyChanged(nameof(ApiItems));
            }
        }

        /// <summary>
        /// 服务名称
        /// </summary>
        [DataMember, JsonProperty("serviceName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string serviceName;

        /// <summary>
        /// 服务名称
        /// </summary>
        /// <remark>
        /// 接口名称
        /// </remark>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"服务名称"), Description("服务名称")]
        public string ServiceName
        {
            get => WorkContext.InCoderGenerating ? serviceName ?? Abbreviation : serviceName;
            set
            {
                if (serviceName == value)
                    return;
                if (value == Name)
                    value = null;
                BeforePropertyChange(nameof(ServiceName), serviceName, value);
                serviceName = string.IsNullOrWhiteSpace(value) ? null : value.Trim('\\', '/').Trim();
                OnPropertyChanged(nameof(ServiceName));
            }
        }

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
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"接口名称"), Description("接口名称")]
        public string ApiName
        {
            get => WorkContext.InCoderGenerating ? _apiName ?? $"api/{Abbreviation}" : _apiName;
            set
            {
                if (_apiName == value)
                    return;
                if (value == Name)
                    value = null;
                BeforePropertyChange(nameof(ApiName), _apiName, value);
                _apiName = string.IsNullOrWhiteSpace(value) ? null : value.Trim('\\', '/').Trim();
                OnPropertyChanged(nameof(ApiName));
            }
        }

        /// <summary>
        /// 枚举集合
        /// </summary>
        [JsonIgnore]
        internal ConfigCollection<EnumConfig> _enums;

        /// <summary>
        /// 枚举集合
        /// </summary>
        /// <remark>
        /// 枚举集合
        /// </remark>
        [JsonIgnore]
        [Category(@"对象集合"), DisplayName(@"枚举集合"), Description("枚举集合")]
        public ConfigCollection<EnumConfig> Enums
        {
            get
            {
                if (_enums != null)
                    return _enums;
                _enums = new ConfigCollection<EnumConfig>(this, nameof(Enums));
                RaisePropertyChanged(nameof(Enums));
                return _enums;
            }
            set
            {
                if (_enums == value)
                    return;
                BeforePropertyChange(nameof(Enums), _enums, value);
                _enums = value;
                if (value != null)
                {
                    value.Name = nameof(Enums);
                    value.Parent = this;
                }
                RaisePropertyChanged(nameof(Enums));
            }
        }
        #endregion

        #region 代码路径

        /// <summary>
        /// 接口代码主文件夹
        /// </summary>
        [DataMember, JsonProperty("_apiFolder", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _apiFolder;

        /// <summary>
        /// 接口代码主文件夹
        /// </summary>
        /// <remark>
        /// 接口代码主文件夹
        /// </remark>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"接口代码主文件夹"), Description("接口代码主文件夹")]
        public string ApiFolder
        {
            get => _apiFolder;
            set
            {
                if (_apiFolder == value)
                    return;
                BeforePropertyChange(nameof(ApiFolder), _apiFolder, value);
                _apiFolder = string.IsNullOrWhiteSpace(value) ? null : value.Trim('\\', '/').Trim();
                OnPropertyChanged(nameof(ApiFolder));
                OnPropertyChanged(nameof(ApiPath));
            }
        }

        /// <summary>
        /// 模型代码主文件夹
        /// </summary>
        [DataMember, JsonProperty("_modelFolder", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _modelFolder;

        /// <summary>
        /// 模型代码主文件夹
        /// </summary>
        /// <remark>
        /// 模型代码主文件夹
        /// </remark>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"模型代码主文件夹"), Description("模型代码主文件夹")]
        public string ModelFolder
        {
            get => _modelFolder;
            set
            {
                if (_modelFolder == value)
                    return;
                BeforePropertyChange(nameof(ModelFolder), _modelFolder, value);
                _modelFolder = string.IsNullOrWhiteSpace(value) ? null : value.Trim('\\', '/').Trim();
                OnPropertyChanged(nameof(ModelFolder));
                OnPropertyChanged(nameof(ModelPath));
            }
        }


        /// <summary>
        /// WEB页面主文件夹
        /// </summary>
        [DataMember, JsonProperty("_pageFolder", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _pageFolder;

        /// <summary>
        /// WEB页面主文件夹
        /// </summary>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"WEB页面主文件夹"), Description("WEB页面主文件夹")]
        public string PageFolder
        {
            get => (_pageFolder ?? Name).ToLWord();
            set
            {
                if (_pageFolder == value)
                    return;
                BeforePropertyChange(nameof(PageFolder), _pageFolder, value);
                _pageFolder = string.IsNullOrWhiteSpace(value) ? null : value.Trim('\\', '/').Trim();
                OnPropertyChanged(nameof(PageFolder));
                OnPropertyChanged(nameof(PagePath));
            }
        }

        /// <summary>
        /// WEB页面主文件夹
        /// </summary>
        public string PageRoot => _pageFolder ?? Name;

        /// <summary>
        /// 子级文件夹
        /// </summary>
        [DataMember, JsonProperty("_branchPath", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _branchFolder;

        /// <summary>
        /// 子级文件夹
        /// </summary>
        /// <remark>
        /// 子级文件夹
        /// </remark>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"子级文件夹"), Description("子级文件夹")]
        public string BranchFolder
        {
            get => _branchFolder ?? Name;
            set
            {
                if (_branchFolder == value)
                    return;
                BeforePropertyChange(nameof(BranchFolder), _branchFolder, value);
                _branchFolder = string.IsNullOrWhiteSpace(value) ? null : value.Trim('\\', '/').Trim();
                OnPropertyChanged(nameof(BranchFolder));
                RaisePropertyChanged(nameof(ApiPath));
                RaisePropertyChanged(nameof(ModelPath));
            }
        }

        /// <summary>
        /// 重置模型路径
        /// </summary>
        public void CheckPath()
        {
            string root = GlobalConfig.CheckPath(Solution.RootPath, Solution.SrcFolder);

            if (!string.IsNullOrWhiteSpace(_branchFolder))
                root = GlobalConfig.CheckPath(root, _branchFolder);

            if (string.IsNullOrWhiteSpace(_modelFolder))
                GlobalConfig.CheckPath(root, "Model");
            else
                GlobalConfig.CheckPath(root, _modelFolder);
            RaisePropertyChanged(nameof(ModelPath));

            if (string.IsNullOrWhiteSpace(_apiFolder))
                GlobalConfig.CheckPath(root, "Api");
            else
                GlobalConfig.CheckPath(root, _apiFolder);
            RaisePropertyChanged(nameof(ApiPath));

            if (string.IsNullOrWhiteSpace(_pageFolder))
                GlobalConfig.CheckPath(root, Name);
            else
                GlobalConfig.CheckPath(root, _pageFolder);
            RaisePropertyChanged(nameof(PagePath));
        }

        /// <summary>
        /// 接口代码路径
        /// </summary>
        /// <remark>
        /// 接口代码路径
        /// </remark>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"接口代码路径"), Description("接口代码路径")]
        public string ApiPath => FormatPath(_apiFolder ?? "Api");

        /// <summary>
        /// 模型代码路径
        /// </summary>
        /// <remark>
        /// 模型代码路径
        /// </remark>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"模型代码路径"), Description("模型代码路径")]
        public string ModelPath => FormatPath(_modelFolder ?? "Model");

        /// <summary>
        /// 源代码路径
        /// </summary>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"源代码路径"), Description("源代码路径")]
        public string SrcPath => Path.Combine(Solution.RootPath, Solution.SrcFolder ?? "src");

        /// <summary>
        /// 格式化路径
        /// </summary>
        /// <param name="end">结束目录</param>
        /// <param name="isRoot">是示使用根</param>
        /// <returns></returns>
        public string FormatPath(string end, bool isRoot = false)
        {
            var folders = new List<string>
            {
                Solution.SrcFolder ?? "src"
            };
            if (!isRoot)
            {
                folders.Add(BranchFolder);
            }
            if (!end.IsMissing())
            {
                folders.Add(end);
            }
            if (!string.IsNullOrWhiteSpace(Solution.RootPath))
                return IOHelper.CheckPath(Solution.RootPath, folders.ToArray());
            return folders.LinkToString("\\");
        }

        /// <summary>
        /// WEB页面(C#)
        /// </summary>
        /// <remark>
        /// 页面代码路径
        /// </remark>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"WEB页面(C#)"), Description("页面代码路径")]
        public string PagePath => $"{Solution.PagePath}\\{ PageFolder}";

        /// <summary>
        /// 移动端(C#)
        /// </summary>
        [DataMember, JsonProperty("_mobileCsPath", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _mobileCsPath;

        /// <summary>
        /// 移动端(C#)
        /// </summary>
        /// <remark>
        /// 移动端代码路径
        /// </remark>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"移动端(C#)"), Description("移动端代码路径")]
        public string MobileCsPath
        {
            get => _mobileCsPath;
            set
            {
                if (_mobileCsPath == value)
                    return;
                BeforePropertyChange(nameof(MobileCsPath), _mobileCsPath, value);
                _mobileCsPath = string.IsNullOrWhiteSpace(value) ? null : value.Trim('\\', '/').Trim();
                OnPropertyChanged(nameof(MobileCsPath));
            }
        }

        /// <summary>
        /// 服务端(C++)
        /// </summary>
        [DataMember, JsonProperty("_codePath", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _cppCodePath;

        /// <summary>
        /// 服务端(C++)
        /// </summary>
        /// <remark>
        /// C++代码地址
        /// </remark>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"服务端(C++)"), Description("C++代码地址")]
        public string CppCodePath
        {
            get => _cppCodePath;
            set
            {
                if (_cppCodePath == value)
                    return;
                BeforePropertyChange(nameof(CppCodePath), _cppCodePath, value);
                _cppCodePath = string.IsNullOrWhiteSpace(value) ? null : value.Trim('\\', '/').Trim();
                OnPropertyChanged(nameof(CppCodePath));
            }
        }

        /// <summary>
        /// 业务逻辑(C#)的说明文字
        /// </summary>
        const string BusinessPath_Description = @"业务逻辑代码路径,在有C++时需要,因为要关联C++项目而多一层";

        /// <summary>
        /// 业务逻辑(C#)
        /// </summary>
        [DataMember, JsonProperty("_blPath", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _businessPath;

        /// <summary>
        /// 业务逻辑(C#)
        /// </summary>
        /// <remark>
        /// 业务逻辑代码路径,在有C++时需要,因为要关联C++项目而多一层
        /// </remark>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"业务逻辑(C#)"), Description(BusinessPath_Description)]
        public string BusinessPath
        {
            get => _businessPath;
            set
            {
                if (_businessPath == value)
                    return;
                BeforePropertyChange(nameof(BusinessPath), _businessPath, value);
                _businessPath = string.IsNullOrWhiteSpace(value) ? null : value.Trim('\\', '/').Trim();
                OnPropertyChanged(nameof(BusinessPath));
            }
        }

        #endregion

        #region 数据库

        /// <summary>
        /// 数据库类型
        /// </summary>
        [DataMember, JsonProperty("_dbType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal DataBaseType _dbType;

        /// <summary>
        /// 数据库类型
        /// </summary>
        /// <remark>
        /// 数据库类型
        /// </remark>
        [JsonIgnore]
        [Category(@"数据库"), DisplayName(@"数据库类型"), Description("数据库类型")]
        public DataBaseType DbType
        {
            get => _dbType;
            set
            {
                if (_dbType == value)
                    return;
                BeforePropertyChange(nameof(DbType), _dbType, value);
                _dbType = value;
                OnPropertyChanged(nameof(DbType));
            }
        }

        /// <summary>
        /// 数据库地址
        /// </summary>
        [DataMember, JsonProperty("_dbHost", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbHost;

        /// <summary>
        /// 数据库地址
        /// </summary>
        /// <remark>
        /// 数据库地址
        /// </remark>
        [JsonIgnore]
        [Category(@"数据库"), DisplayName(@"数据库地址"), Description("数据库地址")]
        public string DbHost
        {
            get => _dbHost;
            set
            {
                if (_dbHost == value)
                    return;
                BeforePropertyChange(nameof(DbHost), _dbHost, value);
                _dbHost = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(DbHost));
            }
        }

        /// <summary>
        /// 数据库名称
        /// </summary>
        [DataMember, JsonProperty("_dbSoruce", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbSoruce;

        /// <summary>
        /// 数据库名称
        /// </summary>
        /// <remark>
        /// 数据库名称
        /// </remark>
        [JsonIgnore]
        [Category(@"数据库"), DisplayName(@"数据库名称"), Description("数据库名称")]
        public string DbSoruce
        {
            get => _dbSoruce;
            set
            {
                if (_dbSoruce == value)
                    return;
                BeforePropertyChange(nameof(DbSoruce), _dbSoruce, value);
                _dbSoruce = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(DbSoruce));
            }
        }

        /// <summary>
        /// 数据库密码
        /// </summary>
        [DataMember, JsonProperty("_dbPassWord", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbPassWord;

        /// <summary>
        /// 数据库密码
        /// </summary>
        /// <remark>
        /// 数据库名称
        /// </remark>
        [JsonIgnore]
        [Category(@"数据库"), DisplayName(@"数据库密码"), Description("数据库密码")]
        public string DbPassWord
        {
            get => _dbPassWord;
            set
            {
                if (_dbPassWord == value)
                    return;
                BeforePropertyChange(nameof(DbPassWord), _dbPassWord, value);
                _dbPassWord = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(DbPassWord));
            }
        }

        /// <summary>
        /// 数据库密码
        /// </summary>
        [DataMember, JsonProperty("_dbPort", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal ushort _dbPort;

        /// <summary>
        /// 数据库密码
        /// </summary>
        /// <remark>
        /// 数据库名称
        /// </remark>
        [JsonIgnore]
        [Category(@"数据库"), DisplayName(@"数据库密码"), Description("数据库密码")]
        public ushort DbPort
        {
            get => _dbPort;
            set
            {
                if (_dbPort == value)
                    return;
                ushort pt = (ushort)(value <= 0 || value > 65535 ? 3306 : value);
                BeforePropertyChange(nameof(DbPort), _dbPort, pt);
                _dbPort = pt;
                OnPropertyChanged(nameof(_dbPort));
            }
        }

        /// <summary>
        /// 数据库用户
        /// </summary>
        [DataMember, JsonProperty("_dbUser", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbUser;

        /// <summary>
        /// 数据库用户
        /// </summary>
        /// <remark>
        /// 数据库名称
        /// </remark>
        [JsonIgnore]
        [Category(@"数据库"), DisplayName(@"数据库用户"), Description("数据库名称")]
        public string DbUser
        {
            get => _dbUser;
            set
            {
                if (_dbUser == value)
                    return;
                BeforePropertyChange(nameof(DbUser), _dbUser, value);
                _dbUser = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(DbUser));
            }
        }

        #endregion

        #region 数据模型

        /// <summary>
        /// 应用标识
        /// </summary>
        [DataMember, JsonProperty("_appId", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _appId;

        /// <summary>
        /// 应用标识
        /// </summary>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"应用标识"), Description("应用标识")]
        public string AppId
        {
            get => _appId;
            set
            {
                if (_appId == value)
                    return;
                BeforePropertyChange(nameof(AppId), _appId, value);
                _appId = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(AppId));
            }
        }

        /// <summary>
        /// 项目类型
        /// </summary>
        [DataMember, JsonProperty("_projectType", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _projectType;

        /// <summary>
        /// 项目类型
        /// </summary>
        /// <remark>
        /// 项目类型
        /// </remark>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"项目类型"), Description("项目类型")]
        public string ProjectType
        {
            get => _projectType ?? "WebApp";
            set
            {
                if (_projectType == value)
                    return;
                BeforePropertyChange(nameof(ProjectType), _projectType, value);
                _projectType = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ProjectType));
            }
        }

        /// <summary>
        /// 代码风格
        /// </summary>
        [DataMember, JsonProperty("codeStyle", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _codeStyle = CodeStyleConst.Style.General;

        /// <summary>
        /// 代码风格
        /// </summary>
        [JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"代码风格"), Description("支持不同的命名与编码风格")]
        public string CodeStyle
        {
            get => _codeStyle;
            set
            {
                if (_codeStyle == value)
                    return;
                BeforePropertyChange(nameof(CodeStyle), _codeStyle, value);
                _codeStyle = value;
                OnPropertyChanged(nameof(CodeStyle));
            }
        }

        /// <summary>
        /// 运行时只读
        /// </summary>
        [DataMember, JsonProperty("_readOnly", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _readOnly;

        /// <summary>
        /// 运行时只读
        /// </summary>
        /// <remark>
        /// 运行时只读
        /// </remark>
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"运行时只读"), Description("运行时只读")]
        public bool ReadOnly
        {
            get => _readOnly;
            set
            {
                if (_readOnly == value)
                    return;
                BeforePropertyChange(nameof(ReadOnly), _readOnly, value);
                _readOnly = value;
                OnPropertyChanged(nameof(ReadOnly));
            }
        }

        /// <summary>
        /// 引用的命名空间
        /// </summary>
        [DataMember, JsonProperty("usingNameSpaces", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _usingNameSpaces;

        /// <summary>
        /// 引用的命名空间
        /// </summary>
        /// <remark>
        /// 引用的命名空间
        /// </remark>
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"引用的命名空间"), Description("引用的命名空间")]
        public string UsingNameSpaces
        {
            get => _usingNameSpaces;
            set
            {
                if (_usingNameSpaces == value)
                    return;
                if (value != null)
                {
                    var words = value.Split(new[] { '\r', '\n', ';', '；' }, StringSplitOptions.RemoveEmptyEntries);
                    value = words.Length == 0 ? null : words.LinkToString(";\r\n") + ";";
                }
                BeforePropertyChange(nameof(UsingNameSpaces), _usingNameSpaces, value);
                _usingNameSpaces = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(UsingNameSpaces));
            }
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        [DataMember, JsonProperty("_entityNameSpace", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _nameSpace;

        /// <summary>
        /// 命名空间
        /// </summary>
        /// <remark>
        /// 命名空间
        /// </remark>
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"命名空间"), Description("命名空间")]
        public string NameSpace
        {
            get => WorkContext.InCoderGenerating ? _nameSpace ?? Solution.NameSpace ?? Name : _nameSpace;
            set
            {
                if (_nameSpace == value)
                    return;
                if (value == Solution.NameSpace)
                    value = null;
                BeforePropertyChange(nameof(NameSpace), _nameSpace, value);
                _nameSpace = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(NameSpace));
            }
        }

        /// <summary>
        /// 数据项目名称
        /// </summary>
        [DataMember, JsonProperty("_dbName", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _dataBaseObjectName;

        /// <summary>
        /// 数据项目名称
        /// </summary>
        /// <remark>
        /// 数据项目名称
        /// </remark>
        [JsonIgnore]
        [Category(@"数据模型"), DisplayName(@"数据项目名称"), Description("数据项目名称")]
        public string DataBaseObjectName
        {
            get => WorkContext.InCoderGenerating ? _dataBaseObjectName ?? Name : _dataBaseObjectName;
            set
            {
                if (_dataBaseObjectName == value)
                    return;
                if (value == Name)
                    value = null;
                BeforePropertyChange(nameof(DataBaseObjectName), _dataBaseObjectName, value);
                _dataBaseObjectName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(DataBaseObjectName));
            }
        }
        #endregion

        #region 设计器支持



        [DataMember, JsonProperty("noClassify", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        bool _noClassify;
        /// <summary>
        /// 无分类
        /// </summary>
        [JsonIgnore]
        public bool NoClassify
        {
            get => _noClassify;
            set
            {
                if (_noClassify == value)
                    return;
                BeforePropertyChange(nameof(NoClassify), _noClassify, value);
                _noClassify = value;
                OnPropertyChanged(nameof(NoClassify));
            }
        }

        /// <summary>
        /// 加入实体
        /// </summary>
        /// <param name="classify"></param>
        public void Add(EntityClassify classify)
        {
            if (classify == null)
                return;
            try
            {
                classify.Project = this;
                classify.Option.IsDelete = false;
                Classifies.TryAdd(classify);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        /// <summary>
        /// 加入实体
        /// </summary>
        /// <param name="entity"></param>
        public void Add(ModelConfig model)
        {
            if (model == null)
                return;
            try
            {
                SolutionConfig.Current.Add(model);
                model.Project = this;
                model.Option.IsDelete = false;
                Models.TryAdd(model);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        /// <summary>
        /// 加入实体
        /// </summary>
        /// <param name="entity"></param>
        public void Add(EntityConfig entity)
        {
            if (entity == null)
                return;
            try
            {
                SolutionConfig.Current.Add(entity);
                entity.Project = this;
                entity.Option.IsDelete = false;
                Entities.TryAdd(entity);
                if (string.IsNullOrWhiteSpace(entity.Classify))
                    return;
                var classf = Classifies.FirstOrDefault(p => p.Name == entity.Classify);
                if (classf == null)
                {
                    Classifies.Add(classf = new EntityClassify
                    {
                        Project = this,
                        Name = entity.Classify,
                        Caption = entity.Classify
                    });
                }
                classf.Items.TryAdd(entity);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        /// <summary>
        /// 加入实体
        /// </summary>
        /// <param name="enumConfig"></param>
        public void Add(EnumConfig enumConfig)
        {
            if (enumConfig == null)
                return;
            try
            {
                SolutionConfig.Current.Add(enumConfig);
                enumConfig.Project?.Remove(enumConfig);
                enumConfig.Project = this;
                enumConfig.Option.IsDelete = false;
                Enums.TryAdd(enumConfig);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        /// <summary>
        /// 加入实体
        /// </summary>
        /// <param name="api"></param>
        public void Add(ApiItem api)
        {
            SolutionConfig.Current.Add(api);
            api.Project = this;
            api.Option.IsDelete = false;
            ApiItems.TryAdd(api);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="classify"></param>
        public void Remove(EntityClassify classify)
        {
            try
            {
                classify.Option.IsDelete = true;
                Classifies.Remove(classify);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        /// <summary>
        /// 加入实体
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(ModelConfig model)
        {
            try
            {
                model.Option.IsDelete = true;
                SolutionConfig.Current.Remove(model);
                model.Project = this;
                Models.Remove(model);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        /// <summary>
        /// 加入实体
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(EntityConfig entity)
        {
            try
            {
                entity.Option.IsDelete = true;
                SolutionConfig.Current.Remove(entity);
                entity.Project = this;
                Entities.Remove(entity);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        /// <summary>
        /// 加入实体
        /// </summary>
        /// <param name="enumConfig"></param>
        public void Remove(EnumConfig enumConfig)
        {
            try
            {
                enumConfig.Option.IsDelete = true;
                SolutionConfig.Current.Remove(enumConfig);
                enumConfig.Project = this;
                Enums.Remove(enumConfig);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        /// <summary>
        /// 加入实体
        /// </summary>
        /// <param name="api"></param>
        public void Remove(ApiItem api)
        {
            try
            {
                api.Option.IsDelete = true;
                SolutionConfig.Current.Remove(api);
                api.Project = this;
                ApiItems.Remove(api);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        }
        #endregion

        #region 解决方案

        [DataMember, JsonProperty("apiKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        Guid _apiKey;
        /// <summary>
        /// API的GUID
        /// </summary>
        [JsonIgnore]
        public Guid ApiKey
        {
            get
            {
                if (_apiKey == Guid.Empty)
                {
                    _apiKey = Guid.NewGuid();
                    OnPropertyChanged(nameof(ApiKey));
                }
                return _apiKey;
            }
        }


        [DataMember, JsonProperty("modelKey", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        Guid _modelKey;
        /// <summary>
        /// 模型的GUID
        /// </summary>
        [JsonIgnore]
        public Guid ModelKey
        {
            get
            {
                if (_modelKey == Guid.Empty)
                {
                    _modelKey = Guid.NewGuid();
                    OnPropertyChanged(nameof(ModelKey));
                }
                return _modelKey;
            }
        }

        [JsonIgnore]
        ISimpleConfig IChildrenConfig.Parent { get; set; }

        #endregion

    }
}