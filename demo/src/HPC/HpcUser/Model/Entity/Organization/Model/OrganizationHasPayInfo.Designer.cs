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
    /// 组织是否支付信息
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class OrganizationHasPayInfoData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public OrganizationHasPayInfoData()
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
        public void ChangePrimaryKey(long pID)
        {
            _pID = pID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _pID;

        partial void OnPIDGet();

        partial void OnPIDSet(ref long value);

        partial void OnPIDLoad(ref long value);

        partial void OnPIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("PID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long PID
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
                //if(this._pID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnPIDSet(ref value);
                this._pID = value;
                this.OnPropertyChanged(_DataStruct_.Real_PID);
                OnPIDSeted();
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
        /// 组织标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _orgOID;

        partial void OnOrgOIDGet();

        partial void OnOrgOIDSet(ref long value);

        partial void OnOrgOIDSeted();

        
        /// <summary>
        /// 组织标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgOID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织标识")]
        public  long OrgOID
        {
            get
            {
                OnOrgOIDGet();
                return this._orgOID;
            }
            set
            {
                if(this._orgOID == value)
                    return;
                OnOrgOIDSet(ref value);
                this._orgOID = value;
                OnOrgOIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgOID);
            }
        }
        /// <summary>
        /// 工资来源
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _paySource;

        partial void OnpaySourceGet();

        partial void OnpaySourceSet(ref string value);

        partial void OnpaySourceSeted();

        
        /// <summary>
        /// 工资来源
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("paySource", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"工资来源")]
        public  string paySource
        {
            get
            {
                OnpaySourceGet();
                return this._paySource;
            }
            set
            {
                if(this._paySource == value)
                    return;
                OnpaySourceSet(ref value);
                this._paySource = value;
                OnpaySourceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_paySource);
            }
        }
        /// <summary>
        /// 应用标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _aPPID;

        partial void OnAPPIDGet();

        partial void OnAPPIDSet(ref string value);

        partial void OnAPPIDSeted();

        
        /// <summary>
        /// 应用标识
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("APPID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用标识")]
        public  string APPID
        {
            get
            {
                OnAPPIDGet();
                return this._aPPID;
            }
            set
            {
                if(this._aPPID == value)
                    return;
                OnAPPIDSet(ref value);
                this._aPPID = value;
                OnAPPIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_APPID);
            }
        }
        /// <summary>
        /// 阿普基
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _aPPKEY;

        partial void OnAPPKEYGet();

        partial void OnAPPKEYSet(ref string value);

        partial void OnAPPKEYSeted();

        
        /// <summary>
        /// 阿普基
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("APPKEY", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"阿普基")]
        public  string APPKEY
        {
            get
            {
                OnAPPKEYGet();
                return this._aPPKEY;
            }
            set
            {
                if(this._aPPKEY == value)
                    return;
                OnAPPKEYSet(ref value);
                this._aPPKEY = value;
                OnAPPKEYSeted();
                this.OnPropertyChanged(_DataStruct_.Real_APPKEY);
            }
        }
        /// <summary>
        /// 妇幼保健院
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _mCHID;

        partial void OnMCHIDGet();

        partial void OnMCHIDSet(ref string value);

        partial void OnMCHIDSeted();

        
        /// <summary>
        /// 妇幼保健院
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MCHID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"妇幼保健院")]
        public  string MCHID
        {
            get
            {
                OnMCHIDGet();
                return this._mCHID;
            }
            set
            {
                if(this._mCHID == value)
                    return;
                OnMCHIDSet(ref value);
                this._mCHID = value;
                OnMCHIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MCHID);
            }
        }
        /// <summary>
        /// 麦克奇
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _mCHKEY;

        partial void OnMCHKEYGet();

        partial void OnMCHKEYSet(ref string value);

        partial void OnMCHKEYSeted();

        
        /// <summary>
        /// 麦克奇
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MCHKEY", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"麦克奇")]
        public  string MCHKEY
        {
            get
            {
                OnMCHKEYGet();
                return this._mCHKEY;
            }
            set
            {
                if(this._mCHKEY == value)
                    return;
                OnMCHKEYSet(ref value);
                this._mCHKEY = value;
                OnMCHKEYSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MCHKEY);
            }
        }
        /// <summary>
        /// 服务器MCH
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _serverMCH;

        partial void OnServerMCHGet();

        partial void OnServerMCHSet(ref string value);

        partial void OnServerMCHSeted();

        
        /// <summary>
        /// 服务器MCH
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ServerMCH", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"服务器MCH")]
        public  string ServerMCH
        {
            get
            {
                OnServerMCHGet();
                return this._serverMCH;
            }
            set
            {
                if(this._serverMCH == value)
                    return;
                OnServerMCHSet(ref value);
                this._serverMCH = value;
                OnServerMCHSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ServerMCH);
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
        /// 可存储100个字符.合理长度应不大于100.
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

        #region 接口属性


        /// <summary>
        /// 对象标识
        /// </summary>
        [IgnoreDataMember,Browsable(false)]
        public long Id
        {
            get
            {
                return this.PID;
            }
            set
            {
                this.PID = value;
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
            case "orgoid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.OrgOID = vl;
                        return true;
                    }
                }
                return false;
            case "paysource":
                this.paySource = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "appid":
                this.APPID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "appkey":
                this.APPKEY = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "mchid":
                this.MCHID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "mchkey":
                this.MCHKEY = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "servermch":
                this.ServerMCH = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "pid":
                this.PID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "orgoid":
                this.OrgOID = (long)Convert.ToDecimal(value);
                return;
            case "paysource":
                this.paySource = value == null ? null : value.ToString();
                return;
            case "appid":
                this.APPID = value == null ? null : value.ToString();
                return;
            case "appkey":
                this.APPKEY = value == null ? null : value.ToString();
                return;
            case "mchid":
                this.MCHID = value == null ? null : value.ToString();
                return;
            case "mchkey":
                this.MCHKEY = value == null ? null : value.ToString();
                return;
            case "servermch":
                this.ServerMCH = value == null ? null : value.ToString();
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
            case _DataStruct_.PID:
                this.PID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgOID:
                this.OrgOID = Convert.ToInt64(value);
                return;
            case _DataStruct_.paySource:
                this.paySource = value == null ? null : value.ToString();
                return;
            case _DataStruct_.APPID:
                this.APPID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.APPKEY:
                this.APPKEY = value == null ? null : value.ToString();
                return;
            case _DataStruct_.MCHID:
                this.MCHID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.MCHKEY:
                this.MCHKEY = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ServerMCH:
                this.ServerMCH = value == null ? null : value.ToString();
                return;
            case _DataStruct_.IsDefault:
                this.IsDefault = Convert.ToBoolean(value);
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
            case "pid":
                return this.PID;
            case "sitesid":
                return this.SiteSID;
            case "orgoid":
                return this.OrgOID;
            case "paysource":
                return this.paySource;
            case "appid":
                return this.APPID;
            case "appkey":
                return this.APPKEY;
            case "mchid":
                return this.MCHID;
            case "mchkey":
                return this.MCHKEY;
            case "servermch":
                return this.ServerMCH;
            case "isdefault":
                return this.IsDefault;
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
                case _DataStruct_.PID:
                    return this.PID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.OrgOID:
                    return this.OrgOID;
                case _DataStruct_.paySource:
                    return this.paySource;
                case _DataStruct_.APPID:
                    return this.APPID;
                case _DataStruct_.APPKEY:
                    return this.APPKEY;
                case _DataStruct_.MCHID:
                    return this.MCHID;
                case _DataStruct_.MCHKEY:
                    return this.MCHKEY;
                case _DataStruct_.ServerMCH:
                    return this.ServerMCH;
                case _DataStruct_.IsDefault:
                    return this.IsDefault;
                case _DataStruct_.Remarks:
                    return this.Remarks;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(OrganizationHasPayInfoData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as OrganizationHasPayInfoData;
            if(sourceEntity == null)
                return;
            this._pID = sourceEntity._pID;
            this._siteSID = sourceEntity._siteSID;
            this._orgOID = sourceEntity._orgOID;
            this._paySource = sourceEntity._paySource;
            this._aPPID = sourceEntity._aPPID;
            this._aPPKEY = sourceEntity._aPPKEY;
            this._mCHID = sourceEntity._mCHID;
            this._mCHKEY = sourceEntity._mCHKEY;
            this._serverMCH = sourceEntity._serverMCH;
            this._isDefault = sourceEntity._isDefault;
            this._remarks = sourceEntity._remarks;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(OrganizationHasPayInfoData source)
        {
                this.PID = source.PID;
                this.SiteSID = source.SiteSID;
                this.OrgOID = source.OrgOID;
                this.paySource = source.paySource;
                this.APPID = source.APPID;
                this.APPKEY = source.APPKEY;
                this.MCHID = source.MCHID;
                this.MCHKEY = source.MCHKEY;
                this.ServerMCH = source.ServerMCH;
                this.IsDefault = source.IsDefault;
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
                OnPIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnOrgOIDModified(subsist,false);
                OnpaySourceModified(subsist,false);
                OnAPPIDModified(subsist,false);
                OnAPPKEYModified(subsist,false);
                OnMCHIDModified(subsist,false);
                OnMCHKEYModified(subsist,false);
                OnServerMCHModified(subsist,false);
                OnIsDefaultModified(subsist,false);
                OnRemarksModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnPIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnOrgOIDModified(subsist,true);
                OnpaySourceModified(subsist,true);
                OnAPPIDModified(subsist,true);
                OnAPPKEYModified(subsist,true);
                OnMCHIDModified(subsist,true);
                OnMCHKEYModified(subsist,true);
                OnServerMCHModified(subsist,true);
                OnIsDefaultModified(subsist,true);
                OnRemarksModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[11] > 0)
            {
                OnPIDModified(subsist,modifieds[_DataStruct_.Real_PID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnOrgOIDModified(subsist,modifieds[_DataStruct_.Real_OrgOID] == 1);
                OnpaySourceModified(subsist,modifieds[_DataStruct_.Real_paySource] == 1);
                OnAPPIDModified(subsist,modifieds[_DataStruct_.Real_APPID] == 1);
                OnAPPKEYModified(subsist,modifieds[_DataStruct_.Real_APPKEY] == 1);
                OnMCHIDModified(subsist,modifieds[_DataStruct_.Real_MCHID] == 1);
                OnMCHKEYModified(subsist,modifieds[_DataStruct_.Real_MCHKEY] == 1);
                OnServerMCHModified(subsist,modifieds[_DataStruct_.Real_ServerMCH] == 1);
                OnIsDefaultModified(subsist,modifieds[_DataStruct_.Real_IsDefault] == 1);
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
        /// 组织标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgOIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 工资来源修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnpaySourceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 应用标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAPPIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 阿普基修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAPPKEYModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 妇幼保健院修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMCHIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 麦克奇修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMCHKEYModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 服务器MCH修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnServerMCHModified(EntitySubsist subsist,bool isModified);

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
            public const string EntityName = @"OrganizationHasPayInfo";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"组织是否支付信息";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"组织是否支付信息";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "PID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte PID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_PID = 0;

            /// <summary>
            /// 站点标识的数字标识
            /// </summary>
            public const byte SiteSID = 2;
            
            /// <summary>
            /// 站点标识的实时记录顺序
            /// </summary>
            public const int Real_SiteSID = 1;

            /// <summary>
            /// 组织标识的数字标识
            /// </summary>
            public const byte OrgOID = 3;
            
            /// <summary>
            /// 组织标识的实时记录顺序
            /// </summary>
            public const int Real_OrgOID = 2;

            /// <summary>
            /// 工资来源的数字标识
            /// </summary>
            public const byte paySource = 4;
            
            /// <summary>
            /// 工资来源的实时记录顺序
            /// </summary>
            public const int Real_paySource = 3;

            /// <summary>
            /// 应用标识的数字标识
            /// </summary>
            public const byte APPID = 5;
            
            /// <summary>
            /// 应用标识的实时记录顺序
            /// </summary>
            public const int Real_APPID = 4;

            /// <summary>
            /// 阿普基的数字标识
            /// </summary>
            public const byte APPKEY = 6;
            
            /// <summary>
            /// 阿普基的实时记录顺序
            /// </summary>
            public const int Real_APPKEY = 5;

            /// <summary>
            /// 妇幼保健院的数字标识
            /// </summary>
            public const byte MCHID = 7;
            
            /// <summary>
            /// 妇幼保健院的实时记录顺序
            /// </summary>
            public const int Real_MCHID = 6;

            /// <summary>
            /// 麦克奇的数字标识
            /// </summary>
            public const byte MCHKEY = 8;
            
            /// <summary>
            /// 麦克奇的实时记录顺序
            /// </summary>
            public const int Real_MCHKEY = 7;

            /// <summary>
            /// 服务器MCH的数字标识
            /// </summary>
            public const byte ServerMCH = 9;
            
            /// <summary>
            /// 服务器MCH的实时记录顺序
            /// </summary>
            public const int Real_ServerMCH = 8;

            /// <summary>
            /// 默认为的数字标识
            /// </summary>
            public const byte IsDefault = 10;
            
            /// <summary>
            /// 默认为的实时记录顺序
            /// </summary>
            public const int Real_IsDefault = 9;

            /// <summary>
            /// 评论的数字标识
            /// </summary>
            public const byte Remarks = 11;
            
            /// <summary>
            /// 评论的实时记录顺序
            /// </summary>
            public const int Real_Remarks = 10;

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
                        Real_PID,
                        new PropertySturct
                        {
                            Index        = PID,
                            Name         = "PID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
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
                        Real_OrgOID,
                        new PropertySturct
                        {
                            Index        = OrgOID,
                            Name         = "OrgOID",
                            Title        = "组织标识",
                            Caption      = @"组织标识",
                            Description  = @"组织标识",
                            ColumnName   = "OrgOID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_paySource,
                        new PropertySturct
                        {
                            Index        = paySource,
                            Name         = "paySource",
                            Title        = "工资来源",
                            Caption      = @"工资来源",
                            Description  = @"工资来源",
                            ColumnName   = "paySource",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_APPID,
                        new PropertySturct
                        {
                            Index        = APPID,
                            Name         = "APPID",
                            Title        = "应用标识",
                            Caption      = @"应用标识",
                            Description  = @"应用标识",
                            ColumnName   = "APPID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_APPKEY,
                        new PropertySturct
                        {
                            Index        = APPKEY,
                            Name         = "APPKEY",
                            Title        = "阿普基",
                            Caption      = @"阿普基",
                            Description  = @"阿普基",
                            ColumnName   = "APPKEY",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MCHID,
                        new PropertySturct
                        {
                            Index        = MCHID,
                            Name         = "MCHID",
                            Title        = "妇幼保健院",
                            Caption      = @"妇幼保健院",
                            Description  = @"妇幼保健院",
                            ColumnName   = "MCHID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MCHKEY,
                        new PropertySturct
                        {
                            Index        = MCHKEY,
                            Name         = "MCHKEY",
                            Title        = "麦克奇",
                            Caption      = @"麦克奇",
                            Description  = @"麦克奇",
                            ColumnName   = "MCHKEY",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ServerMCH,
                        new PropertySturct
                        {
                            Index        = ServerMCH,
                            Name         = "ServerMCH",
                            Title        = "服务器MCH",
                            Caption      = @"服务器MCH",
                            Description  = @"服务器MCH",
                            ColumnName   = "ServerMCH",
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
                    }
                }
            };
        }
        #endregion

    }
}