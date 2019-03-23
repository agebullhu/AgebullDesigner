/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/22 16:21:16*/
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
using Agebull.Common.Organizations;
using Agebull.Common.OAuth;

#endregion

namespace Agebull.Common.AppManage.DataAccess
{
    /// <summary>
    /// 系统内的应用的信息
    /// </summary>
    public partial class AppInfoDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public AppInfoDataAccess()
        {
            Name = AppInfoData._DataStruct_.EntityName;
            Caption = AppInfoData._DataStruct_.EntityCaption;
            Description = AppInfoData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => AppInfoData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_app_app_info";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_app_app_info";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => AppInfoData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `Id`,
    `app_type` AS `AppType`,
    `short_name` AS `ShortName`,
    `full_name` AS `FullName`,
    `app_id` AS `AppId`,
    `app_key` AS `AppKey`,
    `memo` AS `Memo`,
    `data_state_type` AS `DataStateType`,
    `is_freeze` AS `IsFreeze`,
    `add_date` AS `AddDate`,
    `author_id` AS `AuthorId`,
    `last_reviser_id` AS `LastReviserId`,
    `last_modify_date` AS `LastModifyDate`,
    `data_state` AS `DataState`";
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
INSERT INTO `tb_app_app_info`
(
    `app_type`,
    `short_name`,
    `full_name`,
    `app_id`,
    `app_key`,
    `memo`,
    `data_state_type`,
    `is_freeze`,
    `add_date`,
    `author_id`,
    `last_reviser_id`,
    `data_state`
)
VALUES
(
    ?AppType,
    ?ShortName,
    ?FullName,
    ?AppId,
    ?AppKey,
    ?Memo,
    ?DataStateType,
    ?IsFreeze,
    ?AddDate,
    ?AuthorId,
    ?LastReviserId,
    ?DataState
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
UPDATE `tb_app_app_info` SET
       `app_type` = ?AppType,
       `short_name` = ?ShortName,
       `full_name` = ?FullName,
       `app_id` = ?AppId,
       `app_key` = ?AppKey,
       `memo` = ?Memo,
       `data_state_type` = ?DataStateType,
       `is_freeze` = ?IsFreeze,
       `last_reviser_id` = ?LastReviserId,
       `data_state` = ?DataState
 WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(AppInfoData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_app_app_info` SET");
            //应用类型
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_AppType] > 0)
                sql.AppendLine("       `app_type` = ?AppType");
            //应用简称
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_ShortName] > 0)
                sql.AppendLine("       `short_name` = ?ShortName");
            //应用全称
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_FullName] > 0)
                sql.AppendLine("       `full_name` = ?FullName");
            //应用标识
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_AppId] > 0)
                sql.AppendLine("       `app_id` = ?AppId");
            //应用令牌
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_AppKey] > 0)
                sql.AppendLine("       `app_key` = ?AppKey");
            //备注
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_Memo] > 0)
                sql.AppendLine("       `memo` = ?Memo");
            //数据状态枚举类型
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_DataStateType] > 0)
                sql.AppendLine("       `data_state_type` = ?DataStateType");
            //冻结更新
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_IsFreeze] > 0)
                sql.AppendLine("       `is_freeze` = ?IsFreeze");
            //最后修改者
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_LastReviserId] > 0)
                sql.AppendLine("       `last_reviser_id` = ?LastReviserId");
            //数据状态
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_DataState] > 0)
                sql.AppendLine("       `data_state` = ?DataState");
            sql.Append(" WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","AppType","ShortName","FullName","AppId","AppKey","Memo","DataStateType","IsFreeze","AddDate","AuthorId","LastReviserId","LastModifyDate","DataState" };

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
            { "AppType" , "app_type" },
            { "app_type" , "app_type" },
            { "ShortName" , "short_name" },
            { "short_name" , "short_name" },
            { "FullName" , "full_name" },
            { "full_name" , "full_name" },
            { "AppId" , "app_id" },
            { "app_id" , "app_id" },
            { "AppKey" , "app_key" },
            { "app_key" , "app_key" },
            { "Memo" , "memo" },
            { "DataStateType" , "data_state_type" },
            { "data_state_type" , "data_state_type" },
            { "IsFreeze" , "is_freeze" },
            { "is_freeze" , "is_freeze" },
            { "AddDate" , "add_date" },
            { "add_date" , "add_date" },
            { "AuthorId" , "author_id" },
            { "author_id" , "author_id" },
            { "LastReviserId" , "last_reviser_id" },
            { "last_reviser_id" , "last_reviser_id" },
            { "LastModifyDate" , "last_modify_date" },
            { "last_modify_date" , "last_modify_date" },
            { "DataState" , "data_state" },
            { "data_state" , "data_state" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,AppInfoData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._appType = (AppType)reader.GetInt32(1);
                if (!reader.IsDBNull(2))
                    entity._shortName = reader.GetString(2).ToString();
                if (!reader.IsDBNull(3))
                    entity._fullName = reader.GetString(3).ToString();
                if (!reader.IsDBNull(4))
                    entity._appId = reader.GetString(4).ToString();
                if (!reader.IsDBNull(5))
                    entity._appKey = reader.GetString(5).ToString();
                if (!reader.IsDBNull(6))
                    entity._memo = reader.GetString(6).ToString();
                if (!reader.IsDBNull(7))
                    entity._dataStateType = (DataStateType)reader.GetInt32(7);
                if (!reader.IsDBNull(8))
                    entity._isFreeze = (bool)reader.GetBoolean(8);
                if (!reader.IsDBNull(9))
                    try{entity._addDate = reader.GetMySqlDateTime(9).Value;}catch{}
                if (!reader.IsDBNull(10))
                    entity._authorId = (long)reader.GetInt64(10);
                if (!reader.IsDBNull(11))
                    entity._lastReviserId = (long)reader.GetInt64(11);
                if (!reader.IsDBNull(12))
                    try{entity._lastModifyDate = reader.GetMySqlDateTime(12).Value;}catch{}
                if (!reader.IsDBNull(13))
                    entity._dataState = (DataStateType)reader.GetInt32(13);
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
                case "AppType":
                    return MySqlDbType.Int32;
                case "ShortName":
                    return MySqlDbType.VarString;
                case "FullName":
                    return MySqlDbType.VarString;
                case "AppId":
                    return MySqlDbType.VarString;
                case "AppKey":
                    return MySqlDbType.VarString;
                case "Memo":
                    return MySqlDbType.Text;
                case "DataStateType":
                    return MySqlDbType.Int32;
                case "IsFreeze":
                    return MySqlDbType.Byte;
                case "AddDate":
                    return MySqlDbType.DateTime;
                case "AuthorId":
                    return MySqlDbType.Int64;
                case "LastReviserId":
                    return MySqlDbType.Int64;
                case "LastModifyDate":
                    return MySqlDbType.DateTime;
                case "DataState":
                    return MySqlDbType.Int32;
            }
            return MySqlDbType.VarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        private void CreateFullSqlParameter(AppInfoData entity, MySqlCommand cmd)
        {
            //02:标识(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:应用类型(AppType)
            cmd.Parameters.Add(new MySqlParameter("AppType",MySqlDbType.Int32){ Value = (int)entity.AppType});
            //04:应用简称(ShortName)
            var isNull = string.IsNullOrWhiteSpace(entity.ShortName);
            var parameter = new MySqlParameter("ShortName",MySqlDbType.VarString , isNull ? 10 : (entity.ShortName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ShortName;
            cmd.Parameters.Add(parameter);
            //05:应用全称(FullName)
            isNull = string.IsNullOrWhiteSpace(entity.FullName);
            parameter = new MySqlParameter("FullName",MySqlDbType.VarString , isNull ? 10 : (entity.FullName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.FullName;
            cmd.Parameters.Add(parameter);
            //06:应用标识(AppId)
            isNull = string.IsNullOrWhiteSpace(entity.AppId);
            parameter = new MySqlParameter("AppId",MySqlDbType.VarString , isNull ? 10 : (entity.AppId).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AppId;
            cmd.Parameters.Add(parameter);
            //07:应用令牌(AppKey)
            isNull = string.IsNullOrWhiteSpace(entity.AppKey);
            parameter = new MySqlParameter("AppKey",MySqlDbType.VarString , isNull ? 10 : (entity.AppKey).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AppKey;
            cmd.Parameters.Add(parameter);
            //08:备注(Memo)
            isNull = string.IsNullOrWhiteSpace(entity.Memo);
            parameter = new MySqlParameter("Memo",MySqlDbType.Text , isNull ? 10 : (entity.Memo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Memo;
            cmd.Parameters.Add(parameter);
            //09:数据状态枚举类型(DataStateType)
            cmd.Parameters.Add(new MySqlParameter("DataStateType",MySqlDbType.Int32){ Value = (int)entity.DataStateType});
            //10:冻结更新(IsFreeze)
            cmd.Parameters.Add(new MySqlParameter("IsFreeze",MySqlDbType.Byte) { Value = entity.IsFreeze ? (byte)1 : (byte)0 });
            //11:制作时间(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new MySqlParameter("AddDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
            //12:制作人(AuthorId)
            cmd.Parameters.Add(new MySqlParameter("AuthorId",MySqlDbType.Int64){ Value = entity.AuthorId});
            //13:最后修改者(LastReviserId)
            cmd.Parameters.Add(new MySqlParameter("LastReviserId",MySqlDbType.Int64){ Value = entity.LastReviserId});
            //14:最后修改日期(LastModifyDate)
            isNull = entity.LastModifyDate.Year < 1900;
            parameter = new MySqlParameter("LastModifyDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastModifyDate;
            cmd.Parameters.Add(parameter);
            //15:数据状态(DataState)
            cmd.Parameters.Add(new MySqlParameter("DataState",MySqlDbType.Int32){ Value = (int)entity.DataState});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(AppInfoData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(AppInfoData entity, MySqlCommand cmd)
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
                return @"
    `id` AS `Id`,
    `app_type` AS `AppType`,
    `short_name` AS `ShortName`,
    `full_name` AS `FullName`,
    `app_id` AS `AppId`,
    `data_state_type` AS `DataStateType`,
    `is_freeze` AS `IsFreeze`";
            }
        }


        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="entity">读取数据的实体</param>
        public override void SimpleLoad(MySqlDataReader reader,AppInfoData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._appType = (AppType)reader.GetInt32(1);
                if (!reader.IsDBNull(2))
                    entity._shortName = reader.GetString(2).ToString();
                if (!reader.IsDBNull(3))
                    entity._fullName = reader.GetString(3).ToString();
                if (!reader.IsDBNull(4))
                    entity._appId = reader.GetString(4).ToString();
                if (!reader.IsDBNull(5))
                    entity._dataStateType = (DataStateType)reader.GetInt32(5);
                if (!reader.IsDBNull(6))
                    entity._isFreeze = (bool)reader.GetBoolean(6);
            }
        }
        #endregion

        
    }
    
    partial class AppManageDb
    {


        /// <summary>
        /// 系统内的应用的信息的结构语句
        /// </summary>
        private TableSql _tb_app_app_infoSql = new TableSql
        {
            TableName = "tb_app_app_info",
            PimaryKey = "Id"
        };


        /// <summary>
        /// 系统内的应用的信息数据访问对象
        /// </summary>
        private AppInfoDataAccess _appInfoes;

        /// <summary>
        /// 系统内的应用的信息数据访问对象
        /// </summary>
        public AppInfoDataAccess AppInfoes
        {
            get
            {
                return this._appInfoes ?? ( this._appInfoes = new AppInfoDataAccess{ DataBase = this});
            }
        }
    }
}