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
    /// 组织和员工
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class OrganizationAndEmployeeData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public OrganizationAndEmployeeData()
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
        public void ChangePrimaryKey(long oEID)
        {
            _oEID = oEID;
        }
        /// <summary>
        /// OEID
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _oEID;

        partial void OnOEIDGet();

        partial void OnOEIDSet(ref long value);

        partial void OnOEIDLoad(ref long value);

        partial void OnOEIDSeted();

        
        /// <summary>
        /// OEID
        /// </summary>
        [DataMember , JsonProperty("OEID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"OEID")]
        public long OEID
        {
            get
            {
                OnOEIDGet();
                return this._oEID;
            }
            set
            {
                if(this._oEID == value)
                    return;
                //if(this._oEID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnOEIDSet(ref value);
                this._oEID = value;
                this.OnPropertyChanged(_DataStruct_.Real_OEID);
                OnOEIDSeted();
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
        /// 电磁脉冲
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _empEID;

        partial void OnEmpEIDGet();

        partial void OnEmpEIDSet(ref long value);

        partial void OnEmpEIDSeted();

        
        /// <summary>
        /// 电磁脉冲
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("EmpEID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"电磁脉冲")]
        public  long EmpEID
        {
            get
            {
                OnEmpEIDGet();
                return this._empEID;
            }
            set
            {
                if(this._empEID == value)
                    return;
                OnEmpEIDSet(ref value);
                this._empEID = value;
                OnEmpEIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_EmpEID);
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
                return this.OEID;
            }
            set
            {
                this.OEID = value;
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
            case "oeid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.OEID = vl;
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
            case "empeid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.EmpEID = vl;
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
            case "oeid":
                this.OEID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "orgoid":
                this.OrgOID = (long)Convert.ToDecimal(value);
                return;
            case "empeid":
                this.EmpEID = (long)Convert.ToDecimal(value);
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
            case _DataStruct_.OEID:
                this.OEID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgOID:
                this.OrgOID = Convert.ToInt64(value);
                return;
            case _DataStruct_.EmpEID:
                this.EmpEID = Convert.ToInt64(value);
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
            case "oeid":
                return this.OEID;
            case "sitesid":
                return this.SiteSID;
            case "orgoid":
                return this.OrgOID;
            case "empeid":
                return this.EmpEID;
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
                case _DataStruct_.OEID:
                    return this.OEID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.OrgOID:
                    return this.OrgOID;
                case _DataStruct_.EmpEID:
                    return this.EmpEID;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(OrganizationAndEmployeeData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as OrganizationAndEmployeeData;
            if(sourceEntity == null)
                return;
            this._oEID = sourceEntity._oEID;
            this._siteSID = sourceEntity._siteSID;
            this._orgOID = sourceEntity._orgOID;
            this._empEID = sourceEntity._empEID;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(OrganizationAndEmployeeData source)
        {
                this.OEID = source.OEID;
                this.SiteSID = source.SiteSID;
                this.OrgOID = source.OrgOID;
                this.EmpEID = source.EmpEID;
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
                OnOEIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnOrgOIDModified(subsist,false);
                OnEmpEIDModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnOEIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnOrgOIDModified(subsist,true);
                OnEmpEIDModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[4] > 0)
            {
                OnOEIDModified(subsist,modifieds[_DataStruct_.Real_OEID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnOrgOIDModified(subsist,modifieds[_DataStruct_.Real_OrgOID] == 1);
                OnEmpEIDModified(subsist,modifieds[_DataStruct_.Real_EmpEID] == 1);
            }
        }

        /// <summary>
        /// OEID修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOEIDModified(EntitySubsist subsist,bool isModified);

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
        /// 电磁脉冲修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnEmpEIDModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"OrganizationAndEmployee";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"组织和员工";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"组织和员工";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "OEID";
            
            
            /// <summary>
            /// OEID的数字标识
            /// </summary>
            public const byte OEID = 1;
            
            /// <summary>
            /// OEID的实时记录顺序
            /// </summary>
            public const int Real_OEID = 0;

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
            /// 电磁脉冲的数字标识
            /// </summary>
            public const byte EmpEID = 4;
            
            /// <summary>
            /// 电磁脉冲的实时记录顺序
            /// </summary>
            public const int Real_EmpEID = 3;

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
                        Real_OEID,
                        new PropertySturct
                        {
                            Index        = OEID,
                            Name         = "OEID",
                            Title        = "OEID",
                            Caption      = @"OEID",
                            Description  = @"OEID",
                            ColumnName   = "OEID",
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
                        Real_EmpEID,
                        new PropertySturct
                        {
                            Index        = EmpEID,
                            Name         = "EmpEID",
                            Title        = "电磁脉冲",
                            Caption      = @"电磁脉冲",
                            Description  = @"电磁脉冲",
                            ColumnName   = "EmpEID",
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