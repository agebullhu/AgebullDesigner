using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// HTTP����
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
    /// ��ʾһ��API�ڵ�
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class ApiItem : ProjectChildConfigBase, ICommandItem
    {

        #region �ֶζ���

        private const string Method_Description = @"Api���÷�ʽ��GET��POST��";

        /// <summary>
        /// Api���÷�ʽ
        /// </summary>
        [DataMember, JsonProperty("Method", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal HttpMethod _Method;

        /// <summary>
        /// Api���÷�ʽ
        /// </summary>
        /// <remark>
        /// Api���÷�ʽ��GET��POST)
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
        private const string Project_Description = @"���ڵ���Ŀ";

        /// <summary>
        /// �����������
        /// </summary>
        [DataMember, JsonProperty("CallArg", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _callArg;

        /// <summary>
        /// �����������
        /// </summary>
        /// <remark>
        /// �����������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("�����������"), Description("�����������")]
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
        /// API����
        /// </summary>
        [DataMember, JsonProperty("Code", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _code;

        /// <summary>
        /// API����
        /// </summary>
        /// <remark>
        /// API����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("API����"), Description("API����")]
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
        /// ���ز�������
        /// </summary>
        [DataMember, JsonProperty("ResultArg", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _resultArg;

        /// <summary>
        /// ���ز�������
        /// </summary>
        /// <remark>
        /// ���ز�������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("���ز�������"), Description("���ز�������")]
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
        /// ·��·��
        /// </summary>
        [DataMember, JsonProperty("RoutePath", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _routePath;

        /// <summary>
        /// ·��·��
        /// </summary>
        /// <remark>
        /// ·��·��
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("·��·��"), Description("·��·��")]
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
        /// �Ƿ��û�����
        /// </summary>
        [DataMember, JsonProperty("IsUserCommand", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isUserCommand;

        /// <summary>
        /// �Ƿ��û�����
        /// </summary>
        /// <remark>
        /// �Ƿ��û�����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("�Ƿ��û�����"), Description("�Ƿ��û�����")]
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
        /// ԭʼ����
        /// </summary>
        [DataMember, JsonProperty("Org",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _org;

        /// <summary>
        /// ԭʼ����
        /// </summary>
        /// <remark>
        /// ԭʼ����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("ԭʼ����"), Description("ԭʼ����")]
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
        /// �Ƿ��û�����
        /// </summary>
        [DataMember, JsonProperty("IsUserCommand",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isUserCommand;

        /// <summary>
        /// �Ƿ��û�����
        /// </summary>
        /// <remark>
        /// �Ƿ��û�����
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("�Ƿ��û�����"), Description("�Ƿ��û�����")]
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

        private const string Friend_Description = @"��Ӧ����һ��(API��Notify�Ĺ�ϵ)";

        /// <summary>
        /// ��Ӧ����һ��
        /// </summary>
        [DataMember, JsonProperty("Friend",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal string _friend;

        /// <summary>
        /// ��Ӧ����һ��
        /// </summary>
        /// <remark>
        /// ��Ӧ����һ��(API��Notify�Ĺ�ϵ)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("��Ӧ����һ��"), Description(Friend_Description)]
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

        private const string FriendKey_Description = @"��Ӧ����һ��(API��Notify�Ĺ�ϵ)";

        /// <summary>
        /// ��Ӧ����һ��
        /// </summary>
        [DataMember, JsonProperty("FriendKey",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal Guid _friendKey;

        /// <summary>
        /// ��Ӧ����һ��
        /// </summary>
        /// <remark>
        /// ��Ӧ����һ��(API��Notify�Ĺ�ϵ)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("��Ӧ����һ��"), Description(FriendKey_Description)]
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
        /// ��������
        /// </summary>
        [DataMember, JsonProperty("LocalCommand",  DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        internal bool _localCommand;

        /// <summary>
        /// ��������
        /// </summary>
        /// <remark>
        /// ��������(��ת��)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("��������"), Description("��������(��ת��)")]
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

        #region �ӿ�

        /// <summary>
        /// ����ʵ��
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private EntityConfig _argument;

        /// <summary>
        /// ����ʵ��
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public EntityConfig Argument
        {
            get => CallArg == null ? null : (_argument ??= GlobalConfig.GetEntity(CallArg));
            set => CallArg = value?.Name;
        }

        /// <summary>
        /// ����ֵ
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private EntityConfig _result;

        /// <summary>
        /// ���ض�������
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public EntityConfig Result
        {
            get => ResultArg == null ? null : (_result ??= GlobalConfig.GetEntity(ResultArg));
            set => ResultArg = value?.Name;
        }


        /// <summary>
        /// �����������
        /// </summary>
        string ICommandItem.OrgArg => CallArg;

        string ICommandItem.CurArg => ResultArg;

        #endregion
    }
}