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
    /// 用户订单列表
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class UserOrderListData 
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserOrderListData()
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
        public void ChangePrimaryKey(int lID)
        {
            _lID = lID;
        }
        /// <summary>
        /// 主键
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _lID;

        partial void OnLIDGet();

        partial void OnLIDSet(ref int value);

        partial void OnLIDLoad(ref int value);

        partial void OnLIDSeted();

        
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember , JsonProperty("LID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"主键")]
        public int LID
        {
            get
            {
                OnLIDGet();
                return this._lID;
            }
            set
            {
                if(this._lID == value)
                    return;
                //if(this._lID > 0)
                //    throw new Exception("主键一旦设置就不可以修改");
                OnLIDSet(ref value);
                this._lID = value;
                this.OnPropertyChanged(_DataStruct_.Real_LID);
                OnLIDSeted();
            }
        }
        /// <summary>
        /// 订单标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orderID;

        partial void OnOrderIDGet();

        partial void OnOrderIDSet(ref string value);

        partial void OnOrderIDSeted();

        
        /// <summary>
        /// 订单标识
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrderID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"订单标识")]
        public  string OrderID
        {
            get
            {
                OnOrderIDGet();
                return this._orderID;
            }
            set
            {
                if(this._orderID == value)
                    return;
                OnOrderIDSet(ref value);
                this._orderID = value;
                OnOrderIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrderID);
            }
        }
        /// <summary>
        /// 产品SKU站点标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _productSkuSID;

        partial void OnProductSkuSIDGet();

        partial void OnProductSkuSIDSet(ref int value);

        partial void OnProductSkuSIDSeted();

        
        /// <summary>
        /// 产品SKU站点标识
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ProductSkuSID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"产品SKU站点标识")]
        public  int ProductSkuSID
        {
            get
            {
                OnProductSkuSIDGet();
                return this._productSkuSID;
            }
            set
            {
                if(this._productSkuSID == value)
                    return;
                OnProductSkuSIDSet(ref value);
                this._productSkuSID = value;
                OnProductSkuSIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ProductSkuSID);
            }
        }
        /// <summary>
        /// SKU图像
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _skuImage;

        partial void OnSkuImageGet();

        partial void OnSkuImageSet(ref string value);

        partial void OnSkuImageSeted();

        
        /// <summary>
        /// SKU图像
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SkuImage", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"SKU图像")]
        public  string SkuImage
        {
            get
            {
                OnSkuImageGet();
                return this._skuImage;
            }
            set
            {
                if(this._skuImage == value)
                    return;
                OnSkuImageSet(ref value);
                this._skuImage = value;
                OnSkuImageSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SkuImage);
            }
        }
        /// <summary>
        /// SKU名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _skuName;

        partial void OnSkuNameGet();

        partial void OnSkuNameSet(ref string value);

        partial void OnSkuNameSeted();

        
        /// <summary>
        /// SKU名称
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SkuName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"SKU名称")]
        public  string SkuName
        {
            get
            {
                OnSkuNameGet();
                return this._skuName;
            }
            set
            {
                if(this._skuName == value)
                    return;
                OnSkuNameSet(ref value);
                this._skuName = value;
                OnSkuNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SkuName);
            }
        }
        /// <summary>
        /// SKU计数
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _skuCount;

        partial void OnSkuCountGet();

        partial void OnSkuCountSet(ref int value);

        partial void OnSkuCountSeted();

        
        /// <summary>
        /// SKU计数
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SkuCount", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"SKU计数")]
        public  int SkuCount
        {
            get
            {
                OnSkuCountGet();
                return this._skuCount;
            }
            set
            {
                if(this._skuCount == value)
                    return;
                OnSkuCountSet(ref value);
                this._skuCount = value;
                OnSkuCountSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SkuCount);
            }
        }
        /// <summary>
        /// SKU价格
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _skuPrice;

        partial void OnSkuPriceGet();

        partial void OnSkuPriceSet(ref string value);

        partial void OnSkuPriceSeted();

        
        /// <summary>
        /// SKU价格
        /// </summary>
        /// <value>
        /// 可存储19个字符.合理长度应不大于19.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SkuPrice", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"SKU价格")]
        public  string SkuPrice
        {
            get
            {
                OnSkuPriceGet();
                return this._skuPrice;
            }
            set
            {
                if(this._skuPrice == value)
                    return;
                OnSkuPriceSet(ref value);
                this._skuPrice = value;
                OnSkuPriceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SkuPrice);
            }
        }
        /// <summary>
        /// SKU信息
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _skuInfo;

        partial void OnSkuInfoGet();

        partial void OnSkuInfoSet(ref string value);

        partial void OnSkuInfoSeted();

        
        /// <summary>
        /// SKU信息
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SkuInfo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"SKU信息")]
        public  string SkuInfo
        {
            get
            {
                OnSkuInfoGet();
                return this._skuInfo;
            }
            set
            {
                if(this._skuInfo == value)
                    return;
                OnSkuInfoSet(ref value);
                this._skuInfo = value;
                OnSkuInfoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SkuInfo);
            }
        }
        /// <summary>
        /// 包装重量
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public float _packageWeight;

        partial void OnPackageWeightGet();

        partial void OnPackageWeightSet(ref float value);

        partial void OnPackageWeightSeted();

        
        /// <summary>
        /// 包装重量
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("PackageWeight", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"包装重量")]
        public  float PackageWeight
        {
            get
            {
                OnPackageWeightGet();
                return this._packageWeight;
            }
            set
            {
                if(this._packageWeight == value)
                    return;
                OnPackageWeightSet(ref value);
                this._packageWeight = value;
                OnPackageWeightSeted();
                this.OnPropertyChanged(_DataStruct_.Real_PackageWeight);
            }
        }
        /// <summary>
        /// 包装体积
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public float _packageVolume;

        partial void OnPackageVolumeGet();

        partial void OnPackageVolumeSet(ref float value);

        partial void OnPackageVolumeSeted();

        
        /// <summary>
        /// 包装体积
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("PackageVolume", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"包装体积")]
        public  float PackageVolume
        {
            get
            {
                OnPackageVolumeGet();
                return this._packageVolume;
            }
            set
            {
                if(this._packageVolume == value)
                    return;
                OnPackageVolumeSet(ref value);
                this._packageVolume = value;
                OnPackageVolumeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_PackageVolume);
            }
        }
        /// <summary>
        /// 这个托托
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _thisTotle;

        partial void OnthisTotleGet();

        partial void OnthisTotleSet(ref string value);

        partial void OnthisTotleSeted();

        
        /// <summary>
        /// 这个托托
        /// </summary>
        /// <value>
        /// 可存储19个字符.合理长度应不大于19.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("thisTotle", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"这个托托")]
        public  string thisTotle
        {
            get
            {
                OnthisTotleGet();
                return this._thisTotle;
            }
            set
            {
                if(this._thisTotle == value)
                    return;
                OnthisTotleSet(ref value);
                this._thisTotle = value;
                OnthisTotleSeted();
                this.OnPropertyChanged(_DataStruct_.Real_thisTotle);
            }
        }

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
            case "lid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.LID = vl;
                        return true;
                    }
                }
                return false;
            case "orderid":
                this.OrderID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "productskusid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.ProductSkuSID = vl;
                        return true;
                    }
                }
                return false;
            case "skuimage":
                this.SkuImage = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "skuname":
                this.SkuName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "skucount":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.SkuCount = vl;
                        return true;
                    }
                }
                return false;
            case "skuprice":
                this.SkuPrice = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "skuinfo":
                this.SkuInfo = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "packageweight":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (float.TryParse(value, out var vl))
                    {
                        this.PackageWeight = vl;
                        return true;
                    }
                }
                return false;
            case "packagevolume":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (float.TryParse(value, out var vl))
                    {
                        this.PackageVolume = vl;
                        return true;
                    }
                }
                return false;
            case "thistotle":
                this.thisTotle = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "lid":
                this.LID = (int)Convert.ToDecimal(value);
                return;
            case "orderid":
                this.OrderID = value == null ? null : value.ToString();
                return;
            case "productskusid":
                this.ProductSkuSID = (int)Convert.ToDecimal(value);
                return;
            case "skuimage":
                this.SkuImage = value == null ? null : value.ToString();
                return;
            case "skuname":
                this.SkuName = value == null ? null : value.ToString();
                return;
            case "skucount":
                this.SkuCount = (int)Convert.ToDecimal(value);
                return;
            case "skuprice":
                this.SkuPrice = value == null ? null : value.ToString();
                return;
            case "skuinfo":
                this.SkuInfo = value == null ? null : value.ToString();
                return;
            case "packageweight":
                this.PackageWeight = Convert.ToSingle(value);
                return;
            case "packagevolume":
                this.PackageVolume = Convert.ToSingle(value);
                return;
            case "thistotle":
                this.thisTotle = value == null ? null : value.ToString();
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
            case _DataStruct_.LID:
                this.LID = Convert.ToInt32(value);
                return;
            case _DataStruct_.OrderID:
                this.OrderID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ProductSkuSID:
                this.ProductSkuSID = Convert.ToInt32(value);
                return;
            case _DataStruct_.SkuImage:
                this.SkuImage = value == null ? null : value.ToString();
                return;
            case _DataStruct_.SkuName:
                this.SkuName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.SkuCount:
                this.SkuCount = Convert.ToInt32(value);
                return;
            case _DataStruct_.SkuPrice:
                this.SkuPrice = value == null ? null : value.ToString();
                return;
            case _DataStruct_.SkuInfo:
                this.SkuInfo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.PackageWeight:
                this.PackageWeight = Convert.ToSingle(value);
                return;
            case _DataStruct_.PackageVolume:
                this.PackageVolume = Convert.ToSingle(value);
                return;
            case _DataStruct_.thisTotle:
                this.thisTotle = value == null ? null : value.ToString();
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
            case "lid":
                return this.LID;
            case "orderid":
                return this.OrderID;
            case "productskusid":
                return this.ProductSkuSID;
            case "skuimage":
                return this.SkuImage;
            case "skuname":
                return this.SkuName;
            case "skucount":
                return this.SkuCount;
            case "skuprice":
                return this.SkuPrice;
            case "skuinfo":
                return this.SkuInfo;
            case "packageweight":
                return this.PackageWeight;
            case "packagevolume":
                return this.PackageVolume;
            case "thistotle":
                return this.thisTotle;
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
                case _DataStruct_.LID:
                    return this.LID;
                case _DataStruct_.OrderID:
                    return this.OrderID;
                case _DataStruct_.ProductSkuSID:
                    return this.ProductSkuSID;
                case _DataStruct_.SkuImage:
                    return this.SkuImage;
                case _DataStruct_.SkuName:
                    return this.SkuName;
                case _DataStruct_.SkuCount:
                    return this.SkuCount;
                case _DataStruct_.SkuPrice:
                    return this.SkuPrice;
                case _DataStruct_.SkuInfo:
                    return this.SkuInfo;
                case _DataStruct_.PackageWeight:
                    return this.PackageWeight;
                case _DataStruct_.PackageVolume:
                    return this.PackageVolume;
                case _DataStruct_.thisTotle:
                    return this.thisTotle;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(UserOrderListData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as UserOrderListData;
            if(sourceEntity == null)
                return;
            this._lID = sourceEntity._lID;
            this._orderID = sourceEntity._orderID;
            this._productSkuSID = sourceEntity._productSkuSID;
            this._skuImage = sourceEntity._skuImage;
            this._skuName = sourceEntity._skuName;
            this._skuCount = sourceEntity._skuCount;
            this._skuPrice = sourceEntity._skuPrice;
            this._skuInfo = sourceEntity._skuInfo;
            this._packageWeight = sourceEntity._packageWeight;
            this._packageVolume = sourceEntity._packageVolume;
            this._thisTotle = sourceEntity._thisTotle;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(UserOrderListData source)
        {
                this.LID = source.LID;
                this.OrderID = source.OrderID;
                this.ProductSkuSID = source.ProductSkuSID;
                this.SkuImage = source.SkuImage;
                this.SkuName = source.SkuName;
                this.SkuCount = source.SkuCount;
                this.SkuPrice = source.SkuPrice;
                this.SkuInfo = source.SkuInfo;
                this.PackageWeight = source.PackageWeight;
                this.PackageVolume = source.PackageVolume;
                this.thisTotle = source.thisTotle;
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
                OnLIDModified(subsist,false);
                OnOrderIDModified(subsist,false);
                OnProductSkuSIDModified(subsist,false);
                OnSkuImageModified(subsist,false);
                OnSkuNameModified(subsist,false);
                OnSkuCountModified(subsist,false);
                OnSkuPriceModified(subsist,false);
                OnSkuInfoModified(subsist,false);
                OnPackageWeightModified(subsist,false);
                OnPackageVolumeModified(subsist,false);
                OnthisTotleModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnLIDModified(subsist,true);
                OnOrderIDModified(subsist,true);
                OnProductSkuSIDModified(subsist,true);
                OnSkuImageModified(subsist,true);
                OnSkuNameModified(subsist,true);
                OnSkuCountModified(subsist,true);
                OnSkuPriceModified(subsist,true);
                OnSkuInfoModified(subsist,true);
                OnPackageWeightModified(subsist,true);
                OnPackageVolumeModified(subsist,true);
                OnthisTotleModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[11] > 0)
            {
                OnLIDModified(subsist,modifieds[_DataStruct_.Real_LID] == 1);
                OnOrderIDModified(subsist,modifieds[_DataStruct_.Real_OrderID] == 1);
                OnProductSkuSIDModified(subsist,modifieds[_DataStruct_.Real_ProductSkuSID] == 1);
                OnSkuImageModified(subsist,modifieds[_DataStruct_.Real_SkuImage] == 1);
                OnSkuNameModified(subsist,modifieds[_DataStruct_.Real_SkuName] == 1);
                OnSkuCountModified(subsist,modifieds[_DataStruct_.Real_SkuCount] == 1);
                OnSkuPriceModified(subsist,modifieds[_DataStruct_.Real_SkuPrice] == 1);
                OnSkuInfoModified(subsist,modifieds[_DataStruct_.Real_SkuInfo] == 1);
                OnPackageWeightModified(subsist,modifieds[_DataStruct_.Real_PackageWeight] == 1);
                OnPackageVolumeModified(subsist,modifieds[_DataStruct_.Real_PackageVolume] == 1);
                OnthisTotleModified(subsist,modifieds[_DataStruct_.Real_thisTotle] == 1);
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
        partial void OnLIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 订单标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrderIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 产品SKU站点标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnProductSkuSIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// SKU图像修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSkuImageModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// SKU名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSkuNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// SKU计数修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSkuCountModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// SKU价格修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSkuPriceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// SKU信息修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSkuInfoModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 包装重量修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPackageWeightModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 包装体积修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPackageVolumeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 这个托托修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnthisTotleModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"UserOrderList";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户订单列表";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户订单列表";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "LID";
            
            
            /// <summary>
            /// 主键的数字标识
            /// </summary>
            public const byte LID = 1;
            
            /// <summary>
            /// 主键的实时记录顺序
            /// </summary>
            public const int Real_LID = 0;

            /// <summary>
            /// 订单标识的数字标识
            /// </summary>
            public const byte OrderID = 2;
            
            /// <summary>
            /// 订单标识的实时记录顺序
            /// </summary>
            public const int Real_OrderID = 1;

            /// <summary>
            /// 产品SKU站点标识的数字标识
            /// </summary>
            public const byte ProductSkuSID = 3;
            
            /// <summary>
            /// 产品SKU站点标识的实时记录顺序
            /// </summary>
            public const int Real_ProductSkuSID = 2;

            /// <summary>
            /// SKU图像的数字标识
            /// </summary>
            public const byte SkuImage = 4;
            
            /// <summary>
            /// SKU图像的实时记录顺序
            /// </summary>
            public const int Real_SkuImage = 3;

            /// <summary>
            /// SKU名称的数字标识
            /// </summary>
            public const byte SkuName = 5;
            
            /// <summary>
            /// SKU名称的实时记录顺序
            /// </summary>
            public const int Real_SkuName = 4;

            /// <summary>
            /// SKU计数的数字标识
            /// </summary>
            public const byte SkuCount = 6;
            
            /// <summary>
            /// SKU计数的实时记录顺序
            /// </summary>
            public const int Real_SkuCount = 5;

            /// <summary>
            /// SKU价格的数字标识
            /// </summary>
            public const byte SkuPrice = 7;
            
            /// <summary>
            /// SKU价格的实时记录顺序
            /// </summary>
            public const int Real_SkuPrice = 6;

            /// <summary>
            /// SKU信息的数字标识
            /// </summary>
            public const byte SkuInfo = 8;
            
            /// <summary>
            /// SKU信息的实时记录顺序
            /// </summary>
            public const int Real_SkuInfo = 7;

            /// <summary>
            /// 包装重量的数字标识
            /// </summary>
            public const byte PackageWeight = 9;
            
            /// <summary>
            /// 包装重量的实时记录顺序
            /// </summary>
            public const int Real_PackageWeight = 8;

            /// <summary>
            /// 包装体积的数字标识
            /// </summary>
            public const byte PackageVolume = 10;
            
            /// <summary>
            /// 包装体积的实时记录顺序
            /// </summary>
            public const int Real_PackageVolume = 9;

            /// <summary>
            /// 这个托托的数字标识
            /// </summary>
            public const byte thisTotle = 11;
            
            /// <summary>
            /// 这个托托的实时记录顺序
            /// </summary>
            public const int Real_thisTotle = 10;

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
                        Real_LID,
                        new PropertySturct
                        {
                            Index        = LID,
                            Name         = "LID",
                            Title        = "主键",
                            Caption      = @"主键",
                            Description  = @"主键",
                            ColumnName   = "LID",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrderID,
                        new PropertySturct
                        {
                            Index        = OrderID,
                            Name         = "OrderID",
                            Title        = "订单标识",
                            Caption      = @"订单标识",
                            Description  = @"订单标识",
                            ColumnName   = "OrderID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ProductSkuSID,
                        new PropertySturct
                        {
                            Index        = ProductSkuSID,
                            Name         = "ProductSkuSID",
                            Title        = "产品SKU站点标识",
                            Caption      = @"产品SKU站点标识",
                            Description  = @"产品SKU站点标识",
                            ColumnName   = "ProductSkuSID",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SkuImage,
                        new PropertySturct
                        {
                            Index        = SkuImage,
                            Name         = "SkuImage",
                            Title        = "SKU图像",
                            Caption      = @"SKU图像",
                            Description  = @"SKU图像",
                            ColumnName   = "SkuImage",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SkuName,
                        new PropertySturct
                        {
                            Index        = SkuName,
                            Name         = "SkuName",
                            Title        = "SKU名称",
                            Caption      = @"SKU名称",
                            Description  = @"SKU名称",
                            ColumnName   = "SkuName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SkuCount,
                        new PropertySturct
                        {
                            Index        = SkuCount,
                            Name         = "SkuCount",
                            Title        = "SKU计数",
                            Caption      = @"SKU计数",
                            Description  = @"SKU计数",
                            ColumnName   = "SkuCount",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SkuPrice,
                        new PropertySturct
                        {
                            Index        = SkuPrice,
                            Name         = "SkuPrice",
                            Title        = "SKU价格",
                            Caption      = @"SKU价格",
                            Description  = @"SKU价格",
                            ColumnName   = "SkuPrice",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SkuInfo,
                        new PropertySturct
                        {
                            Index        = SkuInfo,
                            Name         = "SkuInfo",
                            Title        = "SKU信息",
                            Caption      = @"SKU信息",
                            Description  = @"SKU信息",
                            ColumnName   = "SkuInfo",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PackageWeight,
                        new PropertySturct
                        {
                            Index        = PackageWeight,
                            Name         = "PackageWeight",
                            Title        = "包装重量",
                            Caption      = @"包装重量",
                            Description  = @"包装重量",
                            ColumnName   = "PackageWeight",
                            PropertyType = typeof(float),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PackageVolume,
                        new PropertySturct
                        {
                            Index        = PackageVolume,
                            Name         = "PackageVolume",
                            Title        = "包装体积",
                            Caption      = @"包装体积",
                            Description  = @"包装体积",
                            ColumnName   = "PackageVolume",
                            PropertyType = typeof(float),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_thisTotle,
                        new PropertySturct
                        {
                            Index        = thisTotle,
                            Name         = "thisTotle",
                            Title        = "这个托托",
                            Caption      = @"这个托托",
                            Description  = @"这个托托",
                            ColumnName   = "thisTotle",
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