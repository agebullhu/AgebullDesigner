/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/15 10:58:40*/
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
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;

using Agebull.Common.Organizations;
using Agebull.Common.OAuth;
#endregion

namespace Agebull.Common.AppManage
{
    /// <summary>
    /// 系统内的应用的信息
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class AppInfoData : IStateData , IHistoryData , IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public AppInfoData()
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
        /// 标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _id;

        partial void OnIdGet();

        partial void OnIdSet(ref long value);

        partial void OnIdLoad(ref long value);

        partial void OnIdSeted();

        
        /// <summary>
        /// 标识
        /// </summary>
        [DataMember , JsonProperty("id", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"标识")]
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
        /// 应用类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public AppType _appType;

        partial void OnAppTypeGet();

        partial void OnAppTypeSet(ref AppType value);

        partial void OnAppTypeSeted();

        
        /// <summary>
        /// 应用类型
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("AppType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用类型")]
        public  AppType AppType
        {
            get
            {
                OnAppTypeGet();
                return this._appType;
            }
            set
            {
                if(this._appType == value)
                    return;
                OnAppTypeSet(ref value);
                this._appType = value;
                OnAppTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AppType);
            }
        }
        /// <summary>
        /// 应用类型的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("应用类型")]
        public string AppType_Content => AppType.ToCaption();

        /// <summary>
        /// 应用类型的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int AppType_Number
        {
            get => (int)this.AppType;
            set => this.AppType = (AppType)value;
        }
        /// <summary>
        /// 应用简称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _shortName;

        partial void OnShortNameGet();

        partial void OnShortNameSet(ref string value);

        partial void OnShortNameSeted();

        
        /// <summary>
        /// 应用简称
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("shortName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用简称")]
        public  string ShortName
        {
            get
            {
                OnShortNameGet();
                return this._shortName;
            }
            set
            {
                if(this._shortName == value)
                    return;
                OnShortNameSet(ref value);
                this._shortName = value;
                OnShortNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ShortName);
            }
        }
        /// <summary>
        /// 应用全称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _fullName;

        partial void OnFullNameGet();

        partial void OnFullNameSet(ref string value);

        partial void OnFullNameSeted();

        
        /// <summary>
        /// 应用全称
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("fullName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用全称")]
        public  string FullName
        {
            get
            {
                OnFullNameGet();
                return this._fullName;
            }
            set
            {
                if(this._fullName == value)
                    return;
                OnFullNameSet(ref value);
                this._fullName = value;
                OnFullNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_FullName);
            }
        }
        /// <summary>
        /// 应用标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _appId;

        partial void OnAppIdGet();

        partial void OnAppIdSet(ref string value);

        partial void OnAppIdSeted();

        
        /// <summary>
        /// 应用标识
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("appId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用标识")]
        public  string AppId
        {
            get
            {
                OnAppIdGet();
                return this._appId;
            }
            set
            {
                if(this._appId == value)
                    return;
                OnAppIdSet(ref value);
                this._appId = value;
                OnAppIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AppId);
            }
        }
        /// <summary>
        /// 应用令牌
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _appKey;

        partial void OnAppKeyGet();

        partial void OnAppKeySet(ref string value);

        partial void OnAppKeySeted();

        
        /// <summary>
        /// 应用令牌
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("AppKey", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用令牌")]
        public  string AppKey
        {
            get
            {
                OnAppKeyGet();
                return this._appKey;
            }
            set
            {
                if(this._appKey == value)
                    return;
                OnAppKeySet(ref value);
                this._appKey = value;
                OnAppKeySeted();
                this.OnPropertyChanged(_DataStruct_.Real_AppKey);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _memo;

        partial void OnMemoGet();

        partial void OnMemoSet(ref string value);

        partial void OnMemoSeted();

        
        /// <summary>
        /// 备注
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("memo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"备注")]
        public  string Memo
        {
            get
            {
                OnMemoGet();
                return this._memo;
            }
            set
            {
                if(this._memo == value)
                    return;
                OnMemoSet(ref value);
                this._memo = value;
                OnMemoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Memo);
            }
        }
        /// <summary>
        /// 数据状态枚举类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DataStateType _dataStateType;

        partial void OnDataStateTypeGet();

        partial void OnDataStateTypeSet(ref DataStateType value);

        partial void OnDataStateTypeSeted();

        
        /// <summary>
        /// 数据状态枚举类型
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("DataStateType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"数据状态枚举类型")]
        public  DataStateType DataStateType
        {
            get
            {
                OnDataStateTypeGet();
                return this._dataStateType;
            }
            set
            {
                if(this._dataStateType == value)
                    return;
                OnDataStateTypeSet(ref value);
                this._dataStateType = value;
                OnDataStateTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_DataStateType);
            }
        }
        /// <summary>
        /// 数据状态枚举类型的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("数据状态枚举类型")]
        public string DataStateType_Content => DataStateType.ToCaption();

        /// <summary>
        /// 数据状态枚举类型的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int DataStateType_Number
        {
            get => (int)this.DataStateType;
            set => this.DataStateType = (DataStateType)value;
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
        protected override bool SetValueInner(string property, string value)
        {
            if(property == null) return false;
            switch(property.Trim().ToLower())
            {
            case "id":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.Id = vl;
                        return true;
                    }
                }
                return false;
            case "apptype":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (AppType.TryParse(value, out var val))
                    {
                        this.AppType = val;
                        return true;
                    }
                    else if (int.TryParse(value, out var vl))
                    {
                        this.AppType = (AppType)vl;
                        return true;
                    }
                }
                return false;
            case "shortname":
                this.ShortName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "fullname":
                this.FullName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "appid":
                this.AppId = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "appkey":
                this.AppKey = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "memo":
                this.Memo = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "datastatetype":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DataStateType.TryParse(value, out var val))
                    {
                        this.DataStateType = val;
                        return true;
                    }
                    else if (int.TryParse(value, out var vl))
                    {
                        this.DataStateType = (DataStateType)vl;
                        return true;
                    }
                }
                return false;
            case "isfreeze":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (bool.TryParse(value, out var vl))
                    {
                        this.IsFreeze = vl;
                        return true;
                    }
                }
                return false;
            case "adddate":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.AddDate = vl;
                        return true;
                    }
                }
                return false;
            case "authorid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.AuthorId = vl;
                        return true;
                    }
                }
                return false;
            case "lastreviserid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.LastReviserId = vl;
                        return true;
                    }
                }
                return false;
            case "lastmodifydate":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.LastModifyDate = vl;
                        return true;
                    }
                }
                return false;
            case "datastate":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DataStateType.TryParse(value, out var val))
                    {
                        this.DataState = val;
                        return true;
                    }
                    else if (int.TryParse(value, out var vl))
                    {
                        this.DataState = (DataStateType)vl;
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

    

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
            case "apptype":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.AppType = (AppType)(int)value;
                    }
                    else if(value is AppType)
                    {
                        this.AppType = (AppType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        AppType val;
                        if (AppType.TryParse(str, out val))
                        {
                            this.AppType = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.AppType = (AppType)vl;
                            }
                        }
                    }
                }
                return;
            case "shortname":
                this.ShortName = value == null ? null : value.ToString();
                return;
            case "fullname":
                this.FullName = value == null ? null : value.ToString();
                return;
            case "appid":
                this.AppId = value == null ? null : value.ToString();
                return;
            case "appkey":
                this.AppKey = value == null ? null : value.ToString();
                return;
            case "memo":
                this.Memo = value == null ? null : value.ToString();
                return;
            case "datastatetype":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.DataStateType = (DataStateType)(int)value;
                    }
                    else if(value is DataStateType)
                    {
                        this.DataStateType = (DataStateType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        DataStateType val;
                        if (DataStateType.TryParse(str, out val))
                        {
                            this.DataStateType = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.DataStateType = (DataStateType)vl;
                            }
                        }
                    }
                }
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
            case "adddate":
                this.AddDate = Convert.ToDateTime(value);
                return;
            case "authorid":
                this.AuthorId = (long)Convert.ToDecimal(value);
                return;
            case "lastreviserid":
                this.LastReviserId = (long)Convert.ToDecimal(value);
                return;
            case "lastmodifydate":
                this.LastModifyDate = Convert.ToDateTime(value);
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
            case _DataStruct_.AppType:
                this.AppType = (AppType)value;
                return;
            case _DataStruct_.ShortName:
                this.ShortName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.FullName:
                this.FullName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AppId:
                this.AppId = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AppKey:
                this.AppKey = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Memo:
                this.Memo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.DataStateType:
                this.DataStateType = (DataStateType)value;
                return;
            case _DataStruct_.IsFreeze:
                this.IsFreeze = Convert.ToBoolean(value);
                return;
            case _DataStruct_.AddDate:
                this.AddDate = Convert.ToDateTime(value);
                return;
            case _DataStruct_.AuthorId:
                this.AuthorId = Convert.ToInt64(value);
                return;
            case _DataStruct_.LastReviserId:
                this.LastReviserId = Convert.ToInt64(value);
                return;
            case _DataStruct_.LastModifyDate:
                this.LastModifyDate = Convert.ToDateTime(value);
                return;
            case _DataStruct_.DataState:
                this.DataState = (DataStateType)value;
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
            case "apptype":
                return this.AppType.ToCaption();
            case "shortname":
                return this.ShortName;
            case "fullname":
                return this.FullName;
            case "appid":
                return this.AppId;
            case "appkey":
                return this.AppKey;
            case "memo":
                return this.Memo;
            case "datastatetype":
                return this.DataStateType.ToCaption();
            case "isfreeze":
                return this.IsFreeze;
            case "adddate":
                return this.AddDate;
            case "authorid":
                return this.AuthorId;
            case "lastreviserid":
                return this.LastReviserId;
            case "lastmodifydate":
                return this.LastModifyDate;
            case "datastate":
                return this.DataState.ToCaption();
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
                case _DataStruct_.AppType:
                    return this.AppType;
                case _DataStruct_.ShortName:
                    return this.ShortName;
                case _DataStruct_.FullName:
                    return this.FullName;
                case _DataStruct_.AppId:
                    return this.AppId;
                case _DataStruct_.AppKey:
                    return this.AppKey;
                case _DataStruct_.Memo:
                    return this.Memo;
                case _DataStruct_.DataStateType:
                    return this.DataStateType;
                case _DataStruct_.IsFreeze:
                    return this.IsFreeze;
                case _DataStruct_.AddDate:
                    return this.AddDate;
                case _DataStruct_.AuthorId:
                    return this.AuthorId;
                case _DataStruct_.LastReviserId:
                    return this.LastReviserId;
                case _DataStruct_.LastModifyDate:
                    return this.LastModifyDate;
                case _DataStruct_.DataState:
                    return this.DataState;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(AppInfoData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as AppInfoData;
            if(sourceEntity == null)
                return;
            this._id = sourceEntity._id;
            this._appType = sourceEntity._appType;
            this._shortName = sourceEntity._shortName;
            this._fullName = sourceEntity._fullName;
            this._appId = sourceEntity._appId;
            this._appKey = sourceEntity._appKey;
            this._memo = sourceEntity._memo;
            this._dataStateType = sourceEntity._dataStateType;
            this._isFreeze = sourceEntity._isFreeze;
            this._addDate = sourceEntity._addDate;
            this._authorId = sourceEntity._authorId;
            this._lastReviserId = sourceEntity._lastReviserId;
            this._lastModifyDate = sourceEntity._lastModifyDate;
            this._dataState = sourceEntity._dataState;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(AppInfoData source)
        {
                this.Id = source.Id;
                this.AppType = source.AppType;
                this.ShortName = source.ShortName;
                this.FullName = source.FullName;
                this.AppId = source.AppId;
                this.AppKey = source.AppKey;
                this.Memo = source.Memo;
                this.DataStateType = source.DataStateType;
                this.IsFreeze = source.IsFreeze;
                this.AddDate = source.AddDate;
                this.AuthorId = source.AuthorId;
                this.LastReviserId = source.LastReviserId;
                this.LastModifyDate = source.LastModifyDate;
                this.DataState = source.DataState;
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
                OnAppTypeModified(subsist,false);
                OnShortNameModified(subsist,false);
                OnFullNameModified(subsist,false);
                OnAppIdModified(subsist,false);
                OnAppKeyModified(subsist,false);
                OnMemoModified(subsist,false);
                OnDataStateTypeModified(subsist,false);
                OnIsFreezeModified(subsist,false);
                OnAddDateModified(subsist,false);
                OnAuthorIdModified(subsist,false);
                OnLastReviserIdModified(subsist,false);
                OnLastModifyDateModified(subsist,false);
                OnDataStateModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIdModified(subsist,true);
                OnAppTypeModified(subsist,true);
                OnShortNameModified(subsist,true);
                OnFullNameModified(subsist,true);
                OnAppIdModified(subsist,true);
                OnAppKeyModified(subsist,true);
                OnMemoModified(subsist,true);
                OnDataStateTypeModified(subsist,true);
                OnIsFreezeModified(subsist,true);
                OnAddDateModified(subsist,true);
                OnAuthorIdModified(subsist,true);
                OnLastReviserIdModified(subsist,true);
                OnLastModifyDateModified(subsist,true);
                OnDataStateModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[14] > 0)
            {
                OnIdModified(subsist,modifieds[_DataStruct_.Real_Id] == 1);
                OnAppTypeModified(subsist,modifieds[_DataStruct_.Real_AppType] == 1);
                OnShortNameModified(subsist,modifieds[_DataStruct_.Real_ShortName] == 1);
                OnFullNameModified(subsist,modifieds[_DataStruct_.Real_FullName] == 1);
                OnAppIdModified(subsist,modifieds[_DataStruct_.Real_AppId] == 1);
                OnAppKeyModified(subsist,modifieds[_DataStruct_.Real_AppKey] == 1);
                OnMemoModified(subsist,modifieds[_DataStruct_.Real_Memo] == 1);
                OnDataStateTypeModified(subsist,modifieds[_DataStruct_.Real_DataStateType] == 1);
                OnIsFreezeModified(subsist,modifieds[_DataStruct_.Real_IsFreeze] == 1);
                OnAddDateModified(subsist,modifieds[_DataStruct_.Real_AddDate] == 1);
                OnAuthorIdModified(subsist,modifieds[_DataStruct_.Real_AuthorId] == 1);
                OnLastReviserIdModified(subsist,modifieds[_DataStruct_.Real_LastReviserId] == 1);
                OnLastModifyDateModified(subsist,modifieds[_DataStruct_.Real_LastModifyDate] == 1);
                OnDataStateModified(subsist,modifieds[_DataStruct_.Real_DataState] == 1);
            }
        }

        /// <summary>
        /// 标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 应用类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 应用简称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnShortNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 应用全称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnFullNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 应用标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 应用令牌修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppKeyModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 备注修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMemoModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 数据状态枚举类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDataStateTypeModified(EntitySubsist subsist,bool isModified);

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
        /// 制作时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAddDateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 制作人修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAuthorIdModified(EntitySubsist subsist,bool isModified);

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
        /// 数据状态修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDataStateModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"AppInfo";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"应用信息";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"系统内的应用的信息";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x2000B;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "Id";
            
            
            /// <summary>
            /// 标识的数字标识
            /// </summary>
            public const byte Id = 2;
            
            /// <summary>
            /// 标识的实时记录顺序
            /// </summary>
            public const int Real_Id = 0;

            /// <summary>
            /// 应用类型的数字标识
            /// </summary>
            public const byte AppType = 3;
            
            /// <summary>
            /// 应用类型的实时记录顺序
            /// </summary>
            public const int Real_AppType = 1;

            /// <summary>
            /// 应用简称的数字标识
            /// </summary>
            public const byte ShortName = 4;
            
            /// <summary>
            /// 应用简称的实时记录顺序
            /// </summary>
            public const int Real_ShortName = 2;

            /// <summary>
            /// 应用全称的数字标识
            /// </summary>
            public const byte FullName = 5;
            
            /// <summary>
            /// 应用全称的实时记录顺序
            /// </summary>
            public const int Real_FullName = 3;

            /// <summary>
            /// 应用标识的数字标识
            /// </summary>
            public const byte AppId = 6;
            
            /// <summary>
            /// 应用标识的实时记录顺序
            /// </summary>
            public const int Real_AppId = 4;

            /// <summary>
            /// 应用令牌的数字标识
            /// </summary>
            public const byte AppKey = 7;
            
            /// <summary>
            /// 应用令牌的实时记录顺序
            /// </summary>
            public const int Real_AppKey = 5;

            /// <summary>
            /// 备注的数字标识
            /// </summary>
            public const byte Memo = 8;
            
            /// <summary>
            /// 备注的实时记录顺序
            /// </summary>
            public const int Real_Memo = 6;

            /// <summary>
            /// 数据状态枚举类型的数字标识
            /// </summary>
            public const byte DataStateType = 9;
            
            /// <summary>
            /// 数据状态枚举类型的实时记录顺序
            /// </summary>
            public const int Real_DataStateType = 7;

            /// <summary>
            /// 冻结更新的数字标识
            /// </summary>
            public const byte IsFreeze = 10;
            
            /// <summary>
            /// 冻结更新的实时记录顺序
            /// </summary>
            public const int Real_IsFreeze = 8;

            /// <summary>
            /// 制作时间的数字标识
            /// </summary>
            public const byte AddDate = 11;
            
            /// <summary>
            /// 制作时间的实时记录顺序
            /// </summary>
            public const int Real_AddDate = 9;

            /// <summary>
            /// 制作人的数字标识
            /// </summary>
            public const byte AuthorId = 12;
            
            /// <summary>
            /// 制作人的实时记录顺序
            /// </summary>
            public const int Real_AuthorId = 10;

            /// <summary>
            /// 最后修改者的数字标识
            /// </summary>
            public const byte LastReviserId = 13;
            
            /// <summary>
            /// 最后修改者的实时记录顺序
            /// </summary>
            public const int Real_LastReviserId = 11;

            /// <summary>
            /// 最后修改日期的数字标识
            /// </summary>
            public const byte LastModifyDate = 14;
            
            /// <summary>
            /// 最后修改日期的实时记录顺序
            /// </summary>
            public const int Real_LastModifyDate = 12;

            /// <summary>
            /// 数据状态的数字标识
            /// </summary>
            public const byte DataState = 15;
            
            /// <summary>
            /// 数据状态的实时记录顺序
            /// </summary>
            public const int Real_DataState = 13;

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
                            Title        = "标识",
                            Caption      = @"标识",
                            Description  = @"标识",
                            ColumnName   = "id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AppType,
                        new PropertySturct
                        {
                            Index        = AppType,
                            Name         = "AppType",
                            Title        = "应用类型",
                            Caption      = @"应用类型",
                            Description  = @"应用类型",
                            ColumnName   = "app_type",
                            PropertyType = typeof(AppType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ShortName,
                        new PropertySturct
                        {
                            Index        = ShortName,
                            Name         = "ShortName",
                            Title        = "应用简称",
                            Caption      = @"应用简称",
                            Description  = @"应用简称",
                            ColumnName   = "short_name",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_FullName,
                        new PropertySturct
                        {
                            Index        = FullName,
                            Name         = "FullName",
                            Title        = "应用全称",
                            Caption      = @"应用全称",
                            Description  = @"应用全称",
                            ColumnName   = "full_name",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AppId,
                        new PropertySturct
                        {
                            Index        = AppId,
                            Name         = "AppId",
                            Title        = "应用标识",
                            Caption      = @"应用标识",
                            Description  = @"应用标识",
                            ColumnName   = "app_id",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AppKey,
                        new PropertySturct
                        {
                            Index        = AppKey,
                            Name         = "AppKey",
                            Title        = "应用令牌",
                            Caption      = @"应用令牌",
                            Description  = @"应用令牌",
                            ColumnName   = "app_key",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Memo,
                        new PropertySturct
                        {
                            Index        = Memo,
                            Name         = "Memo",
                            Title        = "备注",
                            Caption      = @"备注",
                            Description  = @"备注",
                            ColumnName   = "memo",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_DataStateType,
                        new PropertySturct
                        {
                            Index        = DataStateType,
                            Name         = "DataStateType",
                            Title        = "数据状态枚举类型",
                            Caption      = @"数据状态枚举类型",
                            Description  = @"数据状态枚举类型",
                            ColumnName   = "data_state_type",
                            PropertyType = typeof(DataStateType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Object,
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
                    }
                }
            };
        }
        #endregion

    }
}