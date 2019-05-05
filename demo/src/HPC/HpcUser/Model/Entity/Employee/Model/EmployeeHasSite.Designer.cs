/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 16:28:58*/
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
    /// 员工站点关联
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class EmployeeHasSiteData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public EmployeeHasSiteData()
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
        /// 员工标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _empEID;

        partial void OnEmpEIDGet();

        partial void OnEmpEIDSet(ref long value);

        partial void OnEmpEIDSeted();

        
        /// <summary>
        /// 员工标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("EmpEID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"员工标识")]
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
        /// 备注
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _remarks;

        partial void OnRemarksGet();

        partial void OnRemarksSet(ref string value);

        partial void OnRemarksSeted();

        
        /// <summary>
        /// 备注
        /// </summary>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Remarks", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"备注")]
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
            case "oid":
                this.OID = (long)Convert.ToDecimal(value);
                return;
            case "empeid":
                this.EmpEID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
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
            case _DataStruct_.OID:
                this.OID = Convert.ToInt64(value);
                return;
            case _DataStruct_.EmpEID:
                this.EmpEID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
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
            case "oid":
                return this.OID;
            case "empeid":
                return this.EmpEID;
            case "sitesid":
                return this.SiteSID;
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
                case _DataStruct_.OID:
                    return this.OID;
                case _DataStruct_.EmpEID:
                    return this.EmpEID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.Remarks:
                    return this.Remarks;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(EmployeeHasSiteData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as EmployeeHasSiteData;
            if(sourceEntity == null)
                return;
            this._oID = sourceEntity._oID;
            this._empEID = sourceEntity._empEID;
            this._siteSID = sourceEntity._siteSID;
            this._remarks = sourceEntity._remarks;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(EmployeeHasSiteData source)
        {
                this.OID = source.OID;
                this.EmpEID = source.EmpEID;
                this.SiteSID = source.SiteSID;
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
                OnOIDModified(subsist,false);
                OnEmpEIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnRemarksModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnOIDModified(subsist,true);
                OnEmpEIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnRemarksModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[4] > 0)
            {
                OnOIDModified(subsist,modifieds[_DataStruct_.Real_OID] == 1);
                OnEmpEIDModified(subsist,modifieds[_DataStruct_.Real_EmpEID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
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
        partial void OnOIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 员工标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnEmpEIDModified(EntitySubsist subsist,bool isModified);

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
        /// 备注修改的后期处理(保存前)
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
            public const string EntityName = @"EmployeeHasSite";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"员工站点关联";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"员工站点关联";
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
            /// 员工标识的数字标识
            /// </summary>
            public const byte EmpEID = 2;
            
            /// <summary>
            /// 员工标识的实时记录顺序
            /// </summary>
            public const int Real_EmpEID = 1;

            /// <summary>
            /// 站点标识的数字标识
            /// </summary>
            public const byte SiteSID = 3;
            
            /// <summary>
            /// 站点标识的实时记录顺序
            /// </summary>
            public const int Real_SiteSID = 2;

            /// <summary>
            /// 备注的数字标识
            /// </summary>
            public const byte Remarks = 4;
            
            /// <summary>
            /// 备注的实时记录顺序
            /// </summary>
            public const int Real_Remarks = 3;

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
                        Real_EmpEID,
                        new PropertySturct
                        {
                            Index        = EmpEID,
                            Name         = "EmpEID",
                            Title        = "员工标识",
                            Caption      = @"员工标识",
                            Description  = @"员工标识",
                            ColumnName   = "EmpEID",
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
                        Real_Remarks,
                        new PropertySturct
                        {
                            Index        = Remarks,
                            Name         = "Remarks",
                            Title        = "备注",
                            Caption      = @"备注",
                            Description  = @"备注",
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