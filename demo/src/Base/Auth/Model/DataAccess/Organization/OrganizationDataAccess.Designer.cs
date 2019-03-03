/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 23:21:22*/
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
    /// 机构
    /// </summary>
    public partial class OrganizationDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public OrganizationDataAccess()
        {
            Name = OrganizationData._DataStruct_.EntityName;
            Caption = OrganizationData._DataStruct_.EntityCaption;
            Description = OrganizationData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => OrganizationData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"view_org_organization";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_org_organization";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => OrganizationData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `Id`,
    `type` AS `Type`,
    `code` AS `Code`,
    `full_name` AS `FullName`,
    `short_name` AS `ShortName`,
    `tree_name` AS `TreeName`,
    `org_level` AS `OrgLevel`,
    `level_index` AS `LevelIndex`,
    `parent_id` AS `ParentId`,
    `boundary_id` AS `BoundaryId`,
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
    `super_orgcode` AS `SuperOrgcode`,
    `manag_orgcode` AS `ManagOrgcode`,
    `manag_orgname` AS `ManagOrgname`,
    `city_code` AS `CityCode`,
    `district_code` AS `DistrictCode`,
    `org_address` AS `OrgAddress`,
    `law_personname` AS `LawPersonname`,
    `law_persontel` AS `LawPersontel`,
    `contact_name` AS `ContactName`,
    `contact_tel` AS `ContactTel`,
    `area_id` AS `AreaId`,
    `area` AS `Area`";
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
INSERT INTO `tb_org_organization`
(
    `type`,
    `code`,
    `full_name`,
    `short_name`,
    `tree_name`,
    `org_level`,
    `level_index`,
    `parent_id`,
    `boundary_id`,
    `memo`,
    `is_freeze`,
    `data_state`,
    `add_date`,
    `last_reviser_id`,
    `author_id`,
    `auditor_id`,
    `audit_date`,
    `audit_state`,
    `super_orgcode`,
    `manag_orgcode`,
    `manag_orgname`,
    `city_code`,
    `district_code`,
    `org_address`,
    `law_personname`,
    `law_persontel`,
    `contact_name`,
    `contact_tel`,
    `area_id`
)
VALUES
(
    ?Type,
    ?Code,
    ?FullName,
    ?ShortName,
    ?TreeName,
    ?OrgLevel,
    ?LevelIndex,
    ?ParentId,
    ?BoundaryId,
    ?Memo,
    ?IsFreeze,
    ?DataState,
    ?AddDate,
    ?LastReviserId,
    ?AuthorId,
    ?AuditorId,
    ?AuditDate,
    ?AuditState,
    ?SuperOrgcode,
    ?ManagOrgcode,
    ?ManagOrgname,
    ?CityCode,
    ?DistrictCode,
    ?OrgAddress,
    ?LawPersonname,
    ?LawPersontel,
    ?ContactName,
    ?ContactTel,
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
UPDATE `tb_org_organization` SET
       `type` = ?Type,
       `code` = ?Code,
       `full_name` = ?FullName,
       `short_name` = ?ShortName,
       `tree_name` = ?TreeName,
       `org_level` = ?OrgLevel,
       `level_index` = ?LevelIndex,
       `parent_id` = ?ParentId,
       `boundary_id` = ?BoundaryId,
       `memo` = ?Memo,
       `is_freeze` = ?IsFreeze,
       `data_state` = ?DataState,
       `last_reviser_id` = ?LastReviserId,
       `auditor_id` = ?AuditorId,
       `audit_date` = ?AuditDate,
       `audit_state` = ?AuditState,
       `super_orgcode` = ?SuperOrgcode,
       `manag_orgcode` = ?ManagOrgcode,
       `manag_orgname` = ?ManagOrgname,
       `city_code` = ?CityCode,
       `district_code` = ?DistrictCode,
       `org_address` = ?OrgAddress,
       `law_personname` = ?LawPersonname,
       `law_persontel` = ?LawPersontel,
       `contact_name` = ?ContactName,
       `contact_tel` = ?ContactTel,
       `area_id` = ?AreaId
 WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(OrganizationData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_org_organization` SET");
            //机构类型
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_Type] > 0)
                sql.AppendLine("       `type` = ?Type");
            //编码
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_Code] > 0)
                sql.AppendLine("       `code` = ?Code");
            //全称
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_FullName] > 0)
                sql.AppendLine("       `full_name` = ?FullName");
            //简称
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_ShortName] > 0)
                sql.AppendLine("       `short_name` = ?ShortName");
            //树形名称
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_TreeName] > 0)
                sql.AppendLine("       `tree_name` = ?TreeName");
            //级别
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_OrgLevel] > 0)
                sql.AppendLine("       `org_level` = ?OrgLevel");
            //层级的序号
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_LevelIndex] > 0)
                sql.AppendLine("       `level_index` = ?LevelIndex");
            //上级标识
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_ParentId] > 0)
                sql.AppendLine("       `parent_id` = ?ParentId");
            //边界机构标识
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_BoundaryId] > 0)
                sql.AppendLine("       `boundary_id` = ?BoundaryId");
            //备注
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_Memo] > 0)
                sql.AppendLine("       `memo` = ?Memo");
            //冻结更新
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_IsFreeze] > 0)
                sql.AppendLine("       `is_freeze` = ?IsFreeze");
            //数据状态
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_DataState] > 0)
                sql.AppendLine("       `data_state` = ?DataState");
            //最后修改者
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_LastReviserId] > 0)
                sql.AppendLine("       `last_reviser_id` = ?LastReviserId");
            //审核人
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_AuditorId] > 0)
                sql.AppendLine("       `auditor_id` = ?AuditorId");
            //审核时间
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_AuditDate] > 0)
                sql.AppendLine("       `audit_date` = ?AuditDate");
            //审核状态
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_AuditState] > 0)
                sql.AppendLine("       `audit_state` = ?AuditState");
            //上级机构代码
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_SuperOrgcode] > 0)
                sql.AppendLine("       `super_orgcode` = ?SuperOrgcode");
            //注册管理机构代码
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_ManagOrgcode] > 0)
                sql.AppendLine("       `manag_orgcode` = ?ManagOrgcode");
            //注册管理机构名称
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_ManagOrgname] > 0)
                sql.AppendLine("       `manag_orgname` = ?ManagOrgname");
            //所在市级编码
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_CityCode] > 0)
                sql.AppendLine("       `city_code` = ?CityCode");
            //所在区县编码
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_DistrictCode] > 0)
                sql.AppendLine("       `district_code` = ?DistrictCode");
            //机构详细地址
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_OrgAddress] > 0)
                sql.AppendLine("       `org_address` = ?OrgAddress");
            //机构负责人
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_LawPersonname] > 0)
                sql.AppendLine("       `law_personname` = ?LawPersonname");
            //机构负责人电话
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_LawPersontel] > 0)
                sql.AppendLine("       `law_persontel` = ?LawPersontel");
            //机构联系人
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_ContactName] > 0)
                sql.AppendLine("       `contact_name` = ?ContactName");
            //机构联系人电话
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_ContactTel] > 0)
                sql.AppendLine("       `contact_tel` = ?ContactTel");
            //行政区域外键
            if (data.__EntityStatus.ModifiedProperties[OrganizationData._DataStruct_.Real_AreaId] > 0)
                sql.AppendLine("       `area_id` = ?AreaId");
            sql.Append(" WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","Type","Code","FullName","ShortName","TreeName","OrgLevel","LevelIndex","ParentId","BoundaryId","Memo","IsFreeze","DataState","AddDate","LastReviserId","LastModifyDate","AuthorId","AuditorId","AuditDate","AuditState","SuperOrgcode","ManagOrgcode","ManagOrgname","CityCode","DistrictCode","OrgAddress","LawPersonname","LawPersontel","ContactName","ContactTel","AreaId","Area" };

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
            { "Type" , "type" },
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
            { "BoundaryId" , "boundary_id" },
            { "boundary_id" , "boundary_id" },
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
            { "SuperOrgcode" , "super_orgcode" },
            { "super_orgcode" , "super_orgcode" },
            { "ManagOrgcode" , "manag_orgcode" },
            { "manag_orgcode" , "manag_orgcode" },
            { "ManagOrgname" , "manag_orgname" },
            { "manag_orgname" , "manag_orgname" },
            { "CityCode" , "city_code" },
            { "city_code" , "city_code" },
            { "DistrictCode" , "district_code" },
            { "district_code" , "district_code" },
            { "OrgAddress" , "org_address" },
            { "org_address" , "org_address" },
            { "LawPersonname" , "law_personname" },
            { "law_personname" , "law_personname" },
            { "LawPersontel" , "law_persontel" },
            { "law_persontel" , "law_persontel" },
            { "ContactName" , "contact_name" },
            { "contact_name" , "contact_name" },
            { "ContactTel" , "contact_tel" },
            { "contact_tel" , "contact_tel" },
            { "AreaId" , "area_id" },
            { "area_id" , "area_id" },
            { "Area" , "area" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,OrganizationData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._type = (OrganizationType)reader.GetInt32(1);
                if (!reader.IsDBNull(2))
                    entity._code = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._fullName = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._shortName = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._treeName = reader.GetString(5);
                if (!reader.IsDBNull(6))
                    entity._orgLevel = (int)reader.GetInt32(6);
                if (!reader.IsDBNull(7))
                    entity._levelIndex = (int)reader.GetInt32(7);
                if (!reader.IsDBNull(8))
                    entity._parentId = (long)reader.GetInt64(8);
                if (!reader.IsDBNull(9))
                    entity._boundaryId = (long)reader.GetInt64(9);
                if (!reader.IsDBNull(10))
                    entity._memo = reader.GetString(10).ToString();
                if (!reader.IsDBNull(11))
                    entity._isFreeze = (bool)reader.GetBoolean(11);
                if (!reader.IsDBNull(12))
                    entity._dataState = (DataStateType)reader.GetInt32(12);
                if (!reader.IsDBNull(13))
                    try{entity._addDate = reader.GetMySqlDateTime(13).Value;}catch{}
                if (!reader.IsDBNull(14))
                    entity._lastReviserId = (long)reader.GetInt64(14);
                if (!reader.IsDBNull(15))
                    try{entity._lastModifyDate = reader.GetMySqlDateTime(15).Value;}catch{}
                if (!reader.IsDBNull(16))
                    entity._authorId = (long)reader.GetInt64(16);
                if (!reader.IsDBNull(17))
                    entity._auditorId = (long)reader.GetInt64(17);
                if (!reader.IsDBNull(18))
                    try{entity._auditDate = reader.GetMySqlDateTime(18).Value;}catch{}
                if (!reader.IsDBNull(19))
                    entity._auditState = (AuditStateType)reader.GetInt32(19);
                if (!reader.IsDBNull(20))
                    entity._superOrgcode = reader.GetString(20);
                if (!reader.IsDBNull(21))
                    entity._managOrgcode = reader.GetString(21);
                if (!reader.IsDBNull(22))
                    entity._managOrgname = reader.GetString(22);
                if (!reader.IsDBNull(23))
                    entity._cityCode = reader.GetString(23);
                if (!reader.IsDBNull(24))
                    entity._districtCode = reader.GetString(24);
                if (!reader.IsDBNull(25))
                    entity._orgAddress = reader.GetString(25);
                if (!reader.IsDBNull(26))
                    entity._lawPersonname = reader.GetString(26);
                if (!reader.IsDBNull(27))
                    entity._lawPersontel = reader.GetString(27);
                if (!reader.IsDBNull(28))
                    entity._contactName = reader.GetString(28);
                if (!reader.IsDBNull(29))
                    entity._contactTel = reader.GetString(29);
                if (!reader.IsDBNull(30))
                    entity._areaId = (long)reader.GetInt64(30);
                if (!reader.IsDBNull(31))
                    entity._area = reader.GetString(31);
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
                case "Type":
                    return MySqlDbType.Int32;
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
                case "BoundaryId":
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
                case "AuditorId":
                    return MySqlDbType.Int64;
                case "AuditDate":
                    return MySqlDbType.DateTime;
                case "AuditState":
                    return MySqlDbType.Int32;
                case "SuperOrgcode":
                    return MySqlDbType.VarString;
                case "ManagOrgcode":
                    return MySqlDbType.VarString;
                case "ManagOrgname":
                    return MySqlDbType.VarString;
                case "CityCode":
                    return MySqlDbType.VarString;
                case "DistrictCode":
                    return MySqlDbType.VarString;
                case "OrgAddress":
                    return MySqlDbType.VarString;
                case "LawPersonname":
                    return MySqlDbType.VarString;
                case "LawPersontel":
                    return MySqlDbType.VarString;
                case "ContactName":
                    return MySqlDbType.VarString;
                case "ContactTel":
                    return MySqlDbType.VarString;
                case "AreaId":
                    return MySqlDbType.Int64;
                case "Area":
                    return MySqlDbType.VarString;
            }
            return MySqlDbType.VarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        private void CreateFullSqlParameter(OrganizationData entity, MySqlCommand cmd)
        {
            //02:标识(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:机构类型(Type)
            cmd.Parameters.Add(new MySqlParameter("Type",MySqlDbType.Int32){ Value = (int)entity.Type});
            //04:编码(Code)
            var isNull = string.IsNullOrWhiteSpace(entity.Code);
            var parameter = new MySqlParameter("Code",MySqlDbType.VarString , isNull ? 10 : (entity.Code).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Code;
            cmd.Parameters.Add(parameter);
            //05:全称(FullName)
            isNull = string.IsNullOrWhiteSpace(entity.FullName);
            parameter = new MySqlParameter("FullName",MySqlDbType.VarString , isNull ? 10 : (entity.FullName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.FullName;
            cmd.Parameters.Add(parameter);
            //06:简称(ShortName)
            isNull = string.IsNullOrWhiteSpace(entity.ShortName);
            parameter = new MySqlParameter("ShortName",MySqlDbType.VarString , isNull ? 10 : (entity.ShortName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ShortName;
            cmd.Parameters.Add(parameter);
            //07:树形名称(TreeName)
            isNull = string.IsNullOrWhiteSpace(entity.TreeName);
            parameter = new MySqlParameter("TreeName",MySqlDbType.VarString , isNull ? 10 : (entity.TreeName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.TreeName;
            cmd.Parameters.Add(parameter);
            //08:级别(OrgLevel)
            cmd.Parameters.Add(new MySqlParameter("OrgLevel",MySqlDbType.Int32){ Value = entity.OrgLevel});
            //09:层级的序号(LevelIndex)
            cmd.Parameters.Add(new MySqlParameter("LevelIndex",MySqlDbType.Int32){ Value = entity.LevelIndex});
            //10:上级标识(ParentId)
            cmd.Parameters.Add(new MySqlParameter("ParentId",MySqlDbType.Int64){ Value = entity.ParentId});
            //11:边界机构标识(BoundaryId)
            cmd.Parameters.Add(new MySqlParameter("BoundaryId",MySqlDbType.Int64){ Value = entity.BoundaryId});
            //12:备注(Memo)
            isNull = string.IsNullOrWhiteSpace(entity.Memo);
            parameter = new MySqlParameter("Memo",MySqlDbType.Text , isNull ? 10 : (entity.Memo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Memo;
            cmd.Parameters.Add(parameter);
            //13:冻结更新(IsFreeze)
            cmd.Parameters.Add(new MySqlParameter("IsFreeze",MySqlDbType.Byte) { Value = entity.IsFreeze ? (byte)1 : (byte)0 });
            //14:数据状态(DataState)
            cmd.Parameters.Add(new MySqlParameter("DataState",MySqlDbType.Int32){ Value = (int)entity.DataState});
            //15:制作时间(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new MySqlParameter("AddDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
            //16:最后修改者(LastReviserId)
            cmd.Parameters.Add(new MySqlParameter("LastReviserId",MySqlDbType.Int64){ Value = entity.LastReviserId});
            //17:最后修改日期(LastModifyDate)
            isNull = entity.LastModifyDate.Year < 1900;
            parameter = new MySqlParameter("LastModifyDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastModifyDate;
            cmd.Parameters.Add(parameter);
            //18:制作人(AuthorId)
            cmd.Parameters.Add(new MySqlParameter("AuthorId",MySqlDbType.Int64){ Value = entity.AuthorId});
            //19:审核人(AuditorId)
            cmd.Parameters.Add(new MySqlParameter("AuditorId",MySqlDbType.Int64){ Value = entity.AuditorId});
            //20:审核时间(AuditDate)
            isNull = entity.AuditDate.Year < 1900;
            parameter = new MySqlParameter("AuditDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AuditDate;
            cmd.Parameters.Add(parameter);
            //21:审核状态(AuditState)
            cmd.Parameters.Add(new MySqlParameter("AuditState",MySqlDbType.Int32){ Value = (int)entity.AuditState});
            //22:上级机构代码(SuperOrgcode)
            isNull = string.IsNullOrWhiteSpace(entity.SuperOrgcode);
            parameter = new MySqlParameter("SuperOrgcode",MySqlDbType.VarString , isNull ? 10 : (entity.SuperOrgcode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.SuperOrgcode;
            cmd.Parameters.Add(parameter);
            //23:注册管理机构代码(ManagOrgcode)
            isNull = string.IsNullOrWhiteSpace(entity.ManagOrgcode);
            parameter = new MySqlParameter("ManagOrgcode",MySqlDbType.VarString , isNull ? 10 : (entity.ManagOrgcode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ManagOrgcode;
            cmd.Parameters.Add(parameter);
            //24:注册管理机构名称(ManagOrgname)
            isNull = string.IsNullOrWhiteSpace(entity.ManagOrgname);
            parameter = new MySqlParameter("ManagOrgname",MySqlDbType.VarString , isNull ? 10 : (entity.ManagOrgname).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ManagOrgname;
            cmd.Parameters.Add(parameter);
            //25:所在市级编码(CityCode)
            isNull = string.IsNullOrWhiteSpace(entity.CityCode);
            parameter = new MySqlParameter("CityCode",MySqlDbType.VarString , isNull ? 10 : (entity.CityCode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.CityCode;
            cmd.Parameters.Add(parameter);
            //26:所在区县编码(DistrictCode)
            isNull = string.IsNullOrWhiteSpace(entity.DistrictCode);
            parameter = new MySqlParameter("DistrictCode",MySqlDbType.VarString , isNull ? 10 : (entity.DistrictCode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.DistrictCode;
            cmd.Parameters.Add(parameter);
            //27:机构详细地址(OrgAddress)
            isNull = string.IsNullOrWhiteSpace(entity.OrgAddress);
            parameter = new MySqlParameter("OrgAddress",MySqlDbType.VarString , isNull ? 10 : (entity.OrgAddress).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrgAddress;
            cmd.Parameters.Add(parameter);
            //28:机构负责人(LawPersonname)
            isNull = string.IsNullOrWhiteSpace(entity.LawPersonname);
            parameter = new MySqlParameter("LawPersonname",MySqlDbType.VarString , isNull ? 10 : (entity.LawPersonname).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LawPersonname;
            cmd.Parameters.Add(parameter);
            //29:机构负责人电话(LawPersontel)
            isNull = string.IsNullOrWhiteSpace(entity.LawPersontel);
            parameter = new MySqlParameter("LawPersontel",MySqlDbType.VarString , isNull ? 10 : (entity.LawPersontel).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LawPersontel;
            cmd.Parameters.Add(parameter);
            //30:机构联系人(ContactName)
            isNull = string.IsNullOrWhiteSpace(entity.ContactName);
            parameter = new MySqlParameter("ContactName",MySqlDbType.VarString , isNull ? 10 : (entity.ContactName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ContactName;
            cmd.Parameters.Add(parameter);
            //31:机构联系人电话(ContactTel)
            isNull = string.IsNullOrWhiteSpace(entity.ContactTel);
            parameter = new MySqlParameter("ContactTel",MySqlDbType.VarString , isNull ? 10 : (entity.ContactTel).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ContactTel;
            cmd.Parameters.Add(parameter);
            //32:行政区域外键(AreaId)
            cmd.Parameters.Add(new MySqlParameter("AreaId",MySqlDbType.Int64){ Value = entity.AreaId});
            //33:行政区域(Area)
            isNull = string.IsNullOrWhiteSpace(entity.Area);
            parameter = new MySqlParameter("Area",MySqlDbType.VarString , isNull ? 10 : (entity.Area).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Area;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(OrganizationData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(OrganizationData entity, MySqlCommand cmd)
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
    `super_orgcode` AS `SuperOrgcode`,
    `manag_orgcode` AS `ManagOrgcode`,
    `manag_orgname` AS `ManagOrgname`,
    `city_code` AS `CityCode`,
    `district_code` AS `DistrictCode`,
    `org_address` AS `OrgAddress`,
    `law_personname` AS `LawPersonname`,
    `law_persontel` AS `LawPersontel`,
    `contact_name` AS `ContactName`,
    `contact_tel` AS `ContactTel`,
    `area_id` AS `AreaId`,
    `area` AS `Area`";
            }
        }


        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="entity">读取数据的实体</param>
        public override void SimpleLoad(MySqlDataReader reader,OrganizationData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._superOrgcode = reader.GetString(0);
                if (!reader.IsDBNull(1))
                    entity._managOrgcode = reader.GetString(1);
                if (!reader.IsDBNull(2))
                    entity._managOrgname = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._cityCode = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._districtCode = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._orgAddress = reader.GetString(5);
                if (!reader.IsDBNull(6))
                    entity._lawPersonname = reader.GetString(6);
                if (!reader.IsDBNull(7))
                    entity._lawPersontel = reader.GetString(7);
                if (!reader.IsDBNull(8))
                    entity._contactName = reader.GetString(8);
                if (!reader.IsDBNull(9))
                    entity._contactTel = reader.GetString(9);
                if (!reader.IsDBNull(10))
                    entity._areaId = (long)reader.GetInt64(10);
                if (!reader.IsDBNull(11))
                    entity._area = reader.GetString(11);
            }
        }
        #endregion

        
    }
    
    partial class UserCenterDb
    {


        /// <summary>
        /// 机构的结构语句
        /// </summary>
        private TableSql _view_org_organizationSql = new TableSql
        {
            TableName = "view_org_organization",
            PimaryKey = "Id"
        };


        /// <summary>
        /// 机构数据访问对象
        /// </summary>
        private OrganizationDataAccess _organizations;

        /// <summary>
        /// 机构数据访问对象
        /// </summary>
        public OrganizationDataAccess Organizations
        {
            get
            {
                return this._organizations ?? ( this._organizations = new OrganizationDataAccess{ DataBase = this});
            }
        }
    }
}