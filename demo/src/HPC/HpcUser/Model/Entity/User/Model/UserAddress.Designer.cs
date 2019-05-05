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
    /// 用户地址
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class UserAddressData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserAddressData()
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
        public void ChangePrimaryKey(long aID)
        {
            _aID = aID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _aID;

        partial void OnAIDGet();

        partial void OnAIDSet(ref long value);

        partial void OnAIDLoad(ref long value);

        partial void OnAIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("AID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long AID
        {
            get
            {
                OnAIDGet();
                return this._aID;
            }
            set
            {
                if(this._aID == value)
                    return;
                //if(this._aID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnAIDSet(ref value);
                this._aID = value;
                this.OnPropertyChanged(_DataStruct_.Real_AID);
                OnAIDSeted();
            }
        }
        /// <summary>
        /// 组织标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _userUID;

        partial void OnUserUIDGet();

        partial void OnUserUIDSet(ref long value);

        partial void OnUserUIDSeted();

        
        /// <summary>
        /// 组织标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("UserUID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织标识")]
        public  long UserUID
        {
            get
            {
                OnUserUIDGet();
                return this._userUID;
            }
            set
            {
                if(this._userUID == value)
                    return;
                OnUserUIDSet(ref value);
                this._userUID = value;
                OnUserUIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UserUID);
            }
        }
        /// <summary>
        /// 接收方名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _receiverName;

        partial void OnReceiverNameGet();

        partial void OnReceiverNameSet(ref string value);

        partial void OnReceiverNameSeted();

        
        /// <summary>
        /// 接收方名称
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ReceiverName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"接收方名称")]
        public  string ReceiverName
        {
            get
            {
                OnReceiverNameGet();
                return this._receiverName;
            }
            set
            {
                if(this._receiverName == value)
                    return;
                OnReceiverNameSet(ref value);
                this._receiverName = value;
                OnReceiverNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ReceiverName);
            }
        }
        /// <summary>
        /// 接收机电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _receiverPhone;

        partial void OnReceiverPhoneGet();

        partial void OnReceiverPhoneSet(ref string value);

        partial void OnReceiverPhoneSeted();

        
        /// <summary>
        /// 接收机电话
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ReceiverPhone", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"接收机电话")]
        public  string ReceiverPhone
        {
            get
            {
                OnReceiverPhoneGet();
                return this._receiverPhone;
            }
            set
            {
                if(this._receiverPhone == value)
                    return;
                OnReceiverPhoneSet(ref value);
                this._receiverPhone = value;
                OnReceiverPhoneSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ReceiverPhone);
            }
        }
        /// <summary>
        /// 收件人地址
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _receiverAddress;

        partial void OnReceiverAddressGet();

        partial void OnReceiverAddressSet(ref string value);

        partial void OnReceiverAddressSeted();

        
        /// <summary>
        /// 收件人地址
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ReceiverAddress", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"收件人地址")]
        public  string ReceiverAddress
        {
            get
            {
                OnReceiverAddressGet();
                return this._receiverAddress;
            }
            set
            {
                if(this._receiverAddress == value)
                    return;
                OnReceiverAddressSet(ref value);
                this._receiverAddress = value;
                OnReceiverAddressSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ReceiverAddress);
            }
        }
        /// <summary>
        /// 默认为
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public bool _isDefault;

        partial void OnIsDefaultGet();

        partial void OnIsDefaultSet(ref bool value);

        partial void OnIsDefaultSeted();

        
        /// <summary>
        /// 默认为
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("IsDefault", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"默认为")]
        public  bool IsDefault
        {
            get
            {
                OnIsDefaultGet();
                return this._isDefault;
            }
            set
            {
                if(this._isDefault == value)
                    return;
                OnIsDefaultSet(ref value);
                this._isDefault = value;
                OnIsDefaultSeted();
                this.OnPropertyChanged(_DataStruct_.Real_IsDefault);
            }
        }
        /// <summary>
        /// 评论
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _remarks;

        partial void OnRemarksGet();

        partial void OnRemarksSet(ref string value);

        partial void OnRemarksSeted();

        
        /// <summary>
        /// 评论
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Remarks", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"评论")]
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
        /// <summary>
        /// 邮政编码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _postalCode;

        partial void OnPostalCodeGet();

        partial void OnPostalCodeSet(ref string value);

        partial void OnPostalCodeSeted();

        
        /// <summary>
        /// 邮政编码
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("PostalCode", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"邮政编码")]
        public  string PostalCode
        {
            get
            {
                OnPostalCodeGet();
                return this._postalCode;
            }
            set
            {
                if(this._postalCode == value)
                    return;
                OnPostalCodeSet(ref value);
                this._postalCode = value;
                OnPostalCodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_PostalCode);
            }
        }
        /// <summary>
        /// 国家代码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _nationalCode;

        partial void OnNationalCodeGet();

        partial void OnNationalCodeSet(ref string value);

        partial void OnNationalCodeSeted();

        
        /// <summary>
        /// 国家代码
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("NationalCode", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"国家代码")]
        public  string NationalCode
        {
            get
            {
                OnNationalCodeGet();
                return this._nationalCode;
            }
            set
            {
                if(this._nationalCode == value)
                    return;
                OnNationalCodeSet(ref value);
                this._nationalCode = value;
                OnNationalCodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_NationalCode);
            }
        }
        /// <summary>
        /// 省名
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _provinceName;

        partial void OnProvinceNameGet();

        partial void OnProvinceNameSet(ref string value);

        partial void OnProvinceNameSeted();

        
        /// <summary>
        /// 省名
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ProvinceName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"省名")]
        public  string ProvinceName
        {
            get
            {
                OnProvinceNameGet();
                return this._provinceName;
            }
            set
            {
                if(this._provinceName == value)
                    return;
                OnProvinceNameSet(ref value);
                this._provinceName = value;
                OnProvinceNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ProvinceName);
            }
        }
        /// <summary>
        /// 城市名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _cityName;

        partial void OnCityNameGet();

        partial void OnCityNameSet(ref string value);

        partial void OnCityNameSeted();

        
        /// <summary>
        /// 城市名称
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("CityName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"城市名称")]
        public  string CityName
        {
            get
            {
                OnCityNameGet();
                return this._cityName;
            }
            set
            {
                if(this._cityName == value)
                    return;
                OnCityNameSet(ref value);
                this._cityName = value;
                OnCityNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_CityName);
            }
        }
        /// <summary>
        /// 县名
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _countyName;

        partial void OnCountyNameGet();

        partial void OnCountyNameSet(ref string value);

        partial void OnCountyNameSeted();

        
        /// <summary>
        /// 县名
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("CountyName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"县名")]
        public  string CountyName
        {
            get
            {
                OnCountyNameGet();
                return this._countyName;
            }
            set
            {
                if(this._countyName == value)
                    return;
                OnCountyNameSet(ref value);
                this._countyName = value;
                OnCountyNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_CountyName);
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
                return this.AID;
            }
            set
            {
                this.AID = value;
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
            case "aid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.AID = vl;
                        return true;
                    }
                }
                return false;
            case "useruid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.UserUID = vl;
                        return true;
                    }
                }
                return false;
            case "receivername":
                this.ReceiverName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "receiverphone":
                this.ReceiverPhone = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "receiveraddress":
                this.ReceiverAddress = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "isdefault":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (bool.TryParse(value, out var vl))
                    {
                        this.IsDefault = vl;
                        return true;
                    }
                }
                return false;
            case "remarks":
                this.Remarks = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "postalcode":
                this.PostalCode = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "nationalcode":
                this.NationalCode = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "provincename":
                this.ProvinceName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "cityname":
                this.CityName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "countyname":
                this.CountyName = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "aid":
                this.AID = (long)Convert.ToDecimal(value);
                return;
            case "useruid":
                this.UserUID = (long)Convert.ToDecimal(value);
                return;
            case "receivername":
                this.ReceiverName = value == null ? null : value.ToString();
                return;
            case "receiverphone":
                this.ReceiverPhone = value == null ? null : value.ToString();
                return;
            case "receiveraddress":
                this.ReceiverAddress = value == null ? null : value.ToString();
                return;
            case "isdefault":
                if (value != null)
                {
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {
                        this.IsDefault = vl != 0;
                    }
                    else
                    {
                        this.IsDefault = Convert.ToBoolean(value);
                    }
                }
                return;
            case "remarks":
                this.Remarks = value == null ? null : value.ToString();
                return;
            case "postalcode":
                this.PostalCode = value == null ? null : value.ToString();
                return;
            case "nationalcode":
                this.NationalCode = value == null ? null : value.ToString();
                return;
            case "provincename":
                this.ProvinceName = value == null ? null : value.ToString();
                return;
            case "cityname":
                this.CityName = value == null ? null : value.ToString();
                return;
            case "countyname":
                this.CountyName = value == null ? null : value.ToString();
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
            case _DataStruct_.AID:
                this.AID = Convert.ToInt64(value);
                return;
            case _DataStruct_.UserUID:
                this.UserUID = Convert.ToInt64(value);
                return;
            case _DataStruct_.ReceiverName:
                this.ReceiverName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ReceiverPhone:
                this.ReceiverPhone = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ReceiverAddress:
                this.ReceiverAddress = value == null ? null : value.ToString();
                return;
            case _DataStruct_.IsDefault:
                this.IsDefault = Convert.ToBoolean(value);
                return;
            case _DataStruct_.Remarks:
                this.Remarks = value == null ? null : value.ToString();
                return;
            case _DataStruct_.PostalCode:
                this.PostalCode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.NationalCode:
                this.NationalCode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ProvinceName:
                this.ProvinceName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.CityName:
                this.CityName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.CountyName:
                this.CountyName = value == null ? null : value.ToString();
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
            case "aid":
                return this.AID;
            case "useruid":
                return this.UserUID;
            case "receivername":
                return this.ReceiverName;
            case "receiverphone":
                return this.ReceiverPhone;
            case "receiveraddress":
                return this.ReceiverAddress;
            case "isdefault":
                return this.IsDefault;
            case "remarks":
                return this.Remarks;
            case "postalcode":
                return this.PostalCode;
            case "nationalcode":
                return this.NationalCode;
            case "provincename":
                return this.ProvinceName;
            case "cityname":
                return this.CityName;
            case "countyname":
                return this.CountyName;
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
                case _DataStruct_.AID:
                    return this.AID;
                case _DataStruct_.UserUID:
                    return this.UserUID;
                case _DataStruct_.ReceiverName:
                    return this.ReceiverName;
                case _DataStruct_.ReceiverPhone:
                    return this.ReceiverPhone;
                case _DataStruct_.ReceiverAddress:
                    return this.ReceiverAddress;
                case _DataStruct_.IsDefault:
                    return this.IsDefault;
                case _DataStruct_.Remarks:
                    return this.Remarks;
                case _DataStruct_.PostalCode:
                    return this.PostalCode;
                case _DataStruct_.NationalCode:
                    return this.NationalCode;
                case _DataStruct_.ProvinceName:
                    return this.ProvinceName;
                case _DataStruct_.CityName:
                    return this.CityName;
                case _DataStruct_.CountyName:
                    return this.CountyName;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(UserAddressData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as UserAddressData;
            if(sourceEntity == null)
                return;
            this._aID = sourceEntity._aID;
            this._userUID = sourceEntity._userUID;
            this._receiverName = sourceEntity._receiverName;
            this._receiverPhone = sourceEntity._receiverPhone;
            this._receiverAddress = sourceEntity._receiverAddress;
            this._isDefault = sourceEntity._isDefault;
            this._remarks = sourceEntity._remarks;
            this._postalCode = sourceEntity._postalCode;
            this._nationalCode = sourceEntity._nationalCode;
            this._provinceName = sourceEntity._provinceName;
            this._cityName = sourceEntity._cityName;
            this._countyName = sourceEntity._countyName;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(UserAddressData source)
        {
                this.AID = source.AID;
                this.UserUID = source.UserUID;
                this.ReceiverName = source.ReceiverName;
                this.ReceiverPhone = source.ReceiverPhone;
                this.ReceiverAddress = source.ReceiverAddress;
                this.IsDefault = source.IsDefault;
                this.Remarks = source.Remarks;
                this.PostalCode = source.PostalCode;
                this.NationalCode = source.NationalCode;
                this.ProvinceName = source.ProvinceName;
                this.CityName = source.CityName;
                this.CountyName = source.CountyName;
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
                OnAIDModified(subsist,false);
                OnUserUIDModified(subsist,false);
                OnReceiverNameModified(subsist,false);
                OnReceiverPhoneModified(subsist,false);
                OnReceiverAddressModified(subsist,false);
                OnIsDefaultModified(subsist,false);
                OnRemarksModified(subsist,false);
                OnPostalCodeModified(subsist,false);
                OnNationalCodeModified(subsist,false);
                OnProvinceNameModified(subsist,false);
                OnCityNameModified(subsist,false);
                OnCountyNameModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnAIDModified(subsist,true);
                OnUserUIDModified(subsist,true);
                OnReceiverNameModified(subsist,true);
                OnReceiverPhoneModified(subsist,true);
                OnReceiverAddressModified(subsist,true);
                OnIsDefaultModified(subsist,true);
                OnRemarksModified(subsist,true);
                OnPostalCodeModified(subsist,true);
                OnNationalCodeModified(subsist,true);
                OnProvinceNameModified(subsist,true);
                OnCityNameModified(subsist,true);
                OnCountyNameModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[12] > 0)
            {
                OnAIDModified(subsist,modifieds[_DataStruct_.Real_AID] == 1);
                OnUserUIDModified(subsist,modifieds[_DataStruct_.Real_UserUID] == 1);
                OnReceiverNameModified(subsist,modifieds[_DataStruct_.Real_ReceiverName] == 1);
                OnReceiverPhoneModified(subsist,modifieds[_DataStruct_.Real_ReceiverPhone] == 1);
                OnReceiverAddressModified(subsist,modifieds[_DataStruct_.Real_ReceiverAddress] == 1);
                OnIsDefaultModified(subsist,modifieds[_DataStruct_.Real_IsDefault] == 1);
                OnRemarksModified(subsist,modifieds[_DataStruct_.Real_Remarks] == 1);
                OnPostalCodeModified(subsist,modifieds[_DataStruct_.Real_PostalCode] == 1);
                OnNationalCodeModified(subsist,modifieds[_DataStruct_.Real_NationalCode] == 1);
                OnProvinceNameModified(subsist,modifieds[_DataStruct_.Real_ProvinceName] == 1);
                OnCityNameModified(subsist,modifieds[_DataStruct_.Real_CityName] == 1);
                OnCountyNameModified(subsist,modifieds[_DataStruct_.Real_CountyName] == 1);
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
        partial void OnAIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 组织标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserUIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 接收方名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnReceiverNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 接收机电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnReceiverPhoneModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 收件人地址修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnReceiverAddressModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 默认为修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIsDefaultModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 评论修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRemarksModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 邮政编码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPostalCodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 国家代码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnNationalCodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 省名修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnProvinceNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 城市名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCityNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 县名修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCountyNameModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"UserAddress";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户地址";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户地址";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "AID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte AID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_AID = 0;

            /// <summary>
            /// 组织标识的数字标识
            /// </summary>
            public const byte UserUID = 2;
            
            /// <summary>
            /// 组织标识的实时记录顺序
            /// </summary>
            public const int Real_UserUID = 1;

            /// <summary>
            /// 接收方名称的数字标识
            /// </summary>
            public const byte ReceiverName = 3;
            
            /// <summary>
            /// 接收方名称的实时记录顺序
            /// </summary>
            public const int Real_ReceiverName = 2;

            /// <summary>
            /// 接收机电话的数字标识
            /// </summary>
            public const byte ReceiverPhone = 4;
            
            /// <summary>
            /// 接收机电话的实时记录顺序
            /// </summary>
            public const int Real_ReceiverPhone = 3;

            /// <summary>
            /// 收件人地址的数字标识
            /// </summary>
            public const byte ReceiverAddress = 5;
            
            /// <summary>
            /// 收件人地址的实时记录顺序
            /// </summary>
            public const int Real_ReceiverAddress = 4;

            /// <summary>
            /// 默认为的数字标识
            /// </summary>
            public const byte IsDefault = 6;
            
            /// <summary>
            /// 默认为的实时记录顺序
            /// </summary>
            public const int Real_IsDefault = 5;

            /// <summary>
            /// 评论的数字标识
            /// </summary>
            public const byte Remarks = 7;
            
            /// <summary>
            /// 评论的实时记录顺序
            /// </summary>
            public const int Real_Remarks = 6;

            /// <summary>
            /// 邮政编码的数字标识
            /// </summary>
            public const byte PostalCode = 8;
            
            /// <summary>
            /// 邮政编码的实时记录顺序
            /// </summary>
            public const int Real_PostalCode = 7;

            /// <summary>
            /// 国家代码的数字标识
            /// </summary>
            public const byte NationalCode = 9;
            
            /// <summary>
            /// 国家代码的实时记录顺序
            /// </summary>
            public const int Real_NationalCode = 8;

            /// <summary>
            /// 省名的数字标识
            /// </summary>
            public const byte ProvinceName = 10;
            
            /// <summary>
            /// 省名的实时记录顺序
            /// </summary>
            public const int Real_ProvinceName = 9;

            /// <summary>
            /// 城市名称的数字标识
            /// </summary>
            public const byte CityName = 11;
            
            /// <summary>
            /// 城市名称的实时记录顺序
            /// </summary>
            public const int Real_CityName = 10;

            /// <summary>
            /// 县名的数字标识
            /// </summary>
            public const byte CountyName = 12;
            
            /// <summary>
            /// 县名的实时记录顺序
            /// </summary>
            public const int Real_CountyName = 11;

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
                        Real_AID,
                        new PropertySturct
                        {
                            Index        = AID,
                            Name         = "AID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "AID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_UserUID,
                        new PropertySturct
                        {
                            Index        = UserUID,
                            Name         = "UserUID",
                            Title        = "组织标识",
                            Caption      = @"组织标识",
                            Description  = @"组织标识",
                            ColumnName   = "UserUID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ReceiverName,
                        new PropertySturct
                        {
                            Index        = ReceiverName,
                            Name         = "ReceiverName",
                            Title        = "接收方名称",
                            Caption      = @"接收方名称",
                            Description  = @"接收方名称",
                            ColumnName   = "ReceiverName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ReceiverPhone,
                        new PropertySturct
                        {
                            Index        = ReceiverPhone,
                            Name         = "ReceiverPhone",
                            Title        = "接收机电话",
                            Caption      = @"接收机电话",
                            Description  = @"接收机电话",
                            ColumnName   = "ReceiverPhone",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ReceiverAddress,
                        new PropertySturct
                        {
                            Index        = ReceiverAddress,
                            Name         = "ReceiverAddress",
                            Title        = "收件人地址",
                            Caption      = @"收件人地址",
                            Description  = @"收件人地址",
                            ColumnName   = "ReceiverAddress",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_IsDefault,
                        new PropertySturct
                        {
                            Index        = IsDefault,
                            Name         = "IsDefault",
                            Title        = "默认为",
                            Caption      = @"默认为",
                            Description  = @"默认为",
                            ColumnName   = "IsDefault",
                            PropertyType = typeof(bool),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
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
                            Title        = "评论",
                            Caption      = @"评论",
                            Description  = @"评论",
                            ColumnName   = "Remarks",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PostalCode,
                        new PropertySturct
                        {
                            Index        = PostalCode,
                            Name         = "PostalCode",
                            Title        = "邮政编码",
                            Caption      = @"邮政编码",
                            Description  = @"邮政编码",
                            ColumnName   = "PostalCode",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_NationalCode,
                        new PropertySturct
                        {
                            Index        = NationalCode,
                            Name         = "NationalCode",
                            Title        = "国家代码",
                            Caption      = @"国家代码",
                            Description  = @"国家代码",
                            ColumnName   = "NationalCode",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ProvinceName,
                        new PropertySturct
                        {
                            Index        = ProvinceName,
                            Name         = "ProvinceName",
                            Title        = "省名",
                            Caption      = @"省名",
                            Description  = @"省名",
                            ColumnName   = "ProvinceName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_CityName,
                        new PropertySturct
                        {
                            Index        = CityName,
                            Name         = "CityName",
                            Title        = "城市名称",
                            Caption      = @"城市名称",
                            Description  = @"城市名称",
                            ColumnName   = "CityName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_CountyName,
                        new PropertySturct
                        {
                            Index        = CountyName,
                            Name         = "CountyName",
                            Title        = "县名",
                            Caption      = @"县名",
                            Description  = @"县名",
                            ColumnName   = "CountyName",
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