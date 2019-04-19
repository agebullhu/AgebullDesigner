/*此标记表明此文件可被设计器更新,如果不允许此操作,请删除此行代码.design by:agebull designer date:2019/4/15 10:58:48*/
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

using MySql.Data.MySqlClient;
using Agebull.EntityModel.MySql;

using Agebull.Common;
using Agebull.Common.OAuth;
using Agebull.EntityModel.Common;
using Agebull.EntityModel.Interfaces;
using Agebull.Common.Organizations;
using Agebull.Common.OAuth;

#endregion

namespace Agebull.Common.AppManage.DataAccess
{
    /// <summary>
    /// 页面节点
    /// </summary>
    public partial class PageItemDataAccess
    {
        /// <summary>
        /// 构造
        /// </summary>
        public PageItemDataAccess()
        {
            Name = PageItemData._DataStruct_.EntityName;
            Caption = PageItemData._DataStruct_.EntityCaption;
            Description = PageItemData._DataStruct_.EntityDescription;
        }
        

        #region 基本SQL语句

        /// <summary>
        /// 表的唯一标识
        /// </summary>
        public override int TableId => PageItemData._DataStruct_.EntityIdentity;

        /// <summary>
        /// 读取表名
        /// </summary>
        protected sealed override string ReadTableName
        {
            get
            {
                return @"tb_app_page";
            }
        }

        /// <summary>
        /// 写入表名
        /// </summary>
        protected sealed override string WriteTableName
        {
            get
            {
                return @"tb_app_page";
            }
        }

        /// <summary>
        /// 主键
        /// </summary>
        protected sealed override string PrimaryKey => PageItemData._DataStruct_.EntityPrimaryKey;

        /// <summary>
        /// 全表读取的SQL语句
        /// </summary>
        protected sealed override string FullLoadFields
        {
            get
            {
                return @"
    `id` AS `Id`,
    `app_info_id` AS `AppInfoId`,
    `name` AS `Name`,
    `caption` AS `Caption`,
    `item_type` AS `ItemType`,
    `index` AS `Index`,
    `icon` AS `Icon`,
    `url` AS `Url`,
    `extend_value` AS `ExtendValue`,
    `json` AS `Json`,
    `parent_id` AS `ParentId`,
    `is_show` AS `IsShow`,
    `icon_size` AS `IconSize`,
    `icon_color` AS `IconColor`,
    `memo` AS `Memo`,
    `page_item_id` AS `PageItemId`";
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
INSERT INTO `{ContextWriteTable}`
(
    `id`,
    `app_info_id`,
    `name`,
    `caption`,
    `item_type`,
    `index`,
    `icon`,
    `url`,
    `extend_value`,
    `json`,
    `parent_id`,
    `is_show`,
    `icon_size`,
    `icon_color`,
    `memo`,
    `page_item_id`
)
VALUES
(
    ?Id,
    ?AppInfoId,
    ?Name,
    ?Caption,
    ?ItemType,
    ?Index,
    ?Icon,
    ?Url,
    ?ExtendValue,
    ?Json,
    ?ParentId,
    ?IsShow,
    ?IconSize,
    ?IconColor,
    ?Memo,
    ?PageItemId
);";
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
UPDATE `{ContextWriteTable}` SET
       `id` = ?Id,
       `app_info_id` = ?AppInfoId,
       `name` = ?Name,
       `caption` = ?Caption,
       `item_type` = ?ItemType,
       `index` = ?Index,
       `icon` = ?Icon,
       `url` = ?Url,
       `extend_value` = ?ExtendValue,
       `json` = ?Json,
       `parent_id` = ?ParentId,
       `is_show` = ?IsShow,
       `icon_size` = ?IconSize,
       `icon_color` = ?IconColor,
       `memo` = ?Memo,
       `page_item_id` = ?PageItemId
 WHERE `id` = ?Id;";
            }
        }

