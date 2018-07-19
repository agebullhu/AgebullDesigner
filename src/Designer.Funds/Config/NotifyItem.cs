using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Agebull.Common.LUA;
using Newtonsoft.Json;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    /// ��ʾһ��������֪ͨ
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class NotifyItem : ProjectChildConfigBase, ICommandItem
    {
        #region �ӿ�

        /// <summary>
        /// ��ʢ��������
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private EntityConfig _esEntity;

        /// <summary>
        /// ��ʢ��������
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public EntityConfig EsEntity => _esEntity ?? (_esEntity = GlobalConfig.GetEntity(NotifyEntity));

        /// <summary>
        /// �ͻ��˵��ö�������
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private EntityConfig _localClientEntity;

        /// <summary>
        /// ���ض�������
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public EntityConfig LocalEntity => _localClientEntity ?? (_localClientEntity = GlobalConfig.GetEntity(ClientEntity));


        /// <summary>
        /// APIԭʼ���������������
        /// </summary>
        string ICommandItem.OrgArg => NotifyEntity;

        string ICommandItem.CurArg => ClientEntity;
        
        #endregion

        #region

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
            get => _friend;
            set
            {
                if (_friend == value)
                    return;
                BeforePropertyChanged(nameof(Friend), _friend, value);
                _friend = String.IsNullOrWhiteSpace(value) ? null : value.Trim();
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
            get => _friendKey;
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
            get => _localCommand;
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
        /// ֪ͨ�������
        /// </summary>
        [DataMember, JsonProperty("CommandId", NullValueHandling = NullValueHandling.Ignore)]
        internal string _commandId;

        /// <summary>
        /// ֪ͨ�������
        /// </summary>
        /// <remark>
        /// ֪ͨ�������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("֪ͨ�������"), Description("֪ͨ�������")]
        public string CommandId
        {
            get => _commandId;
            set
            {
                if (_commandId == value)
                    return;
                BeforePropertyChanged(nameof(CommandId), _commandId, value);
                _commandId = String.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(CommandId));
            }
        }

        /// <summary>
        /// ֪ͨʹ�õ����ݽṹ
        /// </summary>
        [DataMember, JsonProperty("NotifyEntity", NullValueHandling = NullValueHandling.Ignore)]
        internal string _notifyEntity;

        /// <summary>
        /// ֪ͨʹ�õ����ݽṹ
        /// </summary>
        /// <remark>
        /// ֪ͨʹ�õ����ݽṹ
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("֪ͨʹ�õ����ݽṹ"), Description("֪ͨʹ�õ����ݽṹ")]
        public string NotifyEntity
        {
            get => _notifyEntity;
            set
            {
                if (_notifyEntity == value)
                    return;
                BeforePropertyChanged(nameof(NotifyEntity), _notifyEntity, value);
                _notifyEntity = String.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(NotifyEntity));
            }
        }

        /// <summary>
        /// �ͻ�ʹ�õ����ݽṹ
        /// </summary>
        [DataMember, JsonProperty("ClientEntity", NullValueHandling = NullValueHandling.Ignore)]
        internal string _clientEntity;

        /// <summary>
        /// �ͻ�ʹ�õ����ݽṹ
        /// </summary>
        /// <remark>
        /// �ͻ�ʹ�õ����ݽṹ
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("�ͻ�ʹ�õ����ݽṹ"), Description("�ͻ�ʹ�õ����ݽṹ")]
        public string ClientEntity
        {
            get => _clientEntity;
            set
            {
                if (_clientEntity == value)
                    return;
                BeforePropertyChanged(nameof(ClientEntity), _clientEntity, value);
                _clientEntity = String.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(ClientEntity));
            }
        }

        /// <summary>
        /// �Ƿ������
        /// </summary>
        [DataMember, JsonProperty("IsCommandResult", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isCommandResult;

        /// <summary>
        /// �Ƿ������
        /// </summary>
        /// <remark>
        /// �Ƿ������
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("�Ƿ������"), Description("�Ƿ������")]
        public bool IsCommandResult
        {
            get => _isCommandResult;
            set
            {
                if (_isCommandResult == value)
                    return;
                BeforePropertyChanged(nameof(IsCommandResult), _isCommandResult, value);
                _isCommandResult = value;
                OnPropertyChanged(nameof(IsCommandResult));
            }
        }

        /// <summary>
        /// �Ƿ����֪ͨ
        /// </summary>
        [DataMember, JsonProperty("IsMulit", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isMulit;

        /// <summary>
        /// �Ƿ����֪ͨ
        /// </summary>
        /// <remark>
        /// �Ƿ����֪ͨ(ͨ��������ǽ���)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("�Ƿ����֪ͨ"), Description("�Ƿ����֪ͨ(ͨ��������ǽ���)")]
        public bool IsMulit
        {
            get => _isMulit;
            set
            {
                if (_isMulit == value)
                    return;
                BeforePropertyChanged(nameof(IsMulit), _isMulit, value);
                _isMulit = value;
                OnPropertyChanged(nameof(IsMulit));
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
            get => _org;
            set
            {
                if (_org == value)
                    return;
                BeforePropertyChanged(nameof(Org), _org, value);
                _org = String.IsNullOrWhiteSpace(value) ? null : value.Trim();
                OnPropertyChanged(nameof(Org));
            }
        }
        public override void ForeachChild(Action<ConfigBase> action)
        {
        }
        #endregion
  

        /// <summary>
        /// LUA�ṹ֧��
        /// </summary>
        /// <returns></returns>
        public override void GetLuaStruct(StringBuilder code)
        {
            base.GetLuaStruct(code);
            if (!String.IsNullOrWhiteSpace(Friend))
                code.AppendLine($@"['Friend'] = '{Friend.ToLuaString()}',");
            else
                code.AppendLine($@"['Friend'] = nil,");

            code.AppendLine($@"['FriendKey'] ='{FriendKey}',");

            code.AppendLine($@"['LocalCommand'] ={(LocalCommand.ToString().ToLower())},");

            if (!String.IsNullOrWhiteSpace(CommandId))
                code.AppendLine($@"['CommandId'] = '{CommandId.ToLuaString()}',");
            else
                code.AppendLine($@"['CommandId'] = nil,");

            if (!String.IsNullOrWhiteSpace(NotifyEntity))
                code.AppendLine($@"['NotifyEntity'] = '{NotifyEntity.ToLuaString()}',");
            else
                code.AppendLine($@"['NotifyEntity'] = nil,");

            if (!String.IsNullOrWhiteSpace(ClientEntity))
                code.AppendLine($@"['ClientEntity'] = '{ClientEntity.ToLuaString()}',");
            else
                code.AppendLine($@"['ClientEntity'] = nil,");

            code.AppendLine($@"['IsCommandResult'] ={(IsCommandResult.ToString().ToLower())},");

            code.AppendLine($@"['IsMulit'] ={(IsMulit.ToString().ToLower())},");

            if (!String.IsNullOrWhiteSpace(Org))
                code.AppendLine($@"['Org'] = '{Org.ToLuaString()}',");
            else
                code.AppendLine($@"['Org'] = nil,");

            if (EsEntity != null)
                code.AppendLine($@"['EsEntity'] = {EsEntity.GetLuaStruct()},");

            if (LocalEntity != null)
                code.AppendLine($@"['LocalEntity'] = {LocalEntity.GetLuaStruct()},");

        }
    }
}