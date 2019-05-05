/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:52*/
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
    /// 用户OpenID
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class UserOpenidData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserOpenidData()
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
        /// Wx应用标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _wxAppid;

        partial void OnWxAppidGet();

        partial void OnWxAppidSet(ref string value);

        partial void OnWxAppidSeted();

        
        /// <summary>
        /// Wx应用标识
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("WxAppid", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"Wx应用标识")]
        public  string WxAppid
        {
            get
            {
                OnWxAppidGet();
                return this._wxAppid;
            }
            set
            {
                if(this._wxAppid == value)
                    return;
                OnWxAppidSet(ref value);
                this._wxAppid = value;
                OnWxAppidSeted();
                this.OnPropertyChanged(_DataStruct_.Real_WxAppid);
            }
        }
        /// <summary>
        /// WXOpenID
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _wxOpenid;

        partial void OnWxOpenidGet();

        partial void OnWxOpenidSet(ref string value);

        partial void OnWxOpenidSeted();

        
        /// <summary>
        /// WXOpenID
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("WxOpenid", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"WXOpenID")]
        public  string WxOpenid
        {
            get
            {
                OnWxOpenidGet();
                return this._wxOpenid;
            }
            set
            {
                if(this._wxOpenid == value)
                    return;
                OnWxOpenidSet(ref value);
                this._wxOpenid = value;
                OnWxOpenidSeted();
                this.OnPropertyChanged(_DataStruct_.Real_WxOpenid);
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
            case "wxappid":
                this.WxAppid = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "wxopenid":
                this.WxOpenid = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "useruid":
                this.UserUID = (long)Convert.ToDecimal(value);
                return;
            case "wxappid":
                this.WxAppid = value == null ? null : value.ToString();
                return;
            case "wxopenid":
                this.WxOpenid = value == null ? null : value.ToString();
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
            case _DataStruct_.UserUID:
                this.UserUID = Convert.ToInt64(value);
                return;
            case _DataStruct_.WxAppid:
                this.WxAppid = value == null ? null : value.ToString();
                return;
            case _DataStruct_.WxOpenid:
                this.WxOpenid = value == null ? null : value.ToString();
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
            case "useruid":
                return this.UserUID;
            case "wxappid":
                return this.WxAppid;
            case "wxopenid":
                return this.WxOpenid;
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
                case _DataStruct_.UserUID:
                    return this.UserUID;
                case _DataStruct_.WxAppid:
                    return this.WxAppid;
                case _DataStruct_.WxOpenid:
                    return this.WxOpenid;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(UserOpenidData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as UserOpenidData;
            if(sourceEntity == null)
                return;
            this._oID = sourceEntity._oID;
            this._userUID = sourceEntity._userUID;
            this._wxAppid = sourceEntity._wxAppid;
            this._wxOpenid = sourceEntity._wxOpenid;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(UserOpenidData source)
        {
                this.OID = source.OID;
                this.UserUID = source.UserUID;
                this.WxAppid = source.WxAppid;
                this.WxOpenid = source.WxOpenid;
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
                OnUserUIDModified(subsist,false);
                OnWxAppidModified(subsist,false);
                OnWxOpenidModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnOIDModified(subsist,true);
                OnUserUIDModified(subsist,true);
                OnWxAppidModified(subsist,true);
                OnWxOpenidModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[4] > 0)
            {
                OnOIDModified(subsist,modifieds[_DataStruct_.Real_OID] == 1);
                OnUserUIDModified(subsist,modifieds[_DataStruct_.Real_UserUID] == 1);
                OnWxAppidModified(subsist,modifieds[_DataStruct_.Real_WxAppid] == 1);
                OnWxOpenidModified(subsist,modifieds[_DataStruct_.Real_WxOpenid] == 1);
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
        /// 组织标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserUIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// Wx应用标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnWxAppidModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// WXOpenID修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnWxOpenidModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"UserOpenid";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户OpenID";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户OpenID";
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
            /// 组织标识的数字标识
            /// </summary>
            public const byte UserUID = 2;
            
            /// <summary>
            /// 组织标识的实时记录顺序
            /// </summary>
            public const int Real_UserUID = 1;

            /// <summary>
            /// Wx应用标识的数字标识
            /// </summary>
            public const byte WxAppid = 3;
            
            /// <summary>
            /// Wx应用标识的实时记录顺序
            /// </summary>
            public const int Real_WxAppid = 2;

            /// <summary>
            /// WXOpenID的数字标识
            /// </summary>
            public const byte WxOpenid = 4;
            
            /// <summary>
            /// WXOpenID的实时记录顺序
            /// </summary>
            public const int Real_WxOpenid = 3;

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
                        Real_WxAppid,
                        new PropertySturct
                        {
                            Index        = WxAppid,
                            Name         = "WxAppid",
                            Title        = "Wx应用标识",
                            Caption      = @"Wx应用标识",
                            Description  = @"Wx应用标识",
                            ColumnName   = "WxAppid",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_WxOpenid,
                        new PropertySturct
                        {
                            Index        = WxOpenid,
                            Name         = "WxOpenid",
                            Title        = "WXOpenID",
                            Caption      = @"WXOpenID",
                            Description  = @"WXOpenID",
                            ColumnName   = "WxOpenid",
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