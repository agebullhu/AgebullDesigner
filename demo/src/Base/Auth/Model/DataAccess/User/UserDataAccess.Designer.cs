/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/1/2 20:11:23*/
#region
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using Agebull.EntityModel.Common;
#endregion

namespace Agebull.Common.OAuth.DataAccess
{
    /// <summary>
    /// APP端用户信息表
    /// </summary>
    public partial class UserDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public UserDataAccess()
        {
            Name = UserData._DataStruct_.EntityName;
            Caption = UserData._DataStruct_.EntityCaption;
            Description = UserData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => UserData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_user_user";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_user_user";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => UserData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `user_id` AS `UserId`,
    `user_type` AS `UserType`,
    `open_id` AS `OpenId`,
    `status` AS `Status`,
    `add_date` AS `AddDate`,
    `regist_soure` AS `RegistSoure`,
    `os` AS `Os`,
    `app` AS `App`,
    `device_id` AS `DeviceId`,
    `channel` AS `Channel`,
    `trace_mark` AS `TraceMark`,
    `is_freeze` AS `IsFreeze`,
    `data_state` AS `DataState`,
    `last_reviser_id` AS `LastReviserId`,
    `last_modify_date` AS `LastModifyDate`,
    `author_id` AS `AuthorId`";
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
INSERT INTO `tb_user_user`
(
    `user_id`,
    `user_type`,
    `open_id`,
    `status`,
    `add_date`,
    `regist_soure`,
    `os`,
    `app`,
    `device_id`,
    `channel`,
    `trace_mark`,
    `is_freeze`,
    `data_state`,
    `last_reviser_id`,
    `author_id`
)
VALUES
(
    ?UserId,
    ?UserType,
    ?OpenId,
    ?Status,
    ?AddDate,
    ?RegistSoure,
    ?Os,
    ?App,
    ?DeviceId,
    ?Channel,
    ?TraceMark,
    ?IsFreeze,
    ?DataState,
    ?LastReviserId,
    ?AuthorId
);";
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
UPDATE `tb_user_user` SET
       `user_type` = ?UserType,
       `open_id` = ?OpenId,
       `status` = ?Status,
       `regist_soure` = ?RegistSoure,
       `os` = ?Os,
       `app` = ?App,
       `device_id` = ?DeviceId,
       `channel` = ?Channel,
       `trace_mark` = ?TraceMark,
       `is_freeze` = ?IsFreeze,
       `data_state` = ?DataState,
       `last_reviser_id` = ?LastReviserId
 WHERE `user_id` = ?UserId AND `is_freeze` = 0 AND `data_state` < 255;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(UserData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_user_user` SET");
            //用户类型
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_UserType] > 0)
                sql.AppendLine("       `user_type` = ?UserType");
            //用户代码
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_OpenId] > 0)
                sql.AppendLine("       `open_id` = ?OpenId");
            //用户状态
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_Status] > 0)
                sql.AppendLine("       `status` = ?Status");
            //注册来源
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_RegistSoure] > 0)
                sql.AppendLine("       `regist_soure` = ?RegistSoure");
            //注册来源操作系统
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_Os] > 0)
                sql.AppendLine("       `os` = ?Os");
            //注册时的应用
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_App] > 0)
                sql.AppendLine("       `app` = ?App");
            //注册时设备识别码
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_DeviceId] > 0)
                sql.AppendLine("       `device_id` = ?DeviceId");
            //注册来源渠道码
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_Channel] > 0)
                sql.AppendLine("       `channel` = ?Channel");
            //注册来源活动跟踪码
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_TraceMark] > 0)
                sql.AppendLine("       `trace_mark` = ?TraceMark");
            //冻结更新
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_IsFreeze] > 0)
                sql.AppendLine("       `is_freeze` = ?IsFreeze");
            //数据状态
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_DataState] > 0)
                sql.AppendLine("       `data_state` = ?DataState");
            //最后修改者
            if (data.__EntityStatus.ModifiedProperties[UserData._DataStruct_.Real_LastReviserId] > 0)
                sql.AppendLine("       `last_reviser_id` = ?LastReviserId");
            sql.Append(" WHERE `user_id` = ?UserId AND `is_freeze` = 0 AND `data_state` < 255;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "UserId","UserType","OpenId","Status","AddDate","RegistSoure","Os","App","DeviceId","Channel","TraceMark","IsFreeze","DataState","LastReviserId","LastModifyDate","AuthorId" };

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
            { "UserId" , "user_id" },
            { "user_id" , "user_id" },
            { "UserType" , "user_type" },
            { "user_type" , "user_type" },
            { "OpenId" , "open_id" },
            { "open_id" , "open_id" },
            { "Status" , "status" },
            { "AddDate" , "add_date" },
            { "add_date" , "add_date" },
            { "RegistSoure" , "regist_soure" },
            { "regist_soure" , "regist_soure" },
            { "Os" , "os" },
            { "App" , "app" },
            { "DeviceId" , "device_id" },
            { "device_id" , "device_id" },
            { "Channel" , "channel" },
            { "TraceMark" , "trace_mark" },
            { "trace_mark" , "trace_mark" },
            { "IsFreeze" , "is_freeze" },
            { "is_freeze" , "is_freeze" },
            { "DataState" , "data_state" },
            { "data_state" , "data_state" },
            { "LastReviserId" , "last_reviser_id" },
            { "last_reviser_id" , "last_reviser_id" },
            { "LastModifyDate" , "last_modify_date" },
            { "last_modify_date" , "last_modify_date" },
            { "AuthorId" , "author_id" },
            { "author_id" , "author_id" },
            { "Id" , "user_id" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,UserData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._userId = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._userType = (AuthorizeType)reader.GetInt32(1);
                if (!reader.IsDBNull(2))
                    entity._openId = reader.GetString(2).ToString();
                if (!reader.IsDBNull(3))
                    entity._status = (UserStatusType)reader.GetInt32(3);
                if (!reader.IsDBNull(4))
                    try{entity._addDate = reader.GetMySqlDateTime(4).Value;}catch{}
                if (!reader.IsDBNull(5))
                    entity._registSoure = (AuthorizeType)reader.GetInt32(5);
                if (!reader.IsDBNull(6))
                    entity._os = reader.GetString(6).ToString();
                if (!reader.IsDBNull(7))
                    entity._app = reader.GetString(7).ToString();
                if (!reader.IsDBNull(8))
                    entity._deviceId = reader.GetString(8).ToString();
                if (!reader.IsDBNull(9))
                    entity._channel = reader.GetString(9).ToString();
                if (!reader.IsDBNull(10))
                    entity._traceMark = reader.GetString(10).ToString();
                if (!reader.IsDBNull(11))
                    entity._isFreeze = (bool)reader.GetBoolean(11);
                if (!reader.IsDBNull(12))
                    entity._dataState = (DataStateType)reader.GetInt32(12);
                if (!reader.IsDBNull(13))
                    entity._lastReviserId = (long)reader.GetInt64(13);
                if (!reader.IsDBNull(14))
                    try{entity._lastModifyDate = reader.GetMySqlDateTime(14).Value;}catch{}
                if (!reader.IsDBNull(15))
                    entity._authorId = (long)reader.GetInt64(15);
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
                case "UserId":
                    return MySqlDbType.Int64;
                case "UserType":
                    return MySqlDbType.Int32;
                case "OpenId":
                    return MySqlDbType.VarString;
                case "Status":
                    return MySqlDbType.Int32;
                case "AddDate":
                    return MySqlDbType.DateTime;
                case "RegistSoure":
                    return MySqlDbType.Int32;
                case "Os":
                    return MySqlDbType.VarString;
                case "App":
                    return MySqlDbType.VarString;
                case "DeviceId":
                    return MySqlDbType.VarString;
                case "Channel":
                    return MySqlDbType.VarString;
                case "TraceMark":
                    return MySqlDbType.VarString;
                case "IsFreeze":
                    return MySqlDbType.Byte;
                case "DataState":
                    return MySqlDbType.Int32;
                case "LastReviserId":
                    return MySqlDbType.Int64;
                case "LastModifyDate":
                    return MySqlDbType.DateTime;
                case "AuthorId":
                    return MySqlDbType.Int64;
            }
            return MySqlDbType.VarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        private void CreateFullSqlParameter(UserData entity, MySqlCommand cmd)
        {
            //02:用户Id(UserId)
            cmd.Parameters.Add(new MySqlParameter("UserId",MySqlDbType.Int64){ Value = entity.UserId});
            //03:用户类型(UserType)
            cmd.Parameters.Add(new MySqlParameter("UserType",MySqlDbType.Int32){ Value = (int)entity.UserType});
            //04:用户代码(OpenId)
            var isNull = string.IsNullOrWhiteSpace(entity.OpenId);
            var parameter = new MySqlParameter("OpenId",MySqlDbType.VarString , isNull ? 10 : (entity.OpenId).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OpenId;
            cmd.Parameters.Add(parameter);
            //05:用户状态(Status)
            cmd.Parameters.Add(new MySqlParameter("Status",MySqlDbType.Int32){ Value = (int)entity.Status});
            //06:制作时间(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new MySqlParameter("AddDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
            //07:注册来源(RegistSoure)
            cmd.Parameters.Add(new MySqlParameter("RegistSoure",MySqlDbType.Int32){ Value = (int)entity.RegistSoure});
            //08:注册来源操作系统(Os)
            isNull = string.IsNullOrWhiteSpace(entity.Os);
            parameter = new MySqlParameter("Os",MySqlDbType.VarString , isNull ? 10 : (entity.Os).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Os;
            cmd.Parameters.Add(parameter);
            //09:注册时的应用(App)
            isNull = string.IsNullOrWhiteSpace(entity.App);
            parameter = new MySqlParameter("App",MySqlDbType.VarString , isNull ? 10 : (entity.App).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.App;
            cmd.Parameters.Add(parameter);
            //10:注册时设备识别码(DeviceId)
            isNull = string.IsNullOrWhiteSpace(entity.DeviceId);
            parameter = new MySqlParameter("DeviceId",MySqlDbType.VarString , isNull ? 10 : (entity.DeviceId).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.DeviceId;
            cmd.Parameters.Add(parameter);
            //11:注册来源渠道码(Channel)
            isNull = string.IsNullOrWhiteSpace(entity.Channel);
            parameter = new MySqlParameter("Channel",MySqlDbType.VarString , isNull ? 10 : (entity.Channel).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Channel;
            cmd.Parameters.Add(parameter);
            //12:注册来源活动跟踪码(TraceMark)
            isNull = string.IsNullOrWhiteSpace(entity.TraceMark);
            parameter = new MySqlParameter("TraceMark",MySqlDbType.VarString , isNull ? 10 : (entity.TraceMark).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.TraceMark;
            cmd.Parameters.Add(parameter);
            //13:冻结更新(IsFreeze)
            cmd.Parameters.Add(new MySqlParameter("IsFreeze",MySqlDbType.Byte) { Value = entity.IsFreeze ? (byte)1 : (byte)0 });
            //14:数据状态(DataState)
            cmd.Parameters.Add(new MySqlParameter("DataState",MySqlDbType.Int32){ Value = (int)entity.DataState});
            //15:最后修改者(LastReviserId)
            cmd.Parameters.Add(new MySqlParameter("LastReviserId",MySqlDbType.Int64){ Value = entity.LastReviserId});
            //16:最后修改日期(LastModifyDate)
            isNull = entity.LastModifyDate.Year < 1900;
            parameter = new MySqlParameter("LastModifyDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastModifyDate;
            cmd.Parameters.Add(parameter);
            //17:制作人(AuthorId)
            cmd.Parameters.Add(new MySqlParameter("AuthorId",MySqlDbType.Int64){ Value = entity.AuthorId});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(UserData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(UserData entity, MySqlCommand cmd)
        {
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return false;
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
        public override void SimpleLoad(MySqlDataReader reader,UserData entity)
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
        /// APP端用户信息表的结构语句
        /// </summary>
        private TableSql _tb_user_userSql = new TableSql
        {
            TableName = "tb_user_user",
            PimaryKey = "UserId"
        };


        /// <summary>
        /// APP端用户信息表数据访问对象
        /// </summary>
        private UserDataAccess _users;

        /// <summary>
        /// APP端用户信息表数据访问对象
        /// </summary>
        public UserDataAccess Users
        {
            get
            {
                return this._users ?? ( this._users = new UserDataAccess{ DataBase = this});
            }
        }
    }
}