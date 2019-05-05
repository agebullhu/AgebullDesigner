/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 16:30:30*/
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
    /// 组织和地址
    /// </summary>
    public partial class OrganizationAndAddressDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_OrganizationAndAddress;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbOrganizationAndAddress";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbOrganizationAndAddress";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"OAID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [OAID] AS [OAID],
    [SiteSID] AS [SiteSID],
    [OrgOID] AS [OrgOID],
    [Address] AS [Address],
    [Person] AS [Person],
    [Phone] AS [Phone]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [OAID],
    [SiteSID],
    [OrgOID],
    [Address],
    [Person],
    [Phone]
)
VALUES
(
    @OAID,
    @SiteSID,
    @OrgOID,
    @Address,
    @Person,
    @Phone
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [OAID] = @OAID,
       [SiteSID] = @SiteSID,
       [OrgOID] = @OrgOID,
       [Address] = @Address,
       [Person] = @Person,
       [Phone] = @Phone
 WHERE [OAID] = @OAID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(OrganizationAndAddressData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //助听器
            if (data.__EntityStatus.ModifiedProperties[OrganizationAndAddressData._DataStruct_.Real_OAID] > 0)
                sql.AppendLine("       [OAID] = @OAID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[OrganizationAndAddressData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[OrganizationAndAddressData._DataStruct_.Real_OrgOID] > 0)
                sql.AppendLine("       [OrgOID] = @OrgOID");
            //地址
            if (data.__EntityStatus.ModifiedProperties[OrganizationAndAddressData._DataStruct_.Real_Address] > 0)
                sql.AppendLine("       [Address] = @Address");
            //人
            if (data.__EntityStatus.ModifiedProperties[OrganizationAndAddressData._DataStruct_.Real_Person] > 0)
                sql.AppendLine("       [Person] = @Person");
            //电话
            if (data.__EntityStatus.ModifiedProperties[OrganizationAndAddressData._DataStruct_.Real_Phone] > 0)
                sql.AppendLine("       [Phone] = @Phone");
            sql.Append(" WHERE [OAID] = @OAID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "OAID","SiteSID","OrgOID","Address","Person","Phone" };

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
            { "OAID" , "OAID" },
            { "SiteSID" , "SiteSID" },
            { "OrgOID" , "OrgOID" },
            { "Address" , "Address" },
            { "Person" , "Person" },
            { "Phone" , "Phone" },
            { "Id" , "OAID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,OrganizationAndAddressData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._oAID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._orgOID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._address = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._person = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._phone = reader.GetString(5);
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
                case "OAID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "OrgOID":
                    return SqlDbType.BigInt;
                case "Address":
                    return SqlDbType.NVarChar;
                case "Person":
                    return SqlDbType.NVarChar;
                case "Phone":
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
        public void CreateFullSqlParameter(OrganizationAndAddressData entity, SqlCommand cmd)
        {
            //02:助听器(OAID)
            cmd.Parameters.Add(new SqlParameter("OAID",SqlDbType.BigInt){ Value = entity.OAID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:组织标识(OrgOID)
            cmd.Parameters.Add(new SqlParameter("OrgOID",SqlDbType.BigInt){ Value = entity.OrgOID});
            //05:地址(Address)
            var isNull = string.IsNullOrWhiteSpace(entity.Address);
            var parameter = new SqlParameter("Address",SqlDbType.NVarChar , isNull ? 10 : (entity.Address).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Address;
            cmd.Parameters.Add(parameter);
            //06:人(Person)
            isNull = string.IsNullOrWhiteSpace(entity.Person);
            parameter = new SqlParameter("Person",SqlDbType.NVarChar , isNull ? 10 : (entity.Person).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Person;
            cmd.Parameters.Add(parameter);
            //07:电话(Phone)
            isNull = string.IsNullOrWhiteSpace(entity.Phone);
            parameter = new SqlParameter("Phone",SqlDbType.NVarChar , isNull ? 10 : (entity.Phone).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Phone;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(OrganizationAndAddressData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(OrganizationAndAddressData entity, SqlCommand cmd)
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
        /// 组织和地址的结构语句
        /// </summary>
        private TableSql _tbOrganizationAndAddressSql = new TableSql
        {
            TableName = "tbOrganizationAndAddress",
            PimaryKey = "OAID"
        };


        /// <summary>
        /// 组织和地址数据访问对象
        /// </summary>
        private OrganizationAndAddressDataAccess _organizationAndAddresses;

        /// <summary>
        /// 组织和地址数据访问对象
        /// </summary>
        public OrganizationAndAddressDataAccess OrganizationAndAddresses
        {
            get
            {
                return this._organizationAndAddresses ?? ( this._organizationAndAddresses = new OrganizationAndAddressDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 组织和地址(tbOrganizationAndAddress):组织和地址
        /// </summary>
        public const int Table_OrganizationAndAddress = 0x0;
    }
}