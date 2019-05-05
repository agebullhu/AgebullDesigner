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
    /// 许可权
    /// </summary>
    public partial class PermitRightDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_PermitRight;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbPermitRight";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbPermitRight";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"RID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [RID] AS [RID],
    [NavigationNID] AS [NavigationNID],
    [RightName] AS [RightName],
    [InterFaceName] AS [InterFaceName],
    [Remark] AS [Remark]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [RID],
    [NavigationNID],
    [RightName],
    [InterFaceName],
    [Remark]
)
VALUES
(
    @RID,
    @NavigationNID,
    @RightName,
    @InterFaceName,
    @Remark
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [RID] = @RID,
       [NavigationNID] = @NavigationNID,
       [RightName] = @RightName,
       [InterFaceName] = @InterFaceName,
       [Remark] = @Remark
 WHERE [RID] = @RID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(PermitRightData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[PermitRightData._DataStruct_.Real_RID] > 0)
                sql.AppendLine("       [RID] = @RID");
            //导航标识
            if (data.__EntityStatus.ModifiedProperties[PermitRightData._DataStruct_.Real_NavigationNID] > 0)
                sql.AppendLine("       [NavigationNID] = @NavigationNID");
            //正确名称
            if (data.__EntityStatus.ModifiedProperties[PermitRightData._DataStruct_.Real_RightName] > 0)
                sql.AppendLine("       [RightName] = @RightName");
            //面间名称
            if (data.__EntityStatus.ModifiedProperties[PermitRightData._DataStruct_.Real_InterFaceName] > 0)
                sql.AppendLine("       [InterFaceName] = @InterFaceName");
            //备注
            if (data.__EntityStatus.ModifiedProperties[PermitRightData._DataStruct_.Real_Remark] > 0)
                sql.AppendLine("       [Remark] = @Remark");
            sql.Append(" WHERE [RID] = @RID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "RID","NavigationNID","RightName","InterFaceName","Remark" };

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
            { "RID" , "RID" },
            { "NavigationNID" , "NavigationNID" },
            { "RightName" , "RightName" },
            { "InterFaceName" , "InterFaceName" },
            { "Remark" , "Remark" },
            { "Id" , "RID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,PermitRightData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._rID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._navigationNID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._rightName = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._interFaceName = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._remark = reader.GetString(4);
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
                case "RID":
                    return SqlDbType.BigInt;
                case "NavigationNID":
                    return SqlDbType.BigInt;
                case "RightName":
                    return SqlDbType.NVarChar;
                case "InterFaceName":
                    return SqlDbType.NVarChar;
                case "Remark":
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
        public void CreateFullSqlParameter(PermitRightData entity, SqlCommand cmd)
        {
            //02:主键(RID)
            cmd.Parameters.Add(new SqlParameter("RID",SqlDbType.BigInt){ Value = entity.RID});
            //03:导航标识(NavigationNID)
            cmd.Parameters.Add(new SqlParameter("NavigationNID",SqlDbType.BigInt){ Value = entity.NavigationNID});
            //04:正确名称(RightName)
            var isNull = string.IsNullOrWhiteSpace(entity.RightName);
            var parameter = new SqlParameter("RightName",SqlDbType.NVarChar , isNull ? 10 : (entity.RightName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.RightName;
            cmd.Parameters.Add(parameter);
            //05:面间名称(InterFaceName)
            isNull = string.IsNullOrWhiteSpace(entity.InterFaceName);
            parameter = new SqlParameter("InterFaceName",SqlDbType.NVarChar , isNull ? 10 : (entity.InterFaceName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.InterFaceName;
            cmd.Parameters.Add(parameter);
            //06:备注(Remark)
            isNull = string.IsNullOrWhiteSpace(entity.Remark);
            parameter = new SqlParameter("Remark",SqlDbType.NVarChar , isNull ? 10 : (entity.Remark).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Remark;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(PermitRightData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(PermitRightData entity, SqlCommand cmd)
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
        /// 许可权的结构语句
        /// </summary>
        private TableSql _tbPermitRightSql = new TableSql
        {
            TableName = "tbPermitRight",
            PimaryKey = "RID"
        };


        /// <summary>
        /// 许可权数据访问对象
        /// </summary>
        private PermitRightDataAccess _permitRights;

        /// <summary>
        /// 许可权数据访问对象
        /// </summary>
        public PermitRightDataAccess PermitRights
        {
            get
            {
                return this._permitRights ?? ( this._permitRights = new PermitRightDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 许可权(tbPermitRight):许可权
        /// </summary>
        public const int Table_PermitRight = 0x0;
    }
}