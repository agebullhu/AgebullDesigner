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
    /// 用户离线访问
    /// </summary>
    public partial class UserVisitOfflineDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_UserVisitOffline;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbUserVisitOffline";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbUserVisitOffline";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"VID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [VID] AS [VID],
    [SiteSID] AS [SiteSID],
    [OrgOID] AS [OrgOID],
    [DeviceDID] AS [DeviceDID],
    [UserUID] AS [UserUID],
    [TimeIn] AS [TimeIn],
    [TimeOut] AS [TimeOut],
    [Direction] AS [Direction]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [VID],
    [SiteSID],
    [OrgOID],
    [DeviceDID],
    [UserUID],
    [TimeIn],
    [TimeOut],
    [Direction]
)
VALUES
(
    @VID,
    @SiteSID,
    @OrgOID,
    @DeviceDID,
    @UserUID,
    @TimeIn,
    @TimeOut,
    @Direction
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [VID] = @VID,
       [SiteSID] = @SiteSID,
       [OrgOID] = @OrgOID,
       [DeviceDID] = @DeviceDID,
       [UserUID] = @UserUID,
       [TimeIn] = @TimeIn,
       [TimeOut] = @TimeOut,
       [Direction] = @Direction
 WHERE [VID] = @VID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(UserVisitOfflineData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[UserVisitOfflineData._DataStruct_.Real_VID] > 0)
                sql.AppendLine("       [VID] = @VID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[UserVisitOfflineData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[UserVisitOfflineData._DataStruct_.Real_OrgOID] > 0)
                sql.AppendLine("       [OrgOID] = @OrgOID");
            //设备确实
            if (data.__EntityStatus.ModifiedProperties[UserVisitOfflineData._DataStruct_.Real_DeviceDID] > 0)
                sql.AppendLine("       [DeviceDID] = @DeviceDID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[UserVisitOfflineData._DataStruct_.Real_UserUID] > 0)
                sql.AppendLine("       [UserUID] = @UserUID");
            //时间在
            if (data.__EntityStatus.ModifiedProperties[UserVisitOfflineData._DataStruct_.Real_TimeIn] > 0)
                sql.AppendLine("       [TimeIn] = @TimeIn");
            //时间到
            if (data.__EntityStatus.ModifiedProperties[UserVisitOfflineData._DataStruct_.Real_TimeOut] > 0)
                sql.AppendLine("       [TimeOut] = @TimeOut");
            //方向
            if (data.__EntityStatus.ModifiedProperties[UserVisitOfflineData._DataStruct_.Real_Direction] > 0)
                sql.AppendLine("       [Direction] = @Direction");
            sql.Append(" WHERE [VID] = @VID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "VID","SiteSID","OrgOID","DeviceDID","UserUID","TimeIn","TimeOut","Direction" };

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
            { "VID" , "VID" },
            { "SiteSID" , "SiteSID" },
            { "OrgOID" , "OrgOID" },
            { "DeviceDID" , "DeviceDID" },
            { "UserUID" , "UserUID" },
            { "TimeIn" , "TimeIn" },
            { "TimeOut" , "TimeOut" },
            { "Direction" , "Direction" },
            { "Id" , "VID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,UserVisitOfflineData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._vID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._orgOID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._deviceDID = (long)reader.GetInt64(3);
                if (!reader.IsDBNull(4))
                    entity._userUID = (long)reader.GetInt64(4);
                if (!reader.IsDBNull(5))
                    entity._timeIn = reader.GetDateTime(5);
                if (!reader.IsDBNull(6))
                    entity._timeOut = reader.GetDateTime(6);
                if (!reader.IsDBNull(7))
                    entity._direction = reader.GetString(7);
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
                case "VID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "OrgOID":
                    return SqlDbType.BigInt;
                case "DeviceDID":
                    return SqlDbType.BigInt;
                case "UserUID":
                    return SqlDbType.BigInt;
                case "TimeIn":
                    return SqlDbType.DateTime;
                case "TimeOut":
                    return SqlDbType.DateTime;
                case "Direction":
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
        public void CreateFullSqlParameter(UserVisitOfflineData entity, SqlCommand cmd)
        {
            //02:主键(VID)
            cmd.Parameters.Add(new SqlParameter("VID",SqlDbType.BigInt){ Value = entity.VID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:组织标识(OrgOID)
            cmd.Parameters.Add(new SqlParameter("OrgOID",SqlDbType.BigInt){ Value = entity.OrgOID});
            //05:设备确实(DeviceDID)
            cmd.Parameters.Add(new SqlParameter("DeviceDID",SqlDbType.BigInt){ Value = entity.DeviceDID});
            //06:组织标识(UserUID)
            cmd.Parameters.Add(new SqlParameter("UserUID",SqlDbType.BigInt){ Value = entity.UserUID});
            //07:时间在(TimeIn)
            var isNull = entity.TimeIn.Year < 1900;
            var parameter = new SqlParameter("TimeIn",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.TimeIn;
            cmd.Parameters.Add(parameter);
            //08:时间到(TimeOut)
            isNull = entity.TimeOut.Year < 1900;
            parameter = new SqlParameter("TimeOut",SqlDbType.DateTime);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.TimeOut;
            cmd.Parameters.Add(parameter);
            //09:方向(Direction)
            isNull = string.IsNullOrWhiteSpace(entity.Direction);
            parameter = new SqlParameter("Direction",SqlDbType.NVarChar , isNull ? 10 : (entity.Direction).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Direction;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(UserVisitOfflineData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(UserVisitOfflineData entity, SqlCommand cmd)
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
        /// 用户离线访问的结构语句
        /// </summary>
        private TableSql _tbUserVisitOfflineSql = new TableSql
        {
            TableName = "tbUserVisitOffline",
            PimaryKey = "VID"
        };


        /// <summary>
        /// 用户离线访问数据访问对象
        /// </summary>
        private UserVisitOfflineDataAccess _userVisitOfflines;

        /// <summary>
        /// 用户离线访问数据访问对象
        /// </summary>
        public UserVisitOfflineDataAccess UserVisitOfflines
        {
            get
            {
                return this._userVisitOfflines ?? ( this._userVisitOfflines = new UserVisitOfflineDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 用户离线访问(tbUserVisitOffline):用户离线访问
        /// </summary>
        public const int Table_UserVisitOffline = 0x0;
    }
}