/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/12 21:30:55*/
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
    /// 导航关联
    /// </summary>
    public partial class PermitNavigationSubDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_PermitNavigationSub;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbPermitNavigationSub";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbPermitNavigationSub";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"SID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [SID] AS [SID],
    [SiteSID] AS [SiteSID],
    [NavigationNID] AS [NavigationNID]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [SID],
    [SiteSID],
    [NavigationNID]
)
VALUES
(
    @SID,
    @SiteSID,
    @NavigationNID
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [SID] = @SID,
       [SiteSID] = @SiteSID,
       [NavigationNID] = @NavigationNID
 WHERE [SID] = @SID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(PermitNavigationSubData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationSubData._DataStruct_.Real_SID] > 0)
                sql.AppendLine("       [SID] = @SID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationSubData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //导航标识
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationSubData._DataStruct_.Real_NavigationNID] > 0)
                sql.AppendLine("       [NavigationNID] = @NavigationNID");
            sql.Append(" WHERE [SID] = @SID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "SID","SiteSID","NavigationNID" };

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
            { "SiteSID" , "SiteSID" },
            { "NavigationNID" , "NavigationNID" },
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
        protected sealed override void LoadEntity(SqlDataReader reader,PermitNavigationSubData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._sID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._navigationNID = (long)reader.GetInt64(2);
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
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "NavigationNID":
                    return SqlDbType.BigInt;
            }
            return SqlDbType.NVarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void CreateFullSqlParameter(PermitNavigationSubData entity, SqlCommand cmd)
        {
            //02:主键(SID)
            cmd.Parameters.Add(new SqlParameter("SID",SqlDbType.BigInt){ Value = entity.SID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:导航标识(NavigationNID)
            cmd.Parameters.Add(new SqlParameter("NavigationNID",SqlDbType.BigInt){ Value = entity.NavigationNID});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(PermitNavigationSubData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(PermitNavigationSubData entity, SqlCommand cmd)
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
        /// 导航关联的结构语句
        /// </summary>
        private TableSql _tbPermitNavigationSubSql = new TableSql
        {
            TableName = "tbPermitNavigationSub",
            PimaryKey = "SID"
        };


        /// <summary>
        /// 导航关联数据访问对象
        /// </summary>
        private PermitNavigationSubDataAccess _permitNavigationSubs;

        /// <summary>
        /// 导航关联数据访问对象
        /// </summary>
        public PermitNavigationSubDataAccess PermitNavigationSubs
        {
            get
            {
                return this._permitNavigationSubs ?? ( this._permitNavigationSubs = new PermitNavigationSubDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 导航关联(tbPermitNavigationSub):导航关联
        /// </summary>
        public const int Table_PermitNavigationSub = 0x0;
    }
}