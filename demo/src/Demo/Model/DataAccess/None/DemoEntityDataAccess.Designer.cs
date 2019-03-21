/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/3/6 17:20:20*/
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
using Agebull.EntityModel.Common;


using Agebull.EntityModel.Interfaces;
using Agebull.EntityModel.MySql;
using Demo.DataModel;
#endregion

namespace Agebull.MicroZero.Demo.DataAccess
{
    /// <summary>
    /// 用于演示实体的作用
    /// </summary>
    public partial class DemoEntityDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public DemoEntityDataAccess()
        {
            Name = DemoEntityData._DataStruct_.EntityName;
            Caption = DemoEntityData._DataStruct_.EntityCaption;
            Description = DemoEntityData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => DemoEntityData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_demo_entity";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_demo_entity";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => DemoEntityData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `Id`,
    `name` AS `Name`,
    `price` AS `Price`,
    `value` AS `Value`,
    `memo` AS `Memo`,
    `type` AS `Type`";
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
INSERT INTO `tb_demo_entity`
(
    `name`,
    `price`,
    `value`,
    `memo`,
    `type`
)
VALUES
(
    ?Name,
    ?Price,
    ?Value,
    ?Memo,
    ?Type
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
UPDATE `tb_demo_entity` SET
       `name` = ?Name,
       `price` = ?Price,
       `value` = ?Value,
       `memo` = ?Memo,
       `type` = ?Type
 WHERE `id` = ?Id;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(DemoEntityData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `tb_demo_entity` SET");
            //名称
            if (data.__EntityStatus.ModifiedProperties[DemoEntityData._DataStruct_.Real_Name] > 0)
                sql.AppendLine("       `name` = ?Name");
            //价格
            if (data.__EntityStatus.ModifiedProperties[DemoEntityData._DataStruct_.Real_Price] > 0)
                sql.AppendLine("       `price` = ?Price");
            //数量
            if (data.__EntityStatus.ModifiedProperties[DemoEntityData._DataStruct_.Real_Value] > 0)
                sql.AppendLine("       `value` = ?Value");
            //注释
            if (data.__EntityStatus.ModifiedProperties[DemoEntityData._DataStruct_.Real_Memo] > 0)
                sql.AppendLine("       `memo` = ?Memo");
            //商品类型
            if (data.__EntityStatus.ModifiedProperties[DemoEntityData._DataStruct_.Real_Type] > 0)
                sql.AppendLine("       `type` = ?Type");
            sql.Append(" WHERE `id` = ?Id;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","Name","Price","Value","Memo","Type" };

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
            { "Name" , "name" },
            { "Price" , "price" },
            { "Value" , "value" },
            { "Memo" , "memo" },
            { "Type" , "type" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,DemoEntityData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._name = reader.GetString(1).ToString();
                if (!reader.IsDBNull(2))
                    entity._price =reader.GetDecimal(2);
                if (!reader.IsDBNull(3))
                    entity._value = (int)reader.GetInt32(3);
                if (!reader.IsDBNull(4))
                    entity._memo = reader.GetString(4).ToString();
                if (!reader.IsDBNull(5))
                    entity._type = (ProductType)reader.GetInt32(5);
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
                case "Name":
                    return MySqlDbType.VarString;
                case "Price":
                    return MySqlDbType.Decimal;
                case "Value":
                    return MySqlDbType.Int32;
                case "Memo":
                    return MySqlDbType.VarString;
                case "Type":
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
        private void CreateFullSqlParameter(DemoEntityData entity, MySqlCommand cmd)
        {
            //02:标识(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:名称(Name)
            var isNull = string.IsNullOrWhiteSpace(entity.Name);
            var parameter = new MySqlParameter("Name",MySqlDbType.VarString , isNull ? 10 : (entity.Name).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Name;
            cmd.Parameters.Add(parameter);
            //04:价格(Price)
            cmd.Parameters.Add(new MySqlParameter("Price",MySqlDbType.Decimal){ Value = entity.Price});
            //05:数量(Value)
            cmd.Parameters.Add(new MySqlParameter("Value",MySqlDbType.Int32){ Value = entity.Value});
            //06:注释(Memo)
            isNull = string.IsNullOrWhiteSpace(entity.Memo);
            parameter = new MySqlParameter("Memo",MySqlDbType.VarString , isNull ? 10 : (entity.Memo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Memo;
            cmd.Parameters.Add(parameter);
            //07:商品类型(Type)
            cmd.Parameters.Add(new MySqlParameter("Type",MySqlDbType.Int32){ Value = (int)entity.Type});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(DemoEntityData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(DemoEntityData entity, MySqlCommand cmd)
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
                return @"
    `id` AS `Id`,
    `name` AS `Name`,
    `price` AS `Price`,
    `value` AS `Value`,
    `memo` AS `Memo`";
            }
        }


        /// <summary>
        /// 载入数据
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="entity">读取数据的实体</param>
        public override void SimpleLoad(MySqlDataReader reader,DemoEntityData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._name = reader.GetString(1).ToString();
                if (!reader.IsDBNull(2))
                    entity._price =reader.GetDecimal(2);
                if (!reader.IsDBNull(3))
                    entity._value = (int)reader.GetInt32(3);
                if (!reader.IsDBNull(4))
                    entity._memo = reader.GetString(4).ToString();
            }
        }
        #endregion

        
    }
    
    partial class ProjectDemoDb
    {


        /// <summary>
        /// 用于演示实体的作用的结构语句
        /// </summary>
        private TableSql _tb_demo_entitySql = new TableSql
        {
            TableName = "tb_demo_entity",
            PimaryKey = "Id"
        };


        /// <summary>
        /// 用于演示实体的作用数据访问对象
        /// </summary>
        private DemoEntityDataAccess _demoEntities;

        /// <summary>
        /// 用于演示实体的作用数据访问对象
        /// </summary>
        public DemoEntityDataAccess DemoEntities
        {
            get
            {
                return this._demoEntities ?? ( this._demoEntities = new DemoEntityDataAccess{ DataBase = this});
            }
        }
    }
}