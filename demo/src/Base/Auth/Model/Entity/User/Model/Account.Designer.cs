/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/1/2 21:22:57*/
#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.DataModel;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.Extends;
using Agebull.Common.WebApi;


#endregion

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 用于支持用户的账户名密码登录
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class AccountData : IStateData , IHistoryData , IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public AccountData()
        {
            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        partial void Initialize();
        #endregion

        #region 基本属性


        /// <summary>
        /// 修改主键
        /// </summary>
        public void ChangePrimaryKey(long id)
        {
            _id = id;
        }
        /// <summary>
        /// 标识主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _id;

        partial void OnIdGet();

        partial void OnIdSet(ref long value);

        partial void OnIdLoad(ref long value);

        partial void OnIdSeted();

        
        /// <summary>
        /// 标识主键
        /// </summary>
        [DataMember , JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"标识主键")]
        public long Id
        {
            get
            {
                OnIdGet();
                return this._id;
            }
            set
            {
                if(this._id == value)
                    return;
                //if(this._id > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnIdSet(ref value);
                this._id = value;
                this.OnPropertyChanged(_DataStruct_.Real_Id);
                OnIdSeted();
            }
        }
        /// <summary>
        /// 角色标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _roleId;

        partial void OnRoleIdGet();

        partial void OnRoleIdSet(ref long value);

        partial void OnRoleIdSeted();

        
        /// <summary>
        /// 角色标识
        /// </summary>
        [DataMember , JsonProperty("RoleId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"角色标识")]
        public  long RoleId
        {
            get
            {
                OnRoleIdGet();
                return this._roleId;
            }
            set
            {
                if(this._roleId == value)
                    return;
                OnRoleIdSet(ref value);
                this._roleId = value;
                OnRoleIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RoleId);
            }
        }
        /// <summary>
        /// 角色
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _role;

        
        /// <summary>
        /// 角色
        /// </summary>
        /// <remarks>
        /// 名称
        /// </remarks>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("Role", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"角色")]
        public  string Role
        {
            get
            {
                return this._role;
            }
            set
            {
                this._role = value;
            }
        }
        /// <summary>
        /// 用户Id
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _userId;

        partial void OnUserIdGet();

        partial void OnUserIdSet(ref long value);

        partial void OnUserIdSeted();

        
        /// <summary>
        /// 用户Id
        /// </summary>
        [DataMember , JsonProperty("UserId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"用户Id")]
        public  long UserId
        {
            get
            {
                OnUserIdGet();
                return this._userId;
            }
            set
            {
                if(this._userId == value)
                    return;
                OnUserIdSet(ref value);
                this._userId = value;
                OnUserIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UserId);
            }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _nickName;

        
        /// <summary>
        /// 昵称
        /// </summary>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataMember , JsonProperty("NickName", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"昵称")]
        public  string NickName
        {
            get
            {
                return this._nickName;
            }
            set
            {
                this._nickName = value;
            }
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _idCard;

        
        /// <summary>
        /// 身份证号
        /// </summary>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataMember , JsonProperty("IdCard", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"身份证号")]
        public  string IdCard
        {
            get
            {
                return this._idCard;
            }
            set
            {
                this._idCard = value;
            }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _phoneNumber;

        
        /// <summary>
        /// 手机号
        /// </summary>
        /// <remarks>
        /// 用户手机号
        /// </remarks>
        /// <value>
        /// 不能为空.可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataRule(Max = 20)]
        [DataMember , JsonProperty("phoneNumber", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"手机号")]
        public  string PhoneNumber
        {
            get
            {
                return this._phoneNumber;
            }
            set
            {
                this._phoneNumber = value;
            }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _realName;

        
        /// <summary>
        /// 真实姓名
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("RealName", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"真实姓名")]
        public  string RealName
        {
            get
            {
                return this._realName;
            }
            set
            {
                this._realName = value;
            }
        }
        /// <summary>
        /// 账户名
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _accountName;

        partial void OnAccountNameGet();

        partial void OnAccountNameSet(ref string value);

        partial void OnAccountNameSeted();

        
        /// <summary>
        /// 账户名
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("AccountName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"账户名")]
        public  string AccountName
        {
            get
            {
                OnAccountNameGet();
                return this._accountName;
            }
            set
            {
                if(this._accountName == value)
                    return;
                OnAccountNameSet(ref value);
                this._accountName = value;
                OnAccountNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AccountName);
            }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _password;

        partial void OnPasswordGet();

        partial void OnPasswordSet(ref string value);

        partial void OnPasswordSeted();

        
        /// <summary>
        /// 用户密码
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Password", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"用户密码")]
        public  string Password
        {
            get
            {
                OnPasswordGet();
                return this._password;
            }
            set
            {
                if(this._password == value)
                    return;
                OnPasswordSet(ref value);
                this._password = value;
                OnPasswordSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Password);
            }
        }
        /// <summary>
        /// 冻结更新
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public bool _isFreeze;

        partial void OnIsFreezeGet();

        partial void OnIsFreezeSet(ref bool value);

        partial void OnIsFreezeSeted();

        
        /// <summary>
        /// 冻结更新
        /// </summary>
        /// <remarks>
        /// 无论在什么数据状态,一旦设置且保存后,数据将不再允许执行Update的操作,作为Update的统一开关.取消的方法是单独设置这个字段的值
        /// </remarks>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("isFreeze", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"冻结更新")]
        public  bool IsFreeze
        {
            get
            {
                OnIsFreezeGet();
                return this._isFreeze;
            }
            set
            {
                if(this._isFreeze == value)
                    return;
                OnIsFreezeSet(ref value);
                this._isFreeze = value;
                OnIsFreezeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_IsFreeze);
            }
        }
        /// <summary>
        /// 数据状态
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DataStateType _dataState;

        partial void OnDataStateGet();

        partial void OnDataStateSet(ref DataStateType value);

        partial void OnDataStateSeted();

        
        /// <summary>
        /// 数据状态
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("dataState", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"数据状态")]
        public  DataStateType DataState
        {
            get
            {
                OnDataStateGet();
                return this._dataState;
            }
            set
            {
                if(this._dataState == value)
                    return;
                OnDataStateSet(ref value);
                this._dataState = value;
                OnDataStateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_DataState);
            }
        }
        /// <summary>
        /// 数据状态的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("数据状态")]
        public string DataState_Content => DataState.ToCaption();

        /// <summary>
        /// 数据状态的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int DataState_Number
        {
            get => (int)this.DataState;
            set => this.DataState = (DataStateType)value;
        }
        /// <summary>
        /// 制作时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _addDate;

        partial void OnAddDateGet();

        partial void OnAddDateSet(ref DateTime value);

        partial void OnAddDateSeted();

        
        /// <summary>
        /// 制作时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("addDate", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"制作时间")]
        public  DateTime AddDate
        {
            get
            {
                OnAddDateGet();
                return this._addDate;
            }
            set
            {
                if(this._addDate == value)
                    return;
                OnAddDateSet(ref value);
                this._addDate = value;
                OnAddDateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AddDate);
            }
        }
        /// <summary>
        /// 最后修改者
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _lastReviserId;

        partial void OnLastReviserIdGet();

        partial void OnLastReviserIdSet(ref long value);

        partial void OnLastReviserIdSeted();

        
        /// <summary>
        /// 最后修改者
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("lastReviserId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"最后修改者")]
        public  long LastReviserId
        {
            get
            {
                OnLastReviserIdGet();
                return this._lastReviserId;
            }
            set
            {
                if(this._lastReviserId == value)
                    return;
                OnLastReviserIdSet(ref value);
                this._lastReviserId = value;
                OnLastReviserIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LastReviserId);
            }
        }
        /// <summary>
        /// 最后修改日期
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _lastModifyDate;

        partial void OnLastModifyDateGet();

        partial void OnLastModifyDateSet(ref DateTime value);

        partial void OnLastModifyDateSeted();

        
        /// <summary>
        /// 最后修改日期
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("lastModifyDate", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"最后修改日期")]
        public  DateTime LastModifyDate
        {
            get
            {
                OnLastModifyDateGet();
                return this._lastModifyDate;
            }
            set
            {
                if(this._lastModifyDate == value)
                    return;
                OnLastModifyDateSet(ref value);
                this._lastModifyDate = value;
                OnLastModifyDateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LastModifyDate);
            }
        }
        /// <summary>
        /// 制作人
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _authorId;

        partial void OnAuthorIdGet();

        partial void OnAuthorIdSet(ref long value);

        partial void OnAuthorIdSeted();

        
        /// <summary>
        /// 制作人
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("authorId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"制作人")]
        public  long AuthorId
        {
            get
            {
                OnAuthorIdGet();
                return this._authorId;
            }
            set
            {
                if(this._authorId == value)
                    return;
                OnAuthorIdSet(ref value);
                this._authorId = value;
                OnAuthorIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AuthorId);
            }
        }

        #region 接口属性

        #endregion
        #region 扩展属性

        #endregion
        #endregion


        #region 名称的属性操作

    

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        protected override void SetValueInner(string property, object value)
        {
            if(property == null) return;
            switch(property.Trim().ToLower())
            {
            case "id":
                this.Id = (long)Convert.ToDecimal(value);
                return;
            case "roleid":
                this.RoleId = (long)Convert.ToDecimal(value);
                return;
            case "role":
                this.Role = value == null ? null : value.ToString();
                return;
            case "userid":
                this.UserId = (long)Convert.ToDecimal(value);
                return;
            case "nickname":
                this.NickName = value == null ? null : value.ToString();
                return;
            case "idcard":
                this.IdCard = value == null ? null : value.ToString();
                return;
            case "phonenumber":
                this.PhoneNumber = value == null ? null : value.ToString();
                return;
            case "realname":
                this.RealName = value == null ? null : value.ToString();
                return;
            case "accountname":
                this.AccountName = value == null ? null : value.ToString();
                return;
            case "password":
                this.Password = value == null ? null : value.ToString();
                return;
            case "isfreeze":
                if (value != null)
                {
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {
                        this.IsFreeze = vl != 0;
                    }
                    else
                    {
                        this.IsFreeze = Convert.ToBoolean(value);
                    }
                }
                return;
            case "datastate":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.DataState = (DataStateType)(int)value;
                    }
                    else if(value is DataStateType)
                    {
                        this.DataState = (DataStateType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        DataStateType val;
                        if (DataStateType.TryParse(str, out val))
                        {
                            this.DataState = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.DataState = (DataStateType)vl;
                            }
                        }
                    }
                }
                return;
            case "adddate":
                this.AddDate = Convert.ToDateTime(value);
                return;
            case "lastreviserid":
                this.LastReviserId = (long)Convert.ToDecimal(value);
                return;
            case "lastmodifydate":
                this.LastModifyDate = Convert.ToDateTime(value);
                return;
            case "authorid":
                this.AuthorId = (long)Convert.ToDecimal(value);
                return;
            }

            //System.Diagnostics.Trace.WriteLine(property + @"=>" + value);

        }

    

        /// <summary>
        ///     设置属性值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        protected override void SetValueInner(int index, object value)
        {
            switch(index)
            {
            case _DataStruct_.Id:
                this.Id = Convert.ToInt64(value);
                return;
            case _DataStruct_.RoleId:
                this.RoleId = Convert.ToInt64(value);
                return;
            case _DataStruct_.Role:
                this.Role = value == null ? null : value.ToString();
                return;
            case _DataStruct_.UserId:
                this.UserId = Convert.ToInt64(value);
                return;
            case _DataStruct_.NickName:
                this.NickName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.IdCard:
                this.IdCard = value == null ? null : value.ToString();
                return;
            case _DataStruct_.PhoneNumber:
                this.PhoneNumber = value == null ? null : value.ToString();
                return;
            case _DataStruct_.RealName:
                this.RealName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AccountName:
                this.AccountName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Password:
                this.Password = value == null ? null : value.ToString();
                return;
            case _DataStruct_.IsFreeze:
                this.IsFreeze = Convert.ToBoolean(value);
                return;
            case _DataStruct_.DataState:
                this.DataState = (DataStateType)value;
                return;
            case _DataStruct_.AddDate:
                this.AddDate = Convert.ToDateTime(value);
                return;
            case _DataStruct_.LastReviserId:
                this.LastReviserId = Convert.ToInt64(value);
                return;
            case _DataStruct_.LastModifyDate:
                this.LastModifyDate = Convert.ToDateTime(value);
                return;
            case _DataStruct_.AuthorId:
                this.AuthorId = Convert.ToInt64(value);
                return;
            }
        }


        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name="property"></param>
        protected override object GetValueInner(string property)
        {
            switch(property)
            {
            case "id":
                return this.Id;
            case "roleid":
                return this.RoleId;
            case "role":
                return this.Role;
            case "userid":
                return this.UserId;
            case "nickname":
                return this.NickName;
            case "idcard":
                return this.IdCard;
            case "phonenumber":
                return this.PhoneNumber;
            case "realname":
                return this.RealName;
            case "accountname":
                return this.AccountName;
            case "password":
                return this.Password;
            case "isfreeze":
                return this.IsFreeze;
            case "datastate":
                return this.DataState.ToCaption();
            case "adddate":
                return this.AddDate;
            case "lastreviserid":
                return this.LastReviserId;
            case "lastmodifydate":
                return this.LastModifyDate;
            case "authorid":
                return this.AuthorId;
            }

            return null;
        }


        /// <summary>
        ///     读取属性值
        /// </summary>
        /// <param name="index"></param>
        protected override object GetValueInner(int index)
        {
            switch(index)
            {
                case _DataStruct_.Id:
                    return this.Id;
                case _DataStruct_.RoleId:
                    return this.RoleId;
                case _DataStruct_.Role:
                    return this.Role;
                case _DataStruct_.UserId:
                    return this.UserId;
                case _DataStruct_.NickName:
                    return this.NickName;
                case _DataStruct_.IdCard:
                    return this.IdCard;
                case _DataStruct_.PhoneNumber:
                    return this.PhoneNumber;
                case _DataStruct_.RealName:
                    return this.RealName;
                case _DataStruct_.AccountName:
                    return this.AccountName;
                case _DataStruct_.Password:
                    return this.Password;
                case _DataStruct_.IsFreeze:
                    return this.IsFreeze;
                case _DataStruct_.DataState:
                    return this.DataState;
                case _DataStruct_.AddDate:
                    return this.AddDate;
                case _DataStruct_.LastReviserId:
                    return this.LastReviserId;
                case _DataStruct_.LastModifyDate:
                    return this.LastModifyDate;
                case _DataStruct_.AuthorId:
                    return this.AuthorId;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(AccountData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as AccountData;
            if(sourceEntity == null)
                return;
            this._id = sourceEntity._id;
            this._roleId = sourceEntity._roleId;
            this._role = sourceEntity._role;
            this._userId = sourceEntity._userId;
            this._nickName = sourceEntity._nickName;
            this._idCard = sourceEntity._idCard;
            this._phoneNumber = sourceEntity._phoneNumber;
            this._realName = sourceEntity._realName;
            this._accountName = sourceEntity._accountName;
            this._password = sourceEntity._password;
            this._isFreeze = sourceEntity._isFreeze;
            this._dataState = sourceEntity._dataState;
            this._addDate = sourceEntity._addDate;
            this._lastReviserId = sourceEntity._lastReviserId;
            this._lastModifyDate = sourceEntity._lastModifyDate;
            this._authorId = sourceEntity._authorId;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(AccountData source)
        {
                this.Id = source.Id;
                this.RoleId = source.RoleId;
                this.Role = source.Role;
                this.UserId = source.UserId;
                this.NickName = source.NickName;
                this.IdCard = source.IdCard;
                this.PhoneNumber = source.PhoneNumber;
                this.RealName = source.RealName;
                this.AccountName = source.AccountName;
                this.Password = source.Password;
                this.IsFreeze = source.IsFreeze;
                this.DataState = source.DataState;
                this.AddDate = source.AddDate;
                this.LastReviserId = source.LastReviserId;
                this.LastModifyDate = source.LastModifyDate;
                this.AuthorId = source.AuthorId;
        }
        #endregion

        #region 后期处理
        

        /// <summary>
        /// 单个属性修改的后期处理(保存后)
        /// </summary>
        /// <param name="subsist">当前实体生存状态</param>
        /// <param name="modifieds">修改列表</param>
        /// <remarks>
        /// 对当前对象的属性的更改,请自行保存,否则将丢失
        /// </remarks>
        protected override void OnLaterPeriodBySignleModified(EntitySubsist subsist,byte[] modifieds)
        {
            if (subsist == EntitySubsist.Deleting)
            {
                OnIdModified(subsist,false);
                OnRoleIdModified(subsist,false);
                OnRoleModified(subsist,false);
                OnUserIdModified(subsist,false);
                OnNickNameModified(subsist,false);
                OnIdCardModified(subsist,false);
                OnPhoneNumberModified(subsist,false);
                OnRealNameModified(subsist,false);
                OnAccountNameModified(subsist,false);
                OnPasswordModified(subsist,false);
                OnIsFreezeModified(subsist,false);
                OnDataStateModified(subsist,false);
                OnAddDateModified(subsist,false);
                OnLastReviserIdModified(subsist,false);
                OnLastModifyDateModified(subsist,false);
                OnAuthorIdModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIdModified(subsist,true);
                OnRoleIdModified(subsist,true);
                OnRoleModified(subsist,true);
                OnUserIdModified(subsist,true);
                OnNickNameModified(subsist,true);
                OnIdCardModified(subsist,true);
                OnPhoneNumberModified(subsist,true);
                OnRealNameModified(subsist,true);
                OnAccountNameModified(subsist,true);
                OnPasswordModified(subsist,true);
                OnIsFreezeModified(subsist,true);
                OnDataStateModified(subsist,true);
                OnAddDateModified(subsist,true);
                OnLastReviserIdModified(subsist,true);
                OnLastModifyDateModified(subsist,true);
                OnAuthorIdModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[16] > 0)
            {
                OnIdModified(subsist,modifieds[_DataStruct_.Real_Id] == 1);
                OnRoleIdModified(subsist,modifieds[_DataStruct_.Real_RoleId] == 1);
                OnRoleModified(subsist,modifieds[_DataStruct_.Real_Role] == 1);
                OnUserIdModified(subsist,modifieds[_DataStruct_.Real_UserId] == 1);
                OnNickNameModified(subsist,modifieds[_DataStruct_.Real_NickName] == 1);
                OnIdCardModified(subsist,modifieds[_DataStruct_.Real_IdCard] == 1);
                OnPhoneNumberModified(subsist,modifieds[_DataStruct_.Real_PhoneNumber] == 1);
                OnRealNameModified(subsist,modifieds[_DataStruct_.Real_RealName] == 1);
                OnAccountNameModified(subsist,modifieds[_DataStruct_.Real_AccountName] == 1);
                OnPasswordModified(subsist,modifieds[_DataStruct_.Real_Password] == 1);
                OnIsFreezeModified(subsist,modifieds[_DataStruct_.Real_IsFreeze] == 1);
                OnDataStateModified(subsist,modifieds[_DataStruct_.Real_DataState] == 1);
                OnAddDateModified(subsist,modifieds[_DataStruct_.Real_AddDate] == 1);
                OnLastReviserIdModified(subsist,modifieds[_DataStruct_.Real_LastReviserId] == 1);
                OnLastModifyDateModified(subsist,modifieds[_DataStruct_.Real_LastModifyDate] == 1);
                OnAuthorIdModified(subsist,modifieds[_DataStruct_.Real_AuthorId] == 1);
            }
        }

        /// <summary>
        /// 标识主键修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 角色标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRoleIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 角色修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRoleModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 用户Id修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 昵称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnNickNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 身份证号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIdCardModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 手机号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPhoneNumberModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 真实姓名修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRealNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 账户名修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAccountNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 用户密码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPasswordModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 冻结更新修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIsFreezeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 数据状态修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDataStateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 制作时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAddDateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 最后修改者修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLastReviserIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 最后修改日期修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLastModifyDateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 制作人修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAuthorIdModified(EntitySubsist subsist,bool isModified);
        #endregion

        #region 数据结构

        /// <summary>
        /// 实体结构
        /// </summary>
        [IgnoreDataMember,Browsable (false)]
        public override EntitySturct __Struct
        {
            get
            {
                return _DataStruct_.Struct;
            }
        }
        /// <summary>
        /// 实体结构
        /// </summary>
        public class _DataStruct_
        {
            /// <summary>
            /// 实体名称
            /// </summary>
            public const string EntityName = @"Account";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"登录账户";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用于支持用户的账户名密码登录";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0xD0001;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "Id";
            
            
            /// <summary>
            /// 标识主键的数字标识
            /// </summary>
            public const byte Id = 2;
            
            /// <summary>
            /// 标识主键的实时记录顺序
            /// </summary>
            public const int Real_Id = 0;

            /// <summary>
            /// 角色标识的数字标识
            /// </summary>
            public const byte RoleId = 3;
            
            /// <summary>
            /// 角色标识的实时记录顺序
            /// </summary>
            public const int Real_RoleId = 1;

            /// <summary>
            /// 角色的数字标识
            /// </summary>
            public const byte Role = 4;
            
            /// <summary>
            /// 角色的实时记录顺序
            /// </summary>
            public const int Real_Role = 2;

            /// <summary>
            /// 用户Id的数字标识
            /// </summary>
            public const byte UserId = 5;
            
            /// <summary>
            /// 用户Id的实时记录顺序
            /// </summary>
            public const int Real_UserId = 3;

            /// <summary>
            /// 昵称的数字标识
            /// </summary>
            public const byte NickName = 6;
            
            /// <summary>
            /// 昵称的实时记录顺序
            /// </summary>
            public const int Real_NickName = 4;

            /// <summary>
            /// 身份证号的数字标识
            /// </summary>
            public const byte IdCard = 7;
            
            /// <summary>
            /// 身份证号的实时记录顺序
            /// </summary>
            public const int Real_IdCard = 5;

            /// <summary>
            /// 手机号的数字标识
            /// </summary>
            public const byte PhoneNumber = 8;
            
            /// <summary>
            /// 手机号的实时记录顺序
            /// </summary>
            public const int Real_PhoneNumber = 6;

            /// <summary>
            /// 真实姓名的数字标识
            /// </summary>
            public const byte RealName = 9;
            
            /// <summary>
            /// 真实姓名的实时记录顺序
            /// </summary>
            public const int Real_RealName = 7;

            /// <summary>
            /// 账户名的数字标识
            /// </summary>
            public const byte AccountName = 10;
            
            /// <summary>
            /// 账户名的实时记录顺序
            /// </summary>
            public const int Real_AccountName = 8;

            /// <summary>
            /// 用户密码的数字标识
            /// </summary>
            public const byte Password = 11;
            
            /// <summary>
            /// 用户密码的实时记录顺序
            /// </summary>
            public const int Real_Password = 9;

            /// <summary>
            /// 冻结更新的数字标识
            /// </summary>
            public const byte IsFreeze = 12;
            
            /// <summary>
            /// 冻结更新的实时记录顺序
            /// </summary>
            public const int Real_IsFreeze = 10;

            /// <summary>
            /// 数据状态的数字标识
            /// </summary>
            public const byte DataState = 13;
            
            /// <summary>
            /// 数据状态的实时记录顺序
            /// </summary>
            public const int Real_DataState = 11;

            /// <summary>
            /// 制作时间的数字标识
            /// </summary>
            public const byte AddDate = 14;
            
            /// <summary>
            /// 制作时间的实时记录顺序
            /// </summary>
            public const int Real_AddDate = 12;

            /// <summary>
            /// 最后修改者的数字标识
            /// </summary>
            public const byte LastReviserId = 15;
            
            /// <summary>
            /// 最后修改者的实时记录顺序
            /// </summary>
            public const int Real_LastReviserId = 13;

            /// <summary>
            /// 最后修改日期的数字标识
            /// </summary>
            public const byte LastModifyDate = 16;
            
            /// <summary>
            /// 最后修改日期的实时记录顺序
            /// </summary>
            public const int Real_LastModifyDate = 14;

            /// <summary>
            /// 制作人的数字标识
            /// </summary>
            public const byte AuthorId = 17;
            
            /// <summary>
            /// 制作人的实时记录顺序
            /// </summary>
            public const int Real_AuthorId = 15;

            /// <summary>
            /// 实体结构
            /// </summary>
            public static readonly EntitySturct Struct = new EntitySturct
            {
                EntityName = EntityName,
                Caption    = EntityCaption,
                Description= EntityDescription,
                PrimaryKey = EntityPrimaryKey,
                EntityType = EntityIdentity,
                Properties = new Dictionary<int, PropertySturct>
                {
                    {
                        Real_Id,
                        new PropertySturct
                        {
                            Index        = Id,
                            Name         = "Id",
                            Title        = "标识主键",
                            Caption      = @"标识主键",
                            Description  = @"标识主键",
                            ColumnName   = "id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RoleId,
                        new PropertySturct
                        {
                            Index        = RoleId,
                            Name         = "RoleId",
                            Title        = "角色标识",
                            Caption      = @"角色标识",
                            Description  = @"角色标识",
                            ColumnName   = "role_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Role,
                        new PropertySturct
                        {
                            Index        = Role,
                            Name         = "Role",
                            Title        = "角色",
                            Caption      = @"角色",
                            Description  = @"名称",
                            ColumnName   = "role",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_UserId,
                        new PropertySturct
                        {
                            Index        = UserId,
                            Name         = "UserId",
                            Title        = "用户Id",
                            Caption      = @"用户Id",
                            Description  = @"用户Id",
                            ColumnName   = "user_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_NickName,
                        new PropertySturct
                        {
                            Index        = NickName,
                            Name         = "NickName",
                            Title        = "昵称",
                            Caption      = @"昵称",
                            Description  = @"昵称",
                            ColumnName   = "nick_name",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_IdCard,
                        new PropertySturct
                        {
                            Index        = IdCard,
                            Name         = "IdCard",
                            Title        = "身份证号",
                            Caption      = @"身份证号",
                            Description  = @"身份证号",
                            ColumnName   = "id_card",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PhoneNumber,
                        new PropertySturct
                        {
                            Index        = PhoneNumber,
                            Name         = "PhoneNumber",
                            Title        = "手机号",
                            Caption      = @"手机号",
                            Description  = @"用户手机号",
                            ColumnName   = "phone_number",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RealName,
                        new PropertySturct
                        {
                            Index        = RealName,
                            Name         = "RealName",
                            Title        = "真实姓名",
                            Caption      = @"真实姓名",
                            Description  = @"真实姓名",
                            ColumnName   = "real_name",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AccountName,
                        new PropertySturct
                        {
                            Index        = AccountName,
                            Name         = "AccountName",
                            Title        = "账户名",
                            Caption      = @"账户名",
                            Description  = @"账户名",
                            ColumnName   = "account_name",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Password,
                        new PropertySturct
                        {
                            Index        = Password,
                            Name         = "Password",
                            Title        = "用户密码",
                            Caption      = @"用户密码",
                            Description  = @"用户密码",
                            ColumnName   = "password",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_IsFreeze,
                        new PropertySturct
                        {
                            Index        = IsFreeze,
                            Name         = "IsFreeze",
                            Title        = "冻结更新",
                            Caption      = @"冻结更新",
                            Description  = @"无论在什么数据状态,一旦设置且保存后,数据将不再允许执行Update的操作,作为Update的统一开关.取消的方法是单独设置这个字段的值",
                            ColumnName   = "is_freeze",
                            PropertyType = typeof(bool),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_DataState,
                        new PropertySturct
                        {
                            Index        = DataState,
                            Name         = "DataState",
                            Title        = "数据状态",
                            Caption      = @"数据状态",
                            Description  = @"数据状态",
                            ColumnName   = "data_state",
                            PropertyType = typeof(DataStateType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AddDate,
                        new PropertySturct
                        {
                            Index        = AddDate,
                            Name         = "AddDate",
                            Title        = "制作时间",
                            Caption      = @"制作时间",
                            Description  = @"制作时间",
                            ColumnName   = "add_date",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LastReviserId,
                        new PropertySturct
                        {
                            Index        = LastReviserId,
                            Name         = "LastReviserId",
                            Title        = "最后修改者",
                            Caption      = @"最后修改者",
                            Description  = @"最后修改者",
                            ColumnName   = "last_reviser_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LastModifyDate,
                        new PropertySturct
                        {
                            Index        = LastModifyDate,
                            Name         = "LastModifyDate",
                            Title        = "最后修改日期",
                            Caption      = @"最后修改日期",
                            Description  = @"最后修改日期",
                            ColumnName   = "last_modify_date",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AuthorId,
                        new PropertySturct
                        {
                            Index        = AuthorId,
                            Name         = "AuthorId",
                            Title        = "制作人",
                            Caption      = @"制作人",
                            Description  = @"制作人",
                            ColumnName   = "author_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    }
                }
            };
        }
        #endregion

    }
}