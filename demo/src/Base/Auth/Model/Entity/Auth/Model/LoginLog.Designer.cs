/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/11/14 19:10:07*/
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
    /// 用户登录日志
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LoginLogData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public LoginLogData()
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
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _id;

        partial void OnIdGet();

        partial void OnIdSet(ref long value);

        partial void OnIdLoad(ref long value);

        partial void OnIdSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
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
        [DataRule(CanNull = true)]
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
        /// 登录账号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _loginName;

        partial void OnLoginNameGet();

        partial void OnLoginNameSet(ref string value);

        partial void OnLoginNameSeted();

        
        /// <summary>
        /// 登录账号
        /// </summary>
        /// <remarks>
        /// 视不同登录方式内容不同  用户名称密码登录是用户名，其它联合登录是对应的OpenId或UnionCode
        /// </remarks>
        /// <value>
        /// 提交时不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LoginName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"登录账号")]
        public  string LoginName
        {
            get
            {
                OnLoginNameGet();
                return this._loginName;
            }
            set
            {
                if(this._loginName == value)
                    return;
                OnLoginNameSet(ref value);
                this._loginName = value;
                OnLoginNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LoginName);
            }
        }
        /// <summary>
        /// 登录时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _addDate;

        partial void OnAddDateGet();

        partial void OnAddDateSet(ref DateTime value);

        partial void OnAddDateSeted();

        
        /// <summary>
        /// 登录时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("AddDate", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"登录时间")]
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
        /// 提交时不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
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
        /// 登录系统
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _app;

        partial void OnAppGet();

        partial void OnAppSet(ref string value);

        partial void OnAppSeted();

        
        /// <summary>
        /// 登录系统
        /// </summary>
        /// <remarks>
        /// 设备识别码所在的浏览器，App中不写
        /// </remarks>
        /// <value>
        /// 提交时不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("App", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"登录系统")]
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
        /// <summary>
        /// 登录操作系统
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _os;

        partial void OnOsGet();

        partial void OnOsSet(ref string value);

        partial void OnOsSeted();

        
        /// <summary>
        /// 登录操作系统
        /// </summary>
        /// <remarks>
        /// 注册来源操作系统
        /// </remarks>
        /// <value>
        /// 提交时不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Os", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"登录操作系统")]
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
        /// 登录渠道
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _channal;

        partial void OnChannalGet();

        partial void OnChannalSet(ref string value);

        partial void OnChannalSeted();

        
        /// <summary>
        /// 登录渠道
        /// </summary>
        /// <remarks>
        /// 注册来源渠道码
        /// </remarks>
        /// <value>
        /// 提交时不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Channal", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"登录渠道")]
        public  string Channal
        {
            get
            {
                OnChannalGet();
                return this._channal;
            }
            set
            {
                if(this._channal == value)
                    return;
                OnChannalSet(ref value);
                this._channal = value;
                OnChannalSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Channal);
            }
        }
        /// <summary>
        /// 登录方式
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public AuthorizeType _loginType;

        partial void OnLoginTypeGet();

        partial void OnLoginTypeSet(ref AuthorizeType value);

        partial void OnLoginTypeSeted();

        
        /// <summary>
        /// 登录方式
        /// </summary>
        /// <remarks>
        /// 来源
        /// </remarks>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("LoginType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"登录方式")]
        public  AuthorizeType LoginType
        {
            get
            {
                OnLoginTypeGet();
                return this._loginType;
            }
            set
            {
                if(this._loginType == value)
                    return;
                OnLoginTypeSet(ref value);
                this._loginType = value;
                OnLoginTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_LoginType);
            }
        }
        /// <summary>
        /// 登录方式的可读内容
        /// </summary>
        [IgnoreDataMember,JsonIgnore,DisplayName("登录方式")]
        public string LoginType_Content => LoginType.ToCaption();

        /// <summary>
        /// 登录方式的数字属性
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public  int LoginType_Number
        {
            get => (int)this.LoginType;
            set => this.LoginType = (AuthorizeType)value;
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public bool _success;

        partial void OnSuccessGet();

        partial void OnSuccessSet(ref bool value);

        partial void OnSuccessSeted();

        
        /// <summary>
        /// 是否成功
        /// </summary>
        /// <remarks>
        /// 是否登录成功
        /// </remarks>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Success", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"是否成功")]
        public  bool Success
        {
            get
            {
                OnSuccessGet();
                return this._success;
            }
            set
            {
                if(this._success == value)
                    return;
                OnSuccessSet(ref value);
                this._success = value;
                OnSuccessSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Success);
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
            case "userid":
                this.UserId = (long)Convert.ToDecimal(value);
                return;
            case "loginname":
                this.LoginName = value == null ? null : value.ToString();
                return;
            case "adddate":
                this.AddDate = Convert.ToDateTime(value);
                return;
            case "deviceid":
                this.DeviceId = value == null ? null : value.ToString();
                return;
            case "app":
                this.App = value == null ? null : value.ToString();
                return;
            case "os":
                this.Os = value == null ? null : value.ToString();
                return;
            case "channal":
                this.Channal = value == null ? null : value.ToString();
                return;
            case "logintype":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.LoginType = (AuthorizeType)(int)value;
                    }
                    else if(value is AuthorizeType)
                    {
                        this.LoginType = (AuthorizeType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        AuthorizeType val;
                        if (AuthorizeType.TryParse(str, out val))
                        {
                            this.LoginType = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.LoginType = (AuthorizeType)vl;
                            }
                        }
                    }
                }
                return;
            case "success":
                if (value != null)
                {
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {
                        this.Success = vl != 0;
                    }
                    else
                    {
                        this.Success = Convert.ToBoolean(value);
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
            case _DataStruct_.Id:
                this.Id = Convert.ToInt64(value);
                return;
            case _DataStruct_.UserId:
                this.UserId = Convert.ToInt64(value);
                return;
            case _DataStruct_.LoginName:
                this.LoginName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AddDate:
                this.AddDate = Convert.ToDateTime(value);
                return;
            case _DataStruct_.DeviceId:
                this.DeviceId = value == null ? null : value.ToString();
                return;
            case _DataStruct_.App:
                this.App = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Os:
                this.Os = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Channal:
                this.Channal = value == null ? null : value.ToString();
                return;
            case _DataStruct_.LoginType:
                this.LoginType = (AuthorizeType)value;
                return;
            case _DataStruct_.Success:
                this.Success = Convert.ToBoolean(value);
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
            case "userid":
                return this.UserId;
            case "loginname":
                return this.LoginName;
            case "adddate":
                return this.AddDate;
            case "deviceid":
                return this.DeviceId;
            case "app":
                return this.App;
            case "os":
                return this.Os;
            case "channal":
                return this.Channal;
            case "logintype":
                return this.LoginType.ToCaption();
            case "success":
                return this.Success;
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
                case _DataStruct_.UserId:
                    return this.UserId;
                case _DataStruct_.LoginName:
                    return this.LoginName;
                case _DataStruct_.AddDate:
                    return this.AddDate;
                case _DataStruct_.DeviceId:
                    return this.DeviceId;
                case _DataStruct_.App:
                    return this.App;
                case _DataStruct_.Os:
                    return this.Os;
                case _DataStruct_.Channal:
                    return this.Channal;
                case _DataStruct_.LoginType:
                    return this.LoginType;
                case _DataStruct_.Success:
                    return this.Success;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(LoginLogData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as LoginLogData;
            if(sourceEntity == null)
                return;
            this._id = sourceEntity._id;
            this._userId = sourceEntity._userId;
            this._loginName = sourceEntity._loginName;
            this._addDate = sourceEntity._addDate;
            this._deviceId = sourceEntity._deviceId;
            this._app = sourceEntity._app;
            this._os = sourceEntity._os;
            this._channal = sourceEntity._channal;
            this._loginType = sourceEntity._loginType;
            this._success = sourceEntity._success;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(LoginLogData source)
        {
                this.Id = source.Id;
                this.UserId = source.UserId;
                this.LoginName = source.LoginName;
                this.AddDate = source.AddDate;
                this.DeviceId = source.DeviceId;
                this.App = source.App;
                this.Os = source.Os;
                this.Channal = source.Channal;
                this.LoginType = source.LoginType;
                this.Success = source.Success;
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
                OnUserIdModified(subsist,false);
                OnLoginNameModified(subsist,false);
                OnAddDateModified(subsist,false);
                OnDeviceIdModified(subsist,false);
                OnAppModified(subsist,false);
                OnOsModified(subsist,false);
                OnChannalModified(subsist,false);
                OnLoginTypeModified(subsist,false);
                OnSuccessModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIdModified(subsist,true);
                OnUserIdModified(subsist,true);
                OnLoginNameModified(subsist,true);
                OnAddDateModified(subsist,true);
                OnDeviceIdModified(subsist,true);
                OnAppModified(subsist,true);
                OnOsModified(subsist,true);
                OnChannalModified(subsist,true);
                OnLoginTypeModified(subsist,true);
                OnSuccessModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[10] > 0)
            {
                OnIdModified(subsist,modifieds[_DataStruct_.Real_Id] == 1);
                OnUserIdModified(subsist,modifieds[_DataStruct_.Real_UserId] == 1);
                OnLoginNameModified(subsist,modifieds[_DataStruct_.Real_LoginName] == 1);
                OnAddDateModified(subsist,modifieds[_DataStruct_.Real_AddDate] == 1);
                OnDeviceIdModified(subsist,modifieds[_DataStruct_.Real_DeviceId] == 1);
                OnAppModified(subsist,modifieds[_DataStruct_.Real_App] == 1);
                OnOsModified(subsist,modifieds[_DataStruct_.Real_Os] == 1);
                OnChannalModified(subsist,modifieds[_DataStruct_.Real_Channal] == 1);
                OnLoginTypeModified(subsist,modifieds[_DataStruct_.Real_LoginType] == 1);
                OnSuccessModified(subsist,modifieds[_DataStruct_.Real_Success] == 1);
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
        partial void OnIdModified(EntitySubsist subsist,bool isModified);

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
        /// 登录账号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLoginNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 登录时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAddDateModified(EntitySubsist subsist,bool isModified);

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
        /// 登录系统修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 登录操作系统修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOsModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 登录渠道修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnChannalModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 登录方式修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLoginTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 是否成功修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSuccessModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"LoginLog";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户登录日志";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户登录日志";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0xC0004;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "Id";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte Id = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_Id = 0;

            /// <summary>
            /// 用户Id的数字标识
            /// </summary>
            public const byte UserId = 2;
            
            /// <summary>
            /// 用户Id的实时记录顺序
            /// </summary>
            public const int Real_UserId = 1;

            /// <summary>
            /// 登录账号的数字标识
            /// </summary>
            public const byte LoginName = 9;
            
            /// <summary>
            /// 登录账号的实时记录顺序
            /// </summary>
            public const int Real_LoginName = 2;

            /// <summary>
            /// 登录时间的数字标识
            /// </summary>
            public const byte AddDate = 11;
            
            /// <summary>
            /// 登录时间的实时记录顺序
            /// </summary>
            public const int Real_AddDate = 3;

            /// <summary>
            /// 设备识别码的数字标识
            /// </summary>
            public const byte DeviceId = 3;
            
            /// <summary>
            /// 设备识别码的实时记录顺序
            /// </summary>
            public const int Real_DeviceId = 4;

            /// <summary>
            /// 登录系统的数字标识
            /// </summary>
            public const byte App = 5;
            
            /// <summary>
            /// 登录系统的实时记录顺序
            /// </summary>
            public const int Real_App = 5;

            /// <summary>
            /// 登录操作系统的数字标识
            /// </summary>
            public const byte Os = 4;
            
            /// <summary>
            /// 登录操作系统的实时记录顺序
            /// </summary>
            public const int Real_Os = 6;

            /// <summary>
            /// 登录渠道的数字标识
            /// </summary>
            public const byte Channal = 6;
            
            /// <summary>
            /// 登录渠道的实时记录顺序
            /// </summary>
            public const int Real_Channal = 7;

            /// <summary>
            /// 登录方式的数字标识
            /// </summary>
            public const byte LoginType = 7;
            
            /// <summary>
            /// 登录方式的实时记录顺序
            /// </summary>
            public const int Real_LoginType = 8;

            /// <summary>
            /// 是否成功的数字标识
            /// </summary>
            public const byte Success = 10;
            
            /// <summary>
            /// 是否成功的实时记录顺序
            /// </summary>
            public const int Real_Success = 9;

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
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "id",
                            PropertyType = typeof(long),
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
                        Real_LoginName,
                        new PropertySturct
                        {
                            Index        = LoginName,
                            Name         = "LoginName",
                            Title        = "登录账号",
                            Caption      = @"登录账号",
                            Description  = @"视不同登录方式内容不同  用户名称密码登录是用户名，其它联合登录是对应的OpenId或UnionCode",
                            ColumnName   = "login_name",
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
                            Title        = "登录时间",
                            Caption      = @"登录时间",
                            Description  = @"登录时间",
                            ColumnName   = "add_date",
                            PropertyType = typeof(DateTime),
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
                        Real_App,
                        new PropertySturct
                        {
                            Index        = App,
                            Name         = "App",
                            Title        = "登录系统",
                            Caption      = @"登录系统",
                            Description  = @"设备识别码所在的浏览器，App中不写",
                            ColumnName   = "browser",
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
                            Title        = "登录操作系统",
                            Caption      = @"登录操作系统",
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
                        Real_Channal,
                        new PropertySturct
                        {
                            Index        = Channal,
                            Name         = "Channal",
                            Title        = "登录渠道",
                            Caption      = @"登录渠道",
                            Description  = @"注册来源渠道码",
                            ColumnName   = "channal",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_LoginType,
                        new PropertySturct
                        {
                            Index        = LoginType,
                            Name         = "LoginType",
                            Title        = "登录方式",
                            Caption      = @"登录方式",
                            Description  = @"来源",
                            ColumnName   = "login_type",
                            PropertyType = typeof(AuthorizeType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Success,
                        new PropertySturct
                        {
                            Index        = Success,
                            Name         = "Success",
                            Title        = "是否成功",
                            Caption      = @"是否成功",
                            Description  = @"是否登录成功",
                            ColumnName   = "success",
                            PropertyType = typeof(bool),
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