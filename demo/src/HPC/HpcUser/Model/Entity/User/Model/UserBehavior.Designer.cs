/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:52*/
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


#endregion

namespace HPC.Projects
{
    /// <summary>
    /// 用户行为
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class UserBehaviorData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserBehaviorData()
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
        public void ChangePrimaryKey(long bID)
        {
            _bID = bID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _bID;

        partial void OnBIDGet();

        partial void OnBIDSet(ref long value);

        partial void OnBIDLoad(ref long value);

        partial void OnBIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("BID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long BID
        {
            get
            {
                OnBIDGet();
                return this._bID;
            }
            set
            {
                if(this._bID == value)
                    return;
                //if(this._bID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnBIDSet(ref value);
                this._bID = value;
                this.OnPropertyChanged(_DataStruct_.Real_BID);
                OnBIDSeted();
            }
        }
        /// <summary>
        /// behavior用户标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _behaviorUID;

        partial void OnBehaviorUIDGet();

        partial void OnBehaviorUIDSet(ref long value);

        partial void OnBehaviorUIDSeted();

        
        /// <summary>
        /// behavior用户标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("BehaviorUID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"behavior用户标识")]
        public  long BehaviorUID
        {
            get
            {
                OnBehaviorUIDGet();
                return this._behaviorUID;
            }
            set
            {
                if(this._behaviorUID == value)
                    return;
                OnBehaviorUIDSet(ref value);
                this._behaviorUID = value;
                OnBehaviorUIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_BehaviorUID);
            }
        }
        /// <summary>
        /// 行为名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _behaviorName;

        partial void OnBehaviorNameGet();

        partial void OnBehaviorNameSet(ref string value);

        partial void OnBehaviorNameSeted();

        
        /// <summary>
        /// 行为名称
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("BehaviorName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"行为名称")]
        public  string BehaviorName
        {
            get
            {
                OnBehaviorNameGet();
                return this._behaviorName;
            }
            set
            {
                if(this._behaviorName == value)
                    return;
                OnBehaviorNameSet(ref value);
                this._behaviorName = value;
                OnBehaviorNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_BehaviorName);
            }
        }
        /// <summary>
        /// 行为时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _behaviorTime;

        partial void OnBehaviorTimeGet();

        partial void OnBehaviorTimeSet(ref DateTime value);

        partial void OnBehaviorTimeSeted();

        
        /// <summary>
        /// 行为时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("BehaviorTime", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"行为时间")]
        public  DateTime BehaviorTime
        {
            get
            {
                OnBehaviorTimeGet();
                return this._behaviorTime;
            }
            set
            {
                if(this._behaviorTime == value)
                    return;
                OnBehaviorTimeSet(ref value);
                this._behaviorTime = value;
                OnBehaviorTimeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_BehaviorTime);
            }
        }
        /// <summary>
        /// MP-RES信息
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _mPResInfo;

        partial void OnMPResInfoGet();

        partial void OnMPResInfoSet(ref string value);

