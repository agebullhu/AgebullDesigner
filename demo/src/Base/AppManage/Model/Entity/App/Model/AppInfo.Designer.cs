/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 17:26:28*/
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

using Agebull.Common.OAuth;
using Agebull.EntityModel.Interfaces;
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
        [DataRule(CanNull = true)]
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
        /// 组织标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _orgId;

        partial void OnOrgIdGet();

        partial void OnOrgIdSet(ref long value);

        partial void OnOrgIdSeted();

        
        /// <summary>
        /// 组织标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("orgId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织标识")]
        public  long OrgId
        {
            get
            {
                OnOrgIdGet();
                return this._orgId;
            }
            set
            {
                if(this._orgId == value)
                    return;
                OnOrgIdSet(ref value);
                this._orgId = value;
                OnOrgIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgId);
            }
        }
        /// <summary>
        /// 机构
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _organization;

        
        /// <summary>
        /// 机构
        /// </summary>
        /// <remarks>
        /// 短名称
        /// </remarks>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("organization", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"机构")]
        public  string Organization
        {
            get
            {
                return this._organization;
            }
            set
            {
                this._organization = value;
            }
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
        /// 应用类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public ClassifyType _classify;

        partial void OnClassifyGet();

        partial void OnClassifySet(ref ClassifyType value);

        partial void OnClassifySeted();

        
        /// <summary>
        /// 应用类型
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("classify", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用类型")]
        public  ClassifyType Classify
        {
            get
            {
                OnClassifyGet();
                return this._classify;
            }
            set
            {
                if(this._classify == value)
                    return;
                OnClassifySet(ref value);
                this._classify = value;
                OnClassifySeted();
                this.OnPropertyChanged(_DataStruct_.Real_Classify);
            }
        }
        /// <summary>
        /// 应用类型的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("应用类型")]
        public string Classify_Content => Classify.ToCaption();

        /// <summary>
        /// 应用类型的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int Classify_Number
        {
            get => (int)this.Classify;
            set => this.Classify = (ClassifyType)value;
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
        /// 注册管理机构代码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _managOrgcode;

        partial void OnManagOrgcodeGet();

        partial void OnManagOrgcodeSet(ref string value);

        partial void OnManagOrgcodeSeted();

        
        /// <summary>
        /// 注册管理机构代码
        /// </summary>
        /// <value>
        /// 可存储10个字符.合理长度应不大于10.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("managOrgcode", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"注册管理机构代码")]
        public  string ManagOrgcode
        {
            get
            {
                OnManagOrgcodeGet();
                return this._managOrgcode;
            }
            set
            {
                if(this._managOrgcode == value)
                    return;
                OnManagOrgcodeSet(ref value);
                this._managOrgcode = value;
                OnManagOrgcodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ManagOrgcode);
            }
        }
        /// <summary>
        /// 注册管理机构名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _managOrgname;

        partial void OnManagOrgnameGet();

        partial void OnManagOrgnameSet(ref string value);

        partial void OnManagOrgnameSeted();

        
        /// <summary>
        /// 注册管理机构名称
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("managOrgname", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"注册管理机构名称")]
        public  string ManagOrgname
        {
            get
            {
                OnManagOrgnameGet();
                return this._managOrgname;
            }
            set
            {
                if(this._managOrgname == value)
                    return;
                OnManagOrgnameSet(ref value);
                this._managOrgname = value;
                OnManagOrgnameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ManagOrgname);
            }
        }
        /// <summary>
        /// 所在市级编码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _cityCode;

        partial void OnCityCodeGet();

        partial void OnCityCodeSet(ref string value);

        partial void OnCityCodeSeted();

        
        /// <summary>
        /// 所在市级编码
        /// </summary>
        /// <remarks>
        /// 参照数据字典【行政区划】,允许更新
        /// </remarks>
        /// <value>
        /// 可存储6个字符.合理长度应不大于6.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("cityCode", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"所在市级编码")]
        public  string CityCode
        {
            get
            {
                OnCityCodeGet();
                return this._cityCode;
            }
            set
            {
                if(this._cityCode == value)
                    return;
                OnCityCodeSet(ref value);
                this._cityCode = value;
                OnCityCodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_CityCode);
            }
        }
        /// <summary>
        /// 所在区县编码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _districtCode;

        partial void OnDistrictCodeGet();

        partial void OnDistrictCodeSet(ref string value);

        partial void OnDistrictCodeSeted();

        
        /// <summary>
        /// 所在区县编码
        /// </summary>
        /// <remarks>
        /// 允许更新
        /// </remarks>
        /// <value>
        /// 可存储6个字符.合理长度应不大于6.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("districtCode", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"所在区县编码")]
        public  string DistrictCode
        {
            get
            {
                OnDistrictCodeGet();
                return this._districtCode;
            }
            set
            {
                if(this._districtCode == value)
                    return;
                OnDistrictCodeSet(ref value);
                this._districtCode = value;
                OnDistrictCodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_DistrictCode);
            }
        }
        /// <summary>
        /// 机构详细地址
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orgAddress;

        partial void OnOrgAddressGet();

        partial void OnOrgAddressSet(ref string value);

        partial void OnOrgAddressSeted();

        
        /// <summary>
        /// 机构详细地址
        /// </summary>
        /// <remarks>
        /// 允许更新
        /// </remarks>
        /// <value>
        /// 可存储128个字符.合理长度应不大于128.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("orgAddress", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"机构详细地址")]
        public  string OrgAddress
        {
            get
            {
                OnOrgAddressGet();
                return this._orgAddress;
            }
            set
            {
                if(this._orgAddress == value)
                    return;
                OnOrgAddressSet(ref value);
                this._orgAddress = value;
                OnOrgAddressSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgAddress);
            }
        }
        /// <summary>
        /// 机构负责人
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _lawPersonname;

        partial void OnLawPersonnameGet();

        partial void OnLawPersonnameSet(ref string value);

        partial void OnLawPersonnameSeted();

        
        /// <summary>
        /// 机构负责人
        /// </summary>
        /// <remarks>
        /// 允许更新
        /// </remarks>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("lawPersonname", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"机构负责人")]
        public  string LawPersonname
        {
            get
            {
                OnLawPersonnameGet();
                return this._lawPersonname;
            }
            set
            {
                if(this._lawPersonname == value)
                    return;
                OnLawPersonnameSet(ref value);
                this._lawPersonname = value;
                OnLawPersonnameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LawPersonname);
            }
        }
        /// <summary>
        /// 机构负责人电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _lawPersontel;

        partial void OnLawPersontelGet();

        partial void OnLawPersontelSet(ref string value);

        partial void OnLawPersontelSeted();

        
        /// <summary>
        /// 机构负责人电话
        /// </summary>
        /// <remarks>
        /// 允许更新
        /// </remarks>
        /// <value>
        /// 可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("lawPersontel", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"机构负责人电话")]
        public  string LawPersontel
        {
            get
            {
                OnLawPersontelGet();
                return this._lawPersontel;
            }
            set
            {
                if(this._lawPersontel == value)
                    return;
                OnLawPersontelSet(ref value);
                this._lawPersontel = value;
                OnLawPersontelSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LawPersontel);
            }
        }
        /// <summary>
        /// 机构联系人
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _contactName;

        partial void OnContactNameGet();

        partial void OnContactNameSet(ref string value);

        partial void OnContactNameSeted();

        
        /// <summary>
        /// 机构联系人
        /// </summary>
        /// <remarks>
        /// 允许更新
        /// </remarks>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("contactName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"机构联系人")]
        public  string ContactName
        {
            get
            {
                OnContactNameGet();
                return this._contactName;
            }
            set
            {
                if(this._contactName == value)
                    return;
                OnContactNameSet(ref value);
                this._contactName = value;
                OnContactNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ContactName);
            }
        }
        /// <summary>
        /// 机构联系人电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _contactTel;

        partial void OnContactTelGet();

        partial void OnContactTelSet(ref string value);

        partial void OnContactTelSeted();

        
        /// <summary>
        /// 机构联系人电话
        /// </summary>
        /// <remarks>
        /// 允许更新
        /// </remarks>
        /// <value>
        /// 可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("contactTel", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"机构联系人电话")]
        public  string ContactTel
        {
            get
            {
                OnContactTelGet();
                return this._contactTel;
            }
            set
            {
                if(this._contactTel == value)
                    return;
                OnContactTelSet(ref value);
                this._contactTel = value;
                OnContactTelSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ContactTel);
            }
        }
        /// <summary>
        /// 上级机构代码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _superOrgcode;

        partial void OnSuperOrgcodeGet();

        partial void OnSuperOrgcodeSet(ref string value);

        partial void OnSuperOrgcodeSeted();

        
        /// <summary>
        /// 上级机构代码
        /// </summary>
        /// <remarks>
        /// 无上级机构时,传当前注册管理机构代码,允许更新
        /// </remarks>
        /// <value>
        /// 可存储10个字符.合理长度应不大于10.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("superOrgcode", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"上级机构代码")]
        public  string SuperOrgcode
        {
            get
            {
                OnSuperOrgcodeGet();
                return this._superOrgcode;
            }
            set
            {
                if(this._superOrgcode == value)
                    return;
                OnSuperOrgcodeSet(ref value);
                this._superOrgcode = value;
                OnSuperOrgcodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SuperOrgcode);
            }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _updateDate;

        partial void OnUpdateDateGet();

        partial void OnUpdateDateSet(ref DateTime value);

        partial void OnUpdateDateSeted();

        
        /// <summary>
        /// 更新时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("updateDate", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"更新时间")]
        public  DateTime UpdateDate
        {
            get
            {
                OnUpdateDateGet();
                return this._updateDate;
            }
            set
            {
                if(this._updateDate == value)
                    return;
                OnUpdateDateSet(ref value);
                this._updateDate = value;
                OnUpdateDateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UpdateDate);
            }
        }
        /// <summary>
        /// 更新操作员工号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _updateUserid;

        partial void OnUpdateUseridGet();

        partial void OnUpdateUseridSet(ref string value);

        partial void OnUpdateUseridSeted();

        
        /// <summary>
        /// 更新操作员工号
        /// </summary>
        /// <value>
        /// 可存储10个字符.合理长度应不大于10.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("updateUserid", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"更新操作员工号")]
        public  string UpdateUserid
        {
            get
            {
                OnUpdateUseridGet();
                return this._updateUserid;
            }
            set
            {
                if(this._updateUserid == value)
                    return;
                OnUpdateUseridSet(ref value);
                this._updateUserid = value;
                OnUpdateUseridSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UpdateUserid);
            }
        }
        /// <summary>
        /// 更新操作员姓名
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _updateUsername;

        partial void OnUpdateUsernameGet();

        partial void OnUpdateUsernameSet(ref string value);

        partial void OnUpdateUsernameSeted();

        
        /// <summary>
        /// 更新操作员姓名
        /// </summary>
        /// <value>
        /// 可存储10个字符.合理长度应不大于10.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("updateUsername", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"更新操作员姓名")]
        public  string UpdateUsername
        {
            get
            {
                OnUpdateUsernameGet();
                return this._updateUsername;
            }
            set
            {
                if(this._updateUsername == value)
                    return;
                OnUpdateUsernameSet(ref value);
                this._updateUsername = value;
                OnUpdateUsernameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UpdateUsername);
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
            case "orgid":
                this.OrgId = (long)Convert.ToDecimal(value);
                return;
            case "organization":
                this.Organization = value == null ? null : value.ToString();
                return;
            case "shortname":
                this.ShortName = value == null ? null : value.ToString();
                return;
            case "fullname":
                this.FullName = value == null ? null : value.ToString();
                return;
            case "classify":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.Classify = (ClassifyType)(int)value;
                    }
                    else if(value is ClassifyType)
                    {
                        this.Classify = (ClassifyType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        ClassifyType val;
                        if (ClassifyType.TryParse(str, out val))
                        {
                            this.Classify = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.Classify = (ClassifyType)vl;
                            }
                        }
                    }
                }
                return;
            case "appid":
                this.AppId = value == null ? null : value.ToString();
                return;
            case "managorgcode":
                this.ManagOrgcode = value == null ? null : value.ToString();
                return;
            case "managorgname":
                this.ManagOrgname = value == null ? null : value.ToString();
                return;
            case "citycode":
                this.CityCode = value == null ? null : value.ToString();
                return;
            case "districtcode":
                this.DistrictCode = value == null ? null : value.ToString();
                return;
            case "orgaddress":
                this.OrgAddress = value == null ? null : value.ToString();
                return;
            case "lawpersonname":
                this.LawPersonname = value == null ? null : value.ToString();
                return;
            case "lawpersontel":
                this.LawPersontel = value == null ? null : value.ToString();
                return;
            case "contactname":
                this.ContactName = value == null ? null : value.ToString();
                return;
            case "contacttel":
                this.ContactTel = value == null ? null : value.ToString();
                return;
            case "superorgcode":
                this.SuperOrgcode = value == null ? null : value.ToString();
                return;
            case "updatedate":
                this.UpdateDate = Convert.ToDateTime(value);
                return;
            case "updateuserid":
                this.UpdateUserid = value == null ? null : value.ToString();
                return;
            case "updateusername":
                this.UpdateUsername = value == null ? null : value.ToString();
                return;
            case "memo":
                this.Memo = value == null ? null : value.ToString();
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
            case _DataStruct_.OrgId:
                this.OrgId = Convert.ToInt64(value);
                return;
            case _DataStruct_.Organization:
                this.Organization = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ShortName:
                this.ShortName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.FullName:
                this.FullName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Classify:
                this.Classify = (ClassifyType)value;
                return;
            case _DataStruct_.AppId:
                this.AppId = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ManagOrgcode:
                this.ManagOrgcode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ManagOrgname:
                this.ManagOrgname = value == null ? null : value.ToString();
                return;
            case _DataStruct_.CityCode:
                this.CityCode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.DistrictCode:
                this.DistrictCode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrgAddress:
                this.OrgAddress = value == null ? null : value.ToString();
                return;
            case _DataStruct_.LawPersonname:
                this.LawPersonname = value == null ? null : value.ToString();
                return;
            case _DataStruct_.LawPersontel:
                this.LawPersontel = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ContactName:
                this.ContactName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ContactTel:
                this.ContactTel = value == null ? null : value.ToString();
                return;
            case _DataStruct_.SuperOrgcode:
                this.SuperOrgcode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.UpdateDate:
                this.UpdateDate = Convert.ToDateTime(value);
                return;
            case _DataStruct_.UpdateUserid:
                this.UpdateUserid = value == null ? null : value.ToString();
                return;
            case _DataStruct_.UpdateUsername:
                this.UpdateUsername = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Memo:
                this.Memo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.DataState:
                this.DataState = (DataStateType)value;
                return;
            case _DataStruct_.IsFreeze:
                this.IsFreeze = Convert.ToBoolean(value);
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
            case "orgid":
                return this.OrgId;
            case "organization":
                return this.Organization;
            case "shortname":
                return this.ShortName;
            case "fullname":
                return this.FullName;
            case "classify":
                return this.Classify.ToCaption();
            case "appid":
                return this.AppId;
            case "managorgcode":
                return this.ManagOrgcode;
            case "managorgname":
                return this.ManagOrgname;
            case "citycode":
                return this.CityCode;
            case "districtcode":
                return this.DistrictCode;
            case "orgaddress":
                return this.OrgAddress;
            case "lawpersonname":
                return this.LawPersonname;
            case "lawpersontel":
                return this.LawPersontel;
            case "contactname":
                return this.ContactName;
            case "contacttel":
                return this.ContactTel;
            case "superorgcode":
                return this.SuperOrgcode;
            case "updatedate":
                return this.UpdateDate;
            case "updateuserid":
                return this.UpdateUserid;
            case "updateusername":
                return this.UpdateUsername;
            case "memo":
                return this.Memo;
            case "datastate":
                return this.DataState.ToCaption();
            case "isfreeze":
                return this.IsFreeze;
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
                case _DataStruct_.OrgId:
                    return this.OrgId;
                case _DataStruct_.Organization:
                    return this.Organization;
                case _DataStruct_.ShortName:
                    return this.ShortName;
                case _DataStruct_.FullName:
                    return this.FullName;
                case _DataStruct_.Classify:
                    return this.Classify;
                case _DataStruct_.AppId:
                    return this.AppId;
                case _DataStruct_.ManagOrgcode:
                    return this.ManagOrgcode;
                case _DataStruct_.ManagOrgname:
                    return this.ManagOrgname;
                case _DataStruct_.CityCode:
                    return this.CityCode;
                case _DataStruct_.DistrictCode:
                    return this.DistrictCode;
                case _DataStruct_.OrgAddress:
                    return this.OrgAddress;
                case _DataStruct_.LawPersonname:
                    return this.LawPersonname;
                case _DataStruct_.LawPersontel:
                    return this.LawPersontel;
                case _DataStruct_.ContactName:
                    return this.ContactName;
                case _DataStruct_.ContactTel:
                    return this.ContactTel;
                case _DataStruct_.SuperOrgcode:
                    return this.SuperOrgcode;
                case _DataStruct_.UpdateDate:
                    return this.UpdateDate;
                case _DataStruct_.UpdateUserid:
                    return this.UpdateUserid;
                case _DataStruct_.UpdateUsername:
                    return this.UpdateUsername;
                case _DataStruct_.Memo:
                    return this.Memo;
                case _DataStruct_.DataState:
                    return this.DataState;
                case _DataStruct_.IsFreeze:
                    return this.IsFreeze;
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
            this._orgId = sourceEntity._orgId;
            this._organization = sourceEntity._organization;
            this._shortName = sourceEntity._shortName;
            this._fullName = sourceEntity._fullName;
            this._classify = sourceEntity._classify;
            this._appId = sourceEntity._appId;
            this._managOrgcode = sourceEntity._managOrgcode;
            this._managOrgname = sourceEntity._managOrgname;
            this._cityCode = sourceEntity._cityCode;
            this._districtCode = sourceEntity._districtCode;
            this._orgAddress = sourceEntity._orgAddress;
            this._lawPersonname = sourceEntity._lawPersonname;
            this._lawPersontel = sourceEntity._lawPersontel;
            this._contactName = sourceEntity._contactName;
            this._contactTel = sourceEntity._contactTel;
            this._superOrgcode = sourceEntity._superOrgcode;
            this._updateDate = sourceEntity._updateDate;
            this._updateUserid = sourceEntity._updateUserid;
            this._updateUsername = sourceEntity._updateUsername;
            this._memo = sourceEntity._memo;
            this._dataState = sourceEntity._dataState;
            this._isFreeze = sourceEntity._isFreeze;
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
        public void Copy(AppInfoData source)
        {
                this.Id = source.Id;
                this.OrgId = source.OrgId;
                this.Organization = source.Organization;
                this.ShortName = source.ShortName;
                this.FullName = source.FullName;
                this.Classify = source.Classify;
                this.AppId = source.AppId;
                this.ManagOrgcode = source.ManagOrgcode;
                this.ManagOrgname = source.ManagOrgname;
                this.CityCode = source.CityCode;
                this.DistrictCode = source.DistrictCode;
                this.OrgAddress = source.OrgAddress;
                this.LawPersonname = source.LawPersonname;
                this.LawPersontel = source.LawPersontel;
                this.ContactName = source.ContactName;
                this.ContactTel = source.ContactTel;
                this.SuperOrgcode = source.SuperOrgcode;
                this.UpdateDate = source.UpdateDate;
                this.UpdateUserid = source.UpdateUserid;
                this.UpdateUsername = source.UpdateUsername;
                this.Memo = source.Memo;
                this.DataState = source.DataState;
                this.IsFreeze = source.IsFreeze;
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
                OnOrgIdModified(subsist,false);
                OnOrganizationModified(subsist,false);
                OnShortNameModified(subsist,false);
                OnFullNameModified(subsist,false);
                OnClassifyModified(subsist,false);
                OnAppIdModified(subsist,false);
                OnManagOrgcodeModified(subsist,false);
                OnManagOrgnameModified(subsist,false);
                OnCityCodeModified(subsist,false);
                OnDistrictCodeModified(subsist,false);
                OnOrgAddressModified(subsist,false);
                OnLawPersonnameModified(subsist,false);
                OnLawPersontelModified(subsist,false);
                OnContactNameModified(subsist,false);
                OnContactTelModified(subsist,false);
                OnSuperOrgcodeModified(subsist,false);
                OnUpdateDateModified(subsist,false);
                OnUpdateUseridModified(subsist,false);
                OnUpdateUsernameModified(subsist,false);
                OnMemoModified(subsist,false);
                OnDataStateModified(subsist,false);
                OnIsFreezeModified(subsist,false);
                OnAddDateModified(subsist,false);
                OnLastReviserIdModified(subsist,false);
                OnLastModifyDateModified(subsist,false);
                OnAuthorIdModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIdModified(subsist,true);
                OnOrgIdModified(subsist,true);
                OnOrganizationModified(subsist,true);
                OnShortNameModified(subsist,true);
                OnFullNameModified(subsist,true);
                OnClassifyModified(subsist,true);
                OnAppIdModified(subsist,true);
                OnManagOrgcodeModified(subsist,true);
                OnManagOrgnameModified(subsist,true);
                OnCityCodeModified(subsist,true);
                OnDistrictCodeModified(subsist,true);
                OnOrgAddressModified(subsist,true);
                OnLawPersonnameModified(subsist,true);
                OnLawPersontelModified(subsist,true);
                OnContactNameModified(subsist,true);
                OnContactTelModified(subsist,true);
                OnSuperOrgcodeModified(subsist,true);
                OnUpdateDateModified(subsist,true);
                OnUpdateUseridModified(subsist,true);
                OnUpdateUsernameModified(subsist,true);
                OnMemoModified(subsist,true);
                OnDataStateModified(subsist,true);
                OnIsFreezeModified(subsist,true);
                OnAddDateModified(subsist,true);
                OnLastReviserIdModified(subsist,true);
                OnLastModifyDateModified(subsist,true);
                OnAuthorIdModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[27] > 0)
            {
                OnIdModified(subsist,modifieds[_DataStruct_.Real_Id] == 1);
                OnOrgIdModified(subsist,modifieds[_DataStruct_.Real_OrgId] == 1);
                OnOrganizationModified(subsist,modifieds[_DataStruct_.Real_Organization] == 1);
                OnShortNameModified(subsist,modifieds[_DataStruct_.Real_ShortName] == 1);
                OnFullNameModified(subsist,modifieds[_DataStruct_.Real_FullName] == 1);
                OnClassifyModified(subsist,modifieds[_DataStruct_.Real_Classify] == 1);
                OnAppIdModified(subsist,modifieds[_DataStruct_.Real_AppId] == 1);
                OnManagOrgcodeModified(subsist,modifieds[_DataStruct_.Real_ManagOrgcode] == 1);
                OnManagOrgnameModified(subsist,modifieds[_DataStruct_.Real_ManagOrgname] == 1);
                OnCityCodeModified(subsist,modifieds[_DataStruct_.Real_CityCode] == 1);
                OnDistrictCodeModified(subsist,modifieds[_DataStruct_.Real_DistrictCode] == 1);
                OnOrgAddressModified(subsist,modifieds[_DataStruct_.Real_OrgAddress] == 1);
                OnLawPersonnameModified(subsist,modifieds[_DataStruct_.Real_LawPersonname] == 1);
                OnLawPersontelModified(subsist,modifieds[_DataStruct_.Real_LawPersontel] == 1);
                OnContactNameModified(subsist,modifieds[_DataStruct_.Real_ContactName] == 1);
                OnContactTelModified(subsist,modifieds[_DataStruct_.Real_ContactTel] == 1);
                OnSuperOrgcodeModified(subsist,modifieds[_DataStruct_.Real_SuperOrgcode] == 1);
                OnUpdateDateModified(subsist,modifieds[_DataStruct_.Real_UpdateDate] == 1);
                OnUpdateUseridModified(subsist,modifieds[_DataStruct_.Real_UpdateUserid] == 1);
                OnUpdateUsernameModified(subsist,modifieds[_DataStruct_.Real_UpdateUsername] == 1);
                OnMemoModified(subsist,modifieds[_DataStruct_.Real_Memo] == 1);
                OnDataStateModified(subsist,modifieds[_DataStruct_.Real_DataState] == 1);
                OnIsFreezeModified(subsist,modifieds[_DataStruct_.Real_IsFreeze] == 1);
                OnAddDateModified(subsist,modifieds[_DataStruct_.Real_AddDate] == 1);
                OnLastReviserIdModified(subsist,modifieds[_DataStruct_.Real_LastReviserId] == 1);
                OnLastModifyDateModified(subsist,modifieds[_DataStruct_.Real_LastModifyDate] == 1);
                OnAuthorIdModified(subsist,modifieds[_DataStruct_.Real_AuthorId] == 1);
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
        /// 组织标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 机构修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrganizationModified(EntitySubsist subsist,bool isModified);

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
        /// 应用类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnClassifyModified(EntitySubsist subsist,bool isModified);

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
        /// 注册管理机构代码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnManagOrgcodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 注册管理机构名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnManagOrgnameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 所在市级编码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCityCodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 所在区县编码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDistrictCodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 机构详细地址修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgAddressModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 机构负责人修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLawPersonnameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 机构负责人电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLawPersontelModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 机构联系人修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnContactNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 机构联系人电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnContactTelModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 上级机构代码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSuperOrgcodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 更新时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUpdateDateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 更新操作员工号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUpdateUseridModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 更新操作员姓名修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUpdateUsernameModified(EntitySubsist subsist,bool isModified);

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
        /// 数据状态修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDataStateModified(EntitySubsist subsist,bool isModified);

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
            /// 组织标识的数字标识
            /// </summary>
            public const byte OrgId = 3;
            
            /// <summary>
            /// 组织标识的实时记录顺序
            /// </summary>
            public const int Real_OrgId = 1;

            /// <summary>
            /// 机构的数字标识
            /// </summary>
            public const byte Organization = 4;
            
            /// <summary>
            /// 机构的实时记录顺序
            /// </summary>
            public const int Real_Organization = 2;

            /// <summary>
            /// 应用简称的数字标识
            /// </summary>
            public const byte ShortName = 5;
            
            /// <summary>
            /// 应用简称的实时记录顺序
            /// </summary>
            public const int Real_ShortName = 3;

            /// <summary>
            /// 应用全称的数字标识
            /// </summary>
            public const byte FullName = 6;
            
            /// <summary>
            /// 应用全称的实时记录顺序
            /// </summary>
            public const int Real_FullName = 4;

            /// <summary>
            /// 应用类型的数字标识
            /// </summary>
            public const byte Classify = 7;
            
            /// <summary>
            /// 应用类型的实时记录顺序
            /// </summary>
            public const int Real_Classify = 5;

            /// <summary>
            /// 应用标识的数字标识
            /// </summary>
            public const byte AppId = 8;
            
            /// <summary>
            /// 应用标识的实时记录顺序
            /// </summary>
            public const int Real_AppId = 6;

            /// <summary>
            /// 注册管理机构代码的数字标识
            /// </summary>
            public const byte ManagOrgcode = 9;
            
            /// <summary>
            /// 注册管理机构代码的实时记录顺序
            /// </summary>
            public const int Real_ManagOrgcode = 7;

            /// <summary>
            /// 注册管理机构名称的数字标识
            /// </summary>
            public const byte ManagOrgname = 10;
            
            /// <summary>
            /// 注册管理机构名称的实时记录顺序
            /// </summary>
            public const int Real_ManagOrgname = 8;

            /// <summary>
            /// 所在市级编码的数字标识
            /// </summary>
            public const byte CityCode = 11;
            
            /// <summary>
            /// 所在市级编码的实时记录顺序
            /// </summary>
            public const int Real_CityCode = 9;

            /// <summary>
            /// 所在区县编码的数字标识
            /// </summary>
            public const byte DistrictCode = 12;
            
            /// <summary>
            /// 所在区县编码的实时记录顺序
            /// </summary>
            public const int Real_DistrictCode = 10;

            /// <summary>
            /// 机构详细地址的数字标识
            /// </summary>
            public const byte OrgAddress = 13;
            
            /// <summary>
            /// 机构详细地址的实时记录顺序
            /// </summary>
            public const int Real_OrgAddress = 11;

            /// <summary>
            /// 机构负责人的数字标识
            /// </summary>
            public const byte LawPersonname = 14;
            
            /// <summary>
            /// 机构负责人的实时记录顺序
            /// </summary>
            public const int Real_LawPersonname = 12;

            /// <summary>
            /// 机构负责人电话的数字标识
            /// </summary>
            public const byte LawPersontel = 15;
            
            /// <summary>
            /// 机构负责人电话的实时记录顺序
            /// </summary>
            public const int Real_LawPersontel = 13;

            /// <summary>
            /// 机构联系人的数字标识
            /// </summary>
            public const byte ContactName = 16;
            
            /// <summary>
            /// 机构联系人的实时记录顺序
            /// </summary>
            public const int Real_ContactName = 14;

            /// <summary>
            /// 机构联系人电话的数字标识
            /// </summary>
            public const byte ContactTel = 17;
            
            /// <summary>
            /// 机构联系人电话的实时记录顺序
            /// </summary>
            public const int Real_ContactTel = 15;

            /// <summary>
            /// 上级机构代码的数字标识
            /// </summary>
            public const byte SuperOrgcode = 18;
            
            /// <summary>
            /// 上级机构代码的实时记录顺序
            /// </summary>
            public const int Real_SuperOrgcode = 16;

            /// <summary>
            /// 更新时间的数字标识
            /// </summary>
            public const byte UpdateDate = 19;
            
            /// <summary>
            /// 更新时间的实时记录顺序
            /// </summary>
            public const int Real_UpdateDate = 17;

            /// <summary>
            /// 更新操作员工号的数字标识
            /// </summary>
            public const byte UpdateUserid = 20;
            
            /// <summary>
            /// 更新操作员工号的实时记录顺序
            /// </summary>
            public const int Real_UpdateUserid = 18;

            /// <summary>
            /// 更新操作员姓名的数字标识
            /// </summary>
            public const byte UpdateUsername = 21;
            
            /// <summary>
            /// 更新操作员姓名的实时记录顺序
            /// </summary>
            public const int Real_UpdateUsername = 19;

            /// <summary>
            /// 备注的数字标识
            /// </summary>
            public const byte Memo = 22;
            
            /// <summary>
            /// 备注的实时记录顺序
            /// </summary>
            public const int Real_Memo = 20;

            /// <summary>
            /// 数据状态的数字标识
            /// </summary>
            public const byte DataState = 23;
            
            /// <summary>
            /// 数据状态的实时记录顺序
            /// </summary>
            public const int Real_DataState = 21;

            /// <summary>
            /// 冻结更新的数字标识
            /// </summary>
            public const byte IsFreeze = 24;
            
            /// <summary>
            /// 冻结更新的实时记录顺序
            /// </summary>
            public const int Real_IsFreeze = 22;

            /// <summary>
            /// 制作时间的数字标识
            /// </summary>
            public const byte AddDate = 25;
            
            /// <summary>
            /// 制作时间的实时记录顺序
            /// </summary>
            public const int Real_AddDate = 23;

            /// <summary>
            /// 最后修改者的数字标识
            /// </summary>
            public const byte LastReviserId = 26;
            
            /// <summary>
            /// 最后修改者的实时记录顺序
            /// </summary>
            public const int Real_LastReviserId = 24;

            /// <summary>
            /// 最后修改日期的数字标识
            /// </summary>
            public const byte LastModifyDate = 27;
            
            /// <summary>
            /// 最后修改日期的实时记录顺序
            /// </summary>
            public const int Real_LastModifyDate = 25;

            /// <summary>
            /// 制作人的数字标识
            /// </summary>
            public const byte AuthorId = 28;
            
            /// <summary>
            /// 制作人的实时记录顺序
            /// </summary>
            public const int Real_AuthorId = 26;

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
                        Real_OrgId,
                        new PropertySturct
                        {
                            Index        = OrgId,
                            Name         = "OrgId",
                            Title        = "组织标识",
                            Caption      = @"组织标识",
                            Description  = @"组织标识",
                            ColumnName   = "org_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Organization,
                        new PropertySturct
                        {
                            Index        = Organization,
                            Name         = "Organization",
                            Title        = "机构",
                            Caption      = @"机构",
                            Description  = @"短名称",
                            ColumnName   = "organization",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
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
                        Real_Classify,
                        new PropertySturct
                        {
                            Index        = Classify,
                            Name         = "Classify",
                            Title        = "应用类型",
                            Caption      = @"应用类型",
                            Description  = @"应用类型",
                            ColumnName   = "Classify",
                            PropertyType = typeof(ClassifyType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
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
                            ColumnName   = "app_key",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ManagOrgcode,
                        new PropertySturct
                        {
                            Index        = ManagOrgcode,
                            Name         = "ManagOrgcode",
                            Title        = "注册管理机构代码",
                            Caption      = @"注册管理机构代码",
                            Description  = @"注册管理机构代码",
                            ColumnName   = "manag_orgcode",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ManagOrgname,
                        new PropertySturct
                        {
                            Index        = ManagOrgname,
                            Name         = "ManagOrgname",
                            Title        = "注册管理机构名称",
                            Caption      = @"注册管理机构名称",
                            Description  = @"注册管理机构名称",
                            ColumnName   = "manag_orgname",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_CityCode,
                        new PropertySturct
                        {
                            Index        = CityCode,
                            Name         = "CityCode",
                            Title        = "所在市级编码",
                            Caption      = @"所在市级编码",
                            Description  = @"参照数据字典【行政区划】,允许更新",
                            ColumnName   = "city_code",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_DistrictCode,
                        new PropertySturct
                        {
                            Index        = DistrictCode,
                            Name         = "DistrictCode",
                            Title        = "所在区县编码",
                            Caption      = @"所在区县编码",
                            Description  = @"允许更新",
                            ColumnName   = "district_code",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrgAddress,
                        new PropertySturct
                        {
                            Index        = OrgAddress,
                            Name         = "OrgAddress",
                            Title        = "机构详细地址",
                            Caption      = @"机构详细地址",
                            Description  = @"允许更新",
                            ColumnName   = "org_address",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LawPersonname,
                        new PropertySturct
                        {
                            Index        = LawPersonname,
                            Name         = "LawPersonname",
                            Title        = "机构负责人",
                            Caption      = @"机构负责人",
                            Description  = @"允许更新",
                            ColumnName   = "law_personname",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LawPersontel,
                        new PropertySturct
                        {
                            Index        = LawPersontel,
                            Name         = "LawPersontel",
                            Title        = "机构负责人电话",
                            Caption      = @"机构负责人电话",
                            Description  = @"允许更新",
                            ColumnName   = "law_persontel",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ContactName,
                        new PropertySturct
                        {
                            Index        = ContactName,
                            Name         = "ContactName",
                            Title        = "机构联系人",
                            Caption      = @"机构联系人",
                            Description  = @"允许更新",
                            ColumnName   = "contact_name",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ContactTel,
                        new PropertySturct
                        {
                            Index        = ContactTel,
                            Name         = "ContactTel",
                            Title        = "机构联系人电话",
                            Caption      = @"机构联系人电话",
                            Description  = @"允许更新",
                            ColumnName   = "contact_tel",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SuperOrgcode,
                        new PropertySturct
                        {
                            Index        = SuperOrgcode,
                            Name         = "SuperOrgcode",
                            Title        = "上级机构代码",
                            Caption      = @"上级机构代码",
                            Description  = @"无上级机构时,传当前注册管理机构代码,允许更新",
                            ColumnName   = "super_orgcode",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_UpdateDate,
                        new PropertySturct
                        {
                            Index        = UpdateDate,
                            Name         = "UpdateDate",
                            Title        = "更新时间",
                            Caption      = @"更新时间",
                            Description  = @"更新时间",
                            ColumnName   = "update_date",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_UpdateUserid,
                        new PropertySturct
                        {
                            Index        = UpdateUserid,
                            Name         = "UpdateUserid",
                            Title        = "更新操作员工号",
                            Caption      = @"更新操作员工号",
                            Description  = @"更新操作员工号",
                            ColumnName   = "update_userid",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_UpdateUsername,
                        new PropertySturct
                        {
                            Index        = UpdateUsername,
                            Name         = "UpdateUsername",
                            Title        = "更新操作员姓名",
                            Caption      = @"更新操作员姓名",
                            Description  = @"更新操作员姓名",
                            ColumnName   = "update_username",
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