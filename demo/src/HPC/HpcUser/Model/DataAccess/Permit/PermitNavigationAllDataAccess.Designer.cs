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
    /// 导航表
    /// </summary>
    public partial class PermitNavigationAllDataAccess
    {
        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => HpcSqlServerDb.Table_PermitNavigationAll;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName => @"tbPermitNavigationAll";

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName => @"tbPermitNavigationAll";

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey =>  @"NID";

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields => @"
    [NID] AS [NID],
    [SiteSID] AS [SiteSID],
    [NavType] AS [NavType],
    [MenuName] AS [MenuName],
    [MenuTitle] AS [MenuTitle],
    [MenuUrl] AS [MenuUrl],
    [PID] AS [PID],
    [Icon] AS [Icon],
    [IconSize] AS [IconSize],
    [IconColor] AS [IconColor],
    [Sort] AS [Sort],
    [IsShow] AS [IsShow],
    [Remark] AS [Remark],
    [Level] AS [Level]";

        /// <summary>
        /// 插入的SQL语句
        /// </summary>
        protected sealed override string InsertSqlCode => $@"
INSERT INTO [{ContextWriteTable}]
(
    [NID],
    [SiteSID],
    [NavType],
    [MenuName],
    [MenuTitle],
    [MenuUrl],
    [PID],
    [Icon],
    [IconSize],
    [IconColor],
    [Sort],
    [IsShow],
    [Remark],
    [Level]
)
VALUES
(
    @NID,
    @SiteSID,
    @NavType,
    @MenuName,
    @MenuTitle,
    @MenuUrl,
    @PID,
    @Icon,
    @IconSize,
    @IconColor,
    @Sort,
    @IsShow,
    @Remark,
    @Level
);";

        /// <summary>
        /// 全部更新的SQL语句
        /// </summary>
        protected sealed override string UpdateSqlCode => $@"
UPDATE [{ContextWriteTable}] SET
       [NID] = @NID,
       [SiteSID] = @SiteSID,
       [NavType] = @NavType,
       [MenuName] = @MenuName,
       [MenuTitle] = @MenuTitle,
       [MenuUrl] = @MenuUrl,
       [PID] = @PID,
       [Icon] = @Icon,
       [IconSize] = @IconSize,
       [IconColor] = @IconColor,
       [Sort] = @Sort,
       [IsShow] = @IsShow,
       [Remark] = @Remark,
       [Level] = @Level
 WHERE [NID] = @NID;";

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(PermitNavigationAllData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"UPDATE [{ContextWriteTable}] SET");
            //主键
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_NID] > 0)
                sql.AppendLine("       [NID] = @NID");
            //站点标识
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_SiteSID] > 0)
                sql.AppendLine("       [SiteSID] = @SiteSID");
            //导航类型
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_NavType] > 0)
                sql.AppendLine("       [NavType] = @NavType");
            //菜单名
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_MenuName] > 0)
                sql.AppendLine("       [MenuName] = @MenuName");
            //菜单标题
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_MenuTitle] > 0)
                sql.AppendLine("       [MenuTitle] = @MenuTitle");
            //菜单链接
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_MenuUrl] > 0)
                sql.AppendLine("       [MenuUrl] = @MenuUrl");
            //主键
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_PID] > 0)
                sql.AppendLine("       [PID] = @PID");
            //偶像
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_Icon] > 0)
                sql.AppendLine("       [Icon] = @Icon");
            //图标大小
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_IconSize] > 0)
                sql.AppendLine("       [IconSize] = @IconSize");
            //图标颜色
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_IconColor] > 0)
                sql.AppendLine("       [IconColor] = @IconColor");
            //排序
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_Sort] > 0)
                sql.AppendLine("       [Sort] = @Sort");
            //是否显示
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_IsShow] > 0)
                sql.AppendLine("       [IsShow] = @IsShow");
            //备注
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_Remark] > 0)
                sql.AppendLine("       [Remark] = @Remark");
            //等级
            if (data.__EntityStatus.ModifiedProperties[PermitNavigationAllData._DataStruct_.Real_Level] > 0)
                sql.AppendLine("       [Level] = @Level");
            sql.Append(" WHERE [NID] = @NID;");
            return sql.ToString();
        }

        #endregion

        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "NID","SiteSID","NavType","MenuName","MenuTitle","MenuUrl","PID","Icon","IconSize","IconColor","Sort","IsShow","Remark","Level" };

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
            { "NID" , "NID" },
            { "SiteSID" , "SiteSID" },
            { "NavType" , "NavType" },
            { "MenuName" , "MenuName" },
            { "MenuTitle" , "MenuTitle" },
            { "MenuUrl" , "MenuUrl" },
            { "PID" , "PID" },
            { "Icon" , "Icon" },
            { "IconSize" , "IconSize" },
            { "IconColor" , "IconColor" },
            { "Sort" , "Sort" },
            { "IsShow" , "IsShow" },
            { "Remark" , "Remark" },
            { "Level" , "Level" },
            { "Id" , "NID" }
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
        protected sealed override void LoadEntity(SqlDataReader reader,PermitNavigationAllData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._nID = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._siteSID = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._navType = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    entity._menuName = reader.GetString(3);
                if (!reader.IsDBNull(4))
                    entity._menuTitle = reader.GetString(4);
                if (!reader.IsDBNull(5))
                    entity._menuUrl = reader.GetString(5).ToString();
                if (!reader.IsDBNull(6))
                    entity._pID = (long)reader.GetInt64(6);
                if (!reader.IsDBNull(7))
                    entity._icon = reader.GetString(7).ToString();
                if (!reader.IsDBNull(8))
                    entity._iconSize = reader.GetInt32(8);
                if (!reader.IsDBNull(9))
                    entity._iconColor = reader.GetString(9);
                if (!reader.IsDBNull(10))
                    entity._sort = reader.GetInt32(10);
                if (!reader.IsDBNull(11))
                    entity._isShow = (bool)reader.GetBoolean(11);
                if (!reader.IsDBNull(12))
                    entity._remark = reader.GetString(12);
                if (!reader.IsDBNull(13))
                    entity._level = reader.GetString(13);
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
                case "NID":
                    return SqlDbType.BigInt;
                case "SiteSID":
                    return SqlDbType.BigInt;
                case "NavType":
                    return SqlDbType.NVarChar;
                case "MenuName":
                    return SqlDbType.NVarChar;
                case "MenuTitle":
                    return SqlDbType.NVarChar;
                case "MenuUrl":
                    return SqlDbType.NVarChar;
                case "PID":
                    return SqlDbType.BigInt;
                case "Icon":
                    return SqlDbType.NVarChar;
                case "IconSize":
                    return SqlDbType.Int;
                case "IconColor":
                    return SqlDbType.NVarChar;
                case "Sort":
                    return SqlDbType.Int;
                case "IsShow":
                    return SqlDbType.Bit;
                case "Remark":
                    return SqlDbType.NVarChar;
                case "Level":
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
        public void CreateFullSqlParameter(PermitNavigationAllData entity, SqlCommand cmd)
        {
            //02:主键(NID)
            cmd.Parameters.Add(new SqlParameter("NID",SqlDbType.BigInt){ Value = entity.NID});
            //03:站点标识(SiteSID)
            cmd.Parameters.Add(new SqlParameter("SiteSID",SqlDbType.BigInt){ Value = entity.SiteSID});
            //04:导航类型(NavType)
            var isNull = string.IsNullOrWhiteSpace(entity.NavType);
            var parameter = new SqlParameter("NavType",SqlDbType.NVarChar , isNull ? 10 : (entity.NavType).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.NavType;
            cmd.Parameters.Add(parameter);
            //05:菜单名(MenuName)
            isNull = string.IsNullOrWhiteSpace(entity.MenuName);
            parameter = new SqlParameter("MenuName",SqlDbType.NVarChar , isNull ? 10 : (entity.MenuName).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.MenuName;
            cmd.Parameters.Add(parameter);
            //06:菜单标题(MenuTitle)
            isNull = string.IsNullOrWhiteSpace(entity.MenuTitle);
            parameter = new SqlParameter("MenuTitle",SqlDbType.NVarChar , isNull ? 10 : (entity.MenuTitle).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.MenuTitle;
            cmd.Parameters.Add(parameter);
            //07:菜单链接(MenuUrl)
            isNull = string.IsNullOrWhiteSpace(entity.MenuUrl);
            parameter = new SqlParameter("MenuUrl",SqlDbType.NVarChar , isNull ? 10 : (entity.MenuUrl).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.MenuUrl;
            cmd.Parameters.Add(parameter);
            //08:主键(PID)
            cmd.Parameters.Add(new SqlParameter("PID",SqlDbType.BigInt){ Value = entity.PID});
            //09:偶像(Icon)
            isNull = string.IsNullOrWhiteSpace(entity.Icon);
            parameter = new SqlParameter("Icon",SqlDbType.NVarChar , isNull ? 10 : (entity.Icon).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Icon;
            cmd.Parameters.Add(parameter);
            //10:图标大小(IconSize)
            cmd.Parameters.Add(new SqlParameter("IconSize",SqlDbType.Int){ Value = entity.IconSize});
            //11:图标颜色(IconColor)
            isNull = string.IsNullOrWhiteSpace(entity.IconColor);
            parameter = new SqlParameter("IconColor",SqlDbType.NVarChar , isNull ? 10 : (entity.IconColor).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.IconColor;
            cmd.Parameters.Add(parameter);
            //12:排序(Sort)
            cmd.Parameters.Add(new SqlParameter("Sort",SqlDbType.Int){ Value = entity.Sort});
            //13:是否显示(IsShow)
            cmd.Parameters.Add(new SqlParameter("IsShow",SqlDbType.Bit){ Value = entity.IsShow});
            //14:备注(Remark)
            isNull = string.IsNullOrWhiteSpace(entity.Remark);
            parameter = new SqlParameter("Remark",SqlDbType.NVarChar , isNull ? 10 : (entity.Remark).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Remark;
            cmd.Parameters.Add(parameter);
            //15:等级(Level)
            isNull = string.IsNullOrWhiteSpace(entity.Level);
            parameter = new SqlParameter("Level",SqlDbType.NVarChar , isNull ? 10 : (entity.Level).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Level;
            cmd.Parameters.Add(parameter);
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(PermitNavigationAllData entity, SqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(PermitNavigationAllData entity, SqlCommand cmd)
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
        /// 导航表的结构语句
        /// </summary>
        private TableSql _tbPermitNavigationAllSql = new TableSql
        {
            TableName = "tbPermitNavigationAll",
            PimaryKey = "NID"
        };


        /// <summary>
        /// 导航表数据访问对象
        /// </summary>
        private PermitNavigationAllDataAccess _permitNavigationAlls;

        /// <summary>
        /// 导航表数据访问对象
        /// </summary>
        public PermitNavigationAllDataAccess PermitNavigationAlls
        {
            get
            {
                return this._permitNavigationAlls ?? ( this._permitNavigationAlls = new PermitNavigationAllDataAccess{ DataBase = this});
            }
        }


        /// <summary>
        /// 导航表(tbPermitNavigationAll):导航表
        /// </summary>
        public const int Table_PermitNavigationAll = 0x0;
    }
}