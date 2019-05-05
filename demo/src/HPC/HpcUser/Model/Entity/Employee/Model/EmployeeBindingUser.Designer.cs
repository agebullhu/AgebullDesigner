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
    /// 员工绑定用户
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class EmployeeBindingUserData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public EmployeeBindingUserData()
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
        public void ChangePrimaryKey(long eUID)
        {
            _eUID = eUID;
        }
        /// <summary>
        /// 尤伊德
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _eUID;

        partial void OnEUIDGet();

        partial void OnEUIDSet(ref long value);

        partial void OnEUIDLoad(ref long value);

        partial void OnEUIDSeted();

        
        /// <summary>
        /// 尤伊德
        /// </summary>
        [DataMember , JsonProperty("EUID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"尤伊德")]
        public long EUID
        {
            get
            {
                OnEUIDGet();
                return this._eUID;
            }
            set
            {
                if(this._eUID == value)
                    return;
                //if(this._eUID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnEUIDSet(ref value);
                this._eUID = value;
                this.OnPropertyChanged(_DataStruct_.Real_EUID);
                OnEUIDSeted();
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
        /// <summary>
        /// 组织标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _userUID;

        partial void OnUserUIDGet();

        partial void OnUserUIDSet(ref long value);

        partial void OnUserUIDSeted();

        
        /// <summary>
        /// 组织标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("UserUID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织标识")]
        public  long UserUID
        {
            get
            {
                OnUserUIDGet();
                return this._userUID;
            }
            set
            {
                if(this._userUID == value)
                    return;
                OnUserUIDSet(ref value);
                this._userUID = value;
                OnUserUIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UserUID);
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
                return this.EUID;
            }
            set
            {
                this.EUID = value;
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
            case "euid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.EUID = vl;
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
            case "useruid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.UserUID = vl;
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
            case "euid":
                this.EUID = (long)Convert.ToDecimal(value);
                return;
            case "empeid":
                this.EmpEID = (long)Convert.ToDecimal(value);
                return;
            case "useruid":
                this.UserUID = (long)Convert.ToDecimal(value);
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
            case _DataStruct_.EUID:
                this.EUID = Convert.ToInt64(value);
                return;
            case _DataStruct_.EmpEID:
                this.EmpEID = Convert.ToInt64(value);
                return;
            case _DataStruct_.UserUID:
                this.UserUID = Convert.ToInt64(value);
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
            case "euid":
                return this.EUID;
            case "empeid":
                return this.EmpEID;
            case "useruid":
                return this.UserUID;
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
                case _DataStruct_.EUID:
                    return this.EUID;
                case _DataStruct_.EmpEID:
                    return this.EmpEID;
                case _DataStruct_.UserUID:
                    return this.UserUID;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(EmployeeBindingUserData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as EmployeeBindingUserData;
            if(sourceEntity == null)
                return;
            this._eUID = sourceEntity._eUID;
            this._empEID = sourceEntity._empEID;
            this._userUID = sourceEntity._userUID;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(EmployeeBindingUserData source)
        {
                this.EUID = source.EUID;
                this.EmpEID = source.EmpEID;
                this.UserUID = source.UserUID;
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
                OnEUIDModified(subsist,false);
                OnEmpEIDModified(subsist,false);
                OnUserUIDModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnEUIDModified(subsist,true);
                OnEmpEIDModified(subsist,true);
                OnUserUIDModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[3] > 0)
            {
                OnEUIDModified(subsist,modifieds[_DataStruct_.Real_EUID] == 1);
                OnEmpEIDModified(subsist,modifieds[_DataStruct_.Real_EmpEID] == 1);
                OnUserUIDModified(subsist,modifieds[_DataStruct_.Real_UserUID] == 1);
            }
        }

        /// <summary>
        /// 尤伊德修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnEUIDModified(EntitySubsist subsist,bool isModified);

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
        /// 组织标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserUIDModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"EmployeeBindingUser";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"员工绑定用户";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"员工绑定用户";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "EUID";
            
            
            /// <summary>
            /// 尤伊德的数字标识
            /// </summary>
            public const byte EUID = 1;
            
            /// <summary>
            /// 尤伊德的实时记录顺序
            /// </summary>
            public const int Real_EUID = 0;

            /// <summary>
            /// 电磁脉冲的数字标识
            /// </summary>
            public const byte EmpEID = 2;
            
            /// <summary>
            /// 电磁脉冲的实时记录顺序
            /// </summary>
            public const int Real_EmpEID = 1;

            /// <summary>
            /// 组织标识的数字标识
            /// </summary>
            public const byte UserUID = 3;
            
            /// <summary>
            /// 组织标识的实时记录顺序
            /// </summary>
            public const int Real_UserUID = 2;

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
                        Real_EUID,
                        new PropertySturct
                        {
                            Index        = EUID,
                            Name         = "EUID",
                            Title        = "尤伊德",
                            Caption      = @"尤伊德",
                            Description  = @"尤伊德",
                            ColumnName   = "EUID",
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
                        Real_UserUID,
                        new PropertySturct
                        {
                            Index        = UserUID,
                            Name         = "UserUID",
                            Title        = "组织标识",
                            Caption      = @"组织标识",
                            Description  = @"组织标识",
                            ColumnName   = "UserUID",
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