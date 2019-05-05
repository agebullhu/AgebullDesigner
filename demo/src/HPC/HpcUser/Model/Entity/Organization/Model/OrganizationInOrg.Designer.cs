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
    /// 组织中的组织
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class OrganizationInOrgData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public OrganizationInOrgData()
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
        public void ChangePrimaryKey(long sOID)
        {
            _sOID = sOID;
        }
        /// <summary>
        /// SOID
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _sOID;

        partial void OnSOIDGet();

        partial void OnSOIDSet(ref long value);

        partial void OnSOIDLoad(ref long value);

        partial void OnSOIDSeted();

        
        /// <summary>
        /// SOID
        /// </summary>
        [DataMember , JsonProperty("SOID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"SOID")]
        public long SOID
        {
            get
            {
                OnSOIDGet();
                return this._sOID;
            }
            set
            {
                if(this._sOID == value)
                    return;
                //if(this._sOID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnSOIDSet(ref value);
                this._sOID = value;
                this.OnPropertyChanged(_DataStruct_.Real_SOID);
                OnSOIDSeted();
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
        /// 站点站点标识relative
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _siteSID_Relative;

        partial void OnSiteSID_RelativeGet();

        partial void OnSiteSID_RelativeSet(ref long value);

        partial void OnSiteSID_RelativeSeted();

        
        /// <summary>
        /// 站点站点标识relative
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SiteSID_Relative", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"站点站点标识relative")]
        public  long SiteSID_Relative
        {
            get
            {
                OnSiteSID_RelativeGet();
                return this._siteSID_Relative;
            }
            set
            {
                if(this._siteSID_Relative == value)
                    return;
                OnSiteSID_RelativeSet(ref value);
                this._siteSID_Relative = value;
                OnSiteSID_RelativeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SiteSID_Relative);
            }
        }
        /// <summary>
        /// 组织标识相对
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _orgOID_Relative;

        partial void OnOrgOID_RelativeGet();

        partial void OnOrgOID_RelativeSet(ref long value);

        partial void OnOrgOID_RelativeSeted();

        
        /// <summary>
        /// 组织标识相对
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgOID_Relative", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织标识相对")]
        public  long OrgOID_Relative
        {
            get
            {
                OnOrgOID_RelativeGet();
                return this._orgOID_Relative;
            }
            set
            {
                if(this._orgOID_Relative == value)
                    return;
                OnOrgOID_RelativeSet(ref value);
                this._orgOID_Relative = value;
                OnOrgOID_RelativeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgOID_Relative);
            }
        }
        /// <summary>
        /// 风俗习惯
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _remarkCustom;

        partial void OnRemarkCustomGet();

        partial void OnRemarkCustomSet(ref string value);

        partial void OnRemarkCustomSeted();

        
        /// <summary>
        /// 风俗习惯
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("RemarkCustom", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"风俗习惯")]
        public  string RemarkCustom
        {
            get
            {
                OnRemarkCustomGet();
                return this._remarkCustom;
            }
            set
            {
                if(this._remarkCustom == value)
                    return;
                OnRemarkCustomSet(ref value);
                this._remarkCustom = value;
                OnRemarkCustomSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RemarkCustom);
            }
        }
        /// <summary>
        /// 销售折扣
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public float _salesDiscount;

        partial void OnSalesDiscountGet();

        partial void OnSalesDiscountSet(ref float value);

        partial void OnSalesDiscountSeted();

        
        /// <summary>
        /// 销售折扣
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SalesDiscount", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"销售折扣")]
        public  float SalesDiscount
        {
            get
            {
                OnSalesDiscountGet();
                return this._salesDiscount;
            }
            set
            {
                if(this._salesDiscount == value)
                    return;
                OnSalesDiscountSet(ref value);
                this._salesDiscount = value;
                OnSalesDiscountSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SalesDiscount);
            }
        }
        /// <summary>
        /// 相对
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _relative;

        partial void OnRelativeGet();

        partial void OnRelativeSet(ref string value);

        partial void OnRelativeSeted();

        
        /// <summary>
        /// 相对
        /// </summary>
        /// <value>
        /// 可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Relative", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"相对")]
        public  string Relative
        {
            get
            {
                OnRelativeGet();
                return this._relative;
            }
            set
            {
                if(this._relative == value)
                    return;
                OnRelativeSet(ref value);
                this._relative = value;
                OnRelativeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Relative);
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
                return this.SOID;
            }
            set
            {
                this.SOID = value;
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
            case "soid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.SOID = vl;
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
            case "sitesid_relative":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.SiteSID_Relative = vl;
                        return true;
                    }
                }
                return false;
            case "orgoid_relative":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.OrgOID_Relative = vl;
                        return true;
                    }
                }
                return false;
            case "remarkcustom":
                this.RemarkCustom = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "salesdiscount":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (float.TryParse(value, out var vl))
                    {
                        this.SalesDiscount = vl;
                        return true;
                    }
                }
                return false;
            case "relative":
                this.Relative = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "soid":
                this.SOID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "orgoid":
                this.OrgOID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid_relative":
                this.SiteSID_Relative = (long)Convert.ToDecimal(value);
                return;
            case "orgoid_relative":
                this.OrgOID_Relative = (long)Convert.ToDecimal(value);
                return;
            case "remarkcustom":
                this.RemarkCustom = value == null ? null : value.ToString();
                return;
            case "salesdiscount":
                this.SalesDiscount = Convert.ToSingle(value);
                return;
            case "relative":
                this.Relative = value == null ? null : value.ToString();
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
            case _DataStruct_.SOID:
                this.SOID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgOID:
                this.OrgOID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID_Relative:
                this.SiteSID_Relative = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgOID_Relative:
                this.OrgOID_Relative = Convert.ToInt64(value);
                return;
            case _DataStruct_.RemarkCustom:
                this.RemarkCustom = value == null ? null : value.ToString();
                return;
            case _DataStruct_.SalesDiscount:
                this.SalesDiscount = Convert.ToSingle(value);
                return;
            case _DataStruct_.Relative:
                this.Relative = value == null ? null : value.ToString();
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
            case "soid":
                return this.SOID;
            case "sitesid":
                return this.SiteSID;
            case "orgoid":
                return this.OrgOID;
            case "sitesid_relative":
                return this.SiteSID_Relative;
            case "orgoid_relative":
                return this.OrgOID_Relative;
            case "remarkcustom":
                return this.RemarkCustom;
            case "salesdiscount":
                return this.SalesDiscount;
            case "relative":
                return this.Relative;
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
                case _DataStruct_.SOID:
                    return this.SOID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.OrgOID:
                    return this.OrgOID;
                case _DataStruct_.SiteSID_Relative:
                    return this.SiteSID_Relative;
                case _DataStruct_.OrgOID_Relative:
                    return this.OrgOID_Relative;
                case _DataStruct_.RemarkCustom:
                    return this.RemarkCustom;
                case _DataStruct_.SalesDiscount:
                    return this.SalesDiscount;
                case _DataStruct_.Relative:
                    return this.Relative;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(OrganizationInOrgData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as OrganizationInOrgData;
            if(sourceEntity == null)
                return;
            this._sOID = sourceEntity._sOID;
            this._siteSID = sourceEntity._siteSID;
            this._orgOID = sourceEntity._orgOID;
            this._siteSID_Relative = sourceEntity._siteSID_Relative;
            this._orgOID_Relative = sourceEntity._orgOID_Relative;
            this._remarkCustom = sourceEntity._remarkCustom;
            this._salesDiscount = sourceEntity._salesDiscount;
            this._relative = sourceEntity._relative;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(OrganizationInOrgData source)
        {
                this.SOID = source.SOID;
                this.SiteSID = source.SiteSID;
                this.OrgOID = source.OrgOID;
                this.SiteSID_Relative = source.SiteSID_Relative;
                this.OrgOID_Relative = source.OrgOID_Relative;
                this.RemarkCustom = source.RemarkCustom;
                this.SalesDiscount = source.SalesDiscount;
                this.Relative = source.Relative;
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
                OnSOIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnOrgOIDModified(subsist,false);
                OnSiteSID_RelativeModified(subsist,false);
                OnOrgOID_RelativeModified(subsist,false);
                OnRemarkCustomModified(subsist,false);
                OnSalesDiscountModified(subsist,false);
                OnRelativeModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnSOIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnOrgOIDModified(subsist,true);
                OnSiteSID_RelativeModified(subsist,true);
                OnOrgOID_RelativeModified(subsist,true);
                OnRemarkCustomModified(subsist,true);
                OnSalesDiscountModified(subsist,true);
                OnRelativeModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[8] > 0)
            {
                OnSOIDModified(subsist,modifieds[_DataStruct_.Real_SOID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnOrgOIDModified(subsist,modifieds[_DataStruct_.Real_OrgOID] == 1);
                OnSiteSID_RelativeModified(subsist,modifieds[_DataStruct_.Real_SiteSID_Relative] == 1);
                OnOrgOID_RelativeModified(subsist,modifieds[_DataStruct_.Real_OrgOID_Relative] == 1);
                OnRemarkCustomModified(subsist,modifieds[_DataStruct_.Real_RemarkCustom] == 1);
                OnSalesDiscountModified(subsist,modifieds[_DataStruct_.Real_SalesDiscount] == 1);
                OnRelativeModified(subsist,modifieds[_DataStruct_.Real_Relative] == 1);
            }
        }

        /// <summary>
        /// SOID修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSOIDModified(EntitySubsist subsist,bool isModified);

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
        /// 站点站点标识relative修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSiteSID_RelativeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 组织标识相对修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgOID_RelativeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 风俗习惯修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRemarkCustomModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 销售折扣修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSalesDiscountModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 相对修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRelativeModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"OrganizationInOrg";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"组织中的组织";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"组织中的组织";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "SOID";
            
            
            /// <summary>
            /// SOID的数字标识
            /// </summary>
            public const byte SOID = 1;
            
            /// <summary>
            /// SOID的实时记录顺序
            /// </summary>
            public const int Real_SOID = 0;

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
            /// 站点站点标识relative的数字标识
            /// </summary>
            public const byte SiteSID_Relative = 4;
            
            /// <summary>
            /// 站点站点标识relative的实时记录顺序
            /// </summary>
            public const int Real_SiteSID_Relative = 3;

            /// <summary>
            /// 组织标识相对的数字标识
            /// </summary>
            public const byte OrgOID_Relative = 5;
            
            /// <summary>
            /// 组织标识相对的实时记录顺序
            /// </summary>
            public const int Real_OrgOID_Relative = 4;

            /// <summary>
            /// 风俗习惯的数字标识
            /// </summary>
            public const byte RemarkCustom = 6;
            
            /// <summary>
            /// 风俗习惯的实时记录顺序
            /// </summary>
            public const int Real_RemarkCustom = 5;

            /// <summary>
            /// 销售折扣的数字标识
            /// </summary>
            public const byte SalesDiscount = 7;
            
            /// <summary>
            /// 销售折扣的实时记录顺序
            /// </summary>
            public const int Real_SalesDiscount = 6;

            /// <summary>
            /// 相对的数字标识
            /// </summary>
            public const byte Relative = 8;
            
            /// <summary>
            /// 相对的实时记录顺序
            /// </summary>
            public const int Real_Relative = 7;

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
                        Real_SOID,
                        new PropertySturct
                        {
                            Index        = SOID,
                            Name         = "SOID",
                            Title        = "SOID",
                            Caption      = @"SOID",
                            Description  = @"SOID",
                            ColumnName   = "SOID",
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
                        Real_SiteSID_Relative,
                        new PropertySturct
                        {
                            Index        = SiteSID_Relative,
                            Name         = "SiteSID_Relative",
                            Title        = "站点站点标识relative",
                            Caption      = @"站点站点标识relative",
                            Description  = @"站点站点标识relative",
                            ColumnName   = "SiteSID_Relative",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrgOID_Relative,
                        new PropertySturct
                        {
                            Index        = OrgOID_Relative,
                            Name         = "OrgOID_Relative",
                            Title        = "组织标识相对",
                            Caption      = @"组织标识相对",
                            Description  = @"组织标识相对",
                            ColumnName   = "OrgOID_Relative",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RemarkCustom,
                        new PropertySturct
                        {
                            Index        = RemarkCustom,
                            Name         = "RemarkCustom",
                            Title        = "风俗习惯",
                            Caption      = @"风俗习惯",
                            Description  = @"风俗习惯",
                            ColumnName   = "RemarkCustom",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SalesDiscount,
                        new PropertySturct
                        {
                            Index        = SalesDiscount,
                            Name         = "SalesDiscount",
                            Title        = "销售折扣",
                            Caption      = @"销售折扣",
                            Description  = @"销售折扣",
                            ColumnName   = "SalesDiscount",
                            PropertyType = typeof(float),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Relative,
                        new PropertySturct
                        {
                            Index        = Relative,
                            Name         = "Relative",
                            Title        = "相对",
                            Caption      = @"相对",
                            Description  = @"相对",
                            ColumnName   = "Relative",
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