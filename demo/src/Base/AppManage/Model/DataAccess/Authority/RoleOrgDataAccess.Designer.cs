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
    /// 角色组织关联
    /// </summary>
    public partial class RoleOrgDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public RoleOrgDataAccess()
        {
            Name = RoleOrgData._DataStruct_.EntityName;
            Caption = RoleOrgData._DataStruct_.EntityCaption;
            Description = RoleOrgData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => RoleOrgData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_app_role_org";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_app_role_org";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => RoleOrgData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `role_org_id` AS `RoleOrgId`,
    `site_id` AS `SiteID`,
    `role_id` AS `RoleID`,
    `org_id` AS `OrgID`";
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
    `role_org_id`,
    `site_id`,
    `role_id`,
    `org_id`
)
VALUES
(
    ?RoleOrgId,
    ?SiteID,
    ?RoleID,
    ?OrgID
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
       `role_org_id` = ?RoleOrgId,
       `site_id` = ?SiteID,
       `role_id` = ?RoleID,
       `org_id` = ?OrgID
 WHERE `role_org_id` = ?RoleOrgId;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(RoleOrgData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `{ContextWriteTable}` SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[RoleOrgData._DataStruct_.Real_RoleOrgId] > 0)
                sql.AppendLine("       `role_org_id` = ?RoleOrgId");
            //站点编号
            if (data.__EntityStatus.ModifiedProperties[RoleOrgData._DataStruct_.Real_SiteID] > 0)
                sql.AppendLine("       `site_id` = ?SiteID");
            //角色编号
            if (data.__EntityStatus.ModifiedProperties[RoleOrgData._DataStruct_.Real_RoleID] > 0)
                sql.AppendLine("       `role_id` = ?RoleID");
            //组织编号
            if (data.__EntityStatus.ModifiedProperties[RoleOrgData._DataStruct_.Real_OrgID] > 0)
                sql.AppendLine("       `org_id` = ?OrgID");
            sql.Append(" WHERE `role_org_id` = ?RoleOrgId;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "RoleOrgId","SiteID","RoleID","OrgID" };

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
            { "RoleOrgId" , "role_org_id" },
            { "role_org_id" , "role_org_id" },
            { "SiteID" , "site_id" },
            { "site_id" , "site_id" },
            { "RoleID" , "role_id" },
            { "role_id" , "role_id" },
            { "OrgID" , "org_id" },
            { "org_id" , "org_id" },
            { "Id" , "role_org_id" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,RoleOrgData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._roleOrgId = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._roleID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._orgID = (long)reader.GetInt64(3);
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
                case "RoleOrgId":
                    return MySqlDbType.Int64;
                case "SiteID":
                    return MySqlDbType.Int64;
                case "RoleID":
                    return MySqlDbType.Int64;
                case "OrgID":
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
        public void CreateFullSqlParameter(RoleOrgData entity, MySqlCommand cmd)
        {
            //02:主键(RoleOrgId)
            cmd.Parameters.Add(new MySqlParameter("RoleOrgId",MySqlDbType.Int64){ Value = entity.RoleOrgId});
            //03:站点编号(SiteID)
            cmd.Parameters.Add(new MySqlParameter("SiteID",MySqlDbType.Int64){ Value = entity.SiteID});
            //04:角色编号(RoleID)
            cmd.Parameters.Add(new MySqlParameter("RoleID",MySqlDbType.Int64){ Value = entity.RoleID});
            //05:组织编号(OrgID)
            cmd.Parameters.Add(new MySqlParameter("OrgID",MySqlDbType.Int64){ Value = entity.OrgID});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(RoleOrgData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(RoleOrgData entity, MySqlCommand cmd)
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
        public override void SimpleLoad(MySqlDataReader reader,RoleOrgData entity)
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
        /// 角色组织关联的结构语句
        /// </summary>
        private TableSql _tb_app_role_orgSql = new TableSql
        {
            TableName = "tb_app_role_org",
            PimaryKey = "RoleOrgId"
        };


        /// <summary>
        /// 角色组织关联数据访问对象
        /// </summary>
        private RoleOrgDataAccess _roleOrgs;

        /// <summary>
        /// 角色组织关联数据访问对象
        /// </summary>
        public RoleOrgDataAccess RoleOrgs
        {
            get
            {
                return this._roleOrgs ?? ( this._roleOrgs = new RoleOrgDataAccess{ DataBase = this});
            }
        }
    }
}