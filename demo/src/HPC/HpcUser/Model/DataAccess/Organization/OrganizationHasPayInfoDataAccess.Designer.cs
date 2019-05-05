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
    /// 组织是否支付信息
    /// </summary>
    public partial class OrganizationHasPayInfoDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_OrganizationHasPayInfo;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbOrganizationHasPayInfo";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbOrganizationHasPayInfo";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"PID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [PID] AS [PID],
    [SiteSID] AS [SiteSID],
    [OrgOID] AS [OrgOID],
    [paySource] AS [paySource],
    [APPID] AS [APPID],
    [APPKEY] AS [APPKEY],
    [MCHID] AS [MCHID],
    [MCHKEY] AS [MCHKEY],
    [ServerMCH] AS [ServerMCH],
    [IsDefault] AS [IsDefault],
    [Remarks] AS [Remarks]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [PID],
    [SiteSID],
    [OrgOID],
    [paySource],
    [APPID],
    [APPKEY],
    [MCHID],
    [MCHKEY],
    [ServerMCH],
    [IsDefault],
    [Remarks]
)
VALUES
(
    @PID,
    @SiteSID,
    @OrgOID,
    @paySource,
    @APPID,
    @APPKEY,
    @MCHID,
    @MCHKEY,
    @ServerMCH,
    @IsDefault,
    @Remarks
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [PID] = @PID,
       [SiteSID] = @SiteSID,
       [OrgOID] = @OrgOID,
       [paySource] = @paySource,
       [APPID] = @APPID,
       [APPKEY] = @APPKEY,
       [MCHID] = @MCHID,
       [MCHKEY] = @MCHKEY,
       [ServerMCH] = @ServerMCH,
       [IsDefault] = @IsDefault,
       [Remarks] = @Remarks
 WHERE [PID] = @PID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(OrganizationHasPayInfoData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[OrganizationHasPayInfoData._DataStruct_.Real_PID] > 0)
                sql.AppendLine("       [PID] = @PID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[OrganizationHasPayInfoData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[OrganizationHasPayInfoData._DataStruct_.Real_OrgOID] > 0)
                sql.AppendLine("       [OrgOID] = @OrgOID");
            //工资来源
            if (data.__EntityStatus.ModifiedProperties[OrganizationHasPayInfoData._DataStruct_.Real_paySource] > 0)
                sql.AppendLine("       [paySource] = @paySource");
            //应用标识
            if (data.__EntityStatus.ModifiedProperties[OrganizationHasPayInfoData._DataStruct_.Real_APPID] > 0)
                sql.AppendLine("       [APPID] = @APPID");
            //阿普基
            if (data.__EntityStatus.ModifiedProperties[OrganizationHasPayInfoData._DataStruct_.Real_APPKEY] > 0)
                sql.AppendLine("       [APPKEY] = @APPKEY");
            //妇幼保健院
            if (data.__EntityStatus.ModifiedProperties[OrganizationHasPayInfoData._DataStruct_.Real_MCHID] > 0)
                sql.AppendLine("       [MCHID] = @MCHID");
            //麦克奇
            if (data.__EntityStatus.ModifiedProperties[OrganizationHasPayInfoData._DataStruct_.Real_MCHKEY] > 0)
                sql.AppendLine("       [MCHKEY] = @MCHKEY");
            //服务器MCH
            if (data.__EntityStatus.ModifiedProperties[OrganizationHasPayInfoData._DataStruct_.Real_ServerMCH] > 0)
                sql.AppendLine("       [ServerMCH] = @ServerMCH");
            //默认为
            if (data.__EntityStatus.ModifiedProperties[OrganizationHasPayInfoData._DataStruct_.Real_IsDefault] > 0)
                sql.AppendLine("       [IsDefault] = @IsDefault");
            //评论
            if (data.__EntityStatus.ModifiedProperties[OrganizationHasPayInfoData._DataStruct_.Real_Remarks] > 0)
                sql.AppendLine("       [Remarks] = @Remarks");
            sql.Append(" WHERE [PID] = @PID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "PID","SiteSID","OrgOID","paySource","APPID","APPKEY","MCHID","MCHKEY","ServerMCH","IsDefault","Remarks" };

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
            { "PID" , "PID" },
            { "SiteSID" , "SiteSID" },
            { "OrgOID" , "OrgOID" },
            { "paySource" , "paySource" },
            { "APPID" , "APPID" },
            { "APPKEY" , "APPKEY" },
            { "MCHID" , "MCHID" },
            { "MCHKEY" , "MCHKEY" },
            { "ServerMCH" , "ServerMCH" },
            { "IsDefault" , "IsDefault" },
            { "Remarks" , "Remarks" },
            { "Id" , "PID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,OrganizationHasPayInfoData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._pID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._orgOID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._paySource = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._aPPID = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._aPPKEY = reader.GetString(5);
                if (!reader.IsDBNull(6))
                    entity._mCHID = reader.GetString(6);
                if (!reader.IsDBNull(7))
                    entity._mCHKEY = reader.GetString(7);
                if (!reader.IsDBNull(8))
                    entity._serverMCH = reader.GetString(8);
                if (!reader.IsDBNull(9))
                    entity._isDefault = (bool)reader.GetBoolean(9);
                if (!reader.IsDBNull(10))
                    entity._remarks = reader.GetString(10);
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
                case "PID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "OrgOID":
                    return SqlDbType.BigInt;
                case "paySource":
                    return SqlDbType.NVarChar;
                case "APPID":
                    return SqlDbType.NVarChar;
                case "APPKEY":
                    return SqlDbType.NVarChar;
                case "MCHID":
                    return SqlDbType.NVarChar;
                case "MCHKEY":
                    return SqlDbType.NVarChar;
                case "ServerMCH":
                    return SqlDbType.NVarChar;
                case "IsDefault":
                    return SqlDbType.Bit;
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
        public void CreateFullSqlParameter(OrganizationHasPayInfoData entity, SqlCommand cmd)
        {
            //02:主键(PID)
            cmd.Parameters.Add(new SqlParameter("PID",SqlDbType.BigInt){ Value = entity.PID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:组织标识(OrgOID)
            cmd.Parameters.Add(new SqlParameter("OrgOID",SqlDbType.BigInt){ Value = entity.OrgOID});
            //05:工资来源(paySource)
            var isNull = string.IsNullOrWhiteSpace(entity.paySource);
            var parameter = new SqlParameter("paySource",SqlDbType.NVarChar , isNull ? 10 : (entity.paySource).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.paySource;
            cmd.Parameters.Add(parameter);
            //06:应用标识(APPID)
            isNull = string.IsNullOrWhiteSpace(entity.APPID);
            parameter = new SqlParameter("APPID",SqlDbType.NVarChar , isNull ? 10 : (entity.APPID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.APPID;
            cmd.Parameters.Add(parameter);
            //07:阿普基(APPKEY)
            isNull = string.IsNullOrWhiteSpace(entity.APPKEY);
            parameter = new SqlParameter("APPKEY",SqlDbType.NVarChar , isNull ? 10 : (entity.APPKEY).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.APPKEY;
            cmd.Parameters.Add(parameter);
            //08:妇幼保健院(MCHID)
            isNull = string.IsNullOrWhiteSpace(entity.MCHID);
            parameter = new SqlParameter("MCHID",SqlDbType.NVarChar , isNull ? 10 : (entity.MCHID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.MCHID;
            cmd.Parameters.Add(parameter);
            //09:麦克奇(MCHKEY)
            isNull = string.IsNullOrWhiteSpace(entity.MCHKEY);
            parameter = new SqlParameter("MCHKEY",SqlDbType.NVarChar , isNull ? 10 : (entity.MCHKEY).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.MCHKEY;
            cmd.Parameters.Add(parameter);
            //10:服务器MCH(ServerMCH)
            isNull = string.IsNullOrWhiteSpace(entity.ServerMCH);
            parameter = new SqlParameter("ServerMCH",SqlDbType.NVarChar , isNull ? 10 : (entity.ServerMCH).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ServerMCH;
            cmd.Parameters.Add(parameter);
            //11:默认为(IsDefault)
            cmd.Parameters.Add(new SqlParameter("IsDefault",SqlDbType.Bit){ Value = entity.IsDefault});
            //12:评论(Remarks)
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
        protected sealed override void SetUpdateCommand(OrganizationHasPayInfoData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(OrganizationHasPayInfoData entity, SqlCommand cmd)
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
        /// 组织是否支付信息的结构语句
        /// </summary>
        private TableSql _tbOrganizationHasPayInfoSql = new TableSql
        {
            TableName = "tbOrganizationHasPayInfo",
            PimaryKey = "PID"
        };


        /// <summary>
        /// 组织是否支付信息数据访问对象
        /// </summary>
        private OrganizationHasPayInfoDataAccess _organizationHasPayInfoes;

        /// <summary>
        /// 组织是否支付信息数据访问对象
        /// </summary>
        public OrganizationHasPayInfoDataAccess OrganizationHasPayInfoes
        {
            get
            {
                return this._organizationHasPayInfoes ?? ( this._organizationHasPayInfoes = new OrganizationHasPayInfoDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 组织是否支付信息(tbOrganizationHasPayInfo):组织是否支付信息
        /// </summary>
        public const int Table_OrganizationHasPayInfo = 0x0;
    }
}