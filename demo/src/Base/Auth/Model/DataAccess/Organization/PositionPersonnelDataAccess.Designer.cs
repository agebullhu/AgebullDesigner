/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/22 10:13:29*/
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
    /// 人员职位设置
    /// </summary>
    public partial class PositionPersonnelDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public PositionPersonnelDataAccess()
        {
            Name = PositionPersonnelData._DataStruct_.EntityName;
            Caption = PositionPersonnelData._DataStruct_.EntityCaption;
            Description = PositionPersonnelData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => PositionPersonnelData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"view_org_position_personnel";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_org_position_personnel";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => PositionPersonnelData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `Id`,
    `appellation` AS `Appellation`,
    `real_name` AS `RealName`,
    `user_id` AS `UserId`,
    `sex` AS `Sex`,
    `birthday` AS `Birthday`,
    `tel` AS `Tel`,
    `phone_number` AS `PhoneNumber`,
    `role` AS `Role`,
    `role_id` AS `RoleId`,
    `organize_position_id` AS `OrganizePositionId`,
    `position` AS `Position`,
    `department_id` AS `DepartmentId`,
    `department` AS `Department`,
    `organization_id` AS `OrganizationId`,
    `organization` AS `Organization`,
    `org_level` AS `OrgLevel`,
    `memo` AS `Memo`,
    `is_freeze` AS `IsFreeze`,
    `data_state` AS `DataState`,
    `add_date` AS `AddDate`,
    `last_reviser_id` AS `LastReviserId`,
    `last_modify_date` AS `LastModifyDate`,
    `author_id` AS `AuthorId`,
    `auditor_id` AS `AuditorId`,
    `audit_date` AS `AuditDate`,
    `audit_state` AS `AuditState`";
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
INSERT INTO `tb_org_position_personnel`
(
    `appellation`,
    `user_id`,
    `role_id`,
    `organize_position_id`,
    `memo`,
    `is_freeze`,
    `data_state`,
    `add_date`,
    `last_reviser_id`,
    `author_id`,
    `auditor_id`,
    `audit_date`,
    `audit_state`
)
VALUES
(
    ?Appellation,
    ?UserId,
    ?RoleId,
    ?OrganizePositionId,
    ?Memo,
    ?IsFreeze,
    ?DataState,
    ?AddDate,
    ?LastReviserId,
    ?AuthorId,
    ?AuditorId,
    ?AuditDate,
    ?AuditState
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
UPDATE `tb_org_position_personnel` SET
       `appellation` = ?Appellation,
       `user_id` = ?UserId,
       `role_id` = ?RoleId,
       `organize_position_id` = ?OrganizePositionId,
       `memo` = ?Memo,
       `is_freeze` = ?IsFreeze,
       `data_state` = ?DataState,
       `last_reviser_id` = ?LastReviserId,
       `auditor_id` = ?AuditorId,
       `audit_date` = ?AuditDate,
       `audit_state` = ?AuditState
 WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(PositionPersonnelData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_org_position_personnel` SET");
            //称谓
            if (data.__EntityStatus.ModifiedProperties[PositionPersonnelData._DataStruct_.Real_Appellation] > 0)
                sql.AppendLine("       `appellation` = ?Appellation");
            //员工标识
            if (data.__EntityStatus.ModifiedProperties[PositionPersonnelData._DataStruct_.Real_UserId] > 0)
                sql.AppendLine("       `user_id` = ?UserId");
            //角色外键
            if (data.__EntityStatus.ModifiedProperties[PositionPersonnelData._DataStruct_.Real_RoleId] > 0)
                sql.AppendLine("       `role_id` = ?RoleId");
            //职位标识
            if (data.__EntityStatus.ModifiedProperties[PositionPersonnelData._DataStruct_.Real_OrganizePositionId] > 0)
                sql.AppendLine("       `organize_position_id` = ?OrganizePositionId");
            //备注
            if (data.__EntityStatus.ModifiedProperties[PositionPersonnelData._DataStruct_.Real_Memo] > 0)
                sql.AppendLine("       `memo` = ?Memo");
            //冻结更新
            if (data.__EntityStatus.ModifiedProperties[PositionPersonnelData._DataStruct_.Real_IsFreeze] > 0)
                sql.AppendLine("       `is_freeze` = ?IsFreeze");
            //数据状态
            if (data.__EntityStatus.ModifiedProperties[PositionPersonnelData._DataStruct_.Real_DataState] > 0)
                sql.AppendLine("       `data_state` = ?DataState");
            //最后修改者
            if (data.__EntityStatus.ModifiedProperties[PositionPersonnelData._DataStruct_.Real_LastReviserId] > 0)
                sql.AppendLine("       `last_reviser_id` = ?LastReviserId");
            //审核人
            if (data.__EntityStatus.ModifiedProperties[PositionPersonnelData._DataStruct_.Real_AuditorId] > 0)
                sql.AppendLine("       `auditor_id` = ?AuditorId");
            //审核时间
            if (data.__EntityStatus.ModifiedProperties[PositionPersonnelData._DataStruct_.Real_AuditDate] > 0)
                sql.AppendLine("       `audit_date` = ?AuditDate");
            //审核状态
            if (data.__EntityStatus.ModifiedProperties[PositionPersonnelData._DataStruct_.Real_AuditState] > 0)
                sql.AppendLine("       `audit_state` = ?AuditState");
            sql.Append(" WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","Appellation","RealName","UserId","Sex","Birthday","Tel","PhoneNumber","Role","RoleId","OrganizePositionId","Position","DepartmentId","Department","OrganizationId","Organization","OrgLevel","Memo","IsFreeze","DataState","AddDate","LastReviserId","LastModifyDate","AuthorId","AuditorId","AuditDate","AuditState","MasterId" };

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
            { "Appellation" , "appellation" },
            { "RealName" , "real_name" },
            { "real_name" , "real_name" },
            { "UserId" , "user_id" },
            { "user_id" , "user_id" },
            { "Sex" , "sex" },
            { "Birthday" , "birthday" },
            { "Tel" , "tel" },
            { "PhoneNumber" , "phone_number" },
            { "phone_number" , "phone_number" },
            { "Role" , "role" },
            { "RoleId" , "role_id" },
            { "role_id" , "role_id" },
            { "OrganizePositionId" , "organize_position_id" },
            { "organize_position_id" , "organize_position_id" },
            { "Position" , "position" },
            { "DepartmentId" , "department_id" },
            { "department_id" , "department_id" },
            { "Department" , "department" },
            { "OrganizationId" , "organization_id" },
            { "organization_id" , "organization_id" },
            { "Organization" , "organization" },
            { "OrgLevel" , "org_level" },
            { "org_level" , "org_level" },
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
            { "master_id" , "master_id" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,PositionPersonnelData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._appellation = reader.GetString(1);
                if (!reader.IsDBNull(2))
                    entity._realName = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._userId = (long)reader.GetInt64(3);
                if (!reader.IsDBNull(4))
                    entity._sex = (SexType)reader.GetInt32(4);
                if (!reader.IsDBNull(5))
                    try{entity._birthday = reader.GetMySqlDateTime(5).Value;}catch{}
                if (!reader.IsDBNull(6))
                    entity._tel = reader.GetString(6);
                if (!reader.IsDBNull(7))
                    entity._phoneNumber = reader.GetString(7);
                if (!reader.IsDBNull(8))
                    entity._role = reader.GetString(8);
                if (!reader.IsDBNull(9))
                    entity._roleId = (long)reader.GetInt64(9);
                if (!reader.IsDBNull(10))
                    entity._organizePositionId = (long)reader.GetInt64(10);
                if (!reader.IsDBNull(11))
                    entity._position = reader.GetString(11);
                if (!reader.IsDBNull(12))
                    entity._departmentId = (long)reader.GetInt64(12);
                if (!reader.IsDBNull(13))
                    entity._department = reader.GetString(13);
                if (!reader.IsDBNull(14))
                    entity._organizationId = (long)reader.GetInt64(14);
                if (!reader.IsDBNull(15))
                    entity._organization = reader.GetString(15);
                if (!reader.IsDBNull(16))
                    entity._orgLevel = (int)reader.GetInt32(16);
                if (!reader.IsDBNull(17))
                    entity._memo = reader.GetString(17);
                if (!reader.IsDBNull(18))
                    entity._isFreeze = (bool)reader.GetBoolean(18);
                if (!reader.IsDBNull(19))
                    entity._dataState = (DataStateType)reader.GetInt32(19);
                if (!reader.IsDBNull(20))
                    try{entity._addDate = reader.GetMySqlDateTime(20).Value;}catch{}
                if (!reader.IsDBNull(21))
                    entity._lastReviserId = (long)reader.GetInt64(21);
                if (!reader.IsDBNull(22))
                    try{entity._lastModifyDate = reader.GetMySqlDateTime(22).Value;}catch{}
                if (!reader.IsDBNull(23))
                    entity._authorId = (long)reader.GetInt64(23);
                if (!reader.IsDBNull(24))
                    entity._auditorId = (long)reader.GetInt64(24);
                if (!reader.IsDBNull(25))
                    try{entity._auditDate = reader.GetMySqlDateTime(25).Value;}catch{}
                if (!reader.IsDBNull(26))
                    entity._auditState = (AuditStateType)reader.GetInt32(26);
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
                case "Appellation":
                    return MySqlDbType.VarString;
                case "RealName":
                    return MySqlDbType.VarString;
                case "UserId":
                    return MySqlDbType.Int64;
                case "Sex":
                    return MySqlDbType.Byte;
                case "Birthday":
                    return MySqlDbType.Int64;
                case "Tel":
                    return MySqlDbType.VarString;
                case "PhoneNumber":
                    return MySqlDbType.VarString;
                case "Role":
                    return MySqlDbType.VarString;
                case "RoleId":
                    return MySqlDbType.Int64;
                case "OrganizePositionId":
                    return MySqlDbType.Int64;
                case "Position":
                    return MySqlDbType.VarString;
                case "DepartmentId":
                    return MySqlDbType.Int64;
                case "Department":
                    return MySqlDbType.VarString;
                case "OrganizationId":
                    return MySqlDbType.Int64;
                case "Organization":
                    return MySqlDbType.VarString;
                case "OrgLevel":
                    return MySqlDbType.Int32;
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
            }
            return MySqlDbType.VarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        private void CreateFullSqlParameter(PositionPersonnelData entity, MySqlCommand cmd)
        {
            //02:标识(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:称谓(Appellation)
            var isNull = string.IsNullOrWhiteSpace(entity.Appellation);
            var parameter = new MySqlParameter("Appellation",MySqlDbType.VarString , isNull ? 10 : (entity.Appellation).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Appellation;
            cmd.Parameters.Add(parameter);
            //04:姓名(RealName)
            isNull = string.IsNullOrWhiteSpace(entity.RealName);
            parameter = new MySqlParameter("RealName",MySqlDbType.VarString , isNull ? 10 : (entity.RealName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.RealName;
            cmd.Parameters.Add(parameter);
            //05:员工标识(UserId)
            cmd.Parameters.Add(new MySqlParameter("UserId",MySqlDbType.Int64){ Value = entity.UserId});
            //06:性别(Sex)
            cmd.Parameters.Add(new MySqlParameter("Sex",MySqlDbType.Byte){ Value = (int)entity.Sex});
            //07:生日(Birthday)
            isNull = entity.Birthday.Year < 1900;
            parameter = new MySqlParameter("Birthday",MySqlDbType.Int64);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Birthday;
            cmd.Parameters.Add(parameter);
            //08:电话(Tel)
            isNull = string.IsNullOrWhiteSpace(entity.Tel);
            parameter = new MySqlParameter("Tel",MySqlDbType.VarString , isNull ? 10 : (entity.Tel).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Tel;
            cmd.Parameters.Add(parameter);
            //09:手机(PhoneNumber)
            isNull = string.IsNullOrWhiteSpace(entity.PhoneNumber);
            parameter = new MySqlParameter("PhoneNumber",MySqlDbType.VarString , isNull ? 10 : (entity.PhoneNumber).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.PhoneNumber;
            cmd.Parameters.Add(parameter);
            //10:角色(Role)
            isNull = string.IsNullOrWhiteSpace(entity.Role);
            parameter = new MySqlParameter("Role",MySqlDbType.VarString , isNull ? 10 : (entity.Role).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Role;
            cmd.Parameters.Add(parameter);
            //11:角色外键(RoleId)
            cmd.Parameters.Add(new MySqlParameter("RoleId",MySqlDbType.Int64){ Value = entity.RoleId});
            //12:职位标识(OrganizePositionId)
            cmd.Parameters.Add(new MySqlParameter("OrganizePositionId",MySqlDbType.Int64){ Value = entity.OrganizePositionId});
            //13:职位(Position)
            isNull = string.IsNullOrWhiteSpace(entity.Position);
            parameter = new MySqlParameter("Position",MySqlDbType.VarString , isNull ? 10 : (entity.Position).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Position;
            cmd.Parameters.Add(parameter);
            //14:部门外键(DepartmentId)
            cmd.Parameters.Add(new MySqlParameter("DepartmentId",MySqlDbType.Int64){ Value = entity.DepartmentId});
            //15:部门(Department)
            isNull = string.IsNullOrWhiteSpace(entity.Department);
            parameter = new MySqlParameter("Department",MySqlDbType.VarString , isNull ? 10 : (entity.Department).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Department;
            cmd.Parameters.Add(parameter);
            //16:机构标识(OrganizationId)
            cmd.Parameters.Add(new MySqlParameter("OrganizationId",MySqlDbType.Int64){ Value = entity.OrganizationId});
            //17:所在机构(Organization)
            isNull = string.IsNullOrWhiteSpace(entity.Organization);
            parameter = new MySqlParameter("Organization",MySqlDbType.VarString , isNull ? 10 : (entity.Organization).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Organization;
            cmd.Parameters.Add(parameter);
            //18:级别(OrgLevel)
            cmd.Parameters.Add(new MySqlParameter("OrgLevel",MySqlDbType.Int32){ Value = entity.OrgLevel});
            //19:备注(Memo)
            isNull = string.IsNullOrWhiteSpace(entity.Memo);
            parameter = new MySqlParameter("Memo",MySqlDbType.VarString , isNull ? 10 : (entity.Memo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Memo;
            cmd.Parameters.Add(parameter);
            //20:冻结更新(IsFreeze)
            cmd.Parameters.Add(new MySqlParameter("IsFreeze",MySqlDbType.Byte) { Value = entity.IsFreeze ? (byte)1 : (byte)0 });
            //21:数据状态(DataState)
            cmd.Parameters.Add(new MySqlParameter("DataState",MySqlDbType.Int32){ Value = (int)entity.DataState});
            //22:制作时间(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new MySqlParameter("AddDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
            //23:最后修改者(LastReviserId)
            cmd.Parameters.Add(new MySqlParameter("LastReviserId",MySqlDbType.Int64){ Value = entity.LastReviserId});
            //24:最后修改日期(LastModifyDate)
            isNull = entity.LastModifyDate.Year < 1900;
            parameter = new MySqlParameter("LastModifyDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastModifyDate;
            cmd.Parameters.Add(parameter);
            //25:制作人(AuthorId)
            cmd.Parameters.Add(new MySqlParameter("AuthorId",MySqlDbType.Int64){ Value = entity.AuthorId});
            //26:审核人(AuditorId)
            cmd.Parameters.Add(new MySqlParameter("AuditorId",MySqlDbType.Int64){ Value = entity.AuditorId});
            //27:审核时间(AuditDate)
            isNull = entity.AuditDate.Year < 1900;
            parameter = new MySqlParameter("AuditDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AuditDate;
            cmd.Parameters.Add(parameter);
            //28:审核状态(AuditState)
            cmd.Parameters.Add(new MySqlParameter("AuditState",MySqlDbType.Int32){ Value = (int)entity.AuditState});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(PositionPersonnelData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(PositionPersonnelData entity, MySqlCommand cmd)
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
        public override void SimpleLoad(MySqlDataReader reader,PositionPersonnelData entity)
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
        /// 人员职位设置的结构语句
        /// </summary>
        private TableSql _view_org_position_personnelSql = new TableSql
        {
            TableName = "view_org_position_personnel",
            PimaryKey = "Id"
        };


        /// <summary>
        /// 人员职位设置数据访问对象
        /// </summary>
        private PositionPersonnelDataAccess _positionPersonnels;

        /// <summary>
        /// 人员职位设置数据访问对象
        /// </summary>
        public PositionPersonnelDataAccess PositionPersonnels
        {
            get
            {
                return this._positionPersonnels ?? ( this._positionPersonnels = new PositionPersonnelDataAccess{ DataBase = this});
            }
        }
    }
}