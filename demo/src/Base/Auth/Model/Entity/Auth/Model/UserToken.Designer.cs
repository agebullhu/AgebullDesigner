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
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;



#endregion

namespace Agebull.Common.OAuth
{
    /// <summary>
    /// 用户访问令牌
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class UserTokenData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserTokenData()
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
        /// 设备标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _userDeviceId;

        partial void OnUserDeviceIdGet();

        partial void OnUserDeviceIdSet(ref long value);

        partial void OnUserDeviceIdSeted();

        
        /// <summary>
        /// 设备标识
        /// </summary>
        [DataMember , JsonProperty("UserDeviceId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"设备标识")]
        public  long UserDeviceId
        {
            get
            {
                OnUserDeviceIdGet();
                return this._userDeviceId;
            }
            set
            {
                if(this._userDeviceId == value)
                    return;
                OnUserDeviceIdSet(ref value);
                this._userDeviceId = value;
                OnUserDeviceIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UserDeviceId);
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
        /// 用户标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _userId;

        partial void OnUserIdGet();

        partial void OnUserIdSet(ref long value);

        partial void OnUserIdSeted();

        
        /// <summary>
        /// 用户标识
        /// </summary>
        [DataMember , JsonProperty("UserId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"用户标识")]
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
        /// 设备标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _deviceId;

        partial void OnDeviceIdGet();

        partial void OnDeviceIdSet(ref string value);

        partial void OnDeviceIdSeted();

        
        /// <summary>
        /// 设备标识
        /// </summary>
        /// <remarks>
        /// App 端用户生成的DeviceId
        /// </remarks>
        /// <value>
        /// 不能为空.可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataMember , JsonProperty("DeviceId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"设备标识")]
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
        /// 访问令牌
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _accessToken;

        partial void OnAccessTokenGet();

        partial void OnAccessTokenSet(ref string value);

        partial void OnAccessTokenSeted();

        
        /// <summary>
        /// 访问令牌
        /// </summary>
        /// <remarks>
        /// 用户accesstoken
        /// </remarks>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("AccessToken", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"访问令牌")]
        public  string AccessToken
        {
            get
            {
                OnAccessTokenGet();
                return this._accessToken;
            }
            set
            {
                if(this._accessToken == value)
                    return;
                OnAccessTokenSet(ref value);
                this._accessToken = value;
                OnAccessTokenSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AccessToken);
            }
        }
        /// <summary>
        /// 刷新令牌
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _refreshToken;

        partial void OnRefreshTokenGet();

        partial void OnRefreshTokenSet(ref string value);

        partial void OnRefreshTokenSeted();

        
        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <remarks>
        /// 用户的Refresh token，用于刷新access token
        /// </remarks>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("RefreshToken", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"刷新令牌")]
        public  string RefreshToken
        {
            get
            {
                OnRefreshTokenGet();
                return this._refreshToken;
            }
            set
            {
                if(this._refreshToken == value)
                    return;
                OnRefreshTokenSet(ref value);
                this._refreshToken = value;
                OnRefreshTokenSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RefreshToken);
            }
        }
        /// <summary>
        /// 访问令牌过期时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _accessTokenExpiresTime;

        partial void OnAccessTokenExpiresTimeGet();

        partial void OnAccessTokenExpiresTimeSet(ref DateTime value);

        partial void OnAccessTokenExpiresTimeSeted();

        
        /// <summary>
        /// 访问令牌过期时间
        /// </summary>
        /// <remarks>
        /// access token过期时间
        /// </remarks>
        [DataMember , JsonProperty("AccessTokenExpiresTime", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"访问令牌过期时间")]
        public  DateTime AccessTokenExpiresTime
        {
            get
            {
                OnAccessTokenExpiresTimeGet();
                return this._accessTokenExpiresTime;
            }
            set
            {
                if(this._accessTokenExpiresTime == value)
                    return;
                OnAccessTokenExpiresTimeSet(ref value);
                this._accessTokenExpiresTime = value;
                OnAccessTokenExpiresTimeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AccessTokenExpiresTime);
            }
        }
        /// <summary>
        /// 刷新令牌过期时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _refreshTokenExpiresTime;

        partial void OnRefreshTokenExpiresTimeGet();

        partial void OnRefreshTokenExpiresTimeSet(ref DateTime value);

        partial void OnRefreshTokenExpiresTimeSeted();

        
        /// <summary>
        /// 刷新令牌过期时间
        /// </summary>
        /// <remarks>
        /// refresh token 过期时间
        /// </remarks>
        [DataMember , JsonProperty("RefreshTokenExpiresTime", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"刷新令牌过期时间")]
        public  DateTime RefreshTokenExpiresTime
        {
            get
            {
                OnRefreshTokenExpiresTimeGet();
                return this._refreshTokenExpiresTime;
            }
            set
            {
                if(this._refreshTokenExpiresTime == value)
                    return;
                OnRefreshTokenExpiresTimeSet(ref value);
                this._refreshTokenExpiresTime = value;
                OnRefreshTokenExpiresTimeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_RefreshTokenExpiresTime);
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
            case "userdeviceid":
                this.UserDeviceId = (long)Convert.ToDecimal(value);
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
            case "accesstoken":
                this.AccessToken = value == null ? null : value.ToString();
                return;
            case "refreshtoken":
                this.RefreshToken = value == null ? null : value.ToString();
                return;
            case "accesstokenexpirestime":
                this.AccessTokenExpiresTime = Convert.ToDateTime(value);
                return;
            case "refreshtokenexpirestime":
                this.RefreshTokenExpiresTime = Convert.ToDateTime(value);
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
            case _DataStruct_.UserDeviceId:
                this.UserDeviceId = Convert.ToInt64(value);
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
            case _DataStruct_.AccessToken:
                this.AccessToken = value == null ? null : value.ToString();
                return;
            case _DataStruct_.RefreshToken:
                this.RefreshToken = value == null ? null : value.ToString();
                return;
            case _DataStruct_.AccessTokenExpiresTime:
                this.AccessTokenExpiresTime = Convert.ToDateTime(value);
                return;
            case _DataStruct_.RefreshTokenExpiresTime:
                this.RefreshTokenExpiresTime = Convert.ToDateTime(value);
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
            case "userdeviceid":
                return this.UserDeviceId;
            case "adddate":
                return this.AddDate;
            case "userid":
                return this.UserId;
            case "deviceid":
                return this.DeviceId;
            case "accesstoken":
                return this.AccessToken;
            case "refreshtoken":
                return this.RefreshToken;
            case "accesstokenexpirestime":
                return this.AccessTokenExpiresTime;
            case "refreshtokenexpirestime":
                return this.RefreshTokenExpiresTime;
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
                case _DataStruct_.UserDeviceId:
                    return this.UserDeviceId;
                case _DataStruct_.AddDate:
                    return this.AddDate;
                case _DataStruct_.UserId:
                    return this.UserId;
                case _DataStruct_.DeviceId:
                    return this.DeviceId;
                case _DataStruct_.AccessToken:
                    return this.AccessToken;
                case _DataStruct_.RefreshToken:
                    return this.RefreshToken;
                case _DataStruct_.AccessTokenExpiresTime:
                    return this.AccessTokenExpiresTime;
                case _DataStruct_.RefreshTokenExpiresTime:
                    return this.RefreshTokenExpiresTime;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(UserTokenData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as UserTokenData;
            if(sourceEntity == null)
                return;
            this._id = sourceEntity._id;
            this._userDeviceId = sourceEntity._userDeviceId;
            this._addDate = sourceEntity._addDate;
            this._userId = sourceEntity._userId;
            this._deviceId = sourceEntity._deviceId;
            this._accessToken = sourceEntity._accessToken;
            this._refreshToken = sourceEntity._refreshToken;
            this._accessTokenExpiresTime = sourceEntity._accessTokenExpiresTime;
            this._refreshTokenExpiresTime = sourceEntity._refreshTokenExpiresTime;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(UserTokenData source)
        {
                this.Id = source.Id;
                this.UserDeviceId = source.UserDeviceId;
                this.AddDate = source.AddDate;
                this.UserId = source.UserId;
                this.DeviceId = source.DeviceId;
                this.AccessToken = source.AccessToken;
                this.RefreshToken = source.RefreshToken;
                this.AccessTokenExpiresTime = source.AccessTokenExpiresTime;
                this.RefreshTokenExpiresTime = source.RefreshTokenExpiresTime;
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
                OnUserDeviceIdModified(subsist,false);
                OnAddDateModified(subsist,false);
                OnUserIdModified(subsist,false);
                OnDeviceIdModified(subsist,false);
                OnAccessTokenModified(subsist,false);
                OnRefreshTokenModified(subsist,false);
                OnAccessTokenExpiresTimeModified(subsist,false);
                OnRefreshTokenExpiresTimeModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIdModified(subsist,true);
                OnUserDeviceIdModified(subsist,true);
                OnAddDateModified(subsist,true);
                OnUserIdModified(subsist,true);
                OnDeviceIdModified(subsist,true);
                OnAccessTokenModified(subsist,true);
                OnRefreshTokenModified(subsist,true);
                OnAccessTokenExpiresTimeModified(subsist,true);
                OnRefreshTokenExpiresTimeModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[9] > 0)
            {
                OnIdModified(subsist,modifieds[_DataStruct_.Real_Id] == 1);
                OnUserDeviceIdModified(subsist,modifieds[_DataStruct_.Real_UserDeviceId] == 1);
                OnAddDateModified(subsist,modifieds[_DataStruct_.Real_AddDate] == 1);
                OnUserIdModified(subsist,modifieds[_DataStruct_.Real_UserId] == 1);
                OnDeviceIdModified(subsist,modifieds[_DataStruct_.Real_DeviceId] == 1);
                OnAccessTokenModified(subsist,modifieds[_DataStruct_.Real_AccessToken] == 1);
                OnRefreshTokenModified(subsist,modifieds[_DataStruct_.Real_RefreshToken] == 1);
                OnAccessTokenExpiresTimeModified(subsist,modifieds[_DataStruct_.Real_AccessTokenExpiresTime] == 1);
                OnRefreshTokenExpiresTimeModified(subsist,modifieds[_DataStruct_.Real_RefreshTokenExpiresTime] == 1);
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
        /// 设备标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserDeviceIdModified(EntitySubsist subsist,bool isModified);

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
        /// 用户标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 设备标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDeviceIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 访问令牌修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAccessTokenModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 刷新令牌修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRefreshTokenModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 访问令牌过期时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAccessTokenExpiresTimeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 刷新令牌过期时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRefreshTokenExpiresTimeModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"UserToken";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户令牌";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户访问令牌";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0xC0007;
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
            /// 设备标识的数字标识
            /// </summary>
            public const byte UserDeviceId = 8;
            
            /// <summary>
            /// 设备标识的实时记录顺序
            /// </summary>
            public const int Real_UserDeviceId = 1;

            /// <summary>
            /// 登录时间的数字标识
            /// </summary>
            public const byte AddDate = 9;
            
            /// <summary>
            /// 登录时间的实时记录顺序
            /// </summary>
            public const int Real_AddDate = 2;

            /// <summary>
            /// 用户标识的数字标识
            /// </summary>
            public const byte UserId = 2;
            
            /// <summary>
            /// 用户标识的实时记录顺序
            /// </summary>
            public const int Real_UserId = 3;

            /// <summary>
            /// 设备标识的数字标识
            /// </summary>
            public const byte DeviceId = 3;
            
            /// <summary>
            /// 设备标识的实时记录顺序
            /// </summary>
            public const int Real_DeviceId = 4;

            /// <summary>
            /// 访问令牌的数字标识
            /// </summary>
            public const byte AccessToken = 4;
            
            /// <summary>
            /// 访问令牌的实时记录顺序
            /// </summary>
            public const int Real_AccessToken = 5;

            /// <summary>
            /// 刷新令牌的数字标识
            /// </summary>
            public const byte RefreshToken = 5;
            
            /// <summary>
            /// 刷新令牌的实时记录顺序
            /// </summary>
            public const int Real_RefreshToken = 6;

            /// <summary>
            /// 访问令牌过期时间的数字标识
            /// </summary>
            public const byte AccessTokenExpiresTime = 6;
            
            /// <summary>
            /// 访问令牌过期时间的实时记录顺序
            /// </summary>
            public const int Real_AccessTokenExpiresTime = 7;

            /// <summary>
            /// 刷新令牌过期时间的数字标识
            /// </summary>
            public const byte RefreshTokenExpiresTime = 7;
            
            /// <summary>
            /// 刷新令牌过期时间的实时记录顺序
            /// </summary>
            public const int Real_RefreshTokenExpiresTime = 8;

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
                        Real_UserDeviceId,
                        new PropertySturct
                        {
                            Index        = UserDeviceId,
                            Name         = "UserDeviceId",
                            Title        = "设备标识",
                            Caption      = @"设备标识",
                            Description  = @"设备标识",
                            ColumnName   = "user_device_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
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
                        Real_UserId,
                        new PropertySturct
                        {
                            Index        = UserId,
                            Name         = "UserId",
                            Title        = "用户标识",
                            Caption      = @"用户标识",
                            Description  = @"用户标识",
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
                            Title        = "设备标识",
                            Caption      = @"设备标识",
                            Description  = @"App 端用户生成的DeviceId",
                            ColumnName   = "device_id",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AccessToken,
                        new PropertySturct
                        {
                            Index        = AccessToken,
                            Name         = "AccessToken",
                            Title        = "访问令牌",
                            Caption      = @"访问令牌",
                            Description  = @"用户accesstoken",
                            ColumnName   = "access_token",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RefreshToken,
                        new PropertySturct
                        {
                            Index        = RefreshToken,
                            Name         = "RefreshToken",
                            Title        = "刷新令牌",
                            Caption      = @"刷新令牌",
                            Description  = @"用户的Refresh token，用于刷新access token",
                            ColumnName   = "refresh_token",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AccessTokenExpiresTime,
                        new PropertySturct
                        {
                            Index        = AccessTokenExpiresTime,
                            Name         = "AccessTokenExpiresTime",
                            Title        = "访问令牌过期时间",
                            Caption      = @"访问令牌过期时间",
                            Description  = @"access token过期时间",
                            ColumnName   = "access_token_expires_time",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_RefreshTokenExpiresTime,
                        new PropertySturct
                        {
                            Index        = RefreshTokenExpiresTime,
                            Name         = "RefreshTokenExpiresTime",
                            Title        = "刷新令牌过期时间",
                            Caption      = @"刷新令牌过期时间",
                            Description  = @"refresh token 过期时间",
                            ColumnName   = "refresh_token_expires_time",
                            PropertyType = typeof(DateTime),
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