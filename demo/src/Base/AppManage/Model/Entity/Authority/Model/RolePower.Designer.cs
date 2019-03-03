/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/1 15:31:53*/
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
using Agebull.Common.DataModel;
using Gboxt.Common.DataModel;
using Gboxt.Common.DataModel.Extends;
using Agebull.Common.WebApi;

using Agebull.Common.WebApi.Auth;
#endregion

namespace Agebull.Common.AppManage
{
    /// <summary>
    /// 角色权限
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class RolePowerData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public RolePowerData()
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
        public void ChangePrimaryKey(long iD)
        {
            _iD = iD;
        }
        /// <summary>
        /// 标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _iD;

        partial void OnIDGet();

        partial void OnIDSet(ref long value);

        partial void OnIDLoad(ref long value);

        partial void OnIDSeted();

        
        /// <summary>
        /// 标识
        /// </summary>
        [DataMember , JsonProperty("ID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"标识")]
        public long ID
        {
            get
            {
                OnIDGet();
                return this._iD;
            }
            set
            {
                if(this._iD == value)
                    return;
                //if(this._iD > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnIDSet(ref value);
                this._iD = value;
                this.OnPropertyChanged(_DataStruct_.Real_ID);
                OnIDSeted();
            }
        }
        /// <summary>
        /// 页面标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _pageItemId;

        partial void OnPageItemIdGet();

        partial void OnPageItemIdSet(ref long value);

        partial void OnPageItemIdSeted();

        
        /// <summary>
        /// 页面标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("PageItemId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"页面标识")]
        public  long PageItemId
        {
            get
            {
                OnPageItemIdGet();
                return this._pageItemId;
            }
            set
            {
                if(this._pageItemId == value)
                    return;
                OnPageItemIdSet(ref value);
                this._pageItemId = value;
                OnPageItemIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_PageItemId);
            }
        }
        /// <summary>
        /// 角色标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _roleId;

        partial void OnRoleIdGet();

        partial void OnRoleIdSet(ref long value);

        partial void OnRoleIdSeted();

        
        /// <summary>
        /// 角色标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("RoleId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"角色标识")]
        public  long RoleId
        {
            get
            {
                OnRoleIdGet();
                return this._roleId;
            }
            set
            {
                if(this._roleId == value)
                    return;
                OnRoleIdSet(ref value);
                this._roleId = value;
                OnRoleIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RoleId);
            }
        }
        /// <summary>
        /// 权限
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public RolePowerType _power;

        partial void OnPowerGet();

        partial void OnPowerSet(ref RolePowerType value);

