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
    /// 用户端
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class UserOrderData : IIdentityData
    {
        #region 构造
        
        /// <summary>
        /// 构造
        /// </summary>
        public UserOrderData()
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
        /// 可存储100个字符.合理长度应不大于100.
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
        /// 订单标识Sub标识
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orderIDSubID;

        partial void OnOrderIDSubIDGet();

        partial void OnOrderIDSubIDSet(ref string value);

        partial void OnOrderIDSubIDSeted();

        
        /// <summary>
        /// 订单标识Sub标识
        /// </summary>
        /// <value>
        /// 可存储40个字符.合理长度应不大于40.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrderIDSubID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"订单标识Sub标识")]
        public  string OrderIDSubID
        {
            get
            {
                OnOrderIDSubIDGet();
                return this._orderIDSubID;
            }
            set
            {
                if(this._orderIDSubID == value)
                    return;
                OnOrderIDSubIDSet(ref value);
                this._orderIDSubID = value;
                OnOrderIDSubIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrderIDSubID);
            }
        }
        /// <summary>
        /// 翻转状态
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orderState;

        partial void OnOrderStateGet();

        partial void OnOrderStateSet(ref string value);

        partial void OnOrderStateSeted();

        
        /// <summary>
        /// 翻转状态
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrderState", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"翻转状态")]
        public  string OrderState
        {
            get
            {
                OnOrderStateGet();
                return this._orderState;
            }
            set
            {
                if(this._orderState == value)
                    return;
                OnOrderStateSet(ref value);
                this._orderState = value;
                OnOrderStateSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrderState);
            }
        }
        /// <summary>
        /// 最高价格
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orderPrice;

        partial void OnOrderPriceGet();

        partial void OnOrderPriceSet(ref string value);

        partial void OnOrderPriceSeted();

        
        /// <summary>
        /// 最高价格
        /// </summary>
        /// <value>
        /// 可存储19个字符.合理长度应不大于19.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrderPrice", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"最高价格")]
        public  string OrderPrice
        {
            get
            {
                OnOrderPriceGet();
                return this._orderPrice;
            }
            set
            {
                if(this._orderPrice == value)
                    return;
                OnOrderPriceSet(ref value);
                this._orderPrice = value;
                OnOrderPriceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrderPrice);
            }
        }
        /// <summary>
        /// 货运价格
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _freightPrice;

        partial void OnFreightPriceGet();

        partial void OnFreightPriceSet(ref string value);

        partial void OnFreightPriceSeted();

        
        /// <summary>
        /// 货运价格
        /// </summary>
        /// <value>
        /// 可存储19个字符.合理长度应不大于19.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("FreightPrice", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"货运价格")]
        public  string FreightPrice
        {
            get
            {
                OnFreightPriceGet();
                return this._freightPrice;
            }
            set
            {
                if(this._freightPrice == value)
                    return;
                OnFreightPriceSet(ref value);
                this._freightPrice = value;
                OnFreightPriceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_FreightPrice);
            }
        }
        /// <summary>
        /// 总价格
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _totlePrice;

        partial void OnTotlePriceGet();

        partial void OnTotlePriceSet(ref string value);

        partial void OnTotlePriceSeted();

        
        /// <summary>
        /// 总价格
        /// </summary>
        /// <value>
        /// 可存储19个字符.合理长度应不大于19.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("TotlePrice", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"总价格")]
        public  string TotlePrice
        {
            get
            {
                OnTotlePriceGet();
                return this._totlePrice;
            }
            set
            {
                if(this._totlePrice == value)
                    return;
                OnTotlePriceSet(ref value);
                this._totlePrice = value;
                OnTotlePriceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_TotlePrice);
            }
        }
        /// <summary>
        /// 总重量
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public float _totleWeight;

        partial void OnTotleWeightGet();

        partial void OnTotleWeightSet(ref float value);

        partial void OnTotleWeightSeted();

        
        /// <summary>
        /// 总重量
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("TotleWeight", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"总重量")]
        public  float TotleWeight
        {
            get
            {
                OnTotleWeightGet();
                return this._totleWeight;
            }
            set
            {
                if(this._totleWeight == value)
                    return;
                OnTotleWeightSet(ref value);
                this._totleWeight = value;
                OnTotleWeightSeted();
                this.OnPropertyChanged(_DataStruct_.Real_TotleWeight);
            }
        }
        /// <summary>
        /// SKU列表
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _skuList;

        partial void OnSkuListGet();

        partial void OnSkuListSet(ref string value);

        partial void OnSkuListSeted();

        
        /// <summary>
        /// SKU列表
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("SkuList", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"SKU列表")]
        public  string SkuList
        {
            get
            {
                OnSkuListGet();
                return this._skuList;
            }
            set
            {
                if(this._skuList == value)
                    return;
                OnSkuListSet(ref value);
                this._skuList = value;
                OnSkuListSeted();
                this.OnPropertyChanged(_DataStruct_.Real_SkuList);
            }
        }
        /// <summary>
        /// 订单Time
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _orderTime;

        partial void OnOrderTimeGet();

        partial void OnOrderTimeSet(ref DateTime value);

        partial void OnOrderTimeSeted();

        
        /// <summary>
        /// 订单Time
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrderTime", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"订单Time")]
        public  DateTime OrderTime
        {
            get
            {
                OnOrderTimeGet();
                return this._orderTime;
            }
            set
            {
                if(this._orderTime == value)
                    return;
                OnOrderTimeSet(ref value);
                this._orderTime = value;
                OnOrderTimeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrderTime);
            }
        }
        /// <summary>
        /// 订单Name
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orderName;

        partial void OnOrderNameGet();

        partial void OnOrderNameSet(ref string value);

        partial void OnOrderNameSeted();

        
        /// <summary>
        /// 订单Name
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrderName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"订单Name")]
        public  string OrderName
        {
            get
            {
                OnOrderNameGet();
                return this._orderName;
            }
            set
            {
                if(this._orderName == value)
                    return;
                OnOrderNameSet(ref value);
                this._orderName = value;
                OnOrderNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrderName);
            }
        }
        /// <summary>
        /// 公用电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orderPhone;

        partial void OnOrderPhoneGet();

        partial void OnOrderPhoneSet(ref string value);

        partial void OnOrderPhoneSeted();

        
        /// <summary>
        /// 公用电话
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrderPhone", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"公用电话")]
        public  string OrderPhone
        {
            get
            {
                OnOrderPhoneGet();
                return this._orderPhone;
            }
            set
            {
                if(this._orderPhone == value)
                    return;
                OnOrderPhoneSet(ref value);
                this._orderPhone = value;
                OnOrderPhoneSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrderPhone);
            }
        }
        /// <summary>
        /// 自动电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orderTel;

        partial void OnOrderTelGet();

        partial void OnOrderTelSet(ref string value);

        partial void OnOrderTelSeted();

        
        /// <summary>
        /// 自动电话
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrderTel", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"自动电话")]
        public  string OrderTel
        {
            get
            {
                OnOrderTelGet();
                return this._orderTel;
            }
            set
            {
                if(this._orderTel == value)
                    return;
                OnOrderTelSet(ref value);
                this._orderTel = value;
                OnOrderTelSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrderTel);
            }
        }
        /// <summary>
        /// 接收方名称
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _receiverName;

        partial void OnReceiverNameGet();

        partial void OnReceiverNameSet(ref string value);

        partial void OnReceiverNameSeted();

        
        /// <summary>
        /// 接收方名称
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ReceiverName", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"接收方名称")]
        public  string ReceiverName
        {
            get
            {
                OnReceiverNameGet();
                return this._receiverName;
            }
            set
            {
                if(this._receiverName == value)
                    return;
                OnReceiverNameSet(ref value);
                this._receiverName = value;
                OnReceiverNameSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ReceiverName);
            }
        }
        /// <summary>
        /// 接收机电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _receiverPhone;

        partial void OnReceiverPhoneGet();

        partial void OnReceiverPhoneSet(ref string value);

        partial void OnReceiverPhoneSeted();

        
        /// <summary>
        /// 接收机电话
        /// </summary>
        /// <value>
        /// 可存储50个字符.合理长度应不大于50.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ReceiverPhone", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"接收机电话")]
        public  string ReceiverPhone
        {
            get
            {
                OnReceiverPhoneGet();
                return this._receiverPhone;
            }
            set
            {
                if(this._receiverPhone == value)
                    return;
                OnReceiverPhoneSet(ref value);
                this._receiverPhone = value;
                OnReceiverPhoneSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ReceiverPhone);
            }
        }
        /// <summary>
        /// 接收机电话
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _receiverTel;

        partial void OnReceiverTelGet();

        partial void OnReceiverTelSet(ref string value);

        partial void OnReceiverTelSeted();

        
        /// <summary>
        /// 接收机电话
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ReceiverTel", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"接收机电话")]
        public  string ReceiverTel
        {
            get
            {
                OnReceiverTelGet();
                return this._receiverTel;
            }
            set
            {
                if(this._receiverTel == value)
                    return;
                OnReceiverTelSet(ref value);
                this._receiverTel = value;
                OnReceiverTelSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ReceiverTel);
            }
        }
        /// <summary>
        /// 接收省
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _receiverProvince;

        partial void OnReceiverProvinceGet();

        partial void OnReceiverProvinceSet(ref string value);

        partial void OnReceiverProvinceSeted();

        
        /// <summary>
        /// 接收省
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ReceiverProvince", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"接收省")]
        public  string ReceiverProvince
        {
            get
            {
                OnReceiverProvinceGet();
                return this._receiverProvince;
            }
            set
            {
                if(this._receiverProvince == value)
                    return;
                OnReceiverProvinceSet(ref value);
                this._receiverProvince = value;
                OnReceiverProvinceSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ReceiverProvince);
            }
        }
        /// <summary>
        /// 接收城市
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _receiverCity;

        partial void OnReceiverCityGet();

        partial void OnReceiverCitySet(ref string value);

        partial void OnReceiverCitySeted();

        
        /// <summary>
        /// 接收城市
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ReceiverCity", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"接收城市")]
        public  string ReceiverCity
        {
            get
            {
                OnReceiverCityGet();
                return this._receiverCity;
            }
            set
            {
                if(this._receiverCity == value)
                    return;
                OnReceiverCitySet(ref value);
                this._receiverCity = value;
                OnReceiverCitySeted();
                this.OnPropertyChanged(_DataStruct_.Real_ReceiverCity);
            }
        }
        /// <summary>
        /// 接收区
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _receiverArea;

        partial void OnReceiverAreaGet();

        partial void OnReceiverAreaSet(ref string value);

        partial void OnReceiverAreaSeted();

        
        /// <summary>
        /// 接收区
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ReceiverArea", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"接收区")]
        public  string ReceiverArea
        {
            get
            {
                OnReceiverAreaGet();
                return this._receiverArea;
            }
            set
            {
                if(this._receiverArea == value)
                    return;
                OnReceiverAreaSet(ref value);
                this._receiverArea = value;
                OnReceiverAreaSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ReceiverArea);
            }
        }
        /// <summary>
        /// 收件人地址
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _receiverAddress;

        partial void OnReceiverAddressGet();

        partial void OnReceiverAddressSet(ref string value);

        partial void OnReceiverAddressSeted();

        
        /// <summary>
        /// 收件人地址
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ReceiverAddress", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"收件人地址")]
        public  string ReceiverAddress
        {
            get
            {
                OnReceiverAddressGet();
                return this._receiverAddress;
            }
            set
            {
                if(this._receiverAddress == value)
                    return;
                OnReceiverAddressSet(ref value);
                this._receiverAddress = value;
                OnReceiverAddressSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ReceiverAddress);
            }
        }
        /// <summary>
        /// 用户消息
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _userMessage;

        partial void OnUserMessageGet();

        partial void OnUserMessageSet(ref string value);

        partial void OnUserMessageSeted();

        
        /// <summary>
        /// 用户消息
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("UserMessage", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"用户消息")]
        public  string UserMessage
        {
            get
            {
                OnUserMessageGet();
                return this._userMessage;
            }
            set
            {
                if(this._userMessage == value)
                    return;
                OnUserMessageSet(ref value);
                this._userMessage = value;
                OnUserMessageSeted();
                this.OnPropertyChanged(_DataStruct_.Real_UserMessage);
            }
        }
        /// <summary>
        /// 支付时间
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public DateTime _payTime;

        partial void OnPayTimeGet();

        partial void OnPayTimeSet(ref DateTime value);

        partial void OnPayTimeSeted();

        
        /// <summary>
        /// 支付时间
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("PayTime", NullValueHandling = NullValueHandling.Ignore) , JsonConverter(typeof(MyDateTimeConverter)) , DisplayName(@"支付时间")]
        public  DateTime PayTime
        {
            get
            {
                OnPayTimeGet();
                return this._payTime;
            }
            set
            {
                if(this._payTime == value)
                    return;
                OnPayTimeSet(ref value);
                this._payTime = value;
                OnPayTimeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_PayTime);
            }
        }
        /// <summary>
        /// 贸易壁垒
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _tradeType;

        partial void OnTradeTypeGet();

        partial void OnTradeTypeSet(ref string value);

        partial void OnTradeTypeSeted();

        
        /// <summary>
        /// 贸易壁垒
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("TradeType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"贸易壁垒")]
        public  string TradeType
        {
            get
            {
                OnTradeTypeGet();
                return this._tradeType;
            }
            set
            {
                if(this._tradeType == value)
                    return;
                OnTradeTypeSet(ref value);
                this._tradeType = value;
                OnTradeTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_TradeType);
            }
        }
        /// <summary>
        /// 贸易壁垒
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _tradeID;

        partial void OnTradeIDGet();

        partial void OnTradeIDSet(ref string value);

        partial void OnTradeIDSeted();

        
        /// <summary>
        /// 贸易壁垒
        /// </summary>
        /// <value>
        /// 可存储400个字符.合理长度应不大于400.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("TradeID", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"贸易壁垒")]
        public  string TradeID
        {
            get
            {
                OnTradeIDGet();
                return this._tradeID;
            }
            set
            {
                if(this._tradeID == value)
                    return;
                OnTradeIDSet(ref value);
                this._tradeID = value;
                OnTradeIDSeted();
                this.OnPropertyChanged(_DataStruct_.Real_TradeID);
            }
        }
        /// <summary>
        /// 货运单信息
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _waybillInfo;

        partial void OnWaybillInfoGet();

        partial void OnWaybillInfoSet(ref string value);

        partial void OnWaybillInfoSeted();

        
        /// <summary>
        /// 货运单信息
        /// </summary>
        /// <value>
        /// 可存储400个字符.合理长度应不大于400.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("WaybillInfo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"货运单信息")]
        public  string WaybillInfo
        {
            get
            {
                OnWaybillInfoGet();
                return this._waybillInfo;
            }
            set
            {
                if(this._waybillInfo == value)
                    return;
                OnWaybillInfoSet(ref value);
                this._waybillInfo = value;
                OnWaybillInfoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_WaybillInfo);
            }
        }
        /// <summary>
        /// 价格备忘录
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _priceMemo;

        partial void OnPriceMemoGet();

        partial void OnPriceMemoSet(ref string value);

        partial void OnPriceMemoSeted();

        
        /// <summary>
        /// 价格备忘录
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("PriceMemo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"价格备忘录")]
        public  string PriceMemo
        {
            get
            {
                OnPriceMemoGet();
                return this._priceMemo;
            }
            set
            {
                if(this._priceMemo == value)
                    return;
                OnPriceMemoSet(ref value);
                this._priceMemo = value;
                OnPriceMemoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_PriceMemo);
            }
        }
        /// <summary>
        /// 产品信息
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _productInfo;

        partial void OnProductInfoGet();

        partial void OnProductInfoSet(ref string value);

        partial void OnProductInfoSeted();

        
        /// <summary>
        /// 产品信息
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ProductInfo", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"产品信息")]
        public  string ProductInfo
        {
            get
            {
                OnProductInfoGet();
                return this._productInfo;
            }
            set
            {
                if(this._productInfo == value)
                    return;
                OnProductInfoSet(ref value);
                this._productInfo = value;
                OnProductInfoSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ProductInfo);
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
        /// 产品SKU计数
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public int _productSkuCount;

        partial void OnProductSkuCountGet();

        partial void OnProductSkuCountSet(ref int value);

        partial void OnProductSkuCountSeted();

        
        /// <summary>
        /// 产品SKU计数
        /// </summary>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("ProductSkuCount", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"产品SKU计数")]
        public  int ProductSkuCount
        {
            get
            {
                OnProductSkuCountGet();
                return this._productSkuCount;
            }
            set
            {
                if(this._productSkuCount == value)
                    return;
                OnProductSkuCountSet(ref value);
                this._productSkuCount = value;
                OnProductSkuCountSeted();
                this.OnPropertyChanged(_DataStruct_.Real_ProductSkuCount);
            }
        }
        /// <summary>
        /// 订单From
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orderFrom;

        partial void OnOrderFromGet();

        partial void OnOrderFromSet(ref string value);

        partial void OnOrderFromSeted();

        
        /// <summary>
        /// 订单From
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrderFrom", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"订单From")]
        public  string OrderFrom
        {
            get
            {
                OnOrderFromGet();
                return this._orderFrom;
            }
            set
            {
                if(this._orderFrom == value)
                    return;
                OnOrderFromSet(ref value);
                this._orderFrom = value;
                OnOrderFromSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrderFrom);
            }
        }
        /// <summary>
        /// 订单类型
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orderType;

        partial void OnOrderTypeGet();

        partial void OnOrderTypeSet(ref string value);

        partial void OnOrderTypeSeted();

        
        /// <summary>
        /// 订单类型
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrderType", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"订单类型")]
        public  string OrderType
        {
            get
            {
                OnOrderTypeGet();
                return this._orderType;
            }
            set
            {
                if(this._orderType == value)
                    return;
                OnOrderTypeSet(ref value);
                this._orderType = value;
                OnOrderTypeSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrderType);
            }
        }
        /// <summary>
        /// 订单备注
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _orderRemark;

        partial void OnOrderRemarkGet();

        partial void OnOrderRemarkSet(ref string value);

        partial void OnOrderRemarkSeted();

        
        /// <summary>
        /// 订单备注
        /// </summary>
        /// <value>
        /// 可存储1000个字符.合理长度应不大于1000.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("OrderRemark", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"订单备注")]
        public  string OrderRemark
        {
            get
            {
                OnOrderRemarkGet();
                return this._orderRemark;
            }
            set
            {
                if(this._orderRemark == value)
                    return;
                OnOrderRemarkSet(ref value);
                this._orderRemark = value;
                OnOrderRemarkSeted();
                this.OnPropertyChanged(_DataStruct_.Real_OrderRemark);
            }
        }
        /// <summary>
        /// IP地址
        /// </summary>
        [IgnoreDataMember,JsonIgnore]
        public string _iPAddress;

        partial void OnIPAddressGet();

        partial void OnIPAddressSet(ref string value);

        partial void OnIPAddressSeted();

        
        /// <summary>
        /// IP地址
        /// </summary>
        /// <value>
        /// 可存储100个字符.合理长度应不大于100.
        /// </value>
        [DataRule(CanNull = true)]
        [DataMember , JsonProperty("IPAddress", NullValueHandling = NullValueHandling.Ignore) , DisplayName(@"IP地址")]
        public  string IPAddress
        {
            get
            {
                OnIPAddressGet();
                return this._iPAddress;
            }
            set
            {
                if(this._iPAddress == value)
                    return;
                OnIPAddressSet(ref value);
                this._iPAddress = value;
                OnIPAddressSeted();
                this.OnPropertyChanged(_DataStruct_.Real_IPAddress);
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
            case "orderid":
                this.OrderID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "orderidsubid":
                this.OrderIDSubID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "orderstate":
                this.OrderState = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "orderprice":
                this.OrderPrice = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "freightprice":
                this.FreightPrice = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "totleprice":
                this.TotlePrice = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "totleweight":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (float.TryParse(value, out var vl))
                    {
                        this.TotleWeight = vl;
                        return true;
                    }
                }
                return false;
            case "skulist":
                this.SkuList = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "ordertime":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.OrderTime = vl;
                        return true;
                    }
                }
                return false;
            case "ordername":
                this.OrderName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "orderphone":
                this.OrderPhone = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "ordertel":
                this.OrderTel = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "receivername":
                this.ReceiverName = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "receiverphone":
                this.ReceiverPhone = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "receivertel":
                this.ReceiverTel = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "receiverprovince":
                this.ReceiverProvince = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "receivercity":
                this.ReceiverCity = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "receiverarea":
                this.ReceiverArea = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "receiveraddress":
                this.ReceiverAddress = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "usermessage":
                this.UserMessage = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "paytime":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParse(value, out var vl))
                    {
                        this.PayTime = vl;
                        return true;
                    }
                }
                return false;
            case "tradetype":
                this.TradeType = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "tradeid":
                this.TradeID = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "waybillinfo":
                this.WaybillInfo = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "pricememo":
                this.PriceMemo = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "productinfo":
                this.ProductInfo = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "productskucount":
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (int.TryParse(value, out var vl))
                    {
                        this.ProductSkuCount = vl;
                        return true;
                    }
                }
                return false;
            case "orderfrom":
                this.OrderFrom = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "ordertype":
                this.OrderType = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "orderremark":
                this.OrderRemark = string.IsNullOrWhiteSpace(value) ? null : value;
                return true;
            case "ipaddress":
                this.IPAddress = string.IsNullOrWhiteSpace(value) ? null : value;
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
            case "sitesid":
                this.SiteSID = (long)Convert.ToDecimal(value);
                return;
            case "orgoid":
                this.OrgOID = (long)Convert.ToDecimal(value);
                return;
            case "useruid":
                this.UserUID = (long)Convert.ToDecimal(value);
                return;
            case "orderid":
                this.OrderID = value == null ? null : value.ToString();
                return;
            case "orderidsubid":
                this.OrderIDSubID = value == null ? null : value.ToString();
                return;
            case "orderstate":
                this.OrderState = value == null ? null : value.ToString();
                return;
            case "orderprice":
                this.OrderPrice = value == null ? null : value.ToString();
                return;
            case "freightprice":
                this.FreightPrice = value == null ? null : value.ToString();
                return;
            case "totleprice":
                this.TotlePrice = value == null ? null : value.ToString();
                return;
            case "totleweight":
                this.TotleWeight = Convert.ToSingle(value);
                return;
            case "skulist":
                this.SkuList = value == null ? null : value.ToString();
                return;
            case "ordertime":
                this.OrderTime = Convert.ToDateTime(value);
                return;
            case "ordername":
                this.OrderName = value == null ? null : value.ToString();
                return;
            case "orderphone":
                this.OrderPhone = value == null ? null : value.ToString();
                return;
            case "ordertel":
                this.OrderTel = value == null ? null : value.ToString();
                return;
            case "receivername":
                this.ReceiverName = value == null ? null : value.ToString();
                return;
            case "receiverphone":
                this.ReceiverPhone = value == null ? null : value.ToString();
                return;
            case "receivertel":
                this.ReceiverTel = value == null ? null : value.ToString();
                return;
            case "receiverprovince":
                this.ReceiverProvince = value == null ? null : value.ToString();
                return;
            case "receivercity":
                this.ReceiverCity = value == null ? null : value.ToString();
                return;
            case "receiverarea":
                this.ReceiverArea = value == null ? null : value.ToString();
                return;
            case "receiveraddress":
                this.ReceiverAddress = value == null ? null : value.ToString();
                return;
            case "usermessage":
                this.UserMessage = value == null ? null : value.ToString();
                return;
            case "paytime":
                this.PayTime = Convert.ToDateTime(value);
                return;
            case "tradetype":
                this.TradeType = value == null ? null : value.ToString();
                return;
            case "tradeid":
                this.TradeID = value == null ? null : value.ToString();
                return;
            case "waybillinfo":
                this.WaybillInfo = value == null ? null : value.ToString();
                return;
            case "pricememo":
                this.PriceMemo = value == null ? null : value.ToString();
                return;
            case "productinfo":
                this.ProductInfo = value == null ? null : value.ToString();
                return;
            case "productskusid":
                this.ProductSkuSID = (int)Convert.ToDecimal(value);
                return;
            case "productskucount":
                this.ProductSkuCount = (int)Convert.ToDecimal(value);
                return;
            case "orderfrom":
                this.OrderFrom = value == null ? null : value.ToString();
                return;
            case "ordertype":
                this.OrderType = value == null ? null : value.ToString();
                return;
            case "orderremark":
                this.OrderRemark = value == null ? null : value.ToString();
                return;
            case "ipaddress":
                this.IPAddress = value == null ? null : value.ToString();
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
            case _DataStruct_.SiteSID:
                this.SiteSID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrgOID:
                this.OrgOID = Convert.ToInt64(value);
                return;
            case _DataStruct_.UserUID:
                this.UserUID = Convert.ToInt64(value);
                return;
            case _DataStruct_.OrderID:
                this.OrderID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrderIDSubID:
                this.OrderIDSubID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrderState:
                this.OrderState = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrderPrice:
                this.OrderPrice = value == null ? null : value.ToString();
                return;
            case _DataStruct_.FreightPrice:
                this.FreightPrice = value == null ? null : value.ToString();
                return;
            case _DataStruct_.TotlePrice:
                this.TotlePrice = value == null ? null : value.ToString();
                return;
            case _DataStruct_.TotleWeight:
                this.TotleWeight = Convert.ToSingle(value);
                return;
            case _DataStruct_.SkuList:
                this.SkuList = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrderTime:
                this.OrderTime = Convert.ToDateTime(value);
                return;
            case _DataStruct_.OrderName:
                this.OrderName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrderPhone:
                this.OrderPhone = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrderTel:
                this.OrderTel = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ReceiverName:
                this.ReceiverName = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ReceiverPhone:
                this.ReceiverPhone = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ReceiverTel:
                this.ReceiverTel = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ReceiverProvince:
                this.ReceiverProvince = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ReceiverCity:
                this.ReceiverCity = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ReceiverArea:
                this.ReceiverArea = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ReceiverAddress:
                this.ReceiverAddress = value == null ? null : value.ToString();
                return;
            case _DataStruct_.UserMessage:
                this.UserMessage = value == null ? null : value.ToString();
                return;
            case _DataStruct_.PayTime:
                this.PayTime = Convert.ToDateTime(value);
                return;
            case _DataStruct_.TradeType:
                this.TradeType = value == null ? null : value.ToString();
                return;
            case _DataStruct_.TradeID:
                this.TradeID = value == null ? null : value.ToString();
                return;
            case _DataStruct_.WaybillInfo:
                this.WaybillInfo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.PriceMemo:
                this.PriceMemo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ProductInfo:
                this.ProductInfo = value == null ? null : value.ToString();
                return;
            case _DataStruct_.ProductSkuSID:
                this.ProductSkuSID = Convert.ToInt32(value);
                return;
            case _DataStruct_.ProductSkuCount:
                this.ProductSkuCount = Convert.ToInt32(value);
                return;
            case _DataStruct_.OrderFrom:
                this.OrderFrom = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrderType:
                this.OrderType = value == null ? null : value.ToString();
                return;
            case _DataStruct_.OrderRemark:
                this.OrderRemark = value == null ? null : value.ToString();
                return;
            case _DataStruct_.IPAddress:
                this.IPAddress = value == null ? null : value.ToString();
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
            case "sitesid":
                return this.SiteSID;
            case "orgoid":
                return this.OrgOID;
            case "useruid":
                return this.UserUID;
            case "orderid":
                return this.OrderID;
            case "orderidsubid":
                return this.OrderIDSubID;
            case "orderstate":
                return this.OrderState;
            case "orderprice":
                return this.OrderPrice;
            case "freightprice":
                return this.FreightPrice;
            case "totleprice":
                return this.TotlePrice;
            case "totleweight":
                return this.TotleWeight;
            case "skulist":
                return this.SkuList;
            case "ordertime":
                return this.OrderTime;
            case "ordername":
                return this.OrderName;
            case "orderphone":
                return this.OrderPhone;
            case "ordertel":
                return this.OrderTel;
            case "receivername":
                return this.ReceiverName;
            case "receiverphone":
                return this.ReceiverPhone;
            case "receivertel":
                return this.ReceiverTel;
            case "receiverprovince":
                return this.ReceiverProvince;
            case "receivercity":
                return this.ReceiverCity;
            case "receiverarea":
                return this.ReceiverArea;
            case "receiveraddress":
                return this.ReceiverAddress;
            case "usermessage":
                return this.UserMessage;
            case "paytime":
                return this.PayTime;
            case "tradetype":
                return this.TradeType;
            case "tradeid":
                return this.TradeID;
            case "waybillinfo":
                return this.WaybillInfo;
            case "pricememo":
                return this.PriceMemo;
            case "productinfo":
                return this.ProductInfo;
            case "productskusid":
                return this.ProductSkuSID;
            case "productskucount":
                return this.ProductSkuCount;
            case "orderfrom":
                return this.OrderFrom;
            case "ordertype":
                return this.OrderType;
            case "orderremark":
                return this.OrderRemark;
            case "ipaddress":
                return this.IPAddress;
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
                case _DataStruct_.SiteSID:
                    return this.SiteSID;
                case _DataStruct_.OrgOID:
                    return this.OrgOID;
                case _DataStruct_.UserUID:
                    return this.UserUID;
                case _DataStruct_.OrderID:
                    return this.OrderID;
                case _DataStruct_.OrderIDSubID:
                    return this.OrderIDSubID;
                case _DataStruct_.OrderState:
                    return this.OrderState;
                case _DataStruct_.OrderPrice:
                    return this.OrderPrice;
                case _DataStruct_.FreightPrice:
                    return this.FreightPrice;
                case _DataStruct_.TotlePrice:
                    return this.TotlePrice;
                case _DataStruct_.TotleWeight:
                    return this.TotleWeight;
                case _DataStruct_.SkuList:
                    return this.SkuList;
                case _DataStruct_.OrderTime:
                    return this.OrderTime;
                case _DataStruct_.OrderName:
                    return this.OrderName;
                case _DataStruct_.OrderPhone:
                    return this.OrderPhone;
                case _DataStruct_.OrderTel:
                    return this.OrderTel;
                case _DataStruct_.ReceiverName:
                    return this.ReceiverName;
                case _DataStruct_.ReceiverPhone:
                    return this.ReceiverPhone;
                case _DataStruct_.ReceiverTel:
                    return this.ReceiverTel;
                case _DataStruct_.ReceiverProvince:
                    return this.ReceiverProvince;
                case _DataStruct_.ReceiverCity:
                    return this.ReceiverCity;
                case _DataStruct_.ReceiverArea:
                    return this.ReceiverArea;
                case _DataStruct_.ReceiverAddress:
                    return this.ReceiverAddress;
                case _DataStruct_.UserMessage:
                    return this.UserMessage;
                case _DataStruct_.PayTime:
                    return this.PayTime;
                case _DataStruct_.TradeType:
                    return this.TradeType;
                case _DataStruct_.TradeID:
                    return this.TradeID;
                case _DataStruct_.WaybillInfo:
                    return this.WaybillInfo;
                case _DataStruct_.PriceMemo:
                    return this.PriceMemo;
                case _DataStruct_.ProductInfo:
                    return this.ProductInfo;
                case _DataStruct_.ProductSkuSID:
                    return this.ProductSkuSID;
                case _DataStruct_.ProductSkuCount:
                    return this.ProductSkuCount;
                case _DataStruct_.OrderFrom:
                    return this.OrderFrom;
                case _DataStruct_.OrderType:
                    return this.OrderType;
                case _DataStruct_.OrderRemark:
                    return this.OrderRemark;
                case _DataStruct_.IPAddress:
                    return this.IPAddress;
            }

            return null;
        }

        #endregion

        #region 复制
        

        partial void CopyExtendValue(UserOrderData source);

        /// <summary>
        /// 复制值
        /// </summary>
        /// <param name="source">复制的源字段</param>
        protected override void CopyValueInner(DataObjectBase source)
        {
            var sourceEntity = source as UserOrderData;
            if(sourceEntity == null)
                return;
            this._oID = sourceEntity._oID;
            this._siteSID = sourceEntity._siteSID;
            this._orgOID = sourceEntity._orgOID;
            this._userUID = sourceEntity._userUID;
            this._orderID = sourceEntity._orderID;
            this._orderIDSubID = sourceEntity._orderIDSubID;
            this._orderState = sourceEntity._orderState;
            this._orderPrice = sourceEntity._orderPrice;
            this._freightPrice = sourceEntity._freightPrice;
            this._totlePrice = sourceEntity._totlePrice;
            this._totleWeight = sourceEntity._totleWeight;
            this._skuList = sourceEntity._skuList;
            this._orderTime = sourceEntity._orderTime;
            this._orderName = sourceEntity._orderName;
            this._orderPhone = sourceEntity._orderPhone;
            this._orderTel = sourceEntity._orderTel;
            this._receiverName = sourceEntity._receiverName;
            this._receiverPhone = sourceEntity._receiverPhone;
            this._receiverTel = sourceEntity._receiverTel;
            this._receiverProvince = sourceEntity._receiverProvince;
            this._receiverCity = sourceEntity._receiverCity;
            this._receiverArea = sourceEntity._receiverArea;
            this._receiverAddress = sourceEntity._receiverAddress;
            this._userMessage = sourceEntity._userMessage;
            this._payTime = sourceEntity._payTime;
            this._tradeType = sourceEntity._tradeType;
            this._tradeID = sourceEntity._tradeID;
            this._waybillInfo = sourceEntity._waybillInfo;
            this._priceMemo = sourceEntity._priceMemo;
            this._productInfo = sourceEntity._productInfo;
            this._productSkuSID = sourceEntity._productSkuSID;
            this._productSkuCount = sourceEntity._productSkuCount;
            this._orderFrom = sourceEntity._orderFrom;
            this._orderType = sourceEntity._orderType;
            this._orderRemark = sourceEntity._orderRemark;
            this._iPAddress = sourceEntity._iPAddress;
            CopyExtendValue(sourceEntity);
            this.__EntityStatus.SetModified();
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="source">复制的源字段</param>
        public void Copy(UserOrderData source)
        {
                this.OID = source.OID;
                this.SiteSID = source.SiteSID;
                this.OrgOID = source.OrgOID;
                this.UserUID = source.UserUID;
                this.OrderID = source.OrderID;
                this.OrderIDSubID = source.OrderIDSubID;
                this.OrderState = source.OrderState;
                this.OrderPrice = source.OrderPrice;
                this.FreightPrice = source.FreightPrice;
                this.TotlePrice = source.TotlePrice;
                this.TotleWeight = source.TotleWeight;
                this.SkuList = source.SkuList;
                this.OrderTime = source.OrderTime;
                this.OrderName = source.OrderName;
                this.OrderPhone = source.OrderPhone;
                this.OrderTel = source.OrderTel;
                this.ReceiverName = source.ReceiverName;
                this.ReceiverPhone = source.ReceiverPhone;
                this.ReceiverTel = source.ReceiverTel;
                this.ReceiverProvince = source.ReceiverProvince;
                this.ReceiverCity = source.ReceiverCity;
                this.ReceiverArea = source.ReceiverArea;
                this.ReceiverAddress = source.ReceiverAddress;
                this.UserMessage = source.UserMessage;
                this.PayTime = source.PayTime;
                this.TradeType = source.TradeType;
                this.TradeID = source.TradeID;
                this.WaybillInfo = source.WaybillInfo;
                this.PriceMemo = source.PriceMemo;
                this.ProductInfo = source.ProductInfo;
                this.ProductSkuSID = source.ProductSkuSID;
                this.ProductSkuCount = source.ProductSkuCount;
                this.OrderFrom = source.OrderFrom;
                this.OrderType = source.OrderType;
                this.OrderRemark = source.OrderRemark;
                this.IPAddress = source.IPAddress;
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
                OnSiteSIDModified(subsist,false);
                OnOrgOIDModified(subsist,false);
                OnUserUIDModified(subsist,false);
                OnOrderIDModified(subsist,false);
                OnOrderIDSubIDModified(subsist,false);
                OnOrderStateModified(subsist,false);
                OnOrderPriceModified(subsist,false);
                OnFreightPriceModified(subsist,false);
                OnTotlePriceModified(subsist,false);
                OnTotleWeightModified(subsist,false);
                OnSkuListModified(subsist,false);
                OnOrderTimeModified(subsist,false);
                OnOrderNameModified(subsist,false);
                OnOrderPhoneModified(subsist,false);
                OnOrderTelModified(subsist,false);
                OnReceiverNameModified(subsist,false);
                OnReceiverPhoneModified(subsist,false);
                OnReceiverTelModified(subsist,false);
                OnReceiverProvinceModified(subsist,false);
                OnReceiverCityModified(subsist,false);
                OnReceiverAreaModified(subsist,false);
                OnReceiverAddressModified(subsist,false);
                OnUserMessageModified(subsist,false);
                OnPayTimeModified(subsist,false);
                OnTradeTypeModified(subsist,false);
                OnTradeIDModified(subsist,false);
                OnWaybillInfoModified(subsist,false);
                OnPriceMemoModified(subsist,false);
                OnProductInfoModified(subsist,false);
                OnProductSkuSIDModified(subsist,false);
                OnProductSkuCountModified(subsist,false);
                OnOrderFromModified(subsist,false);
                OnOrderTypeModified(subsist,false);
                OnOrderRemarkModified(subsist,false);
                OnIPAddressModified(subsist,false);
                return;
            }
            else if (subsist == EntitySubsist.Adding || subsist == EntitySubsist.Added)
            {
                OnOIDModified(subsist,true);
                OnSiteSIDModified(subsist,true);
                OnOrgOIDModified(subsist,true);
                OnUserUIDModified(subsist,true);
                OnOrderIDModified(subsist,true);
                OnOrderIDSubIDModified(subsist,true);
                OnOrderStateModified(subsist,true);
                OnOrderPriceModified(subsist,true);
                OnFreightPriceModified(subsist,true);
                OnTotlePriceModified(subsist,true);
                OnTotleWeightModified(subsist,true);
                OnSkuListModified(subsist,true);
                OnOrderTimeModified(subsist,true);
                OnOrderNameModified(subsist,true);
                OnOrderPhoneModified(subsist,true);
                OnOrderTelModified(subsist,true);
                OnReceiverNameModified(subsist,true);
                OnReceiverPhoneModified(subsist,true);
                OnReceiverTelModified(subsist,true);
                OnReceiverProvinceModified(subsist,true);
                OnReceiverCityModified(subsist,true);
                OnReceiverAreaModified(subsist,true);
                OnReceiverAddressModified(subsist,true);
                OnUserMessageModified(subsist,true);
                OnPayTimeModified(subsist,true);
                OnTradeTypeModified(subsist,true);
                OnTradeIDModified(subsist,true);
                OnWaybillInfoModified(subsist,true);
                OnPriceMemoModified(subsist,true);
                OnProductInfoModified(subsist,true);
                OnProductSkuSIDModified(subsist,true);
                OnProductSkuCountModified(subsist,true);
                OnOrderFromModified(subsist,true);
                OnOrderTypeModified(subsist,true);
                OnOrderRemarkModified(subsist,true);
                OnIPAddressModified(subsist,true);
                return;
            }
            else if(modifieds != null && modifieds[36] > 0)
            {
                OnOIDModified(subsist,modifieds[_DataStruct_.Real_OID] == 1);
                OnSiteSIDModified(subsist,modifieds[_DataStruct_.Real_SiteSID] == 1);
                OnOrgOIDModified(subsist,modifieds[_DataStruct_.Real_OrgOID] == 1);
                OnUserUIDModified(subsist,modifieds[_DataStruct_.Real_UserUID] == 1);
                OnOrderIDModified(subsist,modifieds[_DataStruct_.Real_OrderID] == 1);
                OnOrderIDSubIDModified(subsist,modifieds[_DataStruct_.Real_OrderIDSubID] == 1);
                OnOrderStateModified(subsist,modifieds[_DataStruct_.Real_OrderState] == 1);
                OnOrderPriceModified(subsist,modifieds[_DataStruct_.Real_OrderPrice] == 1);
                OnFreightPriceModified(subsist,modifieds[_DataStruct_.Real_FreightPrice] == 1);
                OnTotlePriceModified(subsist,modifieds[_DataStruct_.Real_TotlePrice] == 1);
                OnTotleWeightModified(subsist,modifieds[_DataStruct_.Real_TotleWeight] == 1);
                OnSkuListModified(subsist,modifieds[_DataStruct_.Real_SkuList] == 1);
                OnOrderTimeModified(subsist,modifieds[_DataStruct_.Real_OrderTime] == 1);
                OnOrderNameModified(subsist,modifieds[_DataStruct_.Real_OrderName] == 1);
                OnOrderPhoneModified(subsist,modifieds[_DataStruct_.Real_OrderPhone] == 1);
                OnOrderTelModified(subsist,modifieds[_DataStruct_.Real_OrderTel] == 1);
                OnReceiverNameModified(subsist,modifieds[_DataStruct_.Real_ReceiverName] == 1);
                OnReceiverPhoneModified(subsist,modifieds[_DataStruct_.Real_ReceiverPhone] == 1);
                OnReceiverTelModified(subsist,modifieds[_DataStruct_.Real_ReceiverTel] == 1);
                OnReceiverProvinceModified(subsist,modifieds[_DataStruct_.Real_ReceiverProvince] == 1);
                OnReceiverCityModified(subsist,modifieds[_DataStruct_.Real_ReceiverCity] == 1);
                OnReceiverAreaModified(subsist,modifieds[_DataStruct_.Real_ReceiverArea] == 1);
                OnReceiverAddressModified(subsist,modifieds[_DataStruct_.Real_ReceiverAddress] == 1);
                OnUserMessageModified(subsist,modifieds[_DataStruct_.Real_UserMessage] == 1);
                OnPayTimeModified(subsist,modifieds[_DataStruct_.Real_PayTime] == 1);
                OnTradeTypeModified(subsist,modifieds[_DataStruct_.Real_TradeType] == 1);
                OnTradeIDModified(subsist,modifieds[_DataStruct_.Real_TradeID] == 1);
                OnWaybillInfoModified(subsist,modifieds[_DataStruct_.Real_WaybillInfo] == 1);
                OnPriceMemoModified(subsist,modifieds[_DataStruct_.Real_PriceMemo] == 1);
                OnProductInfoModified(subsist,modifieds[_DataStruct_.Real_ProductInfo] == 1);
                OnProductSkuSIDModified(subsist,modifieds[_DataStruct_.Real_ProductSkuSID] == 1);
                OnProductSkuCountModified(subsist,modifieds[_DataStruct_.Real_ProductSkuCount] == 1);
                OnOrderFromModified(subsist,modifieds[_DataStruct_.Real_OrderFrom] == 1);
                OnOrderTypeModified(subsist,modifieds[_DataStruct_.Real_OrderType] == 1);
                OnOrderRemarkModified(subsist,modifieds[_DataStruct_.Real_OrderRemark] == 1);
                OnIPAddressModified(subsist,modifieds[_DataStruct_.Real_IPAddress] == 1);
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
        /// 组织标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserUIDModified(EntitySubsist subsist,bool isModified);

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
        /// 订单标识Sub标识修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrderIDSubIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 翻转状态修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrderStateModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 最高价格修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrderPriceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 货运价格修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnFreightPriceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 总价格修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTotlePriceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 总重量修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTotleWeightModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// SKU列表修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnSkuListModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 订单Time修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrderTimeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 订单Name修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrderNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 公用电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrderPhoneModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 自动电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrderTelModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 接收方名称修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnReceiverNameModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 接收机电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnReceiverPhoneModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 接收机电话修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnReceiverTelModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 接收省修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnReceiverProvinceModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 接收城市修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnReceiverCityModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 接收区修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnReceiverAreaModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 收件人地址修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnReceiverAddressModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 用户消息修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnUserMessageModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 支付时间修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPayTimeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 贸易壁垒修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTradeTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 贸易壁垒修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnTradeIDModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 货运单信息修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnWaybillInfoModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 价格备忘录修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnPriceMemoModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 产品信息修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnProductInfoModified(EntitySubsist subsist,bool isModified);

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
        /// 产品SKU计数修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnProductSkuCountModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 订单From修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrderFromModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 订单类型修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrderTypeModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// 订单备注修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnOrderRemarkModified(EntitySubsist subsist,bool isModified);

        /// <summary>
        /// IP地址修改的后期处理(保存前)
        /// </summary>
        /// <param name="subsist">当前对象状态</param>
        /// <param name="isModified">是否被修改</param>
        /// <remarks>
        /// 对关联的属性的更改,请自行保存,否则可能丢失
        /// </remarks>
        partial void OnIPAddressModified(EntitySubsist subsist,bool isModified);
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
            public const string EntityName = @"UserOrder";
            /// <summary>
            /// 实体标题
            /// </summary>
            public const string EntityCaption = @"用户端";
            /// <summary>
            /// 实体说明
            /// </summary>
            public const string EntityDescription = @"用户端";
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
            /// 组织标识的数字标识
            /// </summary>
            public const byte UserUID = 4;
            
            /// <summary>
            /// 组织标识的实时记录顺序
            /// </summary>
            public const int Real_UserUID = 3;

            /// <summary>
            /// 订单标识的数字标识
            /// </summary>
            public const byte OrderID = 5;
            
            /// <summary>
            /// 订单标识的实时记录顺序
            /// </summary>
            public const int Real_OrderID = 4;

            /// <summary>
            /// 订单标识Sub标识的数字标识
            /// </summary>
            public const byte OrderIDSubID = 6;
            
            /// <summary>
            /// 订单标识Sub标识的实时记录顺序
            /// </summary>
            public const int Real_OrderIDSubID = 5;

            /// <summary>
            /// 翻转状态的数字标识
            /// </summary>
            public const byte OrderState = 7;
            
            /// <summary>
            /// 翻转状态的实时记录顺序
            /// </summary>
            public const int Real_OrderState = 6;

            /// <summary>
            /// 最高价格的数字标识
            /// </summary>
            public const byte OrderPrice = 8;
            
            /// <summary>
            /// 最高价格的实时记录顺序
            /// </summary>
            public const int Real_OrderPrice = 7;

            /// <summary>
            /// 货运价格的数字标识
            /// </summary>
            public const byte FreightPrice = 9;
            
            /// <summary>
            /// 货运价格的实时记录顺序
            /// </summary>
            public const int Real_FreightPrice = 8;

            /// <summary>
            /// 总价格的数字标识
            /// </summary>
            public const byte TotlePrice = 10;
            
            /// <summary>
            /// 总价格的实时记录顺序
            /// </summary>
            public const int Real_TotlePrice = 9;

            /// <summary>
            /// 总重量的数字标识
            /// </summary>
            public const byte TotleWeight = 11;
            
            /// <summary>
            /// 总重量的实时记录顺序
            /// </summary>
            public const int Real_TotleWeight = 10;

            /// <summary>
            /// SKU列表的数字标识
            /// </summary>
            public const byte SkuList = 12;
            
            /// <summary>
            /// SKU列表的实时记录顺序
            /// </summary>
            public const int Real_SkuList = 11;

            /// <summary>
            /// 订单Time的数字标识
            /// </summary>
            public const byte OrderTime = 13;
            
            /// <summary>
            /// 订单Time的实时记录顺序
            /// </summary>
            public const int Real_OrderTime = 12;

            /// <summary>
            /// 订单Name的数字标识
            /// </summary>
            public const byte OrderName = 14;
            
            /// <summary>
            /// 订单Name的实时记录顺序
            /// </summary>
            public const int Real_OrderName = 13;

            /// <summary>
            /// 公用电话的数字标识
            /// </summary>
            public const byte OrderPhone = 15;
            
            /// <summary>
            /// 公用电话的实时记录顺序
            /// </summary>
            public const int Real_OrderPhone = 14;

            /// <summary>
            /// 自动电话的数字标识
            /// </summary>
            public const byte OrderTel = 16;
            
            /// <summary>
            /// 自动电话的实时记录顺序
            /// </summary>
            public const int Real_OrderTel = 15;

            /// <summary>
            /// 接收方名称的数字标识
            /// </summary>
            public const byte ReceiverName = 17;
            
            /// <summary>
            /// 接收方名称的实时记录顺序
            /// </summary>
            public const int Real_ReceiverName = 16;

            /// <summary>
            /// 接收机电话的数字标识
            /// </summary>
            public const byte ReceiverPhone = 18;
            
            /// <summary>
            /// 接收机电话的实时记录顺序
            /// </summary>
            public const int Real_ReceiverPhone = 17;

            /// <summary>
            /// 接收机电话的数字标识
            /// </summary>
            public const byte ReceiverTel = 19;
            
            /// <summary>
            /// 接收机电话的实时记录顺序
            /// </summary>
            public const int Real_ReceiverTel = 18;

            /// <summary>
            /// 接收省的数字标识
            /// </summary>
            public const byte ReceiverProvince = 20;
            
            /// <summary>
            /// 接收省的实时记录顺序
            /// </summary>
            public const int Real_ReceiverProvince = 19;

            /// <summary>
            /// 接收城市的数字标识
            /// </summary>
            public const byte ReceiverCity = 21;
            
            /// <summary>
            /// 接收城市的实时记录顺序
            /// </summary>
            public const int Real_ReceiverCity = 20;

            /// <summary>
            /// 接收区的数字标识
            /// </summary>
            public const byte ReceiverArea = 22;
            
            /// <summary>
            /// 接收区的实时记录顺序
            /// </summary>
            public const int Real_ReceiverArea = 21;

            /// <summary>
            /// 收件人地址的数字标识
            /// </summary>
            public const byte ReceiverAddress = 23;
            
            /// <summary>
            /// 收件人地址的实时记录顺序
            /// </summary>
            public const int Real_ReceiverAddress = 22;

            /// <summary>
            /// 用户消息的数字标识
            /// </summary>
            public const byte UserMessage = 24;
            
            /// <summary>
            /// 用户消息的实时记录顺序
            /// </summary>
            public const int Real_UserMessage = 23;

            /// <summary>
            /// 支付时间的数字标识
            /// </summary>
            public const byte PayTime = 25;
            
            /// <summary>
            /// 支付时间的实时记录顺序
            /// </summary>
            public const int Real_PayTime = 24;

            /// <summary>
            /// 贸易壁垒的数字标识
            /// </summary>
            public const byte TradeType = 26;
            
            /// <summary>
            /// 贸易壁垒的实时记录顺序
            /// </summary>
            public const int Real_TradeType = 25;

            /// <summary>
            /// 贸易壁垒的数字标识
            /// </summary>
            public const byte TradeID = 27;
            
            /// <summary>
            /// 贸易壁垒的实时记录顺序
            /// </summary>
            public const int Real_TradeID = 26;

            /// <summary>
            /// 货运单信息的数字标识
            /// </summary>
            public const byte WaybillInfo = 28;
            
            /// <summary>
            /// 货运单信息的实时记录顺序
            /// </summary>
            public const int Real_WaybillInfo = 27;

            /// <summary>
            /// 价格备忘录的数字标识
            /// </summary>
            public const byte PriceMemo = 29;
            
            /// <summary>
            /// 价格备忘录的实时记录顺序
            /// </summary>
            public const int Real_PriceMemo = 28;

            /// <summary>
            /// 产品信息的数字标识
            /// </summary>
            public const byte ProductInfo = 30;
            
            /// <summary>
            /// 产品信息的实时记录顺序
            /// </summary>
            public const int Real_ProductInfo = 29;

            /// <summary>
            /// 产品SKU站点标识的数字标识
            /// </summary>
            public const byte ProductSkuSID = 31;
            
            /// <summary>
            /// 产品SKU站点标识的实时记录顺序
            /// </summary>
            public const int Real_ProductSkuSID = 30;

            /// <summary>
            /// 产品SKU计数的数字标识
            /// </summary>
            public const byte ProductSkuCount = 32;
            
            /// <summary>
            /// 产品SKU计数的实时记录顺序
            /// </summary>
            public const int Real_ProductSkuCount = 31;

            /// <summary>
            /// 订单From的数字标识
            /// </summary>
            public const byte OrderFrom = 33;
            
            /// <summary>
            /// 订单From的实时记录顺序
            /// </summary>
            public const int Real_OrderFrom = 32;

            /// <summary>
            /// 订单类型的数字标识
            /// </summary>
            public const byte OrderType = 34;
            
            /// <summary>
            /// 订单类型的实时记录顺序
            /// </summary>
            public const int Real_OrderType = 33;

            /// <summary>
            /// 订单备注的数字标识
            /// </summary>
            public const byte OrderRemark = 35;
            
            /// <summary>
            /// 订单备注的实时记录顺序
            /// </summary>
            public const int Real_OrderRemark = 34;

            /// <summary>
            /// IP地址的数字标识
            /// </summary>
            public const byte IPAddress = 36;
            
            /// <summary>
            /// IP地址的实时记录顺序
            /// </summary>
            public const int Real_IPAddress = 35;

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
                        Real_OrderIDSubID,
                        new PropertySturct
                        {
                            Index        = OrderIDSubID,
                            Name         = "OrderIDSubID",
                            Title        = "订单标识Sub标识",
                            Caption      = @"订单标识Sub标识",
                            Description  = @"订单标识Sub标识",
                            ColumnName   = "OrderIDSubID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrderState,
                        new PropertySturct
                        {
                            Index        = OrderState,
                            Name         = "OrderState",
                            Title        = "翻转状态",
                            Caption      = @"翻转状态",
                            Description  = @"翻转状态",
                            ColumnName   = "OrderState",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrderPrice,
                        new PropertySturct
                        {
                            Index        = OrderPrice,
                            Name         = "OrderPrice",
                            Title        = "最高价格",
                            Caption      = @"最高价格",
                            Description  = @"最高价格",
                            ColumnName   = "OrderPrice",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_FreightPrice,
                        new PropertySturct
                        {
                            Index        = FreightPrice,
                            Name         = "FreightPrice",
                            Title        = "货运价格",
                            Caption      = @"货运价格",
                            Description  = @"货运价格",
                            ColumnName   = "FreightPrice",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_TotlePrice,
                        new PropertySturct
                        {
                            Index        = TotlePrice,
                            Name         = "TotlePrice",
                            Title        = "总价格",
                            Caption      = @"总价格",
                            Description  = @"总价格",
                            ColumnName   = "TotlePrice",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_TotleWeight,
                        new PropertySturct
                        {
                            Index        = TotleWeight,
                            Name         = "TotleWeight",
                            Title        = "总重量",
                            Caption      = @"总重量",
                            Description  = @"总重量",
                            ColumnName   = "TotleWeight",
                            PropertyType = typeof(float),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_SkuList,
                        new PropertySturct
                        {
                            Index        = SkuList,
                            Name         = "SkuList",
                            Title        = "SKU列表",
                            Caption      = @"SKU列表",
                            Description  = @"SKU列表",
                            ColumnName   = "SkuList",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrderTime,
                        new PropertySturct
                        {
                            Index        = OrderTime,
                            Name         = "OrderTime",
                            Title        = "订单Time",
                            Caption      = @"订单Time",
                            Description  = @"订单Time",
                            ColumnName   = "OrderTime",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrderName,
                        new PropertySturct
                        {
                            Index        = OrderName,
                            Name         = "OrderName",
                            Title        = "订单Name",
                            Caption      = @"订单Name",
                            Description  = @"订单Name",
                            ColumnName   = "OrderName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrderPhone,
                        new PropertySturct
                        {
                            Index        = OrderPhone,
                            Name         = "OrderPhone",
                            Title        = "公用电话",
                            Caption      = @"公用电话",
                            Description  = @"公用电话",
                            ColumnName   = "OrderPhone",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrderTel,
                        new PropertySturct
                        {
                            Index        = OrderTel,
                            Name         = "OrderTel",
                            Title        = "自动电话",
                            Caption      = @"自动电话",
                            Description  = @"自动电话",
                            ColumnName   = "OrderTel",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ReceiverName,
                        new PropertySturct
                        {
                            Index        = ReceiverName,
                            Name         = "ReceiverName",
                            Title        = "接收方名称",
                            Caption      = @"接收方名称",
                            Description  = @"接收方名称",
                            ColumnName   = "ReceiverName",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ReceiverPhone,
                        new PropertySturct
                        {
                            Index        = ReceiverPhone,
                            Name         = "ReceiverPhone",
                            Title        = "接收机电话",
                            Caption      = @"接收机电话",
                            Description  = @"接收机电话",
                            ColumnName   = "ReceiverPhone",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ReceiverTel,
                        new PropertySturct
                        {
                            Index        = ReceiverTel,
                            Name         = "ReceiverTel",
                            Title        = "接收机电话",
                            Caption      = @"接收机电话",
                            Description  = @"接收机电话",
                            ColumnName   = "ReceiverTel",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ReceiverProvince,
                        new PropertySturct
                        {
                            Index        = ReceiverProvince,
                            Name         = "ReceiverProvince",
                            Title        = "接收省",
                            Caption      = @"接收省",
                            Description  = @"接收省",
                            ColumnName   = "ReceiverProvince",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ReceiverCity,
                        new PropertySturct
                        {
                            Index        = ReceiverCity,
                            Name         = "ReceiverCity",
                            Title        = "接收城市",
                            Caption      = @"接收城市",
                            Description  = @"接收城市",
                            ColumnName   = "ReceiverCity",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ReceiverArea,
                        new PropertySturct
                        {
                            Index        = ReceiverArea,
                            Name         = "ReceiverArea",
                            Title        = "接收区",
                            Caption      = @"接收区",
                            Description  = @"接收区",
                            ColumnName   = "ReceiverArea",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ReceiverAddress,
                        new PropertySturct
                        {
                            Index        = ReceiverAddress,
                            Name         = "ReceiverAddress",
                            Title        = "收件人地址",
                            Caption      = @"收件人地址",
                            Description  = @"收件人地址",
                            ColumnName   = "ReceiverAddress",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_UserMessage,
                        new PropertySturct
                        {
                            Index        = UserMessage,
                            Name         = "UserMessage",
                            Title        = "用户消息",
                            Caption      = @"用户消息",
                            Description  = @"用户消息",
                            ColumnName   = "UserMessage",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PayTime,
                        new PropertySturct
                        {
                            Index        = PayTime,
                            Name         = "PayTime",
                            Title        = "支付时间",
                            Caption      = @"支付时间",
                            Description  = @"支付时间",
                            ColumnName   = "PayTime",
                            PropertyType = typeof(DateTime),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_TradeType,
                        new PropertySturct
                        {
                            Index        = TradeType,
                            Name         = "TradeType",
                            Title        = "贸易壁垒",
                            Caption      = @"贸易壁垒",
                            Description  = @"贸易壁垒",
                            ColumnName   = "TradeType",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_TradeID,
                        new PropertySturct
                        {
                            Index        = TradeID,
                            Name         = "TradeID",
                            Title        = "贸易壁垒",
                            Caption      = @"贸易壁垒",
                            Description  = @"贸易壁垒",
                            ColumnName   = "TradeID",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_WaybillInfo,
                        new PropertySturct
                        {
                            Index        = WaybillInfo,
                            Name         = "WaybillInfo",
                            Title        = "货运单信息",
                            Caption      = @"货运单信息",
                            Description  = @"货运单信息",
                            ColumnName   = "WaybillInfo",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_PriceMemo,
                        new PropertySturct
                        {
                            Index        = PriceMemo,
                            Name         = "PriceMemo",
                            Title        = "价格备忘录",
                            Caption      = @"价格备忘录",
                            Description  = @"价格备忘录",
                            ColumnName   = "PriceMemo",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_ProductInfo,
                        new PropertySturct
                        {
                            Index        = ProductInfo,
                            Name         = "ProductInfo",
                            Title        = "产品信息",
                            Caption      = @"产品信息",
                            Description  = @"产品信息",
                            ColumnName   = "ProductInfo",
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
                        Real_ProductSkuCount,
                        new PropertySturct
                        {
                            Index        = ProductSkuCount,
                            Name         = "ProductSkuCount",
                            Title        = "产品SKU计数",
                            Caption      = @"产品SKU计数",
                            Description  = @"产品SKU计数",
                            ColumnName   = "ProductSkuCount",
                            PropertyType = typeof(int),
                            CanNull      = false,
                            ValueType    = PropertyValueType.Value,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrderFrom,
                        new PropertySturct
                        {
                            Index        = OrderFrom,
                            Name         = "OrderFrom",
                            Title        = "订单From",
                            Caption      = @"订单From",
                            Description  = @"订单From",
                            ColumnName   = "OrderFrom",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrderType,
                        new PropertySturct
                        {
                            Index        = OrderType,
                            Name         = "OrderType",
                            Title        = "订单类型",
                            Caption      = @"订单类型",
                            Description  = @"订单类型",
                            ColumnName   = "OrderType",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_OrderRemark,
                        new PropertySturct
                        {
                            Index        = OrderRemark,
                            Name         = "OrderRemark",
                            Title        = "订单备注",
                            Caption      = @"订单备注",
                            Description  = @"订单备注",
                            ColumnName   = "OrderRemark",
                            PropertyType = typeof(string),
                            CanNull      = false,
                            ValueType    = PropertyValueType.String,
                            CanImport    = false,
                            CanExport    = false
                        }
                    },
                    {
                        Real_IPAddress,
                        new PropertySturct
                        {
                            Index        = IPAddress,
                            Name         = "IPAddress",
                            Title        = "IP地址",
                            Caption      = @"IP地址",
                            Description  = @"IP地址",
                            ColumnName   = "IPAddress",
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