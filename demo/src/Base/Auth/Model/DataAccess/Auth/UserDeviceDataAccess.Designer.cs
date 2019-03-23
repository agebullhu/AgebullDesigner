/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/22 9:58:46*/
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

using MySql.Data.MySqlClient;
using Agebull.EntityModel.MySql;

using Agebull.Common;
using Agebull.Common.OAuth;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;
using Agebull.Common.OAuth;

#endregion

namespace Agebull.Common.Organizations.DataAccess
{
    /// <summary>
    /// 用户设备信息
    /// </summary>
    public partial class UserDeviceDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public UserDeviceDataAccess()
        {
            Name = UserDeviceData._DataStruct_.EntityName;
            Caption = UserDeviceData._DataStruct_.EntityCaption;
            Description = UserDeviceData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => UserDeviceData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_auth_user_device";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_auth_user_device";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => UserDeviceData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `Id`,
    `device_info` AS `DeviceInfo`,
    `add_date` AS `AddDate`,
    `user_id` AS `UserId`,
    `device_id` AS `DeviceId`,
    `os` AS `Os`,
    `app` AS `App`";
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
INSERT INTO `tb_auth_user_device`
(
    `device_info`,
    `add_date`,
    `user_id`,
    `device_id`,
    `os`,
    `app`
)
VALUES
(
    ?DeviceInfo,
    ?AddDate,
    ?UserId,
    ?DeviceId,
    ?Os,
    ?App
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
UPDATE `tb_auth_user_device` SET
       `device_info` = ?DeviceInfo,
       `add_date` = ?AddDate,
       `user_id` = ?UserId,
       `device_id` = ?DeviceId,
       `os` = ?Os,
       `app` = ?App
 WHERE `id` = ?Id;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(UserDeviceData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_auth_user_device` SET");
            //设备的详细信息
            if (data.__EntityStatus.ModifiedProperties[UserDeviceData._DataStruct_.Real_DeviceInfo] > 0)
                sql.AppendLine("       `device_info` = ?DeviceInfo");
            //新增日期
            if (data.__EntityStatus.ModifiedProperties[UserDeviceData._DataStruct_.Real_AddDate] > 0)
                sql.AppendLine("       `add_date` = ?AddDate");
            //用户Id
            if (data.__EntityStatus.ModifiedProperties[UserDeviceData._DataStruct_.Real_UserId] > 0)
                sql.AppendLine("       `user_id` = ?UserId");
            //设备识别码
            if (data.__EntityStatus.ModifiedProperties[UserDeviceData._DataStruct_.Real_DeviceId] > 0)
                sql.AppendLine("       `device_id` = ?DeviceId");
            //设备操作系统
            if (data.__EntityStatus.ModifiedProperties[UserDeviceData._DataStruct_.Real_Os] > 0)
                sql.AppendLine("       `os` = ?Os");
            //登录的应用
            if (data.__EntityStatus.ModifiedProperties[UserDeviceData._DataStruct_.Real_App] > 0)
                sql.AppendLine("       `app` = ?App");
            sql.Append(" WHERE `id` = ?Id;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","DeviceInfo","AddDate","UserId","DeviceId","Os","App" };

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
            { "DeviceInfo" , "device_info" },
            { "device_info" , "device_info" },
            { "AddDate" , "add_date" },
            { "add_date" , "add_date" },
            { "UserId" , "user_id" },
            { "user_id" , "user_id" },
            { "DeviceId" , "device_id" },
            { "device_id" , "device_id" },
            { "Os" , "os" },
            { "App" , "app" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,UserDeviceData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._deviceInfo = reader.GetString(1).ToString();
                if (!reader.IsDBNull(2))
                    try{entity._addDate = reader.GetMySqlDateTime(2).Value;}catch{}
                if (!reader.IsDBNull(3))
                    entity._userId = (long)reader.GetInt64(3);
                if (!reader.IsDBNull(4))
                    entity._deviceId = reader.GetString(4).ToString();
                if (!reader.IsDBNull(5))
                    entity._os = reader.GetString(5).ToString();
                if (!reader.IsDBNull(6))
                    entity._app = reader.GetString(6).ToString();
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
                case "DeviceInfo":
                    return MySqlDbType.Text;
                case "AddDate":
                    return MySqlDbType.DateTime;
                case "UserId":
                    return MySqlDbType.Int64;
                case "DeviceId":
                    return MySqlDbType.VarString;
                case "Os":
                    return MySqlDbType.VarString;
                case "App":
                    return MySqlDbType.VarString;
            }
            return MySqlDbType.VarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        private void CreateFullSqlParameter(UserDeviceData entity, MySqlCommand cmd)
        {
            //02:标识(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:设备的详细信息(DeviceInfo)
            var isNull = string.IsNullOrWhiteSpace(entity.DeviceInfo);
            var parameter = new MySqlParameter("DeviceInfo",MySqlDbType.Text , isNull ? 10 : (entity.DeviceInfo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.DeviceInfo;
            cmd.Parameters.Add(parameter);
            //04:新增日期(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new MySqlParameter("AddDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
            //05:用户Id(UserId)
            cmd.Parameters.Add(new MySqlParameter("UserId",MySqlDbType.Int64){ Value = entity.UserId});
            //06:设备识别码(DeviceId)
            isNull = string.IsNullOrWhiteSpace(entity.DeviceId);
            parameter = new MySqlParameter("DeviceId",MySqlDbType.VarString , isNull ? 10 : (entity.DeviceId).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.DeviceId;
            cmd.Parameters.Add(parameter);
            //07:设备操作系统(Os)
            isNull = string.IsNullOrWhiteSpace(entity.Os);
            parameter = new MySqlParameter("Os",MySqlDbType.VarString , isNull ? 10 : (entity.Os).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Os;
            cmd.Parameters.Add(parameter);
            //08:登录的应用(App)
            isNull = string.IsNullOrWhiteSpace(entity.App);
            parameter = new MySqlParameter("App",MySqlDbType.VarString , isNull ? 10 : (entity.App).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.App;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(UserDeviceData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(UserDeviceData entity, MySqlCommand cmd)
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
        public override void SimpleLoad(MySqlDataReader reader,UserDeviceData entity)
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
        /// 用户设备信息的结构语句
        /// </summary>
        private TableSql _tb_auth_user_deviceSql = new TableSql
        {
            TableName = "tb_auth_user_device",
            PimaryKey = "Id"
        };


        /// <summary>
        /// 用户设备信息数据访问对象
        /// </summary>
        private UserDeviceDataAccess _userDevices;

        /// <summary>
        /// 用户设备信息数据访问对象
        /// </summary>
        public UserDeviceDataAccess UserDevices
        {
            get
            {
                return this._userDevices ?? ( this._userDevices = new UserDeviceDataAccess{ DataBase = this});
            }
        }
    }
}