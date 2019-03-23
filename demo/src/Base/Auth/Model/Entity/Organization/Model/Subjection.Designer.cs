/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/11/14 19:10:09*/
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
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;



#endregion

namespace Agebull.Common.Organizations
{
    /// <summary>
    /// 行级权限关联
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class SubjectionData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public SubjectionData()
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
        public void ChangePrimaryKey(long id)
        {
            _id = id;
        }
        /// <summary>
        /// 标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _id;

        partial void OnIdGet();

        partial void OnIdSet(ref long value);

        partial void OnIdLoad(ref long value);

        partial void OnIdSeted();

        
        /// <summary>
        /// 标识
        /// </summary>
        [DataMember , JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"标识")]
        public long Id
        {
            get
            {
                OnIdGet();
                return this._id;
            }
            set
            {
                if(this._id == value)
                    return;
                //if(this._id > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnIdSet(ref value);
                this._id = value;
                this.OnPropertyChanged(_DataStruct_.Real_Id);
                OnIdSeted();
            }
        }
        /// <summary>
        /// 关联类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public SubjectionType _subjectionType;

        partial void OnSubjectionTypeGet();

        partial void OnSubjectionTypeSet(ref SubjectionType value);

        partial void OnSubjectionTypeSeted();

        
        /// <summary>
        /// 关联类型
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SubjectionType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"关联类型")]
        public  SubjectionType SubjectionType
        {
            get
            {
                OnSubjectionTypeGet();
                return this._subjectionType;
            }
            set
            {
                if(this._subjectionType == value)
                    return;
                OnSubjectionTypeSet(ref value);
                this._subjectionType = value;
                OnSubjectionTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SubjectionType);
            }
        }
        /// <summary>
        /// 关联类型的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("关联类型")]
        public string SubjectionType_Content => SubjectionType.ToCaption();

        /// <summary>
        /// 关联类型的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int SubjectionType_Number
        {
            get => (int)this.SubjectionType;
            set => this.SubjectionType = (SubjectionType)value;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _masterId;

        partial void OnMasterIdGet();

        partial void OnMasterIdSet(ref long value);

        partial void OnMasterIdSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MasterId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"主键")]
        public  long MasterId
        {
            get
            {
                OnMasterIdGet();
                return this._masterId;
            }
            set
            {
                if(this._masterId == value)
                    return;
                OnMasterIdSet(ref value);
                this._masterId = value;
                OnMasterIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MasterId);
            }
        }
        /// <summary>
        /// 关联
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _slaveId;

        partial void OnSlaveIdGet();

        partial void OnSlaveIdSet(ref long value);

        partial void OnSlaveIdSeted();

        
        /// <summary>
        /// 关联
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SlaveId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"关联")]
        public  long SlaveId
        {
            get
            {
                OnSlaveIdGet();
                return this._slaveId;
            }
            set
            {
                if(this._slaveId == value)
                    return;
                OnSlaveIdSet(ref value);
                this._slaveId = value;
                OnSlaveIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SlaveId);
            }
        }
        /// <summary>
        /// 关联场景
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _subjectionSreen;

        partial void OnSubjectionSreenGet();

        partial void OnSubjectionSreenSet(ref int value);

        partial void OnSubjectionSreenSeted();

        
        /// <summary>
        /// 关联场景
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SubjectionSreen", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"关联场景")]
        public  int SubjectionSreen
        {
            get
            {
                OnSubjectionSreenGet();
                return this._subjectionSreen;
            }
            set
            {
                if(this._subjectionSreen == value)
                    return;
                OnSubjectionSreenSet(ref value);
                this._subjectionSreen = value;
                OnSubjectionSreenSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SubjectionSreen);
            }
        }

        #region 接口属性

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
                this.Id = (long)Convert.ToDecimal(value);
                return;
            case "subjectiontype":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.SubjectionType = (SubjectionType)(int)value;
                    }
                    else if(value is SubjectionType)
                    {
                        this.SubjectionType = (SubjectionType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        SubjectionType val;
                        if (SubjectionType.TryParse(str, out val))
                        {
                            this.SubjectionType = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.SubjectionType = (SubjectionType)vl;
                            }
                        }
                    }
                }
                return;
            case "masterid":
                this.MasterId = (long)Convert.ToDecimal(value);
                return;
            case "slaveid":
                this.SlaveId = (long)Convert.ToDecimal(value);
                return;
            case "subjectionsreen":
                this.SubjectionSreen = (int)Convert.ToDecimal(value);
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
            case _DataStruct_.Id:
                this.Id = Convert.ToInt64(value);
                return;
            case _DataStruct_.SubjectionType:
                this.SubjectionType = (SubjectionType)value;
                return;
            case _DataStruct_.MasterId:
                this.MasterId = Convert.ToInt64(value);
                return;
            case _DataStruct_.SlaveId:
                this.SlaveId = Convert.ToInt64(value);
                return;
            case _DataStruct_.SubjectionSreen:
                this.SubjectionSreen = Convert.ToInt32(value);
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
                return this.Id;
            case "subjectiontype":
                return this.SubjectionType.ToCaption();
            case "masterid":
                return this.MasterId;
            case "slaveid":
                return this.SlaveId;
            case "subjectionsreen":
                return this.SubjectionSreen;
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
                case _DataStruct_.Id:
                    return this.Id;
                case _DataStruct_.SubjectionType:
                    return this.SubjectionType;
                case _DataStruct_.MasterId:
                    return this.MasterId;
                case _DataStruct_.SlaveId:
                    return this.SlaveId;
                case _DataStruct_.SubjectionSreen:
                    return this.SubjectionSreen;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(SubjectionData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as SubjectionData;
            if(sourceEntity == null)
                return;
            this._id = sourceEntity._id;
            this._subjectionType = sourceEntity._subjectionType;
            this._masterId = sourceEntity._masterId;
            this._slaveId = sourceEntity._slaveId;
            this._subjectionSreen = sourceEntity._subjectionSreen;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(SubjectionData source)
        {
                this.Id = source.Id;
                this.SubjectionType = source.SubjectionType;
                this.MasterId = source.MasterId;
                this.SlaveId = source.SlaveId;
                this.SubjectionSreen = source.SubjectionSreen;
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
                OnIdModified(subsist,false);
                OnSubjectionTypeModified(subsist,false);
                OnMasterIdModified(subsist,false);
                OnSlaveIdModified(subsist,false);
                OnSubjectionSreenModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIdModified(subsist,true);
                OnSubjectionTypeModified(subsist,true);
                OnMasterIdModified(subsist,true);
                OnSlaveIdModified(subsist,true);
                OnSubjectionSreenModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[5] > 0)
            {
                OnIdModified(subsist,modifieds[_DataStruct_.Real_Id] == 1);
                OnSubjectionTypeModified(subsist,modifieds[_DataStruct_.Real_SubjectionType] == 1);
                OnMasterIdModified(subsist,modifieds[_DataStruct_.Real_MasterId] == 1);
                OnSlaveIdModified(subsist,modifieds[_DataStruct_.Real_SlaveId] == 1);
                OnSubjectionSreenModified(subsist,modifieds[_DataStruct_.Real_SubjectionSreen] == 1);
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
        partial void OnIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 关联类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSubjectionTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 主键修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMasterIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 关联修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSlaveIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 关联场景修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSubjectionSreenModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"Subjection";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"行级权限关联";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"行级权限关联";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x20009;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "Id";
            
            
            /// <summary>
            /// 标识的数字标识
            /// </summary>
            public const byte Id = 1;
            
            /// <summary>
            /// 标识的实时记录顺序
            /// </summary>
            public const int Real_Id = 0;

            /// <summary>
            /// 关联类型的数字标识
            /// </summary>
            public const byte SubjectionType = 2;
            
            /// <summary>
            /// 关联类型的实时记录顺序
            /// </summary>
            public const int Real_SubjectionType = 1;

            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte MasterId = 3;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_MasterId = 2;

            /// <summary>
            /// 关联的数字标识
            /// </summary>
            public const byte SlaveId = 4;
            
            /// <summary>
            /// 关联的实时记录顺序
            /// </summary>
            public const int Real_SlaveId = 3;

            /// <summary>
            /// 关联场景的数字标识
            /// </summary>
            public const byte SubjectionSreen = 5;
            
            /// <summary>
            /// 关联场景的实时记录顺序
            /// </summary>
            public const int Real_SubjectionSreen = 4;

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
                        Real_Id,
                        new PropertySturct
                        {
                            Index        = Id,
                            Name         = "Id",
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
                        Real_SubjectionType,
                        new PropertySturct
                        {
                            Index        = SubjectionType,
                            Name         = "SubjectionType",
                            Title        = "关联类型",
                            Caption      = @"关联类型",
                            Description  = @"关联类型",
                            ColumnName   = "subjection_type",
                            PropertyType = typeof(SubjectionType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MasterId,
                        new PropertySturct
                        {
                            Index        = MasterId,
                            Name         = "MasterId",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "master_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SlaveId,
                        new PropertySturct
                        {
                            Index        = SlaveId,
                            Name         = "SlaveId",
                            Title        = "关联",
                            Caption      = @"关联",
                            Description  = @"关联",
                            ColumnName   = "slave_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SubjectionSreen,
                        new PropertySturct
                        {
                            Index        = SubjectionSreen,
                            Name         = "SubjectionSreen",
                            Title        = "关联场景",
                            Caption      = @"关联场景",
                            Description  = @"关联场景",
                            ColumnName   = "subjection_sreen",
                            PropertyType = typeof(int),
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