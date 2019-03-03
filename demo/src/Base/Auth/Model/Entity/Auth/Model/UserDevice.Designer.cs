/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/11/14 19:10:10*/
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


#endregion

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 用户设备信息
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class UserDeviceData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserDeviceData()
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
        /// 设备的详细信息
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _deviceInfo;

        partial void OnDeviceInfoGet();

        partial void OnDeviceInfoSet(ref string value);

        partial void OnDeviceInfoSeted();

        
        /// <summary>
        /// 设备的详细信息
        /// </summary>
        /// <value>
        /// 不能为空.
        /// </value>
        [DataMember , JsonProperty("DeviceInfo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"设备的详细信息")]
        public  string DeviceInfo
        {
            get
            {
                OnDeviceInfoGet();
                return this._deviceInfo;
            }
            set
            {
                if(this._deviceInfo == value)
                    return;
                OnDeviceInfoSet(ref value);
                this._deviceInfo = value;
                OnDeviceInfoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_DeviceInfo);
            }
        }
        /// <summary>
        /// 新增日期
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _addDate;

        partial void OnAddDateGet();

        partial void OnAddDateSet(ref DateTime value);

        partial void OnAddDateSeted();

        
        /// <summary>
        /// 新增日期
        /// </summary>
        [DataMember , JsonProperty("AddDate", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"新增日期")]
        public  DateTime AddDate
        {
            get
            {
                OnAddDateGet();
                return this._addDate;
            }
            set
            {
                if(this._addDate == value)
                    return;
                OnAddDateSet(ref value);
                this._addDate = value;
                OnAddDateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AddDate);
            }
        }
        /// <summary>
        /// 用户Id
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _userId;

        partial void OnUserIdGet();

        partial void OnUserIdSet(ref long value);

        partial void OnUserIdSeted();

        
        /// <summary>
        /// 用户Id
        /// </summary>
        [DataMember , JsonProperty("UserId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"用户Id")]
        public  long UserId
        {
            get
            {
                OnUserIdGet();
                return this._userId;
            }
            set
            {
                if(this._userId == value)
                    return;
                OnUserIdSet(ref value);
                this._userId = value;
                OnUserIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UserId);
            }
        }
        /// <summary>
        /// 设备识别码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _deviceId;

        partial void OnDeviceIdGet();

        partial void OnDeviceIdSet(ref string value);

        partial void OnDeviceIdSeted();

        
        /// <summary>
        /// 设备识别码
        /// </summary>
        /// <remarks>
        /// App 客户端生成的deviceId
        /// </remarks>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataMember , JsonProperty("DeviceId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"设备识别码")]
        public  string DeviceId
        {
            get
            {
                OnDeviceIdGet();
                return this._deviceId;
            }
            set
            {
                if(this._deviceId == value)
                    return;
                OnDeviceIdSet(ref value);
                this._deviceId = value;
                OnDeviceIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_DeviceId);
            }
        }
        /// <summary>
        /// 设备操作系统
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _os;

        partial void OnOsGet();

        partial void OnOsSet(ref string value);

        partial void OnOsSeted();

        
        /// <summary>
        /// 设备操作系统
        /// </summary>
        /// <remarks>
        /// 注册来源操作系统
        /// </remarks>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("Os", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"设备操作系统")]
        public  string Os
        {
            get
            {
                OnOsGet();
                return this._os;
            }
            set
            {
                if(this._os == value)
                    return;
                OnOsSet(ref value);
                this._os = value;
                OnOsSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Os);
            }
        }
        /// <summary>
        /// 登录的应用
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _app;

        partial void OnAppGet();

        partial void OnAppSet(ref string value);

        partial void OnAppSeted();

        
        /// <summary>
        /// 登录的应用
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("App", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"登录的应用")]
        public  string App
        {
            get
            {
                OnAppGet();
                return this._app;
            }
            set
            {
                if(this._app == value)
                    return;
                OnAppSet(ref value);
                this._app = value;
                OnAppSeted();
                this.OnPropertyChanged(_DataStruct_.Real_App);
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
            case "deviceinfo":
                this.DeviceInfo = value == null ? null : value.ToString();
                return;
            case "adddate":
                this.AddDate = Convert.ToDateTime(value);
                return;
            case "userid":
                this.UserId = (long)Convert.ToDecimal(value);
                return;
            case "deviceid":
                this.DeviceId = value == null ? null : value.ToString();
                return;
            case "os":
                this.Os = value == null ? null : value.ToString();
                return;
            case "app":
                this.App = value == null ? null : value.ToString();
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
            case _DataStruct_.DeviceInfo:
                this.DeviceInfo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AddDate:
                this.AddDate = Convert.ToDateTime(value);
                return;
            case _DataStruct_.UserId:
                this.UserId = Convert.ToInt64(value);
                return;
            case _DataStruct_.DeviceId:
                this.DeviceId = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Os:
                this.Os = value == null ? null : value.ToString();
                return;
            case _DataStruct_.App:
                this.App = value == null ? null : value.ToString();
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
            case "deviceinfo":
                return this.DeviceInfo;
            case "adddate":
                return this.AddDate;
            case "userid":
                return this.UserId;
            case "deviceid":
                return this.DeviceId;
            case "os":
                return this.Os;
            case "app":
                return this.App;
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
                case _DataStruct_.DeviceInfo:
                    return this.DeviceInfo;
                case _DataStruct_.AddDate:
                    return this.AddDate;
                case _DataStruct_.UserId:
                    return this.UserId;
                case _DataStruct_.DeviceId:
                    return this.DeviceId;
                case _DataStruct_.Os:
                    return this.Os;
                case _DataStruct_.App:
                    return this.App;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(UserDeviceData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as UserDeviceData;
            if(sourceEntity == null)
                return;
            this._id = sourceEntity._id;
            this._deviceInfo = sourceEntity._deviceInfo;
            this._addDate = sourceEntity._addDate;
            this._userId = sourceEntity._userId;
            this._deviceId = sourceEntity._deviceId;
            this._os = sourceEntity._os;
            this._app = sourceEntity._app;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(UserDeviceData source)
        {
                this.Id = source.Id;
                this.DeviceInfo = source.DeviceInfo;
                this.AddDate = source.AddDate;
                this.UserId = source.UserId;
                this.DeviceId = source.DeviceId;
                this.Os = source.Os;
                this.App = source.App;
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
                OnDeviceInfoModified(subsist,false);
                OnAddDateModified(subsist,false);
                OnUserIdModified(subsist,false);
                OnDeviceIdModified(subsist,false);
                OnOsModified(subsist,false);
                OnAppModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIdModified(subsist,true);
                OnDeviceInfoModified(subsist,true);
                OnAddDateModified(subsist,true);
                OnUserIdModified(subsist,true);
                OnDeviceIdModified(subsist,true);
                OnOsModified(subsist,true);
                OnAppModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[7] > 0)
            {
                OnIdModified(subsist,modifieds[_DataStruct_.Real_Id] == 1);
                OnDeviceInfoModified(subsist,modifieds[_DataStruct_.Real_DeviceInfo] == 1);
                OnAddDateModified(subsist,modifieds[_DataStruct_.Real_AddDate] == 1);
                OnUserIdModified(subsist,modifieds[_DataStruct_.Real_UserId] == 1);
                OnDeviceIdModified(subsist,modifieds[_DataStruct_.Real_DeviceId] == 1);
                OnOsModified(subsist,modifieds[_DataStruct_.Real_Os] == 1);
                OnAppModified(subsist,modifieds[_DataStruct_.Real_App] == 1);
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
        /// 设备的详细信息修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDeviceInfoModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 新增日期修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAddDateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 用户Id修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 设备识别码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDeviceIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 设备操作系统修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOsModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 登录的应用修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"UserDevice";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户设备信息";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户设备信息";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0xC0006;
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
            /// 设备的详细信息的数字标识
            /// </summary>
            public const byte DeviceInfo = 7;
            
            /// <summary>
            /// 设备的详细信息的实时记录顺序
            /// </summary>
            public const int Real_DeviceInfo = 1;

            /// <summary>
            /// 新增日期的数字标识
            /// </summary>
            public const byte AddDate = 6;
            
            /// <summary>
            /// 新增日期的实时记录顺序
            /// </summary>
            public const int Real_AddDate = 2;

            /// <summary>
            /// 用户Id的数字标识
            /// </summary>
            public const byte UserId = 2;
            
            /// <summary>
            /// 用户Id的实时记录顺序
            /// </summary>
            public const int Real_UserId = 3;

            /// <summary>
            /// 设备识别码的数字标识
            /// </summary>
            public const byte DeviceId = 3;
            
            /// <summary>
            /// 设备识别码的实时记录顺序
            /// </summary>
            public const int Real_DeviceId = 4;

            /// <summary>
            /// 设备操作系统的数字标识
            /// </summary>
            public const byte Os = 4;
            
            /// <summary>
            /// 设备操作系统的实时记录顺序
            /// </summary>
            public const int Real_Os = 5;

            /// <summary>
            /// 登录的应用的数字标识
            /// </summary>
            public const byte App = 5;
            
            /// <summary>
            /// 登录的应用的实时记录顺序
            /// </summary>
            public const int Real_App = 6;

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
                        Real_DeviceInfo,
                        new PropertySturct
                        {
                            Index        = DeviceInfo,
                            Name         = "DeviceInfo",
                            Title        = "设备的详细信息",
                            Caption      = @"设备的详细信息",
                            Description  = @"设备的详细信息",
                            ColumnName   = "device_info",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AddDate,
                        new PropertySturct
                        {
                            Index        = AddDate,
                            Name         = "AddDate",
                            Title        = "新增日期",
                            Caption      = @"新增日期",
                            Description  = @"新增日期",
                            ColumnName   = "add_date",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_UserId,
                        new PropertySturct
                        {
                            Index        = UserId,
                            Name         = "UserId",
                            Title        = "用户Id",
                            Caption      = @"用户Id",
                            Description  = @"用户Id",
                            ColumnName   = "user_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_DeviceId,
                        new PropertySturct
                        {
                            Index        = DeviceId,
                            Name         = "DeviceId",
                            Title        = "设备识别码",
                            Caption      = @"设备识别码",
                            Description  = @"App 客户端生成的deviceId",
                            ColumnName   = "device_id",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Os,
                        new PropertySturct
                        {
                            Index        = Os,
                            Name         = "Os",
                            Title        = "设备操作系统",
                            Caption      = @"设备操作系统",
                            Description  = @"注册来源操作系统",
                            ColumnName   = "os",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_App,
                        new PropertySturct
                        {
                            Index        = App,
                            Name         = "App",
                            Title        = "登录的应用",
                            Caption      = @"登录的应用",
                            Description  = @"登录的应用",
                            ColumnName   = "app",
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