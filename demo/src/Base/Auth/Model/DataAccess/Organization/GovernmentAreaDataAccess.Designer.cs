/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/3 0:16:20*/
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
    /// 行政区域
    /// </summary>
    public partial class GovernmentAreaDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public GovernmentAreaDataAccess()
        {
            Name = GovernmentAreaData._DataStruct_.EntityName;
            Caption = GovernmentAreaData._DataStruct_.EntityCaption;
            Description = GovernmentAreaData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => GovernmentAreaData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_org_area";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_org_area";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => GovernmentAreaData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `Id`,
    `code` AS `Code`,
    `full_name` AS `FullName`,
    `short_name` AS `ShortName`,
    `tree_name` AS `TreeName`,
    `org_level` AS `OrgLevel`,
    `level_index` AS `LevelIndex`,
    `parent_id` AS `ParentId`,
    `memo` AS `Memo`,
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
INSERT INTO `tb_org_area`
(
    `code`,
    `full_name`,
    `short_name`,
    `tree_name`,
    `org_level`,
    `level_index`,
    `parent_id`,
    `memo`,
    `is_freeze`,
    `data_state`,
    `add_date`,
    `last_reviser_id`,
    `author_id`
)
VALUES
(
    ?Code,
    ?FullName,
    ?ShortName,
    ?TreeName,
    ?OrgLevel,
    ?LevelIndex,
    ?ParentId,
    ?Memo,
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
UPDATE `tb_org_area` SET
       `code` = ?Code,
       `full_name` = ?FullName,
       `short_name` = ?ShortName,
       `tree_name` = ?TreeName,
       `org_level` = ?OrgLevel,
       `level_index` = ?LevelIndex,
       `parent_id` = ?ParentId,
       `memo` = ?Memo,
       `is_freeze` = ?IsFreeze,
       `data_state` = ?DataState,
       `last_reviser_id` = ?LastReviserId
 WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(GovernmentAreaData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_org_area` SET");
            //编码
            if (data.__EntityStatus.ModifiedProperties[GovernmentAreaData._DataStruct_.Real_Code] > 0)
                sql.AppendLine("       `code` = ?Code");
            //全称
            if (data.__EntityStatus.ModifiedProperties[GovernmentAreaData._DataStruct_.Real_FullName] > 0)
                sql.AppendLine("       `full_name` = ?FullName");
            //简称
            if (data.__EntityStatus.ModifiedProperties[GovernmentAreaData._DataStruct_.Real_ShortName] > 0)
                sql.AppendLine("       `short_name` = ?ShortName");
            //树形名称
            if (data.__EntityStatus.ModifiedProperties[GovernmentAreaData._DataStruct_.Real_TreeName] > 0)
                sql.AppendLine("       `tree_name` = ?TreeName");
            //级别
            if (data.__EntityStatus.ModifiedProperties[GovernmentAreaData._DataStruct_.Real_OrgLevel] > 0)
                sql.AppendLine("       `org_level` = ?OrgLevel");
            //层级的序号
            if (data.__EntityStatus.ModifiedProperties[GovernmentAreaData._DataStruct_.Real_LevelIndex] > 0)
                sql.AppendLine("       `level_index` = ?LevelIndex");
            //上级标识
            if (data.__EntityStatus.ModifiedProperties[GovernmentAreaData._DataStruct_.Real_ParentId] > 0)
                sql.AppendLine("       `parent_id` = ?ParentId");
            //备注
            if (data.__EntityStatus.ModifiedProperties[GovernmentAreaData._DataStruct_.Real_Memo] > 0)
                sql.AppendLine("       `memo` = ?Memo");
            //冻结更新
            if (data.__EntityStatus.ModifiedProperties[GovernmentAreaData._DataStruct_.Real_IsFreeze] > 0)
                sql.AppendLine("       `is_freeze` = ?IsFreeze");
            //数据状态
            if (data.__EntityStatus.ModifiedProperties[GovernmentAreaData._DataStruct_.Real_DataState] > 0)
                sql.AppendLine("       `data_state` = ?DataState");
            //最后修改者
            if (data.__EntityStatus.ModifiedProperties[GovernmentAreaData._DataStruct_.Real_LastReviserId] > 0)
                sql.AppendLine("       `last_reviser_id` = ?LastReviserId");
            sql.Append(" WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","Code","FullName","ShortName","TreeName","OrgLevel","LevelIndex","ParentId","Memo","IsFreeze","DataState","AddDate","LastReviserId","LastModifyDate","AuthorId" };

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
            { "Code" , "code" },
            { "FullName" , "full_name" },
            { "full_name" , "full_name" },
            { "ShortName" , "short_name" },
            { "short_name" , "short_name" },
            { "TreeName" , "tree_name" },
            { "tree_name" , "tree_name" },
            { "OrgLevel" , "org_level" },
            { "org_level" , "org_level" },
            { "LevelIndex" , "level_index" },
            { "level_index" , "level_index" },
            { "ParentId" , "parent_id" },
            { "parent_id" , "parent_id" },
            { "Memo" , "memo" },
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
        protected sealed override void LoadEntity(MySqlDataReader reader,GovernmentAreaData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._code = reader.GetString(1);
                if (!reader.IsDBNull(2))
                    entity._fullName = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._shortName = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._treeName = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._orgLevel = (int)reader.GetInt32(5);
                if (!reader.IsDBNull(6))
                    entity._levelIndex = (int)reader.GetInt32(6);
                if (!reader.IsDBNull(7))
                    entity._parentId = (long)reader.GetInt64(7);
                if (!reader.IsDBNull(8))
                    entity._memo = reader.GetString(8).ToString();
                if (!reader.IsDBNull(9))
                    entity._isFreeze = (bool)reader.GetBoolean(9);
                if (!reader.IsDBNull(10))
                    entity._dataState = (DataStateType)reader.GetInt32(10);
                if (!reader.IsDBNull(11))
                    try{entity._addDate = reader.GetMySqlDateTime(11).Value;}catch{}
                if (!reader.IsDBNull(12))
                    entity._lastReviserId = (long)reader.GetInt64(12);
                if (!reader.IsDBNull(13))
                    try{entity._lastModifyDate = reader.GetMySqlDateTime(13).Value;}catch{}
                if (!reader.IsDBNull(14))
                    entity._authorId = (long)reader.GetInt64(14);
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
                case "Code":
                    return MySqlDbType.VarString;
                case "FullName":
                    return MySqlDbType.VarString;
                case "ShortName":
                    return MySqlDbType.VarString;
                case "TreeName":
                    return MySqlDbType.VarString;
                case "OrgLevel":
                    return MySqlDbType.Int32;
                case "LevelIndex":
                    return MySqlDbType.Int32;
                case "ParentId":
                    return MySqlDbType.Int64;
                case "Memo":
                    return MySqlDbType.Text;
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
        private void CreateFullSqlParameter(GovernmentAreaData entity, MySqlCommand cmd)
        {
            //02:标识(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:编码(Code)
            var isNull = string.IsNullOrWhiteSpace(entity.Code);
            var parameter = new MySqlParameter("Code",MySqlDbType.VarString , isNull ? 10 : (entity.Code).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Code;
            cmd.Parameters.Add(parameter);
            //04:全称(FullName)
            isNull = string.IsNullOrWhiteSpace(entity.FullName);
            parameter = new MySqlParameter("FullName",MySqlDbType.VarString , isNull ? 10 : (entity.FullName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.FullName;
            cmd.Parameters.Add(parameter);
            //05:简称(ShortName)
            isNull = string.IsNullOrWhiteSpace(entity.ShortName);
            parameter = new MySqlParameter("ShortName",MySqlDbType.VarString , isNull ? 10 : (entity.ShortName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ShortName;
            cmd.Parameters.Add(parameter);
            //06:树形名称(TreeName)
            isNull = string.IsNullOrWhiteSpace(entity.TreeName);
            parameter = new MySqlParameter("TreeName",MySqlDbType.VarString , isNull ? 10 : (entity.TreeName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.TreeName;
            cmd.Parameters.Add(parameter);
            //07:级别(OrgLevel)
            cmd.Parameters.Add(new MySqlParameter("OrgLevel",MySqlDbType.Int32){ Value = entity.OrgLevel});
            //08:层级的序号(LevelIndex)
            cmd.Parameters.Add(new MySqlParameter("LevelIndex",MySqlDbType.Int32){ Value = entity.LevelIndex});
            //09:上级标识(ParentId)
            cmd.Parameters.Add(new MySqlParameter("ParentId",MySqlDbType.Int64){ Value = entity.ParentId});
            //10:备注(Memo)
            isNull = string.IsNullOrWhiteSpace(entity.Memo);
            parameter = new MySqlParameter("Memo",MySqlDbType.Text , isNull ? 10 : (entity.Memo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Memo;
            cmd.Parameters.Add(parameter);
            //11:冻结更新(IsFreeze)
            cmd.Parameters.Add(new MySqlParameter("IsFreeze",MySqlDbType.Byte) { Value = entity.IsFreeze ? (byte)1 : (byte)0 });
            //12:数据状态(DataState)
            cmd.Parameters.Add(new MySqlParameter("DataState",MySqlDbType.Int32){ Value = (int)entity.DataState});
            //13:制作时间(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new MySqlParameter("AddDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
            //14:最后修改者(LastReviserId)
            cmd.Parameters.Add(new MySqlParameter("LastReviserId",MySqlDbType.Int64){ Value = entity.LastReviserId});
            //15:最后修改日期(LastModifyDate)
            isNull = entity.LastModifyDate.Year < 1900;
            parameter = new MySqlParameter("LastModifyDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastModifyDate;
            cmd.Parameters.Add(parameter);
            //16:制作人(AuthorId)
            cmd.Parameters.Add(new MySqlParameter("AuthorId",MySqlDbType.Int64){ Value = entity.AuthorId});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(GovernmentAreaData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(GovernmentAreaData entity, MySqlCommand cmd)
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
    `code` AS `Code`,
    `full_name` AS `FullName`,
    `short_name` AS `ShortName`,
    `tree_name` AS `TreeName`,
    `org_level` AS `OrgLevel`,
    `level_index` AS `LevelIndex`,
    `parent_id` AS `ParentId`,
    `memo` AS `Memo`,
    `is_freeze` AS `IsFreeze`,
    `data_state` AS `DataState`";
            }
        }


        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="entity">读取数据的实体</param>
        public override void SimpleLoad(MySqlDataReader reader,GovernmentAreaData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._code = reader.GetString(1);
                if (!reader.IsDBNull(2))
                    entity._fullName = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._shortName = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._treeName = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._orgLevel = (int)reader.GetInt32(5);
                if (!reader.IsDBNull(6))
                    entity._levelIndex = (int)reader.GetInt32(6);
                if (!reader.IsDBNull(7))
                    entity._parentId = (long)reader.GetInt64(7);
                if (!reader.IsDBNull(8))
                    entity._memo = reader.GetString(8).ToString();
                if (!reader.IsDBNull(9))
                    entity._isFreeze = (bool)reader.GetBoolean(9);
                if (!reader.IsDBNull(10))
                    entity._dataState = (DataStateType)reader.GetInt32(10);
            }
        }
        #endregion

        
    }
    
    partial class UserCenterDb
    {


        /// <summary>
        /// 行政区域的结构语句
        /// </summary>
        private TableSql _tb_org_areaSql = new TableSql
        {
            TableName = "tb_org_area",
            PimaryKey = "Id"
        };


        /// <summary>
        /// 行政区域数据访问对象
        /// </summary>
        private GovernmentAreaDataAccess _governmentAreas;

        /// <summary>
        /// 行政区域数据访问对象
        /// </summary>
        public GovernmentAreaDataAccess GovernmentAreas
        {
            get
            {
                return this._governmentAreas ?? ( this._governmentAreas = new GovernmentAreaDataAccess{ DataBase = this});
            }
        }
    }
}