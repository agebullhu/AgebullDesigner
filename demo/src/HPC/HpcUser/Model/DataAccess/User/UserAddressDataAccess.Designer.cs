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
    /// 用户地址
    /// </summary>
    public partial class UserAddressDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_UserAddress;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbUserAddress";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbUserAddress";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"AID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [AID] AS [AID],
    [UserUID] AS [UserUID],
    [ReceiverName] AS [ReceiverName],
    [ReceiverPhone] AS [ReceiverPhone],
    [ReceiverAddress] AS [ReceiverAddress],
    [IsDefault] AS [IsDefault],
    [Remarks] AS [Remarks],
    [PostalCode] AS [PostalCode],
    [NationalCode] AS [NationalCode],
    [ProvinceName] AS [ProvinceName],
    [CityName] AS [CityName],
    [CountyName] AS [CountyName]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [AID],
    [UserUID],
    [ReceiverName],
    [ReceiverPhone],
    [ReceiverAddress],
    [IsDefault],
    [Remarks],
    [PostalCode],
    [NationalCode],
    [ProvinceName],
    [CityName],
    [CountyName]
)
VALUES
(
    @AID,
    @UserUID,
    @ReceiverName,
    @ReceiverPhone,
    @ReceiverAddress,
    @IsDefault,
    @Remarks,
    @PostalCode,
    @NationalCode,
    @ProvinceName,
    @CityName,
    @CountyName
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [AID] = @AID,
       [UserUID] = @UserUID,
       [ReceiverName] = @ReceiverName,
       [ReceiverPhone] = @ReceiverPhone,
       [ReceiverAddress] = @ReceiverAddress,
       [IsDefault] = @IsDefault,
       [Remarks] = @Remarks,
       [PostalCode] = @PostalCode,
       [NationalCode] = @NationalCode,
       [ProvinceName] = @ProvinceName,
       [CityName] = @CityName,
       [CountyName] = @CountyName
 WHERE [AID] = @AID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(UserAddressData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[UserAddressData._DataStruct_.Real_AID] > 0)
                sql.AppendLine("       [AID] = @AID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[UserAddressData._DataStruct_.Real_UserUID] > 0)
                sql.AppendLine("       [UserUID] = @UserUID");
            //接收方名称
            if (data.__EntityStatus.ModifiedProperties[UserAddressData._DataStruct_.Real_ReceiverName] > 0)
                sql.AppendLine("       [ReceiverName] = @ReceiverName");
            //接收机电话
            if (data.__EntityStatus.ModifiedProperties[UserAddressData._DataStruct_.Real_ReceiverPhone] > 0)
                sql.AppendLine("       [ReceiverPhone] = @ReceiverPhone");
            //收件人地址
            if (data.__EntityStatus.ModifiedProperties[UserAddressData._DataStruct_.Real_ReceiverAddress] > 0)
                sql.AppendLine("       [ReceiverAddress] = @ReceiverAddress");
            //默认为
            if (data.__EntityStatus.ModifiedProperties[UserAddressData._DataStruct_.Real_IsDefault] > 0)
                sql.AppendLine("       [IsDefault] = @IsDefault");
            //评论
            if (data.__EntityStatus.ModifiedProperties[UserAddressData._DataStruct_.Real_Remarks] > 0)
                sql.AppendLine("       [Remarks] = @Remarks");
            //邮政编码
            if (data.__EntityStatus.ModifiedProperties[UserAddressData._DataStruct_.Real_PostalCode] > 0)
                sql.AppendLine("       [PostalCode] = @PostalCode");
            //国家代码
            if (data.__EntityStatus.ModifiedProperties[UserAddressData._DataStruct_.Real_NationalCode] > 0)
                sql.AppendLine("       [NationalCode] = @NationalCode");
            //省名
            if (data.__EntityStatus.ModifiedProperties[UserAddressData._DataStruct_.Real_ProvinceName] > 0)
                sql.AppendLine("       [ProvinceName] = @ProvinceName");
            //城市名称
            if (data.__EntityStatus.ModifiedProperties[UserAddressData._DataStruct_.Real_CityName] > 0)
                sql.AppendLine("       [CityName] = @CityName");
            //县名
            if (data.__EntityStatus.ModifiedProperties[UserAddressData._DataStruct_.Real_CountyName] > 0)
                sql.AppendLine("       [CountyName] = @CountyName");
            sql.Append(" WHERE [AID] = @AID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "AID","UserUID","ReceiverName","ReceiverPhone","ReceiverAddress","IsDefault","Remarks","PostalCode","NationalCode","ProvinceName","CityName","CountyName" };

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
            { "AID" , "AID" },
            { "UserUID" , "UserUID" },
            { "ReceiverName" , "ReceiverName" },
            { "ReceiverPhone" , "ReceiverPhone" },
            { "ReceiverAddress" , "ReceiverAddress" },
            { "IsDefault" , "IsDefault" },
            { "Remarks" , "Remarks" },
            { "PostalCode" , "PostalCode" },
            { "NationalCode" , "NationalCode" },
            { "ProvinceName" , "ProvinceName" },
            { "CityName" , "CityName" },
            { "CountyName" , "CountyName" },
            { "Id" , "AID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,UserAddressData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._aID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._userUID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._receiverName = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._receiverPhone = reader.GetString(3).ToString();
                if (!reader.IsDBNull(4))
                    entity._receiverAddress = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._isDefault = (bool)reader.GetBoolean(5);
                if (!reader.IsDBNull(6))
                    entity._remarks = reader.GetString(6);
                if (!reader.IsDBNull(7))
                    entity._postalCode = reader.GetString(7);
                if (!reader.IsDBNull(8))
                    entity._nationalCode = reader.GetString(8);
                if (!reader.IsDBNull(9))
                    entity._provinceName = reader.GetString(9);
                if (!reader.IsDBNull(10))
                    entity._cityName = reader.GetString(10);
                if (!reader.IsDBNull(11))
                    entity._countyName = reader.GetString(11);
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
                case "AID":
                    return SqlDbType.BigInt;
                case "UserUID":
                    return SqlDbType.BigInt;
                case "ReceiverName":
                    return SqlDbType.NVarChar;
                case "ReceiverPhone":
                    return SqlDbType.NVarChar;
                case "ReceiverAddress":
                    return SqlDbType.NVarChar;
                case "IsDefault":
                    return SqlDbType.Bit;
                case "Remarks":
                    return SqlDbType.NVarChar;
                case "PostalCode":
                    return SqlDbType.NVarChar;
                case "NationalCode":
                    return SqlDbType.NVarChar;
                case "ProvinceName":
                    return SqlDbType.NVarChar;
                case "CityName":
                    return SqlDbType.NVarChar;
                case "CountyName":
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
        public void CreateFullSqlParameter(UserAddressData entity, SqlCommand cmd)
        {
            //02:主键(AID)
            cmd.Parameters.Add(new SqlParameter("AID",SqlDbType.BigInt){ Value = entity.AID});
            //03:组织标识(UserUID)
            cmd.Parameters.Add(new SqlParameter("UserUID",SqlDbType.BigInt){ Value = entity.UserUID});
            //04:接收方名称(ReceiverName)
            var isNull = string.IsNullOrWhiteSpace(entity.ReceiverName);
            var parameter = new SqlParameter("ReceiverName",SqlDbType.NVarChar , isNull ? 10 : (entity.ReceiverName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ReceiverName;
            cmd.Parameters.Add(parameter);
            //05:接收机电话(ReceiverPhone)
            isNull = string.IsNullOrWhiteSpace(entity.ReceiverPhone);
            parameter = new SqlParameter("ReceiverPhone",SqlDbType.NVarChar , isNull ? 10 : (entity.ReceiverPhone).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ReceiverPhone;
            cmd.Parameters.Add(parameter);
            //06:收件人地址(ReceiverAddress)
            isNull = string.IsNullOrWhiteSpace(entity.ReceiverAddress);
            parameter = new SqlParameter("ReceiverAddress",SqlDbType.NVarChar , isNull ? 10 : (entity.ReceiverAddress).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ReceiverAddress;
            cmd.Parameters.Add(parameter);
            //07:默认为(IsDefault)
            cmd.Parameters.Add(new SqlParameter("IsDefault",SqlDbType.Bit){ Value = entity.IsDefault});
            //08:评论(Remarks)
            isNull = string.IsNullOrWhiteSpace(entity.Remarks);
            parameter = new SqlParameter("Remarks",SqlDbType.NVarChar , isNull ? 10 : (entity.Remarks).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Remarks;
            cmd.Parameters.Add(parameter);
            //09:邮政编码(PostalCode)
            isNull = string.IsNullOrWhiteSpace(entity.PostalCode);
            parameter = new SqlParameter("PostalCode",SqlDbType.NVarChar , isNull ? 10 : (entity.PostalCode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.PostalCode;
            cmd.Parameters.Add(parameter);
            //10:国家代码(NationalCode)
            isNull = string.IsNullOrWhiteSpace(entity.NationalCode);
            parameter = new SqlParameter("NationalCode",SqlDbType.NVarChar , isNull ? 10 : (entity.NationalCode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.NationalCode;
            cmd.Parameters.Add(parameter);
            //11:省名(ProvinceName)
            isNull = string.IsNullOrWhiteSpace(entity.ProvinceName);
            parameter = new SqlParameter("ProvinceName",SqlDbType.NVarChar , isNull ? 10 : (entity.ProvinceName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ProvinceName;
            cmd.Parameters.Add(parameter);
            //12:城市名称(CityName)
            isNull = string.IsNullOrWhiteSpace(entity.CityName);
            parameter = new SqlParameter("CityName",SqlDbType.NVarChar , isNull ? 10 : (entity.CityName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.CityName;
            cmd.Parameters.Add(parameter);
            //13:县名(CountyName)
            isNull = string.IsNullOrWhiteSpace(entity.CountyName);
            parameter = new SqlParameter("CountyName",SqlDbType.NVarChar , isNull ? 10 : (entity.CountyName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.CountyName;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(UserAddressData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(UserAddressData entity, SqlCommand cmd)
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
        /// 用户地址的结构语句
        /// </summary>
        private TableSql _tbUserAddressSql = new TableSql
        {
            TableName = "tbUserAddress",
            PimaryKey = "AID"
        };


        /// <summary>
        /// 用户地址数据访问对象
        /// </summary>
        private UserAddressDataAccess _userAddresses;

        /// <summary>
        /// 用户地址数据访问对象
        /// </summary>
        public UserAddressDataAccess UserAddresses
        {
            get
            {
                return this._userAddresses ?? ( this._userAddresses = new UserAddressDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 用户地址(tbUserAddress):用户地址
        /// </summary>
        public const int Table_UserAddress = 0x0;
    }
}