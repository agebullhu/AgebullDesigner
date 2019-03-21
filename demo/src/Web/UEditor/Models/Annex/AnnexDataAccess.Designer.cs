/*design by:agebull designer date:2017/9/16 22:47:59*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Linq;
using System.Text;
using Agebull.EntityModel.Common;

using MySql.Data.MySqlClient;
using Agebull.EntityModel.MySql;
using Agebull.Common.Ioc;

namespace Agebull.ZeroNet.ManageApplication.DataAccess
{
    /// <summary>
    /// 附件
    /// </summary>
    public partial class AnnexDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId
        {
            get { return Table_Annex; }
        }

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_project_annex";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_project_annex";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey
        {
            get
            {
                return @"ID";
            }
        }

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `ID`,
    `title` AS `Title`,
    `annex_type` AS `AnnexType`,
    `entity_type` AS `EntityType`,
    `link_id` AS `LinkId`,
    `file_name` AS `FileName`,
    `is_public` AS `IsPublic`,
    `url` AS `Url`,
    `storage` AS `Storage`,
    `memo` AS `Memo`,
    `data_state` AS `DataState`,
    `is_freeze` AS `IsFreeze`,
    `author_id` AS `AuthorID`,
    `add_date` AS `AddDate`,
    `last_reviser_id` AS `LastReviserID`,
    `last_modify_date` AS `LastModifyDate`,
    `area_id` AS `AreaId`,
    `department_id` AS `DepartmentId`,
    `wx_media_id` AS `WeiXinMediaId`,
    `department_level` AS `DepartmentLevel`";
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
INSERT INTO `tb_project_annex`
(
    `title`,
    `annex_type`,
    `entity_type`,
    `link_id`,
    `file_name`,
    `is_public`,
    `url`,
    `storage`,
    `memo`,
    `data_state`,
    `is_freeze`,
    `author_id`,
    `add_date`,
    `last_reviser_id`,
    `last_modify_date`,
    `area_id`,
    `department_id`,
    `department_level`
)
VALUES
(
    ?Title,
    ?AnnexType,
    ?EntityType,
    ?LinkId,
    ?FileName,
    ?IsPublic,
    ?Url,
    ?Storage,
    ?Memo,
    ?DataState,
    ?IsFreeze,
    ?AuthorID,
    ?AddDate,
    ?LastReviserID,
    ?LastModifyDate,
    ?AreaId,
    ?DepartmentId,
    ?DepartmentLevel
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
UPDATE `tb_project_annex` SET
       `title` = ?Title,
       `entity_type` = ?EntityType,
       `link_id` = ?LinkId,
       `file_name` = ?FileName,
       `is_public` = ?IsPublic,
       `url` = ?Url,
       `storage` = ?Storage,
       `memo` = ?Memo,
       `data_state` = ?DataState,
       `is_freeze` = ?IsFreeze,
       `department_id` = ?DepartmentId,
       `department_level` = ?DepartmentLevel
 WHERE `id` = ?ID;";
            }
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[] { "ID", "Title", "AnnexType", "EntityType", "LinkId", "FileName", "IsPublic", "Url", "Storage", "Memo", "DataState", "IsFreeze", "AuthorID", "AddDate", "LastReviserID", "LastModifyDate", "AreaId", "DepartmentId", "WeiXinMediaId", "DepartmentLevel" };

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
            { "ID" , "id" },
            { "Title" , "title" },
            { "AnnexType" , "annex_type" },
            { "annex_type" , "annex_type" },
            { "EntityType" , "entity_type" },
            { "entity_type" , "entity_type" },
            { "LinkId" , "link_id" },
            { "link_id" , "link_id" },
            { "FileName" , "file_name" },
            { "file_name" , "file_name" },
            { "IsPublic" , "is_public" },
            { "is_public" , "is_public" },
            { "Url" , "url" },
            { "Storage" , "storage" },
            { "Memo" , "memo" },
            { "DataState" , "data_state" },
            { "data_state" , "data_state" },
            { "IsFreeze" , "is_freeze" },
            { "is_freeze" , "is_freeze" },
            { "AuthorID" , "author_id" },
            { "author_id" , "author_id" },
            { "AddDate" , "add_date" },
            { "add_date" , "add_date" },
            { "LastReviserID" , "last_reviser_id" },
            { "last_reviser_id" , "last_reviser_id" },
            { "LastModifyDate" , "last_modify_date" },
            { "last_modify_date" , "last_modify_date" },
            { "AreaId" , "area_id" },
            { "area_id" , "area_id" },
            { "DepartmentId" , "department_id" },
            { "department_id" , "department_id" },
            { "WeiXinMediaId" , "wx_media_id" },
            { "wx_media_id" , "wx_media_id" },
            { "DepartmentLevel" , "department_level" },
            { "department_level" , "department_level" }
        };

        /// <summary>
        ///  字段字典
        /// </summary>
        public sealed override Dictionary<string, string> FieldMap
        {
            get { return fieldMap; }
        }
        #endregion
        #region 方法实现


        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="entity">读取数据的实体</param>
        protected sealed override void LoadEntity(MySqlDataReader reader, AnnexData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                entity._id = (int)reader.GetInt32(0);
                if (!reader.IsDBNull(1))
                    entity._title = reader.GetString(1).ToString();
                if (!reader.IsDBNull(2))
                    entity._annextype = (AnnexType)reader.GetInt32(2);
                entity._entitytype = (int)reader.GetInt32(3);
                entity._linkid = (int)reader.GetInt32(4);
                if (!reader.IsDBNull(5))
                    entity._filename = reader.GetString(5).ToString();
                entity._ispublic = (bool)reader.GetBoolean(6);
                if (!reader.IsDBNull(7))
                    entity._url = reader.GetString(7).ToString();
                if (!reader.IsDBNull(8))
                    entity._storage = reader.GetString(8).ToString();
                if (!reader.IsDBNull(9))
                    entity._memo = reader.GetString(9).ToString();
                if (!reader.IsDBNull(10))
                    entity._datastate = (DataStateType)reader.GetInt32(10);
                entity._isfreeze = (bool)reader.GetBoolean(11);
                entity._authorid = (int)reader.GetInt32(12);
                if (!reader.IsDBNull(13))
                    try { entity._adddate = reader.GetMySqlDateTime(13).Value; } catch { }
                entity._lastreviserid = (int)reader.GetInt32(14);
                if (!reader.IsDBNull(15))
                    try { entity._lastmodifydate = reader.GetMySqlDateTime(15).Value; } catch { }
                entity._areaid = (int)reader.GetInt32(16);
                entity._departmentid = (int)reader.GetInt32(17);
                if (!reader.IsDBNull(18))
                    entity._weixinmediaid = reader.GetString(18).ToString();
                entity._departmentlevel = (int)reader.GetInt32(19);
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
                case "ID":
                    return MySqlDbType.Int32;
                case "Title":
                    return MySqlDbType.VarString;
                case "AnnexType":
                    return MySqlDbType.Int32;
                case "EntityType":
                    return MySqlDbType.Int32;
                case "LinkId":
                    return MySqlDbType.Int32;
                case "FileName":
                    return MySqlDbType.VarString;
                case "IsPublic":
                    return MySqlDbType.Byte;
                case "Url":
                    return MySqlDbType.VarString;
                case "Storage":
                    return MySqlDbType.VarString;
                case "Memo":
                    return MySqlDbType.Text;
                case "DataState":
                    return MySqlDbType.Int32;
                case "IsFreeze":
                    return MySqlDbType.Byte;
                case "AuthorID":
                    return MySqlDbType.Int32;
                case "AddDate":
                    return MySqlDbType.DateTime;
                case "LastReviserID":
                    return MySqlDbType.Int32;
                case "LastModifyDate":
                    return MySqlDbType.DateTime;
                case "AreaId":
                    return MySqlDbType.Int32;
                case "DepartmentId":
                    return MySqlDbType.Int32;
                case "WeiXinMediaId":
                    return MySqlDbType.VarString;
                case "DepartmentLevel":
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
        private void CreateFullSqlParameter(AnnexData entity, MySqlCommand cmd)
        {
            //02:标识(ID)
            cmd.Parameters.Add(new MySqlParameter("ID", MySqlDbType.Int32) { Value = entity.ID });
            //03:附件标题(Title)
            var isNull = String.IsNullOrWhiteSpace(entity.Title);
            var parameter = new MySqlParameter("Title", MySqlDbType.VarString, isNull ? 10 : (entity.Title).Length);
            if (isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Title;
            cmd.Parameters.Add(parameter);
            //04:附件类型(AnnexType)
            cmd.Parameters.Add(new MySqlParameter("AnnexType", MySqlDbType.Int32) { Value = (int)entity.AnnexType });
            //05:连接类型(EntityType)
            cmd.Parameters.Add(new MySqlParameter("EntityType", MySqlDbType.Int32) { Value = entity.EntityType });
            //06:关联标识(LinkId)
            cmd.Parameters.Add(new MySqlParameter("LinkId", MySqlDbType.Int32) { Value = entity.LinkId });
            //07:文件名称(FileName)
            isNull = String.IsNullOrWhiteSpace(entity.FileName);
            parameter = new MySqlParameter("FileName", MySqlDbType.VarString, isNull ? 10 : (entity.FileName).Length);
            if (isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.FileName;
            cmd.Parameters.Add(parameter);
            //08:是否公开(IsPublic)
            cmd.Parameters.Add(new MySqlParameter("IsPublic", MySqlDbType.Byte) { Value = entity.IsPublic ? (byte)1 : (byte)0 });
            //09:连接地址(Url)
            isNull = String.IsNullOrWhiteSpace(entity.Url);
            parameter = new MySqlParameter("Url", MySqlDbType.VarString, isNull ? 10 : (entity.Url).Length);
            if (isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Url;
            cmd.Parameters.Add(parameter);
            //10:存储地址(Storage)
            isNull = String.IsNullOrWhiteSpace(entity.Storage);
            parameter = new MySqlParameter("Storage", MySqlDbType.VarString, isNull ? 10 : (entity.Storage).Length);
            if (isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Storage;
            cmd.Parameters.Add(parameter);
            //11:备注(Memo)
            isNull = String.IsNullOrWhiteSpace(entity.Memo);
            parameter = new MySqlParameter("Memo", MySqlDbType.Text, isNull ? 10 : (entity.Memo).Length);
            if (isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Memo;
            cmd.Parameters.Add(parameter);
            //164:数据状态(DataState)
            cmd.Parameters.Add(new MySqlParameter("DataState", MySqlDbType.Int32) { Value = (int)entity.DataState });
            //165:数据是否已冻结(IsFreeze)
            cmd.Parameters.Add(new MySqlParameter("IsFreeze", MySqlDbType.Byte) { Value = entity.IsFreeze ? (byte)1 : (byte)0 });
            //167:制作人(AuthorID)
            cmd.Parameters.Add(new MySqlParameter("AuthorID", MySqlDbType.Int32) { Value = entity.AuthorId });
            //169:制作时间(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new MySqlParameter("AddDate", MySqlDbType.DateTime);
            if (isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
            //171:最后修改者(LastReviserID)
            cmd.Parameters.Add(new MySqlParameter("LastReviserID", MySqlDbType.Int32) { Value = entity.LastReviserId });
            //173:最后修改日期(LastModifyDate)
            isNull = entity.LastModifyDate.Year < 1900;
            parameter = new MySqlParameter("LastModifyDate", MySqlDbType.DateTime);
            if (isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastModifyDate;
            cmd.Parameters.Add(parameter);
            //174:区域标识(AreaId)
            cmd.Parameters.Add(new MySqlParameter("AreaId", MySqlDbType.Int32) { Value = entity.AreaId });
            //175:部门所有者(DepartmentId)
            cmd.Parameters.Add(new MySqlParameter("DepartmentId", MySqlDbType.Int32) { Value = entity.DepartmentId });
            //177:微信的MediaId(WeiXinMediaId)
            isNull = String.IsNullOrWhiteSpace(entity.WeiXinMediaId);
            parameter = new MySqlParameter("WeiXinMediaId", MySqlDbType.VarString, isNull ? 10 : (entity.WeiXinMediaId).Length);
            if (isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.WeiXinMediaId;
            cmd.Parameters.Add(parameter);
            //178:部门级别(DepartmentLevel)
            cmd.Parameters.Add(new MySqlParameter("DepartmentLevel", MySqlDbType.Int32) { Value = entity.DepartmentLevel });
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(AnnexData entity, MySqlCommand cmd)
        {
            cmd.CommandText = UpdateSqlCode;
            CreateFullSqlParameter(entity, cmd);
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        protected sealed override bool SetInsertCommand(AnnexData entity, MySqlCommand cmd)
        {
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return true;
        }
        #endregion
    }
}