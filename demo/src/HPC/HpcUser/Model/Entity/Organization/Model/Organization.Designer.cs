/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 16:30:30*/
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
    /// 组织机构
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class OrganizationData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public OrganizationData()
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
        public void ChangePrimaryKey(long oID)
        {
            _oID = oID;
        }
        /// <summary>
        /// 组织编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _oID;

        partial void OnOIDGet();

        partial void OnOIDSet(ref long value);

        partial void OnOIDLoad(ref long value);

        partial void OnOIDSeted();

        
        /// <summary>
        /// 组织编号
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"组织编号")]
        public long OID
        {
            get
            {
                OnOIDGet();
                return this._oID;
            }
            set
            {
                if(this._oID == value)
                    return;
                //if(this._oID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnOIDSet(ref value);
                this._oID = value;
                this.OnPropertyChanged(_DataStruct_.Real_OID);
                OnOIDSeted();
            }
        }
        /// <summary>
        /// 上级组织编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _pID;

        partial void OnPIDGet();

        partial void OnPIDSet(ref long value);

        partial void OnPIDSeted();

        
        /// <summary>
        /// 上级组织编号
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("PID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"上级组织编号")]
        public  long PID
        {
            get
            {
                OnPIDGet();
                return this._pID;
            }
            set
            {
                if(this._pID == value)
                    return;
                OnPIDSet(ref value);
                this._pID = value;
                OnPIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_PID);
            }
        }
        /// <summary>
        /// 站点标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _siteSID;

        partial void OnSiteSIDGet();

        partial void OnSiteSIDSet(ref long value);

        partial void OnSiteSIDSeted();

        
        /// <summary>
        /// 站点标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SiteSID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"站点标识")]
        public  long SiteSID
        {
            get
            {
                OnSiteSIDGet();
                return this._siteSID;
            }
            set
            {
                if(this._siteSID == value)
                    return;
                OnSiteSIDSet(ref value);
                this._siteSID = value;
                OnSiteSIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SiteSID);
            }
        }
        /// <summary>
        /// 全局标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orgKey;

        partial void OnOrgKeyGet();

        partial void OnOrgKeySet(ref string value);

        partial void OnOrgKeySeted();

        
        /// <summary>
        /// 全局标识
        /// </summary>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgKey", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"全局标识")]
        public  string OrgKey
        {
            get
            {
                OnOrgKeyGet();
                return this._orgKey;
            }
            set
            {
                if(this._orgKey == value)
                    return;
                OnOrgKeySet(ref value);
                this._orgKey = value;
                OnOrgKeySeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgKey);
            }
        }
        /// <summary>
        /// 组织名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orgName;

        partial void OnOrgNameGet();

        partial void OnOrgNameSet(ref string value);

        partial void OnOrgNameSeted();

        
        /// <summary>
        /// 组织名称
        /// </summary>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织名称")]
        public  string OrgName
        {
            get
            {
                OnOrgNameGet();
                return this._orgName;
            }
            set
            {
                if(this._orgName == value)
                    return;
                OnOrgNameSet(ref value);
                this._orgName = value;
                OnOrgNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgName);
            }
        }
        /// <summary>
        /// 首席执行官
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orgChief;

        partial void OnOrgChiefGet();

        partial void OnOrgChiefSet(ref string value);

        partial void OnOrgChiefSeted();

        
        /// <summary>
        /// 首席执行官
        /// </summary>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgChief", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"首席执行官")]
        public  string OrgChief
        {
            get
            {
                OnOrgChiefGet();
                return this._orgChief;
            }
            set
            {
                if(this._orgChief == value)
                    return;
                OnOrgChiefSet(ref value);
                this._orgChief = value;
                OnOrgChiefSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgChief);
            }
        }
        /// <summary>
        /// 组织备注
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orgRemark;

        partial void OnOrgRemarkGet();

        partial void OnOrgRemarkSet(ref string value);

        partial void OnOrgRemarkSeted();

        
        /// <summary>
        /// 组织备注
        /// </summary>
        /// <value>
        /// 不能为空.可存储400个字符.合理长度应不大于400.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgRemark", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织备注")]
        public  string OrgRemark
        {
            get
            {
                OnOrgRemarkGet();
                return this._orgRemark;
            }
            set
            {
                if(this._orgRemark == value)
                    return;
                OnOrgRemarkSet(ref value);
                this._orgRemark = value;
                OnOrgRemarkSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgRemark);
            }
        }
        /// <summary>
        /// Logo
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orgLogo;

        partial void OnOrgLogoGet();

        partial void OnOrgLogoSet(ref string value);

        partial void OnOrgLogoSeted();

        
        /// <summary>
        /// Logo
        /// </summary>
        /// <value>
        /// 不能为空.可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgLogo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"Logo")]
        public  string OrgLogo
        {
            get
            {
                OnOrgLogoGet();
                return this._orgLogo;
            }
            set
            {
                if(this._orgLogo == value)
                    return;
                OnOrgLogoSet(ref value);
                this._orgLogo = value;
                OnOrgLogoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgLogo);
            }
        }
        /// <summary>
        /// 组织简介
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orgIntroduce;

        partial void OnOrgIntroduceGet();

        partial void OnOrgIntroduceSet(ref string value);

        partial void OnOrgIntroduceSeted();

        
        /// <summary>
        /// 组织简介
        /// </summary>
        /// <value>
        /// 不能为空.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgIntroduce", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织简介")]
        public  string OrgIntroduce
        {
            get
            {
                OnOrgIntroduceGet();
                return this._orgIntroduce;
            }
            set
            {
                if(this._orgIntroduce == value)
                    return;
                OnOrgIntroduceSet(ref value);
                this._orgIntroduce = value;
                OnOrgIntroduceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgIntroduce);
            }
        }
        /// <summary>
        /// 序号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _sort;

        partial void OnSortGet();

        partial void OnSortSet(ref int value);

        partial void OnSortSeted();

        
        /// <summary>
        /// 序号
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Sort", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"序号")]
        public  int Sort
        {
            get
            {
                OnSortGet();
                return this._sort;
            }
            set
            {
                if(this._sort == value)
                    return;
                OnSortSet(ref value);
                this._sort = value;
                OnSortSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Sort);
            }
        }
        /// <summary>
        /// 小图标
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _icon;

        partial void OnIconGet();

        partial void OnIconSet(ref string value);

        partial void OnIconSeted();

        
        /// <summary>
        /// 小图标
        /// </summary>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Icon", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"小图标")]
        public  string Icon
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
        /// 外部状态
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public bool _stateExternal;

        partial void OnStateExternalGet();

        partial void OnStateExternalSet(ref bool value);

        partial void OnStateExternalSeted();

        
        /// <summary>
        /// 外部状态
        /// </summary>
        /// <remarks>
        /// 开放查询需求
        /// </remarks>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("StateExternal", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"外部状态")]
        public  bool StateExternal
        {
            get
            {
                OnStateExternalGet();
                return this._stateExternal;
            }
            set
            {
                if(this._stateExternal == value)
                    return;
                OnStateExternalSet(ref value);
                this._stateExternal = value;
                OnStateExternalSeted();
                this.OnPropertyChanged(_DataStruct_.Real_StateExternal);
            }
        }
        /// <summary>
        /// 托管状态
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public bool _stateTrusteeship;

        partial void OnStateTrusteeshipGet();

        partial void OnStateTrusteeshipSet(ref bool value);

        partial void OnStateTrusteeshipSeted();

        
        /// <summary>
        /// 托管状态
        /// </summary>
        /// <remarks>
        /// 管理主体与业务主体不相同的情况使用
        /// </remarks>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("StateTrusteeship", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"托管状态")]
        public  bool StateTrusteeship
        {
            get
            {
                OnStateTrusteeshipGet();
                return this._stateTrusteeship;
            }
            set
            {
                if(this._stateTrusteeship == value)
                    return;
                OnStateTrusteeshipSet(ref value);
                this._stateTrusteeship = value;
                OnStateTrusteeshipSeted();
                this.OnPropertyChanged(_DataStruct_.Real_StateTrusteeship);
            }
        }
        /// <summary>
        /// 营业执照代码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _businessLicenseCode;

        partial void OnBusinessLicenseCodeGet();

        partial void OnBusinessLicenseCodeSet(ref string value);

        partial void OnBusinessLicenseCodeSeted();

        
        /// <summary>
        /// 营业执照代码
        /// </summary>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("BusinessLicenseCode", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"营业执照代码")]
        public  string BusinessLicenseCode
        {
            get
            {
                OnBusinessLicenseCodeGet();
                return this._businessLicenseCode;
            }
            set
            {
                if(this._businessLicenseCode == value)
                    return;
                OnBusinessLicenseCodeSet(ref value);
                this._businessLicenseCode = value;
                OnBusinessLicenseCodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_BusinessLicenseCode);
            }
        }
        /// <summary>
        /// 营业执照图片
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _businessLicenseImage;

        partial void OnBusinessLicenseImageGet();

        partial void OnBusinessLicenseImageSet(ref string value);

        partial void OnBusinessLicenseImageSeted();

        
        /// <summary>
        /// 营业执照图片
        /// </summary>
        /// <value>
        /// 不能为空.可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("BusinessLicenseImage", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"营业执照图片")]
        public  string BusinessLicenseImage
        {
            get
            {
                OnBusinessLicenseImageGet();
                return this._businessLicenseImage;
            }
            set
            {
                if(this._businessLicenseImage == value)
                    return;
                OnBusinessLicenseImageSet(ref value);
                this._businessLicenseImage = value;
                OnBusinessLicenseImageSeted();
                this.OnPropertyChanged(_DataStruct_.Real_BusinessLicenseImage);
            }
        }
        /// <summary>
        /// 法人姓名
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _legalPersonName;

        partial void OnLegalPersonNameGet();

        partial void OnLegalPersonNameSet(ref string value);

        partial void OnLegalPersonNameSeted();

        
        /// <summary>
        /// 法人姓名
        /// </summary>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LegalPersonName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"法人姓名")]
        public  string LegalPersonName
        {
            get
            {
                OnLegalPersonNameGet();
                return this._legalPersonName;
            }
            set
            {
                if(this._legalPersonName == value)
                    return;
                OnLegalPersonNameSet(ref value);
                this._legalPersonName = value;
                OnLegalPersonNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LegalPersonName);
            }
        }
        /// <summary>
        /// 法人身份证号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _legalPersonCardID;

        partial void OnLegalPersonCardIDGet();

        partial void OnLegalPersonCardIDSet(ref string value);

        partial void OnLegalPersonCardIDSeted();

        
        /// <summary>
        /// 法人身份证号
        /// </summary>
        /// <value>
        /// 不能为空.可存储40个字符.合理长度应不大于40.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LegalPersonCardID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"法人身份证号")]
        public  string LegalPersonCardID
        {
            get
            {
                OnLegalPersonCardIDGet();
                return this._legalPersonCardID;
            }
            set
            {
                if(this._legalPersonCardID == value)
                    return;
                OnLegalPersonCardIDSet(ref value);
                this._legalPersonCardID = value;
                OnLegalPersonCardIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LegalPersonCardID);
            }
        }
        /// <summary>
        /// 法人证件图像01
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _legalPersonCardImage01;

        partial void OnLegalPersonCardImage01Get();

        partial void OnLegalPersonCardImage01Set(ref string value);

        partial void OnLegalPersonCardImage01Seted();

        
        /// <summary>
        /// 法人证件图像01
        /// </summary>
        /// <value>
        /// 不能为空.可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LegalPersonCardImage01", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"法人证件图像01")]
        public  string LegalPersonCardImage01
        {
            get
            {
                OnLegalPersonCardImage01Get();
                return this._legalPersonCardImage01;
            }
            set
            {
                if(this._legalPersonCardImage01 == value)
                    return;
                OnLegalPersonCardImage01Set(ref value);
                this._legalPersonCardImage01 = value;
                OnLegalPersonCardImage01Seted();
                this.OnPropertyChanged(_DataStruct_.Real_LegalPersonCardImage01);
            }
        }
        /// <summary>
        /// 法人证件图像02
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _legalPersonCardImage02;

        partial void OnLegalPersonCardImage02Get();

        partial void OnLegalPersonCardImage02Set(ref string value);

        partial void OnLegalPersonCardImage02Seted();

        
        /// <summary>
        /// 法人证件图像02
        /// </summary>
        /// <value>
        /// 不能为空.可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LegalPersonCardImage02", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"法人证件图像02")]
        public  string LegalPersonCardImage02
        {
            get
            {
                OnLegalPersonCardImage02Get();
                return this._legalPersonCardImage02;
            }
            set
            {
                if(this._legalPersonCardImage02 == value)
                    return;
                OnLegalPersonCardImage02Set(ref value);
                this._legalPersonCardImage02 = value;
                OnLegalPersonCardImage02Seted();
                this.OnPropertyChanged(_DataStruct_.Real_LegalPersonCardImage02);
            }
        }
        /// <summary>
        /// 地理地址
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _address;

        partial void OnAddressGet();

        partial void OnAddressSet(ref string value);

        partial void OnAddressSeted();

        
        /// <summary>
        /// 地理地址
        /// </summary>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Address", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"地理地址")]
        public  string Address
        {
            get
            {
                OnAddressGet();
                return this._address;
            }
            set
            {
                if(this._address == value)
                    return;
                OnAddressSet(ref value);
                this._address = value;
                OnAddressSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Address);
            }
        }
        /// <summary>
        /// 地理纬度
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public float _location_latitude;

        partial void OnLocation_latitudeGet();

        partial void OnLocation_latitudeSet(ref float value);

        partial void OnLocation_latitudeSeted();

        
        /// <summary>
        /// 地理纬度
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Location_latitude", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"地理纬度")]
        public  float Location_latitude
        {
            get
            {
                OnLocation_latitudeGet();
                return this._location_latitude;
            }
            set
            {
                if(this._location_latitude == value)
                    return;
                OnLocation_latitudeSet(ref value);
                this._location_latitude = value;
                OnLocation_latitudeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Location_latitude);
            }
        }
        /// <summary>
        /// 地理经度
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public float _location_longitude;

        partial void OnLocation_longitudeGet();

        partial void OnLocation_longitudeSet(ref float value);

        partial void OnLocation_longitudeSeted();

        
        /// <summary>
        /// 地理经度
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Location_longitude", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"地理经度")]
        public  float Location_longitude
        {
            get
            {
                OnLocation_longitudeGet();
                return this._location_longitude;
            }
            set
            {
                if(this._location_longitude == value)
                    return;
                OnLocation_longitudeSet(ref value);
                this._location_longitude = value;
                OnLocation_longitudeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Location_longitude);
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
                return this.OID;
            }
            set
            {
                this.OID = value;
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
            case "oid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.OID = vl;
                        return true;
                    }
                }
                return false;
            case "pid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.PID = vl;
                        return true;
                    }
                }
                return false;
            case "sitesid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.SiteSID = vl;
                        return true;
                    }
                }
                return false;
            case "orgkey":
                this.OrgKey = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "orgname":
                this.OrgName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "orgchief":
                this.OrgChief = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "orgremark":
                this.OrgRemark = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "orglogo":
                this.OrgLogo = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "orgintroduce":
                this.OrgIntroduce = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "sort":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.Sort = vl;
                        return true;
                    }
                }
                return false;
            case "icon":
                this.Icon = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "stateexternal":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (bool.TryParse(value, out var vl))
                    {
                        this.StateExternal = vl;
                        return true;
                    }
                }
                return false;
            case "statetrusteeship":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (bool.TryParse(value, out var vl))
                    {
                        this.StateTrusteeship = vl;
                        return true;
                    }
                }
                return false;
            case "businesslicensecode":
                this.BusinessLicenseCode = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "businesslicenseimage":
                this.BusinessLicenseImage = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "legalpersonname":
                this.LegalPersonName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "legalpersoncardid":
                this.LegalPersonCardID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "legalpersoncardimage01":
                this.LegalPersonCardImage01 = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "legalpersoncardimage02":
                this.LegalPersonCardImage02 = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "address":
                this.Address = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "location_latitude":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (float.TryParse(value, out var vl))
                    {
                        this.Location_latitude = vl;
                        return true;
                    }
                }
                return false;
            case "location_longitude":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (float.TryParse(value, out var vl))
                    {
                        this.Location_longitude = vl;
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
            case "oid":
                this.OID = (long)Convert.ToDecimal(value);
                return;
            case "pid":
                this.PID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "orgkey":
                this.OrgKey = value == null ? null : value.ToString();
                return;
            case "orgname":
                this.OrgName = value == null ? null : value.ToString();
                return;
            case "orgchief":
                this.OrgChief = value == null ? null : value.ToString();
                return;
            case "orgremark":
                this.OrgRemark = value == null ? null : value.ToString();
                return;
            case "orglogo":
                this.OrgLogo = value == null ? null : value.ToString();
                return;
            case "orgintroduce":
                this.OrgIntroduce = value == null ? null : value.ToString();
                return;
            case "sort":
                this.Sort = (int)Convert.ToDecimal(value);
                return;
            case "icon":
                this.Icon = value == null ? null : value.ToString();
                return;
            case "stateexternal":
                if (value != null)
                {
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {
                        this.StateExternal = vl != 0;
                    }
                    else
                    {
                        this.StateExternal = Convert.ToBoolean(value);
                    }
                }
                return;
            case "statetrusteeship":
                if (value != null)
                {
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {
                        this.StateTrusteeship = vl != 0;
                    }
                    else
                    {
                        this.StateTrusteeship = Convert.ToBoolean(value);
                    }
                }
                return;
            case "businesslicensecode":
                this.BusinessLicenseCode = value == null ? null : value.ToString();
                return;
            case "businesslicenseimage":
                this.BusinessLicenseImage = value == null ? null : value.ToString();
                return;
            case "legalpersonname":
                this.LegalPersonName = value == null ? null : value.ToString();
                return;
            case "legalpersoncardid":
                this.LegalPersonCardID = value == null ? null : value.ToString();
                return;
            case "legalpersoncardimage01":
                this.LegalPersonCardImage01 = value == null ? null : value.ToString();
                return;
            case "legalpersoncardimage02":
                this.LegalPersonCardImage02 = value == null ? null : value.ToString();
                return;
            case "address":
                this.Address = value == null ? null : value.ToString();
                return;
            case "location_latitude":
                this.Location_latitude = Convert.ToSingle(value);
                return;
            case "location_longitude":
                this.Location_longitude = Convert.ToSingle(value);
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
            case _DataStruct_.OID:
                this.OID = Convert.ToInt64(value);
                return;
            case _DataStruct_.PID:
                this.PID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgKey:
                this.OrgKey = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrgName:
                this.OrgName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrgChief:
                this.OrgChief = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrgRemark:
                this.OrgRemark = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrgLogo:
                this.OrgLogo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrgIntroduce:
                this.OrgIntroduce = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Sort:
                this.Sort = Convert.ToInt32(value);
                return;
            case _DataStruct_.Icon:
                this.Icon = value == null ? null : value.ToString();
                return;
            case _DataStruct_.StateExternal:
                this.StateExternal = Convert.ToBoolean(value);
                return;
            case _DataStruct_.StateTrusteeship:
                this.StateTrusteeship = Convert.ToBoolean(value);
                return;
            case _DataStruct_.BusinessLicenseCode:
                this.BusinessLicenseCode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.BusinessLicenseImage:
                this.BusinessLicenseImage = value == null ? null : value.ToString();
                return;
            case _DataStruct_.LegalPersonName:
                this.LegalPersonName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.LegalPersonCardID:
                this.LegalPersonCardID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.LegalPersonCardImage01:
                this.LegalPersonCardImage01 = value == null ? null : value.ToString();
                return;
            case _DataStruct_.LegalPersonCardImage02:
                this.LegalPersonCardImage02 = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Address:
                this.Address = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Location_latitude:
                this.Location_latitude = Convert.ToSingle(value);
                return;
            case _DataStruct_.Location_longitude:
                this.Location_longitude = Convert.ToSingle(value);
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
            case "oid":
                return this.OID;
            case "pid":
                return this.PID;
            case "sitesid":
                return this.SiteSID;
            case "orgkey":
                return this.OrgKey;
            case "orgname":
                return this.OrgName;
            case "orgchief":
                return this.OrgChief;
            case "orgremark":
                return this.OrgRemark;
            case "orglogo":
                return this.OrgLogo;
            case "orgintroduce":
                return this.OrgIntroduce;
            case "sort":
                return this.Sort;
            case "icon":
                return this.Icon;
            case "stateexternal":
                return this.StateExternal;
            case "statetrusteeship":
                return this.StateTrusteeship;
            case "businesslicensecode":
                return this.BusinessLicenseCode;
            case "businesslicenseimage":
                return this.BusinessLicenseImage;
            case "legalpersonname":
                return this.LegalPersonName;
            case "legalpersoncardid":
                return this.LegalPersonCardID;
            case "legalpersoncardimage01":
                return this.LegalPersonCardImage01;
            case "legalpersoncardimage02":
                return this.LegalPersonCardImage02;
            case "address":
                return this.Address;
            case "location_latitude":
                return this.Location_latitude;
            case "location_longitude":
                return this.Location_longitude;
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
                case _DataStruct_.OID:
                    return this.OID;
                case _DataStruct_.PID:
                    return this.PID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.OrgKey:
                    return this.OrgKey;
                case _DataStruct_.OrgName:
                    return this.OrgName;
                case _DataStruct_.OrgChief:
                    return this.OrgChief;
                case _DataStruct_.OrgRemark:
                    return this.OrgRemark;
                case _DataStruct_.OrgLogo:
                    return this.OrgLogo;
                case _DataStruct_.OrgIntroduce:
                    return this.OrgIntroduce;
                case _DataStruct_.Sort:
                    return this.Sort;
                case _DataStruct_.Icon:
                    return this.Icon;
                case _DataStruct_.StateExternal:
                    return this.StateExternal;
                case _DataStruct_.StateTrusteeship:
                    return this.StateTrusteeship;
                case _DataStruct_.BusinessLicenseCode:
                    return this.BusinessLicenseCode;
                case _DataStruct_.BusinessLicenseImage:
                    return this.BusinessLicenseImage;
                case _DataStruct_.LegalPersonName:
                    return this.LegalPersonName;
                case _DataStruct_.LegalPersonCardID:
                    return this.LegalPersonCardID;
                case _DataStruct_.LegalPersonCardImage01:
                    return this.LegalPersonCardImage01;
                case _DataStruct_.LegalPersonCardImage02:
                    return this.LegalPersonCardImage02;
                case _DataStruct_.Address:
                    return this.Address;
                case _DataStruct_.Location_latitude:
                    return this.Location_latitude;
                case _DataStruct_.Location_longitude:
                    return this.Location_longitude;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(OrganizationData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as OrganizationData;
            if(sourceEntity == null)
                return;
            this._oID = sourceEntity._oID;
            this._pID = sourceEntity._pID;
            this._siteSID = sourceEntity._siteSID;
            this._orgKey = sourceEntity._orgKey;
            this._orgName = sourceEntity._orgName;
            this._orgChief = sourceEntity._orgChief;
            this._orgRemark = sourceEntity._orgRemark;
            this._orgLogo = sourceEntity._orgLogo;
            this._orgIntroduce = sourceEntity._orgIntroduce;
            this._sort = sourceEntity._sort;
            this._icon = sourceEntity._icon;
            this._stateExternal = sourceEntity._stateExternal;
            this._stateTrusteeship = sourceEntity._stateTrusteeship;
            this._businessLicenseCode = sourceEntity._businessLicenseCode;
            this._businessLicenseImage = sourceEntity._businessLicenseImage;
            this._legalPersonName = sourceEntity._legalPersonName;
            this._legalPersonCardID = sourceEntity._legalPersonCardID;
            this._legalPersonCardImage01 = sourceEntity._legalPersonCardImage01;
            this._legalPersonCardImage02 = sourceEntity._legalPersonCardImage02;
            this._address = sourceEntity._address;
            this._location_latitude = sourceEntity._location_latitude;
            this._location_longitude = sourceEntity._location_longitude;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(OrganizationData source)
        {
                this.OID = source.OID;
                this.PID = source.PID;
                this.SiteSID = source.SiteSID;
                this.OrgKey = source.OrgKey;
                this.OrgName = source.OrgName;
                this.OrgChief = source.OrgChief;
                this.OrgRemark = source.OrgRemark;
                this.OrgLogo = source.OrgLogo;
                this.OrgIntroduce = source.OrgIntroduce;
                this.Sort = source.Sort;
                this.Icon = source.Icon;
                this.StateExternal = source.StateExternal;
                this.StateTrusteeship = source.StateTrusteeship;
                this.BusinessLicenseCode = source.BusinessLicenseCode;
                this.BusinessLicenseImage = source.BusinessLicenseImage;
                this.LegalPersonName = source.LegalPersonName;
                this.LegalPersonCardID = source.LegalPersonCardID;
                this.LegalPersonCardImage01 = source.LegalPersonCardImage01;
                this.LegalPersonCardImage02 = source.LegalPersonCardImage02;
                this.Address = source.Address;
                this.Location_latitude = source.Location_latitude;
                this.Location_longitude = source.Location_longitude;
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
                OnOIDModified(subsist,false);
                OnPIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnOrgKeyModified(subsist,false);
                OnOrgNameModified(subsist,false);
                OnOrgChiefModified(subsist,false);
                OnOrgRemarkModified(subsist,false);
                OnOrgLogoModified(subsist,false);
                OnOrgIntroduceModified(subsist,false);
                OnSortModified(subsist,false);
                OnIconModified(subsist,false);
                OnStateExternalModified(subsist,false);
                OnStateTrusteeshipModified(subsist,false);
                OnBusinessLicenseCodeModified(subsist,false);
                OnBusinessLicenseImageModified(subsist,false);
                OnLegalPersonNameModified(subsist,false);
                OnLegalPersonCardIDModified(subsist,false);
                OnLegalPersonCardImage01Modified(subsist,false);
                OnLegalPersonCardImage02Modified(subsist,false);
                OnAddressModified(subsist,false);
                OnLocation_latitudeModified(subsist,false);
                OnLocation_longitudeModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnOIDModified(subsist,true);
                OnPIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnOrgKeyModified(subsist,true);
                OnOrgNameModified(subsist,true);
                OnOrgChiefModified(subsist,true);
                OnOrgRemarkModified(subsist,true);
                OnOrgLogoModified(subsist,true);
                OnOrgIntroduceModified(subsist,true);
                OnSortModified(subsist,true);
                OnIconModified(subsist,true);
                OnStateExternalModified(subsist,true);
                OnStateTrusteeshipModified(subsist,true);
                OnBusinessLicenseCodeModified(subsist,true);
                OnBusinessLicenseImageModified(subsist,true);
                OnLegalPersonNameModified(subsist,true);
                OnLegalPersonCardIDModified(subsist,true);
                OnLegalPersonCardImage01Modified(subsist,true);
                OnLegalPersonCardImage02Modified(subsist,true);
                OnAddressModified(subsist,true);
                OnLocation_latitudeModified(subsist,true);
                OnLocation_longitudeModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[22] > 0)
            {
                OnOIDModified(subsist,modifieds[_DataStruct_.Real_OID] == 1);
                OnPIDModified(subsist,modifieds[_DataStruct_.Real_PID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnOrgKeyModified(subsist,modifieds[_DataStruct_.Real_OrgKey] == 1);
                OnOrgNameModified(subsist,modifieds[_DataStruct_.Real_OrgName] == 1);
                OnOrgChiefModified(subsist,modifieds[_DataStruct_.Real_OrgChief] == 1);
                OnOrgRemarkModified(subsist,modifieds[_DataStruct_.Real_OrgRemark] == 1);
                OnOrgLogoModified(subsist,modifieds[_DataStruct_.Real_OrgLogo] == 1);
                OnOrgIntroduceModified(subsist,modifieds[_DataStruct_.Real_OrgIntroduce] == 1);
                OnSortModified(subsist,modifieds[_DataStruct_.Real_Sort] == 1);
                OnIconModified(subsist,modifieds[_DataStruct_.Real_Icon] == 1);
                OnStateExternalModified(subsist,modifieds[_DataStruct_.Real_StateExternal] == 1);
                OnStateTrusteeshipModified(subsist,modifieds[_DataStruct_.Real_StateTrusteeship] == 1);
                OnBusinessLicenseCodeModified(subsist,modifieds[_DataStruct_.Real_BusinessLicenseCode] == 1);
                OnBusinessLicenseImageModified(subsist,modifieds[_DataStruct_.Real_BusinessLicenseImage] == 1);
                OnLegalPersonNameModified(subsist,modifieds[_DataStruct_.Real_LegalPersonName] == 1);
                OnLegalPersonCardIDModified(subsist,modifieds[_DataStruct_.Real_LegalPersonCardID] == 1);
                OnLegalPersonCardImage01Modified(subsist,modifieds[_DataStruct_.Real_LegalPersonCardImage01] == 1);
                OnLegalPersonCardImage02Modified(subsist,modifieds[_DataStruct_.Real_LegalPersonCardImage02] == 1);
                OnAddressModified(subsist,modifieds[_DataStruct_.Real_Address] == 1);
                OnLocation_latitudeModified(subsist,modifieds[_DataStruct_.Real_Location_latitude] == 1);
                OnLocation_longitudeModified(subsist,modifieds[_DataStruct_.Real_Location_longitude] == 1);
            }
        }

        /// <summary>
        /// 组织编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 上级组织编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 站点标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSiteSIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 全局标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgKeyModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 组织名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 首席执行官修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgChiefModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 组织备注修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgRemarkModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// Logo修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgLogoModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 组织简介修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgIntroduceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 序号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSortModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 小图标修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIconModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 外部状态修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnStateExternalModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 托管状态修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnStateTrusteeshipModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 营业执照代码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBusinessLicenseCodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 营业执照图片修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBusinessLicenseImageModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 法人姓名修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLegalPersonNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 法人身份证号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLegalPersonCardIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 法人证件图像01修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLegalPersonCardImage01Modified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 法人证件图像02修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLegalPersonCardImage02Modified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 地理地址修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAddressModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 地理纬度修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLocation_latitudeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 地理经度修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLocation_longitudeModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"Organization";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"组织机构";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"组织机构";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "OID";
            
            
            /// <summary>
            /// 组织编号的数字标识
            /// </summary>
            public const byte OID = 3;
            
            /// <summary>
            /// 组织编号的实时记录顺序
            /// </summary>
            public const int Real_OID = 0;

            /// <summary>
            /// 上级组织编号的数字标识
            /// </summary>
            public const byte PID = 4;
            
            /// <summary>
            /// 上级组织编号的实时记录顺序
            /// </summary>
            public const int Real_PID = 1;

            /// <summary>
            /// 站点标识的数字标识
            /// </summary>
            public const byte SiteSID = 5;
            
            /// <summary>
            /// 站点标识的实时记录顺序
            /// </summary>
            public const int Real_SiteSID = 2;

            /// <summary>
            /// 全局标识的数字标识
            /// </summary>
            public const byte OrgKey = 6;
            
            /// <summary>
            /// 全局标识的实时记录顺序
            /// </summary>
            public const int Real_OrgKey = 3;

            /// <summary>
            /// 组织名称的数字标识
            /// </summary>
            public const byte OrgName = 7;
            
            /// <summary>
            /// 组织名称的实时记录顺序
            /// </summary>
            public const int Real_OrgName = 4;

            /// <summary>
            /// 首席执行官的数字标识
            /// </summary>
            public const byte OrgChief = 8;
            
            /// <summary>
            /// 首席执行官的实时记录顺序
            /// </summary>
            public const int Real_OrgChief = 5;

            /// <summary>
            /// 组织备注的数字标识
            /// </summary>
            public const byte OrgRemark = 9;
            
            /// <summary>
            /// 组织备注的实时记录顺序
            /// </summary>
            public const int Real_OrgRemark = 6;

            /// <summary>
            /// Logo的数字标识
            /// </summary>
            public const byte OrgLogo = 10;
            
            /// <summary>
            /// Logo的实时记录顺序
            /// </summary>
            public const int Real_OrgLogo = 7;

            /// <summary>
            /// 组织简介的数字标识
            /// </summary>
            public const byte OrgIntroduce = 11;
            
            /// <summary>
            /// 组织简介的实时记录顺序
            /// </summary>
            public const int Real_OrgIntroduce = 8;

            /// <summary>
            /// 序号的数字标识
            /// </summary>
            public const byte Sort = 12;
            
            /// <summary>
            /// 序号的实时记录顺序
            /// </summary>
            public const int Real_Sort = 9;

            /// <summary>
            /// 小图标的数字标识
            /// </summary>
            public const byte Icon = 13;
            
            /// <summary>
            /// 小图标的实时记录顺序
            /// </summary>
            public const int Real_Icon = 10;

            /// <summary>
            /// 外部状态的数字标识
            /// </summary>
            public const byte StateExternal = 14;
            
            /// <summary>
            /// 外部状态的实时记录顺序
            /// </summary>
            public const int Real_StateExternal = 11;

            /// <summary>
            /// 托管状态的数字标识
            /// </summary>
            public const byte StateTrusteeship = 15;
            
            /// <summary>
            /// 托管状态的实时记录顺序
            /// </summary>
            public const int Real_StateTrusteeship = 12;

            /// <summary>
            /// 营业执照代码的数字标识
            /// </summary>
            public const byte BusinessLicenseCode = 16;
            
            /// <summary>
            /// 营业执照代码的实时记录顺序
            /// </summary>
            public const int Real_BusinessLicenseCode = 13;

            /// <summary>
            /// 营业执照图片的数字标识
            /// </summary>
            public const byte BusinessLicenseImage = 17;
            
            /// <summary>
            /// 营业执照图片的实时记录顺序
            /// </summary>
            public const int Real_BusinessLicenseImage = 14;

            /// <summary>
            /// 法人姓名的数字标识
            /// </summary>
            public const byte LegalPersonName = 18;
            
            /// <summary>
            /// 法人姓名的实时记录顺序
            /// </summary>
            public const int Real_LegalPersonName = 15;

            /// <summary>
            /// 法人身份证号的数字标识
            /// </summary>
            public const byte LegalPersonCardID = 19;
            
            /// <summary>
            /// 法人身份证号的实时记录顺序
            /// </summary>
            public const int Real_LegalPersonCardID = 16;

            /// <summary>
            /// 法人证件图像01的数字标识
            /// </summary>
            public const byte LegalPersonCardImage01 = 20;
            
            /// <summary>
            /// 法人证件图像01的实时记录顺序
            /// </summary>
            public const int Real_LegalPersonCardImage01 = 17;

            /// <summary>
            /// 法人证件图像02的数字标识
            /// </summary>
            public const byte LegalPersonCardImage02 = 21;
            
            /// <summary>
            /// 法人证件图像02的实时记录顺序
            /// </summary>
            public const int Real_LegalPersonCardImage02 = 18;

            /// <summary>
            /// 地理地址的数字标识
            /// </summary>
            public const byte Address = 22;
            
            /// <summary>
            /// 地理地址的实时记录顺序
            /// </summary>
            public const int Real_Address = 19;

            /// <summary>
            /// 地理纬度的数字标识
            /// </summary>
            public const byte Location_latitude = 23;
            
            /// <summary>
            /// 地理纬度的实时记录顺序
            /// </summary>
            public const int Real_Location_latitude = 20;

            /// <summary>
            /// 地理经度的数字标识
            /// </summary>
            public const byte Location_longitude = 24;
            
            /// <summary>
            /// 地理经度的实时记录顺序
            /// </summary>
            public const int Real_Location_longitude = 21;

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
                        Real_OID,
                        new PropertySturct
                        {
                            Index        = OID,
                            Name         = "OID",
                            Title        = "组织编号",
                            Caption      = @"组织编号",
                            Description  = @"组织编号",
                            ColumnName   = "OID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PID,
                        new PropertySturct
                        {
                            Index        = PID,
                            Name         = "PID",
                            Title        = "上级组织编号",
                            Caption      = @"上级组织编号",
                            Description  = @"上级组织编号",
                            ColumnName   = "PID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SiteSID,
                        new PropertySturct
                        {
                            Index        = SiteSID,
                            Name         = "SiteSID",
                            Title        = "站点标识",
                            Caption      = @"站点标识",
                            Description  = @"站点标识",
                            ColumnName   = "SiteSID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrgKey,
                        new PropertySturct
                        {
                            Index        = OrgKey,
                            Name         = "OrgKey",
                            Title        = "全局标识",
                            Caption      = @"全局标识",
                            Description  = @"全局标识",
                            ColumnName   = "OrgKey",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrgName,
                        new PropertySturct
                        {
                            Index        = OrgName,
                            Name         = "OrgName",
                            Title        = "组织名称",
                            Caption      = @"组织名称",
                            Description  = @"组织名称",
                            ColumnName   = "OrgName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrgChief,
                        new PropertySturct
                        {
                            Index        = OrgChief,
                            Name         = "OrgChief",
                            Title        = "首席执行官",
                            Caption      = @"首席执行官",
                            Description  = @"首席执行官",
                            ColumnName   = "OrgChief",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrgRemark,
                        new PropertySturct
                        {
                            Index        = OrgRemark,
                            Name         = "OrgRemark",
                            Title        = "组织备注",
                            Caption      = @"组织备注",
                            Description  = @"组织备注",
                            ColumnName   = "OrgRemark",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrgLogo,
                        new PropertySturct
                        {
                            Index        = OrgLogo,
                            Name         = "OrgLogo",
                            Title        = "Logo",
                            Caption      = @"Logo",
                            Description  = @"Logo",
                            ColumnName   = "OrgLogo",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrgIntroduce,
                        new PropertySturct
                        {
                            Index        = OrgIntroduce,
                            Name         = "OrgIntroduce",
                            Title        = "组织简介",
                            Caption      = @"组织简介",
                            Description  = @"组织简介",
                            ColumnName   = "OrgIntroduce",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Sort,
                        new PropertySturct
                        {
                            Index        = Sort,
                            Name         = "Sort",
                            Title        = "序号",
                            Caption      = @"序号",
                            Description  = @"序号",
                            ColumnName   = "Sort",
                            PropertyType = typeof(int),
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
                            Title        = "小图标",
                            Caption      = @"小图标",
                            Description  = @"小图标",
                            ColumnName   = "Icon",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_StateExternal,
                        new PropertySturct
                        {
                            Index        = StateExternal,
                            Name         = "StateExternal",
                            Title        = "外部状态",
                            Caption      = @"外部状态",
                            Description  = @"开放查询需求",
                            ColumnName   = "StateExternal",
                            PropertyType = typeof(bool),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_StateTrusteeship,
                        new PropertySturct
                        {
                            Index        = StateTrusteeship,
                            Name         = "StateTrusteeship",
                            Title        = "托管状态",
                            Caption      = @"托管状态",
                            Description  = @"管理主体与业务主体不相同的情况使用",
                            ColumnName   = "StateTrusteeship",
                            PropertyType = typeof(bool),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_BusinessLicenseCode,
                        new PropertySturct
                        {
                            Index        = BusinessLicenseCode,
                            Name         = "BusinessLicenseCode",
                            Title        = "营业执照代码",
                            Caption      = @"营业执照代码",
                            Description  = @"营业执照代码",
                            ColumnName   = "BusinessLicenseCode",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_BusinessLicenseImage,
                        new PropertySturct
                        {
                            Index        = BusinessLicenseImage,
                            Name         = "BusinessLicenseImage",
                            Title        = "营业执照图片",
                            Caption      = @"营业执照图片",
                            Description  = @"营业执照图片",
                            ColumnName   = "BusinessLicenseImage",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LegalPersonName,
                        new PropertySturct
                        {
                            Index        = LegalPersonName,
                            Name         = "LegalPersonName",
                            Title        = "法人姓名",
                            Caption      = @"法人姓名",
                            Description  = @"法人姓名",
                            ColumnName   = "LegalPersonName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LegalPersonCardID,
                        new PropertySturct
                        {
                            Index        = LegalPersonCardID,
                            Name         = "LegalPersonCardID",
                            Title        = "法人身份证号",
                            Caption      = @"法人身份证号",
                            Description  = @"法人身份证号",
                            ColumnName   = "LegalPersonCardID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LegalPersonCardImage01,
                        new PropertySturct
                        {
                            Index        = LegalPersonCardImage01,
                            Name         = "LegalPersonCardImage01",
                            Title        = "法人证件图像01",
                            Caption      = @"法人证件图像01",
                            Description  = @"法人证件图像01",
                            ColumnName   = "LegalPersonCardImage01",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LegalPersonCardImage02,
                        new PropertySturct
                        {
                            Index        = LegalPersonCardImage02,
                            Name         = "LegalPersonCardImage02",
                            Title        = "法人证件图像02",
                            Caption      = @"法人证件图像02",
                            Description  = @"法人证件图像02",
                            ColumnName   = "LegalPersonCardImage02",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Address,
                        new PropertySturct
                        {
                            Index        = Address,
                            Name         = "Address",
                            Title        = "地理地址",
                            Caption      = @"地理地址",
                            Description  = @"地理地址",
                            ColumnName   = "Address",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Location_latitude,
                        new PropertySturct
                        {
                            Index        = Location_latitude,
                            Name         = "Location_latitude",
                            Title        = "地理纬度",
                            Caption      = @"地理纬度",
                            Description  = @"地理纬度",
                            ColumnName   = "Location_latitude",
                            PropertyType = typeof(float),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Location_longitude,
                        new PropertySturct
                        {
                            Index        = Location_longitude,
                            Name         = "Location_longitude",
                            Title        = "地理经度",
                            Caption      = @"地理经度",
                            Description  = @"地理经度",
                            ColumnName   = "Location_longitude",
                            PropertyType = typeof(float),
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