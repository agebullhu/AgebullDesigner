/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/12 16:44:14*/
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
    /// 角色组织关联
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class PermitRoleHasOrgData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public PermitRoleHasOrgData()
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
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _oID;

        partial void OnOIDGet();

        partial void OnOIDSet(ref long value);

        partial void OnOIDLoad(ref long value);

        partial void OnOIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
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
        /// 站点编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _siteSID;

        partial void OnSiteSIDGet();

        partial void OnSiteSIDSet(ref long value);

        partial void OnSiteSIDSeted();

        
        /// <summary>
        /// 站点编号
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SiteSID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"站点编号")]
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
        /// 角色编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _roleRID;

        partial void OnRoleRIDGet();

        partial void OnRoleRIDSet(ref long value);

        partial void OnRoleRIDSeted();

        
        /// <summary>
        /// 角色编号
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("RoleRID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"角色编号")]
        public  long RoleRID
        {
            get
            {
                OnRoleRIDGet();
                return this._roleRID;
            }
            set
            {
                if(this._roleRID == value)
                    return;
                OnRoleRIDSet(ref value);
                this._roleRID = value;
                OnRoleRIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RoleRID);
            }
        }
        /// <summary>
        /// 组织编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _orgOID;

        partial void OnOrgOIDGet();

        partial void OnOrgOIDSet(ref long value);

        partial void OnOrgOIDSeted();

        
        /// <summary>
        /// 组织编号
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgOID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织编号")]
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
            case "rolerid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.RoleRID = vl;
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
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "rolerid":
                this.RoleRID = (long)Convert.ToDecimal(value);
                return;
            case "orgoid":
                this.OrgOID = (long)Convert.ToDecimal(value);
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
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.RoleRID:
                this.RoleRID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgOID:
                this.OrgOID = Convert.ToInt64(value);
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
            case "sitesid":
                return this.SiteSID;
            case "rolerid":
                return this.RoleRID;
            case "orgoid":
                return this.OrgOID;
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
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.RoleRID:
                    return this.RoleRID;
                case _DataStruct_.OrgOID:
                    return this.OrgOID;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(PermitRoleHasOrgData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as PermitRoleHasOrgData;
            if(sourceEntity == null)
                return;
            this._oID = sourceEntity._oID;
            this._siteSID = sourceEntity._siteSID;
            this._roleRID = sourceEntity._roleRID;
            this._orgOID = sourceEntity._orgOID;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(PermitRoleHasOrgData source)
        {
                this.OID = source.OID;
                this.SiteSID = source.SiteSID;
                this.RoleRID = source.RoleRID;
                this.OrgOID = source.OrgOID;
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
                OnSiteSIDModified(subsist,false);
                OnRoleRIDModified(subsist,false);
                OnOrgOIDModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnOIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnRoleRIDModified(subsist,true);
                OnOrgOIDModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[4] > 0)
            {
                OnOIDModified(subsist,modifieds[_DataStruct_.Real_OID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnRoleRIDModified(subsist,modifieds[_DataStruct_.Real_RoleRID] == 1);
                OnOrgOIDModified(subsist,modifieds[_DataStruct_.Real_OrgOID] == 1);
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
        partial void OnOIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 站点编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSiteSIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 角色编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRoleRIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 组织编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgOIDModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"PermitRoleHasOrg";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"角色组织关联";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"角色组织关联";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "OID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte OID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_OID = 0;

            /// <summary>
            /// 站点编号的数字标识
            /// </summary>
            public const byte SiteSID = 2;
            
            /// <summary>
            /// 站点编号的实时记录顺序
            /// </summary>
            public const int Real_SiteSID = 1;

            /// <summary>
            /// 角色编号的数字标识
            /// </summary>
            public const byte RoleRID = 3;
            
            /// <summary>
            /// 角色编号的实时记录顺序
            /// </summary>
            public const int Real_RoleRID = 2;

            /// <summary>
            /// 组织编号的数字标识
            /// </summary>
            public const byte OrgOID = 4;
            
            /// <summary>
            /// 组织编号的实时记录顺序
            /// </summary>
            public const int Real_OrgOID = 3;

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
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "OID",
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
                            Title        = "站点编号",
                            Caption      = @"站点编号",
                            Description  = @"站点编号",
                            ColumnName   = "SiteSID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RoleRID,
                        new PropertySturct
                        {
                            Index        = RoleRID,
                            Name         = "RoleRID",
                            Title        = "角色编号",
                            Caption      = @"角色编号",
                            Description  = @"角色编号",
                            ColumnName   = "RoleRID",
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
                            Title        = "组织编号",
                            Caption      = @"组织编号",
                            Description  = @"组织编号",
                            ColumnName   = "OrgOID",
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