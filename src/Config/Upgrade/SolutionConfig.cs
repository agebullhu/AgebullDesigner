/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/7/12 22:06:40*/
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
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// 解决方案配置
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class SolutionConfig : ParentConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public SolutionConfig()
        {
        }

        #endregion

 
        #region 设计器支持

        /// <summary>
        /// 上帝模式
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal bool _godMode;

        /// <summary>
        /// 上帝模式
        /// </summary>
        /// <remark>
        /// 可以任意修改任意配置的上帝模式
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"设计器支持"),DisplayName(@"上帝模式"),Description("可以任意修改任意配置的上帝模式")]
        public bool GodMode
        {
            get
            {
                return _godMode;
            }
            set
            {
                if(_godMode == value)
                    return;
                BeforePropertyChanged(nameof(GodMode), _godMode,value);
                _godMode = value;
                OnPropertyChanged(nameof(GodMode));
            }
        } 
        #endregion 
        #region 对象集合

        /// <summary>
        /// 枚举集合
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal ObservableCollection<EnumConfig> _enums;

        /// <summary>
        /// 枚举集合
        /// </summary>
        /// <remark>
        /// 枚举集合
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"对象集合"),DisplayName(@"枚举集合"),Description("枚举集合")]
        public ObservableCollection<EnumConfig> Enums
        {
            get
            {
                if (_enums != null)
                    return _enums;
                _enums = new ObservableCollection<EnumConfig>();
                OnPropertyChanged(nameof(Enums));
                return _enums;
            }
            set
            {
                if(_enums == value)
                    return;
                BeforePropertyChanged(nameof(Enums), _enums,value);
                _enums = value;
                OnPropertyChanged(nameof(Enums));
            }
        }

        /// <summary>
        /// 类型(C++)集合
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal ObservableCollection<TypedefItem> _typedefItems;

        /// <summary>
        /// 类型(C++)集合
        /// </summary>
        /// <remark>
        /// 所有C++类型定义
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"对象集合"),DisplayName(@"类型(C++)集合"),Description("所有C++类型定义")]
        public ObservableCollection<TypedefItem> TypedefItems
        {
            get
            {
                if (_typedefItems != null)
                    return _typedefItems;
                _typedefItems = new ObservableCollection<TypedefItem>();
                OnPropertyChanged(nameof(TypedefItems));
                return _typedefItems;
            }
            set
            {
                if(_typedefItems == value)
                    return;
                BeforePropertyChanged(nameof(TypedefItems), _typedefItems,value);
                _typedefItems = value;
                OnPropertyChanged(nameof(TypedefItems));
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
        /// 所有表设置
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"对象集合"),DisplayName(@"实体集合"),Description("所有表设置")]
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

        /// <summary>
        /// 项目集合
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal ObservableCollection<ProjectConfig> _projects;

        /// <summary>
        /// 项目集合
        /// </summary>
        /// <remark>
        /// 项目
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"对象集合"),DisplayName(@"项目集合"),Description("项目")]
        public ObservableCollection<ProjectConfig> Projects
        {
            get
            {
                if (_projects != null)
                    return _projects;
                _projects = new ObservableCollection<ProjectConfig>();
                OnPropertyChanged(nameof(Projects));
                return _projects;
            }
            set
            {
                if(_projects == value)
                    return;
                BeforePropertyChanged(nameof(Projects), _projects,value);
                _projects = value;
                OnPropertyChanged(nameof(Projects));
            }
        }

        /// <summary>
        /// 子级(继承)
        /// </summary>
        /// <remark>
        /// 子级(继承),所有子项目
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"对象集合"),DisplayName(@"子级(继承)"),Description("子级(继承),所有子项目")]
        public override IEnumerable<ConfigBase> MyChilds => _projects;

        /// <summary>
        /// API集合
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal ObservableCollection<ApiItem> _apiItems;

        /// <summary>
        /// API集合
        /// </summary>
        /// <remark>
        /// 对应的API集合
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"对象集合"),DisplayName(@"API集合"),Description("对应的API集合")]
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
                if(_apiItems == value)
                    return;
                BeforePropertyChanged(nameof(ApiItems), _apiItems,value);
                _apiItems = value;
                OnPropertyChanged(nameof(ApiItems));
            }
        }

        /// <summary>
        /// 通知集合
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal ObservableCollection<NotifyItem> _notifyItems;

        /// <summary>
        /// 通知集合
        /// </summary>
        /// <remark>
        /// 对应的通知集合
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"对象集合"),DisplayName(@"通知集合"),Description("对应的通知集合")]
        public ObservableCollection<NotifyItem> NotifyItems
        {
            get
            {
                if (_notifyItems != null)
                    return _notifyItems;
                _notifyItems = new ObservableCollection<NotifyItem>();
                OnPropertyChanged(nameof(NotifyItems));
                return _notifyItems;
            }
            set
            {
                if(_notifyItems == value)
                    return;
                BeforePropertyChanged(nameof(NotifyItems), _notifyItems,value);
                _notifyItems = value;
                OnPropertyChanged(nameof(NotifyItems));
            }
        } 
        #endregion 
        #region 代码生成

        /// <summary>
        /// 解决方案根路径
        /// </summary>
        [DataMember,JsonProperty("_rootPath", NullValueHandling = NullValueHandling.Ignore)]
        internal string _rootPath;

        /// <summary>
        /// 解决方案根路径
        /// </summary>
        /// <remark>
        /// 解决方案根路径
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"代码生成"),DisplayName(@"解决方案根路径"),Description("解决方案根路径")]
        public string RootPath
        {
            get
            {
                return _rootPath;
            }
            set
            {
                if(_rootPath == value)
                    return;
                BeforePropertyChanged(nameof(RootPath), _rootPath,value);
                _rootPath = value;
                OnPropertyChanged(nameof(RootPath));
            }
        }

        /// <summary>
        /// 解决方案命名空间
        /// </summary>
        [DataMember,JsonProperty("_nameSpace", NullValueHandling = NullValueHandling.Ignore)]
        internal string _nameSpace;

        /// <summary>
        /// 解决方案命名空间
        /// </summary>
        /// <remark>
        /// 解决方案根路径
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"代码生成"),DisplayName(@"解决方案命名空间"),Description("解决方案根路径")]
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
        #endregion 
        #region 系统设置

        /// <summary>
        /// 解决方案类型
        /// </summary>
        [DataMember,JsonProperty("SolutionType", NullValueHandling = NullValueHandling.Ignore)]
        internal SolutionType _solutionType;

        /// <summary>
        /// 解决方案类型
        /// </summary>
        /// <remark>
        /// 解决方案类型
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@"系统设置"),DisplayName(@"解决方案类型"),Description("解决方案类型")]
        public SolutionType SolutionType
        {
            get
            {
                return _solutionType;
            }
            set
            {
                if(_solutionType == value)
                    return;
                BeforePropertyChanged(nameof(SolutionType), _solutionType,value);
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
        [IgnoreDataMember,JsonIgnore]
        [Category(@"系统设置"),DisplayName(@"是否一般WEB应用"),Description("是否一般WEB应用")]
        public bool IsWeb => SolutionType == SolutionType.Web; 
        #endregion

    }
}