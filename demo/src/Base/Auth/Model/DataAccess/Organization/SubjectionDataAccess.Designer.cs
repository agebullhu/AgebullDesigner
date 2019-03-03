/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2018/11/14 19:10:09*/
#region
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

using Agebull.Common;
using Agebull.Common.DataModel;
using Agebull.Common.Rpc;
using Agebull.Common.WebApi;
using Gboxt.Common;
using Gboxt.Common.DataModel;


using Gboxt.Common.DataModel.Extends;
using Gboxt.Common.DataModel.MySql;
#endregion

namespace Agebull.Common.OAuth.DataAccess
{
    /// <summary>
    /// 行级权限关联
    /// </summary>
    internal partial class SubjectionDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public SubjectionDataAccess()
        {
            Name = SubjectionData._DataStruct_.EntityName;
            Caption = SubjectionData._DataStruct_.EntityCaption;
            Description = SubjectionData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => SubjectionData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_org_subjection";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_org_subjection";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => SubjectionData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `Id`,
    `subjection_type` AS `SubjectionType`,
    `master_id` AS `MasterId`,
    `slave_id` AS `SlaveId`,
    `subjection_sreen` AS `SubjectionSreen`";
            }
        }

        

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode
        {
            get
            {
                return @"
INSERT INTO `tb_org_subjection`
(
    `subjection_type`,
    `master_id`,
    `slave_id`,
    `subjection_sreen`
)
VALUES
(
    ?SubjectionType,
    ?MasterId,
    ?SlaveId,
    ?SubjectionSreen
);
SELECT @@IDENTITY;";
            }
        }

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode
        {
            get
            {
                return @"
UPDATE `tb_org_subjection` SET
       `subjection_type` = ?SubjectionType,
       `master_id` = ?MasterId,
       `slave_id` = ?SlaveId,
       `subjection_sreen` = ?SubjectionSreen
 WHERE `id` = ?Id;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(SubjectionData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_org_subjection` SET");
            //关联类型
            if (data.__EntityStatus.ModifiedProperties[SubjectionData._DataStruct_.Real_SubjectionType] > 0)
                sql.AppendLine("       `subjection_type` = ?SubjectionType");
            //主键
            if (data.__EntityStatus.ModifiedProperties[SubjectionData._DataStruct_.Real_MasterId] > 0)
                sql.AppendLine("       `master_id` = ?MasterId");
            //关联
            if (data.__EntityStatus.ModifiedProperties[SubjectionData._DataStruct_.Real_SlaveId] > 0)
                sql.AppendLine("       `slave_id` = ?SlaveId");
            //关联场景
            if (data.__EntityStatus.ModifiedProperties[SubjectionData._DataStruct_.Real_SubjectionSreen] > 0)
                sql.AppendLine("       `subjection_sreen` = ?SubjectionSreen");
            sql.Append(" WHERE `id` = ?Id;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","SubjectionType","MasterId","SlaveId","SubjectionSreen" };

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
            { "Id" , "id" },
            { "SubjectionType" , "subjection_type" },
            { "subjection_type" , "subjection_type" },
            { "MasterId" , "master_id" },
            { "master_id" , "master_id" },
            { "SlaveId" , "slave_id" },
            { "slave_id" , "slave_id" },
            { "SubjectionSreen" , "subjection_sreen" },
            { "subjection_sreen" , "subjection_sreen" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,SubjectionData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._subjectionType = (SubjectionType)reader.GetInt32(1);
                if (!reader.IsDBNull(2))
                    entity._masterId = (long)reader.GetInt64(2);
                if (!reader.IsDBNull(3))
                    entity._slaveId = (long)reader.GetInt64(3);
                if (!reader.IsDBNull(4))
                    entity._subjectionSreen = (int)reader.GetInt32(4);
            }
        }

        /// <summary>
        /// 得到字段的DbType类型
        /// </summary>
        /// <param name="field">字段名称</param>
        /// <returns>参数</returns>
        protected sealed override MySqlDbType GetDbType(string field)
        {
            switch (field)
            {
                case "Id":
                    return MySqlDbType.Int64;
                case "SubjectionType":
                    return MySqlDbType.Int32;
                case "MasterId":
                    return MySqlDbType.Int64;
                case "SlaveId":
                    return MySqlDbType.Int64;
                case "SubjectionSreen":
                    return MySqlDbType.Int32;
            }
            return MySqlDbType.VarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        private void CreateFullSqlParameter(SubjectionData entity, MySqlCommand cmd)
        {
            //02:标识(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:关联类型(SubjectionType)
            cmd.Parameters.Add(new MySqlParameter("SubjectionType",MySqlDbType.Int32){ Value = (int)entity.SubjectionType});
            //04:主键(MasterId)
            cmd.Parameters.Add(new MySqlParameter("MasterId",MySqlDbType.Int64){ Value = entity.MasterId});
            //05:关联(SlaveId)
            cmd.Parameters.Add(new MySqlParameter("SlaveId",MySqlDbType.Int64){ Value = entity.SlaveId});
            //06:关联场景(SubjectionSreen)
            cmd.Parameters.Add(new MySqlParameter("SubjectionSreen",MySqlDbType.Int32){ Value = entity.SubjectionSreen});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(SubjectionData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(SubjectionData entity, MySqlCommand cmd)
        {
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return true;
        }

        #endregion

        #region 简单读取

        /// <summary>
        /// SQL语句
        /// </summary>
        public override string SimpleFields
        {
            get
            {
                return @"";
            }
        }


        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="entity">读取数据的实体</param>
        public override void SimpleLoad(MySqlDataReader reader,SubjectionData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
            }
        }
        #endregion

        
    }
    
    partial class UserCenterDb
    {


        /// <summary>
        /// 行级权限关联的结构语句
        /// </summary>
        private TableSql _tb_org_subjectionSql = new TableSql
        {
            TableName = "tb_org_subjection",
            PimaryKey = "Id"
        };


        /// <summary>
        /// 行级权限关联数据访问对象
        /// </summary>
        private SubjectionDataAccess _subjections;

        /// <summary>
        /// 行级权限关联数据访问对象
        /// </summary>
        internal SubjectionDataAccess Subjections
        {
            get
            {
                return this._subjections ?? ( this._subjections = new SubjectionDataAccess{ DataBase = this});
            }
        }
    }
}