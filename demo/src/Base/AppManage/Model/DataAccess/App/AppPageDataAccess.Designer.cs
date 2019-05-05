/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/15 10:58:48*/
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
    /// 应用页面关联
    /// </summary>
    public partial class AppPageDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public AppPageDataAccess()
        {
            Name = AppPageData._DataStruct_.EntityName;
            Caption = AppPageData._DataStruct_.EntityCaption;
            Description = AppPageData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => AppPageData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_app_app_page";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_app_app_page";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => AppPageData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `app_page_id` AS `AppPageId`,
    `site_id` AS `SiteID`,
    `app_id` AS `AppId`,
    `page_item_id` AS `PageItemId`";
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
INSERT INTO `{ContextWriteTable}`
(
    `app_page_id`,
    `site_id`,
    `app_id`,
    `page_item_id`
)
VALUES
(
    ?AppPageId,
    ?SiteID,
    ?AppId,
    ?PageItemId
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
UPDATE `{ContextWriteTable}` SET
       `app_page_id` = ?AppPageId,
       `site_id` = ?SiteID,
       `app_id` = ?AppId,
       `page_item_id` = ?PageItemId
 WHERE `app_page_id` = ?AppPageId;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(AppPageData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `{ContextWriteTable}` SET");
            //应用页面关联标识
            if (data.__EntityStatus.ModifiedProperties[AppPageData._DataStruct_.Real_AppPageId] > 0)
                sql.AppendLine("       `app_page_id` = ?AppPageId");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[AppPageData._DataStruct_.Real_SiteID] > 0)
                sql.AppendLine("       `site_id` = ?SiteID");
            //应用信息外键
            if (data.__EntityStatus.ModifiedProperties[AppPageData._DataStruct_.Real_AppId] > 0)
                sql.AppendLine("       `app_id` = ?AppId");
            //页面节点外键
            if (data.__EntityStatus.ModifiedProperties[AppPageData._DataStruct_.Real_PageItemId] > 0)
                sql.AppendLine("       `page_item_id` = ?PageItemId");
            sql.Append(" WHERE `app_page_id` = ?AppPageId;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "AppPageId","SiteID","AppId","PageItemId" };

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
            { "AppPageId" , "app_page_id" },
            { "app_page_id" , "app_page_id" },
            { "SiteID" , "site_id" },
            { "site_id" , "site_id" },
            { "AppId" , "app_id" },
            { "app_id" , "app_id" },
            { "PageItemId" , "page_item_id" },
            { "page_item_id" , "page_item_id" },
            { "Id" , "app_page_id" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,AppPageData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._appPageId = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._appId = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._pageItemId = (long)reader.GetInt64(3);
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
                case "AppPageId":
                    return MySqlDbType.Int64;
                case "SiteID":
                    return MySqlDbType.Int64;
                case "AppId":
                    return MySqlDbType.Int64;
                case "PageItemId":
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
        public void CreateFullSqlParameter(AppPageData entity, MySqlCommand cmd)
        {
            //02:应用页面关联标识(AppPageId)
            cmd.Parameters.Add(new MySqlParameter("AppPageId",MySqlDbType.Int64){ Value = entity.AppPageId});
            //03:站点标识(SiteID)
            cmd.Parameters.Add(new MySqlParameter("SiteID",MySqlDbType.Int64){ Value = entity.SiteID});
            //04:应用信息外键(AppId)
            cmd.Parameters.Add(new MySqlParameter("AppId",MySqlDbType.Int64){ Value = entity.AppId});
            //05:页面节点外键(PageItemId)
            cmd.Parameters.Add(new MySqlParameter("PageItemId",MySqlDbType.Int64){ Value = entity.PageItemId});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(AppPageData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(AppPageData entity, MySqlCommand cmd)
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
        public override void SimpleLoad(MySqlDataReader reader,AppPageData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
            }
        }
        #endregion

        
    }
    
    partial class AppManageDb
    {


        /// <summary>
        /// 应用页面关联的结构语句
        /// </summary>
        private TableSql _tb_app_app_pageSql = new TableSql
        {
            TableName = "tb_app_app_page",
            PimaryKey = "AppPageId"
        };


        /// <summary>
        /// 应用页面关联数据访问对象
        /// </summary>
        private AppPageDataAccess _appPages;

        /// <summary>
        /// 应用页面关联数据访问对象
        /// </summary>
        public AppPageDataAccess AppPages
        {
            get
            {
                return this._appPages ?? ( this._appPages = new AppPageDataAccess{ DataBase = this});
            }
        }
    }
}