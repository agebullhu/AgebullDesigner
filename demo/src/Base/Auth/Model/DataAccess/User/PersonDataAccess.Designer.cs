/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/2 23:17:40*/
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
    /// 用户的个人信息
    /// </summary>
    public partial class PersonDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public PersonDataAccess()
        {
            Name = PersonData._DataStruct_.EntityName;
            Caption = PersonData._DataStruct_.EntityCaption;
            Description = PersonData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => PersonData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_user_person";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_user_person";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => PersonData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `user_id` AS `UserId`,
    `sex` AS `Sex`,
    `region_province` AS `RegionProvince`,
    `region_city` AS `RegionCity`,
    `region_county` AS `RegionCounty`,
    `avatar_url` AS `AvatarUrl`,
    `nick_name` AS `NickName`,
    `id_card` AS `IdCard`,
    `real_name` AS `RealName`,
    `phone_number` AS `PhoneNumber`,
    `birthday` AS `Birthday`,
    `cert_type` AS `CertType`,
    `icon` AS `Icon`,
    `nation` AS `Nation`,
    `tel` AS `Tel`,
    `email` AS `Email`,
    `home_address` AS `HomeAddress`,
    `company` AS `Company`,
    `job_title` AS `JobTitle`,
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
INSERT INTO `tb_user_person`
(
    `user_id`,
    `sex`,
    `region_province`,
    `region_city`,
    `region_county`,
    `avatar_url`,
    `nick_name`,
    `id_card`,
    `real_name`,
    `phone_number`,
    `birthday`,
    `cert_type`,
    `icon`,
    `nation`,
    `tel`,
    `email`,
    `home_address`,
    `company`,
    `job_title`,
    `is_freeze`,
    `data_state`,
    `add_date`,
    `last_reviser_id`,
    `author_id`
)
VALUES
(
    ?UserId,
    ?Sex,
    ?RegionProvince,
    ?RegionCity,
    ?RegionCounty,
    ?AvatarUrl,
    ?NickName,
    ?IdCard,
    ?RealName,
    ?PhoneNumber,
    ?Birthday,
    ?CertType,
    ?Icon,
    ?Nation,
    ?Tel,
    ?Email,
    ?HomeAddress,
    ?Company,
    ?JobTitle,
    ?IsFreeze,
    ?DataState,
    ?AddDate,
    ?LastReviserId,
    ?AuthorId
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
UPDATE `tb_user_person` SET
       `sex` = ?Sex,
       `region_province` = ?RegionProvince,
       `region_city` = ?RegionCity,
       `region_county` = ?RegionCounty,
       `avatar_url` = ?AvatarUrl,
       `nick_name` = ?NickName,
       `id_card` = ?IdCard,
       `real_name` = ?RealName,
       `phone_number` = ?PhoneNumber,
       `birthday` = ?Birthday,
       `cert_type` = ?CertType,
       `icon` = ?Icon,
       `nation` = ?Nation,
       `tel` = ?Tel,
       `email` = ?Email,
       `home_address` = ?HomeAddress,
       `company` = ?Company,
       `job_title` = ?JobTitle,
       `is_freeze` = ?IsFreeze,
       `data_state` = ?DataState,
       `last_reviser_id` = ?LastReviserId
 WHERE `user_id` = ?UserId AND `is_freeze` = 0 AND `data_state` < 255;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(PersonData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_user_person` SET");
            //性别
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_Sex] > 0)
                sql.AppendLine("       `sex` = ?Sex");
            //所在省
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_RegionProvince] > 0)
                sql.AppendLine("       `region_province` = ?RegionProvince");
            //所在市
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_RegionCity] > 0)
                sql.AppendLine("       `region_city` = ?RegionCity");
            //所在县
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_RegionCounty] > 0)
                sql.AppendLine("       `region_county` = ?RegionCounty");
            //头像
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_AvatarUrl] > 0)
                sql.AppendLine("       `avatar_url` = ?AvatarUrl");
            //昵称
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_NickName] > 0)
                sql.AppendLine("       `nick_name` = ?NickName");
            //身份证号
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_IdCard] > 0)
                sql.AppendLine("       `id_card` = ?IdCard");
            //真实姓名
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_RealName] > 0)
                sql.AppendLine("       `real_name` = ?RealName");
            //手机号
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_PhoneNumber] > 0)
                sql.AppendLine("       `phone_number` = ?PhoneNumber");
            //生日
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_Birthday] > 0)
                sql.AppendLine("       `birthday` = ?Birthday");
            //证件类型
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_CertType] > 0)
                sql.AppendLine("       `cert_type` = ?CertType");
            //头像照片
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_Icon] > 0)
                sql.AppendLine("       `icon` = ?Icon");
            //民族
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_Nation] > 0)
                sql.AppendLine("       `nation` = ?Nation");
            //电话
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_Tel] > 0)
                sql.AppendLine("       `tel` = ?Tel");
            //电子邮件
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_Email] > 0)
                sql.AppendLine("       `email` = ?Email");
            //地址
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_HomeAddress] > 0)
                sql.AppendLine("       `home_address` = ?HomeAddress");
            //所在公司
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_Company] > 0)
                sql.AppendLine("       `company` = ?Company");
            //职位称呼
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_JobTitle] > 0)
                sql.AppendLine("       `job_title` = ?JobTitle");
            //冻结更新
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_IsFreeze] > 0)
                sql.AppendLine("       `is_freeze` = ?IsFreeze");
            //数据状态
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_DataState] > 0)
                sql.AppendLine("       `data_state` = ?DataState");
            //最后修改者
            if (data.__EntityStatus.ModifiedProperties[PersonData._DataStruct_.Real_LastReviserId] > 0)
                sql.AppendLine("       `last_reviser_id` = ?LastReviserId");
            sql.Append(" WHERE `user_id` = ?UserId AND `is_freeze` = 0 AND `data_state` < 255;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "UserId","Sex","RegionProvince","RegionCity","RegionCounty","AvatarUrl","NickName","IdCard","RealName","PhoneNumber","Birthday","CertType","Icon","Nation","Tel","Email","HomeAddress","Company","JobTitle","IsFreeze","DataState","AddDate","LastReviserId","LastModifyDate","AuthorId" };

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
            { "UserId" , "user_id" },
            { "user_id" , "user_id" },
            { "Sex" , "sex" },
            { "RegionProvince" , "region_province" },
            { "region_province" , "region_province" },
            { "RegionCity" , "region_city" },
            { "region_city" , "region_city" },
            { "RegionCounty" , "region_county" },
            { "region_county" , "region_county" },
            { "AvatarUrl" , "avatar_url" },
            { "avatar_url" , "avatar_url" },
            { "NickName" , "nick_name" },
            { "nick_name" , "nick_name" },
            { "IdCard" , "id_card" },
            { "id_card" , "id_card" },
            { "RealName" , "real_name" },
            { "real_name" , "real_name" },
            { "PhoneNumber" , "phone_number" },
            { "phone_number" , "phone_number" },
            { "Birthday" , "birthday" },
            { "CertType" , "cert_type" },
            { "cert_type" , "cert_type" },
            { "Icon" , "icon" },
            { "Nation" , "nation" },
            { "Tel" , "tel" },
            { "Email" , "email" },
            { "HomeAddress" , "home_address" },
            { "home_address" , "home_address" },
            { "Company" , "company" },
            { "JobTitle" , "job_title" },
            { "job_title" , "job_title" },
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
            { "Id" , "user_id" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,PersonData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._userId = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._sex = (SexType)reader.GetInt32(1);
                if (!reader.IsDBNull(2))
                    entity._regionProvince = (int)reader.GetInt32(2);
                if (!reader.IsDBNull(3))
                    entity._regionCity = (int)reader.GetInt32(3);
                if (!reader.IsDBNull(4))
                    entity._regionCounty = (int)reader.GetInt32(4);
                if (!reader.IsDBNull(5))
                    entity._avatarUrl = reader.GetString(5).ToString();
                if (!reader.IsDBNull(6))
                    entity._nickName = reader.GetString(6).ToString();
                if (!reader.IsDBNull(7))
                    entity._idCard = reader.GetString(7).ToString();
                if (!reader.IsDBNull(8))
                    entity._realName = reader.GetString(8).ToString();
                if (!reader.IsDBNull(9))
                    entity._phoneNumber = reader.GetString(9).ToString();
                if (!reader.IsDBNull(10))
                    try{entity._birthday = reader.GetMySqlDateTime(10).Value;}catch{}
                if (!reader.IsDBNull(11))
                    entity._certType = (CardType)reader.GetInt32(11);
                if (!reader.IsDBNull(12))
                    entity._icon = (byte[])reader[12];
                if (!reader.IsDBNull(13))
                    entity._nation = reader.GetString(13);
                if (!reader.IsDBNull(14))
                    entity._tel = reader.GetString(14);
                if (!reader.IsDBNull(15))
                    entity._email = reader.GetString(15);
                if (!reader.IsDBNull(16))
                    entity._homeAddress = reader.GetString(16);
                if (!reader.IsDBNull(17))
                    entity._company = reader.GetString(17);
                if (!reader.IsDBNull(18))
                    entity._jobTitle = reader.GetString(18);
                if (!reader.IsDBNull(19))
                    entity._isFreeze = (bool)reader.GetBoolean(19);
                if (!reader.IsDBNull(20))
                    entity._dataState = (DataStateType)reader.GetInt32(20);
                if (!reader.IsDBNull(21))
                    try{entity._addDate = reader.GetMySqlDateTime(21).Value;}catch{}
                if (!reader.IsDBNull(22))
                    entity._lastReviserId = (long)reader.GetInt64(22);
                if (!reader.IsDBNull(23))
                    try{entity._lastModifyDate = reader.GetMySqlDateTime(23).Value;}catch{}
                if (!reader.IsDBNull(24))
                    entity._authorId = (long)reader.GetInt64(24);
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
                case "UserId":
                    return MySqlDbType.Int64;
                case "Sex":
                    return MySqlDbType.Int32;
                case "RegionProvince":
                    return MySqlDbType.Int32;
                case "RegionCity":
                    return MySqlDbType.Int32;
                case "RegionCounty":
                    return MySqlDbType.Int32;
                case "AvatarUrl":
                    return MySqlDbType.VarString;
                case "NickName":
                    return MySqlDbType.VarString;
                case "IdCard":
                    return MySqlDbType.VarString;
                case "RealName":
                    return MySqlDbType.VarString;
                case "PhoneNumber":
                    return MySqlDbType.VarString;
                case "Birthday":
                    return MySqlDbType.DateTime;
                case "CertType":
                    return MySqlDbType.Int32;
                case "Icon":
                    return MySqlDbType.LongBlob;
                case "Nation":
                    return MySqlDbType.VarString;
                case "Tel":
                    return MySqlDbType.VarString;
                case "Email":
                    return MySqlDbType.VarString;
                case "HomeAddress":
                    return MySqlDbType.VarString;
                case "Company":
                    return MySqlDbType.VarString;
                case "JobTitle":
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
        private void CreateFullSqlParameter(PersonData entity, MySqlCommand cmd)
        {
            //02:用户标识(UserId)
            cmd.Parameters.Add(new MySqlParameter("UserId",MySqlDbType.Int64){ Value = entity.UserId});
            //03:性别(Sex)
            cmd.Parameters.Add(new MySqlParameter("Sex",MySqlDbType.Int32){ Value = (int)entity.Sex});
            //04:所在省(RegionProvince)
            cmd.Parameters.Add(new MySqlParameter("RegionProvince",MySqlDbType.Int32){ Value = entity.RegionProvince});
            //05:所在市(RegionCity)
            cmd.Parameters.Add(new MySqlParameter("RegionCity",MySqlDbType.Int32){ Value = entity.RegionCity});
            //06:所在县(RegionCounty)
            cmd.Parameters.Add(new MySqlParameter("RegionCounty",MySqlDbType.Int32){ Value = entity.RegionCounty});
            //07:头像(AvatarUrl)
            var isNull = string.IsNullOrWhiteSpace(entity.AvatarUrl);
            var parameter = new MySqlParameter("AvatarUrl",MySqlDbType.VarString , isNull ? 10 : (entity.AvatarUrl).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AvatarUrl;
            cmd.Parameters.Add(parameter);
            //08:昵称(NickName)
            isNull = string.IsNullOrWhiteSpace(entity.NickName);
            parameter = new MySqlParameter("NickName",MySqlDbType.VarString , isNull ? 10 : (entity.NickName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.NickName;
            cmd.Parameters.Add(parameter);
            //09:身份证号(IdCard)
            isNull = string.IsNullOrWhiteSpace(entity.IdCard);
            parameter = new MySqlParameter("IdCard",MySqlDbType.VarString , isNull ? 10 : (entity.IdCard).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.IdCard;
            cmd.Parameters.Add(parameter);
            //10:真实姓名(RealName)
            isNull = string.IsNullOrWhiteSpace(entity.RealName);
            parameter = new MySqlParameter("RealName",MySqlDbType.VarString , isNull ? 10 : (entity.RealName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.RealName;
            cmd.Parameters.Add(parameter);
            //11:手机号(PhoneNumber)
            isNull = string.IsNullOrWhiteSpace(entity.PhoneNumber);
            parameter = new MySqlParameter("PhoneNumber",MySqlDbType.VarString , isNull ? 10 : (entity.PhoneNumber).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.PhoneNumber;
            cmd.Parameters.Add(parameter);
            //12:生日(Birthday)
            isNull = entity.Birthday.Year < 1900;
            parameter = new MySqlParameter("Birthday",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Birthday;
            cmd.Parameters.Add(parameter);
            //13:证件类型(CertType)
            cmd.Parameters.Add(new MySqlParameter("CertType",MySqlDbType.Int32){ Value = (int)entity.CertType});
            //14:头像照片(Icon)
            isNull = entity.Icon == null || entity.Icon.Length == 0;
            parameter = new MySqlParameter("Icon",MySqlDbType.LongBlob , isNull ? 10 : entity.Icon.Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Icon;
            cmd.Parameters.Add(parameter);
            //15:民族(Nation)
            isNull = string.IsNullOrWhiteSpace(entity.Nation);
            parameter = new MySqlParameter("Nation",MySqlDbType.VarString , isNull ? 10 : (entity.Nation).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Nation;
            cmd.Parameters.Add(parameter);
            //16:电话(Tel)
            isNull = string.IsNullOrWhiteSpace(entity.Tel);
            parameter = new MySqlParameter("Tel",MySqlDbType.VarString , isNull ? 10 : (entity.Tel).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Tel;
            cmd.Parameters.Add(parameter);
            //17:电子邮件(Email)
            isNull = string.IsNullOrWhiteSpace(entity.Email);
            parameter = new MySqlParameter("Email",MySqlDbType.VarString , isNull ? 10 : (entity.Email).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Email;
            cmd.Parameters.Add(parameter);
            //18:地址(HomeAddress)
            isNull = string.IsNullOrWhiteSpace(entity.HomeAddress);
            parameter = new MySqlParameter("HomeAddress",MySqlDbType.VarString , isNull ? 10 : (entity.HomeAddress).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.HomeAddress;
            cmd.Parameters.Add(parameter);
            //19:所在公司(Company)
            isNull = string.IsNullOrWhiteSpace(entity.Company);
            parameter = new MySqlParameter("Company",MySqlDbType.VarString , isNull ? 10 : (entity.Company).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Company;
            cmd.Parameters.Add(parameter);
            //20:职位称呼(JobTitle)
            isNull = string.IsNullOrWhiteSpace(entity.JobTitle);
            parameter = new MySqlParameter("JobTitle",MySqlDbType.VarString , isNull ? 10 : (entity.JobTitle).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.JobTitle;
            cmd.Parameters.Add(parameter);
            //21:冻结更新(IsFreeze)
            cmd.Parameters.Add(new MySqlParameter("IsFreeze",MySqlDbType.Byte) { Value = entity.IsFreeze ? (byte)1 : (byte)0 });
            //22:数据状态(DataState)
            cmd.Parameters.Add(new MySqlParameter("DataState",MySqlDbType.Int32){ Value = (int)entity.DataState});
            //23:制作时间(AddDate)
            isNull = entity.AddDate.Year < 1900;
            parameter = new MySqlParameter("AddDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AddDate;
            cmd.Parameters.Add(parameter);
            //24:最后修改者(LastReviserId)
            cmd.Parameters.Add(new MySqlParameter("LastReviserId",MySqlDbType.Int64){ Value = entity.LastReviserId});
            //25:最后修改日期(LastModifyDate)
            isNull = entity.LastModifyDate.Year < 1900;
            parameter = new MySqlParameter("LastModifyDate",MySqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LastModifyDate;
            cmd.Parameters.Add(parameter);
            //26:制作人(AuthorId)
            cmd.Parameters.Add(new MySqlParameter("AuthorId",MySqlDbType.Int64){ Value = entity.AuthorId});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(PersonData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(PersonData entity, MySqlCommand cmd)
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
        public override void SimpleLoad(MySqlDataReader reader,PersonData entity)
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
        /// 用户的个人信息的结构语句
        /// </summary>
        private TableSql _tb_user_personSql = new TableSql
        {
            TableName = "tb_user_person",
            PimaryKey = "UserId"
        };


        /// <summary>
        /// 用户的个人信息数据访问对象
        /// </summary>
        private PersonDataAccess _persons;

        /// <summary>
        /// 用户的个人信息数据访问对象
        /// </summary>
        public PersonDataAccess Persons
        {
            get
            {
                return this._persons ?? ( this._persons = new PersonDataAccess{ DataBase = this});
            }
        }
    }
}