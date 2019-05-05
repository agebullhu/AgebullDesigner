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
    /// 相对组织中的组织
    /// </summary>
    public partial class OrganizationInOrgRelativeDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_OrganizationInOrgRelative;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbOrganizationInOrgRelative";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbOrganizationInOrgRelative";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"ORID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [ORID] AS [ORID],
    [SiteSID] AS [SiteSID],
    [OrgOID] AS [OrgOID],
    [SOID] AS [SOID],
    [Relative] AS [Relative]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [ORID],
    [SiteSID],
    [OrgOID],
    [SOID],
    [Relative]
)
VALUES
(
    @ORID,
    @SiteSID,
    @OrgOID,
    @SOID,
    @Relative
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [ORID] = @ORID,
       [SiteSID] = @SiteSID,
       [OrgOID] = @OrgOID,
       [SOID] = @SOID,
       [Relative] = @Relative
 WHERE [ORID] = @ORID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(OrganizationInOrgRelativeData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //奥里德
            if (data.__EntityStatus.ModifiedProperties[OrganizationInOrgRelativeData._DataStruct_.Real_ORID] > 0)
                sql.AppendLine("       [ORID] = @ORID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[OrganizationInOrgRelativeData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[OrganizationInOrgRelativeData._DataStruct_.Real_OrgOID] > 0)
                sql.AppendLine("       [OrgOID] = @OrgOID");
            //SOID
            if (data.__EntityStatus.ModifiedProperties[OrganizationInOrgRelativeData._DataStruct_.Real_SOID] > 0)
                sql.AppendLine("       [SOID] = @SOID");
            //相对
            if (data.__EntityStatus.ModifiedProperties[OrganizationInOrgRelativeData._DataStruct_.Real_Relative] > 0)
                sql.AppendLine("       [Relative] = @Relative");
            sql.Append(" WHERE [ORID] = @ORID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "ORID","SiteSID","OrgOID","SOID","Relative" };

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
            { "ORID" , "ORID" },
            { "SiteSID" , "SiteSID" },
            { "OrgOID" , "OrgOID" },
            { "SOID" , "SOID" },
            { "Relative" , "Relative" },
            { "Id" , "ORID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,OrganizationInOrgRelativeData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._oRID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._orgOID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._sOID = (long)reader.GetInt64(3);
                if (!reader.IsDBNull(4))
                    entity._relative = reader.GetString(4);
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
                case "ORID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "OrgOID":
                    return SqlDbType.BigInt;
                case "SOID":
                    return SqlDbType.BigInt;
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
        public void CreateFullSqlParameter(OrganizationInOrgRelativeData entity, SqlCommand cmd)
        {
            //02:奥里德(ORID)
            cmd.Parameters.Add(new SqlParameter("ORID",SqlDbType.BigInt){ Value = entity.ORID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:组织标识(OrgOID)
            cmd.Parameters.Add(new SqlParameter("OrgOID",SqlDbType.BigInt){ Value = entity.OrgOID});
            //05:SOID(SOID)
            cmd.Parameters.Add(new SqlParameter("SOID",SqlDbType.BigInt){ Value = entity.SOID});
            //06:相对(Relative)
            var isNull = string.IsNullOrWhiteSpace(entity.Relative);
            var parameter = new SqlParameter("Relative",SqlDbType.NVarChar , isNull ? 10 : (entity.Relative).Length);
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
        protected sealed override void SetUpdateCommand(OrganizationInOrgRelativeData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(OrganizationInOrgRelativeData entity, SqlCommand cmd)
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
        /// 相对组织中的组织的结构语句
        /// </summary>
        private TableSql _tbOrganizationInOrgRelativeSql = new TableSql
        {
            TableName = "tbOrganizationInOrgRelative",
            PimaryKey = "ORID"
        };


        /// <summary>
        /// 相对组织中的组织数据访问对象
        /// </summary>
        private OrganizationInOrgRelativeDataAccess _organizationInOrgRelatives;

        /// <summary>
        /// 相对组织中的组织数据访问对象
        /// </summary>
        public OrganizationInOrgRelativeDataAccess OrganizationInOrgRelatives
        {
            get
            {
                return this._organizationInOrgRelatives ?? ( this._organizationInOrgRelatives = new OrganizationInOrgRelativeDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 相对组织中的组织(tbOrganizationInOrgRelative):相对组织中的组织
        /// </summary>
        public const int Table_OrganizationInOrgRelative = 0x0;
    }
}