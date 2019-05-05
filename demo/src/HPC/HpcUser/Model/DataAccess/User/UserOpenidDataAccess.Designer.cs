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
    /// 用户OpenID
    /// </summary>
    public partial class UserOpenidDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_UserOpenid;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbUserOpenid";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbUserOpenid";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"OID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [OID] AS [OID],
    [UserUID] AS [UserUID],
    [WxAppid] AS [WxAppid],
    [WxOpenid] AS [WxOpenid]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [OID],
    [UserUID],
    [WxAppid],
    [WxOpenid]
)
VALUES
(
    @OID,
    @UserUID,
    @WxAppid,
    @WxOpenid
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [OID] = @OID,
       [UserUID] = @UserUID,
       [WxAppid] = @WxAppid,
       [WxOpenid] = @WxOpenid
 WHERE [OID] = @OID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(UserOpenidData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[UserOpenidData._DataStruct_.Real_OID] > 0)
                sql.AppendLine("       [OID] = @OID");
            //组织标识
            if (data.__EntityStatus.ModifiedProperties[UserOpenidData._DataStruct_.Real_UserUID] > 0)
                sql.AppendLine("       [UserUID] = @UserUID");
            //Wx应用标识
            if (data.__EntityStatus.ModifiedProperties[UserOpenidData._DataStruct_.Real_WxAppid] > 0)
                sql.AppendLine("       [WxAppid] = @WxAppid");
            //WXOpenID
            if (data.__EntityStatus.ModifiedProperties[UserOpenidData._DataStruct_.Real_WxOpenid] > 0)
                sql.AppendLine("       [WxOpenid] = @WxOpenid");
            sql.Append(" WHERE [OID] = @OID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "OID","UserUID","WxAppid","WxOpenid" };

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
            { "OID" , "OID" },
            { "UserUID" , "UserUID" },
            { "WxAppid" , "WxAppid" },
            { "WxOpenid" , "WxOpenid" },
            { "Id" , "OID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,UserOpenidData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._oID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._userUID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._wxAppid = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._wxOpenid = reader.GetString(3);
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
                case "OID":
                    return SqlDbType.BigInt;
                case "UserUID":
                    return SqlDbType.BigInt;
                case "WxAppid":
                    return SqlDbType.NVarChar;
                case "WxOpenid":
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
        public void CreateFullSqlParameter(UserOpenidData entity, SqlCommand cmd)
        {
            //02:主键(OID)
            cmd.Parameters.Add(new SqlParameter("OID",SqlDbType.BigInt){ Value = entity.OID});
            //03:组织标识(UserUID)
            cmd.Parameters.Add(new SqlParameter("UserUID",SqlDbType.BigInt){ Value = entity.UserUID});
            //04:Wx应用标识(WxAppid)
            var isNull = string.IsNullOrWhiteSpace(entity.WxAppid);
            var parameter = new SqlParameter("WxAppid",SqlDbType.NVarChar , isNull ? 10 : (entity.WxAppid).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.WxAppid;
            cmd.Parameters.Add(parameter);
            //05:WXOpenID(WxOpenid)
            isNull = string.IsNullOrWhiteSpace(entity.WxOpenid);
            parameter = new SqlParameter("WxOpenid",SqlDbType.NVarChar , isNull ? 10 : (entity.WxOpenid).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.WxOpenid;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(UserOpenidData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(UserOpenidData entity, SqlCommand cmd)
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
        /// 用户OpenID的结构语句
        /// </summary>
        private TableSql _tbUserOpenidSql = new TableSql
        {
            TableName = "tbUserOpenid",
            PimaryKey = "OID"
        };


        /// <summary>
        /// 用户OpenID数据访问对象
        /// </summary>
        private UserOpenidDataAccess _userOpenids;

        /// <summary>
        /// 用户OpenID数据访问对象
        /// </summary>
        public UserOpenidDataAccess UserOpenids
        {
            get
            {
                return this._userOpenids ?? ( this._userOpenids = new UserOpenidDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 用户OpenID(tbUserOpenid):用户OpenID
        /// </summary>
        public const int Table_UserOpenid = 0x0;
    }
}