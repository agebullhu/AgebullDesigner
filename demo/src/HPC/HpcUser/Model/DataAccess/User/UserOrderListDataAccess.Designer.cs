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
    /// 用户订单列表
    /// </summary>
    public partial class UserOrderListDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_UserOrderList;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbUserOrderList";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbUserOrderList";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"LID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [LID] AS [LID],
    [OrderID] AS [OrderID],
    [ProductSkuSID] AS [ProductSkuSID],
    [SkuImage] AS [SkuImage],
    [SkuName] AS [SkuName],
    [SkuCount] AS [SkuCount],
    [SkuPrice] AS [SkuPrice],
    [SkuInfo] AS [SkuInfo],
    [PackageWeight] AS [PackageWeight],
    [PackageVolume] AS [PackageVolume],
    [thisTotle] AS [thisTotle]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [OrderID],
    [ProductSkuSID],
    [SkuImage],
    [SkuName],
    [SkuCount],
    [SkuPrice],
    [SkuInfo],
    [PackageWeight],
    [PackageVolume],
    [thisTotle]
)
VALUES
(
    @OrderID,
    @ProductSkuSID,
    @SkuImage,
    @SkuName,
    @SkuCount,
    @SkuPrice,
    @SkuInfo,
    @PackageWeight,
    @PackageVolume,
    @thisTotle
);
SELECT SCOPE_IDENTITY();";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [OrderID] = @OrderID,
       [ProductSkuSID] = @ProductSkuSID,
       [SkuImage] = @SkuImage,
       [SkuName] = @SkuName,
       [SkuCount] = @SkuCount,
       [SkuPrice] = @SkuPrice,
       [SkuInfo] = @SkuInfo,
       [PackageWeight] = @PackageWeight,
       [PackageVolume] = @PackageVolume,
       [thisTotle] = @thisTotle
 WHERE [LID] = @LID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(UserOrderListData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //订单标识
            if (data.__EntityStatus.ModifiedProperties[UserOrderListData._DataStruct_.Real_OrderID] > 0)
                sql.AppendLine("       [OrderID] = @OrderID");
            //产品SKU站点标识
            if (data.__EntityStatus.ModifiedProperties[UserOrderListData._DataStruct_.Real_ProductSkuSID] > 0)
                sql.AppendLine("       [ProductSkuSID] = @ProductSkuSID");
            //SKU图像
            if (data.__EntityStatus.ModifiedProperties[UserOrderListData._DataStruct_.Real_SkuImage] > 0)
                sql.AppendLine("       [SkuImage] = @SkuImage");
            //SKU名称
            if (data.__EntityStatus.ModifiedProperties[UserOrderListData._DataStruct_.Real_SkuName] > 0)
                sql.AppendLine("       [SkuName] = @SkuName");
            //SKU计数
            if (data.__EntityStatus.ModifiedProperties[UserOrderListData._DataStruct_.Real_SkuCount] > 0)
                sql.AppendLine("       [SkuCount] = @SkuCount");
            //SKU价格
            if (data.__EntityStatus.ModifiedProperties[UserOrderListData._DataStruct_.Real_SkuPrice] > 0)
                sql.AppendLine("       [SkuPrice] = @SkuPrice");
            //SKU信息
            if (data.__EntityStatus.ModifiedProperties[UserOrderListData._DataStruct_.Real_SkuInfo] > 0)
                sql.AppendLine("       [SkuInfo] = @SkuInfo");
            //包装重量
            if (data.__EntityStatus.ModifiedProperties[UserOrderListData._DataStruct_.Real_PackageWeight] > 0)
                sql.AppendLine("       [PackageWeight] = @PackageWeight");
            //包装体积
            if (data.__EntityStatus.ModifiedProperties[UserOrderListData._DataStruct_.Real_PackageVolume] > 0)
                sql.AppendLine("       [PackageVolume] = @PackageVolume");
            //这个托托
            if (data.__EntityStatus.ModifiedProperties[UserOrderListData._DataStruct_.Real_thisTotle] > 0)
                sql.AppendLine("       [thisTotle] = @thisTotle");
            sql.Append(" WHERE [LID] = @LID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "LID","OrderID","ProductSkuSID","SkuImage","SkuName","SkuCount","SkuPrice","SkuInfo","PackageWeight","PackageVolume","thisTotle" };

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
            { "LID" , "LID" },
            { "OrderID" , "OrderID" },
            { "ProductSkuSID" , "ProductSkuSID" },
            { "SkuImage" , "SkuImage" },
            { "SkuName" , "SkuName" },
            { "SkuCount" , "SkuCount" },
            { "SkuPrice" , "SkuPrice" },
            { "SkuInfo" , "SkuInfo" },
            { "PackageWeight" , "PackageWeight" },
            { "PackageVolume" , "PackageVolume" },
            { "thisTotle" , "thisTotle" },
            { "Id" , "LID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,UserOrderListData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._lID = reader.GetInt32(0);
                if (!reader.IsDBNull(1))
                    entity._orderID = reader.GetString(1).ToString();
                if (!reader.IsDBNull(2))
                    entity._productSkuSID = reader.GetInt32(2);
                if (!reader.IsDBNull(3))
                    entity._skuImage = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._skuName = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._skuCount = reader.GetInt32(5);
                if (!reader.IsDBNull(6))
                    entity._skuPrice = /*(money)*/reader.GetValue(6).ToString();
                if (!reader.IsDBNull(7))
                    entity._skuInfo = reader.GetString(7);
                if (!reader.IsDBNull(8))
                    entity._packageWeight = (float)reader.GetDouble(8);
                if (!reader.IsDBNull(9))
                    entity._packageVolume = (float)reader.GetDouble(9);
                if (!reader.IsDBNull(10))
                    entity._thisTotle = /*(money)*/reader.GetValue(10).ToString();
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
                case "LID":
                    return SqlDbType.Int;
                case "OrderID":
                    return SqlDbType.NVarChar;
                case "ProductSkuSID":
                    return SqlDbType.Int;
                case "SkuImage":
                    return SqlDbType.NVarChar;
                case "SkuName":
                    return SqlDbType.NVarChar;
                case "SkuCount":
                    return SqlDbType.Int;
                case "SkuPrice":
                    return SqlDbType.NVarChar;
                case "SkuInfo":
                    return SqlDbType.NVarChar;
                case "PackageWeight":
                    return SqlDbType.Decimal;
                case "PackageVolume":
                    return SqlDbType.Decimal;
                case "thisTotle":
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
        public void CreateFullSqlParameter(UserOrderListData entity, SqlCommand cmd)
        {
            //02:主键(LID)
            cmd.Parameters.Add(new SqlParameter("LID",SqlDbType.Int){ Value = entity.LID});
            //03:订单标识(OrderID)
            var isNull = string.IsNullOrWhiteSpace(entity.OrderID);
            var parameter = new SqlParameter("OrderID",SqlDbType.NVarChar , isNull ? 10 : (entity.OrderID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrderID;
            cmd.Parameters.Add(parameter);
            //04:产品SKU站点标识(ProductSkuSID)
            cmd.Parameters.Add(new SqlParameter("ProductSkuSID",SqlDbType.Int){ Value = entity.ProductSkuSID});
            //05:SKU图像(SkuImage)
            isNull = string.IsNullOrWhiteSpace(entity.SkuImage);
            parameter = new SqlParameter("SkuImage",SqlDbType.NVarChar , isNull ? 10 : (entity.SkuImage).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.SkuImage;
            cmd.Parameters.Add(parameter);
            //06:SKU名称(SkuName)
            isNull = string.IsNullOrWhiteSpace(entity.SkuName);
            parameter = new SqlParameter("SkuName",SqlDbType.NVarChar , isNull ? 10 : (entity.SkuName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.SkuName;
            cmd.Parameters.Add(parameter);
            //07:SKU计数(SkuCount)
            cmd.Parameters.Add(new SqlParameter("SkuCount",SqlDbType.Int){ Value = entity.SkuCount});
            //08:SKU价格(SkuPrice)
            isNull = string.IsNullOrWhiteSpace(entity.SkuPrice);
            parameter = new SqlParameter("SkuPrice",SqlDbType.NVarChar , isNull ? 10 : (entity.SkuPrice).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.SkuPrice;
            cmd.Parameters.Add(parameter);
            //09:SKU信息(SkuInfo)
            isNull = string.IsNullOrWhiteSpace(entity.SkuInfo);
            parameter = new SqlParameter("SkuInfo",SqlDbType.NVarChar , isNull ? 10 : (entity.SkuInfo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.SkuInfo;
            cmd.Parameters.Add(parameter);
            //10:包装重量(PackageWeight)
            cmd.Parameters.Add(new SqlParameter("PackageWeight",SqlDbType.Decimal){ Value = entity.PackageWeight});
            //11:包装体积(PackageVolume)
            cmd.Parameters.Add(new SqlParameter("PackageVolume",SqlDbType.Decimal){ Value = entity.PackageVolume});
            //12:这个托托(thisTotle)
            isNull = string.IsNullOrWhiteSpace(entity.thisTotle);
            parameter = new SqlParameter("thisTotle",SqlDbType.NVarChar , isNull ? 10 : (entity.thisTotle).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.thisTotle;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(UserOrderListData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(UserOrderListData entity, SqlCommand cmd)
        {
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return true;
        }
        #endregion

    }

    sealed partial class HpcSqlServerDb
    {


        /// <summary>
        /// 用户订单列表的结构语句
        /// </summary>
        private TableSql _tbUserOrderListSql = new TableSql
        {
            TableName = "tbUserOrderList",
            PimaryKey = "LID"
        };


        /// <summary>
        /// 用户订单列表数据访问对象
        /// </summary>
        private UserOrderListDataAccess _userOrderLists;

        /// <summary>
        /// 用户订单列表数据访问对象
        /// </summary>
        public UserOrderListDataAccess UserOrderLists
        {
            get
            {
                return this._userOrderLists ?? ( this._userOrderLists = new UserOrderListDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 用户订单列表(tbUserOrderList):用户订单列表
        /// </summary>
        public const int Table_UserOrderList = 0x0;
    }
}