        /// <summary>
        /// 取得仅更新的SQL语句
        /// </summary>
        public string GetModifiedSqlCode(PageItemData data)
        {
            if (data.__EntityStatusNull || !data.__EntityStatus.IsModified)
                return ";";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE `{ContextWriteTable}` SET");
            //标识
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_Id] > 0)
                sql.AppendLine("       `id` = ?Id");
            //应用信息外键
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_AppInfoId] > 0)
                sql.AppendLine("       `app_info_id` = ?AppInfoId");
            //名称
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_Name] > 0)
                sql.AppendLine("       `name` = ?Name");
            //标题
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_Caption] > 0)
                sql.AppendLine("       `caption` = ?Caption");
            //节点类型
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_ItemType] > 0)
                sql.AppendLine("       `item_type` = ?ItemType");
            //序号
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_Index] > 0)
                sql.AppendLine("       `index` = ?Index");
            //图标
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_Icon] > 0)
                sql.AppendLine("       `icon` = ?Icon");
            //链接地址
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_Url] > 0)
                sql.AppendLine("       `url` = ?Url");
            //扩展值
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_ExtendValue] > 0)
                sql.AppendLine("       `extend_value` = ?ExtendValue");
            //扩展的JSON配置
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_Json] > 0)
                sql.AppendLine("       `json` = ?Json");
            //上级标识
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_ParentId] > 0)
                sql.AppendLine("       `parent_id` = ?ParentId");
            //是否显示
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_IsShow] > 0)
                sql.AppendLine("       `is_show` = ?IsShow");
            //图标大小
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_IconSize] > 0)
                sql.AppendLine("       `icon_size` = ?IconSize");
            //图标颜色
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_IconColor] > 0)
                sql.AppendLine("       `icon_color` = ?IconColor");
            //备注
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_Memo] > 0)
                sql.AppendLine("       `memo` = ?Memo");
            //页面标识
            if (data.__EntityStatus.ModifiedProperties[PageItemData._DataStruct_.Real_PageItemId] > 0)
                sql.AppendLine("       `page_item_id` = ?PageItemId");
            sql.Append(" WHERE `id` = ?Id;");
            return sql.ToString();
        }

        #endregion


        #region 字段

        /// <summary>
        ///  所有字段
        /// </summary>
        static string[] _fields = new string[]{ "Id","AppInfoId","Name","Caption","ItemType","Index","Icon","Url","ExtendValue","Json","ParentId","IsShow","IconSize","IconColor","Memo","PageItemId" };

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
            { "AppInfoId" , "app_info_id" },
            { "app_info_id" , "app_info_id" },
            { "Name" , "name" },
            { "Caption" , "caption" },
            { "ItemType" , "item_type" },
            { "item_type" , "item_type" },
            { "Index" , "index" },
            { "Icon" , "icon" },
            { "Url" , "url" },
            { "ExtendValue" , "extend_value" },
            { "extend_value" , "extend_value" },
            { "Json" , "json" },
            { "ParentId" , "parent_id" },
            { "parent_id" , "parent_id" },
            { "IsShow" , "is_show" },
            { "is_show" , "is_show" },
            { "IconSize" , "icon_size" },
            { "icon_size" , "icon_size" },
            { "IconColor" , "icon_color" },
            { "icon_color" , "icon_color" },
            { "Memo" , "memo" },
            { "PageItemId" , "page_item_id" },
            { "page_item_id" , "page_item_id" }
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
        protected sealed override void LoadEntity(MySqlDataReader reader,PageItemData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
                if (!reader.IsDBNull(0))
                    entity._id = (long)reader.GetInt64(0);
                if (!reader.IsDBNull(1))
                    entity._appInfoId = (long)reader.GetInt64(1);
                if (!reader.IsDBNull(2))
                    entity._name = reader.GetString(2).ToString();
                if (!reader.IsDBNull(3))
                    entity._caption = reader.GetString(3).ToString();
                if (!reader.IsDBNull(4))
                    entity._itemType = (PageItemType)reader.GetInt32(4);
                if (!reader.IsDBNull(5))
                    entity._index = (int)reader.GetInt32(5);
                if (!reader.IsDBNull(6))
                    entity._icon = reader.GetString(6).ToString();
                if (!reader.IsDBNull(7))
                    entity._url = reader.GetString(7).ToString();
                if (!reader.IsDBNull(8))
                    entity._extendValue = reader.GetString(8).ToString();
                if (!reader.IsDBNull(9))
                    entity._json = /*(LONGTEXT)*/reader.GetValue(9).ToString();
                if (!reader.IsDBNull(10))
                    entity._parentId = (long)reader.GetInt64(10);
                if (!reader.IsDBNull(11))
                    entity._isShow = (bool)reader.GetBoolean(11);
                if (!reader.IsDBNull(12))
                    entity._iconSize = (int)reader.GetInt32(12);
                if (!reader.IsDBNull(13))
                    entity._iconColor = reader.GetString(13).ToString();
                if (!reader.IsDBNull(14))
                    entity._memo = reader.GetString(14).ToString();
                if (!reader.IsDBNull(15))
                    entity._pageItemId = (long)reader.GetInt64(15);
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
                case "AppInfoId":
                    return MySqlDbType.Int64;
                case "Name":
                    return MySqlDbType.VarString;
                case "Caption":
                    return MySqlDbType.VarString;
                case "ItemType":
                    return MySqlDbType.Int32;
                case "Index":
                    return MySqlDbType.Int32;
                case "Icon":
                    return MySqlDbType.VarString;
                case "Url":
                    return MySqlDbType.Text;
                case "ExtendValue":
                    return MySqlDbType.VarString;
                case "Json":
                    return MySqlDbType.LongText;
                case "ParentId":
                    return MySqlDbType.Int64;
                case "IsShow":
                    return MySqlDbType.VarString;
                case "IconSize":
                    return MySqlDbType.Int32;
                case "IconColor":
                    return MySqlDbType.VarString;
                case "Memo":
                    return MySqlDbType.Text;
                case "PageItemId":
                    return MySqlDbType.Int64;
            }
            return MySqlDbType.VarChar;
        }


        /// <summary>
        /// 设置插入数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        /// <returns>返回真说明要取主键</returns>
        public void CreateFullSqlParameter(PageItemData entity, MySqlCommand cmd)
        {
            //02:标识(Id)
            cmd.Parameters.Add(new MySqlParameter("Id",MySqlDbType.Int64){ Value = entity.Id});
            //03:应用信息外键(AppInfoId)
            cmd.Parameters.Add(new MySqlParameter("AppInfoId",MySqlDbType.Int64){ Value = entity.AppInfoId});
            //04:名称(Name)
            var isNull = string.IsNullOrWhiteSpace(entity.Name);
            var parameter = new MySqlParameter("Name",MySqlDbType.VarString , isNull ? 10 : (entity.Name).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Name;
            cmd.Parameters.Add(parameter);
            //05:标题(Caption)
            isNull = string.IsNullOrWhiteSpace(entity.Caption);
            parameter = new MySqlParameter("Caption",MySqlDbType.VarString , isNull ? 10 : (entity.Caption).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Caption;
            cmd.Parameters.Add(parameter);
            //06:节点类型(ItemType)
            cmd.Parameters.Add(new MySqlParameter("ItemType",MySqlDbType.Int32){ Value = (int)entity.ItemType});
            //07:序号(Index)
            cmd.Parameters.Add(new MySqlParameter("Index",MySqlDbType.Int32){ Value = entity.Index});
            //08:图标(Icon)
            isNull = string.IsNullOrWhiteSpace(entity.Icon);
            parameter = new MySqlParameter("Icon",MySqlDbType.VarString , isNull ? 10 : (entity.Icon).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Icon;
            cmd.Parameters.Add(parameter);
            //09:链接地址(Url)
            isNull = string.IsNullOrWhiteSpace(entity.Url);
            parameter = new MySqlParameter("Url",MySqlDbType.Text , isNull ? 10 : (entity.Url).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Url;
            cmd.Parameters.Add(parameter);
            //10:扩展值(ExtendValue)
            isNull = string.IsNullOrWhiteSpace(entity.ExtendValue);
            parameter = new MySqlParameter("ExtendValue",MySqlDbType.VarString , isNull ? 10 : (entity.ExtendValue).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.ExtendValue;
            cmd.Parameters.Add(parameter);
            //11:扩展的JSON配置(Json)
            isNull = string.IsNullOrWhiteSpace(entity.Json);
            parameter = new MySqlParameter("Json",MySqlDbType.LongText , isNull ? 10 : (entity.Json).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Json;
            cmd.Parameters.Add(parameter);
            //12:上级标识(ParentId)
            cmd.Parameters.Add(new MySqlParameter("ParentId",MySqlDbType.Int64){ Value = entity.ParentId});
            //13:是否显示(IsShow)
            cmd.Parameters.Add(new MySqlParameter("IsShow",MySqlDbType.Byte) { Value = entity.IsShow ? (byte)1 : (byte)0 });
            //14:图标大小(IconSize)
            cmd.Parameters.Add(new MySqlParameter("IconSize",MySqlDbType.Int32){ Value = entity.IconSize});
            //15:图标颜色(IconColor)
            isNull = string.IsNullOrWhiteSpace(entity.IconColor);
            parameter = new MySqlParameter("IconColor",MySqlDbType.VarString , isNull ? 10 : (entity.IconColor).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.IconColor;
            cmd.Parameters.Add(parameter);
            //16:备注(Memo)
            isNull = string.IsNullOrWhiteSpace(entity.Memo);
            parameter = new MySqlParameter("Memo",MySqlDbType.Text , isNull ? 10 : (entity.Memo).Length);
            if(isNull)
                parameter.Value = DBNull.Value;
            else
                parameter.Value = entity.Memo;
            cmd.Parameters.Add(parameter);
            //17:页面标识(PageItemId)
            cmd.Parameters.Add(new MySqlParameter("PageItemId",MySqlDbType.Int64){ Value = entity.PageItemId});
        }


        /// <summary>
        /// 设置更新数据的命令
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="cmd">命令</param>
        protected sealed override void SetUpdateCommand(PageItemData entity, MySqlCommand cmd)
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
        protected sealed override bool SetInsertCommand(PageItemData entity, MySqlCommand cmd)
        {
            cmd.CommandText = InsertSqlCode;
            CreateFullSqlParameter(entity, cmd);
            return false;
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
        public override void SimpleLoad(MySqlDataReader reader,PageItemData entity)
        {
            using (new EditScope(entity.__EntityStatus, EditArrestMode.All, false))
            {
            }
        }
        #endregion

        
    }
    
    partial class AppManageDb
    {


        /// <summary>
        /// 页面节点的结构语句
        /// </summary>
        private TableSql _tb_app_pageSql = new TableSql
        {
            TableName = "tb_app_page",
            PimaryKey = "Id"
        };


        /// <summary>
        /// 页面节点数据访问对象
        /// </summary>
        private PageItemDataAccess _pageItems;

        /// <summary>
        /// 页面节点数据访问对象
        /// </summary>
        public PageItemDataAccess PageItems
        {
            get
            {
                return this._pageItems ?? ( this._pageItems = new PageItemDataAccess{ DataBase = this});
            }
        }
    }
}