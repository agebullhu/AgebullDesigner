// // /*****************************************************
// // (c)2016-2016 Copy right Agebull.hu
// // 作者:
// // 工程:CodeRefactor
// // 建立:2016-06-06
// // 修改:2016-08-03
// // *****************************************************/

#region 引用

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

#endregion

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    ///     项目设置
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ProjectConfig : ParentConfigBase
    {

        #region 子级


        /// <summary>
        /// 子级
        /// </summary>
        public override IEnumerable<ConfigBase> MyChilds => _children;

        /// <summary>
        ///     子级
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private ObservableCollection<EntityConfig> _children;

        /// <summary>
        ///     子级
        /// </summary>
        [Browsable(false), IgnoreDataMember, JsonIgnore]
        public ObservableCollection<EntityConfig> Children
        {
            get { return _children ?? (_children = new ObservableCollection<EntityConfig>()); }
            set
            {
                if (_children == value)
                {
                    return;
                }
                _children = value;
                RaisePropertyChanged(() => Children);
            }
        }
        /// <summary>
        ///     子级
        /// </summary>
        [Browsable(false), IgnoreDataMember, JsonIgnore]
        public ObservableCollection<EntityConfig> Entities
        {
            get { return Children; }
            set
            {
                Children = value;
                RaisePropertyChanged(() => Entities);
            }
        }

        /// <summary>
        ///     对应的API集合
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private ObservableCollection<TypedefItem> _typedefItem;

        /// <summary>
        ///     对应的API集合
        /// </summary>
        [Browsable(false), IgnoreDataMember, JsonIgnore]
        public ObservableCollection<TypedefItem> TypedefItems
        {
            get { return _typedefItem ?? (_typedefItem = new ObservableCollection<TypedefItem>()); }
            set
            {
                if (_typedefItem == value)
                {
                    return;
                }
                _typedefItem = value;
                RaisePropertyChanged(() => TypedefItems);
            }
        }
        #endregion
        
        #region 代码路径

        /// <summary>
        /// C# 管理端(WEB页面)
        /// </summary>
        [DataMember, JsonProperty("_pagePath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _pagePath;

        /// <summary>
        /// C# 管理端(WEB页面)
        /// </summary>
        /// <remark>
        /// 页面代码路径
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("代码路径"), DisplayName("C# 管理端(WEB页面)"), Description("页面代码路径")]
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
        /// C# 管理端(数据模型)
        /// </summary>
        [DataMember, JsonProperty("_modelPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _modelPath;

        /// <summary>
        /// C# 管理端(数据模型)
        /// </summary>
        /// <remark>
        /// 模型代码路径
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("代码路径"), DisplayName("C# 管理端(数据模型)"), Description("模型代码路径")]
        public string ModelPath
        {
            get
            {
                return _modelPath;
            }
            set
            {
                if (_modelPath == value)
                    return;
                BeforePropertyChanged(nameof(ModelPath), _modelPath, value);
                _modelPath = value;
                OnPropertyChanged(nameof(ModelPath));
            }
        }

        /// <summary>
        /// C# 移动端
        /// </summary>
        [DataMember, JsonProperty("_mobileCsPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _mobileCsPath;

        /// <summary>
        /// C# 移动端
        /// </summary>
        /// <remark>
        /// 移动端代码路径
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("代码路径"), DisplayName("C# 移动端"), Description("移动端代码路径")]
        public string MobileCsPath
        {
            get
            {
                return _mobileCsPath;
            }
            set
            {
                if (_mobileCsPath == value)
                    return;
                BeforePropertyChanged(nameof(MobileCsPath), _mobileCsPath, value);
                _mobileCsPath = value;
                OnPropertyChanged(nameof(MobileCsPath));
            }
        }

        /// <summary>
        /// C++ 服务端
        /// </summary>
        [DataMember, JsonProperty("_codePath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _codePath;

        /// <summary>
        /// C++ 服务端
        /// </summary>
        /// <remark>
        /// C++代码地址
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("代码路径"), DisplayName("C++ 服务端"), Description("C++代码地址")]
        public string CodePath
        {
            get
            {
                return _codePath;
            }
            set
            {
                if (_codePath == value)
                    return;
                BeforePropertyChanged(nameof(CodePath), _codePath, value);
                _codePath = value;
                OnPropertyChanged(nameof(CodePath));
            }
        }

        /// <summary>
        /// C# 管理端(业务逻辑)
        /// </summary>
        [DataMember, JsonProperty("_blPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _businessPath;

        /// <summary>
        /// C# 管理端(业务逻辑)
        /// </summary>
        /// <remark>
        /// 业务逻辑代码路径
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("代码路径"), DisplayName("C# 管理端(业务逻辑)"), Description("业务逻辑代码路径")]
        public string BusinessPath
        {
            get
            {
                return _businessPath;
            }
            set
            {
                if (_businessPath == value)
                    return;
                BeforePropertyChanged(nameof(BusinessPath), _businessPath, value);
                _businessPath = value;
                OnPropertyChanged(nameof(BusinessPath));
            }
        }

        /// <summary>
        /// C# PC端
        /// </summary>
        [DataMember, JsonProperty("_clientPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _clientCsPath;

        /// <summary>
        /// C# PC端
        /// </summary>
        /// <remark>
        /// 模型代码路径
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("代码路径"), DisplayName("C# PC端"), Description("模型代码路径")]
        public string ClientCsPath
        {
            get
            {
                return _clientCsPath;
            }
            set
            {
                if (_clientCsPath == value)
                    return;
                BeforePropertyChanged(nameof(ClientCsPath), _clientCsPath, value);
                _clientCsPath = value;
                OnPropertyChanged(nameof(ClientCsPath));
            }
        }
        #endregion 代码路径 

        #region 数据库

        /// <summary>
        /// 数据库类型
        /// </summary>
        [DataMember, JsonProperty("_dbType", NullValueHandling = NullValueHandling.Ignore)]
        internal DataBaseType _dbType;

        /// <summary>
        /// 数据库类型
        /// </summary>
        /// <remark>
        /// 数据库类型
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("数据库类型"), Description("数据库类型")]
        public DataBaseType DbType
        {
            get
            {
                return _dbType;
            }
            set
            {
                if (_dbType == value)
                    return;
                BeforePropertyChanged(nameof(DbType), _dbType, value);
                _dbType = value;
                OnPropertyChanged(nameof(DbType));
            }
        }

        /// <summary>
        /// 数据库地址
        /// </summary>
        [DataMember, JsonProperty("_dbHost", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbHost;

        /// <summary>
        /// 数据库地址
        /// </summary>
        /// <remark>
        /// 数据库地址
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("数据库地址"), Description("数据库地址")]
        public string DbHost
        {
            get
            {
                return _dbHost;
            }
            set
            {
                if (_dbHost == value)
                    return;
                BeforePropertyChanged(nameof(DbHost), _dbHost, value);
                _dbHost = value;
                OnPropertyChanged(nameof(DbHost));
            }
        }

        /// <summary>
        /// 数据库名称
        /// </summary>
        [DataMember, JsonProperty("_dbSoruce", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbSoruce;

        /// <summary>
        /// 数据库名称
        /// </summary>
        /// <remark>
        /// 数据库名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("数据库名称"), Description("数据库名称")]
        public string DbSoruce
        {
            get
            {
                return _dbSoruce;
            }
            set
            {
                if (_dbSoruce == value)
                    return;
                BeforePropertyChanged(nameof(DbSoruce), _dbSoruce, value);
                _dbSoruce = value;
                OnPropertyChanged(nameof(DbSoruce));
            }
        }

        /// <summary>
        /// 数据库密码
        /// </summary>
        [DataMember, JsonProperty("_dbPassWord", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbPassWord;

        /// <summary>
        /// 数据库密码
        /// </summary>
        /// <remark>
        /// 数据库名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("数据库密码"), Description("数据库名称")]
        public string DbPassWord
        {
            get
            {
                return _dbPassWord;
            }
            set
            {
                if (_dbPassWord == value)
                    return;
                BeforePropertyChanged(nameof(DbPassWord), _dbPassWord, value);
                _dbPassWord = value;
                OnPropertyChanged(nameof(DbPassWord));
            }
        }

        /// <summary>
        /// 数据库用户
        /// </summary>
        [DataMember, JsonProperty("_dbUser", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dbUser;

        /// <summary>
        /// 数据库用户
        /// </summary>
        /// <remark>
        /// 数据库名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据库"), DisplayName("数据库用户"), Description("数据库名称")]
        public string DbUser
        {
            get
            {
                return _dbUser;
            }
            set
            {
                if (_dbUser == value)
                    return;
                BeforePropertyChanged(nameof(DbUser), _dbUser, value);
                _dbUser = value;
                OnPropertyChanged(nameof(DbUser));
            }
        }
        #endregion 数据库 

        #region 数据模型

        /// <summary>
        /// 运行时只读
        /// </summary>
        [DataMember, JsonProperty("_readOnly", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _readOnly;

        /// <summary>
        /// 运行时只读
        /// </summary>
        /// <remark>
        /// 运行时只读
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据模型"), DisplayName("运行时只读"), Description("运行时只读")]
        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                if (_readOnly == value)
                    return;
                BeforePropertyChanged(nameof(ReadOnly), _readOnly, value);
                _readOnly = value;
                OnPropertyChanged(nameof(ReadOnly));
            }
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        [DataMember, JsonProperty("_entityNameSpace", NullValueHandling = NullValueHandling.Ignore)]
        internal string _nameSpace;

        /// <summary>
        /// 命名空间
        /// </summary>
        /// <remark>
        /// 命名空间
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据模型"), DisplayName("命名空间"), Description("命名空间")]
        public string NameSpace
        {
            get
            {
                return _nameSpace;
            }
            set
            {
                if (_nameSpace == value)
                    return;
                BeforePropertyChanged(nameof(NameSpace), _nameSpace, value);
                _nameSpace = value;
                OnPropertyChanged(nameof(NameSpace));
            }
        }

        /// <summary>
        /// 数据库名称
        /// </summary>
        [DataMember, JsonProperty("_dbName", NullValueHandling = NullValueHandling.Ignore)]
        internal string _dataBaseObjectName;

        /// <summary>
        /// 数据库名称
        /// </summary>
        /// <remark>
        /// 数据库名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category("数据模型"), DisplayName("数据库名称"), Description("数据库名称")]
        public string DataBaseObjectName
        {
            get
            {
                return _dataBaseObjectName;
            }
            set
            {
                if (_dataBaseObjectName == value)
                    return;
                BeforePropertyChanged(nameof(DataBaseObjectName), _dataBaseObjectName, value);
                _dataBaseObjectName = value;
                OnPropertyChanged(nameof(DataBaseObjectName));
            }
        }
        #endregion 数据模型
    }
}