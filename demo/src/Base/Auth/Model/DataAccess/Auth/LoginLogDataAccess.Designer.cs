/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/11/14 19:10:07*/
#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.EntityModel.Common;
#endregion

namespace Agebull.Common.OAuth.DataAccess
{
    /// <summary>
    /// 用户登录日志
    /// </summary>
    public partial class LoginLogDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public LoginLogDataAccess()
        {
            Name = LoginLogData._DataStruct_.EntityName;
            Caption = LoginLogData._DataStruct_.EntityCaption;
            Description = LoginLogData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => LoginLogData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_auth_login_log";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_auth_login_log";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => LoginLogData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `Id`,
    `user_id` AS `UserId`,
    `login_name` AS `LoginName`,
    `add_date` AS `AddDate`,
    `device_id` AS `DeviceId`,
    `browser` AS `App`,
    `os` AS `Os`,
    `channal` AS `Channal`,
    `login_type` AS `LoginType`,
    `success` AS `Success`";
            }
        }

        

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode
        {
            get
            {
                return @"
INSERT INTO `tb_auth_login_log`
(
    `user_id`,
    `login_name`,
    `add_date`,
    `device_id`,
    `browser`,
    `os`,
    `channal`,
    `login_type`,
    `success`
)
VALUES
(
    ?UserId,
    ?LoginName,
    ?AddDate,
    ?DeviceId,
    ?App,
    ?Os,
    ?Channal,
    ?LoginType,
    ?Success
);
SELECT @@IDENTITY;";
            }
        }

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode
        {
            get
            {
                return @"
UPDATE `tb_auth_login_log` SET
       `user_id` = ?UserId,
       `login_name` = ?LoginName,
       `add_date` = ?AddDate,
       `device_id` = ?DeviceId,
       `browser` = ?App,
       `os` = ?Os,
       `channal` = ?Channal,
       `login_type` = ?LoginType,
       `success` = ?Success
 WHERE `id` = ?Id;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(LoginLogData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_auth_login_log` SET");
            //用户Id
            if (data.__EntityStatus.ModifiedProperties[LoginLogData._DataStruct_.Real_UserId] > 0)
                sql.AppendLine("       `user_id` = ?UserId");
            //登录账号
            if (data.__EntityStatus.ModifiedProperties[LoginLogData._DataStruct_.Real_LoginName] > 0)
                sql.AppendLine("       `login_name` = ?LoginName");
            //登录时间
            if (data.__EntityStatus.ModifiedProperties[LoginLogData._DataStruct_.Real_AddDate] > 0)
                sql.AppendLine("       `add_date` = ?AddDate");
            //设备识别码
            if (data.__EntityStatus.ModifiedProperties[LoginLogData._DataStruct_.Real_DeviceId] > 0)
                sql.AppendLine("       `device_id` = ?DeviceId");
            //登录系统
            if (data.__EntityStatus.ModifiedProperties[LoginLogData._DataStruct_.Real_App] > 0)
                sql.AppendLine("       `browser` = ?App");
            //登录操作系统
            if (data.__EntityStatus.ModifiedProperties[LoginLogData._DataStruct_.Real_Os] > 0)
                sql.AppendLine("       `os` = ?Os");
            //登录渠道
            if (data.__EntityStatus.ModifiedProperties[LoginLogData._DataStruct_.Real_Channal] > 0)
                sql.AppendLine("       `channal` = ?Channal");
            //登录方式
            if (data.__EntityStatus.ModifiedProperties[LoginLogData._DataStruct_.Real_LoginType] > 0)
                sql.AppendLine("       `login_type` = ?LoginType");
            //是否成功
            if (data.__EntityStatus.ModifiedProperties[LoginLogData._DataStruct_.Real_Success] > 0)
                sql.AppendLine("       `success` = ?Success");
            sql.Append(" WHERE `id` = ?Id;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","UserId","LoginName","AddDate","DeviceId","App","Os","Channal","LoginType","Success" };

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
            { "Id" , "id" },
            { "UserId" , "user_id" },
            { "user_id" , "user_id" },
            { "LoginName" , "login_name" },
            { "login_name" , "login_name" },
            { "AddDate" , "add_date" },
            { "add_date" , "add_date" },
            { "DeviceId" , "device_id" },
            { "device_id" , "device_id" },
            { "App" , "browser" },
            { "browser" , "browser" },
            { "Os" , "os" },
            { "Channal" , "channal" },
            { "LoginType" , "login_type" },
            { "login_type" , "login_type" },
            { "Success" , "success" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,LoginLogData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._userId = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._loginName = reader.GetString(2).ToString();
                if (!reader.IsDBNull(3))
                    try{entity._addDate = reader.GetMySqlDateTime(3).Value;}catch{}
                if (!reader.IsDBNull(4))
                    entity._deviceId = reader.GetString(4).ToString();
                if (!reader.IsDBNull(5))
                    entity._app = reader.GetString(5).ToString();
                if (!reader.IsDBNull(6))
                    entity._os = reader.GetString(6).ToString();
                if (!reader.IsDBNull(7))
                    entity._channal = reader.GetString(7).ToString();
                if (!reader.IsDBNull(8))
                    entity._loginType = (AuthorizeType)reader.GetInt32(8);
                if (!reader.IsDBNull(9))
                    entity._success = (bool)reader.GetBoolean(9);
            }
        }

        /// <summary>
        /// 得到字段的DbType类型
        /// </summary>
        /// <param name="field">字段名称</param>
        /// <returns>参数</returns>
        protected sealed override MySqlDbType GetDbType(string field)
        {
            switch (field)
            {
                case "Id":
                    return MySqlDbType.Int64;
                case "UserId":
                    return MySqlDbType.Int64;
                case "LoginName":
                    return MySqlDbType.VarString;
                case "AddDate":
                    return MySqlDbType.DateTime;
                case "DeviceId":
                    return MySqlDbType.VarString;
                case "App":
                    return MySqlDbType.VarString;
                case "Os":
                    return MySqlDbType.VarString;
                case "Channal":
                    return MySqlDbType.VarString;
                case "LoginType":
                    return MySqlDbType.Int32;
                case "Success":
                    return MySqlDbType.Byte;
            }
            return MySqlDbType.VarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        private void CreateFullSqlParameter(LoginLogData entity, MySqlCommand cmd)
        {
            //02:主键(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:用户Id(UserId)
            cmd.Parameters.Add(new MySqlParameter("UserId",MySqlDbType.Int64){ Value = entity.UserId});
            //04:登录账号(LoginName)
            var isNull = string.IsNullOrWhiteSpace(entity.LoginName);
            var parameter = new MySqlParameter("LoginName",MySqlDbType.VarString , isNull ? 10 : (entity.LoginName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LoginName;
            cmd.Parameters.Add(parameter);
            //05:登录时间(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new MySqlParameter("AddDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
            //06:设备识别码(DeviceId)
            isNull = string.IsNullOrWhiteSpace(entity.DeviceId);
            parameter = new MySqlParameter("DeviceId",MySqlDbType.VarString , isNull ? 10 : (entity.DeviceId).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.DeviceId;
            cmd.Parameters.Add(parameter);
            //07:登录系统(App)
            isNull = string.IsNullOrWhiteSpace(entity.App);
            parameter = new MySqlParameter("App",MySqlDbType.VarString , isNull ? 10 : (entity.App).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.App;
            cmd.Parameters.Add(parameter);
            //08:登录操作系统(Os)
            isNull = string.IsNullOrWhiteSpace(entity.Os);
            parameter = new MySqlParameter("Os",MySqlDbType.VarString , isNull ? 10 : (entity.Os).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Os;
            cmd.Parameters.Add(parameter);
            //09:登录渠道(Channal)
            isNull = string.IsNullOrWhiteSpace(entity.Channal);
            parameter = new MySqlParameter("Channal",MySqlDbType.VarString , isNull ? 10 : (entity.Channal).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Channal;
            cmd.Parameters.Add(parameter);
            //10:登录方式(LoginType)
            cmd.Parameters.Add(new MySqlParameter("LoginType",MySqlDbType.Int32){ Value = (int)entity.LoginType});
            //11:是否成功(Success)
            cmd.Parameters.Add(new MySqlParameter("Success",MySqlDbType.Byte) { Value = entity.Success ? (byte)1 : (byte)0 });
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(LoginLogData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(LoginLogData entity, MySqlCommand cmd)
        {
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return true;
        }

        #endregion

        #region 简单读取

        /// <summary>
        /// SQL语句
        /// </summary>
        public override string SimpleFields
        {
            get
            {
                return @"";
            }
        }


        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="entity">读取数据的实体</param>
        public override void SimpleLoad(MySqlDataReader reader,LoginLogData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
            }
        }
        #endregion

        
    }
    
    partial class UserCenterDb
    {


        /// <summary>
        /// 用户登录日志的结构语句
        /// </summary>
        private TableSql _tb_auth_login_logSql = new TableSql
        {
            TableName = "tb_auth_login_log",
            PimaryKey = "Id"
        };


        /// <summary>
        /// 用户登录日志数据访问对象
        /// </summary>
        private LoginLogDataAccess _loginLogs;

        /// <summary>
        /// 用户登录日志数据访问对象
        /// </summary>
        public LoginLogDataAccess LoginLogs
        {
            get
            {
                return this._loginLogs ?? ( this._loginLogs = new LoginLogDataAccess{ DataBase = this});
            }
        }
    }
}