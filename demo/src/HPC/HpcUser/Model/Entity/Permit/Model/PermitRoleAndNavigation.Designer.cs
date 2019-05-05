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
    /// 角色导航关联
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class PermitRoleAndNavigationData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public PermitRoleAndNavigationData()
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
        public void ChangePrimaryKey(long rNID)
        {
            _rNID = rNID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _rNID;

        partial void OnRNIDGet();

        partial void OnRNIDSet(ref long value);

        partial void OnRNIDLoad(ref long value);

        partial void OnRNIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("RNID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long RNID
        {
            get
            {
                OnRNIDGet();
                return this._rNID;
            }
            set
            {
                if(this._rNID == value)
                    return;
                //if(this._rNID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnRNIDSet(ref value);
                this._rNID = value;
                this.OnPropertyChanged(_DataStruct_.Real_RNID);
                OnRNIDSeted();
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
        /// 角色扮演
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _roleRID;

        partial void OnRoleRIDGet();

        partial void OnRoleRIDSet(ref long value);

        partial void OnRoleRIDSeted();

        
        /// <summary>
        /// 角色扮演
        /// </summary>
        [DataMember , JsonProperty("RoleRID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"角色扮演")]
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
        /// 导航关联标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _navigationSubSID;

        partial void OnNavigationSubSIDGet();

        partial void OnNavigationSubSIDSet(ref long value);

        partial void OnNavigationSubSIDSeted();

        
        /// <summary>
        /// 导航关联标识
        /// </summary>
        [DataMember , JsonProperty("NavigationSubSID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"导航关联标识")]
        public  long NavigationSubSID
        {
            get
            {
                OnNavigationSubSIDGet();
                return this._navigationSubSID;
            }
            set
            {
                if(this._navigationSubSID == value)
                    return;
                OnNavigationSubSIDSet(ref value);
                this._navigationSubSID = value;
                OnNavigationSubSIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_NavigationSubSID);
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
                return this.RNID;
            }
            set
            {
                this.RNID = value;
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
            case "rnid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.RNID = vl;
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
            case "navigationsubsid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.NavigationSubSID = vl;
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
            case "rnid":
                this.RNID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "rolerid":
                this.RoleRID = (long)Convert.ToDecimal(value);
                return;
            case "navigationsubsid":
                this.NavigationSubSID = (long)Convert.ToDecimal(value);
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
            case _DataStruct_.RNID:
                this.RNID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.RoleRID:
                this.RoleRID = Convert.ToInt64(value);
                return;
            case _DataStruct_.NavigationSubSID:
                this.NavigationSubSID = Convert.ToInt64(value);
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
            case "rnid":
                return this.RNID;
            case "sitesid":
                return this.SiteSID;
            case "rolerid":
                return this.RoleRID;
            case "navigationsubsid":
                return this.NavigationSubSID;
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
                case _DataStruct_.RNID:
                    return this.RNID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.RoleRID:
                    return this.RoleRID;
                case _DataStruct_.NavigationSubSID:
                    return this.NavigationSubSID;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(PermitRoleAndNavigationData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as PermitRoleAndNavigationData;
            if(sourceEntity == null)
                return;
            this._rNID = sourceEntity._rNID;
            this._siteSID = sourceEntity._siteSID;
            this._roleRID = sourceEntity._roleRID;
            this._navigationSubSID = sourceEntity._navigationSubSID;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(PermitRoleAndNavigationData source)
        {
                this.RNID = source.RNID;
                this.SiteSID = source.SiteSID;
                this.RoleRID = source.RoleRID;
                this.NavigationSubSID = source.NavigationSubSID;
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
                OnRNIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnRoleRIDModified(subsist,false);
                OnNavigationSubSIDModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnRNIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnRoleRIDModified(subsist,true);
                OnNavigationSubSIDModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[4] > 0)
            {
                OnRNIDModified(subsist,modifieds[_DataStruct_.Real_RNID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnRoleRIDModified(subsist,modifieds[_DataStruct_.Real_RoleRID] == 1);
                OnNavigationSubSIDModified(subsist,modifieds[_DataStruct_.Real_NavigationSubSID] == 1);
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
        partial void OnRNIDModified(EntitySubsist subsist,bool isModified);

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
        /// 角色扮演修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRoleRIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 导航关联标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnNavigationSubSIDModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"PermitRoleAndNavigation";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"角色导航关联";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"角色导航关联";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "RNID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte RNID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_RNID = 0;

            /// <summary>
            /// 站点标识的数字标识
            /// </summary>
            public const byte SiteSID = 2;
            
            /// <summary>
            /// 站点标识的实时记录顺序
            /// </summary>
            public const int Real_SiteSID = 1;

            /// <summary>
            /// 角色扮演的数字标识
            /// </summary>
            public const byte RoleRID = 3;
            
            /// <summary>
            /// 角色扮演的实时记录顺序
            /// </summary>
            public const int Real_RoleRID = 2;

            /// <summary>
            /// 导航关联标识的数字标识
            /// </summary>
            public const byte NavigationSubSID = 4;
            
            /// <summary>
            /// 导航关联标识的实时记录顺序
            /// </summary>
            public const int Real_NavigationSubSID = 3;

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
                        Real_RNID,
                        new PropertySturct
                        {
                            Index        = RNID,
                            Name         = "RNID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "RNID",
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
                        Real_RoleRID,
                        new PropertySturct
                        {
                            Index        = RoleRID,
                            Name         = "RoleRID",
                            Title        = "角色扮演",
                            Caption      = @"角色扮演",
                            Description  = @"角色扮演",
                            ColumnName   = "RoleRID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_NavigationSubSID,
                        new PropertySturct
                        {
                            Index        = NavigationSubSID,
                            Name         = "NavigationSubSID",
                            Title        = "导航关联标识",
                            Caption      = @"导航关联标识",
                            Description  = @"导航关联标识",
                            ColumnName   = "NavigationSubSID",
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