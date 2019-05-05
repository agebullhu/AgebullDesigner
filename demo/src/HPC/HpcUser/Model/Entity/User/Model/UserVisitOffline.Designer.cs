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
    /// 用户离线访问
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class UserVisitOfflineData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserVisitOfflineData()
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
        public void ChangePrimaryKey(long vID)
        {
            _vID = vID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _vID;

        partial void OnVIDGet();

        partial void OnVIDSet(ref long value);

        partial void OnVIDLoad(ref long value);

        partial void OnVIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("VID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long VID
        {
            get
            {
                OnVIDGet();
                return this._vID;
            }
            set
            {
                if(this._vID == value)
                    return;
                //if(this._vID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnVIDSet(ref value);
                this._vID = value;
                this.OnPropertyChanged(_DataStruct_.Real_VID);
                OnVIDSeted();
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
        /// 组织标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _orgOID;

        partial void OnOrgOIDGet();

        partial void OnOrgOIDSet(ref long value);

        partial void OnOrgOIDSeted();

        
        /// <summary>
        /// 组织标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgOID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织标识")]
        public  long OrgOID
        {
            get
            {
                OnOrgOIDGet();
                return this._orgOID;
            }
            set
            {
                if(this._orgOID == value)
                    return;
                OnOrgOIDSet(ref value);
                this._orgOID = value;
                OnOrgOIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrgOID);
            }
        }
        /// <summary>
        /// 设备确实
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _deviceDID;

        partial void OnDeviceDIDGet();

        partial void OnDeviceDIDSet(ref long value);

        partial void OnDeviceDIDSeted();

        
        /// <summary>
        /// 设备确实
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("DeviceDID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"设备确实")]
        public  long DeviceDID
        {
            get
            {
                OnDeviceDIDGet();
                return this._deviceDID;
            }
            set
            {
                if(this._deviceDID == value)
                    return;
                OnDeviceDIDSet(ref value);
                this._deviceDID = value;
                OnDeviceDIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_DeviceDID);
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
        /// 时间在
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _timeIn;

        partial void OnTimeInGet();

        partial void OnTimeInSet(ref DateTime value);

        partial void OnTimeInSeted();

        
        /// <summary>
        /// 时间在
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("TimeIn", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"时间在")]
        public  DateTime TimeIn
        {
            get
            {
                OnTimeInGet();
                return this._timeIn;
            }
            set
            {
                if(this._timeIn == value)
                    return;
                OnTimeInSet(ref value);
                this._timeIn = value;
                OnTimeInSeted();
                this.OnPropertyChanged(_DataStruct_.Real_TimeIn);
            }
        }
        /// <summary>
        /// 时间到
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _timeOut;

        partial void OnTimeOutGet();

        partial void OnTimeOutSet(ref DateTime value);

        partial void OnTimeOutSeted();

        
        /// <summary>
        /// 时间到
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("TimeOut", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"时间到")]
        public  DateTime TimeOut
        {
            get
            {
                OnTimeOutGet();
                return this._timeOut;
            }
            set
            {
                if(this._timeOut == value)
                    return;
                OnTimeOutSet(ref value);
                this._timeOut = value;
                OnTimeOutSeted();
                this.OnPropertyChanged(_DataStruct_.Real_TimeOut);
            }
        }
        /// <summary>
        /// 方向
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _direction;

        partial void OnDirectionGet();

        partial void OnDirectionSet(ref string value);

        partial void OnDirectionSeted();

        
        /// <summary>
        /// 方向
        /// </summary>
        /// <value>
        /// 可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Direction", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"方向")]
        public  string Direction
        {
            get
            {
                OnDirectionGet();
                return this._direction;
            }
            set
            {
                if(this._direction == value)
                    return;
                OnDirectionSet(ref value);
                this._direction = value;
                OnDirectionSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Direction);
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
                return this.VID;
            }
            set
            {
                this.VID = value;
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
            case "vid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.VID = vl;
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
            case "orgoid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.OrgOID = vl;
                        return true;
                    }
                }
                return false;
            case "devicedid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.DeviceDID = vl;
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
            case "timein":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.TimeIn = vl;
                        return true;
                    }
                }
                return false;
            case "timeout":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.TimeOut = vl;
                        return true;
                    }
                }
                return false;
            case "direction":
                this.Direction = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "vid":
                this.VID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "orgoid":
                this.OrgOID = (long)Convert.ToDecimal(value);
                return;
            case "devicedid":
                this.DeviceDID = (long)Convert.ToDecimal(value);
                return;
            case "useruid":
                this.UserUID = (long)Convert.ToDecimal(value);
                return;
            case "timein":
                this.TimeIn = Convert.ToDateTime(value);
                return;
            case "timeout":
                this.TimeOut = Convert.ToDateTime(value);
                return;
            case "direction":
                this.Direction = value == null ? null : value.ToString();
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
            case _DataStruct_.VID:
                this.VID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgOID:
                this.OrgOID = Convert.ToInt64(value);
                return;
            case _DataStruct_.DeviceDID:
                this.DeviceDID = Convert.ToInt64(value);
                return;
            case _DataStruct_.UserUID:
                this.UserUID = Convert.ToInt64(value);
                return;
            case _DataStruct_.TimeIn:
                this.TimeIn = Convert.ToDateTime(value);
                return;
            case _DataStruct_.TimeOut:
                this.TimeOut = Convert.ToDateTime(value);
                return;
            case _DataStruct_.Direction:
                this.Direction = value == null ? null : value.ToString();
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
            case "vid":
                return this.VID;
            case "sitesid":
                return this.SiteSID;
            case "orgoid":
                return this.OrgOID;
            case "devicedid":
                return this.DeviceDID;
            case "useruid":
                return this.UserUID;
            case "timein":
                return this.TimeIn;
            case "timeout":
                return this.TimeOut;
            case "direction":
                return this.Direction;
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
                case _DataStruct_.VID:
                    return this.VID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.OrgOID:
                    return this.OrgOID;
                case _DataStruct_.DeviceDID:
                    return this.DeviceDID;
                case _DataStruct_.UserUID:
                    return this.UserUID;
                case _DataStruct_.TimeIn:
                    return this.TimeIn;
                case _DataStruct_.TimeOut:
                    return this.TimeOut;
                case _DataStruct_.Direction:
                    return this.Direction;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(UserVisitOfflineData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as UserVisitOfflineData;
            if(sourceEntity == null)
                return;
            this._vID = sourceEntity._vID;
            this._siteSID = sourceEntity._siteSID;
            this._orgOID = sourceEntity._orgOID;
            this._deviceDID = sourceEntity._deviceDID;
            this._userUID = sourceEntity._userUID;
            this._timeIn = sourceEntity._timeIn;
            this._timeOut = sourceEntity._timeOut;
            this._direction = sourceEntity._direction;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(UserVisitOfflineData source)
        {
                this.VID = source.VID;
                this.SiteSID = source.SiteSID;
                this.OrgOID = source.OrgOID;
                this.DeviceDID = source.DeviceDID;
                this.UserUID = source.UserUID;
                this.TimeIn = source.TimeIn;
                this.TimeOut = source.TimeOut;
                this.Direction = source.Direction;
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
                OnVIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnOrgOIDModified(subsist,false);
                OnDeviceDIDModified(subsist,false);
                OnUserUIDModified(subsist,false);
                OnTimeInModified(subsist,false);
                OnTimeOutModified(subsist,false);
                OnDirectionModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnVIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnOrgOIDModified(subsist,true);
                OnDeviceDIDModified(subsist,true);
                OnUserUIDModified(subsist,true);
                OnTimeInModified(subsist,true);
                OnTimeOutModified(subsist,true);
                OnDirectionModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[8] > 0)
            {
                OnVIDModified(subsist,modifieds[_DataStruct_.Real_VID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnOrgOIDModified(subsist,modifieds[_DataStruct_.Real_OrgOID] == 1);
                OnDeviceDIDModified(subsist,modifieds[_DataStruct_.Real_DeviceDID] == 1);
                OnUserUIDModified(subsist,modifieds[_DataStruct_.Real_UserUID] == 1);
                OnTimeInModified(subsist,modifieds[_DataStruct_.Real_TimeIn] == 1);
                OnTimeOutModified(subsist,modifieds[_DataStruct_.Real_TimeOut] == 1);
                OnDirectionModified(subsist,modifieds[_DataStruct_.Real_Direction] == 1);
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
        partial void OnVIDModified(EntitySubsist subsist,bool isModified);

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
        /// 组织标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgOIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 设备确实修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDeviceDIDModified(EntitySubsist subsist,bool isModified);

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
        /// 时间在修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTimeInModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 时间到修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTimeOutModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 方向修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnDirectionModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"UserVisitOffline";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户离线访问";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户离线访问";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "VID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte VID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_VID = 0;

            /// <summary>
            /// 站点标识的数字标识
            /// </summary>
            public const byte SiteSID = 2;
            
            /// <summary>
            /// 站点标识的实时记录顺序
            /// </summary>
            public const int Real_SiteSID = 1;

            /// <summary>
            /// 组织标识的数字标识
            /// </summary>
            public const byte OrgOID = 3;
            
            /// <summary>
            /// 组织标识的实时记录顺序
            /// </summary>
            public const int Real_OrgOID = 2;

            /// <summary>
            /// 设备确实的数字标识
            /// </summary>
            public const byte DeviceDID = 4;
            
            /// <summary>
            /// 设备确实的实时记录顺序
            /// </summary>
            public const int Real_DeviceDID = 3;

            /// <summary>
            /// 组织标识的数字标识
            /// </summary>
            public const byte UserUID = 5;
            
            /// <summary>
            /// 组织标识的实时记录顺序
            /// </summary>
            public const int Real_UserUID = 4;

            /// <summary>
            /// 时间在的数字标识
            /// </summary>
            public const byte TimeIn = 6;
            
            /// <summary>
            /// 时间在的实时记录顺序
            /// </summary>
            public const int Real_TimeIn = 5;

            /// <summary>
            /// 时间到的数字标识
            /// </summary>
            public const byte TimeOut = 7;
            
            /// <summary>
            /// 时间到的实时记录顺序
            /// </summary>
            public const int Real_TimeOut = 6;

            /// <summary>
            /// 方向的数字标识
            /// </summary>
            public const byte Direction = 8;
            
            /// <summary>
            /// 方向的实时记录顺序
            /// </summary>
            public const int Real_Direction = 7;

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
                        Real_VID,
                        new PropertySturct
                        {
                            Index        = VID,
                            Name         = "VID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "VID",
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
                        Real_OrgOID,
                        new PropertySturct
                        {
                            Index        = OrgOID,
                            Name         = "OrgOID",
                            Title        = "组织标识",
                            Caption      = @"组织标识",
                            Description  = @"组织标识",
                            ColumnName   = "OrgOID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_DeviceDID,
                        new PropertySturct
                        {
                            Index        = DeviceDID,
                            Name         = "DeviceDID",
                            Title        = "设备确实",
                            Caption      = @"设备确实",
                            Description  = @"设备确实",
                            ColumnName   = "DeviceDID",
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
                        Real_TimeIn,
                        new PropertySturct
                        {
                            Index        = TimeIn,
                            Name         = "TimeIn",
                            Title        = "时间在",
                            Caption      = @"时间在",
                            Description  = @"时间在",
                            ColumnName   = "TimeIn",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_TimeOut,
                        new PropertySturct
                        {
                            Index        = TimeOut,
                            Name         = "TimeOut",
                            Title        = "时间到",
                            Caption      = @"时间到",
                            Description  = @"时间到",
                            ColumnName   = "TimeOut",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Direction,
                        new PropertySturct
                        {
                            Index        = Direction,
                            Name         = "Direction",
                            Title        = "方向",
                            Caption      = @"方向",
                            Description  = @"方向",
                            ColumnName   = "Direction",
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