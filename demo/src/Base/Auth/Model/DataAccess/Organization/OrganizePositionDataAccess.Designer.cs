/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/3 1:24:04*/
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
    /// 机构职位设置
    /// </summary>
    public partial class OrganizePositionDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public OrganizePositionDataAccess()
        {
            Name = OrganizePositionData._DataStruct_.EntityName;
            Caption = OrganizePositionData._DataStruct_.EntityCaption;
            Description = OrganizePositionData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => OrganizePositionData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"view_org_organize_position";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_org_organize_position";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => OrganizePositionData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `Id`,
    `appellation` AS `Position`,
    `department_id` AS `DepartmentId`,
    `organization` AS `Department`,
    `org_level` AS `OrgLevel`,
    `boundary_id` AS `BoundaryId`,
    `role_id` AS `RoleId`,
    `role` AS `Role`,
    `memo` AS `Memo`,
    `is_freeze` AS `IsFreeze`,
    `data_state` AS `DataState`,
    `add_date` AS `AddDate`,
    `last_reviser_id` AS `LastReviserId`,
    `last_modify_date` AS `LastModifyDate`,
    `author_id` AS `AuthorId`,
    `auditor_id` AS `AuditorId`,
    `audit_date` AS `AuditDate`,
    `audit_state` AS `AuditState`,
    `area_id` AS `AreaId`";
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
INSERT INTO `tb_org_organize_position`
(
    `appellation`,
    `department_id`,
    `boundary_id`,
    `role_id`,
    `memo`,
    `is_freeze`,
    `data_state`,
    `add_date`,
    `last_reviser_id`,
    `author_id`,
    `auditor_id`,
    `audit_date`,
    `audit_state`,
    `area_id`
)
VALUES
(
    ?Position,
    ?DepartmentId,
    ?BoundaryId,
    ?RoleId,
    ?Memo,
    ?IsFreeze,
    ?DataState,
    ?AddDate,
    ?LastReviserId,
    ?AuthorId,
    ?AuditorId,
    ?AuditDate,
    ?AuditState,
    ?AreaId
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
UPDATE `tb_org_organize_position` SET
       `appellation` = ?Position,
       `department_id` = ?DepartmentId,
       `boundary_id` = ?BoundaryId,
       `role_id` = ?RoleId,
       `memo` = ?Memo,
       `is_freeze` = ?IsFreeze,
       `data_state` = ?DataState,
       `last_reviser_id` = ?LastReviserId,
       `auditor_id` = ?AuditorId,
       `audit_date` = ?AuditDate,
       `audit_state` = ?AuditState,
       `area_id` = ?AreaId
 WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(OrganizePositionData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_org_organize_position` SET");
            //职位
            if (data.__EntityStatus.ModifiedProperties[OrganizePositionData._DataStruct_.Real_Position] > 0)
                sql.AppendLine("       `appellation` = ?Position");
            //部门标识
            if (data.__EntityStatus.ModifiedProperties[OrganizePositionData._DataStruct_.Real_DepartmentId] > 0)
                sql.AppendLine("       `department_id` = ?DepartmentId");
            //边界机构标识
            if (data.__EntityStatus.ModifiedProperties[OrganizePositionData._DataStruct_.Real_BoundaryId] > 0)
                sql.AppendLine("       `boundary_id` = ?BoundaryId");
            //角色外键
            if (data.__EntityStatus.ModifiedProperties[OrganizePositionData._DataStruct_.Real_RoleId] > 0)
                sql.AppendLine("       `role_id` = ?RoleId");
            //备注
            if (data.__EntityStatus.ModifiedProperties[OrganizePositionData._DataStruct_.Real_Memo] > 0)
                sql.AppendLine("       `memo` = ?Memo");
            //冻结更新
            if (data.__EntityStatus.ModifiedProperties[OrganizePositionData._DataStruct_.Real_IsFreeze] > 0)
                sql.AppendLine("       `is_freeze` = ?IsFreeze");
            //数据状态
            if (data.__EntityStatus.ModifiedProperties[OrganizePositionData._DataStruct_.Real_DataState] > 0)
                sql.AppendLine("       `data_state` = ?DataState");
            //最后修改者
            if (data.__EntityStatus.ModifiedProperties[OrganizePositionData._DataStruct_.Real_LastReviserId] > 0)
                sql.AppendLine("       `last_reviser_id` = ?LastReviserId");
            //审核人
            if (data.__EntityStatus.ModifiedProperties[OrganizePositionData._DataStruct_.Real_AuditorId] > 0)
                sql.AppendLine("       `auditor_id` = ?AuditorId");
            //审核时间
            if (data.__EntityStatus.ModifiedProperties[OrganizePositionData._DataStruct_.Real_AuditDate] > 0)
                sql.AppendLine("       `audit_date` = ?AuditDate");
            //审核状态
            if (data.__EntityStatus.ModifiedProperties[OrganizePositionData._DataStruct_.Real_AuditState] > 0)
                sql.AppendLine("       `audit_state` = ?AuditState");
            //行政区域外键
            if (data.__EntityStatus.ModifiedProperties[OrganizePositionData._DataStruct_.Real_AreaId] > 0)
                sql.AppendLine("       `area_id` = ?AreaId");
            sql.Append(" WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","Position","DepartmentId","Department","OrgLevel","BoundaryId","RoleId","Role","Memo","IsFreeze","DataState","AddDate","LastReviserId","LastModifyDate","AuthorId","AuditorId","AuditDate","AuditState","MasterId","AreaId" };

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
            { "Position" , "appellation" },
            { "appellation" , "appellation" },
            { "DepartmentId" , "department_id" },
            { "department_id" , "department_id" },
            { "Department" , "organization" },
            { "organization" , "organization" },
            { "OrgLevel" , "org_level" },
            { "org_level" , "org_level" },
            { "BoundaryId" , "boundary_id" },
            { "boundary_id" , "boundary_id" },
            { "RoleId" , "role_id" },
            { "role_id" , "role_id" },
            { "Role" , "role" },
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
            { "author_id" , "author_id" },
            { "AuditorId" , "auditor_id" },
            { "auditor_id" , "auditor_id" },
            { "AuditDate" , "audit_date" },
            { "audit_date" , "audit_date" },
            { "AuditState" , "audit_state" },
            { "audit_state" , "audit_state" },
            { "MasterId" , "master_id" },
            { "master_id" , "master_id" },
            { "AreaId" , "area_id" },
            { "area_id" , "area_id" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,OrganizePositionData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._position = reader.GetString(1);
                if (!reader.IsDBNull(2))
                    entity._departmentId = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._department = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._orgLevel = (int)reader.GetInt32(4);
                if (!reader.IsDBNull(5))
                    entity._boundaryId = (long)reader.GetInt64(5);
                if (!reader.IsDBNull(6))
                    entity._roleId = (long)reader.GetInt64(6);
                if (!reader.IsDBNull(7))
                    entity._role = reader.GetString(7);
                if (!reader.IsDBNull(8))
                    entity._memo = reader.GetString(8);
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
                if (!reader.IsDBNull(15))
                    entity._auditorId = (long)reader.GetInt64(15);
                if (!reader.IsDBNull(16))
                    try{entity._auditDate = reader.GetMySqlDateTime(16).Value;}catch{}
                if (!reader.IsDBNull(17))
                    entity._auditState = (AuditStateType)reader.GetInt32(17);
                if (!reader.IsDBNull(18))
                    entity._areaId = (long)reader.GetInt64(18);
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
                case "Position":
                    return MySqlDbType.VarString;
                case "DepartmentId":
                    return MySqlDbType.Int64;
                case "Department":
                    return MySqlDbType.VarString;
                case "OrgLevel":
                    return MySqlDbType.Int32;
                case "BoundaryId":
                    return MySqlDbType.Int64;
                case "RoleId":
                    return MySqlDbType.Int64;
                case "Role":
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
                case "AuditorId":
                    return MySqlDbType.Int64;
                case "AuditDate":
                    return MySqlDbType.DateTime;
                case "AuditState":
                    return MySqlDbType.Int32;
                case "AreaId":
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
        private void CreateFullSqlParameter(OrganizePositionData entity, MySqlCommand cmd)
        {
            //02:标识(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:职位(Position)
            var isNull = string.IsNullOrWhiteSpace(entity.Position);
            var parameter = new MySqlParameter("Position",MySqlDbType.VarString , isNull ? 10 : (entity.Position).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Position;
            cmd.Parameters.Add(parameter);
            //04:部门标识(DepartmentId)
            cmd.Parameters.Add(new MySqlParameter("DepartmentId",MySqlDbType.Int64){ Value = entity.DepartmentId});
            //05:部门名称(Department)
            isNull = string.IsNullOrWhiteSpace(entity.Department);
            parameter = new MySqlParameter("Department",MySqlDbType.VarString , isNull ? 10 : (entity.Department).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Department;
            cmd.Parameters.Add(parameter);
            //06:部门级别(OrgLevel)
            cmd.Parameters.Add(new MySqlParameter("OrgLevel",MySqlDbType.Int32){ Value = entity.OrgLevel});
            //07:边界机构标识(BoundaryId)
            cmd.Parameters.Add(new MySqlParameter("BoundaryId",MySqlDbType.Int64){ Value = entity.BoundaryId});
            //08:角色外键(RoleId)
            cmd.Parameters.Add(new MySqlParameter("RoleId",MySqlDbType.Int64){ Value = entity.RoleId});
            //09:角色(Role)
            isNull = string.IsNullOrWhiteSpace(entity.Role);
            parameter = new MySqlParameter("Role",MySqlDbType.VarString , isNull ? 10 : (entity.Role).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Role;
            cmd.Parameters.Add(parameter);
            //10:备注(Memo)
            isNull = string.IsNullOrWhiteSpace(entity.Memo);
            parameter = new MySqlParameter("Memo",MySqlDbType.VarString , isNull ? 10 : (entity.Memo).Length);
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
            //17:审核人(AuditorId)
            cmd.Parameters.Add(new MySqlParameter("AuditorId",MySqlDbType.Int64){ Value = entity.AuditorId});
            //18:审核时间(AuditDate)
            isNull = entity.AuditDate.Year < 1900;
            parameter = new MySqlParameter("AuditDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AuditDate;
            cmd.Parameters.Add(parameter);
            //19:审核状态(AuditState)
            cmd.Parameters.Add(new MySqlParameter("AuditState",MySqlDbType.Int32){ Value = (int)entity.AuditState});
            //21:行政区域外键(AreaId)
            cmd.Parameters.Add(new MySqlParameter("AreaId",MySqlDbType.Int64){ Value = entity.AreaId});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(OrganizePositionData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(OrganizePositionData entity, MySqlCommand cmd)
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
    `area_id` AS `AreaId`";
            }
        }


        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="entity">读取数据的实体</param>
        public override void SimpleLoad(MySqlDataReader reader,OrganizePositionData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._areaId = (long)reader.GetInt64(0);
            }
        }
        #endregion

        
    }
    
    partial class UserCenterDb
    {


        /// <summary>
        /// 机构职位设置的结构语句
        /// </summary>
        private TableSql _view_org_organize_positionSql = new TableSql
        {
            TableName = "view_org_organize_position",
            PimaryKey = "Id"
        };


        /// <summary>
        /// 机构职位设置数据访问对象
        /// </summary>
        private OrganizePositionDataAccess _organizePositions;

        /// <summary>
        /// 机构职位设置数据访问对象
        /// </summary>
        public OrganizePositionDataAccess OrganizePositions
        {
            get
            {
                return this._organizePositions ?? ( this._organizePositions = new OrganizePositionDataAccess{ DataBase = this});
            }
        }
    }
}