using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Gboxt.Common.DataAccess.Schemas
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
    public partial class ApiItem : FileConfigBase, ICommandItem
    {

        #region �ֶζ���

        private const string Method_Description = @"Api���÷�ʽ��GET��POST��";

        /// <summary>
        /// ��Ӧ����һ��
        /// </summary>
        [DataMember, JsonProperty("Method", NullValueHandling = NullValueHandling.Ignore)]
        internal HttpMethod _Method;

        /// <summary>
        /// ��Ӧ����һ��
        /// </summary>
        /// <remark>
        /// Api���÷�ʽ��GET��POST)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("Api���÷�ʽ��GET��POST)"), Description(Method_Description)]
        public HttpMethod Method
        {
            get
            {
                return _Method;
            }
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
        /// ���ڵ���Ŀ
        /// </summary>
        [DataMember, JsonProperty("Project", NullValueHandling = NullValueHandling.Ignore)]
        internal string _Project;

        /// <summary>
        /// ���ڵ���Ŀ
        /// </summary>
        /// <remark>
        /// ���ڵ���Ŀ
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("��Ӧ����һ��"), Description(Project_Description)]
        public string Project
        {
            get
            {
                return _Project;
            }
            set
            {
                if (_Project == value)
                    return;
                BeforePropertyChanged(nameof(Project), _Project, value);
                _Project = value;
                OnPropertyChanged(nameof(Project));
            }
        }

        private const string Friend_Description = @"��Ӧ����һ��(API��Notify�Ĺ�ϵ)";

        /// <summary>
        /// ��Ӧ����һ��
        /// </summary>
        [DataMember, JsonProperty("Friend", NullValueHandling = NullValueHandling.Ignore)]
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
        [DataMember, JsonProperty("FriendKey", NullValueHandling = NullValueHandling.Ignore)]
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
        [DataMember, JsonProperty("LocalCommand", NullValueHandling = NullValueHandling.Ignore)]
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
        }

        /// <summary>
        /// APIԭʼ���������������
        /// </summary>
        [DataMember, JsonProperty("CallArg", NullValueHandling = NullValueHandling.Ignore)]
        internal string _callArg;

        /// <summary>
        /// APIԭʼ���������������
        /// </summary>
        /// <remark>
        /// APIԭʼ���������������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("APIԭʼ���������������"), Description("APIԭʼ���������������")]
        public string CallArg
        {
            get
            {
                return _callArg;
            }
            set
            {
                if (_callArg == value)
                    return;
                BeforePropertyChanged(nameof(CallArg), _callArg, value);
                _callArg = value;
                OnPropertyChanged(nameof(CallArg));
            }
        }
        
        /// <summary>
        /// �Ƿ��û�����
        /// </summary>
        [DataMember, JsonProperty("IsUserCommand", NullValueHandling = NullValueHandling.Ignore)]
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

        /// <summary>
        /// ����ز�������
        /// </summary>
        [DataMember, JsonProperty("ResultArg", NullValueHandling = NullValueHandling.Ignore)]
        internal string _resultArg;

        /// <summary>
        /// ����ز�������
        /// </summary>
        /// <remark>
        /// ����ز�������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("����ز�������"), Description("����ز�������")]
        public string ResultArg
        {
            get
            {
                return _resultArg;
            }
            set
            {
                if (_resultArg == value)
                    return;
                BeforePropertyChanged(nameof(ResultArg), _resultArg, value);
                _resultArg = value;
                OnPropertyChanged(nameof(ResultArg));
            }
        }

        /// <summary>
        /// ԭʼ����
        /// </summary>
        [DataMember, JsonProperty("Org", NullValueHandling = NullValueHandling.Ignore)]
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
        /// API��Ӧ�������
        /// </summary>
        [DataMember, JsonProperty("CommandId", NullValueHandling = NullValueHandling.Ignore)]
        internal string _commandId;

        /// <summary>
        /// API��Ӧ�������
        /// </summary>
        /// <remark>
        /// API��Ӧ�������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("API��Ӧ�������"), Description("API��Ӧ�������")]
        public string CommandId
        {
            get
            {
                return _commandId;
            }
            set
            {
                if (_commandId == value)
                    return;
                BeforePropertyChanged(nameof(CommandId), _commandId, value);
                _commandId = value;
                OnPropertyChanged(nameof(CommandId));
            }
        }
        #endregion
        #region �ӿ�

        /// <summary>
        /// ��ʢ��������
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private EntityConfig _argument;

        /// <summary>
        /// ��ʢ��������
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public EntityConfig Argument => _argument ?? (_argument = GlobalConfig.GetEntity(CallArg));

        /// <summary>
        /// �ͻ��˵��ö�������
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private EntityConfig _result;

        /// <summary>
        /// ���ض�������
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public EntityConfig Result => _result ?? (_result = GlobalConfig.GetEntity(ResultArg));


        /// <summary>
        /// APIԭʼ���������������
        /// </summary>
        string ICommandItem.OrgArg => CallArg;

        string ICommandItem.CurArg => ResultArg;

        /// <summary>
        /// ԭʼ��������
        /// </summary>
        string ICommandItem.DefaultCode => Org;

        #endregion
    }
}