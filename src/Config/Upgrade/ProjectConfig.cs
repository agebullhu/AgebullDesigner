/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/7/12 22:06:39*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using Agebull.Common;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 项目设置
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class ProjectConfig : ParentConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public ProjectConfig()
        {
        }

        #endregion
        #region 子级

        /// <summary>
        /// 子级(继承)
        /// </summary>
        /// <remark>
        /// 子级(继承)
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"子级"),DisplayName(@"子级(继承)"),Description("子级(继承)")]
        public override IEnumerable<ConfigBase> MyChilds => _entities;

        /// <summary>
        /// 实体分组
        /// </summary>
        [DataMember,JsonProperty("Classifies", NullValueHandling = NullValueHandling.Ignore)]
        internal ConfigCollection<ClassifyItem<EntityConfig>> _classifies;

        /// <summary>
        /// 实体分组
        /// </summary>
        /// <remark>
        /// 实体分组
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"子级"),DisplayName(@"实体分组"),Description("实体分组")]
        public ConfigCollection<ClassifyItem<EntityConfig>> Classifies
        {
            get
            {
                if (_classifies != null)
                    return _classifies;
                _classifies = new ConfigCollection<ClassifyItem<EntityConfig>>();
                OnPropertyChanged(nameof(Classifies));
                return _classifies;
            }
            set
            {
                if(_classifies == value)
                    return;
                BeforePropertyChanged(nameof(Classifies), _classifies,value);
                _classifies = value;
                OnPropertyChanged(nameof(Classifies));
            }
        }

        /// <summary>
        /// 实体集合
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal ObservableCollection<EntityConfig> _entities;

        /// <summary>
        /// 实体集合
        /// </summary>
        /// <remark>
        /// 实体集合
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"子级"),DisplayName(@"实体集合"),Description("实体集合")]
        public ObservableCollection<EntityConfig> Entities
        {
            get
            {
                if (_entities != null)
                    return _entities;
                _entities = new ObservableCollection<EntityConfig>();
                OnPropertyChanged(nameof(Entities));
                return _entities;
            }
            set
            {
                if(_entities == value)
                    return;
                BeforePropertyChanged(nameof(Entities), _entities,value);
                _entities = value;
                OnPropertyChanged(nameof(Entities));
            }
        }
        #endregion
        #region API节点集合

        /// <summary>
        /// API节点集合
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        internal ObservableCollection<ApiItem> _apiItems;

        /// <summary>
        /// API节点集合
        /// </summary>
        /// <remark>
        /// API节点集合
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"子级"), DisplayName(@"API节点集合"), Description("API节点集合")]
        public ObservableCollection<ApiItem> ApiItems
        {
            get
            {
                if (_apiItems != null)
                    return _apiItems;
                _apiItems = new ObservableCollection<ApiItem>();
                OnPropertyChanged(nameof(ApiItems));
                return _apiItems;
            }
            set
            {
                if (_apiItems == value)
                    return;
                BeforePropertyChanged(nameof(ApiItems), _apiItems, value);
                _apiItems = value;
                OnPropertyChanged(nameof(ApiItems));
            }
        }
        /// <summary>
        /// 接口名称
        /// </summary>
        [DataMember, JsonProperty("_apiName", NullValueHandling = NullValueHandling.Ignore)]
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
            get
            {
                return _apiName ?? (Name + "Api");
            }
            set
            {
                if (_apiName == value)
                    return;
                BeforePropertyChanged(nameof(ApiName), _apiName, value);
                _apiName = value;
                OnPropertyChanged(nameof(ApiName));
            }
        }
        #endregion

        #region 代码路径
        /// <summary>
        /// 接口代码主文件夹
        /// </summary>
        [DataMember, JsonProperty("_apiFolder", NullValueHandling = NullValueHandling.Ignore)]
        internal string _apiFolder;

        /// <summary>
        /// 接口代码主文件夹
        /// </summary>
        /// <remark>
        /// 接口代码主文件夹
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"接口代码主文件夹"), Description("接口代码主文件夹")]
        public string ApiFolder
        {
            get
            {
                return _apiFolder;
            }
            set
            {
                if (_apiFolder == value)
                    return;
                BeforePropertyChanged(nameof(ApiFolder), _apiFolder, value);
                _apiFolder = value;
                OnPropertyChanged(nameof(ApiFolder));
                ResetPath();
            }
        }

        /// <summary>
        /// 模型代码主文件夹
        /// </summary>
        [DataMember, JsonProperty("_modelFolder", NullValueHandling = NullValueHandling.Ignore)]
        internal string _modelFolder;

        /// <summary>
        /// 模型代码主文件夹
        /// </summary>
        /// <remark>
        /// 模型代码主文件夹
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"模型代码主文件夹"), Description("模型代码主文件夹")]
        public string ModelFolder
        {
            get
            {
                return _modelFolder;
            }
            set
            {
                if (_modelFolder == value)
                    return;
                BeforePropertyChanged(nameof(ModelFolder), _modelFolder, value);
                _modelFolder = value;
                OnPropertyChanged(nameof(ModelFolder));
                ResetPath();
            }
        }

        /// <summary>
        /// 子级文件夹
        /// </summary>
        [DataMember, JsonProperty("_branchPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _branchFolder;

        /// <summary>
        /// 子级文件夹
        /// </summary>
        /// <remark>
        /// 子级文件夹
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"子级文件夹"), Description("子级文件夹")]
        public string BranchFolder
        {
            get
            {
                return _branchFolder;
            }
            set
            {
                if (_branchFolder == value)
                    return;
                BeforePropertyChanged(nameof(BranchFolder), _branchFolder, value);
                _branchFolder = value;
                OnPropertyChanged(nameof(BranchFolder));
            }
        }

        /// <summary>
        /// 重置模型路径
        /// </summary>
        public void ResetPath()
        {
            string root = IOHelper.CheckPath(Solution.RootPath, Solution.SrcFolder);
            ModelPath = string.IsNullOrWhiteSpace(_modelFolder)
                ? IOHelper.CheckPath(root, "Model")
                : IOHelper.CheckPath(root, _modelFolder);

            ApiPath = string.IsNullOrWhiteSpace(_apiFolder)
                ? IOHelper.CheckPath(root, "Api")
                : IOHelper.CheckPath(root, _apiFolder);
        }

        /// <summary>
        /// 重置模型路径
        /// </summary>
        public string GetPath(params string[] folders)
        {
            string root = IOHelper.CheckPath(Solution.RootPath, Solution.SrcFolder);
            return IOHelper.CheckPath(root, folders);
        }
        
        /// <summary>
        /// 重置模型路径
        /// </summary>
        public string GetModelPath(string type)
        {
            ResetPath();
            return string.IsNullOrWhiteSpace(_branchFolder)
                ? IOHelper.CheckPath(ModelPath, type)
                : IOHelper.CheckPath(ModelPath, type, _branchFolder);
        }

        /// <summary>
        /// 重置模型路径
        /// </summary>
        public string GetModelPath(string type, string sub)
        {
            ResetPath();
            return string.IsNullOrWhiteSpace(_branchFolder)
                ? IOHelper.CheckPath(ModelPath, type, sub)
                : IOHelper.CheckPath(ModelPath, type, sub, _branchFolder);
        }
        /// <summary>
        /// 重置模型路径
        /// </summary>
        public string GetApiPath(string type)
        {
            ResetPath();
            return string.IsNullOrWhiteSpace(_branchFolder)
                ? IOHelper.CheckPath(ApiPath, type)
                : IOHelper.CheckPath(ApiPath, type, _branchFolder);
        }

        /// <summary>
        /// 重置模型路径
        /// </summary>
        public string GetApiPath(string type,string sub)
        {
            ResetPath();
            return string.IsNullOrWhiteSpace(_branchFolder)
                ? IOHelper.CheckPath(ApiPath, type, sub)
                : IOHelper.CheckPath(ApiPath, type, sub, _branchFolder);
        }

        /// <summary>
        /// 接口代码路径
        /// </summary>
        [DataMember, JsonProperty("_apiPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _apiPath;

        /// <summary>
        /// 接口代码路径
        /// </summary>
        /// <remark>
        /// 接口代码路径
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"接口代码路径"), Description("接口代码路径")]
        public string ApiPath
        {
            get
            {
                return _apiPath;
            }
            set
            {
                if (_apiPath == value)
                    return;
                BeforePropertyChanged(nameof(ApiPath), _apiPath, value);
                _apiPath = value;
                OnPropertyChanged(nameof(ApiPath));
            }
        }
        /// <summary>
        /// 模型代码路径
        /// </summary>
        [DataMember,JsonProperty("_modelPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _modelPath;

        /// <summary>
        /// 模型代码路径
        /// </summary>
        /// <remark>
        /// 模型代码路径
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"解决方案"),DisplayName(@"模型代码路径"),Description("模型代码路径")]
        public string ModelPath
        {
            get
            {
                return _modelPath;
            }
            set
            {
                if(_modelPath == value)
                    return;
                BeforePropertyChanged(nameof(ModelPath), _modelPath,value);
                _modelPath = value;
                OnPropertyChanged(nameof(ModelPath));
            }
        }


        /// <summary>
        /// WEB页面(C#)
        /// </summary>
        [DataMember, JsonProperty("_pagePath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _pagePath;

        /// <summary>
        /// WEB页面(C#)
        /// </summary>
        /// <remark>
        /// 页面代码路径
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"WEB页面(C#)"), Description("页面代码路径")]
        public string PagePath
        {
            get
            {
                return _pagePath;
            }
            set
            {
                if (_pagePath == value)
                    return;
                BeforePropertyChanged(nameof(PagePath), _pagePath, value);
                _pagePath = value;
                OnPropertyChanged(nameof(PagePath));
            }
        }
        /// <summary>
        /// 移动端(C#)
        /// </summary>
        [DataMember,JsonProperty("_mobileCsPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _mobileCsPath;

        /// <summary>
        /// 移动端(C#)
        /// </summary>
        /// <remark>
        /// 移动端代码路径
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"解决方案"),DisplayName(@"移动端(C#)"),Description("移动端代码路径")]
        public string MobileCsPath
        {
            get
            {
                return _mobileCsPath;
            }
            set
            {
                if(_mobileCsPath == value)
                    return;
                BeforePropertyChanged(nameof(MobileCsPath), _mobileCsPath,value);
                _mobileCsPath = value;
                OnPropertyChanged(nameof(MobileCsPath));
            }
        }

        /// <summary>
        /// 服务端(C++)
        /// </summary>
        [DataMember,JsonProperty("_codePath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _cppCodePath;

        /// <summary>
        /// 服务端(C++)
        /// </summary>
        /// <remark>
        /// C++代码地址
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"解决方案"),DisplayName(@"服务端(C++)"),Description("C++代码地址")]
        public string CppCodePath
        {
            get
            {
                return _cppCodePath;
            }
            set
            {
                if(_cppCodePath == value)
                    return;
                BeforePropertyChanged(nameof(CppCodePath), _cppCodePath,value);
                _cppCodePath = value;
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
        [DataMember,JsonProperty("_blPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _businessPath;

        /// <summary>
        /// 业务逻辑(C#)
        /// </summary>
        /// <remark>
        /// 业务逻辑代码路径,在有C++时需要,因为要关联C++项目而多一层
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"解决方案"),DisplayName(@"业务逻辑(C#)"),Description(BusinessPath_Description)]
        public string BusinessPath
        {
            get
            {
                return _businessPath;
            }
            set
            {
                if(_businessPath == value)
                    return;
                BeforePropertyChanged(nameof(BusinessPath), _businessPath,value);
                _businessPath = value;
                OnPropertyChanged(nameof(BusinessPath));
            }
        }
        
        #endregion
        #region 数据库

        /// <summary>
        /// 数据库类型
        /// </summary>
        [DataMember,JsonProperty("_dbType", NullValueHandling = NullValueHandling.Ignore)]
        internal DataBaseType _dbType;

        /// <summary>
        /// 数据库类型
        /// </summary>
        /// <remark>
        /// 数据库类型
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据库"),DisplayName(@"数据库类型"),Description("数据库类型")]
        public DataBaseType DbType
        {
            get
            {
                return _dbType;
            }
            set
            {
                if(_dbType == value)
                    return;
                BeforePropertyChanged(nameof(DbType), _dbType,value);
                _dbType = value;
                OnPropertyChanged(nameof(DbType));
            }
        }

        /// <summary>
        /// 数据库地址
        /// </summary>
        [DataMember,JsonProperty("_dbHost", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbHost;

        /// <summary>
        /// 数据库地址
        /// </summary>
        /// <remark>
        /// 数据库地址
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据库"),DisplayName(@"数据库地址"),Description("数据库地址")]
        public string DbHost
        {
            get
            {
                return _dbHost;
            }
            set
            {
                if(_dbHost == value)
                    return;
                BeforePropertyChanged(nameof(DbHost), _dbHost,value);
                _dbHost = value;
                OnPropertyChanged(nameof(DbHost));
            }
        }

        /// <summary>
        /// 数据库名称
        /// </summary>
        [DataMember,JsonProperty("_dbSoruce", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbSoruce;

        /// <summary>
        /// 数据库名称
        /// </summary>
        /// <remark>
        /// 数据库名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据库"),DisplayName(@"数据库名称"),Description("数据库名称")]
        public string DbSoruce
        {
            get
            {
                return _dbSoruce;
            }
            set
            {
                if(_dbSoruce == value)
                    return;
                BeforePropertyChanged(nameof(DbSoruce), _dbSoruce,value);
                _dbSoruce = value;
                OnPropertyChanged(nameof(DbSoruce));
            }
        }

        /// <summary>
        /// 数据库密码
        /// </summary>
        [DataMember,JsonProperty("_dbPassWord", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbPassWord;

        /// <summary>
        /// 数据库密码
        /// </summary>
        /// <remark>
        /// 数据库名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据库"),DisplayName(@"数据库密码"),Description("数据库名称")]
        public string DbPassWord
        {
            get
            {
                return _dbPassWord;
            }
            set
            {
                if(_dbPassWord == value)
                    return;
                BeforePropertyChanged(nameof(DbPassWord), _dbPassWord,value);
                _dbPassWord = value;
                OnPropertyChanged(nameof(DbPassWord));
            }
        }

        /// <summary>
        /// 数据库用户
        /// </summary>
        [DataMember,JsonProperty("_dbUser", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbUser;

        /// <summary>
        /// 数据库用户
        /// </summary>
        /// <remark>
        /// 数据库名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据库"),DisplayName(@"数据库用户"),Description("数据库名称")]
        public string DbUser
        {
            get
            {
                return _dbUser;
            }
            set
            {
                if(_dbUser == value)
                    return;
                BeforePropertyChanged(nameof(DbUser), _dbUser,value);
                _dbUser = value;
                OnPropertyChanged(nameof(DbUser));
            }
        }
        #endregion
        #region 数据模型

        /// <summary>
        /// 项目类型
        /// </summary>
        [DataMember, JsonProperty("_projectType", NullValueHandling = NullValueHandling.Ignore)]
        internal string _projectType;

        /// <summary>
        /// 项目类型
        /// </summary>
        /// <remark>
        /// 项目类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(@"解决方案"), DisplayName(@"项目类型"), Description("项目类型")]
        public string ProjectType
        {
            get
            {
                return _projectType ?? "WebApp";
            }
            set
            {
                if (_projectType == value)
                    return;
                BeforePropertyChanged(nameof(ProjectType), _pagePath, value);
                _projectType = value;
                OnPropertyChanged(nameof(ProjectType));
            }
        }

        /// <summary>
        /// 运行时只读
        /// </summary>
        [DataMember,JsonProperty("_readOnly", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _readOnly;

        /// <summary>
        /// 运行时只读
        /// </summary>
        /// <remark>
        /// 运行时只读
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"运行时只读"),Description("运行时只读")]
        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                if(_readOnly == value)
                    return;
                BeforePropertyChanged(nameof(ReadOnly), _readOnly,value);
                _readOnly = value;
                OnPropertyChanged(nameof(ReadOnly));
            }
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        [DataMember,JsonProperty("_entityNameSpace", NullValueHandling = NullValueHandling.Ignore)]
        internal string _nameSpace;

        /// <summary>
        /// 命名空间
        /// </summary>
        /// <remark>
        /// 命名空间
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"命名空间"),Description("命名空间")]
        public string NameSpace
        {
            get
            {
                return _nameSpace;
            }
            set
            {
                if(_nameSpace == value)
                    return;
                BeforePropertyChanged(nameof(NameSpace), _nameSpace,value);
                _nameSpace = value;
                OnPropertyChanged(nameof(NameSpace));
            }
        }

        /// <summary>
        /// 数据项目名称
        /// </summary>
        [DataMember,JsonProperty("_dbName", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dataBaseObjectName;

        /// <summary>
        /// 数据项目名称
        /// </summary>
        /// <remark>
        /// 数据项目名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"数据模型"),DisplayName(@"数据项目名称"),Description("数据项目名称")]
        public string DataBaseObjectName
        {
            get
            {
                return _dataBaseObjectName;
            }
            set
            {
                if(_dataBaseObjectName == value)
                    return;
                BeforePropertyChanged(nameof(DataBaseObjectName), _dataBaseObjectName,value);
                _dataBaseObjectName = value;
                OnPropertyChanged(nameof(DataBaseObjectName));
            }
        } 
        #endregion 
        #region 设计器支持

        /// <summary>
        /// 全局项目
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal bool _isGlobal;

        /// <summary>
        /// 全局项目
        /// </summary>
        /// <remark>
        /// 全局项目,是作为设计器支持的基础项目
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"全局项目"),Description("全局项目,是作为设计器支持的基础项目")]
        public bool IsGlobal
        {
            get
            {
                return _isGlobal;
            }
            set
            {
                if(_isGlobal == value)
                    return;
                BeforePropertyChanged(nameof(IsGlobal), _isGlobal,value);
                _isGlobal = value;
                OnPropertyChanged(nameof(IsGlobal));
            }
        } 
        #endregion

    }
    /// <summary>
    /// 项目类型
    /// </summary>
    //[Flags]
    public enum ProjectType
    {
        /// <summary>
        /// BS应用
        /// </summary>
        WebApplition = 0x1,
        /// <summary>
        /// CS应用
        /// </summary>
        WpfApplition = 0x2,
        /// <summary>
        /// WebApi应用
        /// </summary>
        WebApi = 0x3,
    }
}