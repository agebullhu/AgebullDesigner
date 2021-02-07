using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// HTTP方法
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// GET
        /// </summary>
        GET,
        /// <summary>
        /// POST
        /// </summary>
        POST,
        /// <summary>
        /// DELETE
        /// </summary>
        DELETE
    }

    /// <summary>
    /// 表示一个API节点
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ApiItem : ProjectChildConfigBase, ICommandItem
    {

        #region 字段定义

        private const string Method_Description = @"Api调用方式（GET、POST）";

        /// <summary>
        /// Api调用方式
        /// </summary>
        [DataMember, JsonProperty("Method", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal HttpMethod _Method;

        /// <summary>
        /// Api调用方式
        /// </summary>
        /// <remark>
        /// Api调用方式（GET、POST)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName(Method_Description), Description(Method_Description)]
        public HttpMethod Method
        {
            get => _Method;
            set
            {
                if (_Method == value)
                    return;
                BeforePropertyChanged(nameof(Method), _Method, value);
                _Method = value;
                OnPropertyChanged(nameof(Method));
            }
        }
        private const string Project_Description = @"所在的项目";

        /// <summary>
        /// 请求参数名称
        /// </summary>
        [DataMember, JsonProperty("CallArg", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _callArg;

        /// <summary>
        /// 请求参数名称
        /// </summary>
        /// <remark>
        /// 请求参数名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("请求参数名称"), Description("请求参数名称")]
        public string CallArg
        {
            get => _callArg;
            set
            {
                if (_callArg == value)
                    return;
                BeforePropertyChanged(nameof(CallArg), _callArg, value);
                _callArg = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(CallArg));
                OnPropertyChanged(nameof(Argument));
            }
        }
        /// <summary>
        /// API编码
        /// </summary>
        [DataMember, JsonProperty("Code", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _code;

        /// <summary>
        /// API编码
        /// </summary>
        /// <remark>
        /// API编码
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("API编码"), Description("API编码")]
        public string Code
        {
            get => _code;
            set
            {
                if (_code == value)
                    return;
                BeforePropertyChanged(nameof(Code), _code, value);
                _code = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Code));
            }
        }

        /// <summary>
        /// 返回参数名称
        /// </summary>
        [DataMember, JsonProperty("ResultArg", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _resultArg;

        /// <summary>
        /// 返回参数名称
        /// </summary>
        /// <remark>
        /// 返回参数名称
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("返回参数名称"), Description("返回参数名称")]
        public string ResultArg
        {
            get => _resultArg;
            set
            {
                if (_resultArg == value)
                    return;
                BeforePropertyChanged(nameof(ResultArg), _resultArg, value);
                _resultArg = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ResultArg));
                OnPropertyChanged(nameof(Result));
            }
        }

        /// <summary>
        /// 路由路径
        /// </summary>
        [DataMember, JsonProperty("RoutePath", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _routePath;

        /// <summary>
        /// 路由路径
        /// </summary>
        /// <remark>
        /// 路由路径
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("路由路径"), Description("路由路径")]
        public string RoutePath
        {
            get => _routePath;
            set
            {
                if (_routePath == value)
                    return;
                BeforePropertyChanged(nameof(RoutePath), _routePath, value);
                _routePath = string.IsNullOrWhiteSpace(value) ? null : value.Trim().Trim('/');
                OnPropertyChanged(nameof(RoutePath));
            }
        }

        /// <summary>
        /// 是否用户命令
        /// </summary>
        [DataMember, JsonProperty("IsUserCommand", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isUserCommand;

        /// <summary>
        /// 是否用户命令
        /// </summary>
        /// <remark>
        /// 是否用户命令
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("是否用户命令"), Description("是否用户命令")]
        public bool IsUserCommand
        {
            get => _isUserCommand;
            set
            {
                if (_isUserCommand == value)
                    return;
                BeforePropertyChanged(nameof(IsUserCommand), _isUserCommand, value);
                _isUserCommand = value;
                OnPropertyChanged(nameof(IsUserCommand));
            }
        }
        /*// <summary>
        /// 原始内容
        /// </summary>
        [DataMember, JsonProperty("Org",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _org;

        /// <summary>
        /// 原始内容
        /// </summary>
        /// <remark>
        /// 原始内容
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("原始内容"), Description("原始内容")]
        public string Org
        {
            get
            {
                return _org;
            }
            set
            {
                if (_org == value)
                    return;
                BeforePropertyChanged(nameof(Org), _org, value);
                _org = value;
                OnPropertyChanged(nameof(Org));
            }
        }
        
        
        /// <summary>
        /// 是否用户命令
        /// </summary>
        [DataMember, JsonProperty("IsUserCommand",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isUserCommand;

        /// <summary>
        /// 是否用户命令
        /// </summary>
        /// <remark>
        /// 是否用户命令
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("是否用户命令"), Description("是否用户命令")]
        public bool IsUserCommand
        {
            get
            {
                return _isUserCommand;
            }
            set
            {
                if (_isUserCommand == value)
                    return;
                BeforePropertyChanged(nameof(IsUserCommand), _isUserCommand, value);
                _isUserCommand = value;
                OnPropertyChanged(nameof(IsUserCommand));
            }
        }

        private const string Friend_Description = @"对应的另一半(API与Notify的关系)";

        /// <summary>
        /// 对应的另一半
        /// </summary>
        [DataMember, JsonProperty("Friend",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _friend;

        /// <summary>
        /// 对应的另一半
        /// </summary>
        /// <remark>
        /// 对应的另一半(API与Notify的关系)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("对应的另一半"), Description(Friend_Description)]
        public string Friend
        {
            get
            {
                return _friend;
            }
            set
            {
                if (_friend == value)
                    return;
                BeforePropertyChanged(nameof(Friend), _friend, value);
                _friend = value;
                OnPropertyChanged(nameof(Friend));
            }
        }

        private const string FriendKey_Description = @"对应的另一半(API与Notify的关系)";

        /// <summary>
        /// 对应的另一半
        /// </summary>
        [DataMember, JsonProperty("FriendKey",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal Guid _friendKey;

        /// <summary>
        /// 对应的另一半
        /// </summary>
        /// <remark>
        /// 对应的另一半(API与Notify的关系)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("对应的另一半"), Description(FriendKey_Description)]
        public Guid FriendKey
        {
            get
            {
                return _friendKey;
            }
            set
            {
                if (_friendKey == value)
                    return;
                BeforePropertyChanged(nameof(FriendKey), _friendKey, value);
                _friendKey = value;
                OnPropertyChanged(nameof(FriendKey));
            }
        }

        /// <summary>
        /// 本地命令
        /// </summary>
        [DataMember, JsonProperty("LocalCommand",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _localCommand;

        /// <summary>
        /// 本地命令
        /// </summary>
        /// <remark>
        /// 本地命令(不转发)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("本地命令"), Description("本地命令(不转发)")]
        public bool LocalCommand
        {
            get
            {
                return _localCommand;
            }
            set
            {
                if (_localCommand == value)
                    return;
                BeforePropertyChanged(nameof(LocalCommand), _localCommand, value);
                _localCommand = value;
                OnPropertyChanged(nameof(LocalCommand));
            }
        }*/
        #endregion

        #region 接口

        /// <summary>
        /// 参数实体
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private EntityConfig _argument;

        /// <summary>
        /// 参数实体
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public EntityConfig Argument
        {
            get => CallArg == null ? null : (_argument ??= GlobalConfig.GetEntity(CallArg));
            set => CallArg = value?.Name;
        }

        /// <summary>
        /// 返回值
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private EntityConfig _result;

        /// <summary>
        /// 本地对象名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public EntityConfig Result
        {
            get => ResultArg == null ? null : (_result ??= GlobalConfig.GetEntity(ResultArg));
            set => ResultArg = value?.Name;
        }


        /// <summary>
        /// 请求参数名称
        /// </summary>
        string ICommandItem.OrgArg => CallArg;

        string ICommandItem.CurArg => ResultArg;

        #endregion
    }
}