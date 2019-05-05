/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:36*/
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
    /// 员工和角色
    /// </summary>
    public partial class EmployeeAndRoleDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_EmployeeAndRole;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbEmployeeAndRole";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbEmployeeAndRole";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"ERID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [ERID] AS [ERID],
    [SiteSID] AS [SiteSID],
    [EmpEID] AS [EmpEID],
    [RoleRID] AS [RoleRID]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [ERID],
    [SiteSID],
    [EmpEID],
    [RoleRID]
)
VALUES
(
    @ERID,
    @SiteSID,
    @EmpEID,
    @RoleRID
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [ERID] = @ERID,
       [SiteSID] = @SiteSID,
       [EmpEID] = @EmpEID,
       [RoleRID] = @RoleRID
 WHERE [ERID] = @ERID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(EmployeeAndRoleData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //厄立德
            if (data.__EntityStatus.ModifiedProperties[EmployeeAndRoleData._DataStruct_.Real_ERID] > 0)
                sql.AppendLine("       [ERID] = @ERID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[EmployeeAndRoleData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //电磁脉冲
            if (data.__EntityStatus.ModifiedProperties[EmployeeAndRoleData._DataStruct_.Real_EmpEID] > 0)
                sql.AppendLine("       [EmpEID] = @EmpEID");
            //角色扮演
            if (data.__EntityStatus.ModifiedProperties[EmployeeAndRoleData._DataStruct_.Real_RoleRID] > 0)
                sql.AppendLine("       [RoleRID] = @RoleRID");
            sql.Append(" WHERE [ERID] = @ERID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "ERID","SiteSID","EmpEID","RoleRID" };

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
            { "ERID" , "ERID" },
            { "SiteSID" , "SiteSID" },
            { "EmpEID" , "EmpEID" },
            { "RoleRID" , "RoleRID" },
            { "Id" , "ERID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,EmployeeAndRoleData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._eRID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._empEID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._roleRID = (long)reader.GetInt64(3);
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
                case "ERID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "EmpEID":
                    return SqlDbType.BigInt;
                case "RoleRID":
                    return SqlDbType.BigInt;
            }
            return SqlDbType.NVarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void CreateFullSqlParameter(EmployeeAndRoleData entity, SqlCommand cmd)
        {
            //02:厄立德(ERID)
            cmd.Parameters.Add(new SqlParameter("ERID",SqlDbType.BigInt){ Value = entity.ERID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:电磁脉冲(EmpEID)
            cmd.Parameters.Add(new SqlParameter("EmpEID",SqlDbType.BigInt){ Value = entity.EmpEID});
            //05:角色扮演(RoleRID)
            cmd.Parameters.Add(new SqlParameter("RoleRID",SqlDbType.BigInt){ Value = entity.RoleRID});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(EmployeeAndRoleData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(EmployeeAndRoleData entity, SqlCommand cmd)
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
        /// 员工和角色的结构语句
        /// </summary>
        private TableSql _tbEmployeeAndRoleSql = new TableSql
        {
            TableName = "tbEmployeeAndRole",
            PimaryKey = "ERID"
        };


        /// <summary>
        /// 员工和角色数据访问对象
        /// </summary>
        private EmployeeAndRoleDataAccess _employeeAndRoles;

        /// <summary>
        /// 员工和角色数据访问对象
        /// </summary>
        public EmployeeAndRoleDataAccess EmployeeAndRoles
        {
            get
            {
                return this._employeeAndRoles ?? ( this._employeeAndRoles = new EmployeeAndRoleDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 员工和角色(tbEmployeeAndRole):员工和角色
        /// </summary>
        public const int Table_EmployeeAndRole = 0x0;
    }
}