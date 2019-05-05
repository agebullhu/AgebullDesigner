/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/15 10:58:48*/
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

using Agebull.Common.Organizations;
using Agebull.Common.OAuth;
#endregion

namespace Agebull.Common.AppManage
{
    /// <summary>
    /// 应用页面关联
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class AppPageData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public AppPageData()
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
        public void ChangePrimaryKey(long appPageId)
        {
            _appPageId = appPageId;
        }
        /// <summary>
        /// 应用页面关联标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _appPageId;

        partial void OnAppPageIdGet();

        partial void OnAppPageIdSet(ref long value);

        partial void OnAppPageIdLoad(ref long value);

        partial void OnAppPageIdSeted();

        
        /// <summary>
        /// 应用页面关联标识
        /// </summary>
        [DataMember , JsonProperty("AppPageId", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"应用页面关联标识")]
        public long AppPageId
        {
            get
            {
                OnAppPageIdGet();
                return this._appPageId;
            }
            set
            {
                if(this._appPageId == value)
                    return;
                //if(this._appPageId > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnAppPageIdSet(ref value);
                this._appPageId = value;
                this.OnPropertyChanged(_DataStruct_.Real_AppPageId);
                OnAppPageIdSeted();
            }
        }
        /// <summary>
        /// 站点标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _siteID;

        partial void OnSiteIDGet();

        partial void OnSiteIDSet(ref long value);

