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
    /// 导航表
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class PermitNavigationAllData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public PermitNavigationAllData()
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
        public void ChangePrimaryKey(long nID)
        {
            _nID = nID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _nID;

        partial void OnNIDGet();

        partial void OnNIDSet(ref long value);

        partial void OnNIDLoad(ref long value);

        partial void OnNIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("NID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public long NID
        {
            get
            {
                OnNIDGet();
                return this._nID;
            }
            set
            {
                if(this._nID == value)
                    return;
                //if(this._nID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnNIDSet(ref value);
                this._nID = value;
                this.OnPropertyChanged(_DataStruct_.Real_NID);
                OnNIDSeted();
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
        /// 导航类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _navType;

        partial void OnNavTypeGet();

        partial void OnNavTypeSet(ref string value);

        partial void OnNavTypeSeted();

        
        /// <summary>
        /// 导航类型
        /// </summary>
        /// <value>
        /// 可存储40个字符.合理长度应不大于40.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("NavType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"导航类型")]
        public  string NavType
        {
            get
            {
                OnNavTypeGet();
                return this._navType;
            }
            set
            {
                if(this._navType == value)
                    return;
                OnNavTypeSet(ref value);
                this._navType = value;
                OnNavTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_NavType);
            }
        }
        /// <summary>
        /// 菜单名
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _menuName;

        partial void OnMenuNameGet();

        partial void OnMenuNameSet(ref string value);

        partial void OnMenuNameSeted();

        
        /// <summary>
        /// 菜单名
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MenuName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"菜单名")]
        public  string MenuName
        {
            get
            {
                OnMenuNameGet();
                return this._menuName;
            }
            set
            {
                if(this._menuName == value)
                    return;
                OnMenuNameSet(ref value);
                this._menuName = value;
                OnMenuNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MenuName);
            }
        }
        /// <summary>
        /// 菜单标题
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _menuTitle;

        partial void OnMenuTitleGet();

        partial void OnMenuTitleSet(ref string value);

        partial void OnMenuTitleSeted();

        
        /// <summary>
        /// 菜单标题
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MenuTitle", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"菜单标题")]
        public  string MenuTitle
        {
            get
            {
                OnMenuTitleGet();
                return this._menuTitle;
            }
            set
            {
                if(this._menuTitle == value)
                    return;
                OnMenuTitleSet(ref value);
                this._menuTitle = value;
                OnMenuTitleSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MenuTitle);
            }
        }
        /// <summary>
        /// 菜单链接
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _menuUrl;

        partial void OnMenuUrlGet();

        partial void OnMenuUrlSet(ref string value);

        partial void OnMenuUrlSeted();

        
        /// <summary>
        /// 菜单链接
        /// </summary>
        /// <value>
        /// 可存储200个字符.合理长度应不大于200.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MenuUrl", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"菜单链接")]
        public  string MenuUrl
        {
            get
            {
                OnMenuUrlGet();
                return this._menuUrl;
            }
            set
            {
                if(this._menuUrl == value)
                    return;
                OnMenuUrlSet(ref value);
                this._menuUrl = value;
                OnMenuUrlSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MenuUrl);
            }
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _pID;

        partial void OnPIDGet();

        partial void OnPIDSet(ref long value);

        partial void OnPIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("PID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"主键")]
        public  long PID
        {
            get
            {
                OnPIDGet();
                return this._pID;
            }
            set
            {
                if(this._pID == value)
                    return;
                OnPIDSet(ref value);
                this._pID = value;
                OnPIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_PID);
            }
        }
        /// <summary>
        /// 偶像
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _icon;

        partial void OnIconGet();

        partial void OnIconSet(ref string value);

        partial void OnIconSeted();

        
        /// <summary>
        /// 偶像
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Icon", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"偶像")]
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
        /// 图标大小
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _iconSize;

        partial void OnIconSizeGet();

        partial void OnIconSizeSet(ref int value);

        partial void OnIconSizeSeted();

        
        /// <summary>
        /// 图标大小
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("IconSize", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"图标大小")]
        public  int IconSize
        {
            get
            {
                OnIconSizeGet();
                return this._iconSize;
            }
            set
            {
                if(this._iconSize == value)
                    return;
                OnIconSizeSet(ref value);
                this._iconSize = value;
                OnIconSizeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_IconSize);
            }
        }
        /// <summary>
        /// 图标颜色
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _iconColor;

        partial void OnIconColorGet();

        partial void OnIconColorSet(ref string value);

        partial void OnIconColorSeted();

        
        /// <summary>
        /// 图标颜色
        /// </summary>
        /// <value>
        /// 可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("IconColor", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"图标颜色")]
        public  string IconColor
        {
            get
            {
                OnIconColorGet();
                return this._iconColor;
            }
            set
            {
                if(this._iconColor == value)
                    return;
                OnIconColorSet(ref value);
                this._iconColor = value;
                OnIconColorSeted();
                this.OnPropertyChanged(_DataStruct_.Real_IconColor);
            }
        }
        /// <summary>
        /// 排序
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _sort;

        partial void OnSortGet();

        partial void OnSortSet(ref int value);

        partial void OnSortSeted();

        
        /// <summary>
        /// 排序
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Sort", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"排序")]
        public  int Sort
        {
            get
            {
                OnSortGet();
                return this._sort;
            }
            set
            {
                if(this._sort == value)
                    return;
                OnSortSet(ref value);
                this._sort = value;
                OnSortSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Sort);
            }
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public bool _isShow;

        partial void OnIsShowGet();

        partial void OnIsShowSet(ref bool value);

        partial void OnIsShowSeted();

        
        /// <summary>
        /// 是否显示
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("IsShow", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"是否显示")]
        public  bool IsShow
        {
            get
            {
                OnIsShowGet();
                return this._isShow;
            }
            set
            {
                if(this._isShow == value)
                    return;
                OnIsShowSet(ref value);
                this._isShow = value;
                OnIsShowSeted();
                this.OnPropertyChanged(_DataStruct_.Real_IsShow);
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _remark;

        partial void OnRemarkGet();

        partial void OnRemarkSet(ref string value);

        partial void OnRemarkSeted();

        
        /// <summary>
        /// 备注
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Remark", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"备注")]
        public  string Remark
        {
            get
            {
                OnRemarkGet();
                return this._remark;
            }
            set
            {
                if(this._remark == value)
                    return;
                OnRemarkSet(ref value);
                this._remark = value;
                OnRemarkSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Remark);
            }
        }
        /// <summary>
        /// 等级
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _level;

        partial void OnLevelGet();

        partial void OnLevelSet(ref string value);

        partial void OnLevelSeted();

        
        /// <summary>
        /// 等级
        /// </summary>
        /// <value>
        /// 可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("Level", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"等级")]
        public  string Level
        {
            get
            {
                OnLevelGet();
                return this._level;
            }
            set
            {
                if(this._level == value)
                    return;
                OnLevelSet(ref value);
                this._level = value;
                OnLevelSeted();
                this.OnPropertyChanged(_DataStruct_.Real_Level);
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
                return this.NID;
            }
            set
            {
                this.NID = value;
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
            case "nid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.NID = vl;
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
            case "navtype":
                this.NavType = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "menuname":
                this.MenuName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "menutitle":
                this.MenuTitle = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "menuurl":
                this.MenuUrl = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "pid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.PID = vl;
                        return true;
                    }
                }
                return false;
            case "icon":
                this.Icon = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "iconsize":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.IconSize = vl;
                        return true;
                    }
                }
                return false;
            case "iconcolor":
                this.IconColor = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "sort":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.Sort = vl;
                        return true;
                    }
                }
                return false;
            case "isshow":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (bool.TryParse(value, out var vl))
                    {
                        this.IsShow = vl;
                        return true;
                    }
                }
                return false;
            case "remark":
                this.Remark = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "level":
                this.Level = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "nid":
                this.NID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "navtype":
                this.NavType = value == null ? null : value.ToString();
                return;
            case "menuname":
                this.MenuName = value == null ? null : value.ToString();
                return;
            case "menutitle":
                this.MenuTitle = value == null ? null : value.ToString();
                return;
            case "menuurl":
                this.MenuUrl = value == null ? null : value.ToString();
                return;
            case "pid":
                this.PID = (long)Convert.ToDecimal(value);
                return;
            case "icon":
                this.Icon = value == null ? null : value.ToString();
                return;
            case "iconsize":
                this.IconSize = (int)Convert.ToDecimal(value);
                return;
            case "iconcolor":
                this.IconColor = value == null ? null : value.ToString();
                return;
            case "sort":
                this.Sort = (int)Convert.ToDecimal(value);
                return;
            case "isshow":
                if (value != null)
                {
                    int vl;
                    if (int.TryParse(value.ToString(), out vl))
                    {
                        this.IsShow = vl != 0;
                    }
                    else
                    {
                        this.IsShow = Convert.ToBoolean(value);
                    }
                }
                return;
            case "remark":
                this.Remark = value == null ? null : value.ToString();
                return;
            case "level":
                this.Level = value == null ? null : value.ToString();
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
            case _DataStruct_.NID:
                this.NID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.NavType:
                this.NavType = value == null ? null : value.ToString();
                return;
            case _DataStruct_.MenuName:
                this.MenuName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.MenuTitle:
                this.MenuTitle = value == null ? null : value.ToString();
                return;
            case _DataStruct_.MenuUrl:
                this.MenuUrl = value == null ? null : value.ToString();
                return;
            case _DataStruct_.PID:
                this.PID = Convert.ToInt64(value);
                return;
            case _DataStruct_.Icon:
                this.Icon = value == null ? null : value.ToString();
                return;
            case _DataStruct_.IconSize:
                this.IconSize = Convert.ToInt32(value);
                return;
            case _DataStruct_.IconColor:
                this.IconColor = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Sort:
                this.Sort = Convert.ToInt32(value);
                return;
            case _DataStruct_.IsShow:
                this.IsShow = Convert.ToBoolean(value);
                return;
            case _DataStruct_.Remark:
                this.Remark = value == null ? null : value.ToString();
                return;
            case _DataStruct_.Level:
                this.Level = value == null ? null : value.ToString();
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
            case "nid":
                return this.NID;
            case "sitesid":
                return this.SiteSID;
            case "navtype":
                return this.NavType;
            case "menuname":
                return this.MenuName;
            case "menutitle":
                return this.MenuTitle;
            case "menuurl":
                return this.MenuUrl;
            case "pid":
                return this.PID;
            case "icon":
                return this.Icon;
            case "iconsize":
                return this.IconSize;
            case "iconcolor":
                return this.IconColor;
            case "sort":
                return this.Sort;
            case "isshow":
                return this.IsShow;
            case "remark":
                return this.Remark;
            case "level":
                return this.Level;
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
                case _DataStruct_.NID:
                    return this.NID;
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.NavType:
                    return this.NavType;
                case _DataStruct_.MenuName:
                    return this.MenuName;
                case _DataStruct_.MenuTitle:
                    return this.MenuTitle;
                case _DataStruct_.MenuUrl:
                    return this.MenuUrl;
                case _DataStruct_.PID:
                    return this.PID;
                case _DataStruct_.Icon:
                    return this.Icon;
                case _DataStruct_.IconSize:
                    return this.IconSize;
                case _DataStruct_.IconColor:
                    return this.IconColor;
                case _DataStruct_.Sort:
                    return this.Sort;
                case _DataStruct_.IsShow:
                    return this.IsShow;
                case _DataStruct_.Remark:
                    return this.Remark;
                case _DataStruct_.Level:
                    return this.Level;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(PermitNavigationAllData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as PermitNavigationAllData;
            if(sourceEntity == null)
                return;
            this._nID = sourceEntity._nID;
            this._siteSID = sourceEntity._siteSID;
            this._navType = sourceEntity._navType;
            this._menuName = sourceEntity._menuName;
            this._menuTitle = sourceEntity._menuTitle;
            this._menuUrl = sourceEntity._menuUrl;
            this._pID = sourceEntity._pID;
            this._icon = sourceEntity._icon;
            this._iconSize = sourceEntity._iconSize;
            this._iconColor = sourceEntity._iconColor;
            this._sort = sourceEntity._sort;
            this._isShow = sourceEntity._isShow;
            this._remark = sourceEntity._remark;
            this._level = sourceEntity._level;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(PermitNavigationAllData source)
        {
                this.NID = source.NID;
                this.SiteSID = source.SiteSID;
                this.NavType = source.NavType;
                this.MenuName = source.MenuName;
                this.MenuTitle = source.MenuTitle;
                this.MenuUrl = source.MenuUrl;
                this.PID = source.PID;
                this.Icon = source.Icon;
                this.IconSize = source.IconSize;
                this.IconColor = source.IconColor;
                this.Sort = source.Sort;
                this.IsShow = source.IsShow;
                this.Remark = source.Remark;
                this.Level = source.Level;
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
                OnNIDModified(subsist,false);
                OnSiteSIDModified(subsist,false);
                OnNavTypeModified(subsist,false);
                OnMenuNameModified(subsist,false);
                OnMenuTitleModified(subsist,false);
                OnMenuUrlModified(subsist,false);
                OnPIDModified(subsist,false);
                OnIconModified(subsist,false);
                OnIconSizeModified(subsist,false);
                OnIconColorModified(subsist,false);
                OnSortModified(subsist,false);
                OnIsShowModified(subsist,false);
                OnRemarkModified(subsist,false);
                OnLevelModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnNIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnNavTypeModified(subsist,true);
                OnMenuNameModified(subsist,true);
                OnMenuTitleModified(subsist,true);
                OnMenuUrlModified(subsist,true);
                OnPIDModified(subsist,true);
                OnIconModified(subsist,true);
                OnIconSizeModified(subsist,true);
                OnIconColorModified(subsist,true);
                OnSortModified(subsist,true);
                OnIsShowModified(subsist,true);
                OnRemarkModified(subsist,true);
                OnLevelModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[14] > 0)
            {
                OnNIDModified(subsist,modifieds[_DataStruct_.Real_NID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnNavTypeModified(subsist,modifieds[_DataStruct_.Real_NavType] == 1);
                OnMenuNameModified(subsist,modifieds[_DataStruct_.Real_MenuName] == 1);
                OnMenuTitleModified(subsist,modifieds[_DataStruct_.Real_MenuTitle] == 1);
                OnMenuUrlModified(subsist,modifieds[_DataStruct_.Real_MenuUrl] == 1);
                OnPIDModified(subsist,modifieds[_DataStruct_.Real_PID] == 1);
                OnIconModified(subsist,modifieds[_DataStruct_.Real_Icon] == 1);
                OnIconSizeModified(subsist,modifieds[_DataStruct_.Real_IconSize] == 1);
                OnIconColorModified(subsist,modifieds[_DataStruct_.Real_IconColor] == 1);
                OnSortModified(subsist,modifieds[_DataStruct_.Real_Sort] == 1);
                OnIsShowModified(subsist,modifieds[_DataStruct_.Real_IsShow] == 1);
                OnRemarkModified(subsist,modifieds[_DataStruct_.Real_Remark] == 1);
                OnLevelModified(subsist,modifieds[_DataStruct_.Real_Level] == 1);
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
        partial void OnNIDModified(EntitySubsist subsist,bool isModified);

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
        /// 导航类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnNavTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 菜单名修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMenuNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 菜单标题修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMenuTitleModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 菜单链接修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMenuUrlModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 主键修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 偶像修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIconModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 图标大小修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIconSizeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 图标颜色修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIconColorModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 排序修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSortModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 是否显示修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIsShowModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 备注修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnRemarkModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 等级修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnLevelModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"PermitNavigationAll";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"导航表";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"导航表";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "NID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte NID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_NID = 0;

            /// <summary>
            /// 站点标识的数字标识
            /// </summary>
            public const byte SiteSID = 2;
            
            /// <summary>
            /// 站点标识的实时记录顺序
            /// </summary>
            public const int Real_SiteSID = 1;

            /// <summary>
            /// 导航类型的数字标识
            /// </summary>
            public const byte NavType = 3;
            
            /// <summary>
            /// 导航类型的实时记录顺序
            /// </summary>
            public const int Real_NavType = 2;

            /// <summary>
            /// 菜单名的数字标识
            /// </summary>
            public const byte MenuName = 4;
            
            /// <summary>
            /// 菜单名的实时记录顺序
            /// </summary>
            public const int Real_MenuName = 3;

            /// <summary>
            /// 菜单标题的数字标识
            /// </summary>
            public const byte MenuTitle = 5;
            
            /// <summary>
            /// 菜单标题的实时记录顺序
            /// </summary>
            public const int Real_MenuTitle = 4;

            /// <summary>
            /// 菜单链接的数字标识
            /// </summary>
            public const byte MenuUrl = 6;
            
            /// <summary>
            /// 菜单链接的实时记录顺序
            /// </summary>
            public const int Real_MenuUrl = 5;

            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte PID = 7;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_PID = 6;

            /// <summary>
            /// 偶像的数字标识
            /// </summary>
            public const byte Icon = 8;
            
            /// <summary>
            /// 偶像的实时记录顺序
            /// </summary>
            public const int Real_Icon = 7;

            /// <summary>
            /// 图标大小的数字标识
            /// </summary>
            public const byte IconSize = 9;
            
            /// <summary>
            /// 图标大小的实时记录顺序
            /// </summary>
            public const int Real_IconSize = 8;

            /// <summary>
            /// 图标颜色的数字标识
            /// </summary>
            public const byte IconColor = 10;
            
            /// <summary>
            /// 图标颜色的实时记录顺序
            /// </summary>
            public const int Real_IconColor = 9;

            /// <summary>
            /// 排序的数字标识
            /// </summary>
            public const byte Sort = 11;
            
            /// <summary>
            /// 排序的实时记录顺序
            /// </summary>
            public const int Real_Sort = 10;

            /// <summary>
            /// 是否显示的数字标识
            /// </summary>
            public const byte IsShow = 12;
            
            /// <summary>
            /// 是否显示的实时记录顺序
            /// </summary>
            public const int Real_IsShow = 11;

            /// <summary>
            /// 备注的数字标识
            /// </summary>
            public const byte Remark = 13;
            
            /// <summary>
            /// 备注的实时记录顺序
            /// </summary>
            public const int Real_Remark = 12;

            /// <summary>
            /// 等级的数字标识
            /// </summary>
            public const byte Level = 14;
            
            /// <summary>
            /// 等级的实时记录顺序
            /// </summary>
            public const int Real_Level = 13;

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
                        Real_NID,
                        new PropertySturct
                        {
                            Index        = NID,
                            Name         = "NID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "NID",
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
                        Real_NavType,
                        new PropertySturct
                        {
                            Index        = NavType,
                            Name         = "NavType",
                            Title        = "导航类型",
                            Caption      = @"导航类型",
                            Description  = @"导航类型",
                            ColumnName   = "NavType",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MenuName,
                        new PropertySturct
                        {
                            Index        = MenuName,
                            Name         = "MenuName",
                            Title        = "菜单名",
                            Caption      = @"菜单名",
                            Description  = @"菜单名",
                            ColumnName   = "MenuName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MenuTitle,
                        new PropertySturct
                        {
                            Index        = MenuTitle,
                            Name         = "MenuTitle",
                            Title        = "菜单标题",
                            Caption      = @"菜单标题",
                            Description  = @"菜单标题",
                            ColumnName   = "MenuTitle",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MenuUrl,
                        new PropertySturct
                        {
                            Index        = MenuUrl,
                            Name         = "MenuUrl",
                            Title        = "菜单链接",
                            Caption      = @"菜单链接",
                            Description  = @"菜单链接",
                            ColumnName   = "MenuUrl",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PID,
                        new PropertySturct
                        {
                            Index        = PID,
                            Name         = "PID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "PID",
                            PropertyType = typeof(long),
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
                            Title        = "偶像",
                            Caption      = @"偶像",
                            Description  = @"偶像",
                            ColumnName   = "Icon",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_IconSize,
                        new PropertySturct
                        {
                            Index        = IconSize,
                            Name         = "IconSize",
                            Title        = "图标大小",
                            Caption      = @"图标大小",
                            Description  = @"图标大小",
                            ColumnName   = "IconSize",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_IconColor,
                        new PropertySturct
                        {
                            Index        = IconColor,
                            Name         = "IconColor",
                            Title        = "图标颜色",
                            Caption      = @"图标颜色",
                            Description  = @"图标颜色",
                            ColumnName   = "IconColor",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Sort,
                        new PropertySturct
                        {
                            Index        = Sort,
                            Name         = "Sort",
                            Title        = "排序",
                            Caption      = @"排序",
                            Description  = @"排序",
                            ColumnName   = "Sort",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_IsShow,
                        new PropertySturct
                        {
                            Index        = IsShow,
                            Name         = "IsShow",
                            Title        = "是否显示",
                            Caption      = @"是否显示",
                            Description  = @"是否显示",
                            ColumnName   = "IsShow",
                            PropertyType = typeof(bool),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Remark,
                        new PropertySturct
                        {
                            Index        = Remark,
                            Name         = "Remark",
                            Title        = "备注",
                            Caption      = @"备注",
                            Description  = @"备注",
                            ColumnName   = "Remark",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_Level,
                        new PropertySturct
                        {
                            Index        = Level,
                            Name         = "Level",
                            Title        = "等级",
                            Caption      = @"等级",
                            Description  = @"等级",
                            ColumnName   = "Level",
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