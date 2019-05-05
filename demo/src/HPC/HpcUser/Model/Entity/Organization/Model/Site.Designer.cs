/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 16:30:31*/
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
    /// 站点
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class SiteData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public SiteData()
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
        public void ChangePrimaryKey(long sID)
        {
            _sID = sID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _sID;

        partial void OnSIDGet();

        partial void OnSIDSet(ref long value);

        partial void OnSIDLoad(ref long value);

        partial void OnSIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("SID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long SID
        {
            get
            {
                OnSIDGet();
                return this._sID;
            }
            set
            {
                if(this._sID == value)
                    return;
                //if(this._sID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnSIDSet(ref value);
                this._sID = value;
                this.OnPropertyChanged(_DataStruct_.Real_SID);
                OnSIDSeted();
            }
        }
        /// <summary>
        /// 站点标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _siteID;

        partial void OnSiteIDGet();

        partial void OnSiteIDSet(ref string value);

        partial void OnSiteIDSeted();

        
        /// <summary>
        /// 站点标识
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SiteID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"站点标识")]
        public  string SiteID
        {
            get
            {
                OnSiteIDGet();
                return this._siteID;
            }
            set
            {
                if(this._siteID == value)
                    return;
                OnSiteIDSet(ref value);
                this._siteID = value;
                OnSiteIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SiteID);
            }
        }
        /// <summary>
        /// 站点全局标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _siteKey;

        partial void OnSiteKeyGet();

        partial void OnSiteKeySet(ref string value);

        partial void OnSiteKeySeted();

        
        /// <summary>
        /// 站点全局标识
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SiteKey", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"站点全局标识")]
        public  string SiteKey
        {
            get
            {
                OnSiteKeyGet();
                return this._siteKey;
            }
            set
            {
                if(this._siteKey == value)
                    return;
                OnSiteKeySet(ref value);
                this._siteKey = value;
                OnSiteKeySeted();
                this.OnPropertyChanged(_DataStruct_.Real_SiteKey);
            }
        }
        /// <summary>
        /// 站点全称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _siteNameWhole;

        partial void OnSiteNameWholeGet();

        partial void OnSiteNameWholeSet(ref string value);

        partial void OnSiteNameWholeSeted();

        
        /// <summary>
        /// 站点全称
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SiteNameWhole", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"站点全称")]
        public  string SiteNameWhole
        {
            get
            {
                OnSiteNameWholeGet();
                return this._siteNameWhole;
            }
            set
            {
                if(this._siteNameWhole == value)
                    return;
                OnSiteNameWholeSet(ref value);
                this._siteNameWhole = value;
                OnSiteNameWholeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SiteNameWhole);
            }
        }
        /// <summary>
        /// 站点简称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _siteNameShort;

        partial void OnSiteNameShortGet();

        partial void OnSiteNameShortSet(ref string value);

        partial void OnSiteNameShortSeted();

        
        /// <summary>
        /// 站点简称
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SiteNameShort", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"站点简称")]
        public  string SiteNameShort
        {
            get
            {
                OnSiteNameShortGet();
                return this._siteNameShort;
            }
            set
            {
                if(this._siteNameShort == value)
                    return;
                OnSiteNameShortSet(ref value);
                this._siteNameShort = value;
                OnSiteNameShortSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SiteNameShort);
            }
        }
        /// <summary>
        /// LOGO
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _siteLogo;

        partial void OnSiteLogoGet();

        partial void OnSiteLogoSet(ref string value);

        partial void OnSiteLogoSeted();

        
        /// <summary>
        /// LOGO
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SiteLogo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"LOGO")]
        public  string SiteLogo
        {
            get
            {
                OnSiteLogoGet();
                return this._siteLogo;
            }
            set
            {
                if(this._siteLogo == value)
                    return;
                OnSiteLogoSet(ref value);
                this._siteLogo = value;
                OnSiteLogoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SiteLogo);
            }
        }
        /// <summary>
        /// 站点介绍
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _siteIntroduce;

        partial void OnSiteIntroduceGet();

        partial void OnSiteIntroduceSet(ref string value);

        partial void OnSiteIntroduceSeted();

        
        /// <summary>
        /// 站点介绍
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SiteIntroduce", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"站点介绍")]
        public  string SiteIntroduce
        {
            get
            {
                OnSiteIntroduceGet();
                return this._siteIntroduce;
            }
            set
            {
                if(this._siteIntroduce == value)
                    return;
                OnSiteIntroduceSet(ref value);
                this._siteIntroduce = value;
                OnSiteIntroduceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SiteIntroduce);
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
        /// 可存储100个字符.合理长度应不大于100.
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
        /// 营业执照形象
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _businessLicenseImage;

        partial void OnBusinessLicenseImageGet();

        partial void OnBusinessLicenseImageSet(ref string value);

        partial void OnBusinessLicenseImageSeted();

        
        /// <summary>
        /// 营业执照形象
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("BusinessLicenseImage", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"营业执照形象")]
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
        /// 法人名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _legalPersonName;

        partial void OnLegalPersonNameGet();

        partial void OnLegalPersonNameSet(ref string value);

        partial void OnLegalPersonNameSeted();

        
        /// <summary>
        /// 法人名称
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LegalPersonName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"法人名称")]
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
        /// 可存储40个字符.合理长度应不大于40.
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
        /// 法人身份证图像正面
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _legalPersonCardImage01;

        partial void OnLegalPersonCardImage01Get();

        partial void OnLegalPersonCardImage01Set(ref string value);

        partial void OnLegalPersonCardImage01Seted();

        
        /// <summary>
        /// 法人身份证图像正面
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LegalPersonCardImage01", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"法人身份证图像正面")]
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
        /// 法人身份证图像反面
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _legalPersonCardImage02;

        partial void OnLegalPersonCardImage02Get();

        partial void OnLegalPersonCardImage02Set(ref string value);

        partial void OnLegalPersonCardImage02Seted();

        
        /// <summary>
        /// 法人身份证图像反面
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LegalPersonCardImage02", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"法人身份证图像反面")]
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
        /// 备注
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _remarks;

        partial void OnRemarksGet();

        partial void OnRemarksSet(ref string value);

        partial void OnRemarksSeted();

        
        /// <summary>
        /// 备注
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Remarks", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"备注")]
        public  string Remarks
        {
            get
            {
                OnRemarksGet();
                return this._remarks;
            }
            set
            {
                if(this._remarks == value)
                    return;
                OnRemarksSet(ref value);
                this._remarks = value;
                OnRemarksSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Remarks);
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
                return this.SID;
            }
            set
            {
                this.SID = value;
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
            case "sid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.SID = vl;
                        return true;
                    }
                }
                return false;
            case "siteid":
                this.SiteID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "sitekey":
                this.SiteKey = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "sitenamewhole":
                this.SiteNameWhole = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "sitenameshort":
                this.SiteNameShort = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "sitelogo":
                this.SiteLogo = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "siteintroduce":
                this.SiteIntroduce = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
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
            case "remarks":
                this.Remarks = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "sid":
                this.SID = (long)Convert.ToDecimal(value);
                return;
            case "siteid":
                this.SiteID = value == null ? null : value.ToString();
                return;
            case "sitekey":
                this.SiteKey = value == null ? null : value.ToString();
                return;
            case "sitenamewhole":
                this.SiteNameWhole = value == null ? null : value.ToString();
                return;
            case "sitenameshort":
                this.SiteNameShort = value == null ? null : value.ToString();
                return;
            case "sitelogo":
                this.SiteLogo = value == null ? null : value.ToString();
                return;
            case "siteintroduce":
                this.SiteIntroduce = value == null ? null : value.ToString();
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
            case "remarks":
                this.Remarks = value == null ? null : value.ToString();
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
            case _DataStruct_.SID:
                this.SID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteID:
                this.SiteID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.SiteKey:
                this.SiteKey = value == null ? null : value.ToString();
                return;
            case _DataStruct_.SiteNameWhole:
                this.SiteNameWhole = value == null ? null : value.ToString();
                return;
            case _DataStruct_.SiteNameShort:
                this.SiteNameShort = value == null ? null : value.ToString();
                return;
            case _DataStruct_.SiteLogo:
                this.SiteLogo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.SiteIntroduce:
                this.SiteIntroduce = value == null ? null : value.ToString();
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
            case _DataStruct_.Remarks:
                this.Remarks = value == null ? null : value.ToString();
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
            case "sid":
                return this.SID;
            case "siteid":
                return this.SiteID;
            case "sitekey":
                return this.SiteKey;
            case "sitenamewhole":
                return this.SiteNameWhole;
            case "sitenameshort":
                return this.SiteNameShort;
            case "sitelogo":
                return this.SiteLogo;
            case "siteintroduce":
                return this.SiteIntroduce;
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
            case "remarks":
                return this.Remarks;
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
                case _DataStruct_.SID:
                    return this.SID;
                case _DataStruct_.SiteID:
                    return this.SiteID;
                case _DataStruct_.SiteKey:
                    return this.SiteKey;
                case _DataStruct_.SiteNameWhole:
                    return this.SiteNameWhole;
                case _DataStruct_.SiteNameShort:
                    return this.SiteNameShort;
                case _DataStruct_.SiteLogo:
                    return this.SiteLogo;
                case _DataStruct_.SiteIntroduce:
                    return this.SiteIntroduce;
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
                case _DataStruct_.Remarks:
                    return this.Remarks;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(SiteData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as SiteData;
            if(sourceEntity == null)
                return;
            this._sID = sourceEntity._sID;
            this._siteID = sourceEntity._siteID;
            this._siteKey = sourceEntity._siteKey;
            this._siteNameWhole = sourceEntity._siteNameWhole;
            this._siteNameShort = sourceEntity._siteNameShort;
            this._siteLogo = sourceEntity._siteLogo;
            this._siteIntroduce = sourceEntity._siteIntroduce;
            this._businessLicenseCode = sourceEntity._businessLicenseCode;
            this._businessLicenseImage = sourceEntity._businessLicenseImage;
            this._legalPersonName = sourceEntity._legalPersonName;
            this._legalPersonCardID = sourceEntity._legalPersonCardID;
            this._legalPersonCardImage01 = sourceEntity._legalPersonCardImage01;
            this._legalPersonCardImage02 = sourceEntity._legalPersonCardImage02;
            this._remarks = sourceEntity._remarks;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(SiteData source)
        {
                this.SID = source.SID;
                this.SiteID = source.SiteID;
                this.SiteKey = source.SiteKey;
                this.SiteNameWhole = source.SiteNameWhole;
                this.SiteNameShort = source.SiteNameShort;
                this.SiteLogo = source.SiteLogo;
                this.SiteIntroduce = source.SiteIntroduce;
                this.BusinessLicenseCode = source.BusinessLicenseCode;
                this.BusinessLicenseImage = source.BusinessLicenseImage;
                this.LegalPersonName = source.LegalPersonName;
                this.LegalPersonCardID = source.LegalPersonCardID;
                this.LegalPersonCardImage01 = source.LegalPersonCardImage01;
                this.LegalPersonCardImage02 = source.LegalPersonCardImage02;
                this.Remarks = source.Remarks;
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
                OnSIDModified(subsist,false);
                OnSiteIDModified(subsist,false);
                OnSiteKeyModified(subsist,false);
                OnSiteNameWholeModified(subsist,false);
                OnSiteNameShortModified(subsist,false);
                OnSiteLogoModified(subsist,false);
                OnSiteIntroduceModified(subsist,false);
                OnBusinessLicenseCodeModified(subsist,false);
                OnBusinessLicenseImageModified(subsist,false);
                OnLegalPersonNameModified(subsist,false);
                OnLegalPersonCardIDModified(subsist,false);
                OnLegalPersonCardImage01Modified(subsist,false);
                OnLegalPersonCardImage02Modified(subsist,false);
                OnRemarksModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnSIDModified(subsist,true);
                OnSiteIDModified(subsist,true);
                OnSiteKeyModified(subsist,true);
                OnSiteNameWholeModified(subsist,true);
                OnSiteNameShortModified(subsist,true);
                OnSiteLogoModified(subsist,true);
                OnSiteIntroduceModified(subsist,true);
                OnBusinessLicenseCodeModified(subsist,true);
                OnBusinessLicenseImageModified(subsist,true);
                OnLegalPersonNameModified(subsist,true);
                OnLegalPersonCardIDModified(subsist,true);
                OnLegalPersonCardImage01Modified(subsist,true);
                OnLegalPersonCardImage02Modified(subsist,true);
                OnRemarksModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[14] > 0)
            {
                OnSIDModified(subsist,modifieds[_DataStruct_.Real_SID] == 1);
                OnSiteIDModified(subsist,modifieds[_DataStruct_.Real_SiteID] == 1);
                OnSiteKeyModified(subsist,modifieds[_DataStruct_.Real_SiteKey] == 1);
                OnSiteNameWholeModified(subsist,modifieds[_DataStruct_.Real_SiteNameWhole] == 1);
                OnSiteNameShortModified(subsist,modifieds[_DataStruct_.Real_SiteNameShort] == 1);
                OnSiteLogoModified(subsist,modifieds[_DataStruct_.Real_SiteLogo] == 1);
                OnSiteIntroduceModified(subsist,modifieds[_DataStruct_.Real_SiteIntroduce] == 1);
                OnBusinessLicenseCodeModified(subsist,modifieds[_DataStruct_.Real_BusinessLicenseCode] == 1);
                OnBusinessLicenseImageModified(subsist,modifieds[_DataStruct_.Real_BusinessLicenseImage] == 1);
                OnLegalPersonNameModified(subsist,modifieds[_DataStruct_.Real_LegalPersonName] == 1);
                OnLegalPersonCardIDModified(subsist,modifieds[_DataStruct_.Real_LegalPersonCardID] == 1);
                OnLegalPersonCardImage01Modified(subsist,modifieds[_DataStruct_.Real_LegalPersonCardImage01] == 1);
                OnLegalPersonCardImage02Modified(subsist,modifieds[_DataStruct_.Real_LegalPersonCardImage02] == 1);
                OnRemarksModified(subsist,modifieds[_DataStruct_.Real_Remarks] == 1);
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
        partial void OnSIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 站点标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSiteIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 站点全局标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSiteKeyModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 站点全称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSiteNameWholeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 站点简称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSiteNameShortModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// LOGO修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSiteLogoModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 站点介绍修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSiteIntroduceModified(EntitySubsist subsist,bool isModified);

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
        /// 营业执照形象修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBusinessLicenseImageModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 法人名称修改的后期处理(保存前)
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
        /// 法人身份证图像正面修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLegalPersonCardImage01Modified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 法人身份证图像反面修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLegalPersonCardImage02Modified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 备注修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRemarksModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"Site";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"站点";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"站点";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "SID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte SID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_SID = 0;

            /// <summary>
            /// 站点标识的数字标识
            /// </summary>
            public const byte SiteID = 2;
            
            /// <summary>
            /// 站点标识的实时记录顺序
            /// </summary>
            public const int Real_SiteID = 1;

            /// <summary>
            /// 站点全局标识的数字标识
            /// </summary>
            public const byte SiteKey = 3;
            
            /// <summary>
            /// 站点全局标识的实时记录顺序
            /// </summary>
            public const int Real_SiteKey = 2;

            /// <summary>
            /// 站点全称的数字标识
            /// </summary>
            public const byte SiteNameWhole = 4;
            
            /// <summary>
            /// 站点全称的实时记录顺序
            /// </summary>
            public const int Real_SiteNameWhole = 3;

            /// <summary>
            /// 站点简称的数字标识
            /// </summary>
            public const byte SiteNameShort = 5;
            
            /// <summary>
            /// 站点简称的实时记录顺序
            /// </summary>
            public const int Real_SiteNameShort = 4;

            /// <summary>
            /// LOGO的数字标识
            /// </summary>
            public const byte SiteLogo = 6;
            
            /// <summary>
            /// LOGO的实时记录顺序
            /// </summary>
            public const int Real_SiteLogo = 5;

            /// <summary>
            /// 站点介绍的数字标识
            /// </summary>
            public const byte SiteIntroduce = 7;
            
            /// <summary>
            /// 站点介绍的实时记录顺序
            /// </summary>
            public const int Real_SiteIntroduce = 6;

            /// <summary>
            /// 营业执照代码的数字标识
            /// </summary>
            public const byte BusinessLicenseCode = 8;
            
            /// <summary>
            /// 营业执照代码的实时记录顺序
            /// </summary>
            public const int Real_BusinessLicenseCode = 7;

            /// <summary>
            /// 营业执照形象的数字标识
            /// </summary>
            public const byte BusinessLicenseImage = 9;
            
            /// <summary>
            /// 营业执照形象的实时记录顺序
            /// </summary>
            public const int Real_BusinessLicenseImage = 8;

            /// <summary>
            /// 法人名称的数字标识
            /// </summary>
            public const byte LegalPersonName = 10;
            
            /// <summary>
            /// 法人名称的实时记录顺序
            /// </summary>
            public const int Real_LegalPersonName = 9;

            /// <summary>
            /// 法人身份证号的数字标识
            /// </summary>
            public const byte LegalPersonCardID = 11;
            
            /// <summary>
            /// 法人身份证号的实时记录顺序
            /// </summary>
            public const int Real_LegalPersonCardID = 10;

            /// <summary>
            /// 法人身份证图像正面的数字标识
            /// </summary>
            public const byte LegalPersonCardImage01 = 12;
            
            /// <summary>
            /// 法人身份证图像正面的实时记录顺序
            /// </summary>
            public const int Real_LegalPersonCardImage01 = 11;

            /// <summary>
            /// 法人身份证图像反面的数字标识
            /// </summary>
            public const byte LegalPersonCardImage02 = 13;
            
            /// <summary>
            /// 法人身份证图像反面的实时记录顺序
            /// </summary>
            public const int Real_LegalPersonCardImage02 = 12;

            /// <summary>
            /// 备注的数字标识
            /// </summary>
            public const byte Remarks = 14;
            
            /// <summary>
            /// 备注的实时记录顺序
            /// </summary>
            public const int Real_Remarks = 13;

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
                        Real_SID,
                        new PropertySturct
                        {
                            Index        = SID,
                            Name         = "SID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "SID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SiteID,
                        new PropertySturct
                        {
                            Index        = SiteID,
                            Name         = "SiteID",
                            Title        = "站点标识",
                            Caption      = @"站点标识",
                            Description  = @"站点标识",
                            ColumnName   = "SiteID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SiteKey,
                        new PropertySturct
                        {
                            Index        = SiteKey,
                            Name         = "SiteKey",
                            Title        = "站点全局标识",
                            Caption      = @"站点全局标识",
                            Description  = @"站点全局标识",
                            ColumnName   = "SiteKey",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SiteNameWhole,
                        new PropertySturct
                        {
                            Index        = SiteNameWhole,
                            Name         = "SiteNameWhole",
                            Title        = "站点全称",
                            Caption      = @"站点全称",
                            Description  = @"站点全称",
                            ColumnName   = "SiteNameWhole",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SiteNameShort,
                        new PropertySturct
                        {
                            Index        = SiteNameShort,
                            Name         = "SiteNameShort",
                            Title        = "站点简称",
                            Caption      = @"站点简称",
                            Description  = @"站点简称",
                            ColumnName   = "SiteNameShort",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SiteLogo,
                        new PropertySturct
                        {
                            Index        = SiteLogo,
                            Name         = "SiteLogo",
                            Title        = "LOGO",
                            Caption      = @"LOGO",
                            Description  = @"LOGO",
                            ColumnName   = "SiteLogo",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SiteIntroduce,
                        new PropertySturct
                        {
                            Index        = SiteIntroduce,
                            Name         = "SiteIntroduce",
                            Title        = "站点介绍",
                            Caption      = @"站点介绍",
                            Description  = @"站点介绍",
                            ColumnName   = "SiteIntroduce",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
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
                            Title        = "营业执照形象",
                            Caption      = @"营业执照形象",
                            Description  = @"营业执照形象",
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
                            Title        = "法人名称",
                            Caption      = @"法人名称",
                            Description  = @"法人名称",
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
                            Title        = "法人身份证图像正面",
                            Caption      = @"法人身份证图像正面",
                            Description  = @"法人身份证图像正面",
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
                            Title        = "法人身份证图像反面",
                            Caption      = @"法人身份证图像反面",
                            Description  = @"法人身份证图像反面",
                            ColumnName   = "LegalPersonCardImage02",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Remarks,
                        new PropertySturct
                        {
                            Index        = Remarks,
                            Name         = "Remarks",
                            Title        = "备注",
                            Caption      = @"备注",
                            Description  = @"备注",
                            ColumnName   = "Remarks",
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