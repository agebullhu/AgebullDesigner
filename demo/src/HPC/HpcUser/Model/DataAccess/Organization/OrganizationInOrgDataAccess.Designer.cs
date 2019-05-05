/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 16:30:31*/
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
    /// 组织中的组织
    /// </summary>
    public partial class OrganizationInOrgDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_OrganizationInOrg;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbOrganizationInOrg";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbOrganizationInOrg";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"SOID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [SOID] AS [SOID],
    [SiteSID] AS [SiteSID],
    [OrgOID] AS [OrgOID],
    [SiteSID_Relative] AS [SiteSID_Relative],
    [OrgOID_Relative] AS [OrgOID_Relative],
    [RemarkCustom] AS [RemarkCustom],
    [SalesDiscount] AS [SalesDiscount],
    [Relative] AS [Relative]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [SOID],
    [SiteSID],
    [OrgOID],
    [SiteSID_Relative],
    [OrgOID_Relative],
    [RemarkCustom],
    [SalesDiscount],
    [Relative]
)
VALUES
(
    @SOID,
    @SiteSID,
    @OrgOID,
    @SiteSID_Relative,
    @OrgOID_Relative,
    @RemarkCustom,
    @SalesDiscount,
    @Relative
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [SOID] = @SOID,
       [SiteSID] = @SiteSID,
       [OrgOID] = @OrgOID,
       [SiteSID_Relative] = @SiteSID_Relative,
       [OrgOID_Relative] = @OrgOID_Relative,
       [RemarkCustom] = @RemarkCustom,
       [SalesDiscount] = @SalesDiscount,
       [Relative] = @Relative
 WHERE [SOID] = @SOID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(OrganizationInOrgData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //SOID
            if (data.__EntityStatus.ModifiedProperties[OrganizationInOrgData._DataStruct_.Real_SOID] > 0)
                sql.AppendLine("       [SOID] = @SOID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[OrganizationInOrgData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[OrganizationInOrgData._DataStruct_.Real_OrgOID] > 0)
                sql.AppendLine("       [OrgOID] = @OrgOID");
            //站点站点标识relative
            if (data.__EntityStatus.ModifiedProperties[OrganizationInOrgData._DataStruct_.Real_SiteSID_Relative] > 0)
                sql.AppendLine("       [SiteSID_Relative] = @SiteSID_Relative");
            //组织标识相对
            if (data.__EntityStatus.ModifiedProperties[OrganizationInOrgData._DataStruct_.Real_OrgOID_Relative] > 0)
                sql.AppendLine("       [OrgOID_Relative] = @OrgOID_Relative");
            //风俗习惯
            if (data.__EntityStatus.ModifiedProperties[OrganizationInOrgData._DataStruct_.Real_RemarkCustom] > 0)
                sql.AppendLine("       [RemarkCustom] = @RemarkCustom");
            //销售折扣
            if (data.__EntityStatus.ModifiedProperties[OrganizationInOrgData._DataStruct_.Real_SalesDiscount] > 0)
                sql.AppendLine("       [SalesDiscount] = @SalesDiscount");
            //相对
            if (data.__EntityStatus.ModifiedProperties[OrganizationInOrgData._DataStruct_.Real_Relative] > 0)
                sql.AppendLine("       [Relative] = @Relative");
            sql.Append(" WHERE [SOID] = @SOID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "SOID","SiteSID","OrgOID","SiteSID_Relative","OrgOID_Relative","RemarkCustom","SalesDiscount","Relative" };

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
            { "SOID" , "SOID" },
            { "SiteSID" , "SiteSID" },
            { "OrgOID" , "OrgOID" },
            { "SiteSID_Relative" , "SiteSID_Relative" },
            { "OrgOID_Relative" , "OrgOID_Relative" },
            { "RemarkCustom" , "RemarkCustom" },
            { "SalesDiscount" , "SalesDiscount" },
            { "Relative" , "Relative" },
            { "Id" , "SOID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,OrganizationInOrgData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._sOID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._orgOID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._siteSID_Relative = (long)reader.GetInt64(3);
                if (!reader.IsDBNull(4))
                    entity._orgOID_Relative = (long)reader.GetInt64(4);
                if (!reader.IsDBNull(5))
                    entity._remarkCustom = reader.GetString(5);
                if (!reader.IsDBNull(6))
                    entity._salesDiscount = (float)reader.GetDouble(6);
                if (!reader.IsDBNull(7))
                    entity._relative = reader.GetString(7);
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
                case "SOID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "OrgOID":
                    return SqlDbType.BigInt;
                case "SiteSID_Relative":
                    return SqlDbType.BigInt;
                case "OrgOID_Relative":
                    return SqlDbType.BigInt;
                case "RemarkCustom":
                    return SqlDbType.NVarChar;
                case "SalesDiscount":
                    return SqlDbType.Decimal;
                case "Relative":
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
        public void CreateFullSqlParameter(OrganizationInOrgData entity, SqlCommand cmd)
        {
            //02:SOID(SOID)
            cmd.Parameters.Add(new SqlParameter("SOID",SqlDbType.BigInt){ Value = entity.SOID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:组织标识(OrgOID)
            cmd.Parameters.Add(new SqlParameter("OrgOID",SqlDbType.BigInt){ Value = entity.OrgOID});
            //05:站点站点标识relative(SiteSID_Relative)
            cmd.Parameters.Add(new SqlParameter("SiteSID_Relative",SqlDbType.BigInt){ Value = entity.SiteSID_Relative});
            //06:组织标识相对(OrgOID_Relative)
            cmd.Parameters.Add(new SqlParameter("OrgOID_Relative",SqlDbType.BigInt){ Value = entity.OrgOID_Relative});
            //07:风俗习惯(RemarkCustom)
            var isNull = string.IsNullOrWhiteSpace(entity.RemarkCustom);
            var parameter = new SqlParameter("RemarkCustom",SqlDbType.NVarChar , isNull ? 10 : (entity.RemarkCustom).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.RemarkCustom;
            cmd.Parameters.Add(parameter);
            //08:销售折扣(SalesDiscount)
            cmd.Parameters.Add(new SqlParameter("SalesDiscount",SqlDbType.Decimal){ Value = entity.SalesDiscount});
            //09:相对(Relative)
            isNull = string.IsNullOrWhiteSpace(entity.Relative);
            parameter = new SqlParameter("Relative",SqlDbType.NVarChar , isNull ? 10 : (entity.Relative).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Relative;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(OrganizationInOrgData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(OrganizationInOrgData entity, SqlCommand cmd)
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
        /// 组织中的组织的结构语句
        /// </summary>
        private TableSql _tbOrganizationInOrgSql = new TableSql
        {
            TableName = "tbOrganizationInOrg",
            PimaryKey = "SOID"
        };


        /// <summary>
        /// 组织中的组织数据访问对象
        /// </summary>
        private OrganizationInOrgDataAccess _organizationInOrgs;

        /// <summary>
        /// 组织中的组织数据访问对象
        /// </summary>
        public OrganizationInOrgDataAccess OrganizationInOrgs
        {
            get
            {
                return this._organizationInOrgs ?? ( this._organizationInOrgs = new OrganizationInOrgDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 组织中的组织(tbOrganizationInOrg):组织中的组织
        /// </summary>
        public const int Table_OrganizationInOrg = 0x0;
    }
}