/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/12 21:30:55*/
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
    /// 导航关联
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class PermitNavigationSubData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public PermitNavigationSubData()
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
        public void ChangePrimaryKey(long sID)
        {
            _sID = sID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _sID;

        partial void OnSIDGet();

        partial void OnSIDSet(ref long value);

        partial void OnSIDLoad(ref long value);

        partial void OnSIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("SID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long SID
        {
            get
            {
                OnSIDGet();
                return this._sID;
            }
            set
            {
                if(this._sID == value)
                    return;
                //if(this._sID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnSIDSet(ref value);
                this._sID = value;
                this.OnPropertyChanged(_DataStruct_.Real_SID);
                OnSIDSeted();
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
        /// 导航标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _navigationNID;

        partial void OnNavigationNIDGet();

        partial void OnNavigationNIDSet(ref long value);

        partial void OnNavigationNIDSeted();

        
        /// <summary>
        /// 导航标识
        /// </summary>
        [DataMember , JsonProperty("NavigationNID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"导航标识")]
        public  long NavigationNID
        {
            get
            {
                OnNavigationNIDGet();
                return this._navigationNID;
            }
            set
            {
                if(this._navigationNID == value)
                    return;
                OnNavigationNIDSet(ref value);
                this._navigationNID = value;
                OnNavigationNIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_NavigationNID);
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
                return this.SID;
            }
            set
            {
                this.SID = value;
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
            case "sid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.SID = vl;
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
            case "navigationnid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.NavigationNID = vl;
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
            case "sid":
                this.SID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "navigationnid":
                this.NavigationNID = (long)Convert.ToDecimal(value);
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
            case _DataStruct_.SID:
                this.SID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.NavigationNID:
                this.NavigationNID = Convert.ToInt64(value);
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
            case "sid":
                return this.SID;
            case "sitesid":
                return this.SiteSID;
            case "navigationnid":
                return this.NavigationNID;
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
                case _DataStruct_.SID:
                    return this.SID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.NavigationNID:
                    return this.NavigationNID;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(PermitNavigationSubData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as PermitNavigationSubData;
            if(sourceEntity == null)
                return;
            this._sID = sourceEntity._sID;
            this._siteSID = sourceEntity._siteSID;
            this._navigationNID = sourceEntity._navigationNID;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(PermitNavigationSubData source)
        {
                this.SID = source.SID;
                this.SiteSID = source.SiteSID;
                this.NavigationNID = source.NavigationNID;
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
                OnSIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnNavigationNIDModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnSIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnNavigationNIDModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[3] > 0)
            {
                OnSIDModified(subsist,modifieds[_DataStruct_.Real_SID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnNavigationNIDModified(subsist,modifieds[_DataStruct_.Real_NavigationNID] == 1);
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
        partial void OnSIDModified(EntitySubsist subsist,bool isModified);

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
        /// 导航标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnNavigationNIDModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"PermitNavigationSub";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"导航关联";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"导航关联";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "SID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte SID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_SID = 0;

            /// <summary>
            /// 站点标识的数字标识
            /// </summary>
            public const byte SiteSID = 2;
            
            /// <summary>
            /// 站点标识的实时记录顺序
            /// </summary>
            public const int Real_SiteSID = 1;

            /// <summary>
            /// 导航标识的数字标识
            /// </summary>
            public const byte NavigationNID = 3;
            
            /// <summary>
            /// 导航标识的实时记录顺序
            /// </summary>
            public const int Real_NavigationNID = 2;

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
                        Real_SID,
                        new PropertySturct
                        {
                            Index        = SID,
                            Name         = "SID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "SID",
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
                        Real_NavigationNID,
                        new PropertySturct
                        {
                            Index        = NavigationNID,
                            Name         = "NavigationNID",
                            Title        = "导航标识",
                            Caption      = @"导航标识",
                            Description  = @"导航标识",
                            ColumnName   = "NavigationNID",
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