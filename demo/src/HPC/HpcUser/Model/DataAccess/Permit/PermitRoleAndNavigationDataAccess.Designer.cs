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
    /// 角色导航关联
    /// </summary>
    public partial class PermitRoleAndNavigationDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_PermitRoleAndNavigation;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbPermitRoleAndNavigation";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbPermitRoleAndNavigation";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"RNID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [RNID] AS [RNID],
    [SiteSID] AS [SiteSID],
    [RoleRID] AS [RoleRID],
    [NavigationSubSID] AS [NavigationSubSID]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [RNID],
    [SiteSID],
    [RoleRID],
    [NavigationSubSID]
)
VALUES
(
    @RNID,
    @SiteSID,
    @RoleRID,
    @NavigationSubSID
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [RNID] = @RNID,
       [SiteSID] = @SiteSID,
       [RoleRID] = @RoleRID,
       [NavigationSubSID] = @NavigationSubSID
 WHERE [RNID] = @RNID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(PermitRoleAndNavigationData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[PermitRoleAndNavigationData._DataStruct_.Real_RNID] > 0)
                sql.AppendLine("       [RNID] = @RNID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[PermitRoleAndNavigationData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //角色扮演
            if (data.__EntityStatus.ModifiedProperties[PermitRoleAndNavigationData._DataStruct_.Real_RoleRID] > 0)
                sql.AppendLine("       [RoleRID] = @RoleRID");
            //导航关联标识
            if (data.__EntityStatus.ModifiedProperties[PermitRoleAndNavigationData._DataStruct_.Real_NavigationSubSID] > 0)
                sql.AppendLine("       [NavigationSubSID] = @NavigationSubSID");
            sql.Append(" WHERE [RNID] = @RNID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "RNID","SiteSID","RoleRID","NavigationSubSID" };

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
            { "RNID" , "RNID" },
            { "SiteSID" , "SiteSID" },
            { "RoleRID" , "RoleRID" },
            { "NavigationSubSID" , "NavigationSubSID" },
            { "Id" , "RNID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,PermitRoleAndNavigationData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._rNID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._roleRID = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._navigationSubSID = (long)reader.GetInt64(3);
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
                case "RNID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "RoleRID":
                    return SqlDbType.BigInt;
                case "NavigationSubSID":
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
        public void CreateFullSqlParameter(PermitRoleAndNavigationData entity, SqlCommand cmd)
        {
            //02:主键(RNID)
            cmd.Parameters.Add(new SqlParameter("RNID",SqlDbType.BigInt){ Value = entity.RNID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:角色扮演(RoleRID)
            cmd.Parameters.Add(new SqlParameter("RoleRID",SqlDbType.BigInt){ Value = entity.RoleRID});
            //05:导航关联标识(NavigationSubSID)
            cmd.Parameters.Add(new SqlParameter("NavigationSubSID",SqlDbType.BigInt){ Value = entity.NavigationSubSID});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(PermitRoleAndNavigationData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(PermitRoleAndNavigationData entity, SqlCommand cmd)
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
        /// 角色导航关联的结构语句
        /// </summary>
        private TableSql _tbPermitRoleAndNavigationSql = new TableSql
        {
            TableName = "tbPermitRoleAndNavigation",
            PimaryKey = "RNID"
        };


        /// <summary>
        /// 角色导航关联数据访问对象
        /// </summary>
        private PermitRoleAndNavigationDataAccess _permitRoleAndNavigations;

        /// <summary>
        /// 角色导航关联数据访问对象
        /// </summary>
        public PermitRoleAndNavigationDataAccess PermitRoleAndNavigations
        {
            get
            {
                return this._permitRoleAndNavigations ?? ( this._permitRoleAndNavigations = new PermitRoleAndNavigationDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 角色导航关联(tbPermitRoleAndNavigation):角色导航关联
        /// </summary>
        public const int Table_PermitRoleAndNavigation = 0x0;
    }
}