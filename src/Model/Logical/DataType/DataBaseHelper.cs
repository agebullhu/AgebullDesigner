using Agebull.Common;
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
        public static string ToTableName(EntityConfig entity)
        {
            var style = CodeStyleManager.GetDatabaseStyle(entity.Parent.CodeStyle, entity.Parent.DbType);
            return style.FormatTableName(entity); 
        }

        /// <summary>
        /// 到标准数据表名称
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string ToViewName(EntityConfig entity)
        {
            var style = CodeStyleManager.GetDatabaseStyle(entity.Parent.CodeStyle, entity.Parent.DbType);
            return style.FormatViewName(entity);
        }

        /// <summary>
        /// 到标准数据表名称
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string ToDbFieldName(IFieldConfig field)
        {
            var style = CodeStyleManager.GetDatabaseStyle(field.Parent.Parent.CodeStyle, field.Parent.Parent.DbType);
            return style.FormatFieldName(field);
        }

        /// <summary>
        /// 执行器
        /// </summary>
        public static void CheckFieldLink(EntityConfig entity)
        {
            CheckFieldLink(entity.Properties);
        }

        /// <summary>
        /// 检查字段关联
        /// </summary>
        public static bool CheckFieldLink(IEnumerable<FieldConfig> fields)
        {
            bool hase = false;
            foreach (var field in fields)
            {
                var entity = field.Entity;

                if (string.IsNullOrWhiteSpace(field.LinkTable) ||
                    field.LinkTable == entity.Name || field.LinkTable == entity.ReadTableName ||
                    field.LinkTable == entity.SaveTableName)
                {
                    SetNoLink(field);
                    continue;
                }

                var table = entity.Parent.Find(field.LinkTable) ?? GlobalConfig.GetEntity(field.LinkTable);

                if(table == null || table == entity)
                {
                    SetNoLink(field);
                    continue;
                }
                FieldConfig pro = field.IsLinkKey ? table.PrimaryColumn : table.Find(field.LinkField); 
                if(pro == null && !field.Option.ReferenceKey.IsEmpty())
                {
                    pro = GlobalConfig.GetConfig<FieldConfig>(field.Option.ReferenceKey);
                }
                if (pro == null || pro == field || pro.Entity == entity)
                {
                    SetNoLink(field);
                    continue;
                }
                hase = true;
                field.Option.ReferenceConfig = pro;
                field.Option.IsLink = true;

                field.IsLinkField = true;
                field.IsLinkCaption = pro.IsCaption;
                field.IsLinkKey = pro.IsPrimaryKey;
                field.IsCompute = !field.IsLinkKey;
                field.LinkTable = table.Name;
                field.LinkField = pro.Name;
                field.NoStorage = false;

                field.DbType = pro.DbType;
                field.ArrayLen = pro.ArrayLen;
                field.Datalen = pro.Datalen;
                field.Scale = pro.Scale;

                if (field.IsLinkKey)
                {
                    field.DbNullable = false;
                    field.IsDbIndex = true;
                    field.Nullable = false;
                }

                Trace.WriteLine($"    {(field.IsLinkKey ? 'F' : 'L')}:{field.Caption}({field.Name})");


            }
            return hase;
        }

        private static void SetNoLink(FieldConfig field)
        {
            Trace.WriteLine($"X    :{field.Caption}({field.Name})");
            field.LinkTable = field.LinkField = null;
            field.Option.ReferenceKey = null;
            field.IsLinkField = field.IsLinkKey = field.IsLinkCaption = false;
        }
    }
}
