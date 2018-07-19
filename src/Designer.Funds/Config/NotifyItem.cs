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
    /// 表示一个第三方通知
    /// </summary>
    [DataContract, JsonObject(MemberSerialization.OptIn)]
    public partial class NotifyItem : ProjectChildConfigBase, ICommandItem
    {
        #region 接口

        /// <summary>
        /// 易盛对象名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private EntityConfig _esEntity;

        /// <summary>
        /// 易盛对象名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public EntityConfig EsEntity => _esEntity ?? (_esEntity = GlobalConfig.GetEntity(NotifyEntity));

        /// <summary>
        /// 客户端调用对象名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        private EntityConfig _localClientEntity;

        /// <summary>
        /// 本地对象名称
        /// </summary>
        [IgnoreDataMember, JsonIgnore]
        public EntityConfig LocalEntity => _localClientEntity ?? (_localClientEntity = GlobalConfig.GetEntity(ClientEntity));


        /// <summary>
        /// API原始命令请求参数名称
        /// </summary>
        string ICommandItem.OrgArg => NotifyEntity;

        string ICommandItem.CurArg => ClientEntity;
        
        #endregion

        #region

        private const string Friend_Description = @"对应的另一半(API与Notify的关系)";

        /// <summary>
        /// 对应的另一半
        /// </summary>
        [DataMember, JsonProperty("Friend", NullValueHandling = NullValueHandling.Ignore)]
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

        private const string FriendKey_Description = @"对应的另一半(API与Notify的关系)";

        /// <summary>
        /// 对应的另一半
        /// </summary>
        [DataMember, JsonProperty("FriendKey", NullValueHandling = NullValueHandling.Ignore)]
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
        /// 本地命令
        /// </summary>
        [DataMember, JsonProperty("LocalCommand", NullValueHandling = NullValueHandling.Ignore)]
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
        /// 通知的命令号
        /// </summary>
        [DataMember, JsonProperty("CommandId", NullValueHandling = NullValueHandling.Ignore)]
        internal string _commandId;

        /// <summary>
        /// 通知的命令号
        /// </summary>
        /// <remark>
        /// 通知的命令号
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("通知的命令号"), Description("通知的命令号")]
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
        /// 通知使用的数据结构
        /// </summary>
        [DataMember, JsonProperty("NotifyEntity", NullValueHandling = NullValueHandling.Ignore)]
        internal string _notifyEntity;

        /// <summary>
        /// 通知使用的数据结构
        /// </summary>
        /// <remark>
        /// 通知使用的数据结构
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("通知使用的数据结构"), Description("通知使用的数据结构")]
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
        /// 客户使用的数据结构
        /// </summary>
        [DataMember, JsonProperty("ClientEntity", NullValueHandling = NullValueHandling.Ignore)]
        internal string _clientEntity;

        /// <summary>
        /// 客户使用的数据结构
        /// </summary>
        /// <remark>
        /// 客户使用的数据结构
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("客户使用的数据结构"), Description("客户使用的数据结构")]
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
        /// 是否命令返回
        /// </summary>
        [DataMember, JsonProperty("IsCommandResult", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isCommandResult;

        /// <summary>
        /// 是否命令返回
        /// </summary>
        /// <remark>
        /// 是否命令返回
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("是否命令返回"), Description("是否命令返回")]
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
        /// 是否多条通知
        /// </summary>
        [DataMember, JsonProperty("IsMulit", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isMulit;

        /// <summary>
        /// 是否多条通知
        /// </summary>
        /// <remark>
        /// 是否多条通知(通过结束标记结束)
        /// </remark>
        [IgnoreDataMember, JsonIgnore]
        [Category(""), DisplayName("是否多条通知"), Description("是否多条通知(通过结束标记结束)")]
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
        /// 原始内容
        /// </summary>
        [DataMember, JsonProperty("Org", NullValueHandling = NullValueHandling.Ignore)]
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
        /// LUA结构支持
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