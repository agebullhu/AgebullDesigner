/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 17:33:40*/
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

using Agebull.Common.WebApi.Auth;
#endregion

namespace Agebull.Common.AppManage
{
    /// <summary>
    /// 页面节点
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class PageItemData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public PageItemData()
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
        [DataMember , JsonProperty("id", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"标识")]
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
        /// 应用信息外键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _appInfoId;

        partial void OnAppInfoIdGet();

        partial void OnAppInfoIdSet(ref long value);

        partial void OnAppInfoIdSeted();

        
        /// <summary>
        /// 应用信息外键
        /// </summary>
        [DataMember , JsonProperty("appInfoId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"应用信息外键")]
        public  long AppInfoId
        {
            get
            {
                OnAppInfoIdGet();
                return this._appInfoId;
            }
            set
            {
                if(this._appInfoId == value)
                    return;
                OnAppInfoIdSet(ref value);
                this._appInfoId = value;
                OnAppInfoIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_AppInfoId);
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _name;

        partial void OnNameGet();

        partial void OnNameSet(ref string value);

        partial void OnNameSeted();

        
        /// <summary>
        /// 名称
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("name", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"名称")]
        public  string Name
        {
            get
            {
                OnNameGet();
                return this._name;
            }
            set
            {
                if(this._name == value)
                    return;
                OnNameSet(ref value);
                this._name = value;
                OnNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Name);
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _caption;

        partial void OnCaptionGet();

        partial void OnCaptionSet(ref string value);

        partial void OnCaptionSeted();

        
        /// <summary>
        /// 标题
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("caption", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"标题")]
        public  string Caption
        {
            get
            {
                OnCaptionGet();
                return this._caption;
            }
            set
            {
                if(this._caption == value)
                    return;
                OnCaptionSet(ref value);
                this._caption = value;
                OnCaptionSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Caption);
            }
        }
        /// <summary>
        /// 节点类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public PageItemType _itemType;

        partial void OnItemTypeGet();

        partial void OnItemTypeSet(ref PageItemType value);

        partial void OnItemTypeSeted();

        
        /// <summary>
        /// 节点类型
        /// </summary>
        [DataMember , JsonProperty("itemType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"节点类型")]
        public  PageItemType ItemType
        {
            get
            {
                OnItemTypeGet();
                return this._itemType;
            }
            set
            {
                if(this._itemType == value)
                    return;
                OnItemTypeSet(ref value);
                this._itemType = value;
                OnItemTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ItemType);
            }
        }
        /// <summary>
        /// 序号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _index;

        partial void OnIndexGet();

        partial void OnIndexSet(ref int value);

        partial void OnIndexSeted();

        
        /// <summary>
        /// 序号
        /// </summary>
        [DataMember , JsonProperty("index", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"序号")]
        public  int Index
        {
            get
            {
                OnIndexGet();
                return this._index;
            }
            set
            {
                if(this._index == value)
                    return;
                OnIndexSet(ref value);
                this._index = value;
                OnIndexSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Index);
            }
        }
        /// <summary>
        /// 图标
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _icon;

        partial void OnIconGet();

        partial void OnIconSet(ref string value);

        partial void OnIconSeted();

        
        /// <summary>
        /// 图标
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"图标")]
        public  string Icon
        {
            get
            {
                OnIconGet();
                return this._icon;
            }
            set
            {
                if(this._icon == value)
                    return;
                OnIconSet(ref value);
                this._icon = value;
                OnIconSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Icon);
            }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _url;

        partial void OnUrlGet();

        partial void OnUrlSet(ref string value);

        partial void OnUrlSeted();

        
        /// <summary>
        /// 链接地址
        /// </summary>
        /// <value>
        /// 不能为空.
        /// </value>
        [DataMember , JsonProperty("url", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"链接地址")]
        public  string Url
        {
            get
            {
                OnUrlGet();
                return this._url;
            }
            set
            {
                if(this._url == value)
                    return;
                OnUrlSet(ref value);
                this._url = value;
                OnUrlSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Url);
            }
        }
        /// <summary>
        /// 扩展值
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _extendValue;

        partial void OnExtendValueGet();

        partial void OnExtendValueSet(ref string value);

        partial void OnExtendValueSeted();

        
        /// <summary>
        /// 扩展值
        /// </summary>
        /// <value>
        /// 不能为空.可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataMember , JsonProperty("extendValue", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"扩展值")]
        public  string ExtendValue
        {
            get
            {
                OnExtendValueGet();
                return this._extendValue;
            }
            set
            {
                if(this._extendValue == value)
                    return;
                OnExtendValueSet(ref value);
                this._extendValue = value;
                OnExtendValueSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ExtendValue);
            }
        }
        /// <summary>
        /// 扩展的JSON配置
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _json;

        partial void OnJsonGet();

        partial void OnJsonSet(ref string value);

        partial void OnJsonSeted();

        
        /// <summary>
        /// 扩展的JSON配置
        /// </summary>
        /// <value>
        /// 不能为空.
        /// </value>
        [DataMember , JsonProperty("json", NullValueHandling = NullValueHandling.Ignore) , Browsable(false) , DisplayName(@"扩展的JSON配置")]
        public  string Json
        {
            get
            {
                OnJsonGet();
                return this._json;
            }
            set
            {
                if(this._json == value)
                    return;
                OnJsonSet(ref value);
                this._json = value;
                OnJsonSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Json);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _memo;

        partial void OnMemoGet();

        partial void OnMemoSet(ref string value);

        partial void OnMemoSeted();

        
        /// <summary>
        /// 备注
        /// </summary>
        /// <value>
        /// 不能为空.
        /// </value>
        [DataMember , JsonProperty("memo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"备注")]
        public  string Memo
        {
            get
            {
                OnMemoGet();
                return this._memo;
            }
            set
            {
                if(this._memo == value)
                    return;
                OnMemoSet(ref value);
                this._memo = value;
                OnMemoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Memo);
            }
        }
        /// <summary>
        /// 上级标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _parentId;

        partial void OnParentIdGet();

        partial void OnParentIdSet(ref long value);

        partial void OnParentIdSeted();

        
        /// <summary>
        /// 上级标识
        /// </summary>
        [DataMember , JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"上级标识")]
        public  long ParentId
        {
            get
            {
                OnParentIdGet();
                return this._parentId;
            }
            set
            {
                if(this._parentId == value)
                    return;
                OnParentIdSet(ref value);
                this._parentId = value;
                OnParentIdSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ParentId);
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
            case "appinfoid":
                this.AppInfoId = (long)Convert.ToDecimal(value);
                return;
            case "name":
                this.Name = value == null ? null : value.ToString();
                return;
            case "caption":
                this.Caption = value == null ? null : value.ToString();
                return;
            case "itemtype":
                if (value != null)
                {
                    if(value is int)
                    {
                        this.ItemType = (PageItemType)(int)value;
                    }
                    else if(value is PageItemType)
                    {
                        this.ItemType = (PageItemType)value;
                    }
                    else
                    {
                        var str = value.ToString();
                        PageItemType val;
                        if (PageItemType.TryParse(str, out val))
                        {
                            this.ItemType = val;
                        }
                        else
                        {
                            int vl;
                            if (int.TryParse(str, out vl))
                            {
                                this.ItemType = (PageItemType)vl;
                            }
                        }
                    }
                }
                return;
            case "index":
                this.Index = (int)Convert.ToDecimal(value);
                return;
            case "icon":
                this.Icon = value == null ? null : value.ToString();
                return;
            case "url":
                this.Url = value == null ? null : value.ToString();
                return;
            case "extendvalue":
                this.ExtendValue = value == null ? null : value.ToString();
                return;
            case "json":
                this.Json = value == null ? null : value.ToString();
                return;
            case "memo":
                this.Memo = value == null ? null : value.ToString();
                return;
            case "parentid":
                this.ParentId = (long)Convert.ToDecimal(value);
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
            case _DataStruct_.AppInfoId:
                this.AppInfoId = Convert.ToInt64(value);
                return;
            case _DataStruct_.Name:
                this.Name = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Caption:
                this.Caption = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ItemType:
                this.ItemType = (PageItemType)value;
                return;
            case _DataStruct_.Index:
                this.Index = Convert.ToInt32(value);
                return;
            case _DataStruct_.Icon:
                this.Icon = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Url:
                this.Url = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ExtendValue:
                this.ExtendValue = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Json:
                this.Json = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Memo:
                this.Memo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ParentId:
                this.ParentId = Convert.ToInt64(value);
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
            case "appinfoid":
                return this.AppInfoId;
            case "name":
                return this.Name;
            case "caption":
                return this.Caption;
            case "itemtype":
                return this.ItemType;
            case "index":
                return this.Index;
            case "icon":
                return this.Icon;
            case "url":
                return this.Url;
            case "extendvalue":
                return this.ExtendValue;
            case "json":
                return this.Json;
            case "memo":
                return this.Memo;
            case "parentid":
                return this.ParentId;
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
                case _DataStruct_.AppInfoId:
                    return this.AppInfoId;
                case _DataStruct_.Name:
                    return this.Name;
                case _DataStruct_.Caption:
                    return this.Caption;
                case _DataStruct_.ItemType:
                    return this.ItemType;
                case _DataStruct_.Index:
                    return this.Index;
                case _DataStruct_.Icon:
                    return this.Icon;
                case _DataStruct_.Url:
                    return this.Url;
                case _DataStruct_.ExtendValue:
                    return this.ExtendValue;
                case _DataStruct_.Json:
                    return this.Json;
                case _DataStruct_.Memo:
                    return this.Memo;
                case _DataStruct_.ParentId:
                    return this.ParentId;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(PageItemData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as PageItemData;
            if(sourceEntity == null)
                return;
            this._id = sourceEntity._id;
            this._appInfoId = sourceEntity._appInfoId;
            this._name = sourceEntity._name;
            this._caption = sourceEntity._caption;
            this._itemType = sourceEntity._itemType;
            this._index = sourceEntity._index;
            this._icon = sourceEntity._icon;
            this._url = sourceEntity._url;
            this._extendValue = sourceEntity._extendValue;
            this._json = sourceEntity._json;
            this._memo = sourceEntity._memo;
            this._parentId = sourceEntity._parentId;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(PageItemData source)
        {
                this.Id = source.Id;
                this.AppInfoId = source.AppInfoId;
                this.Name = source.Name;
                this.Caption = source.Caption;
                this.ItemType = source.ItemType;
                this.Index = source.Index;
                this.Icon = source.Icon;
                this.Url = source.Url;
                this.ExtendValue = source.ExtendValue;
                this.Json = source.Json;
                this.Memo = source.Memo;
                this.ParentId = source.ParentId;
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
                OnAppInfoIdModified(subsist,false);
                OnNameModified(subsist,false);
                OnCaptionModified(subsist,false);
                OnItemTypeModified(subsist,false);
                OnIndexModified(subsist,false);
                OnIconModified(subsist,false);
                OnUrlModified(subsist,false);
                OnExtendValueModified(subsist,false);
                OnJsonModified(subsist,false);
                OnMemoModified(subsist,false);
                OnParentIdModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnIdModified(subsist,true);
                OnAppInfoIdModified(subsist,true);
                OnNameModified(subsist,true);
                OnCaptionModified(subsist,true);
                OnItemTypeModified(subsist,true);
                OnIndexModified(subsist,true);
                OnIconModified(subsist,true);
                OnUrlModified(subsist,true);
                OnExtendValueModified(subsist,true);
                OnJsonModified(subsist,true);
                OnMemoModified(subsist,true);
                OnParentIdModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[12] > 0)
            {
                OnIdModified(subsist,modifieds[_DataStruct_.Real_Id] == 1);
                OnAppInfoIdModified(subsist,modifieds[_DataStruct_.Real_AppInfoId] == 1);
                OnNameModified(subsist,modifieds[_DataStruct_.Real_Name] == 1);
                OnCaptionModified(subsist,modifieds[_DataStruct_.Real_Caption] == 1);
                OnItemTypeModified(subsist,modifieds[_DataStruct_.Real_ItemType] == 1);
                OnIndexModified(subsist,modifieds[_DataStruct_.Real_Index] == 1);
                OnIconModified(subsist,modifieds[_DataStruct_.Real_Icon] == 1);
                OnUrlModified(subsist,modifieds[_DataStruct_.Real_Url] == 1);
                OnExtendValueModified(subsist,modifieds[_DataStruct_.Real_ExtendValue] == 1);
                OnJsonModified(subsist,modifieds[_DataStruct_.Real_Json] == 1);
                OnMemoModified(subsist,modifieds[_DataStruct_.Real_Memo] == 1);
                OnParentIdModified(subsist,modifieds[_DataStruct_.Real_ParentId] == 1);
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
        /// 应用信息外键修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnAppInfoIdModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 标题修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCaptionModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 节点类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnItemTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 序号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIndexModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 图标修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIconModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 链接地址修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUrlModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 扩展值修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnExtendValueModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 扩展的JSON配置修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnJsonModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 备注修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMemoModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 上级标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnParentIdModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"PageItem";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"页面节点";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"页面节点";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0xF000C;
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
            /// 应用信息外键的数字标识
            /// </summary>
            public const byte AppInfoId = 13;
            
            /// <summary>
            /// 应用信息外键的实时记录顺序
            /// </summary>
            public const int Real_AppInfoId = 1;

            /// <summary>
            /// 名称的数字标识
            /// </summary>
            public const byte Name = 2;
            
            /// <summary>
            /// 名称的实时记录顺序
            /// </summary>
            public const int Real_Name = 2;

            /// <summary>
            /// 标题的数字标识
            /// </summary>
            public const byte Caption = 4;
            
            /// <summary>
            /// 标题的实时记录顺序
            /// </summary>
            public const int Real_Caption = 3;

            /// <summary>
            /// 节点类型的数字标识
            /// </summary>
            public const byte ItemType = 5;
            
            /// <summary>
            /// 节点类型的实时记录顺序
            /// </summary>
            public const int Real_ItemType = 4;

            /// <summary>
            /// 序号的数字标识
            /// </summary>
            public const byte Index = 6;
            
            /// <summary>
            /// 序号的实时记录顺序
            /// </summary>
            public const int Real_Index = 5;

            /// <summary>
            /// 图标的数字标识
            /// </summary>
            public const byte Icon = 7;
            
            /// <summary>
            /// 图标的实时记录顺序
            /// </summary>
            public const int Real_Icon = 6;

            /// <summary>
            /// 链接地址的数字标识
            /// </summary>
            public const byte Url = 8;
            
            /// <summary>
            /// 链接地址的实时记录顺序
            /// </summary>
            public const int Real_Url = 7;

            /// <summary>
            /// 扩展值的数字标识
            /// </summary>
            public const byte ExtendValue = 10;
            
            /// <summary>
            /// 扩展值的实时记录顺序
            /// </summary>
            public const int Real_ExtendValue = 8;

            /// <summary>
            /// 扩展的JSON配置的数字标识
            /// </summary>
            public const byte Json = 11;
            
            /// <summary>
            /// 扩展的JSON配置的实时记录顺序
            /// </summary>
            public const int Real_Json = 9;

            /// <summary>
            /// 备注的数字标识
            /// </summary>
            public const byte Memo = 12;
            
            /// <summary>
            /// 备注的实时记录顺序
            /// </summary>
            public const int Real_Memo = 10;

            /// <summary>
            /// 上级标识的数字标识
            /// </summary>
            public const byte ParentId = 9;
            
            /// <summary>
            /// 上级标识的实时记录顺序
            /// </summary>
            public const int Real_ParentId = 11;

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
                        Real_AppInfoId,
                        new PropertySturct
                        {
                            Index        = AppInfoId,
                            Name         = "AppInfoId",
                            Title        = "应用信息外键",
                            Caption      = @"应用信息外键",
                            Description  = @"应用信息外键",
                            ColumnName   = "app_info_id",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Name,
                        new PropertySturct
                        {
                            Index        = Name,
                            Name         = "Name",
                            Title        = "名称",
                            Caption      = @"名称",
                            Description  = @"名称",
                            ColumnName   = "name",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Caption,
                        new PropertySturct
                        {
                            Index        = Caption,
                            Name         = "Caption",
                            Title        = "标题",
                            Caption      = @"标题",
                            Description  = @"标题",
                            ColumnName   = "caption",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ItemType,
                        new PropertySturct
                        {
                            Index        = ItemType,
                            Name         = "ItemType",
                            Title        = "节点类型",
                            Caption      = @"节点类型",
                            Description  = @"节点类型",
                            ColumnName   = "item_type",
                            PropertyType = typeof(PageItemType),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Index,
                        new PropertySturct
                        {
                            Index        = Index,
                            Name         = "Index",
                            Title        = "序号",
                            Caption      = @"序号",
                            Description  = @"序号",
                            ColumnName   = "index",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Icon,
                        new PropertySturct
                        {
                            Index        = Icon,
                            Name         = "Icon",
                            Title        = "图标",
                            Caption      = @"图标",
                            Description  = @"图标",
                            ColumnName   = "icon",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Url,
                        new PropertySturct
                        {
                            Index        = Url,
                            Name         = "Url",
                            Title        = "链接地址",
                            Caption      = @"链接地址",
                            Description  = @"链接地址",
                            ColumnName   = "url",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ExtendValue,
                        new PropertySturct
                        {
                            Index        = ExtendValue,
                            Name         = "ExtendValue",
                            Title        = "扩展值",
                            Caption      = @"扩展值",
                            Description  = @"扩展值",
                            ColumnName   = "extend_value",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Json,
                        new PropertySturct
                        {
                            Index        = Json,
                            Name         = "Json",
                            Title        = "扩展的JSON配置",
                            Caption      = @"扩展的JSON配置",
                            Description  = @"扩展的JSON配置",
                            ColumnName   = "json",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Memo,
                        new PropertySturct
                        {
                            Index        = Memo,
                            Name         = "Memo",
                            Title        = "备注",
                            Caption      = @"备注",
                            Description  = @"备注",
                            ColumnName   = "memo",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ParentId,
                        new PropertySturct
                        {
                            Index        = ParentId,
                            Name         = "ParentId",
                            Title        = "上级标识",
                            Caption      = @"上级标识",
                            Description  = @"上级标识",
                            ColumnName   = "parent_id",
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