        partial void OnMPResInfoSeted();

        
        /// <summary>
        /// MP-RES信息
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MPResInfo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"MP-RES信息")]
        public  string MPResInfo
        {
            get
            {
                OnMPResInfoGet();
                return this._mPResInfo;
            }
            set
            {
                if(this._mPResInfo == value)
                    return;
                OnMPResInfoSet(ref value);
                this._mPResInfo = value;
                OnMPResInfoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MPResInfo);
            }
        }
        /// <summary>
        /// 伊尔莱斯信息
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _yLResInfo;

        partial void OnYLResInfoGet();

        partial void OnYLResInfoSet(ref string value);

        partial void OnYLResInfoSeted();

        
        /// <summary>
        /// 伊尔莱斯信息
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("YLResInfo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"伊尔莱斯信息")]
        public  string YLResInfo
        {
            get
            {
                OnYLResInfoGet();
                return this._yLResInfo;
            }
            set
            {
                if(this._yLResInfo == value)
                    return;
                OnYLResInfoSet(ref value);
                this._yLResInfo = value;
                OnYLResInfoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_YLResInfo);
            }
        }
        /// <summary>
        /// 应用程序启动路径
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _appLaunchPath;

        partial void OnAppLaunchPathGet();

        partial void OnAppLaunchPathSet(ref string value);

        partial void OnAppLaunchPathSeted();

        
        /// <summary>
        /// 应用程序启动路径
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("AppLaunchPath", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用程序启动路径")]
        public  string AppLaunchPath
        {
            get
            {
                OnAppLaunchPathGet();
                return this._appLaunchPath;
            }
            set
            {
                if(this._appLaunchPath == value)
                    return;
                OnAppLaunchPathSet(ref value);
                this._appLaunchPath = value;
                OnAppLaunchPathSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AppLaunchPath);
            }
        }
        /// <summary>
        /// 应用程序启动查询
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _appLaunchQuery;

        partial void OnAppLaunchQueryGet();

        partial void OnAppLaunchQuerySet(ref string value);

        partial void OnAppLaunchQuerySeted();

        
        /// <summary>
        /// 应用程序启动查询
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("AppLaunchQuery", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用程序启动查询")]
        public  string AppLaunchQuery
        {
            get
            {
                OnAppLaunchQueryGet();
                return this._appLaunchQuery;
            }
            set
            {
                if(this._appLaunchQuery == value)
                    return;
                OnAppLaunchQuerySet(ref value);
                this._appLaunchQuery = value;
                OnAppLaunchQuerySeted();
                this.OnPropertyChanged(_DataStruct_.Real_AppLaunchQuery);
            }
        }
        /// <summary>
        /// 应用程序启动查询场景
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _appLaunchQueryScene;

        partial void OnAppLaunchQuerySceneGet();

        partial void OnAppLaunchQuerySceneSet(ref string value);

        partial void OnAppLaunchQuerySceneSeted();

        
        /// <summary>
        /// 应用程序启动查询场景
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("AppLaunchQueryScene", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用程序启动查询场景")]
        public  string AppLaunchQueryScene
        {
            get
            {
                OnAppLaunchQuerySceneGet();
                return this._appLaunchQueryScene;
            }
            set
            {
                if(this._appLaunchQueryScene == value)
                    return;
                OnAppLaunchQuerySceneSet(ref value);
                this._appLaunchQueryScene = value;
                OnAppLaunchQuerySceneSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AppLaunchQueryScene);
            }
        }
        /// <summary>
        /// 应用程序启动场景
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _appLaunchScene;

        partial void OnAppLaunchSceneGet();

        partial void OnAppLaunchSceneSet(ref string value);

        partial void OnAppLaunchSceneSeted();

        
        /// <summary>
        /// 应用程序启动场景
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("AppLaunchScene", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用程序启动场景")]
        public  string AppLaunchScene
        {
            get
            {
                OnAppLaunchSceneGet();
                return this._appLaunchScene;
            }
            set
            {
                if(this._appLaunchScene == value)
                    return;
                OnAppLaunchSceneSet(ref value);
                this._appLaunchScene = value;
                OnAppLaunchSceneSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AppLaunchScene);
            }
        }
        /// <summary>
        /// 应用程序发布共享票
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _appLaunchShareTicket;

        partial void OnAppLaunchShareTicketGet();

        partial void OnAppLaunchShareTicketSet(ref string value);

        partial void OnAppLaunchShareTicketSeted();

        
        /// <summary>
        /// 应用程序发布共享票
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("AppLaunchShareTicket", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用程序发布共享票")]
        public  string AppLaunchShareTicket
        {
            get
            {
                OnAppLaunchShareTicketGet();
                return this._appLaunchShareTicket;
            }
            set
            {
                if(this._appLaunchShareTicket == value)
                    return;
                OnAppLaunchShareTicketSet(ref value);
                this._appLaunchShareTicket = value;
                OnAppLaunchShareTicketSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AppLaunchShareTicket);
            }
        }
        /// <summary>
        /// applaunchrefererinfoapp标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _appLaunchReferrerInfoAppId;

        partial void OnAppLaunchReferrerInfoAppIdGet();

        partial void OnAppLaunchReferrerInfoAppIdSet(ref string value);

        partial void OnAppLaunchReferrerInfoAppIdSeted();

        
        /// <summary>
        /// applaunchrefererinfoapp标识
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("AppLaunchReferrerInfoAppId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"applaunchrefererinfoapp标识")]
        public  string AppLaunchReferrerInfoAppId
        {
            get
            {
                OnAppLaunchReferrerInfoAppIdGet();
                return this._appLaunchReferrerInfoAppId;
            }
            set
            {
                if(this._appLaunchReferrerInfoAppId == value)
                    return;
                OnAppLaunchReferrerInfoAppIdSet(ref value);
                this._appLaunchReferrerInfoAppId = value;
                OnAppLaunchReferrerInfoAppIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AppLaunchReferrerInfoAppId);
            }
        }
        /// <summary>
        /// 应用程序启动引用信息额外数据
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _appLaunchReferrerInfoExtraData;

        partial void OnAppLaunchReferrerInfoExtraDataGet();

        partial void OnAppLaunchReferrerInfoExtraDataSet(ref string value);

        partial void OnAppLaunchReferrerInfoExtraDataSeted();

        
        /// <summary>
        /// 应用程序启动引用信息额外数据
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("AppLaunchReferrerInfoExtraData", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用程序启动引用信息额外数据")]
        public  string AppLaunchReferrerInfoExtraData
        {
            get
            {
                OnAppLaunchReferrerInfoExtraDataGet();
                return this._appLaunchReferrerInfoExtraData;
            }
            set
            {
                if(this._appLaunchReferrerInfoExtraData == value)
                    return;
                OnAppLaunchReferrerInfoExtraDataSet(ref value);
                this._appLaunchReferrerInfoExtraData = value;
                OnAppLaunchReferrerInfoExtraDataSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AppLaunchReferrerInfoExtraData);
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
                return this.BID;
            }
            set
            {
                this.BID = value;
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
        protected override bool SetValueInner(string property, string value)
        {
            if(property == null) return false;
            switch(property.Trim().ToLower())
            {
            case "bid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.BID = vl;
                        return true;
                    }
                }
                return false;
            case "behavioruid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.BehaviorUID = vl;
                        return true;
                    }
                }
                return false;
            case "behaviorname":
                this.BehaviorName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "behaviortime":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.BehaviorTime = vl;
                        return true;
                    }
                }
                return false;
            case "mpresinfo":
                this.MPResInfo = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "ylresinfo":
                this.YLResInfo = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "applaunchpath":
                this.AppLaunchPath = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "applaunchquery":
                this.AppLaunchQuery = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "applaunchqueryscene":
                this.AppLaunchQueryScene = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "applaunchscene":
                this.AppLaunchScene = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "applaunchshareticket":
                this.AppLaunchShareTicket = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "applaunchreferrerinfoappid":
                this.AppLaunchReferrerInfoAppId = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "applaunchreferrerinfoextradata":
                this.AppLaunchReferrerInfoExtraData = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
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
            case "bid":
                this.BID = (long)Convert.ToDecimal(value);
                return;
            case "behavioruid":
                this.BehaviorUID = (long)Convert.ToDecimal(value);
                return;
            case "behaviorname":
                this.BehaviorName = value == null ? null : value.ToString();
                return;
            case "behaviortime":
                this.BehaviorTime = Convert.ToDateTime(value);
                return;
            case "mpresinfo":
                this.MPResInfo = value == null ? null : value.ToString();
                return;
            case "ylresinfo":
                this.YLResInfo = value == null ? null : value.ToString();
                return;
            case "applaunchpath":
                this.AppLaunchPath = value == null ? null : value.ToString();
                return;
            case "applaunchquery":
                this.AppLaunchQuery = value == null ? null : value.ToString();
                return;
            case "applaunchqueryscene":
                this.AppLaunchQueryScene = value == null ? null : value.ToString();
                return;
            case "applaunchscene":
                this.AppLaunchScene = value == null ? null : value.ToString();
                return;
            case "applaunchshareticket":
                this.AppLaunchShareTicket = value == null ? null : value.ToString();
                return;
            case "applaunchreferrerinfoappid":
                this.AppLaunchReferrerInfoAppId = value == null ? null : value.ToString();
                return;
            case "applaunchreferrerinfoextradata":
                this.AppLaunchReferrerInfoExtraData = value == null ? null : value.ToString();
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
            case _DataStruct_.BID:
                this.BID = Convert.ToInt64(value);
                return;
            case _DataStruct_.BehaviorUID:
                this.BehaviorUID = Convert.ToInt64(value);
                return;
            case _DataStruct_.BehaviorName:
                this.BehaviorName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.BehaviorTime:
                this.BehaviorTime = Convert.ToDateTime(value);
                return;
            case _DataStruct_.MPResInfo:
                this.MPResInfo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.YLResInfo:
                this.YLResInfo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AppLaunchPath:
                this.AppLaunchPath = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AppLaunchQuery:
                this.AppLaunchQuery = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AppLaunchQueryScene:
                this.AppLaunchQueryScene = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AppLaunchScene:
                this.AppLaunchScene = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AppLaunchShareTicket:
                this.AppLaunchShareTicket = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AppLaunchReferrerInfoAppId:
                this.AppLaunchReferrerInfoAppId = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AppLaunchReferrerInfoExtraData:
                this.AppLaunchReferrerInfoExtraData = value == null ? null : value.ToString();
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
            case "bid":
                return this.BID;
            case "behavioruid":
                return this.BehaviorUID;
            case "behaviorname":
                return this.BehaviorName;
            case "behaviortime":
                return this.BehaviorTime;
            case "mpresinfo":
                return this.MPResInfo;
            case "ylresinfo":
                return this.YLResInfo;
            case "applaunchpath":
                return this.AppLaunchPath;
            case "applaunchquery":
                return this.AppLaunchQuery;
            case "applaunchqueryscene":
                return this.AppLaunchQueryScene;
            case "applaunchscene":
                return this.AppLaunchScene;
            case "applaunchshareticket":
                return this.AppLaunchShareTicket;
            case "applaunchreferrerinfoappid":
                return this.AppLaunchReferrerInfoAppId;
            case "applaunchreferrerinfoextradata":
                return this.AppLaunchReferrerInfoExtraData;
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
                case _DataStruct_.BID:
                    return this.BID;
                case _DataStruct_.BehaviorUID:
                    return this.BehaviorUID;
                case _DataStruct_.BehaviorName:
                    return this.BehaviorName;
                case _DataStruct_.BehaviorTime:
                    return this.BehaviorTime;
                case _DataStruct_.MPResInfo:
                    return this.MPResInfo;
                case _DataStruct_.YLResInfo:
                    return this.YLResInfo;
                case _DataStruct_.AppLaunchPath:
                    return this.AppLaunchPath;
                case _DataStruct_.AppLaunchQuery:
                    return this.AppLaunchQuery;
                case _DataStruct_.AppLaunchQueryScene:
                    return this.AppLaunchQueryScene;
                case _DataStruct_.AppLaunchScene:
                    return this.AppLaunchScene;
                case _DataStruct_.AppLaunchShareTicket:
                    return this.AppLaunchShareTicket;
                case _DataStruct_.AppLaunchReferrerInfoAppId:
                    return this.AppLaunchReferrerInfoAppId;
                case _DataStruct_.AppLaunchReferrerInfoExtraData:
                    return this.AppLaunchReferrerInfoExtraData;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(UserBehaviorData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as UserBehaviorData;
            if(sourceEntity == null)
                return;
            this._bID = sourceEntity._bID;
            this._behaviorUID = sourceEntity._behaviorUID;
            this._behaviorName = sourceEntity._behaviorName;
            this._behaviorTime = sourceEntity._behaviorTime;
            this._mPResInfo = sourceEntity._mPResInfo;
            this._yLResInfo = sourceEntity._yLResInfo;
            this._appLaunchPath = sourceEntity._appLaunchPath;
            this._appLaunchQuery = sourceEntity._appLaunchQuery;
            this._appLaunchQueryScene = sourceEntity._appLaunchQueryScene;
            this._appLaunchScene = sourceEntity._appLaunchScene;
            this._appLaunchShareTicket = sourceEntity._appLaunchShareTicket;
            this._appLaunchReferrerInfoAppId = sourceEntity._appLaunchReferrerInfoAppId;
            this._appLaunchReferrerInfoExtraData = sourceEntity._appLaunchReferrerInfoExtraData;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(UserBehaviorData source)
        {
                this.BID = source.BID;
                this.BehaviorUID = source.BehaviorUID;
                this.BehaviorName = source.BehaviorName;
                this.BehaviorTime = source.BehaviorTime;
                this.MPResInfo = source.MPResInfo;
                this.YLResInfo = source.YLResInfo;
                this.AppLaunchPath = source.AppLaunchPath;
                this.AppLaunchQuery = source.AppLaunchQuery;
                this.AppLaunchQueryScene = source.AppLaunchQueryScene;
                this.AppLaunchScene = source.AppLaunchScene;
                this.AppLaunchShareTicket = source.AppLaunchShareTicket;
                this.AppLaunchReferrerInfoAppId = source.AppLaunchReferrerInfoAppId;
                this.AppLaunchReferrerInfoExtraData = source.AppLaunchReferrerInfoExtraData;
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
                OnBIDModified(subsist,false);
                OnBehaviorUIDModified(subsist,false);
                OnBehaviorNameModified(subsist,false);
                OnBehaviorTimeModified(subsist,false);
                OnMPResInfoModified(subsist,false);
                OnYLResInfoModified(subsist,false);
                OnAppLaunchPathModified(subsist,false);
                OnAppLaunchQueryModified(subsist,false);
                OnAppLaunchQuerySceneModified(subsist,false);
                OnAppLaunchSceneModified(subsist,false);
                OnAppLaunchShareTicketModified(subsist,false);
                OnAppLaunchReferrerInfoAppIdModified(subsist,false);
                OnAppLaunchReferrerInfoExtraDataModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnBIDModified(subsist,true);
                OnBehaviorUIDModified(subsist,true);
                OnBehaviorNameModified(subsist,true);
                OnBehaviorTimeModified(subsist,true);
                OnMPResInfoModified(subsist,true);
                OnYLResInfoModified(subsist,true);
                OnAppLaunchPathModified(subsist,true);
                OnAppLaunchQueryModified(subsist,true);
                OnAppLaunchQuerySceneModified(subsist,true);
                OnAppLaunchSceneModified(subsist,true);
                OnAppLaunchShareTicketModified(subsist,true);
                OnAppLaunchReferrerInfoAppIdModified(subsist,true);
                OnAppLaunchReferrerInfoExtraDataModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[13] > 0)
            {
                OnBIDModified(subsist,modifieds[_DataStruct_.Real_BID] == 1);
                OnBehaviorUIDModified(subsist,modifieds[_DataStruct_.Real_BehaviorUID] == 1);
                OnBehaviorNameModified(subsist,modifieds[_DataStruct_.Real_BehaviorName] == 1);
                OnBehaviorTimeModified(subsist,modifieds[_DataStruct_.Real_BehaviorTime] == 1);
                OnMPResInfoModified(subsist,modifieds[_DataStruct_.Real_MPResInfo] == 1);
                OnYLResInfoModified(subsist,modifieds[_DataStruct_.Real_YLResInfo] == 1);
                OnAppLaunchPathModified(subsist,modifieds[_DataStruct_.Real_AppLaunchPath] == 1);
                OnAppLaunchQueryModified(subsist,modifieds[_DataStruct_.Real_AppLaunchQuery] == 1);
                OnAppLaunchQuerySceneModified(subsist,modifieds[_DataStruct_.Real_AppLaunchQueryScene] == 1);
                OnAppLaunchSceneModified(subsist,modifieds[_DataStruct_.Real_AppLaunchScene] == 1);
                OnAppLaunchShareTicketModified(subsist,modifieds[_DataStruct_.Real_AppLaunchShareTicket] == 1);
                OnAppLaunchReferrerInfoAppIdModified(subsist,modifieds[_DataStruct_.Real_AppLaunchReferrerInfoAppId] == 1);
                OnAppLaunchReferrerInfoExtraDataModified(subsist,modifieds[_DataStruct_.Real_AppLaunchReferrerInfoExtraData] == 1);
            }
        }

        /// <summary>
        /// 主键修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// behavior用户标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBehaviorUIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 行为名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBehaviorNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 行为时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBehaviorTimeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// MP-RES信息修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMPResInfoModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 伊尔莱斯信息修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnYLResInfoModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 应用程序启动路径修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppLaunchPathModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 应用程序启动查询修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppLaunchQueryModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 应用程序启动查询场景修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppLaunchQuerySceneModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 应用程序启动场景修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppLaunchSceneModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 应用程序发布共享票修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppLaunchShareTicketModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// applaunchrefererinfoapp标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppLaunchReferrerInfoAppIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 应用程序启动引用信息额外数据修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppLaunchReferrerInfoExtraDataModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"UserBehavior";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户行为";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户行为";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "BID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte BID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_BID = 0;

            /// <summary>
            /// behavior用户标识的数字标识
            /// </summary>
            public const byte BehaviorUID = 2;
            
            /// <summary>
            /// behavior用户标识的实时记录顺序
            /// </summary>
            public const int Real_BehaviorUID = 1;

            /// <summary>
            /// 行为名称的数字标识
            /// </summary>
            public const byte BehaviorName = 3;
            
            /// <summary>
            /// 行为名称的实时记录顺序
            /// </summary>
            public const int Real_BehaviorName = 2;

            /// <summary>
            /// 行为时间的数字标识
            /// </summary>
            public const byte BehaviorTime = 4;
            
            /// <summary>
            /// 行为时间的实时记录顺序
            /// </summary>
            public const int Real_BehaviorTime = 3;

            /// <summary>
            /// MP-RES信息的数字标识
            /// </summary>
            public const byte MPResInfo = 5;
            
            /// <summary>
            /// MP-RES信息的实时记录顺序
            /// </summary>
            public const int Real_MPResInfo = 4;

            /// <summary>
            /// 伊尔莱斯信息的数字标识
            /// </summary>
            public const byte YLResInfo = 6;
            
            /// <summary>
            /// 伊尔莱斯信息的实时记录顺序
            /// </summary>
            public const int Real_YLResInfo = 5;

            /// <summary>
            /// 应用程序启动路径的数字标识
            /// </summary>
            public const byte AppLaunchPath = 7;
            
            /// <summary>
            /// 应用程序启动路径的实时记录顺序
            /// </summary>
            public const int Real_AppLaunchPath = 6;

            /// <summary>
            /// 应用程序启动查询的数字标识
            /// </summary>
            public const byte AppLaunchQuery = 8;
            
            /// <summary>
            /// 应用程序启动查询的实时记录顺序
            /// </summary>
            public const int Real_AppLaunchQuery = 7;

            /// <summary>
            /// 应用程序启动查询场景的数字标识
            /// </summary>
            public const byte AppLaunchQueryScene = 9;
            
            /// <summary>
            /// 应用程序启动查询场景的实时记录顺序
            /// </summary>
            public const int Real_AppLaunchQueryScene = 8;

            /// <summary>
            /// 应用程序启动场景的数字标识
            /// </summary>
            public const byte AppLaunchScene = 10;
            
            /// <summary>
            /// 应用程序启动场景的实时记录顺序
            /// </summary>
            public const int Real_AppLaunchScene = 9;

            /// <summary>
            /// 应用程序发布共享票的数字标识
            /// </summary>
            public const byte AppLaunchShareTicket = 11;
            
            /// <summary>
            /// 应用程序发布共享票的实时记录顺序
            /// </summary>
            public const int Real_AppLaunchShareTicket = 10;

            /// <summary>
            /// applaunchrefererinfoapp标识的数字标识
            /// </summary>
            public const byte AppLaunchReferrerInfoAppId = 12;
            
            /// <summary>
            /// applaunchrefererinfoapp标识的实时记录顺序
            /// </summary>
            public const int Real_AppLaunchReferrerInfoAppId = 11;

            /// <summary>
            /// 应用程序启动引用信息额外数据的数字标识
            /// </summary>
            public const byte AppLaunchReferrerInfoExtraData = 13;
            
            /// <summary>
            /// 应用程序启动引用信息额外数据的实时记录顺序
            /// </summary>
            public const int Real_AppLaunchReferrerInfoExtraData = 12;

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
                        Real_BID,
                        new PropertySturct
                        {
                            Index        = BID,
                            Name         = "BID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "BID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_BehaviorUID,
                        new PropertySturct
                        {
                            Index        = BehaviorUID,
                            Name         = "BehaviorUID",
                            Title        = "behavior用户标识",
                            Caption      = @"behavior用户标识",
                            Description  = @"behavior用户标识",
                            ColumnName   = "BehaviorUID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_BehaviorName,
                        new PropertySturct
                        {
                            Index        = BehaviorName,
                            Name         = "BehaviorName",
                            Title        = "行为名称",
                            Caption      = @"行为名称",
                            Description  = @"行为名称",
                            ColumnName   = "BehaviorName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_BehaviorTime,
                        new PropertySturct
                        {
                            Index        = BehaviorTime,
                            Name         = "BehaviorTime",
                            Title        = "行为时间",
                            Caption      = @"行为时间",
                            Description  = @"行为时间",
                            ColumnName   = "BehaviorTime",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MPResInfo,
                        new PropertySturct
                        {
                            Index        = MPResInfo,
                            Name         = "MPResInfo",
                            Title        = "MP-RES信息",
                            Caption      = @"MP-RES信息",
                            Description  = @"MP-RES信息",
                            ColumnName   = "MPResInfo",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_YLResInfo,
                        new PropertySturct
                        {
                            Index        = YLResInfo,
                            Name         = "YLResInfo",
                            Title        = "伊尔莱斯信息",
                            Caption      = @"伊尔莱斯信息",
                            Description  = @"伊尔莱斯信息",
                            ColumnName   = "YLResInfo",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AppLaunchPath,
                        new PropertySturct
                        {
                            Index        = AppLaunchPath,
                            Name         = "AppLaunchPath",
                            Title        = "应用程序启动路径",
                            Caption      = @"应用程序启动路径",
                            Description  = @"应用程序启动路径",
                            ColumnName   = "AppLaunchPath",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AppLaunchQuery,
                        new PropertySturct
                        {
                            Index        = AppLaunchQuery,
                            Name         = "AppLaunchQuery",
                            Title        = "应用程序启动查询",
                            Caption      = @"应用程序启动查询",
                            Description  = @"应用程序启动查询",
                            ColumnName   = "AppLaunchQuery",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AppLaunchQueryScene,
                        new PropertySturct
                        {
                            Index        = AppLaunchQueryScene,
                            Name         = "AppLaunchQueryScene",
                            Title        = "应用程序启动查询场景",
                            Caption      = @"应用程序启动查询场景",
                            Description  = @"应用程序启动查询场景",
                            ColumnName   = "AppLaunchQueryScene",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AppLaunchScene,
                        new PropertySturct
                        {
                            Index        = AppLaunchScene,
                            Name         = "AppLaunchScene",
                            Title        = "应用程序启动场景",
                            Caption      = @"应用程序启动场景",
                            Description  = @"应用程序启动场景",
                            ColumnName   = "AppLaunchScene",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AppLaunchShareTicket,
                        new PropertySturct
                        {
                            Index        = AppLaunchShareTicket,
                            Name         = "AppLaunchShareTicket",
                            Title        = "应用程序发布共享票",
                            Caption      = @"应用程序发布共享票",
                            Description  = @"应用程序发布共享票",
                            ColumnName   = "AppLaunchShareTicket",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AppLaunchReferrerInfoAppId,
                        new PropertySturct
                        {
                            Index        = AppLaunchReferrerInfoAppId,
                            Name         = "AppLaunchReferrerInfoAppId",
                            Title        = "applaunchrefererinfoapp标识",
                            Caption      = @"applaunchrefererinfoapp标识",
                            Description  = @"applaunchrefererinfoapp标识",
                            ColumnName   = "AppLaunchReferrerInfoAppId",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AppLaunchReferrerInfoExtraData,
                        new PropertySturct
                        {
                            Index        = AppLaunchReferrerInfoExtraData,
                            Name         = "AppLaunchReferrerInfoExtraData",
                            Title        = "应用程序启动引用信息额外数据",
                            Caption      = @"应用程序启动引用信息额外数据",
                            Description  = @"应用程序启动引用信息额外数据",
                            ColumnName   = "AppLaunchReferrerInfoExtraData",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
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