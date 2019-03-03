/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/1/2 20:11:23*/
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
    /// APP端用户信息表
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class UserData : IStateData , IHistoryData , IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserData()
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
        public void ChangePrimaryKey(long userId)
        {
            _userId = userId;
        }
        /// <summary>
        /// 用户Id
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _userId;

        partial void OnUserIdGet();

        partial void OnUserIdSet(ref long value);

        partial void OnUserIdLoad(ref long value);

        partial void OnUserIdSeted();

        
        /// <summary>
        /// 用户Id
        /// </summary>
        [DataMember , JsonProperty("UserId", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"用户Id")]
        public long UserId
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
                //if(this._userId > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnUserIdSet(ref value);
                this._userId = value;
                this.OnPropertyChanged(_DataStruct_.Real_UserId);
                OnUserIdSeted();
            }
        }
        /// <summary>
        /// 用户类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public AuthorizeType _userType;

        partial void OnUserTypeGet();

        partial void OnUserTypeSet(ref AuthorizeType value);

        partial void OnUserTypeSeted();

        
        /// <summary>
        /// 用户类型
        /// </summary>
        /// <remarks>
        /// 0 无,1 手机认证，2 账户认证，4 微信认证，8 微博认证
        /// </remarks>
        [DataMember , JsonProperty("UserType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"用户类型")]
        public  AuthorizeType UserType
        {
            get
            {
                OnUserTypeGet();
                return this._userType;
            }
            set
            {
                if(this._userType == value)
                    return;
                OnUserTypeSet(ref value);
                this._userType = value;
                OnUserTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UserType);
            }
        }
        /// <summary>
        /// 用户类型的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("用户类型")]
        public string UserType_Content => UserType.ToCaption();

        /// <summary>
        /// 用户类型的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int UserType_Number
        {
            get => (int)this.UserType;
            set => this.UserType = (AuthorizeType)value;
        }
        /// <summary>
        /// 用户代码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _openId;

        partial void OnOpenIdGet();

        partial void OnOpenIdSet(ref string value);

        partial void OnOpenIdSeted();

        
        /// <summary>
        /// 用户代码
        /// </summary>
        /// <remarks>
        /// 本系统发布的数字与大写字母组成的用户代码
        /// </remarks>
        /// <value>
        /// 不能为空.可存储16个字符.合理长度应不大于16.
        /// </value>
        [DataMember , JsonProperty("OpenId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"用户代码")]
        public  string OpenId
        {
            get
            {
                OnOpenIdGet();
                return this._openId;
            }
            set
            {
                if(this._openId == value)
                    return;
                OnOpenIdSet(ref value);
                this._openId = value;
                OnOpenIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OpenId);
            }
        }
        /// <summary>
        /// 用户状态
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public UserStatusType _status;

        partial void OnStatusGet();

        partial void OnStatusSet(ref UserStatusType value);

        partial void OnStatusSeted();

        
        /// <summary>
        /// 用户状态
        /// </summary>
        /// <remarks>
        /// 是否可用,0:不可用;1:可用
        /// </remarks>
        [DataMember , JsonProperty("Status", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"用户状态")]
        public  UserStatusType Status
        {
            get
            {
                OnStatusGet();
                return this._status;
            }
            set
            {
                if(this._status == value)
                    return;
                OnStatusSet(ref value);
                this._status = value;
                OnStatusSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Status);
            }
        }
        /// <summary>
        /// 用户状态的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("用户状态")]
        public string Status_Content => Status.ToCaption();

        /// <summary>
        /// 用户状态的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int Status_Number
        {
            get => (int)this.Status;
            set => this.Status = (UserStatusType)value;
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
        /// 注册来源
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public AuthorizeType _registSoure;

        partial void OnRegistSoureGet();

        partial void OnRegistSoureSet(ref AuthorizeType value);

        partial void OnRegistSoureSeted();

        
        /// <summary>
        /// 注册来源
        /// </summary>
        /// <remarks>
        /// 来源
        /// </remarks>
        [DataMember , JsonProperty("RegistSoure", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"注册来源")]
        public  AuthorizeType RegistSoure
        {
            get
            {
                OnRegistSoureGet();
                return this._registSoure;
            }
            set
            {
                if(this._registSoure == value)
                    return;
                OnRegistSoureSet(ref value);
                this._registSoure = value;
                OnRegistSoureSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RegistSoure);
            }
        }
        /// <summary>
        /// 注册来源的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("注册来源")]
        public string RegistSoure_Content => RegistSoure.ToCaption();

        /// <summary>
        /// 注册来源的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int RegistSoure_Number
        {
            get => (int)this.RegistSoure;
            set => this.RegistSoure = (AuthorizeType)value;
        }
        /// <summary>
        /// 注册来源操作系统
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _os;

        partial void OnOsGet();

        partial void OnOsSet(ref string value);

        partial void OnOsSeted();

        
        /// <summary>
        /// 注册来源操作系统
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("Os", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"注册来源操作系统")]
        public  string Os
        {
            get
            {
                OnOsGet();
                return this._os;
            }
            set
            {
                if(this._os == value)
                    return;
                OnOsSet(ref value);
                this._os = value;
                OnOsSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Os);
            }
        }
        /// <summary>
        /// 注册时的应用
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _app;

        partial void OnAppGet();

        partial void OnAppSet(ref string value);

        partial void OnAppSeted();

        
        /// <summary>
        /// 注册时的应用
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("App", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"注册时的应用")]
        public  string App
        {
            get
            {
                OnAppGet();
                return this._app;
            }
            set
            {
                if(this._app == value)
                    return;
                OnAppSet(ref value);
                this._app = value;
                OnAppSeted();
                this.OnPropertyChanged(_DataStruct_.Real_App);
            }
        }
        /// <summary>
        /// 注册时设备识别码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _deviceId;

        partial void OnDeviceIdGet();

        partial void OnDeviceIdSet(ref string value);

        partial void OnDeviceIdSeted();

        
        /// <summary>
        /// 注册时设备识别码
        /// </summary>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataMember , JsonProperty("DeviceId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"注册时设备识别码")]
        public  string DeviceId
        {
            get
            {
                OnDeviceIdGet();
                return this._deviceId;
            }
            set
            {
                if(this._deviceId == value)
                    return;
                OnDeviceIdSet(ref value);
                this._deviceId = value;
                OnDeviceIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_DeviceId);
            }
        }
        /// <summary>
        /// 注册来源渠道码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _channel;

        partial void OnChannelGet();

        partial void OnChannelSet(ref string value);

        partial void OnChannelSeted();

        
        /// <summary>
        /// 注册来源渠道码
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("Channel", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"注册来源渠道码")]
        public  string Channel
        {
            get
            {
                OnChannelGet();
                return this._channel;
            }
            set
            {
                if(this._channel == value)
                    return;
                OnChannelSet(ref value);
                this._channel = value;
                OnChannelSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Channel);
            }
        }
        /// <summary>
        /// 注册来源活动跟踪码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _traceMark;

        partial void OnTraceMarkGet();

        partial void OnTraceMarkSet(ref string value);

        partial void OnTraceMarkSeted();

        
        /// <summary>
        /// 注册来源活动跟踪码
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("TraceMark", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"注册来源活动跟踪码")]
        public  string TraceMark
        {
            get
            {
                OnTraceMarkGet();
                return this._traceMark;
            }
            set
            {
                if(this._traceMark == value)
                    return;
                OnTraceMarkSet(ref value);
                this._traceMark = value;
                OnTraceMarkSeted();
                this.OnPropertyChanged(_DataStruct_.Real_TraceMark);
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


        /// <summary>
        /// 对象标识
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public long Id
        {
            get
            {
                return this.UserId;
            }
            set
            {
                this.UserId = value;
            }
        }
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
            case "userid":
                this.UserId = (long)Convert.ToDecimal(value);
                return;
            case "usertype":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.UserType = (AuthorizeType)(int)value;
                    }
                    else if(value is AuthorizeType)
                    {
                        this.UserType = (AuthorizeType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        AuthorizeType val;
                        if (AuthorizeType.TryParse(str, out val))
                        {
                            this.UserType = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.UserType = (AuthorizeType)vl;
                            }
                        }
                    }
                }
                return;
            case "openid":
                this.OpenId = value == null ? null : value.ToString();
                return;
            case "status":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.Status = (UserStatusType)(int)value;
                    }
                    else if(value is UserStatusType)
                    {
                        this.Status = (UserStatusType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        UserStatusType val;
                        if (UserStatusType.TryParse(str, out val))
                        {
                            this.Status = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.Status = (UserStatusType)vl;
                            }
                        }
                    }
                }
                return;
            case "adddate":
                this.AddDate = Convert.ToDateTime(value);
                return;
            case "registsoure":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.RegistSoure = (AuthorizeType)(int)value;
                    }
                    else if(value is AuthorizeType)
                    {
                        this.RegistSoure = (AuthorizeType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        AuthorizeType val;
                        if (AuthorizeType.TryParse(str, out val))
                        {
                            this.RegistSoure = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.RegistSoure = (AuthorizeType)vl;
                            }
                        }
                    }
                }
                return;
            case "os":
                this.Os = value == null ? null : value.ToString();
                return;
            case "app":
                this.App = value == null ? null : value.ToString();
                return;
            case "deviceid":
                this.DeviceId = value == null ? null : value.ToString();
                return;
            case "channel":
                this.Channel = value == null ? null : value.ToString();
                return;
            case "tracemark":
                this.TraceMark = value == null ? null : value.ToString();
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
            case _DataStruct_.UserId:
                this.UserId = Convert.ToInt64(value);
                return;
            case _DataStruct_.UserType:
                this.UserType = (AuthorizeType)value;
                return;
            case _DataStruct_.OpenId:
                this.OpenId = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Status:
                this.Status = (UserStatusType)value;
                return;
            case _DataStruct_.AddDate:
                this.AddDate = Convert.ToDateTime(value);
                return;
            case _DataStruct_.RegistSoure:
                this.RegistSoure = (AuthorizeType)value;
                return;
            case _DataStruct_.Os:
                this.Os = value == null ? null : value.ToString();
                return;
            case _DataStruct_.App:
                this.App = value == null ? null : value.ToString();
                return;
            case _DataStruct_.DeviceId:
                this.DeviceId = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Channel:
                this.Channel = value == null ? null : value.ToString();
                return;
            case _DataStruct_.TraceMark:
                this.TraceMark = value == null ? null : value.ToString();
                return;
            case _DataStruct_.IsFreeze:
                this.IsFreeze = Convert.ToBoolean(value);
                return;
            case _DataStruct_.DataState:
                this.DataState = (DataStateType)value;
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
            case "userid":
                return this.UserId;
            case "usertype":
                return this.UserType.ToCaption();
            case "openid":
                return this.OpenId;
            case "status":
                return this.Status.ToCaption();
            case "adddate":
                return this.AddDate;
            case "registsoure":
                return this.RegistSoure.ToCaption();
            case "os":
                return this.Os;
            case "app":
                return this.App;
            case "deviceid":
                return this.DeviceId;
            case "channel":
                return this.Channel;
            case "tracemark":
                return this.TraceMark;
            case "isfreeze":
                return this.IsFreeze;
            case "datastate":
                return this.DataState.ToCaption();
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
                case _DataStruct_.UserId:
                    return this.UserId;
                case _DataStruct_.UserType:
                    return this.UserType;
                case _DataStruct_.OpenId:
                    return this.OpenId;
                case _DataStruct_.Status:
                    return this.Status;
                case _DataStruct_.AddDate:
                    return this.AddDate;
                case _DataStruct_.RegistSoure:
                    return this.RegistSoure;
                case _DataStruct_.Os:
                    return this.Os;
                case _DataStruct_.App:
                    return this.App;
                case _DataStruct_.DeviceId:
                    return this.DeviceId;
                case _DataStruct_.Channel:
                    return this.Channel;
                case _DataStruct_.TraceMark:
                    return this.TraceMark;
                case _DataStruct_.IsFreeze:
                    return this.IsFreeze;
                case _DataStruct_.DataState:
                    return this.DataState;
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
        

        partial void CopyExtendValue(UserData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as UserData;
            if(sourceEntity == null)
                return;
            this._userId = sourceEntity._userId;
            this._userType = sourceEntity._userType;
            this._openId = sourceEntity._openId;
            this._status = sourceEntity._status;
            this._addDate = sourceEntity._addDate;
            this._registSoure = sourceEntity._registSoure;
            this._os = sourceEntity._os;
            this._app = sourceEntity._app;
            this._deviceId = sourceEntity._deviceId;
            this._channel = sourceEntity._channel;
            this._traceMark = sourceEntity._traceMark;
            this._isFreeze = sourceEntity._isFreeze;
            this._dataState = sourceEntity._dataState;
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
        public void Copy(UserData source)
        {
                this.UserId = source.UserId;
                this.UserType = source.UserType;
                this.OpenId = source.OpenId;
                this.Status = source.Status;
                this.AddDate = source.AddDate;
                this.RegistSoure = source.RegistSoure;
                this.Os = source.Os;
                this.App = source.App;
                this.DeviceId = source.DeviceId;
                this.Channel = source.Channel;
                this.TraceMark = source.TraceMark;
                this.IsFreeze = source.IsFreeze;
                this.DataState = source.DataState;
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
                OnUserIdModified(subsist,false);
                OnUserTypeModified(subsist,false);
                OnOpenIdModified(subsist,false);
                OnStatusModified(subsist,false);
                OnAddDateModified(subsist,false);
                OnRegistSoureModified(subsist,false);
                OnOsModified(subsist,false);
                OnAppModified(subsist,false);
                OnDeviceIdModified(subsist,false);
                OnChannelModified(subsist,false);
                OnTraceMarkModified(subsist,false);
                OnIsFreezeModified(subsist,false);
                OnDataStateModified(subsist,false);
                OnLastReviserIdModified(subsist,false);
                OnLastModifyDateModified(subsist,false);
                OnAuthorIdModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnUserIdModified(subsist,true);
                OnUserTypeModified(subsist,true);
                OnOpenIdModified(subsist,true);
                OnStatusModified(subsist,true);
                OnAddDateModified(subsist,true);
                OnRegistSoureModified(subsist,true);
                OnOsModified(subsist,true);
                OnAppModified(subsist,true);
                OnDeviceIdModified(subsist,true);
                OnChannelModified(subsist,true);
                OnTraceMarkModified(subsist,true);
                OnIsFreezeModified(subsist,true);
                OnDataStateModified(subsist,true);
                OnLastReviserIdModified(subsist,true);
                OnLastModifyDateModified(subsist,true);
                OnAuthorIdModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[16] > 0)
            {
                OnUserIdModified(subsist,modifieds[_DataStruct_.Real_UserId] == 1);
                OnUserTypeModified(subsist,modifieds[_DataStruct_.Real_UserType] == 1);
                OnOpenIdModified(subsist,modifieds[_DataStruct_.Real_OpenId] == 1);
                OnStatusModified(subsist,modifieds[_DataStruct_.Real_Status] == 1);
                OnAddDateModified(subsist,modifieds[_DataStruct_.Real_AddDate] == 1);
                OnRegistSoureModified(subsist,modifieds[_DataStruct_.Real_RegistSoure] == 1);
                OnOsModified(subsist,modifieds[_DataStruct_.Real_Os] == 1);
                OnAppModified(subsist,modifieds[_DataStruct_.Real_App] == 1);
                OnDeviceIdModified(subsist,modifieds[_DataStruct_.Real_DeviceId] == 1);
                OnChannelModified(subsist,modifieds[_DataStruct_.Real_Channel] == 1);
                OnTraceMarkModified(subsist,modifieds[_DataStruct_.Real_TraceMark] == 1);
                OnIsFreezeModified(subsist,modifieds[_DataStruct_.Real_IsFreeze] == 1);
                OnDataStateModified(subsist,modifieds[_DataStruct_.Real_DataState] == 1);
                OnLastReviserIdModified(subsist,modifieds[_DataStruct_.Real_LastReviserId] == 1);
                OnLastModifyDateModified(subsist,modifieds[_DataStruct_.Real_LastModifyDate] == 1);
                OnAuthorIdModified(subsist,modifieds[_DataStruct_.Real_AuthorId] == 1);
            }
        }

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
        /// 用户类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 用户代码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOpenIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 用户状态修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnStatusModified(EntitySubsist subsist,bool isModified);

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
        /// 注册来源修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRegistSoureModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 注册来源操作系统修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOsModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 注册时的应用修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 注册时设备识别码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDeviceIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 注册来源渠道码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnChannelModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 注册来源活动跟踪码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTraceMarkModified(EntitySubsist subsist,bool isModified);

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
            public const string EntityName = @"User";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户信息";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"APP端用户信息表";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0xD0012;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "UserId";
            
            
            /// <summary>
            /// 用户Id的数字标识
            /// </summary>
            public const byte UserId = 1;
            
            /// <summary>
            /// 用户Id的实时记录顺序
            /// </summary>
            public const int Real_UserId = 0;

            /// <summary>
            /// 用户类型的数字标识
            /// </summary>
            public const byte UserType = 2;
            
            /// <summary>
            /// 用户类型的实时记录顺序
            /// </summary>
            public const int Real_UserType = 1;

            /// <summary>
            /// 用户代码的数字标识
            /// </summary>
            public const byte OpenId = 9;
            
            /// <summary>
            /// 用户代码的实时记录顺序
            /// </summary>
            public const int Real_OpenId = 2;

            /// <summary>
            /// 用户状态的数字标识
            /// </summary>
            public const byte Status = 11;
            
            /// <summary>
            /// 用户状态的实时记录顺序
            /// </summary>
            public const int Real_Status = 3;

            /// <summary>
            /// 制作时间的数字标识
            /// </summary>
            public const byte AddDate = 12;
            
            /// <summary>
            /// 制作时间的实时记录顺序
            /// </summary>
            public const int Real_AddDate = 4;

            /// <summary>
            /// 注册来源的数字标识
            /// </summary>
            public const byte RegistSoure = 3;
            
            /// <summary>
            /// 注册来源的实时记录顺序
            /// </summary>
            public const int Real_RegistSoure = 5;

            /// <summary>
            /// 注册来源操作系统的数字标识
            /// </summary>
            public const byte Os = 6;
            
            /// <summary>
            /// 注册来源操作系统的实时记录顺序
            /// </summary>
            public const int Real_Os = 6;

            /// <summary>
            /// 注册时的应用的数字标识
            /// </summary>
            public const byte App = 7;
            
            /// <summary>
            /// 注册时的应用的实时记录顺序
            /// </summary>
            public const int Real_App = 7;

            /// <summary>
            /// 注册时设备识别码的数字标识
            /// </summary>
            public const byte DeviceId = 8;
            
            /// <summary>
            /// 注册时设备识别码的实时记录顺序
            /// </summary>
            public const int Real_DeviceId = 8;

            /// <summary>
            /// 注册来源渠道码的数字标识
            /// </summary>
            public const byte Channel = 4;
            
            /// <summary>
            /// 注册来源渠道码的实时记录顺序
            /// </summary>
            public const int Real_Channel = 9;

            /// <summary>
            /// 注册来源活动跟踪码的数字标识
            /// </summary>
            public const byte TraceMark = 5;
            
            /// <summary>
            /// 注册来源活动跟踪码的实时记录顺序
            /// </summary>
            public const int Real_TraceMark = 10;

            /// <summary>
            /// 冻结更新的数字标识
            /// </summary>
            public const byte IsFreeze = 13;
            
            /// <summary>
            /// 冻结更新的实时记录顺序
            /// </summary>
            public const int Real_IsFreeze = 11;

            /// <summary>
            /// 数据状态的数字标识
            /// </summary>
            public const byte DataState = 14;
            
            /// <summary>
            /// 数据状态的实时记录顺序
            /// </summary>
            public const int Real_DataState = 12;

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
                        Real_UserType,
                        new PropertySturct
                        {
                            Index        = UserType,
                            Name         = "UserType",
                            Title        = "用户类型",
                            Caption      = @"用户类型",
                            Description  = @"0 无,1 手机认证，2 账户认证，4 微信认证，8 微博认证",
                            ColumnName   = "user_type",
                            PropertyType = typeof(AuthorizeType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OpenId,
                        new PropertySturct
                        {
                            Index        = OpenId,
                            Name         = "OpenId",
                            Title        = "用户代码",
                            Caption      = @"用户代码",
                            Description  = @"本系统发布的数字与大写字母组成的用户代码",
                            ColumnName   = "open_id",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Status,
                        new PropertySturct
                        {
                            Index        = Status,
                            Name         = "Status",
                            Title        = "用户状态",
                            Caption      = @"用户状态",
                            Description  = @"是否可用,0:不可用;1:可用",
                            ColumnName   = "status",
                            PropertyType = typeof(UserStatusType),
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
                        Real_RegistSoure,
                        new PropertySturct
                        {
                            Index        = RegistSoure,
                            Name         = "RegistSoure",
                            Title        = "注册来源",
                            Caption      = @"注册来源",
                            Description  = @"来源",
                            ColumnName   = "regist_soure",
                            PropertyType = typeof(AuthorizeType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Os,
                        new PropertySturct
                        {
                            Index        = Os,
                            Name         = "Os",
                            Title        = "注册来源操作系统",
                            Caption      = @"注册来源操作系统",
                            Description  = @"注册来源操作系统",
                            ColumnName   = "os",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_App,
                        new PropertySturct
                        {
                            Index        = App,
                            Name         = "App",
                            Title        = "注册时的应用",
                            Caption      = @"注册时的应用",
                            Description  = @"注册时的应用",
                            ColumnName   = "app",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_DeviceId,
                        new PropertySturct
                        {
                            Index        = DeviceId,
                            Name         = "DeviceId",
                            Title        = "注册时设备识别码",
                            Caption      = @"注册时设备识别码",
                            Description  = @"注册时设备识别码",
                            ColumnName   = "device_id",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Channel,
                        new PropertySturct
                        {
                            Index        = Channel,
                            Name         = "Channel",
                            Title        = "注册来源渠道码",
                            Caption      = @"注册来源渠道码",
                            Description  = @"注册来源渠道码",
                            ColumnName   = "channel",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_TraceMark,
                        new PropertySturct
                        {
                            Index        = TraceMark,
                            Name         = "TraceMark",
                            Title        = "注册来源活动跟踪码",
                            Caption      = @"注册来源活动跟踪码",
                            Description  = @"注册来源活动跟踪码",
                            ColumnName   = "trace_mark",
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