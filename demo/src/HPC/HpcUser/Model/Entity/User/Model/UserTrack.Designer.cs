/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:53*/
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
    /// 用户跟踪
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class UserTrackData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserTrackData()
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
        /// <summary>
        /// 定位时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _positionTime;

        partial void OnpositionTimeGet();

        partial void OnpositionTimeSet(ref DateTime value);

        partial void OnpositionTimeSeted();

        
        /// <summary>
        /// 定位时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("positionTime", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"定位时间")]
        public  DateTime positionTime
        {
            get
            {
                OnpositionTimeGet();
                return this._positionTime;
            }
            set
            {
                if(this._positionTime == value)
                    return;
                OnpositionTimeSet(ref value);
                this._positionTime = value;
                OnpositionTimeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_positionTime);
            }
        }
        /// <summary>
        /// 纬度位置
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public float _position_latitude;

        partial void Onposition_latitudeGet();

        partial void Onposition_latitudeSet(ref float value);

        partial void Onposition_latitudeSeted();

        
        /// <summary>
        /// 纬度位置
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("position_latitude", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"纬度位置")]
        public  float position_latitude
        {
            get
            {
                Onposition_latitudeGet();
                return this._position_latitude;
            }
            set
            {
                if(this._position_latitude == value)
                    return;
                Onposition_latitudeSet(ref value);
                this._position_latitude = value;
                Onposition_latitudeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_position_latitude);
            }
        }
        /// <summary>
        /// 位置经度
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public float _position_longitude;

        partial void Onposition_longitudeGet();

        partial void Onposition_longitudeSet(ref float value);

        partial void Onposition_longitudeSeted();

        
        /// <summary>
        /// 位置经度
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("position_longitude", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"位置经度")]
        public  float position_longitude
        {
            get
            {
                Onposition_longitudeGet();
                return this._position_longitude;
            }
            set
            {
                if(this._position_longitude == value)
                    return;
                Onposition_longitudeSet(ref value);
                this._position_longitude = value;
                Onposition_longitudeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_position_longitude);
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
            case "positiontime":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.positionTime = vl;
                        return true;
                    }
                }
                return false;
            case "position_latitude":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (float.TryParse(value, out var vl))
                    {
                        this.position_latitude = vl;
                        return true;
                    }
                }
                return false;
            case "position_longitude":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (float.TryParse(value, out var vl))
                    {
                        this.position_longitude = vl;
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
            case "tid":
                this.TID = (long)Convert.ToDecimal(value);
                return;
            case "useruid":
                this.UserUID = (long)Convert.ToDecimal(value);
                return;
            case "positiontime":
                this.positionTime = Convert.ToDateTime(value);
                return;
            case "position_latitude":
                this.position_latitude = Convert.ToSingle(value);
                return;
            case "position_longitude":
                this.position_longitude = Convert.ToSingle(value);
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
            case _DataStruct_.UserUID:
                this.UserUID = Convert.ToInt64(value);
                return;
            case _DataStruct_.positionTime:
                this.positionTime = Convert.ToDateTime(value);
                return;
            case _DataStruct_.position_latitude:
                this.position_latitude = Convert.ToSingle(value);
                return;
            case _DataStruct_.position_longitude:
                this.position_longitude = Convert.ToSingle(value);
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
            case "useruid":
                return this.UserUID;
            case "positiontime":
                return this.positionTime;
            case "position_latitude":
                return this.position_latitude;
            case "position_longitude":
                return this.position_longitude;
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
                case _DataStruct_.UserUID:
                    return this.UserUID;
                case _DataStruct_.positionTime:
                    return this.positionTime;
                case _DataStruct_.position_latitude:
                    return this.position_latitude;
                case _DataStruct_.position_longitude:
                    return this.position_longitude;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(UserTrackData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as UserTrackData;
            if(sourceEntity == null)
                return;
            this._tID = sourceEntity._tID;
            this._userUID = sourceEntity._userUID;
            this._positionTime = sourceEntity._positionTime;
            this._position_latitude = sourceEntity._position_latitude;
            this._position_longitude = sourceEntity._position_longitude;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(UserTrackData source)
        {
                this.TID = source.TID;
                this.UserUID = source.UserUID;
                this.positionTime = source.positionTime;
                this.position_latitude = source.position_latitude;
                this.position_longitude = source.position_longitude;
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
                OnUserUIDModified(subsist,false);
                OnpositionTimeModified(subsist,false);
                Onposition_latitudeModified(subsist,false);
                Onposition_longitudeModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnTIDModified(subsist,true);
                OnUserUIDModified(subsist,true);
                OnpositionTimeModified(subsist,true);
                Onposition_latitudeModified(subsist,true);
                Onposition_longitudeModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[5] > 0)
            {
                OnTIDModified(subsist,modifieds[_DataStruct_.Real_TID] == 1);
                OnUserUIDModified(subsist,modifieds[_DataStruct_.Real_UserUID] == 1);
                OnpositionTimeModified(subsist,modifieds[_DataStruct_.Real_positionTime] == 1);
                Onposition_latitudeModified(subsist,modifieds[_DataStruct_.Real_position_latitude] == 1);
                Onposition_longitudeModified(subsist,modifieds[_DataStruct_.Real_position_longitude] == 1);
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
        /// 组织标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserUIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 定位时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnpositionTimeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 纬度位置修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void Onposition_latitudeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 位置经度修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void Onposition_longitudeModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"UserTrack";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户跟踪";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户跟踪";
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
            /// 组织标识的数字标识
            /// </summary>
            public const byte UserUID = 2;
            
            /// <summary>
            /// 组织标识的实时记录顺序
            /// </summary>
            public const int Real_UserUID = 1;

            /// <summary>
            /// 定位时间的数字标识
            /// </summary>
            public const byte positionTime = 3;
            
            /// <summary>
            /// 定位时间的实时记录顺序
            /// </summary>
            public const int Real_positionTime = 2;

            /// <summary>
            /// 纬度位置的数字标识
            /// </summary>
            public const byte position_latitude = 4;
            
            /// <summary>
            /// 纬度位置的实时记录顺序
            /// </summary>
            public const int Real_position_latitude = 3;

            /// <summary>
            /// 位置经度的数字标识
            /// </summary>
            public const byte position_longitude = 5;
            
            /// <summary>
            /// 位置经度的实时记录顺序
            /// </summary>
            public const int Real_position_longitude = 4;

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
                    },
                    {
                        Real_positionTime,
                        new PropertySturct
                        {
                            Index        = positionTime,
                            Name         = "positionTime",
                            Title        = "定位时间",
                            Caption      = @"定位时间",
                            Description  = @"定位时间",
                            ColumnName   = "positionTime",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_position_latitude,
                        new PropertySturct
                        {
                            Index        = position_latitude,
                            Name         = "position_latitude",
                            Title        = "纬度位置",
                            Caption      = @"纬度位置",
                            Description  = @"纬度位置",
                            ColumnName   = "position_latitude",
                            PropertyType = typeof(float),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_position_longitude,
                        new PropertySturct
                        {
                            Index        = position_longitude,
                            Name         = "position_longitude",
                            Title        = "位置经度",
                            Caption      = @"位置经度",
                            Description  = @"位置经度",
                            ColumnName   = "position_longitude",
                            PropertyType = typeof(float),
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