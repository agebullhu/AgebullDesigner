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
    /// 组织类型
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class OrganizationTypeData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public OrganizationTypeData()
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
        public void ChangePrimaryKey(long tID)
        {
            _tID = tID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _tID;

        partial void OnTIDGet();

        partial void OnTIDSet(ref long value);

        partial void OnTIDLoad(ref long value);

        partial void OnTIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("TID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long TID
        {
            get
            {
                OnTIDGet();
                return this._tID;
            }
            set
            {
                if(this._tID == value)
                    return;
                //if(this._tID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnTIDSet(ref value);
                this._tID = value;
                this.OnPropertyChanged(_DataStruct_.Real_TID);
                OnTIDSeted();
            }
        }
        /// <summary>
        /// 类型Name
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _typeName;

        partial void OnTypeNameGet();

        partial void OnTypeNameSet(ref string value);

        partial void OnTypeNameSeted();

        
        /// <summary>
        /// 类型Name
        /// </summary>
        /// <value>
        /// 可存储40个字符.合理长度应不大于40.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("TypeName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"类型Name")]
        public  string TypeName
        {
            get
            {
                OnTypeNameGet();
                return this._typeName;
            }
            set
            {
                if(this._typeName == value)
                    return;
                OnTypeNameSet(ref value);
                this._typeName = value;
                OnTypeNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_TypeName);
            }
        }
        /// <summary>
        /// 评论
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _remarks;

        partial void OnRemarksGet();

        partial void OnRemarksSet(ref string value);

        partial void OnRemarksSeted();

        
        /// <summary>
        /// 评论
        /// </summary>
        /// <value>
        /// 可存储400个字符.合理长度应不大于400.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Remarks", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"评论")]
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
                return this.TID;
            }
            set
            {
                this.TID = value;
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
            case "tid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.TID = vl;
                        return true;
                    }
                }
                return false;
            case "typename":
                this.TypeName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
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
            case "tid":
                this.TID = (long)Convert.ToDecimal(value);
                return;
            case "typename":
                this.TypeName = value == null ? null : value.ToString();
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
            case _DataStruct_.TID:
                this.TID = Convert.ToInt64(value);
                return;
            case _DataStruct_.TypeName:
                this.TypeName = value == null ? null : value.ToString();
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
            case "tid":
                return this.TID;
            case "typename":
                return this.TypeName;
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
                case _DataStruct_.TID:
                    return this.TID;
                case _DataStruct_.TypeName:
                    return this.TypeName;
                case _DataStruct_.Remarks:
                    return this.Remarks;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(OrganizationTypeData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as OrganizationTypeData;
            if(sourceEntity == null)
                return;
            this._tID = sourceEntity._tID;
            this._typeName = sourceEntity._typeName;
            this._remarks = sourceEntity._remarks;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(OrganizationTypeData source)
        {
                this.TID = source.TID;
                this.TypeName = source.TypeName;
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
                OnTIDModified(subsist,false);
                OnTypeNameModified(subsist,false);
                OnRemarksModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnTIDModified(subsist,true);
                OnTypeNameModified(subsist,true);
                OnRemarksModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[3] > 0)
            {
                OnTIDModified(subsist,modifieds[_DataStruct_.Real_TID] == 1);
                OnTypeNameModified(subsist,modifieds[_DataStruct_.Real_TypeName] == 1);
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
        partial void OnTIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 类型Name修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTypeNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 评论修改的后期处理(保存前)
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
            public const string EntityName = @"OrganizationType";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"组织类型";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"组织类型";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "TID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte TID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_TID = 0;

            /// <summary>
            /// 类型Name的数字标识
            /// </summary>
            public const byte TypeName = 2;
            
            /// <summary>
            /// 类型Name的实时记录顺序
            /// </summary>
            public const int Real_TypeName = 1;

            /// <summary>
            /// 评论的数字标识
            /// </summary>
            public const byte Remarks = 3;
            
            /// <summary>
            /// 评论的实时记录顺序
            /// </summary>
            public const int Real_Remarks = 2;

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
                        Real_TID,
                        new PropertySturct
                        {
                            Index        = TID,
                            Name         = "TID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "TID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_TypeName,
                        new PropertySturct
                        {
                            Index        = TypeName,
                            Name         = "TypeName",
                            Title        = "类型Name",
                            Caption      = @"类型Name",
                            Description  = @"类型Name",
                            ColumnName   = "TypeName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
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
                            Title        = "评论",
                            Caption      = @"评论",
                            Description  = @"评论",
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