        partial void OnPowerSeted();

        
        /// <summary>
        /// 权限
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Power", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"权限")]
        public  RolePowerType Power
        {
            get
            {
                OnPowerGet();
                return this._power;
            }
            set
            {
                if(this._power == value)
                    return;
                OnPowerSet(ref value);
                this._power = value;
                OnPowerSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Power);
            }
        }
        /// <summary>
        /// 权限的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("权限")]
        public string Power_Content => Power.ToCaption();

        /// <summary>
        /// 权限的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int Power_Number
        {
            get => (int)this.Power;
            set => this.Power = (RolePowerType)value;
        }
        /// <summary>
        /// 权限范围
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public SubjectionType _dataScope;

        partial void OnDataScopeGet();

        partial void OnDataScopeSet(ref SubjectionType value);

        partial void OnDataScopeSeted();

        
        /// <summary>
        /// 权限范围
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("DataScope", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"权限范围")]
        public  SubjectionType DataScope
        {
            get
            {
                OnDataScopeGet();
                return this._dataScope;
            }
            set
            {
                if(this._dataScope == value)
                    return;
                OnDataScopeSet(ref value);
                this._dataScope = value;
                OnDataScopeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_DataScope);
            }
        }
        /// <summary>
        /// 权限范围的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("权限范围")]
        public string DataScope_Content => DataScope.ToCaption();

        /// <summary>
        /// 权限范围的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int DataScope_Number
        {
            get => (int)this.DataScope;
            set => this.DataScope = (SubjectionType)value;
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
                return this.ID;
            }
            set
            {
                this.ID = value;
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
        protected override void SetValueInner(string property, object value)
        {
            if(property == null) return;
            switch(property.Trim().ToLower())
            {
            case "id":
                this.ID = (long)Convert.ToDecimal(value);
                return;
            case "pageitemid":
                this.PageItemId = (long)Convert.ToDecimal(value);
                return;
            case "roleid":
                this.RoleId = (long)Convert.ToDecimal(value);
                return;
            case "power":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.Power = (RolePowerType)(int)value;
                    }
                    else if(value is RolePowerType)
                    {
                        this.Power = (RolePowerType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        RolePowerType val;
                        if (RolePowerType.TryParse(str, out val))
                        {
                            this.Power = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.Power = (RolePowerType)vl;
                            }
                        }
                    }
                }
                return;
            case "datascope":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.DataScope = (SubjectionType)(int)value;
                    }
                    else if(value is SubjectionType)
                    {
                        this.DataScope = (SubjectionType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        SubjectionType val;
                        if (SubjectionType.TryParse(str, out val))
                        {
                            this.DataScope = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.DataScope = (SubjectionType)vl;
                            }
                        }
                    }
                }
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
            case _DataStruct_.ID:
                this.ID = Convert.ToInt64(value);
                return;
            case _DataStruct_.PageItemId:
                this.PageItemId = Convert.ToInt64(value);
                return;
            case _DataStruct_.RoleId:
                this.RoleId = Convert.ToInt64(value);
                return;
            case _DataStruct_.Power:
                this.Power = (RolePowerType)value;
                return;
            case _DataStruct_.DataScope:
                this.DataScope = (SubjectionType)value;
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
            case "id":
                return this.ID;
            case "pageitemid":
                return this.PageItemId;
            case "roleid":
                return this.RoleId;
            case "power":
                return this.Power.ToCaption();
            case "datascope":
                return this.DataScope.ToCaption();
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
                case _DataStruct_.ID:
                    return this.ID;
                case _DataStruct_.PageItemId:
                    return this.PageItemId;
                case _DataStruct_.RoleId:
                    return this.RoleId;
                case _DataStruct_.Power:
                    return this.Power;
                case _DataStruct_.DataScope:
                    return this.DataScope;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(RolePowerData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as RolePowerData;
            if(sourceEntity == null)
                return;
            this._iD = sourceEntity._iD;
            this._pageItemId = sourceEntity._pageItemId;
            this._roleId = sourceEntity._roleId;
            this._power = sourceEntity._power;
            this._dataScope = sourceEntity._dataScope;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(RolePowerData source)
        {
                this.ID = source.ID;
                this.PageItemId = source.PageItemId;
                this.RoleId = source.RoleId;
                this.Power = source.Power;
                this.DataScope = source.DataScope;
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
                OnIDModified(subsist,false);
                OnPageItemIdModified(subsist,false);
                OnRoleIdModified(subsist,false);
                OnPowerModified(subsist,false);
                OnDataScopeModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIDModified(subsist,true);
                OnPageItemIdModified(subsist,true);
                OnRoleIdModified(subsist,true);
                OnPowerModified(subsist,true);
                OnDataScopeModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[5] > 0)
            {
                OnIDModified(subsist,modifieds[_DataStruct_.Real_ID] == 1);
                OnPageItemIdModified(subsist,modifieds[_DataStruct_.Real_PageItemId] == 1);
                OnRoleIdModified(subsist,modifieds[_DataStruct_.Real_RoleId] == 1);
                OnPowerModified(subsist,modifieds[_DataStruct_.Real_Power] == 1);
                OnDataScopeModified(subsist,modifieds[_DataStruct_.Real_DataScope] == 1);
            }
        }

        /// <summary>
        /// 标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 页面标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPageItemIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 角色标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRoleIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 权限修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPowerModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 权限范围修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDataScopeModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"RolePower";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"角色权限";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"角色权限";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x20008;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "ID";
            
            
            /// <summary>
            /// 标识的数字标识
            /// </summary>
            public const byte ID = 1;
            
            /// <summary>
            /// 标识的实时记录顺序
            /// </summary>
            public const int Real_ID = 0;

            /// <summary>
            /// 页面标识的数字标识
            /// </summary>
            public const byte PageItemId = 2;
            
            /// <summary>
            /// 页面标识的实时记录顺序
            /// </summary>
            public const int Real_PageItemId = 1;

            /// <summary>
            /// 角色标识的数字标识
            /// </summary>
            public const byte RoleId = 3;
            
            /// <summary>
            /// 角色标识的实时记录顺序
            /// </summary>
            public const int Real_RoleId = 2;

            /// <summary>
            /// 权限的数字标识
            /// </summary>
            public const byte Power = 4;
            
            /// <summary>
            /// 权限的实时记录顺序
            /// </summary>
            public const int Real_Power = 3;

            /// <summary>
            /// 权限范围的数字标识
            /// </summary>
            public const byte DataScope = 5;
            
            /// <summary>
            /// 权限范围的实时记录顺序
            /// </summary>
            public const int Real_DataScope = 4;

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
                        Real_ID,
                        new PropertySturct
                        {
                            Index        = ID,
                            Name         = "ID",
                            Title        = "标识",
                            Caption      = @"标识",
                            Description  = @"标识",
                            ColumnName   = "id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PageItemId,
                        new PropertySturct
                        {
                            Index        = PageItemId,
                            Name         = "PageItemId",
                            Title        = "页面标识",
                            Caption      = @"页面标识",
                            Description  = @"页面标识",
                            ColumnName   = "page_item_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RoleId,
                        new PropertySturct
                        {
                            Index        = RoleId,
                            Name         = "RoleId",
                            Title        = "角色标识",
                            Caption      = @"角色标识",
                            Description  = @"角色标识",
                            ColumnName   = "role_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Power,
                        new PropertySturct
                        {
                            Index        = Power,
                            Name         = "Power",
                            Title        = "权限",
                            Caption      = @"权限",
                            Description  = @"权限",
                            ColumnName   = "power",
                            PropertyType = typeof(RolePowerType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_DataScope,
                        new PropertySturct
                        {
                            Index        = DataScope,
                            Name         = "DataScope",
                            Title        = "权限范围",
                            Caption      = @"权限范围",
                            Description  = @"权限范围",
                            ColumnName   = "data_scope",
                            PropertyType = typeof(SubjectionType),
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