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
    /// 相对组织中的组织
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class OrganizationInOrgRelativeData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public OrganizationInOrgRelativeData()
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
        public void ChangePrimaryKey(long oRID)
        {
            _oRID = oRID;
        }
        /// <summary>
        /// 奥里德
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _oRID;

        partial void OnORIDGet();

        partial void OnORIDSet(ref long value);

        partial void OnORIDLoad(ref long value);

        partial void OnORIDSeted();

        
        /// <summary>
        /// 奥里德
        /// </summary>
        [DataMember , JsonProperty("ORID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"奥里德")]
        public long ORID
        {
            get
            {
                OnORIDGet();
                return this._oRID;
            }
            set
            {
                if(this._oRID == value)
                    return;
                //if(this._oRID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnORIDSet(ref value);
                this._oRID = value;
                this.OnPropertyChanged(_DataStruct_.Real_ORID);
                OnORIDSeted();
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
        /// SOID
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _sOID;

        partial void OnSOIDGet();

        partial void OnSOIDSet(ref long value);

        partial void OnSOIDSeted();

        
        /// <summary>
        /// SOID
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SOID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"SOID")]
        public  long SOID
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
                OnSOIDSet(ref value);
                this._sOID = value;
                OnSOIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SOID);
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
        /// 可存储40个字符.合理长度应不大于40.
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
                return this.ORID;
            }
            set
            {
                this.ORID = value;
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
            case "orid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.ORID = vl;
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
            case "orid":
                this.ORID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "orgoid":
                this.OrgOID = (long)Convert.ToDecimal(value);
                return;
            case "soid":
                this.SOID = (long)Convert.ToDecimal(value);
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
            case _DataStruct_.ORID:
                this.ORID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgOID:
                this.OrgOID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SOID:
                this.SOID = Convert.ToInt64(value);
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
            case "orid":
                return this.ORID;
            case "sitesid":
                return this.SiteSID;
            case "orgoid":
                return this.OrgOID;
            case "soid":
                return this.SOID;
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
                case _DataStruct_.ORID:
                    return this.ORID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.OrgOID:
                    return this.OrgOID;
                case _DataStruct_.SOID:
                    return this.SOID;
                case _DataStruct_.Relative:
                    return this.Relative;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(OrganizationInOrgRelativeData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as OrganizationInOrgRelativeData;
            if(sourceEntity == null)
                return;
            this._oRID = sourceEntity._oRID;
            this._siteSID = sourceEntity._siteSID;
            this._orgOID = sourceEntity._orgOID;
            this._sOID = sourceEntity._sOID;
            this._relative = sourceEntity._relative;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(OrganizationInOrgRelativeData source)
        {
                this.ORID = source.ORID;
                this.SiteSID = source.SiteSID;
                this.OrgOID = source.OrgOID;
                this.SOID = source.SOID;
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
                OnORIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnOrgOIDModified(subsist,false);
                OnSOIDModified(subsist,false);
                OnRelativeModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnORIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnOrgOIDModified(subsist,true);
                OnSOIDModified(subsist,true);
                OnRelativeModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[5] > 0)
            {
                OnORIDModified(subsist,modifieds[_DataStruct_.Real_ORID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnOrgOIDModified(subsist,modifieds[_DataStruct_.Real_OrgOID] == 1);
                OnSOIDModified(subsist,modifieds[_DataStruct_.Real_SOID] == 1);
                OnRelativeModified(subsist,modifieds[_DataStruct_.Real_Relative] == 1);
            }
        }

        /// <summary>
        /// 奥里德修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnORIDModified(EntitySubsist subsist,bool isModified);

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
        /// SOID修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSOIDModified(EntitySubsist subsist,bool isModified);

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
            public const string EntityName = @"OrganizationInOrgRelative";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"相对组织中的组织";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"相对组织中的组织";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "ORID";
            
            
            /// <summary>
            /// 奥里德的数字标识
            /// </summary>
            public const byte ORID = 1;
            
            /// <summary>
            /// 奥里德的实时记录顺序
            /// </summary>
            public const int Real_ORID = 0;

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
            /// SOID的数字标识
            /// </summary>
            public const byte SOID = 4;
            
            /// <summary>
            /// SOID的实时记录顺序
            /// </summary>
            public const int Real_SOID = 3;

            /// <summary>
            /// 相对的数字标识
            /// </summary>
            public const byte Relative = 5;
            
            /// <summary>
            /// 相对的实时记录顺序
            /// </summary>
            public const int Real_Relative = 4;

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
                        Real_ORID,
                        new PropertySturct
                        {
                            Index        = ORID,
                            Name         = "ORID",
                            Title        = "奥里德",
                            Caption      = @"奥里德",
                            Description  = @"奥里德",
                            ColumnName   = "ORID",
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