/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:52*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;
using Agebull.Common;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.SqlServer;
using Agebull.EntityModel.Redis;
using Agebull.EntityModel.EasyUI;



namespace HPC.Projects.DataAccess
{
    /// <summary>
    /// 用户端
    /// </summary>
    public partial class UserOrderDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_UserOrder;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbUserOrder";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbUserOrder";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"OID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [OID] AS [OID],
    [SiteSID] AS [SiteSID],
    [OrgOID] AS [OrgOID],
    [UserUID] AS [UserUID],
    [OrderID] AS [OrderID],
    [OrderIDSubID] AS [OrderIDSubID],
    [OrderState] AS [OrderState],
    [OrderPrice] AS [OrderPrice],
    [FreightPrice] AS [FreightPrice],
    [TotlePrice] AS [TotlePrice],
    [TotleWeight] AS [TotleWeight],
    [SkuList] AS [SkuList],
    [OrderTime] AS [OrderTime],
    [OrderName] AS [OrderName],
    [OrderPhone] AS [OrderPhone],
    [OrderTel] AS [OrderTel],
    [ReceiverName] AS [ReceiverName],
    [ReceiverPhone] AS [ReceiverPhone],
    [ReceiverTel] AS [ReceiverTel],
    [ReceiverProvince] AS [ReceiverProvince],
    [ReceiverCity] AS [ReceiverCity],
    [ReceiverArea] AS [ReceiverArea],
    [ReceiverAddress] AS [ReceiverAddress],
    [UserMessage] AS [UserMessage],
    [PayTime] AS [PayTime],
    [TradeType] AS [TradeType],
    [TradeID] AS [TradeID],
    [WaybillInfo] AS [WaybillInfo],
    [PriceMemo] AS [PriceMemo],
    [ProductInfo] AS [ProductInfo],
    [ProductSkuSID] AS [ProductSkuSID],
    [ProductSkuCount] AS [ProductSkuCount],
    [OrderFrom] AS [OrderFrom],
    [OrderType] AS [OrderType],
    [OrderRemark] AS [OrderRemark],
    [IPAddress] AS [IPAddress]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [OID],
    [SiteSID],
    [OrgOID],
    [UserUID],
    [OrderID],
    [OrderIDSubID],
    [OrderState],
    [OrderPrice],
    [FreightPrice],
    [TotlePrice],
    [TotleWeight],
    [SkuList],
    [OrderTime],
    [OrderName],
    [OrderPhone],
    [OrderTel],
    [ReceiverName],
    [ReceiverPhone],
    [ReceiverTel],
    [ReceiverProvince],
    [ReceiverCity],
    [ReceiverArea],
    [ReceiverAddress],
    [UserMessage],
    [PayTime],
    [TradeType],
    [TradeID],
    [WaybillInfo],
    [PriceMemo],
    [ProductInfo],
    [ProductSkuSID],
    [ProductSkuCount],
    [OrderFrom],
    [OrderType],
    [OrderRemark],
    [IPAddress]
)
VALUES
(
    @OID,
    @SiteSID,
    @OrgOID,
    @UserUID,
    @OrderID,
    @OrderIDSubID,
    @OrderState,
    @OrderPrice,
    @FreightPrice,
    @TotlePrice,
    @TotleWeight,
    @SkuList,
    @OrderTime,
    @OrderName,
    @OrderPhone,
    @OrderTel,
    @ReceiverName,
    @ReceiverPhone,
    @ReceiverTel,
    @ReceiverProvince,
    @ReceiverCity,
    @ReceiverArea,
    @ReceiverAddress,
    @UserMessage,
    @PayTime,
    @TradeType,
    @TradeID,
    @WaybillInfo,
    @PriceMemo,
    @ProductInfo,
    @ProductSkuSID,
    @ProductSkuCount,
    @OrderFrom,
    @OrderType,
    @OrderRemark,
    @IPAddress
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [OID] = @OID,
       [SiteSID] = @SiteSID,
       [OrgOID] = @OrgOID,
       [UserUID] = @UserUID,
       [OrderID] = @OrderID,
       [OrderIDSubID] = @OrderIDSubID,
       [OrderState] = @OrderState,
       [OrderPrice] = @OrderPrice,
       [FreightPrice] = @FreightPrice,
       [TotlePrice] = @TotlePrice,
       [TotleWeight] = @TotleWeight,
       [SkuList] = @SkuList,
       [OrderTime] = @OrderTime,
       [OrderName] = @OrderName,
       [OrderPhone] = @OrderPhone,
       [OrderTel] = @OrderTel,
       [ReceiverName] = @ReceiverName,
       [ReceiverPhone] = @ReceiverPhone,
       [ReceiverTel] = @ReceiverTel,
       [ReceiverProvince] = @ReceiverProvince,
       [ReceiverCity] = @ReceiverCity,
       [ReceiverArea] = @ReceiverArea,
       [ReceiverAddress] = @ReceiverAddress,
       [UserMessage] = @UserMessage,
       [PayTime] = @PayTime,
       [TradeType] = @TradeType,
       [TradeID] = @TradeID,
       [WaybillInfo] = @WaybillInfo,
       [PriceMemo] = @PriceMemo,
       [ProductInfo] = @ProductInfo,
       [ProductSkuSID] = @ProductSkuSID,
       [ProductSkuCount] = @ProductSkuCount,
       [OrderFrom] = @OrderFrom,
       [OrderType] = @OrderType,
       [OrderRemark] = @OrderRemark,
       [IPAddress] = @IPAddress
 WHERE [OID] = @OID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(UserOrderData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_OID] > 0)
                sql.AppendLine("       [OID] = @OID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_OrgOID] > 0)
                sql.AppendLine("       [OrgOID] = @OrgOID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_UserUID] > 0)
                sql.AppendLine("       [UserUID] = @UserUID");
            //订单标识
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_OrderID] > 0)
                sql.AppendLine("       [OrderID] = @OrderID");
            //订单标识Sub标识
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_OrderIDSubID] > 0)
                sql.AppendLine("       [OrderIDSubID] = @OrderIDSubID");
            //翻转状态
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_OrderState] > 0)
                sql.AppendLine("       [OrderState] = @OrderState");
            //最高价格
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_OrderPrice] > 0)
                sql.AppendLine("       [OrderPrice] = @OrderPrice");
            //货运价格
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_FreightPrice] > 0)
                sql.AppendLine("       [FreightPrice] = @FreightPrice");
            //总价格
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_TotlePrice] > 0)
                sql.AppendLine("       [TotlePrice] = @TotlePrice");
            //总重量
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_TotleWeight] > 0)
                sql.AppendLine("       [TotleWeight] = @TotleWeight");
            //SKU列表
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_SkuList] > 0)
                sql.AppendLine("       [SkuList] = @SkuList");
            //订单Time
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_OrderTime] > 0)
                sql.AppendLine("       [OrderTime] = @OrderTime");
            //订单Name
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_OrderName] > 0)
                sql.AppendLine("       [OrderName] = @OrderName");
            //公用电话
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_OrderPhone] > 0)
                sql.AppendLine("       [OrderPhone] = @OrderPhone");
            //自动电话
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_OrderTel] > 0)
                sql.AppendLine("       [OrderTel] = @OrderTel");
            //接收方名称
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_ReceiverName] > 0)
                sql.AppendLine("       [ReceiverName] = @ReceiverName");
            //接收机电话
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_ReceiverPhone] > 0)
                sql.AppendLine("       [ReceiverPhone] = @ReceiverPhone");
            //接收机电话
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_ReceiverTel] > 0)
                sql.AppendLine("       [ReceiverTel] = @ReceiverTel");
            //接收省
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_ReceiverProvince] > 0)
                sql.AppendLine("       [ReceiverProvince] = @ReceiverProvince");
            //接收城市
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_ReceiverCity] > 0)
                sql.AppendLine("       [ReceiverCity] = @ReceiverCity");
            //接收区
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_ReceiverArea] > 0)
                sql.AppendLine("       [ReceiverArea] = @ReceiverArea");
            //收件人地址
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_ReceiverAddress] > 0)
                sql.AppendLine("       [ReceiverAddress] = @ReceiverAddress");
            //用户消息
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_UserMessage] > 0)
                sql.AppendLine("       [UserMessage] = @UserMessage");
            //支付时间
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_PayTime] > 0)
                sql.AppendLine("       [PayTime] = @PayTime");
            //贸易壁垒
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_TradeType] > 0)
                sql.AppendLine("       [TradeType] = @TradeType");
            //贸易壁垒
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_TradeID] > 0)
                sql.AppendLine("       [TradeID] = @TradeID");
            //货运单信息
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_WaybillInfo] > 0)
                sql.AppendLine("       [WaybillInfo] = @WaybillInfo");
            //价格备忘录
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_PriceMemo] > 0)
                sql.AppendLine("       [PriceMemo] = @PriceMemo");
            //产品信息
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_ProductInfo] > 0)
                sql.AppendLine("       [ProductInfo] = @ProductInfo");
            //产品SKU站点标识
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_ProductSkuSID] > 0)
                sql.AppendLine("       [ProductSkuSID] = @ProductSkuSID");
            //产品SKU计数
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_ProductSkuCount] > 0)
                sql.AppendLine("       [ProductSkuCount] = @ProductSkuCount");
            //订单From
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_OrderFrom] > 0)
                sql.AppendLine("       [OrderFrom] = @OrderFrom");
            //订单类型
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_OrderType] > 0)
                sql.AppendLine("       [OrderType] = @OrderType");
            //订单备注
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_OrderRemark] > 0)
                sql.AppendLine("       [OrderRemark] = @OrderRemark");
            //IP地址
            if (data.__EntityStatus.ModifiedProperties[UserOrderData._DataStruct_.Real_IPAddress] > 0)
                sql.AppendLine("       [IPAddress] = @IPAddress");
            sql.Append(" WHERE [OID] = @OID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "OID","SiteSID","OrgOID","UserUID","OrderID","OrderIDSubID","OrderState","OrderPrice","FreightPrice","TotlePrice","TotleWeight","SkuList","OrderTime","OrderName","OrderPhone","OrderTel","ReceiverName","ReceiverPhone","ReceiverTel","ReceiverProvince","ReceiverCity","ReceiverArea","ReceiverAddress","UserMessage","PayTime","TradeType","TradeID","WaybillInfo","PriceMemo","ProductInfo","ProductSkuSID","ProductSkuCount","OrderFrom","OrderType","OrderRemark","IPAddress" };

        /// <summary>
        ///  所有字段
        /// </summary>
        public sealed override string[] Fields
        {
            get
            {
                return _fields;
            }
        }

        /// <summary>
        ///  字段字典
        /// </summary>
        public static Dictionary<string, string> fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "OID" , "OID" },
            { "SiteSID" , "SiteSID" },
            { "OrgOID" , "OrgOID" },
            { "UserUID" , "UserUID" },
            { "OrderID" , "OrderID" },
            { "OrderIDSubID" , "OrderIDSubID" },
            { "OrderState" , "OrderState" },
            { "OrderPrice" , "OrderPrice" },
            { "FreightPrice" , "FreightPrice" },
            { "TotlePrice" , "TotlePrice" },
            { "TotleWeight" , "TotleWeight" },
            { "SkuList" , "SkuList" },
            { "OrderTime" , "OrderTime" },
            { "OrderName" , "OrderName" },
            { "OrderPhone" , "OrderPhone" },
            { "OrderTel" , "OrderTel" },
            { "ReceiverName" , "ReceiverName" },
            { "ReceiverPhone" , "ReceiverPhone" },
            { "ReceiverTel" , "ReceiverTel" },
            { "ReceiverProvince" , "ReceiverProvince" },
            { "ReceiverCity" , "ReceiverCity" },
            { "ReceiverArea" , "ReceiverArea" },
            { "ReceiverAddress" , "ReceiverAddress" },
            { "UserMessage" , "UserMessage" },
            { "PayTime" , "PayTime" },
            { "TradeType" , "TradeType" },
            { "TradeID" , "TradeID" },
            { "WaybillInfo" , "WaybillInfo" },
            { "PriceMemo" , "PriceMemo" },
            { "ProductInfo" , "ProductInfo" },
            { "ProductSkuSID" , "ProductSkuSID" },
            { "ProductSkuCount" , "ProductSkuCount" },
            { "OrderFrom" , "OrderFrom" },
            { "OrderType" , "OrderType" },
            { "OrderRemark" , "OrderRemark" },
            { "IPAddress" , "IPAddress" },
            { "Id" , "OID" }
        };

        /// <summary>
        ///  字段字典
        /// </summary>
        public sealed override Dictionary<string, string> FieldMap
        {
            get { return fieldMap ; }
        }
        #endregion

        #region 方法实现


        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="entity">读取数据的实体</param>
        protected sealed override void LoadEntity(SqlDataReader reader,UserOrderData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._oID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._orgOID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._userUID = (long)reader.GetInt64(3);
                if (!reader.IsDBNull(4))
                    entity._orderID = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._orderIDSubID = reader.GetString(5);
                if (!reader.IsDBNull(6))
                    entity._orderState = reader.GetString(6);
                if (!reader.IsDBNull(7))
                    entity._orderPrice = /*(money)*/reader.GetValue(7).ToString();
                if (!reader.IsDBNull(8))
                    entity._freightPrice = /*(money)*/reader.GetValue(8).ToString();
                if (!reader.IsDBNull(9))
                    entity._totlePrice = /*(money)*/reader.GetValue(9).ToString();
                if (!reader.IsDBNull(10))
                    entity._totleWeight = (float)reader.GetDouble(10);
                if (!reader.IsDBNull(11))
                    entity._skuList = reader.GetString(11);
                if (!reader.IsDBNull(12))
                    entity._orderTime = reader.GetDateTime(12);
                if (!reader.IsDBNull(13))
                    entity._orderName = reader.GetString(13);
                if (!reader.IsDBNull(14))
                    entity._orderPhone = reader.GetString(14);
                if (!reader.IsDBNull(15))
                    entity._orderTel = reader.GetString(15);
                if (!reader.IsDBNull(16))
                    entity._receiverName = reader.GetString(16);
                if (!reader.IsDBNull(17))
                    entity._receiverPhone = reader.GetString(17).ToString();
                if (!reader.IsDBNull(18))
                    entity._receiverTel = reader.GetString(18);
                if (!reader.IsDBNull(19))
                    entity._receiverProvince = reader.GetString(19);
                if (!reader.IsDBNull(20))
                    entity._receiverCity = reader.GetString(20);
                if (!reader.IsDBNull(21))
                    entity._receiverArea = reader.GetString(21);
                if (!reader.IsDBNull(22))
                    entity._receiverAddress = reader.GetString(22);
                if (!reader.IsDBNull(23))
                    entity._userMessage = reader.GetString(23);
                if (!reader.IsDBNull(24))
                    entity._payTime = reader.GetDateTime(24);
                if (!reader.IsDBNull(25))
                    entity._tradeType = reader.GetString(25);
                if (!reader.IsDBNull(26))
                    entity._tradeID = reader.GetString(26);
                if (!reader.IsDBNull(27))
                    entity._waybillInfo = reader.GetString(27);
                if (!reader.IsDBNull(28))
                    entity._priceMemo = reader.GetString(28);
                if (!reader.IsDBNull(29))
                    entity._productInfo = reader.GetString(29);
                if (!reader.IsDBNull(30))
                    entity._productSkuSID = reader.GetInt32(30);
                if (!reader.IsDBNull(31))
                    entity._productSkuCount = reader.GetInt32(31);
                if (!reader.IsDBNull(32))
                    entity._orderFrom = reader.GetString(32);
                if (!reader.IsDBNull(33))
                    entity._orderType = reader.GetString(33);
                if (!reader.IsDBNull(34))
                    entity._orderRemark = reader.GetString(34);
                if (!reader.IsDBNull(35))
                    entity._iPAddress = reader.GetString(35);
            }
        }

        /// <summary>
        /// 得到字段的DbType类型
        /// </summary>
        /// <param name="field">字段名称</param>
        /// <returns>参数</returns>
        protected sealed override SqlDbType GetDbType(string field)
        {
            switch (field)
            {
                case "OID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "OrgOID":
                    return SqlDbType.BigInt;
                case "UserUID":
                    return SqlDbType.BigInt;
                case "OrderID":
                    return SqlDbType.NVarChar;
                case "OrderIDSubID":
                    return SqlDbType.NVarChar;
                case "OrderState":
                    return SqlDbType.NVarChar;
                case "OrderPrice":
                    return SqlDbType.NVarChar;
                case "FreightPrice":
                    return SqlDbType.NVarChar;
                case "TotlePrice":
                    return SqlDbType.NVarChar;
                case "TotleWeight":
                    return SqlDbType.Decimal;
                case "SkuList":
                    return SqlDbType.NVarChar;
                case "OrderTime":
                    return SqlDbType.DateTime;
                case "OrderName":
                    return SqlDbType.NVarChar;
                case "OrderPhone":
                    return SqlDbType.NVarChar;
                case "OrderTel":
                    return SqlDbType.NVarChar;
                case "ReceiverName":
                    return SqlDbType.NVarChar;
                case "ReceiverPhone":
                    return SqlDbType.NVarChar;
                case "ReceiverTel":
                    return SqlDbType.NVarChar;
                case "ReceiverProvince":
                    return SqlDbType.NVarChar;
                case "ReceiverCity":
                    return SqlDbType.NVarChar;
                case "ReceiverArea":
                    return SqlDbType.NVarChar;
                case "ReceiverAddress":
                    return SqlDbType.NVarChar;
                case "UserMessage":
                    return SqlDbType.NVarChar;
                case "PayTime":
                    return SqlDbType.DateTime;
                case "TradeType":
                    return SqlDbType.NVarChar;
                case "TradeID":
                    return SqlDbType.NVarChar;
                case "WaybillInfo":
                    return SqlDbType.NVarChar;
                case "PriceMemo":
                    return SqlDbType.NVarChar;
                case "ProductInfo":
                    return SqlDbType.NVarChar;
                case "ProductSkuSID":
                    return SqlDbType.Int;
                case "ProductSkuCount":
                    return SqlDbType.Int;
                case "OrderFrom":
                    return SqlDbType.NVarChar;
                case "OrderType":
                    return SqlDbType.NVarChar;
                case "OrderRemark":
                    return SqlDbType.NVarChar;
                case "IPAddress":
                    return SqlDbType.NVarChar;
            }
            return SqlDbType.NVarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void CreateFullSqlParameter(UserOrderData entity, SqlCommand cmd)
        {
            //02:主键(OID)
            cmd.Parameters.Add(new SqlParameter("OID",SqlDbType.BigInt){ Value = entity.OID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:组织标识(OrgOID)
            cmd.Parameters.Add(new SqlParameter("OrgOID",SqlDbType.BigInt){ Value = entity.OrgOID});
            //05:组织标识(UserUID)
            cmd.Parameters.Add(new SqlParameter("UserUID",SqlDbType.BigInt){ Value = entity.UserUID});
            //06:订单标识(OrderID)
            var isNull = string.IsNullOrWhiteSpace(entity.OrderID);
            var parameter = new SqlParameter("OrderID",SqlDbType.NVarChar , isNull ? 10 : (entity.OrderID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrderID;
            cmd.Parameters.Add(parameter);
            //07:订单标识Sub标识(OrderIDSubID)
            isNull = string.IsNullOrWhiteSpace(entity.OrderIDSubID);
            parameter = new SqlParameter("OrderIDSubID",SqlDbType.NVarChar , isNull ? 10 : (entity.OrderIDSubID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrderIDSubID;
            cmd.Parameters.Add(parameter);
            //08:翻转状态(OrderState)
            isNull = string.IsNullOrWhiteSpace(entity.OrderState);
            parameter = new SqlParameter("OrderState",SqlDbType.NVarChar , isNull ? 10 : (entity.OrderState).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrderState;
            cmd.Parameters.Add(parameter);
            //09:最高价格(OrderPrice)
            isNull = string.IsNullOrWhiteSpace(entity.OrderPrice);
            parameter = new SqlParameter("OrderPrice",SqlDbType.NVarChar , isNull ? 10 : (entity.OrderPrice).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrderPrice;
            cmd.Parameters.Add(parameter);
            //10:货运价格(FreightPrice)
            isNull = string.IsNullOrWhiteSpace(entity.FreightPrice);
            parameter = new SqlParameter("FreightPrice",SqlDbType.NVarChar , isNull ? 10 : (entity.FreightPrice).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.FreightPrice;
            cmd.Parameters.Add(parameter);
            //11:总价格(TotlePrice)
            isNull = string.IsNullOrWhiteSpace(entity.TotlePrice);
            parameter = new SqlParameter("TotlePrice",SqlDbType.NVarChar , isNull ? 10 : (entity.TotlePrice).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.TotlePrice;
            cmd.Parameters.Add(parameter);
            //12:总重量(TotleWeight)
            cmd.Parameters.Add(new SqlParameter("TotleWeight",SqlDbType.Decimal){ Value = entity.TotleWeight});
            //13:SKU列表(SkuList)
            isNull = string.IsNullOrWhiteSpace(entity.SkuList);
            parameter = new SqlParameter("SkuList",SqlDbType.NVarChar , isNull ? 10 : (entity.SkuList).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.SkuList;
            cmd.Parameters.Add(parameter);
            //14:订单Time(OrderTime)
            isNull = entity.OrderTime.Year < 1900;
            parameter = new SqlParameter("OrderTime",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrderTime;
            cmd.Parameters.Add(parameter);
            //15:订单Name(OrderName)
            isNull = string.IsNullOrWhiteSpace(entity.OrderName);
            parameter = new SqlParameter("OrderName",SqlDbType.NVarChar , isNull ? 10 : (entity.OrderName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrderName;
            cmd.Parameters.Add(parameter);
            //16:公用电话(OrderPhone)
            isNull = string.IsNullOrWhiteSpace(entity.OrderPhone);
            parameter = new SqlParameter("OrderPhone",SqlDbType.NVarChar , isNull ? 10 : (entity.OrderPhone).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrderPhone;
            cmd.Parameters.Add(parameter);
            //17:自动电话(OrderTel)
            isNull = string.IsNullOrWhiteSpace(entity.OrderTel);
            parameter = new SqlParameter("OrderTel",SqlDbType.NVarChar , isNull ? 10 : (entity.OrderTel).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrderTel;
            cmd.Parameters.Add(parameter);
            //18:接收方名称(ReceiverName)
            isNull = string.IsNullOrWhiteSpace(entity.ReceiverName);
            parameter = new SqlParameter("ReceiverName",SqlDbType.NVarChar , isNull ? 10 : (entity.ReceiverName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ReceiverName;
            cmd.Parameters.Add(parameter);
            //19:接收机电话(ReceiverPhone)
            isNull = string.IsNullOrWhiteSpace(entity.ReceiverPhone);
            parameter = new SqlParameter("ReceiverPhone",SqlDbType.NVarChar , isNull ? 10 : (entity.ReceiverPhone).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ReceiverPhone;
            cmd.Parameters.Add(parameter);
            //20:接收机电话(ReceiverTel)
            isNull = string.IsNullOrWhiteSpace(entity.ReceiverTel);
            parameter = new SqlParameter("ReceiverTel",SqlDbType.NVarChar , isNull ? 10 : (entity.ReceiverTel).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ReceiverTel;
            cmd.Parameters.Add(parameter);
            //21:接收省(ReceiverProvince)
            isNull = string.IsNullOrWhiteSpace(entity.ReceiverProvince);
            parameter = new SqlParameter("ReceiverProvince",SqlDbType.NVarChar , isNull ? 10 : (entity.ReceiverProvince).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ReceiverProvince;
            cmd.Parameters.Add(parameter);
            //22:接收城市(ReceiverCity)
            isNull = string.IsNullOrWhiteSpace(entity.ReceiverCity);
            parameter = new SqlParameter("ReceiverCity",SqlDbType.NVarChar , isNull ? 10 : (entity.ReceiverCity).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ReceiverCity;
            cmd.Parameters.Add(parameter);
            //23:接收区(ReceiverArea)
            isNull = string.IsNullOrWhiteSpace(entity.ReceiverArea);
            parameter = new SqlParameter("ReceiverArea",SqlDbType.NVarChar , isNull ? 10 : (entity.ReceiverArea).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ReceiverArea;
            cmd.Parameters.Add(parameter);
            //24:收件人地址(ReceiverAddress)
            isNull = string.IsNullOrWhiteSpace(entity.ReceiverAddress);
            parameter = new SqlParameter("ReceiverAddress",SqlDbType.NVarChar , isNull ? 10 : (entity.ReceiverAddress).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ReceiverAddress;
            cmd.Parameters.Add(parameter);
            //25:用户消息(UserMessage)
            isNull = string.IsNullOrWhiteSpace(entity.UserMessage);
            parameter = new SqlParameter("UserMessage",SqlDbType.NVarChar , isNull ? 10 : (entity.UserMessage).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.UserMessage;
            cmd.Parameters.Add(parameter);
            //26:支付时间(PayTime)
            isNull = entity.PayTime.Year < 1900;
            parameter = new SqlParameter("PayTime",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.PayTime;
            cmd.Parameters.Add(parameter);
            //27:贸易壁垒(TradeType)
            isNull = string.IsNullOrWhiteSpace(entity.TradeType);
            parameter = new SqlParameter("TradeType",SqlDbType.NVarChar , isNull ? 10 : (entity.TradeType).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.TradeType;
            cmd.Parameters.Add(parameter);
            //28:贸易壁垒(TradeID)
            isNull = string.IsNullOrWhiteSpace(entity.TradeID);
            parameter = new SqlParameter("TradeID",SqlDbType.NVarChar , isNull ? 10 : (entity.TradeID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.TradeID;
            cmd.Parameters.Add(parameter);
            //29:货运单信息(WaybillInfo)
            isNull = string.IsNullOrWhiteSpace(entity.WaybillInfo);
            parameter = new SqlParameter("WaybillInfo",SqlDbType.NVarChar , isNull ? 10 : (entity.WaybillInfo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.WaybillInfo;
            cmd.Parameters.Add(parameter);
            //30:价格备忘录(PriceMemo)
            isNull = string.IsNullOrWhiteSpace(entity.PriceMemo);
            parameter = new SqlParameter("PriceMemo",SqlDbType.NVarChar , isNull ? 10 : (entity.PriceMemo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.PriceMemo;
            cmd.Parameters.Add(parameter);
            //31:产品信息(ProductInfo)
            isNull = string.IsNullOrWhiteSpace(entity.ProductInfo);
            parameter = new SqlParameter("ProductInfo",SqlDbType.NVarChar , isNull ? 10 : (entity.ProductInfo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ProductInfo;
            cmd.Parameters.Add(parameter);
            //32:产品SKU站点标识(ProductSkuSID)
            cmd.Parameters.Add(new SqlParameter("ProductSkuSID",SqlDbType.Int){ Value = entity.ProductSkuSID});
            //33:产品SKU计数(ProductSkuCount)
            cmd.Parameters.Add(new SqlParameter("ProductSkuCount",SqlDbType.Int){ Value = entity.ProductSkuCount});
            //34:订单From(OrderFrom)
            isNull = string.IsNullOrWhiteSpace(entity.OrderFrom);
            parameter = new SqlParameter("OrderFrom",SqlDbType.NVarChar , isNull ? 10 : (entity.OrderFrom).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrderFrom;
            cmd.Parameters.Add(parameter);
            //35:订单类型(OrderType)
            isNull = string.IsNullOrWhiteSpace(entity.OrderType);
            parameter = new SqlParameter("OrderType",SqlDbType.NVarChar , isNull ? 10 : (entity.OrderType).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrderType;
            cmd.Parameters.Add(parameter);
            //36:订单备注(OrderRemark)
            isNull = string.IsNullOrWhiteSpace(entity.OrderRemark);
            parameter = new SqlParameter("OrderRemark",SqlDbType.NVarChar , isNull ? 10 : (entity.OrderRemark).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrderRemark;
            cmd.Parameters.Add(parameter);
            //37:IP地址(IPAddress)
            isNull = string.IsNullOrWhiteSpace(entity.IPAddress);
            parameter = new SqlParameter("IPAddress",SqlDbType.NVarChar , isNull ? 10 : (entity.IPAddress).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.IPAddress;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(UserOrderData entity, SqlCommand cmd)
        {
            cmd.CommandText = UpdateSqlCode;
            CreateFullSqlParameter(entity,cmd);
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        protected sealed override bool SetInsertCommand(UserOrderData entity, SqlCommand cmd)
        {
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return false;
        }
        #endregion

    }

    sealed partial class HpcSqlServerDb
    {


        /// <summary>
        /// 用户端的结构语句
        /// </summary>
        private TableSql _tbUserOrderSql = new TableSql
        {
            TableName = "tbUserOrder",
            PimaryKey = "OID"
        };


        /// <summary>
        /// 用户端数据访问对象
        /// </summary>
        private UserOrderDataAccess _userOrders;

        /// <summary>
        /// 用户端数据访问对象
        /// </summary>
        public UserOrderDataAccess UserOrders
        {
            get
            {
                return this._userOrders ?? ( this._userOrders = new UserOrderDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 用户端(tbUserOrder):用户端
        /// </summary>
        public const int Table_UserOrder = 0x0;
    }
}