/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 16:30:31*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;
using Agebull.Common;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.SqlServer;
using Agebull.EntityModel.Redis;
using Agebull.EntityModel.EasyUI;



namespace HPC.Projects.DataAccess
{
    /// <summary>
    /// 站点
    /// </summary>
    public partial class SiteDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_Site;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbSite";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbSite";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"SID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [SID] AS [SID],
    [SiteID] AS [SiteID],
    [SiteKey] AS [SiteKey],
    [SiteNameWhole] AS [SiteNameWhole],
    [SiteNameShort] AS [SiteNameShort],
    [SiteLogo] AS [SiteLogo],
    [SiteIntroduce] AS [SiteIntroduce],
    [BusinessLicenseCode] AS [BusinessLicenseCode],
    [BusinessLicenseImage] AS [BusinessLicenseImage],
    [LegalPersonName] AS [LegalPersonName],
    [LegalPersonCardID] AS [LegalPersonCardID],
    [LegalPersonCardImage01] AS [LegalPersonCardImage01],
    [LegalPersonCardImage02] AS [LegalPersonCardImage02],
    [Remarks] AS [Remarks]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [SID],
    [SiteID],
    [SiteKey],
    [SiteNameWhole],
    [SiteNameShort],
    [SiteLogo],
    [SiteIntroduce],
    [BusinessLicenseCode],
    [BusinessLicenseImage],
    [LegalPersonName],
    [LegalPersonCardID],
    [LegalPersonCardImage01],
    [LegalPersonCardImage02],
    [Remarks]
)
VALUES
(
    @SID,
    @SiteID,
    @SiteKey,
    @SiteNameWhole,
    @SiteNameShort,
    @SiteLogo,
    @SiteIntroduce,
    @BusinessLicenseCode,
    @BusinessLicenseImage,
    @LegalPersonName,
    @LegalPersonCardID,
    @LegalPersonCardImage01,
    @LegalPersonCardImage02,
    @Remarks
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [SID] = @SID,
       [SiteID] = @SiteID,
       [SiteKey] = @SiteKey,
       [SiteNameWhole] = @SiteNameWhole,
       [SiteNameShort] = @SiteNameShort,
       [SiteLogo] = @SiteLogo,
       [SiteIntroduce] = @SiteIntroduce,
       [BusinessLicenseCode] = @BusinessLicenseCode,
       [BusinessLicenseImage] = @BusinessLicenseImage,
       [LegalPersonName] = @LegalPersonName,
       [LegalPersonCardID] = @LegalPersonCardID,
       [LegalPersonCardImage01] = @LegalPersonCardImage01,
       [LegalPersonCardImage02] = @LegalPersonCardImage02,
       [Remarks] = @Remarks
 WHERE [SID] = @SID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(SiteData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_SID] > 0)
                sql.AppendLine("       [SID] = @SID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_SiteID] > 0)
                sql.AppendLine("       [SiteID] = @SiteID");
            //站点全局标识
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_SiteKey] > 0)
                sql.AppendLine("       [SiteKey] = @SiteKey");
            //站点全称
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_SiteNameWhole] > 0)
                sql.AppendLine("       [SiteNameWhole] = @SiteNameWhole");
            //站点简称
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_SiteNameShort] > 0)
                sql.AppendLine("       [SiteNameShort] = @SiteNameShort");
            //LOGO
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_SiteLogo] > 0)
                sql.AppendLine("       [SiteLogo] = @SiteLogo");
            //站点介绍
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_SiteIntroduce] > 0)
                sql.AppendLine("       [SiteIntroduce] = @SiteIntroduce");
            //营业执照代码
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_BusinessLicenseCode] > 0)
                sql.AppendLine("       [BusinessLicenseCode] = @BusinessLicenseCode");
            //营业执照形象
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_BusinessLicenseImage] > 0)
                sql.AppendLine("       [BusinessLicenseImage] = @BusinessLicenseImage");
            //法人名称
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_LegalPersonName] > 0)
                sql.AppendLine("       [LegalPersonName] = @LegalPersonName");
            //法人身份证号
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_LegalPersonCardID] > 0)
                sql.AppendLine("       [LegalPersonCardID] = @LegalPersonCardID");
            //法人身份证图像正面
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_LegalPersonCardImage01] > 0)
                sql.AppendLine("       [LegalPersonCardImage01] = @LegalPersonCardImage01");
            //法人身份证图像反面
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_LegalPersonCardImage02] > 0)
                sql.AppendLine("       [LegalPersonCardImage02] = @LegalPersonCardImage02");
            //备注
            if (data.__EntityStatus.ModifiedProperties[SiteData._DataStruct_.Real_Remarks] > 0)
                sql.AppendLine("       [Remarks] = @Remarks");
            sql.Append(" WHERE [SID] = @SID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "SID","SiteID","SiteKey","SiteNameWhole","SiteNameShort","SiteLogo","SiteIntroduce","BusinessLicenseCode","BusinessLicenseImage","LegalPersonName","LegalPersonCardID","LegalPersonCardImage01","LegalPersonCardImage02","Remarks" };

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
            { "SID" , "SID" },
            { "SiteID" , "SiteID" },
            { "SiteKey" , "SiteKey" },
            { "SiteNameWhole" , "SiteNameWhole" },
            { "SiteNameShort" , "SiteNameShort" },
            { "SiteLogo" , "SiteLogo" },
            { "SiteIntroduce" , "SiteIntroduce" },
            { "BusinessLicenseCode" , "BusinessLicenseCode" },
            { "BusinessLicenseImage" , "BusinessLicenseImage" },
            { "LegalPersonName" , "LegalPersonName" },
            { "LegalPersonCardID" , "LegalPersonCardID" },
            { "LegalPersonCardImage01" , "LegalPersonCardImage01" },
            { "LegalPersonCardImage02" , "LegalPersonCardImage02" },
            { "Remarks" , "Remarks" },
            { "Id" , "SID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,SiteData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._sID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteID = reader.GetString(1).ToString();
                if (!reader.IsDBNull(2))
                    entity._siteKey = reader.GetString(2).ToString();
                if (!reader.IsDBNull(3))
                    entity._siteNameWhole = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._siteNameShort = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._siteLogo = reader.GetString(5);
                if (!reader.IsDBNull(6))
                    entity._siteIntroduce = reader.GetString(6);
                if (!reader.IsDBNull(7))
                    entity._businessLicenseCode = reader.GetString(7);
                if (!reader.IsDBNull(8))
                    entity._businessLicenseImage = reader.GetString(8);
                if (!reader.IsDBNull(9))
                    entity._legalPersonName = reader.GetString(9);
                if (!reader.IsDBNull(10))
                    entity._legalPersonCardID = reader.GetString(10);
                if (!reader.IsDBNull(11))
                    entity._legalPersonCardImage01 = reader.GetString(11);
                if (!reader.IsDBNull(12))
                    entity._legalPersonCardImage02 = reader.GetString(12);
                if (!reader.IsDBNull(13))
                    entity._remarks = reader.GetString(13);
            }
        }

        /// <summary>
        /// 得到字段的DbType类型
        /// </summary>
        /// <param name="field">字段名称</param>
        /// <returns>参数</returns>
        protected sealed override SqlDbType GetDbType(string field)
        {
            switch (field)
            {
                case "SID":
                    return SqlDbType.BigInt;
                case "SiteID":
                    return SqlDbType.NVarChar;
                case "SiteKey":
                    return SqlDbType.NVarChar;
                case "SiteNameWhole":
                    return SqlDbType.NVarChar;
                case "SiteNameShort":
                    return SqlDbType.NVarChar;
                case "SiteLogo":
                    return SqlDbType.NVarChar;
                case "SiteIntroduce":
                    return SqlDbType.NVarChar;
                case "BusinessLicenseCode":
                    return SqlDbType.NVarChar;
                case "BusinessLicenseImage":
                    return SqlDbType.NVarChar;
                case "LegalPersonName":
                    return SqlDbType.NVarChar;
                case "LegalPersonCardID":
                    return SqlDbType.NVarChar;
                case "LegalPersonCardImage01":
                    return SqlDbType.NVarChar;
                case "LegalPersonCardImage02":
                    return SqlDbType.NVarChar;
                case "Remarks":
                    return SqlDbType.NVarChar;
            }
            return SqlDbType.NVarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void CreateFullSqlParameter(SiteData entity, SqlCommand cmd)
        {
            //02:主键(SID)
            cmd.Parameters.Add(new SqlParameter("SID",SqlDbType.BigInt){ Value = entity.SID});
            //03:站点标识(SiteID)
            var isNull = string.IsNullOrWhiteSpace(entity.SiteID);
            var parameter = new SqlParameter("SiteID",SqlDbType.NVarChar , isNull ? 10 : (entity.SiteID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.SiteID;
            cmd.Parameters.Add(parameter);
            //04:站点全局标识(SiteKey)
            isNull = string.IsNullOrWhiteSpace(entity.SiteKey);
            parameter = new SqlParameter("SiteKey",SqlDbType.NVarChar , isNull ? 10 : (entity.SiteKey).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.SiteKey;
            cmd.Parameters.Add(parameter);
            //05:站点全称(SiteNameWhole)
            isNull = string.IsNullOrWhiteSpace(entity.SiteNameWhole);
            parameter = new SqlParameter("SiteNameWhole",SqlDbType.NVarChar , isNull ? 10 : (entity.SiteNameWhole).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.SiteNameWhole;
            cmd.Parameters.Add(parameter);
            //06:站点简称(SiteNameShort)
            isNull = string.IsNullOrWhiteSpace(entity.SiteNameShort);
            parameter = new SqlParameter("SiteNameShort",SqlDbType.NVarChar , isNull ? 10 : (entity.SiteNameShort).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.SiteNameShort;
            cmd.Parameters.Add(parameter);
            //07:LOGO(SiteLogo)
            isNull = string.IsNullOrWhiteSpace(entity.SiteLogo);
            parameter = new SqlParameter("SiteLogo",SqlDbType.NVarChar , isNull ? 10 : (entity.SiteLogo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.SiteLogo;
            cmd.Parameters.Add(parameter);
            //08:站点介绍(SiteIntroduce)
            isNull = string.IsNullOrWhiteSpace(entity.SiteIntroduce);
            parameter = new SqlParameter("SiteIntroduce",SqlDbType.NVarChar , isNull ? 10 : (entity.SiteIntroduce).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.SiteIntroduce;
            cmd.Parameters.Add(parameter);
            //09:营业执照代码(BusinessLicenseCode)
            isNull = string.IsNullOrWhiteSpace(entity.BusinessLicenseCode);
            parameter = new SqlParameter("BusinessLicenseCode",SqlDbType.NVarChar , isNull ? 10 : (entity.BusinessLicenseCode).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.BusinessLicenseCode;
            cmd.Parameters.Add(parameter);
            //10:营业执照形象(BusinessLicenseImage)
            isNull = string.IsNullOrWhiteSpace(entity.BusinessLicenseImage);
            parameter = new SqlParameter("BusinessLicenseImage",SqlDbType.NVarChar , isNull ? 10 : (entity.BusinessLicenseImage).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.BusinessLicenseImage;
            cmd.Parameters.Add(parameter);
            //11:法人名称(LegalPersonName)
            isNull = string.IsNullOrWhiteSpace(entity.LegalPersonName);
            parameter = new SqlParameter("LegalPersonName",SqlDbType.NVarChar , isNull ? 10 : (entity.LegalPersonName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LegalPersonName;
            cmd.Parameters.Add(parameter);
            //12:法人身份证号(LegalPersonCardID)
            isNull = string.IsNullOrWhiteSpace(entity.LegalPersonCardID);
            parameter = new SqlParameter("LegalPersonCardID",SqlDbType.NVarChar , isNull ? 10 : (entity.LegalPersonCardID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LegalPersonCardID;
            cmd.Parameters.Add(parameter);
            //13:法人身份证图像正面(LegalPersonCardImage01)
            isNull = string.IsNullOrWhiteSpace(entity.LegalPersonCardImage01);
            parameter = new SqlParameter("LegalPersonCardImage01",SqlDbType.NVarChar , isNull ? 10 : (entity.LegalPersonCardImage01).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LegalPersonCardImage01;
            cmd.Parameters.Add(parameter);
            //14:法人身份证图像反面(LegalPersonCardImage02)
            isNull = string.IsNullOrWhiteSpace(entity.LegalPersonCardImage02);
            parameter = new SqlParameter("LegalPersonCardImage02",SqlDbType.NVarChar , isNull ? 10 : (entity.LegalPersonCardImage02).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LegalPersonCardImage02;
            cmd.Parameters.Add(parameter);
            //15:备注(Remarks)
            isNull = string.IsNullOrWhiteSpace(entity.Remarks);
            parameter = new SqlParameter("Remarks",SqlDbType.NVarChar , isNull ? 10 : (entity.Remarks).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Remarks;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(SiteData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(SiteData entity, SqlCommand cmd)
        {
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return false;
        }
        #endregion

    }

    sealed partial class HpcSqlServerDb
    {


        /// <summary>
        /// 站点的结构语句
        /// </summary>
        private TableSql _tbSiteSql = new TableSql
        {
            TableName = "tbSite",
            PimaryKey = "SID"
        };


        /// <summary>
        /// 站点数据访问对象
        /// </summary>
        private SiteDataAccess _sites;

        /// <summary>
        /// 站点数据访问对象
        /// </summary>
        public SiteDataAccess Sites
        {
            get
            {
                return this._sites ?? ( this._sites = new SiteDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 站点(tbSite):站点
        /// </summary>
        public const int Table_Site = 0x0;
    }
}