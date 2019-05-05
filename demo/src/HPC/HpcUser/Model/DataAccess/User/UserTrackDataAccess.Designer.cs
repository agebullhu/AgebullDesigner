/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/10 10:44:53*/
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
    /// 用户跟踪
    /// </summary>
    public partial class UserTrackDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_UserTrack;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbUserTrack";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbUserTrack";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"TID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [TID] AS [TID],
    [UserUID] AS [UserUID],
    [positionTime] AS [positionTime],
    [position_latitude] AS [position_latitude],
    [position_longitude] AS [position_longitude]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [TID],
    [UserUID],
    [positionTime],
    [position_latitude],
    [position_longitude]
)
VALUES
(
    @TID,
    @UserUID,
    @positionTime,
    @position_latitude,
    @position_longitude
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [TID] = @TID,
       [UserUID] = @UserUID,
       [positionTime] = @positionTime,
       [position_latitude] = @position_latitude,
       [position_longitude] = @position_longitude
 WHERE [TID] = @TID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(UserTrackData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[UserTrackData._DataStruct_.Real_TID] > 0)
                sql.AppendLine("       [TID] = @TID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[UserTrackData._DataStruct_.Real_UserUID] > 0)
                sql.AppendLine("       [UserUID] = @UserUID");
            //定位时间
            if (data.__EntityStatus.ModifiedProperties[UserTrackData._DataStruct_.Real_positionTime] > 0)
                sql.AppendLine("       [positionTime] = @positionTime");
            //纬度位置
            if (data.__EntityStatus.ModifiedProperties[UserTrackData._DataStruct_.Real_position_latitude] > 0)
                sql.AppendLine("       [position_latitude] = @position_latitude");
            //位置经度
            if (data.__EntityStatus.ModifiedProperties[UserTrackData._DataStruct_.Real_position_longitude] > 0)
                sql.AppendLine("       [position_longitude] = @position_longitude");
            sql.Append(" WHERE [TID] = @TID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "TID","UserUID","positionTime","position_latitude","position_longitude" };

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
            { "TID" , "TID" },
            { "UserUID" , "UserUID" },
            { "positionTime" , "positionTime" },
            { "position_latitude" , "position_latitude" },
            { "position_longitude" , "position_longitude" },
            { "Id" , "TID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,UserTrackData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._tID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._userUID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._positionTime = reader.GetDateTime(2);
                if (!reader.IsDBNull(3))
                    entity._position_latitude = (float)reader.GetDouble(3);
                if (!reader.IsDBNull(4))
                    entity._position_longitude = (float)reader.GetDouble(4);
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
                case "TID":
                    return SqlDbType.BigInt;
                case "UserUID":
                    return SqlDbType.BigInt;
                case "positionTime":
                    return SqlDbType.DateTime;
                case "position_latitude":
                    return SqlDbType.Decimal;
                case "position_longitude":
                    return SqlDbType.Decimal;
            }
            return SqlDbType.NVarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void CreateFullSqlParameter(UserTrackData entity, SqlCommand cmd)
        {
            //02:主键(TID)
            cmd.Parameters.Add(new SqlParameter("TID",SqlDbType.BigInt){ Value = entity.TID});
            //03:组织标识(UserUID)
            cmd.Parameters.Add(new SqlParameter("UserUID",SqlDbType.BigInt){ Value = entity.UserUID});
            //04:定位时间(positionTime)
            var isNull = entity.positionTime.Year < 1900;
            var parameter = new SqlParameter("positionTime",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.positionTime;
            cmd.Parameters.Add(parameter);
            //05:纬度位置(position_latitude)
            cmd.Parameters.Add(new SqlParameter("position_latitude",SqlDbType.Decimal){ Value = entity.position_latitude});
            //06:位置经度(position_longitude)
            cmd.Parameters.Add(new SqlParameter("position_longitude",SqlDbType.Decimal){ Value = entity.position_longitude});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(UserTrackData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(UserTrackData entity, SqlCommand cmd)
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
        /// 用户跟踪的结构语句
        /// </summary>
        private TableSql _tbUserTrackSql = new TableSql
        {
            TableName = "tbUserTrack",
            PimaryKey = "TID"
        };


        /// <summary>
        /// 用户跟踪数据访问对象
        /// </summary>
        private UserTrackDataAccess _userTracks;

        /// <summary>
        /// 用户跟踪数据访问对象
        /// </summary>
        public UserTrackDataAccess UserTracks
        {
            get
            {
                return this._userTracks ?? ( this._userTracks = new UserTrackDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 用户跟踪(tbUserTrack):用户跟踪
        /// </summary>
        public const int Table_UserTrack = 0x0;
    }
}