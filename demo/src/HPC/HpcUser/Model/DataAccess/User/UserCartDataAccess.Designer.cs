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
    /// 用户车
    /// </summary>
    public partial class UserCartDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_UserCart;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbUserCart";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbUserCart";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"CID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [CID] AS [CID],
    [CustomerID] AS [CustomerID],
    [GoodsID] AS [GoodsID],
    [PackageID] AS [PackageID],
    [PackageCount] AS [PackageCount]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [CustomerID],
    [GoodsID],
    [PackageID],
    [PackageCount]
)
VALUES
(
    @CustomerID,
    @GoodsID,
    @PackageID,
    @PackageCount
);
SELECT SCOPE_IDENTITY();";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [CustomerID] = @CustomerID,
       [GoodsID] = @GoodsID,
       [PackageID] = @PackageID,
       [PackageCount] = @PackageCount
 WHERE [CID] = @CID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(UserCartData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //客户满意度
            if (data.__EntityStatus.ModifiedProperties[UserCartData._DataStruct_.Real_CustomerID] > 0)
                sql.AppendLine("       [CustomerID] = @CustomerID");
            //货物运输
            if (data.__EntityStatus.ModifiedProperties[UserCartData._DataStruct_.Real_GoodsID] > 0)
                sql.AppendLine("       [GoodsID] = @GoodsID");
            //包装袋
            if (data.__EntityStatus.ModifiedProperties[UserCartData._DataStruct_.Real_PackageID] > 0)
                sql.AppendLine("       [PackageID] = @PackageID");
            //包装计数
            if (data.__EntityStatus.ModifiedProperties[UserCartData._DataStruct_.Real_PackageCount] > 0)
                sql.AppendLine("       [PackageCount] = @PackageCount");
            sql.Append(" WHERE [CID] = @CID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "CID","CustomerID","GoodsID","PackageID","PackageCount" };

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
            { "CID" , "CID" },
            { "CustomerID" , "CustomerID" },
            { "GoodsID" , "GoodsID" },
            { "PackageID" , "PackageID" },
            { "PackageCount" , "PackageCount" },
            { "Id" , "CID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,UserCartData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._cID = reader.GetInt32(0);
                if (!reader.IsDBNull(1))
                    entity._customerID = reader.GetInt32(1);
                if (!reader.IsDBNull(2))
                    entity._goodsID = reader.GetInt32(2);
                if (!reader.IsDBNull(3))
                    entity._packageID = reader.GetInt32(3);
                if (!reader.IsDBNull(4))
                    entity._packageCount = reader.GetInt32(4);
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
                case "CID":
                    return SqlDbType.Int;
                case "CustomerID":
                    return SqlDbType.Int;
                case "GoodsID":
                    return SqlDbType.Int;
                case "PackageID":
                    return SqlDbType.Int;
                case "PackageCount":
                    return SqlDbType.Int;
            }
            return SqlDbType.NVarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void CreateFullSqlParameter(UserCartData entity, SqlCommand cmd)
        {
            //02:主键(CID)
            cmd.Parameters.Add(new SqlParameter("CID",SqlDbType.Int){ Value = entity.CID});
            //03:客户满意度(CustomerID)
            cmd.Parameters.Add(new SqlParameter("CustomerID",SqlDbType.Int){ Value = entity.CustomerID});
            //04:货物运输(GoodsID)
            cmd.Parameters.Add(new SqlParameter("GoodsID",SqlDbType.Int){ Value = entity.GoodsID});
            //05:包装袋(PackageID)
            cmd.Parameters.Add(new SqlParameter("PackageID",SqlDbType.Int){ Value = entity.PackageID});
            //06:包装计数(PackageCount)
            cmd.Parameters.Add(new SqlParameter("PackageCount",SqlDbType.Int){ Value = entity.PackageCount});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(UserCartData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(UserCartData entity, SqlCommand cmd)
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
        /// 用户车的结构语句
        /// </summary>
        private TableSql _tbUserCartSql = new TableSql
        {
            TableName = "tbUserCart",
            PimaryKey = "CID"
        };


        /// <summary>
        /// 用户车数据访问对象
        /// </summary>
        private UserCartDataAccess _userCarts;

        /// <summary>
        /// 用户车数据访问对象
        /// </summary>
        public UserCartDataAccess UserCarts
        {
            get
            {
                return this._userCarts ?? ( this._userCarts = new UserCartDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 用户车(tbUserCart):用户车
        /// </summary>
        public const int Table_UserCart = 0x0;
    }
}