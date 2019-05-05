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
    /// 员工绑定用户
    /// </summary>
    public partial class EmployeeBindingUserDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_EmployeeBindingUser;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbEmployeeBindingUser";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbEmployeeBindingUser";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"EUID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [EUID] AS [EUID],
    [EmpEID] AS [EmpEID],
    [UserUID] AS [UserUID]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [EUID],
    [EmpEID],
    [UserUID]
)
VALUES
(
    @EUID,
    @EmpEID,
    @UserUID
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [EUID] = @EUID,
       [EmpEID] = @EmpEID,
       [UserUID] = @UserUID
 WHERE [EUID] = @EUID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(EmployeeBindingUserData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //尤伊德
            if (data.__EntityStatus.ModifiedProperties[EmployeeBindingUserData._DataStruct_.Real_EUID] > 0)
                sql.AppendLine("       [EUID] = @EUID");
            //电磁脉冲
            if (data.__EntityStatus.ModifiedProperties[EmployeeBindingUserData._DataStruct_.Real_EmpEID] > 0)
                sql.AppendLine("       [EmpEID] = @EmpEID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[EmployeeBindingUserData._DataStruct_.Real_UserUID] > 0)
                sql.AppendLine("       [UserUID] = @UserUID");
            sql.Append(" WHERE [EUID] = @EUID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "EUID","EmpEID","UserUID" };

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
            { "EUID" , "EUID" },
            { "EmpEID" , "EmpEID" },
            { "UserUID" , "UserUID" },
            { "Id" , "EUID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,EmployeeBindingUserData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._eUID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._empEID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._userUID = (long)reader.GetInt64(2);
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
                case "EUID":
                    return SqlDbType.BigInt;
                case "EmpEID":
                    return SqlDbType.BigInt;
                case "UserUID":
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
        public void CreateFullSqlParameter(EmployeeBindingUserData entity, SqlCommand cmd)
        {
            //02:尤伊德(EUID)
            cmd.Parameters.Add(new SqlParameter("EUID",SqlDbType.BigInt){ Value = entity.EUID});
            //03:电磁脉冲(EmpEID)
            cmd.Parameters.Add(new SqlParameter("EmpEID",SqlDbType.BigInt){ Value = entity.EmpEID});
            //04:组织标识(UserUID)
            cmd.Parameters.Add(new SqlParameter("UserUID",SqlDbType.BigInt){ Value = entity.UserUID});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(EmployeeBindingUserData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(EmployeeBindingUserData entity, SqlCommand cmd)
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
        /// 员工绑定用户的结构语句
        /// </summary>
        private TableSql _tbEmployeeBindingUserSql = new TableSql
        {
            TableName = "tbEmployeeBindingUser",
            PimaryKey = "EUID"
        };


        /// <summary>
        /// 员工绑定用户数据访问对象
        /// </summary>
        private EmployeeBindingUserDataAccess _employeeBindingUsers;

        /// <summary>
        /// 员工绑定用户数据访问对象
        /// </summary>
        public EmployeeBindingUserDataAccess EmployeeBindingUsers
        {
            get
            {
                return this._employeeBindingUsers ?? ( this._employeeBindingUsers = new EmployeeBindingUserDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 员工绑定用户(tbEmployeeBindingUser):员工绑定用户
        /// </summary>
        public const int Table_EmployeeBindingUser = 0x0;
    }
}