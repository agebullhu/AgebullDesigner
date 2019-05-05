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
    /// 员工特权
    /// </summary>
    public partial class EmployeeHasRightDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_EmployeeHasRight;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbEmployeeHasRight";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbEmployeeHasRight";

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
    [RightRID] AS [RightRID]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [ERID],
    [SiteSID],
    [EmpEID],
    [RightRID]
)
VALUES
(
    @ERID,
    @SiteSID,
    @EmpEID,
    @RightRID
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [ERID] = @ERID,
       [SiteSID] = @SiteSID,
       [EmpEID] = @EmpEID,
       [RightRID] = @RightRID
 WHERE [ERID] = @ERID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(EmployeeHasRightData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //厄立德
            if (data.__EntityStatus.ModifiedProperties[EmployeeHasRightData._DataStruct_.Real_ERID] > 0)
                sql.AppendLine("       [ERID] = @ERID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[EmployeeHasRightData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //电磁脉冲
            if (data.__EntityStatus.ModifiedProperties[EmployeeHasRightData._DataStruct_.Real_EmpEID] > 0)
                sql.AppendLine("       [EmpEID] = @EmpEID");
            //右除法
            if (data.__EntityStatus.ModifiedProperties[EmployeeHasRightData._DataStruct_.Real_RightRID] > 0)
                sql.AppendLine("       [RightRID] = @RightRID");
            sql.Append(" WHERE [ERID] = @ERID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "ERID","SiteSID","EmpEID","RightRID" };

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
            { "RightRID" , "RightRID" },
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
        protected sealed override void LoadEntity(SqlDataReader reader,EmployeeHasRightData entity)
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
                    entity._rightRID = (long)reader.GetInt64(3);
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
                case "RightRID":
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
        public void CreateFullSqlParameter(EmployeeHasRightData entity, SqlCommand cmd)
        {
            //02:厄立德(ERID)
            cmd.Parameters.Add(new SqlParameter("ERID",SqlDbType.BigInt){ Value = entity.ERID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:电磁脉冲(EmpEID)
            cmd.Parameters.Add(new SqlParameter("EmpEID",SqlDbType.BigInt){ Value = entity.EmpEID});
            //05:右除法(RightRID)
            cmd.Parameters.Add(new SqlParameter("RightRID",SqlDbType.BigInt){ Value = entity.RightRID});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(EmployeeHasRightData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(EmployeeHasRightData entity, SqlCommand cmd)
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
        /// 员工特权的结构语句
        /// </summary>
        private TableSql _tbEmployeeHasRightSql = new TableSql
        {
            TableName = "tbEmployeeHasRight",
            PimaryKey = "ERID"
        };


        /// <summary>
        /// 员工特权数据访问对象
        /// </summary>
        private EmployeeHasRightDataAccess _employeeHasRights;

        /// <summary>
        /// 员工特权数据访问对象
        /// </summary>
        public EmployeeHasRightDataAccess EmployeeHasRights
        {
            get
            {
                return this._employeeHasRights ?? ( this._employeeHasRights = new EmployeeHasRightDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 员工特权(tbEmployeeHasRight):员工特权
        /// </summary>
        public const int Table_EmployeeHasRight = 0x0;
    }
}