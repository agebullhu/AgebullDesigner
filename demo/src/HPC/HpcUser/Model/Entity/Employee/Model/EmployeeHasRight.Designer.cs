/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:36*/
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
    /// 员工特权
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class EmployeeHasRightData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public EmployeeHasRightData()
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
        public void ChangePrimaryKey(long eRID)
        {
            _eRID = eRID;
        }
        /// <summary>
        /// 厄立德
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _eRID;

        partial void OnERIDGet();

        partial void OnERIDSet(ref long value);

        partial void OnERIDLoad(ref long value);

        partial void OnERIDSeted();

        
        /// <summary>
        /// 厄立德
        /// </summary>
        [DataMember , JsonProperty("ERID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"厄立德")]
        public long ERID
        {
            get
            {
                OnERIDGet();
                return this._eRID;
            }
            set
            {
                if(this._eRID == value)
                    return;
                //if(this._eRID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnERIDSet(ref value);
                this._eRID = value;
                this.OnPropertyChanged(_DataStruct_.Real_ERID);
                OnERIDSeted();
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
        /// <summary>
        /// 右除法
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _rightRID;

        partial void OnRightRIDGet();

        partial void OnRightRIDSet(ref long value);

        partial void OnRightRIDSeted();

        
        /// <summary>
        /// 右除法
        /// </summary>
        [DataMember , JsonProperty("RightRID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"右除法")]
        public  long RightRID
        {
            get
            {
                OnRightRIDGet();
                return this._rightRID;
            }
            set
            {
                if(this._rightRID == value)
                    return;
                OnRightRIDSet(ref value);
                this._rightRID = value;
                OnRightRIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RightRID);
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
                return this.ERID;
            }
            set
            {
                this.ERID = value;
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
            case "erid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.ERID = vl;
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
            case "rightrid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.RightRID = vl;
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
            case "erid":
                this.ERID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "empeid":
                this.EmpEID = (long)Convert.ToDecimal(value);
                return;
            case "rightrid":
                this.RightRID = (long)Convert.ToDecimal(value);
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
            case _DataStruct_.ERID:
                this.ERID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.EmpEID:
                this.EmpEID = Convert.ToInt64(value);
                return;
            case _DataStruct_.RightRID:
                this.RightRID = Convert.ToInt64(value);
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
            case "erid":
                return this.ERID;
            case "sitesid":
                return this.SiteSID;
            case "empeid":
                return this.EmpEID;
            case "rightrid":
                return this.RightRID;
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
                case _DataStruct_.ERID:
                    return this.ERID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.EmpEID:
                    return this.EmpEID;
                case _DataStruct_.RightRID:
                    return this.RightRID;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(EmployeeHasRightData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as EmployeeHasRightData;
            if(sourceEntity == null)
                return;
            this._eRID = sourceEntity._eRID;
            this._siteSID = sourceEntity._siteSID;
            this._empEID = sourceEntity._empEID;
            this._rightRID = sourceEntity._rightRID;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(EmployeeHasRightData source)
        {
                this.ERID = source.ERID;
                this.SiteSID = source.SiteSID;
                this.EmpEID = source.EmpEID;
                this.RightRID = source.RightRID;
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
                OnERIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnEmpEIDModified(subsist,false);
                OnRightRIDModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnERIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnEmpEIDModified(subsist,true);
                OnRightRIDModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[4] > 0)
            {
                OnERIDModified(subsist,modifieds[_DataStruct_.Real_ERID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnEmpEIDModified(subsist,modifieds[_DataStruct_.Real_EmpEID] == 1);
                OnRightRIDModified(subsist,modifieds[_DataStruct_.Real_RightRID] == 1);
            }
        }

        /// <summary>
        /// 厄立德修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnERIDModified(EntitySubsist subsist,bool isModified);

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
        /// 电磁脉冲修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnEmpEIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 右除法修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRightRIDModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"EmployeeHasRight";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"员工特权";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"员工特权";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "ERID";
            
            
            /// <summary>
            /// 厄立德的数字标识
            /// </summary>
            public const byte ERID = 1;
            
            /// <summary>
            /// 厄立德的实时记录顺序
            /// </summary>
            public const int Real_ERID = 0;

            /// <summary>
            /// 站点标识的数字标识
            /// </summary>
            public const byte SiteSID = 2;
            
            /// <summary>
            /// 站点标识的实时记录顺序
            /// </summary>
            public const int Real_SiteSID = 1;

            /// <summary>
            /// 电磁脉冲的数字标识
            /// </summary>
            public const byte EmpEID = 3;
            
            /// <summary>
            /// 电磁脉冲的实时记录顺序
            /// </summary>
            public const int Real_EmpEID = 2;

            /// <summary>
            /// 右除法的数字标识
            /// </summary>
            public const byte RightRID = 4;
            
            /// <summary>
            /// 右除法的实时记录顺序
            /// </summary>
            public const int Real_RightRID = 3;

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
                        Real_ERID,
                        new PropertySturct
                        {
                            Index        = ERID,
                            Name         = "ERID",
                            Title        = "厄立德",
                            Caption      = @"厄立德",
                            Description  = @"厄立德",
                            ColumnName   = "ERID",
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
                    },
                    {
                        Real_RightRID,
                        new PropertySturct
                        {
                            Index        = RightRID,
                            Name         = "RightRID",
                            Title        = "右除法",
                            Caption      = @"右除法",
                            Description  = @"右除法",
                            ColumnName   = "RightRID",
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