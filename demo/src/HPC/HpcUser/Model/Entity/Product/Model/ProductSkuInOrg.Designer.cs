/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/6/20 11:32:55*/
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
    /// 组织SKU
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class ProductSkuInOrgData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public ProductSkuInOrgData()
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
        /// 编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _sID;

        partial void OnSIDGet();

        partial void OnSIDSet(ref long value);

        partial void OnSIDLoad(ref long value);

        partial void OnSIDSeted();

        
        /// <summary>
        ///  编号
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        [DataMember , JsonProperty("SID", NullValueHandling = NullValueHandling.Ignore) , ReadOnly(true) , DisplayName(@"编号")]
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
        /// 站点编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _siteSID;

        partial void OnSiteSIDGet();

        partial void OnSiteSIDSet(ref long value);

        partial void OnSiteSIDSeted();

        
        /// <summary>
        ///  站点编号
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SiteSID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"站点编号")]
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
                this.OnPropertyChanged(nameof(SiteSID));
            }
        }
        /// <summary>
        /// 组织编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _orgOID;

        partial void OnOrgOIDGet();

        partial void OnOrgOIDSet(ref long value);

        partial void OnOrgOIDSeted();

        
        /// <summary>
        ///  组织编号
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrgOID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"组织编号")]
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
                this.OnPropertyChanged(nameof(OrgOID));
            }
        }
        /// <summary>
        /// 供应商编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _supplyOIDCurrent;

        partial void OnSupplyOIDCurrentGet();

        partial void OnSupplyOIDCurrentSet(ref long value);

        partial void OnSupplyOIDCurrentSeted();

        
        /// <summary>
        ///  供应商编号
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        [DataMember , JsonProperty("SupplyOIDCurrent", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"供应商编号")]
        public  long SupplyOIDCurrent
        {
            get
            {
                OnSupplyOIDCurrentGet();
                return this._supplyOIDCurrent;
            }
            set
            {
                if(this._supplyOIDCurrent == value)
                    return;
                OnSupplyOIDCurrentSet(ref value);
                this._supplyOIDCurrent = value;
                OnSupplyOIDCurrentSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SupplyOIDCurrent);
                this.OnPropertyChanged(nameof(SupplyOIDCurrent));
            }
        }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _supplyName;

        partial void OnSupplyNameGet();

        partial void OnSupplyNameSet(ref string value);

        partial void OnSupplyNameSeted();

        
        /// <summary>
        ///  供应商名称
        ///  -- 此字段不存储在数据库中
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        /// <value>
        ///     可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SupplyName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"供应商名称")]
        public  string SupplyName
        {
            get
            {
                OnSupplyNameGet();
                return this._supplyName;
            }
            set
            {
                if(this._supplyName == value)
                    return;
                OnSupplyNameSet(ref value);
                this._supplyName = value;
                OnSupplyNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SupplyName);
                this.OnPropertyChanged(nameof(SupplyName));
            }
        }
        /// <summary>
        /// Sku编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _skuSID;

        partial void OnSkuSIDGet();

        partial void OnSkuSIDSet(ref long value);

        partial void OnSkuSIDSeted();

        
        /// <summary>
        ///  Sku编号
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SkuSID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"Sku编号")]
        public  long SkuSID
        {
            get
            {
                OnSkuSIDGet();
                return this._skuSID;
            }
            set
            {
                if(this._skuSID == value)
                    return;
                OnSkuSIDSet(ref value);
                this._skuSID = value;
                OnSkuSIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SkuSID);
                this.OnPropertyChanged(nameof(SkuSID));
            }
        }
        /// <summary>
        /// 条形码
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _barCode;

        partial void OnBarCodeGet();

        partial void OnBarCodeSet(ref string value);

        partial void OnBarCodeSeted();

        
        /// <summary>
        ///  条形码
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        /// <value>
        ///     用户提交时不能为空,后台保存时不能为空,可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataMember , JsonProperty("BarCode", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"条形码")]
        public  string BarCode
        {
            get
            {
                OnBarCodeGet();
                return this._barCode;
            }
            set
            {
                if(this._barCode == value)
                    return;
                OnBarCodeSet(ref value);
                this._barCode = value;
                OnBarCodeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_BarCode);
                this.OnPropertyChanged(nameof(BarCode));
            }
        }
        /// <summary>
        /// 成本价
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public decimal _costPrice;

        partial void OnCostPriceGet();

        partial void OnCostPriceSet(ref decimal value);

        partial void OnCostPriceSeted();

        
        /// <summary>
        ///  成本价
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        /// <value>
        ///     合理值不应小于0.
        /// </value>
        [DataRule(Min = 0)]
        [DataMember , JsonProperty("CostPrice", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"成本价")]
        public  decimal CostPrice
        {
            get
            {
                OnCostPriceGet();
                return this._costPrice;
            }
            set
            {
                if(this._costPrice == value)
                    return;
                OnCostPriceSet(ref value);
                this._costPrice = value;
                OnCostPriceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_CostPrice);
                this.OnPropertyChanged(nameof(CostPrice));
            }
        }
        /// <summary>
        /// 成本类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _costType;

        partial void OnCostTypeGet();

        partial void OnCostTypeSet(ref string value);

        partial void OnCostTypeSeted();

        
        /// <summary>
        ///  成本类型
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        /// <value>
        ///     用户提交时不能为空,后台保存时不能为空,可存储20个字符.合理长度应不大于20.
        /// </value>
        [DataMember , JsonProperty("CostType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"成本类型")]
        public  string CostType
        {
            get
            {
                OnCostTypeGet();
                return this._costType;
            }
            set
            {
                if(this._costType == value)
                    return;
                OnCostTypeSet(ref value);
                this._costType = value;
                OnCostTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_CostType);
                this.OnPropertyChanged(nameof(CostType));
            }
        }
        /// <summary>
        /// 销售扣除
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public decimal _salePriceDeduct;

        partial void OnSalePriceDeductGet();

        partial void OnSalePriceDeductSet(ref decimal value);

        partial void OnSalePriceDeductSeted();

        
        /// <summary>
        ///  销售扣除
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        /// <value>
        ///     合理值不应小于0.
        /// </value>
        [DataRule(CanNull = true,Min = 0)]
        [DataMember , JsonProperty("SalePriceDeduct", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"销售扣除")]
        public  decimal SalePriceDeduct
        {
            get
            {
                OnSalePriceDeductGet();
                return this._salePriceDeduct;
            }
            set
            {
                if(this._salePriceDeduct == value)
                    return;
                OnSalePriceDeductSet(ref value);
                this._salePriceDeduct = value;
                OnSalePriceDeductSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SalePriceDeduct);
                this.OnPropertyChanged(nameof(SalePriceDeduct));
            }
        }
        /// <summary>
        /// 销售扣点
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public float _salePricePercent;

        partial void OnSalePricePercentGet();

        partial void OnSalePricePercentSet(ref float value);

        partial void OnSalePricePercentSeted();

        
        /// <summary>
        ///  销售扣点
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        /// <value>
        ///     合理值(大等于)0且(小于)1.
        /// </value>
        [DataRule(CanNull = true,Min = 0,Max = 1)]
        [DataMember , JsonProperty("SalePricePercent", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"销售扣点")]
        public  float SalePricePercent
        {
            get
            {
                OnSalePricePercentGet();
                return this._salePricePercent;
            }
            set
            {
                if(this._salePricePercent == value)
                    return;
                OnSalePricePercentSet(ref value);
                this._salePricePercent = value;
                OnSalePricePercentSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SalePricePercent);
                this.OnPropertyChanged(nameof(SalePricePercent));
            }
        }
        /// <summary>
        /// 采购类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _purchaseType;

        partial void OnPurchaseTypeGet();

        partial void OnPurchaseTypeSet(ref string value);

        partial void OnPurchaseTypeSeted();

        
        /// <summary>
        ///  采购类型
        /// </summary>
        /// <value>
        ///     可存储10个字符.合理长度应不大于10.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("PurchaseType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"采购类型")]
        public  string PurchaseType
        {
            get
            {
                OnPurchaseTypeGet();
                return this._purchaseType;
            }
            set
            {
                if(this._purchaseType == value)
                    return;
                OnPurchaseTypeSet(ref value);
                this._purchaseType = value;
                OnPurchaseTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_PurchaseType);
                this.OnPropertyChanged(nameof(PurchaseType));
            }
        }
        /// <summary>
        /// 虚拟仓储
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _storageVirtual;

        partial void OnStorageVirtualGet();

        partial void OnStorageVirtualSet(ref int value);

        partial void OnStorageVirtualSeted();

        
        /// <summary>
        ///  虚拟仓储
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("StorageVirtual", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"虚拟仓储")]
        public  int StorageVirtual
        {
            get
            {
                OnStorageVirtualGet();
                return this._storageVirtual;
            }
            set
            {
                if(this._storageVirtual == value)
                    return;
                OnStorageVirtualSet(ref value);
                this._storageVirtual = value;
                OnStorageVirtualSeted();
                this.OnPropertyChanged(_DataStruct_.Real_StorageVirtual);
                this.OnPropertyChanged(nameof(StorageVirtual));
            }
        }
        /// <summary>
        /// 线上原价
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public decimal _onlineOldPrice;

        partial void OnOnlineOldPriceGet();

        partial void OnOnlineOldPriceSet(ref decimal value);

        partial void OnOnlineOldPriceSeted();

        
        /// <summary>
        ///  线上原价
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        /// <value>
        ///     合理值不应小于0.
        /// </value>
        [DataRule(CanNull = true,Min = 0)]
        [DataMember , JsonProperty("OnlineOldPrice", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"线上原价")]
        public  decimal OnlineOldPrice
        {
            get
            {
                OnOnlineOldPriceGet();
                return this._onlineOldPrice;
            }
            set
            {
                if(this._onlineOldPrice == value)
                    return;
                OnOnlineOldPriceSet(ref value);
                this._onlineOldPrice = value;
                OnOnlineOldPriceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OnlineOldPrice);
                this.OnPropertyChanged(nameof(OnlineOldPrice));
            }
        }
        /// <summary>
        /// 线上现价
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public decimal _onlineNewPrice;

        partial void OnOnlineNewPriceGet();

        partial void OnOnlineNewPriceSet(ref decimal value);

        partial void OnOnlineNewPriceSeted();

        
        /// <summary>
        ///  线上现价
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        /// <value>
        ///     合理值不应小于0.
        /// </value>
        [DataRule(CanNull = true,Min = 0)]
        [DataMember , JsonProperty("OnlineNewPrice", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"线上现价")]
        public  decimal OnlineNewPrice
        {
            get
            {
                OnOnlineNewPriceGet();
                return this._onlineNewPrice;
            }
            set
            {
                if(this._onlineNewPrice == value)
                    return;
                OnOnlineNewPriceSet(ref value);
                this._onlineNewPrice = value;
                OnOnlineNewPriceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OnlineNewPrice);
                this.OnPropertyChanged(nameof(OnlineNewPrice));
            }
        }
        /// <summary>
        /// 线下原价
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public decimal _offlineOldPrice;

        partial void OnOfflineOldPriceGet();

        partial void OnOfflineOldPriceSet(ref decimal value);

        partial void OnOfflineOldPriceSeted();

        
        /// <summary>
        ///  线下原价
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        /// <value>
        ///     合理值不应小于0.
        /// </value>
        [DataRule(CanNull = true,Min = 0)]
        [DataMember , JsonProperty("OfflineOldPrice", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"线下原价")]
        public  decimal OfflineOldPrice
        {
            get
            {
                OnOfflineOldPriceGet();
                return this._offlineOldPrice;
            }
            set
            {
                if(this._offlineOldPrice == value)
                    return;
                OnOfflineOldPriceSet(ref value);
                this._offlineOldPrice = value;
                OnOfflineOldPriceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OfflineOldPrice);
                this.OnPropertyChanged(nameof(OfflineOldPrice));
            }
        }
        /// <summary>
        /// 线下现价
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public decimal _offlineNewPrice;

        partial void OnOfflineNewPriceGet();

        partial void OnOfflineNewPriceSet(ref decimal value);

        partial void OnOfflineNewPriceSeted();

        
        /// <summary>
        ///  线下现价
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        /// <value>
        ///     合理值不应小于0.
        /// </value>
        [DataRule(CanNull = true,Min = 0)]
        [DataMember , JsonProperty("OfflineNewPrice", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"线下现价")]
        public  decimal OfflineNewPrice
        {
            get
            {
                OnOfflineNewPriceGet();
                return this._offlineNewPrice;
            }
            set
            {
                if(this._offlineNewPrice == value)
                    return;
                OnOfflineNewPriceSet(ref value);
                this._offlineNewPrice = value;
                OnOfflineNewPriceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OfflineNewPrice);
                this.OnPropertyChanged(nameof(OfflineNewPrice));
            }
        }
        /// <summary>
        /// 税前价
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public decimal _basePriceNoTax;

        partial void OnBasePriceNoTaxGet();

        partial void OnBasePriceNoTaxSet(ref decimal value);

        partial void OnBasePriceNoTaxSeted();

        
        /// <summary>
        ///  税前价
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        /// <value>
        ///     合理值不应小于0.
        /// </value>
        [DataRule(CanNull = true,Min = 0)]
        [DataMember , JsonProperty("BasePriceNoTax", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"税前价")]
        public  decimal BasePriceNoTax
        {
            get
            {
                OnBasePriceNoTaxGet();
                return this._basePriceNoTax;
            }
            set
            {
                if(this._basePriceNoTax == value)
                    return;
                OnBasePriceNoTaxSet(ref value);
                this._basePriceNoTax = value;
                OnBasePriceNoTaxSeted();
                this.OnPropertyChanged(_DataStruct_.Real_BasePriceNoTax);
                this.OnPropertyChanged(nameof(BasePriceNoTax));
            }
        }
        /// <summary>
        /// 含税价
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public decimal _basePriceHasTax;

        partial void OnBasePriceHasTaxGet();

        partial void OnBasePriceHasTaxSet(ref decimal value);

        partial void OnBasePriceHasTaxSeted();

        
        /// <summary>
        ///  含税价
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        /// <value>
        ///     合理值不应小于0.
        /// </value>
        [DataRule(CanNull = true,Min = 0)]
        [DataMember , JsonProperty("BasePriceHasTax", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"含税价")]
        public  decimal BasePriceHasTax
        {
            get
            {
                OnBasePriceHasTaxGet();
                return this._basePriceHasTax;
            }
            set
            {
                if(this._basePriceHasTax == value)
                    return;
                OnBasePriceHasTaxSet(ref value);
                this._basePriceHasTax = value;
                OnBasePriceHasTaxSeted();
                this.OnPropertyChanged(_DataStruct_.Real_BasePriceHasTax);
                this.OnPropertyChanged(nameof(BasePriceHasTax));
            }
        }
        /// <summary>
        /// 批次编号
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _batchBID;

        partial void OnBatchBIDGet();

        partial void OnBatchBIDSet(ref long value);

        partial void OnBatchBIDSeted();

        
        /// <summary>
        ///  批次编号
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("BatchBID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"批次编号")]
        public  long BatchBID
        {
            get
            {
                OnBatchBIDGet();
                return this._batchBID;
            }
            set
            {
                if(this._batchBID == value)
                    return;
                OnBatchBIDSet(ref value);
                this._batchBID = value;
                OnBatchBIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_BatchBID);
                this.OnPropertyChanged(nameof(BatchBID));
            }
        }
        /// <summary>
        /// 订单标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public long _orderB2BOID;

        partial void OnOrderB2BOIDGet();

        partial void OnOrderB2BOIDSet(ref long value);

        partial void OnOrderB2BOIDSeted();

        
        /// <summary>
        ///  订单标识
        /// </summary>
        /// <example>
        ///     0
        /// </example>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrderB2BOID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"订单标识")]
        public  long OrderB2BOID
        {
            get
            {
                OnOrderB2BOIDGet();
                return this._orderB2BOID;
            }
            set
            {
                if(this._orderB2BOID == value)
                    return;
                OnOrderB2BOIDSet(ref value);
                this._orderB2BOID = value;
                OnOrderB2BOIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrderB2BOID);
                this.OnPropertyChanged(nameof(OrderB2BOID));
            }
        }
        /// <summary>
        /// 导入标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _excelImportID;

        partial void OnExcelImportIDGet();

        partial void OnExcelImportIDSet(ref string value);

        partial void OnExcelImportIDSeted();

        
        /// <summary>
        ///  导入标识
        /// </summary>
        /// <remarks>
        ///     Excel导入标识
        /// </remarks>
        /// <example>
        ///     0
        /// </example>
        /// <value>
        ///     可存储40个字符.合理长度应不大于40.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ExcelImportID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"导入标识")]
        public  string ExcelImportID
        {
            get
            {
                OnExcelImportIDGet();
                return this._excelImportID;
            }
            set
            {
                if(this._excelImportID == value)
                    return;
                OnExcelImportIDSet(ref value);
                this._excelImportID = value;
                OnExcelImportIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ExcelImportID);
                this.OnPropertyChanged(nameof(ExcelImportID));
            }
        }
        /// <summary>
        /// 商品会员类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _memberAttribute;

        partial void OnMemberAttributeGet();

        partial void OnMemberAttributeSet(ref string value);

        partial void OnMemberAttributeSeted();

        
        /// <summary>
        ///  商品会员类型
        /// </summary>
        /// <value>
        ///     可存储40个字符.合理长度应不大于40.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("MemberAttribute", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"商品会员类型")]
        public  string MemberAttribute
        {
            get
            {
                OnMemberAttributeGet();
                return this._memberAttribute;
            }
            set
            {
                if(this._memberAttribute == value)
                    return;
                OnMemberAttributeSet(ref value);
                this._memberAttribute = value;
                OnMemberAttributeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_MemberAttribute);
                this.OnPropertyChanged(nameof(MemberAttribute));
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
            case "supplyoidcurrent":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.SupplyOIDCurrent = vl;
                        return true;
                    }
                }
                return false;
            case "supplyname":
                this.SupplyName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "skusid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.SkuSID = vl;
                        return true;
                    }
                }
                return false;
            case "barcode":
                this.BarCode = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "costprice":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (decimal.TryParse(value, out var vl))
                    {
                        this.CostPrice = vl;
                        return true;
                    }
                }
                return false;
            case "costtype":
                this.CostType = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "salepricededuct":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (decimal.TryParse(value, out var vl))
                    {
                        this.SalePriceDeduct = vl;
                        return true;
                    }
                }
                return false;
            case "salepricepercent":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (float.TryParse(value, out var vl))
                    {
                        this.SalePricePercent = vl;
                        return true;
                    }
                }
                return false;
            case "purchasetype":
                this.PurchaseType = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "storagevirtual":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.StorageVirtual = vl;
                        return true;
                    }
                }
                return false;
            case "onlineoldprice":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (decimal.TryParse(value, out var vl))
                    {
                        this.OnlineOldPrice = vl;
                        return true;
                    }
                }
                return false;
            case "onlinenewprice":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (decimal.TryParse(value, out var vl))
                    {
                        this.OnlineNewPrice = vl;
                        return true;
                    }
                }
                return false;
            case "offlineoldprice":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (decimal.TryParse(value, out var vl))
                    {
                        this.OfflineOldPrice = vl;
                        return true;
                    }
                }
                return false;
            case "offlinenewprice":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (decimal.TryParse(value, out var vl))
                    {
                        this.OfflineNewPrice = vl;
                        return true;
                    }
                }
                return false;
            case "basepricenotax":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (decimal.TryParse(value, out var vl))
                    {
                        this.BasePriceNoTax = vl;
                        return true;
                    }
                }
                return false;
            case "basepricehastax":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (decimal.TryParse(value, out var vl))
                    {
                        this.BasePriceHasTax = vl;
                        return true;
                    }
                }
                return false;
            case "batchbid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.BatchBID = vl;
                        return true;
                    }
                }
                return false;
            case "orderb2boid":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (long.TryParse(value, out var vl))
                    {
                        this.OrderB2BOID = vl;
                        return true;
                    }
                }
                return false;
            case "excelimportid":
                this.ExcelImportID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "memberattribute":
                this.MemberAttribute = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "sid":
                this.SID = (long)Convert.ToDecimal(value);
                return;
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "orgoid":
                this.OrgOID = (long)Convert.ToDecimal(value);
                return;
            case "supplyoidcurrent":
                this.SupplyOIDCurrent = (long)Convert.ToDecimal(value);
                return;
            case "supplyname":
                this.SupplyName = value == null ? null : value.ToString();
                return;
            case "skusid":
                this.SkuSID = (long)Convert.ToDecimal(value);
                return;
            case "barcode":
                this.BarCode = value == null ? null : value.ToString();
                return;
            case "costprice":
                this.CostPrice = Convert.ToDecimal(value);
                return;
            case "costtype":
                this.CostType = value == null ? null : value.ToString();
                return;
            case "salepricededuct":
                this.SalePriceDeduct = Convert.ToDecimal(value);
                return;
            case "salepricepercent":
                this.SalePricePercent = Convert.ToSingle(value);
                return;
            case "purchasetype":
                this.PurchaseType = value == null ? null : value.ToString();
                return;
            case "storagevirtual":
                this.StorageVirtual = (int)Convert.ToDecimal(value);
                return;
            case "onlineoldprice":
                this.OnlineOldPrice = Convert.ToDecimal(value);
                return;
            case "onlinenewprice":
                this.OnlineNewPrice = Convert.ToDecimal(value);
                return;
            case "offlineoldprice":
                this.OfflineOldPrice = Convert.ToDecimal(value);
                return;
            case "offlinenewprice":
                this.OfflineNewPrice = Convert.ToDecimal(value);
                return;
            case "basepricenotax":
                this.BasePriceNoTax = Convert.ToDecimal(value);
                return;
            case "basepricehastax":
                this.BasePriceHasTax = Convert.ToDecimal(value);
                return;
            case "batchbid":
                this.BatchBID = (long)Convert.ToDecimal(value);
                return;
            case "orderb2boid":
                this.OrderB2BOID = (long)Convert.ToDecimal(value);
                return;
            case "excelimportid":
                this.ExcelImportID = value == null ? null : value.ToString();
                return;
            case "memberattribute":
                this.MemberAttribute = value == null ? null : value.ToString();
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
            case _DataStruct_.OrgOID:
                this.OrgOID = Convert.ToInt64(value);
                return;
            case _DataStruct_.SupplyOIDCurrent:
                this.SupplyOIDCurrent = Convert.ToInt64(value);
                return;
            case _DataStruct_.SupplyName:
                this.SupplyName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.SkuSID:
                this.SkuSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.BarCode:
                this.BarCode = value == null ? null : value.ToString();
                return;
            case _DataStruct_.CostPrice:
                this.CostPrice = Convert.ToDecimal(value);
                return;
            case _DataStruct_.CostType:
                this.CostType = value == null ? null : value.ToString();
                return;
            case _DataStruct_.SalePriceDeduct:
                this.SalePriceDeduct = Convert.ToDecimal(value);
                return;
            case _DataStruct_.SalePricePercent:
                this.SalePricePercent = Convert.ToSingle(value);
                return;
            case _DataStruct_.PurchaseType:
                this.PurchaseType = value == null ? null : value.ToString();
                return;
            case _DataStruct_.StorageVirtual:
                this.StorageVirtual = Convert.ToInt32(value);
                return;
            case _DataStruct_.OnlineOldPrice:
                this.OnlineOldPrice = Convert.ToDecimal(value);
                return;
            case _DataStruct_.OnlineNewPrice:
                this.OnlineNewPrice = Convert.ToDecimal(value);
                return;
            case _DataStruct_.OfflineOldPrice:
                this.OfflineOldPrice = Convert.ToDecimal(value);
                return;
            case _DataStruct_.OfflineNewPrice:
                this.OfflineNewPrice = Convert.ToDecimal(value);
                return;
            case _DataStruct_.BasePriceNoTax:
                this.BasePriceNoTax = Convert.ToDecimal(value);
                return;
            case _DataStruct_.BasePriceHasTax:
                this.BasePriceHasTax = Convert.ToDecimal(value);
                return;
            case _DataStruct_.BatchBID:
                this.BatchBID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrderB2BOID:
                this.OrderB2BOID = Convert.ToInt64(value);
                return;
            case _DataStruct_.ExcelImportID:
                this.ExcelImportID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.MemberAttribute:
                this.MemberAttribute = value == null ? null : value.ToString();
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
            case "orgoid":
                return this.OrgOID;
            case "supplyoidcurrent":
                return this.SupplyOIDCurrent;
            case "supplyname":
                return this.SupplyName;
            case "skusid":
                return this.SkuSID;
            case "barcode":
                return this.BarCode;
            case "costprice":
                return this.CostPrice;
            case "costtype":
                return this.CostType;
            case "salepricededuct":
                return this.SalePriceDeduct;
            case "salepricepercent":
                return this.SalePricePercent;
            case "purchasetype":
                return this.PurchaseType;
            case "storagevirtual":
                return this.StorageVirtual;
            case "onlineoldprice":
                return this.OnlineOldPrice;
            case "onlinenewprice":
                return this.OnlineNewPrice;
            case "offlineoldprice":
                return this.OfflineOldPrice;
            case "offlinenewprice":
                return this.OfflineNewPrice;
            case "basepricenotax":
                return this.BasePriceNoTax;
            case "basepricehastax":
                return this.BasePriceHasTax;
            case "batchbid":
                return this.BatchBID;
            case "orderb2boid":
                return this.OrderB2BOID;
            case "excelimportid":
                return this.ExcelImportID;
            case "memberattribute":
                return this.MemberAttribute;
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
                case _DataStruct_.OrgOID:
                    return this.OrgOID;
                case _DataStruct_.SupplyOIDCurrent:
                    return this.SupplyOIDCurrent;
                case _DataStruct_.SupplyName:
                    return this.SupplyName;
                case _DataStruct_.SkuSID:
                    return this.SkuSID;
                case _DataStruct_.BarCode:
                    return this.BarCode;
                case _DataStruct_.CostPrice:
                    return this.CostPrice;
                case _DataStruct_.CostType:
                    return this.CostType;
                case _DataStruct_.SalePriceDeduct:
                    return this.SalePriceDeduct;
                case _DataStruct_.SalePricePercent:
                    return this.SalePricePercent;
                case _DataStruct_.PurchaseType:
                    return this.PurchaseType;
                case _DataStruct_.StorageVirtual:
                    return this.StorageVirtual;
                case _DataStruct_.OnlineOldPrice:
                    return this.OnlineOldPrice;
                case _DataStruct_.OnlineNewPrice:
                    return this.OnlineNewPrice;
                case _DataStruct_.OfflineOldPrice:
                    return this.OfflineOldPrice;
                case _DataStruct_.OfflineNewPrice:
                    return this.OfflineNewPrice;
                case _DataStruct_.BasePriceNoTax:
                    return this.BasePriceNoTax;
                case _DataStruct_.BasePriceHasTax:
                    return this.BasePriceHasTax;
                case _DataStruct_.BatchBID:
                    return this.BatchBID;
                case _DataStruct_.OrderB2BOID:
                    return this.OrderB2BOID;
                case _DataStruct_.ExcelImportID:
                    return this.ExcelImportID;
                case _DataStruct_.MemberAttribute:
                    return this.MemberAttribute;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(ProductSkuInOrgData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as ProductSkuInOrgData;
            if(sourceEntity == null)
                return;
            this._sID = sourceEntity._sID;
            this._siteSID = sourceEntity._siteSID;
            this._orgOID = sourceEntity._orgOID;
            this._supplyOIDCurrent = sourceEntity._supplyOIDCurrent;
            this._supplyName = sourceEntity._supplyName;
            this._skuSID = sourceEntity._skuSID;
            this._barCode = sourceEntity._barCode;
            this._costPrice = sourceEntity._costPrice;
            this._costType = sourceEntity._costType;
            this._salePriceDeduct = sourceEntity._salePriceDeduct;
            this._salePricePercent = sourceEntity._salePricePercent;
            this._purchaseType = sourceEntity._purchaseType;
            this._storageVirtual = sourceEntity._storageVirtual;
            this._onlineOldPrice = sourceEntity._onlineOldPrice;
            this._onlineNewPrice = sourceEntity._onlineNewPrice;
            this._offlineOldPrice = sourceEntity._offlineOldPrice;
            this._offlineNewPrice = sourceEntity._offlineNewPrice;
            this._basePriceNoTax = sourceEntity._basePriceNoTax;
            this._basePriceHasTax = sourceEntity._basePriceHasTax;
            this._batchBID = sourceEntity._batchBID;
            this._orderB2BOID = sourceEntity._orderB2BOID;
            this._excelImportID = sourceEntity._excelImportID;
            this._memberAttribute = sourceEntity._memberAttribute;
            CopyExtendValue(sourceEntity);
            this.__status.IsModified = true;
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(ProductSkuInOrgData source)
        {
                this.SID = source.SID;
                this.SiteSID = source.SiteSID;
                this.OrgOID = source.OrgOID;
                this.SupplyOIDCurrent = source.SupplyOIDCurrent;
                this.SupplyName = source.SupplyName;
                this.SkuSID = source.SkuSID;
                this.BarCode = source.BarCode;
                this.CostPrice = source.CostPrice;
                this.CostType = source.CostType;
                this.SalePriceDeduct = source.SalePriceDeduct;
                this.SalePricePercent = source.SalePricePercent;
                this.PurchaseType = source.PurchaseType;
                this.StorageVirtual = source.StorageVirtual;
                this.OnlineOldPrice = source.OnlineOldPrice;
                this.OnlineNewPrice = source.OnlineNewPrice;
                this.OfflineOldPrice = source.OfflineOldPrice;
                this.OfflineNewPrice = source.OfflineNewPrice;
                this.BasePriceNoTax = source.BasePriceNoTax;
                this.BasePriceHasTax = source.BasePriceHasTax;
                this.BatchBID = source.BatchBID;
                this.OrderB2BOID = source.OrderB2BOID;
                this.ExcelImportID = source.ExcelImportID;
                this.MemberAttribute = source.MemberAttribute;
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
                OnOrgOIDModified(subsist,false);
                OnSupplyOIDCurrentModified(subsist,false);
                OnSupplyNameModified(subsist,false);
                OnSkuSIDModified(subsist,false);
                OnBarCodeModified(subsist,false);
                OnCostPriceModified(subsist,false);
                OnCostTypeModified(subsist,false);
                OnSalePriceDeductModified(subsist,false);
                OnSalePricePercentModified(subsist,false);
                OnPurchaseTypeModified(subsist,false);
                OnStorageVirtualModified(subsist,false);
                OnOnlineOldPriceModified(subsist,false);
                OnOnlineNewPriceModified(subsist,false);
                OnOfflineOldPriceModified(subsist,false);
                OnOfflineNewPriceModified(subsist,false);
                OnBasePriceNoTaxModified(subsist,false);
                OnBasePriceHasTaxModified(subsist,false);
                OnBatchBIDModified(subsist,false);
                OnOrderB2BOIDModified(subsist,false);
                OnExcelImportIDModified(subsist,false);
                OnMemberAttributeModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnSIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnOrgOIDModified(subsist,true);
                OnSupplyOIDCurrentModified(subsist,true);
                OnSupplyNameModified(subsist,true);
                OnSkuSIDModified(subsist,true);
                OnBarCodeModified(subsist,true);
                OnCostPriceModified(subsist,true);
                OnCostTypeModified(subsist,true);
                OnSalePriceDeductModified(subsist,true);
                OnSalePricePercentModified(subsist,true);
                OnPurchaseTypeModified(subsist,true);
                OnStorageVirtualModified(subsist,true);
                OnOnlineOldPriceModified(subsist,true);
                OnOnlineNewPriceModified(subsist,true);
                OnOfflineOldPriceModified(subsist,true);
                OnOfflineNewPriceModified(subsist,true);
                OnBasePriceNoTaxModified(subsist,true);
                OnBasePriceHasTaxModified(subsist,true);
                OnBatchBIDModified(subsist,true);
                OnOrderB2BOIDModified(subsist,true);
                OnExcelImportIDModified(subsist,true);
                OnMemberAttributeModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[23] > 0)
            {
                OnSIDModified(subsist,modifieds[_DataStruct_.Real_SID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnOrgOIDModified(subsist,modifieds[_DataStruct_.Real_OrgOID] == 1);
                OnSupplyOIDCurrentModified(subsist,modifieds[_DataStruct_.Real_SupplyOIDCurrent] == 1);
                OnSupplyNameModified(subsist,modifieds[_DataStruct_.Real_SupplyName] == 1);
                OnSkuSIDModified(subsist,modifieds[_DataStruct_.Real_SkuSID] == 1);
                OnBarCodeModified(subsist,modifieds[_DataStruct_.Real_BarCode] == 1);
                OnCostPriceModified(subsist,modifieds[_DataStruct_.Real_CostPrice] == 1);
                OnCostTypeModified(subsist,modifieds[_DataStruct_.Real_CostType] == 1);
                OnSalePriceDeductModified(subsist,modifieds[_DataStruct_.Real_SalePriceDeduct] == 1);
                OnSalePricePercentModified(subsist,modifieds[_DataStruct_.Real_SalePricePercent] == 1);
                OnPurchaseTypeModified(subsist,modifieds[_DataStruct_.Real_PurchaseType] == 1);
                OnStorageVirtualModified(subsist,modifieds[_DataStruct_.Real_StorageVirtual] == 1);
                OnOnlineOldPriceModified(subsist,modifieds[_DataStruct_.Real_OnlineOldPrice] == 1);
                OnOnlineNewPriceModified(subsist,modifieds[_DataStruct_.Real_OnlineNewPrice] == 1);
                OnOfflineOldPriceModified(subsist,modifieds[_DataStruct_.Real_OfflineOldPrice] == 1);
                OnOfflineNewPriceModified(subsist,modifieds[_DataStruct_.Real_OfflineNewPrice] == 1);
                OnBasePriceNoTaxModified(subsist,modifieds[_DataStruct_.Real_BasePriceNoTax] == 1);
                OnBasePriceHasTaxModified(subsist,modifieds[_DataStruct_.Real_BasePriceHasTax] == 1);
                OnBatchBIDModified(subsist,modifieds[_DataStruct_.Real_BatchBID] == 1);
                OnOrderB2BOIDModified(subsist,modifieds[_DataStruct_.Real_OrderB2BOID] == 1);
                OnExcelImportIDModified(subsist,modifieds[_DataStruct_.Real_ExcelImportID] == 1);
                OnMemberAttributeModified(subsist,modifieds[_DataStruct_.Real_MemberAttribute] == 1);
            }
        }

        /// <summary>
        /// 编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 站点编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSiteSIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 组织编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrgOIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 供应商编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSupplyOIDCurrentModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 供应商名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSupplyNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// Sku编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSkuSIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 条形码修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBarCodeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 成本价修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCostPriceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 成本类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnCostTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 销售扣除修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSalePriceDeductModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 销售扣点修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSalePricePercentModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 采购类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPurchaseTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 虚拟仓储修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnStorageVirtualModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 线上原价修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOnlineOldPriceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 线上现价修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOnlineNewPriceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 线下原价修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOfflineOldPriceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 线下现价修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOfflineNewPriceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 税前价修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBasePriceNoTaxModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 含税价修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBasePriceHasTaxModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 批次编号修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnBatchBIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 订单标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrderB2BOIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 导入标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnExcelImportIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 商品会员类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnMemberAttributeModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"ProductSkuInOrg";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"组织SKU";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"组织SKU";
            /// <summary>
            /// 实体标识
            /// </summary>
            public const int EntityIdentity = 0x0;
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityPrimaryKey = "SID";
            
            
            /// <summary>
            /// 编号的数字标识
            /// </summary>
            public const int SID = 2;
            
            /// <summary>
            /// 编号的实时记录顺序
            /// </summary>
            public const int Real_SID = 0;

            /// <summary>
            /// 站点编号的数字标识
            /// </summary>
            public const int SiteSID = 3;
            
            /// <summary>
            /// 站点编号的实时记录顺序
            /// </summary>
            public const int Real_SiteSID = 1;

            /// <summary>
            /// 组织编号的数字标识
            /// </summary>
            public const int OrgOID = 4;
            
            /// <summary>
            /// 组织编号的实时记录顺序
            /// </summary>
            public const int Real_OrgOID = 2;

            /// <summary>
            /// 供应商编号的数字标识
            /// </summary>
            public const int SupplyOIDCurrent = 5;
            
            /// <summary>
            /// 供应商编号的实时记录顺序
            /// </summary>
            public const int Real_SupplyOIDCurrent = 3;

            /// <summary>
            /// 供应商名称的数字标识
            /// </summary>
            public const int SupplyName = 6;
            
            /// <summary>
            /// 供应商名称的实时记录顺序
            /// </summary>
            public const int Real_SupplyName = 4;

            /// <summary>
            /// Sku编号的数字标识
            /// </summary>
            public const int SkuSID = 7;
            
            /// <summary>
            /// Sku编号的实时记录顺序
            /// </summary>
            public const int Real_SkuSID = 5;

            /// <summary>
            /// 条形码的数字标识
            /// </summary>
            public const int BarCode = 8;
            
            /// <summary>
            /// 条形码的实时记录顺序
            /// </summary>
            public const int Real_BarCode = 6;

            /// <summary>
            /// 成本价的数字标识
            /// </summary>
            public const int CostPrice = 9;
            
            /// <summary>
            /// 成本价的实时记录顺序
            /// </summary>
            public const int Real_CostPrice = 7;

            /// <summary>
            /// 成本类型的数字标识
            /// </summary>
            public const int CostType = 10;
            
            /// <summary>
            /// 成本类型的实时记录顺序
            /// </summary>
            public const int Real_CostType = 8;

            /// <summary>
            /// 销售扣除的数字标识
            /// </summary>
            public const int SalePriceDeduct = 11;
            
            /// <summary>
            /// 销售扣除的实时记录顺序
            /// </summary>
            public const int Real_SalePriceDeduct = 9;

            /// <summary>
            /// 销售扣点的数字标识
            /// </summary>
            public const int SalePricePercent = 12;
            
            /// <summary>
            /// 销售扣点的实时记录顺序
            /// </summary>
            public const int Real_SalePricePercent = 10;

            /// <summary>
            /// 采购类型的数字标识
            /// </summary>
            public const int PurchaseType = 13;
            
            /// <summary>
            /// 采购类型的实时记录顺序
            /// </summary>
            public const int Real_PurchaseType = 11;

            /// <summary>
            /// 虚拟仓储的数字标识
            /// </summary>
            public const int StorageVirtual = 14;
            
            /// <summary>
            /// 虚拟仓储的实时记录顺序
            /// </summary>
            public const int Real_StorageVirtual = 12;

            /// <summary>
            /// 线上原价的数字标识
            /// </summary>
            public const int OnlineOldPrice = 15;
            
            /// <summary>
            /// 线上原价的实时记录顺序
            /// </summary>
            public const int Real_OnlineOldPrice = 13;

            /// <summary>
            /// 线上现价的数字标识
            /// </summary>
            public const int OnlineNewPrice = 16;
            
            /// <summary>
            /// 线上现价的实时记录顺序
            /// </summary>
            public const int Real_OnlineNewPrice = 14;

            /// <summary>
            /// 线下原价的数字标识
            /// </summary>
            public const int OfflineOldPrice = 17;
            
            /// <summary>
            /// 线下原价的实时记录顺序
            /// </summary>
            public const int Real_OfflineOldPrice = 15;

            /// <summary>
            /// 线下现价的数字标识
            /// </summary>
            public const int OfflineNewPrice = 18;
            
            /// <summary>
            /// 线下现价的实时记录顺序
            /// </summary>
            public const int Real_OfflineNewPrice = 16;

            /// <summary>
            /// 税前价的数字标识
            /// </summary>
            public const int BasePriceNoTax = 19;
            
            /// <summary>
            /// 税前价的实时记录顺序
            /// </summary>
            public const int Real_BasePriceNoTax = 17;

            /// <summary>
            /// 含税价的数字标识
            /// </summary>
            public const int BasePriceHasTax = 20;
            
            /// <summary>
            /// 含税价的实时记录顺序
            /// </summary>
            public const int Real_BasePriceHasTax = 18;

            /// <summary>
            /// 批次编号的数字标识
            /// </summary>
            public const int BatchBID = 21;
            
            /// <summary>
            /// 批次编号的实时记录顺序
            /// </summary>
            public const int Real_BatchBID = 19;

            /// <summary>
            /// 订单标识的数字标识
            /// </summary>
            public const int OrderB2BOID = 22;
            
            /// <summary>
            /// 订单标识的实时记录顺序
            /// </summary>
            public const int Real_OrderB2BOID = 20;

            /// <summary>
            /// 导入标识的数字标识
            /// </summary>
            public const int ExcelImportID = 23;
            
            /// <summary>
            /// 导入标识的实时记录顺序
            /// </summary>
            public const int Real_ExcelImportID = 21;

            /// <summary>
            /// 商品会员类型的数字标识
            /// </summary>
            public const int MemberAttribute = 24;
            
            /// <summary>
            /// 商品会员类型的实时记录顺序
            /// </summary>
            public const int Real_MemberAttribute = 22;

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
                            Title        = "编号",
                            Caption      = @"编号",
                            Description  = @"编号",
                            ColumnName   = "SID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 0,
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
                            Title        = "站点编号",
                            Caption      = @"站点编号",
                            Description  = @"站点编号",
                            ColumnName   = "SiteSID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 0,
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
                            Title        = "组织编号",
                            Caption      = @"组织编号",
                            Description  = @"组织编号",
                            ColumnName   = "OrgOID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 0,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SupplyOIDCurrent,
                        new PropertySturct
                        {
                            Index        = SupplyOIDCurrent,
                            Name         = "SupplyOIDCurrent",
                            Title        = "供应商编号",
                            Caption      = @"供应商编号",
                            Description  = @"供应商编号",
                            ColumnName   = "SupplyOIDCurrent",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 0,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SupplyName,
                        new PropertySturct
                        {
                            Index        = SupplyName,
                            Name         = "SupplyName",
                            Title        = "供应商名称",
                            Caption      = @"供应商名称",
                            Description  = @"供应商名称",
                            ColumnName   = "SupplyName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            DbType       = 12,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SkuSID,
                        new PropertySturct
                        {
                            Index        = SkuSID,
                            Name         = "SkuSID",
                            Title        = "Sku编号",
                            Caption      = @"Sku编号",
                            Description  = @"Sku编号",
                            ColumnName   = "SkuSID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 0,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_BarCode,
                        new PropertySturct
                        {
                            Index        = BarCode,
                            Name         = "BarCode",
                            Title        = "条形码",
                            Caption      = @"条形码",
                            Description  = @"条形码",
                            ColumnName   = "BarCode",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            DbType       = 12,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_CostPrice,
                        new PropertySturct
                        {
                            Index        = CostPrice,
                            Name         = "CostPrice",
                            Title        = "成本价",
                            Caption      = @"成本价",
                            Description  = @"成本价",
                            ColumnName   = "CostPrice",
                            PropertyType = typeof(decimal),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 5,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_CostType,
                        new PropertySturct
                        {
                            Index        = CostType,
                            Name         = "CostType",
                            Title        = "成本类型",
                            Caption      = @"成本类型",
                            Description  = @"成本类型",
                            ColumnName   = "CostType",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            DbType       = 12,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SalePriceDeduct,
                        new PropertySturct
                        {
                            Index        = SalePriceDeduct,
                            Name         = "SalePriceDeduct",
                            Title        = "销售扣除",
                            Caption      = @"销售扣除",
                            Description  = @"销售扣除",
                            ColumnName   = "SalePriceDeduct",
                            PropertyType = typeof(decimal),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 5,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SalePricePercent,
                        new PropertySturct
                        {
                            Index        = SalePricePercent,
                            Name         = "SalePricePercent",
                            Title        = "销售扣点",
                            Caption      = @"销售扣点",
                            Description  = @"销售扣点",
                            ColumnName   = "SalePricePercent",
                            PropertyType = typeof(float),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 6,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PurchaseType,
                        new PropertySturct
                        {
                            Index        = PurchaseType,
                            Name         = "PurchaseType",
                            Title        = "采购类型",
                            Caption      = @"采购类型",
                            Description  = @"采购类型",
                            ColumnName   = "PurchaseType",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            DbType       = 12,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_StorageVirtual,
                        new PropertySturct
                        {
                            Index        = StorageVirtual,
                            Name         = "StorageVirtual",
                            Title        = "虚拟仓储",
                            Caption      = @"虚拟仓储",
                            Description  = @"虚拟仓储",
                            ColumnName   = "StorageVirtual",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 8,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OnlineOldPrice,
                        new PropertySturct
                        {
                            Index        = OnlineOldPrice,
                            Name         = "OnlineOldPrice",
                            Title        = "线上原价",
                            Caption      = @"线上原价",
                            Description  = @"线上原价",
                            ColumnName   = "OnlineOldPrice",
                            PropertyType = typeof(decimal),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 5,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OnlineNewPrice,
                        new PropertySturct
                        {
                            Index        = OnlineNewPrice,
                            Name         = "OnlineNewPrice",
                            Title        = "线上现价",
                            Caption      = @"线上现价",
                            Description  = @"线上现价",
                            ColumnName   = "OnlineNewPrice",
                            PropertyType = typeof(decimal),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 5,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OfflineOldPrice,
                        new PropertySturct
                        {
                            Index        = OfflineOldPrice,
                            Name         = "OfflineOldPrice",
                            Title        = "线下原价",
                            Caption      = @"线下原价",
                            Description  = @"线下原价",
                            ColumnName   = "OfflineOldPrice",
                            PropertyType = typeof(decimal),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 5,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OfflineNewPrice,
                        new PropertySturct
                        {
                            Index        = OfflineNewPrice,
                            Name         = "OfflineNewPrice",
                            Title        = "线下现价",
                            Caption      = @"线下现价",
                            Description  = @"线下现价",
                            ColumnName   = "OfflineNewPrice",
                            PropertyType = typeof(decimal),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 5,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_BasePriceNoTax,
                        new PropertySturct
                        {
                            Index        = BasePriceNoTax,
                            Name         = "BasePriceNoTax",
                            Title        = "税前价",
                            Caption      = @"税前价",
                            Description  = @"税前价",
                            ColumnName   = "BasePriceNoTax",
                            PropertyType = typeof(decimal),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 5,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_BasePriceHasTax,
                        new PropertySturct
                        {
                            Index        = BasePriceHasTax,
                            Name         = "BasePriceHasTax",
                            Title        = "含税价",
                            Caption      = @"含税价",
                            Description  = @"含税价",
                            ColumnName   = "BasePriceHasTax",
                            PropertyType = typeof(decimal),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 5,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_BatchBID,
                        new PropertySturct
                        {
                            Index        = BatchBID,
                            Name         = "BatchBID",
                            Title        = "批次编号",
                            Caption      = @"批次编号",
                            Description  = @"批次编号",
                            ColumnName   = "BatchBID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 0,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrderB2BOID,
                        new PropertySturct
                        {
                            Index        = OrderB2BOID,
                            Name         = "OrderB2BOID",
                            Title        = "订单标识",
                            Caption      = @"订单标识",
                            Description  = @"订单标识",
                            ColumnName   = "OrderB2BOID",
                            PropertyType = typeof(long),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            DbType       = 0,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ExcelImportID,
                        new PropertySturct
                        {
                            Index        = ExcelImportID,
                            Name         = "ExcelImportID",
                            Title        = "导入标识",
                            Caption      = @"导入标识",
                            Description  = @"Excel导入标识",
                            ColumnName   = "ExcelImportID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            DbType       = 12,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_MemberAttribute,
                        new PropertySturct
                        {
                            Index        = MemberAttribute,
                            Name         = "MemberAttribute",
                            Title        = "商品会员类型",
                            Caption      = @"商品会员类型",
                            Description  = @"商品会员类型",
                            ColumnName   = "MemberAttribute",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            DbType       = 22,
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