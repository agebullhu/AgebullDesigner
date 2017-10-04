/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2017/7/12 22:06:39*/
/*****************************************************
©2008-2017 Copy right by agebull.hu(胡天水)
作者:agebull.hu(胡天水)
工程:Agebull.Common.Config
建立:2014-12-03
修改:2017-07-12
*****************************************************/

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using Agebull.Common;
using Agebull.Common.DataModel;

using Newtonsoft.Json;

namespace Gboxt.Common.DataAccess.Schemas
{
    /// <summary>
    /// 表示一个第三方通知
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class NotifyItem : FileConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public NotifyItem()
        {
        }

        #endregion

 
        #region 

        /// <summary>
        /// 易盛对象名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal EntityConfig _esEntity;

        /// <summary>
        /// 易盛对象名称
        /// </summary>
        /// <remark>
        /// 易盛对象名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"易盛对象名称"),Description("易盛对象名称")]
        public EntityConfig EsEntity
        {
            get
            {
                return _esEntity;
            }
            set
            {
                if(_esEntity == value)
                    return;
                this.BeforePropertyChanged(nameof(EsEntity), _esEntity,value);
                this._esEntity = value;
                this.OnPropertyChanged(nameof(EsEntity));
            }
        }

        /// <summary>
        /// 本地对象名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal EntityConfig _localEntity;

        /// <summary>
        /// 本地对象名称
        /// </summary>
        /// <remark>
        /// 本地对象名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"本地对象名称"),Description("本地对象名称")]
        public EntityConfig LocalEntity
        {
            get
            {
                return _localEntity;
            }
            set
            {
                if(_localEntity == value)
                    return;
                this.BeforePropertyChanged(nameof(LocalEntity), _localEntity,value);
                this._localEntity = value;
                this.OnPropertyChanged(nameof(LocalEntity));
            }
        }

        /// <summary>
        /// Gboxt.Common.DataAccess.Schemas.ICommandItem.OrgArg的说明文字
        /// </summary>
        const string Gboxt.Common.DataAccess.Schemas.ICommandItem.OrgArg_Description = @"Gboxt.Common.DataAccess.Schemas.ICommandItem.OrgArg";

        /// <summary>
        /// Gboxt.Common.DataAccess.Schemas.ICommandItem.OrgArg
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal string _gboxtCommonDataAccessSchemasICommandItemOrgArg;