        partial void OnSiteIDSeted();

        
        /// <summary>
        /// 站点标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SiteID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"站点标识")]
        public  long SiteID
        {
            get
            {
                OnSiteIDGet();
                return this._siteID;
            }
            set
            {
                if(this._siteID == value)
                    return;
                OnSiteIDSet(ref value);
                this._siteID = value;
                OnSiteIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SiteID);
            }
        }
        /// <summary>
        /// 应用信息外键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _appId;

        partial void OnAppIdGet();

        partial void OnAppIdSet(ref long value);

        partial void OnAppIdSeted();

        
        /// <summary>
        /// 应用信息外键
        /// </summary>
        [DataMember , JsonProperty("appInfoId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用信息外键")]
        public  long AppId
        {
            get
            {
                OnAppIdGet();
                return this._appId;
            }
            set
            {
                if(this._appId == value)
                    return;
                OnAppIdSet(ref value);
                this._appId = value;
                OnAppIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AppId);
            }
        }
        /// <summary>
        /// 页面节点外键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _pageItemId;

        partial void OnPageItemIdGet();

        partial void OnPageItemIdSet(ref long value);

        partial void OnPageItemIdSeted();

        
        /// <summary>
        /// 页面节点外键
        /// </summary>
        [DataMember , JsonProperty("page_item_id", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"页面节点外键")]
        public  long PageItemId
        {
            get
            {
                OnPageItemIdGet();
                return this._pageItemId;
            }
            set
            {
                if(this._pageItemId == value)
                    return;
                OnPageItemIdSet(ref value);
                this._pageItemId = value;
                OnPageItemIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_PageItemId);
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
                return this.AppPageId;
            }
            set
            {
                this.AppPageId = value;
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
            case "apppageid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.AppPageId = vl;
                        return true;
                    }
                }
                return false;
            case "siteid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.SiteID = vl;
                        return true;
                    }
                }
                return false;
            case "appid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.AppId = vl;
                        return true;
                    }
                }
                return false;
            case "pageitemid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.PageItemId = vl;
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
            case "apppageid":
                this.AppPageId = (long)Convert.ToDecimal(value);
                return;
            case "siteid":
                this.SiteID = (long)Convert.ToDecimal(value);
                return;
            case "appid":
                this.AppId = (long)Convert.ToDecimal(value);
                return;
            case "pageitemid":
                this.PageItemId = (long)Convert.ToDecimal(value);
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
            case _DataStruct_.AppPageId:
                this.AppPageId = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteID:
                this.SiteID = Convert.ToInt64(value);
                return;
            case _DataStruct_.AppId:
                this.AppId = Convert.ToInt64(value);
                return;
            case _DataStruct_.PageItemId:
                this.PageItemId = Convert.ToInt64(value);
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
            case "apppageid":
                return this.AppPageId;
            case "siteid":
                return this.SiteID;
            case "appid":
                return this.AppId;
            case "pageitemid":
                return this.PageItemId;
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
                case _DataStruct_.AppPageId:
                    return this.AppPageId;
                case _DataStruct_.SiteID:
                    return this.SiteID;
                case _DataStruct_.AppId:
                    return this.AppId;
                case _DataStruct_.PageItemId:
                    return this.PageItemId;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(AppPageData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as AppPageData;
            if(sourceEntity == null)
                return;
            this._appPageId = sourceEntity._appPageId;
            this._siteID = sourceEntity._siteID;
            this._appId = sourceEntity._appId;
            this._pageItemId = sourceEntity._pageItemId;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(AppPageData source)
        {
                this.AppPageId = source.AppPageId;
                this.SiteID = source.SiteID;
                this.AppId = source.AppId;
                this.PageItemId = source.PageItemId;
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
                OnAppPageIdModified(subsist,false);
                OnSiteIDModified(subsist,false);
                OnAppIdModified(subsist,false);
                OnPageItemIdModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnAppPageIdModified(subsist,true);
                OnSiteIDModified(subsist,true);
                OnAppIdModified(subsist,true);
                OnPageItemIdModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[4] > 0)
            {
                OnAppPageIdModified(subsist,modifieds[_DataStruct_.Real_AppPageId] == 1);
                OnSiteIDModified(subsist,modifieds[_DataStruct_.Real_SiteID] == 1);
                OnAppIdModified(subsist,modifieds[_DataStruct_.Real_AppId] == 1);
                OnPageItemIdModified(subsist,modifieds[_DataStruct_.Real_PageItemId] == 1);
            }
        }

        /// <summary>
        /// 应用页面关联标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppPageIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 站点标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSiteIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 应用信息外键修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 页面节点外键修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPageItemIdModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"AppPage";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"应用页面关联";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"应用页面关联";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "AppPageId";
            
            
            /// <summary>
            /// 应用页面关联标识的数字标识
            /// </summary>
            public const byte AppPageId = 1;
            
            /// <summary>
            /// 应用页面关联标识的实时记录顺序
            /// </summary>
            public const int Real_AppPageId = 0;

            /// <summary>
            /// 站点标识的数字标识
            /// </summary>
            public const byte SiteID = 2;
            
            /// <summary>
            /// 站点标识的实时记录顺序
            /// </summary>
            public const int Real_SiteID = 1;

            /// <summary>
            /// 应用信息外键的数字标识
            /// </summary>
            public const byte AppId = 4;
            
            /// <summary>
            /// 应用信息外键的实时记录顺序
            /// </summary>
            public const int Real_AppId = 2;

            /// <summary>
            /// 页面节点外键的数字标识
            /// </summary>
            public const byte PageItemId = 5;
            
            /// <summary>
            /// 页面节点外键的实时记录顺序
            /// </summary>
            public const int Real_PageItemId = 3;

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
                        Real_AppPageId,
                        new PropertySturct
                        {
                            Index        = AppPageId,
                            Name         = "AppPageId",
                            Title        = "应用页面关联标识",
                            Caption      = @"应用页面关联标识",
                            Description  = @"应用页面关联标识",
                            ColumnName   = "app_page_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SiteID,
                        new PropertySturct
                        {
                            Index        = SiteID,
                            Name         = "SiteID",
                            Title        = "站点标识",
                            Caption      = @"站点标识",
                            Description  = @"站点标识",
                            ColumnName   = "site_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_AppId,
                        new PropertySturct
                        {
                            Index        = AppId,
                            Name         = "AppId",
                            Title        = "应用信息外键",
                            Caption      = @"应用信息外键",
                            Description  = @"应用信息外键",
                            ColumnName   = "app_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PageItemId,
                        new PropertySturct
                        {
                            Index        = PageItemId,
                            Name         = "PageItemId",
                            Title        = "页面节点外键",
                            Caption      = @"页面节点外键",
                            Description  = @"页面节点外键",
                            ColumnName   = "page_item_id",
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