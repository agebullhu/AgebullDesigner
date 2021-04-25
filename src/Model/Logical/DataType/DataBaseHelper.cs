using Agebull.EntityModel.Config.V2021;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     实体实现接口的命令
    /// </summary>
    public class DataBaseHelper
    {
        /// <summary>
        /// 到标准数据表名称
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string ToTableName(IEntityConfig entity)
        {
            var style = CodeStyleManager.GetDatabaseStyle(entity.Project.CodeStyle, entity.Project.DbType);
            return style.FormatTableName(entity);
        }

        /// <summary>
        /// 到标准数据表名称
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string ToViewName(IEntityConfig entity)
        {
            var style = CodeStyleManager.GetDatabaseStyle(entity.Project.CodeStyle, entity.Project.DbType);
            return style.FormatViewName(entity);
        }

        /// <summary>
        /// 到标准数据表名称
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string ToDbFieldName(IPropertyConfig field)
        {
            var style = CodeStyleManager.GetDatabaseStyle(field.Parent.Project.CodeStyle, field.Parent.Project.DbType);
            return style?.FormatFieldName(field);
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public static void CheckFieldLink(DataTableConfig entity)
        {
            CheckFieldLink(entity.Fields);
        }

        /// <summary>
        /// 检查字段关联
        /// </summary>
        public static bool CheckFieldLink(IEnumerable<DataBaseFieldConfig> fields)
        {
            if (fields == null)
                return false;
            bool hase = false;
            foreach (var field in fields)
            {
                if (CheckFieldLink(field)) hase = true;

            }
            return hase;
        }

        /// <summary>
        /// 检查字段关联
        /// </summary>
        public static bool CheckFieldLink(DataBaseFieldConfig field)
        {
            bool hase = false;
            var entity = field.Parent.Entity;

            if (string.IsNullOrWhiteSpace(field.LinkTable) ||
                field.LinkTable == field.Parent.Name || field.LinkTable == field.Parent.ReadTableName ||
                field.LinkTable == field.Parent.SaveTableName)
            {
                SetNoLink(field);
                return hase;
            }

            var table = entity.Project.Find(field.LinkTable) ?? GlobalConfig.GetEntity(field.LinkTable);

            if (table == null || table == entity)
            {
                SetNoLink(field);
                return hase;
            }
            var pro = field.IsLinkKey ? table.DataTable.PrimaryField : table.DataTable.Find(field.LinkField);
            if (pro == null && !field.Option.ReferenceKey.IsMissing())
            {
                pro = GlobalConfig.GetConfig<FieldConfig>(field.Option.ReferenceKey).DataBaseField;
            }
            if (pro == null || pro == field.Property || pro.Entity == entity)
            {
                SetNoLink(field);
                return hase;
            }
            hase = true;
            field.Option.ReferenceConfig = pro;
            field.Option.IsLink = true;

            field.IsLinkField = true;
            field.IsLinkCaption = pro.IsCaption;
            field.IsLinkKey = pro.IsPrimaryKey;
            field.Property.IsCompute = !field.IsLinkKey;
            field.LinkTable = table.Name;
            field.LinkField = pro.Name;
            field.NoStorage = false;

            field.FieldType = pro.FieldType;
            field.Property.ArrayLen = pro.Property.ArrayLen;
            field.Datalen = pro.Datalen;
            field.Scale = pro.Scale;

            if (field.IsLinkKey)
            {
                field.DbNullable = false;
                field.IsDbIndex = true;
                field.Property.Nullable = false;
            }

            Trace.WriteLine($"    {(field.IsLinkKey ? 'F' : 'L')}:{field.Caption}({field.Name})");
            return hase;
        }

        private static void SetNoLink(DataBaseFieldConfig field)
        {
            Trace.WriteLine($"X    :{field.Caption}({field.Name})");
            field.LinkTable = field.LinkField = null;
            field.Option.ReferenceKey = null;
            field.IsLinkField = field.IsLinkKey = field.IsLinkCaption = false;
        }
    }
}
