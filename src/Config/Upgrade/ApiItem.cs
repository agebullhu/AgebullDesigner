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
    /// 表示一个API节点
    /// </summary>
    [DataContract,JsonObject(MemberSerialization.OptIn)]
    public partial class ApiItem : FileConfigBase
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public ApiItem()
        {
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
        /// API原始命令请求参数名称
        /// </summary>
        [DataMember,JsonProperty("CallArg", NullValueHandling = NullValueHandling.Ignore)]
        internal string _callArg;

        /// <summary>
        /// API原始命令请求参数名称
        /// </summary>
        /// <remark>
        /// API原始命令请求参数名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"API原始命令请求参数名称"),Description("API原始命令请求参数名称")]
        public string CallArg
        {
            get
            {
                return _callArg;
            }
            set
            {
                if(_callArg == value)
                    return;
                this.BeforePropertyChanged(nameof(CallArg), _callArg,value);
                this._callArg = value;
                this.OnPropertyChanged(nameof(CallArg));
            }
        }

        /// <summary>
        /// 客户端命令请求参数名称
        /// </summary>
        [DataMember,JsonProperty("ClientArg", NullValueHandling = NullValueHandling.Ignore)]
        internal string _clientArg;

        /// <summary>
        /// 客户端命令请求参数名称
        /// </summary>
        /// <remark>
        /// 客户端命令请求参数名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"客户端命令请求参数名称"),Description("客户端命令请求参数名称")]
        public string ClientArg
        {
            get
            {
                return _clientArg;
            }
            set
            {
                if(_clientArg == value)
                    return;
                this.BeforePropertyChanged(nameof(ClientArg), _clientArg,value);
                this._clientArg = value;
                this.OnPropertyChanged(nameof(ClientArg));
            }
        }

        /// <summary>
        /// 是否用户命令
        /// </summary>
        [DataMember,JsonProperty("IsUserCommand", NullValueHandling = NullValueHandling.Ignore)]
        internal bool _isUserCommand;

        /// <summary>
        /// 是否用户命令
        /// </summary>
        /// <remark>
        /// 是否用户命令
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"是否用户命令"),Description("是否用户命令")]
        public bool IsUserCommand
        {
            get
            {
                return _isUserCommand;
            }
            set
            {
                if(_isUserCommand == value)
                    return;
                this.BeforePropertyChanged(nameof(IsUserCommand), _isUserCommand,value);
                this._isUserCommand = value;
                this.OnPropertyChanged(nameof(IsUserCommand));
            }
        }

        /// <summary>
        /// 命令返回参数名称
        /// </summary>
        [DataMember,JsonProperty("ResultArg", NullValueHandling = NullValueHandling.Ignore)]
        internal string _resultArg;

        /// <summary>
        /// 命令返回参数名称
        /// </summary>
        /// <remark>
        /// 命令返回参数名称
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"命令返回参数名称"),Description("命令返回参数名称")]
        public string ResultArg
        {
            get
            {
                return _resultArg;
            }
            set
            {
                if(_resultArg == value)
                    return;
                this.BeforePropertyChanged(nameof(ResultArg), _resultArg,value);
                this._resultArg = value;
                this.OnPropertyChanged(nameof(ResultArg));
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

        /// <summary>
        /// API对应的命令号
        /// </summary>
        [DataMember,JsonProperty("CommandId", NullValueHandling = NullValueHandling.Ignore)]
        internal string _commandId;

        /// <summary>
        /// API对应的命令号
        /// </summary>
        /// <remark>
        /// API对应的命令号
        /// </remark>
        [IgnoreDataMember,JsonIgnore]
        [Category(@""),DisplayName(@"API对应的命令号"),Description("API对应的命令号")]
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

    }
}