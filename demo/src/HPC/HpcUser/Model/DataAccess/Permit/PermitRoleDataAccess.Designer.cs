/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/12 21:30:55*/
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
    /// 角色
    /// </summary>
    public partial class PermitRoleDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_PermitRole;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbPermitRole";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbPermitRole";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"RID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [RID] AS [RID],
    [SiteSID] AS [SiteSID],
    [OrgOID] AS [OrgOID],
    [RoleName] AS [RoleName],
    [Remark] AS [Remark]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [RID],
    [SiteSID],
    [OrgOID],
    [RoleName],
    [Remark]
)
VALUES
(
    @RID,
    @SiteSID,
    @OrgOID,
    @RoleName,
    @Remark
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [RID] = @RID,
       [SiteSID] = @SiteSID,
       [OrgOID] = @OrgOID,
       [RoleName] = @RoleName,
       [Remark] = @Remark
 WHERE [RID] = @RID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(PermitRoleData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[PermitRoleData._DataStruct_.Real_RID] > 0)
                sql.AppendLine("       [RID] = @RID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[PermitRoleData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[PermitRoleData._DataStruct_.Real_OrgOID] > 0)
                sql.AppendLine("       [OrgOID] = @OrgOID");
            //角色名
            if (data.__EntityStatus.ModifiedProperties[PermitRoleData._DataStruct_.Real_RoleName] > 0)
                sql.AppendLine("       [RoleName] = @RoleName");
            //备注
            if (data.__EntityStatus.ModifiedProperties[PermitRoleData._DataStruct_.Real_Remark] > 0)
                sql.AppendLine("       [Remark] = @Remark");
            sql.Append(" WHERE [RID] = @RID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "RID","SiteSID","OrgOID","RoleName","Remark" };

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
            { "RID" , "RID" },
            { "SiteSID" , "SiteSID" },
            { "OrgOID" , "OrgOID" },
            { "RoleName" , "RoleName" },
            { "Remark" , "Remark" },
            { "Id" , "RID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,PermitRoleData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._rID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._orgOID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._roleName = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._remark = reader.GetString(4);
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
                case "RID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "OrgOID":
                    return SqlDbType.BigInt;
                case "RoleName":
                    return SqlDbType.NVarChar;
                case "Remark":
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
        public void CreateFullSqlParameter(PermitRoleData entity, SqlCommand cmd)
        {
            //02:主键(RID)
            cmd.Parameters.Add(new SqlParameter("RID",SqlDbType.BigInt){ Value = entity.RID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:组织标识(OrgOID)
            cmd.Parameters.Add(new SqlParameter("OrgOID",SqlDbType.BigInt){ Value = entity.OrgOID});
            //05:角色名(RoleName)
            var isNull = string.IsNullOrWhiteSpace(entity.RoleName);
            var parameter = new SqlParameter("RoleName",SqlDbType.NVarChar , isNull ? 10 : (entity.RoleName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.RoleName;
            cmd.Parameters.Add(parameter);
            //06:备注(Remark)
            isNull = string.IsNullOrWhiteSpace(entity.Remark);
            parameter = new SqlParameter("Remark",SqlDbType.NVarChar , isNull ? 10 : (entity.Remark).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Remark;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(PermitRoleData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(PermitRoleData entity, SqlCommand cmd)
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
        /// 角色的结构语句
        /// </summary>
        private TableSql _tbPermitRoleSql = new TableSql
        {
            TableName = "tbPermitRole",
            PimaryKey = "RID"
        };


        /// <summary>
        /// 角色数据访问对象
        /// </summary>
        private PermitRoleDataAccess _permitRoles;

        /// <summary>
        /// 角色数据访问对象
        /// </summary>
        public PermitRoleDataAccess PermitRoles
        {
            get
            {
                return this._permitRoles ?? ( this._permitRoles = new PermitRoleDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 角色(tbPermitRole):角色
        /// </summary>
        public const int Table_PermitRole = 0x0;
    }
}