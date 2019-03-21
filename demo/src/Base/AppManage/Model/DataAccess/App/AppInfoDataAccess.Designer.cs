/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 20:38:51*/
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
using Agebull.EntityModel.MySql;
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
                return @"view_app_app_info";
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
    `org_id` AS `OrgId`,
    `organization` AS `Organization`,
    `short_name` AS `ShortName`,
    `full_name` AS `FullName`,
    `Classify` AS `Classify`,
    `app_key` AS `AppId`,
    `manag_orgcode` AS `ManagOrgcode`,
    `manag_orgname` AS `ManagOrgname`,
    `city_code` AS `CityCode`,
    `district_code` AS `DistrictCode`,
    `org_address` AS `OrgAddress`,
    `law_personname` AS `LawPersonname`,
    `law_persontel` AS `LawPersontel`,
    `contact_name` AS `ContactName`,
    `contact_tel` AS `ContactTel`,
    `super_orgcode` AS `SuperOrgcode`,
    `update_date` AS `UpdateDate`,
    `update_userid` AS `UpdateUserid`,
    `update_username` AS `UpdateUsername`,
    `memo` AS `Memo`,
    `data_state` AS `DataState`,
    `is_freeze` AS `IsFreeze`,
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
INSERT INTO `tb_app_app_info`
(
    `org_id`,
    `short_name`,
    `full_name`,
    `Classify`,
    `app_key`,
    `manag_orgcode`,
    `manag_orgname`,
    `city_code`,
    `district_code`,
    `org_address`,
    `law_personname`,
    `law_persontel`,
    `contact_name`,
    `contact_tel`,
    `super_orgcode`,
    `update_date`,
    `update_userid`,
    `update_username`,
    `memo`,
    `data_state`,
    `is_freeze`,
    `add_date`,
    `last_reviser_id`,
    `author_id`
)
VALUES
(
    ?OrgId,
    ?ShortName,
    ?FullName,
    ?Classify,
    ?AppId,
    ?ManagOrgcode,
    ?ManagOrgname,
    ?CityCode,
    ?DistrictCode,
    ?OrgAddress,
    ?LawPersonname,
    ?LawPersontel,
    ?ContactName,
    ?ContactTel,
    ?SuperOrgcode,
    ?UpdateDate,
    ?UpdateUserid,
    ?UpdateUsername,
    ?Memo,
    ?DataState,
    ?IsFreeze,
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
UPDATE `tb_app_app_info` SET
       `org_id` = ?OrgId,
       `short_name` = ?ShortName,
       `full_name` = ?FullName,
       `Classify` = ?Classify,
       `app_key` = ?AppId,
       `manag_orgcode` = ?ManagOrgcode,
       `manag_orgname` = ?ManagOrgname,
       `city_code` = ?CityCode,
       `district_code` = ?DistrictCode,
       `org_address` = ?OrgAddress,
       `law_personname` = ?LawPersonname,
       `law_persontel` = ?LawPersontel,
       `contact_name` = ?ContactName,
       `contact_tel` = ?ContactTel,
       `super_orgcode` = ?SuperOrgcode,
       `update_date` = ?UpdateDate,
       `update_userid` = ?UpdateUserid,
       `update_username` = ?UpdateUsername,
       `memo` = ?Memo,
       `data_state` = ?DataState,
       `is_freeze` = ?IsFreeze,
       `last_reviser_id` = ?LastReviserId
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
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_OrgId] > 0)
                sql.AppendLine("       `org_id` = ?OrgId");
            //应用简称
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_ShortName] > 0)
                sql.AppendLine("       `short_name` = ?ShortName");
            //应用全称
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_FullName] > 0)
                sql.AppendLine("       `full_name` = ?FullName");
            //应用类型
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_Classify] > 0)
                sql.AppendLine("       `Classify` = ?Classify");
            //应用标识
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_AppId] > 0)
                sql.AppendLine("       `app_key` = ?AppId");
            //注册管理机构代码
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_ManagOrgcode] > 0)
                sql.AppendLine("       `manag_orgcode` = ?ManagOrgcode");
            //注册管理机构名称
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_ManagOrgname] > 0)
                sql.AppendLine("       `manag_orgname` = ?ManagOrgname");
            //所在市级编码
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_CityCode] > 0)
                sql.AppendLine("       `city_code` = ?CityCode");
            //所在区县编码
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_DistrictCode] > 0)
                sql.AppendLine("       `district_code` = ?DistrictCode");
            //机构详细地址
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_OrgAddress] > 0)
                sql.AppendLine("       `org_address` = ?OrgAddress");
            //机构负责人
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_LawPersonname] > 0)
                sql.AppendLine("       `law_personname` = ?LawPersonname");
            //机构负责人电话
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_LawPersontel] > 0)
                sql.AppendLine("       `law_persontel` = ?LawPersontel");
            //机构联系人
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_ContactName] > 0)
                sql.AppendLine("       `contact_name` = ?ContactName");
            //机构联系人电话
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_ContactTel] > 0)
                sql.AppendLine("       `contact_tel` = ?ContactTel");
            //上级机构代码
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_SuperOrgcode] > 0)
                sql.AppendLine("       `super_orgcode` = ?SuperOrgcode");
            //更新时间
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_UpdateDate] > 0)
                sql.AppendLine("       `update_date` = ?UpdateDate");
            //更新操作员工号
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_UpdateUserid] > 0)
                sql.AppendLine("       `update_userid` = ?UpdateUserid");
            //更新操作员姓名
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_UpdateUsername] > 0)
                sql.AppendLine("       `update_username` = ?UpdateUsername");
            //备注
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_Memo] > 0)
                sql.AppendLine("       `memo` = ?Memo");
            //数据状态
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_DataState] > 0)
                sql.AppendLine("       `data_state` = ?DataState");
            //冻结更新
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_IsFreeze] > 0)
                sql.AppendLine("       `is_freeze` = ?IsFreeze");
            //最后修改者
            if (data.__EntityStatus.ModifiedProperties[AppInfoData._DataStruct_.Real_LastReviserId] > 0)
                sql.AppendLine("       `last_reviser_id` = ?LastReviserId");
            sql.Append(" WHERE `id` = ?Id AND `is_freeze` = 0 AND `data_state` < 255;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","OrgId","Organization","ShortName","FullName","Classify","AppId","ManagOrgcode","ManagOrgname","CityCode","DistrictCode","OrgAddress","LawPersonname","LawPersontel","ContactName","ContactTel","SuperOrgcode","UpdateDate","UpdateUserid","UpdateUsername","Memo","DataState","IsFreeze","AddDate","LastReviserId","LastModifyDate","AuthorId" };

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
            { "OrgId" , "org_id" },
            { "org_id" , "org_id" },
            { "Organization" , "organization" },
            { "ShortName" , "short_name" },
            { "short_name" , "short_name" },
            { "FullName" , "full_name" },
            { "full_name" , "full_name" },
            { "Classify" , "Classify" },
            { "AppId" , "app_key" },
            { "app_key" , "app_key" },
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
            { "SuperOrgcode" , "super_orgcode" },
            { "super_orgcode" , "super_orgcode" },
            { "UpdateDate" , "update_date" },
            { "update_date" , "update_date" },
            { "UpdateUserid" , "update_userid" },
            { "update_userid" , "update_userid" },
            { "UpdateUsername" , "update_username" },
            { "update_username" , "update_username" },
            { "Memo" , "memo" },
            { "DataState" , "data_state" },
            { "data_state" , "data_state" },
            { "IsFreeze" , "is_freeze" },
            { "is_freeze" , "is_freeze" },
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
        protected sealed override void LoadEntity(MySqlDataReader reader,AppInfoData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._orgId = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._organization = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._shortName = reader.GetString(3).ToString();
                if (!reader.IsDBNull(4))
                    entity._fullName = reader.GetString(4).ToString();
                if (!reader.IsDBNull(5))
                    entity._classify = (ClassifyType)reader.GetInt32(5);
                if (!reader.IsDBNull(6))
                    entity._appId = reader.GetString(6).ToString();
                if (!reader.IsDBNull(7))
                    entity._managOrgcode = reader.GetString(7);
                if (!reader.IsDBNull(8))
                    entity._managOrgname = reader.GetString(8);
                if (!reader.IsDBNull(9))
                    entity._cityCode = reader.GetString(9);
                if (!reader.IsDBNull(10))
                    entity._districtCode = reader.GetString(10);
                if (!reader.IsDBNull(11))
                    entity._orgAddress = reader.GetString(11);
                if (!reader.IsDBNull(12))
                    entity._lawPersonname = reader.GetString(12);
                if (!reader.IsDBNull(13))
                    entity._lawPersontel = reader.GetString(13);
                if (!reader.IsDBNull(14))
                    entity._contactName = reader.GetString(14);
                if (!reader.IsDBNull(15))
                    entity._contactTel = reader.GetString(15);
                if (!reader.IsDBNull(16))
                    entity._superOrgcode = reader.GetString(16);
                if (!reader.IsDBNull(17))
                    try{entity._updateDate = reader.GetMySqlDateTime(17).Value;}catch{}
                if (!reader.IsDBNull(18))
                    entity._updateUserid = reader.GetString(18);
                if (!reader.IsDBNull(19))
                    entity._updateUsername = reader.GetString(19);
                if (!reader.IsDBNull(20))
                    entity._memo = reader.GetString(20).ToString();
                if (!reader.IsDBNull(21))
                    entity._dataState = (DataStateType)reader.GetInt32(21);
                if (!reader.IsDBNull(22))
                    entity._isFreeze = (bool)reader.GetBoolean(22);
                if (!reader.IsDBNull(23))
                    try{entity._addDate = reader.GetMySqlDateTime(23).Value;}catch{}
                if (!reader.IsDBNull(24))
                    entity._lastReviserId = (long)reader.GetInt64(24);
                if (!reader.IsDBNull(25))
                    try{entity._lastModifyDate = reader.GetMySqlDateTime(25).Value;}catch{}
                if (!reader.IsDBNull(26))
                    entity._authorId = (long)reader.GetInt64(26);
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
                case "OrgId":
                    return MySqlDbType.Int64;
                case "Organization":
                    return MySqlDbType.VarString;
                case "ShortName":
                    return MySqlDbType.VarString;
                case "FullName":
                    return MySqlDbType.VarString;
                case "Classify":
                    return MySqlDbType.Int32;
                case "AppId":
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
                case "SuperOrgcode":
                    return MySqlDbType.VarString;
                case "UpdateDate":
                    return MySqlDbType.DateTime;
                case "UpdateUserid":
                    return MySqlDbType.VarString;
                case "UpdateUsername":
                    return MySqlDbType.VarString;
                case "Memo":
                    return MySqlDbType.Text;
                case "DataState":
                    return MySqlDbType.Int32;
                case "IsFreeze":
                    return MySqlDbType.Byte;
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
        private void CreateFullSqlParameter(AppInfoData entity, MySqlCommand cmd)
        {
            //02:标识(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:组织标识(OrgId)
            cmd.Parameters.Add(new MySqlParameter("OrgId",MySqlDbType.Int64){ Value = entity.OrgId});
            //04:机构(Organization)
            var isNull = string.IsNullOrWhiteSpace(entity.Organization);
            var parameter = new MySqlParameter("Organization",MySqlDbType.VarString , isNull ? 10 : (entity.Organization).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Organization;
            cmd.Parameters.Add(parameter);
            //05:应用简称(ShortName)
            isNull = string.IsNullOrWhiteSpace(entity.ShortName);
            parameter = new MySqlParameter("ShortName",MySqlDbType.VarString , isNull ? 10 : (entity.ShortName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ShortName;
            cmd.Parameters.Add(parameter);
            //06:应用全称(FullName)
            isNull = string.IsNullOrWhiteSpace(entity.FullName);
            parameter = new MySqlParameter("FullName",MySqlDbType.VarString , isNull ? 10 : (entity.FullName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.FullName;
            cmd.Parameters.Add(parameter);
            //07:应用类型(Classify)
            cmd.Parameters.Add(new MySqlParameter("Classify",MySqlDbType.Int32){ Value = (int)entity.Classify});
            //08:应用标识(AppId)
            isNull = string.IsNullOrWhiteSpace(entity.AppId);
            parameter = new MySqlParameter("AppId",MySqlDbType.VarString , isNull ? 10 : (entity.AppId).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AppId;
            cmd.Parameters.Add(parameter);
            //09:注册管理机构代码(ManagOrgcode)
            isNull = string.IsNullOrWhiteSpace(entity.ManagOrgcode);
            parameter = new MySqlParameter("ManagOrgcode",MySqlDbType.VarString , isNull ? 10 : (entity.ManagOrgcode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ManagOrgcode;
            cmd.Parameters.Add(parameter);
            //10:注册管理机构名称(ManagOrgname)
            isNull = string.IsNullOrWhiteSpace(entity.ManagOrgname);
            parameter = new MySqlParameter("ManagOrgname",MySqlDbType.VarString , isNull ? 10 : (entity.ManagOrgname).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ManagOrgname;
            cmd.Parameters.Add(parameter);
            //11:所在市级编码(CityCode)
            isNull = string.IsNullOrWhiteSpace(entity.CityCode);
            parameter = new MySqlParameter("CityCode",MySqlDbType.VarString , isNull ? 10 : (entity.CityCode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.CityCode;
            cmd.Parameters.Add(parameter);
            //12:所在区县编码(DistrictCode)
            isNull = string.IsNullOrWhiteSpace(entity.DistrictCode);
            parameter = new MySqlParameter("DistrictCode",MySqlDbType.VarString , isNull ? 10 : (entity.DistrictCode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.DistrictCode;
            cmd.Parameters.Add(parameter);
            //13:机构详细地址(OrgAddress)
            isNull = string.IsNullOrWhiteSpace(entity.OrgAddress);
            parameter = new MySqlParameter("OrgAddress",MySqlDbType.VarString , isNull ? 10 : (entity.OrgAddress).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrgAddress;
            cmd.Parameters.Add(parameter);
            //14:机构负责人(LawPersonname)
            isNull = string.IsNullOrWhiteSpace(entity.LawPersonname);
            parameter = new MySqlParameter("LawPersonname",MySqlDbType.VarString , isNull ? 10 : (entity.LawPersonname).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LawPersonname;
            cmd.Parameters.Add(parameter);
            //15:机构负责人电话(LawPersontel)
            isNull = string.IsNullOrWhiteSpace(entity.LawPersontel);
            parameter = new MySqlParameter("LawPersontel",MySqlDbType.VarString , isNull ? 10 : (entity.LawPersontel).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LawPersontel;
            cmd.Parameters.Add(parameter);
            //16:机构联系人(ContactName)
            isNull = string.IsNullOrWhiteSpace(entity.ContactName);
            parameter = new MySqlParameter("ContactName",MySqlDbType.VarString , isNull ? 10 : (entity.ContactName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ContactName;
            cmd.Parameters.Add(parameter);
            //17:机构联系人电话(ContactTel)
            isNull = string.IsNullOrWhiteSpace(entity.ContactTel);
            parameter = new MySqlParameter("ContactTel",MySqlDbType.VarString , isNull ? 10 : (entity.ContactTel).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ContactTel;
            cmd.Parameters.Add(parameter);
            //18:上级机构代码(SuperOrgcode)
            isNull = string.IsNullOrWhiteSpace(entity.SuperOrgcode);
            parameter = new MySqlParameter("SuperOrgcode",MySqlDbType.VarString , isNull ? 10 : (entity.SuperOrgcode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.SuperOrgcode;
            cmd.Parameters.Add(parameter);
            //19:更新时间(UpdateDate)
            isNull = entity.UpdateDate.Year < 1900;
            parameter = new MySqlParameter("UpdateDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.UpdateDate;
            cmd.Parameters.Add(parameter);
            //20:更新操作员工号(UpdateUserid)
            isNull = string.IsNullOrWhiteSpace(entity.UpdateUserid);
            parameter = new MySqlParameter("UpdateUserid",MySqlDbType.VarString , isNull ? 10 : (entity.UpdateUserid).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.UpdateUserid;
            cmd.Parameters.Add(parameter);
            //21:更新操作员姓名(UpdateUsername)
            isNull = string.IsNullOrWhiteSpace(entity.UpdateUsername);
            parameter = new MySqlParameter("UpdateUsername",MySqlDbType.VarString , isNull ? 10 : (entity.UpdateUsername).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.UpdateUsername;
            cmd.Parameters.Add(parameter);
            //22:备注(Memo)
            isNull = string.IsNullOrWhiteSpace(entity.Memo);
            parameter = new MySqlParameter("Memo",MySqlDbType.Text , isNull ? 10 : (entity.Memo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Memo;
            cmd.Parameters.Add(parameter);
            //23:数据状态(DataState)
            cmd.Parameters.Add(new MySqlParameter("DataState",MySqlDbType.Int32){ Value = (int)entity.DataState});
            //24:冻结更新(IsFreeze)
            cmd.Parameters.Add(new MySqlParameter("IsFreeze",MySqlDbType.Byte) { Value = entity.IsFreeze ? (byte)1 : (byte)0 });
            //25:制作时间(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new MySqlParameter("AddDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
            //26:最后修改者(LastReviserId)
            cmd.Parameters.Add(new MySqlParameter("LastReviserId",MySqlDbType.Int64){ Value = entity.LastReviserId});
            //27:最后修改日期(LastModifyDate)
            isNull = entity.LastModifyDate.Year < 1900;
            parameter = new MySqlParameter("LastModifyDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastModifyDate;
            cmd.Parameters.Add(parameter);
            //28:制作人(AuthorId)
            cmd.Parameters.Add(new MySqlParameter("AuthorId",MySqlDbType.Int64){ Value = entity.AuthorId});
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
    `org_id` AS `OrgId`,
    `organization` AS `Organization`,
    `short_name` AS `ShortName`,
    `full_name` AS `FullName`,
    `Classify` AS `Classify`,
    `app_key` AS `AppId`,
    `manag_orgcode` AS `ManagOrgcode`,
    `manag_orgname` AS `ManagOrgname`,
    `city_code` AS `CityCode`,
    `district_code` AS `DistrictCode`,
    `org_address` AS `OrgAddress`,
    `law_personname` AS `LawPersonname`,
    `law_persontel` AS `LawPersontel`,
    `contact_name` AS `ContactName`,
    `contact_tel` AS `ContactTel`,
    `super_orgcode` AS `SuperOrgcode`,
    `update_date` AS `UpdateDate`,
    `update_userid` AS `UpdateUserid`,
    `update_username` AS `UpdateUsername`,
    `data_state` AS `DataState`,
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
                    entity._orgId = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._organization = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._shortName = reader.GetString(3).ToString();
                if (!reader.IsDBNull(4))
                    entity._fullName = reader.GetString(4).ToString();
                if (!reader.IsDBNull(5))
                    entity._classify = (ClassifyType)reader.GetInt32(5);
                if (!reader.IsDBNull(6))
                    entity._appId = reader.GetString(6).ToString();
                if (!reader.IsDBNull(7))
                    entity._managOrgcode = reader.GetString(7);
                if (!reader.IsDBNull(8))
                    entity._managOrgname = reader.GetString(8);
                if (!reader.IsDBNull(9))
                    entity._cityCode = reader.GetString(9);
                if (!reader.IsDBNull(10))
                    entity._districtCode = reader.GetString(10);
                if (!reader.IsDBNull(11))
                    entity._orgAddress = reader.GetString(11);
                if (!reader.IsDBNull(12))
                    entity._lawPersonname = reader.GetString(12);
                if (!reader.IsDBNull(13))
                    entity._lawPersontel = reader.GetString(13);
                if (!reader.IsDBNull(14))
                    entity._contactName = reader.GetString(14);
                if (!reader.IsDBNull(15))
                    entity._contactTel = reader.GetString(15);
                if (!reader.IsDBNull(16))
                    entity._superOrgcode = reader.GetString(16);
                if (!reader.IsDBNull(17))
                    try{entity._updateDate = reader.GetMySqlDateTime(17).Value;}catch{}
                if (!reader.IsDBNull(18))
                    entity._updateUserid = reader.GetString(18);
                if (!reader.IsDBNull(19))
                    entity._updateUsername = reader.GetString(19);
                if (!reader.IsDBNull(20))
                    entity._dataState = (DataStateType)reader.GetInt32(20);
                if (!reader.IsDBNull(21))
                    entity._isFreeze = (bool)reader.GetBoolean(21);
            }
        }
        #endregion

        
    }
    
    partial class AppManageDb
    {


        /// <summary>
        /// 系统内的应用的信息的结构语句
        /// </summary>
        private TableSql _view_app_app_infoSql = new TableSql
        {
            TableName = "view_app_app_info",
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