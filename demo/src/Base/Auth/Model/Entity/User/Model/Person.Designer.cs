/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 23:17:39*/
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
    /// 用户的个人信息
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class PersonData : IStateData , IHistoryData , IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public PersonData()
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
        /// 用户标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _userId;

        partial void OnUserIdGet();

        partial void OnUserIdSet(ref long value);

        partial void OnUserIdLoad(ref long value);

        partial void OnUserIdSeted();

        
        /// <summary>
        /// 用户标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("UserId", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"用户标识")]
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
        /// 性别
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public SexType _sex;

        partial void OnSexGet();

        partial void OnSexSet(ref SexType value);

        partial void OnSexSeted();

        
        /// <summary>
        /// 性别
        /// </summary>
        /// <remarks>
        /// 性别,0:女;1:男
        /// </remarks>
        [DataMember , JsonProperty("Sex", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"性别")]
        public  SexType Sex
        {
            get
            {
                OnSexGet();
                return this._sex;
            }
            set
            {
                if(this._sex == value)
                    return;
                OnSexSet(ref value);
                this._sex = value;
                OnSexSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Sex);
            }
        }
        /// <summary>
        /// 性别的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("性别")]
        public string Sex_Content => Sex.ToCaption();

        /// <summary>
        /// 性别的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int Sex_Number
        {
            get => (int)this.Sex;
            set => this.Sex = (SexType)value;
        }
        /// <summary>
        /// 所在省
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _regionProvince;

        partial void OnRegionProvinceGet();

        partial void OnRegionProvinceSet(ref int value);

        partial void OnRegionProvinceSeted();

        
        /// <summary>
        /// 所在省
        /// </summary>
        /// <remarks>
        /// 所在省id
        /// </remarks>
        [DataMember , JsonProperty("RegionProvince", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"所在省")]
        public  int RegionProvince
        {
            get
            {
                OnRegionProvinceGet();
                return this._regionProvince;
            }
            set
            {
                if(this._regionProvince == value)
                    return;
                OnRegionProvinceSet(ref value);
                this._regionProvince = value;
                OnRegionProvinceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RegionProvince);
            }
        }
        /// <summary>
        /// 所在市
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _regionCity;

        partial void OnRegionCityGet();

        partial void OnRegionCitySet(ref int value);

        partial void OnRegionCitySeted();

        
        /// <summary>
        /// 所在市
        /// </summary>
        /// <remarks>
        /// 所在市ID
        /// </remarks>
        [DataMember , JsonProperty("RegionCity", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"所在市")]
        public  int RegionCity
        {
            get
            {
                OnRegionCityGet();
                return this._regionCity;
            }
            set
            {
                if(this._regionCity == value)
                    return;
                OnRegionCitySet(ref value);
                this._regionCity = value;
                OnRegionCitySeted();
                this.OnPropertyChanged(_DataStruct_.Real_RegionCity);
            }
        }
        /// <summary>
        /// 所在县
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _regionCounty;

        partial void OnRegionCountyGet();

        partial void OnRegionCountySet(ref int value);

        partial void OnRegionCountySeted();

        
        /// <summary>
        /// 所在县
        /// </summary>
        /// <remarks>
        /// 所在县Id
        /// </remarks>
        [DataMember , JsonProperty("RegionCounty", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"所在县")]
        public  int RegionCounty
        {
            get
            {
                OnRegionCountyGet();
                return this._regionCounty;
            }
            set
            {
                if(this._regionCounty == value)
                    return;
                OnRegionCountySet(ref value);
                this._regionCounty = value;
                OnRegionCountySeted();
                this.OnPropertyChanged(_DataStruct_.Real_RegionCounty);
            }
        }
        /// <summary>
        /// 头像
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _avatarUrl;

        partial void OnAvatarUrlGet();

        partial void OnAvatarUrlSet(ref string value);

        partial void OnAvatarUrlSeted();

        
        /// <summary>
        /// 头像
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("AvatarUrl", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"头像")]
        public  string AvatarUrl
        {
            get
            {
                OnAvatarUrlGet();
                return this._avatarUrl;
            }
            set
            {
                if(this._avatarUrl == value)
                    return;
                OnAvatarUrlSet(ref value);
                this._avatarUrl = value;
                OnAvatarUrlSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AvatarUrl);
            }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _nickName;

        partial void OnNickNameGet();

        partial void OnNickNameSet(ref string value);

        partial void OnNickNameSeted();

        
        /// <summary>
        /// 昵称
        /// </summary>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataMember , JsonProperty("NickName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"昵称")]
        public  string NickName
        {
            get
            {
                OnNickNameGet();
                return this._nickName;
            }
            set
            {
                if(this._nickName == value)
                    return;
                OnNickNameSet(ref value);
                this._nickName = value;
                OnNickNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_NickName);
            }
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _idCard;

        partial void OnIdCardGet();

        partial void OnIdCardSet(ref string value);

        partial void OnIdCardSeted();

        
        /// <summary>
        /// 身份证号
        /// </summary>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataMember , JsonProperty("IdCard", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"身份证号")]
        public  string IdCard
        {
            get
            {
                OnIdCardGet();
                return this._idCard;
            }
            set
            {
                if(this._idCard == value)
                    return;
                OnIdCardSet(ref value);
                this._idCard = value;
                OnIdCardSeted();
                this.OnPropertyChanged(_DataStruct_.Real_IdCard);
            }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _realName;

        partial void OnRealNameGet();

        partial void OnRealNameSet(ref string value);

        partial void OnRealNameSeted();

        
        /// <summary>
        /// 真实姓名
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("RealName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"真实姓名")]
        public  string RealName
        {
            get
            {
                OnRealNameGet();
                return this._realName;
            }
            set
            {
                if(this._realName == value)
                    return;
                OnRealNameSet(ref value);
                this._realName = value;
                OnRealNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RealName);
            }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _phoneNumber;

        partial void OnPhoneNumberGet();

        partial void OnPhoneNumberSet(ref string value);

        partial void OnPhoneNumberSeted();

        
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
        [DataMember , JsonProperty("phoneNumber", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"手机号")]
        public  string PhoneNumber
        {
            get
            {
                OnPhoneNumberGet();
                return this._phoneNumber;
            }
            set
            {
                if(this._phoneNumber == value)
                    return;
                OnPhoneNumberSet(ref value);
                this._phoneNumber = value;
                OnPhoneNumberSeted();
                this.OnPropertyChanged(_DataStruct_.Real_PhoneNumber);
            }
        }
        /// <summary>
        /// 生日
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _birthday;

        partial void OnBirthdayGet();

        partial void OnBirthdaySet(ref DateTime value);

        partial void OnBirthdaySeted();

        
        /// <summary>
        /// 生日
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("birthday", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"生日")]
        public  DateTime Birthday
        {
            get
            {
                OnBirthdayGet();
                return this._birthday;
            }
            set
            {
                if(this._birthday == value)
                    return;
                OnBirthdaySet(ref value);
                this._birthday = value;
                OnBirthdaySeted();
                this.OnPropertyChanged(_DataStruct_.Real_Birthday);
            }
        }
        /// <summary>
        /// 证件类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public CardType _certType;

        partial void OnCertTypeGet();

        partial void OnCertTypeSet(ref CardType value);

        partial void OnCertTypeSeted();

        
        /// <summary>
        /// 证件类型
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("certType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"证件类型")]
        public  CardType CertType
        {
            get
            {
                OnCertTypeGet();
                return this._certType;
            }
            set
            {
                if(this._certType == value)
                    return;
                OnCertTypeSet(ref value);
                this._certType = value;
                OnCertTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_CertType);
            }
        }
        /// <summary>
        /// 头像照片
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public byte[] _icon;

        partial void OnIconGet();

        partial void OnIconSet(ref byte[] value);

        partial void OnIconSeted();

        
        /// <summary>
        /// 头像照片
        /// </summary>
        /// <remarks>
        /// BASE64二进制
        /// </remarks>
        [DataRule(CanNull = true)]
        [DataMember , JsonIgnore , Browsable(false) , DisplayName(@"头像照片")]
        public  byte[] Icon
        {
            get
            {
                OnIconGet();
                return this._icon;
            }
            set
            {
                if(this._icon == value)
                    return;
                OnIconSet(ref value);
                this._icon = value;
                OnIconSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Icon);
            }
        }
        
        /// <summary>
        /// 头像照片
        /// </summary>
        /// <remarks>
        /// BASE64二进制
        /// </remarks>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore) , Browsable(false) , DisplayName(@"头像照片")]
        public  string Icon_Base64
        {
            get
            {
                return this._icon == null
                       ? null
                       : Convert.ToBase64String(Icon);
            }
            set
            {
                this.Icon = string.IsNullOrWhiteSpace(value)
                       ? null
                       : Convert.FromBase64String(value);
            }
        }
        /// <summary>
        /// 民族
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _nation;

        partial void OnNationGet();

        partial void OnNationSet(ref string value);

        partial void OnNationSeted();

        
        /// <summary>
        /// 民族
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("nation", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"民族")]
        public  string Nation
        {
            get
            {
                OnNationGet();
                return this._nation;
            }
            set
            {
                if(this._nation == value)
                    return;
                OnNationSet(ref value);
                this._nation = value;
                OnNationSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Nation);
            }
        }
        /// <summary>
        /// 电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _tel;

        partial void OnTelGet();

        partial void OnTelSet(ref string value);

        partial void OnTelSeted();

        
        /// <summary>
        /// 电话
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("tel", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"电话")]
        public  string Tel
        {
            get
            {
                OnTelGet();
                return this._tel;
            }
            set
            {
                if(this._tel == value)
                    return;
                OnTelSet(ref value);
                this._tel = value;
                OnTelSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Tel);
            }
        }
        /// <summary>
        /// 电子邮件
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _email;

        partial void OnEmailGet();

        partial void OnEmailSet(ref string value);

        partial void OnEmailSeted();

        
        /// <summary>
        /// 电子邮件
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("email", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"电子邮件")]
        public  string Email
        {
            get
            {
                OnEmailGet();
                return this._email;
            }
            set
            {
                if(this._email == value)
                    return;
                OnEmailSet(ref value);
                this._email = value;
                OnEmailSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Email);
            }
        }
        /// <summary>
        /// 地址
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _homeAddress;

        partial void OnHomeAddressGet();

        partial void OnHomeAddressSet(ref string value);

        partial void OnHomeAddressSeted();

        
        /// <summary>
        /// 地址
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("homeAddress", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"地址")]
        public  string HomeAddress
        {
            get
            {
                OnHomeAddressGet();
                return this._homeAddress;
            }
            set
            {
                if(this._homeAddress == value)
                    return;
                OnHomeAddressSet(ref value);
                this._homeAddress = value;
                OnHomeAddressSeted();
                this.OnPropertyChanged(_DataStruct_.Real_HomeAddress);
            }
        }
        /// <summary>
        /// 所在公司
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _company;

        partial void OnCompanyGet();

        partial void OnCompanySet(ref string value);

        partial void OnCompanySeted();

        
        /// <summary>
        /// 所在公司
        /// </summary>
        /// <remarks>
        /// 访客填写的所在公司
        /// </remarks>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("company", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"所在公司")]
        public  string Company
        {
            get
            {
                OnCompanyGet();
                return this._company;
            }
            set
            {
                if(this._company == value)
                    return;
                OnCompanySet(ref value);
                this._company = value;
                OnCompanySeted();
                this.OnPropertyChanged(_DataStruct_.Real_Company);
            }
        }
        /// <summary>
        /// 职位称呼
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _jobTitle;

        partial void OnJobTitleGet();

        partial void OnJobTitleSet(ref string value);

        partial void OnJobTitleSeted();

        
        /// <summary>
        /// 职位称呼
        /// </summary>
        /// <remarks>
        /// 访客填写的职位称呼
        /// </remarks>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("jobTitle", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"职位称呼")]
        public  string JobTitle
        {
            get
            {
                OnJobTitleGet();
                return this._jobTitle;
            }
            set
            {
                if(this._jobTitle == value)
                    return;
                OnJobTitleSet(ref value);
                this._jobTitle = value;
                OnJobTitleSeted();
                this.OnPropertyChanged(_DataStruct_.Real_JobTitle);
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
            case "sex":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.Sex = (SexType)(int)value;
                    }
                    else if(value is SexType)
                    {
                        this.Sex = (SexType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        SexType val;
                        if (SexType.TryParse(str, out val))
                        {
                            this.Sex = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.Sex = (SexType)vl;
                            }
                        }
                    }
                }
                return;
            case "regionprovince":
                this.RegionProvince = (int)Convert.ToDecimal(value);
                return;
            case "regioncity":
                this.RegionCity = (int)Convert.ToDecimal(value);
                return;
            case "regioncounty":
                this.RegionCounty = (int)Convert.ToDecimal(value);
                return;
            case "avatarurl":
                this.AvatarUrl = value == null ? null : value.ToString();
                return;
            case "nickname":
                this.NickName = value == null ? null : value.ToString();
                return;
            case "idcard":
                this.IdCard = value == null ? null : value.ToString();
                return;
            case "realname":
                this.RealName = value == null ? null : value.ToString();
                return;
            case "phonenumber":
                this.PhoneNumber = value == null ? null : value.ToString();
                return;
            case "birthday":
                this.Birthday = Convert.ToDateTime(value);
                return;
            case "certtype":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.CertType = (CardType)(int)value;
                    }
                    else if(value is CardType)
                    {
                        this.CertType = (CardType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        CardType val;
                        if (CardType.TryParse(str, out val))
                        {
                            this.CertType = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.CertType = (CardType)vl;
                            }
                        }
                    }
                }
                return;
            case "icon":
                this.Icon = (byte[])value;
                return;
            case "nation":
                this.Nation = value == null ? null : value.ToString();
                return;
            case "tel":
                this.Tel = value == null ? null : value.ToString();
                return;
            case "email":
                this.Email = value == null ? null : value.ToString();
                return;
            case "homeaddress":
                this.HomeAddress = value == null ? null : value.ToString();
                return;
            case "company":
                this.Company = value == null ? null : value.ToString();
                return;
            case "jobtitle":
                this.JobTitle = value == null ? null : value.ToString();
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
            case _DataStruct_.UserId:
                this.UserId = Convert.ToInt64(value);
                return;
            case _DataStruct_.Sex:
                this.Sex = (SexType)value;
                return;
            case _DataStruct_.RegionProvince:
                this.RegionProvince = Convert.ToInt32(value);
                return;
            case _DataStruct_.RegionCity:
                this.RegionCity = Convert.ToInt32(value);
                return;
            case _DataStruct_.RegionCounty:
                this.RegionCounty = Convert.ToInt32(value);
                return;
            case _DataStruct_.AvatarUrl:
                this.AvatarUrl = value == null ? null : value.ToString();
                return;
            case _DataStruct_.NickName:
                this.NickName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.IdCard:
                this.IdCard = value == null ? null : value.ToString();
                return;
            case _DataStruct_.RealName:
                this.RealName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.PhoneNumber:
                this.PhoneNumber = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Birthday:
                this.Birthday = Convert.ToDateTime(value);
                return;
            case _DataStruct_.CertType:
                this.CertType = (CardType)value;
                return;
            case _DataStruct_.Icon:
                this.Icon = (byte[])value;
                return;
            case _DataStruct_.Nation:
                this.Nation = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Tel:
                this.Tel = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Email:
                this.Email = value == null ? null : value.ToString();
                return;
            case _DataStruct_.HomeAddress:
                this.HomeAddress = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Company:
                this.Company = value == null ? null : value.ToString();
                return;
            case _DataStruct_.JobTitle:
                this.JobTitle = value == null ? null : value.ToString();
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
            case "userid":
                return this.UserId;
            case "sex":
                return this.Sex.ToCaption();
            case "regionprovince":
                return this.RegionProvince;
            case "regioncity":
                return this.RegionCity;
            case "regioncounty":
                return this.RegionCounty;
            case "avatarurl":
                return this.AvatarUrl;
            case "nickname":
                return this.NickName;
            case "idcard":
                return this.IdCard;
            case "realname":
                return this.RealName;
            case "phonenumber":
                return this.PhoneNumber;
            case "birthday":
                return this.Birthday;
            case "certtype":
                return this.CertType;
            case "icon":
                return this.Icon;
            case "nation":
                return this.Nation;
            case "tel":
                return this.Tel;
            case "email":
                return this.Email;
            case "homeaddress":
                return this.HomeAddress;
            case "company":
                return this.Company;
            case "jobtitle":
                return this.JobTitle;
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
                case _DataStruct_.UserId:
                    return this.UserId;
                case _DataStruct_.Sex:
                    return this.Sex;
                case _DataStruct_.RegionProvince:
                    return this.RegionProvince;
                case _DataStruct_.RegionCity:
                    return this.RegionCity;
                case _DataStruct_.RegionCounty:
                    return this.RegionCounty;
                case _DataStruct_.AvatarUrl:
                    return this.AvatarUrl;
                case _DataStruct_.NickName:
                    return this.NickName;
                case _DataStruct_.IdCard:
                    return this.IdCard;
                case _DataStruct_.RealName:
                    return this.RealName;
                case _DataStruct_.PhoneNumber:
                    return this.PhoneNumber;
                case _DataStruct_.Birthday:
                    return this.Birthday;
                case _DataStruct_.CertType:
                    return this.CertType;
                case _DataStruct_.Icon:
                    return this.Icon;
                case _DataStruct_.Nation:
                    return this.Nation;
                case _DataStruct_.Tel:
                    return this.Tel;
                case _DataStruct_.Email:
                    return this.Email;
                case _DataStruct_.HomeAddress:
                    return this.HomeAddress;
                case _DataStruct_.Company:
                    return this.Company;
                case _DataStruct_.JobTitle:
                    return this.JobTitle;
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
        

        partial void CopyExtendValue(PersonData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as PersonData;
            if(sourceEntity == null)
                return;
            this._userId = sourceEntity._userId;
            this._sex = sourceEntity._sex;
            this._regionProvince = sourceEntity._regionProvince;
            this._regionCity = sourceEntity._regionCity;
            this._regionCounty = sourceEntity._regionCounty;
            this._avatarUrl = sourceEntity._avatarUrl;
            this._nickName = sourceEntity._nickName;
            this._idCard = sourceEntity._idCard;
            this._realName = sourceEntity._realName;
            this._phoneNumber = sourceEntity._phoneNumber;
            this._birthday = sourceEntity._birthday;
            this._certType = sourceEntity._certType;
            this._icon = sourceEntity._icon;
            this._nation = sourceEntity._nation;
            this._tel = sourceEntity._tel;
            this._email = sourceEntity._email;
            this._homeAddress = sourceEntity._homeAddress;
            this._company = sourceEntity._company;
            this._jobTitle = sourceEntity._jobTitle;
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
        public void Copy(PersonData source)
        {
                this.UserId = source.UserId;
                this.Sex = source.Sex;
                this.RegionProvince = source.RegionProvince;
                this.RegionCity = source.RegionCity;
                this.RegionCounty = source.RegionCounty;
                this.AvatarUrl = source.AvatarUrl;
                this.NickName = source.NickName;
                this.IdCard = source.IdCard;
                this.RealName = source.RealName;
                this.PhoneNumber = source.PhoneNumber;
                this.Birthday = source.Birthday;
                this.CertType = source.CertType;
                this.Icon = source.Icon;
                this.Nation = source.Nation;
                this.Tel = source.Tel;
                this.Email = source.Email;
                this.HomeAddress = source.HomeAddress;
                this.Company = source.Company;
                this.JobTitle = source.JobTitle;
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
                OnUserIdModified(subsist,false);
                OnSexModified(subsist,false);
                OnRegionProvinceModified(subsist,false);
                OnRegionCityModified(subsist,false);
                OnRegionCountyModified(subsist,false);
                OnAvatarUrlModified(subsist,false);
                OnNickNameModified(subsist,false);
                OnIdCardModified(subsist,false);
                OnRealNameModified(subsist,false);
                OnPhoneNumberModified(subsist,false);
                OnBirthdayModified(subsist,false);
                OnCertTypeModified(subsist,false);
                OnIconModified(subsist,false);
                OnNationModified(subsist,false);
                OnTelModified(subsist,false);
                OnEmailModified(subsist,false);
                OnHomeAddressModified(subsist,false);
                OnCompanyModified(subsist,false);
                OnJobTitleModified(subsist,false);
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
                OnUserIdModified(subsist,true);
                OnSexModified(subsist,true);
                OnRegionProvinceModified(subsist,true);
                OnRegionCityModified(subsist,true);
                OnRegionCountyModified(subsist,true);
                OnAvatarUrlModified(subsist,true);
                OnNickNameModified(subsist,true);
                OnIdCardModified(subsist,true);
                OnRealNameModified(subsist,true);
                OnPhoneNumberModified(subsist,true);
                OnBirthdayModified(subsist,true);
                OnCertTypeModified(subsist,true);
                OnIconModified(subsist,true);
                OnNationModified(subsist,true);
                OnTelModified(subsist,true);
                OnEmailModified(subsist,true);
                OnHomeAddressModified(subsist,true);
                OnCompanyModified(subsist,true);
                OnJobTitleModified(subsist,true);
                OnIsFreezeModified(subsist,true);
                OnDataStateModified(subsist,true);
                OnAddDateModified(subsist,true);
                OnLastReviserIdModified(subsist,true);
                OnLastModifyDateModified(subsist,true);
                OnAuthorIdModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[25] > 0)
            {
                OnUserIdModified(subsist,modifieds[_DataStruct_.Real_UserId] == 1);
                OnSexModified(subsist,modifieds[_DataStruct_.Real_Sex] == 1);
                OnRegionProvinceModified(subsist,modifieds[_DataStruct_.Real_RegionProvince] == 1);
                OnRegionCityModified(subsist,modifieds[_DataStruct_.Real_RegionCity] == 1);
                OnRegionCountyModified(subsist,modifieds[_DataStruct_.Real_RegionCounty] == 1);
                OnAvatarUrlModified(subsist,modifieds[_DataStruct_.Real_AvatarUrl] == 1);
                OnNickNameModified(subsist,modifieds[_DataStruct_.Real_NickName] == 1);
                OnIdCardModified(subsist,modifieds[_DataStruct_.Real_IdCard] == 1);
                OnRealNameModified(subsist,modifieds[_DataStruct_.Real_RealName] == 1);
                OnPhoneNumberModified(subsist,modifieds[_DataStruct_.Real_PhoneNumber] == 1);
                OnBirthdayModified(subsist,modifieds[_DataStruct_.Real_Birthday] == 1);
                OnCertTypeModified(subsist,modifieds[_DataStruct_.Real_CertType] == 1);
                OnIconModified(subsist,modifieds[_DataStruct_.Real_Icon] == 1);
                OnNationModified(subsist,modifieds[_DataStruct_.Real_Nation] == 1);
                OnTelModified(subsist,modifieds[_DataStruct_.Real_Tel] == 1);
                OnEmailModified(subsist,modifieds[_DataStruct_.Real_Email] == 1);
                OnHomeAddressModified(subsist,modifieds[_DataStruct_.Real_HomeAddress] == 1);
                OnCompanyModified(subsist,modifieds[_DataStruct_.Real_Company] == 1);
                OnJobTitleModified(subsist,modifieds[_DataStruct_.Real_JobTitle] == 1);
                OnIsFreezeModified(subsist,modifieds[_DataStruct_.Real_IsFreeze] == 1);
                OnDataStateModified(subsist,modifieds[_DataStruct_.Real_DataState] == 1);
                OnAddDateModified(subsist,modifieds[_DataStruct_.Real_AddDate] == 1);
                OnLastReviserIdModified(subsist,modifieds[_DataStruct_.Real_LastReviserId] == 1);
                OnLastModifyDateModified(subsist,modifieds[_DataStruct_.Real_LastModifyDate] == 1);
                OnAuthorIdModified(subsist,modifieds[_DataStruct_.Real_AuthorId] == 1);
            }
        }

        /// <summary>
        /// 用户标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 性别修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSexModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 所在省修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRegionProvinceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 所在市修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRegionCityModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 所在县修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRegionCountyModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 头像修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAvatarUrlModified(EntitySubsist subsist,bool isModified);

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
        /// 真实姓名修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRealNameModified(EntitySubsist subsist,bool isModified);

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
        /// 生日修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBirthdayModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 证件类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCertTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 头像照片修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIconModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 民族修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnNationModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTelModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 电子邮件修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnEmailModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 地址修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnHomeAddressModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 所在公司修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCompanyModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 职位称呼修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnJobTitleModified(EntitySubsist subsist,bool isModified);

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
            public const string EntityName = @"Person";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"个人信息";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户的个人信息";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0xD000C;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "UserId";
            
            
            /// <summary>
            /// 用户标识的数字标识
            /// </summary>
            public const byte UserId = 1;
            
            /// <summary>
            /// 用户标识的实时记录顺序
            /// </summary>
            public const int Real_UserId = 0;

            /// <summary>
            /// 性别的数字标识
            /// </summary>
            public const byte Sex = 2;
            
            /// <summary>
            /// 性别的实时记录顺序
            /// </summary>
            public const int Real_Sex = 1;

            /// <summary>
            /// 所在省的数字标识
            /// </summary>
            public const byte RegionProvince = 3;
            
            /// <summary>
            /// 所在省的实时记录顺序
            /// </summary>
            public const int Real_RegionProvince = 2;

            /// <summary>
            /// 所在市的数字标识
            /// </summary>
            public const byte RegionCity = 4;
            
            /// <summary>
            /// 所在市的实时记录顺序
            /// </summary>
            public const int Real_RegionCity = 3;

            /// <summary>
            /// 所在县的数字标识
            /// </summary>
            public const byte RegionCounty = 5;
            
            /// <summary>
            /// 所在县的实时记录顺序
            /// </summary>
            public const int Real_RegionCounty = 4;

            /// <summary>
            /// 头像的数字标识
            /// </summary>
            public const byte AvatarUrl = 6;
            
            /// <summary>
            /// 头像的实时记录顺序
            /// </summary>
            public const int Real_AvatarUrl = 5;

            /// <summary>
            /// 昵称的数字标识
            /// </summary>
            public const byte NickName = 7;
            
            /// <summary>
            /// 昵称的实时记录顺序
            /// </summary>
            public const int Real_NickName = 6;

            /// <summary>
            /// 身份证号的数字标识
            /// </summary>
            public const byte IdCard = 8;
            
            /// <summary>
            /// 身份证号的实时记录顺序
            /// </summary>
            public const int Real_IdCard = 7;

            /// <summary>
            /// 真实姓名的数字标识
            /// </summary>
            public const byte RealName = 9;
            
            /// <summary>
            /// 真实姓名的实时记录顺序
            /// </summary>
            public const int Real_RealName = 8;

            /// <summary>
            /// 手机号的数字标识
            /// </summary>
            public const byte PhoneNumber = 16;
            
            /// <summary>
            /// 手机号的实时记录顺序
            /// </summary>
            public const int Real_PhoneNumber = 9;

            /// <summary>
            /// 生日的数字标识
            /// </summary>
            public const byte Birthday = 17;
            
            /// <summary>
            /// 生日的实时记录顺序
            /// </summary>
            public const int Real_Birthday = 10;

            /// <summary>
            /// 证件类型的数字标识
            /// </summary>
            public const byte CertType = 18;
            
            /// <summary>
            /// 证件类型的实时记录顺序
            /// </summary>
            public const int Real_CertType = 11;

            /// <summary>
            /// 头像照片的数字标识
            /// </summary>
            public const byte Icon = 20;
            
            /// <summary>
            /// 头像照片的实时记录顺序
            /// </summary>
            public const int Real_Icon = 12;

            /// <summary>
            /// 民族的数字标识
            /// </summary>
            public const byte Nation = 21;
            
            /// <summary>
            /// 民族的实时记录顺序
            /// </summary>
            public const int Real_Nation = 13;

            /// <summary>
            /// 电话的数字标识
            /// </summary>
            public const byte Tel = 22;
            
            /// <summary>
            /// 电话的实时记录顺序
            /// </summary>
            public const int Real_Tel = 14;

            /// <summary>
            /// 电子邮件的数字标识
            /// </summary>
            public const byte Email = 23;
            
            /// <summary>
            /// 电子邮件的实时记录顺序
            /// </summary>
            public const int Real_Email = 15;

            /// <summary>
            /// 地址的数字标识
            /// </summary>
            public const byte HomeAddress = 24;
            
            /// <summary>
            /// 地址的实时记录顺序
            /// </summary>
            public const int Real_HomeAddress = 16;

            /// <summary>
            /// 所在公司的数字标识
            /// </summary>
            public const byte Company = 25;
            
            /// <summary>
            /// 所在公司的实时记录顺序
            /// </summary>
            public const int Real_Company = 17;

            /// <summary>
            /// 职位称呼的数字标识
            /// </summary>
            public const byte JobTitle = 26;
            
            /// <summary>
            /// 职位称呼的实时记录顺序
            /// </summary>
            public const int Real_JobTitle = 18;

            /// <summary>
            /// 冻结更新的数字标识
            /// </summary>
            public const byte IsFreeze = 27;
            
            /// <summary>
            /// 冻结更新的实时记录顺序
            /// </summary>
            public const int Real_IsFreeze = 19;

            /// <summary>
            /// 数据状态的数字标识
            /// </summary>
            public const byte DataState = 28;
            
            /// <summary>
            /// 数据状态的实时记录顺序
            /// </summary>
            public const int Real_DataState = 20;

            /// <summary>
            /// 制作时间的数字标识
            /// </summary>
            public const byte AddDate = 29;
            
            /// <summary>
            /// 制作时间的实时记录顺序
            /// </summary>
            public const int Real_AddDate = 21;

            /// <summary>
            /// 最后修改者的数字标识
            /// </summary>
            public const byte LastReviserId = 30;
            
            /// <summary>
            /// 最后修改者的实时记录顺序
            /// </summary>
            public const int Real_LastReviserId = 22;

            /// <summary>
            /// 最后修改日期的数字标识
            /// </summary>
            public const byte LastModifyDate = 31;
            
            /// <summary>
            /// 最后修改日期的实时记录顺序
            /// </summary>
            public const int Real_LastModifyDate = 23;

            /// <summary>
            /// 制作人的数字标识
            /// </summary>
            public const byte AuthorId = 32;
            
            /// <summary>
            /// 制作人的实时记录顺序
            /// </summary>
            public const int Real_AuthorId = 24;

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
                            Title        = "用户标识",
                            Caption      = @"用户标识",
                            Description  = @"用户标识",
                            ColumnName   = "user_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Sex,
                        new PropertySturct
                        {
                            Index        = Sex,
                            Name         = "Sex",
                            Title        = "性别",
                            Caption      = @"性别",
                            Description  = @"性别,0:女;1:男",
                            ColumnName   = "sex",
                            PropertyType = typeof(SexType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RegionProvince,
                        new PropertySturct
                        {
                            Index        = RegionProvince,
                            Name         = "RegionProvince",
                            Title        = "所在省",
                            Caption      = @"所在省",
                            Description  = @"所在省id",
                            ColumnName   = "region_province",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RegionCity,
                        new PropertySturct
                        {
                            Index        = RegionCity,
                            Name         = "RegionCity",
                            Title        = "所在市",
                            Caption      = @"所在市",
                            Description  = @"所在市ID",
                            ColumnName   = "region_city",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RegionCounty,
                        new PropertySturct
                        {
                            Index        = RegionCounty,
                            Name         = "RegionCounty",
                            Title        = "所在县",
                            Caption      = @"所在县",
                            Description  = @"所在县Id",
                            ColumnName   = "region_county",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AvatarUrl,
                        new PropertySturct
                        {
                            Index        = AvatarUrl,
                            Name         = "AvatarUrl",
                            Title        = "头像",
                            Caption      = @"头像",
                            Description  = @"头像",
                            ColumnName   = "avatar_url",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
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
                        Real_Birthday,
                        new PropertySturct
                        {
                            Index        = Birthday,
                            Name         = "Birthday",
                            Title        = "生日",
                            Caption      = @"生日",
                            Description  = @"生日",
                            ColumnName   = "birthday",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_CertType,
                        new PropertySturct
                        {
                            Index        = CertType,
                            Name         = "CertType",
                            Title        = "证件类型",
                            Caption      = @"证件类型",
                            Description  = @"证件类型",
                            ColumnName   = "cert_type",
                            PropertyType = typeof(CardType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Icon,
                        new PropertySturct
                        {
                            Index        = Icon,
                            Name         = "Icon",
                            Title        = "头像照片",
                            Caption      = @"头像照片",
                            Description  = @"BASE64二进制",
                            ColumnName   = "icon",
                            PropertyType = typeof(byte[]),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Object,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Nation,
                        new PropertySturct
                        {
                            Index        = Nation,
                            Name         = "Nation",
                            Title        = "民族",
                            Caption      = @"民族",
                            Description  = @"民族",
                            ColumnName   = "nation",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Tel,
                        new PropertySturct
                        {
                            Index        = Tel,
                            Name         = "Tel",
                            Title        = "电话",
                            Caption      = @"电话",
                            Description  = @"电话",
                            ColumnName   = "tel",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Email,
                        new PropertySturct
                        {
                            Index        = Email,
                            Name         = "Email",
                            Title        = "电子邮件",
                            Caption      = @"电子邮件",
                            Description  = @"电子邮件",
                            ColumnName   = "email",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_HomeAddress,
                        new PropertySturct
                        {
                            Index        = HomeAddress,
                            Name         = "HomeAddress",
                            Title        = "地址",
                            Caption      = @"地址",
                            Description  = @"地址",
                            ColumnName   = "home_address",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Company,
                        new PropertySturct
                        {
                            Index        = Company,
                            Name         = "Company",
                            Title        = "所在公司",
                            Caption      = @"所在公司",
                            Description  = @"访客填写的所在公司",
                            ColumnName   = "company",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_JobTitle,
                        new PropertySturct
                        {
                            Index        = JobTitle,
                            Name         = "JobTitle",
                            Title        = "职位称呼",
                            Caption      = @"职位称呼",
                            Description  = @"访客填写的职位称呼",
                            ColumnName   = "job_title",
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