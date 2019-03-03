/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/11/14 19:10:10*/
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
using Agebull.Common.DataModel;
using Agebull.Common.Rpc;
using Agebull.Common.WebApi;
using Gboxt.Common;
using Gboxt.Common.DataModel;


using Gboxt.Common.DataModel.Extends;
using Gboxt.Common.DataModel.MySql;
#endregion

namespace Agebull.Common.OAuth.DataAccess
{
    /// <summary>
    /// 用户访问令牌
    /// </summary>
    public partial class UserTokenDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public UserTokenDataAccess()
        {
            Name = UserTokenData._DataStruct_.EntityName;
            Caption = UserTokenData._DataStruct_.EntityCaption;
            Description = UserTokenData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => UserTokenData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_auth_user_token";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_auth_user_token";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => UserTokenData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `Id`,
    `user_device_id` AS `UserDeviceId`,
    `add_date` AS `AddDate`,
    `user_id` AS `UserId`,
    `device_id` AS `DeviceId`,
    `access_token` AS `AccessToken`,
    `refresh_token` AS `RefreshToken`,
    `access_token_expires_time` AS `AccessTokenExpiresTime`,
    `refresh_token_expires_time` AS `RefreshTokenExpiresTime`";
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
INSERT INTO `tb_auth_user_token`
(
    `user_device_id`,
    `add_date`,
    `user_id`,
    `device_id`,
    `access_token`,
    `refresh_token`,
    `access_token_expires_time`,
    `refresh_token_expires_time`
)
VALUES
(
    ?UserDeviceId,
    ?AddDate,
    ?UserId,
    ?DeviceId,
    ?AccessToken,
    ?RefreshToken,
    ?AccessTokenExpiresTime,
    ?RefreshTokenExpiresTime
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
UPDATE `tb_auth_user_token` SET
       `user_device_id` = ?UserDeviceId,
       `add_date` = ?AddDate,
       `user_id` = ?UserId,
       `device_id` = ?DeviceId,
       `access_token` = ?AccessToken,
       `refresh_token` = ?RefreshToken,
       `access_token_expires_time` = ?AccessTokenExpiresTime,
       `refresh_token_expires_time` = ?RefreshTokenExpiresTime
 WHERE `id` = ?Id;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(UserTokenData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_auth_user_token` SET");
            //设备标识
            if (data.__EntityStatus.ModifiedProperties[UserTokenData._DataStruct_.Real_UserDeviceId] > 0)
                sql.AppendLine("       `user_device_id` = ?UserDeviceId");
            //登录时间
            if (data.__EntityStatus.ModifiedProperties[UserTokenData._DataStruct_.Real_AddDate] > 0)
                sql.AppendLine("       `add_date` = ?AddDate");
            //用户标识
            if (data.__EntityStatus.ModifiedProperties[UserTokenData._DataStruct_.Real_UserId] > 0)
                sql.AppendLine("       `user_id` = ?UserId");
            //设备标识
            if (data.__EntityStatus.ModifiedProperties[UserTokenData._DataStruct_.Real_DeviceId] > 0)
                sql.AppendLine("       `device_id` = ?DeviceId");
            //访问令牌
            if (data.__EntityStatus.ModifiedProperties[UserTokenData._DataStruct_.Real_AccessToken] > 0)
                sql.AppendLine("       `access_token` = ?AccessToken");
            //刷新令牌
            if (data.__EntityStatus.ModifiedProperties[UserTokenData._DataStruct_.Real_RefreshToken] > 0)
                sql.AppendLine("       `refresh_token` = ?RefreshToken");
            //访问令牌过期时间
            if (data.__EntityStatus.ModifiedProperties[UserTokenData._DataStruct_.Real_AccessTokenExpiresTime] > 0)
                sql.AppendLine("       `access_token_expires_time` = ?AccessTokenExpiresTime");
            //刷新令牌过期时间
            if (data.__EntityStatus.ModifiedProperties[UserTokenData._DataStruct_.Real_RefreshTokenExpiresTime] > 0)
                sql.AppendLine("       `refresh_token_expires_time` = ?RefreshTokenExpiresTime");
            sql.Append(" WHERE `id` = ?Id;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","UserDeviceId","AddDate","UserId","DeviceId","AccessToken","RefreshToken","AccessTokenExpiresTime","RefreshTokenExpiresTime" };

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
            { "UserDeviceId" , "user_device_id" },
            { "user_device_id" , "user_device_id" },
            { "AddDate" , "add_date" },
            { "add_date" , "add_date" },
            { "UserId" , "user_id" },
            { "user_id" , "user_id" },
            { "DeviceId" , "device_id" },
            { "device_id" , "device_id" },
            { "AccessToken" , "access_token" },
            { "access_token" , "access_token" },
            { "RefreshToken" , "refresh_token" },
            { "refresh_token" , "refresh_token" },
            { "AccessTokenExpiresTime" , "access_token_expires_time" },
            { "access_token_expires_time" , "access_token_expires_time" },
            { "RefreshTokenExpiresTime" , "refresh_token_expires_time" },
            { "refresh_token_expires_time" , "refresh_token_expires_time" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,UserTokenData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._userDeviceId = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    try{entity._addDate = reader.GetMySqlDateTime(2).Value;}catch{}
                if (!reader.IsDBNull(3))
                    entity._userId = (long)reader.GetInt64(3);
                if (!reader.IsDBNull(4))
                    entity._deviceId = reader.GetString(4).ToString();
                if (!reader.IsDBNull(5))
                    entity._accessToken = reader.GetString(5).ToString();
                if (!reader.IsDBNull(6))
                    entity._refreshToken = reader.GetString(6).ToString();
                if (!reader.IsDBNull(7))
                    try{entity._accessTokenExpiresTime = reader.GetMySqlDateTime(7).Value;}catch{}
                if (!reader.IsDBNull(8))
                    try{entity._refreshTokenExpiresTime = reader.GetMySqlDateTime(8).Value;}catch{}
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
                case "UserDeviceId":
                    return MySqlDbType.Int64;
                case "AddDate":
                    return MySqlDbType.DateTime;
                case "UserId":
                    return MySqlDbType.Int64;
                case "DeviceId":
                    return MySqlDbType.VarString;
                case "AccessToken":
                    return MySqlDbType.VarString;
                case "RefreshToken":
                    return MySqlDbType.VarString;
                case "AccessTokenExpiresTime":
                    return MySqlDbType.DateTime;
                case "RefreshTokenExpiresTime":
                    return MySqlDbType.DateTime;
            }
            return MySqlDbType.VarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        private void CreateFullSqlParameter(UserTokenData entity, MySqlCommand cmd)
        {
            //02:标识(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:设备标识(UserDeviceId)
            cmd.Parameters.Add(new MySqlParameter("UserDeviceId",MySqlDbType.Int64){ Value = entity.UserDeviceId});
            //04:登录时间(AddDate)
            var isNull = entity.AddDate.Year < 1900;
            var parameter = new MySqlParameter("AddDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
            //05:用户标识(UserId)
            cmd.Parameters.Add(new MySqlParameter("UserId",MySqlDbType.Int64){ Value = entity.UserId});
            //06:设备标识(DeviceId)
            isNull = string.IsNullOrWhiteSpace(entity.DeviceId);
            parameter = new MySqlParameter("DeviceId",MySqlDbType.VarString , isNull ? 10 : (entity.DeviceId).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.DeviceId;
            cmd.Parameters.Add(parameter);
            //07:访问令牌(AccessToken)
            isNull = string.IsNullOrWhiteSpace(entity.AccessToken);
            parameter = new MySqlParameter("AccessToken",MySqlDbType.VarString , isNull ? 10 : (entity.AccessToken).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AccessToken;
            cmd.Parameters.Add(parameter);
            //08:刷新令牌(RefreshToken)
            isNull = string.IsNullOrWhiteSpace(entity.RefreshToken);
            parameter = new MySqlParameter("RefreshToken",MySqlDbType.VarString , isNull ? 10 : (entity.RefreshToken).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.RefreshToken;
            cmd.Parameters.Add(parameter);
            //09:访问令牌过期时间(AccessTokenExpiresTime)
            isNull = entity.AccessTokenExpiresTime.Year < 1900;
            parameter = new MySqlParameter("AccessTokenExpiresTime",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AccessTokenExpiresTime;
            cmd.Parameters.Add(parameter);
            //10:刷新令牌过期时间(RefreshTokenExpiresTime)
            isNull = entity.RefreshTokenExpiresTime.Year < 1900;
            parameter = new MySqlParameter("RefreshTokenExpiresTime",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.RefreshTokenExpiresTime;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(UserTokenData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(UserTokenData entity, MySqlCommand cmd)
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
        public override void SimpleLoad(MySqlDataReader reader,UserTokenData entity)
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
        /// 用户访问令牌的结构语句
        /// </summary>
        private TableSql _tb_auth_user_tokenSql = new TableSql
        {
            TableName = "tb_auth_user_token",
            PimaryKey = "Id"
        };


        /// <summary>
        /// 用户访问令牌数据访问对象
        /// </summary>
        private UserTokenDataAccess _userTokens;

        /// <summary>
        /// 用户访问令牌数据访问对象
        /// </summary>
        public UserTokenDataAccess UserTokens
        {
            get
            {
                return this._userTokens ?? ( this._userTokens = new UserTokenDataAccess{ DataBase = this});
            }
        }
    }
}