        /// <summary>
        /// Gboxt.Common.DataAccess.Schemas.ICommandItem.OrgArg
        /// </summary>
        /// <remark>
        /// Gboxt.Common.DataAccess.Schemas.ICommandItem.OrgArg
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"Gboxt.Common.DataAccess.Schemas.ICommandItem.OrgArg"),Description(Gboxt.Common.DataAccess.Schemas.ICommandItem.OrgArg_Description)]
        public string Gboxt.Common.DataAccess.Schemas.ICommandItem.OrgArg
        {
            get
            {
                return _gboxtCommonDataAccessSchemasICommandItemOrgArg;
            }
            set
            {
                if(_gboxtCommonDataAccessSchemasICommandItemOrgArg == value)
                    return;
                this.BeforePropertyChanged(nameof(Gboxt.Common.DataAccess.Schemas.ICommandItem.OrgArg), _gboxtCommonDataAccessSchemasICommandItemOrgArg,value);
                this._gboxtCommonDataAccessSchemasICommandItemOrgArg = value;
                this.OnPropertyChanged(nameof(Gboxt.Common.DataAccess.Schemas.ICommandItem.OrgArg));
            }
        }

        /// <summary>
        /// Gboxt.Common.DataAccess.Schemas.ICommandItem.CurArg的说明文字
        /// </summary>
        const string Gboxt.Common.DataAccess.Schemas.ICommandItem.CurArg_Description = @"Gboxt.Common.DataAccess.Schemas.ICommandItem.CurArg";

        /// <summary>
        /// Gboxt.Common.DataAccess.Schemas.ICommandItem.CurArg
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal string _gboxtCommonDataAccessSchemasICommandItemCurArg;

        /// <summary>
        /// Gboxt.Common.DataAccess.Schemas.ICommandItem.CurArg
        /// </summary>
        /// <remark>
        /// Gboxt.Common.DataAccess.Schemas.ICommandItem.CurArg
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"Gboxt.Common.DataAccess.Schemas.ICommandItem.CurArg"),Description(Gboxt.Common.DataAccess.Schemas.ICommandItem.CurArg_Description)]
        public string Gboxt.Common.DataAccess.Schemas.ICommandItem.CurArg
        {
            get
            {
                return _gboxtCommonDataAccessSchemasICommandItemCurArg;
            }
            set
            {
                if(_gboxtCommonDataAccessSchemasICommandItemCurArg == value)
                    return;
                this.BeforePropertyChanged(nameof(Gboxt.Common.DataAccess.Schemas.ICommandItem.CurArg), _gboxtCommonDataAccessSchemasICommandItemCurArg,value);
                this._gboxtCommonDataAccessSchemasICommandItemCurArg = value;
                this.OnPropertyChanged(nameof(Gboxt.Common.DataAccess.Schemas.ICommandItem.CurArg));
            }
        }

        /// <summary>
        /// Gboxt.Common.DataAccess.Schemas.ICommandItem.DefaultCode的说明文字
        /// </summary>
        const string Gboxt.Common.DataAccess.Schemas.ICommandItem.DefaultCode_Description = @"Gboxt.Common.DataAccess.Schemas.ICommandItem.DefaultCode";

        /// <summary>
        /// Gboxt.Common.DataAccess.Schemas.ICommandItem.DefaultCode
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        internal string _gboxtCommonDataAccessSchemasICommandItemDefaultCode;

        /// <summary>
        /// Gboxt.Common.DataAccess.Schemas.ICommandItem.DefaultCode
        /// </summary>
        /// <remark>
        /// Gboxt.Common.DataAccess.Schemas.ICommandItem.DefaultCode
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"Gboxt.Common.DataAccess.Schemas.ICommandItem.DefaultCode"),Description(Gboxt.Common.DataAccess.Schemas.ICommandItem.DefaultCode_Description)]
        public string Gboxt.Common.DataAccess.Schemas.ICommandItem.DefaultCode
        {
            get
            {
                return _gboxtCommonDataAccessSchemasICommandItemDefaultCode;
            }
            set
            {
                if(_gboxtCommonDataAccessSchemasICommandItemDefaultCode == value)
                    return;
                this.BeforePropertyChanged(nameof(Gboxt.Common.DataAccess.Schemas.ICommandItem.DefaultCode), _gboxtCommonDataAccessSchemasICommandItemDefaultCode,value);
                this._gboxtCommonDataAccessSchemasICommandItemDefaultCode = value;
                this.OnPropertyChanged(nameof(Gboxt.Common.DataAccess.Schemas.ICommandItem.DefaultCode));
            }
        } 
        #endregion 
        #region 

        /// <summary>
        /// 对应的另一半的说明文字
        /// </summary>
        const string Friend_Description = @"对应的另一半(API与Notify的关系)";

        /// <summary>
        /// 对应的另一半
        /// </summary>
        [DataMember,JsonProperty("Friend", NullValueHandling = NullValueHandling.Ignore)]
        internal string _friend;

        /// <summary>
        /// 对应的另一半
        /// </summary>
        /// <remark>
        /// 对应的另一半(API与Notify的关系)
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"对应的另一半"),Description(Friend_Description)]
        public string Friend
        {
            get
            {
                return _friend;
            }
            set
            {
                if(_friend == value)
                    return;
                this.BeforePropertyChanged(nameof(Friend), _friend,value);
                this._friend = value;
                this.OnPropertyChanged(nameof(Friend));
            }
        }

        /// <summary>
        /// 对应的另一半的说明文字
        /// </summary>
        const string FriendKey_Description = @"对应的另一半(API与Notify的关系)";

        /// <summary>
        /// 对应的另一半
        /// </summary>
        [DataMember,JsonProperty("FriendKey", NullValueHandling = NullValueHandling.Ignore)]
        internal Guid _friendKey;

        /// <summary>
        /// 对应的另一半
        /// </summary>
        /// <remark>
        /// 对应的另一半(API与Notify的关系)
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"对应的另一半"),Description(FriendKey_Description)]
        public Guid FriendKey
        {
            get
            {
                return _friendKey;
            }
            set
            {
                if(_friendKey == value)
                    return;
                this.BeforePropertyChanged(nameof(FriendKey), _friendKey,value);
                this._friendKey = value;
                this.OnPropertyChanged(nameof(FriendKey));
            }
        }

        /// <summary>
        /// 本地命令
        /// </summary>
        [DataMember,JsonProperty("LocalCommand", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _localCommand;

        /// <summary>
        /// 本地命令
        /// </summary>
        /// <remark>
        /// 本地命令(不转发)
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"本地命令"),Description("本地命令(不转发)")]
        public bool LocalCommand
        {
            get
            {
                return _localCommand;
            }
            set
            {
                if(_localCommand == value)
                    return;
                this.BeforePropertyChanged(nameof(LocalCommand), _localCommand,value);
                this._localCommand = value;
                this.OnPropertyChanged(nameof(LocalCommand));
            }
        }

        /// <summary>
        /// 通知的命令号
        /// </summary>
        [DataMember,JsonProperty("CommandId", NullValueHandling = NullValueHandling.Ignore)]
        internal string _commandId;

        /// <summary>
        /// 通知的命令号
        /// </summary>
        /// <remark>
        /// 通知的命令号
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"通知的命令号"),Description("通知的命令号")]
        public string CommandId
        {
            get
            {
                return _commandId;
            }
            set
            {
                if(_commandId == value)
                    return;
                this.BeforePropertyChanged(nameof(CommandId), _commandId,value);
                this._commandId = value;
                this.OnPropertyChanged(nameof(CommandId));
            }
        }

        /// <summary>
        /// 通知使用的数据结构
        /// </summary>
        [DataMember,JsonProperty("NotifyEntity", NullValueHandling = NullValueHandling.Ignore)]
        internal string _notifyEntity;

        /// <summary>
        /// 通知使用的数据结构
        /// </summary>
        /// <remark>
        /// 通知使用的数据结构
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"通知使用的数据结构"),Description("通知使用的数据结构")]
        public string NotifyEntity
        {
            get
            {
                return _notifyEntity;
            }
            set
            {
                if(_notifyEntity == value)
                    return;
                this.BeforePropertyChanged(nameof(NotifyEntity), _notifyEntity,value);
                this._notifyEntity = value;
                this.OnPropertyChanged(nameof(NotifyEntity));
            }
        }

        /// <summary>
        /// 客户使用的数据结构
        /// </summary>
        [DataMember,JsonProperty("ClientEntity", NullValueHandling = NullValueHandling.Ignore)]
        internal string _clientEntity;

        /// <summary>
        /// 客户使用的数据结构
        /// </summary>
        /// <remark>
        /// 客户使用的数据结构
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"客户使用的数据结构"),Description("客户使用的数据结构")]
        public string ClientEntity
        {
            get
            {
                return _clientEntity;
            }
            set
            {
                if(_clientEntity == value)
                    return;
                this.BeforePropertyChanged(nameof(ClientEntity), _clientEntity,value);
                this._clientEntity = value;
                this.OnPropertyChanged(nameof(ClientEntity));
            }
        }

        /// <summary>
        /// 是否命令返回
        /// </summary>
        [DataMember,JsonProperty("IsCommandResult", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isCommandResult;

        /// <summary>
        /// 是否命令返回
        /// </summary>
        /// <remark>
        /// 是否命令返回
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"是否命令返回"),Description("是否命令返回")]
        public bool IsCommandResult
        {
            get
            {
                return _isCommandResult;
            }
            set
            {
                if(_isCommandResult == value)
                    return;
                this.BeforePropertyChanged(nameof(IsCommandResult), _isCommandResult,value);
                this._isCommandResult = value;
                this.OnPropertyChanged(nameof(IsCommandResult));
            }
        }

        /// <summary>
        /// 是否多条通知
        /// </summary>
        [DataMember,JsonProperty("IsMulit", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isMulit;

        /// <summary>
        /// 是否多条通知
        /// </summary>
        /// <remark>
        /// 是否多条通知(通过结束标记结束)
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"是否多条通知"),Description("是否多条通知(通过结束标记结束)")]
        public bool IsMulit
        {
            get
            {
                return _isMulit;
            }
            set
            {
                if(_isMulit == value)
                    return;
                this.BeforePropertyChanged(nameof(IsMulit), _isMulit,value);
                this._isMulit = value;
                this.OnPropertyChanged(nameof(IsMulit));
            }
        }

        /// <summary>
        /// 原始内容
        /// </summary>
        [DataMember,JsonProperty("Org", NullValueHandling = NullValueHandling.Ignore)]
        internal string _org;

        /// <summary>
        /// 原始内容
        /// </summary>
        /// <remark>
        /// 原始内容
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"原始内容"),Description("原始内容")]
        public string Org
        {
            get
            {
                return _org;
            }
            set
            {
                if(_org == value)
                    return;
                this.BeforePropertyChanged(nameof(Org), _org,value);
                this._org = value;
                this.OnPropertyChanged(nameof(Org));
            }
        } 
        #endregion

    }
}