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
    /// 组织类型
    /// </summary>
    public partial class OrganizationTypeDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_OrganizationType;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbOrganizationType";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbOrganizationType";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"TID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [TID] AS [TID],
    [TypeName] AS [TypeName],
    [Remarks] AS [Remarks]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [TID],
    [TypeName],
    [Remarks]
)
VALUES
(
    @TID,
    @TypeName,
    @Remarks
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [TID] = @TID,
       [TypeName] = @TypeName,
       [Remarks] = @Remarks
 WHERE [TID] = @TID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(OrganizationTypeData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[OrganizationTypeData._DataStruct_.Real_TID] > 0)
                sql.AppendLine("       [TID] = @TID");
            //类型Name
            if (data.__EntityStatus.ModifiedProperties[OrganizationTypeData._DataStruct_.Real_TypeName] > 0)
                sql.AppendLine("       [TypeName] = @TypeName");
            //评论
            if (data.__EntityStatus.ModifiedProperties[OrganizationTypeData._DataStruct_.Real_Remarks] > 0)
                sql.AppendLine("       [Remarks] = @Remarks");
            sql.Append(" WHERE [TID] = @TID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "TID","TypeName","Remarks" };

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
            { "TypeName" , "TypeName" },
            { "Remarks" , "Remarks" },
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
        protected sealed override void LoadEntity(SqlDataReader reader,OrganizationTypeData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._tID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._typeName = reader.GetString(1);
                if (!reader.IsDBNull(2))
                    entity._remarks = reader.GetString(2);
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
                case "TypeName":
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
        public void CreateFullSqlParameter(OrganizationTypeData entity, SqlCommand cmd)
        {
            //02:主键(TID)
            cmd.Parameters.Add(new SqlParameter("TID",SqlDbType.BigInt){ Value = entity.TID});
            //03:类型Name(TypeName)
            var isNull = string.IsNullOrWhiteSpace(entity.TypeName);
            var parameter = new SqlParameter("TypeName",SqlDbType.NVarChar , isNull ? 10 : (entity.TypeName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.TypeName;
            cmd.Parameters.Add(parameter);
            //04:评论(Remarks)
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
        protected sealed override void SetUpdateCommand(OrganizationTypeData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(OrganizationTypeData entity, SqlCommand cmd)
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
        /// 组织类型的结构语句
        /// </summary>
        private TableSql _tbOrganizationTypeSql = new TableSql
        {
            TableName = "tbOrganizationType",
            PimaryKey = "TID"
        };


        /// <summary>
        /// 组织类型数据访问对象
        /// </summary>
        private OrganizationTypeDataAccess _organizationTypes;

        /// <summary>
        /// 组织类型数据访问对象
        /// </summary>
        public OrganizationTypeDataAccess OrganizationTypes
        {
            get
            {
                return this._organizationTypes ?? ( this._organizationTypes = new OrganizationTypeDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 组织类型(tbOrganizationType):组织类型
        /// </summary>
        public const int Table_OrganizationType = 0x0;
    }
}