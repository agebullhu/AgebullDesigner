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
    /// 微信联合认证关联的用户信息
    /// </summary>
    public partial class WechatDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public WechatDataAccess()
        {
            Name = WechatData._DataStruct_.EntityName;
            Caption = WechatData._DataStruct_.EntityCaption;
            Description = WechatData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => WechatData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_user_wechat";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_user_wechat";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => WechatData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `user_id` AS `UserId`,
    `union_id` AS `UnionId`,
    `open_id` AS `OpenId`,
    `nick_name` AS `NickName`,
    `avatar_url` AS `AvatarUrl`,
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
INSERT INTO `tb_user_wechat`
(
    `user_id`,
    `union_id`,
    `open_id`,
    `nick_name`,
    `avatar_url`,
    `is_freeze`,
    `data_state`,
    `add_date`,
    `last_reviser_id`,
    `author_id`
)
VALUES
(
    ?UserId,
    ?UnionId,
    ?OpenId,
    ?NickName,
    ?AvatarUrl,
    ?IsFreeze,
    ?DataState,
    ?AddDate,
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
UPDATE `tb_user_wechat` SET
       `union_id` = ?UnionId,
       `open_id` = ?OpenId,
       `nick_name` = ?NickName,
       `avatar_url` = ?AvatarUrl,
       `is_freeze` = ?IsFreeze,
       `data_state` = ?DataState,
       `last_reviser_id` = ?LastReviserId
 WHERE `user_id` = ?UserId AND `is_freeze` = 0 AND `data_state` < 255;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(WechatData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_user_wechat` SET");
            //UnionId
            if (data.__EntityStatus.ModifiedProperties[WechatData._DataStruct_.Real_UnionId] > 0)
                sql.AppendLine("       `union_id` = ?UnionId");
            //OpenId
            if (data.__EntityStatus.ModifiedProperties[WechatData._DataStruct_.Real_OpenId] > 0)
                sql.AppendLine("       `open_id` = ?OpenId");
            //昵称
            if (data.__EntityStatus.ModifiedProperties[WechatData._DataStruct_.Real_NickName] > 0)
                sql.AppendLine("       `nick_name` = ?NickName");
            //头像
            if (data.__EntityStatus.ModifiedProperties[WechatData._DataStruct_.Real_AvatarUrl] > 0)
                sql.AppendLine("       `avatar_url` = ?AvatarUrl");
            //冻结更新
            if (data.__EntityStatus.ModifiedProperties[WechatData._DataStruct_.Real_IsFreeze] > 0)
                sql.AppendLine("       `is_freeze` = ?IsFreeze");
            //数据状态
            if (data.__EntityStatus.ModifiedProperties[WechatData._DataStruct_.Real_DataState] > 0)
                sql.AppendLine("       `data_state` = ?DataState");
            //最后修改者
            if (data.__EntityStatus.ModifiedProperties[WechatData._DataStruct_.Real_LastReviserId] > 0)
                sql.AppendLine("       `last_reviser_id` = ?LastReviserId");
            sql.Append(" WHERE `user_id` = ?UserId AND `is_freeze` = 0 AND `data_state` < 255;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "UserId","UnionId","OpenId","NickName","AvatarUrl","IsFreeze","DataState","AddDate","LastReviserId","LastModifyDate","AuthorId" };

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
            { "UnionId" , "union_id" },
            { "union_id" , "union_id" },
            { "OpenId" , "open_id" },
            { "open_id" , "open_id" },
            { "NickName" , "nick_name" },
            { "nick_name" , "nick_name" },
            { "AvatarUrl" , "avatar_url" },
            { "avatar_url" , "avatar_url" },
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
        protected sealed override void LoadEntity(MySqlDataReader reader,WechatData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._userId = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._unionId = reader.GetString(1).ToString();
                if (!reader.IsDBNull(2))
                    entity._openId = reader.GetString(2).ToString();
                if (!reader.IsDBNull(3))
                    entity._nickName = reader.GetString(3).ToString();
                if (!reader.IsDBNull(4))
                    entity._avatarUrl = reader.GetString(4).ToString();
                if (!reader.IsDBNull(5))
                    entity._isFreeze = (bool)reader.GetBoolean(5);
                if (!reader.IsDBNull(6))
                    entity._dataState = (DataStateType)reader.GetInt32(6);
                if (!reader.IsDBNull(7))
                    try{entity._addDate = reader.GetMySqlDateTime(7).Value;}catch{}
                if (!reader.IsDBNull(8))
                    entity._lastReviserId = (long)reader.GetInt64(8);
                if (!reader.IsDBNull(9))
                    try{entity._lastModifyDate = reader.GetMySqlDateTime(9).Value;}catch{}
                if (!reader.IsDBNull(10))
                    entity._authorId = (long)reader.GetInt64(10);
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
                case "UnionId":
                    return MySqlDbType.VarString;
                case "OpenId":
                    return MySqlDbType.VarString;
                case "NickName":
                    return MySqlDbType.VarString;
                case "AvatarUrl":
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
        private void CreateFullSqlParameter(WechatData entity, MySqlCommand cmd)
        {
            //02:用户标识(UserId)
            cmd.Parameters.Add(new MySqlParameter("UserId",MySqlDbType.Int64){ Value = entity.UserId});
            //03:UnionId(UnionId)
            var isNull = string.IsNullOrWhiteSpace(entity.UnionId);
            var parameter = new MySqlParameter("UnionId",MySqlDbType.VarString , isNull ? 10 : (entity.UnionId).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.UnionId;
            cmd.Parameters.Add(parameter);
            //04:OpenId(OpenId)
            isNull = string.IsNullOrWhiteSpace(entity.OpenId);
            parameter = new MySqlParameter("OpenId",MySqlDbType.VarString , isNull ? 10 : (entity.OpenId).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OpenId;
            cmd.Parameters.Add(parameter);
            //05:昵称(NickName)
            isNull = string.IsNullOrWhiteSpace(entity.NickName);
            parameter = new MySqlParameter("NickName",MySqlDbType.VarString , isNull ? 10 : (entity.NickName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.NickName;
            cmd.Parameters.Add(parameter);
            //06:头像(AvatarUrl)
            isNull = string.IsNullOrWhiteSpace(entity.AvatarUrl);
            parameter = new MySqlParameter("AvatarUrl",MySqlDbType.VarString , isNull ? 10 : (entity.AvatarUrl).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AvatarUrl;
            cmd.Parameters.Add(parameter);
            //07:冻结更新(IsFreeze)
            cmd.Parameters.Add(new MySqlParameter("IsFreeze",MySqlDbType.Byte) { Value = entity.IsFreeze ? (byte)1 : (byte)0 });
            //08:数据状态(DataState)
            cmd.Parameters.Add(new MySqlParameter("DataState",MySqlDbType.Int32){ Value = (int)entity.DataState});
            //09:制作时间(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new MySqlParameter("AddDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
            //10:最后修改者(LastReviserId)
            cmd.Parameters.Add(new MySqlParameter("LastReviserId",MySqlDbType.Int64){ Value = entity.LastReviserId});
            //11:最后修改日期(LastModifyDate)
            isNull = entity.LastModifyDate.Year < 1900;
            parameter = new MySqlParameter("LastModifyDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastModifyDate;
            cmd.Parameters.Add(parameter);
            //12:制作人(AuthorId)
            cmd.Parameters.Add(new MySqlParameter("AuthorId",MySqlDbType.Int64){ Value = entity.AuthorId});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(WechatData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(WechatData entity, MySqlCommand cmd)
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
        public override void SimpleLoad(MySqlDataReader reader,WechatData entity)
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
        /// 微信联合认证关联的用户信息的结构语句
        /// </summary>
        private TableSql _tb_user_wechatSql = new TableSql
        {
            TableName = "tb_user_wechat",
            PimaryKey = "UserId"
        };


        /// <summary>
        /// 微信联合认证关联的用户信息数据访问对象
        /// </summary>
        private WechatDataAccess _wechats;

        /// <summary>
        /// 微信联合认证关联的用户信息数据访问对象
        /// </summary>
        public WechatDataAccess Wechats
        {
            get
            {
                return this._wechats ?? ( this._wechats = new WechatDataAccess{ DataBase = this});
            }
        }
    }
}