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
    /// 用户日志
    /// </summary>
    public partial class UserOrderLogDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_UserOrderLog;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbUserOrderLog";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbUserOrderLog";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"LID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [LID] AS [LID],
    [OrderID] AS [OrderID],
    [LogTime] AS [LogTime],
    [LogState] AS [LogState],
    [LogRelevantID] AS [LogRelevantID],
    [LogRemarks] AS [LogRemarks]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [OrderID],
    [LogTime],
    [LogState],
    [LogRelevantID],
    [LogRemarks]
)
VALUES
(
    @OrderID,
    @LogTime,
    @LogState,
    @LogRelevantID,
    @LogRemarks
);
SELECT SCOPE_IDENTITY();";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [OrderID] = @OrderID,
       [LogTime] = @LogTime,
       [LogState] = @LogState,
       [LogRelevantID] = @LogRelevantID,
       [LogRemarks] = @LogRemarks
 WHERE [LID] = @LID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(UserOrderLogData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //订单标识
            if (data.__EntityStatus.ModifiedProperties[UserOrderLogData._DataStruct_.Real_OrderID] > 0)
                sql.AppendLine("       [OrderID] = @OrderID");
            //日志时间
            if (data.__EntityStatus.ModifiedProperties[UserOrderLogData._DataStruct_.Real_LogTime] > 0)
                sql.AppendLine("       [LogTime] = @LogTime");
            //日志状态
            if (data.__EntityStatus.ModifiedProperties[UserOrderLogData._DataStruct_.Real_LogState] > 0)
                sql.AppendLine("       [LogState] = @LogState");
            //日志相关标识
            if (data.__EntityStatus.ModifiedProperties[UserOrderLogData._DataStruct_.Real_LogRelevantID] > 0)
                sql.AppendLine("       [LogRelevantID] = @LogRelevantID");
            //日志注释
            if (data.__EntityStatus.ModifiedProperties[UserOrderLogData._DataStruct_.Real_LogRemarks] > 0)
                sql.AppendLine("       [LogRemarks] = @LogRemarks");
            sql.Append(" WHERE [LID] = @LID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "LID","OrderID","LogTime","LogState","LogRelevantID","LogRemarks" };

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
            { "LID" , "LID" },
            { "OrderID" , "OrderID" },
            { "LogTime" , "LogTime" },
            { "LogState" , "LogState" },
            { "LogRelevantID" , "LogRelevantID" },
            { "LogRemarks" , "LogRemarks" },
            { "Id" , "LID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,UserOrderLogData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._lID = reader.GetInt32(0);
                if (!reader.IsDBNull(1))
                    entity._orderID = reader.GetString(1).ToString();
                if (!reader.IsDBNull(2))
                    entity._logTime = reader.GetDateTime(2);
                if (!reader.IsDBNull(3))
                    entity._logState = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._logRelevantID = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._logRemarks = reader.GetString(5);
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
                case "LID":
                    return SqlDbType.Int;
                case "OrderID":
                    return SqlDbType.NVarChar;
                case "LogTime":
                    return SqlDbType.DateTime;
                case "LogState":
                    return SqlDbType.NVarChar;
                case "LogRelevantID":
                    return SqlDbType.NVarChar;
                case "LogRemarks":
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
        public void CreateFullSqlParameter(UserOrderLogData entity, SqlCommand cmd)
        {
            //02:主键(LID)
            cmd.Parameters.Add(new SqlParameter("LID",SqlDbType.Int){ Value = entity.LID});
            //03:订单标识(OrderID)
            var isNull = string.IsNullOrWhiteSpace(entity.OrderID);
            var parameter = new SqlParameter("OrderID",SqlDbType.NVarChar , isNull ? 10 : (entity.OrderID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.OrderID;
            cmd.Parameters.Add(parameter);
            //04:日志时间(LogTime)
            isNull = entity.LogTime.Year < 1900;
            parameter = new SqlParameter("LogTime",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LogTime;
            cmd.Parameters.Add(parameter);
            //05:日志状态(LogState)
            isNull = string.IsNullOrWhiteSpace(entity.LogState);
            parameter = new SqlParameter("LogState",SqlDbType.NVarChar , isNull ? 10 : (entity.LogState).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LogState;
            cmd.Parameters.Add(parameter);
            //06:日志相关标识(LogRelevantID)
            isNull = string.IsNullOrWhiteSpace(entity.LogRelevantID);
            parameter = new SqlParameter("LogRelevantID",SqlDbType.NVarChar , isNull ? 10 : (entity.LogRelevantID).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LogRelevantID;
            cmd.Parameters.Add(parameter);
            //07:日志注释(LogRemarks)
            isNull = string.IsNullOrWhiteSpace(entity.LogRemarks);
            parameter = new SqlParameter("LogRemarks",SqlDbType.NVarChar , isNull ? 10 : (entity.LogRemarks).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.LogRemarks;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(UserOrderLogData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(UserOrderLogData entity, SqlCommand cmd)
        {
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return true;
        }
        #endregion

    }

    sealed partial class HpcSqlServerDb
    {


        /// <summary>
        /// 用户日志的结构语句
        /// </summary>
        private TableSql _tbUserOrderLogSql = new TableSql
        {
            TableName = "tbUserOrderLog",
            PimaryKey = "LID"
        };


        /// <summary>
        /// 用户日志数据访问对象
        /// </summary>
        private UserOrderLogDataAccess _userOrderLogs;

        /// <summary>
        /// 用户日志数据访问对象
        /// </summary>
        public UserOrderLogDataAccess UserOrderLogs
        {
            get
            {
                return this._userOrderLogs ?? ( this._userOrderLogs = new UserOrderLogDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 用户日志(tbUserOrderLog):用户日志
        /// </summary>
        public const int Table_UserOrderLog = 0x0;
    }
}