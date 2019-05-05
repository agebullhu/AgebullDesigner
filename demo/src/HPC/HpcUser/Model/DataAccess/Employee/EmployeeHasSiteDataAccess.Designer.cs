/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 16:28:58*/
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
    /// 员工站点关联
    /// </summary>
    public partial class EmployeeHasSiteDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_EmployeeHasSite;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbEmployeeHasSite";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbEmployeeHasSite";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"OID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [OID] AS [OID],
    [EmpEID] AS [EmpEID],
    [SiteSID] AS [SiteSID],
    [Remarks] AS [Remarks]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [OID],
    [EmpEID],
    [SiteSID],
    [Remarks]
)
VALUES
(
    @OID,
    @EmpEID,
    @SiteSID,
    @Remarks
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [OID] = @OID,
       [EmpEID] = @EmpEID,
       [SiteSID] = @SiteSID,
       [Remarks] = @Remarks
 WHERE [OID] = @OID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(EmployeeHasSiteData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[EmployeeHasSiteData._DataStruct_.Real_OID] > 0)
                sql.AppendLine("       [OID] = @OID");
            //员工标识
            if (data.__EntityStatus.ModifiedProperties[EmployeeHasSiteData._DataStruct_.Real_EmpEID] > 0)
                sql.AppendLine("       [EmpEID] = @EmpEID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[EmployeeHasSiteData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //备注
            if (data.__EntityStatus.ModifiedProperties[EmployeeHasSiteData._DataStruct_.Real_Remarks] > 0)
                sql.AppendLine("       [Remarks] = @Remarks");
            sql.Append(" WHERE [OID] = @OID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "OID","EmpEID","SiteSID","Remarks" };

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
            { "OID" , "OID" },
            { "EmpEID" , "EmpEID" },
            { "SiteSID" , "SiteSID" },
            { "Remarks" , "Remarks" },
            { "Id" , "OID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,EmployeeHasSiteData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._oID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._empEID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._siteSID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._remarks = reader.GetString(3);
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
                case "OID":
                    return SqlDbType.BigInt;
                case "EmpEID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "Remarks":
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
        public void CreateFullSqlParameter(EmployeeHasSiteData entity, SqlCommand cmd)
        {
            //02:主键(OID)
            cmd.Parameters.Add(new SqlParameter("OID",SqlDbType.BigInt){ Value = entity.OID});
            //03:员工标识(EmpEID)
            cmd.Parameters.Add(new SqlParameter("EmpEID",SqlDbType.BigInt){ Value = entity.EmpEID});
            //04:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //05:备注(Remarks)
            var isNull = string.IsNullOrWhiteSpace(entity.Remarks);
            var parameter = new SqlParameter("Remarks",SqlDbType.NVarChar , isNull ? 10 : (entity.Remarks).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Remarks;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(EmployeeHasSiteData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(EmployeeHasSiteData entity, SqlCommand cmd)
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
        /// 员工站点关联的结构语句
        /// </summary>
        private TableSql _tbEmployeeHasSiteSql = new TableSql
        {
            TableName = "tbEmployeeHasSite",
            PimaryKey = "OID"
        };


        /// <summary>
        /// 员工站点关联数据访问对象
        /// </summary>
        private EmployeeHasSiteDataAccess _employeeHasSites;

        /// <summary>
        /// 员工站点关联数据访问对象
        /// </summary>
        public EmployeeHasSiteDataAccess EmployeeHasSites
        {
            get
            {
                return this._employeeHasSites ?? ( this._employeeHasSites = new EmployeeHasSiteDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 员工站点关联(tbEmployeeHasSite):员工站点关联
        /// </summary>
        public const int Table_EmployeeHasSite = 0x0;
    }
}