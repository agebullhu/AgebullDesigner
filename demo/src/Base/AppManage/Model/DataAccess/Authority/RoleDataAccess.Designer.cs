/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/1 15:34:58*/
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

using Agebull.EntityModel.Common;
using Agebull.EntityModel.MySql;
#endregion

namespace Agebull.Common.AppManage.DataAccess
{
    /// <summary>
    /// 角色
    /// </summary>
    public partial class RoleDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public RoleDataAccess()
        {
            Name = RoleData._DataStruct_.EntityName;
            Caption = RoleData._DataStruct_.EntityCaption;
            Description = RoleData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => RoleData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_auth_role";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_auth_role";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => RoleData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `Id`,
    `role` AS `Role`,
    `caption` AS `Caption`,
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
INSERT INTO `tb_auth_role`
(
    `role`,
    `caption`,
    `memo`,
    `is_freeze`,
    `data_state`,
    `add_date`,
    `last_reviser_id`,
    `author_id`
)
VALUES
(
    ?Role,
    ?Caption,
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
UPDATE `tb_auth_role` SET
       `role` = ?Role,
       `caption` = ?Caption,
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
        public string GetModifiedSqlCode(RoleData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_auth_role` SET");
            //角色
            if (data.__EntityStatus.ModifiedProperties[RoleData._DataStruct_.Real_Role] > 0)
                sql.AppendLine("       `role` = ?Role");
            //标题
            if (data.__EntityStatus.ModifiedProperties[RoleData._DataStruct_.Real_Caption] > 0)
                sql.AppendLine("       `caption` = ?Caption");
            //备注
            if (data.__EntityStatus.ModifiedProperties[RoleData._DataStruct_.Real_Memo] > 0)
                sql.AppendLine("       `memo` = ?Memo");
            //冻结更新
            if (data.__EntityStatus.ModifiedProperties[RoleData._DataStruct_.Real_IsFreeze] > 0)
                sql.AppendLine("       `is_freeze` = ?IsFreeze");
            //数据状态
            if (data.__EntityStatus.ModifiedProperties[RoleData._DataStruct_.Real_DataState] > 0)
                sql.AppendLine("       `data_state` = ?DataState");
            //最后修改者
            if (data.__EntityStatus.ModifiedProperties[RoleData._DataStruct_.Real_LastReviserId] > 0)
                sql.AppendLine("       `last_reviser_id` = ?LastReviserId");
            sql.Append(" WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","Role","Caption","Memo","IsFreeze","DataState","AddDate","LastReviserId","LastModifyDate","AuthorId" };

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
            { "Role" , "role" },
            { "Name" , "role" },
            { "Caption" , "caption" },
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
        protected sealed override void LoadEntity(MySqlDataReader reader,RoleData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._role = reader.GetString(1);
                if (!reader.IsDBNull(2))
                    entity._caption = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._memo = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._isFreeze = (bool)reader.GetBoolean(4);
                if (!reader.IsDBNull(5))
                    entity._dataState = (DataStateType)reader.GetInt32(5);
                if (!reader.IsDBNull(6))
                    try{entity._addDate = reader.GetMySqlDateTime(6).Value;}catch{}
                if (!reader.IsDBNull(7))
                    entity._lastReviserId = (long)reader.GetInt64(7);
                if (!reader.IsDBNull(8))
                    try{entity._lastModifyDate = reader.GetMySqlDateTime(8).Value;}catch{}
                if (!reader.IsDBNull(9))
                    entity._authorId = (long)reader.GetInt64(9);
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
                case "Role":
                    return MySqlDbType.VarString;
                case "Caption":
                    return MySqlDbType.VarString;
                case "Memo":
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
        private void CreateFullSqlParameter(RoleData entity, MySqlCommand cmd)
        {
            //02:标识(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:角色(Role)
            var isNull = string.IsNullOrWhiteSpace(entity.Role);
            var parameter = new MySqlParameter("Role",MySqlDbType.VarString , isNull ? 10 : (entity.Role).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Role;
            cmd.Parameters.Add(parameter);
            //04:标题(Caption)
            isNull = string.IsNullOrWhiteSpace(entity.Caption);
            parameter = new MySqlParameter("Caption",MySqlDbType.VarString , isNull ? 10 : (entity.Caption).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Caption;
            cmd.Parameters.Add(parameter);
            //05:备注(Memo)
            isNull = string.IsNullOrWhiteSpace(entity.Memo);
            parameter = new MySqlParameter("Memo",MySqlDbType.VarString , isNull ? 10 : (entity.Memo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Memo;
            cmd.Parameters.Add(parameter);
            //06:冻结更新(IsFreeze)
            cmd.Parameters.Add(new MySqlParameter("IsFreeze",MySqlDbType.Byte) { Value = entity.IsFreeze ? (byte)1 : (byte)0 });
            //07:数据状态(DataState)
            cmd.Parameters.Add(new MySqlParameter("DataState",MySqlDbType.Int32){ Value = (int)entity.DataState});
            //08:制作时间(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new MySqlParameter("AddDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
            //09:最后修改者(LastReviserId)
            cmd.Parameters.Add(new MySqlParameter("LastReviserId",MySqlDbType.Int64){ Value = entity.LastReviserId});
            //10:最后修改日期(LastModifyDate)
            isNull = entity.LastModifyDate.Year < 1900;
            parameter = new MySqlParameter("LastModifyDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastModifyDate;
            cmd.Parameters.Add(parameter);
            //11:制作人(AuthorId)
            cmd.Parameters.Add(new MySqlParameter("AuthorId",MySqlDbType.Int64){ Value = entity.AuthorId});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(RoleData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(RoleData entity, MySqlCommand cmd)
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
        public override void SimpleLoad(MySqlDataReader reader,RoleData entity)
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
        /// 角色的结构语句
        /// </summary>
        private TableSql _tb_auth_roleSql = new TableSql
        {
            TableName = "tb_auth_role",
            PimaryKey = "Id"
        };


        /// <summary>
        /// 角色数据访问对象
        /// </summary>
        private RoleDataAccess _roles;

        /// <summary>
        /// 角色数据访问对象
        /// </summary>
        public RoleDataAccess Roles
        {
            get
            {
                return this._roles ?? ( this._roles = new RoleDataAccess{ DataBase = this});
            }
        }
    }
}