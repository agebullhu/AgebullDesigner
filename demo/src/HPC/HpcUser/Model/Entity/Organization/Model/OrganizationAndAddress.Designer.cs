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
    /// 组织和地址
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class OrganizationAndAddressData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public OrganizationAndAddressData()
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
        public void ChangePrimaryKey(long oAID)
        {
            _oAID = oAID;
        }
        /// <summary>
        /// 助听器
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _oAID;

        partial void OnOAIDGet();

        partial void OnOAIDSet(ref long value);

        partial void OnOAIDLoad(ref long value);

        partial void OnOAIDSeted();

        
        /// <summary>
        /// 助听器
        /// </summary>
        [DataMember , JsonProperty("OAID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"助听器")]
        public long OAID
        {
            get
            {
                OnOAIDGet();
                return this._oAID;
            }
            set
            {
                if(this._oAID == value)
                    return;
                //if(this._oAID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnOAIDSet(ref value);
                this._oAID = value;
                this.OnPropertyChanged(_DataStruct_.Real_OAID);
                OnOAIDSeted();
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
        /// 地址
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _address;

        partial void OnAddressGet();

        partial void OnAddressSet(ref string value);

        partial void OnAddressSeted();

        
        /// <summary>
        /// 地址
        /// </summary>
        /// <value>
        /// 可存储400个字符.合理长度应不大于400.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Address", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"地址")]
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
        /// 人
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _person;

        partial void OnPersonGet();

        partial void OnPersonSet(ref string value);

        partial void OnPersonSeted();

        
        /// <summary>
        /// 人
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Person", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"人")]
        public  string Person
        {
            get
            {
                OnPersonGet();
                return this._person;
            }
            set
            {
                if(this._person == value)
                    return;
                OnPersonSet(ref value);
                this._person = value;
                OnPersonSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Person);
            }
        }
        /// <summary>
        /// 电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _phone;

        partial void OnPhoneGet();

        partial void OnPhoneSet(ref string value);

        partial void OnPhoneSeted();

        
        /// <summary>
        /// 电话
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Phone", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"电话")]
        public  string Phone
        {
            get
            {
                OnPhoneGet();
                return this._phone;
            }
            set
            {
                if(this._phone == value)
                    return;
                OnPhoneSet(ref value);
                this._phone = value;
                OnPhoneSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Phone);
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
                return this.OAID;
            }
            set
            {
                this.OAID = value;
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
            case "oaid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.OAID = vl;
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
            case "address":
                this.Address = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "person":
                this.Person = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "phone":
                this.Phone = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "oaid":
                this.OAID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "orgoid":
                this.OrgOID = (long)Convert.ToDecimal(value);
                return;
            case "address":
                this.Address = value == null ? null : value.ToString();
                return;
            case "person":
                this.Person = value == null ? null : value.ToString();
                return;
            case "phone":
                this.Phone = value == null ? null : value.ToString();
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
            case _DataStruct_.OAID:
                this.OAID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgOID:
                this.OrgOID = Convert.ToInt64(value);
                return;
            case _DataStruct_.Address:
                this.Address = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Person:
                this.Person = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Phone:
                this.Phone = value == null ? null : value.ToString();
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
            case "oaid":
                return this.OAID;
            case "sitesid":
                return this.SiteSID;
            case "orgoid":
                return this.OrgOID;
            case "address":
                return this.Address;
            case "person":
                return this.Person;
            case "phone":
                return this.Phone;
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
                case _DataStruct_.OAID:
                    return this.OAID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.OrgOID:
                    return this.OrgOID;
                case _DataStruct_.Address:
                    return this.Address;
                case _DataStruct_.Person:
                    return this.Person;
                case _DataStruct_.Phone:
                    return this.Phone;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(OrganizationAndAddressData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as OrganizationAndAddressData;
            if(sourceEntity == null)
                return;
            this._oAID = sourceEntity._oAID;
            this._siteSID = sourceEntity._siteSID;
            this._orgOID = sourceEntity._orgOID;
            this._address = sourceEntity._address;
            this._person = sourceEntity._person;
            this._phone = sourceEntity._phone;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(OrganizationAndAddressData source)
        {
                this.OAID = source.OAID;
                this.SiteSID = source.SiteSID;
                this.OrgOID = source.OrgOID;
                this.Address = source.Address;
                this.Person = source.Person;
                this.Phone = source.Phone;
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
                OnOAIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnOrgOIDModified(subsist,false);
                OnAddressModified(subsist,false);
                OnPersonModified(subsist,false);
                OnPhoneModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnOAIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnOrgOIDModified(subsist,true);
                OnAddressModified(subsist,true);
                OnPersonModified(subsist,true);
                OnPhoneModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[6] > 0)
            {
                OnOAIDModified(subsist,modifieds[_DataStruct_.Real_OAID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnOrgOIDModified(subsist,modifieds[_DataStruct_.Real_OrgOID] == 1);
                OnAddressModified(subsist,modifieds[_DataStruct_.Real_Address] == 1);
                OnPersonModified(subsist,modifieds[_DataStruct_.Real_Person] == 1);
                OnPhoneModified(subsist,modifieds[_DataStruct_.Real_Phone] == 1);
            }
        }

        /// <summary>
        /// 助听器修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOAIDModified(EntitySubsist subsist,bool isModified);

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
        /// 地址修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAddressModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 人修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPersonModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPhoneModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"OrganizationAndAddress";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"组织和地址";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"组织和地址";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "OAID";
            
            
            /// <summary>
            /// 助听器的数字标识
            /// </summary>
            public const byte OAID = 1;
            
            /// <summary>
            /// 助听器的实时记录顺序
            /// </summary>
            public const int Real_OAID = 0;

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
            /// 地址的数字标识
            /// </summary>
            public const byte Address = 4;
            
            /// <summary>
            /// 地址的实时记录顺序
            /// </summary>
            public const int Real_Address = 3;

            /// <summary>
            /// 人的数字标识
            /// </summary>
            public const byte Person = 5;
            
            /// <summary>
            /// 人的实时记录顺序
            /// </summary>
            public const int Real_Person = 4;

            /// <summary>
            /// 电话的数字标识
            /// </summary>
            public const byte Phone = 6;
            
            /// <summary>
            /// 电话的实时记录顺序
            /// </summary>
            public const int Real_Phone = 5;

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
                        Real_OAID,
                        new PropertySturct
                        {
                            Index        = OAID,
                            Name         = "OAID",
                            Title        = "助听器",
                            Caption      = @"助听器",
                            Description  = @"助听器",
                            ColumnName   = "OAID",
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
                        Real_Address,
                        new PropertySturct
                        {
                            Index        = Address,
                            Name         = "Address",
                            Title        = "地址",
                            Caption      = @"地址",
                            Description  = @"地址",
                            ColumnName   = "Address",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Person,
                        new PropertySturct
                        {
                            Index        = Person,
                            Name         = "Person",
                            Title        = "人",
                            Caption      = @"人",
                            Description  = @"人",
                            ColumnName   = "Person",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Phone,
                        new PropertySturct
                        {
                            Index        = Phone,
                            Name         = "Phone",
                            Title        = "电话",
                            Caption      = @"电话",
                            Description  = @"电话",
                            ColumnName   = "Phone",
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