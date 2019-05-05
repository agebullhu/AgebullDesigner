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
    /// 员工
    /// </summary>
    public partial class EmployeeDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_Employee;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbEmployee";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbEmployee";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"EID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [EID] AS [EID],
    [SiteSID] AS [SiteSID],
    [OrgOID] AS [OrgOID],
    [RoleRID] AS [RoleRID],
    [EmpID] AS [EmpID],
    [StateLogin] AS [StateLogin],
    [StateDelete] AS [StateDelete],
    [EmployeeName] AS [EmployeeName],
    [Password] AS [Password],
    [Token] AS [Token],
    [Gender] AS [Gender],
    [Phone] AS [Phone],
    [Email] AS [Email],
    [Remark] AS [Remark],
    [ErrorTimes] AS [ErrorTimes],
    [LastLoginDate] AS [LastLoginDate],
    [LastLoginIP] AS [LastLoginIP],
    [AddDate] AS [AddDate]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [EID],
    [SiteSID],
    [OrgOID],
    [RoleRID],
    [EmpID],
    [StateLogin],
    [StateDelete],
    [EmployeeName],
    [Password],
    [Token],
    [Gender],
    [Phone],
    [Email],
    [Remark],
    [ErrorTimes],
    [LastLoginDate],
    [LastLoginIP],
    [AddDate]
)
VALUES
(
    @EID,
    @SiteSID,
    @OrgOID,
    @RoleRID,
    @EmpID,
    @StateLogin,
    @StateDelete,
    @EmployeeName,
    @Password,
    @Token,
    @Gender,
    @Phone,
    @Email,
    @Remark,
    @ErrorTimes,
    @LastLoginDate,
    @LastLoginIP,
    @AddDate
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [EID] = @EID,
       [SiteSID] = @SiteSID,
       [OrgOID] = @OrgOID,
       [RoleRID] = @RoleRID,
       [EmpID] = @EmpID,
       [StateLogin] = @StateLogin,
       [StateDelete] = @StateDelete,
       [EmployeeName] = @EmployeeName,
       [Password] = @Password,
       [Token] = @Token,
       [Gender] = @Gender,
       [Phone] = @Phone,
       [Email] = @Email,
       [Remark] = @Remark,
       [ErrorTimes] = @ErrorTimes,
       [LastLoginDate] = @LastLoginDate,
       [LastLoginIP] = @LastLoginIP,
       [AddDate] = @AddDate
 WHERE [EID] = @EID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(EmployeeData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_EID] > 0)
                sql.AppendLine("       [EID] = @EID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_OrgOID] > 0)
                sql.AppendLine("       [OrgOID] = @OrgOID");
            //角色扮演
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_RoleRID] > 0)
                sql.AppendLine("       [RoleRID] = @RoleRID");
            //EMP=35782；
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_EmpID] > 0)
                sql.AppendLine("       [EmpID] = @EmpID");
            //状态登录
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_StateLogin] > 0)
                sql.AppendLine("       [StateLogin] = @StateLogin");
            //状态删除
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_StateDelete] > 0)
                sql.AppendLine("       [StateDelete] = @StateDelete");
            //员工姓名
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_EmployeeName] > 0)
                sql.AppendLine("       [EmployeeName] = @EmployeeName");
            //密码
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_Password] > 0)
                sql.AppendLine("       [Password] = @Password");
            //令牌
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_Token] > 0)
                sql.AppendLine("       [Token] = @Token");
            //性别
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_Gender] > 0)
                sql.AppendLine("       [Gender] = @Gender");
            //电话
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_Phone] > 0)
                sql.AppendLine("       [Phone] = @Phone");
            //电子邮件
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_Email] > 0)
                sql.AppendLine("       [Email] = @Email");
            //备注
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_Remark] > 0)
                sql.AppendLine("       [Remark] = @Remark");
            //误差时间
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_ErrorTimes] > 0)
                sql.AppendLine("       [ErrorTimes] = @ErrorTimes");
            //上次登录日期
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_LastLoginDate] > 0)
                sql.AppendLine("       [LastLoginDate] = @LastLoginDate");
            //最后登录IP
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_LastLoginIP] > 0)
                sql.AppendLine("       [LastLoginIP] = @LastLoginIP");
            //添加日期
            if (data.__EntityStatus.ModifiedProperties[EmployeeData._DataStruct_.Real_AddDate] > 0)
                sql.AppendLine("       [AddDate] = @AddDate");
            sql.Append(" WHERE [EID] = @EID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "EID","SiteSID","OrgOID","RoleRID","EmpID","StateLogin","StateDelete","EmployeeName","Password","Token","Gender","Phone","Email","Remark","ErrorTimes","LastLoginDate","LastLoginIP","AddDate" };

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
            { "EID" , "EID" },
            { "SiteSID" , "SiteSID" },
            { "OrgOID" , "OrgOID" },
            { "RoleRID" , "RoleRID" },
            { "EmpID" , "EmpID" },
            { "StateLogin" , "StateLogin" },
            { "StateDelete" , "StateDelete" },
            { "EmployeeName" , "EmployeeName" },
            { "Password" , "Password" },
            { "Token" , "Token" },
            { "Gender" , "Gender" },
            { "Phone" , "Phone" },
            { "Email" , "Email" },
            { "Remark" , "Remark" },
            { "ErrorTimes" , "ErrorTimes" },
            { "LastLoginDate" , "LastLoginDate" },
            { "LastLoginIP" , "LastLoginIP" },
            { "AddDate" , "AddDate" },
            { "Id" , "EID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,EmployeeData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._eID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._orgOID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._roleRID = (long)reader.GetInt64(3);
                if (!reader.IsDBNull(4))
                    entity._empID = reader.GetString(4).ToString();
                if (!reader.IsDBNull(5))
                    entity._stateLogin = (bool)reader.GetBoolean(5);
                if (!reader.IsDBNull(6))
                    entity._stateDelete = (bool)reader.GetBoolean(6);
                if (!reader.IsDBNull(7))
                    entity._employeeName = reader.GetString(7);
                if (!reader.IsDBNull(8))
                    entity._password = reader.GetString(8).ToString();
                if (!reader.IsDBNull(9))
                    entity._token = reader.GetString(9).ToString();
                if (!reader.IsDBNull(10))
                    entity._gender = reader.GetString(10);
                if (!reader.IsDBNull(11))
                    entity._phone = reader.GetString(11).ToString();
                if (!reader.IsDBNull(12))
                    entity._email = reader.GetString(12);
                if (!reader.IsDBNull(13))
                    entity._remark = reader.GetString(13);
                if (!reader.IsDBNull(14))
                    entity._errorTimes = reader.GetInt32(14);
                if (!reader.IsDBNull(15))
                    entity._lastLoginDate = reader.GetDateTime(15);
                if (!reader.IsDBNull(16))
                    entity._lastLoginIP = reader.GetString(16).ToString();
                if (!reader.IsDBNull(17))
                    entity._addDate = reader.GetDateTime(17);
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
                case "EID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "OrgOID":
                    return SqlDbType.BigInt;
                case "RoleRID":
                    return SqlDbType.BigInt;
                case "EmpID":
                    return SqlDbType.NVarChar;
                case "StateLogin":
                    return SqlDbType.Bit;
                case "StateDelete":
                    return SqlDbType.Bit;
                case "EmployeeName":
                    return SqlDbType.NVarChar;
                case "Password":
                    return SqlDbType.NVarChar;
                case "Token":
                    return SqlDbType.NVarChar;
                case "Gender":
                    return SqlDbType.NVarChar;
                case "Phone":
                    return SqlDbType.NVarChar;
                case "Email":
                    return SqlDbType.NVarChar;
                case "Remark":
                    return SqlDbType.NVarChar;
                case "ErrorTimes":
                    return SqlDbType.Int;
                case "LastLoginDate":
                    return SqlDbType.DateTime;
                case "LastLoginIP":
                    return SqlDbType.NVarChar;
                case "AddDate":
                    return SqlDbType.DateTime;
            }
            return SqlDbType.NVarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void CreateFullSqlParameter(EmployeeData entity, SqlCommand cmd)
        {
            //02:主键(EID)
            cmd.Parameters.Add(new SqlParameter("EID",SqlDbType.BigInt){ Value = entity.EID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:组织标识(OrgOID)
            cmd.Parameters.Add(new SqlParameter("OrgOID",SqlDbType.BigInt){ Value = entity.OrgOID});
            //05:角色扮演(RoleRID)
            cmd.Parameters.Add(new SqlParameter("RoleRID",SqlDbType.BigInt){ Value = entity.RoleRID});
            //06:EMP=35782；(EmpID)
            var isNull = string.IsNullOrWhiteSpace(entity.EmpID);
            var parameter = new SqlParameter("EmpID",SqlDbType.NVarChar , isNull ? 10 : (entity.EmpID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.EmpID;
            cmd.Parameters.Add(parameter);
            //07:状态登录(StateLogin)
            cmd.Parameters.Add(new SqlParameter("StateLogin",SqlDbType.Bit){ Value = entity.StateLogin});
            //08:状态删除(StateDelete)
            cmd.Parameters.Add(new SqlParameter("StateDelete",SqlDbType.Bit){ Value = entity.StateDelete});
            //09:员工姓名(EmployeeName)
            isNull = string.IsNullOrWhiteSpace(entity.EmployeeName);
            parameter = new SqlParameter("EmployeeName",SqlDbType.NVarChar , isNull ? 10 : (entity.EmployeeName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.EmployeeName;
            cmd.Parameters.Add(parameter);
            //10:密码(Password)
            isNull = string.IsNullOrWhiteSpace(entity.Password);
            parameter = new SqlParameter("Password",SqlDbType.NVarChar , isNull ? 10 : (entity.Password).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Password;
            cmd.Parameters.Add(parameter);
            //11:令牌(Token)
            isNull = string.IsNullOrWhiteSpace(entity.Token);
            parameter = new SqlParameter("Token",SqlDbType.NVarChar , isNull ? 10 : (entity.Token).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Token;
            cmd.Parameters.Add(parameter);
            //12:性别(Gender)
            isNull = string.IsNullOrWhiteSpace(entity.Gender);
            parameter = new SqlParameter("Gender",SqlDbType.NVarChar , isNull ? 10 : (entity.Gender).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Gender;
            cmd.Parameters.Add(parameter);
            //13:电话(Phone)
            isNull = string.IsNullOrWhiteSpace(entity.Phone);
            parameter = new SqlParameter("Phone",SqlDbType.NVarChar , isNull ? 10 : (entity.Phone).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Phone;
            cmd.Parameters.Add(parameter);
            //14:电子邮件(Email)
            isNull = string.IsNullOrWhiteSpace(entity.Email);
            parameter = new SqlParameter("Email",SqlDbType.NVarChar , isNull ? 10 : (entity.Email).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Email;
            cmd.Parameters.Add(parameter);
            //15:备注(Remark)
            isNull = string.IsNullOrWhiteSpace(entity.Remark);
            parameter = new SqlParameter("Remark",SqlDbType.NVarChar , isNull ? 10 : (entity.Remark).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Remark;
            cmd.Parameters.Add(parameter);
            //16:误差时间(ErrorTimes)
            cmd.Parameters.Add(new SqlParameter("ErrorTimes",SqlDbType.Int){ Value = entity.ErrorTimes});
            //17:上次登录日期(LastLoginDate)
            isNull = entity.LastLoginDate.Year < 1900;
            parameter = new SqlParameter("LastLoginDate",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastLoginDate;
            cmd.Parameters.Add(parameter);
            //18:最后登录IP(LastLoginIP)
            isNull = string.IsNullOrWhiteSpace(entity.LastLoginIP);
            parameter = new SqlParameter("LastLoginIP",SqlDbType.NVarChar , isNull ? 10 : (entity.LastLoginIP).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastLoginIP;
            cmd.Parameters.Add(parameter);
            //19:添加日期(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new SqlParameter("AddDate",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(EmployeeData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(EmployeeData entity, SqlCommand cmd)
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
        /// 员工的结构语句
        /// </summary>
        private TableSql _tbEmployeeSql = new TableSql
        {
            TableName = "tbEmployee",
            PimaryKey = "EID"
        };


        /// <summary>
        /// 员工数据访问对象
        /// </summary>
        private EmployeeDataAccess _employees;

        /// <summary>
        /// 员工数据访问对象
        /// </summary>
        public EmployeeDataAccess Employees
        {
            get
            {
                return this._employees ?? ( this._employees = new EmployeeDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 员工(tbEmployee):员工
        /// </summary>
        public const int Table_Employee = 0x0;
    }
}