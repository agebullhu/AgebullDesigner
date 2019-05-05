/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:52*/
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
    /// 用户行为
    /// </summary>
    public partial class UserBehaviorDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_UserBehavior;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbUserBehavior";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbUserBehavior";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"BID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [BID] AS [BID],
    [BehaviorUID] AS [BehaviorUID],
    [BehaviorName] AS [BehaviorName],
    [BehaviorTime] AS [BehaviorTime],
    [MPResInfo] AS [MPResInfo],
    [YLResInfo] AS [YLResInfo],
    [AppLaunchPath] AS [AppLaunchPath],
    [AppLaunchQuery] AS [AppLaunchQuery],
    [AppLaunchQueryScene] AS [AppLaunchQueryScene],
    [AppLaunchScene] AS [AppLaunchScene],
    [AppLaunchShareTicket] AS [AppLaunchShareTicket],
    [AppLaunchReferrerInfoAppId] AS [AppLaunchReferrerInfoAppId],
    [AppLaunchReferrerInfoExtraData] AS [AppLaunchReferrerInfoExtraData]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [BID],
    [BehaviorUID],
    [BehaviorName],
    [BehaviorTime],
    [MPResInfo],
    [YLResInfo],
    [AppLaunchPath],
    [AppLaunchQuery],
    [AppLaunchQueryScene],
    [AppLaunchScene],
    [AppLaunchShareTicket],
    [AppLaunchReferrerInfoAppId],
    [AppLaunchReferrerInfoExtraData]
)
VALUES
(
    @BID,
    @BehaviorUID,
    @BehaviorName,
    @BehaviorTime,
    @MPResInfo,
    @YLResInfo,
    @AppLaunchPath,
    @AppLaunchQuery,
    @AppLaunchQueryScene,
    @AppLaunchScene,
    @AppLaunchShareTicket,
    @AppLaunchReferrerInfoAppId,
    @AppLaunchReferrerInfoExtraData
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [BID] = @BID,
       [BehaviorUID] = @BehaviorUID,
       [BehaviorName] = @BehaviorName,
       [BehaviorTime] = @BehaviorTime,
       [MPResInfo] = @MPResInfo,
       [YLResInfo] = @YLResInfo,
       [AppLaunchPath] = @AppLaunchPath,
       [AppLaunchQuery] = @AppLaunchQuery,
       [AppLaunchQueryScene] = @AppLaunchQueryScene,
       [AppLaunchScene] = @AppLaunchScene,
       [AppLaunchShareTicket] = @AppLaunchShareTicket,
       [AppLaunchReferrerInfoAppId] = @AppLaunchReferrerInfoAppId,
       [AppLaunchReferrerInfoExtraData] = @AppLaunchReferrerInfoExtraData
 WHERE [BID] = @BID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(UserBehaviorData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[UserBehaviorData._DataStruct_.Real_BID] > 0)
                sql.AppendLine("       [BID] = @BID");
            //behavior用户标识
            if (data.__EntityStatus.ModifiedProperties[UserBehaviorData._DataStruct_.Real_BehaviorUID] > 0)
                sql.AppendLine("       [BehaviorUID] = @BehaviorUID");
            //行为名称
            if (data.__EntityStatus.ModifiedProperties[UserBehaviorData._DataStruct_.Real_BehaviorName] > 0)
                sql.AppendLine("       [BehaviorName] = @BehaviorName");
            //行为时间
            if (data.__EntityStatus.ModifiedProperties[UserBehaviorData._DataStruct_.Real_BehaviorTime] > 0)
                sql.AppendLine("       [BehaviorTime] = @BehaviorTime");
            //MP-RES信息
            if (data.__EntityStatus.ModifiedProperties[UserBehaviorData._DataStruct_.Real_MPResInfo] > 0)
                sql.AppendLine("       [MPResInfo] = @MPResInfo");
            //伊尔莱斯信息
            if (data.__EntityStatus.ModifiedProperties[UserBehaviorData._DataStruct_.Real_YLResInfo] > 0)
                sql.AppendLine("       [YLResInfo] = @YLResInfo");
            //应用程序启动路径
            if (data.__EntityStatus.ModifiedProperties[UserBehaviorData._DataStruct_.Real_AppLaunchPath] > 0)
                sql.AppendLine("       [AppLaunchPath] = @AppLaunchPath");
            //应用程序启动查询
            if (data.__EntityStatus.ModifiedProperties[UserBehaviorData._DataStruct_.Real_AppLaunchQuery] > 0)
                sql.AppendLine("       [AppLaunchQuery] = @AppLaunchQuery");
            //应用程序启动查询场景
            if (data.__EntityStatus.ModifiedProperties[UserBehaviorData._DataStruct_.Real_AppLaunchQueryScene] > 0)
                sql.AppendLine("       [AppLaunchQueryScene] = @AppLaunchQueryScene");
            //应用程序启动场景
            if (data.__EntityStatus.ModifiedProperties[UserBehaviorData._DataStruct_.Real_AppLaunchScene] > 0)
                sql.AppendLine("       [AppLaunchScene] = @AppLaunchScene");
            //应用程序发布共享票
            if (data.__EntityStatus.ModifiedProperties[UserBehaviorData._DataStruct_.Real_AppLaunchShareTicket] > 0)
                sql.AppendLine("       [AppLaunchShareTicket] = @AppLaunchShareTicket");
            //applaunchrefererinfoapp标识
            if (data.__EntityStatus.ModifiedProperties[UserBehaviorData._DataStruct_.Real_AppLaunchReferrerInfoAppId] > 0)
                sql.AppendLine("       [AppLaunchReferrerInfoAppId] = @AppLaunchReferrerInfoAppId");
            //应用程序启动引用信息额外数据
            if (data.__EntityStatus.ModifiedProperties[UserBehaviorData._DataStruct_.Real_AppLaunchReferrerInfoExtraData] > 0)
                sql.AppendLine("       [AppLaunchReferrerInfoExtraData] = @AppLaunchReferrerInfoExtraData");
            sql.Append(" WHERE [BID] = @BID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "BID","BehaviorUID","BehaviorName","BehaviorTime","MPResInfo","YLResInfo","AppLaunchPath","AppLaunchQuery","AppLaunchQueryScene","AppLaunchScene","AppLaunchShareTicket","AppLaunchReferrerInfoAppId","AppLaunchReferrerInfoExtraData" };

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
            { "BID" , "BID" },
            { "BehaviorUID" , "BehaviorUID" },
            { "BehaviorName" , "BehaviorName" },
            { "BehaviorTime" , "BehaviorTime" },
            { "MPResInfo" , "MPResInfo" },
            { "YLResInfo" , "YLResInfo" },
            { "AppLaunchPath" , "AppLaunchPath" },
            { "AppLaunchQuery" , "AppLaunchQuery" },
            { "AppLaunchQueryScene" , "AppLaunchQueryScene" },
            { "AppLaunchScene" , "AppLaunchScene" },
            { "AppLaunchShareTicket" , "AppLaunchShareTicket" },
            { "AppLaunchReferrerInfoAppId" , "AppLaunchReferrerInfoAppId" },
            { "AppLaunchReferrerInfoExtraData" , "AppLaunchReferrerInfoExtraData" },
            { "Id" , "BID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,UserBehaviorData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._bID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._behaviorUID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._behaviorName = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._behaviorTime = reader.GetDateTime(3);
                if (!reader.IsDBNull(4))
                    entity._mPResInfo = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._yLResInfo = reader.GetString(5);
                if (!reader.IsDBNull(6))
                    entity._appLaunchPath = reader.GetString(6);
                if (!reader.IsDBNull(7))
                    entity._appLaunchQuery = reader.GetString(7);
                if (!reader.IsDBNull(8))
                    entity._appLaunchQueryScene = reader.GetString(8);
                if (!reader.IsDBNull(9))
                    entity._appLaunchScene = reader.GetString(9);
                if (!reader.IsDBNull(10))
                    entity._appLaunchShareTicket = reader.GetString(10);
                if (!reader.IsDBNull(11))
                    entity._appLaunchReferrerInfoAppId = reader.GetString(11);
                if (!reader.IsDBNull(12))
                    entity._appLaunchReferrerInfoExtraData = reader.GetString(12);
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
                case "BID":
                    return SqlDbType.BigInt;
                case "BehaviorUID":
                    return SqlDbType.BigInt;
                case "BehaviorName":
                    return SqlDbType.NVarChar;
                case "BehaviorTime":
                    return SqlDbType.DateTime;
                case "MPResInfo":
                    return SqlDbType.NVarChar;
                case "YLResInfo":
                    return SqlDbType.NVarChar;
                case "AppLaunchPath":
                    return SqlDbType.NVarChar;
                case "AppLaunchQuery":
                    return SqlDbType.NVarChar;
                case "AppLaunchQueryScene":
                    return SqlDbType.NVarChar;
                case "AppLaunchScene":
                    return SqlDbType.NVarChar;
                case "AppLaunchShareTicket":
                    return SqlDbType.NVarChar;
                case "AppLaunchReferrerInfoAppId":
                    return SqlDbType.NVarChar;
                case "AppLaunchReferrerInfoExtraData":
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
        public void CreateFullSqlParameter(UserBehaviorData entity, SqlCommand cmd)
        {
            //02:主键(BID)
            cmd.Parameters.Add(new SqlParameter("BID",SqlDbType.BigInt){ Value = entity.BID});
            //03:behavior用户标识(BehaviorUID)
            cmd.Parameters.Add(new SqlParameter("BehaviorUID",SqlDbType.BigInt){ Value = entity.BehaviorUID});
            //04:行为名称(BehaviorName)
            var isNull = string.IsNullOrWhiteSpace(entity.BehaviorName);
            var parameter = new SqlParameter("BehaviorName",SqlDbType.NVarChar , isNull ? 10 : (entity.BehaviorName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.BehaviorName;
            cmd.Parameters.Add(parameter);
            //05:行为时间(BehaviorTime)
            isNull = entity.BehaviorTime.Year < 1900;
            parameter = new SqlParameter("BehaviorTime",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.BehaviorTime;
            cmd.Parameters.Add(parameter);
            //06:MP-RES信息(MPResInfo)
            isNull = string.IsNullOrWhiteSpace(entity.MPResInfo);
            parameter = new SqlParameter("MPResInfo",SqlDbType.NVarChar , isNull ? 10 : (entity.MPResInfo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.MPResInfo;
            cmd.Parameters.Add(parameter);
            //07:伊尔莱斯信息(YLResInfo)
            isNull = string.IsNullOrWhiteSpace(entity.YLResInfo);
            parameter = new SqlParameter("YLResInfo",SqlDbType.NVarChar , isNull ? 10 : (entity.YLResInfo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.YLResInfo;
            cmd.Parameters.Add(parameter);
            //08:应用程序启动路径(AppLaunchPath)
            isNull = string.IsNullOrWhiteSpace(entity.AppLaunchPath);
            parameter = new SqlParameter("AppLaunchPath",SqlDbType.NVarChar , isNull ? 10 : (entity.AppLaunchPath).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AppLaunchPath;
            cmd.Parameters.Add(parameter);
            //09:应用程序启动查询(AppLaunchQuery)
            isNull = string.IsNullOrWhiteSpace(entity.AppLaunchQuery);
            parameter = new SqlParameter("AppLaunchQuery",SqlDbType.NVarChar , isNull ? 10 : (entity.AppLaunchQuery).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AppLaunchQuery;
            cmd.Parameters.Add(parameter);
            //10:应用程序启动查询场景(AppLaunchQueryScene)
            isNull = string.IsNullOrWhiteSpace(entity.AppLaunchQueryScene);
            parameter = new SqlParameter("AppLaunchQueryScene",SqlDbType.NVarChar , isNull ? 10 : (entity.AppLaunchQueryScene).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AppLaunchQueryScene;
            cmd.Parameters.Add(parameter);
            //11:应用程序启动场景(AppLaunchScene)
            isNull = string.IsNullOrWhiteSpace(entity.AppLaunchScene);
            parameter = new SqlParameter("AppLaunchScene",SqlDbType.NVarChar , isNull ? 10 : (entity.AppLaunchScene).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AppLaunchScene;
            cmd.Parameters.Add(parameter);
            //12:应用程序发布共享票(AppLaunchShareTicket)
            isNull = string.IsNullOrWhiteSpace(entity.AppLaunchShareTicket);
            parameter = new SqlParameter("AppLaunchShareTicket",SqlDbType.NVarChar , isNull ? 10 : (entity.AppLaunchShareTicket).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AppLaunchShareTicket;
            cmd.Parameters.Add(parameter);
            //13:applaunchrefererinfoapp标识(AppLaunchReferrerInfoAppId)
            isNull = string.IsNullOrWhiteSpace(entity.AppLaunchReferrerInfoAppId);
            parameter = new SqlParameter("AppLaunchReferrerInfoAppId",SqlDbType.NVarChar , isNull ? 10 : (entity.AppLaunchReferrerInfoAppId).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AppLaunchReferrerInfoAppId;
            cmd.Parameters.Add(parameter);
            //14:应用程序启动引用信息额外数据(AppLaunchReferrerInfoExtraData)
            isNull = string.IsNullOrWhiteSpace(entity.AppLaunchReferrerInfoExtraData);
            parameter = new SqlParameter("AppLaunchReferrerInfoExtraData",SqlDbType.NVarChar , isNull ? 10 : (entity.AppLaunchReferrerInfoExtraData).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.AppLaunchReferrerInfoExtraData;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(UserBehaviorData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(UserBehaviorData entity, SqlCommand cmd)
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
        /// 用户行为的结构语句
        /// </summary>
        private TableSql _tbUserBehaviorSql = new TableSql
        {
            TableName = "tbUserBehavior",
            PimaryKey = "BID"
        };


        /// <summary>
        /// 用户行为数据访问对象
        /// </summary>
        private UserBehaviorDataAccess _userBehaviors;

        /// <summary>
        /// 用户行为数据访问对象
        /// </summary>
        public UserBehaviorDataAccess UserBehaviors
        {
            get
            {
                return this._userBehaviors ?? ( this._userBehaviors = new UserBehaviorDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 用户行为(tbUserBehavior):用户行为
        /// </summary>
        public const int Table_UserBehavior = 0x0;
    }
}