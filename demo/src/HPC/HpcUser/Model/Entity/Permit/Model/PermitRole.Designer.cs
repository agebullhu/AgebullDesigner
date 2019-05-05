/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/12 21:30:55*/
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
    /// 角色
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class PermitRoleData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public PermitRoleData()
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
        public void ChangePrimaryKey(long rID)
        {
            _rID = rID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _rID;

        partial void OnRIDGet();

        partial void OnRIDSet(ref long value);

        partial void OnRIDLoad(ref long value);

        partial void OnRIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("RID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long RID
        {
            get
            {
                OnRIDGet();
                return this._rID;
            }
            set
            {
                if(this._rID == value)
                    return;
                //if(this._rID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnRIDSet(ref value);
                this._rID = value;
                this.OnPropertyChanged(_DataStruct_.Real_RID);
                OnRIDSeted();
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
        /// 角色名
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _roleName;

        partial void OnRoleNameGet();

        partial void OnRoleNameSet(ref string value);

        partial void OnRoleNameSeted();

        
        /// <summary>
        /// 角色名
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("RoleName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"角色名")]
        public  string RoleName
        {
            get
            {
                OnRoleNameGet();
                return this._roleName;
            }
            set
            {
                if(this._roleName == value)
                    return;
                OnRoleNameSet(ref value);
                this._roleName = value;
                OnRoleNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RoleName);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _remark;

        partial void OnRemarkGet();

        partial void OnRemarkSet(ref string value);

        partial void OnRemarkSeted();

        
        /// <summary>
        /// 备注
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Remark", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"备注")]
        public  string Remark
        {
            get
            {
                OnRemarkGet();
                return this._remark;
            }
            set
            {
                if(this._remark == value)
                    return;
                OnRemarkSet(ref value);
                this._remark = value;
                OnRemarkSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Remark);
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
                return this.RID;
            }
            set
            {
                this.RID = value;
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
            case "rid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.RID = vl;
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
            case "rolename":
                this.RoleName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "remark":
                this.Remark = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "rid":
                this.RID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "orgoid":
                this.OrgOID = (long)Convert.ToDecimal(value);
                return;
            case "rolename":
                this.RoleName = value == null ? null : value.ToString();
                return;
            case "remark":
                this.Remark = value == null ? null : value.ToString();
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
            case _DataStruct_.RID:
                this.RID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgOID:
                this.OrgOID = Convert.ToInt64(value);
                return;
            case _DataStruct_.RoleName:
                this.RoleName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Remark:
                this.Remark = value == null ? null : value.ToString();
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
            case "rid":
                return this.RID;
            case "sitesid":
                return this.SiteSID;
            case "orgoid":
                return this.OrgOID;
            case "rolename":
                return this.RoleName;
            case "remark":
                return this.Remark;
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
                case _DataStruct_.RID:
                    return this.RID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.OrgOID:
                    return this.OrgOID;
                case _DataStruct_.RoleName:
                    return this.RoleName;
                case _DataStruct_.Remark:
                    return this.Remark;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(PermitRoleData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as PermitRoleData;
            if(sourceEntity == null)
                return;
            this._rID = sourceEntity._rID;
            this._siteSID = sourceEntity._siteSID;
            this._orgOID = sourceEntity._orgOID;
            this._roleName = sourceEntity._roleName;
            this._remark = sourceEntity._remark;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(PermitRoleData source)
        {
                this.RID = source.RID;
                this.SiteSID = source.SiteSID;
                this.OrgOID = source.OrgOID;
                this.RoleName = source.RoleName;
                this.Remark = source.Remark;
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
                OnRIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnOrgOIDModified(subsist,false);
                OnRoleNameModified(subsist,false);
                OnRemarkModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnRIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnOrgOIDModified(subsist,true);
                OnRoleNameModified(subsist,true);
                OnRemarkModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[5] > 0)
            {
                OnRIDModified(subsist,modifieds[_DataStruct_.Real_RID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnOrgOIDModified(subsist,modifieds[_DataStruct_.Real_OrgOID] == 1);
                OnRoleNameModified(subsist,modifieds[_DataStruct_.Real_RoleName] == 1);
                OnRemarkModified(subsist,modifieds[_DataStruct_.Real_Remark] == 1);
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
        partial void OnRIDModified(EntitySubsist subsist,bool isModified);

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
        /// 角色名修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRoleNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 备注修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRemarkModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"PermitRole";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"角色";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"角色";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "RID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte RID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_RID = 0;

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
            /// 角色名的数字标识
            /// </summary>
            public const byte RoleName = 4;
            
            /// <summary>
            /// 角色名的实时记录顺序
            /// </summary>
            public const int Real_RoleName = 3;

            /// <summary>
            /// 备注的数字标识
            /// </summary>
            public const byte Remark = 5;
            
            /// <summary>
            /// 备注的实时记录顺序
            /// </summary>
            public const int Real_Remark = 4;

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
                        Real_RID,
                        new PropertySturct
                        {
                            Index        = RID,
                            Name         = "RID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "RID",
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
                        Real_RoleName,
                        new PropertySturct
                        {
                            Index        = RoleName,
                            Name         = "RoleName",
                            Title        = "角色名",
                            Caption      = @"角色名",
                            Description  = @"角色名",
                            ColumnName   = "RoleName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Remark,
                        new PropertySturct
                        {
                            Index        = Remark,
                            Name         = "Remark",
                            Title        = "备注",
                            Caption      = @"备注",
                            Description  = @"备注",
                            ColumnName   = "Remark",
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