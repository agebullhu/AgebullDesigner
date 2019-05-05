/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/15 10:58:48*/
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

using Agebull.Common.Organizations;
using Agebull.Common.OAuth;
#endregion

namespace Agebull.Common.AppManage
{
    /// <summary>
    /// 角色组织关联
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class RoleOrgData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public RoleOrgData()
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
        public void ChangePrimaryKey(long roleOrgId)
        {
            _roleOrgId = roleOrgId;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _roleOrgId;

        partial void OnRoleOrgIdGet();

        partial void OnRoleOrgIdSet(ref long value);

        partial void OnRoleOrgIdLoad(ref long value);

        partial void OnRoleOrgIdSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("RoleOrgId", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long RoleOrgId
        {
            get
            {
                OnRoleOrgIdGet();
                return this._roleOrgId;
            }
            set
            {
                if(this._roleOrgId == value)
                    return;
                //if(this._roleOrgId > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnRoleOrgIdSet(ref value);
                this._roleOrgId = value;
                this.OnPropertyChanged(_DataStruct_.Real_RoleOrgId);
                OnRoleOrgIdSeted();
            }
        }
        /// <summary>
        /// 站点编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _siteID;

        partial void OnSiteIDGet();

        partial void OnSiteIDSet(ref long value);

        partial void OnSiteIDSeted();

        
        /// <summary>
        /// 站点编号
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SiteID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"站点编号")]
        public  long SiteID
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
        /// 角色编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _roleID;

        partial void OnRoleIDGet();

        partial void OnRoleIDSet(ref long value);

        partial void OnRoleIDSeted();

        
        /// <summary>
        /// 角色编号
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("RoleID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"角色编号")]
        public  long RoleID
        {
            get
            {
                OnRoleIDGet();
                return this._roleID;
            }
            set
            {
                if(this._roleID == value)
                    return;
                OnRoleIDSet(ref value);
                this._roleID = value;
                OnRoleIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RoleID);
            }
        }
        /// <summary>
        /// 组织编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _orgID;

        partial void OnOrgIDGet();

        partial void OnOrgIDSet(ref long value);

        partial void OnOrgIDSeted();

        
        /// <summary>
        /// 组织编号
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织编号")]
        public  long OrgID
        {
            get
            {
                OnOrgIDGet();
                return this._orgID;
            }
            set
            {
                if(this._orgID == value)
                    return;
                OnOrgIDSet(ref value);
                this._orgID = value;
                OnOrgIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgID);
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
                return this.RoleOrgId;
            }
            set
            {
                this.RoleOrgId = value;
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
            case "roleorgid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.RoleOrgId = vl;
                        return true;
                    }
                }
                return false;
            case "siteid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.SiteID = vl;
                        return true;
                    }
                }
                return false;
            case "roleid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.RoleID = vl;
                        return true;
                    }
                }
                return false;
            case "orgid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.OrgID = vl;
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
            case "roleorgid":
                this.RoleOrgId = (long)Convert.ToDecimal(value);
                return;
            case "siteid":
                this.SiteID = (long)Convert.ToDecimal(value);
                return;
            case "roleid":
                this.RoleID = (long)Convert.ToDecimal(value);
                return;
            case "orgid":
                this.OrgID = (long)Convert.ToDecimal(value);
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
            case _DataStruct_.RoleOrgId:
                this.RoleOrgId = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteID:
                this.SiteID = Convert.ToInt64(value);
                return;
            case _DataStruct_.RoleID:
                this.RoleID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgID:
                this.OrgID = Convert.ToInt64(value);
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
            case "roleorgid":
                return this.RoleOrgId;
            case "siteid":
                return this.SiteID;
            case "roleid":
                return this.RoleID;
            case "orgid":
                return this.OrgID;
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
                case _DataStruct_.RoleOrgId:
                    return this.RoleOrgId;
                case _DataStruct_.SiteID:
                    return this.SiteID;
                case _DataStruct_.RoleID:
                    return this.RoleID;
                case _DataStruct_.OrgID:
                    return this.OrgID;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(RoleOrgData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as RoleOrgData;
            if(sourceEntity == null)
                return;
            this._roleOrgId = sourceEntity._roleOrgId;
            this._siteID = sourceEntity._siteID;
            this._roleID = sourceEntity._roleID;
            this._orgID = sourceEntity._orgID;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(RoleOrgData source)
        {
                this.RoleOrgId = source.RoleOrgId;
                this.SiteID = source.SiteID;
                this.RoleID = source.RoleID;
                this.OrgID = source.OrgID;
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
                OnRoleOrgIdModified(subsist,false);
                OnSiteIDModified(subsist,false);
                OnRoleIDModified(subsist,false);
                OnOrgIDModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnRoleOrgIdModified(subsist,true);
                OnSiteIDModified(subsist,true);
                OnRoleIDModified(subsist,true);
                OnOrgIDModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[4] > 0)
            {
                OnRoleOrgIdModified(subsist,modifieds[_DataStruct_.Real_RoleOrgId] == 1);
                OnSiteIDModified(subsist,modifieds[_DataStruct_.Real_SiteID] == 1);
                OnRoleIDModified(subsist,modifieds[_DataStruct_.Real_RoleID] == 1);
                OnOrgIDModified(subsist,modifieds[_DataStruct_.Real_OrgID] == 1);
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
        partial void OnRoleOrgIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 站点编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSiteIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 角色编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRoleIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 组织编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgIDModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"RoleOrg";
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
            public const string EntityPrimaryKey = "RoleOrgId";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte RoleOrgId = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_RoleOrgId = 0;

            /// <summary>
            /// 站点编号的数字标识
            /// </summary>
            public const byte SiteID = 2;
            
            /// <summary>
            /// 站点编号的实时记录顺序
            /// </summary>
            public const int Real_SiteID = 1;

            /// <summary>
            /// 角色编号的数字标识
            /// </summary>
            public const byte RoleID = 3;
            
            /// <summary>
            /// 角色编号的实时记录顺序
            /// </summary>
            public const int Real_RoleID = 2;

            /// <summary>
            /// 组织编号的数字标识
            /// </summary>
            public const byte OrgID = 4;
            
            /// <summary>
            /// 组织编号的实时记录顺序
            /// </summary>
            public const int Real_OrgID = 3;

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
                        Real_RoleOrgId,
                        new PropertySturct
                        {
                            Index        = RoleOrgId,
                            Name         = "RoleOrgId",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "role_org_id",
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
                            Title        = "站点编号",
                            Caption      = @"站点编号",
                            Description  = @"站点编号",
                            ColumnName   = "site_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RoleID,
                        new PropertySturct
                        {
                            Index        = RoleID,
                            Name         = "RoleID",
                            Title        = "角色编号",
                            Caption      = @"角色编号",
                            Description  = @"角色编号",
                            ColumnName   = "role_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrgID,
                        new PropertySturct
                        {
                            Index        = OrgID,
                            Name         = "OrgID",
                            Title        = "组织编号",
                            Caption      = @"组织编号",
                            Description  = @"组织编号",
                            ColumnName   = "org_id",
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