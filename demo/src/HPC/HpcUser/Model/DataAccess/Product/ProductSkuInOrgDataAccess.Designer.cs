/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/6/20 11:32:55*/
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


using HPC.Projects.DataAccess;

namespace HPC.Projects.DataAccess
{
    /// <summary>
    /// 组织SKU
    /// </summary>
    public partial class ProductSkuInOrgDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ProductSkuInOrgDataAccess()
        {
            Name = ProductSkuInOrgData._DataStruct_.EntityName;
            Caption = ProductSkuInOrgData._DataStruct_.EntityCaption;
            Description = ProductSkuInOrgData._DataStruct_.EntityDescription;
        }

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_ProductSkuInOrg;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbProductSkuInOrg";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbProductSkuInOrg";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"SID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [SID] AS [SID],
    [SiteSID] AS [SiteSID],
    [OrgOID] AS [OrgOID],
    [SupplyOIDCurrent] AS [SupplyOIDCurrent],
    [SkuSID] AS [SkuSID],
    [BarCode] AS [BarCode],
    [CostPrice] AS [CostPrice],
    [CostType] AS [CostType],
    [SalePriceDeduct] AS [SalePriceDeduct],
    [SalePricePercent] AS [SalePricePercent],
    [PurchaseType] AS [PurchaseType],
    [StorageVirtual] AS [StorageVirtual],
    [OnlineOldPrice] AS [OnlineOldPrice],
    [OnlineNewPrice] AS [OnlineNewPrice],
    [OfflineOldPrice] AS [OfflineOldPrice],
    [OfflineNewPrice] AS [OfflineNewPrice],
    [BasePriceNoTax] AS [BasePriceNoTax],
    [BasePriceHasTax] AS [BasePriceHasTax],
    [BatchBID] AS [BatchBID],
    [OrderB2BOID] AS [OrderB2BOID],
    [ExcelImportID] AS [ExcelImportID],
    [MemberAttribute] AS [MemberAttribute]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [SID],
    [SiteSID],
    [OrgOID],
    [SupplyOIDCurrent],
    [SkuSID],
    [BarCode],
    [CostPrice],
    [CostType],
    [SalePriceDeduct],
    [SalePricePercent],
    [PurchaseType],
    [StorageVirtual],
    [OnlineOldPrice],
    [OnlineNewPrice],
    [OfflineOldPrice],
    [OfflineNewPrice],
    [BasePriceNoTax],
    [BasePriceHasTax],
    [BatchBID],
    [OrderB2BOID],
    [ExcelImportID],
    [MemberAttribute]
)
VALUES
(
    @SID,
    @SiteSID,
    @OrgOID,
    @SupplyOIDCurrent,
    @SkuSID,
    @BarCode,
    @CostPrice,
    @CostType,
    @SalePriceDeduct,
    @SalePricePercent,
    @PurchaseType,
    @StorageVirtual,
    @OnlineOldPrice,
    @OnlineNewPrice,
    @OfflineOldPrice,
    @OfflineNewPrice,
    @BasePriceNoTax,
    @BasePriceHasTax,
    @BatchBID,
    @OrderB2BOID,
    @ExcelImportID,
    @MemberAttribute
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [SID] = @SID,
       [SiteSID] = @SiteSID,
       [OrgOID] = @OrgOID,
       [SupplyOIDCurrent] = @SupplyOIDCurrent,
       [SkuSID] = @SkuSID,
       [BarCode] = @BarCode,
       [CostPrice] = @CostPrice,
       [CostType] = @CostType,
       [SalePriceDeduct] = @SalePriceDeduct,
       [SalePricePercent] = @SalePricePercent,
       [PurchaseType] = @PurchaseType,
       [StorageVirtual] = @StorageVirtual,
       [OnlineOldPrice] = @OnlineOldPrice,
       [OnlineNewPrice] = @OnlineNewPrice,
       [OfflineOldPrice] = @OfflineOldPrice,
       [OfflineNewPrice] = @OfflineNewPrice,
       [BasePriceNoTax] = @BasePriceNoTax,
       [BasePriceHasTax] = @BasePriceHasTax,
       [BatchBID] = @BatchBID,
       [OrderB2BOID] = @OrderB2BOID,
       [ExcelImportID] = @ExcelImportID,
       [MemberAttribute] = @MemberAttribute
 WHERE [SID] = @SID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(ProductSkuInOrgData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //编号
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_SID] > 0)
                sql.AppendLine("       [SID] = @SID");
            //站点编号
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //组织编号
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_OrgOID] > 0)
                sql.AppendLine("       [OrgOID] = @OrgOID");
            //供应商编号
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_SupplyOIDCurrent] > 0)
                sql.AppendLine("       [SupplyOIDCurrent] = @SupplyOIDCurrent");
            //Sku编号
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_SkuSID] > 0)
                sql.AppendLine("       [SkuSID] = @SkuSID");
            //条形码
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_BarCode] > 0)
                sql.AppendLine("       [BarCode] = @BarCode");
            //成本价
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_CostPrice] > 0)
                sql.AppendLine("       [CostPrice] = @CostPrice");
            //成本类型
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_CostType] > 0)
                sql.AppendLine("       [CostType] = @CostType");
            //销售扣除
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_SalePriceDeduct] > 0)
                sql.AppendLine("       [SalePriceDeduct] = @SalePriceDeduct");
            //销售扣点
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_SalePricePercent] > 0)
                sql.AppendLine("       [SalePricePercent] = @SalePricePercent");
            //采购类型
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_PurchaseType] > 0)
                sql.AppendLine("       [PurchaseType] = @PurchaseType");
            //虚拟仓储
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_StorageVirtual] > 0)
                sql.AppendLine("       [StorageVirtual] = @StorageVirtual");
            //线上原价
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_OnlineOldPrice] > 0)
                sql.AppendLine("       [OnlineOldPrice] = @OnlineOldPrice");
            //线上现价
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_OnlineNewPrice] > 0)
                sql.AppendLine("       [OnlineNewPrice] = @OnlineNewPrice");
            //线下原价
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_OfflineOldPrice] > 0)
                sql.AppendLine("       [OfflineOldPrice] = @OfflineOldPrice");
            //线下现价
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_OfflineNewPrice] > 0)
                sql.AppendLine("       [OfflineNewPrice] = @OfflineNewPrice");
            //税前价
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_BasePriceNoTax] > 0)
                sql.AppendLine("       [BasePriceNoTax] = @BasePriceNoTax");
            //含税价
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_BasePriceHasTax] > 0)
                sql.AppendLine("       [BasePriceHasTax] = @BasePriceHasTax");
            //批次编号
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_BatchBID] > 0)
                sql.AppendLine("       [BatchBID] = @BatchBID");
            //订单标识
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_OrderB2BOID] > 0)
                sql.AppendLine("       [OrderB2BOID] = @OrderB2BOID");
            //导入标识
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_ExcelImportID] > 0)
                sql.AppendLine("       [ExcelImportID] = @ExcelImportID");
            //商品会员类型
            if (data.__EntityStatus.ModifiedProperties[ProductSkuInOrgData._DataStruct_.Real_MemberAttribute] > 0)
                sql.AppendLine("       [MemberAttribute] = @MemberAttribute");
            sql.Append(" WHERE [SID] = @SID;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "SID","SiteSID","OrgOID","SupplyOIDCurrent","SkuSID","BarCode","CostPrice","CostType","SalePriceDeduct","SalePricePercent","PurchaseType","StorageVirtual","OnlineOldPrice","OnlineNewPrice","OfflineOldPrice","OfflineNewPrice","BasePriceNoTax","BasePriceHasTax","BatchBID","OrderB2BOID","ExcelImportID","MemberAttribute" };

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
            { "SID" , "SID" },
            { "SiteSID" , "SiteSID" },
            { "OrgOID" , "OrgOID" },
            { "SupplyOIDCurrent" , "SupplyOIDCurrent" },
            { "SkuSID" , "SkuSID" },
            { "BarCode" , "BarCode" },
            { "CostPrice" , "CostPrice" },
            { "CostType" , "CostType" },
            { "SalePriceDeduct" , "SalePriceDeduct" },
            { "SalePricePercent" , "SalePricePercent" },
            { "PurchaseType" , "PurchaseType" },
            { "StorageVirtual" , "StorageVirtual" },
            { "OnlineOldPrice" , "OnlineOldPrice" },
            { "OnlineNewPrice" , "OnlineNewPrice" },
            { "OfflineOldPrice" , "OfflineOldPrice" },
            { "OfflineNewPrice" , "OfflineNewPrice" },
            { "BasePriceNoTax" , "BasePriceNoTax" },
            { "BasePriceHasTax" , "BasePriceHasTax" },
            { "BatchBID" , "BatchBID" },
            { "OrderB2BOID" , "OrderB2BOID" },
            { "ExcelImportID" , "ExcelImportID" },
            { "MemberAttribute" , "MemberAttribute" },
            { "Id" , "SID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,ProductSkuInOrgData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._sID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._orgOID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._supplyOIDCurrent = (long)reader.GetInt64(3);
                if (!reader.IsDBNull(4))
                    entity._skuSID = (long)reader.GetInt64(4);
                if (!reader.IsDBNull(5))
                    entity._barCode = reader.GetString(5);
                if (!reader.IsDBNull(6))
                    entity._costPrice = (decimal)reader.GetDecimal(6);
                if (!reader.IsDBNull(7))
                    entity._costType = reader.GetString(7);
                if (!reader.IsDBNull(8))
                    entity._salePriceDeduct = (decimal)/*(money)*/reader.GetValue(8);
                if (!reader.IsDBNull(9))
                    entity._salePricePercent = (float)reader.GetDouble(9);
                if (!reader.IsDBNull(10))
                    entity._purchaseType = reader.GetString(10);
                if (!reader.IsDBNull(11))
                    entity._storageVirtual = reader.GetInt32(11);
                if (!reader.IsDBNull(12))
                    entity._onlineOldPrice = (decimal)reader.GetDecimal(12);
                if (!reader.IsDBNull(13))
                    entity._onlineNewPrice = (decimal)reader.GetDecimal(13);
                if (!reader.IsDBNull(14))
                    entity._offlineOldPrice = (decimal)reader.GetDecimal(14);
                if (!reader.IsDBNull(15))
                    entity._offlineNewPrice = (decimal)reader.GetDecimal(15);
                if (!reader.IsDBNull(16))
                    entity._basePriceNoTax = (decimal)reader.GetDecimal(16);
                if (!reader.IsDBNull(17))
                    entity._basePriceHasTax = (decimal)reader.GetDecimal(17);
                if (!reader.IsDBNull(18))
                    entity._batchBID = (long)reader.GetInt64(18);
                if (!reader.IsDBNull(19))
                    entity._orderB2BOID = (long)reader.GetInt64(19);
                if (!reader.IsDBNull(20))
                    entity._excelImportID = reader.GetString(20);
                if (!reader.IsDBNull(21))
                    entity._memberAttribute = reader.GetString(21).ToString();
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
                case "SID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "OrgOID":
                    return SqlDbType.BigInt;
                case "SupplyOIDCurrent":
                    return SqlDbType.BigInt;
                case "SkuSID":
                    return SqlDbType.BigInt;
                case "BarCode":
                    return SqlDbType.NVarChar;
                case "CostPrice":
                    return SqlDbType.Decimal;
                case "CostType":
                    return SqlDbType.NVarChar;
                case "SalePriceDeduct":
                    return SqlDbType.Decimal;
                case "SalePricePercent":
                    return SqlDbType.Float;
                case "PurchaseType":
                    return SqlDbType.NVarChar;
                case "StorageVirtual":
                    return SqlDbType.Int;
                case "OnlineOldPrice":
                    return SqlDbType.Decimal;
                case "OnlineNewPrice":
                    return SqlDbType.Decimal;
                case "OfflineOldPrice":
                    return SqlDbType.Decimal;
                case "OfflineNewPrice":
                    return SqlDbType.Decimal;
                case "BasePriceNoTax":
                    return SqlDbType.Decimal;
                case "BasePriceHasTax":
                    return SqlDbType.Decimal;
                case "BatchBID":
                    return SqlDbType.BigInt;
                case "OrderB2BOID":
                    return SqlDbType.BigInt;
                case "ExcelImportID":
                    return SqlDbType.NVarChar;
                case "MemberAttribute":
                    return SqlDbType.VarChar;
            }
            return SqlDbType.NVarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void CreateFullSqlParameter(ProductSkuInOrgData entity, SqlCommand cmd)
        {
            //02:编号(SID)
            cmd.Parameters.Add(new SqlParameter("SID",SqlDbType.BigInt){ Value = entity.SID});
            //03:站点编号(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:组织编号(OrgOID)
            cmd.Parameters.Add(new SqlParameter("OrgOID",SqlDbType.BigInt){ Value = entity.OrgOID});
            //05:供应商编号(SupplyOIDCurrent)
            cmd.Parameters.Add(new SqlParameter("SupplyOIDCurrent",SqlDbType.BigInt){ Value = entity.SupplyOIDCurrent});
            //07:Sku编号(SkuSID)
            cmd.Parameters.Add(new SqlParameter("SkuSID",SqlDbType.BigInt){ Value = entity.SkuSID});
            //08:条形码(BarCode)
            var isNull = string.IsNullOrWhiteSpace(entity.BarCode);
            var parameter = new SqlParameter("BarCode",SqlDbType.NVarChar , isNull ? 10 : (entity.BarCode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.BarCode;
            cmd.Parameters.Add(parameter);
            //09:成本价(CostPrice)
            cmd.Parameters.Add(new SqlParameter("CostPrice",SqlDbType.Decimal){ Value = entity.CostPrice});
            //10:成本类型(CostType)
            isNull = string.IsNullOrWhiteSpace(entity.CostType);
            parameter = new SqlParameter("CostType",SqlDbType.NVarChar , isNull ? 10 : (entity.CostType).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.CostType;
            cmd.Parameters.Add(parameter);
            //11:销售扣除(SalePriceDeduct)
            cmd.Parameters.Add(new SqlParameter("SalePriceDeduct",SqlDbType.Decimal){ Value = entity.SalePriceDeduct});
            //12:销售扣点(SalePricePercent)
            cmd.Parameters.Add(new SqlParameter("SalePricePercent",SqlDbType.Float){ Value = entity.SalePricePercent});
            //13:采购类型(PurchaseType)
            isNull = string.IsNullOrWhiteSpace(entity.PurchaseType);
            parameter = new SqlParameter("PurchaseType",SqlDbType.NVarChar , isNull ? 10 : (entity.PurchaseType).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.PurchaseType;
            cmd.Parameters.Add(parameter);
            //14:虚拟仓储(StorageVirtual)
            cmd.Parameters.Add(new SqlParameter("StorageVirtual",SqlDbType.Int){ Value = entity.StorageVirtual});
            //15:线上原价(OnlineOldPrice)
            cmd.Parameters.Add(new SqlParameter("OnlineOldPrice",SqlDbType.Decimal){ Value = entity.OnlineOldPrice});
            //16:线上现价(OnlineNewPrice)
            cmd.Parameters.Add(new SqlParameter("OnlineNewPrice",SqlDbType.Decimal){ Value = entity.OnlineNewPrice});
            //17:线下原价(OfflineOldPrice)
            cmd.Parameters.Add(new SqlParameter("OfflineOldPrice",SqlDbType.Decimal){ Value = entity.OfflineOldPrice});
            //18:线下现价(OfflineNewPrice)
            cmd.Parameters.Add(new SqlParameter("OfflineNewPrice",SqlDbType.Decimal){ Value = entity.OfflineNewPrice});
            //19:税前价(BasePriceNoTax)
            cmd.Parameters.Add(new SqlParameter("BasePriceNoTax",SqlDbType.Decimal){ Value = entity.BasePriceNoTax});
            //20:含税价(BasePriceHasTax)
            cmd.Parameters.Add(new SqlParameter("BasePriceHasTax",SqlDbType.Decimal){ Value = entity.BasePriceHasTax});
            //21:批次编号(BatchBID)
            cmd.Parameters.Add(new SqlParameter("BatchBID",SqlDbType.BigInt){ Value = entity.BatchBID});
            //22:订单标识(OrderB2BOID)
            cmd.Parameters.Add(new SqlParameter("OrderB2BOID",SqlDbType.BigInt){ Value = entity.OrderB2BOID});
            //23:导入标识(ExcelImportID)
            isNull = string.IsNullOrWhiteSpace(entity.ExcelImportID);
            parameter = new SqlParameter("ExcelImportID",SqlDbType.NVarChar , isNull ? 10 : (entity.ExcelImportID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ExcelImportID;
            cmd.Parameters.Add(parameter);
            //24:商品会员类型(MemberAttribute)
            isNull = string.IsNullOrWhiteSpace(entity.MemberAttribute);
            parameter = new SqlParameter("MemberAttribute",SqlDbType.NVarChar , isNull ? 10 : (entity.MemberAttribute).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.MemberAttribute;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(ProductSkuInOrgData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(ProductSkuInOrgData entity, SqlCommand cmd)
        {
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return false;
        }
        #endregion
    }

    /*
    partial class HpcSqlServerDb
    {


        /// <summary>
        /// 组织SKU的结构语句
        /// </summary>
        private TableSql _ProductSkuInOrgSql = new TableSql
        {
            TableName = "tbProductSkuInOrg",
            PimaryKey = "SID"
        };


        /// <summary>
        /// 组织SKU数据访问对象
        /// </summary>
        private ProductSkuInOrgDataAccess _productSkuInOrgs;

        /// <summary>
        /// 组织SKU数据访问对象
        /// </summary>
        public ProductSkuInOrgDataAccess ProductSkuInOrgs
        {
            get
            {
                return this._productSkuInOrgs ?? ( this._productSkuInOrgs = new ProductSkuInOrgDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 组织SKU(tbProductSkuInOrg):组织SKU
        /// </summary>
        public const int Table_ProductSkuInOrg = 0x0;
    }*/
}