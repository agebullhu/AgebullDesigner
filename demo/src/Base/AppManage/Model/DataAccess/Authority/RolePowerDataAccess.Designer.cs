/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/22 10:16:57*/
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
    /// 角色权限
    /// </summary>
    public partial class RolePowerDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public RolePowerDataAccess()
        {
            Name = RolePowerData._DataStruct_.EntityName;
            Caption = RolePowerData._DataStruct_.EntityCaption;
            Description = RolePowerData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => RolePowerData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_auth_role_power";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_auth_role_power";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => RolePowerData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `ID`,
    `page_item_id` AS `PageItemId`,
    `role_id` AS `RoleId`,
    `power` AS `Power`,
    `data_scope` AS `DataScope`";
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
INSERT INTO `tb_auth_role_power`
(
    `page_item_id`,
    `role_id`,
    `power`,
    `data_scope`
)
VALUES
(
    ?PageItemId,
    ?RoleId,
    ?Power,
    ?DataScope
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
UPDATE `tb_auth_role_power` SET
       `page_item_id` = ?PageItemId,
       `role_id` = ?RoleId,
       `power` = ?Power,
       `data_scope` = ?DataScope
 WHERE `id` = ?ID;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(RolePowerData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_auth_role_power` SET");
            //页面标识
            if (data.__EntityStatus.ModifiedProperties[RolePowerData._DataStruct_.Real_PageItemId] > 0)
                sql.AppendLine("       `page_item_id` = ?PageItemId");
            //角色标识
            if (data.__EntityStatus.ModifiedProperties[RolePowerData._DataStruct_.Real_RoleId] > 0)
                sql.AppendLine("       `role_id` = ?RoleId");
            //权限
            if (data.__EntityStatus.ModifiedProperties[RolePowerData._DataStruct_.Real_Power] > 0)
                sql.AppendLine("       `power` = ?Power");
            //权限范围
            if (data.__EntityStatus.ModifiedProperties[RolePowerData._DataStruct_.Real_DataScope] > 0)
                sql.AppendLine("       `data_scope` = ?DataScope");
            sql.Append(" WHERE `id` = ?ID;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "ID","PageItemId","RoleId","Power","DataScope" };

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
            { "PageItemId" , "page_item_id" },
            { "page_item_id" , "page_item_id" },
            { "RoleId" , "role_id" },
            { "role_id" , "role_id" },
            { "Power" , "power" },
            { "DataScope" , "data_scope" },
            { "data_scope" , "data_scope" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,RolePowerData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._iD = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._pageItemId = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._roleId = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._power = (RolePowerType)reader.GetInt32(3);
                if (!reader.IsDBNull(4))
                    entity._dataScope = (DataScopeType)reader.GetInt32(4);
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
                    return MySqlDbType.Int64;
                case "PageItemId":
                    return MySqlDbType.Int64;
                case "RoleId":
                    return MySqlDbType.Int64;
                case "Power":
                    return MySqlDbType.Int32;
                case "DataScope":
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
        private void CreateFullSqlParameter(RolePowerData entity, MySqlCommand cmd)
        {
            //02:标识(ID)
            cmd.Parameters.Add(new MySqlParameter("ID",MySqlDbType.Int64){ Value = entity.ID});
            //03:页面标识(PageItemId)
            cmd.Parameters.Add(new MySqlParameter("PageItemId",MySqlDbType.Int64){ Value = entity.PageItemId});
            //04:角色标识(RoleId)
            cmd.Parameters.Add(new MySqlParameter("RoleId",MySqlDbType.Int64){ Value = entity.RoleId});
            //05:权限(Power)
            cmd.Parameters.Add(new MySqlParameter("Power",MySqlDbType.Int32){ Value = (int)entity.Power});
            //06:权限范围(DataScope)
            cmd.Parameters.Add(new MySqlParameter("DataScope",MySqlDbType.Int32){ Value = (int)entity.DataScope});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(RolePowerData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(RolePowerData entity, MySqlCommand cmd)
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
        public override void SimpleLoad(MySqlDataReader reader,RolePowerData entity)
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
        /// 角色权限的结构语句
        /// </summary>
        private TableSql _tb_auth_role_powerSql = new TableSql
        {
            TableName = "tb_auth_role_power",
            PimaryKey = "ID"
        };


        /// <summary>
        /// 角色权限数据访问对象
        /// </summary>
        private RolePowerDataAccess _rolePowers;

        /// <summary>
        /// 角色权限数据访问对象
        /// </summary>
        public RolePowerDataAccess RolePowers
        {
            get
            {
                return this._rolePowers ?? ( this._rolePowers = new RolePowerDataAccess{ DataBase = this});
            }
        }
    }
}