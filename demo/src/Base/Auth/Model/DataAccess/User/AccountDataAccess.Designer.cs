/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/1/2 21:22:57*/
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
using Agebull.MicroZero.ZeroApis;

using Agebull.Common;
using Agebull.EntityModel.Common;


using Agebull.EntityModel.Interfaces;
using Agebull.EntityModel.MySql;
#endregion

namespace Agebull.Common.OAuth.DataAccess
{
    /// <summary>
    /// 用于支持用户的账户名密码登录
    /// </summary>
    public partial class AccountDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public AccountDataAccess()
        {
            Name = AccountData._DataStruct_.EntityName;
            Caption = AccountData._DataStruct_.EntityCaption;
            Description = AccountData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => AccountData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"view_user_account";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_user_account";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => AccountData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `Id`,
    `role_id` AS `RoleId`,
    `role` AS `Role`,
    `user_id` AS `UserId`,
    `nick_name` AS `NickName`,
    `id_card` AS `IdCard`,
    `phone_number` AS `PhoneNumber`,
    `real_name` AS `RealName`,
    `account_name` AS `AccountName`,
    `password` AS `Password`,
    `is_freeze` AS `IsFreeze`,
    `data_state` AS `DataState`,
    `add_date` AS `AddDate`,
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
INSERT INTO `tb_user_account`
(
    `role_id`,
    `user_id`,
    `account_name`,
    `password`,
    `is_freeze`,
    `data_state`,
    `add_date`,
    `last_reviser_id`,
    `author_id`
)
VALUES
(
    ?RoleId,
    ?UserId,
    ?AccountName,
    ?Password,
    ?IsFreeze,
    ?DataState,
    ?AddDate,
    ?LastReviserId,
    ?AuthorId
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
UPDATE `tb_user_account` SET
       `role_id` = ?RoleId,
       `user_id` = ?UserId,
       `account_name` = ?AccountName,
       `password` = ?Password,
       `is_freeze` = ?IsFreeze,
       `data_state` = ?DataState,
       `last_reviser_id` = ?LastReviserId
 WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(AccountData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_user_account` SET");
            //角色标识
            if (data.__EntityStatus.ModifiedProperties[AccountData._DataStruct_.Real_RoleId] > 0)
                sql.AppendLine("       `role_id` = ?RoleId");
            //用户Id
            if (data.__EntityStatus.ModifiedProperties[AccountData._DataStruct_.Real_UserId] > 0)
                sql.AppendLine("       `user_id` = ?UserId");
            //账户名
            if (data.__EntityStatus.ModifiedProperties[AccountData._DataStruct_.Real_AccountName] > 0)
                sql.AppendLine("       `account_name` = ?AccountName");
            //用户密码
            if (data.__EntityStatus.ModifiedProperties[AccountData._DataStruct_.Real_Password] > 0)
                sql.AppendLine("       `password` = ?Password");
            //冻结更新
            if (data.__EntityStatus.ModifiedProperties[AccountData._DataStruct_.Real_IsFreeze] > 0)
                sql.AppendLine("       `is_freeze` = ?IsFreeze");
            //数据状态
            if (data.__EntityStatus.ModifiedProperties[AccountData._DataStruct_.Real_DataState] > 0)
                sql.AppendLine("       `data_state` = ?DataState");
            //最后修改者
            if (data.__EntityStatus.ModifiedProperties[AccountData._DataStruct_.Real_LastReviserId] > 0)
                sql.AppendLine("       `last_reviser_id` = ?LastReviserId");
            sql.Append(" WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","RoleId","Role","UserId","NickName","IdCard","PhoneNumber","RealName","AccountName","Password","IsFreeze","DataState","AddDate","LastReviserId","LastModifyDate","AuthorId" };

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
            { "RoleId" , "role_id" },
            { "role_id" , "role_id" },
            { "Role" , "role" },
            { "UserId" , "user_id" },
            { "user_id" , "user_id" },
            { "NickName" , "nick_name" },
            { "nick_name" , "nick_name" },
            { "IdCard" , "id_card" },
            { "id_card" , "id_card" },
            { "PhoneNumber" , "phone_number" },
            { "phone_number" , "phone_number" },
            { "RealName" , "real_name" },
            { "real_name" , "real_name" },
            { "AccountName" , "account_name" },
            { "account_name" , "account_name" },
            { "Password" , "password" },
            { "IsFreeze" , "is_freeze" },
            { "is_freeze" , "is_freeze" },
            { "DataState" , "data_state" },
            { "data_state" , "data_state" },
            { "AddDate" , "add_date" },
            { "add_date" , "add_date" },
            { "LastReviserId" , "last_reviser_id" },
            { "last_reviser_id" , "last_reviser_id" },
            { "LastModifyDate" , "last_modify_date" },
            { "last_modify_date" , "last_modify_date" },
            { "AuthorId" , "author_id" },
            { "author_id" , "author_id" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,AccountData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._roleId = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._role = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._userId = (long)reader.GetInt64(3);
                if (!reader.IsDBNull(4))
                    entity._nickName = reader.GetString(4).ToString();
                if (!reader.IsDBNull(5))
                    entity._idCard = reader.GetString(5).ToString();
                if (!reader.IsDBNull(6))
                    entity._phoneNumber = reader.GetString(6).ToString();
                if (!reader.IsDBNull(7))
                    entity._realName = reader.GetString(7).ToString();
                if (!reader.IsDBNull(8))
                    entity._accountName = reader.GetString(8).ToString();
                if (!reader.IsDBNull(9))
                    entity._password = reader.GetString(9).ToString();
                if (!reader.IsDBNull(10))
                    entity._isFreeze = (bool)reader.GetBoolean(10);
                if (!reader.IsDBNull(11))
                    entity._dataState = (DataStateType)reader.GetInt32(11);
                if (!reader.IsDBNull(12))
                    try{entity._addDate = reader.GetMySqlDateTime(12).Value;}catch{}
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
                case "Id":
                    return MySqlDbType.Int64;
                case "RoleId":
                    return MySqlDbType.Int64;
                case "Role":
                    return MySqlDbType.VarString;
                case "UserId":
                    return MySqlDbType.Int64;
                case "NickName":
                    return MySqlDbType.VarString;
                case "IdCard":
                    return MySqlDbType.VarString;
                case "PhoneNumber":
                    return MySqlDbType.VarString;
                case "RealName":
                    return MySqlDbType.VarString;
                case "AccountName":
                    return MySqlDbType.VarString;
                case "Password":
                    return MySqlDbType.VarString;
                case "IsFreeze":
                    return MySqlDbType.Byte;
                case "DataState":
                    return MySqlDbType.Int32;
                case "AddDate":
                    return MySqlDbType.DateTime;
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
        private void CreateFullSqlParameter(AccountData entity, MySqlCommand cmd)
        {
            //02:标识主键(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:角色标识(RoleId)
            cmd.Parameters.Add(new MySqlParameter("RoleId",MySqlDbType.Int64){ Value = entity.RoleId});
            //04:角色(Role)
            var isNull = string.IsNullOrWhiteSpace(entity.Role);
            var parameter = new MySqlParameter("Role",MySqlDbType.VarString , isNull ? 10 : (entity.Role).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Role;
            cmd.Parameters.Add(parameter);
            //05:用户Id(UserId)
            cmd.Parameters.Add(new MySqlParameter("UserId",MySqlDbType.Int64){ Value = entity.UserId});
            //06:昵称(NickName)
            isNull = string.IsNullOrWhiteSpace(entity.NickName);
            parameter = new MySqlParameter("NickName",MySqlDbType.VarString , isNull ? 10 : (entity.NickName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.NickName;
            cmd.Parameters.Add(parameter);
            //07:身份证号(IdCard)
            isNull = string.IsNullOrWhiteSpace(entity.IdCard);
            parameter = new MySqlParameter("IdCard",MySqlDbType.VarString , isNull ? 10 : (entity.IdCard).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.IdCard;
            cmd.Parameters.Add(parameter);
            //08:手机号(PhoneNumber)
            isNull = string.IsNullOrWhiteSpace(entity.PhoneNumber);
            parameter = new MySqlParameter("PhoneNumber",MySqlDbType.VarString , isNull ? 10 : (entity.PhoneNumber).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.PhoneNumber;
            cmd.Parameters.Add(parameter);
            //09:真实姓名(RealName)
            isNull = string.IsNullOrWhiteSpace(entity.RealName);
            parameter = new MySqlParameter("RealName",MySqlDbType.VarString , isNull ? 10 : (entity.RealName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.RealName;
            cmd.Parameters.Add(parameter);
            //10:账户名(AccountName)
            isNull = string.IsNullOrWhiteSpace(entity.AccountName);
            parameter = new MySqlParameter("AccountName",MySqlDbType.VarString , isNull ? 10 : (entity.AccountName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AccountName;
            cmd.Parameters.Add(parameter);
            //11:用户密码(Password)
            isNull = string.IsNullOrWhiteSpace(entity.Password);
            parameter = new MySqlParameter("Password",MySqlDbType.VarString , isNull ? 10 : (entity.Password).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Password;
            cmd.Parameters.Add(parameter);
            //12:冻结更新(IsFreeze)
            cmd.Parameters.Add(new MySqlParameter("IsFreeze",MySqlDbType.Byte) { Value = entity.IsFreeze ? (byte)1 : (byte)0 });
            //13:数据状态(DataState)
            cmd.Parameters.Add(new MySqlParameter("DataState",MySqlDbType.Int32){ Value = (int)entity.DataState});
            //14:制作时间(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new MySqlParameter("AddDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
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
        protected sealed override void SetUpdateCommand(AccountData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(AccountData entity, MySqlCommand cmd)
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
        public override void SimpleLoad(MySqlDataReader reader,AccountData entity)
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
        /// 用于支持用户的账户名密码登录的结构语句
        /// </summary>
        private TableSql _view_user_accountSql = new TableSql
        {
            TableName = "view_user_account",
            PimaryKey = "Id"
        };


        /// <summary>
        /// 用于支持用户的账户名密码登录数据访问对象
        /// </summary>
        private AccountDataAccess _accounts;

        /// <summary>
        /// 用于支持用户的账户名密码登录数据访问对象
        /// </summary>
        public AccountDataAccess Accounts
        {
            get
            {
                return this._accounts ?? ( this._accounts = new AccountDataAccess{ DataBase = this});
            }
        }
    }
}